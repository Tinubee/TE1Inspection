using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TE1.Schemas
{
    public class 검사자료 : BindingList<검사결과>
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
        private 검사결과테이블 테이블 = null;
        [JsonIgnore]
        private Dictionary<Int32, 검사결과> 검사스플 = new Dictionary<Int32, 검사결과>();
        [JsonIgnore]
        public 검사결과 수동검사;

        public void Init()
        {
            this.AllowEdit = true;
            this.AllowRemove = true;
            this.테이블 = new 검사결과테이블();
            this.Load();
            this.수동검사초기화();
            Global.환경설정.모델변경알림 += 모델변경알림;
        }

        public Boolean Close()
        {
            if (this.테이블 == null) return true;
            this.테이블.Save();
            this.테이블.자료정리(Global.환경설정.결과보관);
            return true;
        }

        private void 수동검사초기화()
        {
            this.수동검사 = new 검사결과();
            this.수동검사.검사번호 = 9999;
            this.수동검사.Reset();
        }

        public void Save(검사결과 검사)
        {
            

            this.테이블.Add(검사);
            this.Save();
        }
        public void Save() => this.테이블.Save();
        public void Load() => this.Load(DateTime.Today, DateTime.Today);
        public void Load(DateTime 시작, DateTime 종료)
        {
            this.Clear();
            this.RaiseListChangedEvents = false;
            List<검사결과> 자료 = this.테이블.Select(시작, 종료);

            //List<Int32> 대상 = Global.장치통신.검사중인항목();
            자료.ForEach(검사 =>
            {
                this.Add(검사);
                //// 검사스플 생성
                //if (검사.측정결과 < 결과구분.ER && 대상.Contains(검사.검사코드) && !this.검사스플.ContainsKey(검사.검사코드))
                //    this.검사스플.Add(검사.검사코드, 검사);
            });
            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public List<검사결과> GetData(DateTime 시작, DateTime 종료, 모델구분 모델) => this.테이블.Select(시작, 종료, 모델);
        public 검사결과 GetItem(DateTime 일시, 모델구분 모델, Int32 코드) => this.테이블.Select(일시, 모델, 코드);
        public 검사결과 GetItem(DateTime 시작, DateTime 종료, 모델구분 모델, String 큐알, String serial) => this.테이블.Select(시작, 종료, 모델, 큐알, serial);

        private void 모델변경알림(모델구분 모델코드) => this.수동검사초기화();

        private void 자료추가(검사결과 결과)
        {
            this.Insert(0, 결과);
            //if (Global.장치상태.자동수동)
            //    this.테이블.Add(결과);
            // 저장은 State 에서
        }

        public void 검사항목제거(List<검사정보> 자료) => this.테이블.Remove(자료);
        public Boolean 결과삭제(검사결과 정보)
        {
            this.Remove(정보);
            return this.테이블.Delete(정보) > 0;
        }

        public void 검사일시추출실행(int numberOfResults, int numberOfProducts) => this.테이블.검사일시추출(numberOfResults, numberOfProducts);
        #region 검사로직
        // PLC에서 검사번호 요청 시 새 검사 자료를 생성하여 스플에 넣음
        public 검사결과 검사시작(Int32 검사코드)
        {
            if (!Global.장치상태.자동수동)
            {
                this.수동검사.Reset();
                return this.수동검사;
            }

            검사결과 검사 = 검사항목찾기(검사코드, true);
            if (검사 == null)
            {
                검사 = new 검사결과() { 검사번호 = 검사코드 };
                검사.Reset();
                this.자료추가(검사);
                this.검사스플.Add(검사.검사번호, 검사);
                Global.정보로그(로그영역.GetString(), $"Started", $"[{(Int32)Global.환경설정.선택모델} - {검사.검사번호}] New inspection started.", false);
            }
            return 검사;
        }

        // 현재 검사중인 정보를 검색
        public 검사결과 검사항목찾기(Int32 검사코드, Boolean 신규여부 = false)
        {
            if (!Global.장치상태.자동수동) return this.수동검사;
            검사결과 검사 = null;
            if (검사코드 > 0 && this.검사스플.ContainsKey(검사코드))
                검사 = this.검사스플[검사코드];
            if (검사 == null && !신규여부)
            {
                검사 = Global.검사자료.Where(x => x.검사번호 == 검사코드).FirstOrDefault();
                if (검사 == null)
                {
                    Global.오류로그(로그영역.GetString(), "검사항목찾기", $"[{검사코드}] 검사항목이 없습니다.", true);
                    return null;
                }
            }
            return 검사;
        }
        public 검사결과 현재검사찾기()
        {
            if (!Global.장치상태.자동수동) return this.수동검사;
            if (this.검사스플.Count < 1) return this.수동검사;
            return this.검사스플.Last().Value;
        }

        public void 검사결과계산(검사결과 검사)
        {
            if (검사 == null) return;
            검사.결과계산();
            if (Global.장치상태.자동수동)
            {
                this.ResetItem(this.IndexOf(검사));
                Global.모델자료.수량추가(검사.모델구분, 검사.측정결과);
                this.검사스플.Remove(검사.검사번호);
            }
            this.검사완료알림?.Invoke(검사);
        }

        public void 검사수행알림(검사결과 검사) => this.검사완료알림?.Invoke(검사);
        public void 수동검사결과(카메라구분 카메라, 검사결과 검사)
        {
            this.검사완료알림?.Invoke(검사);
            this.수동검사알림?.Invoke(카메라, 검사);
        }

        public void ResetItem(검사결과 검사)
        {
            if (검사 == null) return;
            this.ResetItem(this.IndexOf(검사));
        }
        #endregion
    }
}
