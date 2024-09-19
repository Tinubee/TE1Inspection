using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TE1.Schemas
{
    public partial class 피씨통신
    {
        private Dictionary<Hosts, DateTime> 연결정보 = new Dictionary<Hosts, DateTime>();

        #region 자료수신
        private Boolean 자료분석(통신자료 자료)
        {
            통신자료수신(자료.발신);
            if (자료.명령 == 피씨명령.연결알림) return 피씨연결수신(자료);
            if (자료.명령 == 피씨명령.연결종료) return 피씨종료수신(자료);
            if (자료.명령 == 피씨명령.검사설정) return 검사설정수신(자료);
            if (자료.명령 == 피씨명령.평탄완료) return 평탄완료수신(자료);
            if (자료.명령 == 피씨명령.상부완료) return 치수완료수신(자료);
            //if (자료.명령 == 피씨명령.VIP모드) return VIP모드수신(자료);
            return false;
        }

        public Boolean MeasureConnected => 피씨연결여부(Hosts.Measure);
        public Boolean SurfaceConnected => 피씨연결여부(Hosts.Surface);
        public Boolean 피씨연결여부(Hosts host) => 연결정보.ContainsKey(host) && (DateTime.Now - 연결정보[host]).TotalSeconds <= KeepAlivePeriod * 1.5;
        private Boolean 통신자료수신(Hosts host) { 연결정보[host] = DateTime.Now; return true; }

        private Boolean 피씨연결수신(통신자료 자료)
        {
            Debug.WriteLine($"PC 연결 수신완료.");

            if (Global.상태정보.자동수동 != Global.장치상태.자동수동)
                Global.상태정보.자동수동 = Global.장치상태.자동수동;
            if (Global.상태정보.시작정지 != Global.장치상태.시작정지)
                Global.상태정보.시작정지 = Global.장치상태.시작정지;
            if (Global.상태정보.마스터모드 != Global.장치상태.마스터모드)
                Global.상태정보.마스터모드 = Global.장치상태.마스터모드;

            제품상태전송(Global.상태정보);
            return true;
        }

        public void 검사설정송신(검사설정 자료)
        {
            검사설정송신(Hosts.Measure, 자료);
            검사설정송신(Hosts.Surface, 자료);
        }

        public void 검사설정송신(Hosts host, 검사설정 자료)
        {
            List<검사정보> 설정 = new List<검사정보>();
            foreach (검사정보 정보 in 자료)
            {
                if (DeviceInfoAttribute.GetHost(정보.검사장치) != host) continue;
                설정.Add(정보);
            }
            Publish(new 통신자료(피씨명령.검사설정, 설정) { 발신 = 피씨구분, 번호 = 자료.모델번호 }.Get());
        }

        private Boolean 검사설정수신(통신자료 자료)
        {
            Debug.WriteLine("검사설정 수신 완료.");
            모델정보 모델 = Global.모델자료.GetItem((모델구분)자료.번호);
            if (모델 == null) return false;
            List<검사정보> 설정자료 = 자료.Get<List<검사정보>>();
            foreach (검사정보 정보 in 설정자료)
            {
                검사정보 설정 = new 검사정보();
                if (정보.검사항목 == 검사항목.None)
                    설정 = 모델.검사설정.GetItem(정보.검사명칭);
                else
                    설정 = 모델.검사설정.GetItem(정보.검사항목);

                설정.Set(정보, DateTime.Now);
            }
            모델.검사설정.Save();
            return true;
        }

        private Boolean 피씨종료수신(통신자료 자료)
        {
            연결정보.Remove(자료.발신);
            return !연결정보.ContainsKey(자료.발신);
        }
        private Boolean 치수완료수신(통신자료 자료)
        {
            Debug.WriteLine($"치수완료수신 => {자료.번호}");
            검사결과 검사 = Global.검사자료.검사항목찾기(자료.번호);
            if (검사 == null) return false;

            List<검사정보> 치수 = 자료.Get<List<검사정보>>();
            foreach (검사정보 정보 in 치수)
            {
                //Debug.WriteLine($"{정보.검사항목} : {정보.측정값}");
                검사.SetResult(정보.검사항목, (Double)정보.측정값);
            }
            return true;
        }
        private Boolean 평탄완료수신(통신자료 자료)
        {
            Debug.WriteLine("평탄완료수신.");
            Global.장치통신.평탄측정완료();
            검사결과 검사 = Global.검사자료.검사항목찾기(자료.번호);
            if (검사 == null) return false;

            List<검사정보> 센서 = 자료.Get<List<검사정보>>();
            foreach (검사정보 정보 in 센서)
                검사.SetResult(정보.검사항목, (Double)정보.측정값);
            return true;
        }
        #endregion

        #region 명령전송
        public void 제품투입전송(Int32 검사번호) => Publish(검사번호, 피씨명령.제품투입);
        public void 평탄측정전송(Int32 검사번호) => Publish(검사번호, 피씨명령.평탄측정, Hosts.Surface);
        public void 상부치수전송(Int32 검사번호) => Publish(검사번호, 피씨명령.상부치수, Hosts.Measure);
        public void 하부표면전송(Int32 검사번호) => Publish(검사번호, 피씨명령.하부표면, Hosts.Surface);
        public void 상부표면전송(Int32 검사번호) => Publish(검사번호, 피씨명령.상부표면, Hosts.Surface);
        public void 제품상태전송(상태정보 정보) => Publish(정보, 피씨명령.상태정보);
        public void VIP모드상태전송(Boolean 모드) => Publish(모드, 피씨명령.VIP모드);
        #endregion
    }
}
