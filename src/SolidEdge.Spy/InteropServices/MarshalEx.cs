using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Spy.InteropServices
{
    public static class MarshalEx
    {
        public static int CreateInstance(string progID, out IntPtr p)
        {
            int hr = 0;
            Guid clsid = Guid.Empty;
            p = IntPtr.Zero;

            if (Succeeded(hr = NativeMethods.CLSIDFromString(progID, out clsid)))
            {
                Guid iid = new Guid(NativeMethods.IID_IUnknown);

                if (Succeeded(hr = NativeMethods.CoCreateInstance(clsid, IntPtr.Zero, 23, iid, out p)))
                {
                    hr = NativeMethods.OleRun(p);
                }
            }

            return hr;
        }

        public static int CreateInstance(string progID, out ComPtr p)
        {
            int hr = 0;
            Guid clsid = Guid.Empty;
            IntPtr pUnk = IntPtr.Zero;
            p = IntPtr.Zero;

            if (Succeeded(hr = CreateInstance(progID, out pUnk)))
            {
                p = new ComPtr(pUnk);
                Marshal.Release(pUnk);
            }

            return hr;
        }

        //public static int CreateInstance<T>(string progID, out ComPtr<T> p)
        //{
        //    int hr = 0;
        //    Guid clsid = Guid.Empty;
        //    IntPtr pUnk = IntPtr.Zero;
        //    p = IntPtr.Zero;

        //    if (Succeeded(hr = CreateInstance(progID, out pUnk)))
        //    {
        //        p = new ComPtr<T>(pUnk);
        //        Marshal.Release(pUnk);
        //    }

        //    return hr;
        //}

        public static int GetActiveObject(string progID, out IntPtr p)
        {
            int hr = 0;
            Guid clsid = Guid.Empty;
            p = IntPtr.Zero;

            if (Succeeded(hr = NativeMethods.CLSIDFromString(progID, out clsid)))
            {
                hr = NativeMethods.GetActiveObject(clsid, IntPtr.Zero, out p);
            }

            return hr;
        }

        public static int GetActiveObject(string progID, out ComPtr p)
        {
            int hr = 0;
            Guid clsid = Guid.Empty;
            IntPtr pUnk = IntPtr.Zero;
            p = IntPtr.Zero;

            if (Succeeded(hr = GetActiveObject(progID, out pUnk)))
            {
                p = new ComPtr(pUnk);
                Marshal.Release(pUnk);
            }

            return hr;
        }

        //public static int GetActiveObject<T>(string progID, out ComPtr<T> p)
        //{
        //    int hr = 0;
        //    Guid clsid = Guid.Empty;
        //    IntPtr pUnk = IntPtr.Zero;
        //    p = IntPtr.Zero;

        //    if (Succeeded(hr = GetActiveObject(progID, out pUnk)))
        //    {
        //        p = new ComPtr<T>(pUnk);
        //        Marshal.Release(pUnk);
        //    }

        //    return hr;
        //}

        /// <summary>
        /// Returns an array of Guids by QueryInterface()'ing all IIDs known to this system.
        /// </summary>
        public static Dictionary<Guid, string> QueryInterfaces(IntPtr pUnk)
        {
            Dictionary<Guid, string> list = new Dictionary<Guid, string>();

            try
            {
                using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
                {
                    using (RegistryKey interfaceKey = baseKey.OpenSubKey("Interface"))
                    {
                        foreach (string iid in interfaceKey.GetSubKeyNames())
                        {
                            try
                            {
                                Guid guid = Guid.Empty;

                                if (Guid.TryParse(iid, out guid))
                                {
                                    IntPtr ppv = IntPtr.Zero;

                                    if (Marshal.QueryInterface(pUnk, ref guid, out ppv) == 0)
                                    {
                                        using (RegistryKey iidKey = interfaceKey.OpenSubKey(iid))
                                        {
                                            object defaultValue = iidKey.GetValue(null);
                                            list.Add(guid, String.Format("{0}", defaultValue));
                                            Marshal.Release(ppv);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            finally
            {
            }

            return list;
        }
        /// <summary>
        /// Returns an array of Guids by QueryInterface()'ing all IIDs known to this system.
        /// </summary>
        public static Guid[] QueryInterfaces(object o)
        {
            List<Guid> list = new List<Guid>();

            if (Marshal.IsComObject(o))
            {
                IntPtr pUnk = IntPtr.Zero;
                try
                {
                    pUnk = Marshal.GetIUnknownForObject(o);

                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
                    {
                        using (RegistryKey interfaceKey = baseKey.OpenSubKey("Interface"))
                        {
                            foreach (string iid in interfaceKey.GetSubKeyNames())
                            {
                                try
                                {
                                    Guid guid = Guid.Empty;

                                    if (Guid.TryParse(iid, out guid))
                                    {
                                        IntPtr ppv = IntPtr.Zero;

                                        if (Marshal.QueryInterface(pUnk, ref guid, out ppv) == 0)
                                        {
                                            list.Add(guid);
                                            Marshal.Release(ppv);
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                finally
                {
                    if (pUnk.Equals(IntPtr.Zero) == false)
                    {
                        Marshal.Release(pUnk);
                    }
                }
            }

            return list.ToArray();
        }

        public static bool Succeeded(int hr)
        {
            return NativeMethods.Succeeded(hr);
        }

        public static bool Failed(int hr)
        {
            return NativeMethods.Failed(hr);
        }
    }
}
