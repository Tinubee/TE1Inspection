using Cognex.VisionPro;
using Cognex.VisionPro.Dimensioning;
using Cognex.VisionPro.ToolBlock;
using System;

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
        internal void ModifyRecords(ICogRecord lastRecord)
        {
            if (!lastRecord.SubRecords.ContainsKey(ViewerRecodName)) return;
            ICogRecord record = lastRecord.SubRecords[ViewerRecodName];
            if (String.IsNullOrEmpty(Results)) return;

            //try
            //{
            //}
            //catch (Exception ex) { Debug.WriteLine(ex.Message); }
            Results = String.Empty;
        }
        //internal CogGraphicLabel FindRecord(ICogRecord record, String name)
        //{
        //    foreach (ICogRecord rcd in record.SubRecords)
        //    {
        //        if (rcd.ContentType == typeof(CogGraphicLabel))
        //        {
        //            CogGraphicLabel label = rcd.Content as CogGraphicLabel;
        //            if (label.Text == name) return label;
        //        }
        //    }
        //    return null;
        //}
        //internal void SetResult(DisplayResult result, ICogRecord record)
        //{
        //    if (result == null) return;
        //    CogGraphicLabel label = FindRecord(record, result.KeyName);
        //    if (label == null) return;
        //    label.Color = result.Color;
        //    label.Text = result.Display;
        //}
    }

    public class AlignTools : BaseTool
    {
        public AlignTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam01;
        public Double OffsetX = 0;



        internal CogCreateSegmentTool LineBC => GetTool("LineBC") as CogCreateSegmentTool;
        internal CogCreateLinePerpendicularTool LineB => GetTool("LineB") as CogCreateLinePerpendicularTool;
        internal CogCreateLinePerpendicularTool LineT => GetTool("LineT") as CogCreateLinePerpendicularTool;
        internal CogCreateLineParallelTool LineV => GetTool("LineV") as CogCreateLineParallelTool;
        internal CogIntersectLineLineTool Origin => GetTool("Origin") as CogIntersectLineLineTool;
    }
}
