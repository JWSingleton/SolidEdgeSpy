using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public static class IntPtrExtensions
    {
        public static T ToStructure<T>(this IntPtr p)
        {
            return (T)Marshal.PtrToStructure(p, typeof(T));
        }
    }
}
