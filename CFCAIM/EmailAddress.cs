using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;

namespace CFCAIM
{
	public partial class EmailAddress : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		public EmailAddress()
		{
			InitializeComponent();
		}

		private void EmailAddress_Load(object sender, EventArgs e)
		{
			try
			{				
				DataBind();
				SetToolTips();
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
                			
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
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
                Icon = ico;
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
		private void DataBind()
		{
			grdEmailAddress.DataSource = AIMManager.GetEmailAddress();
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
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
				DataBind();
				ClearTextFields();
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void ClearTextFields()
		{
			txtEmailAddress.Text = String.Empty;
			txtIPAddress.Text = String.Empty;
			txtUserName.Text = String.Empty;
			txtPassword.Text = String.Empty;
		}
				
		private void repbtnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				EmailAddressInfo emailAddress = new EmailAddressInfo();
				emailAddress = (EmailAddressInfo)grdEmailAddressView.GetFocusedRow();
				emailAddress.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteEmailAddress(emailAddress.EmailAddressID);
				DataBind();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void grdEmailAddressView_ValidateRow(object sender, ValidateRowEventArgs e)
		{

			EmailAddressInfo emailAddress = new EmailAddressInfo();

			emailAddress = (EmailAddressInfo)grdEmailAddressView.GetFocusedRow();
			if (emailAddress.EmailAddress.Length == 0)
			{
				MessageBox.Show("Please enter an email address. Information is not saved.", "Warning...");
				return;
			}
			if (emailAddress.EmailIPAddress.Length == 0)
			{
				MessageBox.Show("Please enter an IP address. Information is not saved.", "Warning...");
				return;
			}
			if (emailAddress.EmailUserName.Length == 0)
			{
				MessageBox.Show("Please enter a username. Information is not saved.", "Warning...");
				return;
			}
			if (emailAddress.EmailPassword.Length == 0)
			{
				MessageBox.Show("Please enter a password. Information is not saved.", "Warning...");
				return;
			}
			emailAddress.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveEmailAddress(ref emailAddress);
			DataBind();

		}			

		private void btnPreviewGridResults_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdEmailAddress, "Email Address");
				CFCAIMCore.Global.previewGridResults(this.grdEmailAddress, "Email Address");
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
				EmailAddressInfo emailAddress = new EmailAddressInfo();

				emailAddress.EmailAddressID = 0;
				emailAddress.EmailAddress = txtEmailAddress.Text;
				emailAddress.EmailIPAddress = txtIPAddress.Text;
				emailAddress.EmailUserName = txtUserName.Text;
				emailAddress.EmailPassword = txtPassword.Text;
				emailAddress.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				if (txtEmailAddress.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter an Email Address. ", "Warning...", MessageBoxButtons.OK);
					txtEmailAddress.Focus();
					return;
				}
				if (txtIPAddress.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter an IP Address. ", "Warning...", MessageBoxButtons.OK);
					txtIPAddress.Focus();
					return;
				}
				if (txtUserName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Username. ", "Warning...", MessageBoxButtons.OK);
					txtUserName.Focus();
					return;
				}
				if (txtPassword.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Password. ", "Warning...", MessageBoxButtons.OK);
					txtPassword.Focus();
					return;
				}

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveEmailAddress(ref emailAddress);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtEmailAddress.Text = String.Empty;
				txtIPAddress.Text = String.Empty;
				txtUserName.Text = String.Empty;
				txtPassword.Text = String.Empty;
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

        private void EmailAddress_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }
    }
}
