using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using TE1.Schemas;
using TE1.UI.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TE1.UI.Controls
{
    public partial class ResultGrid : XtraUserControl
    {
        public delegate void 검사항목변경대리자(List<검사항목> 항목들);
        public event 검사항목변경대리자 검사항목선택변경;
        public 검사정보 선택정보 = null;

        public ResultGrid() => InitializeComponent();

        private 검사결과 결과 = null;
        public void Init()
        {
            this.e분류.DataSource = Global.분류자료;
            this.GridView1.ApplyFocusedRow = false;
            this.GridView1.Init(this.barManager1);
            this.GridView1.CustomDrawCell += GridView1_CustomDrawCell;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            //this.GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.RowClick += GridView1_RowClick;
            //this.GridView1.ActiveFilter.Clear();
            //this.GridView1.ActiveFilter.Add(this.col결과분류, new ColumnFilterInfo($"[{nameof(검사정보.결과분류)}] = {(Int32)결과분류.Summary}", $"[{Localization.GetString(typeof(검사정보).GetProperty(nameof(검사정보.결과분류)))}] = {결과분류.Summary}"));
            this.GridView2.Init(this.barManager1);
            this.GridView2.OptionsBehavior.Editable = false;
            this.GridView2.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            this.GridView1.ColumnFilterChanged += FilterChanged;
            Localization.SetColumnCaption(this.GridView1, typeof(검사정보));
            Localization.SetColumnCaption(this.GridView2, typeof(표면불량));
        }

        private void FilterChanged(object sender, EventArgs e)
        {
            List<검사정보> filteredItems = new List<검사정보>();
            GridView view = sender as GridView;
            for (int i = 0; i < view.RowCount; i++)
            {
                int rowHandle = view.GetVisibleRowHandle(i);
                if (view.IsDataRow(rowHandle))
                {
                    검사정보 cellValue = view.GetRow(rowHandle) as 검사정보;
                    filteredItems.Add(cellValue);
                }
            }

            //foreach (검사정보 item in filteredItems)
            //    Debug.WriteLine($"{item.검사명칭}");
         
        }

        private void GridView1_RowClick(object sender, RowClickEventArgs e)
        {
            List<검사항목> 항목들 = new List<검사항목>();
            if (선택정보 != null)
            {
                InsItems.GetItem(선택정보.검사명칭).FontSize = 5;
                항목들.Add(선택정보.검사항목);
            }

            GridView view = sender as GridView;
            선택정보 = view.GetRow(e.RowHandle) as 검사정보;
            InsItems.GetItem(선택정보.검사명칭).FontSize = 20;
            항목들.Add(선택정보.검사항목);
            //Debug.WriteLine($"{정보.검사명칭}");
            this.검사항목선택변경?.Invoke(항목들);
        }

        public void RefreshData()
        {
            this.GridView1?.RefreshData();
            this.GridView2?.RefreshData();
        }

        public void SetResults(검사결과 결과)
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { SetResults(결과); })); return; }
            this.결과 = 결과;
            this.GridControl1.DataSource = 결과.검사내역;
            this.GridControl2.DataSource = 결과.표면불량;
            this.RefreshData();
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;
            검사정보 정보 = view.GetRow(e.RowHandle) as 검사정보;
            if (정보 == null) return;
            정보.SetAppearance(e);
        }
    }
}
