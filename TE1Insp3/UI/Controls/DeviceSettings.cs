using DevExpress.XtraEditors;
using System;
using TE1.Schemas;
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
            this.e강제배출.IsOn = Global.환경설정.강제배출;
            this.e배출구분.IsOn = Global.환경설정.양품불량;
            this.e표면검사.IsOn = Global.환경설정.표면검사;
            this.e강제배출.EditValueChanged += 강제배출Changed;
            this.e배출구분.EditValueChanged += 배출구분Changed;
            this.e표면검사.EditValueChanged += 표면검사Changed;
            this.b설정저장.Click += 설정저장;

            this.e큐알각인.Init();
            this.e큐알리더.Init();
            this.e라벨인쇄.Init();
            this.e기본설정.Init();
            this.e유저관리.Init();
        }

        public void Close()
        {
            this.e큐알각인.Close();
            this.e큐알리더.Close();
            this.e라벨인쇄.Close();
            this.e기본설정.Close();
            this.e유저관리.Close();
        }

        private void 강제배출Changed(object sender, EventArgs e) => Global.환경설정.강제배출 = this.e강제배출.IsOn;
        private void 배출구분Changed(object sender, EventArgs e) => Global.환경설정.양품불량 = this.e배출구분.IsOn;
        private void 표면검사Changed(object sender, EventArgs e) => Global.환경설정.표면검사 = this.e표면검사.IsOn;

        private void 설정저장(object sender, EventArgs e)
        {
            this.Bind환경설정.EndEdit();
            if (!Global.Confirm(this.FindForm(), 번역.저장확인)) return;
            Global.환경설정.Save();
            Global.정보로그(환경설정.로그영역.GetString(), 번역.설정저장, 번역.저장완료, true);
        }
    }
}
