using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;							
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using DevExpress.XtraSpreadsheet.UI;

namespace CFCAIM.Maintenance
{
	public partial class AnalyzerCrossReference : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		List<AnalyzerMaterialInfo> _analyzerMaterial = new List<AnalyzerMaterialInfo>();
		List<AnalyzerNutrientInfo> _analyzerNutrient = new List<AnalyzerNutrientInfo>();
		List<AnalyzerSupplierInfo> _analyzerSupplier = new List<AnalyzerSupplierInfo>();
		List<AnalyzerSupplierLocationInfo> _analyzerSupplierLocation = new List<AnalyzerSupplierLocationInfo>();
        Boolean _materialOrNutrientChangesMade = false;		
		AIM aimMaintenance;
		Int32 _SystemSettingsID = 0;
		Int32 _SystemSettingsAIMNutrientID = 0;
		Boolean _SystemSettingsAllowAIMInactive = true;
		Boolean _SystemSettingsShowAnalyzerMaterials = false;
		Boolean _SystemSettingsSampleAimNeedsRebuild = false;

		//1/27/21
		Boolean _materialLoaded = false;
		Boolean _nutrLoaded = false;
		Boolean _ownerLoaded = false;
		Boolean _ownerLocLoaded = false;
		Boolean _suppLoaded = false;
		Boolean _suppLocLoaded = false;
		Boolean _suppCombined = false;

		//StreamWriter stream;        //1/8/21 - Test
				
		public AnalyzerCrossReference()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
			//stream = openStreamWriter("C:\\Test\\Test.txt");                           
		}

		private void AnalyzerCrossReference_Load(object sender, EventArgs e)
		{
			try
			{				

				DataBindDataSourceLue("All", 0);
				lueDataSourceType.EditValue = 0;
				rbAll.Checked = true;

				//1/19/24 -- this global is set on the Sample screen, under AIM/AnalyzerMaterial hyperlink
				if (AIMGlobal.PNEDataSourceTypeID != 0)
				{
					rbSystem.Checked = true;
					DataBindDataSourceLue("System", AIMGlobal.PNEDataSourceTypeID);
					AIMGlobal.PNEDataSourceTypeID = 0;
				}

				if (AIMGlobal.analyzerCrossRefenceDataSourceTypeID > 0)		//Task1061 DB251104
				{
					lueDataSourceType.EditValue = AIMGlobal.analyzerCrossRefenceDataSourceTypeID;
				}
				
                SetToolTips();				
				getSystemSettings();
				CheckGlobalTabs();										//Task1061 DB251104

                AIMGlobal.analyzerCrossRefenceAIMNutrientID = 0;		//Task1061 DB251104
                AIMGlobal.analyzerCrossRefenceDataSourceTypeID = 0;		//Task1061 DB251104
                AIMGlobal.analyzerCrossRefenceIsNutrientTab = false;	//Task1061 DB251104

                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void CheckGlobalTabs()				//Task1061 DB251104
		{
			if (AIMGlobal.analyzerCrossRefenceIsNutrientTab == true)
			{
				tabControl.SelectedIndex = 1;
				if (AIMGlobal.analyzerCrossRefenceAIMNutrientID > 0)
				{
                    lueAIMCodeNutrient.EditValue = AIMGlobal.analyzerCrossRefenceAIMNutrientID;
					//lueAIMCode.EditValue = 1;
                }
            }
		}

		private void DataBindUsingTabControlIndex(Int32 index)
		{			

			switch (index)
			{
				//MATERIAL
				case 0:

					if (_materialLoaded == false)
					{
						CheckDataSourceAndHideOrShowButtons(btnAddMaterial, gbAimMaterial, gbAnalyzerMaterial, "colDelete", grdMaterialRefView);
						DataBindLueAnalyzerMaterial();
						DataBindMateriallue();
						
						//7/11/23
						DataBindAnalyzerMateriallue();

						DataBindMaterialGrid();
						lueAnalyzerCode.Select();
						_materialLoaded = true;
					}
					break;

				//NUTRIENT
				case 1:
					if (_nutrLoaded == false)
					{
						CheckDataSourceAndHideOrShowButtons(btnAddNutrient, gbAimNutrient, gbAnalyzerNutrient, "colDeleteNutrient", grdNutrientRefView);
						DataBindLueAnalyzerNutrient();
						DataBindNutrientlue();						
						DataBindNutrientGrid();
                        

                        lueAnalyzerCodeNutrient.Select();
						_nutrLoaded = true;

					}
					break;

				//OWNER
				case 2:
					if (_ownerLoaded == false)
					{
						CheckDataSourceAndHideOrShowButtons(btnAddOwner, this.gbAIMOwner, gbAnalyzerOwner, "colIsActive", grdOwnerView);
						DataBindAnalyzerOwnerLue();
						DataBindAimOwnerlue();
						DataBindOwnerGrid();
						lueAnalyzerOwnerCode.Select();
						_ownerLoaded = true;
					}
					break;

				//OWNER-LOCATION
				case 3:
					if (_ownerLocLoaded == false)
					{
						CheckDataSourceAndHideOrShowButtons(btnAddOwnerLocation, gbAimOwnerLocation, gbAnalyzerOwnerLocation, "colDeleteOwnerLocation", grdOwnerLocationView);
						DataBindAnalyzerAndAIMOwnerLocationLues();
						DataBindAimOwnerLue();
						DataBindOwnerLocationLue();						
						DataBindOwnerLocationGrid();
						lueAnalyzerOwnerName_OL.Select();
						_ownerLocLoaded = true;
					}
					break;

				//SUPPLIER
				case 4:
					if (_suppLoaded == false)
					{
						CheckDataSourceAndHideOrShowButtons(btnAddSupplier, gbAimSupplier, gbAnalyzerSupplier, "colDeleteSupplier", grdSupplierView);
						DataBindLueAnalyzerSupplier();
						DataBindSupplierlue();						
						DataBindSupplierGrid();
						lueAnalyzerSupplierCode.Select();
						_suppLoaded = true;
					}
					break;

				//SUPPLIER-LOCATION
				case 5:
					if (_suppLocLoaded == false)
					{
						CheckDataSourceAndHideOrShowButtons(btnAddSupplierLocation, gbAimSupplierLocation, gbAnalyzerSupplierLocation, "colDeleteSupplierLocation", grdSupplierLocationView);
						DataBindAnalyzerSupplierLocationreplue();
						DataBindSupplierLocation_Supplierlue();
						DataBindSupplierLocationlue();						
						DataBindSupplierLocationGrid();
						lueAnalyzerSupplierName_SL.Select();
						_suppLocLoaded = true;
					}
					break;

				//12/22/20
				//SUPPLIER-LOCATION COMBINED
				case 6:
					if (_suppCombined == false)
					{
						CheckDataSourceAndHideOrShowButtons(btnAddSupplierLocationCombined, gbAimSupplierLocationCombined, gbAnalyzerSupplierLocationCombined, "colDeleteCombined", grdSupplierLocationCombinedView);
						DataBindAnalyzerSupplierLocationCombined_Supplierlue();
						DataBindAnalyzerSupplierLocationCombinedlue();
						DataBindSupplierLocationCombinedlue();						
						DataBindSupplierLocationCombinedGrid();
						lueAnalyzerSupplierLocationName.Select();
						_suppCombined = true;
					}	
					break;
			}
			
		}
		private void tabControl1_SelectedIndexChanged(Object sender, EventArgs e)
		{
			DataBindUsingTabControlIndex(tabControl.SelectedIndex);
		}
		private void getSystemSettings()
		{
			SystemSettingsInfo systemSettings = new SystemSettingsInfo();
			systemSettings = AIMManager.GetSystemSettings();
			if (systemSettings != null)
			{
				_SystemSettingsID = systemSettings.SystemSettingsID;
				_SystemSettingsAIMNutrientID = systemSettings.AIMNutrientID;
				_SystemSettingsAllowAIMInactive = systemSettings.AllowAIMInactive;
				_SystemSettingsShowAnalyzerMaterials = systemSettings.ShowAnalyzerMaterials;
				_SystemSettingsSampleAimNeedsRebuild = systemSettings.SampleAimNeedsRebuild;
			}
		}
		private void CheckDataSourceAndHideOrShowButtons(Button addButton, GroupBox aimGB, GroupBox analyzerGB, String columnName, GridView view)
		{
			//------------------------------------------------
			//if "All" is selected as datasource, do not allow user to ADD 
			addButton.Enabled = true;				
			aimGB.Enabled = true;
			analyzerGB.Enabled = true;
			view.Columns.ColumnByName(columnName).Visible = true;
			if ((Int32)lueDataSourceType.EditValue == 0)
			{
				addButton.Enabled = false;
				aimGB.Enabled = false;
				analyzerGB.Enabled = false;
				view.Columns.ColumnByName(columnName).Visible = false;						
			}
		}
		private void lueDataSourceType_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				tabControl.Visible = true;

				//1/27/21
				_materialLoaded = false;
				_nutrLoaded = false;
				_ownerLoaded = false;
				_ownerLocLoaded = false;
				_suppLoaded = false;
				_suppLocLoaded = false;
				_suppCombined = false;

				//12/22/20
				DataBindUsingTabControlIndex(tabControl.SelectedIndex);

				lueDataSourceType.BackColor = Color.White;
				if ((Int32)lueDataSourceType.EditValue != 0)
				{
					if (checkUserDefinedSwitch() == true)
					{
						rbUserDefined.Checked = true;
					}
					else
					{
						rbSystem.Checked = true;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DataBindDataSourceLue(String type, Int32 id)
		{		

			List<DataSourceTypeInfo> dataSource = new List<DataSourceTypeInfo>();

			//--------------------------------------------------------
			if (type == "All")
			{
				dataSource = AIMManager.GetDataSourceType(true);
			}
			if (type == "System")
			{
				dataSource = AIMManager.GetDataSourceTypeByUserDefinedSwitch(false);
			}
			if (type == "UserDefined")
			{
				dataSource = AIMManager.GetDataSourceTypeByUserDefinedSwitch(true);
			}					

			//--------------------------------------------------------            
			if (type == "All")
            {
                dataSource.Insert
                (
                    0,
                    new DataSourceTypeInfo
                    {
                        DataSourceTypeID = 0,
                        Name = "--Select ALL--"
                    }
                );
            }			
            else
            {
                dataSource.Insert
               (
                   0,
                   new DataSourceTypeInfo
                   {
                       DataSourceTypeID = 0,
                       Name = "--Select--"
                   }
               );
            }

						
			lueDataSourceType.Properties.DataSource = dataSource;			
			XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");           

			lueDataSourceType.Properties.Columns["TypeDisplay"].Caption = "Type";
			lueDataSourceType.BackColor = Color.White;
			lueDataSourceType.EditValue = id;
			if ((Int32)lueDataSourceType.EditValue == 0)
			{
				lueDataSourceType.BackColor = Color.LightYellow;                
            }
            //selecting value if there is only one
            if (dataSource.Count == 2)
			{
				lueDataSourceType.EditValue = dataSource[1].DataSourceTypeID;
			}

			//--------------------------------------------------------
			//show or hide tab control
			if ((type == "System" || type == "UserDefined") && (Int32)lueDataSourceType.EditValue == 0)
            {
                tabControl.Visible = false;
            }
            else
            {
                tabControl.Visible = true;
            }					

		}
		private void SetToolTips()
		{
			System.Drawing.Icon ico = Properties.Resources.CFCAIM;
			this.Icon = ico;
			ToolTip ttip = new ToolTip();
			foreach (Control ctrl in pnlTopBtns.Controls)
			{
				ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			}
			ttip.SetToolTip(btnPreviewMaterial, "Preview Grid");
			ttip.SetToolTip(btnPreviewNutrient, "Preview Grid");
			ttip.SetToolTip(btnPreviewGridResultsOwner, "Preview Grid");
			ttip.SetToolTip(btnPreviewGridResultsOwnerLocation, "Preview Grid");
			ttip.SetToolTip(btnPreviewSupplier, "Preview Grid");
			ttip.SetToolTip(btnPreviewSupplierLocation, "Preview Grid");
			ttip.SetToolTip(btnAddMaterial, "Add Material Cross-Reference");
			ttip.SetToolTip(btnAddNutrient, "Add Nutrient Cross-Reference");
			ttip.SetToolTip(btnAddOwner, "Add Owner Cross-Reference");
			ttip.SetToolTip(btnAddOwnerLocation, "Add Owner Location Cross-Reference");
			ttip.SetToolTip(btnAddSupplier, "Add Supplier Cross-Reference");
			ttip.SetToolTip(btnAddSupplierLocation, "Add Supplier Location Cross-Reference");

			//12/21/20
			ttip.SetToolTip(btnAddSupplierLocationCombined, "Add Supplier Location (combined) Cross-Reference");

			ttip.SetToolTip(btnUpdateMaterial, "Update Material Cross References and AIM Materials");
			ttip.SetToolTip(btnUpdateOwner, "Update Owner Cross References and AIM Owners");
			ttip.SetToolTip(btnUpdateOwnerLocation, "Update Owner Location Cross References and AIM Owner Locations");
			ttip.SetToolTip(btnUpdateSupplier, "Update Supplier Cross References and AIM Suppliers");
			ttip.SetToolTip(btnUpdateSupplierLocation, "Update Supplier Location Cross References and AIM Supplier Locations");
			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

			ttip.SetToolTip(lblmaterialCode, "Open AIM Material Maintenance");
			ttip.SetToolTip(lblnutrientCode, "Open AIM Nutrient Maintenance");


		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			try
			{
                if (_materialOrNutrientChangesMade == true)
                {								

					//Update system setting					 				   														 
					SystemSettingsInfo settings = new SystemSettingsInfo();
										
					settings = AIMManager.GetSystemSettings();                                  //10/2/24 cm

                    settings.SystemSettingsID = _SystemSettingsID;
					settings.AIMNutrientID = _SystemSettingsAIMNutrientID;
					settings.AllowAIMInactive = _SystemSettingsAllowAIMInactive;
					settings.ShowAnalyzerMaterials = _SystemSettingsShowAnalyzerMaterials;
					settings.SampleAimNeedsRebuild = true;

					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveSystemSettings(ref settings);
					if (error.ErrorNumber > 0)
					{
						return;
					}

					String msg = "";
                    msg = "Changes have been made to Material and/or Nutrient cross references.";
					msg += Environment.NewLine + Environment.NewLine + "The Sample AIM table needs to be rebuilt.";
					msg += Environment.NewLine + Environment.NewLine + "Rebuild the table under Utilities > Rebuild SampleAIM Table.";					
                    msg += Environment.NewLine + Environment.NewLine + "The rebuild may take a few minutes. ";
                    MessageBox.Show(msg, "Warning...");
                    
                }	                


                Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void SyncValueLue(ref LookUpEdit from, ref LookUpEdit to)
		{
			if (from == null || to == null)
			{
				return;
			}
			if (to.EditValue == from.EditValue)
			{
				return;
			}
			to.EditValue = from.EditValue;
		}
		private void ChecklueDataSource()
		{

			if (lueDataSourceType.EditValue != null)
			{
				if ((Int32)lueDataSourceType.EditValue == 0)
				{
					lueDataSourceType.ShowPopup();
				}
			}
		}
		private void rbAll_CheckedChanged(Object sender, EventArgs e)
		{
			try
			{
				if (rbAll.Checked == true)
				{
					DataBindDataSourceLue("All", 0);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void rbUserDefined_CheckedChanged(Object sender, EventArgs e)
		{
			try
			{
				if (rbUserDefined.Checked == true)
				{
					//if a datasource has already been selected, see if it's user-defined or system
					if ((Int32)lueDataSourceType.EditValue != 0)
					{
						//currently system datasource is selected
						if (checkUserDefinedSwitch() == false)
						{
							lueDataSourceType.EditValue = 0;
							DataBindDataSourceLue("UserDefined", (Int32)lueDataSourceType.EditValue);
							lueDataSourceType.ShowPopup();
						}
						else
						{
							DataBindDataSourceLue("UserDefined", (Int32)lueDataSourceType.EditValue);
						}
					}
					else
					{
						DataBindDataSourceLue("UserDefined", 0);
						lueDataSourceType.ShowPopup();
					}
				}
				else
				{
					lueDataSourceType.EditValue = 0;
					DataBindDataSourceLue("System", 0);
					lueDataSourceType.ShowPopup();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void rbSystem_CheckedChanged(Object sender, EventArgs e)
		{
			try
			{
				if (rbSystem.Checked == true)
				{
					//if a datasource has already been selected, see if it's user-defined or system
					if ((Int32)lueDataSourceType.EditValue != 0)
					{
						//currently user-defined datasource is selected
						if (checkUserDefinedSwitch() == true)
						{
							lueDataSourceType.EditValue = 0;
							DataBindDataSourceLue("System", (Int32)lueDataSourceType.EditValue);
							lueDataSourceType.ShowPopup();
						}
						else
						{
							DataBindDataSourceLue("System", (Int32)lueDataSourceType.EditValue);
						}
					}
					else
					{
						DataBindDataSourceLue("System", 0);
						lueDataSourceType.ShowPopup();
					}
				}
				else
				{

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		#region MATERIAL -------------------------------------------------------------------
		private void DataBindMaterialGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdMaterialRefView.FocusedRowHandle.ToString(), out rowNum);

			//7/11/23
			//grdMaterialRef.DataSource = AIMManager.GetAllAIMMaterialRefAnalyzerMaterial();
			if (rbProduct.Checked == true)
			{
				grdMaterialRef.DataSource = AIMManager.GetAIMMaterialRefAnalyzerMaterialListMaterialTypeID((Int32)MaterialTypeEnum.Product);
			}
			else
			{
				grdMaterialRef.DataSource = AIMManager.GetAIMMaterialRefAnalyzerMaterialListMaterialTypeID((Int32)MaterialTypeEnum.Ingredient);
			}
			
			grdMaterialRefView.FocusedRowHandle = rowNum;			
			checkfilterMaterial();
		}
		private void DataBindAimMaterialreplue()
		{
			//1/8/21 commented, not used
			//List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			//aimMaterial = AIMManager.GetAIMMaterial(true);

			//replueCFCCode.DataSource = aimMaterial;
			//XtraEditors.LoadGridLookUpEdit(ref replueCFCCode, "AIMMaterialID", "AIMMaterialCode");
			//replueCFCCode.Columns["AIMMaterialCode"].Visible = true;
			//replueCFCCode.Columns["AIMMaterialName"].Visible = true;

			//replueCFCName.DataSource = aimMaterial;
			//XtraEditors.LoadGridLookUpEdit(ref replueCFCName, "AIMMaterialID", "AIMMaterialName");
			//replueCFCName.Columns["AIMMaterialCode"].Visible = true;
			//replueCFCName.Columns["AIMMaterialName"].Visible = true;
		}

		//7/11/23
		private void DataBindAnalyzerMateriallue()
		{
			List<AnalyzerMaterialInfo> AnalyzerMaterial = new List<AnalyzerMaterialInfo>();
			//gets active materials
			AnalyzerMaterial = AIMManager.GetAllAnalyzerMaterial(true);
			_analyzerMaterial = AnalyzerMaterial;

		}
		private void DataBindMateriallue()
		{						
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			
			//------------------------------------------------------------------------		
			aimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--",
					AIMOwnerLocationName = "--Select--"
				}
			);

			//7/11/23
			//aimMaterial = AIMManager.GetAIMMaterial(true);
			if (rbProduct.Checked == true)
			{
				//10/25/23
				//aimMaterial = AIMManager.GetAIMMaterial_MaterialTypeID_IsActive((Int32)MaterialTypeEnum.Product, true);
				aimMaterial = AIMManager.GetAIMMaterial_MaterialTypeID_IsActive((Int32)MaterialTypeEnum.Product, true, 1);
			}
			else
			{
				//10/25/23
				//aimMaterial = AIMManager.GetAIMMaterial_MaterialTypeID_IsActive((Int32)MaterialTypeEnum.Ingredient, true);
				aimMaterial = AIMManager.GetAIMMaterial_MaterialTypeID_IsActive((Int32)MaterialTypeEnum.Ingredient, true, 1);
			}					
						
			lueAIMCode.Properties.DataSource = aimMaterial;
			lueAIMName.Properties.DataSource = aimMaterial;

			//7/11/23
			//XtraEditors.LoadLookUpEdit(ref lueAIMCode, "AIMMaterialID", "AIMMaterialCode", "AIMOwnerLocationName");
			//XtraEditors.LoadLookUpEdit(ref lueAIMName, "AIMMaterialID", "AIMMaterialName", "AIMOwnerLocationName");
			if (rbProduct.Checked == true)
			{
				XtraEditors.LoadLookUpEdit(ref lueAIMCode, "AIMMaterialID", "AIMMaterialCode", "AIMOwnerLocationName");
				XtraEditors.LoadLookUpEdit(ref lueAIMName, "AIMMaterialID", "AIMMaterialName", "AIMOwnerLocationName");
			}
			else
			{
				XtraEditors.LoadLookUpEdit(ref lueAIMCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
				XtraEditors.LoadLookUpEdit(ref lueAIMName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
			}

			//7/11/23 - moved to it's own function
			//List<AnalyzerMaterialInfo> AnalyzerMaterial = new List<AnalyzerMaterialInfo>();
			////gets active materials
			//AnalyzerMaterial = AIMManager.GetAllAnalyzerMaterial(true);		
			//_analyzerMaterial = AnalyzerMaterial;

			//1/8/21 - commented, not used
			//replueAnalyzerCode.DataSource = AnalyzerMaterial;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerCode, "AnalyzerMaterialID", "AnalyzerMaterialCode");			

			//1/8/21 - commented, not used
			//replueAnalyzerName.DataSource = AnalyzerMaterial;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerName, "AnalyzerMaterialID", "AnalyzerMaterialName");

			//1/8/21 - commented, not used
			////these 2 data-sources do not include "--Select--" in the dropdown (on purpose)
			//List<DataSourceTypeInfo> dataSourceGrid = new List<DataSourceTypeInfo>();
			//dataSourceGrid = AIMManager.GetDataSourceType(true);
			//replueDataSourceType.DataSource = dataSourceGrid;
			//XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");
			//replueDataSourceType.Columns["TypeDisplay"].Caption = "Type";

			//replueDataSourceType.Columns["DataSourceTypeID"].Visible = false;
			//replueDataSourceType.Columns["Name"].Visible = true;

		}
		private void checkfilterMaterial()
		{
			if (lueDataSourceType.EditValue != null)
			{                
                List<AIMMaterialRefAnalyzerMaterialInfo> xref = new List<AIMMaterialRefAnalyzerMaterialInfo>();
                if ((Int32)lueDataSourceType.EditValue != 0)
				{	
					//7/11/23
                    //xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeID((Int32)lueDataSourceType.EditValue);					
					if (rbProduct.Checked == true)
					{
						xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDMaterialTypeID((Int32)lueDataSourceType.EditValue, (Int32)MaterialTypeEnum.Product);
					}
					else
					{
						xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDMaterialTypeID((Int32)lueDataSourceType.EditValue, (Int32)MaterialTypeEnum.Ingredient);
					}

				}
                else
                {   
					//7/11/23
                    //xref = AIMManager.GetAllAIMMaterialRefAnalyzerMaterial();					
					if (rbProduct.Checked == true)
					{
						xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialListMaterialTypeID((Int32)MaterialTypeEnum.Product);
					}
					else
					{
						xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialListMaterialTypeID((Int32)MaterialTypeEnum.Ingredient);
					}

				}
                Int32 currentRowNum = 0;
                if (grdMaterialRefView != null)
                {
                    Int32.TryParse(grdMaterialRefView.FocusedRowHandle.ToString(), out currentRowNum);
                }
                grdMaterialRef.DataSource = xref;
                grdMaterialRefView.FocusedRowHandle = currentRowNum;				 
            }

		}
		private void DataBindLueAnalyzerMaterial()
		{
			List<AnalyzerMaterialInfo> AnalyzerMaterial = new List<AnalyzerMaterialInfo>();
			Int32 dataType = 0;

			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataType = (Int32)lueDataSourceType.EditValue;
			}


			if (dataType == 0)
			{                
                AnalyzerMaterial = AIMManager.GetAnalyzerMaterial(true);
			}
			else
			{
				AnalyzerMaterial = AIMManager.GetAnalyzerMaterial(dataType, true);
			}

			AnalyzerMaterial.Insert
			(
				0,
				new AnalyzerMaterialInfo
				{
					AnalyzerMaterialID = 0,
					AnalyzerMaterialCode = "--Select--",
					AnalyzerMaterialName = "--Select--"
				}
			);
			lueAnalyzerCode.Properties.DataSource = AnalyzerMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerCode, "AnalyzerMaterialID", "AnalyzerMaterialCode");
			lueAnalyzerCode.Properties.Columns["AnalyzerMaterialCode"].Visible = true;
			lueAnalyzerCode.Properties.Columns["AnalyzerMaterialName"].Visible = true;

			lueAnalyzerName.Properties.DataSource = AnalyzerMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerName, "AnalyzerMaterialID", "AnalyzerMaterialName");
			lueAnalyzerName.Properties.Columns["AnalyzerMaterialCode"].Visible = true;
			lueAnalyzerName.Properties.Columns["AnalyzerMaterialName"].Visible = true;

			checkfilterMaterial();

		}
		private void repbtnDelete_Click(object sender, EventArgs e)
		{
			try
			{                
                _materialOrNutrientChangesMade = true;
                AIMMaterialRefAnalyzerMaterialInfo materialRef = new AIMMaterialRefAnalyzerMaterialInfo();
				materialRef = (AIMMaterialRefAnalyzerMaterialInfo)grdMaterialRefView.GetFocusedRow();
                AIMManager.DeleteAIMMaterialRefAnalyzerMaterial(materialRef.AIMMaterialRefAnalyzerMaterialID, userGlobal.CFCLoginUser.CFCUserID);
                DataBindMaterialGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewMaterial_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdMaterialRef, "Analyzer Material Cross Reference");
				CFCAIMCore.Global.previewGridResults(grdMaterialRef, "Analyzer Material Cross Reference");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddMaterial_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueAIMCode.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Material.", "Warning...");
					lueAIMCode.ShowPopup();
					return;
				}
				if ((Int32)lueAnalyzerCode.EditValue == 0)
				{
					MessageBox.Show("Please select Analyzer Material.", "Warning...");
					lueAnalyzerCode.ShowPopup();
				}
                                
                _materialOrNutrientChangesMade = true;
                AIMMaterialRefAnalyzerMaterialInfo materialRef = new AIMMaterialRefAnalyzerMaterialInfo();
				materialRef.AnalyzerMaterialID = (Int32)lueAnalyzerCode.EditValue;
				materialRef.AIMMaterialID = (Int32)lueAIMCode.EditValue;
				materialRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				materialRef.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMMaterialRefAnalyzerMaterial(ref materialRef); //3

				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindMaterialGrid();
				lueAIMCode.EditValue = 0;
				lueAnalyzerCode.EditValue = 0;
				lueAIMCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private DataView filter = null;
		private void grdMaterialRefView_HiddenEditor(object sender, EventArgs e)
		{
			if (filter != null)
			{
				filter.Dispose();
				filter = null;
			}
		}
		private void grdMaterialRefView_ShownEditor(object sender, EventArgs e)
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> x = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			GridView view = (GridView)sender;

			if (view.FocusedColumn.FieldName == "AnalyzerMaterialID" && view.ActiveEditor is DevExpress.XtraEditors.LookUpEdit)
			{
				LookUpEdit edit = (LookUpEdit)view.ActiveEditor;
				x = (List<AIMMaterialRefAnalyzerMaterialInfo>)view.DataSource;

				List<AnalyzerMaterialInfo> filter = new List<AnalyzerMaterialInfo>();
				foreach (var item in _analyzerMaterial)
				{
					if (item.DataSourceTypeID == x[view.FocusedRowHandle].DataSourceTypeID)
					{
						filter.Add(item);
					}
				}
				edit.Properties.DataSource = filter;
			}
		}
		private void grdMaterialRefView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
                
                AIMMaterialRefAnalyzerMaterialInfo materialRef = new AIMMaterialRefAnalyzerMaterialInfo();
				materialRef = (AIMMaterialRefAnalyzerMaterialInfo)grdMaterialRefView.GetFocusedRow();
				materialRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;


				if (materialRef.AIMMaterialID == 0 || materialRef.AnalyzerMaterialID == 0)
				{
					MessageBox.Show("Need to select both FRM Material and Analyzer Material. Information is not saved", "Warning...");
					return;
				}

                _materialOrNutrientChangesMade = true;
                AIMManager.SaveAIMMaterialRefAnalyzerMaterial(ref materialRef); //4
				DataBindMaterialGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void replueCFCCode_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			try
			{
				grdMaterialRefView.SetFocusedRowCellValue(colAIMMaterialName, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAnalyzerCode_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			try
			{
				grdMaterialRefView.SetFocusedRowCellValue(colAnalyzerMaterialName, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAnalyzerName_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			try
			{
				grdMaterialRefView.SetFocusedRowCellValue(colAnalyzerMaterialCode, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnUpdateMaterial_Click(Object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "MATERIAL";
			openCrossRefUpdate();
		}
		#endregion

		#region NUTRIENT -------------------------------------------------------------------
		private void DataBindNutrientGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdNutrientRefView.FocusedRowHandle.ToString(), out rowNum);
			grdNutrientRef.DataSource = AIMManager.GetAllAIMNutrientRefAnalyzerNutrient();
			grdNutrientRefView.FocusedRowHandle = rowNum;
			checkfilterNutrient();
		}

		//1/8/21 - commented, not used
		//private void DataBindAimNutrientreplue()
		//{
		//	List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
		//	aimNutrient = AIMManager.GetAllAIMNutrient(true);

		//	replueAIMCodeNutrient.DataSource = aimNutrient;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMCodeNutrient, "AIMNutrientID", "AIMNutrientCode");
		//	replueAIMCodeNutrient.Columns["AIMNutrientCode"].Visible = true;
		//	replueAIMCodeNutrient.Columns["AIMNutrientName"].Visible = true;

		//	replueAIMNameNutrient.DataSource = aimNutrient;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMNameNutrient, "AIMNutrientID", "AIMNutrientName");
		//	replueAIMNameNutrient.Columns["AIMNutrientCode"].Visible = true;
		//	replueAIMNameNutrient.Columns["AIMNutrientName"].Visible = true;
		//}

		private void DataBindNutrientlue()
		{
			List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
			aimNutrient = AIMManager.GetAllAIMNutrient(true);

			List<AnalyzerNutrientInfo> AnalyzerNutrient = new List<AnalyzerNutrientInfo>();
			AnalyzerNutrient = AIMManager.GetAllAnalyzerNutrient(true);

			List<UnitTypeInfo> unitType = new List<UnitTypeInfo>();
			unitType = AIMManager.GetAllUnitType();

			_analyzerNutrient = AnalyzerNutrient;


			//------------------------------------------------------------------------			
			aimNutrient.Insert
			(
				0,
				new AIMNutrientInfo
				{
					AIMNutrientID = 0,
					AIMNutrientCode = "--Select--",
					AIMNutrientName = "--Select--"
				}
			);
			lueAIMCodeNutrient.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMCodeNutrient, "AIMNutrientID", "AIMNutrientCode","AIMNutrientName");
			
			lueAIMNameNutrient.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMNameNutrient, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");


            replueAIMUnitTypeNutrient.DataSource = unitType;
            XtraEditors.LoadGridLookUpEdit(ref replueAIMUnitTypeNutrient, "UnitTypeID", "UnitTypeDescription");
            replueAIMUnitTypeNutrient.Columns["UnitTypeID"].Visible = true;
            replueAIMUnitTypeNutrient.Columns["UnitTypeDescription"].Visible = true;

			//1/8/21 - commented, not used
			//replueAnalyzerCodeNutrient.DataSource = AnalyzerNutrient;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerCodeNutrient, "AnalyzerNutrientID", "AnalyzerNutrientCode");
			//replueAnalyzerCodeNutrient.Columns["AnalyzerNutrientCode"].Visible = true;
			//replueAnalyzerCodeNutrient.Columns["AnalyzerNutrientName"].Visible = true;

			//replueAnalyzerNameNutrient.DataSource = AnalyzerNutrient;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerNameNutrient, "AnalyzerNutrientID", "AnalyzerNutrientName");
			//replueAnalyzerNameNutrient.Columns["AnalyzerNutrientCode"].Visible = true;
			//replueAnalyzerNameNutrient.Columns["AnalyzerNutrientName"].Visible = true;
		}
		private void checkfilterNutrient()
		{
			if (lueDataSourceType.EditValue != null)
			{                
                List<AIMNutrientRefAnalyzerNutrientInfo> xref = new List<AIMNutrientRefAnalyzerNutrientInfo>();
                if ((Int32)lueDataSourceType.EditValue != 0)
				{                
					xref = AIMManager.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeID((Int32)lueDataSourceType.EditValue);				
				}
                else
                {                    
                    xref = AIMManager.GetAllAIMNutrientRefAnalyzerNutrient();
                }                
                Int32 currentRowNum = 0;
                if (grdNutrientRefView != null)
                {
                    Int32.TryParse(grdNutrientRefView.FocusedRowHandle.ToString(), out currentRowNum);
                }
                grdNutrientRef.DataSource = xref;
                grdNutrientRefView.FocusedRowHandle = currentRowNum;
            }
		}
		private void DataBindLueAnalyzerNutrient()
		{

			List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
			Int32 dataType = 0;

			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataType = (Int32)lueDataSourceType.EditValue;
			}

			if (dataType == 0)
			{
				analyzerNutrient.Clear();
			}
			else
			{
				analyzerNutrient = AIMManager.GetAnalyzerNutrient(dataType, true);
			}

			analyzerNutrient.Insert
			(
				0,
				new AnalyzerNutrientInfo
				{
					AnalyzerNutrientID = 0,
					AnalyzerNutrientCode = "--Select--",
					AnalyzerNutrientName = "--Select--"
				}
			);
			lueAnalyzerCodeNutrient.Properties.DataSource = analyzerNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerCodeNutrient, "AnalyzerNutrientID", "AnalyzerNutrientCode");
			lueAnalyzerCodeNutrient.Properties.Columns["AnalyzerNutrientCode"].Visible = true;
			lueAnalyzerCodeNutrient.Properties.Columns["AnalyzerNutrientName"].Visible = true;

			lueAnalyzerNameNutrient.Properties.DataSource = analyzerNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerNameNutrient, "AnalyzerNutrientID", "AnalyzerNutrientName");
			lueAnalyzerNameNutrient.Properties.Columns["AnalyzerNutrientCode"].Visible = true;
			lueAnalyzerNameNutrient.Properties.Columns["AnalyzerNutrientName"].Visible = true;

			checkfilterNutrient();


		}
		private void repbtnDeleteNutrient_Click(object sender, EventArgs e)
		{
			try
			{                
                _materialOrNutrientChangesMade = true;
                AIMNutrientRefAnalyzerNutrientInfo nutrientRef = new AIMNutrientRefAnalyzerNutrientInfo();
				nutrientRef = (AIMNutrientRefAnalyzerNutrientInfo)grdNutrientRefView.GetFocusedRow();
				AIMManager.DeleteAIMNutrientRefAnalyzerNutrient(nutrientRef.AIMNutrientRefAnalyzerNutrientID, userGlobal.CFCLoginUser.CFCUserID);
				AIMManager.DeleteSampleAim(nutrientRef.AIMNutrientID);

				DataBindNutrientGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMCodeNutrient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMCodeNutrient, ref lueAIMNameNutrient);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMNameNutrient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMNameNutrient, ref lueAIMCodeNutrient);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAnalyzerCodeNutrient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAnalyzerCodeNutrient, ref lueAnalyzerNameNutrient);
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
				//AIMGlobal.previewGridResults(grdNutrientRef, "Analyzer Nutrient Cross Reference");
				CFCAIMCore.Global.previewGridResults(grdNutrientRef, "Analyzer Nutrient Cross Reference");

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
				if ((Int32)lueAIMCodeNutrient.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Nutrient.", "Warning...");
					lueAIMCodeNutrient.ShowPopup();
					return;
				}
				if ((Int32)lueAnalyzerCodeNutrient.EditValue == 0)
				{

					MessageBox.Show("Please select Analyzer Nutrient.", "Warning...");
					lueAnalyzerCodeNutrient.ShowPopup();
					return;
				}

                //4/18/18 - check to see if xref already exists for this AnalyzerNutrientID/DataSourceTypeID and AIMNutrientID
                List<AIMNutrientRefAnalyzerNutrientInfo> xref = new List<AIMNutrientRefAnalyzerNutrientInfo>();
                DialogResult dr = new DialogResult();
                xref = AIMManager.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientIDAnalyzerNutrientID((Int32)lueDataSourceType.EditValue, (Int32)lueAIMCodeNutrient.EditValue, (Int32)lueAnalyzerCodeNutrient.EditValue);
                if (xref.Count > 0)
                {

                    dr = MessageBox.Show("This cross-reference already exists.");
                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }

                //check to see if a cross-reference already exists for this AIMNutrientID.
                //if so, warn user and ask yes/no to proceed 
                xref = AIMManager.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientID((Int32)lueDataSourceType.EditValue, (Int32)lueAIMCodeNutrient.EditValue);
				if (xref.Count > 0)
				{
					
					dr = MessageBox.Show("A cross-reference already exists for this AIM Nutrient and DataSource. Adding another cross-reference may affect sample result values. Continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)					
					{
						return;
					}
				}

				_materialOrNutrientChangesMade = true;

				AIMNutrientRefAnalyzerNutrientInfo nutrientRef = new AIMNutrientRefAnalyzerNutrientInfo();
				nutrientRef.AnalyzerNutrientID = (Int32)lueAnalyzerCodeNutrient.EditValue;
				nutrientRef.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
				nutrientRef.AIMNutrientID = (Int32)lueAIMCodeNutrient.EditValue;
				nutrientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;	
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMNutrientRefAnalyzerNutrient(ref nutrientRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				//UPDATE SAMPLEAIM TABLE -- FOR ANY PREVIOUSLY-LOADED ANALYZER-DATA IN THE SAMPLE-RESULTS TABLE
				ErrorInfo errorSampleAim = new ErrorInfo();
				Int32 aimDatabaseID = 0;

				//9/9/20 - change to use AIMCoreGlobal
				//if (AIMGlobal.aimDatabase != null)
				if (CFCAIMCore.Global.aimDatabase != null)

                {
					//9/9/20 - change to use AIMCoreGlobal
					//aimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
					aimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
                }
				AIMGlobal.SplashScreenShow();
				errorSampleAim = AIMManager.SaveSampleAIM((Int32)lueAnalyzerCodeNutrient.EditValue, (Int32)lueAIMCodeNutrient.EditValue, userGlobal.CFCLoginUser.CFCUserID, aimDatabaseID, (Int32)lueDataSourceType.EditValue);
				AIMGlobal.SplashScreenHide();
				if (errorSampleAim.ErrorNumber > 0)
				{
					return;
				}

				DataBindNutrientGrid();
				lueAIMCodeNutrient.EditValue = 0;
				lueAnalyzerCodeNutrient.EditValue = 0;
				lueAIMCodeNutrient.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAIMCodeNutrient_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			try
			{
				grdNutrientRefView.SetFocusedRowCellValue(colAIMNutrientName, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAnalyzerCodeNutrient_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			try
			{
				grdNutrientRefView.SetFocusedRowCellValue(colAnalyzerNutrientName, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAIMNameNutrient_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			try
			{
				grdNutrientRefView.SetFocusedRowCellValue(colAIMNutrientCode, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAnalyzerNameNutrient_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			try
			{
				grdNutrientRefView.SetFocusedRowCellValue(colAnalyzerNutrientCode, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private DataView filterNutrient = null;
		private void grdNutrientRefView_HiddenEditor(object sender, EventArgs e)
		{
			if (filterNutrient != null)
			{
				filterNutrient.Dispose();
				filterNutrient = null;
			}
		}
		private void grdNutrientRefView_ShownEditor(object sender, EventArgs e)
		{
			List<AIMNutrientRefAnalyzerNutrientInfo> x = new List<AIMNutrientRefAnalyzerNutrientInfo>();
			GridView view = (GridView)sender;

			if (view.FocusedColumn.FieldName == "AnalyzerNutrientID" && view.ActiveEditor is DevExpress.XtraEditors.LookUpEdit)
			{
				LookUpEdit edit = (LookUpEdit)view.ActiveEditor;
				x = (List<AIMNutrientRefAnalyzerNutrientInfo>)view.DataSource;

				List<AnalyzerNutrientInfo> filter = new List<AnalyzerNutrientInfo>();
				foreach (var item in _analyzerNutrient)
				{
					if (item.DataSourceTypeID == x[view.FocusedRowHandle].DataSourceTypeID)
					{
						filter.Add(item);
					}
				}
				edit.Properties.DataSource = filter;
			}
		}
		private void grdNutrientRefView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				AIMNutrientRefAnalyzerNutrientInfo nutrientRef = new AIMNutrientRefAnalyzerNutrientInfo();
				nutrientRef = (AIMNutrientRefAnalyzerNutrientInfo)grdNutrientRefView.GetFocusedRow();
				nutrientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (nutrientRef.AIMNutrientID == 0 || nutrientRef.AnalyzerNutrientID == 0)
				{
					MessageBox.Show("Need to select both AIM Nutrient and Analyzer Nutrient. Information is not saved", "Warning...");
					return;
				}

				//check to see if a cross-reference already exists for this AIMNutrientID.
				//if so, warn user and ask yes/no to proceed 
				List<AIMNutrientRefAnalyzerNutrientInfo> xref = new List<AIMNutrientRefAnalyzerNutrientInfo>();
				xref = AIMManager.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientID((Int32)lueDataSourceType.EditValue, (Int32)lueAIMCodeNutrient.EditValue);
				if (xref.Count > 0)
				{
					DialogResult dr = new DialogResult();
					dr = MessageBox.Show("A cross-reference already exists for this AIM Nutrient and DataSource. Adding another cross-reference may affect sample result values. Continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)
					{
						return;
					}
				}

				//----------------------------------------------
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMNutrientRefAnalyzerNutrient(ref nutrientRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				//UPDATE SAMPLEAIM TABLE -- FOR ANY PREVIOUSLY-LOADED ANALYZER-DATA IN THE SAMPLE-RESULTS TABLE
				ErrorInfo errorSampleAim = new ErrorInfo();
				Int32 aimDatabaseID = 0;

				//9/9/20 - change to use AIMCoreGlobal
				//if (AIMGlobal.aimDatabase != null)
				if (CFCAIMCore.Global.aimDatabase != null)

                {
					//9/9/20 - change to use AIMCoreGlobal
					//aimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
					aimDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
                }
                                
                _materialOrNutrientChangesMade = true;
                AIMGlobal.SplashScreenShow();
				errorSampleAim = AIMManager.SaveSampleAIM(nutrientRef.AnalyzerNutrientID, nutrientRef.AIMNutrientID, userGlobal.CFCLoginUser.CFCUserID, aimDatabaseID, (Int32)lueDataSourceType.EditValue);
				AIMGlobal.SplashScreenHide();
				if (errorSampleAim.ErrorNumber > 0)
				{
					return;
				}
				//----------------------------------------------

				DataBindNutrientGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region OWNER  ---------------------------------------------------------------------		        
		private void DataBindOwnerGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdOwnerView.FocusedRowHandle.ToString(), out rowNum);
			grdOwner.DataSource = AIMManager.GetAllAIMOwnerRefAnalyzerOwner();
			grdOwnerView.FocusedRowHandle = rowNum;
			checkfilterOwner();
		}		
		private void DataBindAimOwnerlue()
		{
			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
            aimOwner = AIMManager.GetAIMOwner(true);
            lueAIMOwnerCode.Properties.DataSource = aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerCode, "AIMOwnerID", "OwnerCode");
			lueAIMOwnerName.Properties.DataSource = aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerName, "AIMOwnerID", "OwnerName");
		}

		//1/8/21 - commented, not used
		//private void DataBindAimOwnerLocationreplue()
		//{
		//	List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
		//	aimOwner = AIMManager.GetAIMOwner(true);
		//	replueAIMOwner_OwnerLocation.DataSource = aimOwner;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMOwner_OwnerLocation, "AIMOwnerID", "OwnerName");
		//	replueAIMOwner_OwnerLocation.Columns["OwnerName"].Visible = true;

		//	List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
		//	aimOwnerLocation = AIMManager.GetAllAIMOwnerLocation(true);
		//	replueAimOwnerLocationName.DataSource = aimOwnerLocation;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAimOwnerLocationName, "AIMOwnerLocationID", "OwnerLocationName");
		//	replueAimOwnerLocationName.Columns["OwnerLocationName"].Visible = true;
		//}	

		//1/8/21 - commented, not used
		//private void DataBindAnalyzerOwnerLocationreplue()
		//{
		//	List<AnalyzerOwnerInfo> analyzerOwner = new List<AnalyzerOwnerInfo>();
		//	analyzerOwner = AIMManager.GetAnalyzerOwner((Int32)lueDataSourceType.EditValue, true);
		//	replueAnalyzerOwner_OwnerLocation.DataSource = analyzerOwner;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwner_OwnerLocation, "AnalyzerOwnerID", "AnalyzerOwnerName");
		//	replueAnalyzerOwner_OwnerLocation.Columns["AnalyzerOwnerName"].Visible = true;

		//	List<AnalyzerOwnerLocationInfo> analyzerOwnerLocation = new List<AnalyzerOwnerLocationInfo>();
		//	analyzerOwnerLocation = AIMManager.GetAnalyzerOwnerLocation((Int32)lueDataSourceType.EditValue, true);
		//	replueAnalyzerOwnerLocationName.DataSource = analyzerOwnerLocation;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerLocationName, "AnalyzerOwnerLocationID", "AnalyzerOwnerLocationName");
		//	replueAnalyzerOwnerLocationName.Columns["AnalyzerOwnerLocationName"].Visible = true;
		//}		

		private void DataBindOwnerlue()
		{
			List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
			owner = AIMManager.GetAIMOwner(true);
			replueAIMOwner_OwnerLocation.DataSource = owner;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMOwner_OwnerLocation, "AIMOwnerID", "OwnerName");
			replueAIMOwner_OwnerLocation.Columns["OwnerName"].Visible = true;
		}	   		 																						  		
		private void checkfilterOwner()
		{
			if (lueDataSourceType.EditValue != null)
			{
				List<AIMOwnerRefAnalyzerOwnerInfo> xref = new List<AIMOwnerRefAnalyzerOwnerInfo>();
				if ((Int32)lueDataSourceType.EditValue != 0)
				{
					xref = AIMManager.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeID((Int32)lueDataSourceType.EditValue);
				}
				else
				{
					xref = AIMManager.GetAllAIMOwnerRefAnalyzerOwner();
				}
				Int32 currentRowNum = 0;
				if (grdOwnerView != null)
				{
					Int32.TryParse(grdOwnerView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdOwner.DataSource = xref;
				grdOwnerView.FocusedRowHandle = currentRowNum;
			}

		}				
		private void DataBindLueAnalyzerOwner()
		{
			List<AnalyzerOwnerInfo> analyzerOwner = new List<AnalyzerOwnerInfo>();

			Int32 dataType = 0;
			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataType = (Int32)lueDataSourceType.EditValue;
			}
			if (dataType == 0)
			{
				analyzerOwner = AIMManager.GetAllAnalyzerOwner(true);
			}
			else
			{	 
				analyzerOwner = AIMManager.GetAnalyzerOwner(dataType, true);
			}

			analyzerOwner.Insert
			(
				0,
				new AnalyzerOwnerInfo
				{
					AnalyzerOwnerID = 0,
					AnalyzerOwnerCode = "--Select--",
					AnalyzerOwnerName = "--Select--"
				}
			);

			lueAnalyzerOwnerCode.Properties.DataSource = analyzerOwner;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerOwnerCode, "AnalyzerOwnerID", "AnalyzerOwnerCode");
			lueAnalyzerOwnerName.Properties.DataSource = analyzerOwner;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");
			lueAnalyzerOwnerName_OL.Properties.DataSource = analyzerOwner;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerOwnerName_OL, "AnalyzerOwnerID", "AnalyzerOwnerName");

			//OwnerLocation			
			List<AnalyzerOwnerLocationInfo> analyzerOwnerLocation = new List<AnalyzerOwnerLocationInfo>();
			analyzerOwnerLocation.Insert
			(
				0,
				new AnalyzerOwnerLocationInfo
				{
					AnalyzerOwnerLocationID = 0,
					AnalyzerOwnerLocationName = "--Select--"
				}
			);

			checkfilterOwner();
		}
		private void btnAddOwner_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSourceType.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSourceType.ShowPopup();
					return;
				}
				if ((Int32)lueAIMOwnerCode.EditValue == 0 || (Int32)lueAnalyzerOwnerCode.EditValue == 0)
				{
					MessageBox.Show("Please select both " + lueDataSourceType.Text + " and AIM Owners.", "Warning...");

					if ((Int32)lueAIMOwnerCode.EditValue == 0)
					{
						lueAIMOwnerCode.ShowPopup();
					}
					else
					{
						lueAnalyzerOwnerCode.ShowPopup();
					}
					return;
				}

				AIMOwnerRefAnalyzerOwnerInfo ownerXref = new AIMOwnerRefAnalyzerOwnerInfo();
				ownerXref.AnalyzerOwnerID = (Int32)lueAnalyzerOwnerCode.EditValue;
				ownerXref.AIMOwnerID = (Int32)lueAIMOwnerCode.EditValue;
				ownerXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                ownerXref.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
                ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMOwnerRefAnalyzerOwner(ref ownerXref);
				if (error.ErrorNumber > 0)
				{
					DataBindAimOwnerLue();
					return;
				}
				DataBindOwnerGrid();
				lueAnalyzerOwnerCode.EditValue = 0;
				lueAIMOwnerCode.EditValue = 0;
				lueAIMOwnerCode.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		
		private void btnPreviewGridResultsOwner_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdOwner, lueDataSourceType.Text + " Analyzer Owner Cross Reference");
				CFCAIMCore.Global.previewGridResults(grdOwner, lueDataSourceType.Text + " Analyzer Owner Cross Reference");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMOwnerName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMOwnerName, ref lueAIMOwnerCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}	   
		private void lueAIMOwnerCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMOwnerCode, ref lueAIMOwnerName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueDeleteOwner_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				AIMOwnerRefAnalyzerOwnerInfo ownerRef = new AIMOwnerRefAnalyzerOwnerInfo();
				ownerRef = (AIMOwnerRefAnalyzerOwnerInfo)grdOwnerView.GetFocusedRow();
				AIMManager.DeleteAIMOwnerRefAnalyzerOwner(ownerRef.AIMOwnerRefAnalyzerOwnerID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindAimOwnerLue();
				DataBindOwnerGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnUpdateOwner_Click(Object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "OWNER";
			openCrossRefUpdate();
		}
		#endregion Owner

		#region OWNERLOCATION --------------------------------------------------------------		
		private void DataBindOwnerLocationGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out rowNum);
			grdOwnerLocation.DataSource = AIMManager.GetAllAIMOwnerLocationRefAnalyzerOwnerLocation();
			grdOwnerLocationView.FocusedRowHandle = rowNum;			
			checkfilterOwnerLocation();

		}				
		private void checkfilterOwnerLocation()
		{
			if (lueDataSourceType.EditValue != null)
			{
				List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> xref = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
				if ((Int32)lueDataSourceType.EditValue != 0)
				{
					xref = AIMManager.GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeID((Int32)lueDataSourceType.EditValue);
				}
				else
				{
					xref = AIMManager.GetAllAIMOwnerLocationRefAnalyzerOwnerLocation();
				}
				Int32 currentRowNum = 0;
				if (grdOwnerLocationView != null)
				{
					Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdOwnerLocation.DataSource = xref;
				grdOwnerLocationView.FocusedRowHandle = currentRowNum;
			}

		}
		private void btnAddOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSourceType.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSourceType.ShowPopup();
					return;
				}				
				if ((Int32)lueOwnerName.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Owner.", "Warning...");
					lueOwnerName.ShowPopup();
					return;
				}
				if ((Int32)lueAIMOwnerLocationName.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Owner Location.", "Warning...");
					lueAIMOwnerLocationName.ShowPopup();
					return;
				}
				if ((Int32)lueAnalyzerOwnerName_OL.EditValue == 0)
				{
					MessageBox.Show("Please select " + lueDataSourceType.Text + " Owner.", "Warning...");
					lueAnalyzerOwnerName_OL.ShowPopup();
					return;
				}
				if ((Int32)lueAnalyzerLocationName.EditValue == 0)
				{
					MessageBox.Show("Please select " + lueDataSourceType.Text + " Owner Location.", "Warning...");
					lueAnalyzerLocationName.ShowPopup();
					return;
				}

				AIMOwnerLocationRefAnalyzerOwnerLocationInfo locXref = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();
				locXref.AnalyzerOwnerID = (Int32)lueAnalyzerOwnerName_OL.EditValue;
				locXref.AnalyzerOwnerLocationID = (Int32)lueAnalyzerLocationName.EditValue;
				locXref.AIMOwnerID = (Int32)lueOwnerName.EditValue;
				locXref.AIMOwnerLocationID = (Int32)lueAIMOwnerLocationName.EditValue;

                //4/18/18
                locXref.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;

                locXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAnalyzerCrossRefAimOwnerLocation(ref locXref);

				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindOwnerLocationGrid();			
				
				//clear out only the owner-locations, not the owners.	 					
				lueAnalyzerLocationName.EditValue = 0;				
				lueAIMOwnerLocationName.EditValue = 0;

				lueAIMOwnerLocationName.Focus();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteOwnerLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				AIMOwnerLocationRefAnalyzerOwnerLocationInfo locRef = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();
				locRef = (AIMOwnerLocationRefAnalyzerOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
				AIMManager.DeleteAnalyzerCrossRefAimOwnerLocation(locRef.AIMOwnerLocationRefAnalyzerOwnerLocationID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindAimOwnerLocationLue();
				DataBindOwnerLocationGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewGridResultsOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdOwnerLocation, lueDataSourceType.Text + " Analyzer Owner Location Cross Reference");
				CFCAIMCore.Global.previewGridResults(grdOwnerLocation, lueDataSourceType.Text + " Analyzer Owner Location Cross Reference");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}	  						
		private void lueAnalyzerOwnerLocationCode_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}
		private void DataBindAimOwnerLue()
		{
			//OWNER --------------------------------------------------
			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
			aimOwner = AIMManager.GetAIMOwner(true);
			aimOwner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--",
					OwnerName = "--Select--"
				}
			);
			lueAIMOwnerCode.Properties.DataSource = aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerCode, "AIMOwnerID", "OwnerCode");
			lueAIMOwnerName.Properties.DataSource = aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerName, "AIMOwnerID", "OwnerName");

			//lueOwnerCode.Properties.DataSource = aimOwner;			
			lueOwnerName.Properties.DataSource = aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName");

			//OwnerLocation
			List<AIMOwnerLocationInfo> ownerLocation = new List<AIMOwnerLocationInfo>();
			ownerLocation.Insert
			(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					LocationName = "--Select--"
				}
			);

		}
		private void DataBindAimOwnerLocationLue()
		{
			//OWNER LOCATION -----------------------------------------
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);
			aimOwnerLocation.Insert
			(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					LocationCode = "--Select--",
					LocationName = "--Select--"
				}
			);
			lueAIMOwnerLocationName.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerLocationName, "AIMOwnerLocationID", "LocationName");

		}
		private void DataBindAnalyzerOwnerLue()
		{
			
			List<AnalyzerOwnerInfo> analyzerOwner = new List<AnalyzerOwnerInfo>();

			Int32 dataSourceType = 0;
			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataSourceType = (Int32)lueDataSourceType.EditValue;
			}
			if (dataSourceType == 0)
			{
				analyzerOwner.Clear();
			}
			else
			{
				
				analyzerOwner = AIMManager.GetAnalyzerOwner(dataSourceType, true);
			}
			
			analyzerOwner.Insert
			(
				0,
				new AnalyzerOwnerInfo
				{
					AnalyzerOwnerID = 0,
					AnalyzerOwnerCode = "--Select--",
					AnalyzerOwnerName = "--Select--"
				}
			);
			
			lueAnalyzerOwnerCode.Properties.DataSource = analyzerOwner;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerOwnerCode, "AnalyzerOwnerID", "AnalyzerOwnerCode");
			lueAnalyzerOwnerName.Properties.DataSource = analyzerOwner;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");
			lueAnalyzerOwnerName_OL.Properties.DataSource = analyzerOwner;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerOwnerName_OL, "AnalyzerOwnerID", "AnalyzerOwnerName");

			//OwnerLocation			
			List<AnalyzerOwnerLocationInfo> analyzerOwnerLocation = new List<AnalyzerOwnerLocationInfo>();
			analyzerOwnerLocation.Insert
			(
				0,
				new AnalyzerOwnerLocationInfo
				{
					AnalyzerOwnerLocationID = 0,
					AnalyzerOwnerLocationName = "--Select--"
				}
			);


		}
		private void lueAIMOwnerLocationName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMOwnerLocationName, ref lueOwnerName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAnalyzerOwnerCode_EditValueChanged(Object sender, EventArgs e)
		{

			try
			{
				SyncValueLue(ref lueAnalyzerOwnerCode, ref lueAnalyzerOwnerName);
				DataBindLueOwnerLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			
		}		  
		private void lueAnalyzerOwnerName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAnalyzerOwnerName, ref lueAnalyzerOwnerCode);
				DataBindLueOwnerLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}			
		private void lueAnalyzerOwnerName_OL_EditValueChanged(Object sender, EventArgs e)
		{				
			DataBindOwnerLocationLue();
		}		
		private void lueOwnerName_EditValueChanged(Object sender, EventArgs e)
		{	
			DataBindLueOwnerLocation();
		}
		private void DataBindOwnerLocationLue()
		{
			List<AnalyzerOwnerLocationInfo> analyzerOwnerLocation = new List<AnalyzerOwnerLocationInfo>();
			Int32 dataSourceType = 0;
			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataSourceType = (Int32)lueDataSourceType.EditValue;
			}
			if (dataSourceType == 0)
			{
				analyzerOwnerLocation = AIMManager.GetAllAnalyzerOwnerLocation(true);
			}
			else
			{				
				analyzerOwnerLocation = AIMManager.GetAnalyzerOwnerLocation(dataSourceType, (Int32)lueAnalyzerOwnerName_OL.EditValue, true);
			}

			analyzerOwnerLocation.Insert
			(
				0,
				new AnalyzerOwnerLocationInfo
				{
					AnalyzerOwnerLocationID = 0,
					AnalyzerOwnerLocationCode = "--Select--",
					AnalyzerOwnerLocationName = "--Select--"
				}
			);
			lueAnalyzerLocationName.Properties.DataSource = analyzerOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerLocationName, "AnalyzerOwnerLocationID", "AnalyzerOwnerLocationName");

            if (analyzerOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                //1/23/18
                if (lueAnalyzerOwnerName_OL.EditValue == null) { }
                else
                {
                    if ((Int32)lueAnalyzerOwnerName_OL.EditValue == 0)
                    {
                        lueAnalyzerLocationName.EditValue = 0;
                    }
                    else
                    {
                        lueAnalyzerLocationName.EditValue = analyzerOwnerLocation[1].AnalyzerOwnerLocationID;
                    }
                }
            }
            else
            {
				lueAnalyzerLocationName.EditValue = 0; // analyzerOwnerLocation; 11/14/24
            }

        }
		private void DataBindLueOwnerLocation()
		{
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			Int32 ownerID = 0;
			if (lueOwnerName.EditValue != null && lueOwnerName.Text != String.Empty)
			{
				ownerID = (Int32)lueOwnerName.EditValue;
			}
			if (ownerID == 0)
			{
				aimOwnerLocation.Clear();
			}
			else
			{
				aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(ownerID);
			}
			aimOwnerLocation.Insert
			(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					LocationCode = "--Select--",
					LocationName = "--Select--"
				}
			);
			
			lueAIMOwnerLocationName.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerLocationName, "AIMOwnerLocationID", "LocationName");

            if (aimOwnerLocation.Count == 2)
            {
                //selecting value if there is only one
                lueAIMOwnerLocationName.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
            }
            else
            {
				lueAIMOwnerLocationName.EditValue = 0; // aimOwnerLocation; 11/14/24
            }

        }
        private void btnUpdateOwnerLocation_Click(Object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "OWNERLOCATION";
			openCrossRefUpdate();
		}
		#endregion OwnerLocation

		#region SUPPLIER -------------------------------------------------------------------		
		private void DataBindSupplierGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdSupplierView.FocusedRowHandle.ToString(), out rowNum);
			grdSupplier.DataSource = AIMManager.GetAllAIMSupplierRefAnalyzerSupplier();
            grdSupplierView.FocusedRowHandle = rowNum;
			checkfilterSupplier();
		}

		//1/8/21 - commented, not used
		//private void DataBindAimSupplierreplue()
		//{
		//	List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
		//	aimSupplier = AIMManager.GetSupplier(true);
		//	replueAIMNameSupplier.DataSource = aimSupplier;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMNameSupplier, "AIMSupplierID", "SupplierName");
		//	replueAIMNameSupplier.Columns["SupplierName"].Visible = true;
		//}

		//1/8/21 - commented, not used
		//private void DataBindAimSupplierLocationreplue()
		//{
		//	List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
		//	aimSupplier = AIMManager.GetSupplier(true);
		//	replueAIMNameSupplier_SL.DataSource = aimSupplier;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMNameSupplier_SL, "AIMSupplierID", "SupplierName");
		//	replueAIMNameSupplier_SL.Columns["SupplierName"].Visible = true;

		//	List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
		//	aimSupplierLocation = AIMManager.GetAllSupplierLocation(true);
		//	replueAIMNameSupplierLocation.DataSource = aimSupplierLocation;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMNameSupplierLocation, "AIMSupplierLocationID", "SupplierLocationName");
		//	replueAIMNameSupplierLocation.Columns["SupplierLocationName"].Visible = true;

		//}
		private void DataBindAnalyzerSupplierLocationreplue()
		{

			//----------------------------------------------------------
			//12/22/20 - copied from DataBindLueAnalyzerSupplier
			List<AnalyzerSupplierInfo> analyzerSupplier = new List<AnalyzerSupplierInfo>();
			Int32 dataType = 0;

			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataType = (Int32)lueDataSourceType.EditValue;
			}

			if (dataType == 0)
			{
				analyzerSupplier.Clear();
			}
			else
			{
				analyzerSupplier = AIMManager.GetAnalyzerSupplier(dataType, true);
			}

			analyzerSupplier.Insert
			(
				0,
				new AnalyzerSupplierInfo
				{
					AnalyzerSupplierID = 0,
					AnalyzerSupplierCode = "--Select--",
					AnalyzerSupplierName = "--Select--"
				}
			);		
			lueAnalyzerSupplierName_SL.Properties.DataSource = analyzerSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerSupplierName_SL, "AnalyzerSupplierID", "AnalyzerSupplierCode", "AnalyzerSupplierName");
			//----------------------------------------------------------			

			//1/8/21 - commented, not used
			//analyzerSupplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSourceType.EditValue, true);
			//replueAnalyzerNameSupplier_SL.DataSource = analyzerSupplier;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerNameSupplier_SL, "AnalyzerSupplierID", "AnalyzerSupplierName");
			//replueAnalyzerNameSupplier_SL.Columns["AnalyzerSupplierName"].Visible = true;

			//1/8/21 - commented, not used
			//List<AnalyzerSupplierLocationInfo> analyzerSupplierLocation = new List<AnalyzerSupplierLocationInfo>();
			//analyzerSupplierLocation = AIMManager.GetAnalyzerSupplierLocation((Int32)lueDataSourceType.EditValue, true);
			//replueAnalyzerNameSupplierLocation.DataSource = analyzerSupplierLocation;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerNameSupplierLocation, "AnalyzerSupplierLocationID", "AnalyzerSupplierLocationName");
			//replueAnalyzerNameSupplierLocation.Columns["AnalyzerSupplierLocationName"].Visible = true;
		}
		private void DataBindSupplierlue()
		{
			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
			aimSupplier = AIMManager.GetSupplier(true);
			aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"
				}
			);
			lueAIMSupplierCode.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierCode, "AIMSupplierID", "AIMSupplierCode");			

			lueAIMSupplierName.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierName, "AIMSupplierID", "SupplierName");
			lueAIMSupplierName.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			//lueAIMSupplierName_SL.Properties.DataSource = aimSupplier;
			//XtraEditors.LoadLookUpEdit(ref lueAIMSupplierName_SL, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			//lueAIMSupplierName_SL.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			////12/21/20
			//lueAIMSupplierName_Combined.Properties.DataSource = aimSupplier;
			//XtraEditors.LoadLookUpEdit(ref lueAIMSupplierName_Combined, "AIMSupplierID", "SupplierName");
			//lueAIMSupplierName_Combined.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			//1/8/21 commented, not used
			//List<AnalyzerSupplierInfo> AnalyzerSupplier = new List<AnalyzerSupplierInfo>();
			//AnalyzerSupplier = AIMManager.GetAllAnalyzerSupplier(true);
			//_analyzerSupplier = AnalyzerSupplier;
			//replueAnalyzerNameSupplier.DataSource = AnalyzerSupplier;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerNameSupplier, "AnalyzerSupplierID", "AnalyzerSupplierName");
			////replueAnalyzerNameSupplier.Columns["AnalyzerSupplierName"].Visible = true;


		}
		private void DataBindLueAnalyzerSupplier()
		{

			List<AnalyzerSupplierInfo> analyzerSupplier = new List<AnalyzerSupplierInfo>();
			Int32 dataType = 0;

			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataType = (Int32)lueDataSourceType.EditValue;
			}

			if (dataType == 0)
			{
				analyzerSupplier.Clear();
			}
			else
			{
				analyzerSupplier = AIMManager.GetAnalyzerSupplier(dataType, true);
			}

			analyzerSupplier.Insert
			(
				0,
				new AnalyzerSupplierInfo
				{
					AnalyzerSupplierID = 0,
					AnalyzerSupplierCode = "--Select--",
					AnalyzerSupplierName = "--Select--"
				}
			);
			lueAnalyzerSupplierCode.Properties.DataSource = analyzerSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerSupplierCode, "AnalyzerSupplierID", "AnalyzerSupplierCode");

			lueAnalyzerSupplierName.Properties.DataSource = analyzerSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerSupplierName, "AnalyzerSupplierID", "AnalyzerSupplierName");			

			//moved to DataBindAnalyzerSupplierLocationreplue
			//lueAnalyzerSupplierName_SL.Properties.DataSource = analyzerSupplier;
			//XtraEditors.LoadLookUpEdit(ref lueAnalyzerSupplierName_SL, "AnalyzerSupplierID", "AnalyzerSupplierCode", "AnalyzerSupplierName");			

			checkfilterSupplier();


		}
		private void checkfilterSupplier()
		{
			if (lueDataSourceType.EditValue != null)
			{                
                List<AIMSupplierRefAnalyzerSupplierInfo> xref = new List<AIMSupplierRefAnalyzerSupplierInfo>();
                if ((Int32)lueDataSourceType.EditValue != 0)
				{
					xref = AIMManager.GetAIMSupplierRefAnalyzerSupplierDataSourceTypeID((Int32)lueDataSourceType.EditValue);				 
				}
                else
                {                    
                    xref = AIMManager.GetAllAIMSupplierRefAnalyzerSupplier();
                }
                Int32 currentRowNum = 0;
                if (grdSupplierView != null)
                {
                    Int32.TryParse(grdSupplierView.FocusedRowHandle.ToString(), out currentRowNum);
                }
                grdSupplier.DataSource = xref;
                grdSupplierView.FocusedRowHandle = currentRowNum;
            }

            
		}	
		private void btnAddSupplier_Click(object sender, EventArgs e)
		{
			try
			{

				if ((Int32)lueAnalyzerSupplierCode.EditValue == 0)
				{
					MessageBox.Show("Please select Analyzer Supplier.", "Warning...");
					lueAnalyzerSupplierCode.ShowPopup();
					return;
				}
				if ((Int32)lueAIMSupplierCode.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier.", "Warning...");
					lueAIMSupplierCode.ShowPopup();
					return;
				}

				if ((Int32)lueAnalyzerSupplierName.EditValue == 0)
				{
					MessageBox.Show("Please select Analyzer Supplier.", "Warning...");
					lueAnalyzerSupplierName.ShowPopup();
					return;
				}
				if ((Int32)lueAIMSupplierName.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier.", "Warning...");
					lueAIMSupplierName.ShowPopup();
					return;
				}
					  
				AIMSupplierRefAnalyzerSupplierInfo supplierRef = new AIMSupplierRefAnalyzerSupplierInfo();
				supplierRef.AIMSupplierID = (Int32)lueAIMSupplierName.EditValue;				
				supplierRef.AnalyzerSupplierID = (Int32)lueAnalyzerSupplierName.EditValue;
				supplierRef.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
				supplierRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref supplierRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				DataBindSupplierGrid();
				lueAIMSupplierName.EditValue = 0;
				lueAnalyzerSupplierName.EditValue = 0;
				lueAIMSupplierCode.EditValue = 0;
				lueAnalyzerSupplierCode.EditValue = 0;

				lueAIMSupplierCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteSupplier_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierRefAnalyzerSupplierInfo supplierRef = new AIMSupplierRefAnalyzerSupplierInfo();
				supplierRef = (AIMSupplierRefAnalyzerSupplierInfo)grdSupplierView.GetFocusedRow();
				AIMManager.DeleteAIMSupplierRefAnalyzerSupplier(supplierRef.AIMSupplierRefAnalyzerSupplierID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindSupplierGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewSupplier_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdSupplier, "Analyzer Supplier Cross Reference");
				CFCAIMCore.Global.previewGridResults(grdSupplier, "Analyzer Supplier Cross Reference");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				AIMSupplierRefAnalyzerSupplierInfo supplierRef = new AIMSupplierRefAnalyzerSupplierInfo();
				supplierRef = (AIMSupplierRefAnalyzerSupplierInfo)grdSupplierView.GetFocusedRow();
				supplierRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (supplierRef.AIMSupplierID == 0 || supplierRef.AnalyzerSupplierID == 0)
				{
					MessageBox.Show("Need to select both AIM Supplier and Analyzer Supplier. Information is not saved", "Warning...");
					return;
				}

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref supplierRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindSupplierGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private DataView filterSupplier = null;
		private void grdSupplierView_HiddenEditor(object sender, EventArgs e)
		{
			if (filterSupplier != null)
			{
				filterSupplier.Dispose();
				filterSupplier = null;
			}
		}
		private void grdSupplierView_ShownEditor(object sender, EventArgs e)
		{
			{
				List<AIMSupplierRefAnalyzerSupplierInfo> x = new List<AIMSupplierRefAnalyzerSupplierInfo>();
				GridView view = (GridView)sender;

				if (view.FocusedColumn.FieldName == "AnalyzerSupplierID" && view.ActiveEditor is DevExpress.XtraEditors.LookUpEdit)
				{
					LookUpEdit edit = (LookUpEdit)view.ActiveEditor;
					x = (List<AIMSupplierRefAnalyzerSupplierInfo>)view.DataSource;

					List<AnalyzerSupplierInfo> filter = new List<AnalyzerSupplierInfo>();
					foreach (var item in _analyzerSupplier)
					{
						if (item.DataSourceTypeID == x[view.FocusedRowHandle].DataSourceTypeID)
						{
							filter.Add(item);
						}
					}
					edit.Properties.DataSource = filter;
				}
			}
		}
		private void lueAIMSupplierName_SL_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMSupplierName_SL, ref lueAIMSupplierNameOnly_SL);       //task 1133 FG 7/21/2026
				DataBindSupplierLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		//task 1133 FG 7/21/2026 - keep the AIM Supplier Name dropdown in sync with the Code dropdown on Supplier Location tab
		private void lueAIMSupplierNameOnly_SL_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMSupplierNameOnly_SL, ref lueAIMSupplierName_SL);
		}
		private void lueAIMSupplierNameOnly_SL_Click(object sender, EventArgs e)
		{
			lueAIMSupplierNameOnly_SL.SelectAll();
		}
		private void lueAIMSupplierNameOnly_SL_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierNameOnly_SL.SelectAll();
		}
		private void lueAnalyzerSupplierName_SL_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				DataBindAnalyzerSupplierLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMSupplierName_SL_Click(object sender, EventArgs e)
		{
			lueAIMSupplierName_SL.SelectAll();
		}
		private void lueAIMSupplierName_SL_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierName_SL.SelectAll();
		}
		private void lueAnalyzerSupplierName_SL_Click(object sender, EventArgs e)
		{
			lueAnalyzerSupplierName_SL.SelectAll();
		}
		private void lueAnalyzerSupplierName_SL_Enter(object sender, EventArgs e)
		{
			lueAnalyzerSupplierName_SL.SelectAll();
		}
		private void btnUpdateSupplier_Click(Object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "SUPPLIER";
			openCrossRefUpdate();
		}
		#endregion Supplier

		#region SUPPLIER LOCATION ----------------------------------------------------------		
		private void DataBindSupplierLocationGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out rowNum);
			if (lueDataSourceType.EditValue != null)
			{                
                grdSupplierLocation.DataSource = AIMManager.GetAllAIMSupplierLocationRefAnalyzerSupplierLocation();
                grdSupplierLocationView.FocusedRowHandle = rowNum;
			}
            checkfilterSupplierLocation();


        }


		private void DataBindSupplierLocation_Supplierlue()
		{
			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
			aimSupplier = AIMManager.GetSupplier(true);
			aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"
				}
			);			
			lueAIMSupplierName_SL.Properties.DataSource = aimSupplier;
			//task 1133 FG 7/21/2026 - existing dropdown shows Supplier Code; new Name dropdown shows Supplier Name
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierName_SL, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			lueAIMSupplierName_SL.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			lueAIMSupplierNameOnly_SL.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierNameOnly_SL, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
			lueAIMSupplierNameOnly_SL.Properties.Columns.Clear();
			lueAIMSupplierNameOnly_SL.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", "AIM Supplier Name"));
			lueAIMSupplierNameOnly_SL.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMSupplierCode", "AIM Supplier Code"));

		}


		private void DataBindAnalyzerSupplierLocationlue()
		{

			Int32 supplier = 0;
			List<AnalyzerSupplierLocationInfo> analyzerSupplierLocation = new List<AnalyzerSupplierLocationInfo>();
			if (lueAnalyzerSupplierName_SL.EditValue != null)
			{
				Int32.TryParse(lueAnalyzerSupplierName_SL.EditValue.ToString(), out supplier);
			}
			analyzerSupplierLocation = AIMManager.GetAnalyzerSupplierLocation((Int32)lueDataSourceType.EditValue, supplier);
			analyzerSupplierLocation.Insert
			(
				0,
				new AnalyzerSupplierLocationInfo
				{
					AnalyzerSupplierLocationID = 0,
					AnalyzerSupplierLocationCode = "--Select--",
					AnalyzerSupplierLocationName = "--Select--"
				}
			);
			lueAnalyzerSupplierLocationName.Properties.DataSource = analyzerSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerSupplierLocationName, "AnalyzerSupplierLocationID", "AnalyzerSupplierLocationCode", "AnalyzerSupplierLocationName");
			lueAnalyzerSupplierLocationName.Properties.Columns["AnalyzerSupplierLocationName"].Visible = true;

			if (analyzerSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueAnalyzerSupplierLocationName.EditValue = analyzerSupplierLocation[1].AnalyzerSupplierLocationID;
			}
			else
			{
				lueAnalyzerSupplierLocationName.EditValue = 0; // analyzerSupplierLocation; 11/14/24
			}
			
            checkfilterSupplierLocation();


        }
		private void DataBindSupplierLocationlue()
		{
			Int32 supplier = 0;

			//------------------------------------------------------
			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			if (lueAIMSupplierName_SL.EditValue != null)
			{
				Int32.TryParse(lueAIMSupplierName_SL.EditValue.ToString(), out supplier);
			}
			aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier);
			aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					AIMSupplierLocationCode = "--Select--",
					SupplierLocationName = "--Select--"
				}
			);
			lueAIMSupplierLocationName.Properties.DataSource = aimSupplierLocation;
			//task 1133 FG 7/21/2026 - existing dropdown shows Location Code; new Name dropdown shows Location Name
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierLocationName, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");			
			lueAIMSupplierLocationName.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			lueAIMSupplierLocationNameOnly.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierLocationNameOnly, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");
			lueAIMSupplierLocationNameOnly.Properties.Columns.Clear();
			lueAIMSupplierLocationNameOnly.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierLocationName", "AIM Supplier Location Name"));
			lueAIMSupplierLocationNameOnly.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMSupplierLocationCode", "AIM Supplier Location Code"));

			//12/21/20
			lueAIMSupplierLocationNameCombined.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierLocationNameCombined, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
			lueAIMSupplierLocationNameCombined.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			//task 1133 FG 7/21/2026 - Name dropdown for Combined AIM Supplier Location
			lueAIMSupplierLocationNameOnlyCombined.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierLocationNameOnlyCombined, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");
			lueAIMSupplierLocationNameOnlyCombined.Properties.Columns.Clear();
			lueAIMSupplierLocationNameOnlyCombined.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierLocationName", "AIM Supplier Location Name"));
			lueAIMSupplierLocationNameOnlyCombined.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMSupplierLocationCode", "AIM Supplier Location Code"));

			if (aimSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueAIMSupplierLocationName.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;

				//12/21/20
				lueAIMSupplierLocationNameCombined.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueAIMSupplierLocationName.EditValue = 0; // aimSupplierLocation; 11/14/24

				//12/21/20
				lueAIMSupplierLocationNameCombined.EditValue = 0; // aimSupplierLocation; 11/14/24

			}

			//------------------------------------------------------
			List<AnalyzerSupplierLocationInfo> analyzerSupplierLocation = new List<AnalyzerSupplierLocationInfo>();
			if (lueAnalyzerSupplierName_SL.EditValue != null)
			{
				Int32.TryParse(lueAnalyzerSupplierName_SL.EditValue.ToString(), out supplier);
			}
						
		}
		private void checkfilterSupplierLocation()
		{
			if (lueDataSourceType.EditValue != null)
			{                
                List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> xref = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
                if ((Int32)lueDataSourceType.EditValue != 0)
				{                
                    xref = AIMManager.GetAIMSupplierLocationRefAnalyzerSupplierLocation((Int32)lueDataSourceType.EditValue);				
                }

                else
                {                    
                    xref = AIMManager.GetAllAIMSupplierLocationRefAnalyzerSupplierLocation();
                }				
                Int32 currentRowNum = 0;
                if (grdSupplierLocationView != null)
                {
                    Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out currentRowNum);
                }
                grdSupplierLocation.DataSource = xref;
                grdSupplierLocationView.FocusedRowHandle = currentRowNum;
            }
		}
		private void btnPreviewSupplierLocation_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdSupplierLocation, "Analyzer Supplier Location Cross Reference");
				CFCAIMCore.Global.previewGridResults(grdSupplierLocation, "Analyzer Supplier Location Cross Reference");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddSupplierLocation_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueAIMSupplierName_SL.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier.", "Warning...");
					lueAIMSupplierName_SL.ShowPopup();
					return;
				}
				if ((Int32)lueAIMSupplierLocationName.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier Location.", "Warning...");
					lueAIMSupplierLocationName.ShowPopup();
					return;
				}
				if ((Int32)lueAnalyzerSupplierName_SL.EditValue == 0)
				{
					MessageBox.Show("Please select Analyzer Supplier.", "Warning...");
					lueAnalyzerSupplierName_SL.ShowPopup();
					return;
				}
				if ((Int32)lueAnalyzerSupplierLocationName.EditValue == 0)
				{
					MessageBox.Show("Please select Analyzer Supplier Location.", "Warning...");
					lueAnalyzerSupplierLocationName.ShowPopup();
					return;
				}

				AIMSupplierLocationRefAnalyzerSupplierLocationInfo supplierLocRef = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
				supplierLocRef.AIMSupplierID = (Int32)lueAIMSupplierName_SL.EditValue;
				supplierLocRef.AIMSupplierLocationID = (Int32)lueAIMSupplierLocationName.EditValue;
				supplierLocRef.AnalyzerSupplierID = (Int32)lueAnalyzerSupplierName_SL.EditValue;
				supplierLocRef.AnalyzerSupplierLocationID = (Int32)lueAnalyzerSupplierLocationName.EditValue;
				supplierLocRef.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
				supplierLocRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref supplierLocRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				DataBindSupplierLocationGrid();

				//clear out only the supplier-locations, not the suppliers.				
				lueAIMSupplierLocationName.EditValue = 0;
				lueAIMSupplierLocationNameOnly.EditValue = 0;     //task 1133 FG 7/21/2026
				lueAnalyzerSupplierLocationName.EditValue = 0;				

				lueAIMSupplierLocationName.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteSupplierLocation_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierLocationRefAnalyzerSupplierLocationInfo Ref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
				Ref = (AIMSupplierLocationRefAnalyzerSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
				AIMManager.DeleteAIMSupplierLocationRefAnalyzerSupplierLocation(Ref.AIMSupplierLocationRefAnalyzerSupplierLocationID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindSupplierLocationGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void lueAIMSupplierLocationName_Click(object sender, EventArgs e)
		{
			lueAIMSupplierLocationName.SelectAll();
		}
		private void lueAIMSupplierLocationName_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierLocationName.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the AIM Supplier Location Code and Name dropdowns in sync
		private void lueAIMSupplierLocationName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMSupplierLocationName, ref lueAIMSupplierLocationNameOnly);
		}
		private void lueAIMSupplierLocationNameOnly_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMSupplierLocationNameOnly, ref lueAIMSupplierLocationName);
		}
		private void lueAIMSupplierLocationNameOnly_Click(object sender, EventArgs e)
		{
			lueAIMSupplierLocationNameOnly.SelectAll();
		}
		private void lueAIMSupplierLocationNameOnly_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierLocationNameOnly.SelectAll();
		}
		private void lueAnalyzerSupplierLocationName_Click(object sender, EventArgs e)
		{
			lueAnalyzerSupplierLocationName.SelectAll();
		}
		private void lueAnalyzerSupplierLocationName_Enter(object sender, EventArgs e)
		{
			lueAnalyzerSupplierLocationName.SelectAll();
		}
		private void btnUpdateSupplierLocation_Click(Object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "SUPPLIERLOCATION";
			openCrossRefUpdate();
		}
		#endregion SupplierLocation        
				
		#region SUPPLIER COMBINED ----------------------------------------------------------
		private void DataBindSupplierLocationCombinedGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdSupplierLocationCombinedView.FocusedRowHandle.ToString(), out rowNum);
			if (lueDataSourceType.EditValue != null)
			{
				grdSupplierLocationCombined.DataSource = AIMManager.GetAllAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined();
				grdSupplierLocationCombinedView.FocusedRowHandle = rowNum;
			}
			checkfilterSupplierLocationCombined();
		}
		private void DataBindAnalyzerSupplierLocationCombined_Supplierlue()
		{
			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
			aimSupplier = AIMManager.GetSupplier(true);
			aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"
				}
			);
			
			//12/21/20
			//task 1133 FG 7/21/2026 - existing dropdown shows Supplier Code; new Name dropdown shows Supplier Name
			lueAIMSupplierName_Combined.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierName_Combined, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			lueAIMSupplierName_Combined.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			lueAIMSupplierNameOnly_Combined.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierNameOnly_Combined, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
			lueAIMSupplierNameOnly_Combined.Properties.Columns.Clear();
			lueAIMSupplierNameOnly_Combined.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", "AIM Supplier Name"));
			lueAIMSupplierNameOnly_Combined.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMSupplierCode", "AIM Supplier Code"));
		}
		private void DataBindAnalyzerSupplierLocationCombinedlue()
		{
			List<AnalyzerSupplierLocationCombinedInfo> analyzerSupplierLocationCombined = new List<AnalyzerSupplierLocationCombinedInfo>();
			analyzerSupplierLocationCombined = AIMManager.GetAnalyzerSupplierLocationCombined((Int32)lueDataSourceType.EditValue);
			analyzerSupplierLocationCombined.Insert
			(
				0,
				new AnalyzerSupplierLocationCombinedInfo
				{
					AnalyzerSupplierLocationCombinedID = 0,
					AnalyzerSupplierLocationCombinedName = "--Select--"
				}
			);

			lueAnalyzerSupplierLocationNameCombined.Properties.DataSource = analyzerSupplierLocationCombined;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerSupplierLocationNameCombined, "AnalyzerSupplierLocationCombinedID", "AnalyzerSupplierLocationCombinedName");
			lueAnalyzerSupplierLocationNameCombined.Properties.Columns["AnalyzerSupplierLocationCombinedName"].Visible = true;

			if (analyzerSupplierLocationCombined.Count == 2)
			{
				//selecting value if there is only one
				lueAnalyzerSupplierLocationNameCombined.EditValue = analyzerSupplierLocationCombined[1].AnalyzerSupplierLocationCombinedID;
			}
			else
			{
				lueAnalyzerSupplierLocationNameCombined.EditValue = 0; // analyzerSupplierLocationCombined; 11/14/24
			}

			checkfilterSupplierLocationCombined();
		}
		private void DataBindSupplierLocationCombinedlue()
		{
			Int32 supplier = 0;

			//------------------------------------------------------
			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			if (lueAIMSupplierName_Combined.EditValue != null)
			{
				Int32.TryParse(lueAIMSupplierName_Combined.EditValue.ToString(), out supplier);
			}
			aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier);
			aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					AIMSupplierLocationCode = "--Select--",
					SupplierLocationName = "--Select--"
				}
			);
			lueAIMSupplierLocationNameCombined.Properties.DataSource = aimSupplierLocation;
			//task 1133 FG 7/21/2026 - existing dropdown shows Location Code; new Name dropdown shows Location Name
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierLocationNameCombined, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
			lueAIMSupplierLocationNameCombined.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			lueAIMSupplierLocationNameOnlyCombined.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMSupplierLocationNameOnlyCombined, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");
			lueAIMSupplierLocationNameOnlyCombined.Properties.Columns.Clear();
			lueAIMSupplierLocationNameOnlyCombined.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierLocationName", "AIM Supplier Location Name"));
			lueAIMSupplierLocationNameOnlyCombined.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMSupplierLocationCode", "AIM Supplier Location Code"));

			if (aimSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueAIMSupplierLocationNameCombined.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueAIMSupplierLocationNameCombined.EditValue = 0; // aimSupplierLocation; 11/14/24
			}



		}
		private void checkfilterSupplierLocationCombined()
		{
			if (lueDataSourceType.EditValue != null)
			{
				List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> xref = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
				if ((Int32)lueDataSourceType.EditValue != 0)
				{
					xref = AIMManager.GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined((Int32)lueDataSourceType.EditValue);
				}

				else
				{
					xref = AIMManager.GetAllAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined();
				}
				Int32 currentRowNum = 0;
				if (grdSupplierLocationCombinedView != null)
				{
					Int32.TryParse(grdSupplierLocationCombinedView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdSupplierLocationCombined.DataSource = xref;
				grdSupplierLocationCombinedView.FocusedRowHandle = currentRowNum;
			}
		}
		private void btnPreviewSupplierLocationCombined_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdSupplierLocationCombined, "Analyzer Supplier Location (combined) Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddSupplierLocationCombined_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueAIMSupplierName_Combined.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier.", "Warning...");
					lueAIMSupplierName_Combined.ShowPopup();
					return;
				}
				if ((Int32)lueAIMSupplierLocationNameCombined.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier Location.", "Warning...");
					lueAIMSupplierLocationNameCombined.ShowPopup();
					return;
				}
				if ((Int32)lueAnalyzerSupplierLocationNameCombined.EditValue == 0)
				{
					MessageBox.Show("Please select Analyzer Supplier Location.", "Warning...");
					lueAnalyzerSupplierLocationNameCombined.ShowPopup();
					return;
				}

				AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo supplierLocRef = new AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo();
				supplierLocRef.AIMSupplierID = (Int32)lueAIMSupplierName_Combined.EditValue;
				supplierLocRef.AIMSupplierLocationID = (Int32)lueAIMSupplierLocationNameCombined.EditValue;
				supplierLocRef.AnalyzerSupplierLocationCombinedID = (Int32)lueAnalyzerSupplierLocationNameCombined.EditValue;
				supplierLocRef.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;
				supplierLocRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(ref supplierLocRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				DataBindSupplierLocationCombinedGrid();
				//DataBindAimSupplierLocationCombinedreplue();						//1/8/21 commented

				//clear out only the supplier-locations, not the suppliers.				
				lueAIMSupplierLocationNameCombined.EditValue = 0;
				lueAIMSupplierLocationNameOnlyCombined.EditValue = 0;     //task 1133 FG 7/21/2026
				lueAnalyzerSupplierLocationNameCombined.EditValue = 0;

				lueAIMSupplierLocationNameCombined.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteSupplierLocationCombined_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo Ref = new AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo();
				Ref = (AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo)grdSupplierLocationCombinedView.GetFocusedRow();
				AIMManager.DeleteAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(Ref.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindSupplierLocationCombinedGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMSupplierName_Combined_Click(object sender, EventArgs e)
		{
			lueAIMSupplierName_Combined.SelectAll();
		}
		private void lueAIMSupplierName_Combined_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierName_Combined.SelectAll();
		}
		private void lueAIMSupplierLocationNameCombined_Click(object sender, EventArgs e)
		{
			lueAIMSupplierLocationNameCombined.SelectAll();
		}
		private void lueAIMSupplierLocationNameCombined_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierLocationNameCombined.SelectAll();
		}
		private void lueAIMSupplierLocationNameCombined_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMSupplierLocationNameCombined, ref lueAIMSupplierLocationNameOnlyCombined);     //task 1133 FG 7/21/2026
		}
		//task 1133 FG 7/21/2026 - keep Combined AIM Supplier / Location Name dropdowns in sync with Code
		private void lueAIMSupplierNameOnly_Combined_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMSupplierNameOnly_Combined, ref lueAIMSupplierName_Combined);
		}
		private void lueAIMSupplierNameOnly_Combined_Click(object sender, EventArgs e)
		{
			lueAIMSupplierNameOnly_Combined.SelectAll();
		}
		private void lueAIMSupplierNameOnly_Combined_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierNameOnly_Combined.SelectAll();
		}
		private void lueAIMSupplierLocationNameOnlyCombined_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMSupplierLocationNameOnlyCombined, ref lueAIMSupplierLocationNameCombined);
		}
		private void lueAIMSupplierLocationNameOnlyCombined_Click(object sender, EventArgs e)
		{
			lueAIMSupplierLocationNameOnlyCombined.SelectAll();
		}
		private void lueAIMSupplierLocationNameOnlyCombined_Enter(object sender, EventArgs e)
		{
			lueAIMSupplierLocationNameOnlyCombined.SelectAll();
		}
		private void lueAnalyzerSupplierLocationNameCombined_Click(object sender, EventArgs e)
		{
			lueAnalyzerSupplierLocationNameCombined.SelectAll();
		}
		private void lueAnalyzerSupplierLocationNameCombined_Enter(object sender, EventArgs e)
		{
			lueAnalyzerSupplierLocationNameCombined.SelectAll();
		}

		//1/8/21 - commented, not used
		//private void DataBindAimSupplierLocationCombinedreplue()
		//{
		//	List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
		//	aimSupplier = AIMManager.GetSupplier(true);
		//	replueAIMNameSupplier_Combined.DataSource = aimSupplier;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMNameSupplier_Combined, "AIMSupplierID", "SupplierName");
		//	replueAIMNameSupplier_Combined.Columns["SupplierName"].Visible = true;

		//	List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
		//	aimSupplierLocation = AIMManager.GetAllSupplierLocation(true);
		//	replueAIMNameSupplierLocationCombined.DataSource = aimSupplierLocation;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAIMNameSupplierLocationCombined, "AIMSupplierLocationID", "SupplierLocationName");
		//	replueAIMNameSupplierLocationCombined.Columns["SupplierLocationName"].Visible = true;

		//	List<AnalyzerSupplierLocationCombinedInfo> analyzer = new List<AnalyzerSupplierLocationCombinedInfo>();
		//	analyzer = AIMManager.GetAllAnalyzerSupplierLocationCombined(true);
		//	replueAnalyzerNameSupplierLocationCombined.DataSource = analyzer;
		//	XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerNameSupplierLocationCombined, "AnalyzerSupplierLocationCombinedID", "AnalyzerSupplierLocationCombinedName");
		//	replueAnalyzerNameSupplierLocationCombined.Columns["AnalyzerSupplierLocationCombinedName"].Visible = true;
		//}

		private void lueAIMSupplierName_Combined_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMSupplierName_Combined, ref lueAIMSupplierNameOnly_Combined);       //task 1133 FG 7/21/2026
				DataBindSupplierLocationCombinedlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void openCrossRefUpdate()
		{
			//CURRENTLY THIS IS NOT GETTING CALLED BECAUSE "UPDATE" BUTTONS ARE NOT VISIBLE -- ON ANY OF THE TABS


			AIMGlobal.analyzerDataSourceTypeID = (Int32)lueDataSourceType.EditValue;
			AIMGlobal.analyzerDataSourceName = lueDataSourceType.Text;

			//10/6/21 - changed to fix resize issue
			//AnalyzerCrossReferenceMassUpdate update = new AnalyzerCrossReferenceMassUpdate();
			//this.Hide();
			//update.WindowState = FormWindowState.Normal;							
			//update.ShowDialog();
			//this.Show();
			AnalyzerCrossReferenceMassUpdate update = new AnalyzerCrossReferenceMassUpdate();
			update.ShowDialog();


			if (AIMGlobal.analyzerRequiredField == "MATERIAL")
			{
				DataBindMaterialGrid();
			}
			if (AIMGlobal.analyzerRequiredField == "OWNER")
			{
				DataBindOwnerGrid();
			}
			if (AIMGlobal.analyzerRequiredField == "OWNERLOCATION")
			{
				DataBindOwnerLocationGrid();
			}
			if (AIMGlobal.analyzerRequiredField == "SUPPLIER")
			{
				DataBindSupplierGrid();
			}
			if (AIMGlobal.analyzerRequiredField == "SUPPLIERLOCATION")
			{
				DataBindSupplierLocationGrid();
			}


		}
		#endregion


		private void DataBindAnalyzerAndAIMOwnerLocationLues()
		{

			//Int32 datatype = 0;		//1/8/21 commented, not used

			//copied from databindanalyzerownerlue

			List<AnalyzerOwnerInfo> analyzerOwner = new List<AnalyzerOwnerInfo>();

			Int32 dataSourceType = 0;
			if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			{
				dataSourceType = (Int32)lueDataSourceType.EditValue;
			}
			if (dataSourceType == 0)
			{
				analyzerOwner.Clear();
			}
			else
			{
				analyzerOwner = AIMManager.GetAnalyzerOwner(dataSourceType, true);
			}

			analyzerOwner.Insert
			(
				0,
				new AnalyzerOwnerInfo
				{
					AnalyzerOwnerID = 0,
					AnalyzerOwnerCode = "--Select--",
					AnalyzerOwnerName = "--Select--"
				}
			);

			lueAnalyzerOwnerName_OL.Properties.DataSource = analyzerOwner;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerOwnerName_OL, "AnalyzerOwnerID", "AnalyzerOwnerName");

			//--------------------------------------------------------

			//1/8/21 - commented, not used
			////OWNER --------------------------------------------------
			//List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
			//aimOwner = AIMManager.GetAIMOwner(true);
			//replueAimOwnerCode.DataSource = aimOwner;
			//XtraEditors.LoadGridLookUpEdit(ref replueAimOwnerCode, "AIMOwnerID", "OwnerCode");
			//replueAIMOwnerName.DataSource = aimOwner;
			//XtraEditors.LoadGridLookUpEdit(ref replueAIMOwnerName, "AIMOwnerID", "OwnerName");

			//1/8/21 - commented, not used
			////OWNER LOCATION -----------------------------------------
			//List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			//aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);
			//replueAimOwnerLocationCode.DataSource = aimOwnerLocation;
			//XtraEditors.LoadGridLookUpEdit(ref replueAimOwnerLocationCode, "AIMOwnerLocationID", "LocationCode", "LocationName");
			//replueAimOwnerLocationName.DataSource = aimOwnerLocation;
			//XtraEditors.LoadGridLookUpEdit(ref replueAimOwnerLocationName, "AIMOwnerLocationID", "LocationName", "LocationCode");

			//1/8/21 - commented, not used
			////ANALYZER OWNER ------------------------------------------------			 		
			//datatype = 0;
			//if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			//{
			//	datatype = (Int32)lueDataSourceType.EditValue;
			//}
			//if (datatype == 0)
			//{
			//	analyzerOwner = AIMManager.GetAllAnalyzerOwner(true);
			//}
			//else
			//{
			//	analyzerOwner = AIMManager.GetAnalyzerOwner(datatype, true);
			//}			
			//replueAnalyzerOwnerCode.DataSource = analyzerOwner;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerCode, "AnalyzerOwnerID", "AnalyzerOwnerCode");
			//replueAnalyzerOwnerName.DataSource = analyzerOwner;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");

			//1/8/21 - commented, not used
			////ANALYZER OWNER LOCATION ---------------------------------------			
			//List<AnalyzerOwnerLocationInfo> analyzerOwnerLocation = new List<AnalyzerOwnerLocationInfo>();
			//datatype = 0;
			//if (lueDataSourceType.EditValue != null && lueDataSourceType.Text != String.Empty)
			//{
			//	datatype = (Int32)lueDataSourceType.EditValue;
			//}
			//if (datatype == 0)
			//{
			//	analyzerOwnerLocation = AIMManager.GetAllAnalyzerOwnerLocation(true);
			//}
			//else
			//{
			//	analyzerOwnerLocation = AIMManager.GetAnalyzerOwnerLocationByDataSource(datatype);
			//}
			//replueAnalyzerOwnerLocationCode.DataSource = analyzerOwnerLocation;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerLocationCode, "AnalyzerOwnerLocationID", "AnalyzerOwnerLocationCode");
			//replueAnalyzerOwnerLocationName.DataSource = analyzerOwnerLocation;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerLocationName, "AnalyzerOwnerLocationID", "AnalyzerOwnerLocationName");

			//replueAnalyzerOwner_OwnerLocation.DataSource = analyzerOwner;
			//XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwner_OwnerLocation, "AnalyzerOwnerID", "AnalyzerOwnerName");
			//replueAnalyzerOwner_OwnerLocation.Columns["AnalyzerOwnerName"].Visible = true;

		}
		private void lueAIMCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMCode, ref lueAIMName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMName, ref lueAIMCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAnalyzerCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAnalyzerCode, ref lueAnalyzerName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAnalyzerName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAnalyzerName, ref lueAnalyzerCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAnalyzerNameNutrient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAnalyzerNameNutrient, ref lueAnalyzerCodeNutrient);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMCode_Click(object sender, EventArgs e)
		{
			lueAIMCode.SelectAll();
		}
		private void lueAIMCode_Enter(object sender, EventArgs e)
		{
			lueAIMCode.SelectAll();
		}
		private void lueAIMName_Enter(object sender, EventArgs e)
		{
			lueAIMName.SelectAll();
		}
		private void lueAIMName_Click(object sender, EventArgs e)
		{
			lueAIMName.SelectAll();
		}
		private void lueAnalyzerCode_Click(object sender, EventArgs e)
		{
			lueAnalyzerCode.SelectAll();
		}
		private void lueAnalyzerCode_Enter(object sender, EventArgs e)
		{
			lueAnalyzerCode.SelectAll();
		}
		private void lueAnalyzerName_Enter(object sender, EventArgs e)
		{
			lueAnalyzerName.SelectAll();
		}
		private void lueAnalyzerName_Click(object sender, EventArgs e)
		{
			lueAnalyzerName.SelectAll();
		}
		private void btnResize_Click(Object sender, EventArgs e)
		{	            
            WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}
		private void AnalyzerCrossReference_SizeChanged(Object sender, EventArgs e)
		{            
            btnResize.Visible = true;
		}       
        private Boolean checkUserDefinedSwitch()
        {
            Boolean rtn = false;

            DataSourceTypeInfo type = new DataSourceTypeInfo();
            type = AIMManager.GetDataSourceType_DataSourceTypeID((Int32)lueDataSourceType.EditValue);
            if (type != null)
            {
                if (type.IsUserDefined == true)
                {
                    rtn = true;
                }
            }

            return rtn;
        }
		private void lblmaterialCode_Click(Object sender, EventArgs e)
		{
			//open AIM maintenance, set tab to material
			try
			{
                AIMGlobal.aimMaterialTab = true;
				AIMGlobal.open_form(aimMaintenance = new AIM());				
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}
		private void lblnutrientCode_Click(Object sender, EventArgs e)
		{
			//open AIM maintenance, set tab to nutrient
			try
			{
				AIMGlobal.aimNutrientTab = true;
				AIMGlobal.open_form(aimMaintenance = new AIM());             
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}
        private void grdSupplierLocation_Click(Object sender, EventArgs e)
        {

        }
		private void lueAnalyzerSupplierCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAnalyzerSupplierCode, ref lueAnalyzerSupplierName);				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAnalyzerSupplierName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAnalyzerSupplierName, ref lueAnalyzerSupplierCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMSupplierName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMSupplierName, ref lueAIMSupplierCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMSupplierCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMSupplierCode, ref lueAIMSupplierName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
        private void lueAnalyzerCodeNutrient_Click(Object sender, EventArgs e)
        {
            lueAnalyzerCodeNutrient.SelectAll();
        }
        private void lueAnalyzerCodeNutrient_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerCodeNutrient.SelectAll();
        }
        private void lueAnalyzerNameNutrient_Click(Object sender, EventArgs e)
        {
            lueAnalyzerNameNutrient.SelectAll();
        }
        private void lueAnalyzerNameNutrient_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerNameNutrient.SelectAll();
        }
        private void lueAIMCodeNutrient_Click(Object sender, EventArgs e)
        {
            lueAIMCodeNutrient.SelectAll();
        }
        private void lueAIMCodeNutrient_Enter(Object sender, EventArgs e)
        {
            lueAIMCodeNutrient.SelectAll();
        }
        private void lueAIMNameNutrient_Click(Object sender, EventArgs e)
        {
            lueAIMNameNutrient.SelectAll();
        }
        private void lueAIMNameNutrient_Enter(Object sender, EventArgs e)
        {
            lueAIMNameNutrient.SelectAll();
        }
        private void lueAnalyzerOwnerCode_Click(Object sender, EventArgs e)
        {
            lueAnalyzerOwnerCode.SelectAll();
        }
        private void lueAnalyzerOwnerCode_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerOwnerCode.SelectAll();
        }
        private void lueAnalyzerOwnerName_Click(Object sender, EventArgs e)
        {
            lueAnalyzerOwnerName.SelectAll();
        }
        private void lueAnalyzerOwnerName_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerOwnerName.SelectAll();
        }
        private void lueAIMOwnerCode_Click(Object sender, EventArgs e)
        {
            lueAIMOwnerCode.SelectAll();
        }
        private void lueAIMOwnerCode_Enter(Object sender, EventArgs e)
        {
            lueAIMOwnerCode.SelectAll();
        }
        private void lueAIMOwnerName_Click(Object sender, EventArgs e)
        {
            lueAIMOwnerName.SelectAll();
        }
        private void lueAIMOwnerName_Enter(Object sender, EventArgs e)
        {
            lueAIMOwnerName.SelectAll();
        }
        private void lueAnalyzerOwnerName_OL_Click(Object sender, EventArgs e)
        {
            lueAnalyzerOwnerName_OL.SelectAll();
        }
        private void lueAnalyzerOwnerName_OL_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerOwnerName_OL.SelectAll();
        }
        private void lueAnalyzerLocationName_Click(Object sender, EventArgs e)
        {
            lueAnalyzerLocationName.SelectAll();
        }
        private void lueAnalyzerLocationName_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerLocationName.SelectAll();
        }
        private void lueOwnerName_Click(Object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueOwnerName_Enter(Object sender, EventArgs e)
        {
            lueOwnerName.SelectAll();
        }
        private void lueAIMOwnerLocationName_Click(Object sender, EventArgs e)
        {
            lueAIMOwnerLocationName.SelectAll();
        }
        private void lueAIMOwnerLocationName_Enter(Object sender, EventArgs e)
        {
            lueAIMOwnerLocationName.SelectAll();
        }
        private void lueAnalyzerSupplierCode_Click(Object sender, EventArgs e)
        {
            lueAnalyzerSupplierCode.SelectAll();
        }
        private void lueAnalyzerSupplierCode_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerSupplierCode.SelectAll();
        }
        private void lueAnalyzerSupplierName_Click(Object sender, EventArgs e)
        {
            lueAnalyzerSupplierName.SelectAll();
        }
        private void lueAnalyzerSupplierName_Enter(Object sender, EventArgs e)
        {
            lueAnalyzerSupplierName.SelectAll();
        }
        private void lueAIMSupplierCode_Click(Object sender, EventArgs e)
        {
            lueAIMSupplierCode.SelectAll();
        }
        private void lueAIMSupplierCode_Enter(Object sender, EventArgs e)
        {
            lueAIMSupplierCode.SelectAll();
        }
        private void lueAIMSupplierName_Click(Object sender, EventArgs e)
        {
            lueAIMSupplierName.SelectAll();
        }
        private void lueAIMSupplierName_Enter(Object sender, EventArgs e)
        {
            lueAIMSupplierName.SelectAll();
        }

		//5/29/19 
		private void grdMaterialRefView_RowCellStyle(object sender, RowCellStyleEventArgs e)		{AIMGlobal.focusedRowGreen(sender, e);}
        private void grdNutrientRefView_RowCellStyle(object sender, RowCellStyleEventArgs e)		{AIMGlobal.focusedRowGreen(sender, e);}
        private void grdOwnerView_RowCellStyle(object sender, RowCellStyleEventArgs e)				{AIMGlobal.focusedRowGreen(sender, e);}
        private void grdOwnerLocationView_RowCellStyle(object sender, RowCellStyleEventArgs e)		{AIMGlobal.focusedRowGreen(sender, e);}
        private void grdSupplierView_RowCellStyle(object sender, RowCellStyleEventArgs e)			{AIMGlobal.focusedRowGreen(sender, e);}
        private void grdSupplierLocationView_RowCellStyle(object sender, RowCellStyleEventArgs e)	{AIMGlobal.focusedRowGreen(sender, e);}

		////1/8/21 - Test -------------------------------------------------------------
		//private StreamWriter openStreamWriter(String pathAndFileName)
		//{
		//	StreamWriter sw = new StreamWriter(pathAndFileName);
		//	return sw;
		//}
		//private void writeToStreamWriter(StreamWriter sw, String txt)
		//{
		//	sw.WriteLine(DateTime.Now + " " + txt);
		//}
		//private void closeStreamWriter(StreamWriter sw)
		//{
		//	sw.Close();
		//}

		private void AnalyzerCrossReference_FormClosing(object sender, FormClosingEventArgs e)
        {
			//closeStreamWriter(stream);
		}

		//7/11/23
		private void rbIngredient_CheckedChanged(object sender, EventArgs e)
		{
			DataBindMateriallue();
			DataBindMaterialGrid();
		}

		//7/11/23
		private void rbProduct_CheckedChanged(object sender, EventArgs e)
		{
			DataBindMateriallue();
			DataBindMaterialGrid();
		}

		private void lueAIMName_Click_1(object sender, EventArgs e)
		{
			lueAIMName.SelectAll();
		}


		//12/22/20 - COMMENTED AND MOVED TO TABCONTROL EVENT
		//private void DataBindGrids()
		//{

		//	DataBindMateriallue();
		//	DataBindNutrientlue();
		//	DataBindSupplierlue();
		//	DataBindSupplierLocationlue();
		//	DataBindAimOwnerLue();
		//	DataBindOwnerLocationLue();

		//	//12/21/20
		//	DataBindSupplierLocationCombinedlue();

		//	DataBindAimMaterialreplue();
		//	DataBindAimNutrientreplue();
		//	DataBindAimSupplierreplue();
		//	DataBindAimSupplierLocationreplue();			
		//	DataBindAimOwnerreplue();
		//	DataBindAimOwnerLocationreplue();

		//	//12/21/20
		//	DataBindAimSupplierLocationCombinedreplue();

		//	DataBindMaterialGrid();
		//	DataBindNutrientGrid();
		//	DataBindSupplierGrid();
		//	DataBindSupplierLocationGrid();			
		//	DataBindOwnerGrid();			
		//	DataBindOwnerLocationGrid();

		//	//12/21/20
		//	DataBindSupplierLocationCombinedGrid();
		//}

		//12/22/20 - COMMENTED AND MOVED TO TABCONTROL EVENT
		//private void DataBindLues()
		//{
		//	DataBindLueAnalyzerMaterial();
		//	DataBindLueAnalyzerNutrient();
		//	DataBindLueAnalyzerSupplier();
		//	DataBindAnalyzerSupplierLocationreplue();
		//	DataBindAnalyzerOwnerLue();
		//	DataBindAnalyzerAndAIMOwnerLocationLues();
		//	//DataBindAnalyzerOwnerLocationreplue();

		//	//12/21/20
		//	DataBindAnalyzerSupplierLocationCombinedlue();
		//}


		//------------------------------------------------------------------
		//12/22/20 - moved from lueDataSourceType_EditValueChanged to function CheckDataSourceAndHideOrShowButtons
		//            //------------------------------------------------
		//            //if "All" is selected as datasource, do not allow user to ADD 
		//            btnAddMaterial.Enabled = true;               
		//            btnAddNutrient.Enabled = true;
		//            btnAddOwner.Enabled = true;
		//            btnAddOwnerLocation.Enabled = true;
		//            btnAddSupplier.Enabled = true;
		//            btnAddSupplierLocation.Enabled = true;

		////12/21/20
		//btnAddSupplierLocationCombined.Enabled = true;

		//            grdMaterialRefView.Columns.ColumnByName("colDelete").Visible = true;
		//            grdNutrientRefView.Columns.ColumnByName("colDeleteNutrient").Visible = true;
		//            grdOwnerView.Columns.ColumnByName("colIsActive").Visible = true;
		//            grdOwnerLocationView.Columns.ColumnByName("colDeleteOwnerLocation").Visible = true;
		//            grdSupplierView.Columns.ColumnByName("colDeleteSupplier").Visible = true;
		//            grdSupplierLocationView.Columns.ColumnByName("colDeleteSupplierLocation").Visible = true;

		//            gbAimMaterial.Enabled = true;
		//            gbAnalyzerMaterial.Enabled = true;
		//            gbNutrient.Enabled = true;
		//            gbAnalyzerNutrient.Enabled = true;
		//            gbAIMOwner.Enabled = true;
		//            gbUserOwner.Enabled = true;
		//            gbAimOwnerLocation.Enabled = true;
		//            gbUserOwnerLocation.Enabled = true;
		//            gbSupplier.Enabled = true;
		//            gbAnalyzerSupplier.Enabled = true;
		//            gbSupplierLocation.Enabled = true;
		//            gbAnalyzerSupplierLocation.Enabled = true;

		////12/21/20				
		//gbSupplierLocationCombined.Enabled = true;
		//gbAnalyzerSupplierLocationCombined.Enabled = true;

		//if ((Int32)lueDataSourceType.EditValue == 0)
		//            {                   
		//                grdMaterialRefView.Columns.ColumnByName("colDelete").Visible = false;
		//                grdNutrientRefView.Columns.ColumnByName("colDeleteNutrient").Visible = false;
		//                grdOwnerView.Columns.ColumnByName("colIsActive").Visible = false;
		//                grdOwnerLocationView.Columns.ColumnByName("colDeleteOwnerLocation").Visible = false;
		//                grdSupplierView.Columns.ColumnByName("colDeleteSupplier").Visible = false;
		//                grdSupplierLocationView.Columns.ColumnByName("colDeleteSupplierLocation").Visible = false;

		//                btnAddMaterial.Enabled = false;
		//                gbAimMaterial.Enabled = false;
		//                gbAnalyzerMaterial.Enabled = false;

		//                btnAddNutrient.Enabled = false;
		//                gbNutrient.Enabled = false;
		//                gbAnalyzerNutrient.Enabled = false;

		//                btnAddOwner.Enabled = false;
		//                gbAIMOwner.Enabled = false;
		//                gbUserOwner.Enabled = false;

		//                btnAddOwnerLocation.Enabled = false;
		//                gbAimOwnerLocation.Enabled = false;
		//                gbUserOwnerLocation.Enabled = false;

		//                btnAddSupplier.Enabled = false;
		//                gbSupplier.Enabled = false;
		//                gbAnalyzerSupplier.Enabled = false;

		//                btnAddSupplierLocation.Enabled = false;
		//                gbSupplierLocation.Enabled = false;
		//                gbAnalyzerSupplierLocation.Enabled = false;

		//	//12/21/20
		//	btnAddSupplierLocationCombined.Enabled = false;
		//	gbSupplierLocationCombined.Enabled = false;
		//	gbAnalyzerSupplierLocationCombined.Enabled = false;
		//}
		//------------------------------------------------------------------


		//12/22/20 - COMMENTED, NOT USED
		//private void DataBindlue()
		//{			
		//	DataBindAimOwnerLue();					
		//	DataBindAnalyzerOwnerLue();			
		//}

		//12/22/20 - COMMENTED, NOT USED
		//private void DataBindGrid()
		//{
		//	DataBindOwnerGrid();
		//	DataBindOwnerLocationGrid();

		//}


	}
}
