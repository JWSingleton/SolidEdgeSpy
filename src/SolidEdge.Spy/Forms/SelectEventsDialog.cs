using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolidEdge.Spy.Forms
{
    public partial class SelectEventsDialog : Form
    {
        private Type[] _eventTypes = new Type[] { };

        public SelectEventsDialog()
        {
            InitializeComponent();
        }

        private void SelectEventsDialog_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        public Type[] EventTypes
        {
            get
            {
                return _eventTypes;
            }
            set
            {
                _eventTypes = value;
            }
        }
    }
}
