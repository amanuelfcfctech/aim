using CFCAIM.Imports;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.Data.Filtering;
using DevExpress.Spreadsheet;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;
using System.Globalization;

namespace CFCAIM
{

	public partial class ImportMapping : Form
	{

		MapWizard mapWizard;	
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		CFCUser.Global userGlobal = new CFCUser.Global();
		
		List<DataSourceTypeRefFileTypeInfo> _fileType = new List<DataSourceTypeRefFileTypeInfo>();
		List<DelimiterTypeInfo> _delimiterType = new List<DelimiterTypeInfo>();
        List<DecimalSeparatorInfo> _decimalSeparator = new List<DecimalSeparatorInfo>();
        List<DataSourceTypeInfo> _dataSource = new List<DataSourceTypeInfo>();
		Boolean _needDelimiterType = false;
		List<UserDefinedDataSourceDefinitionInfo> _templateName = new List<UserDefinedDataSourceDefinitionInfo>();		
		List<SampleImportUserDefinedMapSelectionInfo> _mapSelection = new List<SampleImportUserDefinedMapSelectionInfo>();

        //TODO - the stp that populates this field should be changed to use table instead of hard-coded values in stp
        List<SampleImportColumnNamesInfo> _mapColumns = new List<SampleImportColumnNamesInfo>();

		UserDefinedDataSourceDefinitionInfo _selectedDefinition = new UserDefinedDataSourceDefinitionInfo();
		List<UserDefinedDataSourceTemplateInfo> _template = new List<UserDefinedDataSourceTemplateInfo>();
		List<AIMMaterialRefAnalyzerMaterialInfo> _aimMaterial = new List<AIMMaterialRefAnalyzerMaterialInfo>();

		//12/15/20
		//List<AIMOwnerRefAnalyzerOwnerInfo> _aimOwner = new List<AIMOwnerRefAnalyzerOwnerInfo>();
		//List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
		//List<AIMSupplierRefAnalyzerSupplierInfo> _aimSupplier = new List<AIMSupplierRefAnalyzerSupplierInfo>();
		//List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> _aimsupplierLocation = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
		List<AIMOwnerInfo> _aimOwner = new List<AIMOwnerInfo>();		
		List<AIMOwnerLocationInfo> _aimOwnerLocation = new List<AIMOwnerLocationInfo>();
		List<AIMSupplierInfo> _aimSupplier = new List<AIMSupplierInfo>();
		List<AIMSupplierLocationInfo> _aimSupplierLocation = new List<AIMSupplierLocationInfo>();

		List<ImportHeaderInfo> _header = new List<ImportHeaderInfo>();
		List<DateFormatInfo> _dateFormat = new List<DateFormatInfo>();
        
        String _mapToThisColumn = "";
		Int32 _selectedDefinitionID = 0;		
		Boolean _isLoading = false;
		Boolean _landscapeClicked = false;

		Int32 _saveDataSourceID = 0;
		Int32 _saveFileTypeID = 0;
        Boolean _browseButtonClicked = false;

        //12/6/18
        Boolean _useAsSampleCode = false;
        Int32 _PreviouslySelectedUseAsSampleCode = 0;        
        Boolean _showDoNotUse = true;
                        
        Int32 _importType = 0;                                                                                                                  //7/29/19

        public ImportMapping()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
		}
		private void ImportMapping_Load(object sender, EventArgs e)
		{	   			
			try				
			{	
                	  
				_isLoading = true;
				lueImportFileName.EditValue = 0;
                
				DataBindlue();
				DataBindlueTemplateName();								
				DataBind();				
				DataBindResults();
				ClearTextFields();
				SetLueDefaults();
				SetToolTips();
				btnNext.Visible = false;
				btnPrior.Visible = false;
                
				TabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
				TabControl.DrawItem += new DrawItemEventHandler(TabControl_DrawItem);

				_isLoading = false;
				if ((Int32)lueImportFileName.EditValue == 0)
				{
					lueImportFileName.BackColor = Color.Aqua;
				}                

				TabControl.Visible = false;
				if (AIMGlobal.analyzerDataSourceDefinitionID != 0)
				{
					_selectedDefinitionID = AIMGlobal.analyzerDataSourceDefinitionID;
					lueImportFileName.EditValue = _selectedDefinitionID;
					TabControl.Visible = true;
					checkNutrientBeginColumn();					
					PopulateMapColumns();	   
					CheckArrows();			   					
				}
                
				//4/7/17 - save datasourcetypeID and fileTypeID, in case user changes datasource, and the filetype
				//			for that datasource is something other than XLS, TXT or CSV -- because those are the
				//			only formats currently set up for user-defined imports.  (EX: Nutriquest imports are XML
				//			but code hasn't been added for user-defined XML imports.
				Int32.TryParse(lueImportFileName.EditValue.ToString(), out _selectedDefinitionID);
				_selectedDefinition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
                
                if (_selectedDefinition != null)
                {
                    _saveDataSourceID = _selectedDefinition.DataSourceTypeID;
                    _saveFileTypeID = _selectedDefinition.ImportFileTypeID;
                                        
                    _importType = _selectedDefinition.ImportType;                                                                               //7/29/19           

					//commented this, causing problems
					//8/19/22 - for Romer imports, need to repopulate the mapping dropdowns, skipping the "Use Mapping" option
					//if (_importType == (Int32)EnumImportType.RomerResults)
					//{											

					//	lueOwner.Properties.DataSource = _mapSelection;
					//	XtraEditors.LoadLookUpEdit(ref lueOwner, "SampleImportUserDefinedMapSelectionID", "Description");
					//	lueOwner.EditValue = (Int32)SampleImportUserDefinedMapSelection.SelectOnImport;

					//	lueOwnerLocation.Properties.DataSource = _mapSelection;
					//	XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "SampleImportUserDefinedMapSelectionID", "Description");
					//	lueOwnerLocation.EditValue = (Int32)SampleImportUserDefinedMapSelection.SelectOnImport;

					//	lueSupplier.Properties.DataSource = _mapSelection;
					//	XtraEditors.LoadLookUpEdit(ref lueSupplier, "SampleImportUserDefinedMapSelectionID", "Description");
					//	lueSupplier.EditValue = (Int32)SampleImportUserDefinedMapSelection.SelectOnImport;

					//	lueSupplierLocation.Properties.DataSource = _mapSelection;
					//	XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "SampleImportUserDefinedMapSelectionID", "Description");
					//	lueSupplierLocation.EditValue = (Int32)SampleImportUserDefinedMapSelection.SelectOnImport;

					//}

                }

				//---------------------------------------------------------------------------------
				//8/12/22 - added for Romer
				pnlComposite.Visible = false;
				pnlDatasource.Enabled = true;
				pnlQualityControl.Visible = true;
				pnlAllowDuplicates.Visible = true;
				pnlCreateSampleCode.Visible = true;
				pnlImportType.Visible = true;

				//3/7/23
				panelHeaderDetail.Visible = true;

				if (_importType == (Int32)EnumImportType.RomerResults)
				{
					pnlDatasource.Enabled = false;
					pnlQualityControl.Visible = false;
					pnlAllowDuplicates.Visible = false;
					pnlCreateSampleCode.Visible = false;
					pnlImportType.Visible = false;
					lueFileType.Enabled = false;
					btnPortrait.Enabled = false;
					btnLandscape.Enabled = false;
					cbFileHasHeader.Enabled = false;
					//txtBeginRow.Enabled = false;
					//txtDetailBeginRow.Enabled = false;					
					if (_selectedDefinition.IsCompositeBatch == true)
					{
						pnlComposite.Visible = true;
					}

					//3/7/23
					panelHeaderDetail.Visible = false;

				}
				//---------------------------------------------------------------------------------

				WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void CheckArrows()
		{
			btnNext.Visible = true;
			btnPrior.Visible = true;

			btnNext.Enabled = false;
			btnPrior.Enabled = false;
			if (TabControl.SelectedIndex == 0)
			{
				btnNext.Enabled = true;	
			}
			else if (TabControl.SelectedIndex == 1)
			{
				btnNext.Enabled = true;
				btnPrior.Enabled = true;			
			}
			else
			{
				btnPrior.Enabled = true;				
			}
		}

		private void DataBind()
		{
			DataBindSelectedDefinition();
			DataBindTemplate();
			DataBindResults();					
		}
		private void DataBindlueImportName()
		{
			_templateName = AIMManager.GetAllUserDefinedDataSourceDefinition();
			_templateName.Insert
			(
				0,
				new UserDefinedDataSourceDefinitionInfo
				{
					UserDefinedDataSourceDefinitionID = 0,
					ImportFileName = "--Select--"
				}
			);
			lueImportFileName.Properties.DataSource = _templateName;
			XtraEditors.LoadLookUpEdit(ref lueImportFileName, "UserDefinedDataSourceDefinitionID", "ImportFileName");		
		}
		private void DataBindlueTemplateName()
		{			
			_templateName = AIMManager.GetAllUserDefinedDataSourceDefinition();
			_templateName.Insert
			(
				0,
				new UserDefinedDataSourceDefinitionInfo
				{
					UserDefinedDataSourceDefinitionID = 0,
					ImportFileName = "--Select--"

				}
			);
			lueImportFileName.Properties.DataSource = _templateName;
			XtraEditors.LoadLookUpEdit(ref lueImportFileName, "UserDefinedDataSourceDefinitionID", "ImportFileName");
		}
		private void DataBindFileTypelue()
		{
            
			_fileType = AIMManager.GetDataSourceTypeRefFileType((Int32)lueDataSource.EditValue);           
            _fileType.Insert			
			(
				0,
				new DataSourceTypeRefFileTypeInfo
				{
					FileTypeID = 0,
					FileTypeName = "--Select--"
				}
			);
			lueFileType.Properties.DataSource = _fileType;
			XtraEditors.LoadLookUpEdit(ref lueFileType, "FileTypeID", "FileTypeName");
						
		}

		private void PopulateMapColumns()
		{			
			Boolean datasourceIsMapped = false;	 			
			if (rbYesMulti.Checked == true)
			{
				datasourceIsMapped = true;
			}

            //12/3/19
            Boolean useSystemSampleCodes = false;
            if ((rbCreate.Checked == true || rbCreateOrMerge.Checked == true) && rbSystemCreatedSampleCodeYes.Checked == true)
            {
                useSystemSampleCodes = true;
            }


            if (rbResultsAndHeader.Checked == true)                      
            {     
                //12/4/19                                                                                         
                //_mapColumns = AIMManager.GetSampleColumnNames_CanAlter(datasourceIsMapped);

				//This is sending the useSystemSampleCodes switch so that if switch = yes, 'Sample Code' will NOT show up in the column dropdown and will not be available for the user to select
                _mapColumns = AIMManager.GetSampleColumnNames_CanAlter(datasourceIsMapped, useSystemSampleCodes);
            }                                                                                                                                   

            else if (rbUpdateHeaderOnly.Checked == true)                                                                                        //7/29/19
            {                                                                                                                                   //7/29/19
                _mapColumns = AIMManager.GetSampleColumnNames_HeaderOnly();                                                                     //7/29/19
            }                                                                                                                                   //7/29/19

            else
            {
				//12/3/19
				//_mapColumns = AIMManager.GetSampleColumnNames(datasourceIsMapped);

				//This is sending the useSystemSampleCodes switch so that if switch = yes, 'Sample Code' will NOT show up in the column dropdown and will not be available for the user to select
				_mapColumns = AIMManager.GetSampleColumnNames(datasourceIsMapped, useSystemSampleCodes);
            }
            

            _mapColumns.Insert
			(
				0,
				new SampleImportColumnNamesInfo
				{
					MapColumnID = 0,					
                    MapColumnName = "--Select--"

                }
			);
			replueMapColumnName.DataSource = _mapColumns;

			XtraEditors.LoadGridLookUpEdit(ref replueMapColumnName, "MapColumnID", "MapColumnName", "IsDetail");           
            replueMapColumnName.Columns["MapColumnID"].Visible = false;
			replueMapColumnName.Columns["MapColumnName"].Visible = true;
			replueMapColumnName.Columns["IsDetail"].Visible = true;
		
		}

		private void DataBindlue()
		{					

			_delimiterType = AIMManager.GetDelimiterType(true);
			_delimiterType.Insert
			(
				0,
				new DelimiterTypeInfo
				{
					DelimiterTypeID = 0,
					Description = "--Select--"
				}
			);
			lueDelimiterType.Properties.DataSource = _delimiterType;
			XtraEditors.LoadLookUpEdit(ref lueDelimiterType, "DelimiterTypeID", "Description");
					
			_dateFormat = AIMManager.GetDateFormat();			
			lueDateFormat.Properties.DataSource = _dateFormat;
			XtraEditors.LoadLookUpEdit(ref lueDateFormat, "DateFormatID", "DateFormat");

            //4/1/19           
            _decimalSeparator = AIMManager.GetDecimalSeparator(true);
            _decimalSeparator.Insert
            (
                0,
                new DecimalSeparatorInfo
                {
                    DecimalSeparatorID = 0,
                    Description = "--Select--"
                }
            );
            lueDecimalSeparator.Properties.DataSource = _decimalSeparator;
            XtraEditors.LoadLookUpEdit(ref lueDecimalSeparator, "DecimalSeparatorID", "Description");


            _dateFormat = AIMManager.GetDateFormat();
            lueDateFormat.Properties.DataSource = _dateFormat;
            XtraEditors.LoadLookUpEdit(ref lueDateFormat, "DateFormatID", "DateFormat");

            //3/29/17 - user-defined imports can use any datasource			
            //_dataSource = AIMManager.GetDataSourceTypeUserDefined(true);
            _dataSource = AIMManager.GetDataSourceType(true);

            _dataSource.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--Select--"
				}
			);
			lueDataSource.Properties.DataSource = _dataSource;
			XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name");

			//8/19/22
			//_mapSelection = AIMManager.GetMapSelection(true);	
			if (AIMGlobal.romerImport == true)
			{
				_mapSelection = AIMManager.GetMapSelectionSkipUseMapping(true);
			}
			else
			{
				_mapSelection = AIMManager.GetMapSelection(true);
			}									
			
			lueMaterial.Properties.DataSource = _mapSelection;
			XtraEditors.LoadLookUpEdit(ref lueMaterial, "SampleImportUserDefinedMapSelectionID", "Description");
			lueMaterial.EditValue = (Int32)SampleImportUserDefinedMapSelection.UseMapping;
						
			lueOwner.Properties.DataSource = _mapSelection;
			XtraEditors.LoadLookUpEdit(ref lueOwner, "SampleImportUserDefinedMapSelectionID", "Description");
		
			lueOwnerLocation.Properties.DataSource = _mapSelection;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocation, "SampleImportUserDefinedMapSelectionID", "Description");
		
			lueSupplier.Properties.DataSource = _mapSelection;
			XtraEditors.LoadLookUpEdit(ref lueSupplier, "SampleImportUserDefinedMapSelectionID", "Description");
	
			lueSupplierLocation.Properties.DataSource = _mapSelection;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocation, "SampleImportUserDefinedMapSelectionID", "Description");				
			
			txtImportFileName.Text = lueImportFileName.Text.ToString();
			txtImportFileNameMap.Text = lueImportFileName.Text.ToString();
						
			DataBindLueAIM();

			//-----------------------------------------------------------------
			Refresh();

		}
		private void DataBindLueAIM()

		{
			//-----------------------------------------------------------------
			//Dropdowns to select a specific CROSS-REFERENCED material, owner, supplier, etc.
			//-----------------------------------------------------------------
			DataBindLueMaterial();

			//_aimOwner = AIMManager.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeID((Int32)lueDataSource.EditValue);
			_aimOwner = AIMManager.GetAIMOwner(true);


			//-----------------------------------------------
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
			lueOwnerSelect.Properties.DataSource = _aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerSelect, "AIMOwnerID", "OwnerCode", "OwnerName");

			//-----------------------------------------------------------------

			//12/15/20
			//_aimSupplier = AIMManager.GetAIMSupplierRefAnalyzerSupplierDataSourceTypeID((Int32)lueDataSource.EditValue);
			_aimSupplier = AIMManager.GetSupplier(true);

			_aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"
				}
			);			
			lueSupplierSelect.Properties.DataSource = _aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierSelect, "AIMSupplierID", "AIMSupplierCode", "SupplierName");

		}

		private void DataBindLueMaterial()
		{
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					_aimMaterial.Clear();
				}
				else
				{					
					_aimMaterial = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeID((Int32)lueDataSource.EditValue);
				}			
				_aimMaterial.Insert
				(
					0,
					new AIMMaterialRefAnalyzerMaterialInfo()
					{
						AIMMaterialID = 0,
						AIMMaterialCode = "--Select--"
					}
				);

				lueMaterialSelect.Properties.DataSource = _aimMaterial;
				XtraEditors.LoadLookUpEdit(ref lueMaterialSelect, "AIMMaterialID", "AIMMaterialCode");
			}
		}

		private void DataBindTemplate()
		{
            Int32 rowIndex = 0;

			if (_selectedDefinition != null)
			{
				Int32 currentRowNum = 0;
				Int32.TryParse(grdImportMappingView.FocusedRowHandle.ToString(), out currentRowNum);
				_template = AIMManager.GetUserDefinedDataSourceTemplate(_selectedDefinition.UserDefinedDataSourceDefinitionID);
				grdImportMapping.DataSource = _template;
				grdImportMappingView.FocusedRowHandle = currentRowNum;               

                if (_template.Count != 0)
                {
                    //_checkmarkFound = false;
                    rowIndex = 0;
                    List<UserDefinedDataSourceTemplateInfo> gridTemplate = new List<UserDefinedDataSourceTemplateInfo>();
                    gridTemplate = (List<UserDefinedDataSourceTemplateInfo>)grdImportMapping.DataSource;
                    if (gridTemplate != null)
                    {
                        foreach (var item in gridTemplate)
                        {
                            if (item.UseAsSampleCode == true)
                            {
                                _PreviouslySelectedUseAsSampleCode = rowIndex;
                                //_checkmarkFound = true;
                                break;
                            }
                            rowIndex++;
                        }
                    }
                }                
            }	                                             		
		}

		private void DataBindResults()
		{
			if (_selectedDefinition != null)
			{								
				grdPreview.DataSource = _header;
				grdPreviewView.FocusedRowHandle = 0;				
			}
		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			try
			{
                
                if ((Int32)lueFileType.EditValue == 0)
                {
                    MessageBox.Show("Please select a file type and/or change the Data Source.", "Warning...", MessageBoxButtons.OK);
                }
                else
                {
                    Close();                
					if ((Int32)lueImportFileName.EditValue != 0)
					{
						AIMGlobal.analyzerDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;
					}
                }
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
				ttip.SetToolTip(btnDeleteImportMap, "Delete Template");									
				ttip.SetToolTip(btnAddColumn, "Add Column");
				ttip.SetToolTip(btnPreview, "Preview Results");
				ttip.SetToolTip(btnPreviewGridResults, "Preview Grid Results");
				ttip.SetToolTip(btnPreviewMap, "Preview Grid Results");
				ttip.SetToolTip(btnLandscape, "Landscape: Spreadsheet has multiple nutrient columns, spreading to the right");
				ttip.SetToolTip(btnPortrait, "Portrait: Spreadsheet has single nutrient column");
				ttip.SetToolTip(btnSave, "Save Template");
				ttip.SetToolTip(btnNext, "Next Step");
				ttip.SetToolTip(btnPrior, "Previous Step");								
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	
		private void btnBrowse_Click(object sender, EventArgs e)
		{
			try
			{

				//check row count of grdImportMapping				
				if (grdImportMappingView.RowCount > 0)
				{                    
                    DialogResult dr = new DialogResult();
					dr = MessageBox.Show("This will CLEAR the current import map.  Do you want to continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)
					{
						return;
					}
                    _browseButtonClicked = true;
                }

				OpenFileDialog openFileDialog = new OpenFileDialog();
				String path = txtProcessPath.Text.ToString();
				if (Directory.Exists(path))
				{
					openFileDialog.InitialDirectory = path;
				}
				else
				{
					openFileDialog.InitialDirectory = @"C:\";
				}
				
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string filePath = openFileDialog.FileName;
					txtFilePath.Text = openFileDialog.FileName;

					if (path != Path.GetDirectoryName(filePath))
					{
						DialogResult dr = new DialogResult();
						dr = MessageBox.Show("Permanently save this Source path?", "Warning...", MessageBoxButtons.YesNo);
						if (dr == DialogResult.Yes)
						{
							UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();
							definition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
							definition.UserDefinedDataSourceDefinitionID = _selectedDefinition.UserDefinedDataSourceDefinitionID;
							definition.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							definition.ProcessDirectory = Path.GetDirectoryName(filePath);

                            ErrorInfo error = new ErrorInfo();
							error = AIMManager.SaveDataSourceUserDefinition(ref definition);
							if (error.ErrorNumber > 0)
							{
								return;
							}
						}
					}


					btnDeleteAll.PerformClick();

					populateImportMappingGrid();

                    ShowHideDoNotUseButton();
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		
		private void grdImportMappingView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			//12/22/20
			String mapColumnName = String.Empty;

			UserDefinedDataSourceTemplateInfo dataDef = new UserDefinedDataSourceTemplateInfo();
			dataDef = (UserDefinedDataSourceTemplateInfo)grdImportMappingView.GetFocusedRow();

			if (dataDef.SourceColumnNumber == 0)
			{
				DialogResult dr = new DialogResult();
				dr = MessageBox.Show("Column Number cannot be zero.", "Warning...");
				dataDef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteDataSourceUserTemplate(dataDef.UserDefinedDataSourceTemplateID);
				DataBindTemplate();					
				return;
			}

			//-----------------------------------------------------
			//12/22/20
			//get "actual" MapColumnName from MapColumnsNames table.  
			//On the Mapping screen, if there's an alternate name, that's what shows up in the MapColumnName column -- but the alternatename is NOT what should be save to the template table.
			MapColumnNamesInfo map = new MapColumnNamesInfo();
			map = AIMManager.GetMapColumnName(dataDef.MapColumnID);
			if (map != null)
			{
				mapColumnName = map.MapColumnName;
			}
            else
            {
				mapColumnName = dataDef.MapColumnName;
            }
			//-----------------------------------------------------

			//12/22/20
			//dataDef.MapColumnName = _mapToThisColumn;
			dataDef.MapColumnName = mapColumnName;

			dataDef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;					
            dataDef.UseAsSampleCode = _useAsSampleCode;                                                                                       //12/6/18		
            AIMManager.SaveUserDefinedDataSourceTemplate(ref dataDef);			
			DataBind();
			_mapToThisColumn = "";                        
            _useAsSampleCode = false;                                                                                                           //12/6/18

			//12/23/20
			if (((UserDefinedDataSourceTemplateInfo)e.Row).MapColumnName == "OwnerLocation")
			{				
				lueOwnerLocation.EditValue = 1;
			}

			if (((UserDefinedDataSourceTemplateInfo)e.Row).MapColumnName == "SupplierLocationCombined")
            {
				lueSupplier.EditValue = 1;
				lueSupplierLocation.EditValue = 1;
            }


        }
		private void repbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				UserDefinedDataSourceTemplateInfo template = new UserDefinedDataSourceTemplateInfo();
				template = (UserDefinedDataSourceTemplateInfo)grdImportMappingView.GetFocusedRow();
				template.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteDataSourceUserTemplate(template.UserDefinedDataSourceTemplateID);				
				DataBindTemplate();

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
				CFCAIMCore.Global.previewGridResults(grdImportMapping, "Import Mapping");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueImportFileName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				_isLoading = true;
				DataBindlue();
				DataBind();
				DataBindResults();
				ShowOrHideTabs();
				ClearPreviewGrid();
				txtFilePathPreview.Text = "";				
				_isLoading = false;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
					
		}
		private void ClearPreviewGrid()
		{
			List<ImportHeaderInfo> clearGrid = new List<ImportHeaderInfo>();
			grdPreview.DataSource = clearGrid;
		}
		private void ShowOrHideTabs()
		{
			if (lueImportFileName.EditValue == null)
			{
				return;
			}
			
			Int32 datasourceID = 0;
			Int32.TryParse(lueDataSource.EditValue.ToString(), out datasourceID);

			Int32 definitionID = 0;
			Int32.TryParse(lueImportFileName.EditValue.ToString(), out definitionID);
			if (definitionID == 0)
			{
				grpDataSource.Enabled = false;
				grpTemplateMap.Visible = false;
				grpResults.Visible = false;
				grpFileDefinition.Enabled = false;

			}
			else if (datasourceID == 0)
			{
				grpDataSource.Enabled = true;
				grpTemplateMap.Visible = false;
				grpResults.Visible = false;
				grpFileDefinition.Enabled = false;

			}
			else
			{
				grpDataSource.Enabled = true;
				grpTemplateMap.Visible = true;
				grpResults.Visible = true;
				grpFileDefinition.Enabled = true;
			}

		}
		private void DataBindSelectedDefinition()
		{
			if (lueImportFileName.EditValue != null)
			{
				Int32.TryParse(lueImportFileName.EditValue.ToString(), out _selectedDefinitionID);
				_selectedDefinition = AIMManager.GetDataSourceUserDefinition(_selectedDefinitionID);
                                
                if (_selectedDefinitionID == 0)                                                                                                 //3/28/18
                {                                                                                                                               //3/28/18
                    _selectedDefinitionID = AIMGlobal.analyzerDataSourceDefinitionID;                                                           //3/28/18
                }                                                                                                                               //3/28/18

				lblSheet.Visible = false;
				txtSheetNumber.Visible = false;                                
                lblLine3.Visible = false;                                                                                                       //2/26/18

                if (_selectedDefinition != null)
                {
                    txtImportFileName.Text = _selectedDefinition.ImportFileName;
                    txtImportFileNameMap.Text = _selectedDefinition.ImportFileName;
                    lueDataSource.EditValue = _selectedDefinition.DataSourceTypeID;
                    txtOrientation.Text = _selectedDefinition.OrientationDisplay;
                    btnPortrait.BackColor = Color.Transparent;
                    btnLandscape.BackColor = Color.Transparent;
                    if (txtOrientation.Text == "Portrait")
                    {
                        btnPortrait.BackColor = Color.LightSkyBlue;
                    }
                    else
                    {
                        btnLandscape.BackColor = Color.LightSkyBlue;
                    }

                    cbFileHasHeader.Checked = _selectedDefinition.FileHasHeader;
                    cbSpectraFile.Checked = _selectedDefinition.IsSpectraFile;

                    lueMaterial.EditValue = _selectedDefinition.AIMMaterialMap;
                    lueOwner.EditValue = _selectedDefinition.AIMOwnerMap;
                    lueOwnerLocation.EditValue = _selectedDefinition.AIMOwnerLocationMap;
                    lueSupplier.EditValue = _selectedDefinition.AIMSourceMap;
                    lueSupplierLocation.EditValue = _selectedDefinition.AIMSourceLocationMap;
                    txtBackUpDir.Text = _selectedDefinition.BackupDirectory;
                    txtProcessPath.Text = _selectedDefinition.ProcessDirectory;
                    txtSpectraDirectory.Text = _selectedDefinition.SpectraDirectory;

                    lueMaterialSelect.EditValue = _selectedDefinition.AIMMaterialID;
                    lueOwnerSelect.EditValue = _selectedDefinition.AIMOwnerID;
                    lueOwnerLocationSelect.EditValue = _selectedDefinition.AIMOwnerLocationID;
                    lueSupplierSelect.EditValue = _selectedDefinition.AIMSourceID;
                    lueSupplierLocationSelect.EditValue = _selectedDefinition.AIMSourceLocationID;

                    lueFileType.EditValue = _selectedDefinition.ImportFileTypeID;
                    lueDelimiterType.EditValue = _selectedDefinition.DelimiterTypeID;                    
                    lueDecimalSeparator.EditValue = _selectedDefinition.DecimalSeparatorID;                                                     //4/1/19
                    lueDateFormat.EditValue = _selectedDefinition.DateFormatID;

                    txtBeginRow.Text = _selectedDefinition.HeaderBeginRow.ToString();
                    txtDetailBeginColumn.Text = _selectedDefinition.NutrientDetailBeginColumn.ToString();

                    if (_selectedDefinition.SpectraFileExtension == null)
                    {
                        txtSpectraFileExtension.Text = "";
                    }
                    else
                    {
                        txtSpectraFileExtension.Text = _selectedDefinition.SpectraFileExtension.ToString();
                    }

                    //5/15/20 - changed to use correct ENUM
                    //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Excel)
                    if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX)

                    {
                        lblSheet.Visible = true;
                        txtSheetNumber.Visible = true;
                        txtSheetNumber.Text = Convert.ToString(_selectedDefinition.ExcelSheetNumber);
                        lblLine3.Visible = true;

                    }


                    if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateResultsOnly)           
                    {
                        rbMerge.Checked = true;
                        rbCreate.Checked = false;
                        rbCreateOrMerge.Checked = false;
                        rbResultsAndHeader.Checked = false;                                                                                     //4/1/19
                        rbUpdateHeaderOnly.Checked = false;                                                                                     //7/26/19
                        pnlRequiredFields.Enabled = false;

                        //12/2/19
                        pnlAllowDuplicateSampleCodes.Enabled = false;
                    }

                    //4/1/19
                    else if (_selectedDefinition.ImportType == (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields)         
                    {
                        rbMerge.Checked = false;
                        rbCreate.Checked = false;
                        rbCreateOrMerge.Checked = false;
                        rbResultsAndHeader.Checked = true;
                        rbUpdateHeaderOnly.Checked = false;                                                                                     //7/26/19
                        pnlRequiredFields.Enabled = false;

                        //12/2/19
                        pnlAllowDuplicateSampleCodes.Enabled = false;

                    }

                    //7/26/19
                    else if (_selectedDefinition.ImportType == (Int32)EnumImportType.UpdateHeaderOnly)       
                    {
                        rbMerge.Checked = false;
                        rbCreate.Checked = false;
                        rbCreateOrMerge.Checked = false;
                        rbResultsAndHeader.Checked = false;
                        rbUpdateHeaderOnly.Checked = true;
                        pnlRequiredFields.Enabled = false;

                        //12/2/19
                        pnlAllowDuplicateSampleCodes.Enabled = false;
                    }



                    else
                    {
                        rbMerge.Checked = false;
                        rbResultsAndHeader.Checked = false;                                                                                     //4/1/19
                        rbUpdateHeaderOnly.Checked = false;                                                                                     //7/26/19
                        pnlRequiredFields.Enabled = true;
                        if (_selectedDefinition.ImportType == (Int32)EnumImportType.AddNewSamplesAndResults)         
                        {
                            rbCreate.Checked = true;
                        }
                        else
                        {
                            rbCreateOrMerge.Checked = true;
                        }

                        //12/2/19
                        pnlAllowDuplicateSampleCodes.Enabled = true;
                    }
                                       
                                      
                    if (_selectedDefinition.DataSourceIsMapped == true)
					{
						rbYesMulti.Checked = true;
						rbNoMulti.Checked = false;
					}
					else
					{
						rbYesMulti.Checked = false;
						rbNoMulti.Checked = true;
					}

                    //3/15/18
                    if (_selectedDefinition.IsQualityControl == true)
                    {
                        rbQualityControlYes.Checked = true;
                    }
                    else
                    {
                        rbQualityControlNo.Checked = true;
                    }

                    //8/23/19
                    txtDetailBeginRow.Text = _selectedDefinition.DetailBeginRow.ToString();

                    //12/2/19
                    if (_selectedDefinition.AllowDuplicateSampleCodes == true)
                    {
                        rbAllowDuplicateSampleCodesYes.Checked = true;
                    }
                    else
                    {
                        rbAllowDuplicateSampleCodesNo.Checked = true;
                    }

                    //12/2/19
                    if (_selectedDefinition.SystemCreatedSampleCode == true)
                    {
                        rbSystemCreatedSampleCodeYes.Checked = true;
                        lblSystemSampleCodes.Visible = true;
                    }
                    else
                    {
                        rbSystemCreatedSampleCodeNo.Checked = true;
                        lblSystemSampleCodes.Visible = false;
                    }

                }
				else
				{
					txtImportFileName.Text = String.Empty;
					txtImportFileNameMap.Text = String.Empty;
					lueDataSource.EditValue = 0;
					txtFilePath.Text = String.Empty;
					txtOrientation.Text = String.Empty;
					txtSpectraFileExtension.Text = String.Empty;

                    txtBeginRow.Text = "0";
                    if (txtBeginRow.Visible == true)
					{
						txtBeginRow.Text = "1";                        
                    }
					
					cbFileHasHeader.Checked = false;
					cbSpectraFile.Checked = false;					
					lueMaterial.EditValue = 1;
					lueOwner.EditValue = 1;
					lueOwnerLocation.EditValue = 1;
					lueSupplier.EditValue = 1;
					lueSupplierLocation.EditValue = 1;

					rbMerge.Checked = true;
					rbCreate.Checked = false;
                    rbCreateOrMerge.Checked = false;

					pnlRequiredFields.Enabled = false;
                    pnlAllowDuplicateSampleCodes.Enabled = false;               //12/2/19

					rbYesMulti.Checked = false;
					rbNoMulti.Checked = true;                    
                    rbQualityControlNo.Checked = true;

                }
			}
		}
					
		private void btnDeleteImportMap_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dr = new DialogResult();
				ErrorInfo result = new ErrorInfo();

				dr = MessageBox.Show("This import map will be permanently deleted.  Do you want to delete this map?", "Warning...", MessageBoxButtons.YesNo);
				if (dr != DialogResult.Yes)
				{
					return;
				}
							
				
				AIMManager.DeleteDataSourceUserDefinitionAndTemplate(AIMGlobal.analyzerDataSourceDefinitionID);				
				
				DataBindlueImportName();				
				lueImportFileName.EditValue = 0;
				lueImportFileName.Refresh();
				ClearTextFields();
				DataBind();
				TabControl.Visible = false;
				btnPrior.Visible = false;
				btnNext.Visible = false;                                
                AIMGlobal.analyzerDataSourceDefinitionID = 0;                                                                                   //1/19/18                                
                Close();                                                                                                                                

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void SetLueDefaults()
		{
			lueOwner.ItemIndex = 1;
			lueOwnerLocation.ItemIndex = 1;
			lueSupplier.ItemIndex = 1;
			lueSupplierLocation.ItemIndex = 1;
		}

		private void ClearTextFields()
		{
			txtFilePath.Text = "";

            txtBeginRow.Text = "0";
            if (txtBeginRow.Visible == true)
			{
				txtBeginRow.Text = "1";	
			}			
			txtSpectraFileExtension.Text = "";
			txtProcessPath.Text = "";
			txtBackUpDir.Text = "";
			txtSpectraDirectory.Text = "";		
		}

		private Int32 MappingChecks()
		{
			if (txtFilePathPreview.Text.Length == 0)
			{
				MessageBox.Show("Please select import file.", "Error...", MessageBoxButtons.OK);
				btnBrowsePreview.PerformClick();
				return -1;
			}

			//-----------------------------------------------------------------------------------
			//Check for SourceColumnNumber = 0 -- and delete any of those records (if the SourceColumnNumber = 0, it means the
			//user clicked the add column button, but then did not enter the column#
			AIMManager.DeleteDataSourceUserTemplateZeroSourceColumnNumber((Int32)lueImportFileName.EditValue, 0);

			//-----------------------------------------------------------------------------------
			//Make sure file type matches what is selected on definition
			String extension = "";
			extension = Path.GetExtension(txtFilePathPreview.Text);
			extension = extension.Substring(1);  //remove 1st character (dot)

			List<ExcelFileTypeInfo> excelfileType = new List<ExcelFileTypeInfo>();
			excelfileType = AIMManager.GetExcelFileTypeName(extension);

            if ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLS
                && excelfileType.Count() == 0)
			{
				MessageBox.Show("Import file must be an XLS Excel file.", "Error...", MessageBoxButtons.OK);
				btnBrowsePreview.PerformClick();
				return -1;
			}

            if ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLSX
                && excelfileType.Count() == 0)
            {
                MessageBox.Show("Import file must be an XLSX Excel file.", "Error...", MessageBoxButtons.OK);
                btnBrowsePreview.PerformClick();
                return -1;
            }
			            
            if ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.TXT
                && (txtFilePathPreview.Text.Substring(txtFilePathPreview.Text.Length - 4) != ".TXT" &&
                    txtFilePathPreview.Text.Substring(txtFilePathPreview.Text.Length - 4) != ".txt"))

            {
				MessageBox.Show("Import file must be a Text file.", "Error...", MessageBoxButtons.OK);
				btnBrowsePreview.PerformClick();
				return -1;
			}
			            
            if ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.CSV
                && (txtFilePathPreview.Text.Substring(txtFilePathPreview.Text.Length - 4) != ".CSV" &&
                   txtFilePathPreview.Text.Substring(txtFilePathPreview.Text.Length - 4) != ".csv"))
            {
				MessageBox.Show("Import file must be a CSV file.", "Error...", MessageBoxButtons.OK);
				btnBrowsePreview.PerformClick();
				return -1;
			}

			//-----------------------------------------------------------------------------------
			//Check for Duplicates in SourceColumnNumber - if there are duplicates, show message, and do not allow preview									
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.DataSourceUserTemplateCountSourceColumnNumberDuplicates((Int32)lueImportFileName.EditValue);
			if (error.ReturnIntegerValue != 0)
			{
				MessageBox.Show("There are duplicate Source Column Numbers (column# " + error.ReturnIntegerValue + ") on the Mapping. Please fix and try again.", "Error...", MessageBoxButtons.OK);
				return -1;
			}            

			return 0;
		}

		private void PreviewGrid()
		{ 
			try	
			{
                Boolean boxIsChecked = false;
                String excelFileExtension = "";
                Int32 importtype = 0;
                AIMGlobal.showAutoGenSampleMsg = true;                                  //12/17/18
				String rtn = "";                                                        //1/6/20
				Int32 detailBeginRow = 0;												//3/7/23

				//--------------------------------------------------                                

				_header = new List<ImportHeaderInfo>();
				List<ImportHeaderInfo> importResults = new List<ImportHeaderInfo>();
				Int32 begDetailCol = _selectedDefinition.NutrientDetailBeginColumn;	
				btnPreview.BackColor = Color.Transparent;
				btnExpand.Text = "Expand All";
                
                if ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLS || (Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLSX)
                {
                    excelFileExtension = Path.GetExtension(@txtFilePathPreview.Text);
                }

                //--------------------------------------------------                
                //CHECK PORTRAIT GROUPING FIELD
                if (_selectedDefinition.OrientationDisplay == "Portrait")
                {		
					//8/15/22
                    //boxIsChecked = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleGrouping(_template.Count, (List<UserDefinedDataSourceTemplateInfo>)grdImportMapping.DataSource);
					boxIsChecked = CFCAIM.AIMUtilities.Imports.SharedFunctions.CheckSampleGrouping(_template.Count, (List<UserDefinedDataSourceTemplateInfo>)grdImportMapping.DataSource, _selectedDefinition.IsCompositeBatch);
					if (boxIsChecked == false) return;
                }

                //--------------------------------------------------
                //PRE-MAPPING CHECKS
                if (MappingChecks() != 0) return;

                //--------------------------------------------------
                //SET HEADER BEGIN ROW                
                Int32 headerBeginRow = CFCAIM.AIMUtilities.Imports.SharedFunctions.SetHeaderBeginRow(_selectedDefinitionID);

                //--------------------------------------------------
                //IMPORT FILE
                AIMGlobal.SplashScreenShow();
				
				//--------------------------------------------------                
				// ImportType 1 = Update Results (for existing samples)
				//			  2 = Update Results and Header Fields (for existing samples)
				//            3 = Update Sample Header Fields (for existing samples)
				//            4 = Add New Samples and Results
				//            5 = Add New Sample and Results OR Update Results for existing samples
				//			100 = Romer samples

				//7/29/19 -- sending type 1 for all the others (just from this screen) because, except for type 5, it really doesn't matter for the preview.
				importtype = 1;
                if (_importType == 5)
                {
                    importtype = 5;
                }

				//--------------------------------------------------
				//12/22/20
				String useThisOwner = String.Empty;
				String useThisOwnerLocation = String.Empty;
				String useThisSupplier = String.Empty;
				String useThisSupplierLocation = String.Empty;

                #region Check UseMapping or SelectNow
                if ((Int32)lueOwner.EditValue != (Int32)SampleImportUserDefinedMapSelection.UseMapping)
                {
                    if ((Int32)lueOwner.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
                    {
						useThisOwner = lueOwnerSelect.Text;
					}
                    else
                    {
						useThisOwner = "Select on Import";
                    }										
                }
				if ((Int32)lueOwnerLocation.EditValue != (Int32)SampleImportUserDefinedMapSelection.UseMapping)
				{
					if ((Int32)lueOwnerLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
					{
						useThisOwnerLocation = lueOwnerLocationSelect.Text;
					}
					else
					{
						useThisOwnerLocation = "Select on Import";
					}
				}
				if ((Int32)lueSupplier.EditValue != (Int32)SampleImportUserDefinedMapSelection.UseMapping)
				{
					if ((Int32)lueSupplier.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
					{
						useThisSupplier = lueSupplierSelect.Text;
					}
					else
					{
						useThisSupplier = "Select on Import";
					}
				}
				if ((Int32)lueSupplierLocation.EditValue != (Int32)SampleImportUserDefinedMapSelection.UseMapping)
				{
					if ((Int32)lueSupplierLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
					{
						useThisSupplierLocation = lueSupplierLocationSelect.Text;
					}
					else
					{
						useThisSupplierLocation = "Select on Import";
					}
				}
				#endregion

				//-----------------------------------------------------------------------
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
					using (var streamReader = new StreamReader(@txtFilePathPreview.Text))
					{
						using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
						{
							while (csvReader.Read())
							{
								if (((CsvHelper.CsvParser)csvReader.Context.Parser).Record[0].StartsWith("Sample"))
								{
									break;
								}

								//5/2/23
								else
								{
									skipRowCount++;
								}
							}
						}
					}
					headerBeginRow = skipRowCount + 1;
					detailBeginRow = headerBeginRow + 1;
					//-------------------------------------------------------------------

					importResults = CFCAIM.AIMUtilities.Imports.SharedFunctions.PreviewRomerImport("Map", _selectedDefinitionID, importtype, false, 0, @txtFilePathPreview.Text, headerBeginRow, 1, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, begDetailCol, excelFileExtension, backgroundWorker1, backgroundWorker1, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation, detailBeginRow);
				}


				//-----------------------------------------------------------------------
				else
				{

					//--------------------------------------------------
					//12/22/20 - added the 4 variables above
					//importResults = CFCAIM.AIMUtilities.Imports.SharedFunctions.PreviewImport("Map", _selectedDefinitionID, importtype, false, 0, @txtFilePathPreview.Text, headerBeginRow, 1, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, begDetailCol, excelFileExtension, backgroundWorker1, backgroundWorker1);  //need to send bogus backgroundWorker1 here because this function is used in User-Defined imports (and sends actual backgroundworker and backgroudPreview)
					importResults = CFCAIM.AIMUtilities.Imports.SharedFunctions.PreviewImport("Map", _selectedDefinitionID, importtype, false, 0, @txtFilePathPreview.Text, headerBeginRow, 1, lueMaterial, lueOwner, lueOwnerLocation, lueSupplier, lueSupplierLocation, begDetailCol, excelFileExtension, backgroundWorker1, backgroundWorker1, useThisOwner, useThisOwnerLocation, useThisSupplier, useThisSupplierLocation);
				}

				AIMGlobal.SplashScreenHide();

                //--------------------------------------------------
                if (importResults.Count == 0)
                {
                    txtFilePathPreview.Text = "";
                }
                _header = importResults;


                //--------------------------------------------------                               
                if (importtype == 5)        //(importtype 5 = Update Header Only -- uses a different grid, to show the header fields)
                {
                    grdPreview.Visible = false;
                    grdPreviewHeader.Visible = true;                    
                    grdPreviewHeader.Location = new Point(14, 50);
                    grdPreviewHeader.DataSource = importResults;
                    grdPreviewHeader.Refresh();

                    //1/6/20 - Update grid column headers with alternate names. 
					//12/23/20 - added defID (so supplier/supplier-loc column header can be changed to "combined" if necessary
                    //rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewHeaderView);
					rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewHeaderView, _selectedDefinitionID, lueSupplier, lueSupplierLocation, "Map");
				}
                else
                {
                    grdPreviewHeader.Visible = false;
                    grdPreview.Visible = true;
                    grdPreview.DataSource = importResults;
                    grdPreview.Refresh();

					//1/6/20 - Update grid column headers with alternate names. 
					//12/23/20 - added defID (so supplier/supplier-loc column header can be changed to "combined" if necessary
					//rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewView);
					rtn = CFCAIM.AIMUtilities.Imports.SharedFunctions.ChangeGridHeaderCaptions(grdPreviewView, _selectedDefinitionID, lueSupplier, lueSupplierLocation, "Map");
				}
                //--------------------------------------------------
                                             

            }
            catch (Exception ex)
			{
                AIMGlobal.SplashScreenHide();
                MessageBox.Show(ex.Message);				
			}
			

		}

		private void btnPreview_Click(object sender, EventArgs e)
		{
		}

		

		private void SaveUserDefinition()
		{
			try
			{
				// * DataSourceUser -- is the data source "type" for "unknown/external" users
				// * DataSourceUserDefinition -- is the "header"
				// * DataSourceUserTemplate -- is the mapping of the columns

				UserDefinedDataSourceDefinitionInfo definition = new UserDefinedDataSourceDefinitionInfo();

				if (txtImportFileName.Text.Length == 0)
				{					
					return;
				}

				try
				{
					if (_isLoading == false)
					{
                        
                        definition.UserDefinedDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;
						definition.ImportFileName = txtImportFileName.Text.ToString();						
						definition.DataSourceTypeID = (Int32)lueDataSource.EditValue;						
						definition.FilePath = "";

						// 1 or true = landscape; 0 or false = portrait
						if (txtOrientation.Text == "Landscape")
						{
							definition.Orientation = true;
						}
						else
						{
							definition.Orientation = false;
						}
						Int32 beginRow = 1;
						Int32.TryParse(txtBeginRow.Text.ToString(), out beginRow);
						definition.HeaderBeginRow = beginRow;
						definition.FileHasHeader = cbFileHasHeader.Checked;
						//definition.FileHasHeader = true;
						definition.IsSpectraFile = cbSpectraFile.Checked;
						//definition.IsFurtherAnalyses = false;				
						
						definition.AIMMaterialMap = (Int32)lueMaterial.EditValue;
						definition.AIMSourceMap = (Int32)lueSupplier.EditValue;
						definition.AIMSourceLocationMap = (Int32)lueSupplierLocation.EditValue;
						definition.AIMOwnerMap = (Int32)lueOwner.EditValue;
						definition.AIMOwnerLocationMap = (Int32)lueOwnerLocation.EditValue;
						definition.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						definition.ProcessDirectory = txtProcessPath.Text;
						definition.BackupDirectory = txtBackUpDir.Text;
						definition.SpectraDirectory = txtSpectraDirectory.Text;			

						definition.AIMMaterialID = (Int32)lueMaterialSelect.EditValue;
						definition.AIMOwnerID = (Int32)lueOwnerSelect.EditValue;
						definition.AIMOwnerLocationID = (Int32)lueOwnerLocationSelect.EditValue;
						definition.AIMSourceID = (Int32)lueSupplierSelect.EditValue;
						definition.AIMSourceLocationID = (Int32)lueSupplierLocationSelect.EditValue;

						definition.ImportFileTypeID = (Int32)lueFileType.EditValue;
						definition.DelimiterTypeID = (Int32)lueDelimiterType.EditValue;                        
                        definition.DecimalSeparatorID = (Int32)lueDecimalSeparator.EditValue;                                                   //4/1/19

                        definition.DateFormatID = (Int32)lueDateFormat.EditValue;
						definition.SpectraFileExtension = txtSpectraFileExtension.Text;

						if (txtSheetNumber.Text == "")
						{
							definition.ExcelSheetNumber = 1;
						}
						else
						{
							definition.ExcelSheetNumber = Int32.Parse(txtSheetNumber.Text);
						}
						

						if (txtDetailBeginColumn.Text == "")
						{
							definition.NutrientDetailBeginColumn = 0;
						}
						else
						{
							definition.NutrientDetailBeginColumn = Int32.Parse(txtDetailBeginColumn.Text);
						}						

						//8/23/22 - for Romer imports
						if (pnlComposite.Visible == true)
						{
							definition.IsCompositeBatch = true;
						}

						//--------------------------------------------------------------------------------------------
						//8/22/22 - for Romer imports
						if ((Int32)lueDataSource.EditValue == (Int32)DataSourceTypeEnum.Romer)
						{
							definition.ImportType = (Int32)EnumImportType.RomerResults;
						}

                        //Enum 1 (Merge) = Update Results Only
                        else if (rbMerge.Checked == true)
                        {
                            definition.ImportType = (Int32)EnumImportType.UpdateResultsOnly;             
                        }                                               
                                                
                        //Enum 4 (ResultsAndHeaderFields) = Update Results and Header Fields For Existing Samples                                    
                        else if (rbResultsAndHeader.Checked == true)                                                                            //4/1/19
                        {                                                                                                                       //4/1/19 
                            definition.ImportType = (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields;                                                //4/1/19
                        }
                                                                                                                                               //4/1/19
                        //Enum 2 (Create) = Update Results and Header Fields
                        else if (rbCreate.Checked == true)
                        {
                            definition.ImportType = (Int32)EnumImportType.AddNewSamplesAndResults;       
                        }                                                                                                                                                                                           //7/26/19

                        //Enum 3 (MergeAndOrCreate) = Add New Samples and Results OR Update Results                                       
                        else if (rbCreateOrMerge.Checked == true)
                        {
                            definition.ImportType = (Int32)EnumImportType.AddNewSamplesAndResultsORUpdateResults;        
                        }

                        //Enum 5 (UpdateHeaderOnly) = Update Header Fields Only
                        else                                                                                                                   //7/26/19
                        {                                                                                                                       //7/26/19
                            definition.ImportType = (Int32)EnumImportType.UpdateHeaderOnly;                                                          //7/26/19
                        }

                        //--------------------------------------------------------------------------------------------


                        if (rbYesMulti.Checked == true)
						{
							definition.DataSourceIsMapped = true;
						}
						else
						{
							definition.DataSourceIsMapped = false;                            
                        }

                        //3/15/18
                        if (rbQualityControlYes.Checked == true)
                        {
                            definition.IsQualityControl = true;
                        }
                        else
                        {
                            definition.IsQualityControl = false;
                        }

                        //12/2/19
                        if (rbAllowDuplicateSampleCodesYes.Checked == true)
                        {
                            definition.AllowDuplicateSampleCodes = true;
                        }
                        else
                        {
                            definition.AllowDuplicateSampleCodes = false;
                        }

                        //12/2/19
                        if (rbSystemCreatedSampleCodeYes.Checked == true)
                        {
                            definition.SystemCreatedSampleCode = true;
                        }
                        else
                        {
                            definition.SystemCreatedSampleCode = false;
                        }

                        //8/23/19
                        Int32 detailBeginRow = 1;
                        Int32.TryParse(txtDetailBeginRow.Text.ToString(), out detailBeginRow);
                        definition.DetailBeginRow = detailBeginRow;

						
						ErrorInfo error = new ErrorInfo();
						error = AIMManager.SaveDataSourceUserDefinition(ref definition);
						if (error.ErrorNumber > 0)
						{
							return;
						}

                        


                        //--------------------------------------------------------------------
                        //2/23/18 Update template record
                        //when user opts to map the analyzer datasource, and actually maps the field, but then turns around and chooses NOT to map the datasource,
                        //this stp will update the template record; otherwise the user would have to remember to go to the mapping and set the column to 'do not use';
                        if (rbYesMulti.Checked == false)
                        {
                            Int32 templateid = 0;
                            templateid = (from g in _template where g.MapColumnName == "DataSource" select g.UserDefinedDataSourceTemplateID).FirstOrDefault();
                            if (templateid != 0)
                            {
                                error = AIMManager.UserDefinedDataSourceTemplate_UpdateMap((Int32)lueImportFileName.EditValue, templateid, 1, "DO NOT USE");
                                if (error.ErrorNumber > 0)
                                {
                                    MessageBox.Show("Error updating Map for DataSource column.", "Error...", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                        }


                        Int32 saveRec = 0;
						if ((Int32)lueImportFileName.EditValue == 0)
						{
							saveRec = error.RecordID;
							DataBindlueImportName();
						}
						else
						{
							saveRec = (Int32)lueImportFileName.EditValue;
						}
					
						lueImportFileName.EditValue = saveRec;
					}

                    //12/3/19 - commented - causing infinite loop
					//////DataBind();
				}

				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void cbFileHasHeader_CheckedChanged(object sender, EventArgs e)
		{
            SaveUserDefinition();
						
            lblBeginRow.Visible = false;
            lblBeginRow2.Visible = false;
            txtBeginRow.Visible = false;

            //2/26/18
            txtBeginRow.Text = "0";
            lblLine2.Visible = false;
            lblRequiredHeaderRow.Visible = false;

            if (cbFileHasHeader.Checked == true)
            {
                lblBeginRow.Visible = true;
                lblBeginRow2.Visible = true;
                txtBeginRow.Visible = true;

                //2/26/18
                lblLine2.Visible = true;
                lblRequiredHeaderRow.Visible = true;
            }
        }
		private void cbSpectraFile_CheckedChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();

            lblSpectraFileExtension.Visible = false;
			txtSpectraFileExtension.Visible = false;
			lblSpectraDirectory.Visible = false;
			txtSpectraDirectory.Visible = false;
			btnBrowseSpectra.Visible = false;

			if (cbSpectraFile.Checked == true)
			{
				lblSpectraFileExtension.Visible = true;
				txtSpectraFileExtension.Visible = true;
				lblSpectraDirectory.Visible = true;
				txtSpectraDirectory.Visible = true;
				btnBrowseSpectra.Visible = true;
			}

		}
		
		private void txtImportFileName_Leave(object sender, EventArgs e)
		{
            //4/3/18 -- commented, why are these cleared?!
			//txtBackUpDir.Text = "";
			//txtProcessPath.Text = "";

			SaveUserDefinition();
			lueImportFileName.Refresh();
			ShowOrHideTabs();
						
			if (((List<DataSourceTypeInfo>)lueDataSource.Properties.DataSource).Count == 1)
			{  
				MessageBox.Show("A Data Source needs to be set up (under Setup > User-Defined Data Source) before continuing. ", "Warning...", MessageBoxButtons.OK);							
				return;
			}

			if (grpDataSource.Enabled == true && (Int32)lueDataSource.EditValue == 0)
			{
				lueDataSource.ShowPopup();
			}
		}
		private void lueDataSource_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();

			Int32 datasourceID = 0;
			Int32.TryParse(lueDataSource.EditValue.ToString(), out datasourceID);

			if (datasourceID != 0)
			{
				grpFileDefinition.Enabled = true;
			}


		}
			
		
		private void lueOwner_Leave(object sender, EventArgs e)
		{
            //12/15/20 - commented(CSA)
            //lueOwnerLocation.EditValue = lueOwner.EditValue;		
           
            
			
		}
		private void lueOwnerLocation_Leave(object sender, EventArgs e)
		{
			//12/15/20 - commented (CSA)
			//lueOwner.EditValue = lueOwnerLocation.EditValue;		
		}

		private void cbSpectraFile_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}
		private void cbFileHasHeader_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}
		private void btnAddColumn_Click(object sender, EventArgs e)
		{
			try
			{
				btnAddColumn.BackColor = Color.Transparent;

				if (txtColumnName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Column Name. ", "Warning...", MessageBoxButtons.OK);
					txtColumnName.Focus();
					return;
				}
				
				//////Check template table for existing columns - get last SourceColumnNumber			
				////ErrorInfo outPut = AIMManager.GetUserDefinedDataSourceTemplateLastSourceColumnNumber((Int32)lueImportFileName.EditValue);
				
				UserDefinedDataSourceTemplateInfo template = new UserDefinedDataSourceTemplateInfo();
				template.UserDefinedDataSourceTemplateID = 0;
				template.UserDefinedDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;				
				template.IsManualEntry = true;				
				template.SourceColumnName = txtColumnName.Text.ToString();
				template.MapColumnName = "";
				template.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveUserDefinedDataSourceTemplate(ref template);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtColumnName.Text = "";
				DataBind();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueDataSource_EditValueChanged(object sender, EventArgs e)
		{			

			lueMaterialSelect.EditValue = 0;
			lueOwnerSelect.EditValue = 0;
			lueOwnerLocationSelect.EditValue = 0;
			lueSupplierSelect.EditValue = 0;
			lueSupplierLocationSelect.EditValue = 0;

			SaveUserDefinition();

			//databind the lueFileType for this datasourcetypeid
			DataBindFileTypelue();

			DataBindLueAIM();
			ShowOrHideTabs();
		}
		private void txtFilePath_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
			txtFilePathPreview.Text = txtFilePath.Text;
		}

        private void ShowHideDoNotUseButton()
        {
            //12/12/18 -- check template to see if all are set to "Do not use" if so, change _showDoNotUse = false and change button text
            Int32 countDoNotUse = 0;
            _showDoNotUse = true;
            btnDoNotUse.Text = "Set All to" + Environment.NewLine + "DO NOT USE";
            List<UserDefinedDataSourceTemplateInfo> gridTemplate = new List<UserDefinedDataSourceTemplateInfo>();
            gridTemplate = (List<UserDefinedDataSourceTemplateInfo>)grdImportMapping.DataSource;
            if (gridTemplate.Count == 0)
            {
                btnDoNotUse.Visible = false;
            }
            else
            {
                btnDoNotUse.Visible = true;

                if (gridTemplate != null)
                {
                    foreach (var item in gridTemplate)
                    {
                        if (item.MapColumnID == 1)
                        {
                            countDoNotUse++;
                        }
                    }
                }
                if (gridTemplate.Count == countDoNotUse && gridTemplate.Count != 0)
                {
                    _showDoNotUse = false;
                    btnDoNotUse.Text = "Clear AIM Columns";
                }
            }
        }

		private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			CheckArrows();

			//8/12/22 - added for Romer
			if (TabControl.SelectedIndex == 0)
			{
				pnlDatasource.Enabled = true;
				pnlQualityControl.Visible = true;
				pnlAllowDuplicates.Visible = true;
				pnlCreateSampleCode.Visible = true;
				pnlImportType.Visible = true;
				if (_importType == (Int32)EnumImportType.RomerResults)
				{
					pnlDatasource.Enabled = false;
					pnlQualityControl.Visible = false;
					pnlAllowDuplicates.Visible = false;
					pnlCreateSampleCode.Visible = false;
					pnlImportType.Visible = false;
				}
			}


			if (TabControl.SelectedIndex == 1)		//Column Map
			{
                DataBindTemplate();                
                ShowHideDoNotUseButton();

                if ((Int32)lueFileType.EditValue == 0)
				{
					MessageBox.Show("Please select a File Type. ", "Warning...", MessageBoxButtons.OK);
					TabControl.SelectedIndex = 0;
					lueFileType.Focus();
					return;
				}
								
				if (_needDelimiterType == true && (Int32)lueDelimiterType.EditValue == 0)
				{
					MessageBox.Show("Please select a Delimiter Type. ", "Warning...", MessageBoxButtons.OK);
					TabControl.SelectedIndex = 0;
					lueDelimiterType.Focus();
					return;
				}

                if (cbFileHasHeader.Checked == true)
                {                
				    if (txtBeginRow == null || txtBeginRow.Text == "")
				    {
					    txtBeginRow.Text = "0";
				    }
                }

                if (txtSpectraFileExtension == null)
				{
					txtSpectraFileExtension.Text = "";
				}
				if (cbSpectraFile.Checked == true && (txtSpectraFileExtension.Text == ""))
				{
					MessageBox.Show("Please enter a Spectra File Extension. ", "Warning...", MessageBoxButtons.OK);
					TabControl.SelectedIndex = 0;
					txtSpectraFileExtension.Focus();
					return;
				}

				if (grdImportMappingView.RowCount == 0 && txtProcessPath.Text.Length > 0)
				{
					btnBrowse.PerformClick();
				}

				//8/12/22
				if (_importType == (Int32)EnumImportType.RomerResults)
				{					
					grdImportMapping.Enabled = false;
					gbBrowse.Enabled = false;
					btnDeleteAll.Enabled = false;
					btnDoNotUse.Visible = false;
					btnHelpMapping.Visible = false;
					lblSystemSampleCodes.Visible = false;
				}


			}

            

			if (TabControl.SelectedIndex == 2)		//Preview
			{
				if (txtProcessPath.Text.Length > 0)
				{
					btnBrowsePreview.PerformClick();
				}
			}
			

			if (TabControl.SelectedTab == TabPage)
			{
				txtFilePathPreview.Text = "";				
			}
		}
		private void replueMapColumnName_Leave(object sender, EventArgs e)
		{
			_mapToThisColumn = ((DevExpress.XtraEditors.LookUpEdit)(sender)).Text;
			
		}
		private void btnBrowsePreview_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog = new OpenFileDialog();
				String path = txtProcessPath.Text.ToString();
				if (Directory.Exists(path))
				{
					openFileDialog.InitialDirectory = path;
				}
				else
				{
					openFileDialog.InitialDirectory = @"C:\";
				}
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					string filePath = openFileDialog.FileName;
					txtFilePathPreview.Text = openFileDialog.FileName;					
					ClearPreviewGrid();

					PreviewGrid();
				}
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
				if (Directory.Exists(txtProcessPath.Text) == true)
				{
					fbdSelectedDirectory.SelectedPath = txtProcessPath.Text;
				}
				fbdSelectedDirectory.ShowDialog();
				txtProcessPath.Text = fbdSelectedDirectory.SelectedPath;

				if (txtSpectraDirectory.Text.Length == 0)
				{
					txtSpectraDirectory.Text = txtProcessPath.Text;
				}

				SaveUserDefinition();
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
				SaveUserDefinition();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void txtProcessPath_Leave(object sender, EventArgs e)
		{			
			SaveUserDefinition();			
		}
		private void txtBackUpDir_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}
		
		private void lueAIMMaterial_EditValueChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}
		
		private void lueMaterial_EditValueChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();			
			////EnableOrDisableSelectDropdowns(lueMaterial, lueMaterialSelect);
		}
		
		
		private void EnableOrDisableSelectDropdowns(DevExpress.XtraEditors.LookUpEdit lue, DevExpress.XtraEditors.LookUpEdit lueSelect)	
		{
			lueSelect.Visible = false;
			lueSelect.Enabled = false;
			lueSelect.EditValue = 0;

			//12/15/20
			//if ((Int32)lue.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectOnImport)
			if ((Int32)lue.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
			{
				lueSelect.Enabled = true;
				lueSelect.Visible = true;
			}
            else
            {
                if (lue == lueOwner)
                {
					lueOwnerLocationSelect.Visible = false;
                }
				if (lue == lueSupplier)
				{
					lueSupplierLocationSelect.Visible = false;
				}
			}
		}
		
		private void lueOwnerSelect_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SaveUserDefinition();					
				DataBindOwnerLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private void DataBindSupplierLocation()
		{
			Int32 supplierLocationID = 0;
			Int32 supplierID = 0;
			if (lueSupplierLocationSelect.EditValue != null)
			{
				Int32.TryParse(lueSupplierSelect.EditValue.ToString(), out supplierID);
			}

			//12/15/20
			//_aimSupplierLocation = AIMManager.GetAIMSupplierLocationRefAnalyzerSupplierLocation((Int32)lueDataSource.EditValue);
			_aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID);

			_aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,					
					AIMSupplierLocationCode = "--Select--",
					SupplierLocationName = "--Select--"
				}
			); ;
			lueSupplierLocationSelect.Properties.DataSource = _aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationSelect, "AIMSupplierLocationID", "SupplierLocationName");
			lueSupplierLocationSelect.Properties.Columns["SupplierLocationName"].Visible = true;
			lueSupplierLocationSelect.EditValue = supplierLocationID;
		}

		private void DataBindOwnerLocation()
		{
			Int32 OwnerLocationID = 0;

			//12/15/20 
			//Int32 dataSourceTypeID = 0;
			//if (lueDataSource.EditValue != null)
			//{
			//	Int32.TryParse(lueDataSource.EditValue.ToString(), out dataSourceTypeID);
			//}
			Int32 ownerID = 0;
            if (lueOwnerSelect.EditValue != null)
			{
				Int32.TryParse(lueOwnerSelect.EditValue.ToString(), out ownerID);
			}
			if (ownerID == 0)
			{
				_aimOwnerLocation.Clear();
			}
			else
			{
				//12/15/20
				//_aimOwnerLocation = AIMManager.GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeID(dataSourceTypeID);
				_aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(ownerID);
			}
			_aimOwnerLocation.Insert
			(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					LocationCode = "--Select--",
					LocationName = "--Select--"
				}
			);
			lueOwnerLocationSelect.Properties.DataSource = _aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationSelect, "AIMOwnerLocationID", "LocationCode", "LocationName");
			lueOwnerLocationSelect.Properties.Columns["LocationCode"].Visible = true;
			lueOwnerLocationSelect.EditValue = OwnerLocationID;

		}

		private void lueOwner_EditValueChanged(object sender, EventArgs e)
		{			
			SaveUserDefinition();						
			EnableOrDisableSelectDropdowns(lueOwner, lueOwnerSelect);

			//12/15/20
			if (lueOwnerLocation.EditValue.ToString() == "1") { }
			else
			{
				if ((Int32)lueOwnerLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
				{
					if ((Int32)lueOwner.EditValue != (Int32)SampleImportUserDefinedMapSelection.SelectNow)
					{
						lueOwnerLocation.EditValue = lueOwner.EditValue;
					}
				}
			}
		}

		
		private void lueOwnerLocation_EditValueChanged(object sender, EventArgs e)
		{

			

			SaveUserDefinition();
			EnableOrDisableSelectDropdowns(lueOwnerLocation, lueOwnerLocationSelect);

			//12/15/20
			if ((Int32)lueOwnerLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
			{
				if ((Int32)lueOwner.EditValue != (Int32)SampleImportUserDefinedMapSelection.SelectNow)
				{
					lueOwnerLocation.EditValue = lueOwner.EditValue;
				}
			}			
		}

		private void lueMaterialSelect_EditValueChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();	
		}
				

		private void lueOwnerLocationSelect_EditValueChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();	
		}

		private void txtBeginRow_TextChanged(object sender, EventArgs e)
		{            
            if (System.Text.RegularExpressions.Regex.IsMatch(txtBeginRow.Text, "[^0-9]"))
			{
				MessageBox.Show("Please enter only numbers.");
				txtBeginRow.Text = txtBeginRow.Text.Remove(txtBeginRow.Text.Length - 1);                           				
			}
            else
            {
                lblRequiredHeaderRow.Visible = false;
                txtBeginRow.BackColor = Color.White;
            }

            if (txtBeginRow.Text == "0")
            {
                lblRequiredHeaderRow.Visible = true;
                txtBeginRow.BackColor = SystemColors.Info;
            }

            checkExcelLandscapeHeadingRow();

			
        }

        private void txtBeginRow_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();

            //3/20/25
            clearTemplateDef();
        }

		private void btnPortrait_Click(object sender, EventArgs e)
		{
			txtOrientation.Text = "Portrait";		
			btnPortrait.BackColor = Color.LightSkyBlue;
			btnLandscape.BackColor = Color.Transparent;
			_landscapeClicked = false;

            //2/26/18
            lblLine2.Visible = false;
            txtBeginRow.BackColor = SystemColors.Info;
            txtBeginRow.Text = "0";           //if user changes from Landscape to Portrait, hard-coded to 0
            cbFileHasHeader.Checked = true;


            //2/26/18
            //txtDetailBeginColumn.Text = "1";
            txtDetailBeginColumn.Text = "0";
            

            SaveUserDefinition();
			checkExcelLandscapeHeadingRow();
			checkNutrientBeginColumn();
		}

		private void btnLandscape_Click(object sender, EventArgs e)
		{
			txtOrientation.Text = "Landscape";
			btnLandscape.BackColor = Color.LightSkyBlue;
			btnPortrait.BackColor = Color.Transparent;
			_landscapeClicked = true;

			lblDetailBeginColumn.Visible = true;
			lblDetailBeginColumn2.Visible = true;
			txtDetailBeginColumn.Visible = true;
			lblRequired.Visible = true;

            //2/26/18
            lblRequiredHeaderRow.Visible = true;
            lblLine2.Visible = true;

            //if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.CSV)
            //{							
            //	SaveUserDefinition();				
            //}
            //else
            //{

            //2/26/18
            //txtDetailBeginColumn.Text = "1";  //if user changes from Portrait to Landscape, hard-coded to 1
            txtDetailBeginColumn.Text = "0";  //if user changes from Portrait to Landscape, hard-coded to 0

                txtBeginRow.Text = "0";           //if user changes from Portrait to Landscape, hard-coded to 0
                cbFileHasHeader.Checked = true;


                SaveUserDefinition();
				checkExcelLandscapeHeadingRow();
				checkNutrientBeginColumn();
			//}
			
		}

		private void btnPortrait_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}

		private void btnLandscape_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			btnPortrait.BackColor = Color.Transparent;
			btnLandscape.BackColor = Color.Transparent;
			lueImportFileName.EditValue = 0;
			SetLueDefaults();
			ClearTextFields();
		}
		private void populateImportMappingGrid()
		{
			String msg = "";

			try
			{
                Int32 colCount = 0;				
				String importFileName = txtFilePath.Text;
				UserDefinedDataSourceTemplateInfo template = new UserDefinedDataSourceTemplateInfo();
                Boolean showMsg = true;
				Boolean skipSecondMessage = false;                

                if (txtFilePath.Text.Length == 0)
				{
					MessageBox.Show("Please select import file.", "Error...", MessageBoxButtons.OK);
					btnBrowse.PerformClick();
					return;
				}

				AIMGlobal.SplashScreenShow();

				Int32 sourceColNumber = 0;

				//3/20/25
				//Int32 headerBeginRow = _selectedDefinition.HeaderBeginRow;
                Int32 headerBeginRow = Convert.ToInt32(txtBeginRow.Text);
               

                if (headerBeginRow > 0)
				{
					headerBeginRow--;   //doing this because DevExpress Spreadsheet starts columns and rows at 0, not 1
				}

                //3/20/25
                //Int32 beginDetail = _selectedDefinition.NutrientDetailBeginColumn;
                Int32 beginDetail = Convert.ToInt32(txtDetailBeginColumn.Text);

                switch ((Int32)lueFileType.EditValue)
                {                    
                    case (Int32)FileTypeEnum.CSV:
                    case (Int32)FileTypeEnum.TXT:

                        #region CSV_or_TXT
                        {
                            Char delimiterValue = ',';

                            //Get delimiter
                            if (_selectedDefinition.DelimiterTypeID == (Int32)DelimiterTypeEnum.Comma)
                                delimiterValue = ',';
                            if (_selectedDefinition.DelimiterTypeID == (Int32)DelimiterTypeEnum.Semicolon)
                                delimiterValue = ';';
                            if (_selectedDefinition.DelimiterTypeID == (Int32)DelimiterTypeEnum.Verticalbar)
                                delimiterValue = '|';
                            if (_selectedDefinition.DelimiterTypeID == (Int32)DelimiterTypeEnum.Tab)
                                delimiterValue = '\t';

							var path = importFileName;
							using (TextFieldParser csvParser = new TextFieldParser(path))
							{								
								//csvParser.SetDelimiters(new string[] { "," });
                                csvParser.SetDelimiters(new string[] { delimiterValue.ToString() });

								csvParser.HasFieldsEnclosedInQuotes = true;

								// Skip the row with the column names
								// csvParser.ReadLine();

								try
								{								
									if (!csvParser.EndOfData)
									{										
										string[] fields = csvParser.ReadFields();

										//For landscape, return only the sample header fields
										if (_selectedDefinition.Orientation == true)
										{
											colCount = beginDetail - 1;
										}
										//For portrait, return all the fields
										else
										{
											colCount = fields.Count();
										}
										
										for (int i = 0; i < colCount; i++)
										{

                                            //----------------------------------------------------------
                                            //3/13/18 - added this in case user selects comma and txt files has semi-colons or vice-versa --
                                            //          so bad error message doesn't pop
                                            if (i + 1 > fields.Count())
                                            {
                                                if (showMsg == true)
                                                {
                                                    AIMGlobal.SplashScreenHide();
                                                    MessageBox.Show("Check the template settings. Columns do not match template.", "Warning...", MessageBoxButtons.OK);
                                                    AIMGlobal.SplashScreenShow();
                                                }
                                                showMsg = false;
                                            }
                                            else
                                            {
                                            //----------------------------------------------------------

                                                template.UserDefinedDataSourceTemplateID = 0;
                                                template.UserDefinedDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;
                                                template.IsManualEntry = false;
                                                if (fields[i] == null)
                                                {
                                                    template.SourceColumnName = "";
                                                }
                                                else
                                                {
                                                    template.SourceColumnName = fields[i].ToString();
                                                }
                                                template.SourceColumnNumber = i + 1;
                                                template.MapColumnID = 0;
                                                template.MapColumnName = "";
                                                template.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                                                ErrorInfo error = new ErrorInfo();
                                                error = AIMManager.SaveUserDefinedDataSourceTemplate(ref template);                                            
											    if (error.ErrorNumber > 0)
											    {
												    return;
											    }
                                            }
                                        }									
									}
								}
								catch (Exception ex)
								{
									MessageBox.Show(ex.Message);
								}

								csvParser.Close();
							}
							
						}				

						break;
                    #endregion

                    //5/15/20 - changed to use correct ENUM
                    //case (Int32)ImportFileTypeEnum.Excel:
                    case (Int32)FileTypeEnum.XLS:
                    case (Int32)FileTypeEnum.XLSX:

                        #region Excel	

                        // Load a workbook from a file. 
                        
                        //8/5/20                     
                        //Range range;
                        CellRange range;

                        Int32 lastColumn = 0;
						Int32 lastRow = 0;
						Int32 sheetNo = 1;
						String str = String.Empty;
						SpreadsheetControl spreadsheetControl = new SpreadsheetControl();
                                                
                        String excelFileExtension = "";
                        excelFileExtension = Path.GetExtension(@importFileName);
                        if (excelFileExtension.ToUpper() == ".XLS")
                        {
                            spreadsheetControl.LoadDocument(@importFileName, DocumentFormat.Xls);
                        }
                        else
                        {
                            spreadsheetControl.LoadDocument(@importFileName, DevExpress.Spreadsheet.DocumentFormat.Xlsx);
                        }

						//4/20/17
						if (_selectedDefinition != null)
						{
							sheetNo = _selectedDefinition.ExcelSheetNumber;
							if (sheetNo == 0)
							{
								sheetNo = 1;
							}
						}

                        //4/19/17 if the data is on the excel sheet 1, the code sees that as sheet 0							
                        Worksheet worksheet = spreadsheetControl.Document.Worksheets[sheetNo - 1];                                               

                        
                        range = worksheet.GetDataRange();
						lastRow = range.RowCount;
						lastColumn = range.ColumnCount;

						//-------------------------------------------------------------------------
						//For landscape, return only the header fields from Spreadsheet
						if (_selectedDefinition.Orientation == true)
						{
							colCount = beginDetail - 1;
						}
						//For portrait, return all the fields from Spreadsheet
						else
						{							
							colCount = range.ColumnCount;
						}
						//-------------------------------------------------------------------------						
						for (int j = 0; j < colCount; j++)
						{
							try
							{
								template.UserDefinedDataSourceTemplateID = 0;
								template.UserDefinedDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;
								template.IsManualEntry = false;								
								if (worksheet[headerBeginRow, j].Value == null)
								{
									template.SourceColumnName = "";
								}
								else
								{																		
									template.SourceColumnName = worksheet[headerBeginRow, j].Value.ToString().Trim();
								}
								template.SourceColumnNumber = sourceColNumber + 1;
								sourceColNumber = sourceColNumber + 1;
								template.MapColumnID = 0;
								template.MapColumnName = "";
								template.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

								ErrorInfo error = new ErrorInfo();
								error = AIMManager.SaveUserDefinedDataSourceTemplate(ref template);

								if (error.ErrorNumber > 0)
								{
									return;
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message);
							}
						}						
						break;
                        
						#endregion

					default:
						AIMGlobal.SplashScreenHide();
						MessageBox.Show("Import file format has not been set up. Contact IT.", "Error...", MessageBoxButtons.OK);
						lueDataSource.EditValue = _saveDataSourceID;
						lueFileType.EditValue = _saveFileTypeID;
						break;
				}				
								
				DataBind();
				AIMGlobal.SplashScreenHide();

                //----------------------------------------------------------------------------
                // For Excel, if ALL the import-file names are blank,   
                //		pop message for user to check if correct excel sheet# is being used.

                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Excel)
                if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX)

                {					
					List<UserDefinedDataSourceTemplateInfo> userTemplate = new List<UserDefinedDataSourceTemplateInfo>();
					userTemplate = AIMManager.GetUserDefinedDataSourceTemplate((Int32)lueImportFileName.EditValue);
					Int32 countFields = 0;
					for (int i = 0; i < userTemplate.Count; i++)
					{
						if (userTemplate[i].SourceColumnName.ToString() == "")
						{
							countFields++;
						}
					}
					if (countFields == userTemplate.Count)
					{
						if (skipSecondMessage == false)
						{
							msg = "All the Import File Column Names are blank.";
							msg += Environment.NewLine + Environment.NewLine + "Things to check:  ";
							msg += Environment.NewLine + Environment.NewLine;
							msg += "    Make sure the numbers for the Column Header Begin Row and " + Environment.NewLine;
							msg += "          Nutrient Detail Begin Column are correct.";
							msg += Environment.NewLine + Environment.NewLine;
							msg += "    Are you using the correct Excel Sheet Number?";
							MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
						}
						skipSecondMessage = false;
					}					
				}	
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
				
		private void replueMapColumnName_EditValueChanged(object sender, EventArgs e)
		{
			//Get IsDetail value from dropdown and update the UserDefinedDataSourceTemplate table
			DevExpress.XtraEditors.LookUpEdit editor = (sender as DevExpress.XtraEditors.LookUpEdit);			
			object value = replueMapColumnName.GetDataSourceRowByKeyValue(editor.EditValue);



			UserDefinedDataSourceTemplateInfo template = new UserDefinedDataSourceTemplateInfo();			
			template = (UserDefinedDataSourceTemplateInfo)grdImportMappingView.GetFocusedRow();

            if (template != null)	
            {
                if (value != null)		//12/28/20
                {
					template.MapColumnName = ((SampleImportColumnNamesInfo)(value)).MapColumnName;
					template.MapColumnID = ((SampleImportColumnNamesInfo)(value)).MapColumnID;                                                          //4/3/18
					template.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					template.IsDetail = ((SampleImportColumnNamesInfo)(value)).IsDetail;
				}

				//remove selected column from lookup-list
				


								
			}
			
			AIMManager.SaveUserDefinedDataSourceTemplate(ref template);

		}

		#region Master Events
		private void grdPreview_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
		{
			GridView view;
			view = e.View as GridView;

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

			view.Columns["DetailMessage"].Visible = false;
			view.Columns["DetailMessage"].AppearanceCell.Font = new System.Drawing.Font("Arial", 7.8F);
			view.Columns["DetailMessage"].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9.0F, System.Drawing.FontStyle.Bold);
			view.Columns["DetailMessage"].OptionsColumn.AllowEdit = false;
			view.Columns["DetailMessage"].Caption = "Message";

			//4/4/17
			view.Columns["AnalyzerDataSourceTypeName"].Caption = "Data Source";
			view.Columns["AnalyzerNutrientCode"].Caption = "Nutrient Code";

			//Hide columns
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



            //Hide units (for Excel/Landscape)

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)ImportFileTypeEnum.Excel && _selectedDefinition.OrientationDisplay == "Landscape")
            if ( ((Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLS || (Int32)_selectedDefinition.ImportFileTypeID == (Int32)FileTypeEnum.XLSX) && _selectedDefinition.OrientationDisplay == "Landscape")

            {
				view.Columns["UnitType"].Visible = false;
			}
			

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
			List<ImportDetailInfo> detail = new List<ImportDetailInfo>();
			header = (ImportHeaderInfo)grdPreviewView.GetRow(e.RowHandle);			
			e.ChildList = _header[e.RowHandle].DetailImport;
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
				////dataRowCount = 100;   //hardcoded because test is using sample results -- too many!
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

		private void repTextSourceColumnName_Leave(object sender, EventArgs e)
		{
			UserDefinedDataSourceTemplateInfo dataDef = new UserDefinedDataSourceTemplateInfo();
			dataDef = (UserDefinedDataSourceTemplateInfo)grdImportMappingView.GetFocusedRow();			
			_mapToThisColumn = dataDef.MapColumnName;
		}

		private void txtColumnName_Leave(object sender, EventArgs e)
		{
			if (txtColumnName.Text.Trim().Length != 0)
			{				
				btnAddColumn.BackColor = SystemColors.Info;
			}
			else 
			{
				btnAddColumn.BackColor = Color.Transparent;
			}
		}

		private void btnPreviewMap_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdPreview, "Import Mapping - " + lueImportFileName.Text.ToString());
				CFCAIMCore.Global.previewGridResults(grdPreview, "Import Mapping - " + lueImportFileName.Text.ToString());
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueFileType_EditValueChanged(object sender, EventArgs e)
		{

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueFileType.EditValue != 0 &&
            //	(Int32)lueFileType.EditValue != (Int32)ImportFileTypeEnum.Text &&
            //	(Int32)lueFileType.EditValue != (Int32)ImportFileTypeEnum.CSV &&
            //	(Int32)lueFileType.EditValue != (Int32)ImportFileTypeEnum.Excel)
            if ((Int32)lueFileType.EditValue != 0 &&
                (Int32)lueFileType.EditValue != (Int32)FileTypeEnum.TXT &&
                (Int32)lueFileType.EditValue != (Int32)FileTypeEnum.CSV &&
                (Int32)lueFileType.EditValue != (Int32)FileTypeEnum.XLSX &&
                (Int32)lueFileType.EditValue != (Int32)FileTypeEnum.XLS)
            {
                MessageBox.Show("This import file format is currently unavailable for User-Defined imports. Contact IT.", "Warning...", MessageBoxButtons.OK);				
				return;
			}
			

			//4/7/17 - Currently, assigning datasource on detail is allowed on TXT imports only. (Haven't coded for excel portrait/landscape or CSV or XML)
			lblDataSourceMapped.Visible = false;
			rbYesMulti.Visible = false;
			rbNoMulti.Visible = false;

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Text)
            if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.TXT)
            {
				lblDataSourceMapped.Visible = true;
				rbYesMulti.Visible = true;
				rbNoMulti.Visible = true;
			}

			lblDelimiterType.Visible = false;
			lueDelimiterType.Visible = false;            
            lblDecimalSeparator.Visible = false;                                                                                                //4/1/19
            lueDecimalSeparator.Visible = false;                                                                                                //4/1/19

            btnPortrait.Visible = true;
			btnLandscape.Visible = true;
			_needDelimiterType = false;

			lblDetailBeginColumn.Visible = false;
			lblDetailBeginColumn2.Visible = false;
			txtDetailBeginColumn.Visible = false;
			lblRequired.Visible = false;
            
            lblRequiredHeaderRow.Visible = false;
            lblLine3.Visible = false;

            //3/29/19 - changed to allow delimiter type on CSV files also  (found with Vitapro)

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Text || (Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.CSV)
            if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.TXT || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.CSV)
            {

                lblDelimiterType.Visible = true;
                lueDelimiterType.Visible = true;
                _needDelimiterType = true;
                                
                lblDecimalSeparator.Visible = true;                                                                                             //4/1/19
                lueDecimalSeparator.Visible = true;                                                                                             //4/1/19

            }
            if (txtOrientation.Text == "Landscape")
            {
                lblDetailBeginColumn.Visible = true;
                lblDetailBeginColumn2.Visible = true;
                txtDetailBeginColumn.Visible = true;
                lblRequired.Visible = true;                
                lblRequiredHeaderRow.Visible = true;
            }

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Excel)
            if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX)
           
            {
                lueDelimiterType.EditValue = 0;
            }

            //3/29/19 - commented because now CSV can have different delimiters
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.CSV)
            //{
            //    lueDelimiterType.EditValue = (Int32)DelimiterTypeEnum.Comma;
            //}

            SaveUserDefinition();
			checkExcelLandscapeHeadingRow();
			
			
		}


		private void checkNutrientBeginColumn()
		{
			txtDetailBeginColumn.Visible = true;
			lblDetailBeginColumn.Visible = true;
			lblDetailBeginColumn2.Visible = true;
			lblRequired.Visible = false;
			lblRequired.Text = "Required";
                        
            lblRequiredHeaderRow.Visible = false;                                                                                               //2/26/18
            lblRequiredHeaderRow.Text = "Required";                                                                                             //2/26/18



            if (txtOrientation.Text == "Portrait")
			{
				txtDetailBeginColumn.Visible = false;
				lblDetailBeginColumn.Visible = false;
				lblDetailBeginColumn2.Visible = false;
				lblRequired.Visible = false;
                lblRequiredHeaderRow.Visible = false;                                                                                           //2/26/18
            }
			else
			{
               


                txtDetailBeginColumn.BackColor = Color.White;
				if (txtDetailBeginColumn.Text.Length == 0)
				{
					txtDetailBeginColumn.BackColor = SystemColors.Info;
					txtDetailBeginColumn.Focus();
					lblRequired.Visible = true;
                    lblRequiredHeaderRow.Visible = true;                                                                                        //2/26/18
                    txtBeginRow.BackColor = SystemColors.Info;                                                                                  //2/26/18

                    /////////txtDetailBeginColumn.Text = "0";
                }
				else if (_landscapeClicked == true)
				{
					txtDetailBeginColumn.BackColor = SystemColors.Info;
					txtDetailBeginColumn.Focus();
					lblRequired.Visible = true;
					_landscapeClicked = false;
                    lblRequiredHeaderRow.Visible = true;                                                                                        //2/26/18
                    txtBeginRow.BackColor = SystemColors.Info;                                                                                  //2/26/18

                }
			}
			if (txtDetailBeginColumn.Text == "0" && txtOrientation.Text == "Landscape")
			{
				lblRequired.Visible = true;
				lblRequired.Text = "Value must be greater than zero.";
				txtDetailBeginColumn.Text = "0";
                txtDetailBeginColumn.BackColor = SystemColors.Info;
            }

            //2/26/18
            if (cbFileHasHeader.Checked == true)
            {
                if (txtBeginRow.Text == "0" || txtBeginRow.Text == "" || txtBeginRow.Text == null)
                {
                    lblRequiredHeaderRow.Visible = true;
                    lblRequiredHeaderRow.Text = "Value must be greater than zero.";
                }

                
            }
		}

		private void checkExcelLandscapeHeadingRow()
		{
			if (lueFileType.EditValue != null)
			{
                //5/15/20 - changed to use correct ENUM
                //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Excel && txtOrientation.Text == "Landscape")
                if (((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX) && txtOrientation.Text == "Landscape")

                {

					if (cbFileHasHeader.Checked == false)
					{
                        //2/26/18 - commented
						//MessageBox.Show("Excel-Landscape import files must contain a heading row. Column Header row has been changed to 1.");						
						//txtBeginRow.Text = "1";
						cbFileHasHeader.Checked = true;	
					}

					cbFileHasHeader.Checked = true;				
					if (txtBeginRow.Text == "0" || txtBeginRow.Text == "" || txtBeginRow.Text == null)
					{

                        //2/26/18 - commented
						//MessageBox.Show("Excel-Landscape import files must contain a heading row. Column Header row and Detail Start column have been changed to 1.");						
						//txtBeginRow.Text = "1";
					}
				}
			}
		}

		private void lueDelimiterType_EditValueChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}

		

		private void txtOrientation_TextChanged(object sender, EventArgs e)
		{
			checkExcelLandscapeHeadingRow();
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{

            Imports.ImportHelp importHelp = new Imports.ImportHelp();
            importHelp.ShowDialog();
		}

		private void txtSpectraFileExtension_TextChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}

		private void txtSpectraFileExtension_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
			CheckForSpectraFiles();			
		}

		private void cbFileHasHeader_MouseClick(object sender, MouseEventArgs e)
		{           
                
            //2/1/18 - column headings are REQUIRED for landscape templates (optional for portrait) -- because, on landscape, we don't map the 
            //              result fields. For portrait, the user maps the nutrient column on the template.

            if (lueFileType.EditValue != null)
            {
                if (txtOrientation.Text == "Landscape")
                {
                    //2/26/18 - commented
                    //if (cbFileHasHeader.Checked == false)
                    //{                            
                    //    MessageBox.Show("Landscape import files must contain a heading row.", "Information");
                    //    txtBeginRow.Text = "1";
                    //    cbFileHasHeader.Checked = true;
                    //}

                    cbFileHasHeader.Checked = true;

                    //2/26/18 commented
                    //if (txtBeginRow.Text == "0" || txtBeginRow.Text == "" || txtBeginRow.Text == null)
                    //{
                    //    MessageBox.Show("Landscape import files must contain a heading row. Column Header row has been changed to 1.", "Information");
                    //    txtBeginRow.Text = "1";
                    //}
                }
            }
		}

		private void btnBrowseSpectra_Click(object sender, EventArgs e)
		{
			try
			{
				if (Directory.Exists(txtSpectraDirectory.Text) == true)
				{
					fbdSelectedDirectory.SelectedPath = txtSpectraDirectory.Text;
				}
				fbdSelectedDirectory.ShowDialog();
				txtSpectraDirectory.Text = fbdSelectedDirectory.SelectedPath;
				SaveUserDefinition();								
				CheckForSpectraFiles();			
				
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void CheckForSpectraFiles()
		{
			//search directory for files with spectra extension									
			if (txtSpectraFileExtension.Text.Length > 0)
			{
				if (txtSpectraDirectory.Text.Length > 0)
				{
					Int32 fCount = Directory.GetFiles(txtSpectraDirectory.Text, "*." + txtSpectraFileExtension.Text).Length;
					if (fCount == 0)
					{
						DialogResult dr = new DialogResult();
						dr = MessageBox.Show("Currently, no files exist in this directory with the extension '" + txtSpectraFileExtension.Text + "'.", "Information...");
					}
				}
			}
		}

		private void btnClearFolders_Click(object sender, EventArgs e)
		{
			DialogResult dr = new DialogResult();		
			dr = MessageBox.Show("Clear folder paths?", "Warning...", MessageBoxButtons.YesNo);
			if (dr == DialogResult.Yes)
			{
				txtProcessPath.Text = "";
				txtBackUpDir.Text = "";
				txtSpectraDirectory.Text = "";
				SaveUserDefinition();	
			}			
		}

		

		private void btnAddNewColumn_Click(object sender, EventArgs e)
		{
			try
			{				
				UserDefinedDataSourceTemplateInfo template = new UserDefinedDataSourceTemplateInfo();
				template.UserDefinedDataSourceTemplateID = 0;
				template.UserDefinedDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;
				template.IsManualEntry = true;
				template.SourceColumnName = "";
				template.MapColumnName = "";
				template.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveUserDefinedDataSourceTemplate(ref template);
				if (error.ErrorNumber > 0)
				{
					return;
				}							
				DataBind();
				
				//grdImportMappingView.Columns["SourceColumnNumber"].Visible = true;		
				grdImportMappingView.FocusedRowHandle = 0;
				grdImportMappingView.FocusedColumn = grdImportMappingView.VisibleColumns[0];
				grdImportMappingView.ShowEditor();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueDelimiterType_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}
		
		private void lueSupplier_Leave(object sender, EventArgs e)
		{
			//12/15/20 - commented
			//lueSupplierLocation.EditValue = lueSupplier.EditValue;
		}

		private void lueSupplierLocation_Leave(object sender, EventArgs e)
		{
			//12/15/20 - commented
			//lueSupplier.EditValue = lueSupplierLocation.EditValue;	
		}

		private void lueSupplier_EditValueChanged(object sender, EventArgs e)
		{
			String msg = String.Empty;
			SaveUserDefinition();
			EnableOrDisableSelectDropdowns(lueSupplier, lueSupplierSelect);

            //12/15/20
            if (lueSupplierLocation.EditValue.ToString() == "") { }
			else
			{
				if ((Int32)lueSupplierLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
				{
					if ((Int32)lueSupplier.EditValue != (Int32)SampleImportUserDefinedMapSelection.SelectNow)
					{
						lueSupplierLocation.EditValue = lueSupplier.EditValue;
					}
				}
			}


        }

        private void lueSupplierSelect_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SaveUserDefinition();
				DataBindSupplierLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSupplierLocationSelect_EditValueChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();			
		}

		private void lueSupplierLocation_EditValueChanged(object sender, EventArgs e)
		{
			SaveUserDefinition();
			EnableOrDisableSelectDropdowns(lueSupplierLocation, lueSupplierLocationSelect);

			//12/15/20
			if ((Int32)lueSupplierLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
			{
				if ((Int32)lueSupplier.EditValue != (Int32)SampleImportUserDefinedMapSelection.SelectNow)
				{
					lueSupplierLocation.EditValue = lueSupplier.EditValue;
				}
			}
		}

		private void btnAddNew_Click(object sender, EventArgs e)
		{			
			try
			{
				//Make sure a data-source exists before user gets into wizard
				List<DataSourceTypeInfo> dataSource = new List<DataSourceTypeInfo>();
				dataSource = AIMManager.GetDataSourceTypeUserDefined(true);
				if (dataSource.Count == 0)
				{										
					MessageBox.Show("Before a template can be added, a data-source must be set up.  Please set up a data-source under Settings > Data Source.", "Warning...");
					return;
				}

				//Make sure a file-type is set up for the data-source before the user gets into wizard
				List<DataSourceTypeRefFileTypeInfo> fileType = new List<DataSourceTypeRefFileTypeInfo>();
				fileType = AIMManager.GetDataSourceTypeRefFileType();
				if (fileType.Count == 0)
				{					
					MessageBox.Show("Before a template can be added, a file type for the data-source must be added.  Please set up a file type under Settings > Data Source, File Types tab.", "Warning...");
					return;
				}
				
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

		private void btnSelectExisting_Click(object sender, EventArgs e)
		{
			try
			{				
				CFCAIM.AIMGlobal.ImportMappingTabControlVisible = false;
				AIMGlobal.lblWizardCaption = "Select Existing Template";
				Close();
				mapWizard = new MapWizard();
				mapWizard.Show();	
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			try
			{

				_isLoading = true;
				lueImportFileName.EditValue = 0;
				DataBindlueTemplateName();
				DataBindlue();
				DataBind();
				DataBindResults();
				ClearTextFields();
				SetLueDefaults();
				SetToolTips();
				btnNext.Visible = false;
				btnPrior.Visible = false;

				_isLoading = false;
				if ((Int32)lueImportFileName.EditValue == 0)
				{
					lueImportFileName.BackColor = Color.Aqua;
				}
				TabControl.Visible = false;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
			

		private void txtSheetNumber_Leave(object sender, EventArgs e)
		{

			Int32 n;
			if (txtSheetNumber.Text == "0")
			{
				txtSheetNumber.Text = "1";
			}
			if (int.TryParse(txtSheetNumber.Text, out n) == false)
			{
				txtSheetNumber.Text = "1";
			}
			SaveUserDefinition();
		}

		private void txtDetailBeginColumn_TextChanged(object sender, EventArgs e)
		{
			Int32 checkInt = 0;
			checkNutrientBeginColumn();			


			if (txtOrientation.Text == "Landscape")						
			{
                if (System.Text.RegularExpressions.Regex.IsMatch(txtDetailBeginColumn.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter only numbers.");
                    txtDetailBeginColumn.Text = txtDetailBeginColumn.Text.Remove(txtDetailBeginColumn.Text.Length - 1);
                }
                else if (txtDetailBeginColumn.Text != "")
                {
                    checkInt = Convert.ToInt32(txtDetailBeginColumn.Text);
                }				
				if (checkInt <= 0)
				{					
					//MessageBox.Show("Please enter number greater than zero.");
					//txtDetailBeginColumn.Text = txtDetailBeginColumn.Text.Remove(txtDetailBeginColumn.Text.Length - 1);
					_landscapeClicked = false;
					
				}
				checkExcelLandscapeHeadingRow();
			}
			checkExcelLandscapeHeadingRow();
		}

		private void txtDetailBeginColumn_Leave(object sender, EventArgs e)
		{
			SaveUserDefinition();
		}

		private void btnDeleteAll_Click(object sender, EventArgs e)
		{
			if (grdImportMappingView.RowCount > 0)
			{

                //3/13/18 added so msg doesn't pop twice ---
                if (_browseButtonClicked == true)
                {
                    _browseButtonClicked = false;
                }
                else
                {
                //------------------------------------------

                    DialogResult dr = new DialogResult();
                    dr = MessageBox.Show("This will CLEAR the current import map.  Do you want to continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }
			}
			AIMManager.DeleteDataSourceUserTemplateUsingDefinitionID((Int32)lueImportFileName.EditValue);
			DataBindTemplate();
		}

		private void lueDataSource_Click(object sender, EventArgs e)
		{
			lueDataSource.SelectAll();
		}

		private void lueDataSource_Enter(object sender, EventArgs e)
		{
			lueDataSource.SelectAll();
		}

		private void lueFileType_Click(object sender, EventArgs e)
		{
			lueFileType.SelectAll();
		}

		private void lueFileType_Enter(object sender, EventArgs e)
		{
			lueFileType.SelectAll();
		}

		private void lueDelimiterType_Enter(object sender, EventArgs e)
		{
			lueDelimiterType.SelectAll();
		}

		private void lueDelimiterType_Click(object sender, EventArgs e)
		{
			lueDelimiterType.SelectAll();
		}

		private void lueOwner_Click(object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}

		private void lueOwner_Enter(object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}

		private void lueOwnerLocation_Click(object sender, EventArgs e)
		{
			lueOwnerLocation.SelectAll();
		}

		private void lueOwnerLocation_Enter(object sender, EventArgs e)
		{
			lueOwnerLocation.SelectAll();
		}

		private void lueSupplier_Enter(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}

		private void lueSupplier_Click(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}

		private void lueSupplierLocation_Click(object sender, EventArgs e)
		{
			lueSupplierLocation.SelectAll();
		}

		private void lueSupplierLocation_Enter(object sender, EventArgs e)
		{
			lueSupplierLocation.SelectAll();
		}
        		
		private void rbMerge_CheckedChanged(object sender, EventArgs e)
		{
            //12/3/19
            if (rbMerge.Checked == true)
            {
                pnlAllowDuplicateSampleCodes.Enabled = false;
                rbSystemCreatedSampleCodeNo.Checked = true;
                rbAllowDuplicateSampleCodesNo.Checked = true;
                pnlRequiredFields.Enabled = false;                           //12/5/19
            }
            
            SaveUserDefinition();
		}

		private void rbCreate_CheckedChanged(object sender, EventArgs e)
		{

            //12/3/19
            if (rbCreate.Checked == true)
            {
                pnlAllowDuplicateSampleCodes.Enabled = true;
            }
            pnlRequiredFields.Enabled = true;                           //12/5/19
            rbQualityControlNo.Checked = true;                                                                                                  //3/15/18
            SaveUserDefinition();
		}

		private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
		{

			Font _TabFont = new Font(e.Font.FontFamily, (float)9, FontStyle.Bold, GraphicsUnit.Pixel);
			Brush _TextBrush = new SolidBrush(Color.DarkBlue);
			Rectangle _TabBounds = TabControl.GetTabRect(e.Index);
			StringFormat _StringFlags = new StringFormat();
			_StringFlags.Alignment = StringAlignment.Center;
			_StringFlags.LineAlignment = StringAlignment.Center;


			
			switch (e.Index)
			{
				case 0:
					e.Graphics.FillRectangle(new SolidBrush(Color.AliceBlue), e.Bounds);					
					break;
				case 1:
					e.Graphics.FillRectangle(new SolidBrush(Color.AliceBlue), e.Bounds);
					////e.Graphics.DrawString(TabControl.TabPages[e.Index].Text, _TabFont, _TextBrush, _TabBounds, new StringFormat(_StringFlags));					
					break;
				case 2:
					e.Graphics.FillRectangle(new SolidBrush(Color.AliceBlue), e.Bounds);
					break;
				default:
					e.Graphics.FillRectangle(new SolidBrush(Color.Transparent), e.Bounds);
					break;
			}			
			Rectangle paddedBounds = e.Bounds;
			int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
			paddedBounds.Offset(1, yOffset);
			TextRenderer.DrawText(e.Graphics, TabControl.TabPages[e.Index].Text, Font, paddedBounds, TabControl.TabPages[e.Index].ForeColor);

		}

		private void btnSave_Click(object sender, EventArgs e)
		{

            //4/2/19
            if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.CSV || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.TXT)
            {
                if ((Int32)lueDelimiterType.EditValue == 0 || (Int32)lueDecimalSeparator.EditValue == 0)
                {
                    MessageBox.Show("Please select delimiter and decimal separator.", "Error...", MessageBoxButtons.OK);
                    return;
                }
            }

            SaveUserDefinition();
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			if (TabControl.SelectedIndex == 0)
			{
				TabControl.SelectedIndex = 1;
			}
			else if (TabControl.SelectedIndex == 1)
			{
				TabControl.SelectedIndex = 2;
			}
		}

		private void btnPrior_Click(object sender, EventArgs e)
		{
			if (TabControl.SelectedIndex == 2)
			{
				TabControl.SelectedIndex = 1;
			}
			else if (TabControl.SelectedIndex == 1)
			{
				TabControl.SelectedIndex = 0;
			}
		}
                

        private void ImportMapping_FormClosing(Object sender, FormClosingEventArgs e)
		{            
            if ((Int32)lueImportFileName.EditValue != 0)
			{
				AIMGlobal.analyzerDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;                
            }        

            ImportDataUserDefined importDataUserDefined;
			AIMGlobal.open_form(importDataUserDefined = new ImportDataUserDefined());

        }

		private void rbYesMulti_CheckedChanged(Object sender, EventArgs e)
		{
			SaveUserDefinition();
			PopulateMapColumns();
		}
        		
        private void lueDateFormat_EditValueChanged(Object sender, EventArgs e)
		{
			SaveUserDefinition();
		}

		private void lueDateFormat_Leave(Object sender, EventArgs e)
		{			
			SaveUserDefinition();
		}

		private void lueDateFormat_Enter(Object sender, EventArgs e)
		{
			lueDateFormat.SelectAll();
		}

		private void lueDateFormat_Click(Object sender, EventArgs e)
		{
			lueDateFormat.SelectAll();
		}
        
        private void btnResize_Click(Object sender, EventArgs e)
        {         
            WindowState = FormWindowState.Normal;
            btnResize.Visible = false;
        }

		private void ImportMapping_SizeChanged(Object sender, EventArgs e)
		{			
			btnResize.Visible = true;
		}

        private void btnHelpImportType_Click(Object sender, EventArgs e)
        {
                       
            Imports.ImportHelpType importHelpType = new Imports.ImportHelpType();
            importHelpType.ShowDialog();
                       
        }

        //1/29/18
        private void rbCreateOrMerge_CheckedChanged(Object sender, EventArgs e)
        {

            //12/3/19
            if (rbCreateOrMerge.Checked == true)
            {
                pnlAllowDuplicateSampleCodes.Enabled = true;
            }
            pnlRequiredFields.Enabled = true;                           //12/5/19
            rbQualityControlNo.Checked = true;                                                                                                   //3/15/18
            SaveUserDefinition();
        }


        //3/15/18
        private void rbQualityControlYes_CheckedChanged(Object sender, EventArgs e)
        {

			//Task916 3/4/24
			if (_isLoading == false)
			{
				if (rbQualityControlYes.Checked == false)
				{					
					MessageBox.Show("After changing the Quality Control Results, it is advisable to check the File Type and the Import Type.", "Information...", MessageBoxButtons.OK);
				}
			}

            SaveUserDefinition();

			if (rbQualityControlYes.Checked == true)					//Task916 3/4/24 (was changing file-type and results-only for both yes and no!
			{

				//Task916 3/4/24
				if (_isLoading == false)
				{				
					MessageBox.Show("When Quality Control Results is set to Yes, the File Type changes to XLS and the Import Type changes to Update Results.", "Information...", MessageBoxButtons.OK);
				}

				//Force Excel and Results Only
				lueFileType.EditValue = (Int32)FileTypeEnum.XLS;
				rbMerge.Checked = true;
			}                                                           //Task916 3/4/24

			SaveUserDefinition();
        }
                
        private void rbQualityControlNo_CheckedChanged(Object sender, EventArgs e)                                                              //3/15/18
        {                                                                                                                                       //3/15/18
            SaveUserDefinition();                                                                                                               //3/15/18
        }                                                                                                                                       //3/15/18

        private void txtSheetNumber_TextChanged(Object sender, EventArgs e)
        {

        }

        
        private void repItemCheckEditUseAsSampleCode_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        //12/6/18
        private void repItemCheckEditUseAsSampleCode_Leave(object sender, EventArgs e)
        {
            _useAsSampleCode = false;            
            if (Convert.ToBoolean(repItemCheckEditUseAsSampleCode.ValueChecked) == true)
            {
                _useAsSampleCode = true;
            }
        }

        //12/7/18
        private void grdImportMappingView_ShownEditor(object sender, EventArgs e)
        {
            if (grdImportMappingView.FocusedColumn.FieldName == "UseAsSampleCode")
            {
                grdImportMappingView.ActiveEditor.EditValueChanged += new EventHandler(ActiveEditor_EditValueChanged);
            }
        }

        //12/7/18
        void ActiveEditor_EditValueChanged(object sender, EventArgs e)
        {
            grdImportMappingView.CloseEditor();
        }

        //12/7/18
        private void grdImportMappingView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.FieldName)
            {
                case "UseAsSampleCode": ProcessUseAsSampleCode(e.RowHandle); break;              
            }
        }
        
        //12/7/18
        public void ProcessUseAsSampleCode(int CurrentRow)
        {
            Int32 ID = 0;
           
            UserDefinedDataSourceTemplateInfo template = new UserDefinedDataSourceTemplateInfo();
            template = (UserDefinedDataSourceTemplateInfo)grdImportMappingView.GetFocusedRow();
            _mapToThisColumn = template.MapColumnName;            

            if (CurrentRow == _PreviouslySelectedUseAsSampleCode) return;
                       
            ID = (Int32)(grdImportMappingView.GetRowCellValue(_PreviouslySelectedUseAsSampleCode, grdImportMappingView.Columns["UserDefinedDataSourceTemplateID"]));            
            grdImportMappingView.SetRowCellValue(_PreviouslySelectedUseAsSampleCode, grdImportMappingView.Columns["UseAsSampleCode"], false);

            //Save To database
            grdImportMappingUseAsSampleCodeSave(ID, false);
                        
            _PreviouslySelectedUseAsSampleCode = CurrentRow;

            //Save To database
            ID = (Int32)(grdImportMappingView.GetRowCellValue(CurrentRow, grdImportMappingView.Columns["UserDefinedDataSourceTemplateID"]));           
            grdImportMappingUseAsSampleCodeSave(ID, true);
        }

        //12/6/18
        private void grdImportMappingUseAsSampleCodeSave(Int32 templateID, Boolean value)
        {
            UserDefinedDataSourceTemplateInfo userTemp = new UserDefinedDataSourceTemplateInfo();
            userTemp.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveUseAsSampleCode(templateID, value, userGlobal.CFCLoginUser.CFCUserID);
        }
               
        //12/12/18
        private void btnDoNotUse_Click(object sender, EventArgs e)
        {
            ErrorInfo result = new ErrorInfo();
            DialogResult dr = new DialogResult();

            if (lueImportFileName.EditValue == null)
            {
                return;
            }

            if (_showDoNotUse == true)
            {
                dr = MessageBox.Show("Set all AIM columns to DO NOT USE?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
                result = AIMManager.SaveUserDefinedDataSourceTemplate(_selectedDefinition.UserDefinedDataSourceDefinitionID, 1, "DO NOT USE", userGlobal.CFCLoginUser.CFCUserID);
                _showDoNotUse = false;
                btnDoNotUse.Text = "Clear AIM Columns";
                
            }
            else
            {
                dr = MessageBox.Show("Clear all AIM Columns?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
                result = AIMManager.SaveUserDefinedDataSourceTemplate(_selectedDefinition.UserDefinedDataSourceDefinitionID, 0, "--Select--", userGlobal.CFCLoginUser.CFCUserID);
                _showDoNotUse = true;                
                btnDoNotUse.Text = "Set All to" + Environment.NewLine + "DO NOT USE";
            }                       
            DataBind();
            
            
        }

        //4/1/19
        private void rbResultsAndHeader_CheckedChanged(object sender, EventArgs e)
        {

            //12/3/19
            if (rbResultsAndHeader.Checked == true)
            {
                pnlAllowDuplicateSampleCodes.Enabled = false;
                rbSystemCreatedSampleCodeNo.Checked = true;
                rbAllowDuplicateSampleCodesNo.Checked = true;
                pnlRequiredFields.Enabled = false;                           //12/5/19
            }

            SaveUserDefinition();
        }

        //4/1/19
        private void lueDecimalSeparator_EditValueChanged(object sender, EventArgs e)
        {
            SaveUserDefinition();
        }

        //4/1/19
        private void lueDecimalSeparator_Leave(object sender, EventArgs e)
        {
            SaveUserDefinition();
        }

        //4/1/19
        private void lueDecimalSeparator_Enter(object sender, EventArgs e)
        {
            lueDecimalSeparator.SelectAll();
        }

        //4/1/19
        private void lueDecimalSeparator_Click(object sender, EventArgs e)
        {
            lueDecimalSeparator.SelectAll();
        }

        private void btnHelpMapping_Click(object sender, EventArgs e)
        {
            Imports.ImportMappingHelp importHelpMapping = new Imports.ImportMappingHelp();
            importHelpMapping.ShowDialog();
        }
        
        //8/23/19
        private void txtBeginDetailRow_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtDetailBeginRow.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtDetailBeginRow.Text = txtDetailBeginRow.Text.Remove(txtDetailBeginRow.Text.Length - 1);
            }
            else if (txtDetailBeginRow.Text == "0" || txtDetailBeginRow.Text == "" || txtDetailBeginRow.Text == null)
            {
                txtDetailBeginRow.Text = "1";
            }
			
        }

		//3/20/25
		private void clearTemplateDef()
		{
			AIMManager.DeleteDataSourceUserTemplateUsingDefinitionID(_selectedDefinitionID);
        }

        //8/23/19
        private void txtBeginDetailRow_Leave(object sender, EventArgs e)
        {
            SaveUserDefinition();

            //3/20/25
            clearTemplateDef();
        }
        
        //12/2/19
        private void rbAllowDuplicateSampleCodesYes_CheckedChanged(object sender, EventArgs e)
        {

            //12/3/19
            if (rbAllowDuplicateSampleCodesYes.Checked == true)
            {
                rbSystemCreatedSampleCodeNo.Checked = true;
            }
            
            SaveUserDefinition();            
        }

        //12/2/19
        private void rbAllowDuplicateSampleCodesNo_CheckedChanged(object sender, EventArgs e)
        {
            SaveUserDefinition();
        }

        //12/2/19
        private void rbSystemCreatedSampleCodeYes_CheckedChanged(object sender, EventArgs e)
        {
            //12/3/19
            if (rbSystemCreatedSampleCodeYes.Checked == true)
            {
                rbAllowDuplicateSampleCodesNo.Checked = true;
                lblSystemSampleCodes.Visible = true;
            }

            SaveUserDefinition();
            PopulateMapColumns();
            
        }

        //12/2/19
        private void rbSystemCreatedSampleCodeNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSystemCreatedSampleCodeNo.Checked == true)
            {
                lblSystemSampleCodes.Visible = false;
            }

            SaveUserDefinition();
            PopulateMapColumns();

        }

        private void rbUpdateHeaderOnly_CheckedChanged(object sender, EventArgs e)
        {
            //12/3/19
            if (rbUpdateHeaderOnly.Checked == true)
            {
                pnlAllowDuplicateSampleCodes.Enabled = false;
                rbSystemCreatedSampleCodeNo.Checked = true;
                rbAllowDuplicateSampleCodesNo.Checked = true;
                pnlRequiredFields.Enabled = false;                           //12/5/19
            }

            SaveUserDefinition();
        }

		//12/29/20
		private void lueOwner_Validating(object sender, CancelEventArgs e)
		{
			CheckMappingForColumn(sender, e, lueOwner, "Owner");
		}

		//12/29/20
		private void lueOwnerLocation_Validating(object sender, CancelEventArgs e)
        {			
			CheckMappingForColumn(sender, e, lueOwnerLocation, "OwnerLocation");									
		}

		//12/29/20
		private void lueSupplier_Validating(object sender, CancelEventArgs e)
		{
			CheckMappingForColumn(sender, e, lueSupplier, "Supplier");
			CheckMappingForColumn(sender, e, lueSupplier, "SupplierLocationCombined");
		}

		//12/29/20
		private void lueSupplierLocation_Validating(object sender, CancelEventArgs e)
        {
			CheckMappingForColumn(sender, e, lueSupplierLocation, "SupplierLocation");
			CheckMappingForColumn(sender, e, lueSupplierLocation, "SupplierLocationCombined");
		}		

		//12/29/20
		private void CheckMappingForColumn(object sender, CancelEventArgs e, DevExpress.XtraEditors.LookUpEdit lue, String column)
		{
			try
			{
				if ((Int32)lue.EditValue != (Int32)SampleImportUserDefinedMapSelection.UseMapping)
				{

					//First check to see if column-name from the lue is in template map.
					//If it is, and the EditValue != UseMapping, then pop message to user, 
					//that the mapping must first be changed before the dropdown can be changed.
					//The mapping needs to be changed to "Do Not Use". 
					//And keep the lue old value.

					String msg = String.Empty;					
					
					UserDefinedDataSourceTemplateInfo temp = new UserDefinedDataSourceTemplateInfo();
					temp = AIMManager.GetUserDefinedDataSourceTemplateMapColumnNameTop(_selectedDefinitionID, column);
					if (temp != null)
					{
						msg = column.ToUpper() + " is selected in the mapping.";
						msg += Environment.NewLine + Environment.NewLine + "Please change the mapping (to DO NOT USE)";
						msg += Environment.NewLine + " before changing the value here.";
						MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
						e.Cancel = true;
						SendKeys.Send("{ESC}");
						SendKeys.Send("{ESC}");
						return;

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//1/5/21
        private void replueMapColumnName_PopupFilter(object sender, DevExpress.XtraEditors.Controls.PopupFilterEventArgs e)
        {
			LookUpEdit lookUpEdit = sender as LookUpEdit;
			if (lookUpEdit.EditValue == null) return;
			e.Criteria = CriteriaOperator.Parse($"MapColumnID != '{lookUpEdit.EditValue}'");
		}

	}
}
