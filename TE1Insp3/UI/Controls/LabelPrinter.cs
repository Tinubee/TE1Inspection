using DevExpress.XtraEditors;
using MvUtils;
using System;
using System.Diagnostics;
using TE1.Schemas;

namespace TE1.UI.Controls
{
    public partial class LabelPrinter : XtraUserControl
    {
        public LabelPrinter() => InitializeComponent();

        public void Init()
        {
            EnumToList 라벨모델 = new EnumToList(typeof(모델구분));
            라벨모델.SetLookUpEdit(this.e라벨모델);
            this.e라벨모델.EditValue = Global.환경설정.선택모델;
            this.e라벨날짜.DateTime = DateTime.Today;
            this.e라벨번호.EditValue = 9999;
            this.b라벨출력.Click += 라벨출력;
            this.b라벨부착.Click += 라벨부착;
            this.b장치리셋.Click += 장치리셋;
            this.g통신내역.CustomButtonClick += 내역삭제;
            Global.라벨인쇄.송신수신알림 += 송신수신알림;
        }

        public void Close() { }

        private void 송신수신알림(큐알장치.통신구분 통신, 라벨인쇄.제어명령 명령, String mesg)
        {
            if (this.e통신내역.InvokeRequired)
            {
                this.e통신내역.BeginInvoke(new Action(() => 송신수신알림(통신, 명령, mesg)));
                return;
            }

            Debug.WriteLine($"라벨송신수신알림 => {mesg}");

            this.e통신내역.Items.Insert(0, $"{통신.ToString()}: {mesg}");
            this.e통신내역.SelectedIndex = 0;
            while (this.e통신내역.Items.Count > 100)
                this.e통신내역.Items.RemoveAt(this.e통신내역.Items.Count - 1);
        }

        private void 라벨출력(object sender, EventArgs e) 
        {
            검사결과 검사 = Global.검사자료.검사항목찾기(107);
            검사.결과계산();
            Global.라벨인쇄.자료전송(검사);
        }
            
        private void 라벨부착(object sender, EventArgs e) => Global.라벨인쇄.라벨부착();
        private void 장치리셋(object sender, EventArgs e) => Global.라벨인쇄.장치리셋();
        private void 내역삭제(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e) => this.e통신내역.Items.Clear();
    }
}
