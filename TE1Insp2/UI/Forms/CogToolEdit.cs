using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
using TE1.Schemas;
using MvUtils;
using System;
using System.Windows.Forms;

namespace TE1.UI.Forms
{
    public partial class CogToolEdit : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        public CogToolEdit()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1800, 800);
            //this.WindowState = FormWindowState.Maximized;
            this.Shown += CogToolEditShown;
            this.FormClosed += CogToolEditClosed;
            this.b검사수행.ItemClick += 검사수행;
            this.b사진열기.ItemClick += 이미지로드;
            this.b마스터로드.ItemClick += 마스터로드;
            this.b마스터저장.ItemClick += 마스터저장;
            this.b설정저장.ItemClick += 설정저장;
            Global.검사자료.수동검사알림 += 수동검사알림;
            this.e뷰어.Init();
        }

        public String 사진파일 { get; set; } = String.Empty;
        private const String 로그영역 = "Vision Tools";
        private CogToolGroupEditV2 CogControl = null;
        비전도구 검사도구 = null;

        private void CogToolEditShown(object sender, EventArgs e)
        {
            if (검사도구 == null || String.IsNullOrEmpty(사진파일)) return;
            검사도구.이미지로드(사진파일);
            this.e결과목록.RefreshData();
        }
        private void CogToolEditClosed(object sender, FormClosedEventArgs e)
        {
            this.검사도구.RemoveDisplay();
            this.e뷰어.Close();
            Global.검사자료.수동검사알림 -= 수동검사알림;
            this.CogControl?.Dispose();
            this.e뷰어?.Dispose();
        }

        public void Init(비전도구 도구)
        {
            this.검사도구 = 도구;
            this.Text = 도구.도구명칭;
            this.CogControl = new CogToolBlockEditV2() { Subject = this.검사도구.ToolBlock, LocalDisplayVisible = false, Dock = DockStyle.Fill };
            this.splitContainerControl1.Panel1.Controls.Add(this.CogControl);
            this.e결과목록.Init();
            Global.검사자료.수동검사.Reset();//도구.카메라
            this.e결과목록.SetResults(Global.검사자료.수동검사);
            this.검사도구.SetDisplay(this.e뷰어);
            if (도구.OutputImage != null)
                this.e뷰어.ViewResultImage(도구.OutputImage, 도구.ToolBlock.CreateLastRunRecord(), 도구.ViewerRecodName);
        }

        private void 수동검사알림(카메라구분 카메라, 검사결과 결과) => this.e결과목록.RefreshData();
        private void 검사수행(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.검사도구.다시검사();
        private void 이미지로드(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.검사도구.이미지로드();
        private void 마스터로드(object sender, DevExpress.XtraBars.ItemClickEventArgs e) => this.검사도구.마스터로드(b자동교정.Checked);
        private void 마스터저장(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.검사도구.InputImage == null) return;
            if (!Global.Confirm(this.FindForm(), "Save the current image as the master?")) return;
            if (this.검사도구.마스터저장()) Global.정보로그(로그영역, "Save Master image", Localization.저장완료.GetString(), this);
        }
        private void 설정저장(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!Global.Confirm(this.FindForm(), "Save Vision Tools settings?")) return;
            try { this.검사도구.Save(); }
            catch (Exception ex) { Global.오류로그(로그영역, "Save error", $"{ex.Message}", this); }
        }
    }
}