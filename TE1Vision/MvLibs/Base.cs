using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.CNLSearch;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace MvLibs
{
    public class PointD
    {
        public Double X = Double.NaN;
        public Double Y = Double.NaN;
        [JsonIgnore]
        public Boolean IsEmpty => X == Double.NaN || Y == Double.NaN;
        [JsonIgnore]
        public Boolean IsInfinity => Double.IsInfinity(X) || Double.IsInfinity(Y);
        [JsonIgnore]
        internal Point2d Point2d => new Point2d(X, Y);
        [JsonIgnore]
        internal Point2f Point2f => new Point2f((Single)X, (Single)Y);

        public PointD() { }
        public PointD(Double x, Double y) { X = x; Y = y; }
        internal PointD(Point2d point) { X = point.X; Y = point.Y; }
        public override String ToString() => $"[X={X},Y={Y}]";
    }
    public class LineSegment
    {
        public PointD P1;
        public PointD P2;
        [JsonIgnore]
        public Boolean IsEmpty => P1.IsEmpty || P2.IsEmpty;
        public Double Rotation() => IsEmpty ? 0 : Base.GetRotation(P1, P2);
        public Double Distance() => IsEmpty ? 0 : Base.GetDistance(P1, P2);
        public LineSegment(PointD p1, PointD p2) { P1 = p1; P2 = p2; }
    }

    public static class Base
    {
        public const String SpaceRoot = "@";

        #region Common
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        public static Double ToRadian(Double degree) => degree / 180.0 * Math.PI;
        public static Double ToDegree(Double radian) => radian / Math.PI * 180.0;
        public static Double GetAngle(PointD start, PointD end) => GetAngle(start.X, start.Y, end.X, end.Y);
        public static Double GetAngle(Point2d start, Point2d end) => GetAngle(start.X, start.Y, end.X, end.Y);
        public static Double GetAngle(Double startX, Double startY, Double endX, Double endY) => ToDegree(GetRotation(startX, startY, endX, endY));
        public static Double GetRotation(PointD start, PointD end) => GetRotation(start.X, start.Y, end.X, end.Y);
        public static Double GetRotation(Point2d start, Point2d end) => GetRotation(start.X, start.Y, end.X, end.Y);
        public static Double GetRotation(Double startX, Double startY, Double endX, Double endY)
        {
            if (Double.IsNaN(startX) || Double.IsNaN(startY) || Double.IsNaN(endX) || Double.IsNaN(endY)) return Double.NaN;
            if (Double.IsInfinity(startX) || Double.IsInfinity(startY) || Double.IsInfinity(endX) || Double.IsInfinity(endY)) return 0;
            return Math.Atan2(endY - startY, endX - startX);
        }

        public static Double GetDistance(PointD start, PointD end) => GetDistance(start.X, start.Y, end.X, end.Y);
        public static Double GetDistance(Point2d start, Point2d end) => GetDistance(start.X, start.Y, end.X, end.Y);
        public static Double GetDistance(Double startX, Double startY, Double endX, Double endY)
        {
            if (Double.IsNaN(startX) || Double.IsNaN(startY) || Double.IsNaN(endX) || Double.IsNaN(endY)) return 0;
            if (Double.IsInfinity(startX) || Double.IsInfinity(startY) || Double.IsInfinity(endX) || Double.IsInfinity(endY)) return Double.PositiveInfinity;
            return Math.Sqrt(Math.Pow(endX - startX, 2) + Math.Pow(endY - startY, 2));
        }

        public static PointD MidPoint(PointD start, PointD end) => MidPoint(start.X, start.Y, end.X, end.Y);
        public static PointD MidPoint(Point2d start, Point2d end) => MidPoint(start.X, start.Y, end.X, end.Y);
        public static PointD MidPoint(Double startX, Double startY, Double endX, Double endY) =>
            new PointD((startX + endX) / 2, (startY + endY) / 2);

        public static Point2d RotatePoint(Point2d point, Point2d center, Double angle) => RotatePoint(new Point2d[] { point }, center, angle)[0];
        public static List<Point2d> RotatePoint(List<Point2d> points, Point2d center, Double angle) => RotatePoint(points.ToArray(), center, angle);
        public static List<Point2d> RotatePoint(Point2d[] points, Point2d center, Double angle)
        {
            using (Matrix matrix = new Matrix())
            {
                List<PointF> pointfs = new List<PointF>();
                foreach (Point2d p in points) pointfs.Add(new PointF(Convert.ToSingle(p.X), Convert.ToSingle(p.Y)));
                matrix.RotateAt((Single)angle, new PointF(Convert.ToSingle(center.X), Convert.ToSingle(center.Y)));
                matrix.TransformPoints(pointfs.ToArray());
                return points.ToList();
            }
        }

        public static Point2d CalculatePoint(Point2d start, Double distance, Double radian)
        {
            Double x = start.X + Math.Cos(radian) * distance;
            Double y = start.Y + Math.Sin(radian) * distance;
            return new Point2d(x, y);
        }

        public static PointD LineIntersection(LineSegment line1, LineSegment line2)
        {
            PointD point = new PointD();
            if (line1.IsEmpty || line2.IsEmpty) return point;
            Double x1 = line1.P1.X;
            Double y1 = line1.P1.Y;
            Double f1 = line1.P2.X - line1.P1.X;
            Double g1 = line1.P2.Y - line1.P1.Y;
            Double x2 = line2.P1.X;
            Double y2 = line2.P1.Y;
            Double f2 = line2.P2.X - line2.P1.X;
            Double g2 = line2.P2.Y - line2.P1.Y;
            Double det = f2 * g1 - f1 * g2;
            if (Math.Abs(det) < 1e-9) return point;

            Double dx = x2 - x1;
            Double dy = y2 - y1;
            Double t1 = (f2 * dy - g2 * dx) / det;
            point.X = x1 + (f1 * t1);
            point.Y = y1 + (g1 * t1);
            return point;
        }

        public static LineSegment LinearRegressionLine(List<PointD> points)
        {
            if (points == null || points.Count < 2) return null;
            // Calculate the means of X and Y
            Double meanX = points.Select(p => p.X).Average();
            Double meanY = points.Select(p => p.Y).Average();

            // Calculate the slope (m) and y-intercept (b) for the equation y = mx + b
            Double numerator = 0;
            Double denominator = 0;
            foreach (PointD point in points)
            {
                numerator += (point.X - meanX) * (point.Y - meanY);
                denominator += Math.Pow(point.X - meanX, 2);
            }

            Double slope = numerator / denominator;
            Double yIntercept = meanY - slope * meanX;

            // Calculate the start and end points
            Double startX = points.Min(p => p.X);
            Double endX = points.Max(p => p.X);
            PointD P1 = new PointD(startX, slope * startX + yIntercept);
            PointD P2 = new PointD(endX, slope * endX + yIntercept);
            return new LineSegment(P1, P2);
        }

        public static Mat WarpPerspectivePlane(Mat source, RectanglePoints region, Boolean crop)
        {
            Double l = Math.Min(region.LT.X, region.LB.X);
            Double t = Math.Min(region.LT.Y, region.RT.Y);
            Double r = Math.Max(region.RT.X, region.RB.X);
            Double b = Math.Max(region.LB.Y, region.RB.Y);
            Double w = Math.Abs(r - l);
            Double h = Math.Abs(b - t);
            RectanglePoints dest = new RectanglePoints(l, t, r, t, l, b, r, b);
            using (Mat matrix = Cv2.GetPerspectiveTransform(region.ToArray(), dest.ToArray()))
            {
                Mat aligned = source.WarpPerspective(matrix, new OpenCvSharp.Size(source.Width, source.Height));
                if (!crop) return aligned;
                Mat croped = new Mat(aligned, new Rect2d(l, t, w, h).ToRect());
                aligned.Dispose();
                return croped;
            }
        }

        public static JsonSerializerSettings JsonSetting(Boolean useIndented = true)
        {
            JsonSerializerSettings s = new JsonSerializerSettings();
            s.NullValueHandling = NullValueHandling.Ignore;
            s.DateParseHandling = DateParseHandling.DateTime;
            s.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            s.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            if (useIndented) s.Formatting = Formatting.Indented;
            return s;
        }

        public static Mat Resize(Mat source, Double scale) => Resize(source, scale, scale);
        public static Mat Resize(Mat source, Double scaleX, Double scaleY) =>
            source.Resize(new OpenCvSharp.Size(source.Cols * scaleX, source.Rows * scaleY));

        public static Scalar ToScalar(Color color) => Scalar.FromRgb(color.R, color.G, color.B);
        public static Mat CopyRectangle(Mat image, Rect region) => CopyRectangle(image, region, Scalar.Black);
        public static Mat CopyRectangle(Mat image, Rect region, Scalar background)
        {
            Rect source = region.Intersect(new Rect(0, 0, image.Width, image.Height));
            Rect target = new Rect(0, 0, source.Width, source.Height);
            if (region.X < 0) target.X -= region.X;
            if (region.Y < 0) target.Y -= region.Y;
            //Debug.WriteLine($"region={region}, source={source}, target={target}");
            Mat output = new Mat(region.Size, image.Type(), background);
            image[source].CopyTo(output[target]);
            return output;
        }
        #endregion

        #region Image Convert
        public static Mat ToMat(ICogImage image)
        {
            if (image == null) return null;
            if (image.GetType() == typeof(CogImage8Grey))
            {
                using (ICogImage8PixelMemory mem = (image as CogImage8Grey).Get8GreyPixelMemory(CogImageDataModeConstants.Read, 0, 0, image.Width, image.Height))
                    return new Mat(image.Height, image.Width, MatType.CV_8UC1, mem.Scan0);
            }
            else if (image.GetType() == typeof(CogImage24PlanarColor))
            {
                using (Bitmap bitmap = (image as CogImage24PlanarColor)?.ToBitmap())
                    return BitmapConverter.ToMat(bitmap);
            }
            return null;
        }
        public static Mat ToMat(Bitmap image)
        {
            if (image == null) return null;
            return new Mat(image.GetHbitmap());
        }
        public static ICogImage ToCogImage(Mat mat)
        {
            if (mat.Type() == MatType.CV_8UC3)
                return new CogImage24PlanarColor(mat.ToBitmap());
            return ToCogGray(mat);
        }
        public static CogImage8Grey ToCogGray(Mat mat)
        {
            CogImage8Grey image = new CogImage8Grey();
            using (CogImage8Root cogImage8Root = new CogImage8Root())
            {
                cogImage8Root.Initialize(mat.Width, mat.Height, mat.Ptr(0), mat.Width, null);
                image.SetRoot(cogImage8Root);
            }
            return image;
        }

        public static CogImage8Grey ToCogGray(IntPtr sufaceAddress, Int32 width, Int32 height)
        {
            CogImage8Grey image = new CogImage8Grey();
            using (CogImage8Root cogImage8Root = new CogImage8Root())
            {
                cogImage8Root.Initialize(width, height, sufaceAddress, width, null);
                image.SetRoot(cogImage8Root);
            }
            return image;
        }

        public static CogImage8Grey CopyToCogImage(Mat image)
        {
            Int32 bufferSize = image.Width * image.Height;
            IntPtr bufferAddress = Marshal.AllocHGlobal(bufferSize);
            CopyMemory(bufferAddress, image.Ptr(0), (UInt32)bufferSize);
            return ToCogGray(bufferAddress, image.Width, image.Height);
        }
        #endregion

        #region Cognex
        public static ICogTool GetTool(CogToolBlock block, String name)
        {
            if (block == null || !block.Tools.Contains(name)) return null;
            return block.Tools[name];
        }
        public static T Input<T>(CogToolBlock block, String name)
        {
            if (block == null || !block.Inputs.Contains(name)) return default(T);
            Object v = block.Inputs[name].Value;
            if (v == null) return default(T);
            return (T)v;
        }
        public static Boolean Input(CogToolBlock block, String name, Object value)
        {
            if (block == null || !block.Inputs.Contains(name)) return false;
            block.Inputs[name].Value = value;
            return true;
        }
        public static T Output<T>(CogToolBlock block, String name)
        {
            if (block == null || !block.Outputs.Contains(name)) return default(T);
            Object v = block.Outputs[name].Value;
            if (v == null) return default(T);
            return (T)v;
        }
        public static Boolean Output(CogToolBlock block, String name, Object value)
        {
            if (block == null || !block.Outputs.Contains(name)) return false;
            block.Outputs[name].Value = value;
            return true;
        }

        public static void InitOutputs(CogToolBlock block)
        {
            if (block == null) return;
            foreach (CogToolBlockTerminal item in block.Outputs)
            {
                if (item.Value == null) continue;
                if (item.ValueType == typeof(CogImage8Grey))
                {
                    //(item.Value as CogImage8Grey)?.Dispose();
                    continue;
                }
                item.Value = null;
            }
        }

        public static Boolean LoadTrainImage(CogCNLSearchTool tool, String file)
        {
            if (tool.Pattern.Trained) return true;
            if (!File.Exists(file)) return false;
            tool.Pattern.TrainImage = new CogImage8Grey(new Bitmap(file));
            tool.Pattern.TrainRegion = new CogRectangle() { X = 0, Y = 0, Width = tool.Pattern.TrainImage.Width, Height = tool.Pattern.TrainImage.Height };
            tool.Pattern.OriginX = tool.Pattern.TrainImage.Width / 2;
            tool.Pattern.OriginY = tool.Pattern.TrainImage.Height / 2;
            tool.Pattern.Train();
            return tool.Pattern.Trained;
        }

        public static Boolean LoadTrainImage(CogPMAlignTool tool, String file)
        {
            if (tool.Pattern.Trained) return true;
            if (!File.Exists(file)) return false;
            tool.Pattern.TrainImage = new CogImage8Grey(new Bitmap(file));
            tool.Pattern.TrainRegion = new CogRectangle() { X = 0, Y = 0, Width = tool.Pattern.TrainImage.Width, Height = tool.Pattern.TrainImage.Height };
            tool.Pattern.Train();
            return tool.Pattern.Trained;
        }

        public static Point2d PointTransform(ICogImage orignImage, String targetSpaceName, Point2d point) => PointTransform(orignImage, targetSpaceName, point.X, point.Y);
        public static Point2d PointTransform(ICogImage orignImage, String targetSpaceName, Double originX, Double originY)
        {
            Point2d p = new Point2d() { X = originX, Y = originY };
            if (orignImage != null)
            {
                try
                {
                    CogTransform2DLinear trans = orignImage.GetTransform(targetSpaceName, ".") as CogTransform2DLinear;
                    trans.MapPoint(originX, originY, out p.X, out p.Y);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{p.ToString()} {ex.Message}", "PointTransform");
                }
            }
            return p;
        }

        public static List<CogRectangleAffine> GetBoundingBoxs(CogBlobTool tool, CogBlobAxisConstants axis = CogBlobAxisConstants.Principal)
        {
            if (tool == null || tool.RunStatus.Result != CogToolResultConstants.Accept || tool.Results == null) return null;
            CogBlobResultCollection blobs = tool.Results.GetBlobs();
            List<CogRectangleAffine> boxs = new List<CogRectangleAffine>();
            foreach (CogBlobResult blob in blobs)
                boxs.Add(blob.GetBoundingBox(axis));
            return boxs;
        }

        public static CogRectangleAffine GetBoundingBox(CogBlobTool tool, CogBlobAxisConstants axis = CogBlobAxisConstants.Principal, Int32 index = 0)
        {
            if (tool == null || tool.RunStatus.Result != CogToolResultConstants.Accept || tool.Results == null) return null;
            CogBlobResultCollection blobs = tool.Results.GetBlobs();
            if (blobs.Count <= 0 || blobs.Count < index) return null;
            return blobs[index]?.GetBoundingBox(axis);
        }

        public static CogRectangleAffine GetBoundingBox(CogBlobResult result, CogBlobAxisConstants axis = CogBlobAxisConstants.Principal) =>
            result?.GetBoundingBox(axis);

        //public static CogRectangle GetBoundingBox(CogPolygon polygon) =>
        //    polygon?.EnclosingRectangle(CogCopyShapeConstants.GeometryOnly);
        //public static CogRectangle GetBoundingBox(CogIDResult result) =>
        //    GetBoundingBox(result?.BoundsPolygon);
        #endregion
    }
}
