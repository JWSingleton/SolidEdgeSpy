using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolidEdge.Spy.Extensions
{
    public static class ControlExtensions
    {
        public static void InvokeIfRequired<TControl>(this TControl control, Action<TControl> action)
            where TControl : Control
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, control);
            }
            else
            {
                action(control);
            }
        }

        public static void BeginInvokeIfRequired<TControl>(this TControl control, Action<TControl> action)
            where TControl : Control
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action, control);
            }
            else
            {
                action(control);
            }
        }
    }
}
