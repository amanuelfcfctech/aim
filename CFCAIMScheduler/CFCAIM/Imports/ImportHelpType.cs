using System;
using System.Windows.Forms;

namespace CFCAIM.Imports
{
    public partial class ImportHelpType : Form
    {
        public ImportHelpType()
        {
            InitializeComponent();
        }

        private void ImportHelpType_Leave(Object sender, EventArgs e)
        {
            Close();
        }
    }
}
