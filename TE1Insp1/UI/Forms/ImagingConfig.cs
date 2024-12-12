using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MvLibs.Tools;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TE1;

namespace TE1.UI.Forms
{
    public partial class ImagingConfig : XtraForm
    {
        public ImagingConfig()
        {
            InitializeComponent();
        }

        private CogToolBlock ToolBlock = null;
        private MvImageTool ImagingTool = null;
        private List<ICvTool> DataSource => ImagingTool?.Tools;
        private CogImage8Grey InputImage => MvLibs.Base.Input<CogImage8Grey>(ToolBlock, "InputImage");
        private CogImage8Grey OutputImage => MvLibs.Base.Input<CogImage8Grey>(ToolBlock, "OutputImage");

        public void Init(CogToolBlock tool)
        {
            this.ToolBlock = tool;
            this.ImagingTool = new MvImageTool(tool);
            this.ImagingTool.Init();
            this.ImagingTool.Load();
            this.ImagingTool.RuntimeMessage += RuntimeMessage;
            this.Init();
        }

        private void Init()
        {
            this.e결과.Init();
            this.e설정.CellValueChanged += (object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e) => {
                if (e.Row.Properties.FieldName != nameof(ICvTool.Name) && e.Row.Properties.FieldName != nameof(ICvTool.Enabled)) return;
                e도구.Refresh();
            };
            this.e도구.SelectedIndexChanged += (object sender, EventArgs e) => {
                e설정.SelectedObject = (sender as CheckedListBoxControl)?.SelectedItem;
                e설정.ExpandAllRows();
            };
            this.e도구.ItemChecking += (object sender, ItemCheckingEventArgs e) => e.Cancel = true;
            this.e도구.MouseDoubleClick += (object sender, MouseEventArgs e) => {
                ICvTool item = (sender as CheckedListBoxControl)?.SelectedItem as ICvTool;
                if (item == null) return;
                item.Enabled = !item.Enabled;
                e도구.Refresh();
                e설정.RefreshAllProperties();
            };
            this.e도구.DataSource = DataSource;
            this.DragDropEvent.DragDrop += ViewDragDrop;

            this.FormClosing += (object sender, FormClosingEventArgs e) => this.ImagingTool.Apply(ImagingTool.ToJson());
            this.b적용.Visible = false;
            //this.b적용.Click += (object sender, EventArgs e) => {
            //    this.ImagingTool.Apply(ImagingTool.ToJson());
            //    Global.Notify("Applyed.\r\nDon't forget to save the tool to make it permanent.", ToolBlock.Name, MvUtils.AlertControl.AlertTypes.Information);
            //};
            this.b수행.Click += (object sender, EventArgs e) => this.Run();
            this.e추가.ButtonClick += AddRemoveItem;
            this.e추가.Properties.DataSource = ImageCorrectors.GetItemList();
        }

        private void Run()
        {
            if (ToolBlock == null || InputImage == null) return;
            ImagingTool.RunClean();
            DisplayResult();
        }

        private void RuntimeMessage(object sender, NameValueCollection e)
        {
            StringBuilder sb = new StringBuilder();
            foreach(String k in e.Keys)
                sb.AppendLine($"{k}: {e[k]}");
            Global.Notify(sb.ToString(), "Warning", MvUtils.AlertControl.AlertTypes.Warning);
        }

        #region DisplayResult
        private void DisplayResult()
        {
            e설정.RefreshAllProperties();
            e결과.SetImage(ImagingTool.OutputImage, null, null);
            GC.Collect();
        }
        private Double CalZoom(Size s)
        {
            Double w = (Double)e결과.ClientSize.Width / s.Width;
            Double h = (Double)e결과.ClientSize.Height / s.Height;
            return Math.Floor(Math.Min(Math.Min(w, h), 1.0d) * 100.0d);
        }
        #endregion

        #region DragDrop
        private void ViewDragDrop(object sender, DragDropEventArgs e)
        {
            if (DataSource == null) return;
            if (e.InsertType != InsertType.Before && e.InsertType != InsertType.After) return;
            ICvTool dst = e도구.GetItem(e도구.SelectedIndex) as ICvTool;
            if (dst == null) return;
            Int32 source = DataSource.IndexOf(dst);
            Int32 target = e도구.IndexFromPoint(e도구.PointToClient(Control.MousePosition));
            target = CalOrder(source, target, e.InsertType);
            if (source == target) return;

            e도구.BeginUpdate();
            if (target <= 0 || target >= DataSource.Count)
            {
                DataSource.Remove(dst);
                if (target <= 0) DataSource.Insert(0, dst);
                else DataSource.Add(dst);
            }
            else
            {
                ICvTool tag = DataSource[target];
                DataSource.Remove(dst);
                target = DataSource.IndexOf(tag);
                DataSource.Insert(target, dst);
            }
            source = DataSource.IndexOf(dst);
            e도구.SelectedIndex = source;
            e도구.EndUpdate();
            e도구.Refresh();
        }

        private Int32 CalOrder(Int32 source, Int32 target, InsertType type)
        {
            if (target < 0) return DataSource.Count;
            if (target == 0) return 0;
            if (target >= DataSource.Count) return DataSource.Count;
            if (target >= DataSource.Count - 1 && type == InsertType.After) return DataSource.Count;
            if (type == InsertType.After) return target + 1;
            return target;
        }
        #endregion

        #region Button Events
        private void AddRemoveItem(object sender, ButtonPressedEventArgs e)
        {
            LookUpEdit editor = sender as LookUpEdit;
            if (editor == null) return;
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                Type type = editor.EditValue as Type;
                if (type == null) return;
                Int32 idx = Math.Max(0, e도구.SelectedIndex + 1);
                ICvTool item = Activator.CreateInstance(type) as ICvTool;
                if (idx >= DataSource.Count) DataSource.Add(item);
                else DataSource.Insert(idx, item);
                e도구.Refresh();
            }
            else if (e.Button.Kind == ButtonPredefines.Delete)
            {
                Int32 idx = e도구.SelectedIndex;
                ICvTool item = e도구.GetItem(idx) as ICvTool;
                if (item == null) return;
                DataSource.Remove(item);
                Int32 @new = Math.Max(0, Math.Min(idx - 1, DataSource.Count - 1));
                e도구.SelectedIndex = @new;
                e도구.Refresh();
            }
        }
        #endregion
    }
}
