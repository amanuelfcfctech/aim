using DevExpress.XtraReports.UI;
using DevExpress.XtraSpreadsheet.Commands;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace CFCAIM.ReportDevExpress
{
    public partial class CostAnalysisReport : DevExpress.XtraReports.UI.XtraReport
    {
        private DateTime _toDate;
        private DateTime _fromDate;
        private String _locationName;

        public DateTime ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
            }
        }

        public DateTime FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
            }
        }

        public String LocationName
        {
            get
            {
                return _locationName;
            }
            set
            {
                _locationName = value;
            }
        }

        public CostAnalysisReport()
        {
            InitializeComponent();
        }

        private void CostAnalysisReport_BeforePrint(object sender, CancelEventArgs e)
        {
            xrFromDate.Text = FromDate.ToString("d");
            xrToDate.Text = ToDate.ToString("d");
            xrLocationName.Text = LocationName;
        }
    }
}
