using MvUtils;
using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TE1.Schemas
{
    //public enum 앰프구분
    //{
    //    [Description("None"), DXDescription("")]
    //    None = 0,
    //    [Description("AMP1"), DXDescription("S1")]
    //    AMP1 = 1,
    //}

    public enum 센서명령
    {
        [Description("NONE")]
        없음,
        [Description("SW")]
        영점,
        [Description("M0")]
        측정,
    }

    //public enum 센서오류
    //{
    //    오류없음,
    //    측정갯수,
    //    시간초과,
    //    자료오류,
    //    통신오류,
    //    기타오류,
    //}

    #region 장치정보
    public class 평탄센서
    {
        [JsonProperty("host")]
        public String 연결주소 { get; set; } = String.Empty;
        [JsonProperty("port")]
        public Int32 연결포트 { get; set; } = 64000;

        [JsonIgnore]
        private const String 로그영역 = "센서장치";
        [JsonIgnore]
        private static Encoding ENC = Encoding.ASCII;

        public void Init()
        {
            this.연결주소 = Global.환경설정.센서주소;
            this.연결포트 = Global.환경설정.센서포트;
        }

        private TcpClient Open()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return null;
            
            var client = new TcpClient();
            try { client.Connect(연결주소, 연결포트); }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "장치연결", $"장치에 연결할 수 없습니다.\r\n{ex.Message}", true);
                client.Close();
                client.Dispose();
                return null;
            }
            return client;
        }

        public Boolean 영점설정() 
        {
            using (var client = this.Open())
            {
                if (client == null) return false;
                foreach (Datums datum in typeof(Datums).GetEnumValues())
                {
                    String command = $"{Utils.GetDescription(센서명령.영점)},{((Int32)datum).ToString("d2")},001,+000000001";
                    String response = 명령전송(client, command, 100);
                    Debug.WriteLine(response);
                }
                client.Close();
                return true;
            }
        }

        public 센서자료 측정하기(검사결과 검사)
        {
            센서자료 자료 = new 센서자료();
            using (var client = this.Open())
            {
                if (client == null) return 자료;
                String response = this.명령전송(client, Utils.GetDescription(센서명령.측정), 500);
                자료.결과추가(검사, response);
            }
            return 자료;
        }

        private String 명령전송(TcpClient client, String command, Int32 delay)
        {
            if (client == null || !client.Connected) return String.Empty;
            Debug.WriteLine(command, "명령전송");
            DateTime s = DateTime.Now;
            Byte[] buffer = ENC.GetBytes(command + "\r\n");

            NetworkStream stream = client.GetStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();

            Thread.Sleep(delay);
            List<Byte> rcv = new List<byte>();
            while (client.Available > 0)
            {
                Byte[] bf = new byte[4096];
                Int32 read = stream.Read(bf, 0, bf.Length);
                for (Int32 i = 0; i < read; i++) rcv.Add(bf[i]);
            }
            return ENC.GetString(rcv.ToArray()).Trim();
        }

        public Boolean Ping()
        {
            if (String.IsNullOrEmpty(연결주소)) return true;

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions() { DontFragment = true };
            try
            {
                // Create a buffer of 32 bytes of data to be transmitted.
                PingReply reply = pingSender.Send(연결주소, 1000, ENC.GetBytes("PING"), options);
                Debug.WriteLine($"PingTest {연결주소}[{reply.Status}]");
                return reply.Status == IPStatus.Success;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
    }
    #endregion
}
