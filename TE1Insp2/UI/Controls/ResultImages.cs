using DevExpress.XtraEditors;
using TE1.Schemas;
using MvUtils;
using System;
using System.IO;
using System.Linq;

namespace TE1.UI.Controls
{
    public partial class ResultImages : XtraUserControl
    {
        public ResultImages() => InitializeComponent();

        private 검사결과 결과 = null;
        private 카메라구분 카메라 => (카메라구분)this.e카메라.EditValue;
        public void Init()
        {
            this.e뷰어.Init(false);
            EnumToList 캠 = new EnumToList(Global.그랩제어.Keys.ToArray());
            캠.SetRadioGroup(this.e카메라);
        }
        public void Show(검사결과 결과)
        {
            this.결과 = 결과;
            this.e카메라.EditValue = 카메라구분.Cam03;
            this.e카메라.EditValueChanged += 카메라선택;
            this.카메라선택(this.e카메라, EventArgs.Empty);
        }

        private void 카메라선택(object sender, EventArgs e)
        {
            if (this.결과 == null || !Global.비전검사.ContainsKey(카메라)) return;
            비전도구 도구 = Global.비전검사[카메라];
            if (this.결과.검사번호 >= 9999) this.e뷰어.Image = 도구.InputImage;
            else
            {
                String file = Global.사진자료.CopyImageFile(결과.검사일시, 결과.검사번호, 카메라);
                if (String.IsNullOrEmpty(file) || !File.Exists(file))
                    Utils.WarningMsg($"[{file}] The image file does not exist.");
                else this.e뷰어.Image = Common.LoadImage(file);
            }
        }
    }
}
