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

        public static Type GetCommandType(this SolidEdgeFramework.Environment environment)
        {
            var guid = environment.GetGuid();

            if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Application))
            {
                return typeof(SolidEdgeConstants.SolidEdgeCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Assembly))
            {
                return typeof(SolidEdgeConstants.AssemblyCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.DMAssembly))
            {
                return typeof(SolidEdgeConstants.AssemblyCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.CuttingPlaneLine))
            {
                return typeof(SolidEdgeConstants.CuttingPlaneLineCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Draft))
            {
                return typeof(SolidEdgeConstants.DetailCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.DrawingViewEdit))
            {
                return typeof(SolidEdgeConstants.DrawingViewEditCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Explode))
            {
                return typeof(SolidEdgeConstants.ExplodeCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Layout))
            {
                return typeof(SolidEdgeConstants.LayoutCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Sketch))
            {
                return typeof(SolidEdgeConstants.LayoutInPartCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Motion))
            {
                return typeof(SolidEdgeConstants.MotionCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Part))
            {
                return typeof(SolidEdgeConstants.PartCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.DMPart))
            {
                return typeof(SolidEdgeConstants.PartCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Profile))
            {
                return typeof(SolidEdgeConstants.ProfileCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.ProfileHole))
            {
                return typeof(SolidEdgeConstants.ProfileHoleCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.ProfilePattern))
            {
                return typeof(SolidEdgeConstants.ProfilePatternCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.ProfileRevolved))
            {
                return typeof(SolidEdgeConstants.ProfileRevolvedCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.SheetMetal))
            {
                return typeof(SolidEdgeConstants.SheetMetalCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.SheetMetal))
            {
                return typeof(SolidEdgeConstants.SheetMetalCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Simplify))
            {
                return typeof(SolidEdgeConstants.SimplifyCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Studio))
            {
                return typeof(SolidEdgeConstants.StudioCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.XpresRoute))
            {
                return typeof(SolidEdgeConstants.TubingCommandConstants);
            }
            else if (guid.Equals(SolidEdgeSDK.EnvironmentCategories.Weldment))
            {
                return typeof(SolidEdgeConstants.WeldmentCommandConstants);
            }

            return null;
        }

        #endregion
    }
}
