using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using System.Drawing;
using System.Linq;
using Core.Library.DevExpress;
using CFCAIM.DataExchange;
using CFCAIM.Scheduler;
using Core.Library.SQLDatabaseConnection;
using CFCAIM.CrossReference;
using DevExpress.XtraGrid.Views.Grid;
using CFCAIM.Imports;
using System.Text.RegularExpressions;

namespace CFCAIM
{
    public partial class ImportData : Form
    {

        CFCUser.Global _UserGlobal = new CFCUser.Global();
        MainMenu _MainMenu = (MainMenu)Application.OpenForms["MainMenu"];
        Boolean _stopFileReading = false;
        //to create samples
        Int32 _aimOwnerID = 0;
        Int32 _aimOwnerLocationID = 0;
        Int32 _aimsourceID = 0;
        Int32 _aimsourceLocationID = 0;
        String _analyzerMaterialType = "";
        Boolean _isDataLoaded = false;
        Int32 _delimiterTypeID = 0;
        Boolean _isGoodConnection = false;
        Boolean _onlyOneTemplate = false;
        List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
        List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
        TabPage _PreviousTab;
        TabPage _CurrentTab;
        Int32 _batchID = 0;
		HelpSystemImports helpSystemImports;
        private List<SQLDatabaseInfo> _databases = new List<SQLDatabaseInfo>();

        ImportSqlConnections importSqlConnections;
		String serverName = "";
		String userName = "";
		String password = "";
		String databaseName = "";
		String connectName = "";
		Boolean sqlAuth = false;
        Int32 _configID = 0;
        Int32 _sqlLogID = 0;
        Boolean _connectionSaved = false;

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        List<AIMSupplierInfo> _aimSupplier = new List<AIMSupplierInfo>();
        List<AIMSupplierLocationInfo> _aimSupplierLocation = new List<AIMSupplierLocationInfo>();



        #region Form Controls
        public ImportData()
		{
			InitializeComponent();
		}
		private void ProcessResults_Load(object sender, EventArgs e)
		{
			try
			{

				DataBindTemplateAndDataSourceType();
				LoadGridWithFiles();
				DataBindLueOwner();

                //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
                DataBindLueSupplier();
                DataBindLueSupplierLocation();

                DataBindLogGrid(); 
				SetToolTips();
				txtProcessPath.Text = "";				
				txtErrorFolder.Text = "";
				txtBackUpDir.Text = "";
				grdFiles.DataSource = "";
				pnlAdd.Enabled = true;

				HidePanels();

				if (_onlyOneTemplate == true)
				{
					lueTemplateName.EditValue = 0;
					//????? lueTemplateName.EditValue = 1;
				}

				SQLDatabaseConnection.LoadConnection(ref _databases, false, SQLDatabaseConnection.CFCAIMDatabaseFile);
                DataBindLueConnectName();
                ClearSQLImportGlobals();
                                
                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        private void ClearSQLImportGlobals()
        {
            AIMGlobal.sqlNewOrEdit = "";
            AIMGlobal.sqlDatabaseConnectionName = "";
            AIMGlobal.systemTemplateName = "";
            AIMGlobal.systemTemplateDataSourceTypeID = 0;

        }
        private void DataBindLueConnectName()
        {
            List<SQLDatabaseInfo> dbase = new List<SQLDatabaseInfo>();
            for (int i = 0; i < _databases.Count; i++)
            {
                if (_databases[i].ConnectionType == 2 && _databases[i].ConnectName != "New")
                {
                    dbase.Insert
                    (
                        0,
                        new SQLDatabaseInfo
                        {
                            //DatabaseID = _databases[i].DatabaseID,
                            //ConnectName = _databases[i].ConnectName
                            ConnectName = _databases[i].ConnectName,
                            ConnectionType = _databases[i].ConnectionType,
                            Database = _databases[i].Database,
                            DatabaseID = _databases[i].DatabaseID,
                            IsDefault = _databases[i].IsDefault,
                            IsSQLAuthentication = _databases[i].IsSQLAuthentication,
                            Password = _databases[i].Password,
                            ServerName = _databases[i].ServerName,
                            User = _databases[i].User,

                            //11/30/21
                            ProductionOrNon = _databases[i].ProductionOrNon
                        }
                    ) ;
                }
            }

            lueConnectName.Properties.DataSource = dbase;
            XtraEditors.LoadLookUpEdit(ref lueConnectName, "DatabaseID", "ConnectName");
            lueConnectName.Properties.Columns["DatabaseID"].Caption = "Connection Name";
        }

        private void btnBrowseProcessPath_Click(object sender, EventArgs e)
		{
			try
			{
				if (Directory.Exists(txtProcessPath.Text) == true)
				{
					fbdSelectedDirectory.SelectedPath = txtProcessPath.Text;
				}
				fbdSelectedDirectory.ShowDialog();
				txtProcessPath.Text = fbdSelectedDirectory.SelectedPath;

				txtErrorFolder.Text = txtProcessPath.Text + @"\Error";

				LoadGridWithFiles();
				UpdateImportConfig();
                DataBindTemplateAndDataSourceType();

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
				FilesInfo file = new FilesInfo();
				file = (FilesInfo)grdFilesView.GetFocusedRow();
				file.BackupPath = file.ProcessPath + @"\Rejected";
				DataExchangeManager.MoveFileToFolder(file, 0);
				LoadGridWithFiles();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CheckImportFields()
		{
			if (tcoProcess.TabPages.Contains(tpExisting) == false)
			{
				timer1.Stop();
				btnStartImport.BackColor = SystemColors.Control;
				return;
			}


            //7/17/20 - added check for null
            if (lueFileType.EditValue != null)
            {

                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.SQL)
                if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.SQL)

                {
                    timer1.Start();
                }
            }
            


			else
			{	 
				Int32 files = grdFilesView.DataRowCount;
				if ((Int32)lueTemplateName.EditValue != 0 && files != 0 && txtProcessPath.Text.Length > 0 && txtBackUpDir.Text.Length > 0)
				{
					if (pnlRequiredFields.Visible == true)
					{
						if ((Int32)lueOwner.EditValue != 0 && (Int32)lueOwnerLocation.EditValue != 0)
						{
							timer1.Start();
						}
						else
						{
							timer1.Stop();
							btnStartImport.BackColor = SystemColors.Control;
						}
					}
					else
					{
						timer1.Start();
					}
				}
				else
				{
					timer1.Stop();
					btnStartImport.BackColor = SystemColors.Control;
				}
			}
		}
		private void btnStartProcess_Click(object sender, EventArgs e)
		{
			try
			{
				timer1.Stop();
				prgsbarProcessFiles.Value = 0;

				if ((Int32)lueTemplateName.EditValue == 0)
				{
					MessageBox.Show("Please select template.", "Warning...");
					lueTemplateName.ShowPopup();
					return;
				}

				if ((Int32)lueDataSourceType.EditValue == 0)
				{
					MessageBox.Show("Please select datasource.", "Warning...");
					lueDataSourceType.ShowPopup();
					return;
				}

                //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
                if (pnlRows.Visible == true)
                {
                    if (txtHeaderRow.Text == String.Empty)
                    {
                        MessageBox.Show("Please enter Header Row.", "Error...", MessageBoxButtons.OK);
                        txtHeaderRow.Focus();
                        return;
                    }
                    if (txtDetailRow.Text == String.Empty)
                    {
                        MessageBox.Show("Please enter Detail Row.", "Error...", MessageBoxButtons.OK);
                        txtDetailRow.Focus();
                        return;
                    }
                }

                if (pnlRequiredFields.Visible == true)
				{
					if ((lueOwner.Enabled == true && (Int32)lueOwner.EditValue == 0) || (lueOwnerLocation.Enabled == true && (Int32)lueOwnerLocation.EditValue == 0))
					{
						MessageBox.Show("Please select AIM required fields.", "Error...", MessageBoxButtons.OK);
						pnlRequiredFields.Focus();
						if (lueOwner.Enabled == true && (Int32)lueOwner.EditValue == 0)
						{
							lueOwner.ShowPopup();
						}
						else
						{
							lueOwnerLocation.ShowPopup();
						}
						return;
					}
				}

                //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
                if (pnlSupplier.Visible == true)
                {
                    if ((lueSupplier.Enabled == true && (Int32)lueSupplier.EditValue == 0) || (lueSupplierLocation.Enabled == true && (Int32)lueSupplierLocation.EditValue == 0))
                    {
                        MessageBox.Show("Please select AIM required fields.", "Error...", MessageBoxButtons.OK);
                        pnlRequiredFields.Focus();
                        if (lueSupplier.Enabled == true && (Int32)lueSupplier.EditValue == 0)
                        {
                            lueSupplier.ShowPopup();
                        }
                        else
                        {
                            lueSupplierLocation.ShowPopup();
                        }
                        return;
                    }
                }                              

                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.SQL)
                if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.SQL)

                {

                    //Make sure connection string is selected
                    if (txtConnectNameExisting.Text == "")
                    {
                        MessageBox.Show("Please select or add a Connection Name (where the samples to import reside).", "Warning...");
                        lueConnectName.Focus();
                        return;
                    }


                    //Make sure start/end dates have been entered
                    String checkDates = CheckSqlDates();
					if (checkDates.Length > 0)
					{					
						MessageBox.Show(checkDates, "Warning...");
						dtStartDate.Focus();
						return;
					}					
				}


				if (tcoProcess.TabPages.Contains(tpExisting) == true)
				{
					tcoProcess.TabPages.Remove(tpErrors);
				}


				SetControlsForProcess(true);
				if (backgroundFileRead.IsBusy == false)
				{
					backgroundFileRead.RunWorkerAsync();
				}
				else
				{
					MessageBox.Show("Program is busy reading files for you. Please try later.", "Warning...");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private String CheckSqlDates()
		{
			String rtn = "";
			DateTime temp;

			try
			{
				if (dtStartDate.Text.Length == 0 || dtEndDate.Text.Length == 0)
				{
					rtn = "Please enter start and end dates.";
					return rtn;
				}

				if (DateTime.TryParse(dtStartDate.Text, out temp))
				{}
				else
				{ 				
					rtn = "Please enter valid start date.";
					return rtn;
				}

				if (DateTime.TryParse(dtEndDate.Text, out temp))
				{}			
				else
				{
					rtn = "Please enter valid end date.";
					return rtn;
				}
				return rtn;				
			}
			
			catch (Exception)
			{
				rtn = "Invalid start/end date.";
				return rtn;
			}
		
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			try
			{
				if (tcoProcess.TabPages.Contains(tpErrors) == true)
				{
					DialogResult dr = new DialogResult();
					dr = MessageBox.Show("Some samples have not been imported. Continue exit?", "Warning...", MessageBoxButtons.YesNoCancel);
					if (dr != DialogResult.Yes)
					{
						return;
					}

				}
				Close();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnBrowseBackDir_Click(object sender, EventArgs e)
		{
			try
			{
				if (Directory.Exists(txtBackUpDir.Text) == true)
				{
					fbdSelectedDirectory.SelectedPath = txtBackUpDir.Text;
				}
				fbdSelectedDirectory.ShowDialog();
				txtBackUpDir.Text = fbdSelectedDirectory.SelectedPath;
				UpdateImportConfig();
                DataBindTemplateAndDataSourceType();

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnDeleteSetting_Click(object sender, EventArgs e)
		{
			Int32 importConfig = 0;
			try
			{
				Int32.TryParse(lueTemplateName.EditValue.ToString(), out importConfig);
				if (importConfig > 0)
				{

					DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this import template?", "Warning...", MessageBoxButtons.YesNo);
					if (dialogResult == DialogResult.Yes)
					{
						AIMManager.DeleteImportConfig(importConfig);
						DataBindTemplateAndDataSourceType();
						RefreshEverything();
						btnRefresh.PerformClick();
					}
				}
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnNewBrowseProcessPath_Click(object sender, EventArgs e)
		{
			try
			{
				fbdSelectedDirectory.ShowDialog();
				txtNewProcessPath.Text = fbdSelectedDirectory.SelectedPath;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnNewBrowseBackupPath_Click(object sender, EventArgs e)
		{
			try
			{
				fbdSelectedDirectory.ShowDialog();
				txtNewBackUpPath.Text = fbdSelectedDirectory.SelectedPath;
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
				Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
				ttip.SetToolTip(btnDeleteSetting, "Delete Template");
				ttip.SetToolTip(btnStopProcess, "Stop Process");
				ttip.SetToolTip(btnSaveSetting, "Save Template");
				ttip.SetToolTip(btnStartImport, "Import Samples");
				ttip.SetToolTip(btnSelectExisting, "Select Existing Template");
				ttip.SetToolTip(btnAddNew, "Add New Template");
				ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
				ttip.SetToolTip(btnRefreshGrid, "Refresh Grid");
				ttip.SetToolTip(btnHelp, "System Import Template Examples");
				ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
				ttip.SetToolTip(btnAcceptAllFiles, "Mark All Files (Accepted/Rejected)");
				

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnStopProcess_Click(object sender, EventArgs e)
		{
			try
			{
				_stopFileReading = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtNewConfigName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				ImportConfigInfo importConfig = new ImportConfigInfo();

				importConfig = AIMManager.GetImportConfig(txtNewConfigName.Text);

				if (importConfig != null)
				{
					MessageBox.Show("Template Name already exists.", "Warning...", MessageBoxButtons.OK);
					txtNewConfigName.Text = String.Empty;
					e.Cancel = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void HidePanels()
		{
			pnlDataSource.Visible = false;
			pnlDelimiter.Visible = false;
			pnlFileImport.Visible = false;
			grdFiles.Visible = false;
			btnRefreshGrid.Visible = false;
			btnAcceptAllFiles.Visible = false;
			pnlNewFileImport.Visible = false;
			pnlDelimiterSheetNew.Visible = false;

			pnlSQLConnectionExisting.Visible = false;
			pnlSQLConnectionExisting.Location = new Point(299, 306);

			pnlRequiredFields.Visible = false;
			pnlRequiredFields.Location = new Point(22, 471);

			//-------------------------------------------------
			//hide new
			if (tcoProcess.TabPages.Contains(tpNewSetting) == true)
			{
				tcoProcess.TabPages.Remove(tpNewSetting);
			}
			//hide existing
			if (tcoProcess.TabPages.Contains(tpExisting) == true)
			{
				tcoProcess.TabPages.Remove(tpExisting);
			}
			//hide errors
			if (tcoProcess.TabPages.Contains(tpErrors) == true)
			{
				tcoProcess.TabPages.Remove(tpErrors);
			}

			//TODO - (2/23/17) -- removed log page and commented this until coding is done
			tcoProcess.TabPages.Remove(tpLog);
			
			tcoProcess.DrawMode = TabDrawMode.OwnerDrawFixed;
			tcoProcess.DrawItem += new DrawItemEventHandler(tcoProcess_DrawItem);

		}

		private void CheckMissingSamples()
		{
			btnMissingSamples.Visible = false;			
			List<ImportSQLSampleMissingInfo> log = new List<ImportSQLSampleMissingInfo>();
			log = AIMManager.GetImportSQLSampleMissing();

			if (log.Count > 0)
			{
				btnMissingSamples.Visible = true;
			}
		}

		private void lueSetting_EditValueChanged(object sender, EventArgs e)
		{
			try
			{	   
				if (_isDataLoaded == false)
				{
					return;
				}
                				
				btnStartImport.Visible = true;

                //1/24/18 - changed - was not showing for Marilyn
                btnStartImport.Location = new Point(962, 6);


                pnlDataSource.Visible = true;	   
				pnlDelimiter.Visible = false;
				pnlFileImport.Visible = false;
				grdFiles.Visible = false;
				btnRefreshGrid.Visible = false;

				pnlSqlParameters.Visible = false;
				pnlSqlParameters.Location = new Point(27, 278);
								
				lueDataSourceType.TabStop = true;
				lueFileType.TabStop = true;

				btnAcceptAllFiles.Visible = false;
				btnMissingSamples.Visible = false;

				List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
				importConfig = (List<ImportConfigInfo>)lueTemplateName.Properties.DataSource;
								
				DatabindFileType();

				lueFileType.EditValue = (Int32)importConfig[lueTemplateName.ItemIndex].FileTypeID;	  
				lueFileType2.EditValue = importConfig[lueTemplateName.ItemIndex].FileTypeID;  

				pnlSQLConnectionExisting.Visible = false;
				pnlSQLConnectionExisting.Location = new Point(299, 306);

                //----------------------------------------------------------------
                //SQL FILE TYPE
                if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.SQL)
                {
					btnMissingSamples.Visible = true;
					btnStartImport.Location = new Point(300, 200);					
					pnlSqlParameters.Visible = true;
					pnlSqlParameters.Location = new Point(24, 195 );				
					lueDataSourceType.TabStop = false;
					lueFileType.TabStop = false;						
					_configID = (Int32)importConfig[lueTemplateName.ItemIndex].ImportConfigID;

					//Populate StartDate with LastImportDate 
					List<ImportSQLSampleLogInfo> log = new List<ImportSQLSampleLogInfo>();
					log = AIMManager.GetImportSQLSampleLog(_configID);
					if (log != null)
					{  
						if (log.Count == 0)
						{							
							dtStartDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
							dtEndDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
						}
						else
						{
							dtStartDate.Text = log[0].LastImportDate.ToString("MM/dd/yyyy");
							_sqlLogID = log[0].ImportSQLSampleLogID;
						}
							  
					}

					pnlSQLConnectionExisting.Visible = true;					
					pnlSQLConnectionExisting.Location = new Point(6, 125);
					
					lueDataSourceType.EditValue = (Int32)importConfig[lueTemplateName.ItemIndex].DataSourceTypeID;
					serverName = importConfig[lueTemplateName.ItemIndex].ServerName;
					userName = importConfig[lueTemplateName.ItemIndex].SqlUserName;
					password = importConfig[lueTemplateName.ItemIndex].SqlPassword;
					connectName = importConfig[lueTemplateName.ItemIndex].ConnectName;
					databaseName = importConfig[lueTemplateName.ItemIndex].DatabaseName;
					sqlAuth = importConfig[lueTemplateName.ItemIndex].IsSqlAuthentication;
                    txtConnectNameExisting.Text = importConfig[lueTemplateName.ItemIndex].ConnectName;

                    //If records exist in MissingSamplesLog, show button to view missing
                    CheckMissingSamples();

				}

				//----------------------------------------------------------------
				else
				{			  
					pnlDelimiter.Visible = true;
					pnlFileImport.Visible = true;
					grdFiles.Visible = true;
					btnRefreshGrid.Visible = true;
					btnAcceptAllFiles.Visible = true;

					txtBackUpDir.Text = importConfig[lueTemplateName.ItemIndex].BackUpDirectory;
					txtProcessPath.Text = importConfig[lueTemplateName.ItemIndex].ProcessDirectory;				   
					txtErrorFolder.Text = importConfig[lueTemplateName.ItemIndex].ProcessDirectory + @"\Error";
					lueDelimiterType.EditValue = importConfig[lueTemplateName.ItemIndex].DelimiterTypeID;
					lueDataSourceType.EditValue = importConfig[lueTemplateName.ItemIndex].DataSourceTypeID;					
					lueFileType.EditValue = importConfig[lueTemplateName.ItemIndex].FileTypeID;
					cbCreateSample.Checked = importConfig[lueTemplateName.ItemIndex].IsCreateSample;
					lblSheet.Visible = false;
					txtSheetNumber.Visible = false;
                    if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX)
                    {
						lblSheet.Visible = true;
						txtSheetNumber.Visible = true;
						txtSheetNumber.Text = Convert.ToString(importConfig[lueTemplateName.ItemIndex].ExcelSheetNumber);
					}
					LoadGridWithFiles();
				}

				pnlRequiredFields.Visible = false;
				pnlRequiredFields.Location = new Point(22, 471);

				if ((Int32)lueDataSourceType.EditValue == (Int32)DataSourceTypeEnum.NutriQuest)
				{
					pnlRequiredFields.Visible = true;
					pnlRequiredFields.Location = new Point(22, 275);
					ChangeDropdownBackcolor(lueOwner);
					ChangeDropdownBackcolor(lueOwnerLocation);
				}              

                CheckImportFields();

			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSaveSetting_Click(object sender, EventArgs e)
		{
			try
			{

				timer2.Stop();
				btnSaveSetting.BackColor = SystemColors.Control;

				Int32 aimDatabaseID = 0;
				Int32 sheetNumber = 0;

                //------------------------------------------------------------------

                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueNewFileType.EditValue != (Int32)ImportFileTypeEnum.SQL)
                if ((Int32)lueNewFileType.EditValue != (Int32)FileTypeEnum.SQL)

                {						

					if ((txtNewConfigName.Text.Trim().Length == 0 ||
						txtNewBackUpPath.Text.Trim().Length == 0 ||
						txtNewProcessPath.Text.Trim().Length == 0 ||
						lueNewFileType.EditValue == null ||
						lueNewDataSourceType.EditValue == null))
					{
						MessageBox.Show("Not everything is filled in. Please complete and press save.", "Warning...");
						return;

					}

					if (txtNewConfigName.Text.Trim().Length == 0 ||
					lueNewFileType.EditValue == null ||
					lueNewDataSourceType.EditValue == null)
					{
						MessageBox.Show("Not everything is filled in. Please fill everything in and press save.", "Warning...");
						return;
					}
				}

                //------------------------------------------------------------------

                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueNewFileType.EditValue == (Int32)ImportFileTypeEnum.SQL)
                if ((Int32)lueNewFileType.EditValue == (Int32)FileTypeEnum.SQL)

                {
                    if ((Int32)lueConnectName.EditValue == 0)
                    {
                        MessageBox.Show("Please select or add a Connection Name.", "Warning...");
                        return;
                    }

					if (_isGoodConnection == false)
					{
						MessageBox.Show("Please connect to database before saving template.", "Warning...");
						return;
					}
					if (_connectionSaved == false)
					{
						MessageBox.Show("Database connection was not saved. Save connection before saving the template.", "Warning...");
						return;
					}
				}
				

				ImportConfigInfo importConfig = new ImportConfigInfo();
				CFCUser.Global global = new CFCUser.Global();
				importConfig.CFCUserID = global.CFCLoginUser.CFCUserID;
				importConfig.BackUpDirectory = txtNewBackUpPath.Text;
				importConfig.ProcessDirectory = txtNewProcessPath.Text;
				importConfig.ImportConfigID = 0;
				importConfig.Name = txtNewConfigName.Text;
				importConfig.DelimiterTypeID = (Int32)lueNewDelimiterType.EditValue;				
				importConfig.FileTypeID = (Int32)lueNewFileType.EditValue;
				importConfig.DataSourceTypeID = (Int32)lueNewDataSourceType.EditValue;
				importConfig.AIMDatabaseID = aimDatabaseID;
				importConfig.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
				importConfig.IsCreateSample = cbNewCreateSample.Checked;
				Int32.TryParse(txtNewSheetNumber.Text, out sheetNumber);

                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueNewFileType.EditValue == (Int32)ImportFileTypeEnum.Excel)
                if ((Int32)lueNewFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueNewFileType.EditValue == (Int32)FileTypeEnum.XLSX)

                {
					if (txtNewSheetNumber.Text.Length == 0)
					{
						importConfig.ExcelSheetNumber = 0;
					}
					else
					{
						importConfig.ExcelSheetNumber = sheetNumber;
					}
				}				
               
                importConfig.ConnectName = lueConnectName.Text;

				AIMManager.SaveImportConfig(ref importConfig);

				//save file type xref
				DataSourceTypeRefFileTypeInfo def = new DataSourceTypeRefFileTypeInfo();
				def.DataSourceTypeRefFileTypeID = 0;                
				def.DataSourceTypeID = (Int32)lueNewDataSourceType.EditValue;
				def.FileTypeID = (Int32)lueNewFileType.EditValue;
				def.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveDataSourceTypeRefFileType(ref def);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				RefreshEverything();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		
		private void backgroundFileRead_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			try
			{
				List<FilesInfo> files = new List<FilesInfo>();
				List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
				FilesInfo file = new FilesInfo();
				FilesInfo fileInternal = new FilesInfo();
				FilesCountInfo filesCount = new FilesCountInfo();
				FileTypeEnum fileType = FileTypeEnum.CSV;

                //9/9/20 - change to use AIMCoreGlobal
                //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;

                DataSourceTypeEnum dataSource = DataSourceTypeEnum.Adisseo;			
				CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Clear();		
				AIMGlobal.importTotalRecordCount = 0;

                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;

                AIMGlobal.importCounter = 0;
				AIMGlobal.importTextCounter = 0;		
				String argument = String.Empty;
				String sql = String.Empty;
				String error = String.Empty;


				//-----------------------------------------------------------
				//GET NEXT BATCHID FROM SAMPLEIMPORTLOG	(Currently this is not being set)			
				ErrorInfo batch = AIMManager.GetSampleImportLogNextBatchID();
				_batchID = batch.RecordID + 1;							
				
				dataSource = (DataSourceTypeEnum)lueDataSourceType.EditValue;
				fileType = (FileTypeEnum)lueFileType.EditValue;				 			
				switch (fileType)
				{
                    //---------------------------------------------------------------------------------------------
                    //SQL DATABASE
                    case FileTypeEnum.SQL:
						switch (dataSource)
						{		  								 						 										
							case DataSourceTypeEnum.Perten:								
								DataExchangeManager.SqlDatabaseProcess (DataSourceTypeEnum.Perten,Convert.ToDateTime(dtStartDate.Text),
																Convert.ToDateTime(dtEndDate.Text),txtSampleCode.Text,_sqlLogID,
																txtConnectNameExisting.Text, ref filesCount);
								break;

                            //2/26/20
                            case DataSourceTypeEnum.Bruker:
                                DataExchangeManager.SqlDatabaseProcess(DataSourceTypeEnum.Bruker, Convert.ToDateTime(dtStartDate.Text),
                                                                Convert.ToDateTime(dtEndDate.Text), txtSampleCode.Text, _sqlLogID,
                                                                txtConnectNameExisting.Text, ref filesCount);
                                break;
                        }
						break;

                    //---------------------------------------------------------------------------------------------
                    //ALL OTHER TYPES (where grid will contain files to choose from)
                    default:   															

						//CHECK OWNER/SUPPLIER SET-UP

                        //1/24/19
						//if (CheckOwnerAndSupplierSetup() == true)
                        if (CheckOwnerAndSupplierSetup((Int32)fileType) == true)
                            {
							if ((Int32)lueDelimiterType.EditValue == (Int32)DelimiterTypeEnum.Comma)
							{
								_delimiterTypeID = (Int32)DelimiterTypeEnum.Comma;
							}
							else if ((Int32)lueDelimiterType.EditValue == (Int32)DelimiterTypeEnum.Semicolon)
							{
								_delimiterTypeID = (Int32)DelimiterTypeEnum.Semicolon;
							}

							//Loop through files in grid						 
							files = (List<FilesInfo>)grdFiles.DataSource;
							if (ValidateBeforeProcess(files) == true)
							{
								filesCount.TotalFilesCount = files.Count;
								for (int i = 0; i < files.Count; i++)
								{									
									if (files[i].FileStatus == true)
									{	 
										file = files[i];
										switch (fileType)
										{
											case FileTypeEnum.API:
												DataExchangeManager.APIProcess(dataSource);
												break;

											case FileTypeEnum.CSV:
												DataExchangeManager.CSVProcess(file,ref filesCount,dataSource,txtBackUpDir.Text.Trim(),
																			_aimOwnerID,_aimOwnerLocationID,_aimsourceID,_aimsourceLocationID,
																			cbCreateSample.Checked,_delimiterTypeID);
												break;

											case FileTypeEnum.XLSX:
												String excelFileExtension = "";
												excelFileExtension = Path.GetExtension(@file.ProcessPathAndName);
												DataExchangeManager.XLSXProcess(file,ref filesCount,dataSource,txtBackUpDir.Text.Trim(),cbCreateSample.Checked,
																			Convert.ToInt32(txtSheetNumber.Text),excelFileExtension,backgroundFileRead);
												break;

											case FileTypeEnum.XML:
                                                
												DataExchangeManager.XMLProcess(dataSource,txtBackUpDir.Text.Trim(),file,ref filesCount,_aimOwnerID,_aimOwnerLocationID,
                                                                            "InitialImport",file.ProcessPathAndName,backgroundFileRead,_batchID);
												break;

											case FileTypeEnum.TXT:
												MessageBox.Show("TXT file type is not yet supported. Contact IT to process TXT files.", "Error...", MessageBoxButtons.OK);
												break;
											default:
												MessageBox.Show("This file type is not yet supported. Please contact IT to request this file type.", "Error...", MessageBoxButtons.OK);
												break;
										}

										backgroundFileRead.ReportProgress(0);
										//TODO allow to stop
										if (_stopFileReading == true)
										{
											_stopFileReading = false;
											break;
										}
										backgroundFileRead.ReportProgress(0);
										if (AIMGlobal.stopPreviewingImport == true)
										{
											AIMGlobal.stopPreviewingImport = false;
											break;
										}
									}
								}
							}
							break;						
						}

					break;
				}
				
				e.Result = filesCount;
                
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void backgroundFileRead_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			try
			{

				if (prgsbarProcessFiles.Value != prgsbarProcessFiles.Maximum)
				{
					prgsbarProcessFiles.Value++;

                    //9/9/20 - change to use AIMCoreGlobal
                    //txtCounter.Text = Convert.ToString(CFCAIM.AIMGlobal.importProgressBarCounter++);
                    txtCounter.Text = Convert.ToString(CFCAIMCore.Global.importProgressBarCounter++);

                }
				else
				{
					prgsbarProcessFiles.Value = 0;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void backgroundFileRead_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			try
			{
				String msg = "";
				String ProcessedResults = "";
				FilesCountInfo filesCount = (FilesCountInfo)e.Result;
				SetControlsForProcess(false);
				btnStartImport.BackColor = SystemColors.Control;
				grdFiles.RefreshDataSource();
				prgsbarProcessFiles.Value = prgsbarProcessFiles.Maximum;

                //9/9/20 - change to use AIMCoreGlobal
                //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;


                if (AIMGlobal.importError == -1)
				{
					tcoProcess.TabPages.Add(tpErrors);

					//populate errors grid (using CFCAIM.AIMGlobal.importNutriQuestHeader)
					grdPreview.DataSource = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
					grdPreviewView.RefreshData();
					grdPreview.Refresh();
					grdPreviewView.ClearSelection();
				}

				else
				{
                    //5/15/20 - changed to use correct ENUM
                    //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.SQL)
                    if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.SQL)

                    {
						//Update ImportSQLSampleLog LastImportDate	  					
						ImportSQLSampleLogInfo log = new ImportSQLSampleLogInfo();
						log.ImportSQLSampleLogID = 0;
						log.ImportConfigID = _configID;
						log.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;
						log.LastImportDate = Convert.ToDateTime(dtEndDate.Text);
						AIMManager.SaveImportSQLSampleLog(ref log);

						//If records exist in MissingSamplesLog, show button to view missing
						CheckMissingSamples();

					}
				}

				object[] args = new object[] { Environment.NewLine, filesCount.TotalFilesCount, filesCount.SuccessCount.ToString(), filesCount.SampleMismatchCount.ToString(), filesCount.WrongFormatCount.ToString() };

				if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX)
				{
					msg = "{1} Files processed.{0}{0}";
					msg += "{2} Samples successfully imported.{0}{0}";
					msg += "{3} Samples with unknown SampleID.{0}";
					msg += "{4} Sample nutrient values with invalid format.{0}";
					msg += "{5} Samples rejected.";
					ProcessedResults = String.Format(msg, Environment.NewLine,
						filesCount.TotalFilesCount,
						filesCount.SuccessCount.ToString(),
						filesCount.SampleMismatchCount.ToString(),
						filesCount.WrongFormatCount.ToString(),
						filesCount.SampleMismatchCount.ToString());
				}
				else if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XML)
				{
					msg = "{1} Files processed.{0}{0}";
					msg += "{2} Samples successfully imported.{0}{0}";
					msg += "{3} Samples rejected.{0}{0}";
					msg += "{4} Files sent to error folder.";
					ProcessedResults = String.Format(msg, Environment.NewLine,
						filesCount.TotalFilesCount,
						filesCount.SampleImportAcceptCount.ToString(),
						filesCount.SampleImportRejectCount.ToString(),
						(filesCount.WrongFormatCount + filesCount.SampleMismatchCount).ToString());
				}

                //5/15/20 - changed to use correct ENUM
                //else if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.SQL)
                else if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.SQL)

                {
					msg = "{1} - Sample Results processed.{0}{0}";
					msg += "{2} - Sample Results successfully imported.{0}{0}";
					msg += "{3} - Sample Results rejected (unknown SampleID).{0}";
					ProcessedResults = String.Format(msg, Environment.NewLine,
						filesCount.TotalFilesCount,
						filesCount.SampleImportAcceptCount.ToString(),
						filesCount.SampleImportRejectCount.ToString());
				}

				else
				{
					msg = "{1} Files processed.{0}{0}";
					msg += "{2} Samples successfully imported.{0}{0}";
					msg += "{3} Files had unknown SampleID.{0}";
					msg += "{4} Files had wrong format.{0}";
					msg += "{5} Files sent to error folder.";
					ProcessedResults = String.Format(msg, Environment.NewLine,
						filesCount.TotalFilesCount,
						filesCount.SuccessCount.ToString(),
						filesCount.SampleMismatchCount.ToString(),
						filesCount.WrongFormatCount.ToString(),
						(filesCount.WrongFormatCount + filesCount.SampleMismatchCount).ToString());
				}
				MessageBox.Show(ProcessedResults, "Results...", MessageBoxButtons.OK);
				
				DataBindLogGrid();
                btnRefreshGrid.PerformClick();

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}


		private void lueFileType_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (_isDataLoaded == false)
				{
					return;
				}
				///LoadGridWithFiles();

				DatabindDelimiter();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueFileType_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateImportConfig();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueDataSourceType_Leave(object sender, EventArgs e)
		{
			try
			{
				UpdateImportConfig();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion


		#region Private Functions
		private void DataBindTemplateAndDataSourceType()
        {
            Int32 saveDataSourceID = 0;
            if (lueDataSourceType.EditValue != null)
            {
                saveDataSourceID = (Int32)lueDataSourceType.EditValue;
            }


            _isDataLoaded = false;
            List<AIMDatabaseInfo> aimDatabase = new List<AIMDatabaseInfo>();
            List<ImportConfigInfo> importConfig = new List<ImportConfigInfo>();
            List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();

            //7/17/20 - why are these set to 0?
            //lueFileType.EditValue = 0;
            //lueDelimiterType.EditValue = 0;
            //lueNewFileType.EditValue = 0;
            //lueNewDelimiterType.EditValue = 0;

            importConfig = AIMManager.GetImportConfig();

            //dataSourceType = AIMManager.GetDataSourceType(true);
            dataSourceType = AIMManager.GetDataSourceTypeByUserDefinedSwitch(false);


            importConfig.Insert(0, new ImportConfigInfo
            {
                ImportConfigID = 0,
                Name = "---Select---"
            }
                                );

            lueTemplateName.Properties.DataSource = importConfig;
            XtraEditors.LoadLookUpEdit(ref lueTemplateName, "ImportConfigID", "Name");
            if (importConfig.Count == 2)
            {
                lueTemplateName.EditValue = 1;
                _onlyOneTemplate = true;
            }
            else
            {
                lueTemplateName.EditValue = 0;
            }


            lueDataSourceType.Properties.DataSource = dataSourceType;
            XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name");
            lueDataSourceType.Properties.Columns["TypeDisplay"].Caption = "Type";
            
            lueNewDataSourceType.Properties.DataSource = dataSourceType;
            XtraEditors.LoadLookUpEdit(ref lueNewDataSourceType, "DataSourceTypeID", "Name");
            lueNewDataSourceType.Properties.Columns["TypeDisplay"].Caption = "Type";

            //7/17/20
            if (saveDataSourceID != 0)
            {
                lueDataSourceType.EditValue = saveDataSourceID;
            }
						
			_isDataLoaded = true;
        }


        private void UpdateImportConfig()
        {
            StackTrace stackTrace = new StackTrace();
            _MainMenu.MessageBoxCFCshow(stackTrace.GetFrame(1).GetMethod().Name);

            if (_isDataLoaded == false)
            {
                return;
            }            
            Int32 sheetNum = 0;

            if (lueTemplateName.EditValue == null)
            {
                return;
            }
            if ((Int32)lueTemplateName.EditValue == 0)
            {
                return;
            }
            ImportConfigInfo importConfig = new ImportConfigInfo();
            CFCUser.Global global = new CFCUser.Global();
            importConfig.CFCUserID = global.CFCLoginUser.CFCUserID;
            importConfig.BackUpDirectory = txtBackUpDir.Text;
            importConfig.ProcessDirectory = txtProcessPath.Text;
            Int32 ID = 0;
            Int32.TryParse(lueTemplateName.EditValue.ToString(), out ID);
            importConfig.ImportConfigID = ID;
            importConfig.DelimiterTypeID = (Int32)lueDelimiterType.EditValue;            
            importConfig.FileTypeID = (Int32)lueFileType.EditValue;            
            importConfig.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
            importConfig.IsCreateSample = cbCreateSample.Checked;
            importConfig.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;

            Int32.TryParse(txtSheetNumber.Text, out sheetNum);

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Excel)
            if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX)

            {
                if (txtSheetNumber.Text.Length == 0)
                {
                    importConfig.ExcelSheetNumber = 0;
                }
                else
                {
                    importConfig.ExcelSheetNumber = sheetNum;
                }
            }
                       
            
            importConfig.ConnectName = lueConnectName.Text.ToString();            

            AIMManager.SaveImportConfig(ref importConfig);
        }

        private void LoadGridWithFiles()
        {

			btnAcceptAllFiles.Visible = false;

			List<FilesInfo> files = new List<FilesInfo>();
            if (txtProcessPath.Text.Trim().Length == 0)
            {
                grdFiles.RefreshDataSource();
                return;
            }

            txtProcessPath.BackColor = Color.White;
            if (Directory.Exists(@txtProcessPath.Text.Trim()) == false)
            {
                MessageBox.Show("Source folder does not exist.", "Warning...", MessageBoxButtons.OK);
                txtProcessPath.BackColor = Color.Yellow;
                txtProcessPath.SelectAll();
                txtProcessPath.Focus();
                return;
			}

			//Allow all files to show.  ?? 4/14/17 - changed this back to allow only files of the file type to show
			//String[] filesArray = Directory.GetFiles(txtProcessPath.Text, "*");

			//case (Int32)ImportFileTypeEnum.Excel:
			//			txtFileType.Text = "XLS";
            			
			String[] filesArray;

			if (lueFileType.Text == "XLSX")     
			{
				filesArray = Directory.GetFiles(txtProcessPath.Text, "*XLS");
			}
			else
			{
				filesArray = Directory.GetFiles(txtProcessPath.Text, "*" + lueFileType.Text);
			}
			
            

            for (int i = 0; i < filesArray.Length; i++)
            {
				files.Add(new FilesInfo()
				{
					FileName = filesArray[i].Substring(filesArray[i].LastIndexOf(@"\") + 1),
					ProcessPath = filesArray[i].Substring(0, filesArray[i].LastIndexOf(@"\"))
					,
					FileStatus = true
                });
            }
            grdFiles.DataSource = files;
            prgsbarProcessFiles.Maximum = files.Count;

            if (txtBackUpDir.Text.Trim().Length == 0)
            {
                return;
            }

            txtBackUpDir.BackColor = Color.White;
            if (Directory.Exists(@txtBackUpDir.Text.Trim()) == false)
            {
                MessageBox.Show("Archive folder does not exist.", "Warning...", MessageBoxButtons.OK);
                txtBackUpDir.BackColor = Color.Yellow;
                txtBackUpDir.SelectAll();
                txtBackUpDir.Focus();
                return;
            }

			btnAcceptAllFiles.Visible = true;
        }

        private void RefreshEverything()
        {
            txtNewConfigName.ResetText();
            txtNewProcessPath.ResetText();
            txtNewBackUpPath.ResetText();
            txtProcessPath.ResetText();
			txtErrorFolder.ResetText();

            txtBackUpDir.ResetText();
            txtSheetNumber.ResetText();
            
			serverName = "";
			userName = "";
			password = "";
			databaseName = "";
			connectName = "";
			sqlAuth = false;

			grdFiles.DataSource = null;
            prgsbarProcessFiles.Maximum = 100;
            prgsbarProcessFiles.Value = 0;
            prgsbarProcessFiles.Visible = false;
            txtCounter.Visible = false;
            btnStopProcess.Visible = false;
            DataBindTemplateAndDataSourceType();
        }

        /// <summary>
        /// Boolean true if you are starting the process
        /// and false if you are done.
        /// </summary>
        /// <param name="Start"></param>
        private void SetControlsForProcess(Boolean Start)
        {
            if (Start == true)
            {
                prgsbarProcessFiles.Visible = true;
                txtCounter.Visible = true;
                btnStopProcess.Visible = true;
                tcoProcess.Enabled = false;
                pnlTopBtns.Enabled = false;
                pnlAdd.Enabled = false;

            }
            else
            {
                prgsbarProcessFiles.Visible = false;
                txtCounter.Visible = false;
                btnStopProcess.Visible = false;
                tcoProcess.Enabled = true;
                pnlTopBtns.Enabled = true;

                if (tcoProcess.TabPages.Contains(tpExisting) == true)
                {
                    pnlAdd.Enabled = false;
                }
                else
                {
                    pnlAdd.Enabled = true;
                }

            }
        }

        //1/24/19 - added file-type
        //private Boolean CheckOwnerAndSupplierSetup()
        private Boolean CheckOwnerAndSupplierSetup(Int32 fileTypeID)
        {

            //1/24/19 - check not needed for CSV imports
            if (fileTypeID != (Int32)FileTypeEnum.CSV)            
            {
                List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
                supplier = AIMManager.GetSupplier(true);
                List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
                aimOwner = AIMManager.GetAIMOwner(true);
                if (supplier.Count == 0 || aimOwner.Count == 0)
                {
                    MessageBox.Show("AIM Supplier and/or Owner have not been set up. They must be set up first before continuing.", "Warning...");
                    return false;
                }
            }

            if (cbCreateSample.Checked == true)
            {
                SampleInfoForProcessResult sampleInfoForProcessResult = new SampleInfoForProcessResult();
                sampleInfoForProcessResult.ShowDialog();
                if (sampleInfoForProcessResult.allValueAreGood == false)
                {
                    _aimOwnerID = 0;
                    _aimOwnerLocationID = 0;
                    _aimsourceID = 0;
                    _aimsourceLocationID = 0;
                    _analyzerMaterialType = String.Empty;
                    return false;
                }
                else
                {
                    _aimOwnerID = sampleInfoForProcessResult.aimOwnerID;
                    _aimOwnerLocationID = sampleInfoForProcessResult.aimOwnerLocationID;
                    _aimsourceID = sampleInfoForProcessResult.sourceID;
                    _aimsourceLocationID = sampleInfoForProcessResult.sourceLocationID;
                    _analyzerMaterialType = sampleInfoForProcessResult.AnalyzerMaterialType;
                }
            }
            return true;
        }
        #endregion

        #region file processing

        private Boolean ValidateBeforeProcess(List<FilesInfo> file)
        {
            String msg = "";

            if (txtProcessPath.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please select the process directory first by clicking the browse button.", "Warning...");
                return false;
            }
            else if (txtBackUpDir.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please select the back up directory first by clicking the browse button.", "Warning...");
                return false;
            }
            else if (file == null)
            {
                MessageBox.Show("There is no file found of selected file type. Please select the process directory first by clicking the browse button.", "Warning...");
                return false;
            }
            else
            {
                if (file.Count == 0)
                {
                    msg = "No files found to import.";
                    msg += Environment.NewLine + Environment.NewLine + "Possible causes: ";
                    msg += Environment.NewLine + Environment.NewLine;
                    msg += "     - The Source folder selected is incorrect, " + Environment.NewLine;
                    msg += "     - No " + lueFileType.Text + " files exist in the Source folder, or " + Environment.NewLine;
                    msg += "     - All files have been rejected in the grid.";
                    msg += Environment.NewLine + Environment.NewLine;
                    msg += "Please make sure the correct Source folder is selected, " + lueFileType.Text + " files exist in the folder, and there are files displayed in the grid.";
                    MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }

        #endregion

        
        private void txtBackUpDir_TextChanged(object sender, EventArgs e)
        {
            txtBackUpDir.BackColor = Color.White;
            CheckImportFields();
        }

        private void lblBackUpDirectory_Click(object sender, EventArgs e)
        {

        }

        private void lueDelimiterType_Leave(object sender, EventArgs e)
        {
            try
            {
                UpdateImportConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            Event form = new Event();

            form.Show();
        }

        private void DatabindFileType()
        {			 			
			List<FileTypeInfo> fileType = new List<FileTypeInfo>();
			fileType = AIMManager.GetFileType(true);
            lueFileType.Properties.DataSource = fileType;            
			XtraEditors.LoadLookUpEdit(ref lueFileType, "FileTypeID", "Name");
			if (fileType.Count > 0)
            {
                lueFileType.EditValue = fileType[0].FileTypeID;
            }
        }


        private void DatabindDelimiter()
        {
            Int32 fileTypeID = 0;
            if (lueFileType.EditValue != null)
            {
                fileTypeID = (Int32)lueFileType.EditValue;
            }
            List<FileTypeRefDelimiterTypeInfo> delimiterType = new List<FileTypeRefDelimiterTypeInfo>();
            delimiterType = AIMManager.GetFileTypeRefDelimiterType(fileTypeID);
            lueDelimiterType.Properties.DataSource = delimiterType;
            XtraEditors.LoadLookUpEdit(ref lueDelimiterType, "DelimiterTypeID", "Description");
            if (delimiterType.Count > 0)
            {
                lueDelimiterType.EditValue = delimiterType[0].DelimiterTypeID;
            }
        }

        private void lueNewDataSourceType_EditValueChanged(object sender, EventArgs e)
        {
            DatabindNewFileType();
            DatabindNewDelimiter();
        }

        private void lueNewFileType_EditValueChanged(object sender, EventArgs e)
        {
            pnlDelimiterSheetNew.Visible = false;
            pnlNewFileImport.Visible = false;
            pnlSQLConnection.Visible = false;
            btnSaveSetting.Visible = false;

			lblSheetNumber.Visible = false;
			txtNewSheetNumber.Visible = false;
			txtNewSheetNumber.Text = "1";

			lblNewDelimiterType.Visible = false;
			lueNewDelimiterType.Visible = false;

			if ((Int32)lueNewFileType.EditValue == 0)
            {
                return;
            }

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueNewFileType.EditValue == (Int32)ImportFileTypeEnum.SQL)
            if ((Int32)lueNewFileType.EditValue == (Int32)FileTypeEnum.SQL)

            {
				btnSaveSetting.Visible = true;
				pnlSQLConnection.Visible = true;				
				pnlSQLConnection.Location = new Point(18, 145);			   
            }

            else
            {
                pnlDelimiterSheetNew.Visible = false;
                pnlNewFileImport.Visible = true;
                btnSaveSetting.Visible = true;

                //1/22/18
                pnlNewFileImport.Location = new Point(3, 186);

                //5/15/20
                //if ((Int32)lueNewFileType.EditValue == (Int32)ImportFileTypeEnum.Excel)
                if ((Int32)lueNewFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueNewFileType.EditValue == (Int32)FileTypeEnum.XLSX)

                {
					pnlDelimiterSheetNew.Visible = true;
					lblSheetNumber.Visible = true;
                    txtNewSheetNumber.Visible = true;
                    
                }

                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueNewFileType.EditValue == (Int32)ImportFileTypeEnum.CSV)
                if ((Int32)lueNewFileType.EditValue == (Int32)FileTypeEnum.CSV)

                {
                    pnlDelimiterSheetNew.Visible = true;
					lblNewDelimiterType.Visible = true;
					lueNewDelimiterType.Visible = true;
                }

                    DatabindNewDelimiter();
            }

        }

        private void DatabindNewFileType()
        {
            Int32 datasourceTypeID = 0;
            if (lueNewDataSourceType.EditValue != null)
            {
                datasourceTypeID = (Int32)lueNewDataSourceType.EditValue;
            }

            List<DataSourceTypeRefFileTypeInfo> fileType = new List<DataSourceTypeRefFileTypeInfo>();
            fileType = AIMManager.GetDataSourceTypeRefFileType(datasourceTypeID);
            fileType.Insert
            (
                0,
                new DataSourceTypeRefFileTypeInfo
                {
                    FileTypeID = 0,
                    FileTypeName = "--Select --"
                }
            );

            lueNewFileType.Properties.DataSource = fileType;
            XtraEditors.LoadLookUpEdit(ref lueNewFileType, "FileTypeID", "FileTypeName");

            if (fileType.Count > 0)
            {
                lueNewFileType.EditValue = fileType[0].FileTypeID;
            }

            Refresh();

        }

        private void DatabindNewDelimiter()
        {
            Int32 fileTypeID = 0;
            if (lueNewFileType.EditValue != null)
            {
                fileTypeID = (Int32)lueNewFileType.EditValue;
            }
            List<FileTypeRefDelimiterTypeInfo> delimiterType = new List<FileTypeRefDelimiterTypeInfo>();
            delimiterType = AIMManager.GetFileTypeRefDelimiterType(fileTypeID);
            lueNewDelimiterType.Properties.DataSource = delimiterType;
            XtraEditors.LoadLookUpEdit(ref lueNewDelimiterType, "DelimiterTypeID", "Description");
            if (delimiterType.Count > 0)
            {
                lueNewDelimiterType.EditValue = delimiterType[0].DelimiterTypeID;
            }
        }

        private void lueDataSourceType_EditValueChanged(object sender, EventArgs e)
        {			
            DatabindDelimiter();
        }

        private void txtProcessPath_TextChanged(object sender, EventArgs e)
        {
            txtProcessPath.BackColor = Color.White;
            CheckImportFields();
        }

        private void fbdSelectedDirectory_HelpRequest(object sender, EventArgs e)
        {

        }

        private void txtSheetNumber_TextChanged(object sender, EventArgs e)
        {
            Int32 n;
            if (txtNewSheetNumber.Text == "0")
            {
                txtNewSheetNumber.Text = "1";
            }
            if (int.TryParse(txtNewSheetNumber.Text, out n) == false)
            {
                txtNewSheetNumber.Text = "1";
            }
        }

        private void tcoProcess_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tcoProcess.SelectedIndex == 0) //import tab
            {
                btnStartImport.Visible = true;
                btnRefresh.Visible = true;
            }
            else if (tcoProcess.SelectedIndex == 1 && tcoProcess.TabPages.Contains(tpErrors) == true)
            {
                btnStartImport.Visible = false;
                btnRefresh.Visible = false;
            }

            if (tcoProcess.SelectedIndex == 1) //new tab
            {
                txtNewConfigName.Focus();
            }

            if (_PreviousTab != null)
            {
                if (tcoProcess.SelectedIndex == 0 && _PreviousTab.Text.ToString() == "Samples Not Imported")
                {
                    LoadGridWithFiles();
                }
            }


        }
                
        
        
        private void lueTemplateName_Click(object sender, EventArgs e)
        {
            lueTemplateName.SelectAll();
        }

        private void lueTemplateName_Enter(object sender, EventArgs e)
        {
            lueTemplateName.SelectAll();
        }

        private void lueDataSourceType_Click(object sender, EventArgs e)
        {
            lueDataSourceType.SelectAll();
        }

        private void lueDataSourceType_Enter(object sender, EventArgs e)
        {
            lueDataSourceType.SelectAll();
        }

        private void lueFileType_Click(object sender, EventArgs e)
        {
            lueFileType.SelectAll();
        }

        private void lueFileType_Enter(object sender, EventArgs e)
        {
            lueFileType.SelectAll();
        }

        private void lueDelimiterType_Click(object sender, EventArgs e)
        {
            lueDelimiterType.SelectAll();
        }

        private void lueDelimiterType_Enter(object sender, EventArgs e)
        {
            lueDelimiterType.SelectAll();
        }

        private void lueNewDataSourceType_Enter(object sender, EventArgs e)
        {
            lueNewDataSourceType.SelectAll();
        }

        private void lueNewDataSourceType_Click(object sender, EventArgs e)
        {
            lueNewDataSourceType.SelectAll();
        }

        private void lueNewFileType_Enter(object sender, EventArgs e)
        {
            lueNewFileType.SelectAll();
        }

        private void lueNewFileType_Click(object sender, EventArgs e)
        {
            lueNewFileType.SelectAll();
        }

        private void lueNewDelimiterType_Click(object sender, EventArgs e)
        {
            lueNewDelimiterType.SelectAll();
        }

        private void lueNewDelimiterType_Enter(object sender, EventArgs e)
        {
            lueNewDelimiterType.SelectAll();
        }
                
        private void lueOwner_Enter(object sender, EventArgs e)
        {
            lueOwner.SelectAll();
        }

        private void lueOwnerLocation_Enter(object sender, EventArgs e)
        {
            lueOwnerLocation.SelectAll();
        }

        private void lueOwner_Click(object sender, EventArgs e)
        {
            lueOwner.SelectAll();
        }

        private void lueOwnerLocation_Click(object sender, EventArgs e)
        {
            lueOwnerLocation.SelectAll();
        }

        private void lueOwner_EditValueChanged(object sender, EventArgs e)
        {
            _aimOwnerID = (Int32)lueOwner.EditValue;
            ChangeDropdownBackcolor(lueOwner);
            DataBindOwnerLocation();
            CheckImportFields();
        }
        private void DataBindLueOwner()
        {
            _aimOwner = AIMManager.GetAIMOwner(true);
            _aimOwner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,
                    OwnerCode = "--Select--"
                }
            );
            lueOwner.Properties.DataSource = _aimOwner;
            XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerCode");            
            if (_aimOwner.Count == 2)
            {
                //selecting value if there is only one
                lueOwner.EditValue = _aimOwner[1].AIMOwnerID;
            }
            else
            {
                lueOwnerLocation.EditValue = 0;
            }
        }
        private void DataBindOwnerLocation()
        {
            Int32 OwnerLocationID = 0;
            Int32 OwnerID = 0;
            if (lueOwnerLocation.EditValue != null)
            {
                Int32.TryParse(lueOwner.EditValue.ToString(), out OwnerID);
            }

            _aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID);
            _aimOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
                    LocationCode = "--Select--"
                }
            );
            lueOwnerLocation.Properties.DataSource = _aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "AIMOwnerLocationID", "LocationCode");
            lueOwnerLocation.Properties.Columns["LocationCode"].Visible = true;
            lueOwnerLocation.EditValue = OwnerLocationID;

            if (_aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                lueOwnerLocation.EditValue = _aimOwnerLocation[1].AIMOwnerLocationID;
            }
            else
            {
                lueOwnerLocation.EditValue = OwnerLocationID;
            }

        }

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        private void DataBindLueSupplier()
        {
            _aimSupplier = AIMManager.GetSupplier(true);
            _aimSupplier.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,
                    AIMSupplierCode = "--Select--"
                }
            );
            lueSupplier.Properties.DataSource = _aimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "AIMSupplierCode");
            if (_aimSupplier.Count == 2)
            {
                //selecting value if there is only one
                lueSupplier.EditValue = _aimSupplier[1].AIMSupplierID;
            }
            else
            {
                lueSupplier.EditValue = 0;
            }
        }

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        private void DataBindLueSupplierLocation()
        {
            Int32 SupplierLocationID = 0;
            Int32 SupplierID = 0;
            if (lueSupplierLocation.EditValue != null)
            {
                Int32.TryParse(lueSupplier.EditValue.ToString(), out SupplierID);
            }

            _aimSupplierLocation = AIMManager.GetAIMSupplierLocationForSupplier(SupplierID);
            _aimSupplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    AIMSupplierLocationCode = "--Select--"
                }
            );
            lueSupplierLocation.Properties.DataSource = _aimSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode");
            lueSupplierLocation.Properties.Columns["AIMSupplierLocationCode"].Visible = true;
            lueSupplierLocation.EditValue = SupplierLocationID;

            if (_aimSupplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueSupplierLocation.EditValue = _aimSupplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueSupplierLocation.EditValue = SupplierLocationID;
            }

        }

        private void ChangeDropdownBackcolor(DevExpress.XtraEditors.LookUpEdit lue)
        {
            if (lue.Enabled == true)
            {
                lue.BackColor = Color.White;
                if ((Int32)lue.EditValue == 0)
                {
                    lue.BackColor = SystemColors.Info;
                }
            }
            else
            {
                lue.BackColor = Color.LightGray;
            }
        }
               

        private void lueOwnerLocation_EditValueChanged(object sender, EventArgs e)
        {
            _aimOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
            ChangeDropdownBackcolor(lueOwnerLocation);
            CheckImportFields();
        }

        private void btnSelectExisting_Click(object sender, EventArgs e)
        {
            tcoProcess.Visible = true;
            btnRefresh.Visible = true;
            			
            //hide new
            if (tcoProcess.TabPages.Contains(tpNewSetting) == true)
            {
                tcoProcess.TabPages.Remove(tpNewSetting);
            }
            //show existing
            if (tcoProcess.TabPages.Contains(tpExisting) == false)
            {
                tcoProcess.TabPages.Add(tpExisting);
            }
            //hide errors
            if (tcoProcess.TabPages.Contains(tpErrors) == true)
            {
                tcoProcess.TabPages.Remove(tpErrors);
            }

            //show log

            //TODO -- removed log page and commented this until coding is done
            tcoProcess.TabPages.Remove(tpLog);
            /*
            if (tcoProcess.TabPages.Contains(tpLog) == false)
            {
                tcoProcess.TabPages.Add(tpLog);
            }
            */

            lueTemplateName.ShowPopup();
            CheckImportFields();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            //hide existing
            if (tcoProcess.TabPages.Contains(tpExisting) == true)
            {
                tcoProcess.TabPages.Remove(tpExisting);
            }
            //hide errors
            if (tcoProcess.TabPages.Contains(tpErrors) == true)
            {
                tcoProcess.TabPages.Remove(tpErrors);
            }
            //show new
            if (tcoProcess.TabPages.Contains(tpNewSetting) == false)
            {
                tcoProcess.TabPages.Add(tpNewSetting);
            }
            //hide log
            if (tcoProcess.TabPages.Contains(tpLog) == true)
            {
                tcoProcess.TabPages.Remove(tpLog);
            }

            CheckImportFields();

            //These 3 have to be at the end of the function.
            tcoProcess.Visible = true;
            btnRefresh.Visible = false;
            btnStartImport.Visible = false;
        }

        private void tcoProcess_DrawItem(object sender, DrawItemEventArgs e)
        {
            //if (e.Index == tcoProcess.SelectedIndex)
            if (e.Index == 0 || e.Index == 1)

            {
                e.Graphics.DrawString(tcoProcess.TabPages[e.Index].Text,
                    new Font(tcoProcess.Font, FontStyle.Bold),
                    Brushes.Black,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
            else
            {
                e.Graphics.DrawString(tcoProcess.TabPages[e.Index].Text,
                    tcoProcess.Font,
                    Brushes.Red,
                    new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
            }
        }

        private void setGlobalRequiredField(Int32 msg)
        {
            switch (msg)
            {
                case (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref:
                    AIMGlobal.internalRequiredField = "SUPPLIER";
                    break;
                case (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref:
                    AIMGlobal.internalRequiredField = "SUPPLIERLOCATION";
                    break;
                case (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref:
                    AIMGlobal.internalRequiredField = "OWNER";
                    break;
                case (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref:
                    AIMGlobal.internalRequiredField = "OWNERLOCATION";
                    break;
                case (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref:
                    AIMGlobal.internalRequiredField = "MATERIAL";
                    break;
                default:
                    break;
            }
        }

        private void repHyperMessage_Click(Object sender, EventArgs e)
        {
            String sampleCode = "";
            Int32 message = 0;
            Int32 ID = 0;

            AIMGlobal.internalDataSourceName = lueDataSourceType.Text.ToString();

            sampleCode = (String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSampleCodePreview);
            message = (Int32)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSpecificMessage);
            ID = Convert.ToInt32(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colInternalImportValidateSampleID));

            //--------------------------------------------------------------------------
            if (message != 0)
            {
                AIMGlobal.internalDataSourceTypeID = (Int32)lueDataSourceType.EditValue;
                AIMGlobal.internalImportError = message;
                setGlobalRequiredField(message);

                //--------------------------------------------------------------------------
                if (message == (Int32)SampleImportSpecificMessageDetail.CreateSample)
                {
                    MessageBox.Show("On Import, a new sample will be created for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode, "Create Sample...", MessageBoxButtons.OK);
                    AIMGlobal.internalImportError = 0;
                    return;
                }

                //--------------------------------------------------------------------------
                if (message == (Int32)SampleImportSpecificMessageDetail.Rejected)
                {
                    MessageBox.Show("Sample and results will not be imported.", "Rejected...", MessageBoxButtons.OK);
                    AIMGlobal.internalImportError = 0;
                    return;
                }

                //--------------------------------------------------------------------------
                this.Hide();


                //Refresh _header and set global so it can be used on ExternalAllCrossRefUpdate form				
                validate();

                ImportInternalCrossReferenceUpdate crossRef = new ImportInternalCrossReferenceUpdate();

                if (message == (Int32)SampleImportSpecificMessageDetail.MissingInternalCrossReference)
                {
                    
                    //10/6/21 - commented, not needed
                    //crossRef.WindowState = FormWindowState.Normal;                 

                    crossRef.ShowDialog();
                    validate();
                    //grdPreviewView.RefreshData();
                    //grdPreview.Refresh();

                    grdPreview.DataSource = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
                    grdPreviewView.RefreshData();

                }

                this.Show();
                AIMGlobal.internalImportError = 0;
            }
        }

        private void timer1_Tick(Object sender, EventArgs e)
        {
            if (btnStartImport.BackColor == SystemColors.Control)
            {
                btnStartImport.BackColor = SystemColors.Info;
            }
            else
            {
                btnStartImport.BackColor = SystemColors.Control;
            }
        }

        private void button1_Click(Object sender, EventArgs e)
        {
        }

        private void btnRefreshPreview_Click(Object sender, EventArgs e)
        {
            AIMGlobal.SplashScreenShow();
            validate();
            AIMGlobal.SplashScreenHide();
			
        }

        private void backgroundValidate_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (prgsbarProcessFiles.Value != prgsbarProcessFiles.Maximum)
                {
                    prgsbarProcessFiles.Value++;

                    //9/9/20 - change to use AIMCoreGlobal
                    //txtCounter.Text = Convert.ToString(CFCAIM.AIMGlobal.importProgressBarCounter++);
                    txtCounter.Text = Convert.ToString(CFCAIMCore.Global.importProgressBarCounter++);
                }
                else
                {
                    prgsbarProcessFiles.Value = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundValidate_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                grdPreview.RefreshDataSource();
                prgsbarProcessFiles.Value = prgsbarProcessFiles.Maximum;

                //9/9/20 - change to use AIMCoreGlobal
                //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;

                if (AIMGlobal.importError == -1)
                {

                    if (tcoProcess.TabPages.Contains(tpExisting) == false)
                    {
                        tcoProcess.TabPages.Add(tpErrors);
                    }
                    grdPreview.DataSource = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
                    grdPreviewView.RefreshData();
                    grdPreview.Refresh();
                    grdPreviewView.ClearSelection();
                }
                else
                {
                    SetControlsForProcess(false);
                    btnImportSamples.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void validate()
        {
            //ClearCounts();
            //progressBarPreviewImport.Value = 0;
            //SetControlsForProcess(true);
            if (backgroundValidate.IsBusy == false)
            {
                backgroundValidate.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Validate is busy. Please try later.", "Warning...");
            }
        }

        private void backgroundValidate_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                FilesInfo file = new FilesInfo();
                List<FilesInfo> files = new List<FilesInfo>();
                FilesCountInfo filesCount = new FilesCountInfo();

                //9/9/20 - change to use AIMCoreGlobal
                //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;

                DataSourceTypeEnum dataSource = DataSourceTypeEnum.Adisseo;
                dataSource = (DataSourceTypeEnum)lueDataSourceType.EditValue;

                //foreach (var item in CFCAIM.AIMGlobal.internalFiles)
                //{
                //	files.Add(new FilesInfo()
                //	{
                //		FileName = item.FileName,
                //		ProcessPath = item.ProcessPath

                //	});
                //}								

                DataExchangeManager.XMLProcess(dataSource,
                                                txtBackUpDir.Text.Trim(),
                                                file,
                                                ref filesCount,
                                                _aimOwnerID,
                                                _aimOwnerLocationID,
                                                "Validate",
                                                "",
                                                backgroundValidate,
                                                _batchID);

                backgroundValidate.ReportProgress(0);
                if (_stopFileReading == true)
                {
                    _stopFileReading = false;
                    return;
                }
                backgroundValidate.ReportProgress(0);
                if (AIMGlobal.stopPreviewingImport == true)
                {
                    AIMGlobal.stopPreviewingImport = false;
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tcoProcess_Deselected(Object sender, TabControlEventArgs e)
        {
            _PreviousTab = e.TabPage;
        }

        private void tcoProcess_Selected(Object sender, TabControlEventArgs e)
        {
            _CurrentTab = e.TabPage;
        }

        private void btnImportSamples_Click(Object sender, EventArgs e)
        {
            try
            {

                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Import Samples?", "Warning...", MessageBoxButtons.YesNoCancel);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                SetControlsForProcess(true);
                if (backgroundImport.IsBusy == false)
                {
                    backgroundImport.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Import worker is busy. Please try later.", "Warning...");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundImport_ProgressChanged(Object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            try
            {
                if (prgsbarProcessFiles.Value != prgsbarProcessFiles.Maximum)
                {
                    prgsbarProcessFiles.Value++;

                    //9/9/20 - change to use AIMCoreGlobal
                    //txtCounter.Text = Convert.ToString(CFCAIM.AIMGlobal.importProgressBarCounter++);
                    txtCounter.Text = Convert.ToString(CFCAIMCore.Global.importProgressBarCounter++);
                }
                else
                {
                    prgsbarProcessFiles.Value = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundImport_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                FilesInfo file = new FilesInfo();
                FilesCountInfo filesCount = new FilesCountInfo();

                //9/9/20 - change to use AIMCoreGlobal
                //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;

                DataSourceTypeEnum dataSource = DataSourceTypeEnum.Adisseo;
                dataSource = (DataSourceTypeEnum)lueDataSourceType.EditValue;


                DataExchangeManager.XMLProcess(dataSource,
                                                txtBackUpDir.Text.Trim(),
                                                file,
                                                ref filesCount,
                                                _aimOwnerID,
                                                _aimOwnerLocationID,
                                                "Import",
                                                "",
                                                backgroundImport,
                                                _batchID);

                backgroundImport.ReportProgress(0);
                if (_stopFileReading == true)
                {
                    _stopFileReading = false;
                    return;
                }
                backgroundImport.ReportProgress(0);
                if (AIMGlobal.stopPreviewingImport == true)
                {
                    AIMGlobal.stopPreviewingImport = false;
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundImport_RunWorkerCompleted(Object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                FilesCountInfo filesCount = (FilesCountInfo)e.Result;
                SetControlsForProcess(false);
                btnStartImport.BackColor = SystemColors.Control;

                grdPreview.DataSource = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
                grdPreviewView.RefreshData();
                grdPreview.Refresh();
                grdPreviewView.ClearSelection();


                prgsbarProcessFiles.Value = prgsbarProcessFiles.Maximum;

                //9/9/20 - change to use AIMCoreGlobal
                //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
                CFCAIMCore.Global.importProgressBarCounter = 0;

                List<FilesInfo> clearFiles = new List<FilesInfo>();
                grdFiles.DataSource = clearFiles;


                btnImportSamples.Enabled = false;
                if (tcoProcess.TabPages.Contains(tpErrors) == true)
                {
                    tcoProcess.TabPages.Remove(tpErrors);
                }


                AIMGlobal.SplashScreenHide();
                MessageBox.Show("Import Complete");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAcceptAll_Click(Object sender, EventArgs e)
        {
            if (lueTemplateName.EditValue == null)
            {
                return;
            }

            //change all to accepted status			
            var results = from g in CFCAIM.AIMGlobal.internalImportValidateSampleHeader
                          select g;
            foreach (var item in results)
            {
                item.Status = (Int32)SampleImportStatus.Accepted;



                if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.Rejected)
				{                    
					item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
					item.Message = "Create New Sample";                    
				}

            }

            //change any samples where the message is not null back to rejected			
            CheckRejectedMessages(CFCAIM.AIMGlobal.internalImportValidateSampleHeader);

            ShowHideImportButton();
            grdPreviewView.FocusedRowHandle = 0;
            grdPreview.DataSource = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
            grdPreviewView.RefreshData();

        }

        private void CheckRejectedMessages(List<InternalImportValidateSampleInfo> samples)
        {
            //get sample-id's from resultsPreview where message is not null
            //update all records for those sampleID's, status = 2					
            var rejected_samples = from g in samples
                                   where g.Message != null && g.Message != "" &&
                                            (g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists &&
                                             g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist &&
                                             g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.CreateSample &&
                                             g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.MergeResults)
                                   select g;
            foreach (var item in rejected_samples)
            {
                var sameSampleID = from i in samples
                                   where i.InternalImportValidateSampleID == item.InternalImportValidateSampleID
                                   select i;
                foreach (var sampleid in sameSampleID)
                {
                    sampleid.Status = (Int32)SampleImportStatus.Cancelled;
                }
            }
        }

        private void repbtnAccept_Click(Object sender, EventArgs e)
        {
            if (lueTemplateName.EditValue == null)
            {
                return;
            }
            if (grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colMessagePreview) != null)
            {
                Int32 specificMessage = 0;
                specificMessage = (Int32)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSpecificMessage);
                if (specificMessage == (Int32)SampleImportSpecificMessageDetail.DateError)
                {
                    MessageBox.Show("This sample cannot be accepted because of a data error. See Message column.", "Error...", MessageBoxButtons.OK);
                    return;
                }
                if (specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingInternalCrossReference)
                {
                    MessageBox.Show("This sample cannot be accepted because a cross-reference is missing. See Message column.", "Error...", MessageBoxButtons.OK);
                    return;
                }

                if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colMaterial)).ToString() == "MISSING")
                {
                    MessageBox.Show("This sample cannot be accepted because the MATERIAL is missing.", "Error...", MessageBoxButtons.OK);
                    return;
                }

                if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSource)).ToString() == "MISSING")
                {
                    MessageBox.Show("This sample cannot be accepted because the SOURCE is missing.", "Error...", MessageBoxButtons.OK);
                    return;
                }
                if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSourceLocation)).ToString() == "MISSING")
                {
                    MessageBox.Show("This sample cannot be accepted because the SOURCE LOCATION is missing.", "Error...", MessageBoxButtons.OK);
                    return;
                }

            }

            SetAcceptReject((Int32)(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colInternalImportValidateSampleID)), (Int32)SampleImportStatus.Accepted);
            ShowHideImportButton();
            grdPreview.DataSource = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
            grdPreviewView.RefreshData();


        }
        private void SetAcceptReject(Int32 validateID, Int32 status)
        {
            List<InternalImportValidateSampleInfo> info = new List<InternalImportValidateSampleInfo>();
            info = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
            var validateRecords = from g in info
                                  where g.InternalImportValidateSampleID == validateID
                                  select g;
            foreach (var item in validateRecords)
            {
                item.Status = status;

                if (status == (Int32)SampleImportStatus.Accepted)
                {
                    if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists ||
                        item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.Rejected)
                    {
                        item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
                        item.Message = "Create New Sample";
                    }
                }
                else if (status == (Int32)SampleImportStatus.Cancelled)
                {
                    if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MissingInternalCrossReference)
                    {
                    }
                    else if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.DateError)
                    {
                    }
                    else if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist)
                    {
                        item.Status = 2;
                    }
                    else
                    {
                        item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.Rejected;
                        item.Message = "Rejected";
                    }
                }
            }
        }

        private void repbtnReject_Click(Object sender, EventArgs e)
        {

            if (lueTemplateName.EditValue == null)
            {
                return;
            }
            if (grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colInternalImportValidateSampleID) != null)
            {
                SetAcceptReject((Int32)(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colInternalImportValidateSampleID)), (Int32)SampleImportStatus.Cancelled);
                ShowHideImportButton();
                grdPreview.DataSource = CFCAIM.AIMGlobal.internalImportValidateSampleHeader;
                grdPreviewView.RefreshData();
            }
        }
        private void ShowHideImportButton()
        {
            //Make sure there are "accepted" samples to export before exporting											

            btnImportSamples.Enabled = false;

            var results = from g in CFCAIM.AIMGlobal.internalImportValidateSampleHeader
                          where g.Status == 1
                          select new
                          {
                              g.InternalImportValidateSampleID,
                              g.Status
                          };
            if (results.Count() != 0)
            {
                btnImportSamples.Enabled = true;
            }
        }

        private void btnCancelAll_Click(Object sender, EventArgs e)
        {
            if (lueTemplateName.EditValue == null)
            {
                return;
            }
            var samples = from g in CFCAIM.AIMGlobal.internalImportValidateSampleHeader
                          select g;
            foreach (var item in samples)
            {
                item.Status = (Int32)SampleImportStatus.Cancelled;

                if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.CreateSample ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MergeResults)
                {
                    item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.Rejected;
                    item.Message = "Rejected";
                }
            }
            ShowHideImportButton();
            grdPreviewView.RefreshData();
        }

        private void btnClearStatus_Click(Object sender, EventArgs e)
        {
            if (lueTemplateName.EditValue == null)
            {
                return;
            }
            var samples = from g in CFCAIM.AIMGlobal.internalImportValidateSampleHeader
                          select g;
            foreach (var item in samples)
            {
                item.Status = 0;
            }
            grdPreviewView.RefreshData();
        }

        private void DataBindLogGrid()
        {
            List<SampleImportLogInfo> log = new List<SampleImportLogInfo>();
            log = AIMManager.GetSampleImportLog();
            grdLog.DataSource = log;


        }

        #region Master Events
        private void grdLog_ViewRegistered(Object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            GridView view;
            view = e.View as GridView;
        }

        #endregion

        private void grdLogView_MasterRowEmpty(Object sender, MasterRowEmptyEventArgs e)
        {
            SampleImportLogInfo header = new SampleImportLogInfo();
            SampleImportLogDetailInfo detail = new SampleImportLogDetailInfo();
            header = (SampleImportLogInfo)grdLogView.GetRow(e.RowHandle);
        }

        private void grdLogView_MasterRowExpanded(Object sender, CustomMasterRowEventArgs e)
        {
            GridView masterView = (GridView)sender;
            GridView detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detailView.BestFitColumns();
        }

        private void grdLogView_MasterRowGetChildList(Object sender, MasterRowGetChildListEventArgs e)
        {
            SampleImportLogInfo header = new SampleImportLogInfo();
            header = (SampleImportLogInfo)grdPreviewView.GetRow(e.RowHandle);
            //e.ChildList = header[e.RowHandle].DetailLog;

            //Int32 index = header.IndexOf(header);
            //e.ChildList = header[index].DetailImport;
        }

        private void grdLogView_MasterRowGetRelationCount(Object sender, MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void grdLogView_MasterRowGetRelationName(Object sender, MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "";
        }

		private void btnResize_Click(Object sender, EventArgs e)
		{		
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}
				
		private void ImportData_SizeChanged(Object sender, EventArgs e)
		{			
			btnResize.Visible = true;
		}

		private void btnRefreshGrid_Click(Object sender, EventArgs e)
		{
			LoadGridWithFiles();
		}

		private void btnHelp_Click(Object sender, EventArgs e)
		{
			try
			{
				helpSystemImports = new HelpSystemImports();				
				helpSystemImports.ShowDialog();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void btnMissingSamples_Click(Object sender, EventArgs e)
		{							
			ImportSqlSampleMissing importMissing;
            AIMGlobal.open_form(importMissing = new ImportSqlSampleMissing());
        }

		private void timer2_Tick(Object sender, EventArgs e)
		{
			if (btnSaveSetting.BackColor == SystemColors.Control)
			{
				btnSaveSetting.BackColor = SystemColors.Info;
			}
			else
			{
				btnSaveSetting.BackColor = SystemColors.Control;
			}
		}

		private void btnEditConnection_Click(Object sender, EventArgs e)
		{
            AIMGlobal.sqlNewOrEdit = "Edit";
			AIMGlobal.importConfigID = _configID;
			AIMGlobal.sqlDatabaseConnectionName = txtConnectNameExisting.Text;
			Hide();
            AIMGlobal.open_form(importSqlConnections = new ImportSqlConnections());
        }

		        
        private void btnAddConnection_Click(Object sender, EventArgs e)
        {
            if (txtNewConfigName.Text == "")
            {
                MessageBox.Show("Please enter a Template Name.", "Message...", MessageBoxButtons.OK);
                return;
            }

            AIMGlobal.sqlNewOrEdit = "New";
            AIMGlobal.sqlDatabaseConnectionName = "";
            AIMGlobal.systemTemplateName = txtNewConfigName.Text;
            AIMGlobal.systemTemplateDataSourceTypeID = (Int32)lueNewDataSourceType.EditValue;
            Hide();
            AIMGlobal.open_form(importSqlConnections = new ImportSqlConnections());

        }

		private void btnAcceptAllFiles_Click(Object sender, EventArgs e)
		{
			if (lueTemplateName.EditValue == null)
			{
				return;
			}

			List<FilesInfo> files = new List<FilesInfo>();
			files = (List<FilesInfo>)grdFiles.DataSource;
			if (files != null)
			{
				foreach (var file in files)
				{
					if (file.FileStatus == true)
					{
						file.FileStatus = false;
					}
					else
					{
						file.FileStatus = true;
					}													
					
				}
				grdFilesView.RefreshData();
			}
		}

		private void btnRejectAllFiles_Click(Object sender, EventArgs e)
		{
			if (lueTemplateName.EditValue == null)
			{
				return;
			}

			List<FilesInfo> files = new List<FilesInfo>();
			files = (List<FilesInfo>)grdFiles.DataSource;
			if (files != null)
			{
				foreach (var file in files)
				{
					file.FileStatus = false;
				}
				grdFilesView.RefreshData();
			}
		}

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        private void ChangeTextBackcolor(TextBox tb)
        {
            
            tb.BackColor = Color.White;

            if (tb.Text.All(char.IsDigit))
            {}   //Numeric
            else
            {
                //not numeric
                tb.BackColor = SystemColors.Info;
            }    

            if (tb.Text == String.Empty || tb.Text == "0")
            {
                tb.BackColor = SystemColors.Info;
            }           
        }

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        private void lueSupplier_EditValueChanged(object sender, EventArgs e)
        {
            _aimsourceID = (Int32)lueSupplier.EditValue;
            ChangeDropdownBackcolor(lueSupplier);
            DataBindLueSupplierLocation();
            CheckImportFields();
        }

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        private void lueSupplierLocation_EditValueChanged(object sender, EventArgs e)
        {
            _aimsourceLocationID = (Int32)lueSupplierLocation.EditValue;
            ChangeDropdownBackcolor(lueSupplierLocation);
            CheckImportFields();
        }

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        private void txtHeaderRow_TextChanged(object sender, EventArgs e)
        {
            ChangeTextBackcolor(txtHeaderRow);
        }

        //8/15/22 - added for Romer Labs. Decided Romer Labs should be moved to user-defined imports, but left Supplier/Loc code here. Could be used in future.
        private void txtDetailRow_TextChanged(object sender, EventArgs e)
        {
            ChangeTextBackcolor(txtDetailRow);
        }
    }
}
 