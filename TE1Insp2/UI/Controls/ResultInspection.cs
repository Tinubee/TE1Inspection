using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using TE1.Schemas;
using System;
using System.Windows.Media.Media3D;
using System.Windows;

namespace TE1.UI.Controls
{
    public partial class ResultInspection : XtraUserControl
    {
        public ResultInspection()
        {
            InitializeComponent();
        }

        public enum ViewTypes { Auto, Manual }
        private ViewTypes RunType = ViewTypes.Manual;
        private UPR3P24S3D Model3D = null;
        public void Init(ViewTypes runType = ViewTypes.Manual)
        {
            RunType = runType;
            Model3D = new UPR3P24S3D()
            {
                CameraPosition = new Point3D(0, 0, 1400),
                CameraLookDirection = new Vector3D(0, 0, -1400),
                CameraUpDirection = new Vector3D(0, 1, 0),
            };

            this.e결과뷰어.Init(Model3D);
            this.e결과목록.Init();

            if (this.RunType == ViewTypes.Auto)
            {
                Global.검사자료.검사완료알림 += 검사완료알림;
                검사완료알림(Global.검사자료.현재검사찾기());
            }
            this.e큐알코드.ButtonClick += (object sender, ButtonPressedEventArgs e) => Clipboard.SetText(this.e큐알코드.Text);
        }

        public void Close() { }

        public void 검사완료알림(검사결과 결과)
        {
            if (결과 == null) return;
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { 검사완료알림(결과); })); return; }
            this.e결과뷰어.SetResults(결과);
            this.e결과목록.SetResults(결과);
            this.e측정결과.Appearance.ForeColor = 환경설정.ResultColor(결과.측정결과);
            this.e치수결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.치수결과);
            this.e외관결과.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.외관결과);
            this.e큐알코드.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과.큐알결과);
            검사정보 정보 = 결과.GetItem(검사항목.QrLegibility);
            if (정보 != null) this.e큐알등급.Properties.Appearance.ForeColor = 환경설정.ResultColor(정보.측정결과);
            else this.e큐알등급.Properties.Appearance.ForeColor = 환경설정.ResultColor(결과구분.WA);
            this.Bind검사결과.DataSource = 결과;
            this.Bind검사결과.ResetBindings(false);
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;
            검사정보 정보 = view.GetRow(e.RowHandle) as 검사정보;
            if (정보 == null) return;
            정보.SetAppearance(e);
        }
    }
}
