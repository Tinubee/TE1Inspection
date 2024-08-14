using Euresys.MultiCam;
using MvUtils;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using TE1.Multicam;

namespace TE1.Schemas
{
    public class 그랩제어 : Dictionary<카메라구분, 그랩장치>
    {
        public delegate void 그랩완료대리자(그랩장치 장치);
        public event 그랩완료대리자 그랩완료보고;

        public EuresysLink 카메라1 = null;
        public EuresysLink 카메라2 = null;
        public EuresysLink 카메라3 = null;

        [JsonIgnore]
        private const string 로그영역 = "카메라";
        [JsonIgnore]
        private string 저장파일 => Path.Combine(Global.환경설정.기본경로, "Cameras.json");
        [JsonIgnore]
        public Boolean 정상여부 => !this.Values.Any(e => !e.상태);

        public Boolean Init()
        {
            Dalsa16K cam1 = new Dalsa16K(카메라구분.Cam01) { DriverIndex = 0, AcquisitionMode = AcquisitionMode.PAGE, PageLength_Ln = 61000 };
            Dalsa16K cam2 = new Dalsa16K(카메라구분.Cam02) { DriverIndex = 2, AcquisitionMode = AcquisitionMode.PAGE, PageLength_Ln = 61000 };
            Dalsa16K cam3 = new Dalsa16K(카메라구분.Cam03) { DriverIndex = 1, AcquisitionMode = AcquisitionMode.PAGE, PageLength_Ln = 35000 };
            this.카메라1 = new EuresysLink(cam1) { 코드 = "H2583969", 가로 = 16384, 세로 = cam1.PageLength_Ln };
            this.카메라2 = new EuresysLink(cam2) { 코드 = "H2583945", 가로 = 16384, 세로 = cam2.PageLength_Ln };
            this.카메라3 = new EuresysLink(cam3) { 코드 = "H2583967", 가로 = 16384, 세로 = cam3.PageLength_Ln };
            this.Add(카메라구분.Cam01, this.카메라1);
            this.Add(카메라구분.Cam02, this.카메라2);
            this.Add(카메라구분.Cam03, this.카메라3);

            // 카메라 설정 저장정보 로드
            그랩장치 정보;
            List<그랩장치> 자료 = Load();
            if (자료 != null)
            {
                foreach (그랩장치 설정 in 자료)
                {
                    정보 = this.GetItem(설정.구분);
                    if (정보 == null) continue;
                    정보.Set(설정);
                }
            }

            if (Global.환경설정.동작구분 != 동작구분.Live) return true;

            MC.OpenDriver();
            // CameraLink 초기화
            foreach (그랩장치 장치 in this.Values)
                if (장치.GetType() == typeof(EuresysLink))
                    장치.Init();
            Debug.WriteLine($"카메라 갯수: {this.Count}");
            GC.Collect();
            return true;
        }

        private List<그랩장치> Load()
        {
            if (!File.Exists(this.저장파일)) return null;
            return JsonConvert.DeserializeObject<List<그랩장치>>(File.ReadAllText(this.저장파일), Utils.JsonSetting());
        }

        public void Save()
        {
            if (!Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this.Values, Utils.JsonSetting())))
                Global.오류로그(로그영역, "카메라 설정 저장", "카메라 설정 저장에 실패하였습니다.", true);
        }

        public void Close()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            foreach (그랩장치 장치 in this.Values)
                장치?.Close();
        }
        public void Active(카메라구분 구분) => this.GetItem(구분)?.Active();

        Mat 좌측이미지 = new Mat();
        Mat 우측이미지 = new Mat();

        public void 그랩완료(그랩장치 장치)
        {
            if (장치.구분 == 카메라구분.Cam03)
                new Thread(() => Global.조명제어.TurnOff()).Start();

            if (장치.구분 == 카메라구분.Cam01 || 장치.구분 == 카메라구분.Cam02)
            {
                if (장치.구분 == 카메라구분.Cam01) 좌측이미지 = 장치.MatImage();
                if (장치.구분 == 카메라구분.Cam02) 우측이미지 = 장치.MatImage();
            }

            if (Global.장치상태.자동수동)
            {
                Int32 검사번호 = Global.피씨통신.상부치수번호; //Global.장치통신.촬영위치번호(장치.구분);
                검사결과 검사 = Global.검사자료.검사항목찾기(검사번호);
                if (검사 == null) return;

                if (장치.구분 == 카메라구분.Cam01 || 장치.구분 == 카메라구분.Cam02)
                {
                    if (!검사.그랩완료.Contains(장치.구분)) 검사.그랩완료.Add(장치.구분);

                    if (Global.환경설정.Cam0102개별이미지저장)
                        Global.사진자료.SaveImage(장치, 검사);

                    if (검사.그랩완료.Count == 2)
                    {
                        //Global.그랩제어.GetItem(카메라구분.Cam02).MergeImages(좌측이미지, 우측이미지, 7318, 529);
                        Global.비전검사.Run(장치, 검사, true);
                    }
                }
                else
                    Global.비전검사.Run(장치, 검사);
            }
            else
            {
                if (장치.구분 == 카메라구분.Cam01 || 장치.구분 == 카메라구분.Cam02)
                {
                    if (!Global.검사자료.수동검사.그랩완료.Contains(장치.구분)) Global.검사자료.수동검사.그랩완료.Add(장치.구분);

                    if (Global.환경설정.Cam0102개별이미지저장)
                        Global.사진자료.SaveImage(장치, Global.검사자료.수동검사);

                    if (Global.검사자료.수동검사.그랩완료.Count == 2)
                    {
                        //Global.그랩제어.GetItem(카메라구분.Cam02).MergeImages(좌측이미지, 우측이미지, 7318, 529);
                        Global.비전검사.Run(장치, Global.검사자료.수동검사, true);
                    }
                }
                else
                    Global.비전검사.Run(장치, Global.검사자료.수동검사);
              
                this.그랩완료보고?.Invoke(장치);
            }
        }

        public 그랩장치 GetItem(카메라구분 구분)
        {
            if (this.ContainsKey(구분)) return this[구분];
            return null;
        }

        private 그랩장치 GetItem(String serial) => this.Values.Where(e => e.코드 == serial).FirstOrDefault();

        public Double 교정X(카메라구분 구분)
        {
            그랩장치 장치 = GetItem(구분);
            if (장치 == null) return 1;
            return 장치.교정X;
        }
        public Double 교정Y(카메라구분 구분)
        {
            그랩장치 장치 = GetItem(구분);
            if (장치 == null) return 1;
            return 장치.교정Y;
        }
    }
}