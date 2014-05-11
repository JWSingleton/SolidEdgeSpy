using SolidEdge.Spy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SolidEdge.Spy
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = Resources.CodePlexUrl;
            linkCodeplex.Links.Add(link);

            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();

            AssemblyCompanyAttribute asemblyCompanyAttribute = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false).OfType<AssemblyCompanyAttribute>().FirstOrDefault();
            AssemblyDescriptionAttribute assemblyDescriptionAttribute = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault();

            List<ListViewItem> items = new List<ListViewItem>();

            if (assemblyDescriptionAttribute != null)
            {
                items.Add(new ListViewItem(new string[] { "Description", assemblyDescriptionAttribute.Description }));
            }

            if (asemblyCompanyAttribute != null)
            {
                items.Add(new ListViewItem(new string[] { "Author", asemblyCompanyAttribute.Company }));
            }

            items.Add(new ListViewItem(new string[] { "Version", assemblyName.Version.ToString() }));
            items.Add(new ListViewItem(new string[] { "Website", Resources.CodePlexUrl }));
            items.Add(new ListViewItem(new string[] { ".NET Runtime Version", assembly.ImageRuntimeVersion }));
            items.Add(new ListViewItem(new string[] { "Solid Edge Version", GetSolidEdgeVersion() }));

            listView.Items.AddRange(items.ToArray());
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkCodeplex_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(e.Link.LinkData as string);
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        private string GetSolidEdgeVersion()
        {
            object installData = null;

            try
            {
                Type type = Type.GetTypeFromProgID("SolidEdge.InstallData");

                if (type != null)
                {
                    installData = Activator.CreateInstance(type);

                    object version = installData.GetType().InvokeMember("GetVersion", BindingFlags.InvokeMethod, null, installData, null);

                    if (version != null)
                    {
                        return version.ToString();
                    }
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
            finally
            {
                if (installData != null)
                {
                    Marshal.FinalReleaseComObject(installData);
                }
            }

            return String.Empty;
        }
    }
}
