namespace SolidEdge.Spy.Forms
{
    partial class ObjectBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectBrowser));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonNullObjects = new System.Windows.Forms.ToolStripButton();
            this.buttonEmptyCollection = new System.Windows.Forms.ToolStripButton();
            this.buttonProperties = new System.Windows.Forms.ToolStripButton();
            this.buttonMethods = new System.Windows.Forms.ToolStripButton();
            this.separatorNavigation = new System.Windows.Forms.ToolStripSeparator();
            this.splitContainerInner = new System.Windows.Forms.SplitContainer();
            this.splitContainerOuter = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.comTreeView = new SolidEdge.Spy.Forms.ComTreeView();
            this.typeInfoRichTextBox = new SolidEdge.Spy.Forms.TypeInfoRichTextBox();
            this.comPropertyGrid = new SolidEdge.Spy.Forms.ComPropertyGrid();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).BeginInit();
            this.splitContainerInner.Panel1.SuspendLayout();
            this.splitContainerInner.Panel2.SuspendLayout();
            this.splitContainerInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).BeginInit();
            this.splitContainerOuter.Panel1.SuspendLayout();
            this.splitContainerOuter.Panel2.SuspendLayout();
            this.splitContainerOuter.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Method_636.png");
            this.imageList.Images.SetKeyName(1, "Property_501.png");
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonRefresh,
            this.toolStripSeparator1,
            this.buttonNullObjects,
            this.buttonEmptyCollection,
            this.buttonProperties,
            this.buttonMethods,
            this.separatorNavigation});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(414, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
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
            // buttonNullObjects
            // 
            this.buttonNullObjects.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonNullObjects.Image = global::SolidEdge.Spy.Properties.Resources.ComTreeItemGray_16x16;
            this.buttonNullObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNullObjects.Name = "buttonNullObjects";
            this.buttonNullObjects.Size = new System.Drawing.Size(23, 22);
            this.buttonNullObjects.Text = "Null Objects";
            this.buttonNullObjects.ToolTipText = "Show NULL Objects";
            this.buttonNullObjects.Click += new System.EventHandler(this.buttonNullObjects_Click);
            // 
            // buttonEmptyCollection
            // 
            this.buttonEmptyCollection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonEmptyCollection.Image = global::SolidEdge.Spy.Properties.Resources.ComTreeItemsGray_16x16;
            this.buttonEmptyCollection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonEmptyCollection.Name = "buttonEmptyCollection";
            this.buttonEmptyCollection.Size = new System.Drawing.Size(23, 22);
            this.buttonEmptyCollection.Text = "Empty Collections";
            this.buttonEmptyCollection.ToolTipText = "Show Empty Collections";
            this.buttonEmptyCollection.Click += new System.EventHandler(this.buttonEmptyCollection_Click);
            // 
            // buttonProperties
            // 
            this.buttonProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonProperties.Image = global::SolidEdge.Spy.Properties.Resources.Property_16x16;
            this.buttonProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonProperties.Name = "buttonProperties";
            this.buttonProperties.Size = new System.Drawing.Size(23, 22);
            this.buttonProperties.Text = "Properties";
            this.buttonProperties.ToolTipText = "Show Properties";
            this.buttonProperties.Click += new System.EventHandler(this.buttonProperties_Click);
            // 
            // buttonMethods
            // 
            this.buttonMethods.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonMethods.Image = global::SolidEdge.Spy.Properties.Resources.Method_16x16;
            this.buttonMethods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonMethods.Name = "buttonMethods";
            this.buttonMethods.Size = new System.Drawing.Size(23, 22);
            this.buttonMethods.Text = "Show Methods";
            this.buttonMethods.Click += new System.EventHandler(this.buttonMethods_Click);
            // 
            // separatorNavigation
            // 
            this.separatorNavigation.Name = "separatorNavigation";
            this.separatorNavigation.Size = new System.Drawing.Size(6, 25);
            // 
            // splitContainerInner
            // 
            this.splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerInner.Location = new System.Drawing.Point(0, 0);
            this.splitContainerInner.Name = "splitContainerInner";
            this.splitContainerInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerInner.Panel1
            // 
            this.splitContainerInner.Panel1.Controls.Add(this.comTreeView);
            // 
            // splitContainerInner.Panel2
            // 
            this.splitContainerInner.Panel2.Controls.Add(this.typeInfoRichTextBox);
            this.splitContainerInner.Size = new System.Drawing.Size(242, 329);
            this.splitContainerInner.SplitterDistance = 207;
            this.splitContainerInner.TabIndex = 2;
            // 
            // splitContainerOuter
            // 
            this.splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerOuter.Location = new System.Drawing.Point(0, 25);
            this.splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            this.splitContainerOuter.Panel1.Controls.Add(this.splitContainerInner);
            // 
            // splitContainerOuter.Panel2
            // 
            this.splitContainerOuter.Panel2.Controls.Add(this.comPropertyGrid);
            this.splitContainerOuter.Size = new System.Drawing.Size(414, 329);
            this.splitContainerOuter.SplitterDistance = 242;
            this.splitContainerOuter.TabIndex = 3;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            this.contextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_ItemClicked);
            // 
            // comTreeView
            // 
            this.comTreeView.BackColor = System.Drawing.SystemColors.Window;
            this.comTreeView.ContextMenuStrip = this.contextMenuStrip;
            this.comTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comTreeView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.comTreeView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comTreeView.FullRowSelect = true;
            this.comTreeView.HideSelection = false;
            this.comTreeView.HotTracking = true;
            this.comTreeView.ImageIndex = 0;
            this.comTreeView.ItemHeight = 20;
            this.comTreeView.Location = new System.Drawing.Point(0, 0);
            this.comTreeView.Name = "comTreeView";
            this.comTreeView.SelectedImageIndex = 0;
            this.comTreeView.ShowEmptyCollections = false;
            this.comTreeView.ShowLines = false;
            this.comTreeView.ShowMethods = false;
            this.comTreeView.ShowNullObjects = false;
            this.comTreeView.ShowProperties = false;
            this.comTreeView.Size = new System.Drawing.Size(242, 207);
            this.comTreeView.TabIndex = 0;
            this.comTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.comTreeView_AfterSelect);
            this.comTreeView.Enter += new System.EventHandler(this.comTreeView_Enter);
            // 
            // typeInfoRichTextBox
            // 
            this.typeInfoRichTextBox.BackColor = System.Drawing.Color.White;
            this.typeInfoRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.typeInfoRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeInfoRichTextBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeInfoRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.typeInfoRichTextBox.Name = "typeInfoRichTextBox";
            this.typeInfoRichTextBox.ReadOnly = true;
            this.typeInfoRichTextBox.Size = new System.Drawing.Size(242, 118);
            this.typeInfoRichTextBox.TabIndex = 0;
            this.typeInfoRichTextBox.Text = "";
            this.typeInfoRichTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.typeInfoRichTextBox_LinkClicked);
            // 
            // comPropertyGrid
            // 
            this.comPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comPropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.comPropertyGrid.Name = "comPropertyGrid";
            this.comPropertyGrid.SelectedObject = null;
            this.comPropertyGrid.Size = new System.Drawing.Size(168, 329);
            this.comPropertyGrid.TabIndex = 0;
            this.comPropertyGrid.SelectedGridItemChanged += new System.Windows.Forms.SelectedGridItemChangedEventHandler(this.comPropertyGrid_SelectedGridItemChanged);
            this.comPropertyGrid.Enter += new System.EventHandler(this.comPropertyGrid_Enter);
            // 
            // ObjectBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerOuter);
            this.Controls.Add(this.toolStrip);
            this.Name = "ObjectBrowser";
            this.Size = new System.Drawing.Size(414, 354);
            this.Load += new System.EventHandler(this.ComBrowser_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainerInner.Panel1.ResumeLayout(false);
            this.splitContainerInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerInner)).EndInit();
            this.splitContainerInner.ResumeLayout(false);
            this.splitContainerOuter.Panel1.ResumeLayout(false);
            this.splitContainerOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerOuter)).EndInit();
            this.splitContainerOuter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComTreeView comTreeView;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonNullObjects;
        private System.Windows.Forms.ToolStripButton buttonEmptyCollection;
        private System.Windows.Forms.SplitContainer splitContainerInner;
        private SolidEdge.Spy.Forms.TypeInfoRichTextBox typeInfoRichTextBox;
        private System.Windows.Forms.SplitContainer splitContainerOuter;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator separatorNavigation;
        private ComPropertyGrid comPropertyGrid;
        private System.Windows.Forms.ToolStripButton buttonProperties;
        private System.Windows.Forms.ToolStripButton buttonMethods;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
    }
}
