using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace MvLibs.Tools
{
    public class ImageCorrectors : ICvTool
    {
        public event EventHandler<NameValueCollection> RuntimeMessage;
        public List<ICvTool> Tools = new List<ICvTool>();
        public Mat ResultImage = null;

        private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings { };//Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.All
        public static List<ToolListInfo> GetItemList()
        {
            return new List<ToolListInfo>() {
                // Correct
                new ToolListInfo(typeof(Morphology)),
                new ToolListInfo(typeof(Histogram)),
                new ToolListInfo(typeof(Normalize)),
                new ToolListInfo(typeof(EqualizeHistory)),
                new ToolListInfo(typeof(Threshold)),
                new ToolListInfo(typeof(Inverts)),
                new ToolListInfo(typeof(FloodFill)),
                new ToolListInfo(typeof(SetConstant)),
                new ToolListInfo(typeof(AddConstant)),
                new ToolListInfo(typeof(MultiplyConstant)),
                new ToolListInfo(typeof(GammaCorrect)),

                // Blurs, Sharpen & Filters
                new ToolListInfo(typeof(BlurFilters)),
                new ToolListInfo(typeof(Bilateral)),
                new ToolListInfo(typeof(UnsharpMask)),
                new ToolListInfo(typeof(Laplacian)),
                new ToolListInfo(typeof(KernelSharpen)),
                new ToolListInfo(typeof(Differential)),
                new ToolListInfo(typeof(MeansDenoising)),

                // Detector
                new ToolListInfo(typeof(CanyEdge)),
                new ToolListInfo(typeof(LineDetect)),
                //new ToolListInfo(typeof(CornerDetect)),
                new ToolListInfo(typeof(BlackAndWhite)),

                // Scales
                new ToolListInfo(typeof(Resize)),
                new ToolListInfo(typeof(Copy)),
                new ToolListInfo(typeof(Convertor)),

                // Blob
                new ToolListInfo(typeof(Blob)),
            };
        }

        public virtual Boolean Load(String json)
        {
            if (String.IsNullOrEmpty(json)) return false;
            try
            {
                JArray array = JsonConvert.DeserializeObject<JArray>(json);
                if (array == null || array.Count < 1) return false;
                List<ICvTool> items = LoadItems(array);
                Tools.Clear();
                Tools.AddRange(items);
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message, "Load"); }
            return false;
        }
        private List<ICvTool> LoadItems(JArray items)
        {
            List<ICvTool> tools = new List<ICvTool>();
            foreach (JObject item in items)
            {
                String typeName = item.GetValue("ToolType").Value<String>();
                if (String.IsNullOrEmpty(typeName)) continue;
                Type tp = Type.GetType(typeName, false);
                if (tp == null) continue;
                var tool = item.ToObject(tp);
                tools.Add(tool as ICvTool);
            }
            return tools;
        }

        public virtual String ToJson() => JsonConvert.SerializeObject(Tools, JsonSettings);

        public override void Dispose()
        {
            base.Dispose();
            foreach (var tool in Tools)
                tool.Dispose();
            ResultImage?.Dispose();
            ResultImage = null;
        }

        public void Add(ICvTool tool) => Tools.Add(tool);

        private NameValueCollection Messages = new NameValueCollection();
        private Mat Convert(Mat mat)
        {
            try
            {
                if (mat.Type() != MatType.CV_8UC1)
                    return mat.CvtColor(ColorConversionCodes.BGR2GRAY);
            }
            catch (Exception ex) { Messages.Add("Convert", ex.Message);  }
            return mat;
        }

        public override Mat Run(Mat mat)
        {
            BehaviorTime = 0;
            DateTime start = DateTime.Now;
            Dispose();
            if (mat == null || Tools.Count < 1) return mat;

            Messages.Clear();
            ResultImage = Convert(mat);
            foreach (ICvTool tool in Tools)
            {
                if (!tool.Enabled) continue;
                if (mat == null || mat.IsDisposed)
                {
                    Messages.Add($"{tool.Name}({tool.ID})", "The image has been disposed.");
                    continue;
                }
                try { ResultImage = tool.Run(ResultImage); }
                catch (Exception ex) { Messages.Add(tool.Name, ex.Message); }
            }
            BehaviorTime = Math.Round((DateTime.Now - start).TotalMilliseconds, 3);
            if (Messages.Count > 0) RuntimeMessage?.Invoke(this, Messages);
            return ResultImage;
        }
        internal override Mat Process(Mat mat) => null;
        public System.Drawing.Bitmap ToBitmap()
        {
            if (ResultImage == null) return null;
            return BitmapConverter.ToBitmap(ResultImage);
        }

        public ICvTool GetToolByID(String id) => Tools.Where(e => e.ID == id).FirstOrDefault();
        public ICvTool GetToolByName(String name) => Tools.Where(e => e.Name == name).FirstOrDefault();
        public ICvTool GetToolByType(Type type) => Tools.Where(e => e.GetType().Equals(type)).FirstOrDefault();

        public virtual void DegugBehaviorTime()
        {
            foreach (ICvTool tool in Tools)
            {
                if (!tool.Enabled) continue;
                Debug.WriteLine(tool.BehaviorTime.ToString("0.00"), tool.Name);
            }
            Debug.WriteLine(BehaviorTime.ToString("0.00"), "Total");
        }
    }

    #region Correct
    public enum EqualizeHistoryType { Normal = 0, CLAHE = 1 }
    public class EqualizeHistory : ICvTool
    {
        [Category(Categorys.Options)] public EqualizeHistoryType Type { get; set; } = EqualizeHistoryType.Normal;
        [Category(Categorys.CLAHE)] public Double ClipLimit { get; set; } = 1;
        [Category(Categorys.CLAHE), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public BlockSize TileSize { get; set; } = new BlockSize(64) { MinValue = 1, ValueType = BlockSizeTypes.All };
        internal override Mat Process(Mat mat)
        {
            if (Type == EqualizeHistoryType.Normal) return mat.EqualizeHist();
            Mat result = new Mat();
            CLAHE.Create(ClipLimit, TileSize.Size).Apply(mat, result);
            return result;
        }
    }

    public class Histogram : ICvTool
    {
        [Category(Categorys.Options)] public Int32 Channel { get; set; } = 0;
        [Category(Categorys.Options)] public Int32 Size { get; set; } = 256;
        [Category(Categorys.Options)] public Int32 RangeMin { get; set; } = 0;
        [Category(Categorys.Options)] public Int32 RangeMax { get; set; } = 256;

        [Category(Categorys.Results), JsonIgnore] public Double Min { get; internal set; } = 0;
        [Category(Categorys.Results), JsonIgnore] public Double Max { get; internal set; } = 0;
        [Category(Categorys.Results), JsonIgnore] public Double Mean { get; internal set; } = 0;
        [Category(Categorys.Results), JsonIgnore] public Double StandardDeviation { get; internal set; } = 0;
        [Category(Categorys.Results), JsonIgnore, TypeConverter(typeof(ExpandableObjectConverter))]
        public List<HistogramResult> Results { get; internal set; } = new List<HistogramResult>();

        public override void Dispose()
        {
            base.Dispose();
            Results.Clear();
            Min = Max = Mean = StandardDeviation = 0;
        }

        internal override Mat Process(Mat mat)
        {
            Mat hist = new Mat();
            Cv2.CalcHist(new Mat[] { mat }, new Int32[] { Channel }, null, hist, 1, new Int32[] { Size }, new Rangef[] { new Rangef(RangeMin, RangeMax) });

            Cv2.MinMaxLoc(hist, out Double min, out Double max);
            hist *= Size / max;
            Cv2.MinMaxLoc(hist, out min, out max);
            Cv2.MeanStdDev(hist, out Scalar avg, out Scalar std, null);
            for (Int32 i = 0; i < Size; i++)
                Results.Add(new HistogramResult(i, (Single)Math.Round(hist.Get<Single>(i), 3)));
            Min = min; Max = max; Mean = Math.Round(avg.Val0, 3); StandardDeviation = Math.Round(std.Val0, 3);
            Debug.WriteLine($"Min={Min}, Max={Max}, Mean={Mean}, StandardDeviation={StandardDeviation}");
            return mat;
        }
    }
    public class HistogramResult
    {
        public Int32 X { get; set; } = 0;
        public Single Y { get; set; } = 0;
        public HistogramResult() { }
        public HistogramResult(Int32 x, Single y) { X = x; Y = y; }
    }

    public class Normalize : ICvTool
    {
        [Category(Categorys.Options)] public NormTypes Type { get; set; } = NormTypes.MinMax;
        [Category(Categorys.Options)] public Double Min { get; set; } = 0;
        [Category(Categorys.Options)] public Double Max { get; set; } = 0;

        internal override Mat Process(Mat mat)
        {
            return mat.Normalize(Min, Max, NormTypes.MinMax);
        }
    }

    public class Morphology : ICvTool
    {
        [Category(Categorys.Options)] public MorphTypes Type { get; set; } = MorphTypes.Erode;
        [Category(Categorys.Options)] public MorphShapes Shape { get; set; } = MorphShapes.Rect;
        [Category(Categorys.Options)] public Int32 Iterations { get; set; } = 1;
        [Category(Categorys.Kernel)] public BlockSize KernelSize { get; set; } = new BlockSize(3) { MinValue = 1, ValueType = BlockSizeTypes.Odd };
        internal override Mat Process(Mat mat)
        {
            using (Mat kernel = Cv2.GetStructuringElement(Shape, KernelSize.Size))
                return mat.MorphologyEx(Type, kernel, null, Iterations);
        }
    }

    public enum ThresholdMethodTypes { Fixed = 0, Adaptive = 1 }
    public enum ThresholdBinaryTypes { Binary = 0, BinaryInvert = 1 }
    public class Threshold : ICvTool
    {
        [Category(Categorys.Options)] public ThresholdMethodTypes MethodType { get; set; } = ThresholdMethodTypes.Fixed;
        [Category(Categorys.Options)] public ThresholdBinaryTypes BinaryType { get; set; } = ThresholdBinaryTypes.Binary;
        [Category(Categorys.Options)] public ThresholdTypes ThresholdType { get; set; } = ThresholdTypes.Binary;
        [Category(Categorys.Options)] public Double Thresholds { get; set; } = 127;
        [Category(Categorys.Options)] public Double MaxValue { get; set; } = 255;

        [Category(Categorys.Adaptive)] public AdaptiveThresholdTypes AdaptiveAlgorithm { get; set; } = AdaptiveThresholdTypes.MeanC;
        [Category(Categorys.Adaptive)] public UInt16 BlockSize { get; set; } = 3;
        [Category(Categorys.Adaptive)] public Double Constant { get; set; } = 0;

        internal override Mat Process(Mat mat)
        {
            if (MethodType == ThresholdMethodTypes.Fixed)
                return mat.Threshold(Thresholds, MaxValue, GetThresholdType());
            return mat.AdaptiveThreshold(MaxValue, AdaptiveAlgorithm, GetThresholdType(), GetBlockSize(), Constant);
        }

        internal ThresholdTypes GetThresholdType()
        {
            if (MethodType == ThresholdMethodTypes.Adaptive) return BinaryType == ThresholdBinaryTypes.BinaryInvert ? ThresholdTypes.BinaryInv : ThresholdTypes.Binary;
            if (ThresholdType == ThresholdTypes.Binary || ThresholdType == ThresholdTypes.BinaryInv) return ThresholdType;
            ThresholdTypes type = BinaryType == ThresholdBinaryTypes.BinaryInvert ? ThresholdTypes.BinaryInv : ThresholdTypes.Binary;
            return ThresholdType | type;
        }

        internal Int32 GetBlockSize()
        {
            Int32 b = Math.Max((Int32)BlockSize, 3);
            Int32 r = b % 2;
            return r == 0 ? b + 1 : b;
        }
    }

    public class FloodFill : ICvTool
    {
        [Category(Categorys.Options)] public Int32 PointX { get; set; } = 0;
        [Category(Categorys.Options)] public Int32 PointY { get; set; } = 0;
        [Category(Categorys.Options)] public Byte Constant { get; set; } = Byte.MaxValue;
        [Category(Categorys.Options)] public Byte LowerDiff { get; set; } = Byte.MinValue;
        [Category(Categorys.Options)] public Byte UpperDiff { get; set; } = Byte.MaxValue;
        [Category(Categorys.Options)] public FloodFillFlags FillFlag { get; set; } = FloodFillFlags.Link4;
        [Category(Categorys.Results)] public Rect Rectangle { get; set; } = new Rect();

        internal override Mat Process(Mat mat)
        {
            Int32 result = mat.FloodFill(new Point(PointX, PointY), Scalar.All(Constant), out Rect rect, Scalar.All(LowerDiff), Scalar.All(UpperDiff), FillFlag);
            Rectangle = rect;
            return mat;
        }
    }

    public class SetConstant : ICvTool
    {
        [Category(Categorys.Options)] public Byte MinValue { get; set; } = Byte.MinValue;
        [Category(Categorys.Options)] public Byte MaxValue { get; set; } = Byte.MaxValue;
        [Category(Categorys.Options)] public Byte Constant { get; set; } = Byte.MaxValue;
        [Category(Categorys.Options)] public Byte Background { get; set; } = Byte.MinValue;
        internal override Mat Process(Mat mat)
        {
            Mat result = new Mat(mat.Rows, mat.Cols, MatType.CV_8UC1, Scalar.All(Background));
            using (Mat mask = mat.InRange(Scalar.All(MinValue), Scalar.All(MaxValue)))
                result.SetTo(Scalar.All(Constant), mask);
            return result;
        }
    }

    public class AddConstant : ICvTool
    {
        [Category(Categorys.Options)] public Double Constant { get; set; } = 0;
        internal override Mat Process(Mat mat)
        {
            if (Constant == 0) return mat;
            return mat.Add(new Scalar(Constant));
        }
    }

    public class MultiplyConstant : ICvTool
    {
        [Category(Categorys.Options)] public Double Scale { get; set; } = 1;
        internal override Mat Process(Mat mat)
        {
            if (Scale == 1) return mat;
            return mat.Multiply(Scale);
        }
    }

    public class Inverts : ICvTool
    {
        internal override Mat Process(Mat mat)
        {
            Mat inv = new Mat();
            Cv2.BitwiseNot(mat, inv);
            return inv;
        }
    }

    public class GammaCorrect : ICvTool
    {
        [Category(Categorys.Options)] public Double Gamma { get; set; } = 1;
        internal override Mat Process(Mat mat)
        {
            Mat lut = new Mat(1, 256, MatType.CV_8UC1);
            for (Int32 i = 0; i < 256; i++)
                lut.Set(0, i, (Byte)(Math.Pow(i / 255.0, Gamma) * 255.0));
            return mat.LUT(lut);
        }
    }
    #endregion

    #region Blurs, Sharpen, Filters
    public enum BlurTypes { Gaussian, Median, Average, Box }
    public class BlurFilters : ICvTool
    {
        [Category(Categorys.Options)] public BlurTypes Type { get; set; } = BlurTypes.Gaussian;
        [Category(Categorys.Options)] public BlockSize KernelSize { get; set; } = new BlockSize(3) { MinValue = 1, ValueType = BlockSizeTypes.Odd };
        [Category(Categorys.Options)] public Double SigmaX { get; set; } = 0;
        [Category(Categorys.Options)] public Double SigmaY { get; set; } = 0;
        internal override Mat Process(Mat mat)
        {
            if (Type == BlurTypes.Gaussian) return mat.GaussianBlur(KernelSize.Size, SigmaX, SigmaY);
            if (Type == BlurTypes.Median)   return mat.MedianBlur(KernelSize.Width);
            if (Type == BlurTypes.Box)      return mat.BoxFilter(-1, KernelSize.Size);
            if (Type == BlurTypes.Average)  return mat.Blur(KernelSize.Size);
            return mat;
        }
    }

    public class Bilateral : ICvTool
    {
        [Category(Categorys.Options)] public Int32 D { get; set; } = 9;
        [Category(Categorys.Options)] public Double SigmaColor { get; set; } = 75;
        [Category(Categorys.Options)] public Double SigmaSpace { get; set; } = 75;
        internal override Mat Process(Mat mat)
        {
            return mat.BilateralFilter(D, SigmaColor, SigmaSpace);
        }
    }

    public class UnsharpMask : BlurFilters
    {
        [Category(Categorys.Options)] public Double Alpha { get; set; } = 1.5;
        [Category(Categorys.Options)] public Double Beta { get; set; } = -0.5;
        [Category(Categorys.Options)] public Double Gamma { get; set; } = 0;
        internal override Mat Process(Mat mat)
        {
            using (Mat blurred = mat.GaussianBlur(KernelSize.Size, 0, 0))
            {
                Mat sharp = new Mat();
                Cv2.AddWeighted(mat, Alpha, blurred, Beta, Gamma, sharp);
                return sharp;
            }
        }
    }

    public class Laplacian : ICvTool
    {
        [Category(Categorys.Options)] public Int32 KernelSize { get; set; } = 3;
        //[Category(Categorys.Options)] public Double Alpha { get; set; } = 1;
        [Category(Categorys.Options)] public Double Beta { get; set; } = 0;
        internal override Mat Process(Mat mat)
        {
            using (Mat laplacian = mat.Laplacian(MatType.CV_16S, KernelSize))
                return laplacian.ConvertScaleAbs(1, Beta);
        }
    }

    public class KernelSharpen : ICvTool
    {
        [Category(Categorys.Options)] public Single Center { get; set; } = 5.0f;
        [Category(Categorys.Options)] public Single Proximate { get; set; } = -1.0f;
        [Category(Categorys.Options)] public Single Diagonal { get; set; } = 0.0f;
        private Single C => Center;
        private Single P => Proximate;
        private Single D => Diagonal;
        internal override Mat Process(Mat mat)
        {
            Single[] kernelData = new Single[] {
                D, P, D,
                P, C, P,
                D, P, D,
            };
            using (Mat kernel = Mat.FromPixelData(3, 3, MatType.CV_32F, kernelData))
                return mat.Filter2D(MatType.CV_8U, kernel);
        }
    }

    public enum DifferentialTypes { Scharr = 0, Sobel = 1 }
    public class Differential : ICvTool
    {
        [Category(Categorys.Options)] public DifferentialTypes Type { get; set; } = DifferentialTypes.Scharr;
        [Category(Categorys.Options)] public Int32 KernelSize { get; set; } = 3;
        //[Category(Categorys.Options)] public Int32 xOrder { get; set; } = 1;
        //[Category(Categorys.Options)] public Int32 yOrder { get; set; } = 1;

        internal override Mat Process(Mat mat)
        {
            // Scharr 필터 적용 (x와 y 방향의 미분) 후 결과를 절대값으로 변환 및 8비트로 변환
            Mat gradX, gradY;
            if (Type == DifferentialTypes.Sobel)
            {
                gradX = mat.Sobel(MatType.CV_16S, 1, 0, KernelSize);
                gradY = mat.Sobel(MatType.CV_16S, 0, 1, KernelSize);
            }
            else
            {
                gradX = mat.Scharr(MatType.CV_16S, 1, 0);
                gradY = mat.Scharr(MatType.CV_16S, 0, 1);
            }
            Mat absGradX = gradX.ConvertScaleAbs();
            gradX.Dispose();
            Mat absGradY = gradY.ConvertScaleAbs();
            gradY.Dispose();

            // x와 y 방향의 결과 합산
            Mat sharp = new Mat();
            Cv2.AddWeighted(absGradX, 0.5, absGradY, 0.5, 0, sharp);
            absGradX.Dispose();
            absGradY.Dispose();
            return sharp;
        }
    }

    // 느림
    public class MeansDenoising : ICvTool
    {
        [Category(Categorys.Options)] public Single H { get; set; } = 3;
        [Category(Categorys.Options)] public Int32 TemplateWindowSize { get; set; } = 7;
        [Category(Categorys.Options)] public Int32 SearchWindowSize { get; set; } = 21;

        internal override Mat Process(Mat mat)
        {
            Mat output = new Mat();
            Cv2.FastNlMeansDenoising(mat, output, H, TemplateWindowSize, SearchWindowSize);
            return output;
        }
    }
    #endregion

    #region Detector 
    public enum BowThresholdTypes { Fixed, RateByMean, RateByStDv }
    public class BlackAndWhite : ICvTool
    {
        [Category(Categorys.Options)] public BowThresholdTypes Type { get; set; } = BowThresholdTypes.RateByMean;
        [Category(Categorys.Options)] public Double LowerThreshold { get; set; } = 72;
        [Category(Categorys.Options)] public Double UpperThreshold { get; set; } = 28;
        [Category(Categorys.Options)] public Byte Constant { get; set; } = Byte.MaxValue;
        [JsonIgnore, Category(Categorys.Results)] public Double Mean { get; internal set; } = 0;
        [JsonIgnore, Category(Categorys.Results)] public Double StandardDeviation { get; internal set; } = 0;

        internal (Double Min, Double Max) CalThreshold(Double mean, Double stdv)
        {
            Double min = 0;
            Double max = 255;
            switch (Type)
            {
                case BowThresholdTypes.RateByMean:
                    min = Math.Max(0, mean - mean * LowerThreshold / 100);
                    max = Math.Min(255, mean + mean * UpperThreshold / 100);
                    break;
                case BowThresholdTypes.RateByStDv:
                    min = Math.Max(0, mean - stdv * LowerThreshold / 100);
                    max = Math.Min(255, mean + stdv * UpperThreshold / 100);
                    break;
                case BowThresholdTypes.Fixed:
                    min = Math.Max(0, LowerThreshold);
                    max = Math.Min(255, UpperThreshold);
                    break;
            }
            return (min, max);
        }

        internal override Mat Process(Mat mat)
        {
            Mat result = new Mat(mat.Rows, mat.Cols, MatType.CV_8UC1, Scalar.All(0));
            Byte min = (Byte)LowerThreshold;
            Byte max = (Byte)UpperThreshold;
            if (Type != BowThresholdTypes.Fixed)
            {
                using (Mat hist = mat.EqualizeHist())
                {
                    Cv2.MeanStdDev(hist, out Scalar m, out Scalar s, null);
                    var thres = CalThreshold(m.Val0, s.Val0);
                    min = (Byte)thres.Min;
                    max = (Byte)thres.Max;
                    Mean = Math.Round(m.Val0, 3);
                    StandardDeviation = Math.Round(s.Val0, 3);
                }
            }
            using (Mat mask = mat.InRange(new Scalar(min), new Scalar(max)))
            {
                if (Constant == Byte.MaxValue) Cv2.BitwiseNot(mask, mask);
                else mask.SetTo(Constant, mask);
                mask.CopyTo(result);
            }
            return result;
        }
    }

    public class CanyEdge : ICvTool
    {
        [Category(Categorys.Options)] public Double Threshold1 { get; set; } = 50;
        [Category(Categorys.Options)] public Double Threshold2 { get; set; } = 150;
        [Category(Categorys.Options)] public Int32 ApertureSize { get; set; } = 3;
        [Category(Categorys.Options)] public Boolean L2gradient { get; set; } = false;

        internal override Mat Process(Mat mat)
        {
            Mat cany = mat.Canny(Threshold1, Threshold2, ApertureSize, L2gradient);
            return cany;
        }
    }

    public enum LineDetectDraw { None, New, Overwrite }
    public class LineDetect : ICvTool
    {
        [Category(Categorys.Options)] public Double RHO { get; set; } = 1;
        [Category(Categorys.Options)] public Double Theta = Math.PI / 180;
        [Category(Categorys.Options)] public Int32 Threshold { get; set; } = 100;
        [Category(Categorys.Options)] public Double MinLineLength { get; set; } = 10;
        [Category(Categorys.Options)] public Double MaxLineGap { get; set; } = 10;

        [Category(Categorys.Graphic)] public Byte LineConstant { get; set; } = Byte.MaxValue;
        [Category(Categorys.Graphic)] public Byte LineBackground { get; set; } = Byte.MinValue;
        [Category(Categorys.Graphic)] public Int32 LineThickness { get; set; } = 2;
        [Category(Categorys.Graphic)] public Int32 LineShift { get; set; } = 0;
        [Category(Categorys.Graphic)] public LineDetectDraw LineDraw { get; set; } = LineDetectDraw.None;

        [Category(Categorys.Results), TypeConverter(typeof(ExpandableObjectConverter)), JsonConverter(typeof(ExpandableObjectJsonConverter))]
        public List<LineSegment> Segments { get; set; } = new List<LineSegment>();

        internal override Mat Process(Mat mat)
        {
            Segments.Clear();
            LineSegmentPoint[] lines = mat.HoughLinesP(RHO, Theta, Threshold, MinLineLength, MaxLineGap);
            foreach(var l in lines) Segments.Add(new LineSegment(l));
            if (LineDraw == LineDetectDraw.None) return mat;

            Mat result;
            if (LineDraw == LineDetectDraw.Overwrite) result = mat;
            else result = new Mat(mat.Size(), mat.Type(), Scalar.All(LineBackground));
            if (lines == null || lines.Length < 1) return result;
            foreach (var line in lines)
                result.Line(new Point(line.P1.X, line.P1.Y), new Point(line.P2.X, line.P2.Y), Scalar.All(LineConstant), LineThickness, LineTypes.Link8, LineShift);
            return result;
        }
    }

    //public class CornerDetect : ICvTool
    //{
    //    [Category(Categorys.Options)] public Int32 KernelSize { get; set; } = 3;
    //    internal override Mat Process(Mat mat)
    //    {
    //        return mat.PreCornerDetect(KernelSize);
    //    }
    //}
    #endregion

    #region Scaling
    public class Resize : ICvTool
    {
        private Single scaleX = 1;
        private Single scaleY = 1;
        [Category(Categorys.Options)] public Boolean Locked { get; set; } = true;
        [Category(Categorys.Options)] public Single ScaleX { get => scaleX; set => SetX(value); }
        [Category(Categorys.Options)] public Single ScaleY { get => scaleY; set => SetY(value); }

        internal override Mat Process(Mat mat)
        {
            if (ScaleX == 1) return mat;
            return mat.Resize(new Size(), scaleX, scaleY, InterpolationFlags.Linear);
        }

        private void SetX(Single value)
        {
            if (scaleX == value) return;
            scaleX = value;
            if (Locked) scaleY = value;
        }
        private void SetY(Single value)
        {
            if (scaleY == value) return;
            scaleY = value;
            if (Locked) scaleX = value;
        }
    }

    public class Copy : ICvTool
    {
        [Category(Categorys.Region)]  public Region Region { get; set; } = new Region();
        [Category(Categorys.Options)] public Single Scale { get; set; } = 1;
        [Category(Categorys.Options)] public System.Drawing.Color Background { get; set; } = System.Drawing.Color.Black;
        internal override Mat Process(Mat mat)
        {
            return Base.Copy(mat, Region.RotatedRect, Background, Scale);
        }
    }

    public enum ConvertTypes { Brightness = -1, B = 0, G = 1, R = 2 }
    public class Convertor : ICvTool
    {
        [Category(Categorys.Options)] public ConvertTypes Type { get; set; } = ConvertTypes.Brightness;
        internal override Mat Process(Mat mat)
        {
            if (mat.Type() == MatType.CV_8UC1) return mat;
            if (mat.Type() != MatType.CV_8UC3) return mat;
            if (Type == ConvertTypes.Brightness) return mat.CvtColor(ColorConversionCodes.BGR2GRAY);
            Int32 type = (Int32)Type;
            return mat.ExtractChannel(type).Clone();
        }
    }
    #endregion
}