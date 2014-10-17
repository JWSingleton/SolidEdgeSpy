using SolidEdge.Spy.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidEdge.Spy.InteropServices;

namespace SolidEdge.Spy.Forms
{
    public partial class CommandBrowser : UserControl
    {
        private List<ListViewItem> _commandItems = new List<ListViewItem>();
        private string _filter;
        private SolidEdgeFramework.Environment _activeEnvironment;

        public CommandBrowser()
        {
            InitializeComponent();
        }

        private void textBoxSearch_TextAccepted(object sender, EventArgs e)
        {
            buttonSearch_Click(sender, e);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            _filter = textBoxSearch.Text;
            LoadItems();
        }

        private void buttonClearSearch_Click(object sender, EventArgs e)
        {
            _filter = null;
            LoadItems();
        }

        private void textBoxCommandID_TextChanged(object sender, EventArgs e)
        {
            int commandId = 0;

            if (int.TryParse(textBoxCommandID.Text, out commandId))
            {
                buttonStart.Enabled = true;
            }
            else
            {
                buttonStart.Enabled = false;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int commandId = 0;

            try
            {
                if (int.TryParse(textBoxCommandID.Text, out commandId))
                {
                    var application = _activeEnvironment.Application;
                    application.StartCommand((SolidEdgeFramework.SolidEdgeCommandConstants)commandId);
                }
            }
            catch
            {
            }
        }

        private void listViewEx_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            textBoxCommandID.Text = String.Empty;

            try
            {
                var item = e.Item;

                if (item != null)
                {
                    var commandId = (int)item.Tag;
                    textBoxCommandID.Text = commandId.ToString();
                }
            }
            catch
            {
            }
        }

        private void LoadItems()
        {
            listViewEx.Items.Clear();
            _commandItems.Clear();

            if (_activeEnvironment == null) return;

            try
            {
                //Solid Edge Constants Type Library
                Guid typeLibGuid = new Guid("{C467A6F5-27ED-11D2-BE30-080036B4D502}");

                var constantsTypeLib = ComTypeManager.Instance.ComTypeLibraries.Where(x => x.Guid.Equals(typeLibGuid)).FirstOrDefault();

                if (constantsTypeLib != null)
                {
                    var commandType = _activeEnvironment.GetCommandType();

                    if (commandType != null)
                    {
                        var enumInfo = constantsTypeLib.Enums.Where(x => x.Name.Equals(commandType.Name)).FirstOrDefault();

                        foreach (var variable in enumInfo.Variables)
                        {
                            var enumName = variable.Name;
                            var enumValue = variable.ConstantValue;
                            var listViewItem = new ListViewItem();
                            listViewItem.Text = enumName;
                            listViewItem.SubItems.Add(String.Format("{0}", (int)enumValue));
                            listViewItem.SubItems.Add(commandType.FullName);
                            listViewItem.Tag = (int)enumValue;
                            _commandItems.Add(listViewItem);

                        }

                        // Sort commands by name.
                        _commandItems.Sort(delegate(ListViewItem a, ListViewItem b)
                        {
                            return a.Text.CompareTo(b.Text);
                        });
                    }
                }
            }
            catch
            {
            }

            if (String.IsNullOrWhiteSpace(_filter))
            {
                listViewEx.Items.AddRange(_commandItems.ToArray());
            }
            else
            {
                int idFilter = 0;

                if (int.TryParse(_filter, out idFilter) == false)
                {
                    var filteredItems = _commandItems
                        .Where(x => x.Text.IndexOf(_filter, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToArray();
                    listViewEx.Items.AddRange(filteredItems);
                }
                else
                {
                    var filteredItems = _commandItems
                        .Where(x => x.Tag != null)
                        .Where(x => x.Tag.ToString().IndexOf(_filter, StringComparison.OrdinalIgnoreCase) >= 0)
                        .ToArray();

                    listViewEx.Items.AddRange(filteredItems);
                }
            }

            listViewEx.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public SolidEdgeFramework.Environment ActiveEnvironment
        {
            get { return _activeEnvironment; }
            set
            {
                _activeEnvironment = value;

                try
                {
                    textBoxSearch.Text = null;
                    textBoxCommandID.Text = String.Empty;
                    LoadItems();
                }
                catch
                {
                }
            }
        }
    }
}
