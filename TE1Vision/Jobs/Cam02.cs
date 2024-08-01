using Cognex.VisionPro;
using Cognex.VisionPro.Blob;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.ToolBlock;
using MvLibs;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Shapes;

namespace TE1.Cam02
{
    public class MainTools : BaseTool
    {
        public MainTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam02;

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
        public override Cameras Camera => Cameras.Cam02;

        internal const Double LengthBC = 973.5;
        internal Double CalibX => Input<Double>("CalibX");
        internal Double CalibY => Input<Double>("CalibY");
        internal Boolean Detected { get => Output<Boolean>("Detected"); set => Output("Detected", value); }
        internal CogFixtureTool Fixture => GetTool("Fixture") as CogFixtureTool;
        internal CogTransform2DLinear Transform => Fixture.RunParams.UnfixturedFromFixturedTransform as CogTransform2DLinear;

        internal CogFindCircleTool CircleB => GetTool("CircleB") as CogFindCircleTool;
        internal CogFindCircleTool CircleC => GetTool("CircleC") as CogFindCircleTool;
        internal CogCreateSegmentTool LineBC => GetTool("LineBC") as CogCreateSegmentTool;
        internal CogCreateLinePerpendicularTool LineB => GetTool("LineB") as CogCreateLinePerpendicularTool;
        internal CogCreateLinePerpendicularTool LineT => GetTool("LineT") as CogCreateLinePerpendicularTool;
        internal CogCreateLineParallelTool LineV => GetTool("LineV") as CogCreateLineParallelTool;
        internal CogIntersectLineLineTool Origin => GetTool("Origin") as CogIntersectLineLineTool;

        public override void AfterToolRun(ICogTool tool, CogToolResultConstants result)
        {
            base.AfterToolRun(tool, result);
            if (result != CogToolResultConstants.Accept) return;
            if (tool == LineB) SetOriginX();
            else if (tool == LineV) SetOriginY();
            else if (tool.Name.Contains("H"))
            {
                //Debug.WriteLine($"{tool.Name} Run");
            }
        }

        public override void FinistedRun()
        {
            base.FinistedRun();
            Double y = Math.Round(LengthBC / LineBC.Segment.Length * 1000, 9);
            Debug.WriteLine($"{LengthBC} / {Math.Round(LineBC.Segment.Length, 2)} = {y}", "DatumBC");
            Debug.WriteLine($"CalibX => {CalibX} , CalibY => {CalibY}", "Calibration Value");
            Output("CalibX", CalibX);
            Output("CalibY", CalibY);
        }

        internal virtual void SetOriginX()
        {
            Double length = -63.0 / CalibX;
            Point2d p = Base.CalculatePoint(new Point2d(LineBC.Segment.StartX, LineBC.Segment.StartY), length, LineB.GetOutputLine().Rotation);
            LineV.X = p.X;
            LineV.Y = p.Y;
            //LineV.Line.Rotation = p.
        }

        internal virtual void SetOriginY()
        {
            Double length = 982.25 / CalibY;
            Point2d p = Base.CalculatePoint(new Point2d(LineV.X, LineV.Y), length, LineV.GetOutputLine().Rotation);
            LineT.X = p.X;
            LineT.Y = p.Y;
            Transform.TranslationX = p.X;
            Transform.TranslationY = p.Y;
            Transform.Rotation = LineBC.Segment.Rotation + Math.PI / 2;
            Output("CenterX", p.X);
            Output("CenterY", p.Y);
            Output("Rotation", Transform.Rotation);
        }

        internal virtual void SetFixture() { }
    }

    public class DetectTools : BaseTool
    {
        public DetectTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam02;
        internal Double CalibX => Input<Double>("CalibX");
        internal Double CalibY => Input<Double>("CalibY");

        public override void AfterToolRun(ICogTool tool, CogToolResultConstants result)
        {
            base.AfterToolRun(tool, result);
            if (result != CogToolResultConstants.Accept) return;
            else if (tool.Name.Contains("H"))
            {
                //Debug.WriteLine($"{tool.Name} Run");
            }
        }

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
                    CogFindCircleTool tool;
                    CogBlobTool blobTool;
                    CogFindCircleTool BurrTool;

                    if (ToolBlock.Tools.Contains(item.Key)) tool = GetTool(item.Key) as CogFindCircleTool;
                    else
                    {
                        tool = new CogFindCircleTool();
                        tool.Name = item.Key;
                        this.ToolBlock.Tools.Add(tool);
                    }
                    SetCircle(tool, item.Value);

                    if (item.Key != "H38")
                    {
                        String blobToolName = $"{item.Key}B";
                        String BurrToolName = $"{item.Key}Burr";

                        blobTool = GetOrCreateTool(blobToolName);
                        SetBlob(blobTool, item.Value);

                        if (ToolBlock.Tools.Contains(BurrToolName)) BurrTool = GetTool(BurrToolName) as CogFindCircleTool;
                        else
                        {
                            BurrTool = new CogFindCircleTool();
                            BurrTool.Name = BurrToolName;
                            this.ToolBlock.Tools.Add(BurrTool);
                        }
                        SetBurr(BurrTool, item.Value);
                    }
                }
                if (item.Value.InsType == InsType.R) SetRectangle(GetTool(item.Key) as CogToolBlock, item.Value);
                else if (item.Value.InsType == InsType.X || item.Value.InsType == InsType.Y)
                {
                    CogCaliperTool tool;
                    if (ToolBlock.Tools.Contains(item.Key)) tool = GetTool(item.Key) as CogCaliperTool;
                    else
                    {
                        tool = new CogCaliperTool();
                        tool.Name = item.Key;
                        this.ToolBlock.Tools.Add(tool);
                    }
                    SetCaliper(tool, item.Value);
                }
            }
        }
        private CogBlobTool GetOrCreateTool(string toolName)
        {
            CogBlobTool tool;
            if (ToolBlock.Tools.Contains(toolName)) tool = GetTool(toolName) as CogBlobTool;
            else
            {
                tool = new CogBlobTool();
                tool.Name = toolName;
                this.ToolBlock.Tools.Add(tool);
            }
            return tool;
        }
        internal virtual void SetRectangle(CogToolBlock tool, InsItem p)
        {
            if (tool == null) return;
            Base.Input(tool, "X", -p.Y / CalibX);
            Base.Input(tool, "Y", -p.X / CalibY);
        }
        internal virtual void SetBurr(CogFindCircleTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;

            tool.RunParams.ExpectedCircularArc.CenterX = x;
            tool.RunParams.ExpectedCircularArc.CenterY = y;
            tool.RunParams.ExpectedCircularArc.Radius = r;
            tool.RunParams.ExpectedCircularArc.AngleStart = 0;
            tool.RunParams.ExpectedCircularArc.AngleSpan = 360;

            tool.RunParams.CaliperSearchLength = 150;
            tool.RunParams.CaliperProjectionLength = 10;
            tool.RunParams.CaliperSearchDirection = CogFindCircleSearchDirectionConstants.Outward;
            tool.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
            tool.RunParams.CaliperRunParams.ContrastThreshold = 10;
            tool.RunParams.CaliperRunParams.FilterHalfSizeInPixels = 5;

            tool.RunParams.NumCalipers = 8;
            tool.InputImage = (CogImage8Grey)this.InputImage;
        }
        internal virtual void SetBlob(CogBlobTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;

            tool.RunParams.RegionMode = CogRegionModeConstants.PixelAlignedBoundingBoxAdjustMask;
            CogRectangleAffine rect = new CogRectangleAffine();

            rect.CenterX = x;
            rect.CenterY = y;
            rect.SideXLength = 500;
            rect.SideYLength = 500;

            tool.Region = rect;
            tool.RunParams.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardFixedThreshold;
            tool.RunParams.SegmentationParams.HardFixedThreshold = 40;
            tool.RunParams.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.LightBlobs;
            tool.RunParams.ConnectivityMinPixels = 10000;

            CogBlobMeasure item = new CogBlobMeasure();
            item.FilterMode = CogBlobFilterModeConstants.IncludeBlobsInRange;
            item.FilterRangeLow = 1;
            item.FilterRangeHigh = 1.03;
            item.Measure = CogBlobMeasureConstants.AcircularityRms;
            item.Mode = CogBlobMeasureModeConstants.Filter;

            tool.RunParams.RunTimeMeasures.Clear();
            tool.RunParams.RunTimeMeasures.Add(item);

            tool.InputImage = this.InputImage;
        }

        internal virtual void SetCircle(CogFindCircleTool tool, InsItem p, String name = null)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;

            tool.RunParams.ExpectedCircularArc.CenterX = x;
            tool.RunParams.ExpectedCircularArc.CenterY = y;
            tool.RunParams.ExpectedCircularArc.Radius = r;

            tool.RunParams.CaliperSearchLength = 150;
            tool.RunParams.CaliperProjectionLength = 10;
            tool.RunParams.CaliperSearchDirection = CogFindCircleSearchDirectionConstants.Outward;
            tool.RunParams.CaliperRunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
            tool.RunParams.CaliperRunParams.ContrastThreshold = 10;
            tool.RunParams.CaliperRunParams.FilterHalfSizeInPixels = 5;

            tool.RunParams.NumCalipers = 15;
            //tool.InputImage = (CogImage8Grey)this.InputImage;
        }

        internal virtual void SetCaliper(CogCaliperTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            tool.Region.CenterX = x;
            tool.Region.CenterY = y;
            tool.Region.SideXLength = 200;
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
        }

        internal virtual void CalResults()
        {
            List<Result> results = new List<Result>();
            Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);
            foreach (var item in items)
            {
                if (item.Value.InsType == InsType.H)
                {
                    if (item.Key == "H38")
                    {
                        if (CalResult(GetTool(item.Key) as CogFindCircleTool, item.Value, out InsItem r))
                        {
                            results.Add(new Result(item.Key + "X", item.Value.X));
                            results.Add(new Result(item.Key + "Y", item.Value.Y));
                            results.Add(new Result(item.Key + "D", r.D));
                            results.Add(new Result(item.Key + "P", r.L));
                        }
                        continue;
                    }

                    if (item.Key + "B" != "H38B")
                    {
                        CogBlobTool tool = GetTool(item.Key + "B") as CogBlobTool;
                        Int32 count = tool.Results.GetBlobs().Count;

                        if (count != 1)
                        {
                            results.Add(new Result(item.Key + "X", 0));
                            results.Add(new Result(item.Key + "Y", 0));
                            results.Add(new Result(item.Key + "D", 0));
                            results.Add(new Result(item.Key + "P", 0));
                        }
                        else
                        {
                            CogFindCircleTool circleTool = GetTool(item.Key) as CogFindCircleTool;
                            if (circleTool.Results.Count != 0)
                            {
                                Debug.WriteLine($"{item.Key}");
                                CogFindCircleTool burrTool = GetTool(item.Key + "Burr") as CogFindCircleTool;

                                for (Int32 lop = 0; lop < burrTool.Results.Count; lop++)
                                {
                                    CogDistancePointPointTool distancetool = new CogDistancePointPointTool();
                                    distancetool.InputImage = this.InputImage;
                                    distancetool.StartX = circleTool.Results.GetCircle().CenterX;
                                    distancetool.StartY = circleTool.Results.GetCircle().CenterY;
                                    distancetool.EndX = burrTool.Results[lop].X;
                                    distancetool.EndY = burrTool.Results[lop].Y;
                                    distancetool.Run();

                                    results.Add(new Result(item.Key + $"Burr0{lop + 1}", Math.Round(distancetool.Distance, 3)));

                                    //if (lop == burrTool.Results.Count - 1) 
                                    //{
                                    //    results.Add(new Result(item.Key + $"Burr", 0));
                                    //}
                                }
                            }
                            if (CalResult(circleTool, item.Value, out InsItem r))
                            {
                                if (item.Key == "H37")
                                {
                                    results.Add(new Result(item.Key + "X", item.Value.X));
                                    results.Add(new Result(item.Key + "Y", item.Value.Y));
                                }
                                else
                                {
                                    results.Add(new Result(item.Key + "X", r.X));
                                    results.Add(new Result(item.Key + "Y", r.Y));
                                }
                       
                                results.Add(new Result(item.Key + "D", r.D));
                                results.Add(new Result(item.Key + "P", r.L));
                            }
                        }
                    }
                }
                else if (item.Value.InsType == InsType.R)
                {
                    if (CalResult(GetTool(item.Key) as CogToolBlock, item.Value, out InsItem r))
                    {
                        results.Add(new Result(item.Key + "X", r.X));
                        results.Add(new Result(item.Key + "Y", r.Y));
                        results.Add(new Result(item.Key + "W", r.D));
                        results.Add(new Result(item.Key + "L", r.H));
                    }
                }
                else if (item.Value.InsType == InsType.X || item.Value.InsType == InsType.Y)
                {
                    if (CalResult(GetTool(item.Key) as CogCaliperTool, item.Value, out InsItem r))
                        results.Add(new Result(item.Key, r.D));
                }
                else if (item.Value.InsType == InsType.S)
                {

                }
            }
            //Debug.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
            Output("Results", JsonConvert.SerializeObject(results));
        }
        internal virtual Boolean CalResult(CogToolBlock tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept)
            {
                result.X = -Math.Round(Base.Output<Double>(tool, "Y"), 3);
                result.Y = -Math.Round(Base.Output<Double>(tool, "X"), 3);
                result.H = Math.Round(Base.Output<Double>(tool, "Width"), 3);
                result.D = Math.Round(Base.Output<Double>(tool, "Height"), 3);
            }
            return true;
        }
        internal virtual Boolean CalResult(CogFindCircleTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept && tool.Results.GetCircle() != null)
            {
                CogCircle c = tool.Results.GetCircle();
                result.X = -Math.Round(c.CenterY, 3);
                result.Y = -Math.Round(c.CenterX, 3);

                result.D = Math.Round(c.Radius * 2, 3);
                result.L = 0;
            }
            else
            {
                result.X = 0;
                result.Y = 0;
                result.D = 0;
                result.L = 0;
            }
            return true;
        }
        public CogBlobResult GetBlob(CogBlobTool tool, Int32 Idx = 0)
        {
            if (tool.RunStatus.Result != CogToolResultConstants.Accept || tool.Results == null) return null;
            CogBlobResultCollection Blobs = tool.Results.GetBlobs();
            if (Blobs == null || Blobs.Count < 1) return null;
            if (Idx == 0) return Blobs[0];
            if (Idx < 0) return Blobs[Blobs.Count - 1];
            if (Idx >= Blobs.Count) Idx = Blobs.Count - 1;
            return Blobs[Idx];
        }

        public CogCircle GetCircleRegion(CogFindCircleTool tool)
        {
            if (tool.Results.Count == 0) return null;

            return tool.Results.GetCircle();
        }

        public void SetCircleRegion(CogBlobTool tool, CogCircle region)
        {
            tool.Region = region;
            //if (tool.Results.Count == 0) return null;

            //return tool.Results.GetCircle();
        }

        internal virtual Boolean CalResult(CogCaliperTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;

            if (tool.RunStatus.Result == CogToolResultConstants.Accept && tool.Results.Count != 0)
            {
                CogCaliperResult r = tool.Results[0];
                if (ins.InsType == InsType.X)
                    result.D = r.PositionY;
                else if (ins.InsType == InsType.Y)
                    result.D = r.PositionX;
            }
            else
            {
                result.D = 0;
            }
            return true;
        }
    }
}
