using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    public partial class 피씨통신
    {
        public event Global.BaseEvent 동작상태알림;
        public Int32 상부치수번호 = 0;

        private Boolean 자료분석(통신자료 자료)
        {
            if (자료.명령 == 피씨명령.상태정보) return 상태정보수신(자료);
            if (자료.명령 == 피씨명령.검사설정) return 검사설정수신(자료);
            if (자료.명령 == 피씨명령.제품투입) return 제품투입수신(자료);
            if (자료.명령 == 피씨명령.상부치수) return 상부치수수신(자료);
            if (자료.명령 == 피씨명령.검사결과)
            {
                Debug.WriteLine("검사결과명령");
                return true;
            }
            return false;
        }

        private Boolean 상태정보수신(통신자료 자료)
        {
            Debug.WriteLine($"상태정보수신완료");
            상태정보 정보 = 자료.Get<상태정보>();

            Debug.WriteLine($"상태정보수신완료 => {정보.자동수동}");

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
            Debug.WriteLine($"제품투입수신 => index {자료.번호}");
            검사결과 검사 = Global.검사자료.검사시작(자료.번호);
            return 검사 != null;
        }
        
        public Boolean 상부치수수신(통신자료 자료)
        {
            상부치수번호 = 자료.번호;
            Debug.WriteLine($"상부치수수신 => index {상부치수번호}");
            new Thread(() => {
                Global.그랩제어.Active(카메라구분.Cam01);
                Global.그랩제어.Active(카메라구분.Cam02);
                Global.그랩제어.Active(카메라구분.Cam03);
                Global.조명제어.TurnOn();
            }).Start();
            //Publish(상부치수번호, 피씨명령.상부치수);
            return true;
        }

        public void 검사결과전송()
        {
            Debug.WriteLine("검사결과 전송");
        }
    }
}
