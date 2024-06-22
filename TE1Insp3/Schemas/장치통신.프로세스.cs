using MvUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    partial class 장치통신
    {
        private DateTime 오류알림시간 = DateTime.Today.AddDays(-1);
        private Int32 오류알림간격 = 30; // 초

        public void 통신오류알림(Int32 오류코드)
        {
            if (오류코드 == 0)
            {
                this.정상여부 = true;
                return;
            }
            if ((DateTime.Now - this.오류알림시간).TotalSeconds < this.오류알림간격) return;
            this.오류알림시간 = DateTime.Now;
            this.정상여부 = false;
            Global.오류로그(로그영역, "PLC 통신", $"[{오류코드.ToString("X8")}] 통신 오류가 발생하였습니다.", false);
        }

        private Boolean 입출자료갱신()
        {
            DateTime 현재 = DateTime.Now;
            // 입출자료 갱신
            Int32 오류 = 0;
            Int32[] 자료 = ReadDeviceRandom(입출자료.주소목록, out 오류);
            if (오류 != 0)
            {
                통신오류알림(오류);
                return false;
            }
            this.입출자료.Set(자료);
            return true;
        }

        private Boolean 입출자료분석()
        {
            if (Global.환경설정.동작구분 == 동작구분.LocalTest) return 테스트수행();
            if (!입출자료갱신()) return false;
            검사위치확인();
            제품검사수행();
            장치상태확인();
            통신핑퐁수행();
            return true;
        }

        private void 장치상태확인()
        {
            if (this.입출자료.Changed(정보주소.시작정지))
                Debug.WriteLine($"{Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss.fff}")} => {this.입출자료.Changed(정보주소.시작정지)}", "시작정지");

            if (this.입출자료.Changed(정보주소.자동수동) || this.입출자료.Changed(정보주소.시작정지))
                this.동작상태알림?.Invoke();
        }

        // 검사위치 변경 확인
        private void 검사위치확인()
        {
            Dictionary<정보주소, Int32> 변경 = this.입출자료.Changes(정보주소.제품투입N, 정보주소.결과요청N);
            if (변경.Count < 1) return;
            this.검사위치알림?.Invoke();
        }

        public List<Int32> 검사중인항목()
        {
            List<Int32> 대상 = new List<Int32>();
            Int32 시작 = (Int32)정보주소.제품투입N;
            Int32 종료 = (Int32)정보주소.결과요청N;
            for (Int32 i = 종료; i >= 시작; i--)
            {
                정보주소 구분 = (정보주소)i;
                if (this.입출자료[구분].정보 <= 0) continue;
                대상.Add(this.입출자료[구분].정보);
            }
            return 대상;
        }

        private void 제품검사수행()
        {
            제품투입수행();
            평탄측정수행();
            영상촬영수행();
            두께측정수행();
            큐알각인수행();
            큐알각인완료();
            큐알리딩수행();
            라벨출력수행();
            검사완료수행();
        }

        private void 큐알각인완료()
        {
            Global.큐알각인.각인완료();
        }

        // 트리거 입력 시 현재 인덱스를 버퍼에 입력하고 검사 수행 시 해당 버퍼의 인덱스를 기준으로 검사
        private Int32 검사위치번호(정보주소 주소)
        {
            if (!this.입출자료.Firing(주소, true)) return -1;
            //if (!Global.장치상태.자동수동) return 0;
            Int32 index = 0;
            if (주소 == 정보주소.제품투입) index = this.제품투입N;
            else if (주소 == 정보주소.평탄측정) index = this.평탄측정N;
            else if (주소 == 정보주소.상부치수) index = this.상부치수N;
            else if (주소 == 정보주소.하부표면) index = this.하부표면N;
            else if (주소 == 정보주소.상부표면) index = this.상부표면N;
            else if (주소 == 정보주소.두께측정) index = this.큐알각인N;
            else if (주소 == 정보주소.큐알각인) index = this.큐알각인N;
            else if (주소 == 정보주소.큐알리딩) index = this.큐알리딩N;
            else if (주소 == 정보주소.라벨출력) index = this.라벨출력N;
            else if (주소 == 정보주소.라벨부착) index = this.라벨출력N;
            else if (주소 == 정보주소.결과요청) index = this.결과요청N;
            this.인덱스버퍼[주소] = index;

            //Debug.WriteLine("----------------------------------");
            if (index == 0) Global.경고로그(로그영역, 주소.ToString(), $"해당 위치에 검사할 제품의 Index가 없습니다.", false);
            Debug.WriteLine($"{Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss.fff}")}  {주소} => {index}", "Trigger");
            //Debug.WriteLine($"1=>{정보읽기(정보주소.인덱스01)}, 2=>{정보읽기(정보주소.인덱스02)}, 3=>{정보읽기(정보주소.인덱스03)}, 4=>{정보읽기(정보주소.인덱스04)}, 5=>{정보읽기(정보주소.인덱스05)}, 6=>{정보읽기(정보주소.인덱스06)}", "PLC Index");
            this.입출자료.SetDelay(주소, 0, 500);

            return index;
        }
        private enum Replys { None, Yes, No }
        private Boolean 검사번호확인(정보주소 주소, out 검사결과 검사, Replys reply)
        {
            Int32 검사번호 = this.검사위치번호(주소);
            if (검사번호 <= 0)
            {
                검사 = null;
                if (검사번호 == 0)
                {
                    Debug.WriteLine($"{주소}={검사번호}", "Trig");
                    this.입출자료.SetDelay(주소, 0, 100);
                }
                return false;
            }
            Debug.WriteLine($"{주소}={검사번호}", "Trig");

            if (주소 == 정보주소.제품투입) 검사 = Global.검사자료.검사시작(검사번호);
            else 검사 = Global.검사자료.검사항목찾기(검사번호);
            if (reply == Replys.None) return 검사 != null;
            if (reply == Replys.Yes || 검사 == null) this.입출자료.SetDelay(주소, 0, 100);
            return 검사 != null;
        }

        private void 제품투입수행()
        {
            if (!검사번호확인(정보주소.제품투입, out 검사결과 검사, Replys.Yes)) return;
            Global.피씨통신.제품투입전송(검사.검사번호);
        }

        private void 평탄측정수행()
        {
            if (!검사번호확인(정보주소.평탄측정, out 검사결과 검사, Replys.No)) return;
            new Thread(() => Global.피씨통신.평탄측정전송(검사.검사번호)).Start();
        }
        public void 평탄측정완료() => this.정보쓰기(정보주소.평탄측정, false);

        private void 두께측정수행()
        {
            if (!검사번호확인(정보주소.두께측정, out 검사결과 검사, Replys.Yes)) return;
            검사.SetResult(검사항목.Thickness, 제품두께);
            Debug.WriteLine($"두께측정: {검사.검사번호} => {제품두께}");
        }

        private void 큐알각인시작전송(검사결과 검사)
        {
            Debug.WriteLine("QR각인 수행.");
            new Thread(() =>
            {
                Global.큐알각인.각인시작(검사);
            }).Start();
        }

        private void 큐알리딩시작전송(검사결과 검사)
        {
            Debug.WriteLine("QR리딩 수행.");
            new Thread(() =>
            {
                Global.큐알리더.리딩시작(검사);
                this.정보쓰기(정보주소.큐알리딩, false);
            }).Start();
        }

        private void 라벨출력전송(검사결과 검사)
        {
            new Thread(() =>
            {
                Global.라벨인쇄.라벨출력(검사);
                this.라벨출력 = false;
                this.라벨부착 = true;
            }).Start();
        }

        private void 큐알각인수행()
        {
            if (!검사번호확인(정보주소.큐알각인, out 검사결과 검사, Replys.No)) return;

            검사.결과계산();
            if (Global.환경설정.강제배출)
            {
                if (Global.환경설정.양품불량)
                    큐알각인시작전송(검사);
                else
                {
                    Debug.WriteLine("QR각인 안함.");
                    this.입출자료.SetDelay(정보주소.큐알각인, 0, 100);
                    return;
                }
            }
            else
            {
                if (!Global.환경설정.큐알각인여부 || 검사.측정결과 != 결과구분.OK)
                {
                    Debug.WriteLine("QR각인 안함.");
                    this.입출자료.SetDelay(정보주소.큐알각인, 0, 100);
                    return;
                }
                큐알각인시작전송(검사);
            }
        }

        private void 큐알리딩수행()
        {
            if (!검사번호확인(정보주소.큐알리딩, out 검사결과 검사, Replys.No)) return;
            if (Global.환경설정.강제배출)
            {
                if (Global.환경설정.양품불량)
                    큐알리딩시작전송(검사);
                else
                {
                    Debug.WriteLine("QR각인 안함.");
                    this.입출자료.SetDelay(정보주소.큐알리딩, 0, 100);
                    return;
                }
            }
            else
            {
                if (!Global.환경설정.큐알리딩여부 || 검사.측정결과 != 결과구분.OK)
                {
                    Debug.WriteLine("QR리딩 안함.");
                    this.입출자료.SetDelay(정보주소.큐알리딩, 0, 100);
                    return;
                }
                큐알리딩시작전송(검사);
            }
        }

        public void 라벨출력수행()
        {
            if (!검사번호확인(정보주소.라벨출력, out 검사결과 검사, Replys.No)) return;

            if (Global.환경설정.강제배출)
            {
                if (Global.환경설정.양품불량)
                {
                    this.입출자료.SetDelay(정보주소.라벨출력, 0, 100);
                    this.라벨부착 = false;
                    return;
                }
                else
                    라벨출력전송(검사);
            }
            else
            {
                if (!Global.환경설정.라벨인쇄여부 || 검사.측정결과 == 결과구분.OK)
                {
                    this.입출자료.SetDelay(정보주소.라벨출력, 0, 100);
                    this.라벨부착 = false;
                    return;
                }
                라벨출력전송(검사);
            }
        }

        public void 라벨부착수행()
        {

        }

        private void 영상촬영수행()
        {
            if (검사번호확인(정보주소.상부치수, out 검사결과 상부치수, Replys.Yes))
                Global.피씨통신.상부치수전송(상부치수.검사번호);
            if (검사번호확인(정보주소.하부표면, out 검사결과 하부표면, Replys.Yes))
                Global.피씨통신.하부표면전송(하부표면.검사번호);
            if (검사번호확인(정보주소.상부표면, out 검사결과 상부표면, Replys.Yes))
                Global.피씨통신.상부표면전송(상부표면.검사번호);
        }

        // 최종 검사 결과 보고
        private void 검사완료수행()
        {
            if (!검사번호확인(정보주소.결과요청, out 검사결과 검사, Replys.None)) return;
            if (검사 == null) { 검사결과전송(false); return; }
            new Thread(() =>
            {
                Global.검사자료.검사결과계산(검사);
                if (Global.환경설정.강제배출) 검사결과전송(Global.환경설정.양품불량);
                else 검사결과전송(검사.측정결과 == 결과구분.OK);
            }).Start(); ;
        }
        private void 검사결과전송(Boolean 양품)
        {
            DateTime dt = DateTime.Now;
            Debug.WriteLine($"검사결과 => {양품}");
            this.양품불량 = 양품;
            this.입출자료.SetDelay(정보주소.결과요청, 0, 100);
        }

        // 핑퐁
        private void 통신핑퐁수행()
        {
            if (!this.입출자료[정보주소.통신핑퐁].Passed()) return;
            this.통신핑퐁 = !this.통신핑퐁;
            this.통신상태알림?.Invoke();
        }

        private DateTime 테스트위치확인 = DateTime.Now;
        private Boolean 테스트수행()
        {
            통신핑퐁수행();
            if ((DateTime.Now - 테스트위치확인).TotalSeconds < 5) return true;
            테스트위치확인 = DateTime.Now;
            List<Int32> 목록 = new List<Int32>();
            foreach (정보주소 주소 in typeof(정보주소).GetEnumValues())
            {
                Int32 val = 0;
                if (주소 >= 정보주소.제품투입N && 주소 <= 정보주소.결과요청N)
                    val = Global.Random.Next(0, 32);
                목록.Add(val);
            }
            입출자료.Set(목록.ToArray());
            검사위치확인();
            return true;
        }
    }
}
