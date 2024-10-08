﻿
namespace TE1.UI.Controls
{
    partial class SetInspection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetInspection));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.GridControl1 = new MvUtils.CustomGrid();
            this.검사설정Bind = new System.Windows.Forms.BindingSource(this.components);
            this.GridView1 = new MvUtils.CustomView();
            this.col검사분류 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.e분류 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.Bind검사분류 = new System.Windows.Forms.BindingSource(this.components);
            this.col검사명칭 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사그룹 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사항목 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사장치 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정단위 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최소값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col기준값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최대값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col보정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col교정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col마진값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col실측값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ｅ교정 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.col마스터값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col마스터공차 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사여부 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col중요검사포인트 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.e모델선택 = new DevExpress.XtraEditors.LookUpEdit();
            this.모델자료Bind = new System.Windows.Forms.BindingSource(this.components);
            this.b설정저장 = new DevExpress.XtraEditors.SimpleButton();
            this.b분류설정 = new DevExpress.XtraEditors.PopupContainerEdit();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.e분류자료 = new TE1.UI.Controls.Categorys();
            this.repositoryItemToggleSwitch1 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            this.repositoryItemToggleSwitch2 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.검사설정Bind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e분류)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사분류)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ｅ교정)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e모델선택.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.모델자료Bind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.b분류설정.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch2)).BeginInit();
            this.SuspendLayout();
            // 
            // GridControl1
            // 
            this.GridControl1.DataSource = this.검사설정Bind;
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 52);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ｅ교정,
            this.e분류,
            this.repositoryItemToggleSwitch1,
            this.repositoryItemToggleSwitch2});
            this.GridControl1.Size = new System.Drawing.Size(1244, 729);
            this.GridControl1.TabIndex = 0;
            this.GridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView1});
            // 
            // 검사설정Bind
            // 
            this.검사설정Bind.DataSource = typeof(TE1.Schemas.검사설정);
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
            this.col검사항목,
            this.col검사장치,
            this.col측정단위,
            this.colX,
            this.colY,
            this.col최소값,
            this.col기준값,
            this.col최대값,
            this.col보정값,
            this.col교정값,
            this.col마진값,
            this.col측정값,
            this.col실측값,
            this.col마스터값,
            this.col마스터공차,
            this.col검사여부,
            this.col중요검사포인트});
            this.GridView1.FooterPanelHeight = 21;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 21;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 16;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.GridView1.OptionsView.ShowAutoFilterRow = true;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.RowHeight = 20;
            // 
            // col검사분류
            // 
            this.col검사분류.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사분류.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사분류.ColumnEdit = this.e분류;
            this.col검사분류.FieldName = "검사분류";
            this.col검사분류.Name = "col검사분류";
            this.col검사분류.Visible = true;
            this.col검사분류.VisibleIndex = 0;
            // 
            // e분류
            // 
            this.e분류.AutoHeight = false;
            this.e분류.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e분류.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("명칭", "Category")});
            this.e분류.DataSource = this.Bind검사분류;
            this.e분류.DisplayMember = "명칭";
            this.e분류.Name = "e분류";
            this.e분류.NullText = "[Category]";
            this.e분류.ShowHeader = false;
            this.e분류.ValueMember = "코드";
            // 
            // Bind검사분류
            // 
            this.Bind검사분류.DataSource = typeof(TE1.Schemas.분류자료);
            // 
            // col검사명칭
            // 
            this.col검사명칭.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사명칭.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사명칭.FieldName = "검사명칭";
            this.col검사명칭.Name = "col검사명칭";
            this.col검사명칭.Visible = true;
            this.col검사명칭.VisibleIndex = 1;
            // 
            // col검사그룹
            // 
            this.col검사그룹.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사그룹.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사그룹.FieldName = "검사그룹";
            this.col검사그룹.Name = "col검사그룹";
            this.col검사그룹.OptionsColumn.AllowEdit = false;
            this.col검사그룹.Visible = true;
            this.col검사그룹.VisibleIndex = 2;
            // 
            // col검사항목
            // 
            this.col검사항목.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사항목.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사항목.FieldName = "검사항목";
            this.col검사항목.Name = "col검사항목";
            this.col검사항목.OptionsColumn.AllowEdit = false;
            this.col검사항목.Visible = true;
            this.col검사항목.VisibleIndex = 3;
            // 
            // col검사장치
            // 
            this.col검사장치.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사장치.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사장치.Caption = "검사장치";
            this.col검사장치.FieldName = "검사장치";
            this.col검사장치.Name = "col검사장치";
            this.col검사장치.OptionsColumn.AllowEdit = false;
            this.col검사장치.Visible = true;
            this.col검사장치.VisibleIndex = 4;
            // 
            // col측정단위
            // 
            this.col측정단위.AppearanceCell.Options.UseTextOptions = true;
            this.col측정단위.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정단위.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정단위.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정단위.Caption = "단위";
            this.col측정단위.FieldName = "측정단위";
            this.col측정단위.Name = "col측정단위";
            this.col측정단위.Visible = true;
            this.col측정단위.VisibleIndex = 5;
            // 
            // colX
            // 
            this.colX.AppearanceHeader.Options.UseTextOptions = true;
            this.colX.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colX.DisplayFormat.FormatString = "{0:#,0.00}";
            this.colX.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colX.FieldName = "X";
            this.colX.Name = "colX";
            this.colX.Visible = true;
            this.colX.VisibleIndex = 6;
            // 
            // colY
            // 
            this.colY.AppearanceHeader.Options.UseTextOptions = true;
            this.colY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colY.DisplayFormat.FormatString = "{0:#,0.00}";
            this.colY.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colY.FieldName = "Y";
            this.colY.Name = "colY";
            this.colY.Visible = true;
            this.colY.VisibleIndex = 7;
            // 
            // col최소값
            // 
            this.col최소값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최소값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최소값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col최소값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col최소값.FieldName = "최소값";
            this.col최소값.Name = "col최소값";
            this.col최소값.Visible = true;
            this.col최소값.VisibleIndex = 8;
            // 
            // col기준값
            // 
            this.col기준값.AppearanceHeader.Options.UseTextOptions = true;
            this.col기준값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col기준값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col기준값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col기준값.FieldName = "기준값";
            this.col기준값.Name = "col기준값";
            this.col기준값.Visible = true;
            this.col기준값.VisibleIndex = 9;
            // 
            // col최대값
            // 
            this.col최대값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최대값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최대값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col최대값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col최대값.FieldName = "최대값";
            this.col최대값.Name = "col최대값";
            this.col최대값.Visible = true;
            this.col최대값.VisibleIndex = 10;
            // 
            // col보정값
            // 
            this.col보정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col보정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col보정값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col보정값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col보정값.FieldName = "보정값";
            this.col보정값.Name = "col보정값";
            this.col보정값.Visible = true;
            this.col보정값.VisibleIndex = 11;
            // 
            // col교정값
            // 
            this.col교정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col교정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col교정값.DisplayFormat.FormatString = "{0:#,0.000000000;;#}";
            this.col교정값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col교정값.FieldName = "교정값";
            this.col교정값.Name = "col교정값";
            this.col교정값.Visible = true;
            this.col교정값.VisibleIndex = 12;
            // 
            // col마진값
            // 
            this.col마진값.AppearanceHeader.Options.UseTextOptions = true;
            this.col마진값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col마진값.DisplayFormat.FormatString = "{0:#,0.000000000;;#}";
            this.col마진값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col마진값.FieldName = "마진값";
            this.col마진값.Name = "col마진값";
            this.col마진값.Visible = true;
            this.col마진값.VisibleIndex = 17;
            // 
            // col측정값
            // 
            this.col측정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col측정값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col측정값.FieldName = "측정값";
            this.col측정값.Name = "col측정값";
            this.col측정값.Visible = true;
            this.col측정값.VisibleIndex = 13;
            // 
            // col실측값
            // 
            this.col실측값.AppearanceHeader.Options.UseTextOptions = true;
            this.col실측값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col실측값.ColumnEdit = this.ｅ교정;
            this.col실측값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col실측값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col실측값.FieldName = "실측값";
            this.col실측값.Name = "col실측값";
            this.col실측값.Visible = true;
            this.col실측값.VisibleIndex = 14;
            // 
            // ｅ교정
            // 
            this.ｅ교정.AutoHeight = false;
            this.ｅ교정.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ｅ교정.Name = "ｅ교정";
            // 
            // col마스터값
            // 
            this.col마스터값.AppearanceHeader.Options.UseTextOptions = true;
            this.col마스터값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col마스터값.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col마스터값.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col마스터값.FieldName = "마스터값";
            this.col마스터값.Name = "col마스터값";
            this.col마스터값.Visible = true;
            this.col마스터값.VisibleIndex = 15;
            // 
            // col마스터공차
            // 
            this.col마스터공차.AppearanceHeader.Options.UseTextOptions = true;
            this.col마스터공차.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col마스터공차.DisplayFormat.FormatString = "{0:#,0.000}";
            this.col마스터공차.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.col마스터공차.FieldName = "마스터공차";
            this.col마스터공차.Name = "col마스터공차";
            this.col마스터공차.Visible = true;
            this.col마스터공차.VisibleIndex = 16;
            // 
            // col검사여부
            // 
            this.col검사여부.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사여부.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사여부.Caption = "검사여부";
            this.col검사여부.ColumnEdit = this.repositoryItemToggleSwitch2;
            this.col검사여부.FieldName = "검사여부";
            this.col검사여부.Name = "col검사여부";
            this.col검사여부.Visible = true;
            this.col검사여부.VisibleIndex = 18;
            // 
            // col중요검사포인트
            // 
            this.col중요검사포인트.AppearanceHeader.Options.UseTextOptions = true;
            this.col중요검사포인트.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col중요검사포인트.ColumnEdit = this.repositoryItemToggleSwitch1;
            this.col중요검사포인트.FieldName = "중요검사포인트";
            this.col중요검사포인트.Name = "col중요검사포인트";
            this.col중요검사포인트.Visible = true;
            this.col중요검사포인트.VisibleIndex = 19;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1244, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 781);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1244, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 781);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1244, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 781);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.e모델선택);
            this.panelControl1.Controls.Add(this.b설정저장);
            this.panelControl1.Controls.Add(this.b분류설정);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(1244, 52);
            this.panelControl1.TabIndex = 5;
            // 
            // e모델선택
            // 
            this.e모델선택.Dock = System.Windows.Forms.DockStyle.Left;
            this.e모델선택.EnterMoveNextControl = true;
            this.e모델선택.Location = new System.Drawing.Point(180, 5);
            this.e모델선택.Name = "e모델선택";
            this.e모델선택.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e모델선택.Properties.Appearance.Options.UseFont = true;
            this.e모델선택.Properties.Appearance.Options.UseTextOptions = true;
            this.e모델선택.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e모델선택.Properties.AppearanceDropDown.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e모델선택.Properties.AppearanceDropDown.Options.UseFont = true;
            this.e모델선택.Properties.AutoHeight = false;
            this.e모델선택.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e모델선택.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("모델구분", "구분", 150, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("모델설명", "설명", 240, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.e모델선택.Properties.DataSource = this.모델자료Bind;
            this.e모델선택.Properties.DisplayMember = "모델구분";
            this.e모델선택.Properties.NullText = "[모델선택]";
            this.e모델선택.Properties.ValueMember = "모델구분";
            this.e모델선택.Size = new System.Drawing.Size(300, 42);
            this.e모델선택.TabIndex = 9;
            this.e모델선택.Visible = false;
            // 
            // 모델자료Bind
            // 
            this.모델자료Bind.DataSource = typeof(TE1.Schemas.검사설정);
            // 
            // b설정저장
            // 
            this.b설정저장.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b설정저장.Appearance.Options.UseFont = true;
            this.b설정저장.Dock = System.Windows.Forms.DockStyle.Right;
            this.b설정저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b설정저장.ImageOptions.SvgImage")));
            this.b설정저장.Location = new System.Drawing.Point(1059, 5);
            this.b설정저장.Name = "b설정저장";
            this.b설정저장.Size = new System.Drawing.Size(180, 42);
            this.b설정저장.TabIndex = 0;
            this.b설정저장.Text = "설정저장";
            // 
            // b분류설정
            // 
            this.b분류설정.Dock = System.Windows.Forms.DockStyle.Left;
            this.b분류설정.EnterMoveNextControl = true;
            this.b분류설정.Location = new System.Drawing.Point(5, 5);
            this.b분류설정.MenuManager = this.barManager1;
            this.b분류설정.Name = "b분류설정";
            this.b분류설정.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b분류설정.Properties.Appearance.Options.UseFont = true;
            editorButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("editorButtonImageOptions1.SvgImage")));
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(40, 40);
            serializableAppearanceObject1.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            serializableAppearanceObject1.Options.UseFont = true;
            this.b분류설정.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Categorys", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Categorys", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.b분류설정.Properties.NullText = "Categorys";
            this.b분류설정.Properties.PopupControl = this.popupContainerControl1;
            this.b분류설정.Size = new System.Drawing.Size(175, 42);
            this.b분류설정.TabIndex = 21;
            this.b분류설정.ToolTip = "Categorys";
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Controls.Add(this.e분류자료);
            this.popupContainerControl1.Location = new System.Drawing.Point(610, 136);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(500, 400);
            this.popupContainerControl1.TabIndex = 11;
            // 
            // e분류자료
            // 
            this.e분류자료.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e분류자료.Location = new System.Drawing.Point(0, 0);
            this.e분류자료.Name = "e분류자료";
            this.e분류자료.Size = new System.Drawing.Size(500, 400);
            this.e분류자료.TabIndex = 0;
            // 
            // repositoryItemToggleSwitch1
            // 
            this.repositoryItemToggleSwitch1.AutoHeight = false;
            this.repositoryItemToggleSwitch1.Name = "repositoryItemToggleSwitch1";
            this.repositoryItemToggleSwitch1.OffText = "Off";
            this.repositoryItemToggleSwitch1.OnText = "On";
            // 
            // repositoryItemToggleSwitch2
            // 
            this.repositoryItemToggleSwitch2.AutoHeight = false;
            this.repositoryItemToggleSwitch2.Name = "repositoryItemToggleSwitch2";
            this.repositoryItemToggleSwitch2.OffText = "Off";
            this.repositoryItemToggleSwitch2.OnText = "On";
            // 
            // SetInspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerControl1);
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SetInspection";
            this.Size = new System.Drawing.Size(1244, 781);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.검사설정Bind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e분류)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사분류)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ｅ교정)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e모델선택.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.모델자료Bind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.b분류설정.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MvUtils.CustomGrid GridControl1;
        private System.Windows.Forms.BindingSource 검사설정Bind;
        private MvUtils.CustomView GridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col검사그룹;
        private DevExpress.XtraGrid.Columns.GridColumn col측정단위;
        private DevExpress.XtraGrid.Columns.GridColumn col기준값;
        private DevExpress.XtraGrid.Columns.GridColumn col최소값;
        private DevExpress.XtraGrid.Columns.GridColumn col최대값;
        private DevExpress.XtraGrid.Columns.GridColumn col보정값;
        private DevExpress.XtraGrid.Columns.GridColumn col검사여부;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton b설정저장;
        private DevExpress.XtraGrid.Columns.GridColumn col검사장치;
        private System.Windows.Forms.BindingSource 모델자료Bind;
        private DevExpress.XtraEditors.LookUpEdit e모델선택;
        private DevExpress.XtraGrid.Columns.GridColumn col검사항목;
        private DevExpress.XtraGrid.Columns.GridColumn col교정값;
        private DevExpress.XtraGrid.Columns.GridColumn col검사분류;
        private DevExpress.XtraGrid.Columns.GridColumn col실측값;
        private DevExpress.XtraGrid.Columns.GridColumn col측정값;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ｅ교정;
        private DevExpress.XtraGrid.Columns.GridColumn col검사명칭;
        private DevExpress.XtraGrid.Columns.GridColumn col마진값;
        private DevExpress.XtraEditors.PopupContainerEdit b분류설정;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private Categorys e분류자료;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit e분류;
        private System.Windows.Forms.BindingSource Bind검사분류;
        private DevExpress.XtraGrid.Columns.GridColumn colX;
        private DevExpress.XtraGrid.Columns.GridColumn colY;
        private DevExpress.XtraGrid.Columns.GridColumn col마스터값;
        private DevExpress.XtraGrid.Columns.GridColumn col마스터공차;
        private DevExpress.XtraGrid.Columns.GridColumn col중요검사포인트;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch2;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch1;
    }
}
