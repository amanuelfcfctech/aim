using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
    public partial class AnalysisSourceComparisonReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AnalysisSourceComparisonReport()
        {
            InitializeComponent();
        }

		private void AnalysisSourceComparisonReport_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
		{

            //---------------------------------------            
            //9/10/20 - change to use AIMCoreGlobal
            //txtCompany.Text = CFCAIM.AIMGlobal.CompanyName();
            txtCompany.Text = CFCAIMCore.Global.CompanyName();
        }

    }
}
