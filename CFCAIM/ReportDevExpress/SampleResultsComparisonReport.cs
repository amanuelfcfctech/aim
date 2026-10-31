using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
	public partial class SampleResultsComparisonReport : DevExpress.XtraReports.UI.XtraReport
	{
		public SampleResultsComparisonReport()
		{
			InitializeComponent();
		}

		private void SampleResultsComparisonReport_BeforePrint(object sender, CancelEventArgs e)
		{

			if (AIMGlobal.useResultAnalysisDate == true)
			{
				xrLabelSampleDate.Text = "Analysis Date";				
			}
			else
			{
				xrLabelSampleDate.Text = "Sample Date";				
			}

			txtCompany.Text = CFCAIMCore.Global.CompanyName();
		}
	}
}
