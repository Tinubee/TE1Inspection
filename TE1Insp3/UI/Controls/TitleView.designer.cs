﻿
namespace TE1.UI.Controls
{
    partial class TitleView
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
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.lb모드 = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.label5.Appearance.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.label5.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(160)))), ((int)(((byte)(27)))));
            this.label5.Appearance.Options.UseBackColor = true;
            this.label5.Appearance.Options.UseFont = true;
            this.label5.Appearance.Options.UseForeColor = true;
            this.label5.Appearance.Options.UseTextOptions = true;
            this.label5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.label5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.label5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(3, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 28);
            this.label5.TabIndex = 70;
            this.label5.Text = "TE1 Vision Inspection";
            // 
            // lb모드
            // 
            this.lb모드.Appearance.Font = new System.Drawing.Font("Tahoma", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb모드.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            this.lb모드.Appearance.Options.UseFont = true;
            this.lb모드.Appearance.Options.UseForeColor = true;
            this.lb모드.Appearance.Options.UseTextOptions = true;
            this.lb모드.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lb모드.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lb모드.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lb모드.Location = new System.Drawing.Point(3, 3);
            this.lb모드.Name = "lb모드";
            this.lb모드.Size = new System.Drawing.Size(214, 66);
            this.lb모드.TabIndex = 71;
            this.lb모드.Text = "Car Tech";
            // 
            // TitleView
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb모드);
            this.Controls.Add(this.label5);
            this.Name = "TitleView";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(220, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl label5;
        private DevExpress.XtraEditors.LabelControl lb모드;
    }
}
