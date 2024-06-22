using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TE1.Schemas
{
    public partial class 조명제어
    {
        [JsonIgnore]
        public ALTE8RSUL 컨트롤러1;
        [JsonIgnore]
        public ALTLSP300EUL 컨트롤러2;

        [JsonIgnore]
        public Boolean 정상여부 => this.컨트롤러1.IsOpen && this.컨트롤러2.IsOpen;

        public void Init()
        {
            this.컨트롤러1 = new ALTE8RSUL() { 통신포트 = 통신포트.TCP1000, HostName = Global.환경설정.조명주소1 };
            this.컨트롤러2 = new ALTLSP300EUL() { 통신포트 = 통신포트.TCP1000, HostName = Global.환경설정.조명주소2 };

            this.컨트롤러1.Init();
            this.컨트롤러2.Init();

            // 컨트롤러 당 카메라 1대씩 연결
            this.Add(new 조명정보(카메라구분.Cam01, 컨트롤러1) { 채널 = 조명채널.CH01, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam01, 컨트롤러1) { 채널 = 조명채널.CH02, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam01, 컨트롤러1) { 채널 = 조명채널.CH03, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam01, 컨트롤러1) { 채널 = 조명채널.CH04, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam02, 컨트롤러1) { 채널 = 조명채널.CH05, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam02, 컨트롤러1) { 채널 = 조명채널.CH06, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam02, 컨트롤러1) { 채널 = 조명채널.CH07, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam03, 컨트롤러2) { 채널 = 조명채널.CH01, 밝기 = 100 });
            this.Add(new 조명정보(카메라구분.Cam03, 컨트롤러2) { 채널 = 조명채널.CH02, 밝기 = 100 });

            this.Load();
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            this.Open();
            this.Set();
        }

        public void Open()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            this.컨트롤러1?.Open();
            this.컨트롤러2?.Open();
        }

        public void Close()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return;
            this.컨트롤러1?.TurnOff();
            this.컨트롤러2?.TurnOff();
            this.컨트롤러1?.Close();
            this.컨트롤러2?.Close();
        }

        public void TurnOn()
        {
            new Thread(() =>
            {
                Dictionary<조명채널, Int32> 자료1 = new Dictionary<조명채널, Int32>();
                Dictionary<조명채널, Int32> 자료2 = new Dictionary<조명채널, Int32>();
                foreach(조명정보 정보 in this)
                {
                    if (정보.컨트롤러 == this.컨트롤러1)
                    {
                        if (자료1.ContainsKey(정보.채널)) continue;
                        자료1.Add(정보.채널, 정보.밝기);
                    }
                    else if (정보.컨트롤러 == this.컨트롤러2)
                    {
                        if (자료2.ContainsKey(정보.채널)) continue;
                        자료2.Add(정보.채널, 정보.밝기);
                    }
                }
                this.컨트롤러1.TurnOn(자료1);
                this.컨트롤러2.TurnOn(자료2);
            }).Start();
        }

        public void TurnOff()
        {
            new Thread(() => {
                this.컨트롤러1.TurnOff();
                this.컨트롤러2.TurnOff();
            }).Start();
        }
    }
}
