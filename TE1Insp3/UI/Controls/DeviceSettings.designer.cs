
namespace TE1.UI.Controls
{
    partial class DeviceSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceSettings));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.b설정저장 = new DevExpress.XtraEditors.SimpleButton();
            this.e표면검사 = new DevExpress.XtraEditors.ToggleSwitch();
            this.e배출구분 = new DevExpress.XtraEditors.ToggleSwitch();
            this.e강제배출 = new DevExpress.XtraEditors.ToggleSwitch();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.e큐알리더 = new TE1.UI.Controls.QrReader();
            this.e큐알각인 = new TE1.UI.Controls.QrMarker();
            this.e라벨인쇄 = new TE1.UI.Controls.LabelPrinter();
            this.Bind환경설정 = new System.Windows.Forms.BindingSource(this.components);
            this.e유저관리 = new TE1.UI.Controls.Users();
            this.e기본설정 = new TE1.UI.Controls.Config();
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e표면검사.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e배출구분.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e강제배출.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind환경설정)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.e유저관리);
            this.splitContainerControl1.Panel2.Controls.Add(this.e기본설정);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1920, 1040);
            this.splitContainerControl1.SplitterPosition = 1333;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            this.splitContainerControl2.Panel1.Controls.Add(this.e큐알리더);
            this.splitContainerControl2.Panel1.Controls.Add(this.splitterControl1);
            this.splitContainerControl2.Panel1.Controls.Add(this.e큐알각인);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            this.splitContainerControl2.Panel2.Controls.Add(this.e라벨인쇄);
            this.splitContainerControl2.Panel2.Controls.Add(this.splitterControl2);
            this.splitContainerControl2.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(1333, 1040);
            this.splitContainerControl2.SplitterPosition = 667;
            this.splitContainerControl2.TabIndex = 2;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 446);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(667, 10);
            this.splitterControl1.TabIndex = 2;
            this.splitterControl1.TabStop = false;
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl2.Location = new System.Drawing.Point(0, 446);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(656, 10);
            this.splitterControl2.TabIndex = 5;
            this.splitterControl2.TabStop = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(656, 446);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Tools";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.b설정저장);
            this.layoutControl1.Controls.Add(this.e표면검사);
            this.layoutControl1.Controls.Add(this.e배출구분);
            this.layoutControl1.Controls.Add(this.e강제배출);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 27);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(652, 417);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // b설정저장
            // 
            this.b설정저장.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b설정저장.Appearance.Options.UseFont = true;
            this.b설정저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b설정저장.ImageOptions.SvgImage")));
            this.b설정저장.Location = new System.Drawing.Point(336, 158);
            this.b설정저장.Name = "b설정저장";
            this.b설정저장.Size = new System.Drawing.Size(296, 36);
            this.b설정저장.StyleController = this.layoutControl1;
            this.b설정저장.TabIndex = 12;
            this.b설정저장.Text = "Save";
            // 
            // e표면검사
            // 
            this.e표면검사.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.Bind환경설정, "표면검사", true));
            this.e표면검사.Location = new System.Drawing.Point(110, 154);
            this.e표면검사.Name = "e표면검사";
            this.e표면검사.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.e표면검사.Properties.Appearance.Options.UseFont = true;
            this.e표면검사.Properties.OffText = "Off";
            this.e표면검사.Properties.OnText = "On";
            this.e표면검사.Size = new System.Drawing.Size(194, 28);
            this.e표면검사.StyleController = this.layoutControl1;
            this.e표면검사.TabIndex = 7;
            // 
            // e배출구분
            // 
            this.e배출구분.EnterMoveNextControl = true;
            this.e배출구분.Location = new System.Drawing.Point(413, 57);
            this.e배출구분.Name = "e배출구분";
            this.e배출구분.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.e배출구분.Properties.Appearance.Options.UseFont = true;
            this.e배출구분.Properties.OffText = "NG";
            this.e배출구분.Properties.OnText = "OK";
            this.e배출구분.Size = new System.Drawing.Size(207, 28);
            this.e배출구분.StyleController = this.layoutControl1;
            this.e배출구분.TabIndex = 5;
            // 
            // e강제배출
            // 
            this.e강제배출.EnterMoveNextControl = true;
            this.e강제배출.Location = new System.Drawing.Point(110, 57);
            this.e강제배출.Name = "e강제배출";
            this.e강제배출.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.e강제배출.Properties.Appearance.Options.UseFont = true;
            this.e강제배출.Properties.OffText = "Off";
            this.e강제배출.Properties.OnText = "On";
            this.e강제배출.Size = new System.Drawing.Size(205, 28);
            this.e강제배출.StyleController = this.layoutControl1;
            this.e강제배출.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup1,
            this.layoutControlGroup4,
            this.layoutControlItem8,
            this.emptySpaceItem1,
            this.emptySpaceItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(652, 417);
            this.Root.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(632, 97);
            this.layoutControlGroup1.Text = "Forced Ejection";
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.e강제배출;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.layoutControlItem1.Size = new System.Drawing.Size(303, 48);
            this.layoutControlItem1.Text = "On/Off";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(66, 25);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.e배출구분;
            this.layoutControlItem2.Location = new System.Drawing.Point(303, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.layoutControlItem2.Size = new System.Drawing.Size(305, 48);
            this.layoutControlItem2.Text = "NG/OK";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(66, 25);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup4.Location = new System.Drawing.Point(0, 97);
            this.layoutControlGroup4.Name = "layoutControlGroup4";
            this.layoutControlGroup4.Size = new System.Drawing.Size(316, 97);
            this.layoutControlGroup4.Text = "Enable Surface Inspection";
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.e표면검사;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.layoutControlItem5.Size = new System.Drawing.Size(292, 48);
            this.layoutControlItem5.Text = "Surface";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(66, 25);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.b설정저장;
            this.layoutControlItem8.Location = new System.Drawing.Point(316, 138);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.layoutControlItem8.Size = new System.Drawing.Size(316, 56);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(316, 97);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(316, 41);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 194);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(632, 203);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // e큐알리더
            // 
            this.e큐알리더.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e큐알리더.Location = new System.Drawing.Point(0, 456);
            this.e큐알리더.Name = "e큐알리더";
            this.e큐알리더.Size = new System.Drawing.Size(667, 584);
            this.e큐알리더.TabIndex = 1;
            // 
            // e큐알각인
            // 
            this.e큐알각인.Dock = System.Windows.Forms.DockStyle.Top;
            this.e큐알각인.Location = new System.Drawing.Point(0, 0);
            this.e큐알각인.Name = "e큐알각인";
            this.e큐알각인.Size = new System.Drawing.Size(667, 446);
            this.e큐알각인.TabIndex = 3;
            // 
            // e라벨인쇄
            // 
            this.e라벨인쇄.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e라벨인쇄.Location = new System.Drawing.Point(0, 456);
            this.e라벨인쇄.Name = "e라벨인쇄";
            this.e라벨인쇄.Size = new System.Drawing.Size(656, 584);
            this.e라벨인쇄.TabIndex = 4;
            // 
            // Bind환경설정
            // 
            this.Bind환경설정.DataSource = typeof(TE1.Schemas.환경설정);
            // 
            // e유저관리
            // 
            this.e유저관리.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e유저관리.Location = new System.Drawing.Point(0, 512);
            this.e유저관리.Name = "e유저관리";
            this.e유저관리.Size = new System.Drawing.Size(577, 528);
            this.e유저관리.TabIndex = 0;
            // 
            // e기본설정
            // 
            this.e기본설정.Dock = System.Windows.Forms.DockStyle.Top;
            this.e기본설정.Location = new System.Drawing.Point(0, 0);
            this.e기본설정.Name = "e기본설정";
            this.e기본설정.Size = new System.Drawing.Size(577, 512);
            this.e기본설정.TabIndex = 0;
            // 
            // DeviceSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "DeviceSettings";
            this.Size = new System.Drawing.Size(1920, 1040);
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e표면검사.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e배출구분.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e강제배출.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind환경설정)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private QrReader e큐알리더;
        private Users e유저관리;
        private Config e기본설정;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.ToggleSwitch e배출구분;
        private DevExpress.XtraEditors.ToggleSwitch e강제배출;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.ToggleSwitch e표면검사;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private System.Windows.Forms.BindingSource Bind환경설정;
        private DevExpress.XtraEditors.SimpleButton b설정저장;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private QrMarker e큐알각인;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private LabelPrinter e라벨인쇄;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.SplitterControl splitterControl2;
    }
}
