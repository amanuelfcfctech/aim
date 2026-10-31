using CFCAIM.ReportDevExpress;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFCAIM
{
    public partial class testChart : Form
    {
        public testChart()
        {
            InitializeComponent();
        }

        private void chartGraphControlBySampleCode_CustomDrawCrosshair(Object sender, DevExpress.XtraCharts.CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElement ce in e.CrosshairElements)
            {
                LineSeriesView v;
                v = ce.Series.View as LineSeriesView;
                if (ce.Series.Name == "Max")
                {
                    //////THIS CAUSES THE ENTIRE CHART, LEGEND, TITLES TO DISAPPEAR !
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.False;
                }
                else
                {
                    (ce.Series.View as LineSeriesView).MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                }
            }
        }

        private void chartGraphControlBySampleCode_CustomDrawSeriesPoint(Object sender, CustomDrawSeriesPointEventArgs e)
        {
            //if (e.Series.Name == "Max")
            //{
            //    e.Series.Points.Remove(e.SeriesPoint);
            //}
        }

        private DataTable CreateChartData()
        {
            DataTable table = new DataTable("Table1");
            
            table.Columns.Add("SampleCode", typeof(String));
            table.Columns.Add("SampleDate", typeof(String));
            table.Columns.Add("Section", typeof(String));
            table.Columns.Add("Value", typeof(Int32));

            table.Rows.Add(new object[] { "A", "Jan", "Protein", 10 });
            table.Rows.Add(new object[] { "A", "Jan", "Max", 20 });
            table.Rows.Add(new object[] { "A", "Feb", "Protein", 20 });
            table.Rows.Add(new object[] { "B", "Feb", "Max", 30 });
            table.Rows.Add(new object[] { "B", "March", "Protein", 15 });
            table.Rows.Add(new object[] { "B", "March", "Max", 25 });

            return table;
        }

        private void btnGetChartData_Click(Object sender, EventArgs e)
        {

            chartGraphControlBySampleCode.SeriesDataMember = "Section";
            chartGraphControlBySampleCode.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });
            chartGraphControlBySampleCode.SeriesTemplate.ArgumentDataMember = "SampleCode";           
            chartGraphControlBySampleCode.DataSource = CreateChartData();
            
        }
        
    }
}
