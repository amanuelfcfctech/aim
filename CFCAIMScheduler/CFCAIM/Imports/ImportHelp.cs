using System;
using System.Windows.Forms;

namespace CFCAIM.Imports
{
    public partial class ImportHelp : Form
	{
		public ImportHelp()
		{
			InitializeComponent();
		}			

		private void ImportHelp_Leave(object sender, EventArgs e)
		{
			Close();
		}
	}
}
