using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Spy.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsIDispatch(this object o)
        {
            return o is SolidEdge.Spy.InteropServices.IDispatch;
        }

        public static object SafeInvokeGetProperty(this object o, string name, object defaultValue)
        {
            try
            {
                if (o.IsIDispatch())
                {
                    return o.GetType().InvokeMember(name, BindingFlags.GetProperty, null, o, null);
                }
            }
            catch
            {
                //GlobalExceptionHandler.HandleException();
            }

            return defaultValue;
        }
    }
}
