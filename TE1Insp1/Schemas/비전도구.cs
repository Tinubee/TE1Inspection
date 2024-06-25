using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.CNLSearch;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
using Cogutils;
using MvUtils;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace TE1.Schemas
{
    public class 비전도구
    {
        #region 기본설정
        public String 로그영역 = "Vision Tool";
        public 모델구분 모델구분 = 모델구분.UPR3P24S;
        public 카메라구분 카메라 = 카메라구분.None;
        public 사진형식 마스터형식 { get; set; } = 사진형식.Jpg;
        public String 도구명칭 => this.카메라.ToString();
        public String 도구경로 => Path.Combine(Global.환경설정.도구경로, ((Int32)모델구분).ToString("d2"), $"{도구명칭}.vpp");
        public String 마스터경로 => Path.Combine(Global.환경설정.마스터사진, $"{((Int32)모델구분).ToString("d2")}.{도구명칭}.{마스터형식.ToString()}");
        public 그랩장치 그랩장치 => Global.그랩제어.GetItem(카메라);

        public CogJob Job = null;
        public CogToolBlock ToolBlock = null;
        public CogToolBlock AlignTools => this.GetTool("AlignTools") as CogToolBlock;
        public ICogImage InputImage { get => this.Input<ICogImage>("InputImage"); set => this.Input("InputImage", value); }
        public ICogImage OutputImage => 비전검사.Output<ICogImage>(this.AlignTools, "OutputImage");
        public String ViewerRecodName => "AlignTools.Fixture.OutputImage";

        private Boolean 컬러여부 => false;
        public DateTime 검사시작 = DateTime.Today;
        public DateTime 검사종료 = DateTime.Today;
        public Double 검사시간 => (this.검사종료 - this.검사시작).TotalMilliseconds;
        public RecordDisplay Display = null;
        public RecordsDisplay RecordsDisplay = null;

        public 비전도구(모델구분 모델, 카메라구분 카메라)
        {
            this.모델구분 = 모델;
            this.카메라 = 카메라;
        }

        public Mat MatImage()
        {
            if (!Global.장치상태.자동수동 || 그랩장치 == null)
                return Common.ToMat(this.InputImage);
            return 그랩장치.MatImage();
        }

        public void SetDisplay(RecordsDisplay display) => this.RecordsDisplay = display;
        public void RemoveDisplay() => this.RecordsDisplay = null;

        public ICogTool GetTool(String name) => 비전검사.GetTool(ToolBlock, name);
        public T Input<T>(String name) => 비전검사.Input<T>(ToolBlock, name);
        public Boolean Input(String name, Object value) => 비전검사.Input(ToolBlock, name, value);
        public T Output<T>(String name) => 비전검사.Output<T>(ToolBlock, name);

        public void Init() => this.Load();
        public void Load()
        {
            Debug.WriteLine(this.도구경로, this.카메라.ToString());
            if (File.Exists(this.도구경로))
            {
                 this.Job = CogSerializer.LoadObjectFromFile(this.도구경로) as CogJob;
                this.Job.Name = $"Job{도구명칭}";
                this.ToolBlock = (this.Job.VisionTool as CogToolGroup).Tools[0] as CogToolBlock;
            }
            else
            {
                this.Job = new CogJob($"Job{도구명칭}");
                CogToolGroup group = new CogToolGroup() { Name = $"Group{도구명칭}" };
                this.ToolBlock = new CogToolBlock();
                this.ToolBlock.Name = this.도구명칭;
                group.Tools.Add(this.ToolBlock);
                this.Job.VisionTool = group;
                this.ToolBlock.Tools.Add(new CogToolBlock() { Name = "AlignTools" });
                this.Save();
            }

            if (this.ToolBlock != null) this.ToolBlock.DataBindings.Clear();
            else this.ToolBlock = new CogToolBlock();
            this.ToolBlock.Name = this.도구명칭;

            // 파라미터 체크
            비전검사.AddInput(this.ToolBlock, "InputImage", typeof(CogImage8Grey));
            비전검사.AddInput(this.ToolBlock, "Results", typeof(String));
            비전검사.AddInput(this.AlignTools, "InputImage", typeof(CogImage8Grey));
            비전검사.AddInput(this.AlignTools, "CalibX", 0.020d);
            비전검사.AddInput(this.AlignTools, "CalibY", 0.020d);
            비전검사.AddOutput(this.ToolBlock, "Results", typeof(String));

            비전검사.AddOutput(this.AlignTools, "OutputImage", typeof(CogImage8Grey));
            비전검사.AddOutput(this.AlignTools, "Detected", false);
            //비전검사.AddOutput(this.AlignTools, "Perspective", typeof(String));
            //비전검사.AddOutput(this.AlignTools, "ResolutionX", 0.0d);
            //비전검사.AddOutput(this.AlignTools, "ResolutionY", 0.0d);
            SetVeriables();
        }

        public void Save()
        {
            DisposeTool(this.ToolBlock);
            CogSerializer.SaveObjectToFile(this.Job, this.도구경로, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
            Global.정보로그(this.로그영역, "Save", $"[{Utils.GetDescription(this.카메라)}] It was saved.", true);
        }
        private void DisposeTool(CogToolBlock block)
        {
            if (block == null) return;
            foreach(ICogTool tool in block.Tools)
            {
                if (tool.GetType() == typeof(CogCNLSearchTool))
                    DisposeTool(tool as CogCNLSearchTool);
                //else if (tool.GetType() == typeof(CogMaskCreatorTool))
                //    DisposeTool(tool as CogMaskCreatorTool);
                else if (tool.GetType() == typeof(CogBlobTool))
                    DisposeTool(tool as CogBlobTool);
                else if (tool.GetType() == typeof(CogToolBlock))
                    DisposeTool(tool as CogToolBlock);
            }
        }
        private void DisposeTool(CogCNLSearchTool tool)
        {
            if (tool == null || tool.Pattern == null) return;
            Debug.WriteLine(tool.Name, "DisposeTool");
            tool.Pattern.TrainImage?.Dispose();
            tool.Pattern.TrainImageMask?.Dispose();
            tool.Pattern.TrainImage = null;
            tool.Pattern.TrainImageMask = null;
        }
        private void DisposeTool(CogMaskCreatorTool tool)
        {
            if (tool == null || tool.Result == null) return;
            tool.Result.Mask?.Dispose();
        }
        private void DisposeTool(CogBlobTool tool)
        {
            if (tool == null || tool.RunParams.InputImageMask == null) return;
            tool.RunParams.InputImageMask?.Dispose();
            tool.RunParams.InputImageMask = null;
        }

        public void SetVeriables()
        {
            그랩장치 장치 = this.그랩장치;
            if (장치 == null) return;
            비전검사.Input(this.AlignTools, "CalibX", 장치.교정X / 1000);
            비전검사.Input(this.AlignTools, "CalibY", 장치.교정Y / 1000);
        }
        #endregion

        #region 도구설정, 마스터
        public void 도구설정() => 비전검사.도구설정(this);
        public Boolean 마스터저장()
        {
            if (this.InputImage == null) return false;
            Boolean r = false;
            String error = String.Empty;
            //if      (this.마스터형식 == 사진형식.Bmp) r = Common.ImageSaveBmp(this.InputImage, this.마스터경로, out error);
            //else if (this.마스터형식 == 사진형식.Png) r = Common.ImageSavePng(this.InputImage, this.마스터경로, out error);
            //else 
            if (this.마스터형식 == 사진형식.Jpg) r = Common.ImageSaveJpeg(this.InputImage, this.마스터경로, out error);
            else return false;
            if (!r) Utils.WarningMsg("마스터 이미지 등록실패!!!\n" + error);
            return r;
        }
        public Boolean 마스터로드(Boolean autoCalibration = false)
        {
            Boolean r = 이미지로드(this.마스터경로);
            if (r && 비전검사.Output<Boolean>(AlignTools, "Detected"))
            {
                Double rX = 비전검사.Output<Double>(AlignTools, "ResolutionX");
                Double rY = 비전검사.Output<Double>(AlignTools, "ResolutionY");
                Debug.WriteLine($"CalibX={rX}, CalibY={rY}", this.카메라.ToString());
                if (autoCalibration && rX > 0 && rY > 0)
                {
                    그랩장치 장치 = Global.그랩제어.GetItem(카메라);
                    if (장치 != null)
                    {
                        장치.교정X = rX;
                        //장치.교정Y = rY;
                        비전검사.Input(AlignTools, "CalibX", rX / 1000);
                        //비전검사.Input(AlignTools, "CalibY", rY / 1000);
                        CogTransform2DLinear transform = (비전검사.GetTool(AlignTools, "Fixture") as CogFixtureNPointToNPointTool)?.RunParams.RawFixturedFromFixturedTransform as CogTransform2DLinear;
                        if (transform != null)
                        {
                            //Debug.WriteLine($"OriginX={transform.TranslationX}, OriginY={transform.TranslationY}", this.카메라.ToString());
                            비전검사.Input(AlignTools, "OriginX", Math.Round(transform.TranslationX, 9));
                            비전검사.Input(AlignTools, "OriginY", Math.Round(transform.TranslationY, 9));
                        }
                    }
                }
            }
            return r;
        }
        public Boolean 이미지로드() => 이미지로드(Common.GetImageFile());
        public Boolean 이미지로드(String path)
        {
            if (!File.Exists(path)) return false;
            return 이미지검사(Common.LoadImage(path, this.컬러여부));
        }
        public Boolean 이미지검사(ICogImage image)
        {
            if (image == null) return false;
            Global.검사자료.수동검사.Reset(this.카메라);
            this.Run(image, Global.검사자료.수동검사);
            Global.검사자료.수동검사결과(this.카메라, Global.검사자료.수동검사);
            return true;
        }
        public Boolean 다시검사()
        {
            Global.검사자료.수동검사.Reset(this.카메라);
            this.Run(null, Global.검사자료.수동검사);
            Global.검사자료.수동검사결과(this.카메라, Global.검사자료.수동검사);
            return true;
        }
        #endregion

        #region Display
        public void DisplayResult(검사결과 결과)
        {
            try
            {
                Input("Results", GetDisplayResultJson(결과));
                ICogRecord records = this.ToolBlock.CreateLastRunRecord();
                ICogRecord record = null;
                if (records != null && records.SubRecords != null && records.SubRecords.ContainsKey(this.ViewerRecodName))
                    record = records.SubRecords[this.ViewerRecodName];

                if (this.OutputImage != null)
                {
                    if (this.Display != null) this.Display.SetImage(this.OutputImage, record, null);
                    if (this.RecordsDisplay != null && !Global.장치상태.자동수동)
                        this.RecordsDisplay.ViewResultImage(this.OutputImage, records, this.ViewerRecodName);
                }
                else
                {
                    if (this.Display != null) this.Display.SetImage(this.InputImage, record, null);
                    if (this.RecordsDisplay != null && !Global.장치상태.자동수동)
                        this.RecordsDisplay.ViewResultImage(this.InputImage, records, String.Empty);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message, "DisplayResult"); }
        }
        public String GetDisplayResultJson(검사결과 결과)
        {
            List<DisplayResult> results = new List<DisplayResult>();
            foreach (검사정보 정보 in 결과.검사내역.Where(x => x.카메라여부 && (Int32)x.검사장치 == (Int32)카메라).ToList())
                results.Add(new DisplayResult() { KeyName = 정보.변수명칭, Display = 정보.DisplayText(), Color = CogColor(정보.측정결과) });

            //foreach (표면불량 불량 in 결과.불량영역(this.카메라))
            //{
            //    System.Drawing.Color c = ML.BaseML.라벨설정[카메라].GetColor(불량.불량유형);
            //    var r = 불량.GetRectangleAffine();
            //    results.Add(new DisplayResult() { KeyName = 검사항목.Defects.ToString(), Display = 불량.불량유형, Color = CogColor(c), Rect = new List<Double>() { r.CenterX, r.CenterY, r.SideXLength, r.SideYLength, r.Rotation } });
            //}
            return JsonConvert.SerializeObject(results);
            //return String.Empty;
        }

        public static CogColorConstants CogColor(System.Drawing.Color color) => (CogColorConstants)System.Drawing.ColorTranslator.ToOle(color);
        public static CogColorConstants CogColor(결과구분 판정)
        {
            if (판정 == 결과구분.OK) return CogColorConstants.DarkGreen;
            if (판정 == 결과구분.NG) return CogColorConstants.Red;
            if (판정 == 결과구분.ER) return CogColorConstants.Magenta;
            return CogColorConstants.LightGrey;
        }
        #endregion

        #region Run
        public Boolean IsAccepted()
        {
            foreach (ICogTool tool in this.AlignTools.Tools)
                if (tool.RunStatus.Result != CogToolResultConstants.Accept) return false;
            foreach (ICogTool tool in this.ToolBlock.Tools)
                if (tool.RunStatus.Result != CogToolResultConstants.Accept) return false;
            return true;
        }

        public Boolean Run(ICogImage image, 검사결과 검사)
        {
            Boolean accepted = false;
            try
            {
                if (image != null) this.InputImage = image;
                if (this.InputImage == null) return false;
                this.검사시작 = DateTime.Now;
                Input("Results", String.Empty);
                this.ToolBlock.Run();
                accepted = this.IsAccepted();
                //검사?.SetResults(this.카메라, this.GetResults());
                검사?.SetResults(this.카메라, Output<String>("Results"));
                this.표면검사(검사);
                this.검사종료 = DateTime.Now;
                //Debug.WriteLine($"{this.카메라.ToString()} => {(검사종료 - 검사시작).TotalMilliseconds.ToString("#,0")}", "검사시간");
                DisplayResult(검사);
                Global.캘리브?.AddNew(this.ToolBlock, this.카메라, 검사.검사번호);
                검사완료체크(검사);
            }
            catch (Exception ex) { Global.오류로그(로그영역, "Run", $"[{this.카메라.ToString()}] {ex.Message}", true); }
            return accepted;
        }

        private void 검사완료체크(검사결과 검사)
        {
            if (검사 == null || Global.장치상태.자동수동 && 검사.검사완료.Contains(카메라)) return;
            if (!검사.검사완료.Contains(this.카메라)) 검사.검사완료.Add(this.카메라);
            검사.검사완료여부 = 검사.검사완료.Count >= 3;
            if (검사.검사완료여부)
            {
                Debug.WriteLine($"검사완료 => {검사.검사번호}");
                Global.피씨통신.Publish(검사.검사번호, 검사.검사내역, 피씨명령.상부완료);
            }
        }

        public Dictionary<String, Object> GetResults()
        {
            Dictionary<String, Object> results = new Dictionary<String, Object>();
            foreach (CogToolBlockTerminal terminal in this.ToolBlock.Outputs)
            {
                if (terminal.ValueType != typeof(Double)) continue;
                Double value = terminal.Value == null ? Double.NaN : (Double)terminal.Value;
                results.Add(terminal.Name, value);
            }
            return results;
        }

        // Deep Learning
        private void 표면검사(검사결과 검사)
        {
            //if (!Global.환경설정.표면검사) return;
            //if (this.카메라 == 카메라구분.Cam02 || this.카메라 == 카메라구분.Cam03) return;
            //Mat source = this.MatImage();
            //if (source == null) return;

            ////String 저장경로 = $@"{머신러닝.작업폴더}\Predict\{this.카메라.ToString()}\{검사.검사코드.ToString("d4")}";
            //String 저장경로 = $@"R:\Predict\{this.카메라.ToString()}\{검사.검사코드.ToString("d4")}";
            //사진분할 분할 = new 사진분할(this.카메라, 저장경로);
            //Directory.CreateDirectory(저장경로);
            //분할정보 자료 = 분할.분할하기(source, out Exception ex);

            //if (ex != null) { Debug.WriteLine(ex.ToString()); return; }
            //var results = Global.머신러닝.Predict(this.카메라, 저장경로);
            //if (results == null) { Directory.Delete(저장경로, true); return; }

            //foreach (var result in results)
            //{
            //    foreach (딥러닝정보 item in result)
            //    {
            //        RotatedRect br = 자료.GetRegions(result.사진파일, item.검출위치);
            //        Point2d cp = 비전검사.PointTransform(this.InputImage, "Fixture", br.Center.X, br.Center.Y);
            //        RotatedRect cr = new RotatedRect(new Point2f((Single)cp.X, (Single)cp.Y), br.Size, br.Angle);
            //        표면불량 불량 = new 표면불량(검사.검사일시, this.카메라, item.신뢰점수, cr, item.불량유형, 자료.축소비율);
            //        검사.표면불량.Add(불량);
            //    }
            //}
            //Directory.Delete(저장경로, true);
        }
        #endregion
    }
}