using Euresys.MultiCam;
using System;
using System.Diagnostics;
using System.IO;
using TE1.Schemas;

namespace TE1.Multicam
{
    public class Hike8K : CamControl
    {
        public override 카메라구분 Camera { get; set; } = 카메라구분.None;
        public override String CamFile { get; set; } = "MV-CL084-90CM.cam";
        public override UInt32 DriverIndex { get; set; } = 0;
        public override AcquisitionMode AcquisitionMode { get; set; } = AcquisitionMode.PAGE;
        public override LineRateMode LineRateMode { get; set; } = LineRateMode.CONVERT;
        public override TrigMode TrigMode { get; set; } = TrigMode.HARD;
        public override NextTrigMode NextTrigMode { get; set; } = NextTrigMode.SAME;
        public override EndTrigMode EndTrigMode { get; set; } = EndTrigMode.AUTO;
        public override Int32 SeqLength_Pg { get; set; } = 1;
        public override Int32 PageLength_Ln { get; set; } = 1000;

        public Hike8K(카메라구분 camera)
        {
            this.Camera = camera;
        }

        public override void Init()
        {
            base.Init();
            //Debug.WriteLine($"{this.DriverIndex}, {this.Connector}", this.Camera.ToString());
            MC.Create("CHANNEL", out this.Channel);
            MC.SetParam(this.Channel, "DriverIndex", this.DriverIndex);
            MC.SetParam(this.Channel, "Connector", this.Connector.ToString());
            MC.SetParam(this.Channel, "CamFile", Path.Combine(Global.환경설정.기본경로, this.CamFile));
            MC.SetParam(this.Channel, "AcquisitionMode", this.AcquisitionMode.ToString());

            MC.SetParam(this.Channel, "TapConfiguration", "BASE_2T8");
            //MC.SetParam(this.Channel, "TapGeometry", "1X2");
            //MC.SetParam(this.Channel, "LineRateMode", this.LineRateMode.ToString());
            //MC.SetParam(this.Channel, "TrigMode", this.TrigMode.ToString());
            //MC.SetParam(this.Channel, "NextTrigMode", this.NextTrigMode.ToString());
            //MC.SetParam(this.Channel, "EndTrigMode", this.EndTrigMode.ToString());

            if (this.Hactive_Px > 0)
            {
                MC.SetParam(this.Channel, "GrabWindow", "MAN");
                MC.SetParam(this.Channel, "Hactive_Px", this.Hactive_Px);
                MC.SetParam(this.Channel, "WindowX_Px", this.Hactive_Px);
            }
            if (this.ImageFlipX) MC.SetParam(this.Channel, "ImageFlipX", "ON");
            if (this.ImageFlipY) MC.SetParam(this.Channel, "ImageFlipY", "ON");

            //MC.SetParam(this.Channel, "LineCaptureMode", this.LineCaptureMode.ToString());
            MC.SetParam(this.Channel, "SeqLength_Pg", this.SeqLength_Pg);
            MC.SetParam(this.Channel, "PageLength_Ln", this.PageLength_Ln);

            //DebugParam("SeqLength_Pg");
            //DebugParam("PageLength_Ln");
            //DebugParam("SeqLength_Ln");
            //DebugParam("TapConfiguration");
            //DebugParam("TapGeometry");
            //DebugParam("LineRateMode");
            //DebugParam("TrigMode");
            //DebugParam("NextTrigMode");
            //DebugParam("EndTrigMode");
            //DebugParam("LineTrigLine");
            //DebugParam("TrigCtl");
            //DebugParam("LineTrigCtl");
            //DebugParam("LineTrigEdge");
        }
    }
}
