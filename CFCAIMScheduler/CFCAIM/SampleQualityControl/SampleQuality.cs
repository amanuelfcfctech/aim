using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace CFCAIM.SampleQualityControl
{
    public partial class SampleQuality : Form
    {

        CFCUser.Global userGlobal = new CFCUser.Global();
        List<SampleQualityEquationInfo> _equation = new List<SampleQualityEquationInfo>();
        SampleQualityControlEquation sampleQualityControlEquation;
        SystemSettings systemSettings;

        public SampleQuality()
        {
            InitializeComponent();
        }

        private void SampleQuality_Load(Object sender, EventArgs e)
        {
            try
            {
                DataBind();
                SetToolTips();

                if (AIMGlobal.testNewMathTypeEquation == true)
                {                    
                    AIMGlobal.testNewEquation = false;                    
                }
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBind()
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdQualityControlView.FocusedRowHandle.ToString(), out rowNum);
            grdQualityControl.DataSource = AIMManager.GetQualityControlEquations(true);
            grdQualityControlView.FocusedRowHandle = rowNum;
            _equation = AIMManager.GetQualityControlEquations(true);
        }

        private void SetToolTips()
        {
            try
            {
                System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                this.Icon = ico;
                ToolTip ttip = new ToolTip();
                foreach (Control ctrl in pnlTopBtns.Controls)
                {
                    ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
                }
                ttip.SetToolTip(btnPreviewGridQualityControl, "Preview Equations");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
                ttip.SetToolTip(btnAdd, "Add New Equation");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            try
            {
                Close();
                AIMGlobal.open_form(systemSettings = new SystemSettings());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(Object sender, EventArgs e)
        {
            DataBind();            
        }

        private void btnPreviewGridQualityControl_Click(Object sender, EventArgs e)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdQualityControl, "Sample Quality Control Equations");
                CFCAIMCore.Global.previewGridResults(grdQualityControl, "Sample Quality Control Equations");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repEdit1_Click(Object sender, EventArgs e)
        {
            AIMGlobal.newEquation = false;
            AIMGlobal.equationID = (Int32)grdQualityControlView.GetFocusedRowCellValue(colSampleQualityEquationID);
            Close();
            AIMGlobal.open_form(sampleQualityControlEquation = new SampleQualityControlEquation());

        }

        private void grdQualityControlView_ValidateRow(Object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                SampleQualityEquationInfo equation = new SampleQualityEquationInfo();
                equation = (SampleQualityEquationInfo)grdQualityControlView.GetFocusedRow();
                equation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveSampleQualityEquation(ref equation);
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void SampleQuality_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnAdd_Click(Object sender, EventArgs e)
        {
            AIMGlobal.equationID = 0;
            AIMGlobal.newEquation = true;
            Close();
            AIMGlobal.open_form(sampleQualityControlEquation = new SampleQualityControlEquation());

        }

        private void repDelete1_Click(Object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Delete Equation?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.No)
                {
                    SampleQualityEquationInfo equation = new SampleQualityEquationInfo();
                    equation = (SampleQualityEquationInfo)grdQualityControlView.GetFocusedRow();
                    equation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                    AIMManager.DeleteSampleQualityEquation(equation.SampleQualityEquationID);                    
                    DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdQualityControl_Click(object sender, EventArgs e)
        {

        }
    }
}
