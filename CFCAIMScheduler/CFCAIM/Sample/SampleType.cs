using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CFCAIM.Sample
{
    public partial class SampleType : Form
    {

        CFCUser.Global userGlobal = new CFCUser.Global();
        List<SampleTypeInfo> _sampleType = new List<SampleTypeInfo>();

        public SampleType()
        {
            InitializeComponent();
        }

        private void SampleType_Load(object sender, EventArgs e)
        {
            DataBind();
            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DataBind()
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdSampleTypeView.FocusedRowHandle.ToString(), out rowNum);
            grdSampleType.DataSource = AIMManager.GetSampleType(rbActiveRecords.Checked);
            grdSampleTypeView.FocusedRowHandle = rowNum;
            _sampleType = AIMManager.GetSampleType(rbActiveRecords.Checked);

        }

        private void SampleType_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
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
                ttip.SetToolTip(btnAdd, "Add Record");
                ttip.SetToolTip(btnPreviewGridResults, "Preview Grid Results");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
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
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DataBind();
                SetToolTips();
                txtName.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Please enter a Name. ", "Warning...", MessageBoxButtons.OK);
                    txtName.Focus();
                    return;
                }

                SampleTypeInfo name = new SampleTypeInfo();
                name = AIMManager.GetSampleTypeName(txtName.Text);
                if (name != null && name.SampleTypeID != 0)
                {
                    MessageBox.Show("This name already exists. Please enter a difference one.", "Warning...", MessageBoxButtons.OK);
                    txtName.Text = "";                    
                    txtName.Focus();
                    return;
                }
                if (txtName.Text.Length > 50)
                {
                    MessageBox.Show("Name may not be more than 50 characters. ", "Warning...", MessageBoxButtons.OK);
                    txtName.Focus();
                    return;
                }


                SampleTypeInfo sampleType = new SampleTypeInfo();
                sampleType.SampleTypeID = 0;
                sampleType.SampleTypeName = txtName.Text;
                sampleType.IsActive = true;
                sampleType.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveSampleType(ref sampleType);
                if (error.ErrorNumber > 0)
                {
                    return;
                }
                DataBind();
                txtName.Text = String.Empty;
                txtName.Focus();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
              

        private void repbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                String msg = "";
                SampleTypeInfo sampleType = new SampleTypeInfo();
                sampleType = (SampleTypeInfo)grdSampleTypeView.GetFocusedRow();

                //------------------------------------------------------------------
                //If plant exists on a sample, do not allow it to be deleted, instead inactive the record	
                if (rbActiveRecords.Checked == true)
                {
                    SampleInfo sample = new SampleInfo();
                    sample = AIMManager.GetSampleUsingSampleType(sampleType.SampleTypeID);
                    if (sample != null)
                    {
                        msg =  "This sample type exists on samples and will be INACTIVATED.";
                        msg += Environment.NewLine + Environment.NewLine + "Before it can be deleted, it must first be removed from any samples.";
                        MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);

                        sampleType.IsActive = !sampleType.IsActive;
                        sampleType.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        AIMManager.SaveSampleType(ref sampleType);                        
                    }
                    else
                    {
                        //Delete from SampleType table
                        sampleType.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                        AIMManager.DeleteSampleType(sampleType.SampleTypeID);
                    }
                }
                else
                {
                    sampleType.IsActive = !sampleType.IsActive;
                    sampleType.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                    AIMManager.SaveSampleType(ref sampleType);
                }
                
                DataBind();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rbActiveRecords_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbActiveRecords.Checked)
                {
                    repbtnDelete.Buttons[0].Image = Properties.Resources.delete;
                    repbtnDelete.Buttons[0].ToolTip = "Delete record";
                    txtName.Enabled = true;
                    btnAdd.Enabled = true;                    
                }
                else
                {
                    repbtnDelete.Buttons[0].Image = Properties.Resources.Undo_icon;
                    repbtnDelete.Buttons[0].ToolTip = "Re-activate record";
                    txtName.Enabled = false;                    
                    btnAdd.Enabled = false;

                }
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPreviewGridResults_Click(object sender, EventArgs e)
        {
            try
            {                
                CFCAIMCore.Global.previewGridResults(this.grdSampleType, "Sample Types");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdSampleTypeView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            AIMGlobal.focusedRowGreen(sender, e);
        }
    }
}
