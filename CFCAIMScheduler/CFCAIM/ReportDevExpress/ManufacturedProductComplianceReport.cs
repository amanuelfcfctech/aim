using System;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;

namespace CFCAIM.ReportDevExpress
{
    public partial class ManufacturedProductComplianceReport : DevExpress.XtraReports.UI.XtraReport
    {
        public ManufacturedProductComplianceReport()
        {
            InitializeComponent();
        }

        private void ManufacturedProductComplianceReport_BeforePrint(Object sender, System.ComponentModel.CancelEventArgs e)
        {            
        }
    }
}
