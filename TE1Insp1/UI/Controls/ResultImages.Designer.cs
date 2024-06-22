namespace TE1.UI.Controls
{
    partial class ResultImages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultImages));
            this.e카메라 = new DevExpress.XtraEditors.RadioGroup();
            this.e뷰어 = new Cogutils.RecordDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.e카메라.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e뷰어)).BeginInit();
            this.SuspendLayout();
            // 
            // e카메라
            // 
            this.e카메라.Dock = System.Windows.Forms.DockStyle.Top;
            this.e카메라.Location = new System.Drawing.Point(0, 0);
            this.e카메라.Name = "e카메라";
            this.e카메라.Size = new System.Drawing.Size(953, 38);
            this.e카메라.TabIndex = 0;
            // 
            // e뷰어
            // 
            this.e뷰어.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.e뷰어.ColorMapLowerRoiLimit = 0D;
            this.e뷰어.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.e뷰어.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.e뷰어.ColorMapUpperRoiLimit = 1D;
            this.e뷰어.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e뷰어.DoubleTapZoomCycleLength = 2;
            this.e뷰어.DoubleTapZoomSensitivity = 2.5D;
            this.e뷰어.Location = new System.Drawing.Point(0, 38);
            this.e뷰어.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.e뷰어.MouseWheelSensitivity = 1D;
            this.e뷰어.Name = "e뷰어";
            this.e뷰어.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("e뷰어.OcxState")));
            this.e뷰어.Size = new System.Drawing.Size(953, 622);
            this.e뷰어.TabIndex = 1;
            // 
            // ResultImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.e뷰어);
            this.Controls.Add(this.e카메라);
            this.Name = "ResultImages";
            this.Size = new System.Drawing.Size(953, 660);
            ((System.ComponentModel.ISupportInitialize)(this.e카메라.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e뷰어)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup e카메라;
        private Cogutils.RecordDisplay e뷰어;
    }
}
