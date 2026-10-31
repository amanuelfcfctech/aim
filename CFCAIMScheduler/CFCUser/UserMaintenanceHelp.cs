using System;
using System.Windows.Forms;

namespace CFCUser
{
	public partial class UserMaintenanceHelp : Form
	{
		public UserMaintenanceHelp()
		{
			InitializeComponent();
		}

		private void UserMaintenanceHelp_Leave(object sender, EventArgs e)
		{
			Close();
		}
	}
}
