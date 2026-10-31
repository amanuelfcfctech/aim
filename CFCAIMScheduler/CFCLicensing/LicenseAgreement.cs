using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using CFCLicensing.Object;
using Microsoft.Reporting.WinForms;
using System.IO;
using CFCLicensing.Varible;

namespace CFCLicensing
{
	public partial class LicenseAgreement : Form
	{
		public LicenseAgreement()
		{
			InitializeComponent();
		}

		private void cmdAccept_Click(object sender, EventArgs e)
		{
            Activation.IsLicenseAccepted = true;
            Close();
		}

		private void LicenseAgreement_Load(object sender, EventArgs e)
		{
			List<ReportParameter> reportParameters = new List<ReportParameter>();
			string licensed = string.Empty;

			switch (Activation.LicenseAgreement)
			{
				case CFCLicenseAgreement.Trail:
					licensed = Properties.Settings.Default.LicensedTrailAgreement;
					break;
				case CFCLicenseAgreement.License:
					licensed = Properties.Settings.Default.LicensedAgreement;
					break;
				case CFCLicenseAgreement.Lease:
					licensed = Properties.Settings.Default.LicensedLeaseAgreement;
					break;
				default:
					Close();
					break;
			}

            //2/13/18 - make sure path and report exists
            String path = Directory.GetCurrentDirectory();
            path += "\\Reports";
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                MessageBox.Show("Reports folder (and LicenseAgreement.rdlc) is missing from " + Directory.GetCurrentDirectory().ToString() + ".", "Warning...", MessageBoxButtons.OK);
                return;
            }                     
            FileInfo[] report = di.GetFiles("LicenseAgreement.rdlc");
            if (report.Length == 0)
            {
                MessageBox.Show("LicenseAgreement.rdlc is missing from path.", "Warning...", MessageBoxButtons.OK);
                return;
            }

            reportViewer1.LocalReport.ReportPath = @"Reports\LicenseAgreement.rdlc";
            reportParameters.Add(new ReportParameter("LicenseAgreement", licensed));
			//reportParameters new ReportParameter("LicenseAgreement", licensed);
			reportViewer1.LocalReport.SetParameters(reportParameters);
			reportViewer1.RefreshReport();
		}

        private void cmdDoNotAccept_Click(object sender, EventArgs e)
        {
			Activation.IsLicenseAccepted = false;            
            Close();
        }

		private void cmdPrint_Click(object sender, EventArgs e)
		{
			reportViewer1.PrintDialog();
		}
	}
}
