using ActUtlType64Lib;
using DevExpress.Utils.Extensions;
using MvUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    // PLC 통신
    [Description("MELSEC Q06UDV")]
    public partial class 장치통신
    {
        public event Global.BaseEvent 동작상태알림;
        public event Global.BaseEvent 통신상태알림;
        public event Global.BaseEvent 검사위치알림;

        #region 기본상수 및 멤버
        private static String 로그영역 = "PLC 통신";
        private const Int32 스테이션번호 = 0;
        private const Int32 입출체크간격 = 60;
        private DateTime 시작일시 = DateTime.Now;
        private Boolean 작업여부 = false;  // 동작 FLAG 
        private ActUtlType64 PLC = null;
        public Boolean 정상여부 = false;

        private enum 정보주소 : Int32
        {
            [Address("B1000", 1000, 0, true)] 통신핑퐁,
            [Address("B1001", 1000, 1, true)] 피씨상태,
            [Address("B1002", 1000, -1, true)] 자동수동,
            [Address("B1003", 1000, -1, true)] 시작정지,
            [Address("B1004", 3000, 0, true)] 제품투입,
            [Address("B1005", 3000, 0, true)] 평탄측정,
            [Address("B1006", 3000, 0, true)] 상부치수,
            [Address("B1007", 3000, 0, true)] 하부표면,
            [Address("B1008", 3000, 0, true)] 상부표면,
            [Address("B1009", 3000, 0, true)] 두께측정,
            [Address("B100A", 3000, 0, true)] 큐알각인,
            [Address("B100B", 3000, 0, true)] 큐알리딩,
            [Address("B100C", 3000, 0, true)] 라벨출력, // 신호 수신 시 라벨 데이터 전송
            [Address("B100D", 3000, 0, true)] 라벨부착, // 데이터 전송 후 라벨 부착 신호 전송
            [Address("B100E", 3000, 0, true)] 결과요청,
            [Address("B100F", 3000, 0, true)] 양품불량,
            [Address("B1010", 3000, 0, true)] 번호리셋,

            [Address("W0")] 제품투입N,
            [Address("W1")] 평탄측정N,
            [Address("W2")] 상부치수N,
            [Address("W3")] 하부표면N,
            [Address("W4")] 상부표면N,
            [Address("W5")] 큐알각인N,
            [Address("W6")] 큐알리딩N,
            [Address("W7")] 라벨출력N,
            [Address("W8")] 결과요청N,

            [Address("WA")] 제품두께,
            [Address("WD", 0, 0, false)] 생산수량,
            [Address("WF")] 모델번호,
        }

        private 통신자료 입출자료 = new 통신자료();
        public static Boolean ToBool(Int32 val) => val != 0;
        public static Int32 ToInt(Boolean val) => val ? 1 : 0;
        private Int32 정보읽기(정보주소 구분) => this.입출자료.Get(구분);
        private Boolean 신호읽기(정보주소 구분) => ToBool(this.입출자료.Get(구분));
        private void 정보쓰기(정보주소 구분, Int32 val) => this.입출자료.Set(구분, val);
        private void 정보쓰기(정보주소 구분, Boolean val) => this.입출자료.Set(구분, ToInt(val));

        #region 입출신호
        public Boolean 통신핑퐁 { get => 신호읽기(정보주소.통신핑퐁); set => 정보쓰기(정보주소.통신핑퐁, value); }
        public Boolean 피씨상태 { get => 신호읽기(정보주소.피씨상태); set => 정보쓰기(정보주소.피씨상태, value); }
        public Boolean 자동수동 => 신호읽기(정보주소.자동수동);
        public Boolean 시작정지 => 신호읽기(정보주소.시작정지);

        public Boolean 제품투입 { get => 신호읽기(정보주소.제품투입); set => 정보쓰기(정보주소.제품투입, value); }
        public Boolean 평탄측정 { get => 신호읽기(정보주소.평탄측정); set => 정보쓰기(정보주소.평탄측정, value); }
        public Boolean 상부치수 { get => 신호읽기(정보주소.상부치수); set => 정보쓰기(정보주소.상부치수, value); }
        public Boolean 하부표면 { get => 신호읽기(정보주소.하부표면); set => 정보쓰기(정보주소.하부표면, value); }
        public Boolean 상부표면 { get => 신호읽기(정보주소.상부표면); set => 정보쓰기(정보주소.상부표면, value); }
        public Boolean 큐알각인 { get => 신호읽기(정보주소.큐알각인); set => 정보쓰기(정보주소.큐알각인, value); }
        public Boolean 큐알리딩 { get => 신호읽기(정보주소.큐알리딩); set => 정보쓰기(정보주소.큐알리딩, value); }
        public Boolean 라벨출력 { get => 신호읽기(정보주소.라벨출력); set => 정보쓰기(정보주소.라벨출력, value); }
        public Boolean 라벨부착 { get => 신호읽기(정보주소.라벨부착); set => 정보쓰기(정보주소.라벨부착, value); }
        public Boolean 번호리셋 { get => 신호읽기(정보주소.번호리셋); set => 정보쓰기(정보주소.번호리셋, value); }
        public Boolean 결과요청 { get => 신호읽기(정보주소.결과요청); set => 정보쓰기(정보주소.결과요청, value); }
        public Boolean 양품불량 { get => 신호읽기(정보주소.양품불량); set => 정보쓰기(정보주소.양품불량, value); }

        public Int32 제품투입N => 정보읽기(정보주소.제품투입N);
        public Int32 평탄측정N => 정보읽기(정보주소.평탄측정N);
        public Int32 상부치수N => 정보읽기(정보주소.상부치수N);
        public Int32 하부표면N => 정보읽기(정보주소.하부표면N);
        public Int32 상부표면N => 정보읽기(정보주소.상부표면N);
        public Int32 큐알각인N => 정보읽기(정보주소.큐알각인N);
        public Int32 큐알리딩N => 정보읽기(정보주소.큐알리딩N);
        public Int32 라벨출력N => 정보읽기(정보주소.라벨출력N);
        public Int32 결과요청N => 정보읽기(정보주소.결과요청N);

        public Int32 제품두께 => 정보읽기(정보주소.제품두께);
        public Int32 모델번호 => 정보읽기(정보주소.모델번호);
        public Int32 생산수량 { get => 정보읽기(정보주소.생산수량); set => 정보쓰기(정보주소.생산수량, value); }

        // 트리거 입력 시 버퍼에 입력
        private Dictionary<정보주소, Int32> 인덱스버퍼 = new Dictionary<정보주소, Int32>();
        #endregion
        #endregion

        #region 기본함수
        public void Init()
        {
            this.PLC = new ActUtlType64();
            this.PLC.ActLogicalStationNumber = 스테이션번호;
            if (Global.환경설정.동작구분 == 동작구분.Live)
                this.입출자료.Init(new Action<정보주소, Int32>((주소, 값) => 자료전송(주소, 값)));
            else this.입출자료.Init(null);
        }
        public void Close() { this.Stop(); }

        public void Start()
        {
            if (this.작업여부) return;
            this.작업여부 = true;
            this.정상여부 = true;
            this.시작일시 = DateTime.Now;
            if (Global.환경설정.동작구분 == 동작구분.Live)
            {
                this.입출자료갱신();
                this.입출자료리셋();
                this.인덱스리셋확인();
                this.동작상태알림?.Invoke();
            }
            new Thread(장치통신작업) { Priority = ThreadPriority.Highest }.Start();
        }

        public void Stop() => this.작업여부 = false;
        public Boolean Open() { this.정상여부 = PLC.Open() == 0; return this.정상여부; }

        private void 연결종료()
        {
            try
            {
                PLC.Close();
                Global.정보로그(로그영역, "PLC 연결종료", $"서버에 연결을 종료하였습니다.", false);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "PLC 연결종료", $"서버 연결을 종료하는 중 오류가 발생하였습니다.\r\n{ex.Message}", false);
            }
        }

        private void 자료전송(정보주소 주소, Int32 값)
        {
            DateTime 시간 = DateTime.Now;
            Int32 오류 = 0;
            SetDevice(입출자료.Address(주소), 값, out 오류);
            통신오류알림(오류);
        }

        private void 장치통신작업()
        {
            Global.정보로그(로그영역, "PLC 통신", $"통신을 시작합니다.", false);
            //#if DEBUG
            //List<Double> 시간 = new List<Double>();
            //Int32 횟수 = 300;
            //#endif
            while (this.작업여부)
            {
                //#if DEBUG
                //DateTime 현재 = DateTime.Now;
                //#endif
                입출자료분석();
                Thread.Sleep(입출체크간격);

                //#if DEBUG
                //시간.Add((DateTime.Now - 현재).TotalMilliseconds);
                //if (시간.Count >= 300)
                //{
                //    Int32 최소 = (Int32)시간.Min();
                //    Int32 최대 = (Int32)시간.Max();
                //    Int32 평균 = (Int32)시간.Average();
                //    시간.Clear();
                //    Task.Run(() => {
                //        Global.정보로그(로그영역, "PLC 동작정보", $"Count={횟수}, Min={최소}, Max={최대}, Avg={평균}", false);
                //    });
                //}
                //#endif
            }

            Global.정보로그(로그영역, "PLC 통신", $"통신이 종료되었습니다.", false);
            this.연결종료();
        }

        private void 입출자료리셋()
        {
            this.인덱스버퍼.Clear();
            foreach (정보주소 주소 in typeof(정보주소).GetEnumValues())
            {
                AddressAttribute a = Utils.GetAttribute<AddressAttribute>(주소);
                if (a.Default < 0) continue;
                Int32 value = a.Default;
                if (주소 == 정보주소.생산수량) value = Global.모델자료.선택모델.전체갯수;
                정보쓰기(주소, value);
            }
        }

        // 검사자료 로드 후 수행해야 함
        public void 인덱스리셋확인()
        {
            if (Global.검사자료.Count < 1)
            {
                Debug.WriteLine("인덱스 리셋");
                this.번호리셋 = true;
            }
        }

        public void 생산수량전송() => this.생산수량 = Global.모델자료.선택모델.전체갯수;
        #endregion

        #region Get / Set 함수
        private Int32[] ReadDeviceRandom(String[] 주소, out Int32 오류코드)
        {
            Int32[] 자료 = new Int32[주소.Length];
            오류코드 = PLC.ReadDeviceRandom(String.Join("\n", 주소), 주소.Length, out 자료[0]);
            return 자료;
        }

        private Int32 GetDevice(String Address, out Int32 오류코드)
        {
            Int32 value;
            오류코드 = PLC.GetDevice(Address, out value);
            return value;
        }

        private Boolean SetDevice(String Address, Int32 Data, out Int32 오류코드)
        {
            오류코드 = PLC.SetDevice(Address, Data);
            //Debug.WriteLine($"{Data}, {오류코드}", Address);
            return 오류코드 == 0;
        }

        /*
        private Int16 GetDevice2(String Address, out Int32 오류코드)
        {
            Int16 value;
            오류코드 = PLC.GetDevice2(Address, out value);
            return value;
        }

        private Boolean SetDevice2(String Address, Int16 Data, out Int32 오류코드)
        {
            오류코드 = PLC.SetDevice2(Address, Data);
            //Debug.WriteLine($"{Data}, {오류코드}", Address);
            return 오류코드 == 0;
        }
        */
        #endregion

        #region 기본 클래스 및 함수
        private static UInt16 ToUInt16(BitArray bits)
        {
            UInt16 res = 0;
            for (int i = 0; i < 16; i++)
                if (bits[i]) res |= (UInt16)(1 << i);
            return res;
        }
        private static BitArray FromUInt16(UInt16 val) => new BitArray(BitConverter.GetBytes(val));

        internal class AddressAttribute : Attribute
        {
            public String Address = String.Empty;
            public Int32 Delay = 0;               // Raise 간격
            public Int32 Default = -1;            // 초기값
            public Boolean IsIO = false;          // IO 여부
            public AddressAttribute(String address) : this(address, 0) { }
            public AddressAttribute(String address, Int32 delay) : this(address, delay, false) { }
            public AddressAttribute(String address, Int32 delay, Int32 value) : this(address, delay, value, false) { }
            public AddressAttribute(String address, Int32 delay, Boolean io) : this(address, delay, -1, io) { }
            public AddressAttribute(String address, Int32 delay, Int32 value, Boolean io)
            {
                this.Address = address;
                this.Delay = delay;
                this.Default = value;
                this.IsIO = io;
            }
        }

        private class 통신정보
        {
            public 정보주소 구분;
            public Int32 순번 = 0;
            public Int32 정보 = 0;
            public String 주소 = String.Empty;
            public DateTime 시간 = DateTime.MinValue;
            public Int32 지연 = 0;
            public Boolean 변경 = false;

            public 통신정보(정보주소 구분)
            {
                this.구분 = 구분;
                this.순번 = (Int32)구분;
                AddressAttribute a = Utils.GetAttribute<AddressAttribute>(구분);
                this.주소 = a.Address;
                this.지연 = a.Delay;
            }

            public Boolean Passed()
            {
                if (this.지연 <= 0) return true;
                return (DateTime.Now - 시간).TotalMilliseconds >= this.지연;
            }

            public Boolean Set(Int32 val, Boolean force = false)
            {
                if (this.정보.Equals(val) || !force && !this.Passed())
                {
                    this.변경 = false;
                    return false;
                }

                this.정보 = val;
                this.시간 = DateTime.Now;
                this.변경 = true;
                return true;
            }
        }
        private class 통신자료 : Dictionary<정보주소, 통신정보>
        {
            private Action<정보주소, Int32> Transmit;
            public String[] 주소목록;
            public 통신자료()
            {
                List<String> 주소 = new List<String>();
                foreach (정보주소 구분 in typeof(정보주소).GetEnumValues())
                {
                    통신정보 정보 = new 통신정보(구분);
                    if (정보.순번 < 0) continue;
                    this.Add(구분, 정보);
                    주소.Add(정보.주소);
                }
                this.주소목록 = 주소.ToArray();
            }

            public void Init(Action<정보주소, Int32> transmit) => this.Transmit = transmit;

            public String Address(정보주소 구분)
            {
                if (!this.ContainsKey(구분)) return String.Empty;
                return this[구분].주소;
            }

            public Int32 Get(정보주소 구분)
            {
                if (!this.ContainsKey(구분)) return 0;
                return this[구분].정보;
            }

            public void Set(Int32[] 자료)
            {
                foreach (통신정보 정보 in this.Values)
                {
                    Int32 val = 자료[정보.순번];
                    Boolean 변경 = 정보.Set(val);
                    //if (변경) Debug.WriteLine($"{Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss}")} {(주소구분)정보.순번} = {정보.현재}", "주소자료변경");
                }
            }

            // Return : Changed
            public Boolean Set(정보주소 구분, Int32 value)
            {
                if (!this[구분].Set(value, true)) return false;
                this.Transmit?.Invoke(구분, value);
                return true;
            }

            public void SetDelay(정보주소 구분, Int32 value, Int32 resetTime)
            {
                if (resetTime <= 0)
                {
                    if (!this[구분].Set(value, true)) return;
                    this.Transmit?.Invoke(구분, value);
                }
                Task.Run(() => {
                    Task.Delay(resetTime);
                    if (this[구분].Set(value, true))
                        this.Transmit?.Invoke(구분, value);
                });
            }

            public Boolean Changed(정보주소 구분) => this[구분].변경;
            public Boolean Firing(정보주소 구분, Boolean 상태) => this[구분].변경 && ToBool(this[구분].정보) == 상태;
            public Dictionary<정보주소, Int32> Changes(정보주소 시작, 정보주소 종료) => this.Changes((Int32)시작, (Int32)종료);
            public Dictionary<정보주소, Int32> Changes(Int32 시작, Int32 종료)
            {
                Dictionary<정보주소, Int32> 변경 = new Dictionary<정보주소, Int32>();
                foreach (정보주소 구분 in typeof(정보주소).GetEnumValues())
                {
                    Int32 번호 = (Int32)구분;
                    if (번호 < 시작 || 번호 > 종료 || !this[구분].변경) continue;
                    변경.Add(구분, this[구분].정보);
                }
                return 변경;
            }
        }
        #endregion
    }
}