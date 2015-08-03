namespace SolidEdge.Spy.Forms
{
    partial class CommandBrowser
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
            this.listViewEx = new SolidEdge.Spy.Forms.ListViewEx();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.buttonStart = new System.Windows.Forms.ToolStripButton();
            this.textBoxCommandID = new System.Windows.Forms.ToolStripTextBox();
            this.labelCommandID = new System.Windows.Forms.ToolStripLabel();
            this.textBoxSearch = new SolidEdge.Spy.Forms.ToolStripSpringTextBox();
            this.buttonSearch = new System.Windows.Forms.ToolStripButton();
            this.buttonClearSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewEx
            // 
            this.listViewEx.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listViewEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEx.FullRowSelect = true;
            this.listViewEx.HideSelection = false;
            this.listViewEx.Location = new System.Drawing.Point(0, 25);
            this.listViewEx.Name = "listViewEx";
            this.listViewEx.Size = new System.Drawing.Size(469, 292);
            this.listViewEx.TabIndex = 7;
            this.listViewEx.UseCompatibleStateImageBehavior = false;
            this.listViewEx.View = System.Windows.Forms.View.Details;
            this.listViewEx.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewEx_ItemSelectionChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Command";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "ID";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonStart,
            this.textBoxCommandID,
            this.labelCommandID,
            this.textBoxSearch,
            this.buttonSearch,
            this.buttonClearSearch,
            this.toolStripSeparator1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(469, 25);
            this.toolStrip2.TabIndex = 6;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // buttonStart
            // 
            this.buttonStart.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buttonStart.Image = global::SolidEdge.Spy.Properties.Resources.Start_16x16;
            this.buttonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(111, 22);
            this.buttonStart.Text = "Start Command";
            this.buttonStart.ToolTipText = "Start Command";
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // textBoxCommandID
            // 
            this.textBoxCommandID.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.textBoxCommandID.Name = "textBoxCommandID";
            this.textBoxCommandID.Size = new System.Drawing.Size(75, 25);
            this.textBoxCommandID.TextChanged += new System.EventHandler(this.textBoxCommandID_TextChanged);
            // 
            // labelCommandID
            // 
            this.labelCommandID.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.labelCommandID.Name = "labelCommandID";
            this.labelCommandID.Size = new System.Drawing.Size(78, 22);
            this.labelCommandID.Text = "Command ID";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.InactiveText = "<Search>";
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.textBoxSearch.Size = new System.Drawing.Size(100, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // CommandBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listViewEx);
            this.Controls.Add(this.toolStrip2);
            this.Name = "CommandBrowser";
            this.Size = new System.Drawing.Size(469, 317);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewEx listViewEx;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton buttonStart;
        private System.Windows.Forms.ToolStripTextBox textBoxCommandID;
        private System.Windows.Forms.ToolStripLabel labelCommandID;
        private ToolStripSpringTextBox textBoxSearch;
        private System.Windows.Forms.ToolStripButton buttonSearch;
        private System.Windows.Forms.ToolStripButton buttonClearSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
