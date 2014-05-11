using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

namespace SolidEdge.Spy.InteropServices
{
    public static class IDispatchExtensions
    {
        public static ITypeInfo GetTypeInfo(this IDispatch dispatch)
        {
            return dispatch.GetTypeInfo(0, NativeMethods.LOCALE_SYSTEM_DEFAULT);
        }

        public static int GetIdOfName(this IDispatch dispatch, string name, out int dispid)
        {
            var rgDispId = new int[] { 0 };
            string[] rgsNames = { name };
            var riid = new Guid(NativeMethods.IID_NULL);
            int hr = dispatch.GetIDsOfNames(ref riid, rgsNames, rgsNames.Length, NativeMethods.LOCALE_SYSTEM_DEFAULT, rgDispId);
            dispid = rgDispId[0];
            return hr;
        }

        public static int InvokePropertyGet(this IDispatch dispatch, string name, out object value)
        {
            int hr = NativeMethods.S_OK;
            value = null;
            int dispid = 0;

            if (MarshalEx.Succeeded(dispatch.GetIdOfName(name, out dispid)))
            {
                hr = dispatch.InvokePropertyGet(dispid, out value);
            }

            return hr;
        }

        public static int InvokePropertyGet(this IDispatch dispatch, int dispid, out object value)
        {
            int hr = NativeMethods.S_OK;
            value = null;

            var guid = Guid.Empty;
            var lcid = NativeMethods.LOCALE_SYSTEM_DEFAULT;
            var flags = System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYGET;
            var dp = default(System.Runtime.InteropServices.ComTypes.DISPPARAMS);
            var pExcepInfo = default(System.Runtime.InteropServices.ComTypes.EXCEPINFO);
            uint pArgErr = 0;

            Variant variant = default(Variant);

            try
            {
                hr = dispatch.Invoke(
                                    dispid,
                                    ref guid,
                                    lcid,
                                    flags,
                                    ref dp,
                                    out variant,
                                    ref pExcepInfo,
                                    out pArgErr
                                    );

                if (MarshalEx.Succeeded(hr))
                {
                    try
                    {
                        switch (variant.VariantType)
                        {
                            case VarEnum.VT_PTR:
                                GlobalExceptionHandler.HandleException();
                                break;
                            case VarEnum.VT_UNKNOWN:
                            case VarEnum.VT_DISPATCH:
                                if (variant.ptr1.Equals(IntPtr.Zero) == false)
                                {
                                    value = new ComPtr(variant.ptr1);
                                    int count = Marshal.Release(variant.ptr1);
                                }
                                break;
                            case VarEnum.VT_USERDEFINED:
                                value = variant.ToObject();
                                break;
                            default:
                                value = variant.ToObject();
                                break;
                        }
                    }
                    catch (COMException ex)
                    {
                        hr = ex.ErrorCode;
                    }
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
            finally
            {
                variant.Clear();
            }

            return hr;
        }

        public static int InvokePropertySet(this IDispatch dispatch, string name, object value)
        {
            int hr = NativeMethods.S_OK;
            int dispid = 0;

            if (MarshalEx.Succeeded(dispatch.GetIdOfName(name, out dispid)))
            {
                hr = dispatch.InvokePropertySet(dispid, value);
            }

            return hr;
        }

        public static int InvokePropertySet(this IDispatch dispatch, int dispid, object value)
        {
            int hr = NativeMethods.S_OK;

            var guid = Guid.Empty;
            var lcid = NativeMethods.LOCALE_SYSTEM_DEFAULT;
            var flags = System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_PROPERTYPUT;
            var pExcepInfo = default(System.Runtime.InteropServices.ComTypes.EXCEPINFO);
            uint pArgErr = 0;

            Variant pVarResult = default(Variant);
            VariantArgPtr va = new VariantArgPtr(1);
            
            var dp = new System.Runtime.InteropServices.ComTypes.DISPPARAMS()
            {
                cArgs = va.Count,
                rgvarg = va,
                cNamedArgs = 1,
                rgdispidNamedArgs = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)))
            };

            Marshal.WriteInt32(dp.rgdispidNamedArgs, (int)NativeMethods.DISPID_PROPERTYPUT);

            if (value is VariantWrapper)
            {
                Variant variant = new Variant((VariantWrapper)value);
                Marshal.StructureToPtr(variant, va[0], false);
            }
            else
            {
                Marshal.GetNativeVariantForObject(value, va[0]);
            }

            try
            {
                hr = dispatch.Invoke(
                                    dispid,
                                    ref guid,
                                    lcid,
                                    flags,
                                    ref dp,
                                    out pVarResult,
                                    ref pExcepInfo,
                                    out pArgErr
                                    );
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
            finally
            {
                Marshal.FreeCoTaskMem(dp.rgdispidNamedArgs);
                va.Dispose();
            }

            return hr;
        }

        public static int InvokeMethod(this IDispatch dispatch, string name, object[] args, out object returnValue)
        {
            int hr = NativeMethods.S_OK;
            int id = 0;
            returnValue = null;

            if (MarshalEx.Succeeded(hr = dispatch.GetIdOfName(name, out id)))
            {
                hr = InvokeMethod(dispatch, id, args, out returnValue);
            }

            return hr;
        }

        public static int InvokeMethod(this IDispatch dispatch, int dispId, object[] args, out object returnValue)
        {
            int hr = NativeMethods.S_OK;
            var riid = new Guid(NativeMethods.IID_NULL);
            VariantArgPtr va = new VariantArgPtr(args.Length);
            returnValue = null;

            var dp = new System.Runtime.InteropServices.ComTypes.DISPPARAMS()
            {
                cArgs = args.Length,
                rgvarg = va,
                cNamedArgs = 0,
                rgdispidNamedArgs = IntPtr.Zero
            };

            Array.Reverse(args);

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg is VariantWrapper)
                {
                    Variant variant = new Variant((VariantWrapper)arg);
                    Marshal.StructureToPtr(variant, va[i], false);
                }
                else
                {
                    Marshal.GetNativeVariantForObject(arg, va[i]);
                }
            }

            try
            {
                Variant pVarResult = default(Variant);
                var pExcepInfo = default(System.Runtime.InteropServices.ComTypes.EXCEPINFO);
                uint pArgErr = 0;

                hr = dispatch.Invoke
                (
                  dispId,
                  ref riid,
                  NativeMethods.LOCALE_SYSTEM_DEFAULT,
                  System.Runtime.InteropServices.ComTypes.INVOKEKIND.INVOKE_FUNC,
                  ref dp,
                  out pVarResult,
                  ref pExcepInfo,
                  out pArgErr
                );

                if (hr == 0)
                {
                    for (var i = 0; i < args.Length; i++)
                    {
                        var arg = args[i];

                        if (arg is VariantWrapper)
                        {
                            Variant variant = (Variant)Marshal.PtrToStructure(va[i], typeof(Variant));

                            try
                            {
                                switch (variant.VariantType)
                                {
                                    case VarEnum.VT_UNKNOWN:
                                    case VarEnum.VT_DISPATCH:
                                        args[i] = new ComPtr(variant.ptr1);
                                        if (((ComPtr)returnValue).IsInvalid == false)
                                        {
                                            Marshal.Release(variant.ptr1);
                                        }
                                        break;
                                    default:
                                        args[i] = variant.ToObject();
                                        break;
                                }
                            }
                            catch (COMException ex)
                            {
                                hr = ex.ErrorCode;
                            }

                            //VarEnum variantType = variant.VariantType & (VarEnum.VT_NULL | VarEnum.VT_I2 | VarEnum.VT_I4 | VarEnum.VT_R4 | VarEnum.VT_R8 | VarEnum.VT_CY | VarEnum.VT_DATE | VarEnum.VT_BSTR | VarEnum.VT_DISPATCH | VarEnum.VT_ERROR | VarEnum.VT_BOOL | VarEnum.VT_VARIANT | VarEnum.VT_UNKNOWN | VarEnum.VT_DECIMAL | VarEnum.VT_I1 | VarEnum.VT_UI1 | VarEnum.VT_UI2 | VarEnum.VT_UI4 | VarEnum.VT_I8 | VarEnum.VT_UI8 | VarEnum.VT_INT | VarEnum.VT_UINT | VarEnum.VT_VOID | VarEnum.VT_HRESULT | VarEnum.VT_PTR | VarEnum.VT_SAFEARRAY | VarEnum.VT_CARRAY | VarEnum.VT_USERDEFINED | VarEnum.VT_LPSTR | VarEnum.VT_LPWSTR | VarEnum.VT_RECORD | VarEnum.VT_FILETIME | VarEnum.VT_BLOB | VarEnum.VT_STREAM | VarEnum.VT_STORAGE | VarEnum.VT_STREAMED_OBJECT | VarEnum.VT_STORED_OBJECT | VarEnum.VT_BLOB_OBJECT | VarEnum.VT_CF | VarEnum.VT_CLSID | VarEnum.VT_VECTOR | VarEnum.VT_ARRAY);
                            //args[i] = variant.ToObject();
                            //args[i] = Marshal.GetObjectForNativeVariant(variant.variant_part.variant_union._dispatch);
                            //NativeMethods.VariantClear(variant.variant_part.variant_union.pointer_data);
                        }
                    }

                    switch (pVarResult.VariantType)
                    {
                        case VarEnum.VT_UNKNOWN:
                        case VarEnum.VT_DISPATCH:
                            returnValue = new ComPtr(pVarResult.ptr1);
                            if (((ComPtr)returnValue).IsInvalid == false)
                            {
                                Marshal.Release(pVarResult.ptr1);
                            }
                            break;
                        case VarEnum.VT_EMPTY:
                            break;
                        default:
                            returnValue = pVarResult.ToObject();
                            break;
                    }
                }
                else
                {
                    for (var i = 0; i < args.Length; i++)
                    {
                        if (args[i] is VariantWrapper)
                        {
                            args[i] = null;
                        }
                    }
                }

                Array.Reverse(args);
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
            finally
            {
                va.Dispose();
            }

            return hr;
        }
    }
}
