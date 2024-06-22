using DevExpress.XtraEditors;
using TE1.Schemas;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TE1.UI.Controls
{
    public partial class Viewport3D : XtraUserControl
    {
        public Viewport3D()
        {
            InitializeComponent();
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private SaveFileDialog Export;
        private UPR3P24S3D Model3D = null;
        public void Init(UPR3P24S3D model)
        {
            this.Model3D = model;
            if (!Model3D.Init(out String err2)) { Debug.WriteLine(err2, "Model3D Error"); }
            this.Controls.Add(Model3D.CreateHost());
            this.Export = MvUtils.Utils.GetSaveFileDialog("png", "PNG|*.png", Global.모델자료.선택모델.모델구분.ToString());
            this.Export.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            this.b내보내기.Click += 내보내기;
            this.SetResults(new 검사결과().Reset());
        }

        private void 내보내기(object sender, EventArgs e)
        {
            if (Export.ShowDialog() == DialogResult.OK)
                Model3D.Export(Export.FileName);
        }

        public void SetResults(검사결과 결과)
        {
            if (결과 == null) return;
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { SetResults(결과); })); return; }
            this.Model3D.BeginInit();
            this.Model3D.SetResults(결과);
            this.Model3D.EndInit();
            this.Invalidate();
        }

        public void RefreshViewport()
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(RefreshViewport)); }
            else this.Invalidate();
        }
    }
}
