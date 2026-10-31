using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System.IO;
using CFCAIM.DataExchange;
using DevExpress.XtraGrid.Views.Grid;
using CFCAIM.Maintenance;
using CFCAIM.CrossReference;
using CFCAIM.Imports;
using System.Threading;
using CFCAIMCore;
using System.Windows.Data;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Timers;
using System.Windows.Threading;

namespace CFCAIM
{
    public partial class ImportDataUserDefined : Form
	{
		#region Globals
		CFCUser.Global userGlobal = new CFCUser.Global();
		MapWizard mapWizard;
		MainMenu _MainMenu = (MainMenu)Application.OpenForms["MainMenu"];
               

        Boolean _isDataLoaded = false;
		Int32 _selectedDefinitionID = 0;
		
		List<AIMMaterialInfo> _aimMaterial = new List<AIMMaterialInfo>();
		List<AIMSupplierInfo> _aimSupplier = new List<AIMSupplierInfo>();
		List<AIMSupplierLocationInfo> _aimSupplierLocation = new List<AIMSupplierLocationInfo>();
		List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();
		List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
		UserDefinedDataSourceDefinitionInfo _selectedDefinition = new UserDefinedDataSourceDefinitionInfo();
		UserDefinedDataSourceDefinitionInfo _dataSource = new UserDefinedDataSourceDefinitionInfo();
		List<SampleImportLogInfo> _log = new List<SampleImportLogInfo>();		
		List<ImportHeaderInfo> _header = new List<ImportHeaderInfo>();
		List<ImportHeaderInfo> _importResultsAll = new List<ImportHeaderInfo>();
				
		Boolean _blnMaterial = false;
		Boolean _blnOwner = false;
		Boolean _blnOwnerLocation = false;
		Boolean _blnSupplier = false;
		Boolean _blnSupplierLocation = false;

		DevExpress.XtraEditors.LookUpEdit _lueOwner;
		DevExpress.XtraEditors.LookUpEdit _lueOwnerLocation;
		DevExpress.XtraEditors.LookUpEdit _lueSupplier;
		DevExpress.XtraEditors.LookUpEdit _lueSupplierLocation;
		
		Int32 _lastTabIndex = 0;
		Boolean _okayToPreview = false;
				
		//If anything is changed under the "Select Import" tab, this global is set "ON" 
		//so that the "Preview Import" tab code is fired again; if nothing is changed on the 
		//"Select Import" tab, when changing to the "Preview Import" tab, the preview code is skipped.
		Boolean _previewImport = false;
        
        //10/4/19
        String _strFolderMoveError = "";

        //10/20/20
        //Boolean _templateChange = false;

        //1/3/20
        AIM aimMaintenance;

        //8/6/21
        String _strArchivePath = String.Empty;

        //3/9/23
        static int n = 0;

        #endregion

        public ImportDataUserDefined()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
		}
		private void ImportSampleUserDefined_Load(object sender, EventArgs e)
		{

            //Using this for testing - persistent repositories allows multiple grids to use the same combobox (or whatever control)
            //PersistentRepository persistentRepository1 = new PersistentRepository;
            //RepositoryItemComboBox riCombo = new RepositoryItemComboBox();
            //riCombo.Items.AddRange(new string[] { "Cash", "Visa", "Master", "Am.Express" });
            //persistentRepository1.Items.Add(riCombo);


            DataBindlue();
			DataBindLogGrid();
            DataBindSupplierLocationlueNew();           //12/5/19
            SetToolTips();

			AIMGlobal.lastImportHeaderID = 0;  //used when multiple samples are exported
			AIMGlobal.showAutoGenSampleMsg = true;  //used when multiple samples are exported

            //2/25/21
            AIMGlobal.lastImportSampleCode = String.Empty;  //used when multiple samples are imported           

            AIMGlobal.importDataSourceTypeID = 0;
			AIMGlobal.importDataBindDataSourceType = "";            
            AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = false;                                                                      //8/20/18

            txtAnalyzerDataSourceName.Text = String.Empty;
			txtAnalyzerDataSourceCode.Text = String.Empty;

			tabControl1.TabPages.Remove(tpLog);
            tabControl1.TabPages.Remove(tpDataSource);         //12/4/19 - added this, no longer used??

            if (AIMGlobal.analyzerDataSourceDefinitionID != 0)
			{
				pnlAdd.Visible = true;
				_selectedDefinitionID = AIMGlobal.analyzerDataSourceDefinitionID;
				lueTemplateName.EditValue = _selectedDefinitionID;
				AIMGlobal.analyzerDataSourceDefinitionID = 0;
			}
           			
			WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
			

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
				ttip.SetToolTip(btnCancelPreview, "Stop Process");
				ttip.SetToolTip(btnStartImport, "Import Samples");
				ttip.SetToolTip(btnRefreshPreview, "Validate Samples");
				ttip.SetToolTip(btnAcceptAll, "Accept All");
				ttip.SetToolTip(btnCancelAll, "Reject All");
				ttip.SetToolTip(btnNext, "Preview Import Data");
				ttip.SetToolTip(btnPrior, "Review Selected Import");
				ttip.SetToolTip(btnSelectExisting, "Select Existing Template");
				ttip.SetToolTip(btnAddNew, "Add New Template");
				ttip.SetToolTip(btnEditMap, "Edit Import Map");
				ttip.SetToolTip(btnRefreshGrid, "Refresh Grid");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
				ttip.SetToolTip(btnAcceptAllFiles, "Mark All Files (Accepted/Rejected)");

                //1/3/20
                ttip.SetToolTip(hyperOwner, "Open AIM Owner Maintenance");                
                ttip.SetToolTip(hyperSupplier, "Open AIM Supplier Maintenance");
                ttip.SetToolTip(btnUpdateOwnerSupplier, "Update Owners/Suppliers");


            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#region Buttons
		private void btnStartImport_Click(object sender, EventArgs e)
		{
			try
			{

                String msg = "";				
                Boolean paramsNotSetUp = false;
                String paramList = "";                
				Int32 returnVal = 0;
               
               
                msg = ChecksBeforeImport(_selectedDefinitionID, _header);                                                                       //7/30/19
                if (msg != "")                                                                                                                  //7/30/19
                {                                                                                                                               //7/30/19
                    MessageBox.Show(msg, "Warning...");                                                                                         //7/30/19
                    return;                                                                                                                     //7/30/19
                }                                                                                                                               //7/30/19

                //-------------------------------------------------------------------
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Import Samples?", "Warning...", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                //-------------------------------------------------------------------    
                UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
                def = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
                if (def.ImportType == (Int32)EnumImportType.UpdateResultsOnly || def.ImportType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)        
                {								
					returnVal = CheckAimDropdownsBeforePreview();
					if (returnVal != 0)
					{						
						return;
					}
				}

                //-------------------------------------------------------------------
                List<FilesInfo> files = new List<FilesInfo>();
                files = (List<FilesInfo>)grdFiles.DataSource;
				if (CheckFiles(files) != true)
				{					
					return;
				}

				//-------------------------------------------------------------------
				if (PreImportChecks() == -1)
				{
					tabControl1.SelectedIndex = 0;										
					return;
				}

                //8/6/21
                _strArchivePath = txtArchivePath.Text;

                //-------------------------------------------------------------------
                //3/30/18 - for QC samples, loop thru detail and make sure QC parameter ID's exist for parameters on import file.                
                if (def.IsQualityControl == true)
                {

                    #region Landscape parameter list
                    if (def.Orientation == true)  //Landscape
                    {                       
                        var qcDetail = from det in _header[0].DetailImport select det;   //_header[0] because just need to get first row of nutrient headers
                        foreach (var item in qcDetail)
                        {
                            //Get parameterID
                            SampleQualityParameterInfo param = new SampleQualityParameterInfo();
                            param = AIMManager.GetQualityParameterName(item.AnalyzerNutrientCode, def.DataSourceTypeID);
                            if (param == null)
                            {
                                paramsNotSetUp = true;
                                paramList += item.AnalyzerNutrientCode + ", ";
                            }
                        }
                        if (paramList.Length > 3)
                        {                            
                            paramList = paramList.Substring(0, paramList.Length - 2);  //remove last comma
                        }
                    }
                    #endregion

                    #region Portrait parameter list
                    if (def.Orientation == false)     //portrait
                    {
                        //TODO - 4/6/18 - add "portrait" section here and remove code under AddResults function (dated 4/6/18).
                    }
                    #endregion

                    if (paramsNotSetUp == true)
                    {
                        msg = "These Quality Control Parameters are not set up: ";
                        msg += Environment.NewLine + Environment.NewLine + paramList;
                        msg += Environment.NewLine + Environment.NewLine + "Skip these sample results and continue with Import?";                        
                        dr = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);                       
                        if (dr != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                }


                AIMGlobal.SplashScreenShow();
                //-------------------------------------------------------------------------------
                
                timer1.Stop();


                progressBarPreviewImport.Value = 0;
                AIMGlobal.txtProgressMessage = "Importing Samples ... ";                                                                        //8/5/19

                SetControlsForProcess(true);
				if (backgroundImport.IsBusy == false)
				{
					backgroundImport.RunWorkerAsync();
				}
				else
				{
					MessageBox.Show("Validate is busy. Please try later.", "Warning...");
				}	

			}
			catch (Exception ex)
			{
                //TODO test this -- to get line# where error occurred
                //Int32 lineError = GetLineNumber(ex);
                //MessageBox.Show("Import Start, Line# - " + lineError.ToString() + " " + ex.Message);                
                MessageBox.Show("Import Start - " + ex.Message);
            }			
		}

        //TODO test this -- to get line# where error occurred
        public int GetLineNumber(Exception ex)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return lineNumber;
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
				List<FilesInfo> clearFiles = new List<FilesInfo>();
				List<FilesInfo> clearResults = new List<FilesInfo>();
				grdFiles.DataSource = clearFiles;
				grdPreview.DataSource = clearResults;                
                DataBindlue();
				SetToolTips();
				txtSourcePath.Text = "";
				txtArchivePath.Text = "";
				txtDataSource.Text = "";
				txtFileType.Text = "";
                txtErrorFolder.Text = "";

                //12/5/19
                //rbYesDups.Checked = false;
                //rbNoDups.Checked = true;
                txtAllowDuplicates.Text = "";
                                
                grdAnalyzerDataSource.DataSource = clearResults;
                txtAnalyzerDataSourceName.Text = String.Empty;
				txtAnalyzerDataSourceCode.Text = String.Empty;
                				
				timer1.Stop();
				btnCrossReference.Visible = false;
				ClearCounts();

                txtOrientation.Text = "";
                txtImportType.Text = "";
                txtQualityControlResults.Text = "";
                txtDelim.Text = "";
                txtDecimalSep.Text = "";                                                                                                        //4/2/19

                grdPreviewHeader.DataSource = clearResults;                                                                                     //7/29/19
                btnClearFilter.PerformClick();                  //12/9/19

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnBrowseProcessPath_Click(object sender, EventArgs e)
		{
			try
			{				
				_previewImport = true;

				DialogResult dr = new DialogResult();
				if (Directory.Exists(txtSourcePath.Text) == true)
				{
					fbdSelectedDirectory.SelectedPath = txtSourcePath.Text;
				}
				fbdSelectedDirectory.ShowDialog();

				if (txtSourcePath.Text != fbdSelectedDirectory.SelectedPath)
				{
					dr = MessageBox.Show("Permanently save this Source path?", "Warning...", MessageBoxButtons.YesNo);
					if (dr == DialogResult.Yes)
					{
						UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();
						definition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
						definition.UserDefinedDataSourceDefinitionID = (Int32)lueTemplateName.EditValue;
						definition.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						definition.ProcessDirectory = fbdSelectedDirectory.SelectedPath;                        
						ErrorInfo error = new ErrorInfo();
						error = AIMManager.SaveDataSourceUserDefinition(ref definition);
						if (error.ErrorNumber > 0)
						{
							return;
						}
					}
				}

				txtSourcePath.Text = fbdSelectedDirectory.SelectedPath;
				LoadGridWithFiles();                                
				UpdateImportDefinition();

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

				_previewImport = true;

				DialogResult dr = new DialogResult();
				if (Directory.Exists(txtArchivePath.Text) == true)
				{
					fbdSelectedDirectory.SelectedPath = txtArchivePath.Text;
				}
				fbdSelectedDirectory.ShowDialog();
				if (txtArchivePath.Text != fbdSelectedDirectory.SelectedPath)
				{
					dr = MessageBox.Show("Permanently save this Archive path?", "Warning...", MessageBoxButtons.YesNo);
					if (dr == DialogResult.Yes)
					{
						UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();
						definition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);						
						definition.UserDefinedDataSourceDefinitionID = (Int32)lueTemplateName.EditValue;
						definition.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						definition.BackupDirectory = fbdSelectedDirectory.SelectedPath;
						ErrorInfo error = new ErrorInfo();
						error = AIMManager.SaveDataSourceUserDefinition(ref definition);
						if (error.ErrorNumber > 0)
						{
							return;
						}
					}
				}


				txtArchivePath.Text = fbdSelectedDirectory.SelectedPath;


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
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdPreview, "Import Sample Data - " + lueTemplateName.Text.ToString());
                CFCAIMCore.Global.previewGridResults(grdPreview, "Import Sample Data - " + lueTemplateName.Text.ToString());
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region DataBind	
		private void DataBindLogGrid()
		{
			if (_selectedDefinition != null)
			{
				Int32 rowNum = 0;
				Int32.TryParse(grdLogView.FocusedRowHandle.ToString(), out rowNum);
				_log = AIMManager.GetSampleImportLog(_selectedDefinition.UserDefinedDataSourceDefinitionID);
				grdLog.DataSource = _log;				

			}
		}
		private void DataBindlue()
		{ 			
			List<UserDefinedDataSourceDefinitionInfo> templateName = new List<UserDefinedDataSourceDefinitionInfo>();

			//-----------------------------------------------------------------		
			templateName = AIMManager.GetAllUserDefinedDataSourceDefinition();
			templateName.Insert
			(
				0,
				new UserDefinedDataSourceDefinitionInfo
				{
					UserDefinedDataSourceDefinitionID = 0,
					ImportFileName = "--Select--"
				}
			);
			lueTemplateName.Properties.DataSource = templateName;
			XtraEditors.LoadLookUpEdit(ref lueTemplateName, "UserDefinedDataSourceDefinitionID", "ImportFileName");

			//-----------------------------------------------------------------
                        
			DataBindLueMaterial();
			DataBindLueOwner();
			DataBindLueSupplier();

            //11/11/20
            DataBindNewLueOwner();
            DataBindNewLueSupplier();

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
                  , OwnerName = "--Select--"                //1/2/20
				}
			);
			lueOwner.Properties.DataSource = _aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerCode");

            //1/2/20
            lueOwnerName.Properties.DataSource = _aimOwner;                                      
            XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName");

            //11/11/20 - moved to separate function below
            //lueNewOwnerSupplier.Properties.DataSource = _aimOwner;
            //XtraEditors.LoadLookUpEdit(ref lueNewOwnerSupplier, "AIMOwnerID", "OwnerCode", "OwnerName");
            //lueNewOwnerSupplier.Properties.Columns["OwnerName"].Caption = "AIM Owner Name";
        }

        //11/11/20 - separated this from DataBindLueOwner above
        private void DataBindNewLueOwner()
        {
            _aimOwner = AIMManager.GetAIMOwner(true);
            _aimOwner.Insert
            (
                0,
                new AIMOwnerInfo
                {
                    AIMOwnerID = 0,
                    OwnerCode = "--Select--",
                    OwnerName = "--Select--"
                }
            );            
            lueNewOwnerSupplier.Properties.DataSource = _aimOwner;
            XtraEditors.LoadLookUpEdit(ref lueNewOwnerSupplier, "AIMOwnerID", "OwnerCode", "OwnerName");
            lueNewOwnerSupplier.Properties.Columns["OwnerName"].Caption = "AIM Owner Name";
        }


        private void DataBindLueSupplier()
		{			
			_aimSupplier = AIMManager.GetSupplier(true);
			_aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					SupplierName = "--Select--"
				}
			);
			lueSupplier.Properties.DataSource = _aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "SupplierName");

            //11/11/20 - moved to separate function below
            //lueNewOwnerSupplier.Properties.DataSource = _aimSupplier;
            //XtraEditors.LoadLookUpEdit(ref lueNewOwnerSupplier, "AIMSupplierID", "SupplierName");
            //lueNewOwnerSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

        }

        //11/11/20 - separated this from DataBindLueOwner above
        private void DataBindNewLueSupplier()
        {
            _aimSupplier = AIMManager.GetSupplier(true);
            _aimSupplier.Insert
            (
                0,
                new AIMSupplierInfo
                {
                    AIMSupplierID = 0,
                    SupplierName = "--Select--"
                }
            );            
            lueNewOwnerSupplier.Properties.DataSource = _aimSupplier;
            XtraEditors.LoadLookUpEdit(ref lueNewOwnerSupplier, "AIMSupplierID", "SupplierName");
            lueNewOwnerSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

        }

        //12/5/19
        private void DataBindSupplierLocationlueNew()
        {
            Int32 supplier = 0;

            List<AIMSupplierLocationInfo> aimNewSupplierLocation = new List<AIMSupplierLocationInfo>();
            if (lueNewOwnerSupplier.EditValue != null)
            {
                Int32.TryParse(lueNewOwnerSupplier.EditValue.ToString(), out supplier);
            }
            aimNewSupplierLocation = AIMManager.GetSupplierLocationForSupplierForUtility(supplier);
            aimNewSupplierLocation.Insert
            (
                0,
                new AIMSupplierLocationInfo
                {
                    AIMSupplierLocationID = 0,
                    AIMSupplierLocationCode = "--Select--",
                    SupplierLocationName = "--Select--"
                }
            );
            lueNewOwnerSupplierLocation.Properties.DataSource = aimNewSupplierLocation;
            XtraEditors.LoadLookUpEdit(ref lueNewOwnerSupplierLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
            lueNewOwnerSupplierLocation.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

            if (aimNewSupplierLocation.Count == 2)
            {
                //selecting value if there is only one
                lueNewOwnerSupplierLocation.EditValue = aimNewSupplierLocation[1].AIMSupplierLocationID;
            }
            else
            {
                lueNewOwnerSupplierLocation.EditValue = 0; // aimNewSupplierLocation; 11/14/24
            }
        }

        //12/5/19
        private void DataBindOwnerLocationlueNew()
        {
            Int32 owner = 0;

            List<AIMOwnerLocationInfo> aimNewOwnerLocation = new List<AIMOwnerLocationInfo>();
            if (lueNewOwnerSupplier.EditValue != null)
            {
                Int32.TryParse(lueNewOwnerSupplier.EditValue.ToString(), out owner);
            }
            aimNewOwnerLocation = AIMManager.GetOwnerLocationForOwnerForUtility(owner);
            aimNewOwnerLocation.Insert
            (
                0,
                new AIMOwnerLocationInfo
                {
                    AIMOwnerLocationID = 0,
                    LocationCode = "--Select--",
                    LocationName = "--Select--"
                }
            );
            lueNewOwnerSupplierLocation.Properties.DataSource = aimNewOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueNewOwnerSupplierLocation, "AIMOwnerLocationID", "LocationCode", "LocationName");
            lueNewOwnerSupplierLocation.Properties.Columns["LocationName"].Caption = "AIM Owner Location Name";

            if (aimNewOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                lueNewOwnerSupplierLocation.EditValue = aimNewOwnerLocation[1].AIMOwnerLocationID;
            }
            else
            {
                lueNewOwnerSupplierLocation.EditValue = 0; // aimNewOwnerLocation;  11/14/24
            }
        }


        private void DataBindLueMaterial()
		{
			if (_dataSource.DataSourceTypeID == 0)
			{
				_aimMaterial.Clear();
			}
			else 
			{
				_aimMaterial = AIMManager.GetAIMMaterial(true);
			}
			_aimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--"
				}
			);
			
			lueMaterial.Properties.DataSource = _aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterial, "AIMMaterialID", "AIMMaterialCode");
			
		}
		private void DataBindSourceLocation()
		{			
			Int32 supplierLocationID = 0;
			Int32 supplierID = 0;
			if (lueSupplierLocation.EditValue != null)
			{
				Int32.TryParse(lueSupplier.EditValue.ToString(), out supplierID);
			}
			_aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID);

			_aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,

					SupplierLocationName = "--Select--"
				}
			);
			lueSupplierLocation.Properties.DataSource = _aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "AIMSupplierLocationID", "SupplierLocationName");
			lueSupplierLocation.Properties.Columns["SupplierLocationName"].Visible = true;
            lueSupplierLocation.EditValue = 0; // supplierLocationID;       /11/14/24

            if (_aimSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueSupplierLocation.EditValue = _aimSupplierLocation[1].AIMSupplierLocationID;
			}

            //11/14/24 - commented
			//else
			//{
            //    lueSupplierLocation.EditValue =  supplierLocationID;
			//}
			
		}
		private void DataBindOwnerLocation()
		{			
			//Int32 OwnerLocationID = 0;        //11/14/24 - commented
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
					LocationCode = "--Select--",
                    LocationName = "--Select --"
                }
			);
			lueOwnerLocation.Properties.DataSource = _aimOwnerLocation;            
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "AIMOwnerLocationID", "LocationCode", "LocationName");
            lueOwnerLocation.Properties.Columns["LocationCode"].Visible = true;
            lueOwnerLocation.EditValue = 0; // OwnerLocationID; //11/14/24

            //1/2/20
            lueOwnerLocationName.Properties.DataSource = _aimOwnerLocation;
            XtraEditors.LoadLookUpEdit(ref lueOwnerLocationName, "AIMOwnerLocationID", "LocationName", "LocationCode");

            if (_aimOwnerLocation.Count == 2)
			{
				//selecting value if there is only one
				lueOwnerLocation.EditValue = _aimOwnerLocation[1].AIMOwnerLocationID;
			}

            //11/14/24 - commented
			//else
			//{
			//	lueOwnerLocation.EditValue = OwnerLocationID;
			//}

		}
		#endregion

		#region Import Process

		private void CopyLues()
		{
			//Tab control is disabled so that user can't press buttons while import is working.
			//But when tab control is disabled, the lookupedits on tab1 are also disabled and not available in the import functions.
			//So .... copying the values here to send to the import

			_lueOwner = lueOwner;
			_lueOwnerLocation = lueOwnerLocation;
			_lueSupplier = lueSupplier;
			_lueSupplierLocation = lueSupplierLocation;
		}
		private void SetControlsForProcess(Boolean Start)
		{
            try
            {                             		

			if (Start == true)
			{
                                
                tabControl1.Enabled = false;
				pnlButtons.Enabled = false;
				gbProcess.Enabled = false;
                pnlArrows.Enabled = false;          //12/5/19
				btnCrossReference.Enabled = false;

				grpPreviewProgress.Visible = true;				
				btnCancelPreview.Visible = true;				
				pnlTopBtns.Enabled = false;
				lueTemplateName.Enabled = false;
				btnPreviewGridResults.Enabled = false;
				tabControl1.TabPages.Remove(tpSelectImport);
				btnExpand.Enabled = false;

				btnAcceptAll.Enabled = false;
				btnCancelAll.Enabled = false;				
                grdPreviewView.Columns["Message"].OptionsColumn.ReadOnly = true;
                grdPreviewHeaderView.Columns["Message"].OptionsColumn.ReadOnly = true;                                                          //7/29/19

                //11/9/20
                grdPreviewView.Columns["UpdateOwnerSupplier"].Visible = false;                
                grdPreviewView.Columns["UpdateOwnerSupplier"].Width = 48;
                
                


                }
			else
			{
				
				tabControl1.Enabled = true;
				pnlButtons.Enabled = true;
				gbProcess.Enabled = true;
                pnlArrows.Enabled = true;               //12/5/19
                btnCrossReference.Enabled = true;
				grpPreviewProgress.Visible = false;				
				btnCancelPreview.Visible = false;				
				pnlTopBtns.Enabled = true;
				lueTemplateName.Enabled = true;
				btnPreviewGridResults.Enabled = true;
				tabControl1.TabPages.Insert(0, tpSelectImport);
				btnExpand.Enabled = true;

                btnAcceptAll.Enabled = true;
				btnCancelAll.Enabled = true;			   
                grdPreviewView.Columns["Message"].OptionsColumn.ReadOnly = false;
                grdPreviewHeaderView.Columns["Message"].OptionsColumn.ReadOnly = false;                                                         //7/29/19
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("SetControlsForProcess - " + ex.Message);
            }
        }
		private Boolean CheckFiles(List<FilesInfo> file)
		{
            try
            {

            
			    if (file != null)
			    {
				    if (txtSourcePath.Text.Trim().Length == 0)
				    {
					    MessageBox.Show("Please select the source directory first by clicking the browse button.", "Warning...");
					    tabControl1.SelectedIndex = 0;
					    txtSourcePath.Focus();
					    return false;
				    }
				    else if (txtArchivePath.Text.Trim().Length == 0)
				    {
					    MessageBox.Show("Please select the archive directory first by clicking the browse button.", "Warning...");
					    tabControl1.SelectedIndex = 0;
					    txtArchivePath.Focus();

					    return false;
				    }
                   
                    else if(Directory.Exists(txtArchivePath.Text.Trim()) == false)
                    {
                        MessageBox.Show("Archive folder does not exist.", "Warning...");
                        txtArchivePath.SelectAll();
                        return false;
                    }

				    else if (file == null)
				    {
					    MessageBox.Show("There are no files found with the selected file type. Please select the process directory first by clicking the browse button.", "Warning...");
					    tabControl1.SelectedIndex = 0;
					    txtSourcePath.Focus();
					    return false;
				    }
				    else
				    {
					    if (file.Count == 0)
					    {
						    MessageBox.Show("No files found with the selected type in selected folder or all files have been removed from the grid.", "Warning...");
						    tabControl1.SelectedIndex = 0;
						    txtSourcePath.Focus();
						    return false;
					    }
				    }
			    }
			    return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("CheckFiles - " + ex.Message);
                return false;
            }
        }
		 
		private string InitField(string fieldVal)
		{
			if (fieldVal == null)
			{
				return "";
			}
			else
			{
				return fieldVal;
			}
		}
		private Int32 CheckNumeric(Int32 fieldVal)
		{
			if (fieldVal == 0)
			{
				return 0;
			}
			else
			{
				return fieldVal;
			}
		}

		private void UpdateImportDefinition()
		{

			if (_isDataLoaded == false)
			{
				return;
			}			

			if (lueTemplateName.EditValue == null)
			{
				return;
			}
			if ((Int32)lueTemplateName.EditValue == 0)
			{
				return;
			}
			//Int32 fileTypeID = 0;
			//Int32 dataSourceTypeID = 0;
			//ImportConfigInfo importConfig = new ImportConfigInfo();
			//CFCUser.Global global = new CFCUser.Global();
			//importConfig.CFCUserID = global.CFCLoginUser.CFCUserID;
			//importConfig.BackUpDirectory = txtBackUpDir.Text;
			//importConfig.ProcessDirectory = txtProcessPath.Text;
			//Int32 ID = 0;
			//Int32.TryParse(lueTemplateName.EditValue.ToString(), out ID);
			//importConfig.ImportConfigID = ID;
			//Int32.TryParse(lueFileType.EditValue.ToString(), out fileTypeID);
			//importConfig.FileTypeID = (FileTypeEnum)fileTypeID;
			//Int32.TryParse(lueDataSourceType.EditValue.ToString(), out dataSourceTypeID);
			//importConfig.DataSourceTypeID = (DataSourceTypeEnum)dataSourceTypeID;
			//importConfig.IsCreateSample = cbCreateSample.Checked;
			//importConfig.CFCUserID = _UserGlobal.CFCLoginUser.CFCUserID;

			//AIMManager.SaveImportConfig(ref importConfig);
		}
		private void btnStopProcess_Click(object sender, EventArgs e)
		{
			try
			{
				SetControlsForProcess(false);				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
				
		private Int32 PreImportChecks()
		{
            try
            {
                            
                //make sure something has been selected for the Priority
                String msg = "";
                AIMDatabaseInfo priority = new AIMDatabaseInfo();
                priority = AIMManager.GetAIMDatabase(AIMCoreGlobal.AIMDatabaseID);
                if (priority != null)
                {
                    if (priority.AIMPriorityUsedValueID == 0)
                    {
                        msg = "Before files can be imported, the AIM Priority Used Value must be selected.";                                        
                        msg += Environment.NewLine + Environment.NewLine + "To set the AIM Priority Used Value:";
                        msg += Environment.NewLine + Environment.NewLine + "     Go to the Main Menu > Set Up > Formulation";
                        msg += Environment.NewLine + "     Click the button 'Select Existing Formulation Connection'";
                        msg += Environment.NewLine + "                  and then select the priority from the dropdown.";                    
                        MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
                        tabControl1.SelectedTab = tpSelectImport;                    
                        return -1;
                    }
                }

                Int32 importType = 0;
                importType = GetImportType(_selectedDefinitionID);
            
                List<FilesInfo> files = new List<FilesInfo>();
			    files = (List<FilesInfo>)grdFiles.DataSource;
			    DialogResult dr = new DialogResult();
			    Int32 defID = 0;

			    if (lueTemplateName.EditValue != null)
			    {
				    Int32.TryParse(lueTemplateName.EditValue.ToString(), out defID);
			    }

			    if (defID == 0)
			    {
				    return 0;
			    }					
			    //Get count from Entity where fileStatus = true
			    Int32 acceptedFiles = 0;
			    var query_acceptedFiles = from g in files where g.FileStatus == true										  
									      select new
										      {
											      g.FileName,
											      g.FileStatus																				  
										      };			
			    if (query_acceptedFiles.Count() == 0)
			    {										
				    dr = MessageBox.Show("No files have been accepted.", "Information ...");				
				    return -1;
			    }

			    if (files != null && files.Count > 1 && acceptedFiles > 1)
			    {                    
				    dr = MessageBox.Show("There are multiple Excel files to be imported. The template called " + lueTemplateName.Text.ToString() + " will be used for all files.  Do you want to continue?", "Warning ...", MessageBoxButtons.YesNo);

                    //12/5/19 - added -- dialogResult wasn't here!
                    if (dr != DialogResult.Yes)
                    {
                        return -1;
                    }
                }

                //12/5/19 - moved here (from lueTemplate editValueChanged event)
                if (txtAllowDuplicates.Text == "Yes")
                {
                   
                    dr = MessageBox.Show("Allow duplicate sample codes to be imported?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr != DialogResult.Yes)
                    {
                        return -1;
                    }
                }

                //---------------------------------------------------------------------
                if (lueTemplateName.ItemIndex == 0)
			    {
				    MessageBox.Show("Please select template.", "Error...", MessageBoxButtons.OK);
				    tabControl1.SelectedTab = tpSelectImport;
				    lueTemplateName.ShowPopup();
				    return -1;
			    }
			    else if (CheckFiles(files) != true)
			    {
				    return -1;
			    }
			    else if (lueTemplateName.EditValue != null && defID == 0)
			    {
				    return -1;
			    }
			    else if (files == null || files.Count == 0)
			    {
				    MessageBox.Show("Please select import files.", "Error...", MessageBoxButtons.OK);
				    tabControl1.SelectedTab = tpSelectImport;
				    txtSourcePath.Focus();
				    return -1;
			    }
			    else if (files.Count > 1 && dr == DialogResult.No)
			    {
				    tabControl1.SelectedTab = tpSelectImport;
				    return -1;
			    }               
                //12/5/19 - currently, not used.  User must set "allow dups" in Edit Template
                ////         else if (importType == (Int32)EnumImportType.AddNewSamplesAndResults && rbYesDups.Checked == false && rbNoDups.Checked == false)
                ////         {
                //// MessageBox.Show("Please select Allow Duplicates, Yes or No", "Warning...");
                //// gbAllowDuplicates.Focus();
                //// tabControl1.SelectedTab = tpSelectImport;
                //// return -1;					
                ////}
                	
                
                else if ((lueOwner.Enabled == true && (Int32)lueOwner.EditValue == 0) || (lueOwnerLocation.Enabled == true && (Int32)lueOwnerLocation.EditValue == 0) || (lueSupplier.Enabled == true && (Int32)lueSupplier.EditValue == 0) || (lueSupplierLocation.Enabled == true && (Int32)lueSupplierLocation.EditValue == 0))
                {		
                    //12/9/19		                    
                    //if (importType == (Int32)EnumImportType.AddNewSamplesAndResults || importType == (Int32)EnumImportType.UpdateResultsOnly)
                    if (importType == (Int32)EnumImportType.AddNewSamplesAndResults || importType == (Int32)EnumImportType.AddNewSamplesAndResultsORUpdateResults

                        //8/15/22
                        || importType == (Int32)EnumImportType.RomerResults)      
                    {
					    MessageBox.Show("Please select AIM required fields.", "Error...", MessageBoxButtons.OK);
					    pnlRequiredFields.Focus();
					    tabControl1.SelectedTab = tpSelectImport;
					    return -1;
				    }
				    else
				    {
					    return 0;
				    }
			    }
			    else
			    {                    
				    return 0;
			    }
                //---------------------------------------------------------------------

            }
            catch (Exception ex)
            {
                MessageBox.Show("PreImportChecks - " + ex.Message);
                return -1;
            }
        }


		
		#endregion
		
		#region AcceptReject
				
		private void repbtnAccept_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			Int32 missingXref = 0;
			Int32 active = 0;
			
							
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

				if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colMaterial)).ToString() == "MISSING")
				{
					MessageBox.Show("This sample cannot be accepted because the MATERIAL is missing.", "Error...", MessageBoxButtons.OK);
					return;
				}
				if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colOwner)).ToString() == "MISSING")
				{
					MessageBox.Show("This sample cannot be accepted because the OWNER is missing.", "Error...", MessageBoxButtons.OK);
					return;
				}
				if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colOwnerLocation)).ToString() == "MISSING")
				{
					MessageBox.Show("This sample cannot be accepted because the OWNER LOCATION is missing.", "Error...", MessageBoxButtons.OK);
					return;
				}
				if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSource)).ToString() == "MISSING")
				{
					MessageBox.Show("This sample cannot be accepted because the SUPPLIER is missing.", "Error...", MessageBoxButtons.OK);
					return;
				}
				if (((String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSourceLocation)).ToString() == "MISSING")
				{
					MessageBox.Show("This sample cannot be accepted because the SUPPLIER LOCATION is missing.", "Error...", MessageBoxButtons.OK);
					return;
				}
			}

                        
            Int32.TryParse(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSpecificMessage).ToString(), out active);
            if (active == (Int32)SampleImportSpecificMessageDetail.InactiveMaterial)
            {
                MessageBox.Show("This sample cannot be accepted because the Analyzer MATERIAL is INACTIVE.", "Error...", MessageBoxButtons.OK);
                return;
            }
            if (active == (Int32)SampleImportSpecificMessageDetail.InactiveOwner)
            {
                MessageBox.Show("This sample cannot be accepted because the Analyzer OWNER is INACTIVE.", "Error...", MessageBoxButtons.OK);
                return;
            }
            if (active == (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation)
            {
                MessageBox.Show("This sample cannot be accepted because the Analyzer OWNER LOCATION is INACTIVE.", "Error...", MessageBoxButtons.OK);
                return;
            }
            if (active == (Int32)SampleImportSpecificMessageDetail.InactiveSupplier)
            {
                MessageBox.Show("This sample cannot be accepted because the Analyzer SUPPLIER is INACTIVE.", "Error...", MessageBoxButtons.OK);
                return;
            }
            if (active == (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation)
            {
                MessageBox.Show("This sample cannot be accepted because the Analyzer SUPPLIER LOCATION is INACTIVE.", "Error...", MessageBoxButtons.OK);
                return;
            }


            Int32.TryParse(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSpecificMessage).ToString(), out missingXref);
			if (missingXref == (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref ||
				missingXref == (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref ||
				missingXref == (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref ||
				missingXref == (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref ||
				missingXref == (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref	||
				missingXref == (Int32)SampleImportSpecificMessageDetail.MissingDatasourceCrossReference)
			{
				MessageBox.Show("This sample cannot be accepted because CROSS REFERENCES are missing.", "Error...", MessageBoxButtons.OK);
				return;
			}

			

			SetAcceptReject((Int32)(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colImportHeaderID)), (Int32)SampleImportStatus.Accepted);
            
			grdPreview.DataSource = _header;			
			grdPreviewView.RefreshData();
			UpdateCounts();
		
		}

		
		private void repbtnReject_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (lueTemplateName.EditValue == null)
			{
				return;
			}
			if (grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colImportHeaderID) != null)
			{
				SetAcceptReject((Int32)(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colImportHeaderID)), (Int32)SampleImportStatus.Cancelled);

                grdPreview.DataSource = _header;
				grdPreviewView.RefreshData();
				UpdateCounts();
			
			}
		}

        
        private static Int32 GetImportType(Int32 defID)
        {
            Int32 rtn = 0;
            UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
            if (defID != 0)
            {
                def = AIMManager.GetDataSourceUserDefinition(defID);
                rtn = def.ImportType;                
            }
            return rtn;
        }

		private void SetAcceptReject(Int32 importID, Int32 status)
		{            
            Int32 importType = 0;
            importType = GetImportType(_selectedDefinitionID);

            var headerRecords = from g in _header
								where g.ImportHeaderID == importID
								select g;
			foreach (var item in headerRecords)
			{

                //6/13/17 (Task 381) - if item is set to ignore, and user clicks the "reject" button, leave status as "ignore"
                if (item.Status != (Int32)SampleImportStatus.Ignore)
                {

                    item.Status = status;

                    if (status == (Int32)SampleImportStatus.Accepted)
                    {
                        if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists ||
                            item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.Rejected)
                        {
                            
                            if (importType == (Int32)EnumImportType.AddNewSamplesAndResults)                
                            {
                                item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
                                item.Message = "Create New Sample";
                            }
                            else if (importType == (Int32)EnumImportType.UpdateResultsOnly)                 
                            {
                                item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.MergeResults;
                                item.Message = "Add Results";
                            }
                                                        
                            else if (importType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)                                                //4/1/19
                            {                                                                                                                   //4/1/19
                                item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.AddResultsAndUpdateHeaderFields;                  //4/1/19
                                item.Message = "Add Results and Update Header Fields";                                                          //4/1/19
                            }                                                                                                                   //4/1/19

                            else if (importType == (Int32)EnumImportType.UpdateHeaderOnly)                                                       //7/26/19
                            {                                                                                                                   //7/26/19
                                item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.UpdateHeaderFields;                               //7/26/19
                                item.Message = "Update Header Fields";                                                                          //7/26/19                                                               
                            }                                                                                                                   //7/26/19

                            else
                            {
                                item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateAndOrMergeResults;
                                item.Message = "Create New Sample/Add Results";
                            }
                        }
                        else if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist)
                        {
                            
                            if (importType == (Int32)EnumImportType.AddNewSamplesAndResults)                 
                            {
                                item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
                                item.Message = "Create New Sample";
                            }

                            else
                            {
                                item.Status = 2;
                            }
                        }
                    }

                    else if (status == (Int32)SampleImportStatus.Cancelled)
                    {
                        if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.DateError)
                        {
                        }
                        else if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist)
                        {
                            item.Status = 2;
                        }
                                              
                        else if (
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveMaterial ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveOwner ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveSupplier ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation ||
                        
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref ||
                                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref)
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
		}


		private void UpdateCounts()
		{			
			txtTotalErrors.Text = CountErrors().ToString();
			txtTotalWarnings.Text = CountWarnings().ToString();
			txtTotalSamples.Text = CountSamples().ToString();
			txtAccepted.Text = CountAccepted().ToString();            
            txtIgnored.Text = CountIgnored().ToString();			

			if (Convert.ToInt32(txtTotalErrors.Text) > 0)
			{
				txtTotalErrors.ForeColor = Color.White;	
			}				

			if (Convert.ToInt32(txtAccepted.Text) > 0)
			{
				txtAccepted.BackColor = Color.LightGreen;		
			}
			else
			{
				txtAccepted.BackColor = Color.WhiteSmoke;
			}			
		}

		private void ClearCounts()
		{
			txtTotalSamples.Text = String.Empty;
			txtTotalErrors.Text = String.Empty;
			txtTotalWarnings.Text = String.Empty;
			txtAccepted.Text = String.Empty;            
            txtIgnored.Text = String.Empty;

		}


		private void btnAcceptAll_Click(object sender, EventArgs e)
		{
			if (lueTemplateName.EditValue == null)
			{
				return;
			}
                       
            Int32 importType = 0;
            importType = GetImportType(_selectedDefinitionID);

			//change all to accepted status			
			var samples = from g in _header
						  select g;
			foreach (var item in samples)
			{
				item.Status = (Int32)SampleImportStatus.Accepted;
                                
                if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveMaterial ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveOwner ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveSupplier ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation )
                {
                    item.Status = (Int32)SampleImportStatus.Ignore;
                }                
                	   
				
				if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists ||					
					item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.Rejected || 
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.CreateAndOrMergeResults)
				{
						
                    
                    if (importType == (Int32)EnumImportType.AddNewSamplesAndResults)                             
                    {
						item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
						item.Message = "Create New Sample";
					}
					else if (importType == (Int32)EnumImportType.UpdateResultsOnly)                              
                    {
						item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.MergeResults;
						item.Message = "Add Results";
					}

                    //4/1/19
                    else if (importType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)         
                    {
                        item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.AddResultsAndUpdateHeaderFields;
                        item.Message = "Add Results and Update Header Fields";
                    }                    

                    else
                    {
                        item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateAndOrMergeResults;
                        item.Message = "Create New Sample/Add Results";
                    }
                }
				else if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist)
                {                    
                                        
                    if (importType == (Int32)EnumImportType.UpdateHeaderOnly)                                                                       //7/26/19
                    {                                                                                                                           //7/26/19    
                        item.Status = 2;                                                                                                        //7/26/19    
                    }                                                                                                                           //7/26/19

                    else if (importType == (Int32)EnumImportType.AddNewSamplesAndResults)                
                    {
							item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.CreateSample;
							item.Message = "Create New Sample";
						}                   
                    else
						{						
							item.Status = 2;
						}
				}
				

			}					
			
			//change any samples where the message is not null back to rejected			
			CheckRejectedMessages(_header);

            if (importType == (Int32)EnumImportType.UpdateHeaderOnly)            
            {
                grdPreviewHeader.DataSource = _header;
                grdPreviewHeaderView.FocusedRowHandle = 0;
                grdPreviewHeaderView.RefreshData();
            }
            else
            {
                grdPreview.DataSource = _header;
                grdPreviewView.FocusedRowHandle = 0;
                grdPreviewView.RefreshData();
            }
                

			UpdateCounts();
			
		}
		private void btnCancelAll_Click(object sender, EventArgs e)
		{

            Int32 importType = 0;                                                                                                               //7/29/19
            importType = GetImportType(_selectedDefinitionID);                                                                                  //7/29/19

            if (lueTemplateName.EditValue == null)
			{
				return;
			}
			var samples = from g in _header
						  select g;
			foreach (var item in samples)
			{
				item.Status = (Int32)SampleImportStatus.Cancelled;
                                
                if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveMaterial ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveOwner ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveSupplier ||
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation)
                {
                    item.Status = (Int32)SampleImportStatus.Ignore;
                }

                if (item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.CreateSample ||
					item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists ||
					item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MergeResults || 
                    item.MessageStatus == (Int32)SampleImportSpecificMessageDetail.CreateAndOrMergeResults

                    )
				{
					item.MessageStatus = (Int32)SampleImportSpecificMessageDetail.Rejected;
					item.Message = "Rejected";					
				}	
			}

            //7/29/19
            //grdPreviewView.RefreshData();
            if (importType == (Int32)EnumImportType.UpdateHeaderOnly)            
            {
                grdPreviewHeaderView.RefreshData();
            }
            else
            {
                grdPreviewView.RefreshData();
            }
                
			UpdateCounts();			
		}

		private void CheckRejectedMessages(List<ImportHeaderInfo> hdr)
		{			
			//get sample-id's from resultsPreview where message is not null
			//update all records for those sampleID's, status = 2					
			var rejected_samples = from g in hdr
								   where g.Message != null && g.Message != "" &&
											(g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists &&
											 g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist &&
											 g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.CreateSample &&
											 g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.MergeResults

                                             //2/19/18
                                             && g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.CreateAndOrMergeResults

                                             //7/29/18
                                             && g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.UpdateHeaderFields

                                             && g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.InactiveMaterial &&
                                             g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.InactiveOwner &&
                                             g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation &&
                                             g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.InactiveSupplier &&
                                             g.MessageStatus != (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation
                                             )


								   select g;
			foreach (var item in rejected_samples)
			{
				var sameSampleID = from i in hdr
								   where i.ImportHeaderID == item.ImportHeaderID
								   select i;
				foreach (var sampleid in sameSampleID)
				{
					sampleid.Status = (Int32)SampleImportStatus.Cancelled;
				}
			}

			//update all samples where the detail datasource is not cross-referenced
			var rejected_detail = from g in hdr
								  where g.Message != null && g.Message != "" && g.MessageStatus == (Int32)SampleImportSpecificMessageDetail.MissingDatasourceCrossReference
								  select g;
			foreach (var itemDtl in rejected_detail)
			{
				var sameSampleID = from i in hdr
								   where i.ImportHeaderID == itemDtl.ImportHeaderID
								   select i;
				foreach (var sampleid in sameSampleID)
				{
					sampleid.Status = (Int32)SampleImportStatus.Cancelled;
				}
			}




		}
		private Int32 CountSamples()
		{
			var samples = from g in _importResultsAll						   
						   select g;
			return samples.Count();
		}
		        
        private Int32 CountMissingCrossReferences()
		{
			Int32 missingCrossRef = 0;			

			foreach (ImportHeaderInfo item in _importResultsAll)
			{				
				if (item.Message == "Missing Cross-Reference")
				{
					missingCrossRef++;
				}	   				
			}
			return missingCrossRef;
		}

		private Int32 CountWarnings()
		{
			Int32 headerWarnings = 0;
			Int32 detailWarnings = 0;
						
			foreach (ImportHeaderInfo item in _importResultsAll)
			{
				if (item.Message != null && item.Status == (Int32)SampleImportStatus.Warning)
				{
					headerWarnings++;
				}		
				
				var detailWarn = (from g in item.DetailImport
								  where g.DetailMessage.Length > 0
								  group g by g.ImportHeaderID into samples
								  select samples);
				foreach (var det in detailWarn)
				{
					detailWarnings++;
				}
				
			}
			return headerWarnings + detailWarnings;
		}
		private Int32 CountAccepted()
		{
			Int32 count = 0;

			foreach (ImportHeaderInfo item in _importResultsAll)
			{
				if (item.Status == (Int32)SampleImportStatus.Accepted)
				{
					count++;
				}								
			}
			return count;
		}
                
        private Int32 CountIgnored()
        {
            Int32 count = 0;

            foreach (ImportHeaderInfo item in _importResultsAll)
            {
                if (item.Status == (Int32)SampleImportStatus.Ignore)
                {
                    count++;
                }
            }
            return count;
        }

        private Int32 CountDetailWarningsPerSample(Int32 importHeaderID)
		{			
			Int32 countMsg = 0;
			foreach (ImportHeaderInfo item in _importResultsAll)
			{
				if (item.ImportHeaderID == importHeaderID)
				{
					for (int j = 0; j < item.DetailImport.Count(); j++)
					{						
						if (item.DetailImport[j].DetailMessage.Length > 0)
						{
							countMsg++;
						}
					}
				}
			}				

			return countMsg;			
		}
		private Int32 CountDetailWarnings(List<ImportDetailInfo> dtl)
		{
			var warnings = from g in dtl
						   where g.Message != null && g.DetailMessageStatus != 2
						   select g;
			return warnings.Count();
		}				
		private Int32 CountErrors()
		{			
			Int32 cnt = 0;
			var errors = (from g in _importResultsAll
						  where g.Status == 2
						  group g by g.ImportHeaderID into samples
						  select samples);
			foreach (var item in errors)
			{
				cnt = cnt + 1;
			}			
			return cnt;
		}
			

		#endregion

		#region LookupEdits
		private void lueTemplateName_EditValueChanged(object sender, EventArgs e)
		{
            //3/9/23
            txtDocumentPath.Text = String.Empty;


            //11/18/21
            AIMGlobal.generateSampleCode = false;
            AIMGlobal.sampleCodeMapped = true;
            AIMGlobal.useAnotherFieldAsSampleCode = false;
            AIMGlobal.noGrouping = false;


            _previewImport = true;
            ClearCounts();
			btnCrossReference.Visible = false;
			timer1.Stop();
			txtCounter.Text = "";

			if (lueTemplateName.EditValue != null)
			{				
				Int32.TryParse(lueTemplateName.EditValue.ToString(), out _selectedDefinitionID);
				_selectedDefinition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
			}

			tabControl1.SelectedTab = tpSelectImport;
			pnlRequiredFields.Enabled = false;			
			btnPrior.Visible = false;
			btnNext.Visible = false;
			btnEditMap.Visible = false;
			gbProcess.Visible = false;
            pnlArrows.Visible = false;                              //12/5/19   
            //btnDataSourceCrossReference.Visible = false;          //12/5/19 - commented

            btnChangeOwnerSupplier.Visible = false;                 //12/5/19   --> this button should be made visible only for "Add" type imports (not "Update" type imports)
            pnlMaterial.Visible = false;                            //12/5/19
            panelRest.Visible = false;                              //12/5/19
            grdPreviewView.ActiveFilter.Clear();                    //12/5/19
            lueMaterialFilter.EditValue = 0;                        //12/5/19

            if (_selectedDefinitionID != 0)
            {


                btnPrior.Visible = true;
                btnNext.Visible = true;
                btnEditMap.Visible = true;
                gbProcess.Visible = true;
                pnlArrows.Visible = true;                           //12/5/19

                tabControl1.Visible = true;
                btnNext.Enabled = true;             

                DataBindLogGrid();

                pnlRequiredFields.Enabled = true;                

                //12/5/19
                //gbAllowDuplicates.Visible = false;
                lblAllowDuplicates.Visible = false;
                txtAllowDuplicates.Visible = false;


                //12/5/19
                //rbYesDups.Checked = false;
                //rbNoDups.Checked = true;
                txtAllowDuplicates.Text = "";


                //btnDataSourceCrossReference.Visible = true;       //12/5/19 - commented
                if (_selectedDefinition != null)
                {                    
                    if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateResultsOnly || _selectedDefinition.ImportType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)         
                    {
                        pnlRequiredFields.Enabled = false;
                    }
                                        
                    else if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)                                           //7/26/19
                    {                                                                                                                           //7/26/19
                        pnlRequiredFields.Enabled = false;                                                                                      //7/26/19
                    }                                                                                                                           //7/26/19

                    else
                    {
                        //12/5/19
                        //gbAllowDuplicates.Visible = true;
                        lblAllowDuplicates.Visible = true;
                        txtAllowDuplicates.Visible = true;

                        pnlRequiredFields.Enabled = true;
                        btnChangeOwnerSupplier.Visible = true;      //12/5/19   --> this button should be made visible only for "Add" type imports (not "Update" type imports)
                    }

                    txtDelimiterType.Text = "";
                    txtDelim.Text = "";
                    txtDecimalSep.Text = "";                                                                                                    //4/2/19


                    txtOrientation.Text = "";
                    txtImportType.Text = "";
                    txtQualityControlResults.Text = "";
                    
                    txtSheetNumber.Text = "";

                    switch (_selectedDefinition.ImportFileTypeID)
                    {
                        case (Int32)FileTypeEnum.TXT:
                            txtFileType.Text = "TXT";                            
                            SetTextFields();                           
                            break;

                        case (Int32)FileTypeEnum.XLSX:
                                                        
                            //4/14/20 - changed for Nickki
                            //txtFileType.Text = "XLS";
                            txtFileType.Text = "XLSX";

                            txtSheetNumber.Text = _selectedDefinition.ExcelSheetNumber.ToString();                            
                            SetTextFields();                            
                            break;

                        case (Int32)FileTypeEnum.XLS:
                            txtFileType.Text = "XLS";
                            SetTextFields();
                            break;

                        case (Int32)FileTypeEnum.CSV:
                            txtFileType.Text = "CSV";                            
                            SetTextFields();                            
                            break;                        

                        case 0:
                            MessageBox.Show("Please select an import file type under Edit Template and Import Map.", "Error...", MessageBoxButtons.OK);
                            return;
                        default:
                            MessageBox.Show("Import file format is currently unavailable for User-Defined imports. Contact IT.", "Warning...", MessageBoxButtons.OK);
                            return;

                    }

                    //had to do it this way becase \t , ; | wouldn't show up in text box?
                    if (txtDelimiterType.Text == "\t")
                    {
                        txtDelim.Text = "Tab";
                    }
                    else if (txtDelimiterType.Text == ",")
                    {
                        txtDelim.Text = "Comma";
                    }
                    else if (txtDelimiterType.Text == ";")
                    {
                        txtDelim.Text = "Semicolon";
                    }
                    else if (txtDelimiterType.Text == "|")
                    {
                        txtDelim.Text = "Vertical Bar";
                    }


                    //4/2/19
                    if (txtDecimalSep.Text == ",")
                    {
                        txtDecimalSep.Text = "Comma";
                    }
                    else if(txtDecimalSep.Text == ".")
                    {
                        txtDecimalSep.Text = "Period";
                    }
                    else
                    {
                        txtDecimalSep.Text = "";
                    }


                    _dataSource = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);

                    if (_dataSource != null)
                    {

                        ApplyTemplateXML();                 //1/6/20
                                                
                        DataBindLueMaterial();
                        DataBindLueOwner();                 
                        DataBindLueSupplier();

                        DataSourceTypeInfo user = new DataSourceTypeInfo();
                        user = AIMManager.GetUserDefinedDataSourceTypeID((Int32)_dataSource.DataSourceTypeID);
                        if (user != null)
                        {
                            txtDataSource.Text = user.Name;
                        }
                        else
                        {
                            txtDataSource.Text = "";
                        }

                        txtSourcePath.Text = "";
                        txtArchivePath.Text = "";
                        txtErrorFolder.Text = "";

                        if (_dataSource.ProcessDirectory != null)
                        {
                            txtSourcePath.Text = _dataSource.ProcessDirectory.ToString();
                            txtErrorFolder.Text = _dataSource.ProcessDirectory.ToString() + @"\Error";
                        }
                        if (_dataSource.BackupDirectory != null)
                        {
                            txtArchivePath.Text = _dataSource.BackupDirectory.ToString();
                        }

                        //10/10/20 - commented. This is assigned by its value is never used
                        //_templateChange = true;     //added this so "Allow duplicate sample code?" msg doesn't show when selecting template

                        if (_dataSource.AllowDuplicateSampleCodes == true)
                        {
                            //rbYesDups.Checked = true;
                            txtAllowDuplicates.Text = "Yes";

                        }
                        else
                        {
                            //rbNoDups.Checked = true;
                            txtAllowDuplicates.Text = "No";

                        }

                        //10/10/20 - commented. This is assigned by its value is never used
                        //_templateChange = false;



                        ShowHideAimDropdowns();

                    }
                    else
                    {
                        txtSourcePath.Text = "";
                        txtArchivePath.Text = "";
                        txtErrorFolder.Text = "";

                    }

                    List<FilesInfo> clearFiles = new List<FilesInfo>();
                    List<FilesInfo> clearResults = new List<FilesInfo>();
                    grdFiles.DataSource = clearFiles;
                    grdPreview.DataSource = clearResults;
                    LoadGridWithFiles();                    
                    DataBindGrid_AnalyzerDataSource();

                    grdPreviewHeader.DataSource = clearResults;                                                                                 //7/29/19

                }
            }
						
		}

        

        private void SetTextFields()
        {

            lblNutrientsBeginColumn.Visible = false;
            txtNutrientsColumn.Visible = false;

            txtDelimiterType.Text = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetDelimiterField(_selectedDefinitionID).ToString();
            txtDecimalSep.Text = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetDecimalSeparator(_selectedDefinitionID).ToString();             //4/2/19
            txtOrientation.Text = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetOrientation(_selectedDefinitionID).ToString();
            txtQualityControlResults.Text = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetQualityControl(_selectedDefinitionID).ToString();
            txtImportType.Text = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetImportType(_selectedDefinitionID).ToString();
            txtHeaderExists.Text = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetHeaderExists(_selectedDefinitionID).ToString();
            txtNutrientsColumn.Text = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetNutrientBeginColumn(_selectedDefinitionID).ToString();
            if (txtNutrientsColumn.Text != "0")
            {
                lblNutrientsBeginColumn.Visible = true;
                txtNutrientsColumn.Visible = true;
            }
            

        }

		private void lueOwner_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
                SyncValueLue(ref lueOwner, ref lueOwnerName);           //1/2/20
                ChangeDropdownBackcolor(lueOwnerName);                  //1/2/20
                ChangeDropdownBackcolor(lueOwner);

                //12/16/20 - do I want this here?
                ChangeDropdownBackcolor(lueOwnerLocationName);


				DataBindOwnerLocation();                
				_previewImport = true;

                //12/16/20 - commented
                //SaveRequiredFields();           //12/5/19



            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	
		#endregion

		#region CheckDate
		protected DateTime? ConvertOLEStringToDate(String date)
		{
			try
			{
				double dbt = double.Parse(date);
				DateTime dtconv = DateTime.FromOADate(dbt);
				String strDtConv = dtconv.ToString();
				DateTime dt = DateTime.Parse(strDtConv);
				return dt;
			}
			catch
			{
				return null;
			}

		}
		protected bool CheckOLEDate(String date)
		{
			try
			{
				double dbt = double.Parse(date);
				DateTime dtconv = DateTime.FromOADate(dbt);
				String strDtConv = dtconv.ToString();
				DateTime dt = DateTime.Parse(strDtConv);
				return true;
			}
			catch
			{
				return false;
			}
		}
		protected bool CheckDate(String date)
		{
			try
			{				
				DateTime dt = DateTime.Parse(date);
				return true;
			}
			catch
			{
				return false;
			}

		}
		protected DateTime? ConvertStringToDate(String date)
		{
			try
			{
				DateTime dt = DateTime.Parse(date);
				return dt;
			}
			catch
			{
				return null;
			}

		}
		protected bool CheckForDateColumn(String val)
		{
			try
			{
				//not sure how to check one string for 3 values, or to ignore case -- so set up 3 variables
				Boolean boolDateColumn1 = false;
				Boolean boolDateColumn2 = false;
				Boolean boolDateColumn3 = false;
				boolDateColumn1 = val.Contains("Date");
				boolDateColumn2 = val.Contains("date");
				boolDateColumn3 = val.Contains("DATE");
				if (boolDateColumn1 == true || boolDateColumn2 == true || boolDateColumn3 == true)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}
		#endregion
		
		#region Master Events
		private void grdPreview_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
		{
			GridView view;
			view = e.View as GridView;

			//view.GridControl.Width = 500;
			
			//format visible columns			
			
			view.Columns["ResultValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			view.Columns["ResultValue"].DisplayFormat.FormatString = "n2";
			view.Columns["ResultValue"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			view.Columns["ResultValue"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
						
			view.Columns["AnalyzerNutrientCode"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["ResultValue"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["UnitType"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["AnalyzerDataSourceTypeName"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);

			view.Columns["AnalyzerNutrientCode"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["ResultValue"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["UnitType"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["AnalyzerDataSourceTypeName"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);

			view.Columns["AnalyzerNutrientCode"].OptionsColumn.AllowEdit = false;
			view.Columns["ResultValue"].OptionsColumn.AllowEdit = false;
			view.Columns["UnitType"].OptionsColumn.AllowEdit = false;
			view.Columns["AnalyzerDataSourceTypeName"].OptionsColumn.AllowEdit = false;

			view.Columns["DetailMessage"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["DetailMessage"].AppearanceCell.BackColor = Color.LightYellow;
			view.Columns["DetailMessage"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);			
			view.Columns["DetailMessage"].OptionsColumn.AllowEdit = false;
			view.Columns["DetailMessage"].Caption = "Message";

			//4/4/17
			view.Columns["AnalyzerDataSourceTypeName"].Caption = "Data Source";
			view.Columns["AnalyzerNutrientCode"].Caption = "Nutrient Code";

			//Hide un-used columns					
			view.Columns["ImportHeaderID"].Visible = false;
			view.Columns["ImportDetailID"].Visible = false;
			view.Columns["DetailMessageStatus"].Visible = false;
			view.Columns["Message"].Visible = false;
			view.Columns["Status"].Visible = false;
			view.Columns["StatusDisplay"].Visible = false;
			view.Columns["ResultValueString"].Visible = false;

			//4/4/17			
			view.Columns["AnalyzerDataSourceTypeID"].Visible = false;
			view.Columns["XrefDatasourceExists"].Visible = false;

		}
		private void grdPreviewView_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
		{
			ImportHeaderInfo header = new ImportHeaderInfo();
			ImportDetailInfo detail = new ImportDetailInfo();
			header = (ImportHeaderInfo)grdPreviewView.GetRow(e.RowHandle);
		}
		private void grdPreviewView_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
		{
			GridView masterView = (GridView)sender;
			GridView detailView = masterView.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
			detailView.BestFitColumns();
		}
		private void grdPreviewView_MasterRowGetChildList(object sender, MasterRowGetChildListEventArgs e)
		{
			ImportHeaderInfo header = new ImportHeaderInfo();					
			header = (ImportHeaderInfo)grdPreviewView.GetRow(e.RowHandle);	

			//5/12/16 - if column was sorted, this wasn't returning correct detail for the header -- s/b using the index of the header  								
			//e.ChildList = _header[e.RowHandle].DetailImport;
			Int32 index = _header.IndexOf(header);
			e.ChildList = _header[index].DetailImport;           
		}
		private void grdPreviewView_MasterRowGetRelationCount(object sender, MasterRowGetRelationCountEventArgs e)
		{
			e.RelationCount = 1;
		}
		private void grdPreviewView_MasterRowGetRelationName(object sender, MasterRowGetRelationNameEventArgs e)
		{
			e.RelationName = "";
		}
		#endregion		 

		#region Background Workers

		private void backgroundPreview_DoWork(object sender, DoWorkEventArgs e)
		{

            //When testing imports, need to test all of the possible versions below:
            //  CSV/TXT - Portrait - sample code is mapped
            //  CSV/TXT - Portrait - another field is used to group samples (and system generates sample code)
            //  CSV/TXT - Portrait - no grouping (just system-generated sample code)
            //  CSV/TXT - Portrait - the 3 versions above without header     
            //  CSV/TXT - Portrait - update header only
            //  Excel   - Portrait - sample code is mapped
            //  Excel   - Portrait - another field is used to group samples (and system generates sample code)
            //  Excel   - Portrait - no grouping (just system-generated sample code)
            //  Excel   - Portrait - the 3 versions above without header
            //  Excel   - Portrait - update header only 
            //  CSV/TXT - Landscape
            //  Excel   - Landscape
            //  Variables to watch for portrait imports:  str, n, _saveSampleCode, lastProcessedSampleCode, _sampleCode, _workHeader, workEntity, ptr, _sampleCounter


            if (_previewImport == false)
			{
				return;
			}                       

            String excelFileExtension = "";
            
            String useThisOwner = String.Empty;                         //12/28/20
            String useThisOwnerLocation = String.Empty;                 //12/28/20
            String useThisSupplier = String.Empty;                      //12/28/20
            String useThisSupplierLocation = String.Empty;              //12/28/20
            Int32 detailBeginRow = 0;                                   //3/7/23


            //-------------------------------------------------------------------
            //For each file selected, open file and add values to the importHeader and importDetail ENTITIES            
            AIMGlobal.importTotalRecordCount = 0;
            CFCAIMCore.Global.importProgressBarCounter = 0;
            AIMGlobal.importCounter = 0;
			AIMGlobal.importTextCounter = 0;   
			List<FilesInfo> files = new List<FilesInfo>();
			List<ImportHeaderInfo> importResults = new List<ImportHeaderInfo>();
			_importResultsAll.Clear();

			//Get files from grid where fileStatus = true/checked					
			files = (List<FilesInfo>)grdFiles.DataSource;

			if (files != null)
			{
				foreach (var file in files)
				{
					if (file.FileStatus == true)
					{
						try
						{

                            //-------------------------------------------------- 
                            Int32 defID = 0;                            
                            Int32.TryParse(lueTemplateName.EditValue.ToString(), out defID);
							Int32 begDetailCol = _selectedDefinition.NutrientDetailBeginColumn;
                            Boolean boxIsChecked = false;
                            Boolean allowDuplicates = true;
                            AIMGlobal.importValidateError = 0;

                            if (txtAllowDuplicates.Text == "No")
                            {
								allowDuplicates = false;
							}

                            //12/28/20
                            if ((Int32)lueOwner.EditValue != 0)   {useThisOwner = lueOwner.Text;}
                            if ((Int32)lueOwnerLocation.EditValue != 0) { useThisOwnerLocation = lueOwnerLocation.Text; }
                            if ((Int32)lueSupplier.EditValue != 0) { useThisSupplier = lueSupplier.Text; }
                            if ((Int32)lueSupplierLocation.EditValue != 0) { useThisSupplierLocation = lueSupplierLocation.Text; }

                            if ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLS || (Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLSX)
                            {
                                excelFileExtension = Path.GetExtension(@file.ProcessPathAndName);
                            }

                            //--------------------------------------------------                            
                            //CHECK PORTRAIT GROUPING FIELD                            
                            if (_dataSource.OrientationDisplay == "Portrait")
                            {
                                AIMGlobal.SplashScreenHide();
                                List<UserDefinedDataSourceTemplateInfo> fieldTemplate = new List<UserDefinedDataSourceTemplateInfo>();
                                fieldTemplate = AIMManager.GetUserDefinedDataSourceTemplate(defID);                                

                                //8/15/22
                                //boxIsChecked = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleGrouping(fieldTemplate.Count, (List<UserDefinedDataSourceTemplateInfo>)fieldTemplate);
                                boxIsChecked = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleGrouping(fieldTemplate.Count, (List<UserDefinedDataSourceTemplateInfo>)fieldTemplate,_selectedDefinition.IsCompositeBatch);

                                if (boxIsChecked == false) return;
                                AIMGlobal.SplashScreenShow();
                            }

                            //--------------------------------------------------
                            //SET HEADER BEGIN ROW
                            Int32 headerBeginRow = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetHeaderBeginRow(defID);

                            //--------------------------------------------------
                            //IMPORT FILE
                            AIMGlobal.SplashScreenShow();

                            //8/16/22
                            if (_selectedDefinition.ImportType == (Int32)EnumImportType.RomerResults)
                            {
                                //4/20/23 -- a problem with the csvReader below is that it "ignores" blank lines on the txt file. The record number on
                                //			the csvReader goes from rec# 6 to rec# 8, so the actual header line number is not correct 
                                //		    (and doesn't coincide with the csvParser when the datalist gets created in the PreviewRomerImport function below).
                                //			That's where things are going awry.

                                //-------------------------------------------------------------------
                                //3/7/23
                                //GET NUMBER OF ROWS TO SKIP - For Romer imports, can look for "Sample". On import wizard and map for Romer, the header/detail rows are hidden.
                                Int32 skipRowCount = 0;
                                using (var streamReader = new StreamReader(@file.ProcessPathAndName))
                                {
                                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                                    {
                                        while (csvReader.Read())
                                        {
                                            if (((CsvHelper.CsvParser)csvReader.Context.Parser).Record[0].StartsWith("Sample"))
                                            {
                                                ////skipRowCount = csvReader.Parser.Row - 1;
                                                break;
                                            }

                                            //5/12/23
                                            else
                                            {
                                                skipRowCount++;
                                            }
                                        }
                                    }
                                }
                                headerBeginRow = skipRowCount + 1;
                                detailBeginRow = headerBeginRow + 1;

                                importResults = CFCAIM.AIMUtilities.Imports.SharedFunctions.PreviewRomerImport("Import", _selectedDefinitionID, _selectedDefinition.ImportType, allowDuplicates, AIMGlobal.lastImportHeaderID, @file.ProcessPathAndName, headerBeginRow, 1, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, begDetailCol, excelFileExtension, backgroundPreview, backgroundPreview, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation, detailBeginRow);
                            }
                            else
                            {
                                importResults = CFCAIM.AIMUtilities.Imports.SharedFunctions.PreviewImport("Import", _selectedDefinitionID, _selectedDefinition.ImportType, allowDuplicates, AIMGlobal.lastImportHeaderID, @file.ProcessPathAndName, headerBeginRow, 1, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, begDetailCol, excelFileExtension, backgroundPreview, backgroundPreview, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);
                            }
                            AIMGlobal.SplashScreenHide();

                            //--------------------------------------------------
                            if (AIMGlobal.importValidateError != 0)
							{
								return;
							}
							_importResultsAll.AddRange(importResults);

                        }
                        catch (Exception ex)
						{
							MessageBox.Show(ex.Message);
						}
					}

					//backgroundPreview.ReportProgress(0);
					if (AIMGlobal.stopPreviewingImport == true)
					{
						AIMGlobal.stopPreviewingImport = false;
						break;
					}
				}
			}
		}

        
        private void backgroundPreview_ProgressChanged(object sender, ProgressChangedEventArgs e)       

        {
			try
			{
                {
                    if (progressBarPreviewImport.Value != progressBarPreviewImport.Maximum)
					{
						progressBarPreviewImport.Value++;
                        txtCounter.Text = Convert.ToString(CFCAIMCore.Global.importProgressBarCounter++);
                        txtProgressMessage.Text = AIMGlobal.txtProgressMessage;                                                                 //8/5/19
                    } 
					else
					{
						progressBarPreviewImport.Value = 0;
					}

				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void backgroundPreview_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            String msg = "";
            String rtn = "";                    //1/6/20

            progressBarPreviewImport.Value = progressBarPreviewImport.Maximum;
			SetControlsForProcess(false);
            CFCAIMCore.Global.importProgressBarCounter = 0;
            AIMGlobal.txtProgressMessage = "";                                                                                                  //8/5/19
            _header = _importResultsAll;
                       
            if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)                                                        //7/29/19 - begin
            {
                btnStartImport.Visible = false;
                grdPreview.Visible = false;
                btnUpdateHeaderFields.Visible = true;
                grdPreviewHeader.Visible = true;                          
                grdPreviewHeader.Location = new Point(5, 12);
                grdPreviewHeader.DataSource = _importResultsAll;
                grdPreviewHeaderView.RefreshData();
                grdPreviewHeader.Refresh();

                //1/6/20 - Update grid column headers with alternate names. 
                //12/23/20 - added defID (so supplier/supplier-loc column header can be changed to "combined" if necessary
                //rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewHeaderView);

                //12/29/20
                //rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewHeaderView, _selectedDefinition.UserDefinedDataSourceDefinitionID);
                rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewHeaderView, _selectedDefinition.UserDefinedDataSourceDefinitionID, lueSupplier, lueSupplierLocation, "Import");

                //12/29/20
                rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ShowAdditionalMappedColumns(grdPreviewView, _selectedDefinition.UserDefinedDataSourceDefinitionID);

            }                                                                                                                                   //7/29/19 - end

            else
            {
                btnUpdateHeaderFields.Visible = false;
                grdPreviewHeader.Visible = false;
                btnStartImport.Visible = true;
                grdPreview.Visible = true;                
                grdPreview.DataSource = _importResultsAll;

                
                grdPreviewView.RefreshData();                
                grdPreview.Refresh();

                //----------------------------------------------------------------------------------
                //1/6/20 - Update grid column headers with alternate names. 
                //12/23/20 - added defID (so supplier/supplier-loc column header can be changed to "combined" if necessary
                //rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewView);

                //12/29/20
                //rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewView, _selectedDefinition.UserDefinedDataSourceDefinitionID);
                rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewView, _selectedDefinition.UserDefinedDataSourceDefinitionID, lueSupplier, lueSupplierLocation, "Import");

                //12/29/20
                rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ShowAdditionalMappedColumns(grdPreviewView, _selectedDefinition.UserDefinedDataSourceDefinitionID);

            }

            //12/10/19
            if (_previewImport == true)         
            {                   
                pnlMaterial.Visible = false;                
            }

            UpdateCounts();

			grdPreviewView.ClearSelection();
            grdPreviewHeaderView.ClearSelection();                                                                                              //7/29/19

            btnAcceptAll.Focus();
			AIMGlobal.SplashScreenHide();

            //4/3/18 - added check of inmportValidateError because, if there was an error with the mapping, user would get the mapping error msg and then this message as well.
            //if (CountSamples() == 0 && rbNoDups.Checked == true)

            //12/5/19
            //if (CountSamples() == 0 && rbNoDups.Checked == true && AIMGlobal.importValidateError == 0)
            if (CountSamples() == 0 && txtAllowDuplicates.Text == "No" && AIMGlobal.importValidateError == 0)

            {                                
                msg = "No information to import.  Possible cause: ";
                msg += Environment.NewLine + Environment.NewLine + "Sample Codes already exist in the system";
                msg += Environment.NewLine + "(and Allow Duplicates is set to No.)";
                msg += Environment.NewLine + Environment.NewLine + "Excel sheet number is incorrect.";
                MessageBox.Show(msg, "Nothing to Import...", MessageBoxButtons.OK);
            }
            else
            {
                SetCrossReferencesTimer();
            }            			
			_previewImport = false;
        }

        private void backgroundValidate_DoWork(object sender, DoWorkEventArgs e)
		{            
           
            Int32 defID = 0;
			
			Boolean allowDuplicates = true;
			Int32.TryParse(lueTemplateName.EditValue.ToString(), out defID);
			AIMGlobal.importValidateError = 0;
			Boolean dataSourceIsMapped = false;           
            Int32 importType = 0;
            importType = GetImportType(_selectedDefinitionID);                                                                                   //8/5/19
            CFCAIMCore.Global.importProgressBarCounter = 0;                                                                                      //8/5/19

            //if (rbNoDups.Checked == true)
            if (txtAllowDuplicates.Text == "Yes")
            {
				allowDuplicates = false;
			}
            			
			UserDefinedDataSourceDefinitionInfo dataSource = new UserDefinedDataSourceDefinitionInfo();
			dataSource = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
			if (dataSource.DataSourceIsMapped == true)
			{
				dataSourceIsMapped = true;
			}
                        
            //txtCounter.Text = "";     //txtCounter defined on another form? - moved to Validate sub  10/16/20

            _header = CFCAIM.AIMUtilities.Imports.SharedFunctions.CopyWorkToImportHeaderEntity(_header, "Import", defID, importType, allowDuplicates, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, dataSourceIsMapped, backgroundValidate);


        }
		private void SetCrossReferencesTimer()
		{	            
            timer1.Stop();
			btnCrossReference.Visible = false;
			if (CountMissingCrossReferences() > 0)
			{
				btnCrossReference.Visible = true;
				timer1.Start();
			}
		}
		private void backgroundValidate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
            
            SetControlsForProcess(false);
            CFCAIMCore.Global.importProgressBarCounter = 0;
            AIMGlobal.txtProgressMessage = "";                                                                                                  //8/5/19
                        
            if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)                                                        //7/29/19 - begin
            {
                grdPreview.Visible = false;
                btnStartImport.Visible = false;
                btnUpdateHeaderFields.Visible = true;
                grdPreviewHeader.Visible = true;                
                grdPreviewHeader.Location = new Point(5, 12);
                grdPreviewHeader.DataSource = _header;               
                grdPreviewHeaderView.RefreshData();
                grdPreviewHeader.Refresh();                
            }                                                                                                                                   //7/29/19 - end

            else
            {
                grdPreviewHeader.Visible = false;
                btnUpdateHeaderFields.Visible = false;
                btnStartImport.Visible = true;                
                grdPreview.Visible = true;
                grdPreview.DataSource = _header;                
                grdPreviewView.RefreshData();
                grdPreview.Refresh();
            }

            _importResultsAll = _header;

            UpdateCounts();

            //12/31/20
            grdPreviewView.BestFitColumns();
						
			grdPreviewView.ClearSelection(); //--> this is supposed to remove focus from the grid. Not working because first row of grid has focused set!
                        
            grdPreviewHeaderView.ClearSelection();                                                                                              //7/29/19

            btnAcceptAll.Focus();            

            AIMGlobal.SplashScreenHide();
			SetCrossReferencesTimer();                                        
                  							 
        }
		        
        private IList<BackgroundWorker> m_WorkersWithData = new List<BackgroundWorker>();
		        
        private void CheckAllThreadsHaveFinishedWorking()
		{
			bool hasAllThreadsFinished = false;
			while (!hasAllThreadsFinished)
			{
				hasAllThreadsFinished = (from worker in m_WorkersWithData
										 where worker.IsBusy
										 select worker).ToList().Count == 0;
				Application.DoEvents(); //This call is very important if you want to have a progress bar and want to update it
										//from the Progress event of the background worker.
				Thread.Sleep(1000);     //This call waits if the loop continues making sure that the CPU time gets freed before
										//re-checking.
			}
			m_WorkersWithData.Clear();  //After the loop exits clear the list of all background workers to release memory.
										//On the contrary you can also dispose your background workers.
		}

		#endregion

		#region Lue's
		private void lueOwner_Click(object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}
		private void lueOwner_Enter(object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}
		private void lueOwnerLocation_Enter(object sender, EventArgs e)
		{
			lueOwnerLocation.SelectAll();
		}
		private void lueOwnerLocation_Click(object sender, EventArgs e)
		{
			lueOwnerLocation.SelectAll();
		}
		private void lueSupplier_Click(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}
		private void lueSupplier_Enter(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}
		private void lueSupplierLocation_Enter(object sender, EventArgs e)
		{
			lueSupplierLocation.SelectAll();
		}
		private void lueSupplierLocation_Click(object sender, EventArgs e)
		{
			lueSupplierLocation.SelectAll();
		}
		private void lueTemplateName_Click(object sender, EventArgs e)
		{
			lueTemplateName.SelectAll();
		}
		private void lueTemplateName_Enter(object sender, EventArgs e)
		{
			lueTemplateName.SelectAll();
		}
		#endregion

		private void ShowHideAimDropdowns()
		{			
			pnlRequiredFields.Visible = false;
			lueOwner.Visible = false;
			lueOwnerLocation.Visible = false;

            //12/16/20
            lueOwnerName.Visible = false;
            lueOwnerLocationName.Visible = false;
            lueOwnerName.Enabled = false;
            lueOwnerLocationName.Enabled = false;

            lueSupplier.Visible = false;
			lueSupplierLocation.Visible = false;

            //12/16/20  
            lblOwner.Visible = false;
            lblOwnerLocation.Visible = false;
            lblSupplier.Visible = false;
            lblSupplierLocation.Visible = false;

            lueMaterial.Enabled = false;
			lueOwner.Enabled = false;
			lueOwnerLocation.Enabled = false;
			lueSupplier.Enabled = false;
			lueSupplierLocation.Enabled = false;

            //12/16/20
            //if (_selectedDefinition.AIMSourceMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport)
            if (_selectedDefinition.AIMSourceMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport || _selectedDefinition.AIMSourceMap == (Int32)SampleImportUserDefinedMapSelection.SelectNow)

			{		
				pnlRequiredFields.Visible = true;
				lueSupplier.Visible = true;
				lueSupplier.Enabled = true;                                
                lblSupplier.Visible = true;

                //12/16/20 
                ////lblSupplierLocation.Visible = true;

                //selecting value if there is only one               
                if (_aimSupplier.Count == 2)
                {
                    lueSupplier.EditValue = _aimSupplier[1].AIMSupplierID;
                }
                else
                {
                    lueSupplier.EditValue = 0; // _selectedDefinition.AIMSourceID; //11/14/24
                }
                ChangeDropdownBackcolor(lueSupplier);
			}

            //12/16/20
            //if (_selectedDefinition.AIMSourceLocationMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport)
            if (_selectedDefinition.AIMSourceLocationMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport || _selectedDefinition.AIMSourceLocationMap == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
            {
				
				pnlRequiredFields.Visible = true;
				lueSupplier.Visible = true;
				lueSupplierLocation.Visible = true;

				lueSupplier.Enabled = true;
				lueSupplierLocation.Enabled = true;
                                
                lblSupplier.Visible = true;
                lblSupplierLocation.Visible = true;

                //selecting value if there is only one
                if (_aimSupplierLocation.Count == 2)
                {
                    lueSupplierLocation.EditValue = _aimSupplierLocation[1].AIMSupplierLocationID;
                }
                else
                {
                    lueSupplierLocation.EditValue = 0; // _selectedDefinition.AIMSourceLocationID; //11/14/24
                }
                ChangeDropdownBackcolor(lueSupplierLocation);
			}

            //12/16/20
            //if (_selectedDefinition.AIMOwnerMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport)
            if (_selectedDefinition.AIMOwnerMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport || _selectedDefinition.AIMOwnerMap == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
            {
				
				pnlRequiredFields.Visible = true;
				lueOwner.Visible = true;
				lueOwner.Enabled = true;
                               

                //12/16/20
                lblOwner.Visible = true;
                lueOwnerName.Visible = true;
                lueOwnerName.Enabled = true;

                //selecting value if there is only one
                if (_aimOwner.Count == 2)
                {
                    lueOwner.EditValue = _aimOwner[1].AIMOwnerID;
                }
                else
                {
                    lueOwner.EditValue = 0; // _selectedDefinition.AIMOwnerID;  //11/14/24
                }
                ChangeDropdownBackcolor(lueOwner);

                //12/16/20
                ChangeDropdownBackcolor(lueOwnerName);
            }

            //12/16/20
            //if (_selectedDefinition.AIMOwnerLocationMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport)
            if (_selectedDefinition.AIMOwnerLocationMap == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport || _selectedDefinition.AIMOwnerLocationMap == (Int32)SampleImportUserDefinedMapSelection.SelectNow)

            {				
				pnlRequiredFields.Visible = true;
				lueOwner.Visible = true;
				lueOwnerLocation.Visible = true;
				lueOwner.Enabled = true;
				lueOwnerLocation.Enabled = true;
                                
                lblOwner.Visible = true;
                lblOwnerLocation.Visible = true;

                //12/16/20
                lueOwnerName.Visible = true;
                lueOwnerName.Enabled = true;
                lueOwnerLocationName.Visible = true;
                lueOwnerLocationName.Enabled = true;


                //selecting value if there is only one
                if (_aimOwnerLocation.Count == 2)
                {
                    lueOwnerLocation.EditValue = _aimOwnerLocation[1].AIMOwnerLocationID;
                }
                else
                {
                    lueOwnerLocation.EditValue = 0; // _selectedDefinition.AIMOwnerLocationID; //11/14/24
                }

                ChangeDropdownBackcolor(lueOwnerLocation);
                
                //12/16/20
                ChangeDropdownBackcolor(lueOwnerLocationName);
                ChangeDropdownBackcolor(lueOwner);
                ChangeDropdownBackcolor(lueOwnerName);
			}
			

		}
        private void LoadGridWithFiles()
        {            
            Boolean blnAddFile = true;

            List<FilesInfo> files = new List<FilesInfo>();
            if (txtSourcePath.Text.Trim().Length == 0)
            {
                grdFiles.RefreshDataSource();
                return;
            }

            if (Directory.Exists(txtSourcePath.Text.Trim()) == false)
            {
                MessageBox.Show("Source folder does not exist.", "Warning...", MessageBoxButtons.OK);
                txtSourcePath.SelectAll();
                return;
            }
            
            DirectoryInfo info = new DirectoryInfo(txtSourcePath.Text);
                        
            //FileInfo[] filesInfo = info.GetFiles("*" + txtFileType.Text).OrderByDescending(p => p.LastAccessTime).ToArray();              //2/20/20
            FileInfo[] filesInfo = info.GetFiles("*" + txtFileType.Text + "*").OrderByDescending(p => p.LastAccessTime).ToArray();          //2/20/20

            Boolean[] fileStatus = new Boolean[filesInfo.Length];
            for (int i = 0; i < filesInfo.Length; i++)
            {                
                String str = filesInfo[i].Name;
                if (str.Substring(0, 1) != "~")
                {

                    //5/15/20 - added because "XLS*" would show XLS AND XLSX files
                    blnAddFile = true;
                    if (txtFileType.Text == "XLS" && filesInfo[i].Extension.ToUpper() == ".XLSX")           // shouldn't this be ||, not && ???
                    {
                        blnAddFile = false;
                    }

                    //5/15/20 cm
                    if (blnAddFile == true)
                    {
                        files.Add(new FilesInfo()
                        {
                            FileName = filesInfo[i].Name,
                            ProcessPath = filesInfo[i].DirectoryName,
                            BackupPath = txtArchivePath.Text,
                            FileStatus = fileStatus[i]
                        }
                        );
                    }
                }
            }
            //----------------------------------------------------------------------------------------------

            grdFiles.DataSource = files;
            			
			if (txtArchivePath.Text.Trim().Length == 0)
			{
				return;
			}
            
		}		
		private void repbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
		private int CheckAimDropdownsBeforePreview()
		{
            try
            {

                //if dropdown is enabled, make sure something has been selected

                //3/20/19 - removed lueMaterial - not used
                //DevExpress.XtraEditors.LookUpEdit[] lue = { lueMaterial, lueSupplier, lueSupplierLocation, lueOwner, lueOwnerLocation };
                //string[] desc = { "Material", "Source", "Source Location", "Owner", "Owner Location" };
                DevExpress.XtraEditors.LookUpEdit[] lue = { lueSupplier, lueSupplierLocation, lueOwner, lueOwnerLocation };
                string[] desc = { "Source", "Source Location", "Owner", "Owner Location" };

                //3/20/19 - removed lueMaterial so changed this to 4
                //for (int i = 0; i < 5; i++)
                for (int i = 0; i < 4; i++)

                    {
				    if (lue[i].Enabled == true && (Int32)lue[i].EditValue == 0)
				    {
					    MessageBox.Show("Please select AIM " + desc[i] + ".", "Error...", MessageBoxButtons.OK);
					    tabControl1.SelectedTab = tpSelectImport;
					    lue[i].Focus();
					    lue[i].ShowPopup();
					    return -1;
				    }
			    }
			    return 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("CheckAimDropdownsBeforePreview - " + ex.Message);
                return -1;
                
            }

        }				
		private string CheckRequiredFields(Int32 sampleID)
		{						
			if (_blnMaterial == false)
			{
				return "Missing AIM Material";			
			}
			else if (_blnOwner == false)
			{
				return "Missing AIM Owner";				
			}
			else if (_blnOwnerLocation == false)
			{
				return "Missing AIM OwnerLocation";				
			}
			else if (_blnSupplier == false)
			{
				return "Missing AIM Supplier";
			}
			else if (_blnSupplierLocation == false)
			{
				return "Missing AIM Supplier Location";			
			}
			else
			{
				return "";
			}
						
		}
		private void UpdateRequiredFields(Int32 sampleID, String fieldname)
		{
			SampleImportRequiredFieldsInfo fields = new SampleImportRequiredFieldsInfo();
			if (fieldname == "Material")
			{
				_blnMaterial = true;
			}
			if (fieldname == "Source")
			{
				_blnSupplier = true;
			}
			if (fieldname == "Source Location")
			{
				_blnSupplierLocation = true;
			}
			if (fieldname == "Owner")
			{
				_blnOwner = true;
			}
			if (fieldname == "Owner Location")
			{
				_blnOwnerLocation = true;
			}
		}

        private void populateMaterialFilter()
        {
            //-------------------------------------------------------------------------------------
            //12/6/19
            //CLEAR AND POPULATE MATERIAL FILTER DROPDOWN
            //var entity = from g in _importResultsAll      --> couldn't get group by to work so looping thru results instead
            //             group g by g.MaterialID;        

            btnClearFilter.PerformClick();
                        
            List<ImportHeaderInfo> materialList = new List<ImportHeaderInfo>();
            Boolean isFound = false;
            foreach (ImportHeaderInfo item in _importResultsAll)
            {
                isFound = false;
                for (int i = 0; i < materialList.Count; i++)
                {            
                    if (materialList[i].MaterialName == item.MaterialName)           
                    {
                        isFound = true;
                    }
                }
                if (isFound == false)
                {                    
                    materialList.Add(item);

                }
            }
            lueMaterialFilter.Properties.DataSource = materialList;
            XtraEditors.LoadLookUpEdit(ref lueMaterialFilter, "MaterialName", "MaterialName");            
            //-------------------------------------------------------------------------------------


        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
                
				pnlCounts.Visible = false;
				btnCrossReference.Visible = false;
				timer1.Stop();

                btnNext.Enabled = false;
				btnPrior.Enabled = false;
				if (tabControl1.SelectedIndex == 0)
				{	 
					btnNext.Enabled = true;
				}
				else
				{
					btnPrior.Enabled = true;					
				}

				_okayToPreview = false;
                                
                if (tabControl1.SelectedIndex == 1)                          //1 = preview tab
                {
					textBox1.Text = "Select file(s) to be imported.";
					textBox2.Text = "Fill in yellow highlighted areas.";
					textBox3.Text = "Click Next arrow button below.";
                    populateMaterialFilter();                               //12/9/19
                    lueMaterialFilter.EditValue = "";                       //11/11/20
                    
                }

                if (tabControl1.SelectedIndex == 2)    
                {
					textBox1.Text = "Fix any missing fields/cross-references.";
					textBox2.Text = "Accept/Reject samples.";
					textBox3.Text = "Click Import Samples button.";                   
                }

                //12/16/20
                if (_lastTabIndex == 0)                                     // (0 = Select Import)
                {
                    if (pnlRequiredFields.Visible == true)
                    {
                        SaveRequiredFields();
                    }
                }

                
				if (tabControl1.SelectedIndex == 1 && _lastTabIndex != 2)   // (1 = Preview tab)
				{
					if (PreImportChecks() == -1)
					{
						tabControl1.SelectedIndex = 0;							
						return;
					}
                        						
					pnlCounts.Visible = true;                    
					AIMGlobal.SplashScreenShow();
					AIMGlobal.lastImportHeaderID = 0;

                    //2/25/21
                    AIMGlobal.lastImportSampleCode = String.Empty;
                    
                    AIMGlobal.showAutoGenSampleMsg = true;
					ClearCounts();
					progressBarPreviewImport.Value = 0;
					CopyLues();
					SetControlsForProcess(true);
					if (backgroundPreview.IsBusy == false)
					{
						backgroundPreview.RunWorkerAsync();
						_okayToPreview = true;
					}
					else
					{
						MessageBox.Show("Program is busy reading files for you. Please try later.", "Warning...");
					}
				}
				_lastTabIndex = tabControl1.SelectedIndex;
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}			
		}
		
		private void txtTotalErrors_TextChanged(object sender, EventArgs e)
		{
			txtTotalErrors.BackColor = Color.WhiteSmoke;
			if (((TextBox)(sender)).Text == "" || ((TextBox)(sender)).Text == "0") { }
			else
			{
				txtTotalErrors.BackColor = Color.Red;
				txtTotalErrors.ForeColor = Color.White;
			}

		}
		private void txtTotalWarnings_TextChanged(object sender, EventArgs e)
		{
			txtTotalWarnings.BackColor = Color.WhiteSmoke;
			if (((TextBox)(sender)).Text == "" || ((TextBox)(sender)).Text == "0") { }
			else
			{
				txtTotalWarnings.BackColor = Color.Yellow;				
			}
		}
		
		private void grdFiles_DataSourceChanged(object sender, EventArgs e)
		{				
			btnAcceptAllFiles.Visible = false;
			if (grdFilesView.RowCount > 0)
			{
				btnAcceptAllFiles.Visible = true;
			}

		}

        //3/20/19 - commented - lueMaterial not used
        //private void lueMaterial_EditValueChanged(object sender, EventArgs e)
		//{
        //    ChangeDropdownBackcolor(lueMaterial);
		//}

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
            SyncValueLue(ref lueOwnerLocation, ref lueOwnerLocationName);           //1/2/20
            ChangeDropdownBackcolor(lueOwnerLocationName);                          //1/2/20

            ChangeDropdownBackcolor(lueOwnerLocation);            
			_previewImport = true;

            //12/16/20 - commented
            //SaveRequiredFields();           //12/5/19
        }
		
		private void btnPreviewGridLog_Click(object sender, EventArgs e)
		{
			try
			{
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grdLog, "Import Log - " + lueTemplateName.Text.ToString());
                CFCAIMCore.Global.previewGridResults(grdLog, "Import Log - " + lueTemplateName.Text.ToString());
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	   	private void grdPreviewView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			
			/*
			//this highlights only the one row, not all rows for the sample
			if (e.RowHandle >= 0)
			{
				GridView View = sender as GridView;

				string warning = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Message"]);
				//if (warning == "3" && e.Column.Caption == "Message") --> this highlights only the Message column cell
				if (warning == "3")
				{
					e.Appearance.BackColor = Color.Yellow;
				}

			}
			*/
		}
	   	private void btnExpand_Click(object sender, EventArgs e)
		{
				
			Boolean blnExpand = false;
			GridView view = grdPreviewView;
			
			if (btnExpand.Text == "Expand All")
			{
				blnExpand = true;
				btnExpand.Text = "Collapse All";
			}
			else
			{
				btnExpand.Text = "Expand All";
			}

			view.BeginUpdate();
			try
			{
				int dataRowCount = view.DataRowCount;
				//dataRowCount = 100;   //hard-coded because test is using sample results -- too many!
				AIMGlobal.SplashScreenShow();
				for (int rHandle = 0; rHandle < dataRowCount; rHandle++)
					view.SetMasterRowExpanded(rHandle, blnExpand);
				AIMGlobal.SplashScreenHide();
			}
			finally
			{
				view.EndUpdate();
			}
			
		}

        private Boolean SaveSample()
        {
            Boolean rtn = true;

            try
            {
                Int32 sampleID = 0;
                UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
                def = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);

                for (int row = 0; row < _header.Count; row++)
                {
                    if (backgroundImport.IsBusy == true)
                    {
                        backgroundImport.ReportProgress(0);
                    }

                    if (_header[row].Status == 1)
                    {
                        //-----------------------------------------------------------
                        //UPDATE RESULTS ONLY
                        if (def.ImportType == (Int32)EnumImportType.UpdateResultsOnly)               
                        {
                            //8/17/22
                            //AddSampleResults(_header[row].ExistingImportHeaderID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation);
                            AddSampleResults(_header[row].ExistingImportHeaderID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation, def.ImportType);
                        }


                        //-----------------------------------------------------------
                        //4/1/19
                        //UPDATE RESULTS AND SPECIFIC HEADER FIELDS
                        if (def.ImportType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)          
                        {
                            //8/17/22
                            //AddSampleResults(_header[row].ExistingImportHeaderID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation);
                            AddSampleResults(_header[row].ExistingImportHeaderID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation, def.ImportType);

                            UpdateSampleHeader(row, def.DataSourceTypeID, _header[row].ExistingImportHeaderID);

                        }

                        //-----------------------------------------------------------
                        //CREATE NEW SAMPLE WITH RESULTS
                        else if (def.ImportType == (Int32)EnumImportType.AddNewSamplesAndResults)            
                        {
                            //4/1/19
                            //sampleID = UpdateSampleHeader(row, def.DataSourceTypeID);
                            sampleID = UpdateSampleHeader(row, def.DataSourceTypeID, 0);

                            //8/17/22
                            //AddSampleResults(sampleID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation);
                            AddSampleResults(sampleID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation, def.ImportType);
                        }
                                                
                        else if (def.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)                                                          //7/26/19
                        {                                                                                                                       //7/26/19
                            UpdateSampleHeader(row, def.DataSourceTypeID, _header[row].ExistingImportHeaderID);                                 //7/26/19
                        }                                                                                                                       //7/26/19

                        //-----------------------------------------------------------                   
                        //UPDATE RESULTS OR CREATE NEW SAMPLE WITH RESULTS
                        else
                        {
                            if (_header[row].SampleCodeExists == true)
                            {
                                //3/22/18 - get SampleID for existing samplecode --> this is for samples where a sample in the current entity was created 
                                //                                                   prior and has a sampleID that needs to be used here.
                                Int32 existingSampleID = 0;
                                existingSampleID = _header[row].ExistingImportHeaderID;
                                if (existingSampleID == 0)
                                {                                    
                                    //2/15/21
                                    existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_header[row].SampleCode);                                    
                                    //existingSampleID = CFCAIM.AIMUtilities.Imports.SharedFunctions.SampleCodeAlreadyExistsOnTable(_header[row].SampleCode, def.DataSourceTypeID);                                    
                                }

                                //8/17/22
                                //AddSampleResults(existingSampleID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation);
                                AddSampleResults(existingSampleID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation, def.ImportType);

                            }
                            else
                            {
                                //4/1/19
                                //sampleID = UpdateSampleHeader(row, def.DataSourceTypeID);
                                sampleID = UpdateSampleHeader(row, def.DataSourceTypeID, 0);

                                //8/17/22
                                //AddSampleResults(sampleID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation);
                                AddSampleResults(sampleID, row, def.DataSourceTypeID, def.DataSourceIsMapped, def.IsQualityControl, def.Orientation, def.ImportType);
                            }

                        }
                    }
                    else { }

                    if (AIMGlobal.stopPreviewingImport == true)
                    {
                        rtn = false;
                        AIMGlobal.stopPreviewingImport = false;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("SaveSample - " + ex.Message);
                rtn = false;
            }

            return rtn;

        }
    
        private Int32 UpdateSampleHeader(Int32 row, Int32 dataSourceTypeID, Int32 sampleID)
        {
            try
            {

                //Sample Table -------------------------------------	           
                SampleInfo sample = new SampleInfo();                
                sample.SampleID = sampleID;
                
                if (_header[row].SampleCode == "Missing")
                {
                    sample.SampleCode = "";
                }
                else
                {
                    sample.SampleCode = _header[row].SampleCode;
                }
               
                if (_header[row].AlternateCode.Length > 0)
                {
                    sample.AlternateCode = _header[row].AlternateCode;
                }
                else
                {
                    sample.AlternateCode = _header[row].SampleCode;
                }
                sample.SampleDate = _header[row].SampleDate;
                sample.AIMMaterialID = _header[row].MaterialID;
                sample.AIMOwnerID = _header[row].OwnerID;
                sample.AIMOwnerLocationID = _header[row].OwnerLocationID;
                sample.AIMSupplierID = _header[row].SourceID;
                sample.AIMSupplierLocationID = _header[row].SourceLocationID;
                sample.PoNumber = _header[row].PONumber;
                sample.InvoiceNumber = _header[row].InvoiceNumber;
                sample.TicketNumber = _header[row].TicketNumber;
                sample.VehicleNumber = _header[row].VehicleNumber;
                sample.AnalysisDate = _header[row].SampleDate;
                sample.ContractNumber = _header[row].ContractNumber;
                sample.LoadNumber = _header[row].LoadNumber;
                sample.BillOfLoad = _header[row].BillOfLading;
                sample.LotNumber = _header[row].LotNumber;
                sample.DataSourceTypeID = dataSourceTypeID;
                sample.AnalyzerMaterialID = _header[row].AnalyzerMaterialID;
                sample.AnalyzerSupplierID = _header[row].UserDefinedSupplierID;
                sample.AnalyzerSupplierLocationID = _header[row].UserDefinedSupplierLocationID;                
                sample.Notes = _header[row].Notes;

                //8/17/23
                sample.SampleTestTypeID = 0;


                ErrorInfo outPut = AIMManager.SaveSample(ref sample);
                return outPut.RecordID;
            }

            catch (Exception ex)
            {
                MessageBox.Show("AddSampleHeader - " + ex.Message);
                return -1;
            }
        }
            
        //8/17/22
        //private void AddSampleResults(Int32 sampleID, Int32 row, Int32 dataSourceTypeID, Boolean dataSourceIsMapped, Boolean isQualityControl, Boolean orientation)
        private void AddSampleResults(Int32 sampleID, Int32 row, Int32 dataSourceTypeID, Boolean dataSourceIsMapped, Boolean isQualityControl, Boolean orientation, Int32 importType)

        {
            String msg = "";
            Int32 errCount = 0;

            try
            {                
                if (isQualityControl == true)
                {                    
                    Decimal resultVal = 0;
                    List<ImportDetailInfo> detailQualityControl = new List<ImportDetailInfo>();
                    detailQualityControl = _header[row].DetailImport;
                    foreach (ImportDetailInfo item in detailQualityControl)                     
                    {
                        // * * * The column heading on the import file needs to match the QualityControlParameter Name * * *

                        //Get parameterID
                        SampleQualityParameterInfo param = new SampleQualityParameterInfo();
                        param = AIMManager.GetQualityParameterName(item.AnalyzerNutrientCode, dataSourceTypeID);     

                        if (param != null)
                        {
                            SampleQualityInfo qualityResult = new SampleQualityInfo();
                            qualityResult.SampleID = sampleID;
                            qualityResult.SampleQualityParameterID = param.SampleQualityParameterID;
                            resultVal = 0;
                            Decimal.TryParse(item.ResultValueString, out resultVal);
                            qualityResult.ResultValueNumeric = resultVal;
                            qualityResult.IsActive = true;
                            qualityResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            qualityResult.SampleQualityID = 0;
                            AIMManager.InsertSampleQuality(qualityResult);
                        }

                        //TODO - 4/6/18 - remove the "portrait" section below when the portrait logic is added to code under Import Button (dated 4/6/18).
                        else
                        {
                            if (orientation == false) //portrait
                            {
                                if (errCount < 5)
                                {
                                    msg = "Quality Control parameter on import document does not exist on the Analyzer Quality Control Parameter table.";
                                    msg += Environment.NewLine + Environment.NewLine + "    Parameter missing:     " + item.AnalyzerNutrientCode + Environment.NewLine;
                                    msg += Environment.NewLine + Environment.NewLine + "Sample cannot be imported until parameter is added -- or the ";
                                    msg += Environment.NewLine + "column-name on the import document is changed to match an existing parameter.";
                                    MessageBox.Show(msg, "QC Parameter Missing ...", MessageBoxButtons.OK);
                                    errCount++;
                                }
                            }
                        }
                    }

                    //Update results with Equation

                    //9/9/20 - change to use AIMCoreGlobal
                    //AIMManager.SaveSampleResultUsingEquation(sampleID, userGlobal.CFCLoginUser.CFCUserID, AIMGlobal.aimDatabase.AIMDatabaseID);
                    AIMManager.SaveSampleResultUsingEquation(sampleID, userGlobal.CFCLoginUser.CFCUserID, CFCAIMCore.Global.aimDatabase.AIMDatabaseID);
                }

                else
                {                    
                    //SampleResult Table -------------------------------            
                    List<ImportDetailInfo> detailList = new List<ImportDetailInfo>();
                    detailList = _header[row].DetailImport;
                    foreach (ImportDetailInfo item in detailList)
                    {
                        //5/4/18 - changed the code below.  If nutrient is not found on the AnalyzerNutrient table 
                        //                                  (for the datasourcetypeID and AnalyzerNutrientCode), changed
                        //                                  the code below, which was dated 3/22/18, to insert the nutrient 
                        //                                  (instead of skipping altogether).

                        AnalyzerNutrientInfo nutrientIsActive = new AnalyzerNutrientInfo();
                        nutrientIsActive = AIMManager.GetAnalyzerNutrient(dataSourceTypeID, item.AnalyzerNutrientCode);

                        //5/4/18
                        if (nutrientIsActive == null)
                        {
                            //add analyzer nutrient
                            AnalyzerNutrientInfo AnalyzerNutrient = new AnalyzerNutrientInfo();
                            AnalyzerNutrient.AnalyzerNutrientID = 0;
                            AnalyzerNutrient.AnalyzerNutrientCode = item.AnalyzerNutrientCode;
                            AnalyzerNutrient.AnalyzerNutrientName = item.AnalyzerNutrientCode;
                            AnalyzerNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            AnalyzerNutrient.DataSourceTypeID = dataSourceTypeID;
                            AnalyzerNutrient.UnitType = "";
                            AnalyzerNutrient.ConversionFactor = 1;
                            AnalyzerNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            ErrorInfo error = new ErrorInfo();
                            error = AIMManager.SaveAnalyzerNutrient(ref AnalyzerNutrient);
                            if (error.ErrorNumber > 0)
                            {
                                return;
                            }

                            //add to sampleResult
                            SampleResultInfo sampleResult = new SampleResultInfo();
                            sampleResult.SampleID = sampleID;
                            sampleResult.AnalyzerNutrientID = error.RecordID;
                            sampleResult.AnalyzerNutrientCode = item.AnalyzerNutrientCode;
                            sampleResult.ResultValue = item.ResultValue;
                            sampleResult.Unit = item.UnitType;
                            if (dataSourceIsMapped == true)
                            {
                                sampleResult.DataSourceTypeID = item.AnalyzerDataSourceTypeID;
                            }
                            else
                            {
                                sampleResult.DataSourceTypeID = dataSourceTypeID;
                            }

                            //9/9/20 - change to use AIMCoreGlobal
                            //sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
                            sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                            sampleResult.SqlResultID = String.Empty;

                            //8/17/22
                            //AIMManager.InsertSampleResult(sampleResult, null);
                            AIMManager.InsertSampleResult(sampleResult, null, importType);
                        }
                        else
                        {
                            if (nutrientIsActive.IsActive == true)
                            {
                                //add to sampleResult
                                SampleResultInfo sampleResult = new SampleResultInfo();
                                sampleResult.SampleID = sampleID;
                                sampleResult.AnalyzerNutrientCode = item.AnalyzerNutrientCode;
                                sampleResult.ResultValue = item.ResultValue;                                
                                sampleResult.Unit = item.UnitType;
                                if (dataSourceIsMapped == true)
                                {
                                    sampleResult.DataSourceTypeID = item.AnalyzerDataSourceTypeID;
                                }
                                else
                                {
                                    sampleResult.DataSourceTypeID = dataSourceTypeID;
                                }

                                //9/9/20 - change to use AIMCoreGlobal
                                //sampleResult.AimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
                                sampleResult.AimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;

                                //8/15/22
                                //sampleResult.SqlResultID = String.Empty;
                                if (item.SQLResultIDInfo != String.Empty)
                                {
                                    sampleResult.SqlResultID = item.SQLResultIDInfo;
                                }
                                else
                                {
                                    sampleResult.SqlResultID = String.Empty;
                                }
                                
                                //8/17/22
                                //AIMManager.InsertSampleResult(sampleResult, null);
                                AIMManager.InsertSampleResult(sampleResult, null, importType);

                            }
                        }
                    }
                    
                }
            }

            catch (Exception ex)
            {                
                MessageBox.Show("AddSampleResults - " + ex.Message);
            }
        }

		private void repbtnFileDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
		private void grdPreviewView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
		{
			Int32 specificMessage = 0;

			if (e.Column == colMessagePreview)
			{
                //-----------------------------------------------------------
                //Check specific message 

                //1/31/18 - added check for null -- seems to have fixed the occasional error that would pop on next line down
                if (grdPreviewView.GetRowCellValue(e.RowHandle, colSpecificMessage) != null)
                {
                    specificMessage = (Int32)(grdPreviewView.GetRowCellValue(e.RowHandle, colSpecificMessage));
                    if (specificMessage == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists ||
                        specificMessage == (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist ||
                        specificMessage == (Int32)SampleImportSpecificMessageDetail.Rejected || 
                        specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveMaterial ||
                        specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveOwner ||
                        specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation ||
                        specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveSupplier ||
                        specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation
                        )
                    {
                        e.Appearance.BackColor = Color.Yellow;
                    }
                }

                //-----------------------------------------------------------
                //Date error
                else if (specificMessage == (Int32)SampleImportSpecificMessageDetail.DateError)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }

				//-----------------------------------------------------------
				//Check detail for warnings
				String msg = "";
				msg = Convert.ToString(grdPreviewView.GetRowCellValue(e.RowHandle,colMessagePreview));										
				if (msg.Length == 0)
				{
					Int32 ID = 0;
					ID = Convert.ToInt32(grdPreviewView.GetRowCellValue(e.RowHandle, colImportHeaderID));
					if (CountDetailWarningsPerSample(ID) > 0)
					{
						e.Appearance.BackColor = Color.Yellow;
						e.DisplayText = "Warning";
					}
				}
								
				if (specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingMaterial ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingOwner ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocation ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingSupplier ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocation ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.DateError || 
                    specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveMaterial ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveOwner ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveSupplier ||
					specificMessage == (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation					

					)
				{
					e.Appearance.ForeColor = Color.Red;					
				}
				else
				{
					e.Appearance.ForeColor = Color.Blue;
				}
				

			}
			if (e.Column == colSampleCodePreview)
			{
				if (Convert.ToInt32(grdPreviewView.GetRowCellValue(e.RowHandle, colDetailMessageErrorTotalCount)) > 0)
				{
					e.Appearance.BackColor = Color.Yellow;
				}
			}
		}
		public void Validate()
		{

            //8/20/18                
            AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = false;

            //10/16/20 moved from background worker (causing problems there)
            txtCounter.Text = "";

            ClearCounts();
			progressBarPreviewImport.Value = 0;
			SetControlsForProcess(true);
			if (backgroundValidate.IsBusy == false)
			{				
				backgroundValidate.RunWorkerAsync();
			}
			else
			{
				MessageBox.Show("Validate is busy. Please try later.", "Warning...");
			}			
		}

		private void btnRefreshPreview_Click(object sender, EventArgs e)
		{				
			//Validate();					//12/4/19 - commented, this button is never made visible??
		}

		private void setGlobalRequiredField(Int32 msg)
		{
                       
            AIMGlobal.analyzerRequiredField = "";

            switch (msg)
			{
				case (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref:
                case (Int32)SampleImportSpecificMessageDetail.InactiveSupplier:
                    AIMGlobal.analyzerRequiredField = "SUPPLIER";
					break;
				case (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref:
                case (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation:
                    AIMGlobal.analyzerRequiredField = "SUPPLIERLOCATION";
					break;
				case (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref:
                case (Int32)SampleImportSpecificMessageDetail.InactiveOwner:
                    AIMGlobal.analyzerRequiredField = "OWNER";
					break;
				case (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref:
                case (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation:
                    AIMGlobal.analyzerRequiredField = "OWNERLOCATION";
					break;
				case (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref:
                case (Int32)SampleImportSpecificMessageDetail.InactiveMaterial:
                    AIMGlobal.analyzerRequiredField = "MATERIAL";
					break;
				default:
					break;
			}
		}

		private void repHyperMessage_Click(object sender, EventArgs e)
		{
            		
			String sampleCode = "";
			Int32 message = 0;
			Int32 ID = 0;
			String msg = "";
			String requiredField = "";
                                       
            AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = false;                                                                      //8/20/18             
            Int32 importType = 0;                                                                                                               //1/30/18
            importType = GetImportType(_selectedDefinitionID);                                                                                  //1/30/18

            List<FileTypeInfo> fileType = new List<FileTypeInfo>();
			fileType = AIMManager.GetFileType(_dataSource.ImportFileTypeID);	 

			AIMGlobal.analyzerDataSourceName = txtDataSource.Text;
			
			sampleCode = (String)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSampleCode);				
			message = (Int32)grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colSpecificMessage);			
			ID = Convert.ToInt32(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colImportHeaderID));

			//Yellow Warnings -- will expand the detail
			if (message == 0 && CountDetailWarningsPerSample(ID) > 0)
			{
				if (grdPreviewView.GetMasterRowExpanded(grdPreviewView.FocusedRowHandle) == false)
				{
					grdPreviewView.SetMasterRowExpanded(grdPreviewView.FocusedRowHandle, true);
				}
				else
				{
					grdPreviewView.SetMasterRowExpanded(grdPreviewView.FocusedRowHandle, false);
				}			
			}

			//--------------------------------------------------------------------------
			if (message != 0)
			{						
				AIMGlobal.analyzerDataSourceTypeID = _dataSource.DataSourceTypeID;
				AIMGlobal.userdefinedImportError = message;		
                	
				setGlobalRequiredField(message);

				if (message == (Int32)SampleImportSpecificMessageDetail.DateError)
				{
					String filename = "";
					filename = grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colImportSourceFileName).ToString();
					MessageBox.Show("An invalid Sample Date exists on: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode + Environment.NewLine + Environment.NewLine + "     ImportFile:  " + filename + Environment.NewLine + Environment.NewLine + "Sample cannot be imported until the date is fixed.", "Date Error...", MessageBoxButtons.OK);
					AIMGlobal.userdefinedImportError = 0;					
					return;
				}

				//--------------------------------------------------------------------------
				if (message == (Int32)SampleImportSpecificMessageDetail.SampleAlreadyExists)
				{
					SampleInfo sample = new SampleInfo();
					sample = AIMManager.GetSample(sampleCode);
					if (sample != null)
					{
						MessageBox.Show("A sample already exists for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode + Environment.NewLine + "     Sample Date:  " + sample.SampleDate + Environment.NewLine + Environment.NewLine + "If accepted, a DUPLICATE sample will be created for this Sample Code.", "Sample Already Exists...", MessageBoxButtons.OK);
					}
					AIMGlobal.userdefinedImportError = 0;
					return;
				}

				//--------------------------------------------------------------------------
				if (message == (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist)
				{
                   
                    if (importType == (Int32)EnumImportType.AddNewSamplesAndResults)                             
                    {
						msg = "There is no existing sample for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode + Environment.NewLine + Environment.NewLine + "If accepted, a new sample will be created for this Sample Code.";
					}
                    
					else
					{
						msg = "There is no existing sample for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode + Environment.NewLine + Environment.NewLine + "Results cannot be imported.";
					}
					MessageBox.Show(msg, "Sample Does Not Exist...", MessageBoxButtons.OK);
					AIMGlobal.userdefinedImportError = 0;
					return;
				}

				//--------------------------------------------------------------------------
				if (message == (Int32)SampleImportSpecificMessageDetail.CreateSample)
				{
					MessageBox.Show("On Import, a new sample will be created for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode, "Create Sample...", MessageBoxButtons.OK);
					AIMGlobal.userdefinedImportError = 0;
					return;
				}

				//--------------------------------------------------------------------------
				if (message == (Int32)SampleImportSpecificMessageDetail.MergeResults)
				{					
					MessageBox.Show("On Import, sample results will be added to: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode, "Add Results...", MessageBoxButtons.OK);
					AIMGlobal.userdefinedImportError = 0;
					return;
				}

                //4/2/19
                //--------------------------------------------------------------------------                
                if (message == (Int32)SampleImportSpecificMessageDetail.AddResultsAndUpdateHeaderFields)
                {
                    MessageBox.Show("On Import, header fields will be updated and sample results will be added to: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode, "Add Results and Update Header Fields...", MessageBoxButtons.OK);
                    AIMGlobal.userdefinedImportError = 0;
                    return;
                }

                //--------------------------------------------------------------------------
                if (message == (Int32)SampleImportSpecificMessageDetail.CreateAndOrMergeResults)
                {
                    MessageBox.Show("On Import, if the sample code already exists in the system, the results will be added; otherwise, a new sample will be created for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode, "Create Sample/Add Results...", MessageBoxButtons.OK);
                    AIMGlobal.userdefinedImportError = 0;
                    return;
                }

                //--------------------------------------------------------------------------
                if (message == (Int32)SampleImportSpecificMessageDetail.Rejected)
				{
					MessageBox.Show("Sample and results will not be imported.", "Rejected...", MessageBoxButtons.OK);
					AIMGlobal.userdefinedImportError = 0;
					return;
				}
                
                if (message == (Int32)SampleImportSpecificMessageDetail.MissingMaterial ||
					message == (Int32)SampleImportSpecificMessageDetail.MissingOwner ||
					message == (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocation ||
					message == (Int32)SampleImportSpecificMessageDetail.MissingSupplier ||
					message == (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocation)
				{
					if (message == (Int32)SampleImportSpecificMessageDetail.MissingMaterial)
					{
						requiredField = "Material";
					}
					if (message == (Int32)SampleImportSpecificMessageDetail.MissingOwner)
					{
						requiredField = "Owner";
					}
					if (message == (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocation)
					{
						requiredField = "Owner Location";
					}
					if (message == (Int32)SampleImportSpecificMessageDetail.MissingSupplier)
					{
						requiredField = "Supplier";
					}
					if (message == (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocation)
					{
						requiredField = "Supplier Location";
					}
						  
					msg = "The " + requiredField + " for this sample is missing from the " + fileType[0].Name + " file.";
					msg += Environment.NewLine + "Please fix the file and try again. ";
					msg += Environment.NewLine + Environment.NewLine + "Sample and results will not be imported.";
					MessageBox.Show(msg, "Rejected...", MessageBoxButtons.OK);
					AIMGlobal.userdefinedImportError = 0;
					return;
				}							

				//--------------------------------------------------------------------------
				this.Hide();

                //Refresh _header and set global so it can be used on ExternalAllCrossRefUpdate form							
                CFCAIM.AIMGlobal.crossRef = _header;																									   
				CFCAIM.AIMGlobal.analyzerDataSourceTypeID = _dataSource.DataSourceTypeID;
                CFCAIM.AIMGlobal.importDataBindDataSourceType = "UserDefined";

				ImportExternalCrossReferenceUpdate crossRef = new ImportExternalCrossReferenceUpdate();				
				Analyzer analyzerMaintenance = new Analyzer();                              
                DataSourceCrossReference dataSourceCrossReference = new DataSourceCrossReference();       

								
				switch (message)
				{				
					case (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref:						
					case (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref:
					case (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref:
					case (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref:
					case (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref:

                        //10/6/21 - commented for resize problem
						//crossRef.WindowState = FormWindowState.Normal;                      

                        crossRef.ShowDialog();						
						_header = CFCAIM.AIMGlobal.crossRef;					
			
                        //2/2/18
                        //Validate();
                        DialogResult dr = new DialogResult();
                        dr = MessageBox.Show("Do you want to refresh/validate the samples?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (dr == DialogResult.Yes)
                        {
                            Validate();
                        }
                        grdPreviewView.RefreshData();
						grdPreview.Refresh();
						break;
                                            
                    case (Int32)SampleImportSpecificMessageDetail.InactiveMaterial:
					case (Int32)SampleImportSpecificMessageDetail.InactiveOwner:
					case (Int32)SampleImportSpecificMessageDetail.InactiveOwnerLocation:
					case (Int32)SampleImportSpecificMessageDetail.InactiveSupplier:
					case (Int32)SampleImportSpecificMessageDetail.InactiveSupplierLocation:
						AIMGlobal.importDataSourceTypeID = _dataSource.DataSourceTypeID;
						AIMGlobal.importDataBindDataSourceType = "UserDefined";
						
                        //10/6/21 - commented for resize problem
                        //analyzerMaintenance.WindowState = FormWindowState.Normal;                       

                        analyzerMaintenance.ShowDialog();
						Validate();
						grdPreviewView.RefreshData();
						grdPreview.Refresh();
						break;

                    case (Int32)SampleImportSpecificMessageDetail.MissingDatasourceCrossReference:

                        //8/20/18                
                        AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = true;

                        AIMGlobal.analyzerDataSourceTypeID = (Int32)_dataSource.DataSourceTypeID;
						AIMGlobal.analyzerDataSourceDefinitionID = _selectedDefinition.UserDefinedDataSourceDefinitionID;

                        //10/6/21 - commented for resize problem
                        //dataSourceCrossReference.WindowState = FormWindowState.Normal;                  

                        dataSourceCrossReference.ShowDialog();

                        //2/2/18
                        //Validate();
                        DialogResult drR = new DialogResult();
                        drR = MessageBox.Show("Do you want to refresh/validate the samples?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (drR == DialogResult.Yes)
                        {
                            Validate();
                        }
                        grdPreviewView.RefreshData();
						grdPreview.Refresh();
						break;

					default:
						break;
				}

				this.Show();
				AIMGlobal.userdefinedImportError = 0;                
				tabControl1.SelectedTab = tpPreviewResults;
			}
		}

		private void btnCancelPreview_Click(object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.stopPreviewingImport = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private void lueSupplierLocation_EditValueChanged(object sender, EventArgs e)
		{		
			ChangeDropdownBackcolor(lueSupplierLocation);
			_previewImport = true;

            //12/16/20 - commented
            //SaveRequiredFields();           //12/5/19

        }	   
		private void lueSupplier_EditValueChanged(object sender, EventArgs e)
		{
                        
			try
			{
				ChangeDropdownBackcolor(lueSupplier);
				DataBindSourceLocation();
				_previewImport = true;

                //12/16/20 - commented
                //SaveRequiredFields();           //12/5/19
            }
			catch (Exception ex)									
			{
				MessageBox.Show(ex.Message);
			}
		}

		
		private void MoveFileToArchive()                
        {
                        
            //For each file selected, move to archive folder

            List<FilesInfo> files = new List<FilesInfo>();			
			ErrorInfo error = new ErrorInfo();
			Int32 fileCount = 1;

            //Get files from grid where fileStatus = true/checked					            
            files = (List<FilesInfo>)grdFiles.DataSource;            

            if (files == null)
			{
                return;                
			}

			try
			{
				foreach (var item in files)
				{
					fileCount = 1;
					if (item.FileStatus == true)
					{		
						
						item.NewFileName = String.Empty;

                        //8/6/21 - Perdue found that changing the archive folder wasn't actually saving it into that changed folder.
                        //if (Directory.Exists(item.BackupPath) == false)
                        //{
                        //	Directory.CreateDirectory(item.BackupPath);
                        //}

                        //while (fileCount > 0)
                        //{							
                        //	if (File.Exists(item.BackupPath + "\\" + item.FileName) == false)
                        //	{								
                        //		File.Move(item.ProcessPathAndName, item.BackupPath + "\\" + item.FileName);
                        //		fileCount = 0;
                        //	}
                        //	else
                        //	{
                        //		item.NewFileName = fileCount + "_" + item.FileName;	
                        //		if (item.NewFileName.Length > 0)
                        //		{
                        //			while (File.Exists(item.BackupPath + "\\" + item.NewFileName) == true)
                        //			{									
                        //				fileCount += 1;
                        //				item.NewFileName = fileCount.ToString() + item.NewFileName.Substring(item.NewFileName.IndexOf("_"));
                        //			}
                        //		}								
                        //		File.Move(item.ProcessPathAndName, item.BackupPath + "\\" + item.NewFileName);
                        //		fileCount = 0;
                        //	}
                        //}
                        
                        if (Directory.Exists(_strArchivePath.ToString()) == false)
                        {
                            Directory.CreateDirectory(_strArchivePath.ToString());
                        }

                        while (fileCount > 0)
                        {
                            if (File.Exists(_strArchivePath + "\\" + item.FileName) == false)
                            {
                                File.Move(item.ProcessPathAndName, _strArchivePath + "\\" + item.FileName);
                                fileCount = 0;
                            }
                            else
                            {
                                item.NewFileName = fileCount + "_" + item.FileName;
                                if (item.NewFileName.Length > 0)
                                {
                                    while (File.Exists(_strArchivePath + "\\" + item.NewFileName) == true)
                                    {
                                        fileCount += 1;
                                        item.NewFileName = fileCount.ToString() + item.NewFileName.Substring(item.NewFileName.IndexOf("_"));
                                    }
                                }
                                File.Move(item.ProcessPathAndName, _strArchivePath + "\\" + item.NewFileName);
                                fileCount = 0;
                            }
                        }
                    }
				}
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex + ". Unable to move file.";
				error.ErrorNumber = 1;

                //10/4/19
                _strFolderMoveError = error.ErrorMessage;


            }
            
        }
        		
		private void MoveFileToError()
		{
			//For each file selected, move to error folder

			List<FilesInfo> files = new List<FilesInfo>();
			ErrorInfo error = new ErrorInfo();
			Int32 fileCount = 1;

			//Get files from grid where fileStatus = true/checked					
			files = (List<FilesInfo>)grdFiles.DataSource;
			if (files == null)
			{
				return;
			}

			try
			{
				foreach (var item in files)
				{
					fileCount = 1;
					if (item.FileStatus == true)
					{

						item.NewFileName = String.Empty;

						if (Directory.Exists(item.ProcessPath + @"\Error") == false)
						{
							Directory.CreateDirectory(item.ProcessPath + @"\Error");
						}

						while (fileCount > 0)
						{
							if (File.Exists(item.ProcessPath + @"\Error\\" + item.FileName) == false)
							{
								File.Move(item.ProcessPathAndName, item.ProcessPath + @"\Error\\" + item.FileName);
								fileCount = 0;
							}
							else
							{
								item.NewFileName = fileCount + "_" + item.FileName;
								if (item.NewFileName.Length > 0)
								{
									while (File.Exists(item.ProcessPath + @"\Error\\" + item.NewFileName) == true)
									{
										fileCount += 1;
										item.NewFileName = fileCount.ToString() + item.NewFileName.Substring(item.NewFileName.IndexOf("_"));
									}
								}
								File.Move(item.ProcessPathAndName, item.ProcessPath + @"\Error\\" + item.NewFileName);
								fileCount = 0;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				error.ErrorMessage = ex + ". Unable to move file.";
				error.ErrorNumber = 1;

                //10/4/19
                _strFolderMoveError = error.ErrorMessage;
            }

		}

		private void backgroundImport_DoWork(object sender, DoWorkEventArgs e)
		{
			Boolean result = false;
            String msg = "";                   //10/4/19
			try
			{						
				result = SaveSample();
				if (result == true)
				{
                    //10/4/19 - added global return msg (if archive folder's permissions aren't set, file would not go to Archive, and user wouldn't know why)
                    _strFolderMoveError = "";

                    MoveFileToArchive();
                    
                    //10/4/19
                    if (_strFolderMoveError != "")
                    {
                        AIMGlobal.SplashScreenHide();

                        //2/4/19
                        msg = "";
                        if (AIMGlobal.mainMenuCustomToolStrip == true)                      
                        {
                            msg += "Se produjo un error al mover el archivo de importación a la carpeta ARCHIVO.";
                            msg += Environment.NewLine + Environment.NewLine + "Por favor verifique los permisos de la carpeta.";
                            msg += Environment.NewLine + "_____________________________________________________________________";
                        }
                       
                        msg += Environment.NewLine + Environment.NewLine + "There was an error moving the import file to the ARCHIVE folder.";
                        msg += Environment.NewLine + Environment.NewLine + "Please check the folder's permissions.";
                        msg += Environment.NewLine + "_____________________________________________________________________";
                        msg += Environment.NewLine + Environment.NewLine + "Message Detail:";
                        msg += Environment.NewLine + Environment.NewLine + _strFolderMoveError;
                        MessageBox.Show(msg, "Warning...");
                    }

                }
				else
				{
                    //10/4/19 - added global return msg (if rejected folder's permissions aren't set, file would not go to Rejected, and user wouldn't know why)
                    _strFolderMoveError = "";

                    MoveFileToError();

                    //10/4/19
                    if (_strFolderMoveError != "")
                    {
                        AIMGlobal.SplashScreenHide();
                        msg = "Se produjo un error al mover el archivo de importación a la carpeta RECHAZADA.";
                        msg += Environment.NewLine + Environment.NewLine + "Por favor verifique los permisos de la carpeta.";
                        msg += Environment.NewLine + "_____________________________________________________________________";
                        msg += Environment.NewLine + Environment.NewLine + "There was an error moving the import file to the REJECTED folder.";
                        msg += Environment.NewLine + Environment.NewLine + "Please check the folder's permissions.";
                        msg += Environment.NewLine + "_____________________________________________________________________";
                        msg += Environment.NewLine + Environment.NewLine + "Message Detail:";
                        msg += Environment.NewLine + Environment.NewLine + _strFolderMoveError;
                        MessageBox.Show(msg, "Warning...");
                    }
                }	  
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void backgroundImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{

			SetControlsForProcess(false);

            //9/9/20 - change to use AIMCoreGlobal
            //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
            CFCAIMCore.Global.importProgressBarCounter = 0;

            ClearCounts();
			
			List<FilesInfo> clearResults = new List<FilesInfo>();
			grdPreview.DataSource = clearResults;

			_header = new List<ImportHeaderInfo>();

			List<FilesInfo> clearFiles = new List<FilesInfo>();
			grdFiles.DataSource = clearFiles;
			LoadGridWithFiles();
						
			AIMGlobal.SplashScreenHide();
			MessageBox.Show("Import Complete");
                        
            tabControl1.SelectedIndex = 0;              //1/31/18 

           
        } 

        //12/5/19 - commented - not used
		//private void btnClearStatus_Click(object sender, EventArgs e)
		//{
		//	if (lueTemplateName.EditValue == null)
		//	{
		//		return;
		//	}
		//	var samples = from g in _header
		//				  select g;
		//	foreach (var item in samples)
		//	{
		//		item.Status = 0;					
		//	}
		//	grdPreviewView.RefreshData();

		//	UpdateCounts();		
		//}

		private void txtTotalWarnings_Click(object sender, EventArgs e)
		{
			// get id of 1st rec with DetailMessageStatus = 3 and move cursor there
			//Int32 ID = 0;
			//ID = Convert.ToInt32(grdPreviewView.GetRowCellValue(grdPreviewView.FocusedRowHandle, colImportHeaderID));
			

			///////string warning = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Message"]);

			//////Boolean found = false;
			//////while (found == false)
			//////{
			//////	foreach (ImportHeaderInfo item in _importResultsAll)
			//////	{
			//////		var detailWarn = (from g in item.DetailImport
			//////						  where g.DetailMessage.Length > 0
			//////						  group g by g.ImportHeaderID into samples
			//////						  select samples);
			//////	}
			//////}

		}

				

		private void btnNext_Click(object sender, EventArgs e)
		{
            			
			pnlCounts.Visible = true;
			tabControl1.SelectedIndex = 1;

			if (_okayToPreview == false)
			{	
				
				tabControl1.SelectedTab = tpSelectImport;
				tabControl1.TabPages.Remove(tpPreviewResults);

                //1/30/18
				//tabControl1.TabPages.Add(tpPreviewResults);
                tabControl1.TabPages.Insert(1, tpPreviewResults);

				btnPrior.Enabled = false;
				btnNext.Enabled = true;
				pnlCounts.Visible = false;

				textBox1.Text = "Select file(s) to be imported.";
				textBox2.Text = "Fill in yellow highlighted areas.";
				textBox3.Text = "Click Next arrow button below.";
			}				 
		}		 
		private void btnPrior_Click(object sender, EventArgs e)
		{
			tabControl1.SelectedIndex = 0;            
			pnlCounts.Visible = false;
			btnCrossReference.Visible = false;
			timer1.Stop();	
            
		}

		private void btnSelectExisting_Click(Object sender, EventArgs e)
		{
			pnlAdd.Visible = true;
			lueTemplateName.ShowPopup();
		}

		private void btnAddNew_Click(Object sender, EventArgs e)
		{
			try
			{

                //11/27/19 - commented, no longer need this check (user should be able to select a system datasource even when creating a user-defined template)
                //////Make sure a data-source exists before user gets into wizard
                ////List<DataSourceTypeInfo> dataSource = new List<DataSourceTypeInfo>();
                ////dataSource = AIMManager.GetDataSourceTypeUserDefined(true);
                ////if (dataSource.Count == 0)
                ////{
                ////	MessageBox.Show("Before a template can be added, a data-source and file type must be set up.  Please add a data-source and file type under Setup > Data Sources and Setup> File Types.", "Warning...");
                ////	return;
                ////}

                //Make sure a file-type is set up for the data-source before the user gets into wizard
                List<DataSourceTypeRefFileTypeInfo> fileType = new List<DataSourceTypeRefFileTypeInfo>();
                fileType = AIMManager.GetDataSourceTypeRefFileType();
                if (fileType.Count == 0)
                {
                    MessageBox.Show("Before a template can be added, a file type for the data-source must be added.  Please set up a file type under Setup > File Types.", "Warning...");
                    return;
                }

                // 6/3/16 - commented this because ShowDialog() below not working correctly.  
                //			Changed to close this form and open wizard instead (instead of using ShowDialog).
                //--------------------------------------------------------------
                //CFCAIM.AIMGlobal.ImportMappingTabControlVisible = false;
                //AIMGlobal.lblWizardCaption = "Add New Template";
                //mapWizard = new MapWizard();
                //this.Hide();
                //mapWizard.WindowState = FormWindowState.Normal;
                //mapWizard.ShowDialog();				
                //this.Show();
                //_selectedDefinitionID = CFCAIM.AIMGlobal.userdefinedDataSourceDefinitionID;
                //lueImportFileName.EditValue = _selectedDefinitionID;
                //TabControl.Visible = false;
                //if (CFCAIM.AIMGlobal.ImportMappingTabControlVisible == true)
                //{
                //	TabControl.Visible = true;
                //}		
                //--------------------------------------------------------------
                AIMGlobal.ImportMappingTabControlVisible = false;
				AIMGlobal.lblWizardCaption = "Add New Template";
				Close();
				mapWizard = new MapWizard();
				mapWizard.Show();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		private void btnEditMap_Click(Object sender, EventArgs e)
		{
			Close();
			AIMGlobal.analyzerDataSourceDefinitionID = (Int32)lueTemplateName.EditValue;
			AIMGlobal.ImportMappingTabControlVisible = true;

            //8/19/22
            AIMGlobal.romerImport = false;
            if (_selectedDefinition.ImportType == (Int32)EnumImportType.RomerResults)
            {
                AIMGlobal.romerImport = true;
            }


			ImportMapping importMapping;
            AIMGlobal.open_form(importMapping = new ImportMapping());

        }	


		private void timer1_Tick(Object sender, EventArgs e)
		{
			if (btnCrossReference.BackColor == SystemColors.Control)
			{
				btnCrossReference.BackColor = SystemColors.Info;
			}
			else
			{
				btnCrossReference.BackColor = SystemColors.Control;
			}
		}

		private void btnCrossReference_Click(Object sender, EventArgs e)
		{


            //12/23/20
            AIMGlobal.userDefinedDefID = _dataSource.UserDefinedDataSourceDefinitionID;

            AIMGlobal.analyzerDataSourceName = txtDataSource.Text;
			Hide();

            //if there are detail datasource cross-refs missing, showing that cross-ref screen first. User will have to fix that cross-ref, 
            // rerun import and then fix any other cross-references that may exist.		
			var results = from g in _header
						  where g.DetailDataSourceCrossRefMissingCount > 0
						  select new
						  {
							  g.ImportHeaderID,
							  g.Status
						  };								   
			if (results.Count() > 0)
			{

                               
                AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = true;                                                                   //8/20/18 
                DataSourceCrossReference dataSourceCrossReference = new DataSourceCrossReference();
				AIMGlobal.analyzerDataSourceTypeID = (Int32)_dataSource.DataSourceTypeID;
				AIMGlobal.analyzerDataSourceDefinitionID = _selectedDefinition.UserDefinedDataSourceDefinitionID;

                //10/6/21 - commented for resize issue
				//dataSourceCrossReference.WindowState = FormWindowState.Normal;                  

                dataSourceCrossReference.ShowDialog();		
			}		
			else
			{
                                
                AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = false;                                                                  //8/20/18
                CFCAIM.AIMGlobal.crossRef = _header;
				CFCAIM.AIMGlobal.analyzerDataSourceTypeID = _dataSource.DataSourceTypeID;
				CFCAIM.AIMGlobal.importDataBindDataSourceType = "UserDefined";
				ImportExternalCrossReferenceUpdate crossRef = new ImportExternalCrossReferenceUpdate();
				Analyzer analyzerMaintenance = new Analyzer();                          

                //10/6/21 - commented for resize issue
				//crossRef.WindowState = FormWindowState.Normal;                                  

                crossRef.ShowDialog();
				_header = CFCAIM.AIMGlobal.crossRef;
			}                      

            //2/2/18
            //Validate();
            DialogResult dr = new DialogResult();
            dr = MessageBox.Show("Do you want to refresh/validate the samples?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Validate();
            }            
			grdPreviewView.RefreshData();
			grdPreview.Refresh();

			Show();			
			AIMGlobal.userdefinedImportError = 0;
			tabControl1.SelectedTab = tpPreviewResults;
		}

        //12/5/19 - moved to preview
        private void rbYesDups_CheckedChanged(Object sender, EventArgs e)
        {
            //////12/2/19 
            ////if (gbAllowDuplicates.Visible == true && _templateChange == false)     //added this so "Allow duplicate sample code?" msg doesn't show when selecting template
            ////{
            ////    //12/5/9
            ////    //if (rbYesDups.Checked == true)
            ////    if (txtAllowDuplicates.Text == "Yes")

            ////    {
            ////        DialogResult dr = new DialogResult();
            ////        dr = MessageBox.Show("Allow duplicate sample codes to be imported?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            ////        if (dr != DialogResult.Yes)
            ////        {
            ////            //12/5/19
            ////            //rbYesDups.Checked = false;
            ////            //rbNoDups.Checked = true;
            ////            txtAllowDuplicates.Text = "No";

            ////            return;
            ////        }
            ////    }
            ////    _previewImport = true;
            ////}

		}
               
        private void btnAddAnalyzerDataSource_Click(Object sender, EventArgs e)
        {
            try
            {
                if (txtAnalyzerDataSourceName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter Data Source Name.", "Warning...");
                    txtAnalyzerDataSourceName.Focus();
                    return;
                }

				if (txtAnalyzerDataSourceCode.Text.Length == 0)
				{
					MessageBox.Show("Please enter Data Source Code.", "Warning...");
					txtAnalyzerDataSourceCode.Focus();
					return;
				}

				AnalyzerDataSourceTypeInfo analyzerDataSource = new AnalyzerDataSourceTypeInfo();
                analyzerDataSource.AnalyzerDataSourceTypeID = 0;
                analyzerDataSource.AnalyzerDataSourceTypeName = txtAnalyzerDataSourceName.Text.ToString();
				analyzerDataSource.AnalyzerDataSourceTypeCode = txtAnalyzerDataSourceCode.Text.ToString();
				analyzerDataSource.DataSourceTypeID = (Int32)_dataSource.DataSourceTypeID;
                analyzerDataSource.IsActive = true;
                analyzerDataSource.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ErrorInfo error = new ErrorInfo();
                error = AIMManager.SaveAnalyzerDataSource(ref analyzerDataSource, true);

                if (error.ErrorNumber > 0)
                {
                    return;
                }
                DataBindGrid_AnalyzerDataSource();
                txtAnalyzerDataSourceName.Text = String.Empty;
				txtAnalyzerDataSourceCode.Text = String.Empty;

			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataBindGrid_AnalyzerDataSource()
        {
            List<DataSourceTypeInfo> datasourceType = new List<DataSourceTypeInfo>();
            datasourceType = AIMManager.GetDataSourceType(true);
            replueDataSourceType_AnalyzerDataSource.DataSource = datasourceType;
            XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType_AnalyzerDataSource, "DataSourceTypeID", "Name", "TypeDisplay");
            replueDataSourceType_AnalyzerDataSource.Columns["TypeDisplay"].Caption = "Type";

            Int32 rowNum = 0;
            Int32.TryParse(grdAnalyzerDataSourceView.FocusedRowHandle.ToString(), out rowNum);

            if ((Int32)_dataSource.DataSourceTypeID != 0)
            {
                grdAnalyzerDataSource.DataSource = AIMManager.GetAnalyzerDataSource((Int32)_dataSource.DataSourceTypeID, true);
                grdAnalyzerDataSourceView.FocusedRowHandle = rowNum;
            }
            else
            {
                grdAnalyzerDataSource.DataSource = "";
            }

        }

        private void repbtnDeleteAnalyzerDataSource_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                AnalyzerDataSourceTypeInfo datasourceRef = new AnalyzerDataSourceTypeInfo();
                datasourceRef = (AnalyzerDataSourceTypeInfo)grdAnalyzerDataSourceView.GetFocusedRow();

                //if analyzer data source exists on cross-ref, do not allow delete
                List<DataSourceDetailInfo> xref = new List<DataSourceDetailInfo>();

                xref = AIMManager.GetDataSourceDetail((Int32)_dataSource.DataSourceTypeID, datasourceRef.AnalyzerDataSourceTypeID);
                if (xref != null && xref.Count != 0)
                {
                    MessageBox.Show("This data source exists on the Data Source cross-reference table. You must first delete it under the button, 'Cross Reference AIM Data Source', above.", "Warning...", MessageBoxButtons.OK);
                    return;
                }


                datasourceRef.IsActive = !datasourceRef.IsActive;
                datasourceRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.SaveAnalyzerDataSource(ref datasourceRef, true);
                DataBindGrid_AnalyzerDataSource();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdAnalyzerDataSourceView_ValidateRow(Object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {             
            AnalyzerDataSourceTypeInfo analyzerDataSource = new AnalyzerDataSourceTypeInfo();
            analyzerDataSource = (AnalyzerDataSourceTypeInfo)grdAnalyzerDataSourceView.GetFocusedRow();
            analyzerDataSource.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveAnalyzerDataSource(ref analyzerDataSource, true);
            DataBindGrid_AnalyzerDataSource();
        }

       

        private void btnPreviewGridResultsAnalyzerDataSource_Click(Object sender, EventArgs e)
        {            
            PreviewGridResults(grdAnalyzerDataSource, "Data Sources for " + lueTemplateName.Text);            
        }

        private void PreviewGridResults(DevExpress.XtraPrinting.IPrintable grid, String title)
        {
            try
            {
                //9/9/20 - change to use AIMCoreGlobal
                //AIMGlobal.previewGridResults(grid, title);
                CFCAIMCore.Global.previewGridResults(grid, title);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //12/5/19 - MOVED TO SETUP > DATASOURCES
		////private void btnDataSourceCrossReference_Click(Object sender, EventArgs e)
		////{
		////	Close();
		////	AIMGlobal.analyzerDataSourceTypeID = (Int32)_dataSource.DataSourceTypeID;
		////	AIMGlobal.analyzerDataSourceDefinitionID = _selectedDefinition.UserDefinedDataSourceDefinitionID;                       
  ////          AIMGlobal.dataSourceXRefComingFromPreviewImportScreen = false;                                                                      //8/20/18
  ////          CFCAIM.CrossReference.DataSourceCrossReference datasourceCrossReference;
		////	AIMGlobal.open_form(datasourceCrossReference = new DataSourceCrossReference());
		////}

		private void repositoryItemCheckEdit1_CheckedChanged(Object sender, EventArgs e)
		{			
			_previewImport = true;
		}

		private void ImportDataUserDefined_SizeChanged(Object sender, EventArgs e)
		{			
			btnResize.Visible = true;
		}

		private void btnResize_Click(Object sender, EventArgs e)
		{			
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		private void btnRefreshGrid_Click(Object sender, EventArgs e)
		{
			LoadGridWithFiles();
		}
        		

		private void btnAcceptAllFiles_Click(Object sender, EventArgs e)
		{
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

        //6/4/19
        private void btnBrowseErrorFolder_Click(object sender, EventArgs e)
        {            
            if (Directory.Exists(txtErrorFolder.Text) == true)
            {
                fbdSelectedDirectory.SelectedPath = txtErrorFolder.Text;
            }
            fbdSelectedDirectory.ShowDialog();
            txtErrorFolder.Text = fbdSelectedDirectory.SelectedPath;
            
        }

        //7/29/19
        private void repbtnAcceptHeader_Click(object sender, EventArgs e)
        {
            if (lueTemplateName.EditValue == null)
            {
                return;
            }

            SetAcceptReject((Int32)(grdPreviewHeaderView.GetRowCellValue(grdPreviewHeaderView.FocusedRowHandle, colImportHeaderID)), (Int32)SampleImportStatus.Accepted);

            grdPreviewHeader.DataSource = _header;
            grdPreviewHeaderView.RefreshData();
            UpdateCounts();

        }

        //7/29/19
        private void repbtnRejectHeader_Click(object sender, EventArgs e)
        {
            if (lueTemplateName.EditValue == null)
            {
                return;
            }
            if (grdPreviewHeaderView.GetRowCellValue(grdPreviewHeaderView.FocusedRowHandle, colImportHeaderID) != null)
            {
                SetAcceptReject((Int32)(grdPreviewHeaderView.GetRowCellValue(grdPreviewHeaderView.FocusedRowHandle, colImportHeaderID)), (Int32)SampleImportStatus.Cancelled);

                grdPreviewHeader.DataSource = _header;
                grdPreviewHeaderView.RefreshData();
                UpdateCounts();
            }
        }

        //12/5/19
        private void SaveRequiredFields()
        {
            if (_selectedDefinitionID != 0)
            {
                UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();
                definition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);

                //5/2/23 - added check for null
                if (definition != null)

                {
                    definition.AIMOwnerID = (Int32)lueOwner.EditValue;
                    definition.AIMOwnerLocationID = (Int32)lueOwnerLocation.EditValue;
                    definition.AIMSourceID = (Int32)lueSupplier.EditValue;
                    definition.AIMSourceLocationID = (Int32)lueSupplierLocation.EditValue;
                    definition.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                    ErrorInfo error = new ErrorInfo();
                    error = AIMManager.SaveDataSourceUserDefinition(ref definition);
                    if (error.ErrorNumber > 0)
                    {
                        MessageBox.Show("Error saving AIM fields", "Warning...");
                        return;
                    }
                }
                
            }
        }

        //7/30/19
        private static String ChecksBeforeImport(Int32 defID, List<ImportHeaderInfo> hdr)
        {
            String rtn = "";
            String msg = "";            
            List<FilesInfo> files = new List<FilesInfo>();

            //Make sure a template has been selected
            UserDefinedDataSourceDefinitionInfo def = new UserDefinedDataSourceDefinitionInfo();
            if (defID != 0)
            {
                def = AIMManager.GetDataSourceUserDefinition(defID);
                if (def.DataSourceTypeID == 0)
                {
                    msg = "A data source is not assigned to this template.";
                    msg += Environment.NewLine + Environment.NewLine + "To assigned a Data Source:  ";
                    msg += Environment.NewLine + Environment.NewLine + "Click Edit Import Map button, and select data source from the dropdown. ";                    
                    rtn = msg;
                }
            }

            //-------------------------------------------------------------------
            var statusZero = from g in hdr
                             where g.Status == 0
                             select new
                             {
                                 g.ImportHeaderID,
                                 g.Status
                             };
            if (statusZero.Count() != 0)
            {
                msg = "ALL sample must be accepted or rejected.";
                msg += Environment.NewLine + Environment.NewLine + "Please accept or reject ALL samples -- by clicking the ";
                msg += Environment.NewLine + Environment.NewLine + "     Green Accept button or the ";
                msg += Environment.NewLine + "     Red Reject button. ";
                rtn = msg;
                
            }

            //-------------------------------------------------------------------
            //Make sure there are "accepted" samples to export before exporting												
            var accepted = from g in hdr
                           where g.Status == 1
                           select new
                           {
                               g.ImportHeaderID,
                               g.Status
                           };
            if (accepted.Count() == 0)
            {
                msg = "No Samples have been accepted. Nothing to import.";
                rtn = msg;
            }

            return rtn;
        }


        //7/30/19
        private void btnUpdateHeaderFields_Click(object sender, EventArgs e)
        {
            try
            {
                String msg = "";                                         

                msg = ChecksBeforeImport(_selectedDefinitionID, _header);
                if (msg != "")
                {
                    MessageBox.Show(msg, "Warning...");
                    return;
                }

                //-------------------------------------------------------------------
                DialogResult dr = new DialogResult();
                dr = MessageBox.Show("Update sample fields?", "Warning...", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    return;
                }

                //-------------------------------------------------------------------
                List<FilesInfo> files = new List<FilesInfo>();
                files = (List<FilesInfo>)grdFiles.DataSource;
                if (CheckFiles(files) != true)
                {
                    return;
                }

                //-------------------------------------------------------------------
                if (PreImportChecks() == -1)
                {
                    tabControl1.SelectedIndex = 0;
                    return;
                }

                //-------------------------------------------------------------------
                AIMGlobal.SplashScreenShow();
                timer1.Stop();
                progressBarPreviewImport.Value = 0;
                SetControlsForProcess(true);
                if (backgroundUpdateHeader.IsBusy == false)
                {
                    backgroundUpdateHeader.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Header update is busy. Please try again later.", "Warning...");
                }

            }
            catch (Exception ex)
            {
                //TODO test this -- to get line# where error occurred
                //Int32 lineError = GetLineNumber(ex);
                //MessageBox.Show("Import Start, Line# - " + lineError.ToString() + " " + ex.Message);                
                MessageBox.Show("Update header fields - " + ex.Message);
            }
        }

        private void backgroundUpdateHeader_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean result = false;
            try
            {
                result = SaveSample();
                if (result == true)
                {
                    MoveFileToArchive();
                }
                else
                {
                    MoveFileToError();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundUpdateHeader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetControlsForProcess(false);

            //9/9/20 - change to use AIMCoreGlobal
            //CFCAIM.AIMGlobal.importProgressBarCounter = 0;
            CFCAIMCore.Global.importProgressBarCounter = 0;

            ClearCounts();

            List<FilesInfo> clearResults = new List<FilesInfo>();
            grdPreviewHeader.DataSource = clearResults;

            _header = new List<ImportHeaderInfo>();

            List<FilesInfo> clearFiles = new List<FilesInfo>();
            grdFiles.DataSource = clearFiles;
            LoadGridWithFiles();

            AIMGlobal.SplashScreenHide();
            MessageBox.Show("Import Complete");

           
            tabControl1.SelectedIndex = 0;
        }

        private void backgroundUpdateHeader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                {
                    if (progressBarPreviewImport.Value != progressBarPreviewImport.Maximum)
                    {
                        progressBarPreviewImport.Value++;

                        //9/9/20 - change to use AIMCoreGlobal
                        //txtCounter.Text = Convert.ToString(AIMGlobal.importProgressBarCounter++);
                        txtCounter.Text = Convert.ToString(CFCAIMCore.Global.importProgressBarCounter++);
                    }
                    else
                    {
                        progressBarPreviewImport.Value = 0;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void repHyperHeaderUpdateMessage_Click(object sender, EventArgs e)
        {
            String sampleCode = "";
            Int32 message = 0;
            Int32 ID = 0;
            String msg = "";                                   
            
            sampleCode = (String)grdPreviewHeaderView.GetRowCellValue(grdPreviewHeaderView.FocusedRowHandle, colSampleCode);
            message = (Int32)grdPreviewHeaderView.GetRowCellValue(grdPreviewHeaderView.FocusedRowHandle, colSpecificMessage);
            ID = Convert.ToInt32(grdPreviewHeaderView.GetRowCellValue(grdPreviewHeaderView.FocusedRowHandle, colImportHeaderID));

            //Yellow Warnings -- will expand the detail
            if (message == 0 && CountDetailWarningsPerSample(ID) > 0)
            {
                if (grdPreviewHeaderView.GetMasterRowExpanded(grdPreviewHeaderView.FocusedRowHandle) == false)
                {
                    grdPreviewHeaderView.SetMasterRowExpanded(grdPreviewHeaderView.FocusedRowHandle, true);
                }
                else
                {
                    grdPreviewHeaderView.SetMasterRowExpanded(grdPreviewHeaderView.FocusedRowHandle, false);
                }
            }

            //--------------------------------------------------------------------------
            if (message != 0)
            {
                AIMGlobal.analyzerDataSourceTypeID = _dataSource.DataSourceTypeID;
                AIMGlobal.userdefinedImportError = message;

                setGlobalRequiredField(message);
                                                       
                //--------------------------------------------------------------------------
                if (message == (Int32)SampleImportSpecificMessageDetail.SampleDoesNotExist)
                {                                        
                    msg = "There is no existing sample for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode + Environment.NewLine + Environment.NewLine + "Sample header fields will not be updated.";
                    
                    MessageBox.Show(msg, "Sample Does Not Exist...", MessageBoxButtons.OK);
                    AIMGlobal.userdefinedImportError = 0;
                    return;
                }
                               
                //--------------------------------------------------------------------------                
                if (message == (Int32)SampleImportSpecificMessageDetail.UpdateHeaderFields)
                {
                    MessageBox.Show("On Import, header fields will be updated for: " + Environment.NewLine + Environment.NewLine + "     Sample Code: " + sampleCode, "Update Header Fields...", MessageBoxButtons.OK);
                    AIMGlobal.userdefinedImportError = 0;
                    return;
                }
                
                //--------------------------------------------------------------------------
                if (message == (Int32)SampleImportSpecificMessageDetail.Rejected)
                {
                    MessageBox.Show("Sample header fields will not be updated.", "Rejected...", MessageBoxButtons.OK);
                    AIMGlobal.userdefinedImportError = 0;
                    return;
                }
                                
                //--------------------------------------------------------------------------
                
                AIMGlobal.userdefinedImportError = 0;
                tabControl1.SelectedTab = tpPreviewResults;
            }
        }

        private void tpPreviewResults_Click(object sender, EventArgs e)
        {

        }
                
        //12/5/19
        private void btnChangeOwnerSupplier_Click(object sender, EventArgs e)
        {

            String msg = "";
            if (btnCrossReference.Visible == true)
            {
                DialogResult dr = new DialogResult();
                msg = "Missing cross-references exist. ";
                msg += Environment.NewLine + Environment.NewLine + "Fixing cross-references should be done prior to updating Owners/Suppliers because of a potential loss of data.";
                msg += Environment.NewLine + Environment.NewLine + "Do you want to continue with this update?";
                dr = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }
                     
            if (pnlMaterial.Visible == false)
            {
               
                populateMaterialFilter();       
                pnlMaterial.Visible = true;
                
            }
            else
            {
                pnlMaterial.Visible = false;
                panelRest.Visible = false;
            }
        }

        //12/5/19
        private void lueNewOwnerSupplier_EditValueChanged(object sender, EventArgs e)
        {
        
            try
            {
                if (rbSupplier.Checked == true)
                {
                    DataBindSupplierLocationlueNew();
                }
                else
                {
                    DataBindOwnerLocationlueNew();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //12/5/19
        private void rbOwner_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOwner.Checked == true)
            {
                //11/11/20
                //DataBindLueOwner();
                DataBindNewLueOwner();

                DataBindOwnerLocationlueNew();                
                lueNewOwnerSupplier.Focus();

            }

        }

        //12/5/19
        private void rbSupplier_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSupplier.Checked == true)
            {
                //11/11/20
                //DataBindLueSupplier();
                DataBindNewLueSupplier();

                DataBindSupplierLocationlueNew();                
                lueNewOwnerSupplier.Focus();

            }

            
        }

        //12/6/19
        private void lueMaterialFilter_EditValueChanged(object sender, EventArgs e)
        {

            //11/9/20
            grdPreviewView.Columns["UpdateOwnerSupplier"].Visible = false;            

            panelRest.Visible = false;
            if (lueMaterialFilter.SelectedText != "")
            {
                colMaterial.FilterInfo = new ColumnFilterInfo("[MaterialName] = '" + lueMaterialFilter.SelectedText + "'");
                panelRest.Visible = true;

                //11/9/20
                grdPreviewView.Columns["UpdateOwnerSupplier"].Visible = true;
                grdPreviewView.Columns["UpdateOwnerSupplier"].VisibleIndex = 7;

            }
            else
            {                
                //////pnlMaterial.Visible = false;
            }
            
        }

        //11/9/20
        private Boolean CheckUpdateValues()
        {
            try
            {                

                if (lueMaterialFilter.EditValue.ToString() == "")
                {
                    MessageBox.Show("Please select a material.", "Warning...");
                    lueMaterialFilter.Focus();
                    lueMaterialFilter.ShowPopup();
                    return false;
                }
                if ((Int32)lueNewOwnerSupplier.EditValue == 0 || (Int32)lueNewOwnerSupplierLocation.EditValue == 0)
                {
                    if (rbOwner.Checked == true)
                    {
                        MessageBox.Show("Please select Owner/Location.", "Warning...");
                    }
                    else
                    {
                        MessageBox.Show("Please select Supplier/Location.", "Warning...");
                    }
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //12/5/19
        private void Update_Click(object sender, EventArgs e)
        {
            try
            {
                //if (lueMaterialFilter.EditValue.ToString() == "")
                //{
                //    MessageBox.Show("Please select a material.", "Warning...");
                //    lueMaterialFilter.Focus();
                //    lueMaterialFilter.ShowPopup();
                //    return;
                //}

                //if ((Int32)lueNewOwnerSupplier.EditValue == 0 || (Int32)lueNewOwnerSupplierLocation.EditValue == 0)
                //{
                //    if (rbOwner.Checked == true)
                //    {
                //        MessageBox.Show("Please select Owner/Location.", "Warning...");
                //    }
                //    else
                //    {
                //        MessageBox.Show("Please select Supplier/Location.", "Warning...");
                //    }
                //    return;
                //}
                if (CheckUpdateValues() == false)
                {
                    return;
                }
                
                //------------------------------------------------------------
                //Update entity                    

                AIMGlobal.SplashScreenShow();
                for (int i = 0; i < grdPreviewView.RowCount; i++)
                {
                    if (rbSupplier.Checked == true)
                    {
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).SourceID = (Int32)lueNewOwnerSupplier.EditValue;
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).SourceName = lueNewOwnerSupplier.Text;
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).SourceLocationID = (Int32)lueNewOwnerSupplierLocation.EditValue;
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).SourceLocationName = lueNewOwnerSupplierLocation.Text;
                    }
                    else
                    {
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).OwnerID = (Int32)lueNewOwnerSupplier.EditValue;
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).OwnerName = lueNewOwnerSupplier.Text;
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).OwnerLocationID = (Int32)lueNewOwnerSupplierLocation.EditValue;
                        ((ImportHeaderInfo)grdPreviewView.GetRow(i)).OwnerLocationName = lueNewOwnerSupplierLocation.Text;
                    }
                }
                btnClearFilter.PerformClick();
                AIMGlobal.SplashScreenHide();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //12/6/19
        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            grdPreviewView.ActiveFilter.Clear();
            lueMaterialFilter.EditValue = "";           

        }

        private void lueOwnerName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueOwnerName, ref lueOwner);           //1/2/20
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //1/2/20
        private void SyncValueLue(ref LookUpEdit from, ref LookUpEdit to)
        {
            if (from != null && to != null)
            {
                to.EditValue = from.EditValue;
            }
        }

        private void lueOwnerLocationName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                SyncValueLue(ref lueOwnerLocationName, ref lueOwnerLocation);           //1/2/20
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //1/3/20
        private void hyperSupplier_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to supplier location
            try
            {
                AIMGlobal.aimSupplierLocationTab = true;
                AIMGlobal.open_form(aimMaintenance = new AIM());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //1/3/20
        private void hyperOwner_Click(object sender, EventArgs e)
        {
            //open AIM maintenance, set tab to owner location
            try
            {
                AIMGlobal.aimOwnerLocationTab = true;
                AIMGlobal.open_form(aimMaintenance = new AIM());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
            }
        }

        //1/6/20
        private void ApplyTemplateXML()
        {
            String rtn = "";
            if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)
            {                
                rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetImportXMLStream(grdPreviewHeaderView, _selectedDefinitionID);                
            }
            else
            {             
                rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.GetImportXMLStream(grdPreviewView, _selectedDefinitionID);
            }
            if (rtn.Length > 0)
            {
                MessageBox.Show(rtn, "Warning...");
                return;
            }
        }

        //1/6/20
        private void SaveGridXMLStream()
        {
            String rtn = "";
            if (_selectedDefinitionID != 0)
            {

                //5/2/23 - added this to check for null definition
                UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();
                definition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
                if (definition != null)
                {


                    //getting layout in stream
                    Stream stream = new System.IO.MemoryStream();

                    if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)
                    {
                        grdPreviewHeaderView.SaveLayoutToStream(stream);
                        //rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.SaveImportXMLStream(grdPreviewHeaderView, lueTemplateName);
                        rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.SaveImportXMLStream(grdPreviewHeaderView, _selectedDefinitionID);
                    }

                    else
                    {
                        grdPreviewView.SaveLayoutToStream(stream);
                        //rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.SaveImportXMLStream(grdPreviewView, lueTemplateName);
                        rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.SaveImportXMLStream(grdPreviewView, _selectedDefinitionID);
                    }

                    if (rtn.Length > 0)
                    {
                        MessageBox.Show(rtn, "Warning...");
                        return;
                    }
                }
            }
        }
                
        private void ImportDataUserDefined_FormClosing(object sender, FormClosingEventArgs e)
        {
            //12/16/20
            if (pnlRequiredFields.Visible == true)
            {
                SaveRequiredFields();
            }

            SaveGridXMLStream();
        }

        private void grdPreview_Click(object sender, EventArgs e)
        {

        }

        //11/9/20 - allow user to update the owner/supplier for indivdual samples within the import.
        private void repositoryItemButtonEdit1_Click(object sender, EventArgs e)
        {
            if (CheckUpdateValues() == false)
            {
                return;
            }

            //------------------------------------------------------------
            //Update entity                                
            if (rbSupplier.Checked == true)
            {
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).SourceID = (Int32)lueNewOwnerSupplier.EditValue;
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).SourceName = lueNewOwnerSupplier.Text;
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).SourceLocationID = (Int32)lueNewOwnerSupplierLocation.EditValue;
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).SourceLocationName = lueNewOwnerSupplierLocation.Text;
            }
            else
            {
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).OwnerID = (Int32)lueNewOwnerSupplier.EditValue;
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).OwnerName = lueNewOwnerSupplier.Text;
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).OwnerLocationID = (Int32)lueNewOwnerSupplierLocation.EditValue;
                ((ImportHeaderInfo)grdPreviewView.GetRow(grdPreviewView.FocusedRowHandle)).OwnerLocationName = lueNewOwnerSupplierLocation.Text;
            }

            grdPreviewView.RefreshData();

        }
               

        private void button1_Click(object sender, EventArgs e)
        {
            Int32 skipRowCount = 0;                                

            using (var streamReader = new StreamReader(@"E:\AIM Imports\Smithfield\ShortTest.csv"))
            {
                //GET NUMBER OF ROWS TO SKIP - For Romer imports, can look for "SampleID". On import wizard for Romer, the header row is set to 1 and detail row is set to 2.                
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    while (csvReader.Read())
                    {                        
                        if (((CsvHelper.CsvParser)csvReader.Context.Parser).Record[0].StartsWith("SampleID"))
                        {
                            skipRowCount = csvReader.Parser.Row - 1;
                            break;
                        }
                    }                    
                }

                //For regular user-defined csv imports, where user specifies header/detail, for the header, need to set skipRowCount = header - 1.
                // and then may have to add some more code in case detail doesn't start right after the header -- 
                //need to skip rows between header and detail. Or .. do we make a rule that the detail has to be in the next line following the header.

                // SKIP ROWS
                var textReader = File.OpenText(@"E:\AIM Imports\Smithfield\ShortTest.csv");
                for (int i = 0; i < skipRowCount; i++)
                {
                    textReader.ReadLine();
                }

                // TEXT-READER IS NOW POSITIONED ON THE HEADER ROW                
                var csv = new CsvHelper.CsvReader(textReader, CultureInfo.InvariantCulture);
                var records = csv.GetRecords<dynamic>().ToList();

                // TODO - NEED TO PUT RECORDS IN ENTITY (AND REPLACE DYNAMIC ABOVE)
                // and then put all this in CFCAIMCore
            }         
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                String str = String.Empty;
                OpenFileDialog openFileDialog = new OpenFileDialog();

                if (txtDocumentPath.Text != "")
                {
                    //get directory portion                                        
                    str = Path.GetDirectoryName(txtDocumentPath.Text);
                    if (Directory.Exists(txtDocumentPath.Text) == true)
                    {
                        openFileDialog.InitialDirectory = txtDocumentPath.Text;
                    }
                }                
                else
                {
                    openFileDialog.InitialDirectory = txtSourcePath.Text;
                }

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    txtDocumentPath.Text = openFileDialog.FileName;
                                        
                    String folder;
                    String file;
                    String toFolder;
                    folder = Path.GetDirectoryName(txtDocumentPath.Text);
                    file = Path.GetFileName(txtDocumentPath.Text);                    
                    toFolder = Path.GetDirectoryName(txtSourcePath.Text);

                    //3/15/23
                    if (Directory.Exists(toFolder) == false)
                    {
                        MessageBox.Show("Unable to copy to: " + toFolder + ". Make sure directory/folder exists.");
                    }
                    else
                    {
                        String fileToCopy = txtDocumentPath.Text;
                        String destinationDirectory = txtSourcePath.Text;
                        File.Copy(fileToCopy, destinationDirectory + "\\" + Path.GetFileName(fileToCopy));

                        MessageBox.Show("Copy Complete");
                    }                                    
                }
                btnRefreshGrid.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtClearDocument_Click(object sender, EventArgs e)
        {
            txtDocumentPath.Text = String.Empty;
        }

        
        //public void OnTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    txtComplete.Text = "Copy Complete";
        //    n++;
        //}

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }		
     
}

