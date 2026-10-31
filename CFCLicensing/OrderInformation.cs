using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CFCLicensing
{
	public partial class OrderInformation : Form
	{
		public OrderInformation()
		{
			InitializeComponent();
		}

		private void cmdOrderOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OrderInformation_Load(object sender, EventArgs e)
		{
			lblLicensedInformation.Text = Properties.Settings.Default.LicensedInformation;
			lblLicensedVersion.Text = Properties.Settings.Default.LicensedVersion;
		}
	}
}
