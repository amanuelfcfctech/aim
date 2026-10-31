using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CFCAIMCore.Entity;

namespace CFCAIM.ReportDevExpress
{
	public partial class TestMark : DevExpress.XtraReports.UI.XtraReport
	{
		public TestMark()
		{
			InitializeComponent();
		}

		private void TestMark_BeforePrint(object sender, CancelEventArgs e)
		{
			List<GraphInfo> detail = new List<GraphInfo>();

			detail = (List<GraphInfo>)this.DataSource;
			xrlblIngredientName.Text = detail[0].IngrendientName;
			xrlblUserName.Text = detail[0].UserName;
			xrlblStartDate.Text = detail[0].StartDate.ToShortDateString();
			xrlblEndDate.Text = detail[0].EndDate.ToShortDateString();
			xrChart1.DataSource = this.DataSource;
		}

	}
}
