using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    static class NativeMethods
    {
        public const string IID_NULL = "00000000-0000-0000-0000-000000000000";
        public const string IID_IUnknown = "00000000-0000-0000-C000-000000000046";
        public const string IID_IDispatch = "00020400-0000-0000-C000-000000000046";

        public const int LOCALE_SYSTEM_DEFAULT = 2048;

        public const int DISPID_UNKNOWN = -1;
        public const int DISPID_VALUE = 0;
        public const int DISPID_PROPERTYPUT = -3;
        public const int DISPID_NEWENUM = -4;
        public const int DISPID_EVALUATE = -5;

        public const int S_OK = 0;
        public const int S_FALSE = 1;

        public static bool Succeeded(int hr)
        {
            return hr >= 0;
        }
        
        public static bool Failed(int hr)
        {
            return hr < 0;
        }

        #region ObjIdl.h

        internal enum SERVERCALL
        {
            SERVERCALL_ISHANDLED = 0,
            SERVERCALL_REJECTED = 1,
            SERVERCALL_RETRYLATER = 2
        }

        internal enum PENDINGMSG
        {
            PENDINGMSG_CANCELCALL = 0,
            PENDINGMSG_WAITNOPROCESS = 1,
            PENDINGMSG_WAITDEFPROCESS = 2
        }

        #endregion

        #region WinError.h

        internal const int MK_E_UNAVAILABLE = (int)(0x800401E3 - 0x100000000);
        internal const int CO_E_NOT_SUPPORTED = (int)(0x80004021 - 0x100000000);

        #endregion

        [DllImport("ole32.dll", ExactSpelling = true)]
        internal static extern int CoCreateInstance(
            [In, MarshalAs(UnmanagedType.LPStruct)]
            Guid clsid,
            IntPtr pUnkOuter,
            int dwClsContext,
            [In, MarshalAs(UnmanagedType.LPStruct)]
            Guid iid,
            out IntPtr ppv);

        [DllImport("ole32.dll")]
        internal static extern int CoRegisterMessageFilter(IMessageFilter newFilter, out IMessageFilter oldFilter);

        [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
        internal static extern int CLSIDFromString(string lpsz, out Guid clsid);

        [DllImport("ole32.dll")]
        internal static extern int OleRun(ComPtr pUnk);

        [DllImport("oleaut32.dll")]
        internal static extern int GetActiveObject(
            [MarshalAs(UnmanagedType.LPStruct)]
            Guid clsid,
            IntPtr pReserved,
            out IntPtr pUnk);

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        [LCIDConversion(3)]
        public static extern System.Runtime.InteropServices.ComTypes.ITypeLib LoadRegTypeLib(
            [MarshalAs(UnmanagedType.LPStruct)]
            Guid guid,
            [In, MarshalAs(UnmanagedType.U2)]
            short wVerMajor,
            [In, MarshalAs(UnmanagedType.U2)]
            short wVerMinor);

        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.BStr)]
        public static extern string QueryPathOfRegTypeLib(
            [MarshalAs(UnmanagedType.LPStruct)]
            Guid guid,
            [MarshalAs(UnmanagedType.U2)]
            short wVerMajor,
            [MarshalAs(UnmanagedType.U2)]
            short wVerMinor,
            [MarshalAs(UnmanagedType.U4)]
            int lcid);

        [DllImport("oleaut32.dll", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int VariantClear(IntPtr pvarg);

    }
}
