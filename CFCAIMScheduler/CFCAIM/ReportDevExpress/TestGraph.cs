using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CFCAIMCore.Entity;

namespace CFCAIM.ReportDevExpress
{
	public partial class TestGraph : DevExpress.XtraReports.UI.XtraReport
	{
		public TestGraph()
		{
			InitializeComponent();
		}

        private void xrChart1_BeforePrint(object sender, CancelEventArgs e)
        {
            //List<GraphInfo> detail = new List<GraphInfo>();

            //detail = (List<GraphInfo>)this.DataSource;
            //xrlblIngredientName.Text = detail[0].IngrendientName;
            //xrlblUserName.Text = detail[0].UserName;
            xrChart1.DataSource = this.DataSource;
            for (int i = 0; i < this.xrChart1.Series.Count; i++)
            {
                this.xrChart1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            }
        }

        private void TestGraph_BeforePrint(object sender, CancelEventArgs e)
        {
            
        }

        private void xrChart1_AfterPrint(object sender, EventArgs e)
        {
            //for (int i = 0; i < this.xrChart1.Series.Count; i++)
            //{
            //    this.xrChart1.Series[i].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //}
        }

	}
}
