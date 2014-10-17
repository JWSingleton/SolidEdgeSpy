using SolidEdge.Spy.Extensions;
using SolidEdge.Spy.InteropServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;

namespace SolidEdge.Spy.Forms
{
    public partial class ObjectBrowser : UserControl
    {
        public ObjectBrowser()
        {
            InitializeComponent();
        }

        private void ComBrowser_Load(object sender, EventArgs e)
        {
            buttonNullObjects.Checked = comTreeView.ShowNullObjects;
            buttonEmptyCollection.Checked = comTreeView.ShowEmptyCollections;
            buttonProperties.Checked = comTreeView.ShowProperties;
        }

        private void UpdateToolStrip(ComTreeNode node)
        {
            try
            {
                List<ToolStripItem> baseToolStripItems = new List<ToolStripItem>();
                List<ToolStripItem> newToolStripItems = new List<ToolStripItem>();

                int index = toolStrip.Items.IndexOf(separatorNavigation);

                for (int i = 0; i <= index; i++)
                {
                    baseToolStripItems.Add(toolStrip.Items[i]);
                }

                while (node != null)
                {
                    ToolStripButton button = new ToolStripButton(node.Caption);
                    button.ToolTipText = String.Format("{0} ({1})", node.Caption, node.TypeFullName);
                    button.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;

                    int imageIndex = node.ImageIndex == -1 ? 0 : node.ImageIndex;

                    button.Image = comTreeView.ImageList.Images[imageIndex];
                    button.Tag = node;
                    button.Click += toolStripNavigationButton_Click;
                    newToolStripItems.Insert(0, button);

                    node = node.Parent as ComTreeNode;
                }

                baseToolStripItems.AddRange(newToolStripItems);
                toolStrip.Items.Clear();
                toolStrip.Items.AddRange(baseToolStripItems.ToArray());
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        void toolStripNavigationButton_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton button = sender as ToolStripButton;

                if (button != null)
                {
                    ComTreeNode node = button.Tag as ComTreeNode;
                    if (node != null)
                    {
                        node.EnsureVisible();
                    }
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        private void comTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            comPropertyGrid.SelectedObject = null;

            ComTreeNode comTreeNode = e.Node as ComTreeNode;

            if (comTreeNode != null)
            {
                UpdateToolStrip(comTreeNode);
                UpdateRichTextBox(comTreeNode);

                try
                {
                    if (comTreeNode is ComPtrTreeNode)
                    {
                        comPropertyGrid.SelectedObject = ((ComPtrTreeNode)comTreeNode).ComPtr;
                    }
                }
                catch
                {
                    GlobalExceptionHandler.HandleException();
                }
            }
        }

        private void comPropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            try
            {
                if (comTreeView.Focused == true) return;

                GridItem gridItem = e.NewSelection;
                if (gridItem != null)
                {
                    ComPtrPropertyDescriptor descriptor = gridItem.PropertyDescriptor as ComPtrPropertyDescriptor;
                    if (descriptor != null)
                    {
                        typeInfoRichTextBox.DescribeComPropertyInfo(descriptor.ComPropertyInfo);
                    }
                }
            }
            catch
            {
            }
        }

        private void UpdateRichTextBox(ComTreeNode node)
        {
            try
            {
                typeInfoRichTextBox.Clear();

                if (node == null) return;

                ComTypeInfo comTypeInfo = null;
                ComPropertyInfo comPropertyInfo = null;
                ComFunctionInfo comFunctionInfo = null;
                
                if (node is ComPtrItemTreeNode)
                {
                    comFunctionInfo = ((ComPtrItemTreeNode)node).ComFunctionInfo;
                }
                else if (node is ComMethodTreeNode)
                {
                    comFunctionInfo = ((ComMethodTreeNode)node).ComFunctionInfo;
                }
                else if (node is ComPropertyTreeNode)
                {
                    comPropertyInfo = ((ComPropertyTreeNode)node).ComPropertyInfo;
                }
                else if (node is ComPtrTreeNode)
                {
                    ComPtrTreeNode comObjectPropertyTreeNode = (ComPtrTreeNode)node;

                    if (comObjectPropertyTreeNode.ComPropertyInfo != null)
                    {
                        comPropertyInfo = comObjectPropertyTreeNode.ComPropertyInfo;
                    }
                    else
                    {
                        comTypeInfo = comObjectPropertyTreeNode.ComPtr.TryGetComTypeInfo();
                    }
                }

                if (comTypeInfo != null)
                {
                    typeInfoRichTextBox.DescribeComTypeInfo(comTypeInfo);
                }
                else if (comPropertyInfo != null)
                {
                    typeInfoRichTextBox.DescribeComPropertyInfo(comPropertyInfo);
                }
                else if (comFunctionInfo != null)
                {
                    typeInfoRichTextBox.DescribeComFunctionInfo(comFunctionInfo);
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        private void buttonNullObjects_Click(object sender, EventArgs e)
        {
            buttonNullObjects.Checked = !buttonNullObjects.Checked;
            comTreeView.ShowNullObjects = buttonNullObjects.Checked;
        }

        private void buttonEmptyCollection_Click(object sender, EventArgs e)
        {
            buttonEmptyCollection.Checked = !buttonEmptyCollection.Checked;
            comTreeView.ShowEmptyCollections = buttonEmptyCollection.Checked;
        }

        private void buttonProperties_Click(object sender, EventArgs e)
        {
            buttonProperties.Checked = !buttonProperties.Checked;
            comTreeView.ShowProperties = buttonProperties.Checked;
        }

        private void buttonMethods_Click(object sender, EventArgs e)
        {
            buttonMethods.Checked = !buttonMethods.Checked;
            comTreeView.ShowMethods = buttonMethods.Checked;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Connect();

            Cursor.Current = Cursors.Default;
        }

        public void Connect()
        {
            Disconnect();

            ComPtr pApplication = IntPtr.Zero;

            try
            {
                if (MarshalEx.Succeeded(MarshalEx.GetActiveObject("SolidEdge.Application", out pApplication)))
                {
                    comTreeView.AddRootNode(pApplication, "Application");
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
            finally
            {
            }
        }

        public void Disconnect()
        {
            try
            {
                comPropertyGrid.SelectedObject = null;

                UpdateToolStrip(null);

                comTreeView.CleanupAndRemoveNodes(comTreeView.Nodes);
                typeInfoRichTextBox.Clear();
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        private void typeInfoRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                string[] linkInfo = e.LinkText.Split(new char[] { '#' });

                switch (linkInfo.Length)
                {
                    case 1:
                        ComTypeManager.Instance.LookupAndSelect(linkInfo[0]);
                        break;
                    case 2:
                        ComTypeManager.Instance.LookupAndSelect(linkInfo[1]);
                        break;
                }
            }
            catch
            {
                GlobalExceptionHandler.HandleException();
            }
        }

        private void comTreeView_Enter(object sender, EventArgs e)
        {
            comTreeView_AfterSelect(comTreeView, new TreeViewEventArgs(comTreeView.SelectedNode, TreeViewAction.Unknown));
        }

        private void comPropertyGrid_Enter(object sender, EventArgs e)
        {
            comPropertyGrid_SelectedGridItemChanged(comPropertyGrid, new SelectedGridItemChangedEventArgs(null, comPropertyGrid.SelectedGridItem));
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStrip.Items.Clear();

            var selectedNode = comTreeView.SelectedNode;

            if (selectedNode != null)
            {
                if (selectedNode is ComTreeNode)
                {
                    var comTreeNode = selectedNode as ComTreeNode;

                    if (comTreeNode.TypeFullName.Equals(typeof(SolidEdgeFramework.Application).FullName))
                    {
                        contextMenuStrip.Items.Add("StartCommand");
                    }
                }
                else if (selectedNode is ComMethodTreeNode)
                {
                }
            }

            if (contextMenuStrip.Items.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void contextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
    }
}
