using System;
using System.Windows.Forms;

namespace CFCAIM.Maintenance
{
    public partial class AIMSupplierLocationGroupHelp : Form
    {
        public AIMSupplierLocationGroupHelp()
        {
            InitializeComponent();
        }

        private void AIMSupplierLocationGroupHelp_Leave(object sender, EventArgs e)
        {
            Close();
        }
    }
}
