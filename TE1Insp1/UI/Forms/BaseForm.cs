using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace TE1.UI.Forms
{
    public partial class BaseForm : XtraForm
    {
        public BaseForm() => InitializeComponent();
        public BaseForm(Control control)
        {
            InitializeComponent();
            control.Dock = DockStyle.Fill;
            this.Controls.Add(control);
        }
    }
}