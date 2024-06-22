using DevExpress.XtraEditors;
using TE1.Schemas;
using MvUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TE1.UI.Forms
{
    public partial class BulkTest : XtraForm
    {
        public BulkTest()
        {
            InitializeComponent();
            this.Load += FormLoad;
        }

        private void FormLoad(object sender, EventArgs e) => Init();

        private List<String> 이미지 = new List<String>();
        private Boolean 연속여부 => e연속.IsOn;
        private Int32 지연시간 => Convert.ToInt32(e딜레이.Value);
        private 카메라구분 카메라 => (카메라구분)e카메라.EditValue;

        private void Init()
        {
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.InitialDirectory = Global.환경설정.사진저장;
            e이미지.DataSource = 이미지;
            EnumToList 캠 = new EnumToList(Global.그랩제어.Keys.ToArray());
            캠.SetLookUpEdit(e카메라);
            e카메라.ButtonClick += 이미지선택;
            b수행.Click += 테스트수행;
        }

        private void 이미지선택(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            this.이미지.Clear();
            this.이미지.AddRange(openFileDialog1.FileNames);
        }

        private void 테스트수행(object sender, EventArgs e)
        {
            if (this.이미지.Count < 1) return;
            if (카메라 == 카메라구분.None) return;
            if (!Global.비전검사.ContainsKey(카메라)) return;
            비전도구 도구 = Global.비전검사[카메라];
            Task.Run(() => {
                Boolean 검사 = true;
                while (검사)
                {
                    if (this.이미지.Count < 1) break;
                    String 파일 = this.이미지.First();
                    this.이미지.RemoveAt(0);
                    테스트수행(도구, 파일);
                    검사 = 연속여부;
                    if (검사)
                    {
                        this.e이미지.BeginInvoke(new Action(() => { this.e이미지.Invalidate(); }));
                        Task.Delay(지연시간).Wait();
                    }
                }
            });
        }

        private void 테스트수행(비전도구 도구, String 파일)
        {
            FileInfo fi = new FileInfo(파일);
            if (도구 == null) return;
            if (!fi.Exists) return;
            try { 도구.이미지로드(파일); }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message, fi.Name);
                return;
            }

            List<검사정보> ngs = Global.검사자료.수동검사.검사내역.Where(e => e.측정결과 == 결과구분.NG).ToList();
            if (ngs.Count < 1) return;

            String path = Path.Combine(fi.DirectoryName, "NG");
            Directory.CreateDirectory(path);
            String file = Path.Combine(path, fi.Name);
            if (!File.Exists(file)) File.Copy(fi.FullName, file, false);

            Debug.WriteLine(" --------------------------------", fi.Name);
            foreach (검사정보 정보 in ngs)
            {
                Debug.WriteLine($"{정보.최소값}<={정보.기준값}>={정보.최대값}=>{정보.측정값}: {정보.결과값}", 정보.검사항목.ToString());
            }
        }
    }
}