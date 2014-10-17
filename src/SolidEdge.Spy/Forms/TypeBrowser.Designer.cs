namespace SolidEdge.Spy.Forms
{
    partial class TypeBrowser
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
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.comTypeTreeView = new SolidEdge.Spy.Forms.ComTypeTreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comTypeListView = new SolidEdge.Spy.Forms.ComTypeListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeInfoRichTextBox = new SolidEdge.Spy.Forms.TypeInfoRichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonBack = new System.Windows.Forms.ToolStripButton();
            this.buttonForward = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.textBoxSearch = new SolidEdge.Spy.Forms.ToolStripSpringTextBox();
            this.buttonSearch = new System.Windows.Forms.ToolStripButton();
            this.buttonClearSearch = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerOuter
            // 
            this.splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOuter.Location = new System.Drawing.Point(0, 50);
            this.splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.comTypeTreeView);
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainerOuter.Size = new System.Drawing.Size(401, 287);
            this.splitContainerOuter.SplitterDistance = 124;
            this.splitContainerOuter.TabIndex = 0;
            // 
            // comTypeTreeView
            // 
            this.comTypeTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comTypeTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.comTypeTreeView.Filter = null;
            this.comTypeTreeView.FullRowSelect = true;
            this.comTypeTreeView.HideSelection = false;
            this.comTypeTreeView.HotTracking = true;
            this.comTypeTreeView.ImageIndex = 0;
            this.comTypeTreeView.ItemHeight = 20;
            this.comTypeTreeView.Location = new System.Drawing.Point(0, 0);
            this.comTypeTreeView.Name = "comTypeTreeView";
            this.comTypeTreeView.SelectedImageIndex = 0;
            this.comTypeTreeView.ShowLines = false;
            this.comTypeTreeView.Size = new System.Drawing.Size(124, 287);
            this.comTypeTreeView.TabIndex = 0;
            this.comTypeTreeView.FilterChanged += new System.EventHandler(this.comTypeTreeView_FilterChanged);
            this.comTypeTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.comTypeTreeView_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comTypeListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.typeInfoRichTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(273, 287);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 0;
            // 
            // comTypeListView
            // 
            this.comTypeListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.comTypeListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comTypeListView.FullRowSelect = true;
            this.comTypeListView.HideSelection = false;
            this.comTypeListView.Location = new System.Drawing.Point(0, 0);
            this.comTypeListView.MultiSelect = false;
            this.comTypeListView.Name = "comTypeListView";
            this.comTypeListView.SelectedComTypeInfo = null;
            this.comTypeListView.Size = new System.Drawing.Size(273, 178);
            this.comTypeListView.TabIndex = 0;
            this.comTypeListView.UseCompatibleStateImageBehavior = false;
            this.comTypeListView.View = System.Windows.Forms.View.Details;
            this.comTypeListView.SelectedIndexChanged += new System.EventHandler(this.comTypeListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 229;
            // 
            // typeInfoRichTextBox
            // 
            this.typeInfoRichTextBox.BackColor = System.Drawing.Color.White;
            this.typeInfoRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.typeInfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeInfoRichTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.typeInfoRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.typeInfoRichTextBox.Name = "typeInfoRichTextBox";
            this.typeInfoRichTextBox.ReadOnly = true;
            this.typeInfoRichTextBox.Size = new System.Drawing.Size(273, 105);
            this.typeInfoRichTextBox.TabIndex = 0;
            this.typeInfoRichTextBox.Text = "";
            this.typeInfoRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.typeInfoRichTextBox_LinkClicked);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRefresh,
            this.toolStripSeparator1,
            this.buttonBack,
            this.buttonForward});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(401, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonRefresh.Image = global::SolidEdge.Spy.Properties.Resources.Refresh_16x16;
            this.buttonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(23, 22);
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonBack
            // 
            this.buttonBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonBack.Image = global::SolidEdge.Spy.Properties.Resources.ArrowBack_16x16;
            this.buttonBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(23, 22);
            this.buttonBack.Text = "Back";
            // 
            // buttonForward
            // 
            this.buttonForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonForward.Image = global::SolidEdge.Spy.Properties.Resources.ArrowForward_16x16;
            this.buttonForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(23, 22);
            this.buttonForward.Text = "Forward";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textBoxSearch,
            this.buttonSearch,
            this.buttonClearSearch});
            this.toolStrip2.Location = new System.Drawing.Point(0, 25);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(401, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.textBoxSearch.Size = new System.Drawing.Size(100, 25);
            this.textBoxSearch.Text = "<Search>";
            this.textBoxSearch.TextAccepted += new System.EventHandler(this.textBoxSearch_TextAccepted);
            // 
            // buttonSearch
            // 
            this.buttonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSearch.Image = global::SolidEdge.Spy.Properties.Resources.Find_16x16;
            this.buttonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(23, 22);
            this.buttonSearch.Text = "Search";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonClearSearch
            // 
            this.buttonClearSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClearSearch.Image = global::SolidEdge.Spy.Properties.Resources.ClearSearch_16x16;
            this.buttonClearSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClearSearch.Name = "buttonClearSearch";
            this.buttonClearSearch.Size = new System.Drawing.Size(23, 22);
            this.buttonClearSearch.Text = "Clear Search";
            this.buttonClearSearch.Click += new System.EventHandler(this.buttonClearSearch_Click);
            // 
            // TypeBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerOuter);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TypeBrowser";
            this.Size = new System.Drawing.Size(401, 337);
            this.Load += new System.EventHandler(this.TypeBrowser_Load);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).EndInit();
            this.splitContainerOuter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerOuter;
        private ComTypeTreeView comTypeTreeView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ComTypeListView comTypeListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private TypeInfoRichTextBox typeInfoRichTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonBack;
        private System.Windows.Forms.ToolStripButton buttonForward;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private ToolStripSpringTextBox textBoxSearch;
        private System.Windows.Forms.ToolStripButton buttonSearch;
        private System.Windows.Forms.ToolStripButton buttonClearSearch;
    }
}
