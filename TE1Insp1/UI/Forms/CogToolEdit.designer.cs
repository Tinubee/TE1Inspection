﻿namespace TE1.UI.Forms
{
    partial class CogToolEdit
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CogToolEdit));
            this.toolbarFormControl1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormControl();
            this.toolbarFormManager1 = new DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.b설정저장 = new DevExpress.XtraBars.BarButtonItem();
            this.b마스터로드 = new DevExpress.XtraBars.BarButtonItem();
            this.b마스터저장 = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.b사진열기 = new DevExpress.XtraBars.BarButtonItem();
            this.b검사수행 = new DevExpress.XtraBars.BarButtonItem();
            this.b자동교정 = new DevExpress.XtraBars.BarToggleSwitchItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.b사진보정설정 = new DevExpress.XtraBars.BarEditItem();
            this.e사진보정설정 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.b표면검사설정 = new DevExpress.XtraBars.BarButtonItem();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.e결과목록 = new TE1.UI.Controls.ResultGrid();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.e뷰어 = new Cogutils.RecordsDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e사진보정설정)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolbarFormControl1
            // 
            this.toolbarFormControl1.Location = new System.Drawing.Point(0, 0);
            this.toolbarFormControl1.Manager = this.toolbarFormManager1;
            this.toolbarFormControl1.Name = "toolbarFormControl1";
            this.toolbarFormControl1.Size = new System.Drawing.Size(1800, 30);
            this.toolbarFormControl1.TabIndex = 0;
            this.toolbarFormControl1.TabStop = false;
            this.toolbarFormControl1.TitleItemLinks.Add(this.b검사수행, true);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b사진열기);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b마스터로드);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b마스터저장);
            this.toolbarFormControl1.TitleItemLinks.Add(this.barStaticItem1);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b설정저장);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b자동교정);
            this.toolbarFormControl1.TitleItemLinks.Add(this.barStaticItem2);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b사진보정설정);
            this.toolbarFormControl1.TitleItemLinks.Add(this.barStaticItem3);
            this.toolbarFormControl1.TitleItemLinks.Add(this.b표면검사설정);
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
            this.b설정저장,
            this.b마스터로드,
            this.b마스터저장,
            this.barStaticItem1,
            this.b사진열기,
            this.b검사수행,
            this.b자동교정,
            this.barStaticItem2,
            this.b사진보정설정,
            this.barStaticItem3,
            this.b표면검사설정});
            this.toolbarFormManager1.MaxItemId = 13;
            this.toolbarFormManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.e사진보정설정});
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 30);
            this.barDockControlTop.Manager = this.toolbarFormManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1800, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 900);
            this.barDockControlBottom.Manager = this.toolbarFormManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1800, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 30);
            this.barDockControlLeft.Manager = this.toolbarFormManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 870);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1800, 30);
            this.barDockControlRight.Manager = this.toolbarFormManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 870);
            // 
            // b설정저장
            // 
            this.b설정저장.Caption = "Save Tool";
            this.b설정저장.Description = "Save Settings";
            this.b설정저장.Hint = "Save Settings";
            this.b설정저장.Id = 0;
            this.b설정저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b설정저장.ImageOptions.SvgImage")));
            this.b설정저장.Name = "b설정저장";
            this.b설정저장.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // b마스터로드
            // 
            this.b마스터로드.Caption = "Load Master";
            this.b마스터로드.Id = 1;
            this.b마스터로드.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b마스터로드.ImageOptions.SvgImage")));
            this.b마스터로드.Name = "b마스터로드";
            this.b마스터로드.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // b마스터저장
            // 
            this.b마스터저장.Caption = "Save Master";
            this.b마스터저장.Id = 2;
            this.b마스터저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b마스터저장.ImageOptions.SvgImage")));
            this.b마스터저장.Name = "b마스터저장";
            this.b마스터저장.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = " ";
            this.barStaticItem1.Id = 3;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // b사진열기
            // 
            this.b사진열기.Caption = "Load Image";
            this.b사진열기.Id = 4;
            this.b사진열기.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b사진열기.ImageOptions.SvgImage")));
            this.b사진열기.Name = "b사진열기";
            this.b사진열기.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // b검사수행
            // 
            this.b검사수행.Caption = "Run";
            this.b검사수행.Id = 5;
            this.b검사수행.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b검사수행.ImageOptions.SvgImage")));
            this.b검사수행.Name = "b검사수행";
            this.b검사수행.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // b자동교정
            // 
            this.b자동교정.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.b자동교정.Caption = "Calibration";
            this.b자동교정.Id = 8;
            this.b자동교정.Name = "b자동교정";
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Caption = "  ";
            this.barStaticItem2.Id = 9;
            this.barStaticItem2.Name = "barStaticItem2";
            // 
            // b사진보정설정
            // 
            this.b사진보정설정.Caption = "Defects Config";
            this.b사진보정설정.Edit = this.e사진보정설정;
            this.b사진보정설정.Id = 10;
            this.b사진보정설정.Name = "b사진보정설정";
            this.b사진보정설정.Size = new System.Drawing.Size(250, 0);
            // 
            // e사진보정설정
            // 
            this.e사진보정설정.AutoHeight = false;
            editorButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions2.SvgImage")));
            editorButtonImageOptions2.SvgImageSize = new System.Drawing.Size(16, 16);
            this.e사진보정설정.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, true, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.e사진보정설정.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.e사진보정설정.DisplayMember = "Name";
            this.e사진보정설정.Name = "e사진보정설정";
            this.e사진보정설정.NullText = "[Imaging Config]";
            this.e사진보정설정.ShowHeader = false;
            this.e사진보정설정.ValueMember = "Name";
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Caption = "  ";
            this.barStaticItem3.Id = 11;
            this.barStaticItem3.Name = "barStaticItem3";
            // 
            // b표면검사설정
            // 
            this.b표면검사설정.Caption = "Defects Config";
            this.b표면검사설정.Id = 12;
            this.b표면검사설정.Name = "b표면검사설정";
            this.b표면검사설정.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel1.ID = new System.Guid("a93e4e87-e395-4030-813c-40220c328405");
            this.dockPanel1.Location = new System.Drawing.Point(1202, 30);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(598, 200);
            this.dockPanel1.Size = new System.Drawing.Size(598, 870);
            this.dockPanel1.Text = "Results";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.e결과목록);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 30);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(591, 837);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // e결과목록
            // 
            this.e결과목록.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e결과목록.Location = new System.Drawing.Point(0, 0);
            this.e결과목록.Name = "e결과목록";
            this.e결과목록.Size = new System.Drawing.Size(591, 837);
            this.e결과목록.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 30);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.e뷰어);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1202, 870);
            this.splitContainerControl1.SplitterPosition = 400;
            this.splitContainerControl1.TabIndex = 5;
            // 
            // e뷰어
            // 
            this.e뷰어.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e뷰어.Image = null;
            this.e뷰어.Location = new System.Drawing.Point(0, 0);
            this.e뷰어.Name = "e뷰어";
            this.e뷰어.Size = new System.Drawing.Size(792, 870);
            this.e뷰어.TabIndex = 2;
            // 
            // CogToolEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1800, 900);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.dockPanel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.toolbarFormControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("CogToolEdit.IconOptions.SvgImage")));
            this.LookAndFeel.SkinName = "Office 2013";
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CogToolEdit";
            this.Text = "검사항목 설정";
            this.ToolbarFormControl = this.toolbarFormControl1;
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e사진보정설정)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
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
        private DevExpress.XtraBars.BarButtonItem b설정저장;
        private DevExpress.XtraBars.BarButtonItem b마스터로드;
        private DevExpress.XtraBars.BarButtonItem b마스터저장;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem b사진열기;
        private DevExpress.XtraBars.BarButtonItem b검사수행;
        private DevExpress.XtraBars.BarToggleSwitchItem b자동교정;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private Controls.ResultGrid e결과목록;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private Cogutils.RecordsDisplay e뷰어;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarEditItem b사진보정설정;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit e사진보정설정;
        private DevExpress.XtraBars.BarStaticItem barStaticItem3;
        private DevExpress.XtraBars.BarButtonItem b표면검사설정;
    }
}