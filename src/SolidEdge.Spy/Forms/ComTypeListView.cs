using SolidEdge.Spy.InteropServices;
using SolidEdge.Spy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolidEdge.Spy.Forms
{
    public class ComTypeListView : ListViewEx
    {
        private ComTypeInfo _comTypeInfo;

        public const int MethodImageIndex = 0;
        public const int PropertyImageIndex = 1;
        public const int EventImageIndex = 2;
        public const int ConstantImageIndex = 3;

        public ComTypeListView()
            : base()
        {
            SetupImageList();
        }

        [Browsable(false)]
        public ComTypeInfo SelectedComTypeInfo
        {
            get
            {
                return _comTypeInfo;
            }

            set
            {
                _comTypeInfo = value;
                UpdateItems();
            }
        }

        private void UpdateItems()
        {
            List<ListViewItem> list = new List<ListViewItem>();

            if (_comTypeInfo != null)
            {
                foreach (ComFunctionInfo comFunctionInfo in _comTypeInfo.GetMethods(true))
                {
                    ListViewItem item = new ListViewItem(comFunctionInfo.Name);
                    item.SubItems.Add(comFunctionInfo.Description);
                    item.ImageIndex = MethodImageIndex;
                    item.Tag = comFunctionInfo;

                    if (comFunctionInfo.IsRestricted)
                    {
                        item.ForeColor = Color.DarkGray;
                    }

                    list.Add(item);
                }

                foreach (ComPropertyInfo comPropertyInfo in _comTypeInfo.GetProperties(true))
                {
                    ListViewItem item = new ListViewItem(comPropertyInfo.Name);
                    item.SubItems.Add(comPropertyInfo.Description);
                    item.ImageIndex = PropertyImageIndex;
                    item.Tag = comPropertyInfo;

                    if (comPropertyInfo.GetFunction != null)
                    {
                        if (comPropertyInfo.GetFunction.IsRestricted)
                        {
                            item.ForeColor = Color.DarkGray;
                        }
                    }

                    list.Add(item);
                }

                foreach (ComVariableInfo comVariableInfo in _comTypeInfo.Variables)
                {
                    ListViewItem item = new ListViewItem(comVariableInfo.Name);
                    item.SubItems.Add(comVariableInfo.Description);
                    item.ImageIndex = ConstantImageIndex;
                    item.Tag = comVariableInfo;

                    list.Add(item);
                }

                if (_comTypeInfo is ComCoClassInfo)
                {
                    ComCoClassInfo comCoClassInfo = (ComCoClassInfo)_comTypeInfo;
                    foreach (ComFunctionInfo comFunctionInfo in comCoClassInfo.Events)
                    {
                        ListViewItem item = new ListViewItem(comFunctionInfo.Name);
                        item.SubItems.Add(comFunctionInfo.Description);
                        item.ImageIndex = EventImageIndex;
                        item.Tag = comFunctionInfo;
                        list.Add(item);
                    }
                }
                else
                {
                }
            }

            BeginUpdate();
            Items.Clear();
            Items.AddRange(list.ToArray());
            AutoResizeColumns();
            EndUpdate();
        }

        private void SetupImageList()
        {
            SmallImageList = new ImageList();
            SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            SmallImageList.ImageSize = new Size(16, 16);
            SmallImageList.Images.Add(Resources.Method_16x16);
            SmallImageList.Images.Add(Resources.Property_16x16);
            SmallImageList.Images.Add(Resources.Event_16x16);
            SmallImageList.Images.Add(Resources.Constant_16x16);
        }
    }
}
