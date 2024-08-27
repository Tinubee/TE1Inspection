using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TE1.UI.Controls
{
    public partial class TitleView : DevExpress.XtraEditors.XtraUserControl
    {
        public TitleView()
        {
            InitializeComponent();
         }

        public void Init()
        {
            this.lb모드.Click += Lb모드_Click;
            Global.환경설정.VIP모드변경알림 += VIP모드변경알림;
        }

        private void Lb모드_Click(object sender, EventArgs e)
        {
            Global.환경설정.VIP모드변경요청();
        }

        private void VIP모드변경알림()
        {
            Debug.WriteLine($"{Global.환경설정.VIP모드}");
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => VIP모드변경알림()));
                return;
            }

            Global.피씨통신.VIP모드상태전송(Global.환경설정.VIP모드);
            this.lb모드.ForeColor = Global.환경설정.VIP모드 ? Color.FromArgb(189, 160, 27) : Color.FromArgb(56, 126, 196);
        }
    }
}
