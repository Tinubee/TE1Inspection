using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace TE1.Schemas
{
    public partial class 환경설정
    {
        [JsonIgnore]
        public const Hosts HostType = Hosts.Server;
        [JsonIgnore]
        public const String DefaultPath = @"C:\IVM\TE1Insp3";
        [JsonIgnore]
        public const String LogTableName = "logs3";

        [Translation("Server Address", "서버주소"), JsonProperty("ServerAddress")]
        public String 서버주소 { get; set; } = "localhost";
        [Translation("Server Port", "서버포트"), JsonProperty("ServerPort")]
        public Int32 서버포트 { get; set; } = 1884;

        [Translation("QR Marker Host", "QR 프린터 주소"), JsonProperty("QrMarkerHost")]
        public String 큐알각인주소 { get; set; } = "192.168.20.211";
        [Translation("QR Marker Port", "QR 프린터 포트"), JsonProperty("QrMarkerPort")]
        public Int32 큐알각인포트 { get; set; } = 50002;
        [Translation("Use QR Marker", "QR 프린팅 유무"), JsonProperty("UseQrMarker")]
        public Boolean 큐알각인여부 { get; set; } = true;

        [Translation("QR Reader Host", "QR 리더기 주소"), JsonProperty("QrReaderHost")]
        public String 큐알리더주소 { get; set; } = "192.168.20.212";
        [Translation("QR Reader Port", "QR 리더기 포트"), JsonProperty("QrReaderPort")]
        public Int32 큐알리더포트 { get; set; } = 23;
        [Translation("Use QR Reader", "QR 리딩 유무"), JsonProperty("UseQrReader")]
        public Boolean 큐알리딩여부 { get; set; } = true;

        [Translation("Label Printer Host", "라벨 프린터 주소"), JsonProperty("LabelPrinterHost")]
        public String 라벨인쇄주소 { get; set; } = "192.168.20.213";
        [Translation("Label Printer Port", "라벨 프린터 포트"), JsonProperty("LabelPrinterPort")]
        public Int32 라벨인쇄포트 { get; set; } = 9100;
        [Translation("Use Label Printer", "라벨 프린팅 유무"), JsonProperty("UseLabelPrinter")]
        public Boolean 라벨인쇄여부 { get; set; } = true;

        [JsonProperty("Force Ejection")]
        public Boolean 강제배출 { get; set; } = false;
        [JsonProperty("Force OK/NG")]
        public Boolean 양품불량 { get; set; } = true;

        [Translation("Daytime", "주간 시작"), JsonProperty("Daytime")]
        public TimeSpan 주간시작 { get; set; } = new TimeSpan(7, 0, 0);
        [Translation("Nighttime", "야간 시작"), JsonProperty("Nighttime")]
        public TimeSpan 야간시작 { get; set; } = new TimeSpan(19, 0, 0);

        public DateTime 주간시작일시() => 주간시작일시(DateTime.Today);
        public DateTime 주간종료일시() => 주간종료일시(DateTime.Today);
        public DateTime 야간시작일시() => 야간종료일시(DateTime.Today);
        public DateTime 야간종료일시() => 야간종료일시(DateTime.Today.AddDays(1));
        public DateTime 주간시작일시(DateTime day) => new DateTime(day.Year, day.Month, day.Day).Add(this.주간시작);
        public DateTime 주간종료일시(DateTime day) => new DateTime(day.Year, day.Month, day.Day).Add(this.야간시작).AddMilliseconds(-1);
        public DateTime 야간시작일시(DateTime day) => new DateTime(day.Year, day.Month, day.Day).Add(this.야간시작);
        public DateTime 야간종료일시(DateTime day) => new DateTime(day.Year, day.Month, day.Day).Add(this.주간시작).AddMilliseconds(-1);

        public enum 주야구분 { [Description("D")] 주간, [Description("N")] 야간 }
        public 주야구분 GetDN() => DateTime.Now.Hour >= 주간시작.Hours && DateTime.Now.Hour < 야간시작.Hours ? 주야구분.주간 : 주야구분.야간;
    }
}
