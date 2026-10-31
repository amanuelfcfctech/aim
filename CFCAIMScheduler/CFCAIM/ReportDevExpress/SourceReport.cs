using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
    public partial class SourceReport : DevExpress.XtraReports.UI.XtraReport
    {
        public SourceReport()
        {
            InitializeComponent();
        }

        private void SourceReport_AfterPrint(object sender, EventArgs e)
        {
            xrLabel1.Text = "i m writen at the afterPrint event on report";
        }

        private void SourceReport_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            xrLabel1.Text = "i m writen at the afterPrint event on report";
        }

    }
}
