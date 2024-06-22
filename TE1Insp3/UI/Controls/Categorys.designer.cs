namespace TE1.UI.Controls
{
    partial class Categorys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Categorys));
            this.GridControl1 = new MvUtils.CustomGrid();
            this.Bind검사분류 = new System.Windows.Forms.BindingSource(this.components);
            this.GridView1 = new MvUtils.CustomView();
            this.col코드 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col순번 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col명칭 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col그룹 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.b저장 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사분류)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GridControl1
            // 
            this.GridControl1.DataSource = this.Bind검사분류;
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 0);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(513, 386);
            this.GridControl1.TabIndex = 0;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView1});
            // 
            // Bind검사분류
            // 
            this.Bind검사분류.DataSource = typeof(TE1.Schemas.분류자료);
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
            this.col코드,
            this.col순번,
            this.col명칭,
            this.col그룹});
            this.GridView1.FooterPanelHeight = 21;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 21;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 18;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsCustomization.AllowFilter = false;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.OptionsView.ShowIndicator = false;
            this.GridView1.RowHeight = 20;
            this.GridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.col순번, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // col코드
            // 
            this.col코드.AppearanceCell.Options.UseTextOptions = true;
            this.col코드.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col코드.AppearanceHeader.Options.UseTextOptions = true;
            this.col코드.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col코드.FieldName = "코드";
            this.col코드.Name = "col코드";
            this.col코드.OptionsColumn.AllowEdit = false;
            this.col코드.OptionsColumn.AllowFocus = false;
            // 
            // col순번
            // 
            this.col순번.AppearanceCell.Options.UseTextOptions = true;
            this.col순번.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col순번.AppearanceHeader.Options.UseTextOptions = true;
            this.col순번.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col순번.FieldName = "순번";
            this.col순번.Name = "col순번";
            this.col순번.Visible = true;
            this.col순번.VisibleIndex = 0;
            // 
            // col명칭
            // 
            this.col명칭.AppearanceHeader.Options.UseTextOptions = true;
            this.col명칭.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col명칭.FieldName = "명칭";
            this.col명칭.Name = "col명칭";
            this.col명칭.Visible = true;
            this.col명칭.VisibleIndex = 1;
            // 
            // col그룹
            // 
            this.col그룹.AppearanceCell.Options.UseTextOptions = true;
            this.col그룹.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col그룹.AppearanceHeader.Options.UseTextOptions = true;
            this.col그룹.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col그룹.FieldName = "그룹";
            this.col그룹.Name = "col그룹";
            this.col그룹.Visible = true;
            this.col그룹.VisibleIndex = 2;
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.b저장);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 386);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(513, 34);
            this.panelControl1.TabIndex = 6;
            // 
            // b저장
            // 
            this.b저장.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b저장.Appearance.Options.UseFont = true;
            this.b저장.Dock = System.Windows.Forms.DockStyle.Right;
            this.b저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b저장.ImageOptions.SvgImage")));
            this.b저장.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.b저장.Location = new System.Drawing.Point(330, 3);
            this.b저장.Name = "b저장";
            this.b저장.Size = new System.Drawing.Size(180, 28);
            this.b저장.TabIndex = 1;
            this.b저장.Text = "Save";
            // 
            // Categorys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "Categorys";
            this.Size = new System.Drawing.Size(513, 420);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사분류)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MvUtils.CustomGrid GridControl1;
        private MvUtils.CustomView GridView1;
        private System.Windows.Forms.BindingSource Bind검사분류;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton b저장;
        private DevExpress.XtraGrid.Columns.GridColumn col코드;
        private DevExpress.XtraGrid.Columns.GridColumn col순번;
        private DevExpress.XtraGrid.Columns.GridColumn col명칭;
        private DevExpress.XtraGrid.Columns.GridColumn col그룹;
    }
}
