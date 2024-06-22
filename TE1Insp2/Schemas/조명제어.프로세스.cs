using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TE1.Schemas
{
    public partial class 조명제어
    {
        [JsonIgnore]
        public ALTLSP1000EUL 컨트롤러;

        [JsonIgnore]
        public Boolean 정상여부 => this.컨트롤러.IsOpen;

        public void Init()
        {
            this.컨트롤러 = new ALTLSP1000EUL() { 통신포트 = 통신포트.TCP1000, HostName = Global.환경설정.조명주소 };
            this.컨트롤러.Init();

            // 컨트롤러 당 카메라 1대씩 연결
            this.Add(new 조명정보(카메라구분.Cam04, 컨트롤러) { 채널 = 조명채널.CH03, 밝기 = 100, 설명 = "Bottom Left" });
            this.Add(new 조명정보(카메라구분.Cam04, 컨트롤러) { 채널 = 조명채널.CH04, 밝기 = 100, 설명 = "Bottom Right" });
            this.Add(new 조명정보(카메라구분.Cam05, 컨트롤러) { 채널 = 조명채널.CH01, 밝기 = 100, 설명 = "Top Left" });
            this.Add(new 조명정보(카메라구분.Cam06, 컨트롤러) { 채널 = 조명채널.CH02, 밝기 = 100, 설명 = "Top Right" });
            this.Load();
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            this.Open();
            this.Set();
        }

        public void Open()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            this.컨트롤러?.Open();
        }

        public void Close()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            this.컨트롤러?.TurnOff();
            this.컨트롤러?.Close();
        }

        public void TurnOn()
        {
            new Thread(() =>
            {
                Dictionary<조명채널, Int32> 자료 = new Dictionary<조명채널, Int32>();
                foreach (조명정보 정보 in this)
                {
                    if (자료.ContainsKey(정보.채널)) continue;
                    자료.Add(정보.채널, 정보.밝기);
                }
                this.컨트롤러.TurnOn(자료);
            }).Start();
        }

        public void TurnOff()
        {
            new Thread(() => { this.컨트롤러.TurnOff(); }).Start();
        }

        public void TurnOnBottom()
        {
            new Thread(() =>
            {
                Dictionary<조명채널, Int32> 자료 = new Dictionary<조명채널, Int32>();
                foreach (조명정보 정보 in this.Where(e => e.채널 == 조명채널.CH03 || e.채널 == 조명채널.CH04).ToList())
                {
                    if (자료.ContainsKey(정보.채널)) continue;
                    자료.Add(정보.채널, 정보.밝기);
                }
                this.컨트롤러.TurnOn(자료);
            }).Start();
        }
        public void TurnOffBottom()
        {
            new Thread(() =>
            {
                Dictionary<조명채널, Int32> 자료 = new Dictionary<조명채널, Int32>();
                자료.Add(조명채널.CH03, 0);
                자료.Add(조명채널.CH04, 0);
                this.컨트롤러.TurnOn(자료);
            }).Start();
        }
        public void TurnOnTop()
        {
            new Thread(() =>
            {
                Dictionary<조명채널, Int32> 자료 = new Dictionary<조명채널, Int32>();
                foreach (조명정보 정보 in this.Where(e => e.채널 == 조명채널.CH01 || e.채널 == 조명채널.CH02).ToList())
                {
                    if (자료.ContainsKey(정보.채널)) continue;
                    자료.Add(정보.채널, 정보.밝기);
                }
                this.컨트롤러.TurnOn(자료);
            }).Start();
        }
        public void TurnOffTop()
        {
            new Thread(() =>
            {
                Dictionary<조명채널, Int32> 자료 = new Dictionary<조명채널, Int32>();
                자료.Add(조명채널.CH01, 0);
                자료.Add(조명채널.CH02, 0);
                this.컨트롤러.TurnOn(자료);
            }).Start();
        }
    }
}
