using DevExpress.XtraEditors;
using MvUtils;
using System;
using TE1.Schemas;

namespace TE1.UI.Controls
{
    public partial class QrReader : XtraUserControl
    {
        public QrReader()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.b큐알리딩시작.Click += 큐알리딩시작;
            //this.b큐알리딩종료.Click += 큐알리딩종료;
            this.g큐알통신내역.CustomButtonClick += 큐알통신내역지움;
            Global.큐알리더.송신수신알림 += 큐알송신수신알림;
        }

        public void Close() { }

        private void 큐알송신수신알림(큐알장치.통신구분 통신, 리더명령 명령, String mesg)
        {
            if (this.e큐알통신내역.InvokeRequired)
            {
                this.e큐알통신내역.BeginInvoke(new Action(() => 큐알송신수신알림(통신, 명령, mesg)));
                return;
            }
            this.e큐알통신내역.Items.Insert(0, $"{Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss}")} {통신.ToString()}: {mesg}");
            this.e큐알통신내역.SelectedIndex = 0;
            while (this.e큐알통신내역.Items.Count > 100)
                this.e큐알통신내역.Items.RemoveAt(this.e큐알통신내역.Items.Count - 1);
        }

        private void 큐알리딩시작(object sender, EventArgs e)
        {
            Global.큐알리더.리딩시작(Global.검사자료.수동검사);
            String code = Global.검사자료.수동검사.큐알내용;
        }
        //private void 큐알리딩종료(object sender, EventArgs e) => Global.큐알리더.리딩종료();
        private void 큐알통신내역지움(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e) => this.e큐알통신내역.Items.Clear();
    }
}
