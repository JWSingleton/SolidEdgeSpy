namespace SolidEdge.Spy
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.startupTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.typeBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.projectWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectForumsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.githubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solidEdgeCommunityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubSamplesForSolidEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuGetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nugetInteropSolidEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nugetSolidEdgeCommunityToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nugetSolidEdgeCommunityReaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventMonitorTimer = new System.Windows.Forms.Timer(this.components);
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl = new SolidEdge.Spy.Forms.TabControlEx();
            this.tabObjectBrowser = new System.Windows.Forms.TabPage();
            this.objectBrowser = new SolidEdge.Spy.Forms.ObjectBrowser();
            this.tabTypeBrowser = new System.Windows.Forms.TabPage();
            this.typeBrowser = new SolidEdge.Spy.Forms.TypeBrowser();
            this.tabCommandBrowser = new System.Windows.Forms.TabPage();
            this.commandBrowser = new SolidEdge.Spy.Forms.CommandBrowser();
            this.tabEventMonitor = new System.Windows.Forms.TabPage();
            this.eventMonitor = new SolidEdge.Spy.Forms.EventMonitor();
            this.tabGlobalParameters = new System.Windows.Forms.TabPage();
            this.globalParameterBrowser = new SolidEdge.Spy.Forms.GlobalParameterBrowser();
            this.tabProcessBrowser = new System.Windows.Forms.TabPage();
            this.processBrowser = new SolidEdge.Spy.Forms.ProcessBrowser();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabObjectBrowser.SuspendLayout();
            this.tabTypeBrowser.SuspendLayout();
            this.tabCommandBrowser.SuspendLayout();
            this.tabEventMonitor.SuspendLayout();
            this.tabGlobalParameters.SuspendLayout();
            this.tabProcessBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 2;
            // 
            // startupTimer
            // 
            this.startupTimer.Enabled = true;
            this.startupTimer.Interval = 1000;
            this.startupTimer.Tick += new System.EventHandler(this.startupTimer_Tick);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectBrowserToolStripMenuItem,
            this.typeBrowserToolStripMenuItem,
            this.commandBrowserToolStripMenuItem,
            this.eventMonitorToolStripMenuItem,
            this.globalParametersToolStripMenuItem,
            this.processBrowserToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // objectBrowserToolStripMenuItem
            // 
            this.objectBrowserToolStripMenuItem.Image = global::SolidEdge.Spy.Properties.Resources.ComTreeItemBlue_16x16;
            this.objectBrowserToolStripMenuItem.Name = "objectBrowserToolStripMenuItem";
            this.objectBrowserToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.objectBrowserToolStripMenuItem.Text = "&Object Browser";
            this.objectBrowserToolStripMenuItem.Click += new System.EventHandler(this.objectBrowserToolStripMenuItem_Click);
            // 
            // typeBrowserToolStripMenuItem
            // 
            this.typeBrowserToolStripMenuItem.Image = global::SolidEdge.Spy.Properties.Resources.Library_16x16;
            this.typeBrowserToolStripMenuItem.Name = "typeBrowserToolStripMenuItem";
            this.typeBrowserToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.typeBrowserToolStripMenuItem.Text = "&Type Browser";
            this.typeBrowserToolStripMenuItem.Click += new System.EventHandler(this.typeBrowserToolStripMenuItem_Click);
            // 
            // commandBrowserToolStripMenuItem
            // 
            this.commandBrowserToolStripMenuItem.Image = global::SolidEdge.Spy.Properties.Resources.CommandBrowser_16x16;
            this.commandBrowserToolStripMenuItem.Name = "commandBrowserToolStripMenuItem";
            this.commandBrowserToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.commandBrowserToolStripMenuItem.Text = "Command Browser";
            this.commandBrowserToolStripMenuItem.Click += new System.EventHandler(this.commandBrowserToolStripMenuItem_Click);
            // 
            // eventMonitorToolStripMenuItem
            // 
            this.eventMonitorToolStripMenuItem.Image = global::SolidEdge.Spy.Properties.Resources.Event_16x16;
            this.eventMonitorToolStripMenuItem.Name = "eventMonitorToolStripMenuItem";
            this.eventMonitorToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.eventMonitorToolStripMenuItem.Text = "&Event Monitor";
            this.eventMonitorToolStripMenuItem.Click += new System.EventHandler(this.eventMonitorToolStripMenuItem_Click);
            // 
            // globalParametersToolStripMenuItem
            // 
            this.globalParametersToolStripMenuItem.Image = global::SolidEdge.Spy.Properties.Resources.Parameter_16x16;
            this.globalParametersToolStripMenuItem.Name = "globalParametersToolStripMenuItem";
            this.globalParametersToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.globalParametersToolStripMenuItem.Text = "&Global Parameters";
            this.globalParametersToolStripMenuItem.Click += new System.EventHandler(this.globalParametersToolStripMenuItem_Click);
            // 
            // processBrowserToolStripMenuItem
            // 
            this.processBrowserToolStripMenuItem.Image = global::SolidEdge.Spy.Properties.Resources.ProcessModules_16x16;
            this.processBrowserToolStripMenuItem.Name = "processBrowserToolStripMenuItem";
            this.processBrowserToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.processBrowserToolStripMenuItem.Text = "&Process Browser";
            this.processBrowserToolStripMenuItem.Click += new System.EventHandler(this.processBrowserToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem,
            this.toolStripMenuItem2,
            this.projectWebsiteToolStripMenuItem,
            this.projectForumsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.githubToolStripMenuItem,
            this.nuGetToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.documentationToolStripMenuItem.Text = "&Documentation";
            this.documentationToolStripMenuItem.Click += new System.EventHandler(this.documentationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 6);
            // 
            // projectWebsiteToolStripMenuItem
            // 
            this.projectWebsiteToolStripMenuItem.Name = "projectWebsiteToolStripMenuItem";
            this.projectWebsiteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.projectWebsiteToolStripMenuItem.Text = "Project &Website";
            this.projectWebsiteToolStripMenuItem.Click += new System.EventHandler(this.projectWebsiteToolStripMenuItem_Click);
            // 
            // projectForumsToolStripMenuItem
            // 
            this.projectForumsToolStripMenuItem.Name = "projectForumsToolStripMenuItem";
            this.projectForumsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.projectForumsToolStripMenuItem.Text = "Project &Forums";
            this.projectForumsToolStripMenuItem.Click += new System.EventHandler(this.projectForumsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // githubToolStripMenuItem
            // 
            this.githubToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solidEdgeCommunityToolStripMenuItem,
            this.githubSamplesForSolidEdgeToolStripMenuItem});
            this.githubToolStripMenuItem.Name = "githubToolStripMenuItem";
            this.githubToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.githubToolStripMenuItem.Text = "&GitHub";
            // 
            // solidEdgeCommunityToolStripMenuItem
            // 
            this.solidEdgeCommunityToolStripMenuItem.Name = "solidEdgeCommunityToolStripMenuItem";
            this.solidEdgeCommunityToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.solidEdgeCommunityToolStripMenuItem.Text = "Solid Edge Community";
            this.solidEdgeCommunityToolStripMenuItem.Click += new System.EventHandler(this.solidEdgeCommunityToolStripMenuItem_Click);
            // 
            // githubSamplesForSolidEdgeToolStripMenuItem
            // 
            this.githubSamplesForSolidEdgeToolStripMenuItem.Name = "githubSamplesForSolidEdgeToolStripMenuItem";
            this.githubSamplesForSolidEdgeToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.githubSamplesForSolidEdgeToolStripMenuItem.Text = "Samples";
            this.githubSamplesForSolidEdgeToolStripMenuItem.Click += new System.EventHandler(this.githubSamplesForSolidEdgeToolStripMenuItem_Click);
            // 
            // nuGetToolStripMenuItem
            // 
            this.nuGetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nugetInteropSolidEdgeToolStripMenuItem,
            this.nugetSolidEdgeCommunityToolStripMenuItem1,
            this.nugetSolidEdgeCommunityReaderToolStripMenuItem});
            this.nuGetToolStripMenuItem.Name = "nuGetToolStripMenuItem";
            this.nuGetToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.nuGetToolStripMenuItem.Text = "&NuGet";
            // 
            // nugetInteropSolidEdgeToolStripMenuItem
            // 
            this.nugetInteropSolidEdgeToolStripMenuItem.Name = "nugetInteropSolidEdgeToolStripMenuItem";
            this.nugetInteropSolidEdgeToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.nugetInteropSolidEdgeToolStripMenuItem.Text = "Interop.SolidEdge Package";
            this.nugetInteropSolidEdgeToolStripMenuItem.Click += new System.EventHandler(this.nugetInteropSolidEdgeToolStripMenuItem_Click);
            // 
            // nugetSolidEdgeCommunityToolStripMenuItem1
            // 
            this.nugetSolidEdgeCommunityToolStripMenuItem1.Name = "nugetSolidEdgeCommunityToolStripMenuItem1";
            this.nugetSolidEdgeCommunityToolStripMenuItem1.Size = new System.Drawing.Size(279, 22);
            this.nugetSolidEdgeCommunityToolStripMenuItem1.Text = "SolidEdge.Community Package";
            this.nugetSolidEdgeCommunityToolStripMenuItem1.Click += new System.EventHandler(this.nugetSolidEdgeCommunityToolStripMenuItem1_Click);
            // 
            // nugetSolidEdgeCommunityReaderToolStripMenuItem
            // 
            this.nugetSolidEdgeCommunityReaderToolStripMenuItem.Name = "nugetSolidEdgeCommunityReaderToolStripMenuItem";
            this.nugetSolidEdgeCommunityReaderToolStripMenuItem.Size = new System.Drawing.Size(279, 22);
            this.nugetSolidEdgeCommunityReaderToolStripMenuItem.Text = "SolidEdge.Community.Reader Package";
            this.nugetSolidEdgeCommunityReaderToolStripMenuItem.Click += new System.EventHandler(this.nugetSolidEdgeCommunityReaderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(154, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // eventMonitorTimer
            // 
            this.eventMonitorTimer.Enabled = true;
            this.eventMonitorTimer.Interval = 1000;
            this.eventMonitorTimer.Tick += new System.EventHandler(this.eventMonitorTimer_Tick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "ComTreeItemBlue_16x16.png");
            this.imageList.Images.SetKeyName(1, "Library_16x16.png");
            this.imageList.Images.SetKeyName(2, "Event_16x16.png");
            this.imageList.Images.SetKeyName(3, "Parameter_16x16.png");
            this.imageList.Images.SetKeyName(4, "ProcessModules_16x16.png");
            this.imageList.Images.SetKeyName(5, "CommandBrowser_16x16.png");
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabObjectBrowser);
            this.tabControl.Controls.Add(this.tabTypeBrowser);
            this.tabControl.Controls.Add(this.tabCommandBrowser);
            this.tabControl.Controls.Add(this.tabEventMonitor);
            this.tabControl.Controls.Add(this.tabGlobalParameters);
            this.tabControl.Controls.Add(this.tabProcessBrowser);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.HeaderVisible = true;
            this.tabControl.HotTrack = true;
            this.tabControl.ImageList = this.imageList;
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(784, 515);
            this.tabControl.TabIndex = 1;
            // 
            // tabObjectBrowser
            // 
            this.tabObjectBrowser.Controls.Add(this.objectBrowser);
            this.tabObjectBrowser.ImageIndex = 0;
            this.tabObjectBrowser.Location = new System.Drawing.Point(4, 23);
            this.tabObjectBrowser.Name = "tabObjectBrowser";
            this.tabObjectBrowser.Size = new System.Drawing.Size(776, 488);
            this.tabObjectBrowser.TabIndex = 0;
            this.tabObjectBrowser.Text = "Object Browser";
            // 
            // objectBrowser
            // 
            this.objectBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectBrowser.Location = new System.Drawing.Point(0, 0);
            this.objectBrowser.Name = "objectBrowser";
            this.objectBrowser.Size = new System.Drawing.Size(776, 488);
            this.objectBrowser.TabIndex = 0;
            // 
            // tabTypeBrowser
            // 
            this.tabTypeBrowser.Controls.Add(this.typeBrowser);
            this.tabTypeBrowser.ImageIndex = 1;
            this.tabTypeBrowser.Location = new System.Drawing.Point(4, 23);
            this.tabTypeBrowser.Name = "tabTypeBrowser";
            this.tabTypeBrowser.Size = new System.Drawing.Size(776, 488);
            this.tabTypeBrowser.TabIndex = 3;
            this.tabTypeBrowser.Text = "Type Browser";
            // 
            // typeBrowser
            // 
            this.typeBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeBrowser.Location = new System.Drawing.Point(0, 0);
            this.typeBrowser.Name = "typeBrowser";
            this.typeBrowser.Size = new System.Drawing.Size(776, 488);
            this.typeBrowser.TabIndex = 0;
            // 
            // tabCommandBrowser
            // 
            this.tabCommandBrowser.BackColor = System.Drawing.SystemColors.Control;
            this.tabCommandBrowser.Controls.Add(this.commandBrowser);
            this.tabCommandBrowser.ImageIndex = 5;
            this.tabCommandBrowser.Location = new System.Drawing.Point(4, 23);
            this.tabCommandBrowser.Name = "tabCommandBrowser";
            this.tabCommandBrowser.Size = new System.Drawing.Size(776, 488);
            this.tabCommandBrowser.TabIndex = 5;
            this.tabCommandBrowser.Text = "Command Browser";
            // 
            // commandBrowser
            // 
            this.commandBrowser.ActiveEnvironment = null;
            this.commandBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandBrowser.Location = new System.Drawing.Point(0, 0);
            this.commandBrowser.Name = "commandBrowser";
            this.commandBrowser.Size = new System.Drawing.Size(776, 488);
            this.commandBrowser.TabIndex = 0;
            // 
            // tabEventMonitor
            // 
            this.tabEventMonitor.Controls.Add(this.eventMonitor);
            this.tabEventMonitor.ImageIndex = 2;
            this.tabEventMonitor.Location = new System.Drawing.Point(4, 23);
            this.tabEventMonitor.Name = "tabEventMonitor";
            this.tabEventMonitor.Size = new System.Drawing.Size(776, 488);
            this.tabEventMonitor.TabIndex = 1;
            this.tabEventMonitor.Text = "Event Monitor";
            // 
            // eventMonitor
            // 
            this.eventMonitor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventMonitor.Location = new System.Drawing.Point(0, 0);
            this.eventMonitor.Name = "eventMonitor";
            this.eventMonitor.Size = new System.Drawing.Size(776, 488);
            this.eventMonitor.TabIndex = 0;
            // 
            // tabGlobalParameters
            // 
            this.tabGlobalParameters.Controls.Add(this.globalParameterBrowser);
            this.tabGlobalParameters.ImageIndex = 3;
            this.tabGlobalParameters.Location = new System.Drawing.Point(4, 23);
            this.tabGlobalParameters.Name = "tabGlobalParameters";
            this.tabGlobalParameters.Size = new System.Drawing.Size(776, 488);
            this.tabGlobalParameters.TabIndex = 2;
            this.tabGlobalParameters.Text = "Global Parameters";
            // 
            // globalParameterBrowser
            // 
            this.globalParameterBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.globalParameterBrowser.Location = new System.Drawing.Point(0, 0);
            this.globalParameterBrowser.Name = "globalParameterBrowser";
            this.globalParameterBrowser.SelectedObject = null;
            this.globalParameterBrowser.Size = new System.Drawing.Size(776, 488);
            this.globalParameterBrowser.TabIndex = 0;
            // 
            // tabProcessBrowser
            // 
            this.tabProcessBrowser.Controls.Add(this.processBrowser);
            this.tabProcessBrowser.ImageIndex = 4;
            this.tabProcessBrowser.Location = new System.Drawing.Point(4, 23);
            this.tabProcessBrowser.Name = "tabProcessBrowser";
            this.tabProcessBrowser.Size = new System.Drawing.Size(776, 488);
            this.tabProcessBrowser.TabIndex = 4;
            this.tabProcessBrowser.Text = "Process Browser";
            // 
            // processBrowser
            // 
            this.processBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processBrowser.Location = new System.Drawing.Point(0, 0);
            this.processBrowser.Name = "processBrowser";
            this.processBrowser.ProcessId = 0;
            this.processBrowser.Size = new System.Drawing.Size(776, 488);
            this.processBrowser.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Spy for Solid Edge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Application_FormClosing);
            this.Load += new System.EventHandler(this.Application_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabObjectBrowser.ResumeLayout(false);
            this.tabTypeBrowser.ResumeLayout(false);
            this.tabCommandBrowser.ResumeLayout(false);
            this.tabEventMonitor.ResumeLayout(false);
            this.tabGlobalParameters.ResumeLayout(false);
            this.tabProcessBrowser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Timer startupTimer;
        private SolidEdge.Spy.Forms.ObjectBrowser objectBrowser;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem projectWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem projectForumsToolStripMenuItem;
        private SolidEdge.Spy.Forms.TabControlEx tabControl;
        private System.Windows.Forms.TabPage tabObjectBrowser;
        private System.Windows.Forms.TabPage tabEventMonitor;
        private SolidEdge.Spy.Forms.EventMonitor eventMonitor;
        private System.Windows.Forms.ToolStripMenuItem objectBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventMonitorToolStripMenuItem;
        private System.Windows.Forms.Timer eventMonitorTimer;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TabPage tabGlobalParameters;
        private System.Windows.Forms.ToolStripMenuItem globalParametersToolStripMenuItem;
        private System.Windows.Forms.TabPage tabTypeBrowser;
        private System.Windows.Forms.ToolStripMenuItem typeBrowserToolStripMenuItem;
        private Forms.TypeBrowser typeBrowser;
        private Forms.GlobalParameterBrowser globalParameterBrowser;
        private System.Windows.Forms.ToolStripMenuItem githubToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem githubSamplesForSolidEdgeToolStripMenuItem;
        private System.Windows.Forms.TabPage tabProcessBrowser;
        private Forms.ProcessBrowser processBrowser;
        private System.Windows.Forms.ToolStripMenuItem processBrowserToolStripMenuItem;
        private System.Windows.Forms.TabPage tabCommandBrowser;
        private Forms.CommandBrowser commandBrowser;
        private System.Windows.Forms.ToolStripMenuItem solidEdgeCommunityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuGetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nugetInteropSolidEdgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nugetSolidEdgeCommunityToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nugetSolidEdgeCommunityReaderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandBrowserToolStripMenuItem;
    }
}

