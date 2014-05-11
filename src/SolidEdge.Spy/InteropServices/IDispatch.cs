using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace SolidEdge.Spy.InteropServices
{
    [ComImport()]
    [Guid(NativeMethods.IID_IDispatch)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDispatch
    {
        int GetTypeInfoCount();

        ITypeInfo GetTypeInfo(int iTInfo, int lcid);

        [PreserveSig]
        int GetIDsOfNames
        (
            ref Guid riid,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)]
            string[] rgsNames,
            int cNames,
            int lcid,
            [MarshalAs(UnmanagedType.LPArray)]
            int[] rgDispId
        );

        [PreserveSig]
        int Invoke(
            int dispIdMember,
            ref Guid riid,
            int lcid,
            System.Runtime.InteropServices.ComTypes.INVOKEKIND wFlags,
            ref System.Runtime.InteropServices.ComTypes.DISPPARAMS pDispParams,
            out Variant pvarResult,
            ref System.Runtime.InteropServices.ComTypes.EXCEPINFO pExcepInfo,
            out uint puArgErr);
    }
}
