using Cognex.VisionPro;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.QuickBuild;
using Cognex.VisionPro.ToolBlock;
using Cogutils;
using DevExpress.Utils.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using TE1.UI.Forms;

namespace TE1.Schemas
{
    public class 비전검사 : Dictionary<카메라구분, 비전도구>
    {
        #region 도구초기화
        private CogJobManager Manager = null;
        public 비전검사() { InitPath(); }

        private void InitPath()
        {
            foreach (모델구분 모델 in Enum.GetValues(typeof(모델구분)))
            {
                if (모델 == 모델구분.None) continue;
                String path = Path.Combine(Global.환경설정.도구경로, ((Int32)모델).ToString("d2"));
                if (Directory.Exists(path)) continue;
                Directory.CreateDirectory(path);
            }
        }

        public void Init() => this.Init(Global.환경설정.선택모델);

        public void Init(모델구분 모델)
        {
            this.Close();
            this.Manager = new CogJobManager("JobManager") { GarbageCollection = true, GarbageCollectionInterval = 5 };
            Debug.WriteLine($"GarbageCollection={this.Manager.GarbageCollection}, Interval={this.Manager.GarbageCollectionInterval}", "비젼검사");

            foreach (카메라구분 구분 in Global.그랩제어.Keys)
            {
                if (구분 == 카메라구분.None) continue;
                비전도구 도구 = new 비전도구(모델, 구분);
                도구.Init();
                this.Add(구분, 도구);
                this.Manager.JobAdd(도구.Job);
            }
        }

        public void Close()
        {
            foreach (비전도구 도구 in this.Values)
                도구.Job?.Shutdown();
            this.Manager?.Shutdown();
            this.Manager = null;
            this.Clear();
        }

        public void SetCalib() => this.Values.ForEach(e => e.SetVeriables());

        public void SetDisplay(카메라구분 카메라, RecordDisplay display)
        {
            if (!this.ContainsKey(카메라)) return;
            this[카메라].Display = display;
        }

        public static void 도구설정(비전도구 도구)
        {
            UI.Forms.CogToolEdit viewForm = new UI.Forms.CogToolEdit();
            viewForm.Init(도구);
            viewForm.Show(Global.MainForm);
        }
        public void 도구설정(카메라구분 구분)
        {
            if (!this.ContainsKey(구분)) return;
            도구설정(this[구분]);
        }
        #endregion

        #region Run
        public void RunMaster()
        {
            this.Values.ForEach(도구 => 도구.마스터로드());
        }
        public Boolean RunMaster(카메라구분 카메라)
        {
            if (!this.ContainsKey(카메라)) return false;
            return this[카메라].마스터로드();
        }

        // Live 검사
        public Boolean Run(그랩장치 장치, 검사결과 결과, Boolean Merged = false)
        {
            if (장치 == null || 결과 == null) return false;

            Boolean r = Merged == false ? Run(장치.구분, 장치.CogImage(), 결과) : Run(카메라구분.Cam02, Global.그랩제어.GetItem(카메라구분.Cam01).CogImage(), Global.그랩제어.GetItem(카메라구분.Cam02).CogImage(), 결과);
            if (Merged == false)
                Global.사진자료.SaveImage(장치, 결과);

            return r;
        }
        public Boolean Run(카메라구분 카메라, ICogImage image, 검사결과 검사)
        {
            if (image == null)
            {
                Global.오류로그("비전검사", "이미지없음", $"{카메라} 검사할 이미지가 없습니다.", true);
                return false;
            }
            if (!this.ContainsKey(카메라)) return false;
            비전도구 도구 = this[카메라];
            return 도구.Run(image, 검사);
        }
        public Boolean Run(카메라구분 카메라, ICogImage LeftImage, ICogImage RightImage, 검사결과 검사)
        {
            if (LeftImage == null || RightImage == null)
            {
                Global.오류로그("비전검사", "이미지없음", $"{카메라} 검사할 이미지가 없습니다.", true);
                return false;
            }
            if (!this.ContainsKey(카메라)) return false;
            비전도구 도구 = this[카메라];
            return 도구.Run(LeftImage, RightImage, 검사);
        }
        #endregion

        #region Static Methods
        public static Point2d PointTransform(ICogImage OrignImage, String TargetSpaceName, Double OriginX, Double OriginY)
        {
            if (OrignImage == null) return new Point2d() { X = OriginX, Y = OriginY };
            CogTransform2DLinear trans = OrignImage.GetTransform(TargetSpaceName, ".") as CogTransform2DLinear;
            Point2d p = new Point2d();
            trans.MapPoint(OriginX, OriginY, out p.X, out p.Y);
            return p;
        }

        public static ICogTool GetTool(CogToolBlock block, String name)
        {
            if (block == null || !block.Tools.Contains(name)) return null;
            return block.Tools[name];
        }
        public static T Input<T>(CogToolBlock block, String name)
        {
            if (block == null || !block.Inputs.Contains(name)) return default(T);
            Object v = block.Inputs[name].Value;
            if (v == null) return default(T);
            return (T)v;
        }
        public static Boolean Input(CogToolBlock block, String name, Object value)
        {
            if (block == null || !block.Inputs.Contains(name)) return false;
            block.Inputs[name].Value = value;
            return true;
        }
        public static T Output<T>(CogToolBlock block, String name)
        {
            if (block == null || !block.Outputs.Contains(name)) return default(T);
            Object v = block.Outputs[name].Value;
            if (v == null) return default(T);
            return (T)v;
        }
        public static Boolean Output(CogToolBlock block, String name, Object value)
        {
            if (block == null || !block.Outputs.Contains(name)) return false;
            block.Outputs[name].Value = value;
            return true;
        }

        public static void AddInput(CogToolBlock block, String name, Type type)
        {
            if (block == null || block.Inputs.Contains(name)) return;
            block.Inputs.Add(new CogToolBlockTerminal(name, type));
        }
        public static void AddInput(CogToolBlock block, String name, Object value)
        {
            if (block == null || block.Inputs.Contains(name)) return;
            block.Inputs.Add(new CogToolBlockTerminal(name, value));
        }
        public static void AddOutput(CogToolBlock block, String name, Type type)
        {
            if (block == null || block.Outputs.Contains(name)) return;
            block.Outputs.Add(new CogToolBlockTerminal(name, type));
        }
        public static void AddOutput(CogToolBlock block, String name, Object value)
        {
            if (block == null || block.Outputs.Contains(name)) return;
            block.Outputs.Add(new CogToolBlockTerminal(name, value));
        }

        public static Boolean 표면설정(비전도구 도구)
        {
            if (도구 == null) return false;
            CogToolBlock block = 도구.GetTool(BaseTool.DefectsTools) as CogToolBlock;
            if (block == null) return false;
            CogMaskCreatorTool tool = 비전검사.GetTool(block, BaseTool.DefectsMask) as CogMaskCreatorTool;
            if (tool == null) return false;
            if (도구.InputImage == null) 도구.마스터로드();
            CogDefectsEdit f = new CogDefectsEdit();
            f.Init(도구, tool);
            f.Show();
            return true;
        }
        public Boolean 표면설정(카메라구분 구분)
        {
            if (!this.ContainsKey(구분)) return false;
            return 표면설정(this[구분]);
        }

        #endregion



        #region Bottom 카메라
        //public static void 하부검사시작()
        //{
        //    try
        //    {
        //        Mat image = 하부사진생성();
        //        if (image == null) return;
        //        new Thread(() =>
        //        {
        //            ImageDevice 장치 = Global.그랩제어[카메라구분.Bottom] as ImageDevice;
        //            장치?.AcquisitionFinished(image);

        //        }).Start();
        //    }
        //    catch (Exception ex) { Global.오류로그("Bottom", "Image Mearge", ex.Message, true); }
        //}
        //public static Mat 하부사진생성()
        //{
        //    비전도구 tool1 = Global.비전검사[카메라구분.Cam02];
        //    비전도구 tool2 = Global.비전검사[카메라구분.Cam03];
        //    Mat mat1 = tool1.MatImage();
        //    Mat mat2 = tool2.MatImage();
        //    if (mat1 == null || mat2 == null) return null;

        //    Rect left = new Rect(tool1.Input<Int32>("X"), tool1.Input<Int32>("Y"), tool1.Input<Int32>("Width"), tool1.Input<Int32>("Height"));
        //    Rect right = new Rect(tool2.Input<Int32>("X"), tool2.Input<Int32>("Y"), tool2.Input<Int32>("Width"), tool2.Input<Int32>("Height"));
        //    Mat mearge = new Mat(new Size(left.Width + right.Width, left.Height), MatType.CV_8UC1, Scalar.Black);
        //    mearge[new Rect(0, 0, left.Width, mearge.Height)] = mat1[left];
        //    mearge[new Rect(left.Width, 0, right.Width, mearge.Height)] = mat2[right];
        //    Double scale = BaseML.최대크기.Height / mearge.Height;
        //    if (scale <= 0 || scale >= 1) return mearge;
        //    using (Mat resized = Common.Resize(mearge, scale))
        //    {
        //        Int32 w = (Int32)(Math.Ceiling((Double)resized.Width / 640) * 640);
        //        Int32 x = (w - resized.Width) / 2;
        //        Mat arranged = new Mat(new Size(w, resized.Height), MatType.CV_8UC1, Scalar.Black);
        //        arranged[new Rect(x, 0, resized.Width, resized.Height)] = resized;
        //        mearge?.Dispose();
        //        return arranged;
        //    }
        //}
        #endregion
    }
}
