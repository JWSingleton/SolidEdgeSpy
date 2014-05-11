using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public static class ITypeInfoExtensions
    {
        public static Guid GetGuid(this ITypeInfo typeInfo)
        {
            System.Runtime.InteropServices.ComTypes.TYPEATTR attr;
            IntPtr p = IntPtr.Zero;

            typeInfo.GetTypeAttr(out p);

            try
            {
                attr = p.ToStructure<System.Runtime.InteropServices.ComTypes.TYPEATTR>();
                return attr.guid;
            }
            finally
            {
                typeInfo.ReleaseTypeAttr(p);
            }
        }

        public static Version GetVersion(this ITypeInfo typeInfo)
        {
            System.Runtime.InteropServices.ComTypes.TYPEATTR attr;
            IntPtr p = IntPtr.Zero;

            typeInfo.GetTypeAttr(out p);

            try
            {
                attr = p.ToStructure<System.Runtime.InteropServices.ComTypes.TYPEATTR>();
                return new Version(attr.wMajorVerNum, attr.wMinorVerNum);
            }
            finally
            {
                typeInfo.ReleaseTypeAttr(p);
            }
        }
    }
}
