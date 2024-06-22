using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.ToolBlock;
using MvLibs;
using System;

namespace TE1
{
    public class DefaultAlignTools : BaseTool
    {
        public DefaultAlignTools(CogToolBlock tool) : base(tool) { }
        public override void StartedRun()
        {
            base.StartedRun();
            Detected = false;
        }
        public override void BeforeToolRun(ICogTool tool)
        {
            if (tool != Fixture) return;
            CalFixture();
        }
        public override void AfterToolRun(ICogTool tool, CogToolResultConstants result)
        {
            if (tool != Fixture) return;
            CalPerspective();
            CalResolution();
        }
        public override void FinistedRun()
        {
            base.FinistedRun();
            RPT?.Dispose();
        }

        internal CogFixtureNPointToNPointTool Fixture => GetTool("Fixture") as CogFixtureNPointToNPointTool;
        internal CogTransform2DLinear Transform => Fixture.RunParams.RawFixturedFromFixturedTransform as CogTransform2DLinear;
        internal CogIntersectLineLineTool PointLT => GetTool("PointLT") as CogIntersectLineLineTool;
        internal CogIntersectLineLineTool PointRT => GetTool("PointRT") as CogIntersectLineLineTool;
        internal CogIntersectLineLineTool PointLB => GetTool("PointLB") as CogIntersectLineLineTool;
        internal CogIntersectLineLineTool PointRB => GetTool("PointRB") as CogIntersectLineLineTool;

        internal Double Width => Input<Double>("Width");
        internal Double Height => Input<Double>("Height");
        internal Double CalibX => Input<Double>("CalibX");
        internal Double CalibY => Input<Double>("CalibY");
        internal Double OriginX { get => Input<Double>("OriginX"); set => Input("OriginX", value); }// 마스터 기준 Transform 원점 X
        internal Double OriginY { get => Input<Double>("OriginY"); set => Input("OriginY", value); }// 마스터 기준 Transform 원점 Y
        internal String Perspective { get => Output<String>(PerspectiveName); set => Output(PerspectiveName, value); }
        internal Boolean Detected { get => Output<Boolean>("Detected"); set => Output("Detected", value); }
        internal RectanglePerspectiveTransform RPT = null;

        internal virtual void CalFixture()
        {
            if (InputImage == null) return;
            RPT = new RectanglePerspectiveTransform()
            {
                CalibX = CalibX,
                CalibY = CalibY,
                RealWidth = Width,
                RealHeight = Height,
            };

            Detected =
                PointLT.RunStatus.Result == CogToolResultConstants.Accept &&
                PointRT.RunStatus.Result == CogToolResultConstants.Accept &&
                PointLB.RunStatus.Result == CogToolResultConstants.Accept &&
                PointRB.RunStatus.Result == CogToolResultConstants.Accept;

            Transform.Rotation = 0;
            Transform.Skew = 0;
            if (Detected)
            {
                RectanglePoints origins = new RectanglePoints(
                    new PointD(PointLT.X, PointLT.Y),
                    new PointD(PointRT.X, PointRT.Y),
                    new PointD(PointLB.X, PointLB.Y),
                    new PointD(PointRB.X, PointRB.Y)
                );
                RPT.CreateMatrix(origins, out PointD origin);
                //Debug.WriteLine(origin, "Origin");
                Fixture.RunParams.SetUnfixturedPoint(0, RPT.Origins.LT.X, RPT.Origins.LT.Y);
                Fixture.RunParams.SetUnfixturedPoint(1, RPT.Origins.RT.X, RPT.Origins.RT.Y);
                Fixture.RunParams.SetUnfixturedPoint(2, RPT.Origins.LB.X, RPT.Origins.LB.Y);
                Fixture.RunParams.SetUnfixturedPoint(3, RPT.Origins.RB.X, RPT.Origins.RB.Y);
                Fixture.RunParams.SetRawFixturedPoint(0, RPT.Norminal.LT.X, RPT.Norminal.LT.Y);
                Fixture.RunParams.SetRawFixturedPoint(1, RPT.Norminal.RT.X, RPT.Norminal.RT.Y);
                Fixture.RunParams.SetRawFixturedPoint(2, RPT.Norminal.LB.X, RPT.Norminal.LB.Y);
                Fixture.RunParams.SetRawFixturedPoint(3, RPT.Norminal.RB.X, RPT.Norminal.RB.Y);
                Transform.TranslationX = origin.X;
                Transform.TranslationY = origin.Y;

                OriginX = Transform.TranslationX;
                OriginY = Transform.TranslationY;
                Perspective = RPT.ToJson();
            }
            else
            {
                RPT.CreateMatrix();
                Transform.TranslationX = OriginX;
                Transform.TranslationY = OriginY;
            }
        }

        internal virtual void CalPerspective()
        {
            if (Detected)
            {
                RPT.Fixture(InputImage, Fixture.RunParams.FixturedSpaceName);
                Perspective = RPT.ToJson();
                //Debug.WriteLine($"Fixtured Center => {RPT.TransForward(0, 0).ToString()}", Camera.ToString());
            }
            SetSegments();
        }
        internal virtual void SetSegments()
        {
            CogToolBlock OutLines = GetTool("OutLines") as CogToolBlock;
            if (OutLines == null) return;
            //if (ToolBlock.DisabledTools.Contains(OutLines.Name)) return;
            SetPosition("SegmentL", RPT.Destination.LT, RPT.Destination.LB);
            SetPosition("SegmentR", RPT.Destination.RT, RPT.Destination.RB);
            SetPosition("SegmentT", RPT.Destination.LT, RPT.Destination.RT);
            SetPosition("SegmentB", RPT.Destination.LB, RPT.Destination.RB);
            SetPosition("NorminalL", RPT.Norminal.LT, RPT.Norminal.LB);
            SetPosition("NorminalR", RPT.Norminal.RT, RPT.Norminal.RB);
            SetPosition("NorminalT", RPT.Norminal.LT, RPT.Norminal.RT);
            SetPosition("NorminalB", RPT.Norminal.LB, RPT.Norminal.RB);
        }
        internal virtual void SetPosition(String name, PointD start, PointD end)
        {
            CogToolBlock OutLines = GetTool("OutLines") as CogToolBlock;
            CogCreateSegmentTool tool = Base.GetTool(OutLines, name) as CogCreateSegmentTool;
            if (tool == null) return;
            tool.Segment.StartX = start.X;
            tool.Segment.StartY = start.Y;
            tool.Segment.EndX = end.X;
            tool.Segment.EndY = end.Y;
            if (tool.Name.StartsWith("Norminal")) tool.OutputColor = CogColorConstants.Cyan;
            else tool.OutputColor = CogColorConstants.Magenta;
        }
        internal virtual void CalResolution()
        {
            if (!Detected) return;
            RectanglePoints dst = new RectanglePoints(
                new PointD(PointLT.X, PointLT.Y),
                new PointD(PointRT.X, PointRT.Y),
                new PointD(PointLB.X, PointLB.Y),
                new PointD(PointRB.X, PointRB.Y));
            Double resolutionX = Math.Round(Width / dst.Width() * 1000, 9);
            Double resolutionY = Math.Round(Height / dst.Height() * 1000, 9);
            //Debug.WriteLine($"Resolution X({CalibX * 1000} => {resolutionX}), Y({CalibY * 1000} => {resolutionY})", Camera.ToString());
            Output("ResolutionX", resolutionX);
            Output("ResolutionY", resolutionY);
        }
    }
}
