using DevExpress.XtraEditors;
using MvUtils;
using System;
using System.Diagnostics;

namespace TE1.UI.Controls
{
    public partial class DeviceLamp : XtraUserControl
    {
        public DeviceLamp()
        {
            InitializeComponent();
        }

        private 장치상태 장치통신;
        private 장치상태 각인기;
        private 장치상태 리더기;
        private 장치상태 컴퓨터1;
        private 장치상태 컴퓨터2;
        private 장치상태 프린터;

        public void Init()
        {
            this.장치통신 = new 장치상태(this.e장치통신, true);
            this.컴퓨터1 = new 장치상태(this.e컴퓨터1);
            this.컴퓨터2 = new 장치상태(this.e컴퓨터2);
            this.각인기 = new 장치상태(this.e각인기);
            this.리더기 = new 장치상태(this.e리더기);
            this.프린터 = new 장치상태(this.e프린터);
            Global.장치통신.통신상태알림 += 통신상태알림;
            this.통신상태알림();
        }

        private void 통신상태알림()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(통신상태알림));
                return;
            }

            if (Global.장치통신.정상여부) this.장치통신.Set(Global.장치통신.통신핑퐁 ? 상태구분.정상 : 상태구분.대기);
            else this.장치통신.Set(상태구분.오류);
            if (Global.환경설정.동작구분 != Schemas.동작구분.Live) return;

            this.컴퓨터1.Set(Global.장치상태.컴퓨터1);
            this.컴퓨터2.Set(Global.장치상태.컴퓨터2);
            this.각인기.Set(Global.장치상태.큐알각인);
            this.리더기.Set(Global.장치상태.큐알리더);
            this.프린터.Set(Global.장치상태.라벨인쇄);
        }

        private enum 상태구분
        {
            없음,
            대기,
            정상,
            오류,
        }
        private class 장치상태
        {
            private SvgImageBox 도구;
            private 상태구분 현재상태 = 상태구분.없음;
            private DevExpress.Utils.Svg.SvgImage 대기 = null;
            private DevExpress.Utils.Svg.SvgImage 정상 = null;
            private DevExpress.Utils.Svg.SvgImage 오류 = null;

            public 장치상태(SvgImageBox tool, Boolean HasWait = false)
            {
                this.도구 = tool;
                this.정상 = Utils.SetSvgStyle(tool.SvgImage, Utils.SvgStyles.Green);
                this.오류 = Utils.SetSvgStyle(tool.SvgImage, Utils.SvgStyles.Red);
                if (HasWait) this.대기 = Utils.SetSvgStyle(tool.SvgImage, Utils.SvgStyles.Blue);
                this.도구.SvgImage = this.오류;
            }

            public void Set(Boolean 상태)
            {
                this.Set(상태 ? 상태구분.정상 : 상태구분.오류);
            }

            public void Set(상태구분 상태)
            {
                if (this.현재상태 == 상태) return;
                this.현재상태 = 상태;
                if (상태 == 상태구분.정상) this.도구.SvgImage = this.정상;
                else if (상태 == 상태구분.오류) this.도구.SvgImage = this.오류;
                else if (상태 == 상태구분.대기) this.도구.SvgImage = this.대기;
            }
        }
    }
}
