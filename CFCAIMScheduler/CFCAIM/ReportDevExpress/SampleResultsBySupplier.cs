using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
    public partial class SampleResultsBySupplier : DevExpress.XtraReports.UI.XtraReport
    {
        public SampleResultsBySupplier()
        {
            InitializeComponent();
        }

        private void SampleResultsBySupplier_BeforePrint(Object sender, CancelEventArgs e)
        {
            //9/10/20 - change to use AIMCoreGlobal
            //txtCompany.Text = AIMGlobal.CompanyName();
            txtCompany.Text = CFCAIMCore.Global.CompanyName();
        }
    }
}
