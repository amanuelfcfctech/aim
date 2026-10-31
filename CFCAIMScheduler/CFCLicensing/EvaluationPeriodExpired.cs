using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CFCLicensing.Object;

namespace CFCLicensing
{
	public partial class EvaluationPeriodExpired : Form
	{
		public EvaluationPeriodExpired()
		{
			InitializeComponent();
		}

		private void EvaluationPeriodExpired_Load(object sender, EventArgs e)
		{
			lblInstructions.Text = Properties.Settings.Default.Instructions;
			lblHeading.Text = "Please pick the license you want.";
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			if (Activation.IsTrialExpired == true)
			{
				Application.Exit();
			}
			else
			{
				Close();
			}
		}

		private void btnOrderInfo_Click(object sender, EventArgs e)
		{
			OrderInformation orderInformation = new OrderInformation();

			this.Hide();
			orderInformation.ShowDialog();
			this.Show();
		}

		private void cmdAuthCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnExtendEvaluationPeriod_Click(object sender, EventArgs e)
		{
			LicenseAgreement licenseAgreement = new LicenseAgreement();
			AuthorizationCode authorizationCode = new AuthorizationCode();

			Activation.LicenseAgreement = CFCLicenseAgreement.Trail;
			this.Hide();
			Activation.IsLicenseAccepted = false;
			licenseAgreement.ShowDialog();

			if (Activation.IsLicenseAccepted == true)
			{
				authorizationCode.ShowDialog();
			}
			this.Show();

			if (Activation.IsTrialExtended == true)
			{
				Close();
			}
		}

		private void btnConvertToLicensedSystem_Click(object sender, EventArgs e)
		{
			LicenseAgreement licenseAgreement = new LicenseAgreement();
			AuthorizationCode authorizationCode = new AuthorizationCode();

			Activation.LicenseAgreement = CFCLicenseAgreement.License;
			this.Hide();
			Activation.IsLicenseAccepted = false;
			licenseAgreement.ShowDialog();

			if (Activation.IsLicenseAccepted == true)
			{
				authorizationCode.ShowDialog();
			}
			this.Show();

			if (Activation.IsProductLicensed == true)
			{
				Close();
			}
		}

		private void btnConvertToTemporaryLicensed_Click(object sender, EventArgs e)
		{
			LicenseAgreement licenseAgreement = new LicenseAgreement();
			AuthorizationCode authorizationCode = new AuthorizationCode();

			Activation.LicenseAgreement = CFCLicenseAgreement.Lease;
			this.Hide();
			Activation.IsLicenseAccepted = false;
			licenseAgreement.ShowDialog();

			if (Activation.IsLicenseAccepted == true)
			{
				authorizationCode.ShowDialog();
			}
			this.Show();

			if (Activation.IsProductLicensed == true)
			{
				Close();
			}
		}
	}
}
