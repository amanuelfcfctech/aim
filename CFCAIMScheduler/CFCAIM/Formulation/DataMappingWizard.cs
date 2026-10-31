using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;

namespace CFCAIM.Maintenance
{
	public partial class DataMappingWizard : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();		
		Int32 _currentPage = 0;
		List<AIMDatabaseInfo> _aimDatabase = new List<AIMDatabaseInfo>();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
			

		public DataMappingWizard()
		{
			InitializeComponent();
		}

		private void DataMappingWizard_Load(object sender, EventArgs e)
		{

			tabControl1.TabPages.Remove(tabPage5);
		  
			lblHeading.Text = AIMGlobal.lblWizardCaption;
			if (AIMGlobal.lblWizardCaption == "Select Existing Data Map")
			{
				DataBindlueDatabaseName();				
				_currentPage = 5;
			}
			DataBind();
			ShowHideTabs(_currentPage);
		}

		private void DataBind()
		{

			_aimDatabase = AIMManager.GetAIMDatabase(true);
			_aimDatabase.Insert
				(
					0,
					new AIMDatabaseInfo
					{
						AIMDatabaseID = 0,
						Name = "--Select--"
					}
				);
			lueDatabaseName.Properties.DataSource = _aimDatabase;
			XtraEditors.LoadLookUpEdit(ref lueDatabaseName, "AIMDatabaseID", "Name");
			lueDatabaseNameNew.Properties.DataSource = _aimDatabase;
			XtraEditors.LoadLookUpEdit(ref lueDatabaseNameNew, "AIMDatabaseID", "Name");

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
			lueDataSourceTypeNew.Properties.DataSource = dataSource;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceTypeNew, "DataSourceTypeID", "Name", "TypeDisplay");
			
			
			List<AIMPriorityUsedValueInfo> priorityNew = new List<AIMPriorityUsedValueInfo>();
			priorityNew = AIMManager.GetAIMPriorityUsedValueExcludeDefault();
			luePriorityNew.Properties.DataSource = priorityNew;
			XtraEditors.LoadLookUpEdit(ref luePriorityNew, "AIMPriorityUsedValueID", "Description");
			//ToDo - set priority to second item in list ("Last In") which is item index #1 -- 
			//		  change so it isn't hard-coded
			luePriorityNew.ItemIndex = 1;		

			List<MathTypeInfo> mathType = new List<MathTypeInfo>();
			mathType = AIMManager.GetMathType(true);			
			mathType.Insert
				(
					0,
					new MathTypeInfo
					{
						MathTypeID = 0,
						MathTypeName = "--Select--"
					}
				);
			lueNewDefaultForProposed.Properties.DataSource = mathType;
			XtraEditors.LoadLookUpEdit(ref lueNewDefaultForProposed, "MathTypeID", "MathTypeName");

					
		}

		private void SaveDataMap()
		{

			//10/18/17 - update syncTime on AimDatabase
			String dtTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			//10/23/17
			Int32 mathType = 0;
			Int32 priorityID = 0;
			Int32 datasourcetypeID = 0;
			Int32.TryParse(lueNewDefaultForProposed.EditValue.ToString(), out mathType);
			Int32.TryParse(luePriorityNew.EditValue.ToString(), out priorityID);
			Int32.TryParse(lueDataSourceTypeNew.EditValue.ToString(), out datasourcetypeID);

			AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
			aimDatabase.IsActive = true;
			aimDatabase.Name = txtDatabaseName.Text;
			aimDatabase.CFCPath = txtPath.Text;					   			
			aimDatabase.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			aimDatabase.DefaultForPropose = (MathTypeEnum)mathType;
			aimDatabase.AIMPriorityUsedValueID = priorityID;
			aimDatabase.DataSourceTypeID = datasourcetypeID;

			//10/23/17
			aimDatabase.SyncTime = Convert.ToDateTime(dtTime);

			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveAIMDatabaseReturnID(ref aimDatabase);
			///AIMManager.SaveAIMDatabase(ref aimDatabase);
			
			if (lueDatabaseNameNew.EditValue != null || lueDatabaseNameNew.Text.ToString() != "--Select--")
			{
				//Copy to x-ref tables					
				AIMManager.CopyAIMDatabaseXRef((Int32)aimDatabase.AIMDatabaseID, (Int32)lueDatabaseNameNew.EditValue, aimDatabase.CFCUserID);
			}

			if (error.ErrorNumber > 0)
			{
				return;
			}
			AIMGlobal.dataMapID = error.RecordID;
			


		}
		private void btnNext_Click(object sender, EventArgs e)
		{
			Boolean skipCopyPage = false;

			//------------------------------------------------------------------
			if (_currentPage == 3)  //Copy From
			{
				// if there's nothing in the copy-from dropdown, don't show this page
				if (_aimDatabase.Count == 1)
				{
					_currentPage++;
					skipCopyPage = true;
				}				
			}

			//------------------------------------------------------------------
			if (_currentPage == 4)  //Finish
			{
				if (skipCopyPage == false)
				{
					if (lueDatabaseNameNew.EditValue == null || lueDatabaseNameNew.Text.ToString() == "--Select--")
					{
						DialogResult dr = new DialogResult();
						dr = MessageBox.Show("The 'Copy From' data map is not selected.  Is this correct?", "Warning...", MessageBoxButtons.YesNoCancel);
						if (dr != DialogResult.Yes)
						{
							lueDatabaseNameNew.Focus();
							lueDatabaseNameNew.ShowPopup();
							return;
						}
					}
				}
				SaveDataMap();
				CFCAIM.AIMGlobal.DataMappingTabControlVisible = true;

				//6/9/17 (Task 403) refresh lueDatabase so that lueDataMap gets refreshed
				List<AIMDatabaseInfo> mainMenuAimDatabase = new List<AIMDatabaseInfo>();
				mainMenuAimDatabase = AIMManager.GetAIMDatabase(true);				
				_mainMenu.lueDataMap.Properties.DataSource = mainMenuAimDatabase;
				
				//---------------------------------------------
				AIMGlobal.dataMapForm = true;
				//---------------------------------------------

				Close();
								
			}

			//------------------------------------------------------------------			
			else if (_currentPage == 5)  //Existing
			{
				Int32 defID = 0;
				if (lueDatabaseName.EditValue != null)
				{
					Int32.TryParse(lueDatabaseName.EditValue.ToString(), out defID);
					AIMGlobal.dataMapID = defID;
					CFCAIM.AIMGlobal.DataMappingTabControlVisible = true;
					Close();

				}
			}
			
			else
			{
				_currentPage++;
				ShowHideTabs(_currentPage);
			}

		}
		private void ShowHideTabs(Int32 currentPage)
		{
			tabControl1.ItemSize = new Size(0, 1);
			tabControl1.SelectedIndex = currentPage;
		}
		private void DataBindlueDatabaseName()
		{ 
			List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
			aimDatabase = AIMManager.GetAIMDatabase(true);
			aimDatabase.Insert
				(
					0,
					new AIMDatabaseInfo
					{
						AIMDatabaseID = 0,
						Name = "--Select--"
					}
				);
			lueDatabaseName.Properties.DataSource = aimDatabase;
			XtraEditors.LoadLookUpEdit(ref lueDatabaseName, "AIMDatabaseID", "Name");			
		}

		private void btnCancel_Click(object sender, EventArgs e)
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

		private void btnBack_Click(object sender, EventArgs e)
		{
			try
			{

				//10/23/17
				//if (_currentPage > 0 && _currentPage != 6)
				if (_currentPage > 0 && _currentPage != 5)
				{
					_currentPage--;
				}
				switch (_currentPage)
				{
					case 0:

					//10/23/17
					//case 6:
					case 4:

						if (lueDatabaseName.EditValue != null)
						{
							if ((Int32)lueDatabaseName.EditValue != 0)
							{
								CFCAIM.AIMGlobal.DataMappingTabControlVisible = true;
							}
						}

						Close();
						break;
					default:
						ShowHideTabs(_currentPage);
						break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		private void lueDatabaseName_EditValueChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueDatabaseName.EditValue != 0)
			{
				btnNext.Enabled = true;
				btnNext.PerformClick();
			}
		}

		private void txtDatabaseName_Validated(object sender, EventArgs e)
		{
			try
			{
				AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
				aimDatabase = AIMManager.GetAIMDatabase(txtDatabaseName.Text);

				if (aimDatabase != null)
				{
					MessageBox.Show("This data map name already exists.  Please enter another name.", "Warning...", MessageBoxButtons.OK);
					txtDatabaseName.Text = String.Empty;
					txtDatabaseName.Focus();
				}
				else
				{
					btnNext.Enabled = false;
					if (txtDatabaseName.Text.Length > 0)
					{
						btnNext.Enabled = true;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtDatabaseName_Leave(object sender, EventArgs e)
		{
			
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				fbdSelectFolder.ShowDialog();
				if (fbdSelectFolder.SelectedPath != String.Empty)
				{
					string[] files = System.IO.Directory.GetFiles(fbdSelectFolder.SelectedPath, "*.trm");
					if (files.Length == 0)
					{
						MessageBox.Show("You have selected an invalid folder. Please select another one.");
						return;
					}
				}
				else
				{
					MessageBox.Show("Please select a folder.");
					return;
				}
				txtPath.Text = fbdSelectFolder.SelectedPath;
				txtPath.Text = fbdSelectFolder.SelectedPath;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtPath_Leave(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((string.IsNullOrWhiteSpace(txtPath.Text) == false))
			{
				btnNext.Enabled = true;
			}
		}

		private void lueNewDefaultForProposed_EditValueChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueNewDefaultForProposed.EditValue != 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void lueNewDefaultForProposed_Leave(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueNewDefaultForProposed.EditValue != 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void luePriorityNew_Leave(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)luePriorityNew.EditValue != 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void luePriorityNew_EditValueChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)luePriorityNew.EditValue != 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnCancel.Visible = true;
			btnNext.Text = "Next";

			this.Icon = (Icon)Properties.Resources.CFCAIM;
			ToolTip ttip = new ToolTip();
			ttip.SetToolTip(btnNext, "Advance to next screen");

			Int32 test = _currentPage;

			switch (tabControl1.SelectedIndex)
			{

				//---------------------------
				//Enter New DataMap Name = case 0


				//---------------------------
				//Select Formulation Path 
				case 1:
					btnNext.Enabled = false;
					if (txtPath.Text.Length != 0)
					{
						btnNext.Enabled = true;
					}					
					break;

				//---------------------------
				//Default for Proposed Nutrients
				case 2:
					btnNext.Enabled = false;
					if ((Int32)lueNewDefaultForProposed.EditValue != 0)
					{
						btnNext.Enabled = true;
					}
					lueNewDefaultForProposed.ShowPopup();
					break;

				//---------------------------
				//Priority-Used
				case 3:
					btnNext.Enabled = false;
					if ((Int32)luePriorityNew.EditValue != 0)
					{
						btnNext.Enabled = true;
					}
					luePriorityNew.ShowPopup();
					break;

				//Data Source
				case 4:
					//btnNext.Enabled = false;
					//if ((Int32)lueDataSourceTypeNew.EditValue != 0)
					//{
						btnNext.Enabled = true;
					//}
					//lueDataSourceTypeNew.ShowPopup();
					break;				

				//Select Existing Data Map
				case 5:   
					btnNext.Enabled = true;
					btnNext.Text = "Finish";
					break;


				case 6:
					btnNext.Enabled = true;
					btnCancel.Visible = false;
					lueDatabaseName.ShowPopup();
					break;
				default:
					break;
			}
		}

		private void txtPath_TextChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((string.IsNullOrWhiteSpace(txtPath.Text) == false))
			{
				btnNext.Enabled = true;
			}
		}

		private void lueDataSourceTypeNew_Leave(object sender, EventArgs e)
		{
			
			//btnNext.Enabled = false;
			//if ((Int32)lueDataSourceTypeNew.EditValue != 0)
			//{
			//	btnNext.Enabled = true;
			//}
		}

		private void lueDataSourceTypeNew_EditValueChanged(object sender, EventArgs e)
		{
			
			//btnNext.Enabled = false;
			//if ((Int32)lueDataSourceTypeNew.EditValue != 0)
			//{
			//	btnNext.Enabled = true;
			//}
		}

		private void txtDatabaseName_TextChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if (txtDatabaseName.Text.Length > 0)
			{
				btnNext.Enabled = true;
			}
		}
	}
}
