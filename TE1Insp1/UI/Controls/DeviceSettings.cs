using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using TE1.Schemas;
using System;
using static TE1.UI.Controls.Config;

namespace TE1.UI.Controls
{
    public partial class DeviceSettings : XtraUserControl
    {
        public DeviceSettings()
        {
            InitializeComponent();
        }

        private LocalizationConfig 번역 = new LocalizationConfig();

        public void Init()
        {
            this.Bind환경설정.DataSource = Global.환경설정;
            this.b캠트리거.Click += 캠트리거리셋;
            this.b개별저장.Toggled += 개별저장;
            this.e카메라.Init();
            this.e기본설정.Init();
            this.e사진저장.Init();
            this.e유저관리.Init();
        }

        private void 개별저장(object sender, EventArgs e) => Global.환경설정.Cam0102개별이미지저장 = b개별저장.IsOn;

        public void Close()
        {
            this.e카메라.Close();
            this.e기본설정.Close();
            this.e사진저장.Close();
            this.e유저관리.Close();
        }
        private void 캠트리거리셋(object sender, EventArgs e)
        {
            if (!Global.Confirm(this.FindForm(), "Want to reset the location information for the trigger board?")) return;
            통신포트[] 포트들 = new 통신포트[] { 통신포트.COM3 };
            포트들.ForEach(port => {
                Enc852 트리거보드 = new Enc852(port);
                트리거보드.Clear();
                트리거보드.Close();
            });
            Global.정보로그("Trigger board", "Reset", "Reset completed.", true);
        }
    }
}
