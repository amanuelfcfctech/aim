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

namespace CFCAIM.Cloud
{
    public partial class PendingSample : Form
    {
        tpProdSpec sample;
        List<AnalyzerPendingSampleInfo> _analyzerPendingSample = new List<AnalyzerPendingSampleInfo>();
        public PendingSample()
        {
            InitializeComponent();
        }             

        private void PendingSample_Load(object sender, EventArgs e)
        {
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
            Int32 dateField = (Int32)EnumDateFields.CreatedDate;
            if (rbSent.Checked == true)
            {
                dateField = (Int32)EnumDateFields.SentDate;
            }
            else if (rbReceived.Checked == true)
            {
                dateField = (Int32)EnumDateFields.ReceivedDate;
            }


            _analyzerPendingSample = AIMManager.GetAnalyzerPendingSample(Convert.ToDateTime(deBegDate.Text), Convert.ToDateTime(deEndDate.Text), dateField);
            grdPendingSample.DataSource = _analyzerPendingSample;

        }
        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }
        private void PendingSample_SizeChanged(object sender, EventArgs e)
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
                Int32.TryParse(grdPendingSampleView.GetFocusedRowCellValue(colSampleID).ToString(), out sampleID);
                CFCAIMCore.Global.sampleID = sampleID;

                if (grdPendingSampleView.GetFocusedRowCellValue(colSampleCode) != null)
                {
                    AIMGlobal.sampleCode = grdPendingSampleView.GetFocusedRowCellValue(colSampleCode).ToString();
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

        private void grdPendingSampleView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {            
            if (
                e.Column.FieldName == "CreatedStamp" ||
                e.Column.FieldName == "SentStamp" ||
                e.Column.FieldName == "ReceivedStamp" ||
                e.Column.FieldName == "NIRAnalysis" )
            {                 
                if (e.DisplayText == "1/1/0001")
                {
                    e.DisplayText = "";
                }                
            }            
        }

        private void deBegDate_EditValueChanged(object sender, EventArgs e)
        {
            //DatabindGrid();       //2/23/24 - commented
        }

        private void deEndDate_EditValueChanged(object sender, EventArgs e)
        {
            //DatabindGrid();       //2/23/24 - commented
        }

        private void rbCreated_CheckedChanged(object sender, EventArgs e)
        {
            //DatabindGrid();       //2/23/24 - commented
        }

        private void rbSent_CheckedChanged(object sender, EventArgs e)
        {
            //DatabindGrid();       //2/23/24 - commented
        }

        private void rbReceived_CheckedChanged(object sender, EventArgs e)
        {
            //DatabindGrid();       //2/23/24 - commented
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            rbCreated.Checked = true;
            //deBegDate.EditValue = DateTime.Now.AddDays(-30);      //2/23/24 - commented
            //deEndDate.EditValue = DateTime.Now;                   //2/23/24 - commented            
            //DatabindGrid();                                       //2/23/24 - commented
            deBegDate.Text = "";                                    //2/23/24
            deEndDate.Text = "";                                    //2/23/24
            grdPendingSample.DataSource = null;                     //2/23/24

        }

        //2/23/24
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdPendingSample, "Pending Samples");
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
