using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    [Description("MQTT Message Types")]
    public enum 피씨명령
    {
        통신핑퐁 = 0,
        연결알림 = 1,
        연결종료 = 2,
        상태정보 = 3,
        검사설정 = 4,
        검사결과 = 9,

        // 검사 명령
        제품투입 = 51,
        평탄측정 = 52,
        평탄완료 = 53,
        상부치수 = 54,
        상부완료 = 55,
        하부표면 = 56,
        상부표면 = 57,
        //큐알각인 = 57,
        //큐알리딩 = 58,
        //라벨출력 = 59,
    }

    public partial class 피씨통신
    {
        public String  서버주소 = "localhost";
        public Int32   서버포트 = 1884;
        public Hosts   피씨구분 = 환경설정.HostType;
        public Boolean 연결여부 => Client != null && Client.IsConnected;
        public String  로그영역 => $"Socket({피씨구분.ToString()})";
        public Int32 KeepAlivePeriod => 10;
        public const String Root = "TE1";
        private IMqttClient Client = null;
        private Timer Worker;

        public void Init()
        {
            서버주소 = Global.환경설정.서버주소;
            서버포트 = Global.환경설정.서버포트;

            MqttFactory factory = new MqttFactory();
            Client = factory.CreateMqttClient();
            Client.ConnectedAsync += Connected;
            Client.DisconnectedAsync += Disconnected;
            Client.ApplicationMessageReceivedAsync += ReceivedMessage;
        }

        public Boolean Connect()
        {
            if (Client == null) return false;
            if (연결여부) return true;
            MqttClientOptions options = new MqttClientOptionsBuilder().WithTcpServer(서버주소, 서버포트).WithProtocolVersion(MqttProtocolVersion.V500)
                .WithClientId(피씨구분.ToString()).WithKeepAlivePeriod(TimeSpan.FromSeconds(KeepAlivePeriod)).Build();
            Debug.WriteLine(options.KeepAlivePeriod, "KeepAlivePeriod");
            using (var token = new CancellationTokenSource(TimeSpan.FromSeconds(5)))
            {
                try { Client.ConnectAsync(options, token.Token).Wait(); }
                catch (Exception ex) { Debug.WriteLine(ex.Message, "Connection"); }
            }
            return 연결여부;
        }

        public Boolean Open() => Connect();

        public void Close()
        {
            if (Client == null) return;
            if (연결여부)
            {
                연결종료전송();
                MqttFactory factory = new MqttFactory();
                var options = factory.CreateClientDisconnectOptionsBuilder().WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection).Build();
                Client.DisconnectAsync(options, CancellationToken.None).Wait();
            }
            Client?.Dispose();
            Client = null;
        }

        private Task Disconnected(MqttClientDisconnectedEventArgs arg)
        {
            Global.정보로그(로그영역, "Disconnected", "The connection has ended.", false);
            return Task.CompletedTask;
        }

        private Task Connected(MqttClientConnectedEventArgs arg)
        {
            if (Subscribe())
            {
                if (피씨구분 != Hosts.Server)
                {
                    연결알림전송();
                    Worker?.Dispose();
                    Int32 ms = KeepAlivePeriod * 1000;
                    Worker = new Timer(TimerCallback, null, ms, ms);
                }
            }
            else Global.오류로그(로그영역, "Connected", "Unable to connect to server.", true);
            return Task.CompletedTask;
        }
        private void TimerCallback(Object state) => 통신핑퐁전송();

        private Task ReceivedMessage(MqttApplicationMessageReceivedEventArgs arg)
        {
            try
            {
                //Debug.WriteLine($"{피씨구분.ToString()}={arg.ApplicationMessage.PayloadSegment.Count}", "Received");
                통신자료 자료 = 통신자료.Deserialize<통신자료>(arg.ApplicationMessage.PayloadSegment.Array);
                if (자료 == null) new Exception("Received data is incorrect.");
                자료분석(자료);
            }
            catch(Exception ex) { Debug.WriteLine(ex.Message, "Received"); }
            return Task.CompletedTask;
        }

        private static String GetTopic(Hosts host) => $"{Root}/{host.ToString()}";
        private static List<String> GetTopics(IEnumerable<Hosts> hosts)
        {
            List<String> topics = new List<String>();
            foreach (var item in hosts) topics.Add(GetTopic(item));
            return topics;
        }
        private List<String> GetTopics()
        {
            List<String> topics = new List<String>();
            topics.Add(GetTopic(피씨구분));
            if (피씨구분 != Hosts.Server)
                topics.Add(Root);
            return topics;
        }
        private Boolean Subscribe()
        {
            MqttFactory factory = new MqttFactory();
            MqttClientSubscribeOptionsBuilder builder = factory.CreateSubscribeOptionsBuilder();
            GetTopics().ForEach(t => builder.WithTopicFilter(f => f.WithTopic(t).WithRetainAsPublished(false).WithNoLocal(true)
                .WithRetainHandling(MQTTnet.Protocol.MqttRetainHandling.DoNotSendOnSubscribe).WithExactlyOnceQoS()));
            MqttClientSubscribeOptions options = builder.Build();
            Task<MqttClientSubscribeResult> r = Client.SubscribeAsync(options, CancellationToken.None);
            r.Wait();
            return r.IsCompleted;
        }

        #region Publish
        public void Publish(피씨명령 명령) => Publish(new 통신자료(명령) { 발신 = 피씨구분 }.Get());
        public void Publish(Int32 번호, 피씨명령 명령) => Publish(new 통신자료(명령) { 발신 = 피씨구분, 번호 = 번호 }.Get());
        public void Publish(Int32 번호, 피씨명령 명령, Hosts 수신) => Publish(수신, new 통신자료(명령) { 발신 = 피씨구분, 번호 = 번호 }.Get());
        public void Publish(Int32 번호, Object 자료, 피씨명령 명령) => Publish(new 통신자료(명령, 자료) { 발신 = 피씨구분, 번호 = 번호 }.Get());
        public void Publish(Object 자료, 피씨명령 명령) => Publish(new 통신자료(명령, 자료) { 발신 = 피씨구분}.Get());
        //public void Publish(Object 자료, 피씨명령 명령, 피씨구분 수신) => Publish(수신, new 피씨자료(명령, 자료) { 발신 = 피씨구분 }.Get());
        //public void Publish(Object 자료, 피씨명령 명령, IEnumerable<피씨구분> 수신) => Publish(수신, new 피씨자료(명령, 자료) { 발신 = 피씨구분 }.Get());

        private void Publish(Byte[] payload)
        {
            if (피씨구분 == Hosts.Server) Publish(Root, payload);
            else Publish(Hosts.Server, payload);
        }
        private void Publish(Hosts host, Byte[] payload) => Publish(GetTopic(host), payload);
        private void Publish(IEnumerable<Hosts> hosts, Byte[] payload) => Publish(GetTopics(hosts), payload);
        private void Publish(IEnumerable<String> topics, Byte[] payload)
        {
            foreach(String topic in topics)
                Publish(topic, payload);
        }
        private async void Publish(String topic, Byte[] payload)
        {
            if (!연결여부) return;
            MqttApplicationMessageBuilder builder = new MqttApplicationMessageBuilder().WithPayload(payload).WithTopic(topic)
                .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce).WithRetainFlag(false);
            await Client.PublishAsync(builder.Build(), CancellationToken.None);
            //Debug.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fff")} => {topic}", "Published");
        }
        #endregion

        #region 기본명령 수행 
        private void 통신핑퐁전송() => Publish(피씨명령.통신핑퐁);
        private void 연결알림전송() => Publish(피씨명령.연결알림);
        private void 연결종료전송() => Publish(피씨명령.연결종료);
        #endregion
    }

    [ProtoContract]
    public class 통신자료
    {
        [ProtoMember(1)] public DateTime 일시 = DateTime.Now;
        [ProtoMember(2)] public Hosts    발신 = Hosts.None;
        [ProtoMember(3)] public 피씨명령  명령 = 피씨명령.통신핑퐁;
        [ProtoMember(4)] public Byte[]   자료 = null;
        [ProtoMember(5)] public Int32    번호 = 0;

        public 통신자료() { }
        public 통신자료(피씨명령 type) => 명령 = type;
        public 통신자료(피씨명령 type, Object data)
        {
            명령 = type;
            자료 = Serialize(data);
        }

        public T Get<T>() => Deserialize<T>(자료);
        public Byte[] Get() => Serialize(this);

        public static Byte[] Serialize(Object data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize(ms, data);
                return ms.ToArray();
            }
        }
        public static T Deserialize<T>(Byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
                return Serializer.Deserialize<T>(ms);
        }
    }

    [ProtoContract]
    public class 상태정보
    {
        [ProtoMember(1)]  public Boolean 자동수동 = false;
        [ProtoMember(2)]  public Boolean 시작정지 = false;
        [ProtoMember(3)]  public 모델구분 현재모델 = 모델구분.UPR3P24S;
        [ProtoMember(10)] public Int32   양품갯수 = 0;
        [ProtoMember(11)] public Int32   불량갯수 = 0;

        public void Init()
        {
            //this.자동수동 = Global.장치통신.자동수동;
            //this.시작정지 = Global.장치통신.시작정지;
            this.현재모델 = Global.환경설정.선택모델;
            this.양품갯수 = Global.모델자료.선택모델.양품갯수;
            this.불량갯수 = Global.모델자료.선택모델.불량갯수;
        }
    }
}
