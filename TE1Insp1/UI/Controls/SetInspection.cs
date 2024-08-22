using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MvUtils;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using TE1.Schemas;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Helpers;
using DevExpress.XtraEditors.Filtering.Templates;
using Newtonsoft.Json;

namespace TE1.UI.Controls
{
    public enum 검사설정구분
    {
        [Description("Inspection")]
        기본검사 = 1,
        [Description("Master")]
        마스터검사 = 2,
    }
    public partial class SetInspection : XtraUserControl
    {
        public SetInspection()
        {
            InitializeComponent();
        }

        public delegate void 검사항목선택(모델정보 모델, 검사정보 설정);
        public event 검사항목선택 검사항목변경;
        private LocalizationInspection 번역 = new LocalizationInspection();
        private static String 로그영역 = "Inspection Settings";

        public void Init(검사설정구분 검사설정 = 검사설정구분.기본검사)
        {
            this.GridView1.Init(this.barManager1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            this.GridView1.AddEditSelectionMenuItem();
            this.GridView1.AddSelectPopMenuItems();
            this.GridView1.CustomDrawCell += GridView1_CustomDrawCell;
            this.col최소값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col최대값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col기준값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col보정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col마진값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col실측값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            //this.col교정값.DisplayFormat.FormatString = Global.환경설정.결과표현;

            this.e분류.DataSource = Global.분류자료;
            this.e분류자료.Init();
            this.e모델선택.EditValueChanged += 모델선택;
            this.e모델선택.Properties.DataSource = Global.모델자료;
            this.e모델선택.EditValue = Global.환경설정.선택모델;
            this.e모델선택.CustomDisplayText += 선택모델표현;
            this.b설정저장.Click += 설정저장;
            this.b도구설정.ButtonClick += 도구설정;
            EnumToList 캠 = new EnumToList(Enum.GetValues(typeof(비전파일구분)));

            캠.SetLookUpEdit(this.b도구설정);
            this.b도구설정.EditValue = 캠.First().Value;

            Global.검사자료.수동검사알림 += 수동검사알림;
            this.ｅ교정.ButtonClick += 교정계산;
            this.b측정정보.Click += 측정정보;
            this.b홀버전체보정.Click += B홀버전체보정_Click;
            this.b홀XY옵셋전체입력.Click += B홀XY옵셋전체입력_Click;
            b엑셀데이터불러오기.Click += B엑셀데이터불러오기_Click;

            Localization.SetColumnCaption(this.e모델선택, typeof(모델정보));
            Localization.SetColumnCaption(this.GridView1, typeof(검사정보));
            this.b설정저장.Text = 번역.설정저장;
            this.모델선택(this.e모델선택, EventArgs.Empty);
            검사타입설정(검사설정);
        }
        public void 검사타입설정(검사설정구분 검사설정)
        {
            this.col마스터값.Visible = 검사설정 != 검사설정구분.기본검사;
            this.col마스터공차.Visible = 검사설정 != 검사설정구분.기본검사;

            this.col최대값.Visible = 검사설정 != 검사설정구분.마스터검사;
            this.col최소값.Visible = 검사설정 != 검사설정구분.마스터검사;
            this.col실측값.Visible = 검사설정 != 검사설정구분.마스터검사;
            this.col보정값.Visible = 검사설정 != 검사설정구분.마스터검사;
            this.col마진값.Visible = 검사설정 != 검사설정구분.마스터검사;
            this.col기준값.Visible = 검사설정 != 검사설정구분.마스터검사;
            this.colX.Visible = 검사설정 != 검사설정구분.마스터검사;
            this.colY.Visible = 검사설정 != 검사설정구분.마스터검사;
        }
        private void B엑셀데이터불러오기_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                엑셀데이터불러오기(ofd.FileName);
            }
        }
        private void 엑셀데이터불러오기(string 경로)
        {
            Dictionary<String, Decimal> data = new Dictionary<string, decimal>();
            data = JsonConvert.DeserializeObject<Dictionary<String, Decimal>>(File.ReadAllText(경로), Utils.JsonSetting());

            for (int lop = 0; lop < this.검사설정.Count; lop++)
            {
                검사정보 정보 = this.검사설정[lop] as 검사정보;

                Decimal value = data.FirstOrDefault(x => x.Key == 정보.검사명칭).Value;
                //Debug.WriteLine($"{정보.검사명칭} : {정보.측정값} {정보.결과값} {value}");

                정보.실측값 = value;

                if (정보.측정값 != 0 && 정보.X != 0)// if (value != 0 && 정보.측정값 != 0 && 정보.X != 0)
                {
                    정보.교정계산(정보);
                }
            }
        }
        public void Close() { }

        private 모델구분 선택모델 => (모델구분)this.e모델선택.EditValue;
        public 검사설정 검사설정 => Global.모델자료.GetItem(this.선택모델)?.검사설정;

        private void 모델선택(object sender, EventArgs e)
        {
            try
            {
                this.GridControl1.DataSource = this.검사설정;
                if (this.검사설정 != null && this.검사설정.Count > 0)
                {
                    Task.Run(() =>
                    {
                        Task.Delay(500).Wait();
                        this.GridView1.MoveFirst();
                        //this.SetEditable(this.GridView1, this.GridView1.FocusedRowHandle);
                        this.검사항목변경?.Invoke(Global.모델자료.GetItem(this.선택모델), this.GetItem(this.GridView1, this.GridView1.FocusedRowHandle));
                    });
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(검사설정.로그영역.GetString(), 번역.모델선택, $"{번역.모델선택}\r\n{ex.Message}", true);
                this.GridControl1.DataSource = null;
            }
        }

        private void 선택모델표현(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                모델구분 모델 = (모델구분)e.Value;
                e.DisplayText = $"{((Int32)모델).ToString("d2")}. {Utils.GetDescription(모델)}";
            }
            catch { e.DisplayText = String.Empty; }
        }

        private 검사정보 GetItem(GridView view, Int32 RowHandle)
        {
            if (view == null) return null;
            return view.GetRow(RowHandle) as 검사정보;
        }

        private void 설정저장(object sender, EventArgs e)
        {
            검사설정 설정 = this.검사설정;
            if (설정 == null) return;
            if (!Global.Confirm(this.FindForm(), 번역.저장확인)) return;
            if (설정.Save())
            {
                //Global.피씨통신.검사설정송신(설정);
                Global.정보로그(검사설정.로그영역.GetString(), 번역.설정저장, 번역.저장완료, true);
            }//Global.정보로그(검사설정.로그영역.GetString(), 번역.설정저장, 번역.저장완료, true);
        }

        private void 도구설정(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.b도구설정.EditValue == null || e.Button.Index != 1) return;
            Global.비전검사.도구설정((카메라구분)this.b도구설정.EditValue);
        }

        private void 수동검사알림(카메라구분 카메라, 검사결과 결과)
        {
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => 수동검사알림(카메라, 결과))); return; }
            foreach (검사정보 설정 in 검사설정)
            {
                if (설정.검사장치 != (장치구분)카메라) continue;

                //검사정보 검사 = new 검사정보();
                //if (설정.검사항목 == 검사항목.None)
                //{
                //    검사 = 결과.검사내역.Where(e => e.검사명칭 == 설정.검사명칭).FirstOrDefault();
                //}
                //else
                검사정보 검사 = 결과.검사내역.Where(e => e.검사항목 == 설정.검사항목).FirstOrDefault();

                if (검사 == null || 검사.측정결과 <= 결과구분.ER) continue;
                설정.측정값 = 검사.측정값;
                설정.결과값 = 검사.결과값;
                //설정.교정값 = 검사.교정값;
            }
            this.GridView1.RefreshData();
        }

        private void 교정계산(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            검사정보 정보 = this.GridView1.GetFocusedRow() as 검사정보;
            if (정보 == null) return;
            //if (정보.실측값 == 0) { Global.Notify("Enter the actual value and run it.", 로그영역, AlertControl.AlertTypes.Warning); return; }
            if (정보.측정값 == 0) { Global.Notify("Perform the inspection and then run it.", 로그영역, AlertControl.AlertTypes.Warning); return; }
            if (!Global.Confirm(this.FindForm(), "Want to perform a calibration?")) return;
            정보.교정계산(정보);

            this.GridView1.RefreshRow(this.GridView1.FocusedRowHandle);
        }
        private void B홀버전체보정_Click(object sender, EventArgs e)
        {
            //전체 보정값계산로직 추가 
            for (int lop = 0; lop < this.검사설정.Count; lop++)
            {
                검사정보 정보 = this.검사설정[lop] as 검사정보;
                if (정보.검사명칭.StartsWith("M"))
                    정보.교정계산(정보);
                //if (정보.검사명칭.Contains("Burr"))
                //    정보.교정계산(정보);

                //if (정보.검사명칭.StartsWith("H") && (정보.검사명칭.Contains("X") || 정보.검사명칭.Contains("Y")))
                //    정보.교정계산(정보);

                //if (정보.검사명칭.StartsWith("H") && 정보.검사명칭.Contains("D"))
                //    정보.교정계산(정보);

                //if (정보.검사명칭.StartsWith("T"))
                //{
                //    if (정보.검사항목 != 검사항목.T044)
                //    {
                //        정보.교정계산(정보);
                //    }
                //}
            }
            this.GridView1.RefreshData();
        }

        private void B홀XY옵셋전체입력_Click(object sender, EventArgs e)
        {
            for (int lop = 0; lop < this.검사설정.Count; lop++)
            {
                검사정보 정보 = this.검사설정[lop] as 검사정보;
                if (정보.검사명칭.StartsWith("H"))
                {
                    if (정보.검사명칭.Contains("X"))
                    {
                        정보.보정값 = Convert.ToDecimal(-정보.X);
                        정보.최소값 = Convert.ToDecimal(-0.25);
                        정보.기준값 = 0;
                        정보.최대값 = Convert.ToDecimal(0.25);
                    }
                    else if (정보.검사명칭.Contains("Y"))
                    {
                        정보.보정값 = Convert.ToDecimal(-정보.Y);
                        정보.최소값 = Convert.ToDecimal(-0.25);
                        정보.기준값 = 0;
                        정보.최대값 = Convert.ToDecimal(0.25);
                    }
                }
                else if (정보.검사명칭.StartsWith("T"))
                {
                    if (정보.Attr.검사정보.InsType == InsType.X)
                    {
                        정보.보정값 = (Decimal)정보.Attr.검사정보.X;
                    }
                    else if (정보.Attr.검사정보.InsType == InsType.Y)
                    {
                        정보.보정값 = (Decimal)정보.Attr.검사정보.Y;
                    }
                }
            }
            this.GridView1.RefreshData();
        }

        private void 측정정보(object sender, EventArgs e)
        {
            Forms.CalibInfo f = new Forms.CalibInfo();
            f.Show(Global.MainForm);
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;
            검사정보 정보 = view.GetRow(e.RowHandle) as 검사정보;
            if (정보 == null) return;
            정보.SetAppearance(e);
        }

        private class LocalizationInspection
        {
            private enum Items
            {
                [Translation("Save", "설정저장")]
                설정저장,
                [Translation("It's saved.", "저장되었습니다.")]
                저장완료,
                [Translation("Save the inspection settings?", "검사 설정을 저장하시겠습니까?")]
                저장확인,
                [Translation("Delete this selected item?", "선택 항목을 삭제하시겠습니까?")]
                삭제확인,

                [Translation("Select Model", "모델선택")]
                모델선택,
                [Translation("No models selected.", "선택한 모델이 없습니다.")]
                모델없음,
                [Translation("FlipXY", "위치회전")]
                위치회전,
                [Translation("Do you want to rotate Viewer position?", "뷰어 위치를 회전하시겠습니까?")]
                회전확인,
            }

            public String 설정저장 => Localization.GetString(Items.설정저장);
            public String 저장완료 => Localization.GetString(Items.저장완료);
            public String 저장확인 => Localization.GetString(Items.저장확인);
            public String 삭제확인 => Localization.GetString(Items.삭제확인);
            public String 모델선택 => Localization.GetString(Items.모델선택);
            public String 모델없음 => Localization.GetString(Items.모델없음);
            public String 위치회전 => Localization.GetString(Items.위치회전);
            public String 회전확인 => Localization.GetString(Items.회전확인);
        }
    }
}
