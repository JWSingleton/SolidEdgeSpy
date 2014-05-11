using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SolidEdge.Spy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main()
        {
            try
            {
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.Run(new MainForm());
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }

            return 0;
            //string exeName = Process.GetCurrentProcess().ProcessName;
            //exeName = exeName.Replace(Path.GetExtension(exeName), ".exe");
            
            //if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            //{
            //    int returnValue = 0;

            //    do
            //    {
            //        AppDomain _interopDomain = AppDomain.CreateDomain("Application Domain");

            //        try
            //        {
            //            returnValue = _interopDomain.ExecuteAssembly(exeName);
            //        }
            //        catch (System.Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        finally
            //        {
            //            AppDomain.Unload(_interopDomain);
            //        }

            //    } while (returnValue != 0);

            //    return returnValue;
            //}
            //else
            //{
            //    System.Windows.Forms.Application.EnableVisualStyles();
            //    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            //    System.Windows.Forms.Application.Run(new Application());

            //    var returnValue = AppDomain.CurrentDomain.GetData("ReturnValue");

            //    if (returnValue != null)
            //    {
            //        return (int)returnValue;
            //    }
            //    else
            //    {
            //        return 0;
            //    }
            //}
        }
    }
}
