using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TE1.Schemas
{
    public class 검사자료
    {
        public delegate void 검사진행알림(검사결과 결과);
        public delegate void 수동검사수행(카메라구분 카메라, 검사결과 결과);
        public event 검사진행알림 검사완료알림;
        public event 수동검사수행 수동검사알림;
        //public event 검사진행알림 검사결과추가;
        //public event 검사진행알림 현재검사변경;

        [JsonIgnore]
        public static TranslationAttribute 로그영역 = new TranslationAttribute("Inspection", "검사내역");
        [JsonIgnore]
        private TranslationAttribute 저장오류 = new TranslationAttribute("An error occurred while saving the data.", "자료 저장중 오류가 발생하였습니다.");
        [JsonIgnore]
        private Dictionary<Int32, 검사결과> 검사스플 = new Dictionary<Int32, 검사결과>();
        [JsonIgnore]
        public 검사결과 수동검사;

        public void Init()
        {
            this.수동검사초기화();
            Global.환경설정.모델변경알림 += 모델변경알림;
        }

        public Boolean Close() => true;

        private void 수동검사초기화()
        {
            this.수동검사 = new 검사결과();
            this.수동검사.검사번호 = 0;
            this.수동검사.Reset();
        }

        private void 모델변경알림(모델구분 모델코드) => this.수동검사초기화();

        public 검사결과 검사시작(Int32 검사코드)
        {
            if (!Global.장치상태.자동수동)
            {
                this.수동검사.Reset();
                return this.수동검사;
            }

            검사결과 검사 = 검사항목찾기(검사코드);
            if (검사 == null)
            {
                검사 = new 검사결과() { 검사번호 = 검사코드 };
                검사.Reset();
                this.검사스플.Add(검사.검사번호, 검사);
                Global.정보로그(로그영역.GetString(), $"Started", $"[{(Int32)Global.환경설정.선택모델} - {검사.검사번호}] New inspection started.", false);
            }
            return 검사;
        }

        // 현재 검사중인 정보를 검색
        public 검사결과 검사항목찾기(Int32 검사코드)
        {
            if (!Global.장치상태.자동수동) return this.수동검사;
            if (검사코드 > 0 && this.검사스플.ContainsKey(검사코드))
                return this.검사스플[검사코드];
            Global.오류로그(로그영역.GetString(), "Index", $"[{검사코드}] There is no index.", true);
            return null;
        }

        public void 검사결과계산(검사결과 검사)
        {
            if (검사 == null) return;
            검사.결과계산();
            if (Global.장치상태.자동수동)
            {
                this.검사스플.Remove(검사.검사번호);
            }
            // 검사결과 전송

            this.검사완료알림?.Invoke(검사);
        }

        //public void 검사수행알림(검사결과 검사) => this.검사완료알림?.Invoke(검사);
        public void 수동검사결과(카메라구분 카메라, 검사결과 검사)
        {
            this.검사완료알림?.Invoke(검사);
            this.수동검사알림?.Invoke(카메라, 검사);
        }
    }
}
