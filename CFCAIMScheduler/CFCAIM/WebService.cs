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


namespace CFCAIM
{
	public partial class WebService : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		public WebService()
		{
			InitializeComponent();
		}

		private void WebService_Load(object sender, EventArgs e)
		{			
			try
			{
				this.Icon = Properties.Resources.CFCAIM;				
				DataBind();
				DataBindLue();
				SetToolTips();

                //6/14/17
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DataBind()
		{
			List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
			replueDataSourceType.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceType.Columns["TypeDisplay"].Caption = "Type";
			
			Int32 rowNum = 0;
			Int32.TryParse(grdWebServiceView.FocusedRowHandle.ToString(), out rowNum);
			grdWebService.DataSource = AIMManager.GetAllWebService();
			
			grdWebServiceView.FocusedRowHandle = rowNum;
		}

		private void DataBindLue()
		{
			List<DataSourceTypeInfo> dataSource = new List<DataSourceTypeInfo>();
			dataSource = AIMManager.GetDataSourceType(true);
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
			XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");
			lueDataSourceType.Properties.Columns["TypeDisplay"].Caption = "Type";

			

		}



		private void repbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				WebServiceInfo webService = new WebServiceInfo();
				webService = (WebServiceInfo)grdWebServiceView.GetFocusedRow();
				webService.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteWebService(webService.WebServiceID);
				DataBind();
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
				this.Icon = Properties.Resources.CFCAIM;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
				ttip.SetToolTip(btnAddWebService, "Add Record");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddWebService_Click(object sender, EventArgs e)
		{
			try
			{
				WebServiceInfo webService = new WebServiceInfo();


				if (txtWebAddress.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Web Address.", "Warning...", MessageBoxButtons.OK);
					txtWebAddress.Focus();
					return;
				}
				if (txtToken.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Token.", "Warning...", MessageBoxButtons.OK);
					txtToken.Focus();
					return;
				}
				if ((lueDataSourceType.EditValue == null || lueDataSourceType.Text.ToString() == "--Select--")  )
				{
					MessageBox.Show("Please select a Data Source.", "Warning...", MessageBoxButtons.OK);
					lueDataSourceType.ShowPopup();
					return;
				}

				webService.Name = txtName.Text;
				webService.WebAddress = txtWebAddress.Text;
				webService.Token = txtToken.Text;
				webService.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				webService.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;				

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveWebService(ref webService);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				txtName.Text = String.Empty;
				txtWebAddress.Text = String.Empty;
				txtToken.Text = String.Empty;
				DataBind();
				DataBindLue();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void grdWebServiceView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{			
			WebServiceInfo webService = new WebServiceInfo();
			webService = (WebServiceInfo)grdWebServiceView.GetFocusedRow();
			webService.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveWebService(ref webService);
			DataBind();
		}

		private void btnPrintPreview_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdWebService, "Web Service");
				CFCAIMCore.Global.previewGridResults(grdWebService, "Web Service");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueDataSourceType_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
		{
		
		}

		private void lueDataSourceType_Enter(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

		private void lueDataSourceType_Click(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {
            //6/14/17
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void WebService_SizeChanged(Object sender, EventArgs e)
        {
            //6/14/17
            btnResize.Visible = true;
        }
    }
}
