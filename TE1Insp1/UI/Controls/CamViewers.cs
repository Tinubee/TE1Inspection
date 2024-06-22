using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using TE1.Schemas;
using System.Collections.Generic;
using System.Linq;

namespace TE1.UI.Controls
{
    public partial class CamViewers : XtraUserControl
    {
        public CamViewers() => InitializeComponent();

        public enum ViewType { Inspect, Viewer }
        private Dictionary<카메라구분, Cogutils.RecordDisplay> 뷰어 = new Dictionary<카메라구분, Cogutils.RecordDisplay>();

        public void Init(ViewType type = ViewType.Inspect)
        {
            뷰어.Add(카메라구분.Cam01, this.e좌측캠);
            뷰어.Add(카메라구분.Cam02, this.e우측캠);
            뷰어.Add(카메라구분.Cam03, this.e상부캠);
            뷰어.ForEach(v => {
                v.Value.Init(false);
                if (type == ViewType.Inspect)
                    Global.비전검사.SetDisplay(v.Key, v.Value);
            });
            d좌측캠.Text = MvUtils.Utils.GetDescription(카메라구분.Cam01);
            d우측캠.Text = MvUtils.Utils.GetDescription(카메라구분.Cam02);
            d상부캠.Text = MvUtils.Utils.GetDescription(카메라구분.Cam03);
        }
        public void Close() { }
    }
}
