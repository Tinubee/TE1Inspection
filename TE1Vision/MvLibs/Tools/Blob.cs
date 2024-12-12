using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MvLibs.Tools
{
    public class Blob : ICvTool
    {
        [Category(Categorys.Options)] public PixelConnectivity Connectivity { get; set; } = PixelConnectivity.Connectivity8;
        [Category(Categorys.Options)] public ConnectedComponentsAlgorithmsTypes Algorithms { get; set; } = ConnectedComponentsAlgorithmsTypes.Default;
        [Category(Categorys.Options)] public Int32 MinArea { get; set; } = 100;
        [Category(Categorys.Options)] public Int32 MaxArea { get; set; } = 0;
        [Category(Categorys.Options)] public Byte Constant { get; set; } = Byte.MaxValue;
        [JsonIgnore, Category(Categorys.Results), TypeConverter(typeof(ExpandableObjectConverter))]
        public List<BlobResult> Results { get; set; } = new List<BlobResult>();

        internal override Mat Process(Mat mat)
        {
            Results.Clear();
            ConnectedComponents cc = Cv2.ConnectedComponentsEx(mat, Connectivity, Algorithms);
            return CalResults(cc);
        }

        private Mat CalResults(ConnectedComponents cc)
        {
            if (cc.Blobs is null || cc.Blobs.Count < 1) return null;
            if (cc.Labels is null) return null;

            BoundaryFactory factory = new BoundaryFactory();
            Int32 rows = cc.Labels.GetLength(0);
            Int32 cols = cc.Labels.GetLength(1);
            Mat result = new Mat(new Size(cols, rows), MatType.CV_8UC1, Scalar.Black);

            using (Mat<Byte> temp = new Mat<Byte>(result))
            {
                MatIndexer<Byte> indexer = temp.GetIndexer();

                for (Int32 y = 0; y < rows; y++)
                {
                    for (Int32 x = 0; x < cols; x++)
                    {
                        Int32 label = cc.Labels[y, x];
                        if (label == 0) continue;
                        ConnectedComponents.Blob blob = cc.Blobs[label];
                        if (MinArea > 0 && blob.Area < MinArea) continue;
                        if (MaxArea > 0 && blob.Area > MaxArea) continue;
                        factory.Add(label, y, x);
                        indexer[y, x] = Constant;
                    }
                }
            }

            // Create Boundarys
            Dictionary<Int32, List<Point>> boundarys = factory.GetBoundarys();
            foreach (var d in boundarys)
                Results.Add(new BlobResult(cc.Blobs[d.Key], d.Value));
            boundarys.Clear();
            return result;
        }

        private class BoundaryFactory : Dictionary<Int32, Dictionary<Int32, Tuple<Int32, Int32>>>
        {
            public void Add(Int32 label, Int32 y, Int32 x)
            {
                if (!ContainsKey(label)) Add(label, new Dictionary<Int32, Tuple<Int32, Int32>>());
                var row = this[label];
                if (!row.ContainsKey(y)) row.Add(y, Tuple.Create(x, x));
                else
                {
                    var v = row[y];
                    row[y] = Tuple.Create(Math.Min(v.Item1, x), Math.Max(v.Item2, x));
                }
            }

            public Dictionary<Int32, List<Point>> GetBoundarys()
            {
                Dictionary<Int32, List<Point>> boundarys = new Dictionary<Int32, List<Point>>();
                foreach (var d in this)
                {
                    List<Point> points = new List<Point>();
                    foreach (var p in d.Value)
                    {
                        points.Add(new Point(p.Value.Item1, p.Key));
                        if (p.Value.Item1 != p.Value.Item2)
                            points.Add(new Point(p.Value.Item2, p.Key));
                    }
                    boundarys.Add(d.Key, points);
                }
                this.Clear();
                return boundarys;
            }
        }

        public class BlobResult
        {
            public Int32 Index { get; internal set; } = 0;
            public Int32 Area { get; internal set; } = 0;
            public Double CentroidX { get; internal set; } = 0;
            public Double CentroidY { get; internal set; } = 0;
            public List<Point> Boundarys { get; internal set; } = new List<Point>();
            public Rect Rect { get; internal set; }

            public BlobResult() { }
            public BlobResult(ConnectedComponents.Blob blob, IEnumerable<Point> boundarys) { Set(blob); Boundarys.AddRange(boundarys); }

            internal void Set(ConnectedComponents.Blob blob)
            {
                Index = blob.Label;
                Area = blob.Area;
                CentroidX = blob.Centroid.X;
                CentroidY = blob.Centroid.Y;
                Rect = blob.Rect;
            }

            public RotatedRect RotatedRect()
            {
                if (Boundarys.Count < 1) return new RotatedRect(new Point2f(Rect.X, Rect.Y), new Size2f(Rect.Width, Rect.Height), 0);
                Point2f[] points2f = Boundarys.Select(p => new Point2f(p.X, p.Y)).ToArray();
                return Cv2.MinAreaRect(points2f);
            }
            public Point2f[] RotatedPath() => RotatedRect().Points();
        }
    }
}
