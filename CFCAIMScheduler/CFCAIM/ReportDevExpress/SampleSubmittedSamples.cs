using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Collections.Generic;

namespace CFCAIM.ReportDevExpress
{
    public partial class SampleSubmittedSamples : DevExpress.XtraReports.UI.XtraReport
    {
        public SampleSubmittedSamples()
        {
            InitializeComponent();
        }

        private void SampleSubmittedSamples_BeforePrint(object sender, CancelEventArgs e)
        {
            //12/20/23 - changed from txt to label (so font size looks the same as the other fields in the report header)
            //txtCompany.Text = CFCAIMCore.Global.CompanyName();
            xrCompany.Text = CFCAIMCore.Global.CompanyName();

            if (AIMGlobal.useSampleDate == true)
            {
                xrLabelDate.Text = "Sample";
            }
            else
            {
                xrLabelDate.Text = "Created";
            }

            //Check alternatename table for 'Data Source'
            List<AlternateNameInfo> alternateName = new List<AlternateNameInfo>();
            alternateName = AIMManager.GetAlternateName((Int32)EnumFormID.Sample, "lblDataSource");
            if (alternateName != null)
            {
                xrLabelDataSource.Text = alternateName[0].AlternateName;
            }
            else
            {
                xrLabelDataSource.Text = "Data Source";
            }
        }
    }

}
