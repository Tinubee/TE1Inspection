namespace TE1.UI.Controls
{
    partial class ResultGrid
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.Bind검사정보 = new System.Windows.Forms.BindingSource(this.components);
            this.GridControl1 = new MvUtils.CustomGrid();
            this.GridView1 = new MvUtils.CustomView();
            this.col검사분류 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.e분류 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.col검사명칭 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사그룹 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사장치 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정단위 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col기준값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최소값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최대값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col보정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col결과값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정결과 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.GridControl2 = new MvUtils.CustomGrid();
            this.Bind표면검사 = new System.Windows.Forms.BindingSource(this.components);
            this.GridView2 = new MvUtils.CustomView();
            this.col장치구분 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col불량유형 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col신뢰점수 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검출크기 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col가로중심 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col세로중심 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col가로길이 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col세로길이 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col회전각도 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사정보)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e분류)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind표면검사)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(653, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 683);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(653, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 683);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(653, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 683);
            // 
            // Bind검사정보
            // 
            this.Bind검사정보.DataSource = typeof(System.Collections.Generic.List<TE1.Schemas.검사정보>);
            // 
            // GridControl1
            // 
            this.GridControl1.DataSource = this.Bind검사정보;
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 0);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.e분류});
            this.GridControl1.Size = new System.Drawing.Size(651, 652);
            this.GridControl1.TabIndex = 4;
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
            this.GridView1.Caption = "";
            this.GridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col검사분류,
            this.col검사명칭,
            this.col검사그룹,
            this.col검사장치,
            this.col측정단위,
            this.col측정값,
            this.col기준값,
            this.col최소값,
            this.col최대값,
            this.col보정값,
            this.col결과값,
            this.col측정결과});
            this.GridView1.FooterPanelHeight = 21;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 21;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 18;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.Editable = false;
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsCustomization.AllowSort = false;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.OptionsView.ShowIndicator = false;
            this.GridView1.RowHeight = 20;
            // 
            // col검사분류
            // 
            this.col검사분류.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사분류.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사분류.Caption = "Category";
            this.col검사분류.ColumnEdit = this.e분류;
            this.col검사분류.FieldName = "검사분류";
            this.col검사분류.Name = "col검사분류";
            // 
            // e분류
            // 
            this.e분류.AutoHeight = false;
            this.e분류.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e분류.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("명칭", "명칭")});
            this.e분류.DisplayMember = "명칭";
            this.e분류.Name = "e분류";
            this.e분류.NullText = "[Category]";
            this.e분류.ShowHeader = false;
            this.e분류.ValueMember = "코드";
            // 
            // col검사명칭
            // 
            this.col검사명칭.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사명칭.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사명칭.Caption = "Name";
            this.col검사명칭.FieldName = "검사명칭";
            this.col검사명칭.Name = "col검사명칭";
            this.col검사명칭.Visible = true;
            this.col검사명칭.VisibleIndex = 0;
            // 
            // col검사그룹
            // 
            this.col검사그룹.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사그룹.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사그룹.Caption = "Group";
            this.col검사그룹.FieldName = "검사그룹";
            this.col검사그룹.Name = "col검사그룹";
            // 
            // col검사장치
            // 
            this.col검사장치.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사장치.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사장치.Caption = "Device";
            this.col검사장치.FieldName = "검사장치";
            this.col검사장치.Name = "col검사장치";
            // 
            // col측정단위
            // 
            this.col측정단위.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정단위.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정단위.Caption = "Unit";
            this.col측정단위.FieldName = "측정단위";
            this.col측정단위.Name = "col측정단위";
            this.col측정단위.Visible = true;
            this.col측정단위.VisibleIndex = 1;
            // 
            // col측정값
            // 
            this.col측정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정값.Caption = "Measure";
            this.col측정값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col측정값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col측정값.FieldName = "측정값";
            this.col측정값.Name = "col측정값";
            // 
            // col기준값
            // 
            this.col기준값.AppearanceHeader.Options.UseTextOptions = true;
            this.col기준값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col기준값.Caption = "Norminal";
            this.col기준값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col기준값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col기준값.FieldName = "기준값";
            this.col기준값.Name = "col기준값";
            this.col기준값.Visible = true;
            this.col기준값.VisibleIndex = 3;
            // 
            // col최소값
            // 
            this.col최소값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최소값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최소값.Caption = "Min";
            this.col최소값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col최소값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col최소값.FieldName = "최소값";
            this.col최소값.Name = "col최소값";
            this.col최소값.Visible = true;
            this.col최소값.VisibleIndex = 2;
            // 
            // col최대값
            // 
            this.col최대값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최대값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최대값.Caption = "Max";
            this.col최대값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col최대값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col최대값.FieldName = "최대값";
            this.col최대값.Name = "col최대값";
            this.col최대값.Visible = true;
            this.col최대값.VisibleIndex = 4;
            // 
            // col보정값
            // 
            this.col보정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col보정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col보정값.Caption = "Offset";
            this.col보정값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col보정값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col보정값.FieldName = "보정값";
            this.col보정값.Name = "col보정값";
            // 
            // col결과값
            // 
            this.col결과값.AppearanceHeader.Options.UseTextOptions = true;
            this.col결과값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col결과값.Caption = "Value";
            this.col결과값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col결과값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col결과값.FieldName = "결과값";
            this.col결과값.Name = "col결과값";
            this.col결과값.Visible = true;
            this.col결과값.VisibleIndex = 5;
            // 
            // col측정결과
            // 
            this.col측정결과.AppearanceCell.Options.UseTextOptions = true;
            this.col측정결과.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정결과.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정결과.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정결과.Caption = "Result";
            this.col측정결과.FieldName = "측정결과";
            this.col측정결과.Name = "col측정결과";
            this.col측정결과.Visible = true;
            this.col측정결과.VisibleIndex = 6;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Bottom;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(653, 683);
            this.xtraTabControl1.TabIndex = 9;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.GridControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(651, 652);
            this.xtraTabPage1.Text = "CTQ";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.GridControl2);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(651, 652);
            this.xtraTabPage2.Text = "Surface";
            // 
            // GridControl2
            // 
            this.GridControl2.DataSource = this.Bind표면검사;
            this.GridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl2.Location = new System.Drawing.Point(0, 0);
            this.GridControl2.MainView = this.GridView2;
            this.GridControl2.Name = "GridControl2";
            this.GridControl2.Size = new System.Drawing.Size(651, 652);
            this.GridControl2.TabIndex = 5;
            this.GridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView2});
            // 
            // Bind표면검사
            // 
            this.Bind표면검사.DataSource = typeof(System.Collections.Generic.List<TE1.Schemas.표면불량>);
            // 
            // GridView2
            // 
            this.GridView2.AllowColumnMenu = true;
            this.GridView2.AllowCustomMenu = true;
            this.GridView2.AllowExport = true;
            this.GridView2.AllowPrint = true;
            this.GridView2.AllowSettingsMenu = false;
            this.GridView2.AllowSummaryMenu = true;
            this.GridView2.ApplyFocusedRow = true;
            this.GridView2.Caption = "";
            this.GridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col장치구분,
            this.col불량유형,
            this.col신뢰점수,
            this.col검출크기,
            this.col가로중심,
            this.col세로중심,
            this.col가로길이,
            this.col세로길이,
            this.col회전각도});
            this.GridView2.FooterPanelHeight = 21;
            this.GridView2.GridControl = this.GridControl2;
            this.GridView2.GroupRowHeight = 21;
            this.GridView2.IndicatorWidth = 44;
            this.GridView2.MinColumnRowHeight = 24;
            this.GridView2.MinRowHeight = 18;
            this.GridView2.Name = "GridView2";
            this.GridView2.OptionsBehavior.Editable = false;
            this.GridView2.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView2.OptionsCustomization.AllowGroup = false;
            this.GridView2.OptionsCustomization.AllowSort = false;
            this.GridView2.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView2.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView2.OptionsPrint.AutoWidth = false;
            this.GridView2.OptionsPrint.UsePrintStyles = false;
            this.GridView2.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView2.OptionsView.ShowGroupPanel = false;
            this.GridView2.OptionsView.ShowIndicator = false;
            this.GridView2.RowHeight = 20;
            // 
            // col장치구분
            // 
            this.col장치구분.AppearanceHeader.Options.UseTextOptions = true;
            this.col장치구분.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col장치구분.FieldName = "장치구분";
            this.col장치구분.Name = "col장치구분";
            this.col장치구분.Visible = true;
            this.col장치구분.VisibleIndex = 0;
            // 
            // col불량유형
            // 
            this.col불량유형.AppearanceHeader.Options.UseTextOptions = true;
            this.col불량유형.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col불량유형.FieldName = "불량유형";
            this.col불량유형.Name = "col불량유형";
            this.col불량유형.Visible = true;
            this.col불량유형.VisibleIndex = 3;
            // 
            // col신뢰점수
            // 
            this.col신뢰점수.AppearanceHeader.Options.UseTextOptions = true;
            this.col신뢰점수.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col신뢰점수.FieldName = "신뢰점수";
            this.col신뢰점수.Name = "col신뢰점수";
            this.col신뢰점수.Visible = true;
            this.col신뢰점수.VisibleIndex = 1;
            // 
            // col검출크기
            // 
            this.col검출크기.AppearanceHeader.Options.UseTextOptions = true;
            this.col검출크기.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검출크기.FieldName = "검출크기";
            this.col검출크기.Name = "col검출크기";
            this.col검출크기.Visible = true;
            this.col검출크기.VisibleIndex = 2;
            // 
            // col가로중심
            // 
            this.col가로중심.AppearanceHeader.Options.UseTextOptions = true;
            this.col가로중심.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col가로중심.FieldName = "가로중심";
            this.col가로중심.Name = "col가로중심";
            // 
            // col세로중심
            // 
            this.col세로중심.AppearanceHeader.Options.UseTextOptions = true;
            this.col세로중심.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col세로중심.FieldName = "세로중심";
            this.col세로중심.Name = "col세로중심";
            // 
            // col가로길이
            // 
            this.col가로길이.AppearanceHeader.Options.UseTextOptions = true;
            this.col가로길이.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col가로길이.FieldName = "가로길이";
            this.col가로길이.Name = "col가로길이";
            // 
            // col세로길이
            // 
            this.col세로길이.AppearanceHeader.Options.UseTextOptions = true;
            this.col세로길이.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col세로길이.FieldName = "세로길이";
            this.col세로길이.Name = "col세로길이";
            // 
            // col회전각도
            // 
            this.col회전각도.AppearanceHeader.Options.UseTextOptions = true;
            this.col회전각도.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col회전각도.FieldName = "회전각도";
            this.col회전각도.Name = "col회전각도";
            // 
            // ResultGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ResultGrid";
            this.Size = new System.Drawing.Size(653, 683);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사정보)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e분류)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind표면검사)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.BindingSource Bind검사정보;
        private MvUtils.CustomGrid GridControl1;
        private MvUtils.CustomView GridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col검사명칭;
        private DevExpress.XtraGrid.Columns.GridColumn col검사그룹;
        private DevExpress.XtraGrid.Columns.GridColumn col검사장치;
        private DevExpress.XtraGrid.Columns.GridColumn col검사분류;
        private DevExpress.XtraGrid.Columns.GridColumn col측정단위;
        private DevExpress.XtraGrid.Columns.GridColumn col측정값;
        private DevExpress.XtraGrid.Columns.GridColumn col기준값;
        private DevExpress.XtraGrid.Columns.GridColumn col최소값;
        private DevExpress.XtraGrid.Columns.GridColumn col최대값;
        private DevExpress.XtraGrid.Columns.GridColumn col보정값;
        private DevExpress.XtraGrid.Columns.GridColumn col결과값;
        private DevExpress.XtraGrid.Columns.GridColumn col측정결과;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private MvUtils.CustomGrid GridControl2;
        private MvUtils.CustomView GridView2;
        private System.Windows.Forms.BindingSource Bind표면검사;
        private DevExpress.XtraGrid.Columns.GridColumn col장치구분;
        private DevExpress.XtraGrid.Columns.GridColumn col불량유형;
        private DevExpress.XtraGrid.Columns.GridColumn col신뢰점수;
        private DevExpress.XtraGrid.Columns.GridColumn col검출크기;
        private DevExpress.XtraGrid.Columns.GridColumn col가로중심;
        private DevExpress.XtraGrid.Columns.GridColumn col세로중심;
        private DevExpress.XtraGrid.Columns.GridColumn col가로길이;
        private DevExpress.XtraGrid.Columns.GridColumn col세로길이;
        private DevExpress.XtraGrid.Columns.GridColumn col회전각도;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit e분류;
    }
}
