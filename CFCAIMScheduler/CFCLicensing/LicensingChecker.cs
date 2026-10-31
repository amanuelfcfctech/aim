using System;
using System.Windows.Forms;
using CFCLicensing.Entity;
using CFCLicensing.Manager;
using CFCLicensing.Object;
using CFCLicensing.Varible;
//using CFCAIM;

namespace CFCLicensing
{
	public class LicensingChecker
	{
		public void Main()
		{
			Boolean result = false;
			Int64 leaseLenght = 0;
			string message = string.Empty;
			string day = string.Empty;
			TimeSpan daysLeft;
			ConfigInfo config = new ConfigInfo();
			EvaluationNotice evaluationNotice = new EvaluationNotice();
			EvaluationPeriodExpired evaluationPeriodExpired = new EvaluationPeriodExpired();

			Activation.IsError = false;
			config = LicensingManager.GetConfig();
			if (config == null)
			{
				Activation.IsError = true;
				MessageBox.Show("Config is empty. Program will be terminated.", "Error...", MessageBoxButtons.OK);
				Application.Exit();
			}                       
			else if (config.ConfigID == 0)            
            {
				Activation.IsError = true;
				MessageBox.Show("Config was not found. Program will be terminated.", "Error...", MessageBoxButtons.OK);
				Application.Exit();
			}

            else
            {
				Activation.SetActivationValues(ref config);

				Activation.LeaseLenght = config.LeaseLenght;
				Global.AppStartupPathNDatabaseDirectory = String.Format("Current Folder: {0} , Database Folder: {1}", Application.StartupPath, config.DatabaseDirectory);
				Activation.LicenseAgreement = CFCLicenseAgreement.Trail;

				if (Activation.IsMainFormLoaded == false)
				{
					Activation.IsMainFormLoaded = true;

					if (config.InstallDate == DateTime.MinValue)
					{
						Activation.InstallDate = DateTime.Now;
						config.InstallDate = Activation.InstallDate;
						config.TrialInstallCode = Activation.GetInstallCode();
						config.TrialUseCount = 300;
						Activation.TrialUseCount = config.TrialUseCount;
						LicensingManager.SaveConfig(ref config);

						evaluationNotice.ShowDialog();
					}
					else if (config.AuthorizationCode > 0)
					{
						Activation.LicenseAgreement = CFCLicenseAgreement.License;
						if (Activation.GetValidateAuthorizationCode(config.AuthorizationCode, config.LeaseLenght) == false)
						{
							message = string.Format("Corrupted license, it will be reset to demo.  Contact {0} for a new license.", Properties.Settings.Default.LicensedCompanyName);
							MessageBox.Show(message, "Message...", MessageBoxButtons.OK);
							config.AuthorizationCode = 0;
							config.InstallDate = DateTime.MinValue;
							LicensingManager.SaveConfig(ref config);
							Application.Exit();
						}
					}
					else if (config.LeaseCode > 0)
					{
						Activation.LicenseAgreement = CFCLicenseAgreement.Lease;

						if (config.LeaseLenght > 1)
						{
							leaseLenght = config.LeaseLenght / 2;
						}

						daysLeft = config.InstallDate.AddDays(leaseLenght).Subtract(DateTime.Now);
						Global.LicensingDaysLeft = daysLeft;
						//if (daysLeft.Days + 1 < 2)
						//{
						//    day = "day";
						//}
						//else
						//{
						//    day = "days";
						//}

						//lblLeaseLenght.Text = string.Format("{0} {1} remaining on License", daysLeft.Days + 1, day);
						//lblLeaseLenght.Visible = true;
						if (Activation.GetValidateAuthorizationCode(config.LeaseCode, config.LeaseLenght) == false)
						{
							config.InstallDate = DateTime.MinValue;
							config.LeaseLenght = -1;
							config.LeaseCode = -1;
							LicensingManager.SaveConfig(ref config);

							Application.Exit();
						}
						else
						{
							if (daysLeft.Days + 1 > 0)
							{
								if (daysLeft.Days < 11 && Activation.IsDaysLeftDisplayed == false)
								{
									Activation.IsDaysLeftDisplayed = true;
									message = string.Format("The license will expire in {0} {1}.  Contact {2} for a new license.", daysLeft.Days + 1, day, Properties.Settings.Default.LicensedCompanyName);
									MessageBox.Show(message, "Message...", MessageBoxButtons.OK);
								}
							}
							else
							{
								message = string.Format("The license has expired will be reset to demo.  Contact {0} for a new license.", Properties.Settings.Default.LicensedCompanyName);
								MessageBox.Show(message, "Message...", MessageBoxButtons.OK);
								config.CompanyName = string.Empty;
								LicensingManager.SaveConfig(ref config);

								config.InstallDate = DateTime.MinValue;
								config.LeaseLenght = -1;
								config.LeaseCode = -1;
								LicensingManager.SaveConfig(ref config);

								if (result == false)
								{
									MessageBox.Show("Error writing to the database.", "Error...", MessageBoxButtons.OK);
									Application.Exit();
								}
								Application.Exit();
							}
						}
					}
					else
					{
						config.TrialUseCount = Activation.GetTrialUseCount();
						result = LicensingManager.SaveConfigWithSuccess(ref config);
						if (result == false)
						{
							MessageBox.Show("Error writing to the database.", "Error...", MessageBoxButtons.OK);
							Application.Exit();
						}

						if (config.TrialInstallCode != Activation.GetInstallCode())
						{
							config.InstallDate = DateTime.MinValue;
							config.TrialInstallCode = -1;
							config.TrialUseCount = -1;
							LicensingManager.SaveConfig(ref config);
							if (result == false)
							{
								MessageBox.Show("Error writing to the database.", "Error...", MessageBoxButtons.OK);
							}
							else
							{
								message = string.Format("Corrupted demo license, it will be reset to demo.  Contact {0} for a license.", Properties.Settings.Default.LicensedCompanyName);
								MessageBox.Show(message, "Message...", MessageBoxButtons.OK);
							}
							Application.Exit();
						}
						else if (config.TrialUseCount > 0)
						{
							evaluationNotice.ShowDialog();
						}
						else
						{
							Activation.IsTrialExpired = true;
							evaluationPeriodExpired.ShowDialog();
						}
					}
				}            
			}
		}
	}
}

