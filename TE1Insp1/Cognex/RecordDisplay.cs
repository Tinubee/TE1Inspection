using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace Cogutils
{
    public class RecordDisplay : CogRecordDisplay
    {
        public static Color 배경색상 = DevExpress.LookAndFeel.DXSkinColors.IconColors.Black;

        public void Init(Boolean showScrollBar = true)
        {
            this.AutoFit = true;
            this.HorizontalScrollBar = showScrollBar;
            this.VerticalScrollBar = showScrollBar;
            this.BackColor = 배경색상;
            this.MouseMode = CogDisplayMouseModeConstants.Pan;
        }

        public void SetImage(ICogImage image, ICogRecord record, List<ICogGraphic> graphics)
        {
            if (image == null || !image.Allocated) return;
            if (this.InvokeRequired) { this.BeginInvoke(new Action(() => { SetImage(image, record, graphics); })); return; }

            this.Image = null;
            this.InteractiveGraphics.Clear();
            this.StaticGraphics.Clear();
            this.Image = image;
            this.Record = record;
            if (graphics != null && graphics.Count > 0)
            {
                foreach (ICogGraphic graphic in graphics)
                    this.StaticGraphics.Add(graphic, "Results");
            }
            this.SetBackground();
            //this.Fit(true);
        }

        public void SetBackground() => this.BackColor = 배경색상;

        public void SaveDisplayImage(String file) => SaveDisplayImage(this, file);
        public static void SaveDisplayImage(CogRecordDisplay display, String file)
        {
            if (display == null) return;
            try { display.CreateContentBitmap(CogDisplayContentBitmapConstants.Custom).Save(file, ImageFormat.Jpeg); }
            catch (Exception e) { Debug.WriteLine(e.Message, "결과 이미지 저장 오류"); }
        }
    }
}
