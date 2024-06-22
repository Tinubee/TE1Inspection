using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    public enum 통신포트
    {
        None = 0,
        COM3 = 3,
        //COM4 = 4,
        //COM5 = 5,
        //COM6 = 6,
        //COM7 = 7,
        //COM8 = 8,
        //COM9 = 9,
        TCP1000 = 1000,
    }

    public enum 조명채널
    {
        CH01 = 1,
        CH02 = 2,
        CH03 = 3,
        CH04 = 4,
        CH05 = 5,
        CH06 = 6,
        CH07 = 7,
        CH08 = 8,
        CH09 = 9, 
        CH10 = 10, 
        CH11 = 11, 
        CH12 = 12, 
        CH13 = 13, 
        CH14 = 14, 
        CH15 = 15, 
        CH16 = 16,
    }

    public abstract class 컨트롤러통신
    {
        public virtual String 로그영역 { get; set; } = "Lights Controller";
        public virtual 통신포트 통신포트 { get; set; } = 통신포트.COM3;
        public virtual Int32 BaudRate { get; set; } = 9600;
        public virtual String HostName { get; set; } = String.Empty;
        public virtual Byte[] STX { get; set; }
        public virtual Byte[] ETX { get; set; }

        public abstract Boolean IsOpen { get; }
        public abstract Boolean SendCommand(String 구분, Byte[] 명령);
        public abstract void Init();
        public abstract Boolean Open();
        public abstract void Close();

        //public class 시리얼 : 컨트롤러통신
        //{
        //    public override Boolean IsOpen => 통신장치 != null && 통신장치.IsOpen;
        //    public SerialPort 통신장치;

        //    public override void Init()
        //    {
        //        if (Global.환경설정.동작구분 != 동작구분.Live) return;

        //        통신장치 = new SerialPort();
        //        통신장치.PortName = 통신포트.ToString();
        //        통신장치.BaudRate = BaudRate;
        //        통신장치.DataBits = (Int32)8;
        //        통신장치.StopBits = StopBits.One;
        //        통신장치.Parity = Parity.None;
        //        통신장치.DataReceived += DataReceived;
        //        통신장치.ErrorReceived += ErrorReceived;
        //    }
        //    public override Boolean Open()
        //    {
        //        if (통신장치 == null) return false;
        //        try
        //        {
        //            통신장치.Open();
        //            return 통신장치.IsOpen;
        //        }
        //        catch (Exception ex)
        //        {
        //            Close();
        //            Global.오류로그(로그영역, "장치연결", "조명 제어 포트에 연결할 수 없습니다. " + ex.Message, true);
        //            return false;
        //        }
        //    }
        //    public override void Close()
        //    {
        //        if (통신장치 == null) return;
        //        if (IsOpen) 통신장치.Close();
        //        통신장치.Dispose();
        //        통신장치 = null;
        //    }

        //    public override Boolean SendCommand(String 구분, Byte[] 명령)
        //    {
        //        if (!IsOpen)
        //        {
        //            Global.오류로그(로그영역, 구분, "조명컨트롤러 포트에 연결할 수 없습니다.", true);
        //            return false;
        //        }
        //        try
        //        {
        //            통신장치.Write($"{STX}{명령}{ETX}");
        //            //Debug.WriteLine($"{STX}{Command}{ETX}".Trim(), 구분);
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            Global.오류로그(로그영역, 구분, ex.Message, true);
        //            return false;
        //        }
        //    }

        //    public virtual void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        //    {
        //        Debug.WriteLine($"ErrorReceived 포트={this.통신포트}, {e.EventType.ToString()}", this.로그영역);
        //        Debug.WriteLine(e.ToString());
        //    }
        //    public virtual void DataReceived(object sender, SerialDataReceivedEventArgs e)
        //    {
        //        SerialPort sp = (SerialPort)sender;
        //        String data = sp.ReadExisting();
        //        Debug.WriteLine($"DataReceived 포트={this.통신포트}, {data}", this.로그영역);
        //    }
        //}

        public class 이더넷 : 컨트롤러통신
        {
            public override Boolean IsOpen => PollingState;
            public Boolean 동작여부 = false;
            public Int32 대기시간 = 100;

            public TcpClient 통신장치 = null;
            private NetworkStream Stream => 통신장치?.GetStream();
            private Int32 PollingPeriod = 3000;
            private DateTime PollingTime = DateTime.Today.AddDays(-1);
            private Boolean PollingState = false;
            public virtual Boolean Connect()
            {
                if ((DateTime.Now - PollingTime).TotalMilliseconds < PollingPeriod) return PollingState;
                try 
                {
                    if (통신장치 == null)
                    {
                        통신장치 = new TcpClient();
                        통신장치.Connect(HostName, (Int32)통신포트);
                    }
                    PollingState = 통신장치.Client.Poll(1000, SelectMode.SelectWrite); 
                }
                catch { PollingState = false; }
                PollingTime = DateTime.Now;
                if (PollingState) return true;

                try { 통신장치?.Close(); }
                catch (Exception ex) { Debug.WriteLine(ex.Message, "Connection failed!"); }
                통신장치?.Dispose();
                통신장치 = null;
                return false;
            }
            public override void Init() { }
            public override Boolean Open()
            {
                동작여부 = true;
                Connect(); //new Thread(Run).Start();
                return true;
            }
            public override void Close()
            {
                this.동작여부 = false;
                if (통신장치 == null) return;
                if (this.IsOpen)
                {
                    this.통신장치?.Client?.Shutdown(SocketShutdown.Both);
                    this.통신장치?.Close();
                }
                this.통신장치?.Dispose();
                this.통신장치 = null;
            }
            public override Boolean SendCommand(String 구분, Byte[] 명령)
            {
                if (!IsOpen) return false;
                if (명령.Length < 1) return false;
                try
                {
                    List<Byte> data = new List<Byte>();
                    data.AddRange(STX);
                    data.AddRange(명령);
                    data.AddRange(ETX);
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("Write: [");
                    //data.ForEach(b => sb.Append(b.ToString("X2") + " "));
                    //sb.Append($"] {data.Count}");
                    //Debug.WriteLine(sb.ToString());
                    if (this.통신장치 == null) return false;
                    Stream.Write(data.ToArray(), 0, data.Count);
                    Stream.Flush();
                    //new Thread(() => { Thread.Sleep(100); Read(); }).Start();
                    return true;
                }
                catch (Exception ex)
                {
                    Global.오류로그(로그영역, 구분, ex.Message, true);
                    return false;
                }
            }

            public virtual void Read()
            {
                try
                {
                    if (통신장치 != null || 통신장치.Available < 1) return;
                    Byte[] buffer = new Byte[4096];
                    Int32 read = Stream.Read(buffer, 0, buffer.Length);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Read: [");
                    for (int i = 0; i < read; i++)
                        sb.Append(buffer[i].ToString("X2") + " ");
                    sb.Append($"] {read}");
                    Debug.WriteLine(sb.ToString());
                }
                catch (Exception e) { Debug.WriteLine(e.Message, "Read Error"); }
            }

            public virtual void Run()
            {
                while (동작여부)
                {
                    Thread.Sleep(대기시간);
                    if (!동작여부) break;
                    if (!Connect()) continue;
                    Read();
                }
                Close();
                Debug.WriteLine("통신 종료", "조명컨트롤러");
            }
        }
    }

    public abstract class 조명컨트롤러
    {
        public virtual String 로그영역 { get => 장치통신.로그영역; set => 장치통신.로그영역 = value; }
        public abstract 컨트롤러통신 장치통신 { get; set; }
        public virtual 통신포트 통신포트 { get => 장치통신.통신포트; set => 장치통신.통신포트 = value; }
        public virtual String HostName { get => 장치통신.HostName; set => 장치통신.HostName = value; }
        public virtual Boolean IsOpen => 장치통신.IsOpen;
        public abstract Int32 최대밝기 { get; }
        public virtual Int32 밝기변환(Int32 밝기) => Convert.ToInt32(Math.Round((Double)this.최대밝기 * 밝기 / 100));
        public abstract void Init();
        public abstract Boolean Set(조명정보 정보);
        public abstract Boolean Save(조명정보 정보);
        public abstract Boolean TurnOn(조명정보 정보);
        public abstract Boolean TurnOff(조명정보 정보);
        public abstract void TurnOn(Dictionary<조명채널, Int32> 정보);
        public abstract void TurnOff();

        public virtual Boolean Open() => 장치통신.Open();
        public virtual void Close() => 장치통신.Close();
        public virtual Boolean SendCommand(String 구분, Byte[] 명령) => 장치통신.SendCommand(구분, 명령);
    }

    public class ALTE8RSUL : 조명컨트롤러
    {
        public override 컨트롤러통신 장치통신 { get; set; } = new 컨트롤러통신.이더넷();
        public override String 로그영역 { get; set; } = nameof(ALTE8RSUL);
        public override Int32 최대밝기 { get; } = 255;
        internal virtual Byte 개별제어 => 0x12;
        internal virtual Byte 모두제어 => 0x15;
        internal virtual Byte 저장제어 => 0x1B;
        internal virtual 조명채널 최대채널 => 조명채널.CH08;
        internal virtual Byte PageIndex => 0x00;
        internal virtual Boolean HasPage => false;

        public override void Init()
        {
            장치통신.STX = new Byte[] { 0x4C };
            장치통신.ETX = new Byte[] { 0x0D, 0x0A };
            장치통신.Init();
        }

        internal virtual Byte CheckSum(List<Byte> bytes)
        {
            Byte sum = 0;
            bytes.ForEach(x => sum = (Byte)(sum ^ x));
            return sum;
        }
        internal virtual Byte[] 밝기명령(조명채널 채널, Int32 밝기)
        {
            List<Byte> bytes = new List<Byte>();
            bytes.Add(개별제어);
            if (HasPage) bytes.Add(PageIndex);
            bytes.Add(Convert.ToByte((Int32)채널 - 1));
            bytes.Add((Byte)Convert.ToChar(밝기변환(밝기)));
            bytes.Add(CheckSum(bytes));
            return bytes.ToArray();
        }

        public override Boolean Set(조명정보 정보) => 장치통신.SendCommand($"{정보.카메라} Set", 밝기명령(정보.채널, 정보.밝기));
        public override Boolean Save(조명정보 정보) => 장치통신.SendCommand($"{정보.카메라} Save", new Byte[] { 저장제어, 0x19 });
        public override Boolean TurnOn(조명정보 정보) => 장치통신.SendCommand($"{정보.카메라} On", 밝기명령(정보.채널, 정보.밝기));
        public override Boolean TurnOff(조명정보 정보) => 장치통신.SendCommand($"{정보.카메라} Off", 밝기명령(정보.채널, 0));

        public override void TurnOn(Dictionary<조명채널, Int32> 자료)
        {
            List<Byte> bytes = new List<Byte>();
            bytes.Add(모두제어);
            if (HasPage) bytes.Add(PageIndex);
            foreach (조명채널 채널 in typeof(조명채널).GetEnumValues())
            {
                if (최대채널 < 채널) break;
                Int32 밝기 = 0;
                if (자료.ContainsKey(채널)) 밝기 = 자료[채널];
                bytes.Add((Byte)Convert.ToChar(밝기변환(밝기)));
            }
            bytes.Add(CheckSum(bytes));
            장치통신.SendCommand("Turn On", bytes.ToArray());
        }
        public override void TurnOff()
        {
            List<Byte> bytes = new List<Byte>();
            bytes.Add(모두제어);
            if (HasPage) bytes.Add(PageIndex);
            foreach (조명채널 채널 in typeof(조명채널).GetEnumValues())
            {
                if (최대채널 < 채널) break;
                bytes.Add(0x00);
            }
            bytes.Add(CheckSum(bytes));
            장치통신.SendCommand("Turn On", bytes.ToArray());
        }
    }

    public class ALTLSP300EUL : ALTE8RSUL
    {
        public override 컨트롤러통신 장치통신 { get; set; } = new 컨트롤러통신.이더넷();
        public override String 로그영역 { get; set; } = nameof(ALTLSP300EUL);
        public override Int32 최대밝기 { get; } = 255;
        internal override Byte 개별제어 => 0x16;
        internal override Byte 모두제어 => 0x17;
        internal override Byte 저장제어 => 0x1C;
        internal override 조명채널 최대채널 => 조명채널.CH08;
    }

    public class ALTLSP1000EUL : ALTE8RSUL
    {
        public override String 로그영역 { get; set; } = nameof(ALTLSP1000EUL);
        internal override Byte 개별제어 => 0x15;
        internal override Byte 모두제어 => 0x16;
        internal override Byte 저장제어 => 0x1B;
        internal override 조명채널 최대채널 => 조명채널.CH16;
        internal override Boolean HasPage => true;
        public override Boolean Save(조명정보 정보) => 장치통신.SendCommand($"{정보.카메라} Save", new Byte[] { 저장제어, 저장제어 });
    }

    public class 조명정보
    {
        [JsonProperty("Camera"), Translation("Camera", "카메라")]
        public 카메라구분 카메라 { get; set; } = 카메라구분.None;
        [JsonProperty("Port"), Translation("Port", "포트")]
        public 통신포트 포트 { get; set; } = 통신포트.None;
        [JsonProperty("Channel"), Translation("Channel", "채널")]
        public 조명채널 채널 { get; set; } = 조명채널.CH01;
        [JsonProperty("Brightness"), Translation("Brightness", "밝기")]
        public Int32 밝기 { get; set; } = 100;
        [JsonProperty("Description"), Translation("Description", "설명")]
        public String 설명 { get; set; } = String.Empty;
        [JsonIgnore, Translation("TurnOn", "켜짐")]
        public Boolean 켜짐 { get; set; } = false;
        [JsonIgnore]
        public 조명컨트롤러 컨트롤러;

        public 조명정보() { }
        public 조명정보(카메라구분 카메라, 조명컨트롤러 컨트롤러)
        {
            this.카메라 = 카메라;
            this.컨트롤러 = 컨트롤러;
            this.포트 = 컨트롤러.통신포트;
        }

        //public Boolean Get() { return this.컨트롤러.Get(this); }
        public Boolean Set()
        {
            this.켜짐 = this.컨트롤러.Set(this);
            return this.켜짐;
        }
        public Boolean TurnOn()
        {
            if (this.켜짐) return true;
            this.켜짐 = this.컨트롤러.TurnOn(this);
            return this.켜짐;
        }
        public Boolean TurnOff()
        {
            if (!this.켜짐) return true;
            this.켜짐 = false;
            return this.컨트롤러.TurnOff(this);
        }
        public Boolean OnOff()
        {
            if (this.켜짐) return this.TurnOff();
            else return this.TurnOn();
        }

        public void Set(조명정보 정보)
        {
            this.밝기 = 정보.밝기;
            this.설명 = 정보.설명;
        }
    }

    public partial class 조명제어 : List<조명정보>
    {
        [JsonIgnore]
        private const String 로그영역 = "Lights";
        [JsonIgnore]
        private string 저장파일 => Path.Combine(Global.환경설정.기본경로, "Lights.json");
        [JsonIgnore]
        public Int32 DelayTime = 5;

        public 조명정보 GetItem(카메라구분 카메라)
        {
            foreach (조명정보 조명 in this)
                if (조명.카메라 == 카메라) return 조명;
            return null;
        }
        public 조명정보 GetItem(카메라구분 카메라, 통신포트 포트, 조명채널 채널)
        {
            foreach (조명정보 조명 in this)
                if (조명.카메라 == 카메라 && 조명.포트 == 포트 && 조명.채널 == 채널) return 조명;
            return null;
        }

        public void Load()
        {
            if (!File.Exists(this.저장파일)) return;
            try
            {
                List<조명정보> 자료 = JsonConvert.DeserializeObject<List<조명정보>>(File.ReadAllText(this.저장파일), Utils.JsonSetting());
                foreach (조명정보 정보 in 자료)
                {
                    조명정보 조명 = this.GetItem(정보.카메라, 정보.포트, 정보.채널);
                    if (조명 == null) continue;
                    조명.Set(정보);
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "Load", ex.Message, false);
            }
        }

        public void Save()
        {
            if (!Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, Utils.JsonSetting())))
                Global.오류로그(로그영역, "Save", "Failed to save settings.", true);
        }

        public void Set()
        {
            Task.Run(() =>
            {
                foreach (조명정보 조명 in this)
                {
                    if (!조명.Set()) 조명.TurnOn();
                    Task.Delay(200).Wait();
                    조명.TurnOff();
                    Task.Delay(200).Wait();
                }
            });
        }

        public void Set(카메라구분 카메라)
        {
            new Thread(() =>
            {
                foreach (조명정보 조명 in this)
                {
                    if (조명.카메라 == 카메라)
                    {
                        조명.Set();
                        Thread.Sleep(DelayTime);
                    }
                }
            }).Start();
        }

        public void Set(카메라구분 카메라, 통신포트 포트, Int32 밝기)
        {
            new Thread(() =>
            {
                foreach (조명정보 정보 in this)
                {
                    if (정보.카메라 == 카메라 && 정보.포트 == 포트)
                    {
                        정보.밝기 = 밝기;
                        정보.Set();
                        Thread.Sleep(DelayTime);
                    }
                }
            });
        }

        public void TurnOnOff(카메라구분 카메라, Boolean IsOn)
        {
            if (IsOn) this.TurnOn(카메라);
            else this.TurnOff(카메라);
        }

        public void TurnOn(카메라구분 카메라 = 카메라구분.None)
        {
            new Thread(() =>
            {
                foreach (조명정보 정보 in this)
                {
                    if (카메라 != 카메라구분.None && 정보.카메라 != 카메라) continue;
                    정보.TurnOn();
                    Thread.Sleep(DelayTime);
                }
            }).Start();
        }

        public void TurnOff(카메라구분 카메라 = 카메라구분.None)
        {
            new Thread(() =>
            {
                foreach (조명정보 정보 in this)
                {
                    if (카메라 != 카메라구분.None && 정보.카메라 != 카메라) continue;
                    정보.TurnOff();
                    Thread.Sleep(DelayTime);
                }
            }).Start();
        }
    }
}
