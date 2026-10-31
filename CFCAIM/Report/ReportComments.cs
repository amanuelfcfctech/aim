using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;

namespace CFCAIM.Report
{
    public partial class ReportComments : Form
    {
        CFCUser.Global userGlobal = new CFCUser.Global();
        Boolean _formLoad = true;
        Boolean _databind = true;
        Boolean _changesMade = false;       


        public ReportComments()
        {
            InitializeComponent();
            CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }        
                
        private void ReportComments_Load(object sender, EventArgs e)
        {
            try
            {
                DataBindlueReport();                
                SetToolTips();
                _formLoad = false;                
                WindowState = FormWindowState.Maximized;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBindlueReport()
        {
            List<ReportInfo> reports = new List<ReportInfo>();
            reports = AIMManager.GetAllReportsAllowingComments(true, true);
            reports.Insert
            (
                0,
                new ReportInfo
                {
                    ReportID = 0,
                    ReportName = "--Select--"
                }
            );
            lueReport.Properties.DataSource = reports;
            XtraEditors.LoadLookUpEdit(ref lueReport, "ReportID", "ReportName", "ReportCommentsID");
            lueReport.Properties.Columns["ReportCommentsID"].Visible = false;
        }

        private void CheckChanges()
        {
            if (_changesMade == true)
            {
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Save Changes?", "Warning...", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    _changesMade = false;
                }
                else
                {
                    btnSave.PerformClick();
                }
            }
        }
        
        private void lueReport_EditValueChanged(object sender, EventArgs e)
        {            
            if ((Int32)lueReport.EditValue > 0)
            {
                pnlReportComments.Visible = true;                
            }                                  
            DatabindComments();


        }
        private void DatabindComments()
        {
            _databind = true;
            List<ReportCommentsInfo> comments = new List<ReportCommentsInfo>();          
            if (lueReport.EditValue != null)
            {
                comments = AIMManager.GetReportComments((Int32)lueReport.EditValue);
                if (comments.Count > 0)
                {
                    txtComment1.Text = comments[0].ReportComment1;
                    txtComment2.Text = comments[0].ReportComment2;
                    txtComment3.Text = comments[0].ReportComment3;
                    txtComment4.Text = comments[0].ReportComment4;
                    txtComment5.Text = comments[0].ReportComment5;
                }
                else
                {
                    txtComment1.Text = String.Empty;
                    txtComment2.Text = String.Empty;
                    txtComment3.Text = String.Empty;
                    txtComment4.Text = String.Empty;
                    txtComment5.Text = String.Empty;
                }
            }
            _databind = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if ((Int32)lueReport.EditValue == 0)
            {
                MessageBox.Show("Please select a Report. ", "Warning...", MessageBoxButtons.OK);
                lueReport.Focus();
                lueReport.ShowPopup();
                return;
            }

            ReportCommentsInfo comments = new ReportCommentsInfo();
            comments.ReportCommentsID = (Int32)lueReport.GetColumnValue("ReportCommentsID");
            comments.ReportID = (Int32)lueReport.EditValue;
            comments.ReportComment1 = txtComment1.Text;
            comments.ReportComment2 = txtComment2.Text;
            comments.ReportComment3 = txtComment3.Text;
            comments.ReportComment4 = txtComment4.Text;
            comments.ReportComment5 = txtComment5.Text;
            comments.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

            ErrorInfo error = new ErrorInfo();
            error = AIMManager.SaveReportComments(ref comments);
            if (error.ErrorNumber > 0)
            {
                return;
            }
            _changesMade = false;
            DatabindComments();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ReportComments_SizeChanged(object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                CheckChanges();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtComment1_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == false && _databind == false)
            {
                _changesMade = true;
            }
        }

        private void txtComment2_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == false && _databind == false)
            {
                _changesMade = true;
            }
        }

        private void txtComment3_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == false && _databind == false)
            {
                _changesMade = true;
            }
        }

        private void txtComment4_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == false && _databind == false)
            {
                _changesMade = true;
            }
        }

        private void txtComment5_TextChanged(object sender, EventArgs e)
        {
            if (_formLoad == false && _databind == false)
            {
                _changesMade = true;
            }
        }

        private void lueReport_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_formLoad == false)
            {
                if (e.NewValue != e.OldValue)
                {
                    CheckChanges();
                }
            }
        }
    }
}
