using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.CNLSearch;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.ToolBlock;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Windows.Media.Media3D;

namespace MvLibs
{
    public class PointD
    {
        public Double X { get; set; } = Double.NaN;
        public Double Y { get; set; } = Double.NaN;
        [JsonIgnore] public Boolean IsEmpty => Double.IsNaN(X) || Double.IsNaN(Y);
        //[JsonIgnore] public Boolean IsInfinity => Double.IsInfinity(X) || Double.IsInfinity(Y);
        [JsonIgnore] internal Point2d Point2d => new Point2d(X, Y);
        [JsonIgnore] internal Point2f Point2f => new Point2f((Single)X, (Single)Y);

        public PointD() { }
        public PointD(Double x, Double y) { X = x; Y = y; }
        internal PointD(Point2d point) { X = point.X; Y = point.Y; }
        public override String ToString() => $"(X={X},Y={Y})";
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
        public static JsonSerializerSettings JsonSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };

        #region Common
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, UInt32 count);

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
        public static Point2d CalculatePoint(Point2d start, Double distance, Double radian)
        {
            Double x = start.X + Math.Cos(radian) * distance;
            Double y = start.Y + Math.Sin(radian) * distance;
            return new Point2d(x, y);
        }
        public static PointD MidPoint(PointD start, PointD end) => MidPoint(start.X, start.Y, end.X, end.Y);
        public static PointD MidPoint(Point2d start, Point2d end) => MidPoint(start.X, start.Y, end.X, end.Y);
        public static PointD MidPoint(Double startX, Double startY, Double endX, Double endY) =>
            new PointD((startX + endX) / 2, (startY + endY) / 2);

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

        public static Scalar Scalar(System.Drawing.Color color) => OpenCvSharp.Scalar.FromRgb(color.R, color.G, color.B);
        public static Mat Resize(Mat mat, Double scale) => Resize(mat, scale, scale);
        public static Mat Resize(Mat mat, Double scaleX, Double scaleY) =>
            mat.Resize(new Size(), scaleX, scaleY, InterpolationFlags.Linear);

        public static Mat Copy(Mat mat, Rect region, System.Drawing.Color background, Single scale = 1) => Copy(mat, region, Scalar(background), scale);
        public static Mat Copy(Mat mat, Rect region, Scalar background, Single scale = 1)
        {
            Rect source = region.Intersect(new Rect(0, 0, mat.Width, mat.Height));
            if (source.Width <= 0 || source.Height <= 0) return null;
            Rect target = new Rect(0, 0, source.Width, source.Height);
            if (region.X < 0) target.X -= region.X;
            if (region.Y < 0) target.Y -= region.Y;
            //Debug.WriteLine($"{source.X}, {source.Y}, {source.Width}, {source.Height}", "source");
            //Debug.WriteLine($"{target.X}, {target.Y}, {target.Width}, {target.Height}", "target");
            Mat output = new Mat(region.Size, mat.Type(), background);
            mat[source].CopyTo(output[target]);
            if (scale == 1.0f) return output;
            Mat resized = Resize(output, scale);
            output.Dispose();
            return resized;
        }

        public static Mat Copy(Mat source, RotatedRect region, System.Drawing.Color background, Single scale = 1) => Copy(source, region, Scalar(background), scale);
        public static Mat Copy(Mat source, RotatedRect region, Scalar background, Single scale = 1)
        {
            Rect rect = BoundingRect(region);
            Single angle = region.Angle % 360.0f;
            if (angle == 0.0f) return Copy(source, rect, background, scale);

            Point2f center = new Point2f(rect.Width / 2, rect.Height / 2);
            using (Mat copyed = Copy(source, rect, background))
            using (Mat matrix = Cv2.GetRotationMatrix2D(center, angle, scale))
            using (Mat affine = copyed.WarpAffine(matrix, rect.Size, InterpolationFlags.Linear, BorderTypes.Constant, background))
            {
                Single cx = affine.Cols / 2.0f;
                Single cy = affine.Rows / 2.0f;
                Single w = region.Size.Width * scale;
                Single h = region.Size.Height * scale;
                Single x = cx - w / 2.0f;
                Single y = cy - h / 2.0f;
                Point p = new Point2d(x, y).ToPoint();
                Size s = new Size2d(w, h).ToSize();
                return new Mat(affine, new Rect(p, s));
            }
        }

        public static Rect BoundingRect(RotatedRect rect)
        {
            Point2f[] pt = rect.Points();
            Int32 x = (Int32)Math.Floor(pt.Min(p => p.X));
            Int32 y = (Int32)Math.Floor(pt.Min(p => p.Y));
            Int32 w = (Int32)Math.Ceiling(pt.Max(p => p.X) - x);
            Int32 h = (Int32)Math.Ceiling(pt.Max(p => p.Y) - y);
            return new Rect(x, y, w, h);
        }

        public static T GetAttribute<T>(Enum @enum)
        {
            if (@enum == null) return default(T);
            try
            {
                Type type = @enum.GetType();
                return (T)type.GetField(type.GetEnumName(@enum)).GetCustomAttributes(typeof(T), true).FirstOrDefault();
            }
            catch (Exception ex) { Debug.WriteLine($"[{@enum.GetType()}] {ex.Message}", "GetAttribute"); return default(T); }
        }
        #endregion

        #region Cognex
        public static ICogTool GetTool(CogToolBlock block, String name)
        {
            if (block == null || !block.Tools.Contains(name)) return null;
            return block.Tools[name];
        }

        public static CogToolBlockTerminal AddTerminal(CogToolBlockTerminalCollection terminals, String name, Type type, Object value = null)
        {
            if (terminals.Contains(name)) return terminals[name];
            CogToolBlockTerminal terminal = new CogToolBlockTerminal(name, type);
            if (value != null) terminal.Value = value;
            terminals.Add(terminal);
            return terminal;
        }

        public static CogToolBlockTerminal GetTerminal(CogToolBlockTerminalCollection terminals, String name)
        {
            if (terminals == null || !terminals.Contains(name)) return null;
            return terminals[name];
        }
        public static Boolean SetTerminalValue(CogToolBlockTerminalCollection terminals, String name, Object value)
        {
            CogToolBlockTerminal terminal = GetTerminal(terminals, name);
            if (terminal == null) return false;
            terminal.Value = value;
            return true;
        }
        public static T GetTerminalValue<T>(CogToolBlockTerminalCollection terminals, String name)
        {
            CogToolBlockTerminal terminal = GetTerminal(terminals, name);
            if (terminal == null) return default(T);
            return (T)terminal.Value;
        }
        public static T Input<T>(CogToolBlock block, String name) => GetTerminalValue<T>(block?.Inputs, name);
        public static Boolean Input(CogToolBlock block, String name, Object value) => SetTerminalValue(block?.Inputs, name, value);
        public static T Output<T>(CogToolBlock block, String name) => GetTerminalValue<T>(block?.Outputs, name);
        public static Boolean Output(CogToolBlock block, String name, Object value) => SetTerminalValue(block?.Outputs, name, value);

        public static Boolean DisposeImage(ICogImage image)
        {
            if (image == null) return false;
            if (image.GetType() == typeof(CogImage8Grey)) (image as CogImage8Grey).Dispose();
            else if (image.GetType() == typeof(CogImage24PlanarColor)) (image as CogImage24PlanarColor).Dispose();
            image = null;
            return true;
        }

        public static Boolean DisposeImage(CogToolBlockTerminal terminal)
        {
            if (terminal == null) return false;
            if (terminal.ValueType != typeof(CogImage8Grey) && terminal.ValueType != typeof(CogImage24PlanarColor)) return false;
            if (terminal.Value == null) return true;
            if (terminal.ValueType == typeof(CogImage8Grey))
                (terminal.Value as CogImage8Grey)?.Dispose();
            else if (terminal.ValueType == typeof(CogImage24PlanarColor))
                (terminal.Value as CogImage24PlanarColor)?.Dispose();
            terminal.Value = null;
            return true;
        }

        public static Boolean DisposeInputImage(CogToolBlock block, String name) => DisposeImage(GetTerminal(block?.Inputs, name));
        public static Boolean DisposeOutputImage(CogToolBlock block, String name) => DisposeImage(GetTerminal(block?.Outputs, name));

        public static void InitOutputs(CogToolBlock block)
        {
            if (block == null) return;
            foreach (CogToolBlockTerminal item in block.Outputs)
            {
                if (item.Value == null) continue;
                if (item.ValueType == typeof(CogImage8Grey) || item.ValueType == typeof(CogImage24PlanarColor))
                {
                    //DisposeImage(item);
                    continue;
                }
                item.Value = null;
            }
        }

        //public static String InputPath(CogToolBlock block, String name)
        //{
        //    if (block == null || !block.Inputs.Contains(name)) return null;
        //    CogToolBlockTerminal terminal = block.Inputs[name];
        //    return $"Inputs.Item[\"{terminal.ID}\"].Value.({terminal.ValueType})";
        //}
        //public static String OutputPath(CogToolBlock block, String name)
        //{
        //    if (block == null || !block.Outputs.Contains(name)) return null;
        //    CogToolBlockTerminal terminal = block.Outputs[name];
        //    return $"Outputs.Item[\"{terminal.ID}\"].Value.({terminal.ValueType})";
        //}

        public static Boolean LoadTrainImage(CogPMAlignTool tool, String file)
        {
            if (tool.Pattern.Trained) return true;
            if (!File.Exists(file)) return false;
            tool.Pattern.TrainImage = new CogImage8Grey(new System.Drawing.Bitmap(file));
            tool.Pattern.TrainRegion = new CogRectangle() { X = 0, Y = 0, Width = tool.Pattern.TrainImage.Width, Height = tool.Pattern.TrainImage.Height };
            //tool.Pattern.OriginX = tool.Pattern.TrainImage.Width / 2;
            //tool.Pattern.OriginY = tool.Pattern.TrainImage.Height / 2;
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

        public static ICogImage8PixelMemory[] GetChannels(ICogImage image)
        {
            if (image == null) return null;
            if (image.GetType() == typeof(CogImage8Grey))
                return new ICogImage8PixelMemory[] { (image as CogImage8Grey).Get8GreyPixelMemory(CogImageDataModeConstants.Read, 0, 0, image.Width, image.Height) };
            if (image.GetType() == typeof(CogImage24PlanarColor))
            {
                ICogImage8PixelMemory[] c = new ICogImage8PixelMemory[3];
                (image as CogImage24PlanarColor).Get24PlanarColorPixelMemory(CogImageDataModeConstants.Read, 0, 0, image.Width, image.Height, out c[0], out c[1], out c[2]);
                return c;
            }
            return null;
        }

        public static Mat ToMat(ICogImage image)
        {
            ICogImage8PixelMemory[] channels = GetChannels(image);
            if (channels == null || channels.Length < 1) return null;
            Mat mat = null;

            if (channels.Length == 1)
                mat = Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[0].Scan0).Clone();
            else
            {
                mat = new Mat(image.Height, image.Width, MatType.CV_8UC3);
                Mat[] mats = new Mat[] {
                    Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[2].Scan0).Clone(), // B
                    Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[1].Scan0).Clone(), // G
                    Mat.FromPixelData(image.Height, image.Width, MatType.CV_8UC1, channels[0].Scan0).Clone(), // R
                };
                Cv2.Merge(mats, mat);
                foreach (var m in mats)
                    m.Dispose();
            }
            foreach (var m in channels)
                m.Dispose();
            return mat;
        }

        //public static IntPtr CopyMemory(ICogImage8PixelMemory channel)
        //{
        //    Int32 imageSize = channel.Width * channel.Height * channel.Stride;
        //    IntPtr ptr = Marshal.AllocHGlobal(imageSize);
        //    CopyMemory(ptr, channel.Scan0, (UInt32)imageSize);
        //    return ptr;
        //}
        //public static Mat ToMat(CogImage8Grey image)
        //{
        //    if (image == null) return null;
        //    using (System.Drawing.Bitmap bitmap = image.ToBitmap())
        //    using (Mat color = BitmapConverter.ToMat(bitmap))
        //        return color.CvtColor(ColorConversionCodes.BGR2GRAY);
        //}

        public static ICogImage ToCogImage(Mat mat)
        {
            if (mat == null) return null;
            using(System.Drawing.Bitmap bitmap = mat.ToBitmap())
            {
                if (mat.Type() == MatType.CV_8UC3) return new CogImage24PlanarColor(bitmap);
                else return new CogImage8Grey(bitmap);
            }
        }
        #endregion
    }
}
