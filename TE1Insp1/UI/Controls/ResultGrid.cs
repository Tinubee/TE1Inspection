using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using TE1.Schemas;
using TE1.UI.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using DevExpress.Data.Filtering;
using DevExpress.XtraGrid.Columns;
using System.Runtime.CompilerServices;
using System.Linq;

namespace TE1.UI.Controls
{
    public partial class ResultGrid : XtraUserControl
    {
        public ResultGrid() => InitializeComponent();

        private 검사결과 결과 = null;
        private Boolean Loading { get; set; } = false;
        public void Init()
        {
            this.e분류.DataSource = Global.분류자료;
            this.GridView1.ApplyFocusedRow = false;
            this.GridView1.Init(this.barManager1);
            this.GridView1.AddRowSelectedEvent(RowSelectedEvent);
            this.GridView1.CustomDrawCell += GridView1_CustomDrawCell;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            //this.GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView2.Init(this.barManager1);
            this.GridView2.OptionsBehavior.Editable = false;
            this.GridView2.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
            Localization.SetColumnCaption(this.GridView1, typeof(검사정보));
            Localization.SetColumnCaption(this.GridView2, typeof(표면불량));
            this.GridView1.OptionsView.ShowAutoFilterRow = true;
            //FilterActive();
            this.GridView1.ColumnFilterChanged += FilterChanged;
            //this.GridControl1.DataSource = Bind검사정보;
            //List<검사항목> 필터리스트 = new List<검사항목>();
            //foreach (검사정보 정보 in Global.모델자료.선택모델.검사설정)
            //{
            //    if (정보.isShow)
            //        this.GridView1.ActiveFilterCriteria = new BinaryOperator("Name", $"{정보.검사항목}", BinaryOperatorType.Equal);
            //}
        }

        public void FilterActive()
        {
            this.Loading = true;
            List<String> 필터리스트 = new List<String>();
            foreach (검사정보 정보 in Global.모델자료.선택모델.검사설정)
            {
                if (정보.isShow)
                    필터리스트.Add(정보.검사항목.ToString());
            }

            string filterString = $"[{nameof(검사정보.검사명칭)}] In ({string.Join(",", 필터리스트.Select(n => $"'{n}'"))})";
            this.GridView1.ActiveFilterString = filterString;
            this.Loading = false;
        }
        private void FilterChanged(object sender, EventArgs e)
        {
            if (this.Loading) return;

            GridView view = sender as GridView;
            List<검사항목> 필터리스트 = new List<검사항목>();

            for (int i = 0; i < view.RowCount; i++)
            {
                int rowHandle = view.GetVisibleRowHandle(i);
                검사정보 cellValue = view.GetRow(rowHandle) as 검사정보;

                //검사정보 정보 = Global.모델자료.선택모델.검사설정.GetItem(cellValue.검사항목);
                if (view.IsDataRow(rowHandle))
                    필터리스트.Add(cellValue.검사항목);
            }

            foreach (검사정보 정보 in Global.모델자료.선택모델.검사설정)
            {
                if (필터리스트.Contains(정보.검사항목)) 정보.isShow = true;
                else 정보.isShow = false;
            }
            Global.MainForm.검사항목표시변경();
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
            //this.GridView1.ActiveFilter.Clear();
            //foreach (검사정보 정보 in Global.모델자료.선택모델.검사설정)
            //{
            //    if (정보.isShow)
            //        this.GridView1.ActiveFilter.Add(this.col검사명칭, new ColumnFilterInfo($"[{nameof(검사정보.검사명칭)}] = {정보.검사명칭}"));
            //}
            //Global.MainForm.검사항목표시변경();
        }

        private void RowSelectedEvent(GridView view, Int32 rowHandle)
        {
            if (this.FindForm().GetType() == typeof(CogToolEdit)) return;
            검사정보 검사 = view.GetRow(rowHandle) as 검사정보;
            this.결과?.카메라검사보기(검사);
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
