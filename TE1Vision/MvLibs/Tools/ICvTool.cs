using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Shapes;

namespace MvLibs.Tools
{
    public static class Categorys
    {
        public const String Identity = "Identity";
        public const String Options = "Options";
        public const String Region = "Region";
        public const String Results = "Results";
        public const String Graphic = "Graphic";
        public const String CLAHE = "Contrast Limited Adaptive Histogram Equalization";
        public const String Kernel = "kernel";
        public const String Adaptive = "Adaptive Thresholding Algorithms";
    }

    public abstract class ICvTool : IDisposable
    {
        [Category(Categorys.Identity)] public String ID { get; set; } = UniqueIdGenerator.TimeBasedId(13);
        [Category(Categorys.Identity)] public String Name { get; set; } = String.Empty;
        [Category(Categorys.Identity)] public Boolean Enabled { get; set; } = true;
        [Category(Categorys.Results), JsonIgnore] public Double BehaviorTime { get; internal set; } = 0;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public String ToolType => GetType().ToString();

        public ICvTool() { Name = GetType().Name; }
        public ICvTool(String name) { Name = name; }

        public virtual void Dispose() { }

        public virtual Boolean Set(ICvTool tool)
        {
            if (tool.ID != ID || GetType() != tool.GetType()) return false;
            PropertyCopy.CopyProperties(tool, this, true);
            return true;
        }

        public virtual Mat Run(Mat mat)
        {
            BehaviorTime = 0;
            DateTime start = DateTime.Now;
            Dispose();
            if (!Enabled) return mat;
            Mat output = Process(mat);
            BehaviorTime = Math.Round((DateTime.Now - start).TotalMilliseconds, 3);
            return output;
        }

        internal abstract Mat Process(Mat mat);
    }

    [TypeConverter(typeof(ExpandableObjectConverter)), JsonConverter(typeof(ExpandableObjectJsonConverter))]
    public class Region
    {
        public Double CenterX { get; set; } = 0;
        public Double CenterY { get; set; } = 0;
        public Double Width { get; set; } = 0;
        public Double Height { get; set; } = 0;
        public Double Angle { get; set; } = 0;

        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point2d Center2d => new Point2d(CenterX, CenterY);
        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point2f Center2f => new Point2f((Single)CenterX, (Single)CenterY);
        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point Center => Center2d.ToPoint();

        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size Size => Size2d.ToSize();
        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size2d Size2d => new Size2d(Width, Height);
        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size2f Size2f => new Size2f((Single)Width, (Single)Height);

        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RotatedRect RotatedRect => new RotatedRect(Center2f, Size2f, (Single)Angle);
        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rect Rect => Base.BoundingRect(RotatedRect);

        public Rect IntersectRect(Int32 width, Int32 height) => Rect.Intersect(new Rect(0, 0, width, height));
    }

    [TypeConverter(typeof(ExpandableObjectConverter)), JsonConverter(typeof(ExpandableObjectJsonConverter))]
    public class LineSegment
    {
        public Double StartX { get; set; } = 0;
        public Double StartY { get; set; } = 0;
        public Double EndX { get; set; } = 0;
        public Double EndY { get; set; } = 0;

        public LineSegment() { }
        public LineSegment(Double startX, Double startY, Double endX, Double endY)
        {
            StartY = startY; StartY = startY; EndX = endX; EndY = endY;
        }
        public LineSegment(LineSegmentPoint line)
        {
            StartX = line.P1.X; StartY = line.P1.Y; EndX = line.P2.X; EndY = line.P2.Y;
        }

        public Double Length => Math.Sqrt(Math.Pow(EndX - StartX, 2) + Math.Pow(EndY - StartY, 2));
        public Double Angle => Math.Atan2(EndY - StartY, EndX - StartX) * 180 / Math.PI;
        public LineSegmentPoint LineSegmentPoint => new LineSegmentPoint(new Point2d(StartX, StartY).ToPoint(), new Point2d(EndX, EndY).ToPoint());
    }

    public enum BlockSizeTypes { All = 0, Odd = 1, Even = 2 }
    [TypeConverter(typeof(ExpandableObjectConverter)), JsonConverter(typeof(ExpandableObjectJsonConverter))]
    public class BlockSize
    {
        private Int32 width = 0;
        private Int32 height = 0;
        public Boolean Locked { get; set; } = true;
        public Int32 Width { get => width; set => SetWidth(value); }
        public Int32 Height { get => height; set => SetHeight(value); }

        public BlockSizeTypes ValueType = BlockSizeTypes.All;
        public Int32 MinValue = 1;
        public Int32 MaxValue = 255;
        [JsonIgnore, Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size Size => new Size(Width, Height);

        public BlockSize() { }
        public BlockSize(Int32 size) { Width = size; Height = size; }
        public BlockSize(Int32 width, Int32 height) { Width = width; Height = height; }

        private Int32 CalValue(Int32 val)
        {
            Int32 v = Math.Max(MinValue, Math.Min(MaxValue, val));
            if (ValueType == BlockSizeTypes.All) return v;
            Int32 m = (Int32)ValueType % 2;
            if (v % 2 != m) return v + 1;
            return v;
        }

        private void SetWidth(Int32 value)
        {
            if (width == value) return;
            width = CalValue(value);
            if (Locked) height = width;
        }
        private void SetHeight(Int32 value)
        {
            if (height == value) return;
            height = CalValue(value);
            if (Locked) width = height;
        }
    }

    public class ToolListInfo
    {
        public Type Type { get; set; } = null;
        public String Name => Type?.Name;
        public ToolListInfo() { }
        public ToolListInfo(Type type) { Type = type; }
    }

    //public class CvToolConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        Debug.WriteLine(objectType.ToString());
    //        return true;
    //        //return objectType == typeof(ICvTool);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        JObject jo = JObject.Load(reader);
    //        String typeName = jo["ToolType"].Value<String>();
    //        Type type = Type.GetType(typeName, false);
    //        Object tool = jo.ToObject(type);
    //        serializer.Populate(jo.CreateReader(), tool);
    //        return tool;
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        //serializer.Serialize(writer, value);
    //        JObject jo = new JObject();
    //        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(value))
    //        {
    //            if (!prop.IsBrowsable) continue;
    //            Object propValue = prop.GetValue(value);
    //            if (propValue == null) continue;
    //            jo.Add(prop.Name, JToken.FromObject(propValue, serializer));
    //        }
    //        jo.WriteTo(writer);
    //    }
    //}
}
