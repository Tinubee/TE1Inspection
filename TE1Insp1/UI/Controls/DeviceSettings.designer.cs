
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
            this.e카메라 = new TE1.UI.Controls.CamSettings();
            this.e사진저장 = new TE1.UI.Controls.ImageSave();
            this.e유저관리 = new TE1.UI.Controls.Users();
            this.e기본설정 = new TE1.UI.Controls.Config();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.b캠트리거 = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Bind환경설정 = new System.Windows.Forms.BindingSource(this.components);
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.b개별저장 = new DevExpress.XtraEditors.ToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind환경설정)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b개별저장.Properties)).BeginInit();
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
            this.splitContainerControl1.Panel1.Controls.Add(this.e카메라);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.e사진저장);
            this.splitContainerControl1.Panel2.Controls.Add(this.e유저관리);
            this.splitContainerControl1.Panel2.Controls.Add(this.e기본설정);
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1920, 1040);
            this.splitContainerControl1.SplitterPosition = 1292;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // e카메라
            // 
            this.e카메라.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e카메라.Location = new System.Drawing.Point(0, 0);
            this.e카메라.Name = "e카메라";
            this.e카메라.Size = new System.Drawing.Size(1292, 1040);
            this.e카메라.TabIndex = 0;
            // 
            // e사진저장
            // 
            this.e사진저장.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e사진저장.Location = new System.Drawing.Point(0, 383);
            this.e사진저장.Name = "e사진저장";
            this.e사진저장.Size = new System.Drawing.Size(618, 322);
            this.e사진저장.TabIndex = 1;
            // 
            // e유저관리
            // 
            this.e유저관리.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.e유저관리.Location = new System.Drawing.Point(0, 705);
            this.e유저관리.Name = "e유저관리";
            this.e유저관리.Size = new System.Drawing.Size(618, 335);
            this.e유저관리.TabIndex = 0;
            // 
            // e기본설정
            // 
            this.e기본설정.Dock = System.Windows.Forms.DockStyle.Top;
            this.e기본설정.Location = new System.Drawing.Point(0, 85);
            this.e기본설정.Name = "e기본설정";
            this.e기본설정.Size = new System.Drawing.Size(618, 298);
            this.e기본설정.TabIndex = 0;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(618, 85);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Tools";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.b캠트리거);
            this.layoutControl1.Controls.Add(this.b개별저장);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 27);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(614, 56);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // b캠트리거
            // 
            this.b캠트리거.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b캠트리거.Appearance.Options.UseFont = true;
            this.b캠트리거.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b캠트리거.ImageOptions.SvgImage")));
            this.b캠트리거.Location = new System.Drawing.Point(9, 9);
            this.b캠트리거.Name = "b캠트리거";
            this.b캠트리거.Size = new System.Drawing.Size(376, 36);
            this.b캠트리거.StyleController = this.layoutControl1;
            this.b캠트리거.TabIndex = 4;
            this.b캠트리거.Text = "Camera Trigger Board Position Reset";
            // 
            // Root
            // 
            this.Root.AppearanceItemCaption.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Root.AppearanceItemCaption.Options.UseFont = true;
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(614, 56);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.b캠트리거;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem3.Size = new System.Drawing.Size(384, 46);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // Bind환경설정
            // 
            this.Bind환경설정.DataSource = typeof(TE1.Schemas.환경설정);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.b개별저장;
            this.layoutControlItem1.Location = new System.Drawing.Point(384, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(220, 46);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // b개별저장
            // 
            this.b개별저장.EditValue = null;
            this.b개별저장.Location = new System.Drawing.Point(391, 7);
            this.b개별저장.Name = "b개별저장";
            this.b개별저장.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.b개별저장.Properties.OffText = "Off";
            this.b개별저장.Properties.OnText = "On";
            this.b개별저장.Size = new System.Drawing.Size(216, 22);
            this.b개별저장.StyleController = this.layoutControl1;
            this.b개별저장.TabIndex = 5;
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind환경설정)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b개별저장.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private CamSettings e카메라;
        private Users e유저관리;
        private Config e기본설정;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.SimpleButton b캠트리거;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.BindingSource Bind환경설정;
        private ImageSave e사진저장;
        private DevExpress.XtraEditors.ToggleSwitch b개별저장;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
    }
}
