using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;


namespace CFCAIM.ReportDevExpress
{
    public partial class SampleAnalysisRequestFormAjinomoto : DevExpress.XtraReports.UI.XtraReport
    {
        public SampleAnalysisRequestFormAjinomoto()
        {
            InitializeComponent();
        }

        private void SampleAnalysisRequestFormAjinomoto_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //12/20/23 - changed from txt to label (so font size looks the same as the other fields in the report header)
            //txtCompany.Text = CFCAIMCore.Global.CompanyName();
            xrCompany.Text = CFCAIMCore.Global.CompanyName();

            if (AIMGlobal.materialTypeID == (Int32)MaterialTypeEnum.Product)
            {
                xrIngredientProductLabel.Text = "Product Name";
            }
            else
            {
                xrIngredientProductLabel.Text = "Ingredient Name";
            }

            //10/20/23
            //List<ReportCommentsInfo> notes = new List<ReportCommentsInfo>();
            //notes = AIMManager.GetReportComments(AIMGlobal.reportID);
            //if (notes.Count > 0)
            //{
            //    xrComment1.Text = notes[0].ReportComment1;
            //    xrComment2.Text = notes[0].ReportComment2;
            //    xrComment3.Text = notes[0].ReportComment3;
            //}
        }

        //10/20/23
        private void PageHeader_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<ReportCommentsInfo> notes = new List<ReportCommentsInfo>();
            notes = AIMManager.GetReportComments(AIMGlobal.reportID);
            if (notes.Count > 0)
            {
                if (notes[0].ReportComment1.Length == 0)
                {
                    xrPanelComment1.Visible = false;
                }
                else
                {
                    xrComment1.Text = notes[0].ReportComment1;
                }
                if (notes[0].ReportComment2.Length == 0)
                {
                    xrPanelComment2.Visible = false;
                }
                else
                {
                    xrComment2.Text = notes[0].ReportComment2;
                }
                if (notes[0].ReportComment3.Length == 0)
                {
                    xrPanelComment3.Visible = false;
                }
                else
                {
                    xrComment3.Text = notes[0].ReportComment3;
                }
            }
        }
    }
}
