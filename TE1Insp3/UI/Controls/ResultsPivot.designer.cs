﻿namespace TE1.UI.Controls
{
    partial class ResultsPivot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultsPivot));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.e종료일자 = new DevExpress.XtraEditors.DateEdit();
            this.b엑셀파일 = new DevExpress.XtraEditors.SimpleButton();
            this.BindLocalization = new System.Windows.Forms.BindingSource(this.components);
            this.b자료조회 = new DevExpress.XtraEditors.SimpleButton();
            this.e시작일자 = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.GridControl1 = new MvUtils.CustomGrid();
            this.GridView1 = new MvUtils.CustomBandedView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.e반복횟수 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.e제품갯수 = new DevExpress.XtraEditors.SpinEdit();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.b데이터추출 = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e종료일자.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e종료일자.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindLocalization)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e반복횟수.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e제품갯수.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AutoScroll = false;
            this.layoutControl1.Controls.Add(this.e종료일자);
            this.layoutControl1.Controls.Add(this.b엑셀파일);
            this.layoutControl1.Controls.Add(this.b자료조회);
            this.layoutControl1.Controls.Add(this.e시작일자);
            this.layoutControl1.Controls.Add(this.e반복횟수);
            this.layoutControl1.Controls.Add(this.e제품갯수);
            this.layoutControl1.Controls.Add(this.b데이터추출);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1589, 40);
            this.layoutControl1.TabIndex = 10;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // e종료일자
            // 
            this.e종료일자.EditValue = new System.DateTime(2023, 12, 20, 19, 0, 0, 0);
            this.e종료일자.EnterMoveNextControl = true;
            this.e종료일자.Location = new System.Drawing.Point(307, 9);
            this.e종료일자.Name = "e종료일자";
            this.e종료일자.Properties.Appearance.Options.UseTextOptions = true;
            this.e종료일자.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e종료일자.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e종료일자.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e종료일자.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.e종료일자.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.e종료일자.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.e종료일자.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.e종료일자.Properties.MaskSettings.Set("mask", "yyyy-MM-dd HH:mm");
            this.e종료일자.Size = new System.Drawing.Size(94, 22);
            this.e종료일자.StyleController = this.layoutControl1;
            this.e종료일자.TabIndex = 20;
            // 
            // b엑셀파일
            // 
            this.b엑셀파일.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindLocalization, "엑셀버튼", true));
            this.b엑셀파일.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b엑셀파일.ImageOptions.SvgImage")));
            this.b엑셀파일.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.b엑셀파일.Location = new System.Drawing.Point(612, 9);
            this.b엑셀파일.Name = "b엑셀파일";
            this.b엑셀파일.Size = new System.Drawing.Size(174, 22);
            this.b엑셀파일.StyleController = this.layoutControl1;
            this.b엑셀파일.TabIndex = 6;
            this.b엑셀파일.Text = "Export to Excel";
            // 
            // BindLocalization
            // 
            this.BindLocalization.DataSource = typeof(TE1.UI.Controls.Results.LocalizationResults);
            // 
            // b자료조회
            // 
            this.b자료조회.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindLocalization, "조회버튼", true));
            this.b자료조회.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b자료조회.ImageOptions.SvgImage")));
            this.b자료조회.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.b자료조회.Location = new System.Drawing.Point(409, 9);
            this.b자료조회.Name = "b자료조회";
            this.b자료조회.Size = new System.Drawing.Size(112, 22);
            this.b자료조회.StyleController = this.layoutControl1;
            this.b자료조회.TabIndex = 5;
            this.b자료조회.Text = "Search";
            // 
            // e시작일자
            // 
            this.e시작일자.EditValue = new System.DateTime(2023, 12, 20, 7, 0, 0, 0);
            this.e시작일자.EnterMoveNextControl = true;
            this.e시작일자.Location = new System.Drawing.Point(107, 9);
            this.e시작일자.Name = "e시작일자";
            this.e시작일자.Properties.Appearance.Options.UseTextOptions = true;
            this.e시작일자.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e시작일자.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e시작일자.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e시작일자.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.e시작일자.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.e시작일자.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.e시작일자.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.e시작일자.Properties.MaskSettings.Set("mask", "yyyy-MM-dd HH:mm:ss");
            this.e시작일자.Size = new System.Drawing.Size(94, 22);
            this.e시작일자.StyleController = this.layoutControl1;
            this.e시작일자.TabIndex = 0;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.layoutControlItem7});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1589, 40);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.e시작일자;
            this.layoutControlItem1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindLocalization, "시작일자", true));
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(200, 30);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(200, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem1.Size = new System.Drawing.Size(200, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Start";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(86, 15);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(785, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(364, 30);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.b자료조회;
            this.layoutControlItem3.Location = new System.Drawing.Point(400, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(120, 30);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(120, 30);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem3.Size = new System.Drawing.Size(120, 30);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.b엑셀파일;
            this.layoutControlItem2.Location = new System.Drawing.Point(603, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(182, 30);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(182, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem2.Size = new System.Drawing.Size(182, 30);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.Location = new System.Drawing.Point(520, 0);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(83, 0);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(83, 10);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(83, 30);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.e종료일자;
            this.layoutControlItem5.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.BindLocalization, "종료일자", true));
            this.layoutControlItem5.Location = new System.Drawing.Point(200, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(200, 30);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(200, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem5.Size = new System.Drawing.Size(200, 30);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "End";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(86, 15);
            // 
            // GridControl1
            // 
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 40);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(1589, 962);
            this.GridControl1.TabIndex = 11;
            this.GridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView1});
            // 
            // GridView1
            // 
            this.GridView1.AllowColumnMenu = true;
            this.GridView1.AllowCustomMenu = true;
            this.GridView1.AllowExport = true;
            this.GridView1.AllowPrint = true;
            this.GridView1.AllowSettingsMenu = false;
            this.GridView1.AllowSummaryMenu = true;
            this.GridView1.ApplyFocusedRow = true;
            this.GridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2});
            this.GridView1.Caption = "";
            this.GridView1.FooterPanelHeight = 28;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 21;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 17;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.Editable = false;
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsView.ColumnAutoWidth = false;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.ShowFooter = true;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.RowHeight = 21;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "Item";
            this.gridBand2.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 0;
            this.gridBand2.Width = 252;
            // 
            // e반복횟수
            // 
            this.e반복횟수.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e반복횟수.Location = new System.Drawing.Point(1406, 7);
            this.e반복횟수.Name = "e반복횟수";
            this.e반복횟수.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e반복횟수.Properties.Appearance.Options.UseFont = true;
            this.e반복횟수.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e반복횟수.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.e반복횟수.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.e반복횟수.Size = new System.Drawing.Size(50, 26);
            this.e반복횟수.StyleController = this.layoutControl1;
            this.e반복횟수.TabIndex = 21;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.e반복횟수;
            this.layoutControlItem4.Location = new System.Drawing.Point(1301, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(152, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(152, 30);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Repeat Count :";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(86, 15);
            // 
            // e제품갯수
            // 
            this.e제품갯수.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.e제품갯수.Location = new System.Drawing.Point(1254, 7);
            this.e제품갯수.Name = "e제품갯수";
            this.e제품갯수.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.e제품갯수.Properties.Appearance.Options.UseFont = true;
            this.e제품갯수.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e제품갯수.Size = new System.Drawing.Size(50, 26);
            this.e제품갯수.StyleController = this.layoutControl1;
            this.e제품갯수.TabIndex = 22;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.e제품갯수;
            this.layoutControlItem6.Location = new System.Drawing.Point(1149, 0);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(152, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(152, 30);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "Product Count :";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(86, 15);
            // 
            // b데이터추출
            // 
            this.b데이터추출.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.b데이터추출.Location = new System.Drawing.Point(1460, 7);
            this.b데이터추출.Name = "b데이터추출";
            this.b데이터추출.Size = new System.Drawing.Size(122, 26);
            this.b데이터추출.StyleController = this.layoutControl1;
            this.b데이터추출.TabIndex = 23;
            this.b데이터추출.Text = "Extract Data";
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.b데이터추출;
            this.layoutControlItem7.Location = new System.Drawing.Point(1453, 0);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(89, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(126, 30);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // ResultsPivot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.layoutControl1);
            this.Name = "ResultsPivot";
            this.Size = new System.Drawing.Size(1589, 1002);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e종료일자.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e종료일자.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BindLocalization)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e시작일자.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e반복횟수.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e제품갯수.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton b엑셀파일;
        private DevExpress.XtraEditors.SimpleButton b자료조회;
        private DevExpress.XtraEditors.DateEdit e시작일자;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.DateEdit e종료일자;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private MvUtils.CustomGrid GridControl1;
        private MvUtils.CustomBandedView GridView1;
        private System.Windows.Forms.BindingSource BindLocalization;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.SpinEdit e반복횟수;
        private DevExpress.XtraEditors.SpinEdit e제품갯수;
        private DevExpress.XtraEditors.SimpleButton b데이터추출;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
    }
}
