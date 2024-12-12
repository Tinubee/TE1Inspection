using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.ToolBlock;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace TE1
{
    public class DefaultDefectsTools : BaseTool
    {
        public DefaultDefectsTools(CogToolBlock tool) : base(tool) { Init(); }

        internal String DefectsRegion { get => Output<String>(DefectsJson); set => Output(DefectsJson, value); }
        internal Double DefectsResult { get => Output<Double>(DefectsArea); set => Output(DefectsArea, value); }
        internal CogMaskCreatorTool Mask => GetTool("Mask") as CogMaskCreatorTool;
        internal CogBlobTool Detect => GetTool("Detect") as CogBlobTool;
        internal List<MvImageTool> Tools = new List<MvImageTool>();

        public override void BeforeToolRun(ICogTool tool)
        {
            base.BeforeToolRun(tool);
            ImagingRun(tool);
        }

        internal virtual void Init()
        {
            foreach (ICogTool tool in ToolBlock.Tools)
            {
                if (!tool.Name.StartsWith("Defects") && tool.GetType() != typeof(CogToolBlock)) continue;
                MvImageTool imaging = new MvImageTool(tool as CogToolBlock);
                Tools.Add(imaging);
                imaging.Init();
            }
        }

        internal virtual void ImagingRun(ICogTool tool)
        {
            if (!tool.Name.StartsWith("Defects") && tool.GetType() != typeof(CogToolBlock)) return;
            if (ToolBlock.DisabledTools.Contains(tool)) return;
            MvImageTool imaging = Tools.Where(e => e.ToolBlock.Name == tool.Name).FirstOrDefault();
            if (imaging == null) return;
            if (imaging.InputImage == null) return;
            imaging.Run();
        }

        internal virtual void SetMasks(Mat mat, Scalar color)
        {
            if (mat == null || Mask == null || Mask.RunParams.MaskAreas.Count < 1) return;

            Double x, y, w, h;
            CogImage8Grey img = Mask.InputImage as CogImage8Grey;
            CogTransform2DLinear tr = img.GetTransform("#", ".") as CogTransform2DLinear;
            //Debug.WriteLine($"{tr.ScalingX}, {tr.ScalingY}", "Scale");
            Double scaleX = tr.ScalingX;
            Double scaleY = tr.ScalingY;
            foreach (CogMaskCreatorRegion m in Mask.RunParams.MaskAreas)
            {
                if (!m.Enabled) continue;
                ICogRegion region = m.Region;
                if (region.GetType() == typeof(CogRectangle))
                {
                    CogRectangle r = region as CogRectangle;
                    tr.MapPoint(r.X, r.Y, out x, out y);
                    w = scaleX * r.Width;
                    h = scaleY * r.Height;
                    Rect2d rd = new Rect2d(x, y, w, h);
                    //Debug.WriteLine($"{r.X}, {r.Y}, {r.Width}, {r.Height} => {rd.X}, {rd.Y}, {rd.Width}, {rd.Height}");
                    mat.Rectangle(rd.ToRect(), color, -1);
                }
                else if (region.GetType() == typeof(CogCircle))
                {
                    CogCircle r = region as CogCircle;
                    tr.MapPoint(r.CenterX, r.CenterY, out x, out y);
                    mat.Circle(new Point2d(x, y).ToPoint(), (Int32)(scaleX * r.Radius), color, -1);
                }
            }
        }

        internal virtual void DefectsRun(CogImage8Grey source)
        {
            if (source == null) return;
            using (Mat mat = new Mat(source.Height, source.Width, MatType.CV_8UC1, Scalar.Black))
            {
                foreach (MvImageTool imaging in Tools)
                {
                    if (!imaging.GetRectangle(source, out Rect2d rd)) continue;
                    Rect r = rd.ToRect();
                    Debug.WriteLine($"{r.X}, {r.Y}, {r.Width}, {r.Height}", imaging.ToolBlock.Name);
                    mat[r] = imaging.ResultImage;
                }
                SetMasks(mat, Scalar.All(128));

                using (Bitmap bitamp = BitmapConverter.ToBitmap(mat))
                {
                    CogImage8Grey image = new CogImage8Grey(bitamp);
                    MvImageTool.SetCogSpace(image, source);
                    (Detect.InputImage as CogImage8Grey)?.Dispose();
                    Detect.InputImage = image;
                }
            }
            Detect.Run();
            SetResults();
        }

        internal virtual void SetResults()
        {
            var r = MvImageTool.ToArrayRegions(Detect);
            DefectsRegion = JsonConvert.SerializeObject(r.regions);
            DefectsResult = Math.Round(r.area / 1000, 3);
        }
    }
}
