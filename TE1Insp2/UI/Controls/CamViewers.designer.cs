using System.Collections.Generic;
using TE1.Schemas;

namespace TE1.UI.Controls
{
    partial class CamViewers
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CamViewers));
            DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer dockingContainer1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer();
            this.documentGroup1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup(this.components);
            this.document1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document(this.components);
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.d좌측캠 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.e우측캠 = new Cogutils.RecordDisplay();
            this.d하부캠 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.e좌측캠 = new Cogutils.RecordDisplay();
            this.d우측캠 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.e하부캠 = new Cogutils.RecordDisplay();
            this.Bind검사정보 = new System.Windows.Forms.BindingSource(this.components);
            this.documentManager1 = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tabbedView1 = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.d좌측캠.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e우측캠)).BeginInit();
            this.d하부캠.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e좌측캠)).BeginInit();
            this.d우측캠.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e하부캠)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사정보)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).BeginInit();
            this.SuspendLayout();
            // 
            // documentGroup1
            // 
            this.documentGroup1.Items.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.Document[] {
            this.document1});
            // 
            // document1
            // 
            this.document1.Caption = "Top";
            this.document1.ControlName = "d상부캠";
            this.document1.FloatLocation = new System.Drawing.Point(800, 0);
            this.document1.FloatSize = new System.Drawing.Size(600, 900);
            this.document1.Properties.AllowClose = DevExpress.Utils.DefaultBoolean.False;
            this.document1.Properties.AllowFloat = DevExpress.Utils.DefaultBoolean.True;
            this.document1.Properties.AllowFloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(1200, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(780, 1000);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(780, 1000);
            this.Root.TextVisible = false;
            // 
            // dockManager1
            // 
            this.dockManager1.DockingOptions.ShowCloseButton = false;
            this.dockManager1.Form = this;
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.d좌측캠,
            this.d하부캠,
            this.d우측캠});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1980, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 1000);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1980, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 1000);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1980, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 1000);
            // 
            // d좌측캠
            // 
            this.d좌측캠.Controls.Add(this.controlContainer1);
            this.d좌측캠.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.d좌측캠.FloatSize = new System.Drawing.Size(300, 800);
            this.d좌측캠.ID = new System.Guid("f5882d5d-e42e-471d-a4f7-a603eb936d37");
            this.d좌측캠.Location = new System.Drawing.Point(600, 0);
            this.d좌측캠.Name = "d좌측캠";
            this.d좌측캠.OriginalSize = new System.Drawing.Size(600, 200);
            this.d좌측캠.Size = new System.Drawing.Size(600, 1000);
            this.d좌측캠.Text = "Left";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.e좌측캠);
            this.controlContainer1.Location = new System.Drawing.Point(3, 30);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(593, 967);
            this.controlContainer1.TabIndex = 0;
            // 
            // e우측캠
            // 
            this.e우측캠.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.e우측캠.ColorMapLowerRoiLimit = 0D;
            this.e우측캠.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.e우측캠.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.e우측캠.ColorMapUpperRoiLimit = 1D;
            this.e우측캠.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e우측캠.DoubleTapZoomCycleLength = 2;
            this.e우측캠.DoubleTapZoomSensitivity = 2.5D;
            this.e우측캠.Location = new System.Drawing.Point(0, 0);
            this.e우측캠.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.e우측캠.MouseWheelSensitivity = 1D;
            this.e우측캠.Name = "e우측캠";
            this.e우측캠.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("e우측캠.OcxState")));
            this.e우측캠.Size = new System.Drawing.Size(593, 967);
            this.e우측캠.TabIndex = 1;
            // 
            // d상부캠
            // 
            this.d하부캠.Controls.Add(this.dockPanel2_Container);
            this.d하부캠.DockedAsTabbedDocument = true;
            this.d하부캠.FloatLocation = new System.Drawing.Point(800, 0);
            this.d하부캠.FloatSize = new System.Drawing.Size(600, 900);
            this.d하부캠.FloatVertical = true;
            this.d하부캠.ID = new System.Guid("bb89b76f-49ee-4a47-a8a1-b9e2af121512");
            this.d하부캠.Name = "d상부캠";
            this.d하부캠.OriginalSize = new System.Drawing.Size(200, 200);
            this.d하부캠.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.d하부캠.SavedIndex = 1;
            this.d하부캠.Text = "Top";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.e하부캠);
            this.dockPanel2_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(774, 967);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // e좌측캠
            // 
            this.e좌측캠.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.e좌측캠.ColorMapLowerRoiLimit = 0D;
            this.e좌측캠.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.e좌측캠.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.e좌측캠.ColorMapUpperRoiLimit = 1D;
            this.e좌측캠.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e좌측캠.DoubleTapZoomCycleLength = 2;
            this.e좌측캠.DoubleTapZoomSensitivity = 2.5D;
            this.e좌측캠.Location = new System.Drawing.Point(0, 0);
            this.e좌측캠.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.e좌측캠.MouseWheelSensitivity = 1D;
            this.e좌측캠.Name = "e좌측캠";
            this.e좌측캠.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("e좌측캠.OcxState")));
            this.e좌측캠.Size = new System.Drawing.Size(593, 967);
            this.e좌측캠.TabIndex = 0;
            // 
            // d우측캠
            // 
            this.d우측캠.Controls.Add(this.controlContainer2);
            this.d우측캠.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.d우측캠.FloatSize = new System.Drawing.Size(300, 800);
            this.d우측캠.ID = new System.Guid("ed5ff30a-6e16-43ef-bef7-27d1a49d40ea");
            this.d우측캠.Location = new System.Drawing.Point(0, 0);
            this.d우측캠.Name = "d우측캠";
            this.d우측캠.OriginalSize = new System.Drawing.Size(600, 200);
            this.d우측캠.Size = new System.Drawing.Size(600, 1000);
            this.d우측캠.Text = "Right";
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.e우측캠);
            this.controlContainer2.Location = new System.Drawing.Point(3, 30);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(593, 967);
            this.controlContainer2.TabIndex = 0;
            // 
            // e상부캠
            // 
            this.e하부캠.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.e하부캠.ColorMapLowerRoiLimit = 0D;
            this.e하부캠.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.e하부캠.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.e하부캠.ColorMapUpperRoiLimit = 1D;
            this.e하부캠.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e하부캠.DoubleTapZoomCycleLength = 2;
            this.e하부캠.DoubleTapZoomSensitivity = 2.5D;
            this.e하부캠.Location = new System.Drawing.Point(0, 0);
            this.e하부캠.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.e하부캠.MouseWheelSensitivity = 1D;
            this.e하부캠.Name = "e상부캠";
            this.e하부캠.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("e상부캠.OcxState")));
            this.e하부캠.Size = new System.Drawing.Size(774, 967);
            this.e하부캠.TabIndex = 2;
            // 
            // Bind검사정보
            // 
            this.Bind검사정보.DataSource = typeof(System.Collections.Generic.List<TE1.Schemas.검사정보>);
            // 
            // documentManager1
            // 
            this.documentManager1.ContainerControl = this;
            this.documentManager1.MenuManager = this.barManager1;
            this.documentManager1.View = this.tabbedView1;
            this.documentManager1.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView1});
            // 
            // tabbedView1
            // 
            this.tabbedView1.DocumentGroupProperties.ShowDocumentSelectorButton = false;
            this.tabbedView1.DocumentGroups.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup[] {
            this.documentGroup1});
            this.tabbedView1.Documents.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseDocument[] {
            this.document1});
            dockingContainer1.Element = this.documentGroup1;
            dockingContainer1.Length.UnitValue = 1.9706610012428574D;
            this.tabbedView1.RootContainer.Nodes.AddRange(new DevExpress.XtraBars.Docking2010.Views.Tabbed.DockingContainer[] {
            dockingContainer1});
            // 
            // CamViewers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.d좌측캠);
            this.Controls.Add(this.d우측캠);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CamViewers";
            this.Size = new System.Drawing.Size(1980, 1000);
            ((System.ComponentModel.ISupportInitialize)(this.documentGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.document1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.d좌측캠.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e우측캠)).EndInit();
            this.d하부캠.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e좌측캠)).EndInit();
            this.d우측캠.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e하부캠)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사정보)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView1;
        private DevExpress.XtraBars.Docking.DockPanel d하부캠;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.DocumentGroup documentGroup1;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.Document document1;
        private Cogutils.RecordDisplay e좌측캠;
        private System.Windows.Forms.BindingSource Bind검사정보;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Docking.DockPanel d좌측캠;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraBars.Docking.DockPanel d우측캠;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private Cogutils.RecordDisplay e우측캠;
        private Cogutils.RecordDisplay e하부캠;
    }
}
