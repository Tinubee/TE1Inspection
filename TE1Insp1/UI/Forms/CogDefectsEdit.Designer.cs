namespace TE1.UI.Forms
{
    partial class CogDefectsEdit
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CogDefectsEdit));
            this.toolbarFormControl1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormControl();
            this.toolbarFormManager1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.b영역최소 = new DevExpress.XtraBars.BarEditItem();
            this.Bind설정 = new System.Windows.Forms.BindingSource(this.components);
            this.e영역최소 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.b영역최대 = new DevExpress.XtraBars.BarEditItem();
            this.e영역최대 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.b저장 = new DevExpress.XtraBars.BarButtonItem();
            this.e마스킹 = new Cognex.VisionPro.ImageProcessing.CogMaskCreatorEditV2();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind설정)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e영역최소)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e영역최대)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e마스킹)).BeginInit();
            this.SuspendLayout();
            // 
            // toolbarFormControl1
            // 
            this.toolbarFormControl1.Location = new System.Drawing.Point(0, 0);
            this.toolbarFormControl1.Manager = this.toolbarFormManager1;
            this.toolbarFormControl1.Name = "toolbarFormControl1";
            this.toolbarFormControl1.Size = new System.Drawing.Size(1096, 30);
            this.toolbarFormControl1.TabIndex = 1;
            this.toolbarFormControl1.TabStop = false;
            this.toolbarFormControl1.TitleItemLinks.Add(this.barStaticItem1);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b영역최소);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b영역최대);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b저장);
            this.toolbarFormControl1.ToolbarForm = this;
            // 
            // toolbarFormManager1
            // 
            this.toolbarFormManager1.DockControls.Add(this.barDockControlTop);
            this.toolbarFormManager1.DockControls.Add(this.barDockControlBottom);
            this.toolbarFormManager1.DockControls.Add(this.barDockControlLeft);
            this.toolbarFormManager1.DockControls.Add(this.barDockControlRight);
            this.toolbarFormManager1.Form = this;
            this.toolbarFormManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1,
            this.b영역최소,
            this.b영역최대,
            this.b저장});
            this.toolbarFormManager1.MaxItemId = 4;
            this.toolbarFormManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.e영역최소,
            this.e영역최대});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 30);
            this.barDockControlTop.Manager = this.toolbarFormManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1096, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 627);
            this.barDockControlBottom.Manager = this.toolbarFormManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1096, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.toolbarFormManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 597);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1096, 30);
            this.barDockControlRight.Manager = this.toolbarFormManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 597);
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "  ";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // b영역최소
            // 
            this.b영역최소.Caption = "Min Area";
            this.b영역최소.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.Bind설정, "MinArea", true));
            this.b영역최소.Edit = this.e영역최소;
            this.b영역최소.Id = 1;
            this.b영역최소.Name = "b영역최소";
            this.b영역최소.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.b영역최소.Size = new System.Drawing.Size(200, 0);
            // 
            // Bind설정
            // 
            this.Bind설정.DataSource = typeof(TE1.UI.Forms.CogDefectsEdit.DefectsConfig);
            // 
            // e영역최소
            // 
            this.e영역최소.AutoHeight = false;
            this.e영역최소.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e영역최소.IsFloatValue = false;
            this.e영역최소.MaskSettings.Set("mask", "N00");
            this.e영역최소.MaxValue = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.e영역최소.MinValue = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.e영역최소.Name = "e영역최소";
            // 
            // b영역최대
            // 
            this.b영역최대.Caption = "Max Area";
            this.b영역최대.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.Bind설정, "MaxArea", true));
            this.b영역최대.Edit = this.e영역최대;
            this.b영역최대.Id = 2;
            this.b영역최대.Name = "b영역최대";
            this.b영역최대.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.b영역최대.Size = new System.Drawing.Size(200, 0);
            // 
            // e영역최대
            // 
            this.e영역최대.AutoHeight = false;
            this.e영역최대.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e영역최대.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.e영역최대.IsFloatValue = false;
            this.e영역최대.MaskSettings.Set("mask", "N00");
            this.e영역최대.MaxValue = new decimal(new int[] {
            900000000,
            0,
            0,
            0});
            this.e영역최대.MinValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.e영역최대.Name = "e영역최대";
            // 
            // b저장
            // 
            this.b저장.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.b저장.Caption = "Save";
            this.b저장.Id = 3;
            this.b저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b저장.ImageOptions.SvgImage")));
            this.b저장.Name = "b저장";
            this.b저장.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // e마스킹
            // 
            this.e마스킹.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e마스킹.Location = new System.Drawing.Point(0, 30);
            this.e마스킹.MinimumSize = new System.Drawing.Size(489, 0);
            this.e마스킹.Name = "e마스킹";
            this.e마스킹.Size = new System.Drawing.Size(1096, 597);
            this.e마스킹.SuspendElectricRuns = false;
            this.e마스킹.TabIndex = 9;
            // 
            // CogDefectsEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1096, 627);
            this.Controls.Add(this.e마스킹);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.toolbarFormControl1);
            this.IconOptions.SvgImage = global::TE1.Properties.Resources.vision;
            this.Name = "CogDefectsEdit";
            this.Text = "Defects Config";
            this.ToolbarFormControl = this.toolbarFormControl1;
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind설정)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e영역최소)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e영역최대)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e마스킹)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormControl toolbarFormControl1;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormManager toolbarFormManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private Cognex.VisionPro.ImageProcessing.CogMaskCreatorEditV2 e마스킹;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private System.Windows.Forms.BindingSource Bind설정;
        private DevExpress.XtraBars.BarEditItem b영역최소;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit e영역최소;
        private DevExpress.XtraBars.BarEditItem b영역최대;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit e영역최대;
        private DevExpress.XtraBars.BarButtonItem b저장;
    }
}