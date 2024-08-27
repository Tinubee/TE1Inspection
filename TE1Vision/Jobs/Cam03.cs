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
using Cognex.VisionPro.PMAlign;
using System.IO;
using Cognex.VisionPro.Blob;

namespace TE1.Cam03
{
    public class MainTools : BaseTool
    {
        public MainTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam03;

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
        public override Cameras Camera => Cameras.Cam03;

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
        }

        public override void FinistedRun()
        {
            base.FinistedRun();
            Double y = Math.Round(LengthBC / LineBC.Segment.Length * 1000, 9);
            Debug.WriteLine($"{LengthBC} / {Math.Round(LineBC.Segment.Length, 2)} = {y}", "DatumBC");
            Output("CalibX", CalibX);
            Output("CalibY", CalibY);
        }

        internal virtual void SetOriginX()
        {
            Double length = -63.0 / CalibX;
            Point2d p = Base.CalculatePoint(new Point2d(LineBC.Segment.StartX, LineBC.Segment.StartY), length, LineB.GetOutputLine().Rotation);
            LineV.X = p.X;
            LineV.Y = p.Y;
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
        public override Cameras Camera => Cameras.Cam03;
        internal Double CalibX => Input<Double>("CalibX");
        internal Double CalibY => Input<Double>("CalibY");
        internal CogPMAlignTool BTTrain => GetTool("BTTrain") as CogPMAlignTool;
        internal CogPMAlignTool BBTrain => GetTool("BBTrain") as CogPMAlignTool;

        internal CogFindCircleTool BT => GetTool("BT") as CogFindCircleTool;
        internal CogFindCircleTool BB => GetTool("BB") as CogFindCircleTool;
        public override void AfterToolRun(ICogTool tool, CogToolResultConstants result)
        {
            base.AfterToolRun(tool, result);
            if (result != CogToolResultConstants.Accept) return;
            if (tool == BTTrain)
            {
                BT.RunParams.ExpectedCircularArc.CenterX = BTTrain.Results.Count > 0 ? BTTrain.Results[0].GetPose().TranslationX : 0;
                BT.RunParams.ExpectedCircularArc.CenterY = BTTrain.Results.Count > 0 ? BTTrain.Results[0].GetPose().TranslationY : 0;
            }
            else if (tool == BBTrain)
            {
                BB.RunParams.ExpectedCircularArc.CenterX = BBTrain.Results.Count > 0 ? BBTrain.Results[0].GetPose().TranslationX : 0;
                BB.RunParams.ExpectedCircularArc.CenterY = BBTrain.Results.Count > 0 ? BBTrain.Results[0].GetPose().TranslationY : 0;
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

            //InsItems.LoadMica();  //위치 Setting을 위한 임시 생성.
            Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);

            foreach (var item in items)
            {
                if (item.Value.InsType == InsType.H)
                {
                    CogFindCircleTool tool;
                    if (ToolBlock.Tools.Contains(item.Key)) tool = GetTool(item.Key) as CogFindCircleTool;
                    else
                    {
                        tool = new CogFindCircleTool();
                        tool.Name = item.Key;
                        this.ToolBlock.Tools.Add(tool);
                    }
                    SetCircle(tool, item.Value);
                }
                if (item.Value.InsType == InsType.S)
                {
                    CogCaliperTool tool;
                    if (ToolBlock.Tools.Contains(item.Key)) tool = GetTool(item.Key) as CogCaliperTool;
                    else
                    {
                        tool = new CogCaliperTool();
                        tool.Name = item.Key;
                        this.ToolBlock.Tools.Add(tool);
                    }
                    SetMicaCaliper(tool, item.Value);
                }
                if (item.Value.InsType == InsType.X || item.Value.InsType == InsType.Y)
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
                if (item.Value.InsType == InsType.I)
                {
                    CogPMAlignTool tool;
                    CogBlobTool blobTool;
                    if (ToolBlock.Tools.Contains(item.Key))
                    {
                        if ((item.Key.StartsWith("ImSheet")))
                        {
                            blobTool = GetTool(item.Key) as CogBlobTool;
                            SetBlob(blobTool, item.Value);
                        }
                        else
                        {
                            tool = GetTool(item.Key) as CogPMAlignTool;
                            String trainImagePath = Path.Combine($"{ToolsPath}\\TrainImage", tool.Name);

                            if (!Base.LoadTrainImage(tool, $"{trainImagePath}.bmp"))
                                Debug.WriteLine($"{trainImagePath}.bmp Train Image Load Fail");
                        }
                    }
                    else
                    {
                        if (item.Key.StartsWith("ImSheet"))
                        {
                            blobTool = new CogBlobTool();
                            blobTool.Name = item.Key;
                            this.ToolBlock.Tools.Add(blobTool);
                        }
                        else
                        {
                            tool = new CogPMAlignTool();
                            tool.Name = item.Key;
                            this.ToolBlock.Tools.Add(tool);
                        }
                    }
                }
                if (item.Value.InsType == InsType.B)
                {
                    CogFindCircleTool tool;
                    CogPMAlignTool alignTool;

                    if (ToolBlock.Tools.Contains(item.Key))
                    {
                        alignTool = GetTool(item.Key + "Train") as CogPMAlignTool;

                        if (!alignTool.Pattern.Trained)
                        {
                            String trainImagePath = Path.Combine($"{ToolsPath}\\TrainImage", alignTool.Name);
                            alignTool.Pattern.PCPLoadFromFile($"{trainImagePath}.vpp");
                            alignTool.Pattern.Train();
                        }
                    }
                    else
                    {
                        alignTool = new CogPMAlignTool();
                        alignTool.Name = item.Key;
                        this.ToolBlock.Tools.Add(alignTool);
                    }
                    //찾은 패턴 Point로 CircleTool Setting.
                    //if (alignTool.Results.Count > 0)
                    //{
                    if (ToolBlock.Tools.Contains(item.Key))
                    {
                        tool = GetTool(item.Key) as CogFindCircleTool;
                    }
                    else
                    {
                        tool = new CogFindCircleTool();
                        tool.Name = item.Key;
                        this.ToolBlock.Tools.Add(tool);
                    }

                    SetCircle(tool, item.Value, alignTool);
                    //}
                }
            }
        }

        internal virtual void SetBlob(CogBlobTool tool, InsItem p)
        {
            //Double x = -p.SetY / CalibX;
            //Double y = -p.SetX / CalibY;
            //Double r = p.R / CalibY;

            //tool.RunParams.RegionMode = CogRegionModeConstants.PixelAlignedBoundingBoxAdjustMask;
            //CogRectangleAffine rect = new CogRectangleAffine();

            //rect.CenterX = 0;
            //rect.CenterY = 0;
            //rect.SideXLength = 700;
            //rect.SideYLength = 700;

            //tool.Region = rect;
            //tool.RunParams.SegmentationParams.Mode = CogBlobSegmentationModeConstants.HardFixedThreshold;
            //tool.RunParams.SegmentationParams.HardFixedThreshold = 150;
            //tool.RunParams.SegmentationParams.Polarity = CogBlobSegmentationPolarityConstants.LightBlobs;
            //tool.RunParams.ConnectivityMinPixels = 1000;

            //CogBlobMeasure item = new CogBlobMeasure();
            //item.FilterMode = CogBlobFilterModeConstants.IncludeBlobsInRange;
            //item.FilterRangeLow = 1;
            //item.FilterRangeHigh = 1.03;
            //item.Measure = CogBlobMeasureConstants.AcircularityRms;
            //item.Mode = CogBlobMeasureModeConstants.Filter;

            //tool.RunParams.RunTimeMeasures.Clear();
            //tool.RunParams.RunTimeMeasures.Add(item);

            //tool.InputImage = this.InputImage;
        }

        internal virtual void SetPMAlign(CogPMAlignTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;

        }
        internal virtual void SetCircle(CogFindCircleTool tool, InsItem p, CogPMAlignTool alignTool = null)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;
            if (tool.Name.StartsWith("B"))
            {
                //tool.RunParams.ExpectedCircularArc.CenterX = alignTool.Results[0].GetPose().TranslationX;
                //tool.RunParams.ExpectedCircularArc.CenterY = alignTool.Results[0].GetPose().TranslationY;
            }
            else
            {
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
        }
        internal virtual void SetRectangle(CogToolBlock tool, InsItem p)
        {
            if (tool == null) return;
            Base.Input(tool, "X", -p.Y / CalibX);
            Base.Input(tool, "Y", -p.X / CalibY);
        }

        internal virtual void SetMicaCaliper(CogCaliperTool tool, InsItem p)
        {
            Int32 lop = Convert.ToInt32(tool.Name.Substring(tool.Name.Length - 1));
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;

            tool.Region.CenterX = x;
            tool.Region.CenterY = y;
            //예외사항들
            if (tool.Name == "M03X3") tool.Region.CenterY = y - 30;
            else if (tool.Name == "M32X3") tool.Region.CenterY = y - 30;

            tool.Region.Rotation = lop % 2 == 1 ? -Math.PI / 2 : 0;
            tool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge;
            tool.LastRunRecordDiagEnable = CogCaliperLastRunRecordDiagConstants.InputImageByReference | CogCaliperLastRunRecordDiagConstants.Region;

            if (lop <= 2)
                tool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
            else
                tool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight;
        }

        internal virtual void SetCaliper(CogCaliperTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            tool.Region.CenterX = x;
            tool.Region.CenterY = y;
            tool.Region.SideXLength = 160;
            tool.Region.SideYLength = 50;
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
                    if (CalResult(GetTool(item.Key) as CogFindCircleTool, item.Value, out InsItem r))
                    {
                        results.Add(new Result(item.Key + "X", r.X));
                        results.Add(new Result(item.Key + "Y", r.Y));
                        results.Add(new Result(item.Key + "D", r.D));
                    }
                }
                else if (item.Value.InsType == InsType.B)
                {
                    CogPMAlignTool tool = GetTool(item.Key + "Train") as CogPMAlignTool;
                    Int32 count = tool.Results.Count;

                    if (count == 0)
                    {
                       
                        results.Add(new Result(item.Key + "X", 0));
                        results.Add(new Result(item.Key + "Y", 0));
                        results.Add(new Result(item.Key + "D", 0));
                        results.Add(new Result(item.Key + "P", 0));

                        SetNgRegion(tool);
                    }
                    else
                    {
                        if (CalResult(GetTool(item.Key) as CogFindCircleTool, item.Value, out InsItem r))
                        {
                            results.Add(new Result(item.Key + "X", r.X));
                            results.Add(new Result(item.Key + "Y", r.Y));
                            results.Add(new Result(item.Key + "P", 0));
                        }
                    }
                }
                else if (item.Value.InsType == InsType.X || item.Value.InsType == InsType.Y)
                {
                    if (CalResult(GetTool(item.Key) as CogCaliperTool, item.Value, out InsItem r))
                        results.Add(new Result(item.Key, r.D));
                }
                else if (item.Value.InsType == InsType.S)
                {
                    if (CalResult(GetTool(item.Key) as CogCaliperTool, item.Value, out InsItem r))
                        results.Add(new Result(item.Key, r.D));
                }
                else if (item.Value.InsType == InsType.I)
                {
                    if (item.Key.StartsWith("ImSheet"))
                    {
                        CogBlobTool tool = GetTool(item.Key) as CogBlobTool;
                        Int32 count = tool.Results.GetBlobs().Count;
                        if (count == 0)
                        {
                            results.Add(new Result(item.Key, 0));
                            results.Add(new Result($"{item.Key}Width", 0));
                            results.Add(new Result($"{item.Key}Height", 0));
                            SetNgRegion(tool);
                        }
                        else
                        {
                            CogBlobResult b= GetBlob(tool);
                            CogRectangleAffine r = b.GetBoundingBox(CogBlobAxisConstants.SelectedSpace); //X:가로(Width) Y:세로(Height)
                          
                            //Debug.WriteLine($"{item.Key} => {r.SideXLength} / {r.SideYLength}");

                            results.Add(new Result(item.Key, count));
                            results.Add(new Result($"{item.Key}Width", r.SideXLength));
                            results.Add(new Result($"{item.Key}Height", r.SideYLength));
                        }
                    }
                    else
                    {
                        CogPMAlignTool tool = GetTool(item.Key) as CogPMAlignTool;
                        Int32 count = tool.Results.Count;
                        if (count == 0)
                        {
                            results.Add(new Result(item.Key, 0));
                            SetNgRegion(tool);
                        }
                        else
                        {
                            if (CalResult(GetTool(item.Key) as CogPMAlignTool, item.Value, out InsItem r))
                                results.Add(new Result(item.Key, r.D));
                        }
                    }
                }
            }

            Output("Results", JsonConvert.SerializeObject(results));
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
        private void SetNgRegion(CogPMAlignTool tool)
        {
            try
            {
                ICogRecord record = tool.CreateLastRunRecord();
                ICogRecord searchRegionRecord = record.SubRecords["InputImage"].SubRecords["SearchRegion"];
                if (searchRegionRecord.Content is ICogGraphicInteractive searchRegion)
                {
                    CogRectangleAffine rectangle = searchRegion as CogRectangleAffine;
                    if (rectangle != null)
                        rectangle.Color = CogColorConstants.Red;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
        }

        private void SetNgRegion(CogBlobTool tool)
        {
            try
            {
                //tool.LastRunRecordDiagEnable = CogBlobLastRunRecordDiagConstants.Region;
                //ICogRecord record = tool.CreateLastRunRecord();
                //ICogRecord searchRegionRecord = record.SubRecords["InputImage"].SubRecords["Region"];
                //if (searchRegionRecord.Content is ICogGraphicInteractive searchRegion)
                //{
                //    CogRectangleAffine rectangle = searchRegion as CogRectangleAffine;
                //    if (rectangle != null)
                //        rectangle.Color = CogColorConstants.Red;
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
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
                if (tool.Name.StartsWith("B"))
                    result.D = 0;
                else
                    result.D = Math.Round(c.Radius * 2, 3);
            }
            return true;
        }
        internal virtual Boolean CalResult(CogPMAlignTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept && tool.Results.Count != 0)
            {
                result.D = tool.Results[0].Score * 100;
            }
            return true;
        }
        internal virtual Boolean CalResult(CogCaliperTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept && tool.Results.Count != 0)
            {
                CogCaliperResult r = tool.Results[0];

                if (ins.InsType == InsType.S)
                {
                    Int32 type = Convert.ToInt32(tool.Name.Substring(tool.Name.Length - 1));
                    result.D = type % 2 == 1 ? r.PositionY : r.PositionX;
                }
                else
                {
                    if (ins.InsType == InsType.X)
                        result.D = Math.Abs(r.PositionY);
                    else if (ins.InsType == InsType.Y)
                        result.D = Math.Abs(r.PositionX);
                }
            }
            else
            {
                result.D = 0;
            }
            return true;
        }

        internal virtual Boolean CalResult(CogCaliperTool tool, InsItem ins, InsType type, Int32 lop, out InsItem result)
        {
            result = new InsItem() { InsType = type };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept)
            {
                CogCaliperResult r = tool.Results[0];
                if (type == InsType.X)
                    result.D = r.PositionY;
                else if (type == InsType.Y)
                    result.D = r.PositionX;

            }
            return true;
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
    }
}
