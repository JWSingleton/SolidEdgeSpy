using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolidEdge.Spy.Forms
{
    public class TabControlEx : TabControl
    {
        private bool _headerVisible = true;

        public TabControlEx()
            : base()
        {
        }

        protected override void WndProc(ref Message m)
        {
            bool bHandled = false;

            if (!DesignMode)
            {
                // Hide tabs by trapping the TCM_ADJUSTRECT message
                if (m.Msg == 0x1328)
                {
                    if (_headerVisible == false)
                    {
                        m.Result = (IntPtr)1;
                        bHandled = true;
                    }
                }
            }

            if (bHandled == false)
            {
                base.WndProc(ref m);
            }
        }

        public bool HeaderVisible
        {
            get { return _headerVisible; }
            set { _headerVisible = value; }
        }
    }
}
