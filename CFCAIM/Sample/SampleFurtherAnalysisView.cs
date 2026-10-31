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

namespace CFCAIM.Sample
{
    public partial class SampleFurtherAnalysisView : Form
    {

        CFCUser.Global userGlobal = new CFCUser.Global();
        List<SampleFurtherAnalysisInfo> _sampleFurtherAnalysis = new List<SampleFurtherAnalysisInfo>();

        public SampleFurtherAnalysisView()
        {
            InitializeComponent();
        }

        private void SampleFurtherAnalysisView_Load(object sender, EventArgs e)
        {
            //2/23/24
            if (AIMGlobal.sampleCodeFurtherAnalysis != "" && AIMGlobal.sampleCodeFurtherAnalysis != "New")
            {
                txtSampleCode.Text = AIMGlobal.sampleCodeFurtherAnalysis;
            }
            

            DataBindFurtherAnalysis();                            
            SetToolTips();
            WindowState = FormWindowState.Normal;
            
        }

        private void DataBindFurtherAnalysis()
        {
            Boolean isAll = false;
            SampleInfo sample = new SampleInfo();

			if (deBegDate.Text == "" || deEndDate.Text == "" )
            {
                if (deBegDate.Text == "")
                {
                    deBegDate.EditValue = DateTime.Now.AddDays(-7);
                }
                if (deEndDate.Text == "")
                {
                    deEndDate.EditValue = DateTime.Now;
                }                
            }
                       

            //2/6/24
            //if (rbShowAll.Checked == true)
            //{
            //    showRecs = "A";
            //}
            //else
            //{
            //    showRecs = "U";
            //}
            isAll = rbShowAll.Checked;


            //2/23/24
            //_sampleFurtherAnalysis = AIMManager.GetSampleFurtherAnalysis(Convert.ToDateTime(deBegDate.Text), Convert.ToDateTime(deEndDate.Text), isAll);
            if (txtSampleCode.Text != "")
            {
                sample = AIMManager.GetSample(txtSampleCode.Text);
                if (sample != null)
                {
                    _sampleFurtherAnalysis = AIMManager.GetSampleFurtherAnalysis(Convert.ToDateTime(deBegDate.Text), Convert.ToDateTime(deEndDate.Text), isAll, sample.SampleID);
                }
                
            }
            else
            {          

                //2/6/24
                //_sampleFurtherAnalysis = AIMManager.GetSampleFurtherAnalysis(Convert.ToDateTime(deBegDate.Text), Convert.ToDateTime(deEndDate.Text));
                _sampleFurtherAnalysis = AIMManager.GetSampleFurtherAnalysis(Convert.ToDateTime(deBegDate.Text), Convert.ToDateTime(deEndDate.Text), isAll);

            }

            grdFurtherAnalysis.DataSource = _sampleFurtherAnalysis;

        }

        private void SetToolTips()
        {
            try
            {
                this.Icon = Properties.Resources.CFCAIM;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
                ttip.SetToolTip(btnUpdateProcessDate, "Update Process Date to Current");
                ttip.SetToolTip(btnCheck, "Select All Records");
                ttip.SetToolTip(btnGetRecords, "Load Further Analysis Samples");
                ttip.SetToolTip(btnPreview, "Preview Grid");                                        //2/23/24
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void SampleFurtherAnalysisView_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
               

        private void btnGetRecords_Click(object sender, EventArgs e)
        {
            DataBindFurtherAnalysis();
        }

        private void btnAcceptAllFiles_Click(object sender, EventArgs e)
        {
            List<SampleFurtherAnalysisInfo> files = new List<SampleFurtherAnalysisInfo>();
            files = (List<SampleFurtherAnalysisInfo>)grdFurtherAnalysis.DataSource;
            if (files != null)
            {
                foreach (var file in files)
                {
                    //2/6/24 - check/uncheck only the records that have not been processed yet.
                    if (file.ProcessDate.ToString() == "1/1/0001 12:00:00 AM")    
                        
                    {
                        if (file.UpdateProcessDate == true)
                        {
                            file.UpdateProcessDate = false;
                        }
                        else
                        {
                            file.UpdateProcessDate = true;
                        }
                    }
                }
                grdFurtherAnalysisView.RefreshData();
            }
        }

        private void btnUpdateProcessDate_Click(object sender, EventArgs e)
        {
            //loop thru grid
            List<SampleFurtherAnalysisInfo> samples = new List<SampleFurtherAnalysisInfo>();
            samples = (List<SampleFurtherAnalysisInfo>)grdFurtherAnalysis.DataSource;
            if (samples != null)
            {
                foreach (var item in samples)
                {
                    if (item.UpdateProcessDate == true)
                    {
                        AIMManager.UpdateSampleFurtherAnalysis(item.SampleFurtherAnalysisID);                       
                    }
                }
            }
            btnRefresh.PerformClick();
        }

        //2/6/24
        private void grdFurtherAnalysisView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {                      

            if (
               e.Column.FieldName == "SampleDate" ||
               e.Column.FieldName == "UpdateStamp" ||
               e.Column.FieldName == "ProcessDate")
            {
                if (e.DisplayText == "01/01/01 00:00:00")
                {
                    e.DisplayText = "";
                }
            }
        }

        //2/6/24
        private void rbUnprocessed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbUnprocessed.Checked == true)
            {
				DataBindFurtherAnalysis();
			}
        }

        //2/6/24
        private void rbShowAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbShowAll.Checked == true) 
            { 
                DataBindFurtherAnalysis(); 
            }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                AIMGlobal.sampleCodeFurtherAnalysis = "";                       //2/23/24
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //2/23/24
        private void btnRefresh_Click(object sender, EventArgs e)
        {
        
            txtSampleCode.Text = "";
            DataBindFurtherAnalysis();
        }
    }
}
