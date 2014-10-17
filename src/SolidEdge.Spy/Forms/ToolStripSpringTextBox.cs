using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolidEdge.Spy.Forms
{
    //http://msdn.microsoft.com/en-us/library/vstudio/ms404304.aspx
    public class ToolStripSpringTextBox : ToolStripTextBox
    {
        public event EventHandler TextAccepted;

        public ToolStripSpringTextBox()
        {
            this.Text = "<Search>";
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (this.Text.Equals("<Search>"))
            {
                this.Text = String.Empty;
            }
            else if (this.Text.Equals("<Filter>"))
            {
                this.Text = String.Empty;
            }

            base.OnGotFocus(e);
        }

        protected override bool ProcessCmdKey(ref Message m, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (TextAccepted != null)
                {
                    TextAccepted(this, new EventArgs());
                }

                return true;
            }

            return base.ProcessCmdKey(ref m, keyData);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.Text))
            {
                this.Text = "<Search>";
            }

            base.OnLostFocus(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (this.Focused == false)
            {
                if (String.IsNullOrEmpty(this.Text))
                {
                    this.Text = "<Search>";
                }
            }
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            if (DesignMode) return DefaultSize;

            // Use the default size if the text box is on the overflow menu
            // or is on a vertical ToolStrip.
            if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
            {
                return DefaultSize;
            }

            // Declare a variable to store the total available width as 
            // it is calculated, starting with the display width of the 
            // owning ToolStrip.
            Int32 width = Owner.DisplayRectangle.Width;

            // Subtract the width of the overflow button if it is displayed. 
            if (Owner.OverflowButton.Visible)
            {
                width = width - Owner.OverflowButton.Width -
                    Owner.OverflowButton.Margin.Horizontal;
            }

            // Declare a variable to maintain a count of ToolStripSpringTextBox 
            // items currently displayed in the owning ToolStrip. 
            Int32 springBoxCount = 0;

            foreach (ToolStripItem item in Owner.Items)
            {
                // Ignore items on the overflow menu.
                if (item.IsOnOverflow) continue;

                if (item is ToolStripSpringTextBox)
                {
                    // For ToolStripSpringTextBox items, increment the count and 
                    // subtract the margin width from the total available width.
                    springBoxCount++;
                    width -= item.Margin.Horizontal;
                }
                else
                {
                    // For all other items, subtract the full width from the total
                    // available width.
                    width = width - item.Width - item.Margin.Horizontal;
                }
            }

            // If there are multiple ToolStripSpringTextBox items in the owning
            // ToolStrip, divide the total available width between them. 
            if (springBoxCount > 1) width /= springBoxCount;

            // If the available width is less than the default width, use the
            // default width, forcing one or more items onto the overflow menu.
            if (width < DefaultSize.Width) width = DefaultSize.Width;

            // Retrieve the preferred size from the base class, but change the
            // width to the calculated width. 
            Size size = base.GetPreferredSize(constrainingSize);
            size.Width = width;
            return size;
        }
    }
}
