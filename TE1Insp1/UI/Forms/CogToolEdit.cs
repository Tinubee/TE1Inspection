using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
using TE1.Schemas;
using MvUtils;
using System;
using System.Windows.Forms;
using Cognex.VisionPro.Implementation;
using System.Linq;
using System.Diagnostics;
using Cognex.VisionPro;
using Newtonsoft.Json;
using System.Collections.Generic;

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
            this.b표면검사설정.ItemClick += 표면검사설정;
            this.e사진보정설정.ButtonClick += 사진보정설정;
            Global.검사자료.수동검사알림 += 수동검사알림;
            this.e뷰어.Init();
        }

        public String 사진파일 { get; set; } = String.Empty;
        public const String 로그영역 = "Vision Tools";
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
            SetControl(this.CogControl);
            this.e결과목록.Init();
            Global.검사자료.수동검사.Reset();//도구.카메라
            this.e결과목록.SetResults(Global.검사자료.수동검사);
            this.검사도구.SetDisplay(this.e뷰어);
            if (도구.OutputImage != null)
                this.e뷰어.ViewResultImage(도구.OutputImage, 도구.ToolBlock.CreateLastRunRecord(), 도구.ViewerRecodName);

            LoadDefectsConfig();
        }
        private void SetControl(Control control, Int32 depth = 0)
        {
            if (control == null) return;
            String indent = String.Empty.PadLeft(depth * 2, ' ');
            Debug.WriteLine($"{indent}{control.Name}: {control.GetType().ToString()}, {control.HasChildren}");

            control.ForeColor = System.Drawing.Color.Black;
            if (control.Name == "grpResults" && control.GetType() == typeof(GroupBox))
                control.Dock = DockStyle.Top;
            else if (control.Name == "cogGridView1" && control.GetType() == typeof(CogGridView))
            {
                CogGridView view = control as CogGridView;
                foreach (DataGridViewColumn col in view.Columns)
                    col.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            }
            else if (control.Name == "tbrButtons" && control.GetType() == typeof(ToolBar))
            {
                String[] visibles = { "tbbRun", "tbbLoop", "tbbFloatingResults", "tbbScript", "tbbToolbox", "tbbSep1" };
                ToolBar bar = control as ToolBar;
                foreach (ToolBarButton btn in bar.Buttons)
                {
                    //Debug.WriteLine(btn.Name);
                    btn.Visible = visibles.Contains(btn.Name);
                }
            }

            //else if (control.Name == "Tree") { //&& control.GetType() == typeof(CogUserToolEditV2)
            //    Debug.WriteLine("CogToolTreeView");
            //    //CogUserToolEditV2 tree = control as CogUserToolEditV2;
            //    control.MouseDoubleClick += (object sender, MouseEventArgs e) => {
            //        Debug.WriteLine("MouseDoubleClick");
            //    };
            //    //tree.Click += (object sender, EventArgs e) => { Debug.WriteLine("Click"); };
            //    //tree.MouseClick += (object sender, MouseEventArgs e) => { Debug.WriteLine("MouseClick"); };
            //    //tree.DoubleClick += (object sender, EventArgs e) => { Debug.WriteLine("DoubleClick"); };
            //    //tree.KeyDown += (object sender, KeyEventArgs e) => { Debug.WriteLine("KeyDown"); };
            //}

            if (!control.HasChildren) return;
            foreach (Control child in control.Controls)
                SetControl(child, ++depth);
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

        private List<MvImageTool> MvImageTools = new List<MvImageTool>();
        private void LoadDefectsConfig()
        {
            if (this.검사도구 == null) return;
            String json = this.검사도구.Input<String>(TE1.BaseTool.MvImageTools);
            if (String.IsNullOrEmpty(json)) return;
            try
            {
                String[] settings = JsonConvert.DeserializeObject<String[]>(json);
                foreach (String name in settings)
                {
                    MvImageTool tool = new MvImageTool(this.검사도구.ToolBlock, name);
                    if (tool.Tool == null) continue;
                    MvImageTools.Add(tool);
                }
                this.e사진보정설정.DataSource = MvImageTools;
            }
            catch (Exception ex) { Global.오류로그(로그영역, "Load Imaging Config", ex.Message, true); }
        }

        private class MvImageTool
        {
            public String Name { get; set; } = String.Empty;
            public CogToolBlock Tool = null;
            public MvImageTool() { }
            public MvImageTool(CogToolBlock baseBlock, String name)
            {
                Name = name;
                Tool = GetTool(baseBlock);
            }

            public CogToolBlock GetTool(CogToolBlock baseBlock) => GetTool(baseBlock, Name);
            public static CogToolBlock GetTool(CogToolBlock baseBlock, String name)
            {
                if (baseBlock == null || String.IsNullOrEmpty(name)) return null;
                String[] path = name.Split('.');
                CogToolBlock block = baseBlock;
                foreach (String na in path)
                {
                    if (!block.Tools.Contains(na)) return null;
                    ICogTool tool = block.Tools[na];
                    if (tool.GetType() != typeof(CogToolBlock)) return null;
                    block = tool as CogToolBlock;
                }
                return block;
            }
        }
        private void 사진보정설정(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index != 1) return;
            String name = Convert.ToString(b사진보정설정.EditValue);
            if (String.IsNullOrEmpty(name)) return;
            MvImageTool tool = MvImageTools.Where(t => t.Name == name).FirstOrDefault();
            if (tool == null || tool.Tool == null)
            {
                Global.Notify("There are no vision tools.", name, AlertControl.AlertTypes.Warning);
                return;
            }
            ImagingConfig form = new ImagingConfig() { Text = $"Imaging Settings [{name}]" };
            form.Init(tool.Tool);
            form.Show(this);
        }

        private void 표면검사설정(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            비전검사.표면설정(검사도구);
        }

    }
}