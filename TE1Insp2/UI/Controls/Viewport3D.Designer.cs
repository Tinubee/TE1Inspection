namespace TE1.UI.Controls
{
    partial class Viewport3D
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewport3D));
            this.b내보내기 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // b내보내기
            // 
            this.b내보내기.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b내보내기.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.b내보내기.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b내보내기.ImageOptions.SvgImage")));
            this.b내보내기.Location = new System.Drawing.Point(3, 536);
            this.b내보내기.Name = "b내보내기";
            this.b내보내기.Size = new System.Drawing.Size(36, 36);
            this.b내보내기.TabIndex = 1;
            this.b내보내기.ToolTip = "Export to Image";
            // 
            // Viewport3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.b내보내기);
            this.Name = "Viewport3D";
            this.Size = new System.Drawing.Size(783, 575);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton b내보내기;
    }
}
