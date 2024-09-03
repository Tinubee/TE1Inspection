using DevExpress.XtraEditors;
using TE1.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TE1.UI.Controls
{
    public partial class Categorys : XtraUserControl
    {
        public Categorys()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.GridView1.Init();
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            this.GridControl1.DataSource = Global.분류자료;
            Localization.SetColumnCaption(this.GridView1, typeof(분류정보));
            this.b저장.Click += 자료저장;
            //this.col중요포인트표시
        }

        private void 자료저장(object sender, EventArgs e)
        {
            Global.분류자료.Save();
        }
    }
}
