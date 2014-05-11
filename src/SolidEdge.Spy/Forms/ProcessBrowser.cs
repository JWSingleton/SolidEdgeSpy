using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SolidEdge.Spy.Forms
{
    public partial class ProcessBrowser : UserControl
    {
        private int _processId = 0;

        public ProcessBrowser()
        {
            InitializeComponent();
        }

        private void ProcessBrowser_Load(object sender, EventArgs e)
        {
            RefreshProcessInformation();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshProcessInformation();
        }

        public void RefreshProcessInformation()
        {
            Cursor.Current = Cursors.WaitCursor;

            if (_processId == 0) return;

            try
            {
                processModuleListView.Items.Clear();
                Process process = Process.GetProcessById(_processId);
                processModuleListView.SetItems(process.Modules);
            }
            catch
            {
            }

            Cursor.Current = Cursors.Default;
        }

        public int ProcessId
        {
            get { return _processId; }
            set
            {
                _processId = value;

                try
                {
                    RefreshProcessInformation();
                }
                catch
                {
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (processModuleListView.SelectedItems.Count == 1)
                {
                    ListViewItem listViewItem = processModuleListView.SelectedItems[0];
                    ProcessModule processModule = listViewItem.Tag as ProcessModule;
                    if (processModule != null)
                    {
                        NativeMethods.SHELLEXECUTEINFO info = new NativeMethods.SHELLEXECUTEINFO();
                        info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
                        info.lpVerb = "properties";
                        info.lpFile = processModule.FileName;
                        info.nShow = NativeMethods.SW_SHOW;
                        info.fMask = NativeMethods.SEE_MASK_INVOKEIDLIST;
                        NativeMethods.ShellExecuteEx(ref info);  
                    }
                }
            }
            catch
            {
            }
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (processModuleListView.SelectedItems.Count != 1)
            {
                e.Cancel = true;
            }
        }
    }
}
