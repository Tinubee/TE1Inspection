using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.ToolBlock;
using TE1.Schemas;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TE1.UI.Forms
{
    public partial class CogDefectsEdit : DevExpress.XtraBars.ToolbarForm.ToolbarForm
    {
        public CogDefectsEdit()
        {
            InitializeComponent();
        }

        private 비전도구 비전도구 = null;
        private CogMaskCreatorTool Tool = null;
        public void Init(비전도구 도구, CogMaskCreatorTool tool)
        {
            비전도구 = 도구;
            Tool = tool;
            this.e마스킹.Subject = Tool;
            SetControl(this.e마스킹);
            Bind설정.DataSource = new DefectsConfig(비전도구);
            b저장.ItemClick += 설정저장;
            e영역최소.EditValueChanged += (object sender, EventArgs e) => Bind설정.EndEdit();
            e영역최대.EditValueChanged += (object sender, EventArgs e) => Bind설정.EndEdit();
        }

        private void SetControl(Control control, Int32 depth = 0)
        {
            if (control == null) return;
            //String indent = String.Empty.PadLeft(depth * 2, ' ');
            //Debug.WriteLine($"{indent}{control.Name}: {control.GetType().ToString()}, {control.HasChildren}");

            control.ForeColor = Color.Black;
            if (control.Name == "grpResults" && control.GetType() == typeof(GroupBox))
                control.Dock = DockStyle.Top;
            else if (control.Name == "cogGridView1" && control.GetType() == typeof(CogGridView))
            {
                CogGridView view = control as CogGridView;
                foreach (DataGridViewColumn col in view.Columns)
                    col.DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (control.Name == "tbrButtons" && control.GetType() == typeof(ToolBar))
                control.Visible = false;
            else if (control.Name == "tpgGraphics" && control.GetType() == typeof(TabPage))
                control.Enabled = false;
            else if (control.Name == "cboInsidePixelValue")
                control.Enabled = false;
            else if (control.Name == "cboBackgroundPixelValue")
                control.Enabled = false;
            //else if (control.Name == "btnFitInImage")
            //    control.Enabled = false;
            //else if (control.Name == "ctrlSelectedSpaceNameEdit")
            //    control.Enabled = false;

            if (!control.HasChildren) return;
            foreach (Control child in control.Controls)
                SetControl(child, ++depth);
        }

        private void 설정저장(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Bind설정.EndEdit();
            if (!Global.Confirm(this.FindForm(), "Save Vision Tools settings?")) return;
            try { this.비전도구.Save(); }
            catch (Exception ex) { Global.오류로그(CogToolEdit.로그영역, "Save error", $"{ex.Message}", this); }
        }

        private class DefectsConfig
        {
            private 비전도구 비전도구 = null;
            private CogToolBlock DefectsTools => 비전도구.GetTool(BaseTool.DefectsTools) as CogToolBlock;
            public DefectsConfig(비전도구 도구) { 비전도구 = 도구; }

            public Int32 MinArea { get => 비전검사.Input<Int32>(DefectsTools, "MinArea"); set => 비전검사.Input(DefectsTools, "MinArea", value); }
            public Int32 MaxArea { get => 비전검사.Input<Int32>(DefectsTools, "MaxArea"); set => 비전검사.Input(DefectsTools, "MaxArea", value); }
        }
    }
}