using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
    public partial class AnalysisDateComparisonReport : DevExpress.XtraReports.UI.XtraReport
    {
        public AnalysisDateComparisonReport()
        {
            InitializeComponent();
        }

		private void AnalysisDateComparisonReport_BeforePrint(object sender, CancelEventArgs e)
		{
                        
            //4/20/21
            if (AIMGlobal.sortNutrientsBy == "Code")
            {
                Detail.SortFields.Add(new GroupField("SortNutrientCode", XRColumnSortOrder.Ascending));
            }
            else
            {
                Detail.SortFields.Add(new GroupField("NutrientName", XRColumnSortOrder.Ascending));
            }

            //12/20/23 - changed from txt to label (so font size looks the same as the other fields in the report header)
            //txtCompany.Text = CFCAIMCore.Global.CompanyName();
            xrCompany.Text = CFCAIMCore.Global.CompanyName();
        }

    }
}
