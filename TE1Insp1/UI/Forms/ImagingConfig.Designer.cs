namespace TE1.UI.Forms
{
    partial class ImagingConfig
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagingConfig));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.e도구 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.Bind도구 = new System.Windows.Forms.BindingSource(this.components);
            this.e추가 = new DevExpress.XtraEditors.LookUpEdit();
            this.e설정 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.b적용 = new DevExpress.XtraEditors.SimpleButton();
            this.b수행 = new DevExpress.XtraEditors.SimpleButton();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.DragDropEvent = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.e결과 = new Cogutils.RecordDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).BeginInit();
            this.splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).BeginInit();
            this.splitContainerControl2.Panel2.SuspendLayout();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e도구)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind도구)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e추가.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e설정)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e결과)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.e결과);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1200, 770);
            this.splitContainerControl1.SplitterPosition = 308;
            this.splitContainerControl1.TabIndex = 3;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.e도구);
            this.splitContainerControl2.Panel1.Controls.Add(this.e추가);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.e설정);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(308, 734);
            this.splitContainerControl2.SplitterPosition = 245;
            this.splitContainerControl2.TabIndex = 3;
            // 
            // e도구
            // 
            this.behaviorManager1.SetBehaviors(this.e도구, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.DragDrop.DragDropBehavior.Create(typeof(DevExpress.XtraEditors.DragDropBehaviorSourceForListBox), true, true, true, true, this.DragDropEvent)))});
            this.e도구.CheckMember = "Enabled";
            this.e도구.DataSource = this.Bind도구;
            this.e도구.DisplayMember = "Name";
            this.e도구.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e도구.ItemHeight = 24;
            this.e도구.ItemPadding = new System.Windows.Forms.Padding(2);
            this.e도구.Location = new System.Drawing.Point(0, 22);
            this.e도구.Name = "e도구";
            this.e도구.Size = new System.Drawing.Size(308, 223);
            this.e도구.TabIndex = 2;
            // 
            // Bind도구
            // 
            this.Bind도구.DataSource = typeof(System.Collections.Generic.List<MvLibs.Tools.ICvTool>);
            // 
            // e추가
            // 
            this.e추가.Dock = System.Windows.Forms.DockStyle.Top;
            this.e추가.EnterMoveNextControl = true;
            this.e추가.Location = new System.Drawing.Point(0, 0);
            this.e추가.Name = "e추가";
            this.e추가.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "", -1, true, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.e추가.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.e추가.Properties.DisplayMember = "Name";
            this.e추가.Properties.NullText = "[Add Item]";
            this.e추가.Properties.ShowHeader = false;
            this.e추가.Properties.ValueMember = "Type";
            this.e추가.Size = new System.Drawing.Size(308, 22);
            this.e추가.TabIndex = 3;
            // 
            // e설정
            // 
            this.e설정.Cursor = System.Windows.Forms.Cursors.Default;
            this.e설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e설정.Location = new System.Drawing.Point(0, 0);
            this.e설정.Name = "e설정";
            this.e설정.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.e설정.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.e설정.Size = new System.Drawing.Size(308, 479);
            this.e설정.TabIndex = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.b적용);
            this.panelControl1.Controls.Add(this.b수행);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 734);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(308, 36);
            this.panelControl1.TabIndex = 2;
            // 
            // b적용
            // 
            this.b적용.Dock = System.Windows.Forms.DockStyle.Left;
            this.b적용.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b적용.ImageOptions.SvgImage")));
            this.b적용.Location = new System.Drawing.Point(2, 2);
            this.b적용.Name = "b적용";
            this.b적용.Size = new System.Drawing.Size(100, 32);
            this.b적용.TabIndex = 3;
            this.b적용.Text = "Apply";
            // 
            // b수행
            // 
            this.b수행.Dock = System.Windows.Forms.DockStyle.Right;
            this.b수행.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b수행.ImageOptions.SvgImage")));
            this.b수행.Location = new System.Drawing.Point(206, 2);
            this.b수행.Name = "b수행";
            this.b수행.Size = new System.Drawing.Size(100, 32);
            this.b수행.TabIndex = 0;
            this.b수행.Text = "Run";
            // 
            // e결과
            // 
            this.e결과.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.e결과.ColorMapLowerRoiLimit = 0D;
            this.e결과.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.e결과.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.e결과.ColorMapUpperRoiLimit = 1D;
            this.e결과.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e결과.DoubleTapZoomCycleLength = 2;
            this.e결과.DoubleTapZoomSensitivity = 2.5D;
            this.e결과.Location = new System.Drawing.Point(0, 0);
            this.e결과.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.e결과.MouseWheelSensitivity = 1D;
            this.e결과.Name = "e결과";
            this.e결과.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("e결과.OcxState")));
            this.e결과.Size = new System.Drawing.Size(882, 770);
            this.e결과.TabIndex = 0;
            // 
            // ImagingConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 770);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "ImagingConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Imaging Config";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel1)).EndInit();
            this.splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2.Panel2)).EndInit();
            this.splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e도구)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind도구)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e추가.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e설정)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e결과)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.CheckedListBoxControl e도구;
        private DevExpress.XtraEditors.LookUpEdit e추가;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton b적용;
        private DevExpress.XtraEditors.SimpleButton b수행;
        private System.Windows.Forms.BindingSource Bind도구;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private DevExpress.Utils.DragDrop.DragDropEvents DragDropEvent;
        private DevExpress.XtraVerticalGrid.PropertyGridControl e설정;
        private Cogutils.RecordDisplay e결과;
    }
}
