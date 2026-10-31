using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
	public partial class SamplesByOwnerLocation : DevExpress.XtraReports.UI.XtraReport
	{
		public SamplesByOwnerLocation()
		{
			InitializeComponent();
		}

		private void SamplesByOwnerLocation_BeforePrint(object sender, CancelEventArgs e)
		{
			txtCompany.Text = CFCAIMCore.Global.CompanyName();

			
		}
	}
}
