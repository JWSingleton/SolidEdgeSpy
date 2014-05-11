using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public static class ITypeLibExtensions
    {
        public static Guid GetGuid(this ITypeLib typeLib)
        {
            System.Runtime.InteropServices.ComTypes.TYPELIBATTR attr;
            IntPtr p = IntPtr.Zero;

            typeLib.GetLibAttr(out p);

            try
            {
                attr = p.ToStructure<System.Runtime.InteropServices.ComTypes.TYPELIBATTR>();
                return attr.guid;
            }
            finally
            {
                typeLib.ReleaseTLibAttr(p);
            }
        }

        public static System.Runtime.InteropServices.ComTypes.TYPELIBATTR GetLibAttr(this ITypeLib typeLib)
        {
            System.Runtime.InteropServices.ComTypes.TYPELIBATTR attr;
            IntPtr p = IntPtr.Zero;

            typeLib.GetLibAttr(out p);

            try
            {
                attr = p.ToStructure<System.Runtime.InteropServices.ComTypes.TYPELIBATTR>();
            }
            finally
            {
                typeLib.ReleaseTLibAttr(p);
            }

            return attr;
        }

        public static Version GetVersion(this ITypeLib typeLib)
        {
            System.Runtime.InteropServices.ComTypes.TYPELIBATTR attr;
            IntPtr p = IntPtr.Zero;

            typeLib.GetLibAttr(out p);

            try
            {
                attr = p.ToStructure<System.Runtime.InteropServices.ComTypes.TYPELIBATTR>();
                return new Version(attr.wMajorVerNum, attr.wMinorVerNum);
            }
            finally
            {
                typeLib.ReleaseTLibAttr(p);
            }
        }
    }
}
