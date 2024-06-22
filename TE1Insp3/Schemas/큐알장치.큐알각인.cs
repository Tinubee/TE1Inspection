using MvUtils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    public class 큐알각인 : 큐알장치
    {
        public enum 명령구분
        {
            NO,
            RX,
            WX,
        }

        public enum 제어명령
        {
            [Description("None")]
            명령없음,
            [Description("RX,Ready")]
            인쇄가능,
            [Description("WX,CharacterString")]
            인쇄내용,
            [Description("WX,StopMarking")]
            인쇄중단,
            [Description("WX,StartMarking")]
            인쇄시작,
            [Description("RX,PrintCompTiming")]
            인쇄완료,
            [Description("WX,JobNo")]
            모델변경,
            [Description("RX,JobNo")]
            모델확인,
            [Description("RX,Error")]
            오류상태,
            [Description("WX,ErrorClear")] // 리턴 없음
            오류제거,
        }

        public delegate void Communication(통신구분 통신, 제어명령 명령, String mesg);
        public event Communication 송신수신알림;
        public override String 로그영역 => "Laser Marker";
        public override String Host { get => Global.환경설정.큐알각인주소; set => Global.환경설정.큐알각인주소 = value; }
        public override Int32 Port { get => Global.환경설정.큐알각인포트; set => Global.환경설정.큐알각인포트 = value; }
        private Int32 기본대기시간 = 200;// ms
        private Int32 인쇄대기시간 = 5000;// ms

        public Boolean 각인완료확인중 = false;

        private 결과정보 명령전송(제어명령 명령) => this.명령전송(명령, Utils.GetDescription(명령), 기본대기시간);
        private 결과정보 명령전송(제어명령 명령, String command, Int32 timeOut)
        {
            this.송신수신알림?.Invoke(통신구분.TX, 명령, command);
            String 응답 = this.SendCommand(command, timeOut);
            this.송신수신알림?.Invoke(통신구분.RX, 명령, 응답);
            return new 결과정보(명령, 응답);
        }
        private Boolean 명령결과(제어명령 명령) => 명령전송(명령).정상여부;

        // RX,OK,[0~3] : 0 => READY ON, 1 => READY OFF (에러발생 중), 2 => READY OFF (인쇄 중 또는 전개 중)
        public Boolean 인쇄가능()
        {
            결과정보 결과 = 명령전송(제어명령.인쇄가능);
            if (!결과.정상여부) Global.오류로그(로그영역, "인쇄가능", $"[{로그영역}] 명령에 대한 응답이 정상적이지 않습니다.", true);
            else if (결과.응답번호 == 1) Global.오류로그(로그영역, "인쇄가능", $"[{로그영역}] 장치에 오류가 있습니다.", true);
            else if (결과.응답번호 == 2) Global.오류로그(로그영역, "인쇄가능", $"[{로그영역}] 장치가 인쇄 중 이거나 전개 중 입니다.", true);
            return 결과.정상여부 && 결과.응답번호 == 0;
        }

        public Int32 모델확인()
        {
            결과정보 결과 = 명령전송(제어명령.모델확인);
            return 결과.응답번호;
        }
        public Boolean 모델변경(Int32 모델번호)
        {
            결과정보 결과 = 명령전송(제어명령.모델변경, $"{Utils.GetDescription(제어명령.모델변경)}={모델번호.ToString("d4")}", 기본대기시간);
            return 결과.정상여부;
        }
        public Boolean 모델변경(모델구분 모델) => 모델변경((Int32)모델);
        public Boolean 인쇄내용(String 내용)
        {
            결과정보 결과 = 명령전송(제어명령.인쇄내용, $"{Utils.GetDescription(제어명령.인쇄내용)}={내용}", 인쇄대기시간);
            return 결과.정상여부;
        }
        public Boolean 인쇄시작()
        {
            각인완료확인중 = true;
            return 명령결과(제어명령.인쇄시작);
        }
        public Boolean 인쇄중단() => 명령결과(제어명령.인쇄중단);
        public Boolean 오류상태() => 명령결과(제어명령.오류상태);
        public Boolean 오류제거() => 명령결과(제어명령.오류제거);

        public void 각인시작(검사결과 검사)
        {
            String 내용 = 검사.큐알각인내용();
            Boolean r = 인쇄내용(내용);
            if (r) 인쇄시작();

            Debug.WriteLine($"{r} => {내용}", "큐알각인");
        }

        public void 각인완료()
        {
            while (this.각인완료확인중)
                명령결과(제어명령.인쇄완료);
        }

        public class 결과정보
        {
            public 명령구분 명령구분 = 명령구분.NO;
            public 제어명령 제어명령 = 제어명령.명령없음;
            public String 응답자료 = String.Empty;
            public Boolean 정상여부 = false;
            public Int32 응답번호 = 0;
            public String 오류내용 = String.Empty;

            public 결과정보(제어명령 명령, String 결과) => this.응답분석(명령, 결과);
            public void 응답분석(제어명령 명령, String 결과)
            {
                this.제어명령 = 명령;
                this.응답자료 = 결과.Trim();
                Debug.WriteLine(결과, 명령.ToString());
                if (String.IsNullOrEmpty(this.응답자료)) return;

                try
                {
                    String[] r = this.응답자료.Split(",".ToCharArray());
                    if (r[0] == 명령구분.WX.ToString()) this.명령구분 = 명령구분.WX;
                    else if (r[0] == 명령구분.RX.ToString()) this.명령구분 = 명령구분.RX;
                    this.정상여부 = r[1] == "OK";
                    if ((명령 == 제어명령.인쇄가능 || 명령 == 제어명령.모델확인) && this.명령구분 == 명령구분.RX)
                    {
                        응답번호 = Convert.ToInt32(r[2]);
                    }
                }
                catch (Exception ex)
                {
                    this.정상여부 = false;
                    this.오류내용 = ex.Message;
                }
            }
        }
    }
}
