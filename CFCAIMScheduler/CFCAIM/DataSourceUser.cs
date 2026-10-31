using CFCAIM.CrossReference;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace CFCAIM
{
	public partial class DataSourceUser : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();		
		List<DataSourceTypeInfo> _dataSourceType = new List<DataSourceTypeInfo>();
        DataSourceCrossReference dataSourceCrossReference = new DataSourceCrossReference();             //12/5/19
        
        public DataSourceUser()
		{
			InitializeComponent();
		}

		private void DataSourceUser_Load(object sender, EventArgs e)
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

		private void DataBind()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdDataSourceUserView.FocusedRowHandle.ToString(), out rowNum);			
			grdDataSourceUser.DataSource = AIMManager.GetDataSourceType(true);
			grdDataSourceUserView.FocusedRowHandle = rowNum;	 
			_dataSourceType = AIMManager.GetDataSourceType(true);
			
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
				ClearFields();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void ClearFields()
		{
			txtName.Text = String.Empty;
			txtCode.Text = String.Empty;
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

				if (txtCode.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Code. ", "Warning...", MessageBoxButtons.OK);
					txtCode.Focus();
					return;
				}

				DataSourceTypeInfo name = new DataSourceTypeInfo();
				name = AIMManager.GetDataSourceUserName(txtName.Text);
				if (name != null && name.DataSourceTypeID != 0)
				{
					MessageBox.Show("This name already exists. Please enter a difference one.", "Warning...", MessageBoxButtons.OK);
					txtName.Text = "";
					txtCode.Text = "";
					txtName.Focus();
					return;
				}
				if (txtName.Text.Length > 50)
				{
					MessageBox.Show("Name may not be more than 50 characters. ", "Warning...", MessageBoxButtons.OK);
					txtName.Focus();
					return;
				}


				DataSourceTypeInfo dataSource = new DataSourceTypeInfo();
				dataSource.DataSourceTypeID = 0;
				dataSource.Name = txtName.Text;
				dataSource.DataSourceTypeCode = txtCode.Text;
				dataSource.IsActive = true;
				dataSource.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				dataSource.IsFurtherAnalyses = false;			   
				dataSource.IsSQLDatabase = false;
				dataSource.IsUserDefined = true;				

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveDataSourceUser(ref dataSource);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtName.Text = String.Empty;
				txtCode.Text = String.Empty;
				DataBind();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void grdDataSourceUserView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			DataSourceTypeInfo dataSource = new DataSourceTypeInfo();
			dataSource = (DataSourceTypeInfo)grdDataSourceUserView.GetFocusedRow();

			if (dataSource.IsUserDefined == false)
			{
				MessageBox.Show("Making changes to a System data source is not allowed. Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				DataBind();
				return;
			}
			else
			{
				dataSource.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveDataSourceUser(ref dataSource);
				DataBind();
			}
			
		}

		private void btnPreviewGridResults_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdDataSourceUser, "Data Sources");
				CFCAIMCore.Global.previewGridResults(grdDataSourceUser, "Data Sources");
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

				DataSourceTypeInfo dataSource = new DataSourceTypeInfo();
				dataSource = (DataSourceTypeInfo)grdDataSourceUserView.GetFocusedRow();

				if (dataSource.IsUserDefined == false)
				{
					MessageBox.Show("This is a System data source. Cannot Delete.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
				def = AIMManager.GetDataSourceUserID(dataSource.DataSourceTypeID);
				if (def != null)
				{
					MessageBox.Show("This data source is currently in use on an import map. Cannot Delete.", "Warning...", MessageBoxButtons.OK);
					txtName.Focus();
					return;
				}

				//delete any file type xrefs
				AIMManager.DeleteDataSourceTypeRefFileTypeUsingDataSourceTypeID(dataSource.DataSourceTypeID);
				
				//Delete from DataSourceType table
				dataSource.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteDataSourceUser(dataSource.DataSourceTypeID);

				DataBind();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnRefresh.PerformClick();
		}
				
		private void grdDataSourceUserView_CustomDrawCell(Object sender, RowCellCustomDrawEventArgs e)
		{	  			
			Boolean isUserDefined = false;
			if (e.Column == colName || e.Column == colDataSourceTypeCode || e.Column == colDelete || e.Column == colTypeDisplay)
			{				
				isUserDefined = Convert.ToBoolean(grdDataSourceUserView.GetRowCellValue(e.RowHandle, colIsUserDefined));
				if (isUserDefined == false)
				{
					e.Appearance.BackColor = Color.LightGray;
				}
				else
				{
					e.Appearance.BackColor = Color.White;
				}
			}		 
		}

        private void btnResize_Click(Object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

        private void DataSourceUser_SizeChanged(Object sender, EventArgs e)
        {
            btnResize.Visible = true;
        }

        private void btnDataSourceCrossReference_Click(object sender, EventArgs e)
        {
            Close();
            //AIMGlobal.analyzerDataSourceTypeID = (Int32)_dataSource.DataSourceTypeID;
            //AIMGlobal.analyzerDataSourceDefinitionID = _selectedDefinition.UserDefinedDataSourceDefinitionID;
            AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = false;                                                                      //8/20/18
            CFCAIM.CrossReference.DataSourceCrossReference datasourceCrossReference;
			AIMGlobal.open_form(datasourceCrossReference = new DataSourceCrossReference());

        }
    }
}
