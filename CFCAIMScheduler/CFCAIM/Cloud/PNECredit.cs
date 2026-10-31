using CFCAIMCore.Entity;
using CFCAIMCore.Entity.Adisseo;
using CFCAIMCore.Manager;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CFCAIM.Cloud
{
    public partial class PNECredit : Form
    {

        CFCUser.Global userGlobal = new CFCUser.Global();
        public PNECredit()
        {
            InitializeComponent();
        }

        List<PNECreditInfo> _pneCredits = new List<PNECreditInfo>();
        

        private void PNECredit_Load(object sender, EventArgs e)
        {
            DatabindGrid();
            SetToolTips();
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
        }

        private void DatabindGrid()
        {
            Int32 rowNum = 0;
            Int32.TryParse(grdPNECreditView.FocusedRowHandle.ToString(), out rowNum);
            grdPNECredit.DataSource = AIMManager.GetPNECredit();
            grdPNECreditView.FocusedRowHandle = rowNum;
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void PNECredit_SizeChanged(object sender, EventArgs e)
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

        //2/23/24
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                CFCAIMCore.Global.previewGridResults(grdPNECredit, "PNE Credits");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //2/23/24
        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = new DialogResult();

                if (txtKey.Text == "" && txtDescription.Text == "")
                {
                    MessageBox.Show("Please enter a Key and Description.", "Warning...", MessageBoxButtons.OK);
                    return;
                }
                if (txtKey.Text == "")
                {
                    dr = MessageBox.Show("PNE Key is blank. Continue?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }
                if (txtDescription.Text == "")
                {
                    dr = MessageBox.Show("PNE Description is blank. Continue?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }
                PNECreditInfo pneCredit = new PNECreditInfo();
                pneCredit = new PNECreditInfo();
                pneCredit.PNECreditID = 0;
                pneCredit.PNEKey = txtKey.Text;
                pneCredit.PNEDescription = txtDescription.Text;
                pneCredit.CFCUserID = 0;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SavePNECredit(ref pneCredit);
                DatabindGrid();

                txtKey.Text = "";
                txtDescription.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        //2/23/24
        private void repbtnDeletePNECredit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {            
                DialogResult dr;
                PNECreditInfo pne = new PNECreditInfo();
                pne = (PNECreditInfo)grdPNECreditView.GetFocusedRow();                       
                dr = MessageBox.Show("Delete this PNE Credit?", "Warning...", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    AIMManager.DeletePNECredit(pne.PNECreditID);                
                }
                DatabindGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //2/23/24
        private void grdPNECreditView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                PNECreditInfo pne = new PNECreditInfo();
                pne = (PNECreditInfo)grdPNECreditView.GetFocusedRow();
                pne.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SavePNECredit(ref pne);
                DatabindGrid();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
