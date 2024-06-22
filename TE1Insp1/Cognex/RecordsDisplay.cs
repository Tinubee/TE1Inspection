using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Cogutils
{
    public partial class RecordsDisplay : XtraUserControl
    {
        public static Color 배경색상 = DevExpress.LookAndFeel.DXSkinColors.IconColors.Black;

        public RecordsDisplay()
        {
            InitializeComponent();
        }

        public CogRecordsDisplay CogRecordsDisplay => this.RecordsDisplay1;
        public CogDisplay CogDisplay => this.RecordsDisplay1.Display;
        public CogImage8Grey Image { get => this.CogDisplay.Image as CogImage8Grey; set => this.CogDisplay.Image = value; }

        public void Init(Boolean ShowBars = true)
        {
            if (ShowBars)
            {
                this.DisplayToolBar1.Init(this.CogDisplay);
                this.DisplayStatusBar1.Init(this.CogDisplay);
            }
            else
            {
                this.CogRecordsDisplay.ShowRecordsDropDown = false;
                this.panelControl1.Hide();
            }

            this.SetBackground();
            this.CogDisplay.AutoFit = true;
            this.CogDisplay.MouseMode = CogDisplayMouseModeConstants.Pointer;
        }

        public void Close()
        {
            this.DisplayStatusBar1.Dispose();
            this.DisplayToolBar1.Dispose();
            this.RecordsDisplay1.Dispose();
            this.CogDisplay.Dispose();
        }

        public void SetBackground() =>this.CogDisplay.BackColor = 배경색상;

        public static void DebugRecords(ICogRecord record, Int32 depth = 0, String preKey = "")
        {
            if (record == null) return;
            if (String.IsNullOrEmpty(preKey)) preKey = record.RecordKey;
            else preKey = $"{preKey}.{record.RecordKey}";
            if (record.SubRecords.Count < 1)
            {
                Debug.WriteLine($"[{record.Annotation}] [{record.RecordUsage}] [{record.ContentType.ToString()}] [{record.ContentMutable}]", preKey);
                //if (record.ContentType == typeof(ICogGraphicInteractive))
                //{
                //    ICogGraphicInteractive g = record.Content as ICogGraphicInteractive;
                //    Debug.WriteLine($"\t{g.TipText}: {String.Join(", ", g.StateFlags.Names)}");
                //}
            }
            else
            {
                foreach (ICogRecord r in record.SubRecords)
                    DebugRecords(r, depth, preKey);
                depth++;
            }
        }

        public void ViewResultImage(ICogImage Image, ICogRecord LastRunRecords, String SelectedRecordKey)
        {
            if (this.CogRecordsDisplay.InvokeRequired)
            {
                this.CogRecordsDisplay.BeginInvoke(new Action(() => this.ViewResultImage(Image, LastRunRecords, SelectedRecordKey)));
                return;
            }
            this.CogRecordsDisplay.Subject = LastRunRecords;
            if (!String.IsNullOrEmpty(SelectedRecordKey))
                this.CogRecordsDisplay.SelectedRecordKey = $"LastRun.{SelectedRecordKey}";
            this.CogDisplay.Image = Image;
            this.CogDisplay.Fit(false);
            this.SetBackground();
        }
    }
}
