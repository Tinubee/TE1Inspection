using Cognex.VisionPro.ToolBlock;
using System.Collections.Generic;
using System;

namespace TE1.Cam03
{
    public class MainTools : BaseTool
    {
        public MainTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam03;
    }

    public class AlignTools : BaseTool
    {
        public AlignTools(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam03;
    }

    public class Thickness : BaseTool
    {
        public Thickness(CogToolBlock tool) : base(tool) { }
        public override Cameras Camera => Cameras.Cam03;
    }
}
