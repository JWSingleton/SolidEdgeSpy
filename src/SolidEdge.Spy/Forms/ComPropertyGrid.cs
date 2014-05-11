using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidEdge.Spy.InteropServices;
using System.Runtime.InteropServices;

namespace SolidEdge.Spy.Forms
{
    public partial class ComPropertyGrid : UserControl
    {
        public event SelectedGridItemChangedEventHandler SelectedGridItemChanged;
        private GridItem _selectedGridItem;

        public ComPropertyGrid()
        {
            InitializeComponent();
        }

        private void propertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            _selectedGridItem = e.NewSelection;

            if (SelectedGridItemChanged != null)
            {
                PropertyGrid propertyGrid = sender as PropertyGrid;
                SelectedGridItemChanged(sender, e);
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

        public ComPtr SelectedObject
        {
            get
            {
                return (ComPtr)propertyGrid.SelectedObject;
            }
            set
            {
                propertyGrid.SelectedObject = value;
            }
        }

        public GridItem SelectedGridItem { get { return _selectedGridItem; } }
    }
}
