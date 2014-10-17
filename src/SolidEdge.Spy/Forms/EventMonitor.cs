//using SolidEdge.Spy.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;
using SolidEdge.Spy.InteropServices;
using System.Runtime.InteropServices;
using SolidEdge.Spy.Properties;

namespace SolidEdge.Spy.Forms
{
    public partial class EventMonitor : UserControl
    {
        public const int EventImageIndex = 0;

        public EventMonitor()
        {
            InitializeComponent();
        }

        private void EventBrowser_Load(object sender, EventArgs e)
        {
            SetupImageList();
        }

        private void buttonErase_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
        }

        public void LogEvent(EventMonitorItem item)
        {
            try
            {
                LogEvents(new EventMonitorItem[] { item });
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        public void LogEvents(EventMonitorItem[] items)
        {
            try
            {
                if (items.Length > 0)
                {
                    listView.BeginUpdate();

                    foreach (EventMonitorItem item in items)
                    {
                        item.ImageIndex = EventImageIndex;
                    }

                    listView.Items.AddRange(items);
                    listView.AutoResizeColumns();
                    listView.FocusedItem = null;
                    listView.SelectedItems.Clear();

                    items[items.Length -1].EnsureVisible();
                    items[items.Length -1].Selected = true;

                    listView.EndUpdate();
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        private void SetupImageList()
        {
            listView.SmallImageList = new ImageList();
            listView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            listView.SmallImageList.ImageSize = new Size(16, 16);
            listView.SmallImageList.Images.Add(Resources.Event_16x16);
        }

        private void buttonSelectEvents_ButtonClick(object sender, EventArgs e)
        {
            var dialog = new SelectEventsDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
            }
        }
    }

    public class EventMonitorItem : ListViewItem
    {
        public EventMonitorItem(string eventString, string environmentName, string environmentCaption, string environmentCATID)
            : base(new string[]  { eventString, environmentName, environmentCaption, environmentCATID })
        {
        }
    }
}
