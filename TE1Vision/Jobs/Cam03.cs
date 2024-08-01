﻿using Cognex.VisionPro.ToolBlock;
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
                Debug.WriteLine($"{item.Key}");
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
                    if (ToolBlock.Tools.Contains(item.Key))
                    {
                        tool = GetTool(item.Key) as CogPMAlignTool;
                        String trainImagePath = Path.Combine($"{ToolsPath}\\TrainImage", tool.Name);

                        if (!Base.LoadTrainImage(tool, $"{trainImagePath}.bmp"))
                            Debug.WriteLine($"{trainImagePath}.bmp Train Image Load Fail");

                        /* tool.Pattern.TrainImage = */
                    }
                    else
                    {
                        tool = new CogPMAlignTool();
                        tool.Name = item.Key;
                        this.ToolBlock.Tools.Add(tool);
                    }
                    //SetPMAlign(tool, item.Value);
                }
                if (item.Value.InsType == InsType.B)
                {
                    CogFindCircleTool tool;
                    CogPMAlignTool alignTool;

                    //if (ToolBlock.Tools.Contains(item.Key))
                    //{
                    //    alignTool = GetTool(item.Key) as CogPMAlignTool;
                    //    String trainImagePath = Path.Combine($"{ToolsPath}\\TrainImage", alignTool.Name);

                    //    if (!Base.LoadTrainImage(alignTool, $"{trainImagePath}.bmp"))
                    //        Debug.WriteLine($"{trainImagePath}.bmp Train Image Load Fail");

                    //    /* tool.Pattern.TrainImage = */
                    //}
                    //else
                    //{
                    //    alignTool = new CogPMAlignTool();
                    //    alignTool.Name = item.Key;
                    //    this.ToolBlock.Tools.Add(alignTool);
                    //}
                    //찾은 패턴 Point로 CircleTool Setting.

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

                    SetCircle(tool, item.Value);
                }
            }
        }
        internal virtual void SetPMAlign(CogPMAlignTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;

        }
        internal virtual void SetCircle(CogFindCircleTool tool, InsItem p)
        {
            Double x = -p.Y / CalibX;
            Double y = -p.X / CalibY;
            Double r = p.R / CalibY;
            if (tool.Name.StartsWith("B"))
            {
                //Bolt 속성 설정일단 제외. 검수 후 Hole과 비교 후 공통부분 제외하고 설정값 적용.
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
            //Debug.WriteLine("들어옴");
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
            //if (p.InsType == InsType.X)
            //{
            //    tool.Region.Rotation = Math.PI / 2;
            //}
            //else
            //{
            //    tool.Region.Rotation = -Math.PI;
            //}

            tool.RunParams.EdgeMode = CogCaliperEdgeModeConstants.SingleEdge;
            //tool.RunParams.Edge0Polarity = CogCaliperPolarityConstants.DarkToLight;
            //tool.RunParams.ContrastThreshold = 10;
        }

        internal virtual void CalResults()
        {
            List<Result> results = new List<Result>();
            Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);
            foreach (var item in items)
            {
                Debug.WriteLine($"{item.Key}");
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
                    if (CalResult(GetTool(item.Key) as CogFindCircleTool, item.Value, out InsItem r))
                    {
                        results.Add(new Result(item.Key + "X", r.X));
                        results.Add(new Result(item.Key + "Y", r.Y));
                    }
                }
                else if (item.Value.InsType == InsType.X || item.Value.InsType == InsType.Y)
                {
                    if (CalResult(GetTool(item.Key) as CogCaliperTool, item.Value, out InsItem r))
                    {
                        Debug.WriteLine($"{item.Key} : {r.D}");
                        results.Add(new Result(item.Key, r.D));
                    }
                }
                else if (item.Value.InsType == InsType.S)
                {
                    //Debug.WriteLine("dd");
                    if (CalResult(GetTool(item.Key) as CogCaliperTool, item.Value, out InsItem r))
                    {
                        //Debug.WriteLine($"{item.Key} : {r.D}");
                        results.Add(new Result(item.Key, r.D));
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
            if (tool.RunStatus.Result == CogToolResultConstants.Accept && tool.Results.Count != 0)
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
                    result.D = type % 2 == 1 ? r.PositionY : r.PositionX;//Math.Abs(Math.Round(r.PositionY - Math.Abs(ins.X) / CalibY, 3));
                }
                else
                {
                    if (ins.InsType == InsType.X)
                        result.D = r.PositionY;//Math.Abs(Math.Round(r.PositionY - Math.Abs(ins.X) / CalibY, 3));
                    else if (ins.InsType == InsType.Y)
                        result.D = r.PositionX;//Math.Abs(Math.Round(r.PositionX - Math.Abs(ins.Y) / CalibX, 3));
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
                    result.D = r.PositionX;//Math.Abs(Math.Round(r.PositionX - Math.Abs(ins.Y) / CalibX, 3));

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

    //public class ImprintTools : BaseTool
    //{
    //    public ImprintTools(CogToolBlock tool) : base(tool) { }
    //    public override Cameras Camera => Cameras.Cam03;
    //    internal Double CalibX => Input<Double>("CalibX");
    //    internal Double CalibY => Input<Double>("CalibY");

    //    public override void StartedRun()
    //    {
    //        base.StartedRun();
    //        InitTools();
    //    }

    //    public override void FinistedRun()
    //    {
    //        base.FinistedRun();
    //        CalResults();
    //    }

    //    private Double CenterX => InputImage.Width / 2 - Input<Double>("CenterX");
    //    internal virtual void InitTools()
    //    {
    //        CogCreateLineTool centerV = GetTool("CenterV") as CogCreateLineTool;
    //        if (centerV != null) centerV.Line.X = CenterX;

    //        //InsItems.LoadMica();  //위치 Setting을 위한 임시 생성.
    //        Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);

    //        foreach (var item in items)
    //        {
    //            if (item.Value.InsType == InsType.I)
    //            {
    //                //CogPMAlignTool tool;
    //                //if (ToolBlock.Tools.Contains(item.Key))
    //                //{
    //                //    tool = GetTool(item.Key) as CogPMAlignTool;
    //                //    //if(tool.RunParams.)
    //                //}
    //                //else
    //                //{
    //                //    tool = new CogPMAlignTool();
    //                //    tool.Name = item.Key;
    //                //    this.ToolBlock.Tools.Add(tool);
    //                //}
    //                //SetPMAlign(tool, item.Value);
    //            }
    //        }
    //    }
    //    internal virtual void SetPMAlign(CogPMAlignTool tool, InsItem p)
    //    {
    //        //Double x = -p.Y / CalibX;
    //        //Double y = -p.X / CalibY;
    //        //Double r = p.R / CalibY;

    //    }

    //    internal virtual void CalResults()
    //    {
    //        List<Result> results = new List<Result>();
    //        Dictionary<String, InsItem> items = InsItems.GetItems((Int32)Camera);
    //        foreach (var item in items)
    //        {
    //            if (item.Value.InsType == InsType.I)
    //            {

    //            }
    //        }
    //        //Debug.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
    //        Output("Results", JsonConvert.SerializeObject(results));
    //    }

    //    internal virtual Boolean CalResult(CogPMAlignTool tool, InsItem ins, out InsItem result)
    //    {
    //        result = new InsItem() { InsType = ins.InsType };
    //        if (tool == null) return false;
    //        if (tool.RunStatus.Result == CogToolResultConstants.Accept)
    //        {
    //            //Double score = tool.Pattern.
    //            //CogCircle c = tool.Results.GetCircle();
    //            //result.X = -Math.Round(c.CenterY, 3);
    //            //result.Y = -Math.Round(c.CenterX, 3);
    //            //result.D = Math.Round(c.Radius * 2, 3);
    //        }
    //        return true;
    //    }
    //}
}
