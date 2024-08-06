using DevExpress.Utils.Extensions;
using HelixToolkit.Wpf;
using MvUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace TE1.Schemas
{
    public class UPR3P24S3D : Viewport
    {
        #region 초기화
        public override String StlPath => Global.환경설정.기본경로;
        public override String StlFile => Path.Combine(StlPath, "UPR3P24S.stl");
        public override Double Scale => 1;
        internal override void LoadStl()
        {
            if (!File.Exists(StlFile)) return;
            Point3D p = Center3D();
            Transform3DGroup transform = new Transform3DGroup();
            transform.Children.Add(new TranslateTransform3D(p.X * Scale, p.Y * Scale, -4 * Scale));
            transform.Children.Add(new ScaleTransform3D(Scale, Scale, Scale));

            StLReader reader = new StLReader();
            Model3DGroup groups = reader.Read(StlFile);
            MainModel = groups.Children[0] as GeometryModel3D;
            MainModel.Transform = transform;
            MainModel.SetName(nameof(MainModel));
            MainModel.Material = FrontMaterial;
            MainModel.BackMaterial = BackMaterial;
            ModelGroup.Children.Add(MainModel);
        }
        #endregion

        #region 기본 설정
        private List<Base3D> InspItems = new List<Base3D>();
        private List<Base3D> changeInspItems = new List<Base3D>();
        private List<GeometryModel3D> SurfaceItems = new List<GeometryModel3D>();
        private Material SurfaceMaterial = MaterialHelper.CreateMaterial(Colors.Red, 0.5);
        internal String InspectionName(검사항목 항목)
        {
            검사정보 정보 = Global.모델자료.선택모델.검사설정.GetItem(항목);
            if (정보 == null) return String.Empty;
            return 정보.검사명칭;
        }

        public void 검사항목표시(검사항목 항목, Double originX, Double originY, Double tz)
        {
            if (항목.ToString().StartsWith("H"))
            {
                if (항목.ToString().Contains("X"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else if (항목.ToString().Contains("Y"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - 5, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else if (항목.ToString().Contains("D"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - 10, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else if (항목.ToString().Contains("P"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - 15, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
            }
            else if (항목.ToString().StartsWith("F"))
                InspItems.Add(new Circle3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
            else if (항목.ToString().StartsWith("T"))
            {
                //T001
                Int32 번호 = Convert.ToInt32(항목.ToString().Substring(2, 2));

                if (번호 <= 14)
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y + 30, tz), Name = $"{항목}", LabelStyle = NamePrintType.Up, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else if (번호 <= 20)
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X + 30, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Right, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else if (번호 <= 42)
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - 30, tz), Name = $"{항목}", LabelStyle = NamePrintType.Down, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else if (번호 <= 48)
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X - 30, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Left, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y + 30, tz), Name = $"{항목}", LabelStyle = NamePrintType.Up, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });

                //if (항목.ToString().Substring(0, 2) == "T1" || 항목.ToString().Substring(0, 2) == "T4" || 항목.ToString().Substring(0, 2) == "T6")
                //    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y + 30, tz), Name = $"{항목}", LabelStyle = NamePrintType.Up, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                //else if (항목.ToString().Substring(0, 2) == "T2")
                //    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - 30, tz), Name = $"{항목}", LabelStyle = NamePrintType.Down, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                //else if (항목.ToString().Substring(0, 2) == "T7")
                //    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X - 30, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Left, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                //else if (항목.ToString().Substring(0, 2) == "T8")
                //    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X + 30, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Right, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                //else
                //    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Origin = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - 30, tz), Name = $"{항목}", LabelStyle = NamePrintType.Down, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
            }
            else if (항목.ToString().StartsWith("M"))
            {
                if (항목.ToString().Contains("X1"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Right, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                if (항목.ToString().Contains("Y2"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - InsItems.GetItem(항목.ToString()).FontSize, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                if (항목.ToString().Contains("X3"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Left, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                if (항목.ToString().Contains("Y4"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
            }
            else if (항목.ToString().StartsWith("B"))
            {
                if (항목.ToString().Contains("X"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y, tz + 10), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
                else if (항목.ToString().Contains("Y"))
                    InspItems.Add(new Label3D(항목) { Point = new Point3D(originX - InsItems.GetItem(항목.ToString()).X, originY - InsItems.GetItem(항목.ToString()).Y - 5, tz + 10), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = InsItems.GetItem(항목.ToString()).FontSize });
            }
            else if (항목.ToString().StartsWith("P"))
            {
                InspItems.Add(new Circle3D(항목) { Point = new Point3D(originX + 20, originY + 30, tz), Name = $"{항목}", LabelStyle = NamePrintType.Center, FontHeight = 5 });
            }
        }
        public override void InitModel(Hosts pcType)
        {
            if (MainModel == null) return;

            Rect3D r = MainModel.Bounds;
            Debug.WriteLine($"{r.SizeY}, {r.SizeX}, {r.SizeZ}", "Rectangle3D"); // 217, 562.16
            Double hx = r.SizeX / 2;
            Double hy = r.SizeY / 2;
            Double tz = 10;
            Double originX = -hx;
            Double originY = 0;

            AddText3D(new Point3D(-hx - 60, 0, 0), "L", 48, MajorColors.FrameColor);
            AddText3D(new Point3D(+hx + 60, 0, 0), "R", 48, MajorColors.FrameColor);
            AddText3D(new Point3D(hx - 100, hy + 120, 0), $"H : Hole", 24, Colors.Lime);
            AddText3D(new Point3D(hx - 100, hy + 100, 0), $"T : Trim", 24, Colors.RoyalBlue);
            AddText3D(new Point3D(hx - 100, hy + 80, 0), $"M : MICA", 24, Colors.Yellow);
            AddText3D(new Point3D(hx - 100, hy + 60, 0), $"F : Flatness", 24, Colors.Aqua);
            AddText3D(new Point3D(hx - 100, hy + 40, 0), $"B : Bolt", 24, Colors.Purple);
            AddText3D(new Point3D(hx - 100, hy + 20, 0), $"Thickness : Thickness", 24, Colors.Olive);
            AddArrowLine(new Point3D(-hx - 30, 0, tz), new Point3D(hx + 30, 0, tz), MajorColors.FrameColor);

            foreach (검사항목 항목 in Enum.GetValues(typeof(검사항목)))
            {
                ResultAttribute a = Utils.GetAttribute<ResultAttribute>(항목);

                if (pcType == Hosts.Server)
                    검사항목표시(항목, originX, originY, tz);
                else
                {
                    if (a.피씨구분 == pcType)
                        검사항목표시(항목, originX, originY, tz);
                }

            }
            InspItems.ForEach(e => e.Create(Children));
        }
        #endregion

        public virtual Color GetColor(결과구분 결과) => 결과 == 결과구분.OK ? MajorColors.GoodColor : MajorColors.BadColor;
        public void 검사항목표시변경()
        {
            foreach (Base3D 항목 in InspItems)
            {
                검사정보 정보 = Global.모델자료.선택모델.검사설정.GetItem(항목.Type);
                Remove(항목);

                if (정보.isShow == true)
                    Add(항목);
            }
        }
        public void SetResults(검사결과 결과)
        {
            foreach (Base3D 항목 in InspItems)
            {
                검사정보 정보 = 결과.GetItem(항목.Type);
                if (정보 == null)
                {
                    항목.Draw(항목, Decimal.MinValue, 결과구분.PS);
                    continue;
                }
                try
                {
                    검사정보 항목정보 = Global.모델자료.선택모델.검사설정.GetItem(항목.Type);
                    Remove(항목);

                    if (항목정보.isShow == true)
                        항목.Draw(항목, 정보.결과값, 정보.측정결과);
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }
            foreach (var item in SurfaceItems)
                Remove(item);
            foreach (var item in 결과.표면불량)
            {
                //카메라구분 카메라 = (카메라구분)item.장치구분;
                //변환정보 cf = 변환정보.Get(카메라);
                //Single w = item.가로길이 * cf.비율X;
                //Single h = item.세로길이 * cf.비율Y;
                //Single x = item.가로중심 * cf.비율X;
                //Single y = item.세로중심 * cf.비율Y;

                //Point2f c = RotateClockwise(new Point2f((Single)(x * cf.위치.X), (Single)(y * cf.위치.Y)), cf.각도);
                //RotatedRect r = new RotatedRect(c, new Size2f(w, h), 90 - item.회전각도);
                //List<Point3D> points = new List<Point3D>();
                //foreach (var p in r.Points()) points.Add(new Point3D(p.X, p.Y, cf.위치.Z));
                //SurfaceItems.Add(AddPolygon(points, SurfaceMaterial));
            }
        }
        public void SetFontSize(검사정보 정보)
        {
            foreach (Base3D 항목 in InspItems)
            {
                try
                {
                    if (항목.Name == 정보.검사명칭)
                    {
                        항목.Draw(항목, 정보.결과값, 정보.측정결과);
                    }

                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
            }
            foreach (var item in SurfaceItems)
                Remove(item);
            //foreach (var item in 결과.표면불량)
            //{
            //카메라구분 카메라 = (카메라구분)item.장치구분;
            //변환정보 cf = 변환정보.Get(카메라);
            //Single w = item.가로길이 * cf.비율X;
            //Single h = item.세로길이 * cf.비율Y;
            //Single x = item.가로중심 * cf.비율X;
            //Single y = item.세로중심 * cf.비율Y;

            //Point2f c = RotateClockwise(new Point2f((Single)(x * cf.위치.X), (Single)(y * cf.위치.Y)), cf.각도);
            //RotatedRect r = new RotatedRect(c, new Size2f(w, h), 90 - item.회전각도);
            //List<Point3D> points = new List<Point3D>();
            //foreach (var p in r.Points()) points.Add(new Point3D(p.X, p.Y, cf.위치.Z));
            //SurfaceItems.Add(AddPolygon(points, SurfaceMaterial));
            // }
        }
        // 점들을 중심을 기준으로 시계 방향으로 회전하는 함수
        private System.Drawing.PointF RotateClockwise(System.Drawing.PointF point, Double radian)
        {
            Single x = (Single)(point.X * Math.Cos(radian) - point.Y * Math.Sin(radian));
            Single y = (Single)(point.X * Math.Sin(radian) + point.Y * Math.Cos(radian));
            return new System.Drawing.PointF(x, y);
        }

        private class 변환정보
        {
            public Vector3D 위치 { get; set; }
            public Double 각도 { get; set; }
            public Single 비율Y { get; set; }
            public Single 비율X { get; set; }

            public static 변환정보 Get(카메라구분 카메라)
            {
                //if (카메라 == 카메라구분.Bottom) return new 변환정보() { 위치 = new Vector3D(1, -1, 7.8), 각도 = -Math.PI / 2, 비율X = 0.074155330f, 비율Y = 0.074995964f };
                return new 변환정보() { 위치 = new Vector3D(1, -1, 7.8), 각도 = -Math.PI / 2, 비율X = 0.074155330f, 비율Y = 0.074995964f };
            }
        }
    }
}
