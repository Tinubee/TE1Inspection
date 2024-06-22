using Cognex.VisionPro;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MvLibs
{
    public class RectanglePoints
    {
        public RectanglePoints() { }
        public RectanglePoints(PointD lt, PointD rt, PointD lb, PointD rb)
        {
            LT = lt; RT = rt; LB = lb; RB = rb;
        }
        public RectanglePoints(Double ltx, Double lty, Double rtx, Double rty, Double lbx, Double lby, Double rbx, Double rby)
        {
            LT = new PointD(ltx, lty);
            RT = new PointD(rtx, rty);
            LB = new PointD(lbx, lby);
            RB = new PointD(rbx, rby);
        }

        public PointD LT = new PointD();
        public PointD RT = new PointD();
        public PointD LB = new PointD();
        public PointD RB = new PointD();

        public List<Double> Xs() => new List<Double>() { LT.X, RT.X, LB.X, RB.X };
        public List<Double> Ys() => new List<Double>() { LT.Y, RT.Y, LB.Y, RB.Y };
        public PointD CenterL() => Base.MidPoint(LT, LB);
        public PointD CenterR() => Base.MidPoint(RT, RB);
        public PointD CenterT() => Base.MidPoint(LT, RT);
        public PointD CenterB() => Base.MidPoint(LB, RB);
        public PointD Center() => new PointD(Xs().Average(), Ys().Average());
        public Double Width() => Base.GetDistance(CenterL(), CenterR());
        public Double Height() => Base.GetDistance(CenterT(), CenterB());
        public Point2f[] ToArray() => new Point2f[] { LT.Point2f, RT.Point2f, LB.Point2f, RB.Point2f };
        public override string ToString() => $"LT={LT.ToString()}, RT={RT.ToString()}, LB={LB.ToString()}, RB={RB.ToString()}";
    }

    public class RectanglePerspectiveTransform : IDisposable
    {
        public Double CalibX = 1;
        public Double CalibY = 1;
        public Double RealWidth = 100;
        public Double RealHeight = 100;
        public RectanglePoints Norminal = new RectanglePoints();
        public RectanglePoints Destination = new RectanglePoints();
        public RectanglePoints Origins = new RectanglePoints();

        [JsonIgnore]
        public Double SideX => RealWidth / CalibX / 2;
        [JsonIgnore]
        public Double SideY => RealHeight / CalibY / 2;
        [JsonIgnore]
        public Double Top => Math.Min(Norminal.LT.Y, Norminal.RT.Y);
        [JsonIgnore]
        public Double Bottom => Math.Max(Norminal.LB.Y, Norminal.RB.Y);
        [JsonIgnore]
        public Double Left => Math.Min(Norminal.LT.X, Norminal.LB.X);
        [JsonIgnore]
        public Double Right => Math.Max(Norminal.RT.X, Norminal.RB.X);
        [JsonIgnore]
        public Double Width => Right - Left;
        [JsonIgnore]
        public Double Height => Bottom - Top;
        [JsonIgnore]
        internal Mat Forward;
        [JsonIgnore]
        internal Mat Reverse;

        public String ToJson(Boolean useIndented = false) => JsonConvert.SerializeObject(this, Base.JsonSetting(useIndented));
        public Boolean Load(String json)
        {
            if (String.IsNullOrEmpty(json)) return false;
            try
            {
                RectanglePerspectiveTransform p = JsonConvert.DeserializeObject<RectanglePerspectiveTransform>(json, Base.JsonSetting(false));
                CalibX = p.CalibX;
                CalibY = p.CalibY;
                RealWidth = p.RealWidth;
                RealHeight = p.RealHeight;
                Norminal = p.Norminal;
                Destination = p.Destination;
                Origins = p.Origins;
                CreateTransform();
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message, nameof(RectanglePoints)); }
            return false;
        }

        internal void CreateNorminal()
        {
            Norminal.LT = new PointD(-SideX, -SideY);
            Norminal.RT = new PointD(+SideX, -SideY);
            Norminal.LB = new PointD(-SideX, +SideY);
            Norminal.RB = new PointD(+SideX, +SideY);
        }

        internal void CreateTransform()
        {
            Forward = Cv2.GetPerspectiveTransform(Norminal.ToArray(), Destination.ToArray());
            Reverse = Cv2.GetPerspectiveTransform(Destination.ToArray(), Norminal.ToArray());
        }

        public void CreateMatrix()
        {
            CreateNorminal();
            Destination = Norminal;
            Origins = Norminal;
            CreateTransform();
        }
        public void CreateMatrix(RectanglePoints origins, out PointD center)
        {
            CreateNorminal();
            PointD oc = origins.Center();
            Destination.LT = new PointD(origins.LT.X - oc.X, origins.LT.Y - oc.Y);
            Destination.RT = new PointD(origins.RT.X - oc.X, origins.RT.Y - oc.Y);
            Destination.LB = new PointD(origins.LB.X - oc.X, origins.LB.Y - oc.Y);
            Destination.RB = new PointD(origins.RB.X - oc.X, origins.RB.Y - oc.Y);
            Origins = origins;
            CreateTransform();
            center = TransForward(0, 0);
        }
        public void Fixture(ICogImage image, String spaceName)
        {
            Destination = new RectanglePoints(
                new PointD(Base.PointTransform(image, spaceName, Origins.LT.X, Origins.LT.Y)),
                new PointD(Base.PointTransform(image, spaceName, Origins.RT.X, Origins.RT.Y)),
                new PointD(Base.PointTransform(image, spaceName, Origins.LB.X, Origins.LB.Y)),
                new PointD(Base.PointTransform(image, spaceName, Origins.RB.X, Origins.RB.Y))
            );
            Dispose();
            CreateTransform();
        }

        public PointD TransForward(Double x, Double y) => TransForward(new PointD(x, y));
        public PointD TransForward(PointD p) => TransForward(new List<PointD> { p }).FirstOrDefault();
        public List<PointD> TransForward(List<PointD> targets)
        {
            if (Forward == null) return targets;
            return Cv2.PerspectiveTransform(targets.Select(e => e.Point2d).ToArray(), Forward).Select(e => new PointD(e)).ToList();
        }

        public PointD TransReverse(Double x, Double y) => TransReverse(new PointD(x, y));
        public PointD TransReverse(PointD p) => TransReverse(new List<PointD> { p }).FirstOrDefault();
        public List<PointD> TransReverse(List<PointD> targets)
        {
            if (Reverse == null) return targets;
            return Cv2.PerspectiveTransform(targets.Select(e => e.Point2d).ToArray(), Reverse).Select(e => new PointD(e)).ToList();
        }

        public Double CalDistance(PointD p1, PointD p2)
        {
            List<PointD> p = TransReverse(new List<PointD>() { p1, p2 });
            return Base.GetDistance(p[0], p[1]);
            //return Base.GetDistance(p1, p2);
        }
        public Double CalDistance(Point2d p1, Point2d p2) => CalDistance(new PointD(p1), new PointD(p2));
        public Double CalDistance(Double x1, Double y1, Double x2, Double y2) => CalDistance(new PointD(x1, y1), new PointD(x2, y2));

        //public Double CalHeightRate(Double x)
        //{
        //    Double distance = 0;
        //    using (CogLineSegment top = new CogLineSegment() { StartX = Destination.LT.X, StartY = Destination.LT.Y, EndX = Destination.RT.X, EndY = Destination.RT.Y })
        //    using (CogLineSegment btm = new CogLineSegment() { StartX = Destination.LB.X, StartY = Destination.LB.Y, EndX = Destination.RB.X, EndY = Destination.RB.Y })
        //    {
        //        distance += top.DistanceToPoint(x, 0);
        //        distance += btm.DistanceToPoint(x, 0);
        //    }
        //    //Debug.WriteLine($"{Height} / {distance}");
        //    return Height / distance;
        //}


        public void Dispose() { Forward?.Dispose(); Reverse?.Dispose(); }
    }
}
