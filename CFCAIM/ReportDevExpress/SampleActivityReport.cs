using System;

namespace CFCAIM.ReportDevExpress
{
    public partial class SampleActivityReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SampleActivityReport()
        {
            InitializeComponent();
        }

        private void SampleActivityReport_BeforePrint(Object sender, System.ComponentModel.CancelEventArgs e)
        {            
            txtCompany.Text = CFCAIMCore.Global.CompanyName();
        }
    }
}
