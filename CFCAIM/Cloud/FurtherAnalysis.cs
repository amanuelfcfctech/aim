using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIM.Cloud
{
    public partial class FurtherAnalysis : Form
    {       

        tpProdSpec sample;
        CFCUser.Global userGlobal = new CFCUser.Global();
        List<SampleFurtherAnalysisInfo> _sampleFurtherAnalysis = new List<SampleFurtherAnalysisInfo>();

        public FurtherAnalysis()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }

        private void FurtherAnalysis_Load(object sender, EventArgs e)
        {
            DataBindlueDataSourceType();

            //DatabindGrid();                                   //2/23/24 - commented
            deBegDate.EditValue = DateTime.Now.AddDays(-30);    //2/23/24
            deEndDate.EditValue = DateTime.Now;                 //2/23/24

            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DatabindGrid()
        {          

            //Begin and End Dates
            if (deBegDate.Text == "" || deEndDate.Text == "")
            {
                if (deBegDate.Text == "")
                {
                    deBegDate.EditValue = DateTime.Now.AddDays(-30);
                }
                if (deEndDate.Text == "")
                {
                    deEndDate.EditValue = DateTime.Now;
                }
            }

            //Sample, Process or Created Date
            Int32 dateField = (Int32)EnumDateFields.SampleDate;
            if (rbCreated.Checked == true)
            {
                dateField = (Int32)EnumDateFields.CreatedDate;
            }
            else if (rbProcess.Checked == true)
            {
                dateField = (Int32)EnumDateFields.ProcessDate;
            }            

            _sampleFurtherAnalysis = AIMManager.GetSampleFurtherAnalysis(Convert.ToDateTime(deBegDate.Text), Convert.ToDateTime(deEndDate.Text), dateField, (Int32)lueDataSourceType.EditValue);
            grdFurtherAnalysis.DataSource = _sampleFurtherAnalysis;
        }

        private void DataBindlueDataSourceType()
        {
            List<DataSourceTypeInfo> dataSource = new List<DataSourceTypeInfo>();
            dataSource = AIMManager.GetDataSourceFurtherAnalyses(true);

            dataSource.Insert
            (
                0,
                new DataSourceTypeInfo
                {
                    DataSourceTypeID = 0,
                    Name = "--Select--"
                }
            );
            lueDataSourceType.Properties.DataSource = dataSource;
            XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name");

            if (dataSource.Count == 2)
            {
                //selecting value if there is only one
                lueDataSourceType.EditValue = dataSource[1].DataSourceTypeID;
            }
            else
            {
                lueDataSourceType.EditValue = 0; // dataSource; 11/14/24
            }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void FurtherAnalysis_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
        private void SetToolTips()
        {
            try
            {
                this.Icon = (Icon)Properties.Resources.CFCAIM;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }

                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
              

        private void repHyperSampleCode_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.SplashScreenShow();
                Int32 sampleID;
                Int32.TryParse(grdFurtherAnalysisView.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);                
                CFCAIMCore.Global.sampleID = sampleID;

                if (grdFurtherAnalysisView.GetFocusedRowCellValue(colSampleCode) != null)
                {
                    AIMGlobal.sampleCode = grdFurtherAnalysisView.GetFocusedRowCellValue(colSampleCode).ToString();
                    AIMGlobal.fromAnalysisScreen = true;
                }
                
                AIMGlobal.open_form(sample = new tpProdSpec());

                //-------------------------------------
                if (sample == null || sample.Visible == false)
                {
                    sample = new tpProdSpec();
                    sample.MdiParent = this.MdiParent;
                    sample.Show();
                }
                else
                {             
                    sample.WindowState = FormWindowState.Maximized;
                    sample.Activate();
                }

                AIMGlobal.SplashScreenHide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdFurtherAnalysisView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (
                e.Column.FieldName == "SampleDate" ||
                e.Column.FieldName == "CreatedDate" ||
                e.Column.FieldName == "ProcessDate")
            {
                if (e.DisplayText == "1/1/0001")
                {
                    e.DisplayText = "";
                }
            }
        }
                
        private void deBegDate_EditValueChanged(object sender, EventArgs e)
        {
            //DatabindGrid();               //2/23/24 - commented
        }

        private void deEndDate_EditValueChanged(object sender, EventArgs e)
        {
            //DatabindGrid();               //2/23/24 - commented
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {            
            rbSample.Checked = true;
            deBegDate.EditValue = DateTime.Now.AddDays(-30);
            deEndDate.EditValue = DateTime.Now;

            //DatabindGrid();               //2/23/24 - commented
            deBegDate.Text = "";                                    //2/23/24
            deEndDate.Text = "";                                    //2/23/24
            grdFurtherAnalysis.DataSource = null;                     //2/23/24

            DataBindlueDataSourceType();
        }

        private void rbSample_CheckedChanged(object sender, EventArgs e)
        {
            //DatabindGrid();               //2/23/24 - commented
        }

        private void rbCreated_CheckedChanged(object sender, EventArgs e)
        {
            //DatabindGrid();               //2/23/24 - commented
        }

        private void rbProcess_CheckedChanged(object sender, EventArgs e)
        {
            //DatabindGrid();               //2/23/24 - commented
        }

        private void lueDataSourceType_EditValueChanged(object sender, EventArgs e)
        {
            //DatabindGrid();               //2/23/24 - commented
        }

        //2/23/24
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdFurtherAnalysis, "Further Analysis");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //2/23/24
        private void btnGetRecords_Click(object sender, EventArgs e)
        {
            DatabindGrid();
        }

    }
}
