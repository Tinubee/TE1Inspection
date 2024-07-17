using DevExpress.XtraEditors;
using MvUtils;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TE1.Schemas;

namespace TE1.UI.Controls
{
    public partial class QrMarker : XtraUserControl
    {
        public QrMarker() => InitializeComponent();

        public void Init()
        {
            this.e각인날짜.DateTime = DateTime.Today;
            EnumToList 각인모델 = new EnumToList(typeof(모델구분));
            각인모델.SetLookUpEdit(this.e모델변경);
            this.e모델변경.EditValue = Global.환경설정.선택모델;

            this.b오류상태.Click += 오류상태;
            this.b오류제거.Click += 오류제거;
            this.b모델확인.Click += 모델확인;
            this.e모델변경.ButtonClick += 모델변경;
            this.e각인번호.ButtonClick += 인쇄내용;
            this.b인쇄가능.Click += 인쇄가능;
            this.b인쇄시작.Click += 인쇄시작;
            this.b인쇄중단.Click += 인쇄중단;
            this.g통신내역.CustomButtonClick += 내역삭제;
            Global.큐알각인.송신수신알림 += 송신수신알림;
        }

        public void Close() { }

        private void 송신수신알림(큐알장치.통신구분 통신, 큐알각인.제어명령 명령, String mesg)
        {
            if (this.e통신내역.InvokeRequired)
            {
                this.e통신내역.BeginInvoke(new Action(() => 송신수신알림(통신, 명령, mesg)));
                return;
            }
            Debug.WriteLine($"{통신} : {명령} => {mesg}");
            if(통신 == 큐알장치.통신구분.RX && 명령 == 큐알각인.제어명령.인쇄완료 && mesg.Contains("OK"))
            {
                Global.장치통신.큐알각인 = false;
                Global.큐알각인.각인완료확인중 = false;
                return;
            }

            this.e통신내역.Items.Insert(0, $"{통신}: {mesg}");
            this.e통신내역.SelectedIndex = 0;
            while (this.e통신내역.Items.Count > 100)
                this.e통신내역.Items.RemoveAt(this.e통신내역.Items.Count - 1);
        }

        private void 모델변경(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            LookUpEdit edt = sender as LookUpEdit;
            edt.DoValidate();
            if (edt.EditValue == null) return;
            모델구분 모델 = (모델구분)edt.EditValue;
            Global.큐알각인.모델변경(모델);
        }

        private void 인쇄내용(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            (sender as ButtonEdit).DoValidate();
            Global.큐알각인.인쇄내용(검사결과.큐알각인내용(this.e각인날짜.DateTime, (모델구분)this.e모델변경.EditValue, Utils.IntValue((sender as ButtonEdit).Text.Trim())));
        }

        private void 오류상태(object sender, EventArgs e) => Global.큐알각인.오류상태();
        private void 오류제거(object sender, EventArgs e) => Global.큐알각인.오류제거();
        private void 모델확인(object sender, EventArgs e) => Global.큐알각인.모델확인();
        private void 인쇄가능(object sender, EventArgs e) => Global.큐알각인.인쇄가능();
        private void 인쇄시작(object sender, EventArgs e) => Global.큐알각인.인쇄시작();
        private void 인쇄중단(object sender, EventArgs e) => Global.큐알각인.인쇄중단();
        private void 내역삭제(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e) => this.e통신내역.Items.Clear();
    }
}
