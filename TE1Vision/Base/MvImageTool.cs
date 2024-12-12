using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.ToolBlock;
using MvLibs;
using MvLibs.Tools;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace TE1
{
    public class MvImageTool : ImageCorrectors
    {
        public MvImageTool(CogToolBlock toolBlock) { ToolBlock = toolBlock; }

        public readonly CogToolBlock ToolBlock = null;
        public CogImage8Grey InputImage => Base.Input<CogImage8Grey>(ToolBlock, "InputImage");
        public CogImage8Grey OutputImage { get => Base.Output<CogImage8Grey>(ToolBlock, "OutputImage"); set => Base.Output(ToolBlock, "OutputImage", value); }
        public String Config { get => Base.Input<String>(ToolBlock, "Config"); set => Base.Input(ToolBlock, "Config", value); }
        public DateTime Modified { get => Base.Input<DateTime>(ToolBlock, "Modified"); set => Base.Input(ToolBlock, "Modified", value); }
        public ICogRegion Region { get => Base.Input<ICogRegion>(ToolBlock, "Region"); set => Base.Input(ToolBlock, "Region", value); }
        public DateTime LoadTime = DateTime.MinValue;

        public virtual void Init()
        {
            Base.AddTerminal(ToolBlock.Inputs, "InputImage", typeof(CogImage8Grey));
            Base.AddTerminal(ToolBlock.Inputs, "Config", typeof(String));
            Base.AddTerminal(ToolBlock.Inputs, "Modified", typeof(DateTime), new DateTime(2000, 1, 1));
            Base.AddTerminal(ToolBlock.Inputs, "Region", typeof(ICogRegion));
            Base.AddTerminal(ToolBlock.Outputs, "OutputImage", typeof(CogImage8Grey));
        }

        public virtual Boolean Load()
        {
            if (LoadTime == Modified) return true;
            Boolean r = base.Load(Config);
            LoadTime = Modified;
            return r;
        }
        public virtual void Apply(String json)
        {
            Config = json;
            Modified = DateTime.Now;
        }

        public virtual void RunClean()
        {
            Run();
            Dispose();
        }

        public virtual Mat Run()
        {
            Load();
            Base.DisposeOutputImage(ToolBlock, "OutputImage");
            if (InputImage == null) return null;
            using (Bitmap input = InputImage.ToBitmap())
            {
                if (input == null) return null;
                using (Mat mat = BitmapConverter.ToMat(input))
                    base.Run(mat);
            }
            SetOutputImage();
            return ResultImage;
        }

        public Boolean GetRectangle(ICogImage source, out Rect2d rect)
        {
            if (InputImage == null || ResultImage == null || Region == null)
            {
                rect = new Rect2d();
                return false;
            }

            Double x, y;
            if (Region.GetType() == typeof(CogRectangle))
            {
                CogRectangle r = Region as CogRectangle;
                x = r.X;
                y = r.Y;
            }
            else if (Region.GetType() == typeof(CogRectangleAffine))
            {
                CogRectangleAffine r = Region as CogRectangleAffine;
                x = r.CenterX - r.SideXLength / 2; 
                y = r.CenterY - r.SideYLength / 2;
            }
            else
            {
                rect = new Rect2d();
                return false;
            }

            CogTransform2DLinear tr = source.GetTransform("#", ".") as CogTransform2DLinear;
            tr.MapPoint(x, y, out Double sx, out Double sy);
            rect = new Rect2d(sx, sy, ResultImage.Width, ResultImage.Height);
            return true;
        }

        public virtual void SetOutputImage()
        {
            if (ResultImage == null) return;
            using (Bitmap output = BitmapConverter.ToBitmap(ResultImage))
                OutputImage = new CogImage8Grey(output);
            SetCogSpace(OutputImage, InputImage);
        }

        public static void SetCogSpace(CogImage8Grey dst, CogImage8Grey org)
        {
            dst.PixelFromRootTransform = org.PixelFromRootTransform;
            dst.CoordinateSpaceTree = org.CoordinateSpaceTree;
            dst.SelectedSpaceName = org.SelectedSpaceName;
        }

        public static (List<Double[]> regions, Double area) ToArrayRegions(CogBlobTool tool)
        {
            List<Double[]> regions = new List<Double[]>();
            Double area = 0;
            if (tool.RunStatus.Result != CogToolResultConstants.Accept || tool.Results == null) return (regions, area);

            Double V(Double val, Int32 round = 1) => Math.Round(val, round);
            CogBlobResultCollection blobs = tool.Results.GetBlobs();
            if (blobs.Count < 1) return (regions, area);

            List<RotatedRect> rects = new List<RotatedRect>();
            foreach (CogBlobResult blob in blobs)
            {
                CogRectangleAffine r = blob.GetBoundingBox(CogBlobAxisConstants.Principal);
                regions.Add(new Double[] { V(r.CenterX), V(r.CenterY), V(r.SideXLength), V(r.SideYLength), V(Base.ToDegree(r.Rotation)), V(blob.Area) });
                area += blob.Area;
            }
            return (regions, Math.Round(area, 3));
        }

        public static List<CogRectangleAffine> ToCoRegions(List<Double[]> defects)
        {
            List<CogRectangleAffine> regions = new List<CogRectangleAffine>();
            foreach (Double[] d in defects)
            {
                CogRectangleAffine r = new CogRectangleAffine() { CenterX = d[0], CenterY = d[1], SideXLength = d[2] + 1, SideYLength = d[3] + 1, Rotation = Base.ToRadian(d[4]) };
                regions.Add(r);
            }
            return regions;
        }
    }
}
