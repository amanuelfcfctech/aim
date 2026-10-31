using CFCAIM.ReportDevExpress;
using DevExpress.XtraReports.UserDesigner;
using System;

namespace CFCAIM.Report
{
    public partial class ReportCreate : XRDesignFormEx
    {
        public ReportCreate()
        {
            InitializeComponent();
        }
               
        private void ReportCreate_Load(Object sender, EventArgs e)
        {            
            XtraReport1 report = new XtraReport1();
            reportDesigner1.OpenReport(report);            
        }
    }
}
