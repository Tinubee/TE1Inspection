using Cognex.VisionPro.ToolBlock;
using System.Collections.Generic;
using System;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro;
using MvLibs;
using OpenCvSharp;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Linq;

namespace TE1.Cam05
{
    public class MainTools : BaseTool
    {
        public MainTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam05;

        public override void StartedRun()
        {
            base.StartedRun();
            Results = String.Empty;
        }

        public override void ModifyLastRunRecord(ICogRecord lastRecord)
        {
            base.ModifyLastRunRecord(lastRecord);
            ModifyRecords(lastRecord);
        }
        internal void AddRecord(ICogRecord lastRecord, ICogRecord currentRecord, String name)
        {
            if (!lastRecord.SubRecords.ContainsKey(name)) return;
            foreach (ICogRecord sr in lastRecord.SubRecords[name].SubRecords)
                currentRecord.SubRecords.Add(sr);
        }
        internal void ModifyRecords(ICogRecord lastRecord)
        {
            if (!lastRecord.SubRecords.ContainsKey(ViewerRecodName)) return;
            ICogRecord record = lastRecord.SubRecords[ViewerRecodName];

            if (String.IsNullOrEmpty(Results)) return;

            try
            {
                List<DisplayResult> results = JsonConvert.DeserializeObject<List<DisplayResult>>(Results);
                //foreach (DisplayResult r in results.Where(r => r.KeyName == DefectsName).ToList())
                //{
                //    var rect = new CogRectangleAffine() { CenterX = r.Rect[0], CenterY = r.Rect[1], SideXLength = r.Rect[2], SideYLength = r.Rect[3], Rotation = r.Rect[4], Color = r.Color, TipText = r.Display, LineWidthInScreenPixels = 2 };
                //    ToolBlock.AddGraphicToRunRecord(rect, lastRecord, ViewerRecodName, r.Display);
                //    //var label = new CogGraphicLabel() { Text = r.Display, TipText = r.Display, X = r.Rect[0], Y = r.Rect[1], Alignment = CogGraphicLabelAlignmentConstants.BaselineCenter };
                //    //ToolBlock.AddGraphicToRunRecord(label, lastRecord, ViewerRecodName, r.Display);
                //}
                AddDefectsGraphics(lastRecord, results);

                Dictionary<String, CogGraphicLabel> labels = new Dictionary<String, CogGraphicLabel>();
                foreach (ICogRecord rcd in record.SubRecords)
                {
                    if (rcd.ContentType == typeof(CogGraphicLabel))
                    {
                        CogGraphicLabel label = rcd.Content as CogGraphicLabel;
                        labels.Add(label.Text, label);
                    }
                }
                foreach (DisplayResult r in results.Where(r => r.KeyName.StartsWith("THK")).ToList())
                {
                    String name = r.KeyName.Replace("THK", "");
                    if (labels.ContainsKey(name))
                    {
                        CogGraphicLabel label = labels[name];
                        label.Text = $"{name}: {r.Display}";
                        label.Color = r.Color;
                        label.X += 200;
                        label.Alignment = CogGraphicLabelAlignmentConstants.BaselineLeft;
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally { Results = String.Empty; }
        }
    }

    public class AlignTools : BaseTool
    {
        public AlignTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam05;

        internal const Double LengthBC = 973.5;
        internal Double CalibX => Input<Double>("CalibX");
        internal Double CalibY => Input<Double>("CalibY");
        internal Boolean Detected { get => Output<Boolean>("Detected"); set => Output("Detected", value); }
        internal CogFixtureTool Fixture => GetTool("Fixture") as CogFixtureTool;
        internal CogTransform2DLinear Transform => Fixture.RunParams.UnfixturedFromFixturedTransform as CogTransform2DLinear;

        internal CogFindCircleTool CircleB => GetTool("CircleB") as CogFindCircleTool;
        internal CogFindLineTool LineC => GetTool("LineC") as CogFindLineTool;
        internal CogFindLineTool LineS => GetTool("LineS") as CogFindLineTool;
        internal CogCreateLineParallelTool LineBC => GetTool("LineBC") as CogCreateLineParallelTool;
        internal CogCreateLineParallelTool LineB => GetTool("LineB") as CogCreateLineParallelTool;
        internal CogCreateLineParallelTool LineT => GetTool("LineT") as CogCreateLineParallelTool;
        internal CogCreateLineParallelTool LineV => GetTool("LineV") as CogCreateLineParallelTool;
        internal CogIntersectSegmentLineTool OriginPoint => GetTool("Origin") as CogIntersectSegmentLineTool;
        internal CogIntersectLineLineTool Origin => GetTool("Origin") as CogIntersectLineLineTool;

        //CogIntersectSegmentLineTool1
        public override void AfterToolRun(ICogTool tool, CogToolResultConstants result)
        {
            //Debug.WriteLine($"{tool.Name}");
            base.AfterToolRun(tool, result);
            if (result != CogToolResultConstants.Accept) return;
            if (tool == LineBC) SetOriginY();
            else if (tool == OriginPoint) SetOriginX();
        }

        public override void FinistedRun()
        {
            base.FinistedRun();
            Output("CalibX", CalibX);
            Output("CalibY", CalibY);
        }

        internal virtual void SetOriginX()
        {
            Output("CenterX", OriginPoint.X);
            Output("CenterY", OriginPoint.Y);
        }

        internal virtual void SetOriginY()
        {
            Double length = -63.0 / CalibX;
            Point2d p = Base.CalculatePoint(new Point2d(CircleB.Results.GetCircle().CenterX, CircleB.Results.GetCircle().CenterY), length, LineS.Results.GetLine().Rotation);
            LineB.X = p.X;
            LineB.Y = p.Y;
        }

        internal virtual void SetFixture() { }
    }
    public class DetectTools : BaseTool
    {
        public DetectTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam05;
        internal Double CalibX => Input<Double>("CalibX");
        internal Double CalibY => Input<Double>("CalibY");

        public override void StartedRun()
        {
            base.StartedRun();
            InitTools();
        }

        public override void FinistedRun()
        {
            base.FinistedRun();
            CalResults();
        }

        private Double CenterX => InputImage.Width / 2 - Input<Double>("CenterX");
        internal virtual void InitTools()
        {
            CogCreateLineTool centerV = GetTool("CenterV") as CogCreateLineTool;
            if (centerV != null) centerV.Line.X = CenterX;

            Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);
            foreach (var item in items)
            {
                if (item.Value.InsType == InsType.H)
                {

                }
                if (item.Value.InsType == InsType.S)
                {
                    for (Int32 lop = 1; lop <= 4; lop++)
                    {
                        CogCaliperTool tool;
                        if (ToolBlock.Tools.Contains($"{item.Key}_{lop}")) tool = GetTool($"{item.Key}_{lop}") as CogCaliperTool;
                        else
                        {
                            tool = new CogCaliperTool();
                            tool.Name = $"{item.Key}_{lop}"; ;
                            this.ToolBlock.Tools.Add(tool);
                        }
                        SetMicaCaliper(tool, item.Value, lop);
                    }
                }
                if (item.Value.InsType == InsType.X || item.Value.InsType == InsType.Y)
                {

                }
            }
        }

        internal virtual void SetCircle(CogFindCircleTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;
            tool.RunParams.ExpectedCircularArc.CenterX = x;
            tool.RunParams.ExpectedCircularArc.CenterY = y;
            tool.RunParams.ExpectedCircularArc.Radius = r;
            tool.RunParams.CaliperSearchLength = 100;
            tool.RunParams.CaliperProjectionLength = 10;
            tool.RunParams.CaliperSearchDirection = CogFindCircleSearchDirectionConstants.Outward;
            tool.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
            tool.RunParams.CaliperRunParams.ContrastThreshold = 8;
            tool.RunParams.CaliperRunParams.FilterHalfSizeInPixels = 2;
        }
        internal virtual void SetRectangle(CogToolBlock tool, InsItem p)
        {
            //Debug.WriteLine("setrectangle");
            //if (tool == null) return;
            //Base.Input(tool, "X", -p.Y / CalibX);
            //Base.Input(tool, "Y", -p.X / CalibY);
        }

        internal virtual void SetMicaCaliper(CogCaliperTool tool, InsItem p, Int32 lop)
        {
            Double x = p.X / CalibX;
            Double y = p.Y / CalibY;
            switch (lop)
            {
                case 1:
                    //tool.Region.CenterX = x + (p.D / 2 / CalibX);
                    //tool.Region.CenterX = x;
                    tool.Region.CenterY = y;
                    break;
                case 2:
                    tool.Region.CenterX = x;
                    //tool.Region.CenterY = y;
                    //tool.Region.CenterY = y - (p.H / 2 / CalibY);
                    break;
                case 3:
                    //tool.Region.CenterX = x - (p.D / 2 / CalibX);
                    //tool.Region.CenterX = x;
                    tool.Region.CenterY = y;
                    break;
                case 4:
                    tool.Region.CenterX = x;
                    //tool.Region.CenterY = y;
                    //tool.Region.CenterY = y + (p.H / 2 / CalibY);
                    break;
            }

            //1,2,3,4
            //tool.Region.SideXLength = 160;
            //tool.Region.SideYLength = 50;
            tool.Region.SideXLength = 150;//lop%2 != 1 ? 50 : 160;
            tool.Region.SideYLength = 50;//lop % 2 != 1 ? 160 : 50;
            tool.Region.Rotation = lop % 2 == 1 ? 0 : - Math.PI / 2;


            tool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge;
            //tool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight;
            tool.RunParams.ContrastThreshold = 10;
            tool.RunParams.FilterHalfSizeInPixels = 5;
        }

        internal virtual void SetCaliper(CogCaliperTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            tool.Region.CenterX = x;
            tool.Region.CenterY = y;
            tool.Region.SideXLength = 160;
            tool.Region.SideYLength = 50;
            if (p.InsType == InsType.X)
            {
                tool.Region.Rotation = Math.PI / 2;
            }
            else
            {
                tool.Region.Rotation = -Math.PI;
            }

            tool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge;
            tool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight;
            tool.RunParams.ContrastThreshold = 10;
        }

        internal virtual void CalResults()
        {
            List<Result> results = new List<Result>();
            Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);
            foreach (var item in items)
            {
                if (item.Value.InsType == InsType.H)
                {

                }
                else if (item.Value.InsType == InsType.R)
                {

                }
                else if (item.Value.InsType == InsType.X || item.Value.InsType == InsType.Y)
                {

                }
                else if (item.Value.InsType == InsType.S)
                {
                    for (Int32 lop = 1; lop <= 4; lop++)
                    {
                        if (CalResult(GetTool($"{item.Key}_{lop}") as CogCaliperTool, item.Value, out InsItem r))
                        {
                            results.Add(new Result($"{item.Key}_{lop}", r.D));
                        }
                    }
                }
            }
            //Debug.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
            Output("Results", JsonConvert.SerializeObject(results));
        }

        internal virtual Boolean CalResult(CogFindCircleTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept)
            {
                CogCircle c = tool.Results.GetCircle();
                result.X = -Math.Round(c.CenterY, 3);
                result.Y = -Math.Round(c.CenterX, 3);
                result.D = Math.Round(c.Radius * 2, 3);
            }
            return true;
        }

        internal virtual Boolean CalResult(CogCaliperTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept)
            {
                CogCaliperResult r = tool.Results[0];
                if (ins.InsType == InsType.S)
                {
                    result.D = r.PositionY;//Math.Abs(Math.Round(r.PositionY - Math.Abs(ins.X) / CalibY, 3));
                    Debug.WriteLine($"{tool.Name} : X : {r.PositionY} / ins.X : {ins.X} /  CalibX : {Math.Abs(ins.X) / CalibY}");
                }
                //else if (ins.InsType == InsType.Y)
                //{
                //    result.D = r.PositionX;//Math.Abs(Math.Round(r.PositionX - Math.Abs(ins.Y) / CalibX, 3));
                //    Debug.WriteLine($"{tool.Name} : X : {r.PositionX} / ins.Y : {ins.Y} /  CalibX : {Math.Abs(ins.Y) / CalibX}");
                //}

            }
            return true;
        }

        internal virtual Boolean CalResult(CogToolBlock tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept)
            {
                result.X = -Math.Round(Base.Output<Double>(tool, "X"), 3);
                result.Y = -Math.Round(Base.Output<Double>(tool, "Y"), 3);
                //result.H = Math.Round(Base.Output<Double>(tool, "Width"), 3);
                //result.D = Math.Round(Base.Output<Double>(tool, "Height"), 3);
            }
            return true;
        }
    }
}
