using DevExpress.XtraReports.UI;

namespace CFCAIM.ReportDevExpress
{
    public partial class SampleComparisonReport : DevExpress.XtraReports.UI.XtraReport
	{
		public SampleComparisonReport()
		{
			InitializeComponent();
		}
		private void SampleComparisonReport_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//---------------------------------------
			//4/20/21
			if (AIMGlobal.sortNutrientsBy == "Code")
			{
				Detail.SortFields.Add(new GroupField("SortNutrientCode", XRColumnSortOrder.Ascending));
			}
			else
			{
				Detail.SortFields.Add(new GroupField("AimNutrientName", XRColumnSortOrder.Ascending));
			}

			//---------------------------------------
			//9/10/20 - change to use AIMCoreGlobal
			//txtCompany.Text = AIMGlobal.CompanyName();
			txtCompany.Text = CFCAIMCore.Global.CompanyName();

            xrlabel_Name1.Text = AIMGlobal.dataSourceName1;
			xrlabel_Name2.Text = AIMGlobal.dataSourceName2;
		}

		private void PageHeader_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
		{			
		}
	}
}
