using SolidEdge.Spy.InteropServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SolidEdge.Spy.Extensions
{
    public static class SolidEdgeExtensions
    {
        #region SolidEdgeFramework.Application Extensions

        public static SolidEdgeFramework.Environment GetActiveEnvironment(this SolidEdgeFramework.Application application)
        {
            SolidEdgeFramework.Environments environments = application.Environments;
            return environments.Item(application.ActiveEnvironment);
        }

        //public static object SafeGetGetGlobalParameter(this SolidEdgeFramework.Application application, SolidEdgeFramework.ApplicationGlobalConstants globalConstant)
        //{
        //    object returnVal = null;
        //    object val = null;

        //    using (ComPtr p = ComPtr.FromRCW(application))
        //    {
        //        if (p.IsInvalid == false)
        //        {
        //            object[] args = { globalConstant, new VariantWrapper(null) };
        //            if (MarshalEx.Succeeded(p.TryInvokeMethod("GetGlobalParameter", args, out returnVal)))
        //            {
        //                val = args[1];
        //            }

        //        }
        //    }

        //    return val;
        //}

        //public static bool GetGetGlobalParameterAsBoolean(this SolidEdgeFramework.Application application, SolidEdgeFramework.ApplicationGlobalConstants globalConstant)
        //{
        //    object val = null;
            
        //    try
        //    {
        //        application.GetGlobalParameter(globalConstant, ref val);
        //    }
        //    catch
        //    {
        //        GlobalExceptionHandler.HandleException();
        //    }

        //    return val == null ? false : (bool)val;
        //}

        //public static Color GetGlobalParameterAsColor(this SolidEdgeFramework.Application application, SolidEdgeFramework.ApplicationGlobalConstants globalConstant)
        //{
        //    object val = null;

        //    try
        //    {
        //        application.GetGlobalParameter(globalConstant, ref val);
        //        byte[] rgb = BitConverter.GetBytes((int)val);
        //        return Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);
        //    }
        //    catch
        //    {
        //        GlobalExceptionHandler.HandleException();
        //    }

        //    return Color.Transparent;
        //}

        //public static int GetGlobalParameterAsInteger(this SolidEdgeFramework.Application application, SolidEdgeFramework.ApplicationGlobalConstants globalConstant)
        //{
        //    object val = null;
            
        //    try
        //    {
        //        application.GetGlobalParameter(globalConstant, ref val);
        //    }
        //    catch
        //    {
        //        GlobalExceptionHandler.HandleException();
        //    }

        //    return val == null ? -1 : (int)val;
        //}

        //public static string GetGetGlobalParameterAsString(this SolidEdgeFramework.Application application, SolidEdgeFramework.ApplicationGlobalConstants globalConstant)
        //{
        //    object val = null;
            
        //    try
        //    {
        //        application.GetGlobalParameter(globalConstant, ref val);
        //    }
        //    catch
        //    {
        //        GlobalExceptionHandler.HandleException();
        //    }

        //    return (string)val;
        //}

        #endregion

        #region SolidEdgeFramework.SolidEdgeDocument Extensions

        public static bool IsTemporary(this SolidEdgeFramework.SolidEdgeDocument document)
        {
            try
            {
                DirectoryInfo fileDirectory = new DirectoryInfo(Path.GetDirectoryName(Path.GetFullPath(document.FullName)));
                DirectoryInfo tempDirectory = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.IO.Path.GetTempPath()));
                return fileDirectory.FullName.Equals(tempDirectory.FullName, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }         

            return false;
        }

        #endregion

        #region SolidEdgeFramework.Environment Extensions

        public static Guid GetGuid(this SolidEdgeFramework.Environment environment)
        {
            return new Guid(environment.CATID);
        }

        public static void GetInfo(this SolidEdgeFramework.Environment environment, out string name, out string caption, out string catid)
        {
            name = environment.Name;
            caption = environment.Caption;
            catid = environment.CATID;
        }

        #endregion
    }
}
