using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TE1.Schemas
{
    public partial class 피씨통신
    {
        public event Global.BaseEvent 동작상태알림;
        public Int32 평탄측정번호 = 0;
        public Int32 하부표면번호 = 0;
        public Int32 상부표면번호 = 0;

        private Boolean 자료분석(통신자료 자료)
        {
            if (자료.명령 == 피씨명령.상태정보) return 상태정보수신(자료);
            if (자료.명령 == 피씨명령.검사설정) return 검사설정수신(자료);
            if (자료.명령 == 피씨명령.제품투입) return 제품투입수신(자료);
            if (자료.명령 == 피씨명령.하부표면) return 하부표면수신(자료);
            if (자료.명령 == 피씨명령.상부표면) return 상부표면수신(자료);
            if (자료.명령 == 피씨명령.평탄측정) return 평탄검사수신(자료);
            return false;
        }

        private Boolean 상태정보수신(통신자료 자료)
        {
            상태정보 정보 = 자료.Get<상태정보>();
            if (정보 == null)
            {
                Global.오류로그(로그영역, "State Info.", "The data received is incorrect.", true);
                return false;
            }

            if (Global.환경설정.선택모델 != 정보.현재모델) Global.환경설정.모델변경요청(정보.현재모델);
            else if (Global.모델자료.선택모델.양품갯수 != 정보.양품갯수 || Global.모델자료.선택모델.불량갯수 != 정보.불량갯수)
                Global.모델자료.수량변경(Global.환경설정.선택모델, 정보.양품갯수, 정보.불량갯수);

            if (Global.장치상태.자동수동 != 정보.자동수동 || Global.장치상태.시작정지 != 정보.시작정지)
            {
                Global.장치상태.자동수동 = 정보.자동수동;
                Global.장치상태.시작정지 = 정보.시작정지;
                동작상태알림?.Invoke();
            }
            return true;
        }

        private Boolean 검사설정수신(통신자료 자료)
        {
            모델정보 모델 = Global.모델자료.GetItem((모델구분)자료.번호);
            if (모델 == null) return false;
            모델.검사설정.Load(자료.Get<List<검사정보>>());
            //모델.검사설정.Save();
            return true;
        }
        public void 검사설정전송(검사설정 설정) => Publish(new 통신자료(피씨명령.검사설정, 설정) { 발신 = 피씨구분, 번호 = 설정.모델번호 }.Get());

        private Boolean 제품투입수신(통신자료 자료)
        {
            검사결과 검사 = Global.검사자료.검사시작(자료.번호);
            return 검사 != null;
        }

        public Boolean 하부표면수신(통신자료 자료)
        {
            Debug.WriteLine("하부표면수신완료.");
            하부표면번호 = 자료.번호;
            new Thread(() => {
                Global.그랩제어.Active(카메라구분.Cam04);
                Global.조명제어.TurnOnBottom();
            }).Start();
            return true;
        }

        public Boolean 상부표면수신(통신자료 자료)
        {
            Debug.WriteLine("상부표면수신완료.");
            상부표면번호 = 자료.번호;
            new Thread(() => Global.그랩제어.Active(카메라구분.Cam05)).Start();
            new Thread(() => Global.그랩제어.Active(카메라구분.Cam06)).Start();
            new Thread(() => Global.조명제어.TurnOnTop()).Start();
            return true;
        }

        private Boolean 평탄검사수신(통신자료 자료)
        {
            Debug.WriteLine("평탄검사수신 들어옴");
            평탄측정번호 = 자료.번호;
            new Thread(() => {
                검사결과 검사 = Global.검사자료.검사항목찾기(평탄측정번호);
                센서자료 센서 = Global.평탄센서.측정하기(검사);
                Publish(평탄측정번호, 센서, 피씨명령.평탄완료);
            }).Start();
            return true;
        }

        public void 검사결과전송()
        {
            Debug.WriteLine("검사결과 전송");
        }
    }
}
