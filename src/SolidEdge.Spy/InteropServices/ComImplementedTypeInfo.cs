using SolidEdge.Spy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public class ComImplementedTypeInfo
    {
        private ComTypeInfo _comTypeInfo = null;
        private System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS _implTypeFlags = default(System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS);

        public ComImplementedTypeInfo(ComTypeInfo comTypeInfo, System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS implTypeFlags)
        {
            _comTypeInfo = comTypeInfo;
            _implTypeFlags = implTypeFlags;
        }

        public ComTypeInfo ComTypeInfo { get { return _comTypeInfo; } }
        public System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS ImplementedTypeFlags { get { return _implTypeFlags; } }

        public bool IsDefault { get { return _implTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULT); } }
        public bool IsSource { get { return _implTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS.IMPLTYPEFLAG_FSOURCE); } }
        public bool IsRestricted { get { return _implTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS.IMPLTYPEFLAG_FRESTRICTED); } }
        public bool IsDefaultVTable { get { return _implTypeFlags.IsSet(System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS.IMPLTYPEFLAG_FDEFAULTVTABLE); } }
    }
}
