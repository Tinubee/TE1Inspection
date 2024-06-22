namespace TE1.UI.Controls
{
    partial class DeviceLamp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceLamp));
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.e프린터 = new DevExpress.XtraEditors.SvgImageBox();
            this.e리더기 = new DevExpress.XtraEditors.SvgImageBox();
            this.e컴퓨터1 = new DevExpress.XtraEditors.SvgImageBox();
            this.e각인기 = new DevExpress.XtraEditors.SvgImageBox();
            this.e장치통신 = new DevExpress.XtraEditors.SvgImageBox();
            this.e컴퓨터2 = new DevExpress.XtraEditors.SvgImageBox();
            this.e통신체크 = new DevExpress.XtraEditors.SvgImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e프린터)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e리더기)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e컴퓨터1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e각인기)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e장치통신)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e컴퓨터2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e통신체크)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Controls.Add(this.e프린터);
            this.tablePanel1.Controls.Add(this.e리더기);
            this.tablePanel1.Controls.Add(this.e컴퓨터1);
            this.tablePanel1.Controls.Add(this.e각인기);
            this.tablePanel1.Controls.Add(this.e장치통신);
            this.tablePanel1.Controls.Add(this.e컴퓨터2);
            this.tablePanel1.Controls.Add(this.e통신체크);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Size = new System.Drawing.Size(200, 94);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // e프린터
            // 
            this.tablePanel1.SetColumn(this.e프린터, 2);
            this.e프린터.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e프린터.Location = new System.Drawing.Point(102, 49);
            this.e프린터.Name = "e프린터";
            this.tablePanel1.SetRow(this.e프린터, 1);
            this.e프린터.Size = new System.Drawing.Size(45, 41);
            this.e프린터.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom;
            this.e프린터.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("e프린터.SvgImage")));
            this.e프린터.TabIndex = 15;
            this.e프린터.Text = "Label Printer";
            this.e프린터.ToolTip = "Label Printer";
            // 
            // e리더기
            // 
            this.tablePanel1.SetColumn(this.e리더기, 1);
            this.e리더기.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e리더기.Location = new System.Drawing.Point(53, 49);
            this.e리더기.Name = "e리더기";
            this.tablePanel1.SetRow(this.e리더기, 1);
            this.e리더기.Size = new System.Drawing.Size(45, 41);
            this.e리더기.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom;
            this.e리더기.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("e리더기.SvgImage")));
            this.e리더기.TabIndex = 14;
            this.e리더기.Text = "QR Reader";
            this.e리더기.ToolTip = "QR Reader";
            // 
            // e컴퓨터1
            // 
            this.tablePanel1.SetColumn(this.e컴퓨터1, 1);
            this.e컴퓨터1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e컴퓨터1.Location = new System.Drawing.Point(53, 4);
            this.e컴퓨터1.Name = "e컴퓨터1";
            this.tablePanel1.SetRow(this.e컴퓨터1, 0);
            this.e컴퓨터1.Size = new System.Drawing.Size(45, 41);
            this.e컴퓨터1.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom;
            this.e컴퓨터1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("e컴퓨터1.SvgImage")));
            this.e컴퓨터1.TabIndex = 16;
            this.e컴퓨터1.Text = "PC 1";
            this.e컴퓨터1.ToolTip = "PC 1";
            // 
            // e각인기
            // 
            this.tablePanel1.SetColumn(this.e각인기, 0);
            this.e각인기.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e각인기.Location = new System.Drawing.Point(4, 49);
            this.e각인기.Name = "e각인기";
            this.tablePanel1.SetRow(this.e각인기, 1);
            this.e각인기.Size = new System.Drawing.Size(45, 41);
            this.e각인기.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom;
            this.e각인기.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("e각인기.SvgImage")));
            this.e각인기.TabIndex = 13;
            this.e각인기.Text = "QR Printer";
            this.e각인기.ToolTip = "QR Printer";
            // 
            // e장치통신
            // 
            this.tablePanel1.SetColumn(this.e장치통신, 0);
            this.e장치통신.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e장치통신.Location = new System.Drawing.Point(4, 4);
            this.e장치통신.Name = "e장치통신";
            this.tablePanel1.SetRow(this.e장치통신, 0);
            this.e장치통신.Size = new System.Drawing.Size(45, 41);
            this.e장치통신.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom;
            this.e장치통신.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("e장치통신.SvgImage")));
            this.e장치통신.TabIndex = 10;
            this.e장치통신.Text = "PLC";
            this.e장치통신.ToolTip = "PLC";
            // 
            // e컴퓨터2
            // 
            this.tablePanel1.SetColumn(this.e컴퓨터2, 2);
            this.e컴퓨터2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e컴퓨터2.Location = new System.Drawing.Point(102, 4);
            this.e컴퓨터2.Name = "e컴퓨터2";
            this.tablePanel1.SetRow(this.e컴퓨터2, 0);
            this.e컴퓨터2.Size = new System.Drawing.Size(45, 41);
            this.e컴퓨터2.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom;
            this.e컴퓨터2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("e컴퓨터2.SvgImage")));
            this.e컴퓨터2.TabIndex = 12;
            this.e컴퓨터2.Text = "PC 2";
            this.e컴퓨터2.ToolTip = "PC 2";
            // 
            // e통신체크
            // 
            this.e통신체크.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e통신체크.Location = new System.Drawing.Point(13, 12);
            this.e통신체크.Name = "e통신체크";
            this.e통신체크.Size = new System.Drawing.Size(32, 33);
            this.e통신체크.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Zoom;
            this.e통신체크.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("e통신체크.SvgImage")));
            this.e통신체크.TabIndex = 9;
            this.e통신체크.Text = "svgImageBox1";
            this.e통신체크.ToolTip = "PLC State";
            // 
            // DeviceLamp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tablePanel1);
            this.Name = "DeviceLamp";
            this.Size = new System.Drawing.Size(200, 94);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e프린터)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e리더기)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e컴퓨터1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e각인기)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e장치통신)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e컴퓨터2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e통신체크)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SvgImageBox e통신체크;
        private DevExpress.XtraEditors.SvgImageBox e리더기;
        private DevExpress.XtraEditors.SvgImageBox e각인기;
        private DevExpress.XtraEditors.SvgImageBox e장치통신;
        private DevExpress.XtraEditors.SvgImageBox e컴퓨터2;
        private DevExpress.XtraEditors.SvgImageBox e컴퓨터1;
        private DevExpress.XtraEditors.SvgImageBox e프린터;
    }
}
