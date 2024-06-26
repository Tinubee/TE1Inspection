﻿using Cognex.VisionPro;
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

namespace TE1.Cam01
{
    public class MainTools : BaseTool
    {
        public MainTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam01;

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
        public override Cameras Camera => Cameras.Cam01;
        public Double OffsetX = 0;

        internal const Double LengthBC = 973.5;
        internal const Double LengthVertical = 162.5;
        internal Double CalibX => Input<Double>("CalibX");
        internal Double CalibY => Input<Double>("CalibY");
        internal Boolean Detected { get => Output<Boolean>("Detected"); set => Output("Detected", value); }
        internal CogFixtureTool Fixture => GetTool("Fixture") as CogFixtureTool;
        internal CogTransform2DLinear Transform => Fixture.RunParams.UnfixturedFromFixturedTransform as CogTransform2DLinear;


        internal CogCreateSegmentTool LineTB => GetTool("LineTB") as CogCreateSegmentTool;
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
            Double y = Math.Round(LengthBC / LineTB.Segment.Length * 1000, 9);

            Debug.WriteLine($"{LengthBC} / {Math.Round(LineTB.Segment.Length, 2)} = {y}", "DatumBC");
            Output("CalibX", CalibX);
            Output("CalibY", CalibY);
        }

        internal virtual void SetOriginX()
        {
            Double length = 103 / CalibX;
            Point2d p = Base.CalculatePoint(new Point2d(LineTB.Segment.StartX, LineTB.Segment.StartY), length, LineB.GetOutputLine().Rotation);
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
            Transform.Rotation = LineTB.Segment.Rotation + Math.PI / 2;
            Output("CenterX", p.X);
            Output("CenterY", p.Y);
            Output("Rotation", Transform.Rotation);
        }

        internal virtual void SetFixture() { }
    }

    public class DetectTools : BaseTool
    {
        public DetectTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam01;
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
                    CogBlobTool tool;
                    if (ToolBlock.Tools.Contains(item.Key)) tool = GetTool(item.Key) as CogBlobTool;
                    else
                    {
                        tool = new CogBlobTool();
                        tool.Name = item.Key;
                        this.ToolBlock.Tools.Add(tool);
                    }
                    SetCircle(tool, item.Value);
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
            tool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.LightToDark;
            tool.RunParams.ContrastThreshold = 16;
            tool.RunParams.FilterHalfSizeInPixels = 4;
        }

        internal virtual void SetCircle(CogBlobTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;

            tool.RunParams.RegionMode = CogRegionModeConstants.PixelAlignedBoundingBoxAdjustMask;

            CogRectangleAffine rect = new CogRectangleAffine();

            rect.CenterX = x;
            rect.CenterY = y;
            rect.SideXLength = 600;
            rect.SideYLength = 600;

            tool.Region = rect;
            tool.RunParams.ConnectivityMinPixels = 10000;
            tool.RunParams.SegmentationParams.HardFixedThreshold = 150;
        }

        internal virtual void SetRectangle(CogToolBlock tool, InsItem p)
        {
            if (tool == null) return;
            Base.Input(tool, "X", -p.Y / CalibX);
            Base.Input(tool, "Y", -p.X / CalibY);
        }

        internal virtual void CalResults()
        {
            List<Result> results = new List<Result>();
            Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);
            foreach (var item in items)
            {
                if (item.Value.InsType == InsType.H)
                {
                    if (CalResult(GetTool(item.Key) as CogBlobTool, item.Value, out InsItem r))
                    {
                        results.Add(new Result(item.Key + "X", r.X));
                        results.Add(new Result(item.Key + "Y", r.Y));
                        results.Add(new Result(item.Key + "D", r.D));
                        results.Add(new Result(item.Key + "L", r.L));
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

        internal virtual Boolean CalResult(CogBlobTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept)
            {
                CogBlobResult r = this.GetBlob(tool, 0);
                CogRectangleAffine a = r.GetBoundingBox(CogBlobAxisConstants.SelectedSpace);

                result.X = -Math.Round(a.CenterY, 3);
                result.Y = -Math.Round(a.CenterX, 3);
                result.D = Math.Round(a.SideXLength, 3);
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

        internal virtual Boolean CalResult(CogCaliperTool tool, InsItem ins, out InsItem result)
        {
            result = new InsItem() { InsType = ins.InsType };
            if (tool == null) return false;
            if (tool.RunStatus.Result == CogToolResultConstants.Accept)
            {
                CogCaliperResult r = tool.Results[0];

                if (ins.InsType == InsType.X)
                {
                    result.D = Math.Abs(Math.Round(r.PositionY - Math.Abs(ins.X) / CalibY, 3));

                    Debug.WriteLine($"기준 : {ins.X} / 측정픽셀 : {r.PositionY} / 보정값 : {r.PositionY * CalibY}");
                }

                else if (ins.InsType == InsType.Y)
                {
                    result.D = Math.Abs(Math.Round(r.PositionX - Math.Abs(ins.Y) / CalibX, 3));

                    Debug.WriteLine($"기준 : {ins.Y} / 측정픽셀 : {r.PositionX} / 보정값 : {r.PositionX * CalibX}");
                }

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