namespace SolidEdge.Spy.Forms
{
    partial class EventMonitor
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonSelectEvents = new System.Windows.Forms.ToolStripSplitButton();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iSEFooEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonErase = new System.Windows.Forms.ToolStripButton();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.listView = new SolidEdge.Spy.Forms.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonSelectEvents,
            this.toolStripSeparator1,
            this.buttonErase});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(554, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonSelectEvents
            // 
            this.buttonSelectEvents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSelectEvents.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.iSEFooEventsToolStripMenuItem});
            this.buttonSelectEvents.Image = global::SolidEdge.Spy.Properties.Resources.Event_16x16;
            this.buttonSelectEvents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSelectEvents.Name = "buttonSelectEvents";
            this.buttonSelectEvents.Size = new System.Drawing.Size(32, 22);
            this.buttonSelectEvents.Text = "Events";
            this.buttonSelectEvents.ButtonClick += new System.EventHandler(this.buttonSelectEvents_ButtonClick);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Checked = true;
            this.testToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.testToolStripMenuItem.Text = "ISEApplicationEvents";
            // 
            // iSEFooEventsToolStripMenuItem
            // 
            this.iSEFooEventsToolStripMenuItem.Name = "iSEFooEventsToolStripMenuItem";
            this.iSEFooEventsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.iSEFooEventsToolStripMenuItem.Text = "ISEFooEvents";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonErase
            // 
            this.buttonErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonErase.Image = global::SolidEdge.Spy.Properties.Resources.Erase_16x16;
            this.buttonErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonErase.Name = "buttonErase";
            this.buttonErase.Size = new System.Drawing.Size(23, 22);
            this.buttonErase.Text = "Clear";
            this.buttonErase.Click += new System.EventHandler(this.buttonErase_Click);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 25);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(554, 367);
            this.listView.SmallImageList = this.imageList;
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Event";
            this.columnHeader1.Width = 42;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Environment Name";
            this.columnHeader2.Width = 137;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Environment Caption";
            this.columnHeader3.Width = 139;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Environment CATID";
            this.columnHeader4.Width = 121;
            // 
            // EventMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.Name = "EventMonitor";
            this.Size = new System.Drawing.Size(554, 392);
            this.Load += new System.EventHandler(this.EventBrowser_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonErase;
        private SolidEdge.Spy.Forms.ListViewEx listView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripSplitButton buttonSelectEvents;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iSEFooEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
