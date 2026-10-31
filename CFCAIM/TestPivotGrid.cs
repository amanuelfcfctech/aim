using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
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
    public partial class TestPivotGrid : Form
    {
        public TestPivotGrid()
        {
            InitializeComponent();
        }

        private void TestPivotGrid_Load(Object sender, EventArgs e)
        {
                      

            String fromDate = "2016-01-01";
            String toDate = "2018-04-13";
            DateTime from_Date;
            DateTime to_Date;
            DateTime.TryParse(toDate, out to_Date);
            DateTime.TryParse(fromDate, out from_Date);


            AnalysisReportsInfo analysisSummaryToGet = new AnalysisReportsInfo();                      
            analysisSummaryToGet.SampleOwnerID = 0;
            analysisSummaryToGet.AIMOwnerLocationID = 0;
            analysisSummaryToGet.AIMSupplierID = 0;
            analysisSummaryToGet.AIMSupplierLocationID = 0;
            analysisSummaryToGet.ToDate = to_Date;
            analysisSummaryToGet.FromDate = from_Date;
            analysisSummaryToGet.AIMMaterialID = 0;
            analysisSummaryToGet.SwOutliersOnly = false;
            analysisSummaryToGet.SwExtremeOutlierExclude = false;
            analysisSummaryToGet.SwMildOutlierExclude = false;

            List<AnalysisReportsInfo> analysisSummaryToSet = new List<AnalysisReportsInfo>();
            analysisSummaryToSet = AIMManager.GetAnalysisDetail(ref analysisSummaryToGet,1);
                        
            //List<PivotGridTestInfo> grid = new List<PivotGridTestInfo>();
            pivotGridControl1.DataSource = analysisSummaryToSet;


        }
    }
}
