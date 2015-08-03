namespace SolidEdge.Spy.Forms
{
    partial class GlobalParameterBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalParameterBrowser));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonRefresh = new System.Windows.Forms.ToolStripButton();
            this.textBoxSearch = new SolidEdge.Spy.Forms.ToolStripSpringTextBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolStrip.SuspendLayout();
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
            this.textBoxSearch});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(406, 25);
            this.toolStrip.TabIndex = 2;
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
            // textBoxSearch
            // 
            this.textBoxSearch.InactiveText = "<Filter>";
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.textBoxSearch.Size = new System.Drawing.Size(100, 25);
            this.textBoxSearch.Text = "<Filter>";
            this.textBoxSearch.TextAccepted += new System.EventHandler(this.textBoxSearch_TextAccepted);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 25);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid.Size = new System.Drawing.Size(406, 316);
            this.propertyGrid.TabIndex = 3;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // GlobalParameterBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.toolStrip);
            this.Name = "GlobalParameterBrowser";
            this.Size = new System.Drawing.Size(406, 341);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton buttonRefresh;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private ToolStripSpringTextBox textBoxSearch;
    }
}
