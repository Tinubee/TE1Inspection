using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.ToolGroup;
using System;
using TE1;

public class DefaultScript : CogToolBlockAdvancedScriptBase
{
    private CogToolBlock ToolBlock;
    private BaseTool UserTool;

    public override Boolean GroupRun(ref String message, ref CogToolResultConstants result)
    {
        UserTool.StartedRun();
        foreach (ICogTool tool in ToolBlock.Tools)
        {
            UserTool.BeforeToolRun(tool);
            ToolBlock.RunTool(tool, ref message, ref result);
            UserTool.AfterToolRun(tool, result);
        }
        UserTool.FinistedRun();
        return false;
    }

    public override void ModifyLastRunRecord(ICogRecord lastRecord)
    {
        base.ModifyLastRunRecord(lastRecord);
        UserTool.ModifyLastRunRecord(lastRecord);
    }

    public override void Initialize(CogToolGroup host)
    {
        base.Initialize(host);
        ToolBlock = host as CogToolBlock;
        // 해당 ToolBlock의 Class로 수정 사용
        UserTool = new BaseTool(ToolBlock);
    }
}