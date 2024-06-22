namespace TE1.UI.Controls
{
    partial class QrReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QrReader));
            DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions buttonImageOptions1 = new DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions();
            this.g큐알리더 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.b큐알리딩종료 = new DevExpress.XtraEditors.SimpleButton();
            this.b큐알리딩시작 = new DevExpress.XtraEditors.SimpleButton();
            this.g큐알통신내역 = new DevExpress.XtraEditors.GroupControl();
            this.e큐알통신내역 = new DevExpress.XtraEditors.ListBoxControl();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.g큐알리더)).BeginInit();
            this.g큐알리더.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.g큐알통신내역)).BeginInit();
            this.g큐알통신내역.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e큐알통신내역)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.SuspendLayout();
            // 
            // g큐알리더
            // 
            this.g큐알리더.Controls.Add(this.layoutControl2);
            this.g큐알리더.Dock = System.Windows.Forms.DockStyle.Fill;
            this.g큐알리더.Location = new System.Drawing.Point(0, 0);
            this.g큐알리더.Name = "g큐알리더";
            this.g큐알리더.Size = new System.Drawing.Size(500, 300);
            this.g큐알리더.TabIndex = 2;
            this.g큐알리더.Text = "QR Reader";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.b큐알리딩종료);
            this.layoutControl2.Controls.Add(this.b큐알리딩시작);
            this.layoutControl2.Controls.Add(this.g큐알통신내역);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(2, 27);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.Root = this.layoutControlGroup2;
            this.layoutControl2.Size = new System.Drawing.Size(496, 271);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // b큐알리딩종료
            // 
            this.b큐알리딩종료.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b큐알리딩종료.ImageOptions.SvgImage")));
            this.b큐알리딩종료.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.b큐알리딩종료.Location = new System.Drawing.Point(20, 229);
            this.b큐알리딩종료.Name = "b큐알리딩종료";
            this.b큐알리딩종료.Size = new System.Drawing.Size(82, 22);
            this.b큐알리딩종료.StyleController = this.layoutControl2;
            this.b큐알리딩종료.TabIndex = 7;
            this.b큐알리딩종료.Text = "리딩종료";
            // 
            // b큐알리딩시작
            // 
            this.b큐알리딩시작.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b큐알리딩시작.ImageOptions.SvgImage")));
            this.b큐알리딩시작.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.b큐알리딩시작.Location = new System.Drawing.Point(20, 20);
            this.b큐알리딩시작.Name = "b큐알리딩시작";
            this.b큐알리딩시작.Size = new System.Drawing.Size(82, 22);
            this.b큐알리딩시작.StyleController = this.layoutControl2;
            this.b큐알리딩시작.TabIndex = 6;
            this.b큐알리딩시작.Text = "리딩시작";
            // 
            // g큐알통신내역
            // 
            this.g큐알통신내역.Controls.Add(this.e큐알통신내역);
            buttonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("buttonImageOptions1.SvgImage")));
            buttonImageOptions1.SvgImageSize = new System.Drawing.Size(20, 20);
            this.g큐알통신내역.CustomHeaderButtons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("Clear", true, buttonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Clear contents", -1, true, null, true, false, true, null, -1)});
            this.g큐알통신내역.CustomHeaderButtonsLocation = DevExpress.Utils.GroupElementLocation.AfterText;
            this.g큐알통신내역.Location = new System.Drawing.Point(114, 12);
            this.g큐알통신내역.Name = "g큐알통신내역";
            this.g큐알통신내역.Size = new System.Drawing.Size(370, 247);
            this.g큐알통신내역.TabIndex = 12;
            this.g큐알통신내역.Text = "통신내역";
            // 
            // e큐알통신내역
            // 
            this.e큐알통신내역.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e큐알통신내역.Location = new System.Drawing.Point(2, 27);
            this.e큐알통신내역.Name = "e큐알통신내역";
            this.e큐알통신내역.Size = new System.Drawing.Size(366, 218);
            this.e큐알통신내역.TabIndex = 11;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlItem11});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(496, 271);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.emptySpaceItem2});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup3.Size = new System.Drawing.Size(102, 251);
            this.layoutControlGroup3.Text = "명령전송";
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.b큐알리딩시작;
            this.layoutControlItem13.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(86, 26);
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.b큐알리딩종료;
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 209);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(86, 26);
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextVisible = false;
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(86, 183);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.g큐알통신내역;
            this.layoutControlItem11.Location = new System.Drawing.Point(102, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(374, 251);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // QrReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.g큐알리더);
            this.Name = "QrReader";
            this.Size = new System.Drawing.Size(500, 300);
            ((System.ComponentModel.ISupportInitialize)(this.g큐알리더)).EndInit();
            this.g큐알리더.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.g큐알통신내역)).EndInit();
            this.g큐알통신내역.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e큐알통신내역)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.GroupControl g큐알리더;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.GroupControl g큐알통신내역;
        private DevExpress.XtraEditors.ListBoxControl e큐알통신내역;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SimpleButton b큐알리딩종료;
        private DevExpress.XtraEditors.SimpleButton b큐알리딩시작;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
    }
}
