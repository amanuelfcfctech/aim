using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CFCLicensing.Object;
using CFCLicensing.Manager;
using CFCLicensing.Entity;

namespace CFCLicensing
{
	public partial class AuthorizationCode : Form
	{
		public AuthorizationCode()
		{
			InitializeComponent();
		}

		private void AuthorizationCode_Load(object sender, EventArgs e)
		{
            lblLicensedInformation.Text = Properties.Settings.Default.LicensedInformation;
            lblLicensedVersion.Text = Properties.Settings.Default.LicensedVersion;
			Activation.IsProductLicensed = false;
			if (Properties.Settings.Default.SoftwareCompanyName == Activation.LicenseCompanyName)
			{
				txtCompanyName.Text = string.Empty;
			}
			else
			{
				txtCompanyName.Text = Activation.LicenseCompanyName;
			}

			switch (Activation.LicenseAgreement)
			{
				case CFCLicenseAgreement.Trail:
					Activation.InstallDate = DateTime.Now;
					txtInstallCode.Text = string.Format("Install Code is TR{0}", Activation.GetInstallCode());
                    lblLicensedInformation.Visible = true;
                    lblLicensedVersion.Visible = true;
					txtInstallCode.Visible = true;
					lblActivationCode.Visible = true;
					txtActivationCode.Visible = true;
					btnSubmit.Visible = true;
                    lblMessage.Text = "Extend Trail License";
					break;
				case CFCLicenseAgreement.License:
					lblCompanyName.Visible = true;
					txtCompanyName.Visible = true;
					btnOk.Visible = true;
					MessageBox.Show("Please enter the Company Name that the software will be licensed to.", "Information...", MessageBoxButtons.OK);
					lblMessage.Text = "Full License";
					break;
				case CFCLicenseAgreement.Lease:
					lblCompanyName.Visible = true;
					txtCompanyName.Visible = true;
					btnOk.Visible = true;
					MessageBox.Show("Please enter the Company Name that the software will be temporarily licensed to.", "Information...", MessageBoxButtons.OK);
					lblMessage.Text = "Temporary License";
					break;
				default:
					Close();
					break;
			}
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			decimal authorizationCode = 0;
            Boolean isSuccessFul = false;
			Int64 leaseLenght = 0;
			ConfigInfo config = new ConfigInfo();

			config = LicensingManager.GetConfig();
			if (txtActivationCode.Text.Trim().Length == 0)
			{
				MessageBox.Show("Authorization Code is only numbers. Please re-enter the Authorization Code", "Warning...", MessageBoxButtons.OK);
			}
			else
			{
				decimal.TryParse(txtActivationCode.Text, out authorizationCode);

				if (txtActivationCode.Text.Length > 3)
				{
					Int64.TryParse(txtActivationCode.Text.Substring(txtActivationCode.Text.Length - 3, 3), out leaseLenght);
				}

				if (authorizationCode == 0)
				{
					MessageBox.Show("Authorization Code is only numbers, Please reentry the Authorization Code", "Warning...", MessageBoxButtons.OK);
				}
				else
				{
					if (Activation.GetValidateAuthorizationCode(authorizationCode, leaseLenght) == false)
					{
						MessageBox.Show("Authorization Code is invalid, Please enter a valid Authorization Code", "Warning...", MessageBoxButtons.OK);
						txtActivationCode.Text = String.Empty;
					}
					else
					{
						switch (Activation.LicenseAgreement)
						{
							case CFCLicenseAgreement.Trail:
								Activation.IsTrialExtended = true;
								Activation.TrialUseCount = 300;
								config.InstallDate = Activation.InstallDate;
								config.TrialUseCount = Activation.TrialUseCount;
								config.TrialInstallCode = Activation.GetInstallCode();
                                isSuccessFul = LicensingManager.SaveConfigWithSuccess(ref config);

								if (isSuccessFul == true )
								{
								    MessageBox.Show("You are now trial.", "Message...", MessageBoxButtons.OK);
								    Close();
								}
								
								
								break;
							case CFCLicenseAgreement.License:
								Activation.IsProductLicensed = true;
								config.CompanyName = Activation.LicenseCompanyName;
								config.InstallDate = Activation.InstallDate;
								config.AuthorizationCode = authorizationCode;
                                isSuccessFul = LicensingManager.SaveConfigWithSuccess(ref config);

								if (isSuccessFul == true )
								{
								    MessageBox.Show("You are now licensed.", "Message...", MessageBoxButtons.OK);
								    Close();
								}
									
								
								break;
							case CFCLicenseAgreement.Lease:
								Activation.IsProductLicensed = true;
								config.CompanyName = Activation.LicenseCompanyName;
								config.InstallDate = Activation.InstallDate;
								config.LeaseCode = authorizationCode;
								config.LeaseLenght = leaseLenght;
                                isSuccessFul = LicensingManager.SaveConfigWithSuccess(ref config);

								if (isSuccessFul == true )
								{
								    MessageBox.Show("You are now lease.", "Message...", MessageBoxButtons.OK);
								    Close();
								}
								break;
							default:
								break;
						}
					}
				}
			}
		}

		private void txtCompanyName_Leave(object sender, EventArgs e)
		{
			CheckCompanyName();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			CheckCompanyName();
		}

		private void CheckCompanyName()
		{
			if (txtCompanyName.Text.Trim().Length == 0)
			{
				MessageBox.Show("Please enter the Company Name that the software will be license to.", "Information...", MessageBoxButtons.OK);
			}
			else
			{
				Activation.LicenseCompanyName = txtCompanyName.Text;
                switch (Activation.LicenseAgreement)
                {
                    case CFCLicenseAgreement.Trail:
                        txtInstallCode.Text = string.Format("Install Code is TR{0}", Activation.GetInstallCode());
                        break;
                    case CFCLicenseAgreement.License:
                        txtInstallCode.Text = string.Format("Install Code is LI{0}", Activation.GetInstallCode());
                        break;
                    case CFCLicenseAgreement.Lease:
                        txtInstallCode.Text = string.Format("Install Code is LE{0}", Activation.GetInstallCode());
                        break;
                    default:
                        break;
                }
				txtInstallCode.Visible = true;
				lblActivationCode.Visible = true;
				txtActivationCode.Visible = true;
				btnSubmit.Visible = true;
				lblCompanyName.Visible = false;
				txtCompanyName.Visible = false;
				btnOk.Visible = false;
			}
		}
	}
}
