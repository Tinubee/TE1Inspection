﻿using DevExpress.Utils.Extensions;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace TE1.Schemas
{
    public partial class Viewport : HelixViewport3D
    {
        #region 기본설정
        public Viewport() { }

        internal ModelVisual3D ModelVisual = new ModelVisual3D();
        internal Model3DGroup ModelGroup = new Model3DGroup();
        internal GeometryModel3D MainModel = null;

        public virtual String StlPath => String.Empty;
        public virtual String StlFile => String.Empty;
        public virtual Double Scale => 1;
        public virtual Material FrontMaterial => MaterialHelper.CreateMaterial(Colors.LightGray, 0.8);
        public virtual Material BackMaterial => MaterialHelper.CreateMaterial(Colors.Silver, 0.8);
        public virtual Material GridMaterial => MaterialHelper.CreateMaterial(Colors.LightGray, 0.5);
        public virtual Material BlackMaterial => MaterialHelper.CreateMaterial(Colors.Black, 0.5);
        public virtual Material RedMaterial => MaterialHelper.CreateMaterial(Colors.Red, 0.8);

        public virtual Point3D CameraPosition { get; set; } = default(Point3D);
        public virtual Vector3D CameraLookDirection { get; set; } = default(Vector3D);
        public virtual Vector3D CameraUpDirection { get; set; } = default(Vector3D);

        public Boolean Init(Hosts pc, out String error)
        {
            //this.Children.Clear();
            error = String.Empty;
            //if (!변경)
            //{
            ModelGroup.SetName(nameof(ModelGroup));
            PanGesture.MouseAction = MouseAction.LeftClick;
            PanGesture2.MouseAction = MouseAction.LeftClick;
            MouseDoubleClick += ViewportMouseDoubleClick;

            try { LoadStl(); }
            catch (Exception ex)
            {
                error = ex.Message;
                Debug.WriteLine(ex.Message);
            }

            ModelVisual.Content = ModelGroup;
            Children.Add(new DefaultLights());
            Children.Add(ModelVisual);
            //}

            InitModel(pc);
            InitCamera();
            return String.IsNullOrEmpty(error);
        }
        internal virtual void LoadStl()
        {
            if (!File.Exists(StlFile)) return;
            StLReader reader = new StLReader();
            Model3DGroup groups = reader.Read(StlFile);
            MainModel = groups.Children[0] as GeometryModel3D;

            Point3D p = Center3D();
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new TranslateTransform3D(p.X * Scale, p.Y * Scale, 0 * Scale));
            transform.Children.Add(new ScaleTransform3D(Scale, Scale, Scale));
            MainModel.Transform = transform;
            //Debug.WriteLine(p.ToString(), "p");
            //Debug.WriteLine(MainModel.Transform.Value.ToString(), "Transform");

            MainModel.SetName(nameof(MainModel));
            MainModel.Material = FrontMaterial;
            MainModel.BackMaterial = BackMaterial;
            ModelGroup.Children.Add(MainModel);
        }
        internal virtual void ViewportMouseDoubleClick(object sender, MouseButtonEventArgs e) => InitCamera();

        public ElementHost CreateHost() =>
            new ElementHost() { Child = this, Dock = System.Windows.Forms.DockStyle.Fill };
        public virtual void Refresh() => InvalidateVisual();

        public virtual Point3D Center3D()
        {
            if (MainModel == null) return new Point3D(0, 0, 0);
            Rect3D rect = MainModel.Bounds;
            return new Point3D(rect.X + rect.SizeX / 2, rect.Y + rect.SizeY / 2, rect.Z + rect.SizeZ / 2);
        }

        public virtual void InitModel(Hosts pc) { }
        internal void InitCamera()
        {
            if (CameraPosition != default(Point3D)) Camera.Position = CameraPosition;
            if (CameraLookDirection != default(Vector3D)) Camera.LookDirection = CameraLookDirection;
            if (CameraUpDirection != default(Vector3D)) Camera.UpDirection = CameraUpDirection;
            CameraRotationMode = CameraRotationMode.Trackball;
        }
        #endregion

        #region 이벤트
        internal virtual void ViewportCameraChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Camera is PerspectiveCamera cam)
            {
                Debug.WriteLine(Center3D().ToString(), "Center");
                DebugPoint(cam.Position, "Position");
                DebugVector(cam.LookDirection, "LookDirection");
                DebugVector(cam.UpDirection, "UpDirection");
            }
        }
        private void DebugPoint(Point3D p, String name) =>
            Debug.WriteLine($"Camera.{name} = new Point3D({Math.Round(p.X, 3)}, {Math.Round(p.Y, 3)}, {Math.Round(p.Z)});");
        private void DebugVector(Vector3D p, String name) =>
            Debug.WriteLine($"Camera.{name} = new Vector3D({Math.Round(p.X, 3)}, {Math.Round(p.Y, 3)}, {Math.Round(p.Z)});");
        #endregion

        internal virtual Visual3D Add(Visual3D item) { Children.Add(item); return item; }
        internal virtual Boolean Remove(Visual3D item) => Children.Remove(item);
        //Filter
        internal virtual void Remove(Base3D item) => item.Clear(Children);
        internal virtual void Add(Base3D item)
        {
            item.Create(Children);
        }
      
        internal virtual Boolean Remove(GeometryModel3D item) => ModelGroup.Children.Remove(item);

        internal virtual void AddArrowLine(Point3D s, Point3D e, Color color)
        {
            Point3D center = new Point3D((s.X + e.X) / 2, (s.Y + e.Y) / 2, (s.Z + e.Z) / 2);
            Children.Add(CreateArrowLine(center, s, color));
            Children.Add(CreateArrowLine(center, e, color));
        }
        internal virtual Visual3D AddStaticLine(Point3D s, Point3D e, Color color) =>
            Add(new LinesVisual3D { Points = new Point3DCollection { s, e }, Color = color });
        internal virtual Visual3D AddText3D(Point3D point, String text, Double size, Color color) =>
            Add(CreateText3D(point, text, size, color));
        internal virtual Visual3D AddLabel(Point3D point, String text, Double size, Color color) =>
            Add(CreateLabel(point, text, size, color));
        internal virtual Visual3D AddRectangle(Point3D origin, Double width, Double height, Color color, Vector3D normal) =>
            Add(CreateRectangle(origin, width, height, color, normal));
        internal virtual GeometryModel3D AddPolygon(List<Point3D> points, Material material)
        {
            Polygon3D polygon = new Polygon3D();
            points.ForEach(p => polygon.Points.Add(p));
            MeshBuilder meshBuilder = new MeshBuilder(false, false);
            meshBuilder.AddPolygon(polygon.Points);
            GeometryModel3D model = new GeometryModel3D
            {
                Geometry = meshBuilder.ToMesh(true),
                Transform = new TranslateTransform3D(1, 0, 0),
                Material = material,
            };
            ModelGroup.Children.Add(model);
            return model;
        }
        internal virtual GeometryModel3D AddPolygon(List<Point3D> points, Color color, Double opacity = 1) =>
            AddPolygon(points, MaterialHelper.CreateMaterial(color, opacity));

        public static Color ToColor(System.Drawing.Color color) => Color.FromArgb(color.A, color.R, color.G, color.B);

        public static BillboardTextVisual3D CreateText3D(Point3D point, String text, Double size, Color color) =>
            new BillboardTextVisual3D()
            {
                Text = text,
                Position = point,
                FontSize = size,
                Padding = new System.Windows.Thickness(4, 2, 4, 2),
                Foreground = new SolidColorBrush(color),
                Background = new SolidColorBrush(Colors.Transparent),
                HorizontalAlignment = text == "R" || text == "L" ? System.Windows.HorizontalAlignment.Center : System.Windows.HorizontalAlignment.Left,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
            };

        public static TextVisual3D CreateLabel(Point3D point, String text, Double height, Color color) =>
            new TextVisual3D()
            {
                Text = text,
                Position = point,
                Height = height,
                TextDirection = new Vector3D(1, 0, 0),
                UpDirection = new Vector3D(0, 1, 0),
                FontWeight = System.Windows.FontWeight.FromOpenTypeWeight(600),
                Padding = new System.Windows.Thickness(4, 0, 4, 2),
                Foreground = new SolidColorBrush(color),
                Background = new SolidColorBrush(Colors.Transparent),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
            };

        public static ArrowVisual3D CreateArrowLine(Point3D s, Point3D e, Color color) =>
            new ArrowVisual3D() { Point1 = s, Point2 = e, Diameter = 1, HeadLength = 4, Fill = new SolidColorBrush(color) };

        public static RectangleVisual3D CreateRectangle(Point3D origin, Double width, Double height, Color color, Vector3D normal) =>
            new RectangleVisual3D() { Origin = origin, Width = width, Length = height, Normal = normal, Fill = new SolidColorBrush(color) };

        public static PieSliceVisual3D CreateCircle(Point3D center, Double radius, Color color) =>
            new PieSliceVisual3D() { Center = center, InnerRadius = 0, OuterRadius = radius, Fill = new SolidColorBrush(color), StartAngle = 0, EndAngle = 360, UpVector = new Vector3D(0, 1, 0) };
    }

    public static class MajorColors
    {
        public static Color FrameColor => Colors.Yellow;
        public static Color StaticColor => Colors.Cyan;
        public static Color GoodColor => Colors.Green;
        public static Color BadColor => Colors.Red;
        public static Color WarningColor => Colors.Magenta;
        public static Color LimeColor => Colors.Lime;
        public static Color GoldColor => Colors.Gold;
        public static Color SkyBlueColor => Colors.SkyBlue;

    }
}
