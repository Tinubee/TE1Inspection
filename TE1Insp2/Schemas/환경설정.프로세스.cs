using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace TE1.Schemas
{
    public partial class 환경설정
    {
        [JsonIgnore]
        public const Hosts HostType = Hosts.Surface;
        [JsonIgnore]
        public const String DefaultPath = @"C:\IVM\TE1Insp2";
        [JsonIgnore]
        public const String LogTableName = "logs2";

        [Translation("Server Address", "서버주소"), JsonProperty("ServerAddress")]
        public String 서버주소 { get; set; } = "192.168.23.32"; // 2 <-> 3
        [Translation("Server Port", "서버포트"), JsonProperty("ServerPort")]
        public Int32 서버포트 { get; set; } = 1884;

        [Translation("Sensor Address", "센서주소"), JsonProperty("SensorAddress")]
        public String 센서주소 { get; set; } = "192.168.8.10";
        [Translation("Sensor Port", "센서포트"), JsonProperty("SensorPort")]
        public Int32 센서포트 { get; set; } = 64000;

        [Description("조명 컨트롤러 IP"), JsonProperty("LightsHost")]
        public String 조명주소 { get; set; } = "192.168.5.10";
    }
}