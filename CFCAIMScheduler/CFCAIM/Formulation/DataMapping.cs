using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraGrid.Views.Base;
using CFCAIMCore;

namespace CFCAIM.Maintenance
{
	using System.Diagnostics;
	using System.Threading;

	public partial class DataMapping : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		
		private Boolean _IsLoading = false;
		private String _HoldCFCPath = String.Empty;
		private Int32 _progressBarID = 0;
		String _txtExistingPath = "";
		DataMappingWizard wizard;
				
		Int32 _selectedDataMapID = 0;

		HelpSync helpSync;
			

		public DataMapping()
		{
			InitializeComponent();
		}
		private void DatabaseSetup_Load(object sender, EventArgs e)
		{
			try
			{

				DataBind();				
				DataBindlueDatabaseName();

				if (CFCAIMCore.Global.aimDatabase != null)
                {					
					lueConnectionName.EditValue = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
                }
				else
				{
					lueConnectionName.EditValue = 0;
				}
				txtEditName.Text = lueConnectionName.Text.ToString();
				lueConnectionName.Properties.ShowHeader = false;
				ClearTextFields();
				SetToolTips();
				AIMGlobal.formulationCrossReference = "";


				//if Sync is running, show progress bar and disable Sync button				
				Process[] pname;
				String processName = "CFC5AIM";
				pname = Process.GetProcessesByName(processName);
				if (pname.Length != 0)
				{
					List<ProgressBarInfo> result = new List<ProgressBarInfo>();
					result = AIMManager.GetProgressBarValueByCFCUserID(userGlobal.CFCLoginUser.CFCUserID);
					if (result != null)
					{
						pbAimSync.Visible = true;
						txtCounter.Visible = true;
						bgwTimer.RunWorkerAsync();
						btnSync.Text = "Syncing";
						btnSync.Enabled = false;
					}
				}

                // 3/12/18 - Check UserAccess Permission                
                txtExistingPath.ReadOnly = false;
                btnBrowseFormulationPath.Enabled = true;
                lueDefaultForProposed.ReadOnly = false;
                luePriority.ReadOnly = false;
                lueDataSourceType.ReadOnly = false;
                Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(userGlobal.CFCLoginUser.CFCUserID, (Int32)EnumPermissionTable.FormulationOptions);
                if (rtn == -1)
                {
                    txtExistingPath.ReadOnly = true;
                    btnBrowseFormulationPath.Enabled = false;
                    lueDefaultForProposed.ReadOnly = true;
                    luePriority.ReadOnly = true;
                    lueDataSourceType.ReadOnly = true;
                }
				
				DataBindOverlay();          //11/30/23		


				WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void SetToolTips()
		{
			this.Icon = (Icon)Properties.Resources.CFCAIM;
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}			
			ttip.SetToolTip(btnDeleteTemplete, "Delete Data Map");
			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
			ttip.SetToolTip(btnShowArchive, "Show Archived Products in Grid");          //6/6/24 						
			ttip.SetToolTip(btnAddIngredient, "Add Record");
			ttip.SetToolTip(btnAddNutrient, "Add Record");
			ttip.SetToolTip(btnAddPlant, "Add Record");
			ttip.SetToolTip(btnAddProduct, "Add Record");
			ttip.SetToolTip(btnPreviewIngredient, "Preview Grid");
			ttip.SetToolTip(btnPreviewNutrient, "Preview Grid");
			ttip.SetToolTip(btnPreviewPlant, "Preview Grid");
			ttip.SetToolTip(btnPreviewProduct, "Preview Grid");


		}
		private void DataBind()
		{
			List<MathTypeInfo> mathType = new List<MathTypeInfo>();		
			_IsLoading = true;
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

			lueDataSourceType.Visible = false;
			lueDataSourceType.Visible = false;

			List<AIMPriorityUsedValueInfo> priority = new List<AIMPriorityUsedValueInfo>();
			priority = AIMManager.GetAIMPriorityUsedValueExcludeDefault();
			luePriority.Properties.DataSource = priority;
			XtraEditors.LoadLookUpEdit(ref luePriority, "AIMPriorityUsedValueID", "Description");

			List<AIMPriorityUsedValueInfo> priorityNew = new List<AIMPriorityUsedValueInfo>();
			priorityNew = AIMManager.GetAIMPriorityUsedValueExcludeDefault();
						
			//////ToDo? - set priority to second item in list ("Last In") which is item index #1 -- 
			//////		  change so it isn't hard-coded
			////luePriorityNew.ItemIndex = 1;
					

			lueDefaultForProposed.Properties.DataSource = mathType;
			XtraEditors.LoadLookUpEdit(ref lueDefaultForProposed, "MathTypeID", "MathTypeName");
						
			DataBindlueDatabaseName();

			txtExistingPath.ResetText();
			chkIsActive.Checked = false;
			pnlCrossReferenceButton.Enabled = false;
			_IsLoading = false;
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

			lueConnectionName.Properties.DataSource = aimDatabase;
			XtraEditors.LoadLookUpEdit(ref lueConnectionName, "AIMDatabaseID", "Name");

			//7/27/22
			if (aimDatabase.Count == 2)
			{
				lueConnectionName.EditValue = (Int32)aimDatabase[1].AIMDatabaseID;
				btnSelectExisting.PerformClick();
			}


		}

		private void btnDeleteTemplete_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dr = new DialogResult();
				dr = MessageBox.Show("Do you want to delete this data map?", "Warning...", MessageBoxButtons.YesNoCancel);
				if (dr != DialogResult.Yes)
				{
					return;
				}
				chkIsActive.Checked = false;

				//Call stp to delete 4xref tables, AIMdatabase and AIMConfig			
				DeleteAimDatabase();
				DataBind();				
				DataBindlueDatabaseName();

				txtEditName.Text = "";				
				AIMManager.DeleteAIMConfig();
											
				//after deleting datamap, refresh lueDataMap with "--Select--"				
				List<AIMDatabaseInfo> mainMenuAimDatabase = new List<AIMDatabaseInfo>();
				mainMenuAimDatabase = AIMManager.GetAIMDatabase(true);
				mainMenuAimDatabase.Insert
					(
						0,
						new AIMDatabaseInfo
						{
							AIMDatabaseID = 0,
							Name = "--Select--"
						}
					);
				_mainMenu.lueDataMap.Properties.DataSource = mainMenuAimDatabase;				
				_mainMenu.lueDataMap.EditValue = 0;
				

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{				
		}
		private void ImportData()
		{

			//Call sync program --> sync is vb6 program; send 6 parameters (customer dbase, SQL connection, new aimDatabaseID, "L" for load, userID, ProgressBarID)			

			ProgressBarInfo progressBar = new ProgressBarInfo();
			AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
			String arg = String.Empty;
			String separator = "~~";
            String syncType = "L";
                        
            String sqlConn = String.Empty;
			String filePath = Application.StartupPath + @"\\CFC5AIM.exe";
			Int32 progressBarID = 0;


			// Insert record into ProgressBar table
			progressBar.ProgressBarID = 0;
			progressBar.ProgressBarValue = 1;
			progressBar.ProgressBarDescription = "Sync Program Running";
			progressBar.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

            //5/8/18
            progressBar.ProgressBarType = (Int32)EnumProgressBarType.SyncConcept5;

            progressBarID = AIMManager.InsertProgressBar(ref progressBar).RecordID;                       

            //9/18/17
            //sqlConn = "Provider=SQLNCLI11;" + CFCAIMCore.Data.AIMDataAccess._database.ConnectionString;
            sqlConn = "Provider=SQLOLEDB;" + CFCAIMCore.Data.AIMDataAccess._database.ConnectionString;

			sqlConn = sqlConn.Replace("Initial Catalog", "Database");
			aimDatabase = AIMManager.GetAIMDatabase(lueConnectionName.Text);

			arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}", separator, txtExistingPath.Text, sqlConn, aimDatabase.AIMDatabaseID, syncType, aimDatabase.CFCUserID.ToString(), progressBarID);
            //////arg = String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}", separator, txtExistingPath.Text, sqlConn, aimDatabase.AIMDatabaseID, syncType, aimDatabase.CFCUserID.ToString(), progressBarID);

            if (File.Exists(filePath))
			{
				ProcessStartInfo proc = new ProcessStartInfo();
				proc.WindowStyle = ProcessWindowStyle.Hidden;
				proc.CreateNoWindow = true;
				proc.UseShellExecute = false;
				proc.FileName = filePath;
				proc.Arguments = arg;
				Process.Start(proc);
			}
			else
			{
				MessageBox.Show("CFCLoad is missing. Contact IT.", "Error...", MessageBoxButtons.OK);
			}

			// Delete record from ProgressBar table			
			AIMManager.DeleteProgressBar(progressBarID);

		}
		private void tabDatabase_Click(object sender, EventArgs e)
		{
			try
			{						
			    lueConnectionName.Focus();			
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
		private void btnExitingBrowse_Click(object sender, EventArgs e)
		{
			try
			{				
				if (lueConnectionName.EditValue.ToString() == "0")
				{
					MessageBox.Show("Please select a data map name.", "Warning...", MessageBoxButtons.OK);
                    return;
				}
                               

                //--------------------------------------------------------------------------------------
                fbdSelectFolder.ShowDialog();
				if (fbdSelectFolder.SelectedPath != String.Empty)
				{
					String[] filePath = Directory.GetFiles(@fbdSelectFolder.SelectedPath, "*.trm");
					if (filePath.Length == 0)
					{
						MessageBox.Show("You have selected an invalid folder. Please select another one.", "Warning...", MessageBoxButtons.OK);
					}
					else
					{

                        //--------------------------------------------------------------------------------------
                        //10/1/19 - added when Vitapro pointed the sync to another database and, as a result, lost cross-references
                        if (txtExistingPath.Text.Length > 0 && (txtExistingPath.Text != fbdSelectFolder.SelectedPath))
                        {
                            DialogResult dr = new DialogResult();
                            dr = MessageBox.Show("Changing the Formulation path may cause DATA LOSS.  Do you want to continue?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                            if (dr != DialogResult.Yes)
                            {
                                return;
                            }
                        }


                        txtExistingPath.Text = fbdSelectFolder.SelectedPath;
						SaveAimDatabase();
					}
				}
				else
				{
					MessageBox.Show("Please select a folder.", "Warning...", MessageBoxButtons.OK);
				}
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void txtDatabaseName_Validated(object sender, EventArgs e)
		{
			////try
			////{
			////	AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
			////	aimDatabase = AIMManager.GetAIMDatabase(txtDatabaseName.Text);

			////	if (aimDatabase != null)
			////	{
			////		MessageBox.Show("This data map name already exists.  Please pick another name.", "Warning...", MessageBoxButtons.OK);
			////		txtDatabaseName.Text = String.Empty;
			////		txtDatabaseName.Focus();
			////	}
			////}
			////catch (Exception ex)
			////{
			////	MessageBox.Show(ex.Message);
			////}
		}
		private void lueDatabaseName_EditValueChanged(object sender, EventArgs e)
		{
			Int32 aimDatabaseID = 0;
			AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
			Int32.TryParse(lueConnectionName.EditValue.ToString(), out aimDatabaseID);
			if (aimDatabase != null)
			{
				refreshTemplateData(aimDatabaseID);
			}

			DataBindIngredient();
			DataBindNutrient();
			DataBindPlant();
			DataBindProduct();


		}
		private void refreshTemplateData(Int32 aimDatabaseID)
		{
			AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
			aimDatabase = AIMManager.GetAIMDatabase(aimDatabaseID);

			try
			{
				if (aimDatabase != null)
				{
					_IsLoading = true;
					chkIsActive.Checked = aimDatabase.IsActive;
					txtExistingPath.Text = aimDatabase.CFCPath;
					lueDefaultForProposed.EditValue = (Int32)aimDatabase.DefaultForPropose;
					luePriority.EditValue = (Int32)aimDatabase.AIMPriorityUsedValueID;
					lueDataSourceType.EditValue = (Int32)aimDatabase.DataSourceTypeID;

					if ((Int32)luePriority.EditValue == (Int32)PriorityUsedValue.DataSource)
					{
						lblDataSourceType.Visible = true;
						lueDataSourceType.Visible = true;
					}
					else
					{
						lblDataSourceType.Visible = false;
						lueDataSourceType.Visible = false;
					}

					pnlCrossReferenceButton.Enabled = true;					
					aimDatabase.AIMDatabaseID = aimDatabaseID;

					_mainMenu.SetLueDataMapEditValue(aimDatabase.AIMDatabaseID);
					ShowSyncProgress();
					_IsLoading = false;
					txtEditName.Text = lueConnectionName.Text.ToString();

				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void chkIsActive_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				SaveAimDatabase();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void ShowSyncProgress()
		{
			try
			{
				AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
				aimDatabase = (AIMDatabaseInfo)lueConnectionName.GetSelectedDataRow();

				if (aimDatabase != null)
				{
					aimDatabase = AIMManager.GetAIMDatabase(aimDatabase.AIMDatabaseID);
					if (aimDatabase != null)
					{
						if (aimDatabase.Name != "--Select--")
						{
							lblSyncTime.Text = "Last Sync with AIM on " + aimDatabase.SyncTime;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void SaveAimDatabase()
		{
			try
			{
				AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
				if (_IsLoading == false)
				{
					try
					{

                        //3/21/18
                        if (txtExistingPath.Text == String.Empty || txtExistingPath.Text == "")
                        {
                            MessageBox.Show("Please select a Formulation Path.", "Warning...", MessageBoxButtons.OK);
                            return;
                        }

						if (lueDefaultForProposed.EditValue == null || (Int32)lueDefaultForProposed.EditValue == 0)
						{
                            //3/21/18
                            //MessageBox.Show("Please select a Proposed Nutrient Calculation Method.", "Warning...", MessageBoxButtons.OK);                            
                            lueDefaultForProposed.EditValue = (Int32)MathTypeEnum.Mean;

                            return;
						}

                        //3/21/18
                        if (luePriority.EditValue == null || (Int32)luePriority.EditValue == 0)
                        {
                            MessageBox.Show("Please select a Priority Used Value.", "Warning...", MessageBoxButtons.OK);
                            return;
                        }


                        //Update syncTime on AimDatabase
                        String dtTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                        
                        Int32 mathType = 0;                     
                        Int32 priorityID = 1;
						Int32 datasourcetypeID = 0;
						Int32.TryParse(lueDefaultForProposed.EditValue.ToString(), out mathType);
						Int32.TryParse(luePriority.EditValue.ToString(), out priorityID);
						Int32.TryParse(lueDataSourceType.EditValue.ToString(), out datasourcetypeID);

						aimDatabase = (AIMDatabaseInfo)lueConnectionName.GetSelectedDataRow();  // aimDatabases[lueDatabaseName.ItemIndex];			

						//7/27/22 - added check for null
						if (aimDatabase != null)
						{

							aimDatabase.CFCPath = txtExistingPath.Text.ToString();
							aimDatabase.IsActive = chkIsActive.Checked;
							aimDatabase.DefaultForPropose = (MathTypeEnum)mathType;
							aimDatabase.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							aimDatabase.Name = this.txtEditName.Text.ToString();
							aimDatabase.AIMPriorityUsedValueID = priorityID;
							aimDatabase.DataSourceTypeID = datasourcetypeID;
							aimDatabase.SyncTime = Convert.ToDateTime(dtTime);

							AIMManager.SaveAIMDatabase(ref aimDatabase);
						}	
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DeleteAimDatabase()
		{
			AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
			if (_IsLoading == false)
			{
				aimDatabase = (AIMDatabaseInfo)lueConnectionName.GetSelectedDataRow();// aimDatabases[lueDatabaseName.ItemIndex];                        
				AIMManager.DeleteAIMDatabase(ref aimDatabase);
			}
		}
		private void lueDefaultForProposed_Leave(object sender, EventArgs e)
		{
			try
			{

				if (lueConnectionName.EditValue.ToString() == "0")
				{
					MessageBox.Show("Please select a data map name.", "Warning...", MessageBoxButtons.OK);
				}
				else
				{					
					AIMCoreGlobal.DefaultForPropose = (MathTypeEnum)lueDefaultForProposed.EditValue;
					SaveAimDatabase();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DataMapping_Activated(object sender, EventArgs e)
		{
			try
			{
				ShowSyncProgress();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void txtDatabaseName_TextChanged(object sender, EventArgs e)
		{

		}
		private void label1_Click(object sender, EventArgs e)
		{

		}
		private void txtEditName_Validated(object sender, EventArgs e)
		{
			Int32 aimdatabaseID = 0;
			try
			{
				if ((txtEditName.Text != lueConnectionName.Text))
				{

					//get current aim database id							
					aimdatabaseID = (Int32)lueConnectionName.EditValue;

					//check to see if name already exists (use new name to search aim database table)
					AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
					aimDatabase = AIMManager.GetAIMDatabase(txtEditName.Text);
					if (aimDatabase != null)
					{
						MessageBox.Show("This data map name already exists. ", "Warning...", MessageBoxButtons.OK);
						txtEditName.Text = lueConnectionName.Text;
					}
					else
					{
						SaveAimDatabase();

						refreshTemplateData(aimdatabaseID);
						DataBind();						
						DataBindlueDatabaseName();
						lueConnectionName.EditValue = aimdatabaseID;

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void luePriorityNew_EditValueChanged(object sender, EventArgs e)
		{
		////	if ((Int32)luePriorityNew.EditValue == (Int32)PriorityUsedValue.DataSource)
		////	{
		////		lblDataSourceTypeNew.Visible = true;
		////		lueDataSourceTypeNew.Visible = true;
		////	}
		////	else
		////	{
		////		lueDataSourceTypeNew.EditValue = 0;
		////		lblDataSourceTypeNew.Visible = false;
		////		lueDataSourceTypeNew.Visible = false;
		////	}
		}
		private void luePriority_Leave(object sender, EventArgs e)
		{
			try
			{
				if (luePriority.EditValue.ToString() == "0")
				{
					MessageBox.Show("Please select an AIM Priority Used Value.", "Warning...", MessageBoxButtons.OK);
					luePriority.Focus();
					luePriority.ShowPopup();
					return;
				}
				else if ((Int32)luePriority.EditValue == (Int32)PriorityUsedValue.DataSource && (Int32)lueDataSourceType.EditValue == 0)
				{
					MessageBox.Show("Please select a DataSourceType.", "Warning...", MessageBoxButtons.OK);
					lueDataSourceType.ShowPopup();
					return;
				}
				else if ((Int32)luePriority.EditValue != (Int32)PriorityUsedValue.DataSource)
				{
					lueDataSourceType.EditValue = 0;
				}
				SaveAimDatabase();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void lueDataSourceType_Leave(object sender, EventArgs e)
		{
			if ((Int32)luePriority.EditValue != (Int32)PriorityUsedValue.DataSource)
			{
				lueDataSourceType.EditValue = 0;
			}
			else if ((Int32)luePriority.EditValue == (Int32)PriorityUsedValue.DataSource && (Int32)lueDataSourceType.EditValue == 0)
			{
				MessageBox.Show("Please select a DataSource Type.", "Warning...", MessageBoxButtons.OK);
				lueDataSourceType.Focus();
				lueDataSourceType.ShowPopup();
				return;
			}
			SaveAimDatabase();
		}
		private void luePriority_EditValueChanged(object sender, EventArgs e)
		{
			if ((Int32)luePriority.EditValue == (Int32)PriorityUsedValue.DataSource)
			{
				lblDataSourceType.Visible = true;
				lueDataSourceType.Visible = true;
			}
			else
			{
				lueDataSourceType.EditValue = 0;
				lblDataSourceType.Visible = false;
				lueDataSourceType.Visible = false;
			}
		}

		private void btnSync_Click(object sender, EventArgs e)
		{
			try
			{
				
				Process[] pname;
				String processName = "CFC5AIM";
				_txtExistingPath = txtExistingPath.Text;


                //3/16/18 - make sure folder exists
                if (!Directory.Exists(_txtExistingPath))
                {
                    MessageBox.Show("The Formulation path (" + _txtExistingPath + ") does not exist. Unable to sync database.", "Warning...", MessageBoxButtons.OK);
                    return;
                }

				//3/9/23 - make sure path includes a valid "trm" file   				
				string[] filePaths = Directory.GetFiles(@_txtExistingPath, "P5INGMST.TRM");
				if (filePaths.Length == 0)
				{
					MessageBox.Show("The Formulation path (" + _txtExistingPath + ") does not contain valid Concept5 TRM files. Check the path and try again.", "Warning...", MessageBoxButtons.OK);
					return;
				}
				

				//pname.length will be 0 until proc (CFC5AIM.exe) is started
				pname = Process.GetProcessesByName(processName);
                while (pname.Length != 0)
                {
                    MessageBox.Show("The sync program is currently running.", "Information...", MessageBoxButtons.OK);
                    return;
                }

                if (rbFull.Checked == true)
                {
                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show("The Full Sync may take several hours. Continue with full sync?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }

                //Check to see if this is an Initial Load
                AIMGlobal.initialCFCLoad = 0;
                ErrorInfo cfcOverLay = AIMManager.GetCFCIngredientNutrientOverlayCount();
                if (cfcOverLay.RecordID == 0)
                {
                    // this is the initial load
                    AIMGlobal.initialCFCLoad = 1;
                }
                
                //this background worker runs the vb6 sync program
                if (!this.backgroundSync.IsBusy)
                {
                    backgroundSync.RunWorkerAsync();
                }

                
                //this background worker is for the progress bar on the screen
                pbAimSync.Visible = true;
				txtCounter.Visible = true;
				bgwTimer.RunWorkerAsync();
				btnSync.Text = "Syncing";
				btnSync.Enabled = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void backgroundSync_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
           
            AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
			String syncType = "";

            try
            {
				//9/9/20 - change to use AIMCoreGlobal
				//CFCAIM.AIMGlobal.importProgressBarCounter = 0;
				CFCAIMCore.Global.importProgressBarCounter = 0;

                // Insert record into ProgressBar SQL table
                ProgressBarInfo progressBar = new ProgressBarInfo();
                progressBar.ProgressBarID = 0;
                progressBar.ProgressBarValue = 1;
                progressBar.ProgressBarDescription = "Sync Program Running";
                progressBar.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;                
                progressBar.ProgressBarType = (Int32)EnumProgressBarType.SyncConcept5;
                _progressBarID = AIMManager.InsertProgressBar(ref progressBar).RecordID;
                aimDatabase = (AIMDatabaseInfo)lueConnectionName.GetSelectedDataRow();

                syncType = "L";
                if (rbQuick.Checked == true)
                {
                    syncType = "Q";
                }

                AIMGlobal.SyncDatabase(_txtExistingPath, aimDatabase.AIMDatabaseID, _progressBarID, syncType);
            }
            catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void backgroundSync_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			try
			{
				if (pbAimSync.Value != pbAimSync.Maximum)
				{
					pbAimSync.Value++;

					//9/9/20 - change to use AIMCoreGlobal
					//txtCounter.Text = Convert.ToString(CFCAIM.AIMGlobal.importProgressBarCounter++);
					txtCounter.Text = Convert.ToString(CFCAIMCore.Global.importProgressBarCounter++);
                }
				else
				{
					pbAimSync.Value = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
        private void backgroundSync_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            backgroundSync.Dispose();

			//9/9/20 - change to use AIMCoreGlobal
			//CFCAIM.AIMGlobal.importProgressBarCounter = 0;
			CFCAIMCore.Global.importProgressBarCounter = 0;

            //Update syncTime on AimDatabase
            String dtTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
            aimDatabase = (AIMDatabaseInfo)lueConnectionName.GetSelectedDataRow();

            AIMDatabaseInfo updAIMDatabase = new AIMDatabaseInfo();
            updAIMDatabase.AIMDatabaseID = aimDatabase.AIMDatabaseID;
            updAIMDatabase.Name = aimDatabase.Name;
            updAIMDatabase.CFCPath = aimDatabase.CFCPath;
            updAIMDatabase.DefaultForPropose = aimDatabase.DefaultForPropose;
            updAIMDatabase.IsActive = aimDatabase.IsActive;
            updAIMDatabase.DataSourceTypeID = aimDatabase.DataSourceTypeID;
            updAIMDatabase.SyncTime = Convert.ToDateTime(dtTime);
            updAIMDatabase.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

            //3/27/18
            updAIMDatabase.AIMPriorityUsedValueID = aimDatabase.AIMPriorityUsedValueID;

            AIMManager.SaveAIMDatabase(ref updAIMDatabase);

            
        }
        private void bgwTimer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			try
			{
				Int32 count = 1;
				Process[] pname;
				String processName = "CFC5AIM";

				//9/9/20 - change to use AIMCoreGlobal
				//CFCAIM.AIMGlobal.importProgressBarCounter = 0;
				CFCAIMCore.Global.importProgressBarCounter = 0;

                //pname.length will be 0 until proc (CFC5AIM.exe) is started
                pname = Process.GetProcessesByName(processName);
				while (pname.Length == 0)
				{
					pname = Process.GetProcessesByName(processName);
				}
				while (pname.Length != 0)
				{
					count++;
					bgwTimer.ReportProgress(count);
					Thread.Sleep(2000);
					pname = Process.GetProcessesByName(processName);
				}

				bgwTimer.Dispose();

				// Delete record from SQL ProgressBar table			
				AIMManager.DeleteProgressBar(_progressBarID);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void bgwTimer_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			ProgressBarInfo progressBar = new ProgressBarInfo();

			try
			{
				if (pbAimSync.Value != pbAimSync.Maximum)
				{
					progressBar = AIMManager.GetProgressBarValue(_progressBarID);                    
                    if (progressBar != null)
                    {
                        txtCounter.Text = progressBar.ProgressBarDescription;
                        pbAimSync.Value = progressBar.ProgressBarValue;
                    }
				}
				else
				{
					pbAimSync.Value = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void bgwTimer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			bgwTimer.Dispose();
			pbAimSync.Visible = false;
			txtCounter.Visible = false;
			btnSync.Enabled = true;
			btnSync.Text = "Sync Now";

			//9/9/20 - change to use AIMCoreGlobal
			//CFCAIM.AIMGlobal.importProgressBarCounter = 0;
			CFCAIMCore.Global.importProgressBarCounter = 0;

            MessageBox.Show("Sync is complete.");
		}

		private void btnSelectExisting_Click(object sender, EventArgs e)
		{
			try
			{

                //If only one connection exists, populate template and skip going into the wizard
                List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
				aimDatabase = AIMManager.GetAIMDatabase(true);
				if (aimDatabase.Count == 1)
				{					
					_selectedDataMapID = aimDatabase[0].AIMDatabaseID;
					

					CFCAIM.AIMGlobal.DataMappingTabControlVisible = true;
					lueConnectionName.EditValue = aimDatabase[0].Name;
				}
				else
				{	  
					CFCAIM.AIMGlobal.DataMappingTabControlVisible = false;
					AIMGlobal.lblWizardCaption = "Select Existing Data Map";

                    wizard = new DataMappingWizard();

					//10/6/21 - changed for resize problem
					//this.Hide();
					//wizard.WindowState = FormWindowState.Normal;						
					//wizard.ShowDialog();
					//this.Show();                   
					wizard.ShowDialog();

					_selectedDataMapID = CFCAIM.AIMGlobal.dataMapID;
					
				}

				//TODO - had to add (object) below; otherwise was getting 'object not set to instance ...' error
				/////lueDatabaseName.EditValue = (object)_selectedDataMapID;

				panelMain.Visible = false;
				if (CFCAIM.AIMGlobal.DataMappingTabControlVisible == true)
				{
					panelMain.Visible = true;
                    tabControl2.Visible = true;					
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}

		}

		private void btnAddNew_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIM.AIMGlobal.DataMappingTabControlVisible = false;				
				AIMGlobal.lblWizardCaption = "Add New Data Map";
				wizard = new DataMappingWizard();

				//10/6/21 - changed for resize problem
				//this.Hide();
				//wizard.WindowState = FormWindowState.Normal;                        
				//wizard.ShowDialog();
				//this.Show();
				wizard.ShowDialog();

				_selectedDataMapID = CFCAIM.AIMGlobal.dataMapID;
				lueConnectionName.EditValue = _selectedDataMapID;				

				panelMain.Visible = false;
				if (CFCAIM.AIMGlobal.DataMappingTabControlVisible == true)
				{
					panelMain.Visible = true;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}
	

		private void btnBrowseFormulationPath_Click(object sender, EventArgs e)
		{
			try
			{

				DialogResult dr = new DialogResult();
				dr = MessageBox.Show("Changing the formalation path may destroy existing cross-references.  Do you really want to do this?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if (dr != DialogResult.Yes)
				{
					return;
				}
								
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
					////MessageBox.Show("Please select a folder.");
					return;
				}
				txtExistingPath.Text = fbdSelectFolder.SelectedPath;
				txtExistingPath.Text = fbdSelectFolder.SelectedPath;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPlant_Click(object sender, EventArgs e)
		{
			try
			{

				AIMGlobal.formulationCrossReference = "Plant";
				FormulationCrossReference formulationCrossReference;
				AIMGlobal.open_form(formulationCrossReference = new FormulationCrossReference());
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		
		
		private void DataBindNutrient()
		{			
			//get list of math-types
			List<MathTypeInfo> mathType = new List<MathTypeInfo>();
			mathType = AIMManager.GetMathType(true);
			mathType.Insert
				(
					0,
					new MathTypeInfo
					{
						MathTypeID = 0,
						MathTypeName = "--Default--"
					}
				);

			//populate math-type dropdown on form
			lueMathTypeNutrient.Properties.DataSource = mathType;
			XtraEditors.LoadLookUpEdit(ref lueMathTypeNutrient, "MathTypeID", "MathTypeName");

			//populate math-type dropdown on grid
			replueMathTypeNutrient2.DataSource = mathType;
			XtraEditors.LoadGridLookUpEdit(ref replueMathTypeNutrient2, "MathTypeID", "MathTypeName");

			//populate grid
			Int32 currentCFCPlantRow = 0;
			if (grdCFCNutrientView != null)
			{
				Int32.TryParse(grdCFCNutrientView.FocusedRowHandle.ToString(), out currentCFCPlantRow);
			}
			grdCFCNutrient.DataSource = AIMManager.GetAllCFCNutrientAimDatabaseID(rbActiveRecords.Checked, (Int32)lueConnectionName.EditValue);
			grdCFCNutrientView.FocusedRowHandle = currentCFCPlantRow;

			//set row count
			CountNumberOfRows(grdCFCNutrientView);
		}
		private void DataBindIngredient()
		{
			//get list of math-types
			List<MathTypeInfo> mathType = new List<MathTypeInfo>();
			mathType = AIMManager.GetMathType(true);
			mathType.Insert
				(
					0,
					new MathTypeInfo
					{
						MathTypeID = 0,
						MathTypeName = "--Default--"
					}
				);
			lueMathTypeIngredient.Properties.DataSource = mathType;
			XtraEditors.LoadLookUpEdit(ref lueMathTypeIngredient, "MathTypeID", "MathTypeName");
			replueMathTypeIngredient.DataSource = mathType;
			XtraEditors.LoadGridLookUpEdit(ref replueMathTypeIngredient, "MathTypeID", "MathTypeName");


			//populate grid
			Int32 currentRowNum = 0;
			if (grdCFCIngredientView != null)
			{
				Int32.TryParse(grdCFCIngredientView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdCFCIngredient.DataSource = AIMManager.GetAllCFCIngredientAimDatabaseID(rbActiveRecords.Checked, (Int32)lueConnectionName.EditValue);
			grdCFCIngredientView.FocusedRowHandle = currentRowNum;

			//set row count
			CountNumberOfRows(grdCFCIngredientView);
		}
		private void DataBindPlant()
		{
			Int32 currentCFCPlantRow = 0;
			if (grdCFCPlantView != null)
			{
				Int32.TryParse(grdCFCPlantView.FocusedRowHandle.ToString(), out currentCFCPlantRow);
			}
			grdCFCPlant.DataSource = AIMManager.GetCFCPlantAimDatabaseID(rbActiveRecords.Checked, (Int32)lueConnectionName.EditValue);
			grdCFCPlantView.FocusedRowHandle = currentCFCPlantRow;			
			CountNumberOfRows(grdCFCPlantView);
		}
		private void DataBindProduct()
		{
			Int32 focusedRow = 0;
			if (grdCFCProductView != null)
			{
				Int32.TryParse(grdCFCProductView.FocusedRowHandle.ToString(), out focusedRow);
			}
            
            DataBindPlantLue();

			grdCFCProduct.DataSource = AIMManager.GetAllCFCProductAimDatabaseID(rbActiveRecords.Checked, (Int32)lueConnectionName.EditValue);
			grdCFCProductView.FocusedRowHandle = focusedRow;
			CountNumberOfRows(grdCFCProductView);
		}
		private void DataBindPlantLue()
		{
			List<CFCPlantInfo> cfcPlant = new List<CFCPlantInfo>();
			cfcPlant = AIMManager.GetCFCPlantAimDatabaseID(true, (Int32)lueConnectionName.EditValue);
			cfcPlant.Insert
			(
				0,
				new CFCPlantInfo
				{
					CFCPlantID = 0,
					CFCPlantCode = "--Select--",
					CFCPlantName = "--Select--"
				}
			);
			luePlant.Properties.DataSource = cfcPlant;
			XtraEditors.LoadLookUpEdit(ref luePlant, "CFCPlantID", "CFCPlantCode");
			luePlant.Properties.Columns["CFCPlantCode"].Visible = true;
			luePlant.Properties.Columns["CFCPlantName"].Visible = true;
			luePlant.EditValue = 0;

			repluePlantCode.DataSource = cfcPlant;
			XtraEditors.LoadGridLookUpEdit(ref repluePlantCode, "CFCPlantID", "CFCPlantCode");
			repluePlantCode.Columns["CFCPlantCode"].Visible = true;
			repluePlantCode.Columns["CFCPlantName"].Visible = true;
			repluePlantCode.NullText = "--Select--";

			//6/4/24
			luePlantArchive.Properties.DataSource = cfcPlant;
			XtraEditors.LoadLookUpEdit(ref luePlantArchive, "CFCPlantID", "CFCPlantCode");
			luePlantArchive.Properties.Columns["CFCPlantCode"].Visible = true;
			luePlantArchive.Properties.Columns["CFCPlantName"].Visible = true;
			luePlantArchive.EditValue = 0;

		}

		//6/4/24
		private void DataBindProductLue()
		{
			List<CFCProductInfo> prod = new List<CFCProductInfo>();
			prod = AIMManager.GetAllCFCProduct(true);
			//prod.Insert
			//(
			//	0,
			//	new CFCProductInfo
			//	{
			//		CFCProductID = 0,
			//		CFCProductCode = "--Select--",
			//		CFCProductName = "--Select--",
			//		CFCPlantCode = "--Select--"
			//	}
			//);

			lueProductArchive.Properties.DataSource = prod;
			XtraEditors.LoadLookUpEdit(ref lueProductArchive, "CFCProductID", "CFCProductCode", "CFCPlantCode");
			lueProductArchive.Properties.Columns["CFCProductCode"].Visible = true;
			lueProductArchive.Properties.Columns["CFCProductName"].Visible = true;
			lueProductArchive.Properties.Columns["CFCPlantCode"].Visible = true;
			lueProductArchive.EditValue = 0;

			lueProductArchive.Properties.Columns[1].Width = 90;
			lueProductArchive.Properties.Columns[2].Width = 150;
			lueProductArchive.Properties.Columns[3].Width = 210;

		}

		private void btnAddIngredient_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtIngredientCode.Text.Trim().Length == 0 || txtIngredientName.Text.Trim().Length == 0)
				{
					MessageBox.Show("You must enter ingredient code and ingredient name.", "Warning...", MessageBoxButtons.OK);
					if (txtIngredientCode.Text.Trim().Length == 0)
					{
						txtIngredientCode.Focus();
					}
					else if (txtIngredientName.Text.Trim().Length == 0)
					{
						txtIngredientName.Focus();
					}

					return;
				}

				CFCIngredientInfo cfcIngredient = new CFCIngredientInfo();

				cfcIngredient.CFCIngredientID = 0;
				cfcIngredient.CFCIngredientCode = txtIngredientCode.Text;
				cfcIngredient.CFCIngredientName = txtIngredientName.Text;
				cfcIngredient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//CFCIngredient column is "MathType", not "MathTypeID"
				//cfcIngredient.MathTypeID = (Int32)lueMathTypeIngredient.EditValue;
				cfcIngredient.MathType = (Int32)lueMathTypeIngredient.EditValue;

				cfcIngredient.AimDatabaseID = (Int32)lueConnectionName.EditValue;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveCFCIngredient(ref cfcIngredient);
				if (error.ErrorNumber != 0)
				{
					return;
				}
				txtIngredientCode.Text = String.Empty;
				txtIngredientName.Text = String.Empty;
				DataBindIngredient();
				txtIngredientCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddNutrient_Click(object sender, EventArgs e)
		{				
			try
			{
				if (txtNutrientCode.Text.Trim().Length == 0 || txtNutrientName.Text.Trim().Length == 0 || lueMathTypeNutrient.Text.ToString() == "--Select--")
				{
					MessageBox.Show("You must enter nutrient code and nutrient name.", "Warning...", MessageBoxButtons.OK);
					if (txtNutrientCode.Text.Trim().Length == 0)
					{
						txtNutrientCode.Focus();
					}
					else if (txtNutrientName.Text.Trim().Length == 0)
					{
						txtNutrientName.Focus();
					}
					else
					{
						lueMathTypeNutrient.Focus();
						lueMathTypeNutrient.ShowPopup();
					}
					return;
				}

				CFCNutrientInfo cfcNutrient = new CFCNutrientInfo();

				cfcNutrient.CFCNutrientID = 0;
				cfcNutrient.CFCNutrientCode = txtNutrientCode.Text;
				cfcNutrient.CFCNutrientName = txtNutrientName.Text;
				cfcNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				cfcNutrient.MathTypeID = (Int32)lueMathTypeNutrient.EditValue;
				cfcNutrient.AimDatabaseID = (Int32)lueConnectionName.EditValue;


				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveCFCNutrient(ref cfcNutrient);
				if (error.ErrorNumber != 0)
				{
					return;
				}
				txtNutrientCode.Text = String.Empty;
				txtNutrientName.Text = String.Empty;
				DataBindNutrient();
				txtNutrientCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddPlant_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtPlantCode.Text) == true)
				{
					MessageBox.Show("Please enter a plant code.", "Warning...", MessageBoxButtons.OK);
					txtPlantCode.Focus();
					return;
				}

				if (txtPlantName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a plant name.", "Warning...", MessageBoxButtons.OK);
					txtPlantName.Focus();
					return;
				}
				CFCPlantInfo cfcPlant = new CFCPlantInfo();

				cfcPlant.CFCPlantID = 0;
				cfcPlant.CFCPlantCode = txtPlantCode.Text;
				cfcPlant.CFCPlantName = txtPlantName.Text;
				if (IsCFCPlantCodeUnique(ref cfcPlant) == false)
				{
					return;
				}
				cfcPlant.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				cfcPlant.AimDatabaseID = (Int32)lueConnectionName.EditValue;

				ErrorInfo error = new ErrorInfo();
				AIMManager.InsertCFCPlant(ref cfcPlant);

				txtPlantCode.Text = String.Empty;
				txtPlantName.Text = String.Empty;
				DataBindPlant();
				txtPlantCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddProduct_Click(object sender, EventArgs e)
		{
			try
			{
				CFCProductInfo cfcProduct = new CFCProductInfo();

				cfcProduct.CFCProductID = 0;
				cfcProduct.CFCPlantID = (Int32)luePlant.EditValue;
				cfcProduct.CFCProductCode = txtProductCode.Text;
				cfcProduct.CFCProductName = txtProductName.Text;
				cfcProduct.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				cfcProduct.AimDatabaseID = (Int32)lueConnectionName.EditValue;

				if (txtProductCode.Text.Trim().Length > 0 && txtProductName.Text.Trim().Length > 0 && cfcProduct.CFCPlantID > 0)
				{
					AIMManager.SaveCFCProduct(ref cfcProduct);
					txtProductName.Text = String.Empty;
					txtProductCode.Text = String.Empty;
					luePlant.EditValue = 0;
					DataBindProduct();
					luePlant.Focus();
				}
				else
				{
					MessageBox.Show("You must enter plant, product code and product name.", "Warning...", MessageBoxButtons.OK);
					if (cfcProduct.CFCPlantID == 0)
					{
						luePlant.ShowPopup();
					}
					else if (txtProductCode.Text.Trim().Length == 0)
					{
						txtProductCode.Focus();
					}
					else
					{
						txtProductName.Focus();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void grdCFCIngredientView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			CFCIngredientInfo cfcIngredient = new CFCIngredientInfo();
			cfcIngredient = (CFCIngredientInfo)grdCFCIngredientView.GetFocusedRow();
			cfcIngredient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Ingredients. Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AIMManager.SaveCFCIngredient(ref cfcIngredient);
			DataBindIngredient();
		}
		private void grdCFCNutrientView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			CFCNutrientInfo cfcNutrient = new CFCNutrientInfo();
			cfcNutrient = (CFCNutrientInfo)grdCFCNutrientView.GetFocusedRow();
			cfcNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Nutrients.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AIMManager.SaveCFCNutrient(ref cfcNutrient);
			DataBindNutrient();
		}
		private void grdCFCPlantView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			CFCPlantInfo cfcPlant = new CFCPlantInfo();
			cfcPlant = (CFCPlantInfo)grdCFCPlantView.GetFocusedRow();
			cfcPlant.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive records.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}

			if (IsCFCPlantCodeUnique(ref cfcPlant) == false)
			{
				return;
			}
			AIMManager.SaveCFCPlant(ref cfcPlant);
			DataBindPlant();
		}
		private void grdCFCProductView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			CFCProductInfo cfcProduct = new CFCProductInfo();
			cfcProduct = (CFCProductInfo)grdCFCProductView.GetFocusedRow();
			cfcProduct.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Products. Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}

			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveCFCProduct(ref cfcProduct);
			DataBindProduct();
		}

		private void repbtnDeleteIngredient_Click(object sender, EventArgs e)
		{
			Boolean isActive = true;
			try
			{
				//------------------------------------------------------------------
				//If ingredient exists on cross-ref, do not allow it to be inactivated	
				CFCIngredientInfo cfcIngredient = new CFCIngredientInfo();
				cfcIngredient = (CFCIngredientInfo)grdCFCIngredientView.GetFocusedRow();				
				if (rbActiveRecords.Checked == true)
				{
					CFCIngredientRefAIMMaterialInfo xref = new CFCIngredientRefAIMMaterialInfo();
					xref = AIMManager.GetCFCIngredientRefAIMMaterial(cfcIngredient.CFCIngredientID);
					if (xref != null)
					{
						MessageBox.Show("This ingredient exists on the Formulation cross-reference table. Before it can be inactivated, you must first delete any cross-references - under Setup > Cross Reference > Formulation.", "Warning...", MessageBoxButtons.OK);
						return;
					}
				}

				//---------------------------------------------------------
				//10/25/23
				//if ingredient exists on ExportIngredient table, inactivate it there also.
				isActive = true;
				if (rbActiveRecords.Checked == true)
				{
					isActive = false;
				}
				List<ExportIngredientInfo> export = new List<ExportIngredientInfo>();
				export = AIMManager.GetExportIngredientCFCIngredient(cfcIngredient.CFCIngredientID);
				if (export != null)
				{
					if (export.Count > 0)
					{
						ErrorInfo updateStatus = new ErrorInfo();
						updateStatus = AIMManager.UpdateExportIngredientIsActive(cfcIngredient.CFCIngredientID, isActive, userGlobal.CFCLoginUser.CFCUserID);						
					}
				}

				//---------------------------------------------------------
				cfcIngredient.IsActive = !cfcIngredient.IsActive;
				cfcIngredient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveCFCIngredient(ref cfcIngredient);
				DataBindIngredient();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				CFCNutrientInfo cfcNutrient = new CFCNutrientInfo();
				cfcNutrient = (CFCNutrientInfo)grdCFCNutrientView.GetFocusedRow();

				//------------------------------------------------------------------
				//If nutrient exists on cross-ref, do not allow it to be inactivated	
				if (rbActiveRecords.Checked == true)
				{
					CFCNutrientRefAIMNutrientInfo xref = new CFCNutrientRefAIMNutrientInfo();
					xref = AIMManager.GetCFCNutrientRefAIMNutrient(cfcNutrient.CFCNutrientID);
					if (xref != null)
					{
						MessageBox.Show("This nutrient exists on the Formulation cross-reference table. Before it can be inactivated, you must first delete any cross-references - under Setup > Cross Reference > Formulation. ", "Warning...", MessageBoxButtons.OK);
						return;
					}
				}

				cfcNutrient.IsActive = !cfcNutrient.IsActive;
				cfcNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveCFCNutrient(ref cfcNutrient);
				DataBindNutrient();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeletePlant_Click(object sender, EventArgs e)
		{
			try
			{
				CFCPlantInfo cfcplant = new CFCPlantInfo();
				cfcplant = (CFCPlantInfo)grdCFCPlantView.GetFocusedRow();

				//------------------------------------------------------------------
				//If plant exists on cross-ref, do not allow it to be inactivated	
				if (rbActiveRecords.Checked == true)
				{
					CFCPlantRefAIMOwnerLocationInfo xref = new CFCPlantRefAIMOwnerLocationInfo();
					xref = AIMManager.GetCFCPlantRefAIMOwnerLocation(cfcplant.CFCPlantID);
					if (xref != null)
					{
						MessageBox.Show("This plant exists on the Formulation cross-reference table. Before it can be inactivated, you must first delete any cross-references - under Setup > Cross Reference > Formulation.", "Warning...", MessageBoxButtons.OK);
						return;
					}
				}

				cfcplant.IsActive = !cfcplant.IsActive;
				cfcplant.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveCFCPlant(ref cfcplant);
				DataBindPlant();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteProduct_Click(object sender, EventArgs e)
		{
			try
			{
				CFCProductInfo cfcProduct = new CFCProductInfo();
				cfcProduct = (CFCProductInfo)grdCFCProductView.GetFocusedRow();

				//------------------------------------------------------------------
				//If product exists on cross-ref, do not allow it to be inactivated	
				if (rbActiveRecords.Checked == true)
				{
					CFCProductRefAIMMaterialInfo xref = new CFCProductRefAIMMaterialInfo();
					xref = AIMManager.GetCFCProductRefAIMMaterial(cfcProduct.CFCProductID);
					if (xref != null)
					{
						MessageBox.Show("This product exists on the Formulation cross-reference table. Before it can be inactivated, you must first delete any cross-references - under Setup > Cross Reference > Formulation.", "Warning...", MessageBoxButtons.OK);
						return;
					}
				}

				cfcProduct.IsActive = !cfcProduct.IsActive;
				cfcProduct.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveCFCProduct(ref cfcProduct);
				DataBindProduct();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPreviewIngredient_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdCFCIngredient, "FRM Ingredient");
				CFCAIMCore.Global.previewGridResults(this.grdCFCIngredient, "FRM Ingredient");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdCFCNutrient, "FRM Nutrient");
				CFCAIMCore.Global.previewGridResults(this.grdCFCNutrient, "FRM Nutrient");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewPlant_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdCFCPlant, "FRM Plant");
				CFCAIMCore.Global.previewGridResults(grdCFCPlant, "FRM Plant");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewProduct_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdCFCProduct, "FRM Product");
				CFCAIMCore.Global.previewGridResults(grdCFCProduct, "FRM Product");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void grdCFCIngredientView_Layout(object sender, EventArgs e)
		{
			try
			{
				CountNumberOfRows(grdCFCIngredientView);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdCFCNutrientView_Layout(object sender, EventArgs e)
		{
			try
			{
				CountNumberOfRows(grdCFCNutrientView);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdCFCPlantView_Layout(object sender, EventArgs e)
		{
			try
			{
				CountNumberOfRows(grdCFCPlantView);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private Boolean IsCFCPlantCodeUnique(ref CFCPlantInfo cfcPlant)
		{
			CFCPlantInfo cfcPlantLocal = new CFCPlantInfo();
			Boolean valid = true;
			cfcPlantLocal = AIMManager.GetCFCPlant(cfcPlant.CFCPlantCode, (Int32)lueConnectionName.EditValue);
			if (cfcPlantLocal != null)
			{
				if (cfcPlantLocal.CFCPlantID != cfcPlant.CFCPlantID)
				{
					MessageBox.Show("FRM Plant Code has to be unique.", "Warning...", MessageBoxButtons.OK);
					valid = false;
				}
			}
			return valid;
		}
		private void grdCFCProductView_Layout(object sender, EventArgs e)
		{
			try
			{
				CountNumberOfRows(grdCFCProductView);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CountNumberOfRows(DevExpress.XtraGrid.Views.Grid.GridView view)
		{
			//lblRecordCount.Text = grdCFCIngredientView.DataRowCount.ToString() + " Records Found";
			lblRecordCount.Text = view.DataRowCount.ToString() + " Records Found";
		}

		private void tabControl2_SelectedIndexChanged(Object sender, EventArgs e)
		{
			rbActiveRecords.Visible = false;
			rbDeletedRecords.Visible = false;
			rbActiveRecords.SendToBack();
			rbDeletedRecords.SendToBack();

			if (tabControl2.SelectedIndex == 1 || tabControl2.SelectedIndex == 2 ||
				tabControl2.SelectedIndex == 3 || tabControl2.SelectedIndex == 4)      
			{
				rbActiveRecords.Visible = true;
				rbDeletedRecords.Visible = true;
				rbActiveRecords.BringToFront();
				rbDeletedRecords.BringToFront();
			}

			if (tabControl2.SelectedIndex == 4)		//Product
			{
				DataBindPlantLue();
			}

			if (tabControl2.SelectedIndex == 5)		//Product Archive
			{
				_IsLoading = true;
				DataBindProductLue();
				lueProductArchive.ShowPopup();
				_IsLoading = false;
			}

            if (tabControl2.SelectedIndex == 6)		//Cross-References
            {
                //6/20/18 - Check UserAccess Permission
                pnlCrossReferenceButton.Enabled = true;
                Int32 rtn = CFCAIM.AIMUtilities.GlobalFunctions.CheckUserPermissions(userGlobal.CFCLoginUser.CFCUserID, (Int32)EnumPermissionTable.CrossReferenceMaintenance);
                if (rtn == -1)
                {
                    pnlCrossReferenceButton.Enabled = false;
                }
            }

            //---------------------------------------
            //5/30/19            
            if (tabControl2.SelectedIndex == 1)
            {
                grdCFCIngredient.Focus();
            }
            else if (tabControl2.SelectedIndex == 2)
            {

				//---------------------
				//7/27/22
				txtDefaultMethod.Text = "";				
				List<AIMPriorityUsedValueInfo> priority = new List<AIMPriorityUsedValueInfo>();
				AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
				aimDatabase = AIMManager.GetAIMDatabase(CFCAIMCore.Global.aimDatabase.AIMDatabaseID);
				if (aimDatabase != null)
				{
					txtDefaultMethod.Text = aimDatabase.DefaultForPropose.ToString();				
				}			
				//---------------------

				grdCFCNutrient.Focus();
            }
            else if (tabControl2.SelectedIndex == 3)
            {
                grdCFCPlant.Focus();
            }
            else if (tabControl2.SelectedIndex == 4)
            {
                grdCFCProduct.Focus();
            }
                        
        }

		private void rbActiveRecords_CheckedChanged(Object sender, EventArgs e)
		{  
			try
			{
				if (rbActiveRecords.Checked)
				{
					repbtnDeleteIngredient.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteIngredient.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteNutrient.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteNutrient.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeletePlant.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeletePlant.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteProduct.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteProduct.Buttons[0].ToolTip = "Inactivate record";

					txtIngredientCode.Enabled = true;
					txtIngredientName.Enabled = true;
					txtNutrientCode.Enabled = true;
					txtNutrientName.Enabled = true;
					txtPlantCode.Enabled = true;
					txtPlantName.Enabled = true;
					txtProductCode.Enabled = true;
					txtProductName.Enabled = true;

					lueMathTypeIngredient.Enabled = true;
					lueMathTypeNutrient.Enabled = true;
					luePlant.Enabled = true;

					btnAddIngredient.Enabled = true;
					btnAddNutrient.Enabled = true;
					btnAddPlant.Enabled = true;
					btnAddProduct.Enabled = true;

				}
				else
				{
					repbtnDeleteIngredient.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteIngredient.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteNutrient.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteNutrient.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeletePlant.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeletePlant.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteProduct.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteProduct.Buttons[0].ToolTip = "Re-activate record";

					txtIngredientCode.Enabled = false;
					txtIngredientName.Enabled = false;
					txtNutrientCode.Enabled = false;
					txtNutrientName.Enabled = false;
					txtPlantCode.Enabled = false;
					txtPlantName.Enabled = false;
					txtProductCode.Enabled = false;
					txtProductName.Enabled = false;

					lueMathTypeIngredient.Enabled = false;
					lueMathTypeNutrient.Enabled = false;
					luePlant.Enabled = false;

					btnAddIngredient.Enabled = false;
					btnAddNutrient.Enabled = false;
					btnAddPlant.Enabled = false;
					btnAddProduct.Enabled = false;

				}
				DataBindIngredient();
				DataBindNutrient();
				DataBindPlant();
				DataBindProduct();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ClearTextFields()
		{
			txtIngredientCode.Text = String.Empty;
			txtIngredientName.Text = String.Empty;
			txtNutrientCode.Text = String.Empty;
			txtNutrientName.Text = String.Empty;
			txtPlantCode.Text = String.Empty;
			txtPlantName.Text = String.Empty;
			txtProductCode.Text = String.Empty;
			txtProductName.Text = String.Empty;
		}

		private void lueMathTypeIngredient_Enter(Object sender, EventArgs e)
		{
			lueMathTypeIngredient.SelectAll();
		}

		private void lueMathTypeIngredient_Click(Object sender, EventArgs e)
		{
			lueMathTypeIngredient.SelectAll();
		}

		private void lueMathTypeNutrient_Enter(Object sender, EventArgs e)
		{
			lueMathTypeNutrient.SelectAll();
		}

		private void lueMathTypeNutrient_Click(Object sender, EventArgs e)
		{
			lueMathTypeNutrient.SelectAll();
		}

		private void luePlant_Enter(Object sender, EventArgs e)
		{
			luePlant.SelectAll();
		}

		private void luePlant_Click(Object sender, EventArgs e)
		{
			luePlant.SelectAll();
		}

		private void btnProduct_Click(Object sender, EventArgs e)
		{				
			try
			{
				AIMGlobal.formulationCrossReference = "Product";
				FormulationCrossReference formulationCrossReference;
				AIMGlobal.open_form(formulationCrossReference = new FormulationCrossReference());
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}		
		}

		private void btnIngredient_Click(Object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.formulationCrossReference = "Ingredient";
				FormulationCrossReference formulationCrossReference;
				AIMGlobal.open_form(formulationCrossReference = new FormulationCrossReference());

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		private void btnNutrient_Click(Object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.formulationCrossReference = "Nutrient";
				FormulationCrossReference formulationCrossReference;
				AIMGlobal.open_form(formulationCrossReference = new FormulationCrossReference());

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		private void btnResize_Click(Object sender, EventArgs e)
		{         
            WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		private void DataMapping_SizeChanged(Object sender, EventArgs e)
		{            
            btnResize.Visible = true;
		}

		private void btnSyncHelp_Click(Object sender, EventArgs e)
		{
			try
			{
				helpSync = new HelpSync();
				helpSync.ShowDialog();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void grdCFCIngredientView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //5/30/19
            AIMGlobal.focusedRowGreen(sender, e);
        }

        private void grdCFCNutrientView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //5/30/19
            AIMGlobal.focusedRowGreen(sender, e);
        }

        private void grdCFCPlantView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //5/30/19
            AIMGlobal.focusedRowGreen(sender, e);
        }

        private void grdCFCProductView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            //5/30/19
            AIMGlobal.focusedRowGreen(sender, e);
        }

		//4/12/22
		private void grdCFCIngredientView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: colCFCIngredientCode
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}

		//4/12/22
		private void grdCFCNutrientView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: colCFCNutrientCode
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}

		//4/12/22
		private void grdCFCPlantView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: colCFCPlantCode
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}

		//4/12/22
		private void grdCFCProductView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: colCFCPlantCode_Product, colCFCProductCode
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}

		private void lueDefaultForProposed_Click(object sender, EventArgs e)
		{
			lueDefaultForProposed.SelectAll();
		}

		private void lueDefaultForProposed_Enter(object sender, EventArgs e)
		{
			lueDefaultForProposed.SelectAll();
		}

		private void luePriority_Click(object sender, EventArgs e)
		{
			luePriority.SelectAll();
		}

		private void luePriority_Enter(object sender, EventArgs e)
		{
			luePriority.SelectAll();
		}

		private void lueDataSourceType_Click(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

		private void lueDataSourceType_Enter(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

		//11/30/23
		private void DataBindOverlay()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdOverlayView.FocusedRowHandle.ToString(), out rowNum);			
			grdOverlay.DataSource = AIMManager.GetCFCIngredientNutrientOverlay();
			grdOverlayView.FocusedRowHandle = rowNum;
		}

		//11/30/23		
		private void grdOverlayView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}

		//6/4/24
		private void DataBindProductArchive(Int32 cfProductID)
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdProductArchiveView.FocusedRowHandle.ToString(), out rowNum);
			grdProductArchive.DataSource = AIMManager.GetCFCProductArchive_ProductID(cfProductID);		
			grdProductArchiveView.FocusedRowHandle = rowNum;
		}

		//6/4/24
		private void grdProductArchiveView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}

		//6/4/24
		private void btnShowArchive_Click(object sender, EventArgs e)
		{
			if ((Int32)lueProductArchive.EditValue == 0)
			{
				MessageBox.Show("Please select a product.", "Warning...", MessageBoxButtons.OK);
				lueProductArchive.ShowPopup();
				return;
			}
			DataBindProductArchive((Int32)lueProductArchive.EditValue);
		}

		//6/5/24
		private void lueProductArchive_EditValueChanged(object sender, EventArgs e)
		{
			if (_IsLoading == false)
			{
				btnShowArchive.PerformClick();
			}
			
		}
	}
}

