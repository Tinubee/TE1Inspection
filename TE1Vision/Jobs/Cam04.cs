using Cognex.VisionPro;
using Cognex.VisionPro.Caliper;
using Cognex.VisionPro.ToolBlock;
using MvLibs;
using System;

namespace TE1.Cam04
{
    public class MainTools : BaseTool
    {
        public MainTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam04;

        public override void ModifyLastRunRecord(ICogRecord lastRecord)
        {
            base.ModifyLastRunRecord(lastRecord);
            ModifyRecords(lastRecord);
        }
        internal void ModifyRecords(ICogRecord lastRecord)
        {
            Double Exists = Base.Output<Double>(GetTool("AlignTools") as CogToolBlock, "Exists");
            if (Exists < 1) return;
            String pmAlign = "AlignTools.ImageProcess.OutputImage";
            if (!lastRecord.SubRecords.ContainsKey(ViewerRecodName)) return;
            if (!lastRecord.SubRecords.ContainsKey(pmAlign)) return;
            ICogRecord record = lastRecord.SubRecords[ViewerRecodName];
            ICogRecord target = lastRecord.SubRecords[pmAlign];
            foreach(ICogRecord r in target.SubRecords) record.SubRecords.Add(r);
        }
}

    public class AlignTools : BaseTool
    {
        public AlignTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam04;

        public override void FinistedRun()
        {
            base.FinistedRun();
            SetResults();
        }

        internal void SetResults()
        {
            CogCaliperTool tool = GetTool("Caliper") as CogCaliperTool;
            if (tool == null || tool.Results == null || tool.Results.Count < 1) return;
            CogCaliperResult r = tool.Results[0];
            Double std = Math.Abs(tool.RunParams.Edge0Position) + Math.Abs(tool.RunParams.Edge1Position);
            Double min = std * 0.75;
            Double max = std * 1.65;
            Double cur = r.Width;
            Double val = min <= cur && cur <= max ? 1 : 0;
            Output("Exists", val);
        }
    }
}
