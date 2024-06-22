using Cognex.DataMan.SDK;
using DevExpress.Utils.Extensions;
using MvUtils;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace TE1.Schemas
{
    public enum 리더명령
    {
        [Description("None")]
        명령없음,
        [Description("ON")]
        리딩시작,
        [Description("OFF")]
        리딩종료,
    }

    public class 큐알리더
    {
        public delegate void Communication(큐알장치.통신구분 통신, 리더명령 명령, String mesg);
        public event Communication 송신수신알림;
        public String 로그영역 => "QR Reader";
        public String Host { get => Global.환경설정.큐알리더주소; set => Global.환경설정.큐알리더주소 = value; }
        public Int32  Port { get => Global.환경설정.큐알리더포트; set => Global.환경설정.큐알리더포트 = value; }
        public String User => "admin";
        public String Pass => String.Empty;
        public Boolean 연결여부 => MAN != null && MAN.IsConnected;
        private DataManSystem MAN;
        private 리더명령 현재명령 = 리더명령.명령없음;
        private 검사결과 현재검사 = null;

        public void Init() => Connect();
        public void Close()
        {
            if (MAN == null) return;
            MAN.SystemConnected -= Connected;
            MAN.SystemDisconnected -= Disconnected;
            MAN.ReadStringArrived -= Read;
            //MAN.ImageArrived -= ImageArrived;
            //MAN.CodeQualityDataArrived -= CodeQualityDataArrived;
            MAN.Dispose();
            MAN = null;
        }

        public void 리딩시작(검사결과 검사)
        {
            현재검사 = 검사;
            Trig(리더명령.리딩시작);
        }
        private void Trig(리더명령 명령)
        {
            현재명령 = 명령;
            String command = $"TRIGGER {Utils.GetDescription(명령)}";
            MAN.SendCommand(command);
            송신수신알림?.Invoke(큐알장치.통신구분.TX, 명령, command);
        }

        private void Connect()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            try
            {
                MAN = new DataManSystem(new EthSystemConnector(IPAddress.Parse(Host), Port) { UserName = User, Password = Pass });
                MAN.DefaultTimeout = 5000;
                MAN.SetKeepAliveOptions(true, 3000, 1000);
                MAN.SystemConnected += Connected;
                MAN.SystemDisconnected += Disconnected;
                MAN.ReadStringArrived += Read;
                //MAN.ImageArrived += ImageArrived;
                //MAN.CodeQualityDataArrived += CodeQualityDataArrived;
                MAN.Connect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "Connect");
                Close();
            }
        }

        private void Connected(object sender, EventArgs args) =>
            Debug.WriteLine("Connected", 로그영역);
        private void Disconnected(object sender, EventArgs args) => Close();
        private void Read(object sender, ReadStringArrivedEventArgs args)
        {
            큐알내용 큐알 = new 큐알내용(args.ReadString.Trim());
            if (현재명령 == 리더명령.리딩시작)
            {
                //Utils.DebugSerializeObject(큐알);
                this.현재검사?.큐알정보등록(큐알);
                this.현재검사 = null;
            }
            송신수신알림?.Invoke(큐알장치.통신구분.RX, 현재명령, args.ReadString);
            this.현재명령 = 리더명령.명령없음;
        }
        private void ImageArrived(object sender, ImageArrivedEventArgs args) =>
            Debug.WriteLine($"{args.Image.Size}", "ImageArrived");
        private void CodeQualityDataArrived(object sender, CodeQualityDataArrivedEventArgs args) =>
            Debug.WriteLine($"{args.CodeQualityData}", "CodeQualityDataArrived");
    }

    public class 큐알내용
    {
        public String 큐알 { get; set; } = String.Empty;
        public 큐알등급 등급 => OverallGrade;
        public 큐알등급 OverallGrade { get; set; } = 큐알등급.X;
        public 큐알등급 CellContrast { get; set; } = 큐알등급.X;
        public 큐알등급 AxialNonUniformity { get; set; } = 큐알등급.X;
        public 큐알등급 PrintGrowth { get; set; } = 큐알등급.X;
        public 큐알등급 UEC { get; set; } = 큐알등급.X;
        public 큐알등급 CellModulation { get; set; } = 큐알등급.X;
        public 큐알등급 FixedPatternDamage { get; set; } = 큐알등급.X;
        public 큐알등급 GridNonUniformity { get; set; } = 큐알등급.X;

        private const String None = "NONE";
        public 큐알내용() { }
        public 큐알내용(String data)
        {
            //MFR01407AA;2401220200A	Overall Grade:F,Cell Contrast:A,Axial Non Uniformity:A,Print Growth:-,UEC:B,Cell Modulation:D,Fixed Pattern Damage:F,Grid Non Uniformity:A
            if (String.IsNullOrEmpty(data) || data.StartsWith(None)) return;
            String[] vals = data.Split(new Char[] { '\t' });
            if (vals.Length < 2 ) return;
            큐알 = vals[0];
            vals[1].Split(new Char[] { ',' }).ForEach(d => SetGrade(d));
        }

        private void SetGrade(String data)
        {
            String[] d = data.Split(new Char[] { ':' });
            if (d.Length < 2 ) return;
            String name = d[0].Replace(" ", "").Trim();
            String grade = d[1].Trim().ToUpper();
            PropertyInfo p = typeof(큐알내용).GetProperty(name);
            if (p == null) return;
            p.SetValue(this, GetGrade(grade));
        }

        public static 큐알등급 GetGrade(String grade)
        {
            foreach(큐알등급 등급 in typeof(큐알등급).GetEnumValues())
                if (Utils.GetDescription(등급) == grade) return 등급;
            return 큐알등급.X;
        }
    }
}
