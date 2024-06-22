using Euresys.MultiCam;
using MvUtils;
using Newtonsoft.Json;
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
            Hike8K cam1 = new Hike8K(카메라구분.Cam04) { DriverIndex = 1, Connector = Connector.A, AcquisitionMode = AcquisitionMode.PAGE, PageLength_Ln = 15500, CamFile = "MV-CL084-90CM.cam" };
            Hike8K cam2 = new Hike8K(카메라구분.Cam05) { DriverIndex = 0, Connector = Connector.A, AcquisitionMode = AcquisitionMode.PAGE, PageLength_Ln = 7400, CamFile = "MV-CL084-90CM.cam" };
            Hike8K cam3 = new Hike8K(카메라구분.Cam06) { DriverIndex = 0, Connector = Connector.B, AcquisitionMode = AcquisitionMode.PAGE, PageLength_Ln = 7400, CamFile = "MV-CL084-90CM.cam" };
            this.카메라1 = new EuresysLink(cam1) { 코드 = "DA2731657", 가로 = 8192, 세로 = cam1.PageLength_Ln };
            this.카메라2 = new EuresysLink(cam2) { 코드 = "DA2731658", 가로 = 8192, 세로 = cam2.PageLength_Ln };
            this.카메라3 = new EuresysLink(cam3) { 코드 = "DA2731656", 가로 = 8192, 세로 = cam3.PageLength_Ln };
            this.Add(카메라구분.Cam04, this.카메라1);
            this.Add(카메라구분.Cam05, this.카메라2);
            this.Add(카메라구분.Cam06, this.카메라3);

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

        public void 그랩완료(그랩장치 장치)
        {
            if     (장치.구분 == 카메라구분.Cam04) new Thread(() => Global.조명제어.TurnOffBottom()).Start();
            else if(장치.구분 == 카메라구분.Cam05) new Thread(() => Global.조명제어.TurnOffTop()).Start();

            if (Global.장치상태.자동수동)
            {
                //Int32 검사번호 = Global.장치통신.촬영위치번호(장치.구분);
                //검사결과 검사 = Global.검사자료.검사항목찾기(검사번호);
                //if (검사 == null) return;
                //Global.비전검사.Run(장치, 검사);
            }
            else
            {
                Global.비전검사.Run(장치.구분, 장치.CogImage(), Global.검사자료.수동검사);
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

        //#region 오류메세지
        //public static Boolean Validate(String message, Int32 errorNum, Boolean show)
        //{
        //    if (errorNum == CErrorDefine.MV_OK) return true;

        //    String errorMsg = String.Empty;
        //    switch (errorNum)
        //    {
        //        case CErrorDefine.MV_E_HANDLE: errorMsg = "Error or invalid handle"; break;
        //        case CErrorDefine.MV_E_SUPPORT: errorMsg = "Not supported function"; break;
        //        case CErrorDefine.MV_E_BUFOVER: errorMsg = "Cache is full"; break;
        //        case CErrorDefine.MV_E_CALLORDER: errorMsg = "Function calling order error"; break;
        //        case CErrorDefine.MV_E_PARAMETER: errorMsg = "Incorrect parameter"; break;
        //        case CErrorDefine.MV_E_RESOURCE: errorMsg = "Applying resource failed"; break;
        //        case CErrorDefine.MV_E_NODATA: errorMsg = "No data"; break;
        //        case CErrorDefine.MV_E_PRECONDITION: errorMsg = "Precondition error, or running environment changed"; break;
        //        case CErrorDefine.MV_E_VERSION: errorMsg = "Version mismatches"; break;
        //        case CErrorDefine.MV_E_NOENOUGH_BUF: errorMsg = "Insufficient memory"; break;
        //        case CErrorDefine.MV_E_UNKNOW: errorMsg = "Unknown error"; break;
        //        case CErrorDefine.MV_E_GC_GENERIC: errorMsg = "General error"; break;
        //        case CErrorDefine.MV_E_GC_ACCESS: errorMsg = "Node accessing condition error"; break;
        //        case CErrorDefine.MV_E_ACCESS_DENIED: errorMsg = "No permission"; break;
        //        case CErrorDefine.MV_E_BUSY: errorMsg = "Device is busy, or network disconnected"; break;
        //        case CErrorDefine.MV_E_NETER: errorMsg = "Network error"; break;
        //        default: errorMsg = "Unknown error"; break;
        //    }

        //    Global.오류로그("Camera", "Error", $"[{errorNum}] {message} {errorMsg}", show);
        //    return false;
        //}
        //#endregion
    }
}