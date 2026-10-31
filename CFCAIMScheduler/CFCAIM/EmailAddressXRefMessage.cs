using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CFCAIM.Maintenance;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;



namespace CFCAIM
{
	public partial class EmailAddressXRefMessage : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();

		public EmailAddressXRefMessage()
		{
			InitializeComponent();
		}
			


		private void EmailAddressXRefMessage_Load(object sender, EventArgs e)
		{
			try
			{
				DataBindlue();
				DataBindcmb();
				DataBind();
				SetToolTips();
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION	

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region  Local Function

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
				ttip.SetToolTip(btnPreviewResults, "Preview Grid");
				ttip.SetToolTip(btnAddWarningLimit, "Add Record");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DataBindlue()
		{
			List<CFCUserInfo> userinfo = new List<CFCUserInfo>();
			userinfo = AIMManager.GetCFCUsers(true);
			userinfo.Insert
			(
				0,
				new CFCUserInfo
				{
					CFCUserID = 0,
					FirstName = "--Select--"
				}
			);
			lueCFCUsers.Properties.DataSource = userinfo;
			XtraEditors.LoadLookUpEdit(ref lueCFCUsers, "CFCUserID", "CFCUserName");
			lueCFCUsers.Properties.Columns["CFCUserID"].Visible = false;
			lueCFCUsers.Properties.Columns["CFCUserName"].Visible = true;

			//grid look-up edit
			replueCFCUserName.DataSource = userinfo;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCUserName, "CFCUserID", "CFCUserName");
			replueCFCUserName.Columns["CFCUserID"].Visible = false;
			replueCFCUserName.Columns["CFCUserName"].Visible = true;
		}
		private void DataBindcmb()
		{
			//combo boxes (on form and in grid) - 
			//Had to set up as combo-box instead of lookupedit because data-source is enum (and not SQL table)
			String txtSelect = "--Select--";
			cmbMessage.Items.Add(txtSelect);
			var values = Enum.GetValues(typeof(EmailMessageEnum));
			foreach (var item in values)
			{
				cmbMessage.Items.Add(item);
			}
			cmbMessage.SelectedIndex = 0;


		}
		

		private void DataBind()
		{
			Int32 rowNum = 0;
			Int32.TryParse(this.grdEmailMessageXRefView.FocusedRowHandle.ToString(), out rowNum);
			grdEmailMessageXRef.DataSource = AIMManager.GetAllEmailMessageXrefMessage();
			grdEmailMessageXRefView.FocusedRowHandle = rowNum;
						
		}

		private void Transform(DataTable table)
		{	
			
			table.Columns.Add("colMessageDescription", typeof(EmailMessageEnum));
			foreach (DataRow row in table.Rows)
			{
				int value = (int)row[1]; //int representation of enum
				row[2] = (EmailMessageEnum)value;
			}
		}

		#endregion

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

		private void btnPreviewResults_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdEmailMessageXRef, "Email Address and Message XRef");
				CFCAIMCore.Global.previewGridResults(grdEmailMessageXRef, "Email Address and Message XRef");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddEmailMessage_Click(object sender, EventArgs e)
		{
			try
			{							

				if (lueCFCUsers.Text.ToString() == "--Select-- " || lueCFCUsers.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select a User.", "Warning...", MessageBoxButtons.OK);
					lueCFCUsers.ShowPopup();
					return;
				}
				if (cmbMessage.Text.ToString() == "--Select--" || cmbMessage.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select an email Message.", "Warning...", MessageBoxButtons.OK);
					cmbMessage.DroppedDown = true;
					return;
				}							

				//check to make sure cross-ref doesn't already exist				
				List<EmailAddressXRefMessageInfo> result = new List<EmailAddressXRefMessageInfo>();
				result = AIMManager.GetEmailMessageXrefMessage((Int32)lueCFCUsers.EditValue, (Int32)cmbMessage.SelectedIndex);
				if (result != null && result.Count != 0)
				{
					MessageBox.Show("This cross-reference already exists.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				EmailAddressXRefMessageInfo info = new EmailAddressXRefMessageInfo();
				info.CFCUserID = (Int32)lueCFCUsers.EditValue;
				info.MessageID = (Int32)cmbMessage.SelectedIndex;
				info.UpdateCFCUserID = userGlobal.CFCLoginUser.CFCUserID;		
				
				//save cross-ref
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveEmailAddressXRefMessage(ref info);
				if (error.ErrorNumber > 0)					
				{
					return;
				}

				lueCFCUsers.EditValue = 0;
				cmbMessage.SelectedIndex = 0;				
				DataBind();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				EmailAddressXRefMessageInfo info = new EmailAddressXRefMessageInfo();
				info = (EmailAddressXRefMessageInfo)grdEmailMessageXRefView.GetFocusedRow();
				info.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;				
				AIMManager.DeleteEmailAddressXRefMessage(info.EmailAddressXRefMessageID);				
				DataBind();
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
				DataBindlue();
				DataBind();
				SetToolTips();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void grdEmailMessageXRefView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{

			if (e.Column.Caption == "Message Description")
			{
				Int32 val = (Int32)grdEmailMessageXRefView.GetRowCellValue(e.RowHandle, colMessageID);
				e.DisplayText = Enum.GetName(typeof(EmailMessageEnum), val);
			}
		}

		private void lueCFCUsers_Click(object sender, EventArgs e)
		{
			lueCFCUsers.SelectAll();
		}

		private void lueCFCUsers_Enter(object sender, EventArgs e)
		{
			lueCFCUsers.SelectAll();
		}

		private void cmbMessage_Enter(object sender, EventArgs e)
		{
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void EmailAddressXRefMessage_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void cmbMessage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
