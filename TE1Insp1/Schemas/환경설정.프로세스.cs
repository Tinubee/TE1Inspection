using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;

namespace TE1.Schemas
{
    public partial class 환경설정
    {
        [JsonIgnore]
        public const Hosts HostType = Hosts.Measure;
        [JsonIgnore]
        public const String DefaultPath = @"C:\IVM\TE1Insp1";
        [JsonIgnore]
        public const String LogTableName = "logs1";

        [Translation("Server Address", "서버주소"), JsonProperty("ServerAddress")]
        public String 서버주소 { get; set; } = "192.168.13.31"; // 1 <-> 3
        [Translation("Server Port", "서버포트"), JsonProperty("ServerPort")]
        public Int32 서버포트 { get; set; } = 1884;

        [Description("조명 컨트롤러1 IP"), JsonProperty("LightsHost1")]
        public String 조명주소1 { get; set; } = "192.168.5.10";
        [Description("조명 컨트롤러2 IP"), JsonProperty("LightsHost2")]
        public String 조명주소2 { get; set; } = "192.168.6.10";
    }
}
