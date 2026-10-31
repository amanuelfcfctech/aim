using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


using Core.Library.DevExpress;

using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraEditors;

namespace CFCAIM.Imports
{
    public partial class MapWizard : Form
	{
		List<AIMMaterialRefAnalyzerMaterialInfo> _aimMaterial = new List<AIMMaterialRefAnalyzerMaterialInfo>();
		List<AIMSupplierRefAnalyzerSupplierInfo> _aimsupplier = new List<AIMSupplierRefAnalyzerSupplierInfo>();
		List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> _aimsupplierLocation = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
		List<AIMOwnerRefAnalyzerOwnerInfo> _aimOwner = new List<AIMOwnerRefAnalyzerOwnerInfo>();
		List<SampleImportUserDefinedMapSelectionInfo> _mapSelection = new List<SampleImportUserDefinedMapSelectionInfo>();

		CFCUser.Global userGlobal = new CFCUser.Global();
		Imports.ImportHelp importHelp;
        Imports.ImportHelpType importHelpType;      //12/2/19
        Int32 _currentPage = 0;
		Boolean _askAboutDuplicateFileName = true;

		//8/15/22
		Int32 _importType = 1;

        		
			
		public MapWizard()
		{
			InitializeComponent();
		}
		
		private void MapWizard_Load(object sender, EventArgs e)
		{

			checkedListBoxControl1.Visible = true;
			lblHeading.Text = AIMGlobal.lblWizardCaption;
			if (AIMGlobal.lblWizardCaption == "Select Existing Template")
			{
				checkedListBoxControl1.Visible = false;
				DataBindlueImportName();

				//List<UserDefinedDataSourceDefinitionInfo> templateName = new List<UserDefinedDataSourceDefinitionInfo>();
				//templateName = (List<UserDefinedDataSourceDefinitionInfo>)lueImportFileName.Properties.DataSource;
				//if (templateName.Count == 2)
				//{
				//	//selecting value if there is only one
				//	lueImportFileName.EditValue = templateName[1].UserDefinedDataSourceDefinitionID;
				//}

				_currentPage = 8;

			}					
			
			ShowHideTabs(_currentPage);
			DataBindlue();			
			btnPortrait.BackColor = Color.Transparent;
			btnLandscape.BackColor = Color.Transparent;
			SetToolTips();
			SetLueDefaults();
			ClearTextFields();			
			EnableDisableCheckBoxList();
			
			if (_currentPage == 0)
			{
				txtImportFileName.Focus();
				btnBack.TabStop = false;
				btnCancel.TabStop = false;
				SendKeys.Send("{TAB}");				
			}
			else
			{
				btnBack.TabStop = true;
				btnCancel.TabStop = true;
			}
			
		}
		private void DataBindlueImportName()
		{
			List<UserDefinedDataSourceDefinitionInfo> templateName = new List<UserDefinedDataSourceDefinitionInfo>();
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
			lueImportFileName.Properties.DataSource = templateName;
			XtraEditors.LoadLookUpEdit(ref lueImportFileName, "UserDefinedDataSourceDefinitionID", "ImportFileName");
			
		}
		private void DataBindlue()
		{
			List<DataSourceTypeInfo> dataSource = new List<DataSourceTypeInfo>();

            //3/29/17 - user-defined imports can use any datasource
            //dataSource = AIMManager.GetDataSourceTypeUserDefined(true);
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
			lueDataSource.Properties.DataSource = dataSource;
			XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name");
			if (dataSource.Count == 2)
			{
				lueDataSource.EditValue = dataSource[1].DataSourceTypeID;
			}
			else 
			{
				lueDataSource.EditValue = 0;
			}

			List<DelimiterTypeInfo> delimiterType = new List<DelimiterTypeInfo>();
			delimiterType = AIMManager.GetDelimiterType(true);
			delimiterType.Insert
			(
				0,
				new DelimiterTypeInfo
				{
					DelimiterTypeID = 0,
					Description = "--Select--"
				}
			);
			lueDelimiterType.Properties.DataSource = delimiterType;
			XtraEditors.LoadLookUpEdit(ref lueDelimiterType, "DelimiterTypeID", "Description");
			if (delimiterType.Count == 2)
			{
				lueDelimiterType.EditValue = delimiterType[1].DelimiterTypeID;
			}
			else
			{
				lueDelimiterType.EditValue = 0;
			}

            //4/1/19
            List<DecimalSeparatorInfo> decimalSeparator = new List<DecimalSeparatorInfo>();
            decimalSeparator = AIMManager.GetDecimalSeparator(true);            
            lueDecimalSeparator.Properties.DataSource = decimalSeparator;
            XtraEditors.LoadLookUpEdit(ref lueDecimalSeparator, "DecimalSeparatorID", "Description");            
            lueDecimalSeparator.EditValue = 2;  //default the decimal separator to "period"
            
            List<DateFormatInfo> dateFormat = new List<DateFormatInfo>();
			dateFormat = AIMManager.GetDateFormat();												
			lueDateFormat.Properties.DataSource = dateFormat;
			XtraEditors.LoadLookUpEdit(ref lueDateFormat, "DateFormatID", "DateFormat");
			lueDateFormat.EditValue = dateFormat[0].DateFormatID;
			

			_mapSelection = AIMManager.GetMapSelection(true);

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
						
			DataBindLueAIM();


		}
		private void DataBindFileTypelue()
		{
			List<DataSourceTypeRefFileTypeInfo> fileType = new List<DataSourceTypeRefFileTypeInfo>();
			fileType = AIMManager.GetDataSourceTypeRefFileType((Int32)lueDataSource.EditValue);
			fileType.Insert
			(
				0,
				new DataSourceTypeRefFileTypeInfo
				{
					FileTypeID = 0,
					FileTypeName = "--Select--"
				}
			);
			lueFileType.Properties.DataSource = fileType;
			XtraEditors.LoadLookUpEdit(ref lueFileType, "FileTypeID", "FileTypeName");
			if (fileType.Count == 2)
			{
				lueFileType.EditValue = fileType[1].FileTypeID;
			}
			else
			{
				lueFileType.EditValue = 0;
			}

		}
		private void ShowHideTabs(Int32 currentPage)
		{
			tabControl1.ItemSize = new Size(0,1);
			tabControl1.SelectedIndex = currentPage;	
		}

		private void SetToolTips()
		{
			try
			{
				this.Icon = (Icon)Properties.Resources.CFCAIM;
				ToolTip ttip = new ToolTip();
				ttip.SetToolTip(btnBack, "Return to previous screen");
				ttip.SetToolTip(btnPortrait, "Portrait");
				ttip.SetToolTip(btnLandscape, "Landscape");
				ttip.SetToolTip(btnCancel, "Cancel Wizard");
				ttip.SetToolTip(btnNext, "Advance to next screen");

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void txtImportFileName_Leave(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if (txtImportFileName.Text.Length > 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void lueDataSource_Leave(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueDataSource.EditValue != 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void txtImportFileName_Leave_1(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if (txtImportFileName.Text.Length > 0)
			{
				btnNext.Enabled = true;
			}

			//check to see if template name already exists
			if (_askAboutDuplicateFileName == true)
			{
				List<UserDefinedDataSourceDefinitionInfo> def = new List<UserDefinedDataSourceDefinitionInfo>();
				def = AIMManager.GetUserDefinedDefinitionImportFileName(txtImportFileName.Text);
				if (def.Count != 0)
				{												
					DialogResult dr = new DialogResult();
					dr = MessageBox.Show("This file name already exists for another template. Continue? ", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)
					{
						txtImportFileName.Text = "";
						txtImportFileName.Focus();
						return;
					}
					_askAboutDuplicateFileName = false;
				}
			}


		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			try 
			{                
                if (_currentPage == 0 || _currentPage == 9)

                    {
					if (lueImportFileName.EditValue != null)
					{
						if ((Int32)lueImportFileName.EditValue != 0)
						{
							CFCAIM.AIMGlobal.ImportMappingTabControlVisible = true;
						}
					}									
					Close();

					//6/3/16 - added because ShowDialog() on ImportMapping wasn't working correctly.					
					//		   Changed to close the Import Mapping form and open the wizard instead (instead of using ShowDialog).
                    					
					ImportDataUserDefined importDataUserDefined;
					AIMGlobal.open_form(importDataUserDefined = new ImportDataUserDefined());

                }

                if (_currentPage == 7 && rbUpdateHeaderOnly.Checked == true)            //7/26/19
                {                                                                       //7/26/19
                    _currentPage = 4;                                                   //7/26/19
                }                                                                       //7/26/19    

                if (_currentPage == 8 && rbUpdateHeaderOnly.Checked == true)            //7/26/19
                {                                                                       //7/26/19
                    _currentPage = 5;                                                   //7/26/19            
                }                                                                       //7/26/19

				if (_currentPage == 7)
                {                    
                    if (rbMerge.Checked == true || rbResultsAndHeader.Checked == true)
                    {
                        _currentPage--;
                        ShowHideTabs(_currentPage);
                    }
                } 

                _currentPage--;
				ShowHideTabs(_currentPage);
                				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				Close();

				//6/3/16 - added because ShowDialog() on ImportMapping wasn't working correctly.					
				//		   Changed to close the Import Mapping form and open the wizard instead (instead of using ShowDialog).					

				//10/12/16
				//ImportMapping importMapping;
				//AIMGlobal.open_form(importMapping = new ImportMapping());
				ImportDataUserDefined importDataUserDefined;
				AIMGlobal.open_form(importDataUserDefined = new ImportDataUserDefined());

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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
					definition.UserDefinedDataSourceDefinitionID = (Int32)lueImportFileName.EditValue;
					definition.ImportFileName = txtImportFileName.Text.ToString();
					definition.DataSourceTypeID = (Int32)lueDataSource.EditValue;
					definition.FilePath = "";
					definition.ImportFileTypeID = (Int32)lueFileType.EditValue;
					definition.DelimiterTypeID = (Int32)lueDelimiterType.EditValue;
                                        
                    // 1 or true = landscape; 0 or false = portrait
                    if (txtOrientation.Text == "Landscape")
                    {
                        definition.Orientation = true;
                    }
                    else
                    {
                        definition.Orientation = false;
                    }

                    definition.FileHasHeader = cbFileHasHeader.Checked;
                    definition.HeaderBeginRow = Int32.Parse(txtBeginRow.Text);
                    if (txtDetailBeginColumn.Text == "")
                    {
                        definition.NutrientDetailBeginColumn = 0;
                    }
                    else
                    {
                        definition.NutrientDetailBeginColumn = Int32.Parse(txtDetailBeginColumn.Text);
                    }

					definition.IsSpectraFile = cbSpectraFile.Checked;
					definition.AIMMaterialMap = (Int32)lueMaterial.EditValue;
					definition.AIMSourceMap = (Int32)lueSupplier.EditValue;
					definition.AIMSourceLocationMap = (Int32)lueSupplierLocation.EditValue;
					definition.AIMOwnerMap = (Int32)lueOwner.EditValue;
					definition.AIMOwnerLocationMap = (Int32)lueOwnerLocation.EditValue;
					definition.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					definition.ProcessDirectory = txtProcessPath.Text;
					definition.BackupDirectory = txtBackUpDir.Text;
					definition.SpectraDirectory = txtSpectraDirectory.Text;					
					definition.SpectraFileExtension = txtSpectraFileExtension.Text;
					definition.ExcelSheetNumber = Int32.Parse(txtSheetNumber.Text);																												
					definition.DataSourceIsMapped = false;                    
                    definition.DateFormatID = (Int32)lueDateFormat.EditValue;


					//----------------------------------------------------------   
					//8/12/22					
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						definition.ImportType = (Int32)EnumImportType.RomerResults;

						//5/1/23
						List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
						owner = AIMManager.GetAIMOwner(true);
						if (owner.Count == 1)	
						{
							definition.AIMOwnerID = owner[0].AIMOwnerID;
						}
						
					}
					//----------------------------------------------------------   

					else if (rbMerge.Checked == true)
                    {
                        definition.ImportType = (Int32)EnumImportType.UpdateResultsOnly;                 
                    }                    
                    else if (rbResultsAndHeader.Checked == true)
                    {
                        definition.ImportType = (Int32)EnumImportType.ForExistingSamplesUpdateResultsAndHeaderFields;        
                    }
                    else if (rbCreate.Checked == true)
                    {
                        definition.ImportType = (Int32)EnumImportType.AddNewSamplesAndResults;           
                    }
                    else if (rbUpdateHeaderOnly.Checked == true)
                    {
                        definition.ImportType = (Int32)EnumImportType.UpdateHeaderOnly;              
                    }
                    else 
                    {
                        definition.ImportType = (Int32)EnumImportType.AddNewSamplesAndResultsORUpdateResults;                
                    }       
                    //----------------------------------------------------------

                    //3/15/18
                    if (rbQualityControlYes.Checked == true)
                    {
                        definition.IsQualityControl = true;
                    }
                    else
                    {
                        definition.IsQualityControl = false;
                    }

                    //4/1/19                    
                    definition.DecimalSeparatorID = (Int32)lueDecimalSeparator.EditValue;

                    //8/23/19
                    definition.DetailBeginRow = Int32.Parse(txtBeginDetailRow.Text);

                    if (rbQualityControlYes.Checked == true)
                    {
                        definition.IsQualityControl = true;
                    }
                    else
                    {
                        definition.IsQualityControl = false;
                    }


                    //12/2/19
                    if (rbAllowDupsYes.Checked == true)
                    {
                        definition.AllowDuplicateSampleCodes = true;
                    }
                    else
                    {
                        definition.AllowDuplicateSampleCodes = false;
                    }

                    //12/2/19
                    if (rbSystemSampleCodeYes.Checked == true)
                    {
                        definition.SystemCreatedSampleCode = true;
                    }
                    else
                    {
                        definition.SystemCreatedSampleCode = false;
                    }


					//8/12/22
					if (rbCompositeYes.Checked == true)
					{
						definition.IsCompositeBatch = true;
					}
					else
					{
						definition.IsCompositeBatch = false;
					}

                    
                    ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveDataSourceUserDefinition(ref definition);
					if (error.ErrorNumber > 0)
					{
						return;
					}
					AIMGlobal.analyzerDataSourceDefinitionID = error.RecordID;

					//8/15/22 - For Romer Lab imports, auto create column mapping (so user doesn't have to)
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						AIMManager.RomerImportInsertTemplateRecords(error.RecordID, definition.IsCompositeBatch);
					}	
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

		private void btnNext_Click(object sender, EventArgs e)
		{
                      
			if (_currentPage == 8)  //Finish
			{
				lueImportFileName.EditValue = 0;
				SaveUserDefinition();    //SaveUserDefinition sets global template def-id

				


				CFCAIM.AIMGlobal.ImportMappingTabControlVisible = true;
				Close();

				//6/3/16 - added because ShowDialog() on ImportMapping wasn't working correctly.					
				//		   Changed to close the Import Mapping form and open the wizard instead (instead of using ShowDialog).					

				ImportMapping importMapping;
				AIMGlobal.open_form(importMapping = new ImportMapping());

            }
			else if (_currentPage == 9)  //Existing
			{
				Int32 defID = 0;
				if (lueImportFileName.EditValue != null)
				{
					Int32.TryParse(lueImportFileName.EditValue.ToString(), out defID);
					AIMGlobal.analyzerDataSourceDefinitionID = defID;					
					CFCAIM.AIMGlobal.ImportMappingTabControlVisible = true;
					Close();

					//8/19/22
					AIMGlobal.romerImport = false;
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						AIMGlobal.romerImport = true;
					}


					//6/3/16 - added because ShowDialog() on ImportMapping was working correctly.					
					//		   Changed to close the Import Mapping form and open the wizard instead (instead of using ShowDialog).					

					ImportMapping importMapping;
					AIMGlobal.open_form(importMapping = new ImportMapping());

                }
			}
                        
            else if(_currentPage == 5) //File Type
            {
                //4/1/19
                //if (rbMerge.Checked == true)
                if (rbMerge.Checked == true || rbResultsAndHeader.Checked == true)
                    {
                    _currentPage++;  //skip AIM Required Fields page
                    ShowHideTabs(_currentPage);                    
                }                
                _currentPage++;
                ShowHideTabs(_currentPage);
            }          

			else
			{
				_currentPage++;
				ShowHideTabs(_currentPage);
			}



		}

		private void lueDataSource_EditValueChanged(object sender, EventArgs e)
		{
			//data bind the lueFileType for this data source typeID
			DataBindFileTypelue();

			btnNext.Enabled = false;

			//-----------------------------------------------------------------------
			//8/15/22
			if ((Int32)lueDataSource.EditValue == (Int32)DataSourceTypeEnum.Romer)
			{
				_importType = (Int32)EnumImportType.RomerResults;
			}

			//-----------------------------------------------------------------------
			if ((Int32)lueDataSource.EditValue != 0)
			{
				btnNext.Enabled = true;
			}

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
			}
		}


		private void DataBindLueAIM()
		{
			//-----------------------------------------------------------------
			//Dropdowns to select a specific CROSS-REFERENCED material, owner, supplier, etc.
			//-----------------------------------------------------------------
			DataBindLueMaterial();

			_aimOwner = AIMManager.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeID((Int32)lueDataSource.EditValue);
			_aimOwner.Insert
			(
				0,
				new AIMOwnerRefAnalyzerOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--"
				}
			);
		
			//-----------------------------------------------------------------
			_aimsupplier = AIMManager.GetAIMSupplierRefAnalyzerSupplierDataSourceTypeID((Int32)lueDataSource.EditValue);
			_aimsupplier.Insert
			(
				0,
				new AIMSupplierRefAnalyzerSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierName = "--Select--"
				}
			);
			
		}
		////private void DataBindSupplierLocation()
		////{
		////	Int32 supplierLocationID = 0;
		////	Int32 supplierID = 0;
		////	if (lueSupplierLocationSelect.EditValue != null)
		////	{
		////		Int32.TryParse(lueSupplierSelect.EditValue.ToString(), out supplierID);
		////	}
		////	_aimsupplierLocation = AIMManager.GetUserDefinedCrossRefAimSupplierLocationDataSourceTypeID((Int32)lueDataSource.EditValue);

		////	_aimsupplierLocation.Insert
		////	(
		////		0,
		////		new UserDefinedCrossRefAIMSupplierLocationInfo
		////		{
		////			AIMSupplierLocationID = 0,
		////			SupplierLocationName = "--Select--"
		////		}
		////	);
		////	lueSupplierLocationSelect.Properties.DataSource = _aimsupplierLocation;
		////	XtraEditors.LoadLookUpEdit(ref lueSupplierLocationSelect, "AIMSupplierLocationID", "SupplierLocationName");
		////	lueSupplierLocationSelect.Properties.Columns["SupplierLocationName"].Visible = true;
		////	lueSupplierLocationSelect.EditValue = supplierLocationID;
		////}

		//////private void DataBindOwnerLocation()
		//////{
		//////	Int32 OwnerLocationID = 0;
		//////	Int32 dataSourceTypeID = 0;
		//////	if (lueDataSource.EditValue != null)
		//////	{
		//////		Int32.TryParse(lueDataSource.EditValue.ToString(), out dataSourceTypeID);
		//////	}
		//////	_aimOwnerLocation = AIMManager.GetUserDefinedCrossRefAimOwnerLocationDataSourceTypeID(dataSourceTypeID);
		//////	_aimOwnerLocation.Insert
		//////	(
		//////		0,
		//////		new UserDefinedCrossRefAIMOwnerLocationInfo
		//////		{
		//////			AIMOwnerLocationID = 0,
		//////			LocationCode = "--Select--"
		//////		}
		//////	);
		//////	lueOwnerLocationSelect.Properties.DataSource = _aimOwnerLocation;
		//////	XtraEditors.LoadLookUpEdit(ref lueOwnerLocationSelect, "AIMOwnerLocationID", "LocationCode");
		//////	lueOwnerLocationSelect.Properties.Columns["LocationCode"].Visible = true;
		//////	lueOwnerLocationSelect.EditValue = OwnerLocationID;

		//////}

		private void lueFileType_EditValueChanged(object sender, EventArgs e)
		{
			
			lblDelimiterType.Visible = false;
			lueDelimiterType.Visible = false;
			lblDelimiterHelp.Visible = false;
            lblSheetNumber.Visible = false;            
            txtSheetNumber.Visible = false;
            lblLine3.Visible = false;
            lblRequired.Visible = false;

            //4/1/19
            lblDecimalSeparator.Visible = false;
            lueDecimalSeparator.Visible = false;


            //3/29/19 - changed to allow delimiter type on CSV files also  (found with Vitapro)
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.CSV)
            //{
            //    lueDelimiterType.EditValue = (Int32)DelimiterTypeEnum.Comma;
            //}

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Text || (Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.CSV)
            if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.TXT || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.CSV)

            {
				lblDelimiterType.Visible = true;
				lueDelimiterType.Visible = true;
				lblDelimiterHelp.Visible = true;						
				if ((Int32)lueDelimiterType.EditValue == 0)
				{
					if (tabControl1.SelectedIndex == 3)			//file-type
					{
						lueDelimiterType.ShowPopup();
					}
				}

                //4/1/19
                lblDecimalSeparator.Visible = true;
                lueDecimalSeparator.Visible = true;                
            }

            //5/15/20 - changed to use correct ENUM
            //if ((Int32)lueFileType.EditValue == (Int32)ImportFileTypeEnum.Excel)
            if ((Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLS || (Int32)lueFileType.EditValue == (Int32)FileTypeEnum.XLSX)
            {									
				lblSheetNumber.Visible = true;
				txtSheetNumber.Visible = true;
                lblLine3.Visible = true;

            }

			btnNext.Enabled = false;
			if ((Int32)lueFileType.EditValue != 0 && lueDelimiterType.Visible == false)
			{
				btnNext.Enabled = true;
			}
			else if ((Int32)lueFileType.EditValue != 0 && (Int32)lueDelimiterType.EditValue != 0)
			{
				btnNext.Enabled = true;
			}
		}
		private void checkExcelBeginColumn()
		{
			txtDetailBeginColumn.Visible = true;
			lblDetailBeginColumn.Visible = true;
            lblColumn.Visible = true;
            		
			lblRequired.Visible = false;            
            lblRequired.Text = "Required";

            if (txtOrientation.Text == "Portrait" || txtOrientation.Text == "")
			{			
				txtDetailBeginColumn.Visible = false;
				lblDetailBeginColumn.Visible = false;
                lblColumn.Visible = false;			
				lblRequired.Visible = false;
			}
			else
            {
				txtDetailBeginColumn.BackColor = Color.White;
				if (txtDetailBeginColumn.Text.Length == 0 || txtDetailBeginColumn.Text == "0")
				{
					txtDetailBeginColumn.BackColor = Color.Yellow;
					txtDetailBeginColumn.Focus();
					lblRequired.Visible = true;
				}
			}

			if (txtDetailBeginColumn.Text == "0" && txtOrientation.Text == "Landscape")
			{
				lblRequired.Visible = true;            
                lblRequired.Text = "Value must be greater than zero";                
            }

		}
		private void checkLandscapeHeadingRow()
		{            
            //2/1/18 - column headings are REQUIRED for landscape templates (optional for portrait) -- because, on landscape, we don't map the 
            //              result fields. For portrait, the user maps the nutrient column on the template.

            if (lueFileType.EditValue != null)
            {
                if (txtOrientation.Text == "Landscape")
                {
                    if (cbFileHasHeader.Checked == false)                    {

                        MessageBox.Show("Landscape import files must contain a heading row.", "Information");
                        txtBeginRow.Text = "1";
                        cbFileHasHeader.Checked = true;
                    }

                    cbFileHasHeader.Checked = true;
                    if (txtBeginRow.Text == "0" || txtBeginRow.Text == "" || txtBeginRow.Text == null)
                    {
                        MessageBox.Show("Landscape import files must contain a heading row. Column Header row has been changed to 1.", "Information");
                        txtBeginRow.Text = "1";
                    }
                }
            }


            if (txtOrientation.Text == "Portrait")
			{				
				txtDetailBeginColumn.Text = "0";
			}
			

		}

		private void txtOrientation_TextChanged(object sender, EventArgs e)
		{			
			if (txtOrientation.Text.Length != 0)
			{
				Int32 n;				
				if (txtOrientation.Text == "Portrait")
				{
					btnNext.Enabled = true;
				}
				else if (Int32.TryParse(txtDetailBeginColumn.Text, out n) == true) 
				{
					btnNext.Enabled = true;
				}
				
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
			if (txtBeginRow.Visible == true)
			{
				txtBeginRow.Text = "1";
			}
			txtSpectraFileExtension.Text = "";
			txtProcessPath.Text = "";
			txtBackUpDir.Text = "";
			txtSpectraDirectory.Text = "";
		}

		private void btnHelp_Click(object sender, EventArgs e)
		{
			try
			{
				importHelp = new ImportHelp();

				//10/6/21 - changed to fix resize problem
				//this.Hide();
				//importHelp.WindowState = FormWindowState.Normal;					
				//importHelp.ShowDialog();
				//this.Show();
				importHelp.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void cbFileHasHeader_CheckedChanged(object sender, EventArgs e)
		{
			checkLandscapeHeadingRow();
			
			lblBeginRow.Visible = false;			
			txtBeginRow.Visible = false;			
			if (cbFileHasHeader.Checked == true)
			{
				lblBeginRow.Visible = true;				
				txtBeginRow.Visible = true;
				txtBeginRow.Text = "1";			
			}
			
			checkExcelBeginColumn();
		}

		private void txtBeginRow_TextChanged(object sender, EventArgs e)
		{
			if (System.Text.RegularExpressions.Regex.IsMatch(txtBeginRow.Text, "[^0-9]"))
			{
				MessageBox.Show("Please enter only numbers.");
                txtBeginRow.Text = txtBeginRow.Text.Remove(txtBeginRow.Text.Length - 1);
			}
			checkLandscapeHeadingRow();

		}
        	

		private void lueFileType_Leave(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueFileType.EditValue != 0 && lueDelimiterType.Visible == false )
			{
				btnNext.Enabled = true;
			}
			else if ((Int32)lueFileType.EditValue != 0 && (Int32)lueDelimiterType.EditValue != 0)
			{
				btnNext.Enabled = true; 
			}
			
		}

		private void lueDelimiterType_Leave(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueFileType.EditValue != 0 && (Int32)lueDelimiterType.EditValue != 0)
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

			switch (tabControl1.SelectedIndex)
			{
				case 1:  //Enter template name
					btnNext.Enabled = false;
					if ((Int32)lueDataSource.EditValue != 0)
					{
						btnNext.Enabled = true;
					}

					if ((Int32)lueDataSource.EditValue == 0)
					{
						lueDataSource.ShowPopup();	
					}					
					break;

				case 2:		//Create or Merge
					btnNext.Enabled = true;

					//------------------------------------------------------------------
					//8/12/22 - for Romer Labs					
					pnlQualityControl.Visible = true;
					pnlImportType.Visible = true;
					pnlOther.Visible = true;
					pnlRomerLabs.Visible = false;
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						pnlQualityControl.Visible = false;
						pnlImportType.Visible = false;
						pnlOther.Visible = false;
						rbQualityControlNo.Checked = true;
						rbCreate.Checked = true;
						pnlRomerLabs.Visible = true;
						pnlRomerLabs.Location = new Point(20,32);
						rbAllowDupsNo.Checked = true;						
					}
					else
					{
						//------------------------------------------------------------------


						//12/2/19                    
						if (rbCreate.Checked == true || rbCreateOrMerge.Checked == true)
						{
							pnlSystemSampleCode.Visible = true;
							pnlAllowDups.Visible = true;
						}
						else
						{
							pnlSystemSampleCode.Visible = false;
							pnlAllowDups.Visible = false;
							rbSystemSampleCodeNo.Checked = true;
							rbAllowDupsNo.Checked = true;
						}

					}
					break;

				case 3:	   //File Type
					btnNext.Enabled = false;

					//8/12/22
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						lueFileType.EditValue = (Int32)ImportFileTypeEnum.CSV;
						lueFileType.Enabled = false;												
						lueDelimiterType.EditValue = (Int32)EnumDecimalSeparator.Comma;

						if (rbCompositeYes.Checked == true)
						{
							rbAllowDupsYes.Checked = false;
							rbSystemSampleCodeYes.Checked = true;
						}

						

					}

					if ((Int32)lueFileType.EditValue != 0 && lueDelimiterType.Visible == false )
					{
						btnNext.Enabled = true;
					}
					else if (lueDelimiterType.EditValue != null)
					{
						if ((Int32)lueDelimiterType.EditValue != 0)
						{
							btnNext.Enabled = true;
						}						
					}

					if ((Int32)lueFileType.EditValue == 0)
					{
						lueFileType.ShowPopup();
					}					
					break;

				case 4:  //Orientation
					btnNext.Enabled = false;

					//3/7/23
					panelHeaderDetail.Visible = true;

					//8/15/22
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						btnPortrait.PerformClick();
						cbFileHasHeader.Checked = true;						
						btnPortrait.Enabled = false;
						btnLandscape.Enabled = false;
						cbFileHasHeader.Enabled = false;
						txtBeginRow.Enabled = false;						
						txtBeginDetailRow.Enabled = false;
						txtBeginRow.Text = "1";
						txtBeginDetailRow.Text = "2";

						//3/7/23
						panelHeaderDetail.Visible = false;

						btnNext.Focus();
					}
					else
					{

						//-----------------------------------------------------------
						txtOrientation.Text = "";                           //7/26/19
						btnLandscape.Enabled = true;                        //7/26/19
						if (rbUpdateHeaderOnly.Checked == true)             //7/26/19
						{                                                   //7/26/19
							btnPortrait_Click(sender, e);                   //7/26/19
							btnLandscape.Enabled = false;                   //7/26/19
							_currentPage = 7;                               //7/26/19
						}
						//-----------------------------------------------------------
					}
                    if (txtOrientation.Text.Length != 0)
                    {
                        btnNext.Enabled = true;
                    }

                    break;
				case 5:  //AIM required fields

					//8/15/22
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						pnlRequiredFields.Enabled = false;

						//-----------------------------------------------------------------------------------
						//5/1/23 - see if more than one owner exists, set value to "SelectOnImport"; otherwise "UseMapping"
						List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
						owner = AIMManager.GetAIMOwner(true);
						if (owner.Count != 1)			//0=no owner set up yet  >1=multiple owners
						{
							lueOwner.EditValue = (Int32)SampleImportUserDefinedMapSelection.SelectOnImport;
						}
						else
						{
							lueOwner.EditValue = (Int32)SampleImportUserDefinedMapSelection.UseMapping;
						}
						lueOwnerLocation.EditValue = (Int32)SampleImportUserDefinedMapSelection.UseMapping;
						//-----------------------------------------------------------------------------------

						btnNext.Focus();
					}

					btnNext.Enabled = true;					
					break;
				case 6:     //Date Format
					btnNext.Enabled = true;
					break;
				case 7:  //Spectra files

					//8/15/22
					if (_importType == (Int32)EnumImportType.RomerResults)
					{
						cbSpectraFile.Enabled = false;
						btnNext.Focus();
					}

					btnNext.Enabled = true;					
					break;				
				case 8:  //Source and Archive folders
					btnNext.Enabled = true;					
					btnNext.Text = "Finish";
					break;
				case 9:
					btnNext.Enabled = true;
					btnCancel.Visible = false;
					
					List<UserDefinedDataSourceDefinitionInfo> templateName = new List<UserDefinedDataSourceDefinitionInfo>();
					templateName = (List<UserDefinedDataSourceDefinitionInfo>)lueImportFileName.Properties.DataSource;
					if (templateName.Count == 2)
					{
						//selecting value if there is only one
						lueImportFileName.EditValue = templateName[1].UserDefinedDataSourceDefinitionID;
					}

					lueImportFileName.ShowPopup();
					break;
				default:
					break;
			}
			CheckList();

		}
				
		private void CheckList()
		{			
			CheckedListBoxControl box = checkedListBoxControl1;
			box.SelectedIndex = tabControl1.SelectedIndex;
			
			if (box.GetItemCheckState(box.SelectedIndex) == CheckState.Checked)
			{
				box.SetItemCheckState(tabControl1.SelectedIndex, System.Windows.Forms.CheckState.Unchecked);												
			}
			else
			{
				box.SetItemCheckState(tabControl1.SelectedIndex - 1, System.Windows.Forms.CheckState.Checked);
			}

			EnableDisableCheckBoxList();			
		}
			

		private void EnableDisableCheckBoxList()
		{
			for (int i = 0; i < checkedListBoxControl1.ItemCount; i++)
			{
				object item = checkedListBoxControl1.GetItem(i);

				if (i == 0)
				{
					((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).Enabled = true;
				}
				else
				{
					if (((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).CheckState == CheckState.Unchecked)
					{
						((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).Enabled = false;
					}
					else
					{
						((DevExpress.XtraEditors.Controls.CheckedListBoxItem)(item)).Enabled = true;
					}
				}
			}
		}
		private void btnPortrait_Click(object sender, EventArgs e)
		{
			txtOrientation.Text = "Portrait";
			cbFileHasHeader.Checked = false;
			txtBeginRow.Text = "1";
			btnPortrait.BackColor = Color.LightSkyBlue;
			btnLandscape.BackColor = Color.Transparent;					
			checkLandscapeHeadingRow();
            //lblLine2.Visible = false;            // commented 8/23/19 - should always be visible
            checkExcelBeginColumn();
			
			
		}

		private void btnLandscape_Click(object sender, EventArgs e)
		{

			txtOrientation.Text = "Landscape";
            cbFileHasHeader.Checked = true;
            txtBeginRow.Text = "1";
            txtDetailBeginColumn.Text = "1";
            btnLandscape.BackColor = Color.LightSkyBlue;
            btnPortrait.BackColor = Color.Transparent;
            //lblLine2.Visible = true;           // commented 8/23/19 - should always be visible
            checkExcelBeginColumn();
			
		}
        		

		private void lueDelimiterType_EditValueChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueFileType.EditValue != 0 && (Int32)lueDelimiterType.EditValue != 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void lueImportFileName_EditValueChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if ((Int32)lueImportFileName.EditValue != 0)
			{
				btnNext.Enabled = true;
				btnNext.PerformClick();								
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
			
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void cbSpectraFile_CheckedChanged(object sender, EventArgs e)
		{
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

		private void txtSpectraFileExtension_Leave(object sender, EventArgs e)
		{
			CheckForSpectraFiles();		
		}		

		private void lueOwner_Leave(object sender, EventArgs e)
		{
			//12/28/20
			//lueOwnerLocation.EditValue = lueOwner.EditValue;			
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

		private void lueOwnerLocation_Leave(object sender, EventArgs e)
		{
			//12/28/20
			//lueOwner.EditValue = lueOwnerLocation.EditValue;	
			if ((Int32)lueOwnerLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
			{
				if ((Int32)lueOwner.EditValue != (Int32)SampleImportUserDefinedMapSelection.SelectNow)
				{
					lueOwnerLocation.EditValue = lueOwner.EditValue;
				}
			}		
		}

		private void lueSupplier_Leave(object sender, EventArgs e)
		{
			//12/28/20
			//lueSupplierLocation.EditValue = lueSupplier.EditValue;
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

		private void lueSupplierLocation_Leave(object sender, EventArgs e)
		{
			//12/28/20
			//lueSupplier.EditValue = lueSupplierLocation.EditValue;
			if ((Int32)lueSupplierLocation.EditValue == (Int32)SampleImportUserDefinedMapSelection.SelectNow)
			{
				if ((Int32)lueSupplier.EditValue != (Int32)SampleImportUserDefinedMapSelection.SelectNow)
				{
					lueSupplierLocation.EditValue = lueSupplier.EditValue;
				}
			}
		}

		private void txtImportFileName_TextChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = false;
			if (txtImportFileName.Text.Length > 0)
			{
				btnNext.Enabled = true;
			}
		}

		private void txtSheetNumber_TextChanged(object sender, EventArgs e)
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

		}

		private void txtDetailBeginColumn_TextChanged(object sender, EventArgs e)
		{

		}

		private void lueDataSource_Click(object sender, EventArgs e)
		{
			lueDataSource.SelectAll();
		}

		private void lueDataSource_Enter(object sender, EventArgs e)
		{
			lueDataSource.SelectAll();
		}

		private void lueFileType_Enter(object sender, EventArgs e)
		{
			lueFileType.SelectAll();
		}

		private void lueFileType_Click(object sender, EventArgs e)
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

		private void lueOwner_Enter(object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}

		private void lueOwner_Click(object sender, EventArgs e)
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

		private void lueImportFileName_Click(object sender, EventArgs e)
		{
			lueImportFileName.SelectAll();
		}

		private void lueImportFileName_Enter(object sender, EventArgs e)
		{
			lueImportFileName.SelectAll();
		}

		//private void btnLandscapeCSV_Click(object sender, EventArgs e)
		//{
		//	lblDetailBeginColumn.Visible = true;
		//	lblColumn.Visible = true;
		//	txtDetailBeginColumn.Visible = true;

		//	txtOrientation.Text = "Landscape";
		//	cbFileHasHeader.Checked = false;
		//	txtBeginRow.Text = "1";			
		//	btnLandscape.BackColor = Color.LightSkyBlue;
		//	btnPortrait.BackColor = Color.Transparent;

		//	if (txtDetailBeginColumn.Text != "" && txtDetailBeginColumn.Text != "0")			
		//	{
		//		btnNext.Enabled = true;		
		//	}					
		//}

		//private void cbFileHasHeaderCSV_CheckedChanged(object sender, EventArgs e)
		//{			
		//	lblBeginRow.Visible = false;
		//	lblRow.Visible = false;
		//	txtBeginRow.Visible = false;		  
		//	txtBeginRow.Text = "0";

		//	if (cbFileHasHeader.Checked == true)
		//	{		
		//		lblBeginRow.Visible = true;
		//		lblRow.Visible = true;
		//		txtBeginRow.Visible = true;
		//		txtBeginRow.Text = "1";
		//	}
		//}

		private void txtBeginRowCSV_TextChanged(object sender, EventArgs e)
		{
			btnNext.Enabled = true;
			if (System.Text.RegularExpressions.Regex.IsMatch(txtBeginRow.Text, "[^0-9]"))
			{
				MessageBox.Show("Please enter only numbers.");
				txtBeginRow.Text = txtBeginRow.Text.Remove(txtBeginRow.Text.Length - 1);				
			}

			if (txtOrientation.Text == "")
			{
				btnNext.Enabled = false;
			}

			if (txtOrientation.Text == "Landscape")
			{
				if (txtBeginRow.Text == "0" || txtBeginRow.Text == "")
				{
					txtBeginRow.Text = "1";
				}
				if (txtDetailBeginColumn.Text == "0" || txtDetailBeginColumn.Text == "")
				{
					btnNext.Enabled = false;
				}				
			}

			if (txtOrientation.Text == "Portrait")
			{
				if (cbFileHasHeader.Checked == true)
				{
					if (txtBeginRow.Text == "0" || txtBeginRow.Text == "")
					{
						btnNext.Enabled = false;						
					}
				}
				else
				{
					txtBeginRow.Text = "0";
				}
			}										

		}
			

		//private void btnPortraitCSV_Click(object sender, EventArgs e)
		//{
		//	lblDetailBeginColumn.Visible = false;
		//	lblColumn.Visible = false;
		//	txtDetailBeginColumn.Visible = false;

		//	txtOrientation.Text = "Portrait";
		//	cbFileHasHeader.Checked = false;
		//	txtBeginRow.Text = "1";
		//	btnPortrait.BackColor = Color.LightSkyBlue;
		//	btnLandscape.BackColor = Color.Transparent;
		//	btnNext.Enabled = true;
		//}

		private void txtDetailBeginColumnCSV_TextChanged(object sender, EventArgs e)
		{
			Int32 n;
			checkExcelBeginColumn();
			btnNext.Enabled = false;

			if (txtOrientation.Text == "Portrait")
			{
				btnNext.Enabled = true;
			}
			else
			{
				if (txtDetailBeginColumn.Visible == true)
				{
					if (Int32.TryParse(txtDetailBeginColumn.Text, out n) == true)
					{
						if (n != 0)
						{
							btnNext.Enabled = true;
						}
					}
					else
					{						
						txtDetailBeginColumn.Text = "0";						
					}
				}
			}
		}
        		

		private void rbYesMulti_CheckedChanged(Object sender, EventArgs e)
		{
			
			btnNext.Enabled = false;
			if ((Int32)lueDataSource.EditValue != 0)
			{
				btnNext.Enabled = true;
			}

		}

        private void rbQualityControlYes_CheckedChanged(Object sender, EventArgs e)
        {
            //force Results-Only
            rbMerge.Checked = true;
        }

        private void rbCreate_CheckedChanged(Object sender, EventArgs e)
        {         
            rbQualityControlNo.Checked = true;

            pnlSystemSampleCode.Visible = false;
            pnlAllowDups.Visible = false;
            if (rbCreate.Checked == true)
            {
                pnlSystemSampleCode.Visible = true;
                pnlAllowDups.Visible = true;
            }


        }

        private void rbCreateOrMerge_CheckedChanged(Object sender, EventArgs e)
        {         
            rbQualityControlNo.Checked = true;

            pnlSystemSampleCode.Visible = false;
            pnlAllowDups.Visible = false;
            if (rbCreateOrMerge.Checked == true)
            {
                pnlSystemSampleCode.Visible = true;
                pnlAllowDups.Visible = true;
            }
        }

        //8/23/19
        private void txtBeginDetailRow_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtBeginDetailRow.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                txtBeginDetailRow.Text = txtBeginDetailRow.Text.Remove(txtBeginDetailRow.Text.Length - 1);
            }
            else if (txtBeginDetailRow.Text == "0" || txtBeginDetailRow.Text == "" || txtBeginDetailRow.Text == null)
            {                    
                txtBeginDetailRow.Text = "1";
            }
        }
               

        //12/2/19
        private void btnImportTypeHelp_Click(object sender, EventArgs e)
        {
            try
            {
                importHelpType = new ImportHelpType();

				//10/6/21 - changed to fix resize problem
				//this.Hide();
				//importHelpType.WindowState = FormWindowState.Normal;                    
				//importHelpType.ShowDialog();
				//this.Show();
				importHelpType.ShowDialog();

			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //12/2/19
        private void rbMerge_CheckedChanged(object sender, EventArgs e)
        {
            //12/2/19
            pnlSystemSampleCode.Visible = false;
            pnlAllowDups.Visible = false;
            rbSystemSampleCodeNo.Checked = true;
            rbAllowDupsNo.Checked = true;
        }

        private void rbResultsAndHeader_CheckedChanged(object sender, EventArgs e)
        {
            //12/2/19
            pnlSystemSampleCode.Visible = false;
            pnlAllowDups.Visible = false;
            rbSystemSampleCodeNo.Checked = true;
            rbAllowDupsNo.Checked = true;
        }

        private void rbUpdateHeaderOnly_CheckedChanged(object sender, EventArgs e)
        {
            //12/2/19
            pnlSystemSampleCode.Visible = false;
            pnlAllowDups.Visible = false;
            rbSystemSampleCodeNo.Checked = true;
            rbAllowDupsNo.Checked = true;
        }
        
        //12/3/19
        private void rbSystemSampleCodeYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSystemSampleCodeYes.Checked == true)
            {
                rbAllowDupsNo.Checked = true;
            }
        }

        private void rbAllowDupsYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAllowDupsYes.Checked == true)
            {
                rbSystemSampleCodeNo.Checked = true;
            }
        }
    }
}
