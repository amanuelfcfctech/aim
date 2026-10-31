using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

//these are used for the background worker -- which is not working currently
using System.Diagnostics;
using DevExpress.XtraEditors;
using CFCAIMCore.Entity.Group;
using DevExpress.XtraReports.Templates;
using CFCAIMCore.Entity.Test;

namespace CFCAIM.Maintenance
{
	public partial class AIM : Form
	{
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		public static SqlDatabase _database;
		CFCUser.Global userGlobal = new CFCUser.Global();
		List<AIMSupplierLocationInfo> _aimSupplierLocation = new List<AIMSupplierLocationInfo>();
		List<DataSourceTypeInfo> _dataSourceType = new List<DataSourceTypeInfo>();
		List<SampleTestTypeInfo> _testTypeHeader = new List<SampleTestTypeInfo>();                  //8/9/23
		List<SampleTestTypeDetailInfo> _testTypeDetail = new List<SampleTestTypeDetailInfo>();      //8/9/23
		Int32 _testTypeID = 0;                                                                      //8/9/23		
		Boolean _saveTestTypeAs = false;                                                            //8/9/23        
		Int32 _saveAsCopyTestTypeID = 0;                                                            //8/9/23
		Boolean _allowAIMInactive = true;
		Boolean _tabMaterial = false;
		Boolean _tabStockStatus = false;
		Boolean _tabNutrient = false;
		Boolean _tabOwner = false;
		Boolean _tabOwnerLocation = false;
		Boolean _tabSupplier = false;
		Boolean _tabSupplierLocation = false;
		Boolean _tabGuarantees = false;
		Boolean _tabUnitType = false;
		Boolean _tabWarnings = false;
		Boolean _tabTestType = false;                                                                           //10/6/23
		Boolean _tabGroupOwnerLocation = false;                 //owner-location
		Boolean _tabGroupIngredients = false;                   //SAMPLE ingredient groups
		Boolean _tabGroupProducts = false;                      //SAMPLE product groups							//5/18/23
		Boolean _tabGroupSuppliers = false;                     //supplier groups								//8/30/23
		Boolean _tabGroupSupplierLocations = false;             //supplier-location groups						//8/30/23
		Boolean _tabGroupTestType = false;                      //test type groups								//10/6/23
		Boolean _tabGroupExportIngredients = false;             //export INGREDIENT groups						//10/11/23
		Boolean _loadingExportGroup = true;                                                                     //10/12/23
		Boolean _tabGroupExportPlants = false;                  //export PLANT groups							//1/10/24
		Boolean _loadingExportGroupPlant = true;                                                                //1/11/24
		Boolean _loadingTabOrderGroup = true;                                                                   //Task1041 MW250526
		Boolean _tabAnalyzerPlantGroup = false;                 //ANALYZER plant groups							//Task1114 DB251021
		Boolean _tabNutrientGroup = false;                                                                      //Task1061 DB251027
		Boolean _tabGroupSampleScreenDisplay = false;                                                           //Task1042 FG - Sample Screen Display groups
		Boolean _tabGroupRequiredField = false;                                                                 // task 1155 FG 8/5/26 - Required Field tab
		Boolean _tabTest = false;                                                                               //Task1121 DB251219
		Boolean _tabTestTest = false;                                                                           //Task1121 DB251219
		Boolean _tabTestGroupName = false;                                                                      //Task1121 DB251219        
		Boolean _tabTestMaterial = false;                                                                       //Task1121 DB251219
		Boolean _tabTestNutrientResult = false;                                                                 //Task1121 DB251219
		String _testMaterialLastColumnName = String.Empty;                                                      //Task1121 DB260102
		String _testNutrientResultLastColumnName = String.Empty;												//Task1121 DB260102

        public AIM()
		{
			InitializeComponent();
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
		}

		private void AIM_Load(object sender, EventArgs e)
		{
			AIMGlobal.SplashScreenShow();

			//12/17/20 - added these so grids are populated only once, when entering each tab
			_tabMaterial = false;
			_tabStockStatus = false;
			_tabNutrient = false;
			_tabOwner = false;
			_tabOwnerLocation = false;
			_tabSupplier = false;
			_tabSupplierLocation = false;
			_tabGuarantees = false;
			_tabUnitType = false;
			_tabWarnings = false;
			_tabTestType = false;                       //10/6/23
			_tabGroupOwnerLocation = false;
			_tabGroupIngredients = false;
			_tabGroupProducts = false;                                                         //5/18/23
			_tabGroupSuppliers = false;
			_tabGroupSupplierLocations = false;
			_tabGroupTestType = false;                  //10/6/23
			_tabGroupExportIngredients = false;         //10/11/23
			_tabGroupExportPlants = false;              //1/11/24
			_tabAnalyzerPlantGroup = false;             //Task1114 DB251021
			_tabNutrientGroup = false;
            _tabTest = false;                                                                               //Task1121 DB251219
            _tabTestTest = false;                                                                           //Task1121 DB251219
            _tabTestGroupName = false;                                                                      //Task1121 DB251219        
            _tabTestMaterial = false;                                                                       //Task1121 DB251219
            _tabTestNutrientResult = false;                                                                 //Task1121 DB251219
			

            //1/17/22 - changed which tab gets opened when entering the form -- if anything but the AIMGlobal.aimMaterialTab is set to true, then
            //			open whatever global is set to true.  If the AIMGlobal.aimMaterialTab = true, set AIMGloba.aimOwnerTab = true.
            //			(This is for CSA's speed issue.		
            //2/11/21
            ////AIMGlobal.aimOwnerTab = true;
            if (AIMGlobal.aimMaterialTab == true)
			{
				AIMGlobal.aimOwnerTab = true;
			}

			SystemSettingsInfo systemSettings = new SystemSettingsInfo();
			systemSettings = AIMManager.GetSystemSettings();
			if (systemSettings != null)
			{
				_allowAIMInactive = systemSettings.AllowAIMInactive;
			}

			SetToolTips();
			ClearTextFields();
			CheckGlobalTabs();
			AIMGlobal.SplashScreenHide();
			WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
		}
		private void CheckSampleTable(Int32 resultErrorNumber, String table, Int32 firstID, Int32 secondID)
		{
			//------------------------------------			
			if (resultErrorNumber == 2)
			{
				DialogResult dr = new DialogResult();
				dr = MessageBox.Show("Do you want these samples inactivated?", "Information...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.Yes)
				{
					switch (table)
					{
						case "Owner":
							AIMManager.SaveSampleByOwner(false, firstID);
							break;
						case "OwnerLocation":
							AIMManager.SaveSampleByOwnerAndOwnerLocation(false, firstID, secondID);
							break;
						case "Supplier":
							AIMManager.SaveSampleBySupplier(false, firstID);
							break;
						case "SupplierLocation":
							AIMManager.SaveSampleBySupplierAndSupplierLocation(false, firstID, secondID);
							break;
						default:
							break;
					}
					MessageBox.Show("Complete.", "Information...", MessageBoxButtons.OK);
				}
			}
			//------------------------------------
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
			ttip.SetToolTip(this.btnAddMaterial, "Add Record");
			ttip.SetToolTip(btnPreviewOwner, "Preview Grid");
			ttip.SetToolTip(this.btnAddOwner, "Add Record");
			ttip.SetToolTip(btnPreviewOwnerLocation, "Preview Grid");
			ttip.SetToolTip(this.btnAddOwnerLocation, "Add Record");
			ttip.SetToolTip(btnPreviewSupplier, "Preview Grid");
			ttip.SetToolTip(btnAddSupplier, "Add Record");
			ttip.SetToolTip(btnPreviewSupplierLocation, "Preview Grid");
			ttip.SetToolTip(this.btnAddSupplierLocation, "Add Record");
			ttip.SetToolTip(btnPreviewUnits, "Preview Grid");
			ttip.SetToolTip(this.btnAddUnits, "Add Record");
			ttip.SetToolTip(btnPreviewWarningLimit, "Preview Grid");
			ttip.SetToolTip(btnAddWarningLimit, "Add Record");
			ttip.SetToolTip(btnPreviewGuarantee, "Preview Grid");
			ttip.SetToolTip(this.btnAddGuarantee, "Add Record");
			ttip.SetToolTip(btnPreviewNutrient, "Preview Grid");
			ttip.SetToolTip(btnAddNutrient, "Add Record");
			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

			//11/21/22
			ttip.SetToolTip(btnAddNewGroup, "Add New Sample Ingredient Group");
			ttip.SetToolTip(btnDeleteGroup, "Delete Sample Ingredient Group");
			ttip.SetToolTip(btnAddNewGroupCode, "Add Group");
			ttip.SetToolTip(btnPreviewSpecific, "Preview Grid");
			ttip.SetToolTip(btnAddSpecific, "Add Record");
			ttip.SetToolTip(btnAddNewOwnerLocationGroup, "Add New Owner Location Group");
			ttip.SetToolTip(btnDeleteOwnerLocationGroup, "Delete Owner Location Group");
			ttip.SetToolTip(btnAddNewOwnerLocationGroupCode, "Add Group");
			ttip.SetToolTip(btnPreviewOwnerLocationGroup, "Preview Grid");
			ttip.SetToolTip(btnAddOwnerLocationGroup, "Add Record");

			//5/18/23
			ttip.SetToolTip(btnSaveGroupAs, "Save-As");
			ttip.SetToolTip(btnCancelSaveGroupAs, "Cancel Save-As");
			ttip.SetToolTip(btnAddNewProductGroup, "Add New Sample Product Group");
			ttip.SetToolTip(btnDeleteProductGroup, "Delete Sample Product Group");
			ttip.SetToolTip(btnAddNewProductGroupCode, "Add Group");
			ttip.SetToolTip(btnPreviewSpecificProducts, "Preview Grid");
			ttip.SetToolTip(btnAddSpecificProduct, "Add Record");
			ttip.SetToolTip(btnSaveProductGroupAs, "Save-As");
			ttip.SetToolTip(btnCancelSaveProductGroupAs, "Cancel Save-As");

			//8/30/23
			ttip.SetToolTip(btnPreviewSupplierGroup, "Preview Grid");
			ttip.SetToolTip(btnPreviewSupplierLocationGroup, "Preview Grid");
			ttip.SetToolTip(btnAddNewSupplierGroupCode, "Add New Supplier Group");
			/////ttip.SetToolTip(btnAddNewSupplierLocationGroupCode, "Add New Supplier Location Group");
			ttip.SetToolTip(btnAddSupplierGroup, "Add Supplier to Grid");
			ttip.SetToolTip(btnAddSupplierLocationGroup, "Add Supplier Location to Grid");

			//10/6/23
			ttip.SetToolTip(btnPreviewTestTypeGroup, "Preview Grid");
			ttip.SetToolTip(btnAddNewTestTypeGroupCode, "Add New Test Type Group");
			ttip.SetToolTip(btnAddTestType, "Add Test Type to Grid");
			ttip.SetToolTip(btnTestTypeDelete, "Delete Test Type");
			ttip.SetToolTip(btnTestTypeSaveAs, "Save As");
			ttip.SetToolTip(btnTestTypeAddDatasource, "Add Data Source to Test Type");
			ttip.SetToolTip(btnSaveTestType, "Save Test Type");

			//11/20/23
			ttip.SetToolTip(btnAddNewGroupExport, "Add New Ingredient Group");
			ttip.SetToolTip(btnDeleteGroupExport, "Delete Ingredient Group");
			ttip.SetToolTip(btnSaveAsGroupExport, "Save Ingredient Group As");
			ttip.SetToolTip(btnAddNewGroupCodeExport, "Add Ingredient Group");
			ttip.SetToolTip(btnSaveGroupAsExport, "Save Ingredient Group");
			ttip.SetToolTip(btnAddIngredientExport, "Add Record");
			ttip.SetToolTip(btnPreviewIngredientExport, "Preview Grid");

			//1/11/24
			ttip.SetToolTip(btnAddNewGroupExportPlant, "Add New Plant Group");
			ttip.SetToolTip(btnDeleteGroupExportPlant, "Delete Plant Group");
			ttip.SetToolTip(btnSaveAsGroupExportPlant, "Save Plant Group As");
			ttip.SetToolTip(btnAddNewGroupCodeExportPlant, "Add Plant Group");
			ttip.SetToolTip(btnSaveGroupAsExportPlant, "Save Plant Group");
			ttip.SetToolTip(btnAddPlantExport, "Add Record");
			ttip.SetToolTip(btnPreviewPlantExport, "Preview Grid");


		}
		private void ClearTextFields()
		{
			txtMaterialCode.Text = String.Empty;
			txtMaterialName.Text = String.Empty;
			txtOwnerCode.Text = String.Empty;
			txtOwnerName.Text = String.Empty;
			txtOwnerLocationCode.Text = String.Empty;
			txtOwnerLocationName.Text = String.Empty;
			txtSupplierName.Text = String.Empty;
			txtSupplierLocationCode.Text = String.Empty;
			txtSupplierLocationName.Text = String.Empty;
			txtUnitTypeCode.Text = String.Empty;
			txtUnitTypeDescription.Text = String.Empty;
			txtMax.Text = String.Empty;
			txtMin.Text = String.Empty;
			txtMaxGuarantee.Text = String.Empty;
			txtMinGuarantee.Text = String.Empty;
			txtTarget.Text = String.Empty;
			txtNutrientCode.Text = String.Empty;
			txtNutrientName.Text = String.Empty;
			txtShortNameNutrient.Text = String.Empty;

			rbActiveRecords.Visible = true;
			rbDeletedRecords.Visible = true;
			if (tabControl1.SelectedIndex == 8)
			{
				rbActiveRecords.Visible = false;
				rbDeletedRecords.Visible = false;
			}

			//7/26/19
			lueSupplierLocationCode.Text = String.Empty;
			lueSupplierLocationName.Text = String.Empty;

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
		private void rbActiveRecords_CheckedChanged(object sender, EventArgs e)
		{
			try
			{

				if (rbActiveRecords.Checked)
				{
					repbtnDeleteMaterial.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteMaterial.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteNutrient.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteNutrient.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteOwner.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteOwner.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteOwnerLocation.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteOwnerLocation.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteSupplier.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteSupplier.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteSupplierLocation.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteSupplierLocation.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteGuarantee.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteGuarantee.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteUnits.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteUnits.Buttons[0].ToolTip = "Inactivate record";
					repbtnDeleteMaterialAgain.Buttons[0].Image = Properties.Resources.delete;
					repbtnDeleteMaterialAgain.Buttons[0].ToolTip = "Inactivate record";

					txtMaterialCode.Enabled = true;
					txtMaterialName.Enabled = true;
					txtNutrientCode.Enabled = true;
					txtNutrientName.Enabled = true;
					txtShortNameNutrient.Enabled = true;
					txtOwnerCode.Enabled = true;
					txtOwnerName.Enabled = true;
					txtOwnerLocationCode.Enabled = true;
					txtOwnerLocationName.Enabled = true;
					txtSupplierCode.Enabled = true;
					txtSupplierName.Enabled = true;
					txtSupplierLocationCode.Enabled = true;
					txtSupplierLocationName.Enabled = true;
					txtUnitTypeCode.Enabled = true;
					txtUnitTypeDescription.Enabled = true;

					lueMaterialType.Enabled = true;
					lueUnitType.Enabled = true;
					luePriority.Enabled = true;
					lueMethod.Enabled = true;                       //5/29/24
					lueOwner.Enabled = true;
					lueSupplier.Enabled = true;
					lueSupplierName.Enabled = true;     //task 1133 FG 7/21/2026

					btnAddMaterial.Enabled = true;
					btnAddNutrient.Enabled = true;
					btnAddOwner.Enabled = true;
					btnAddOwnerLocation.Enabled = true;
					btnAddSupplier.Enabled = true;
					btnAddSupplierLocation.Enabled = true;
					btnAddUnits.Enabled = true;

					//7/26/19
					lueSupplierLocationCode.Enabled = true;
					lueSupplierLocationName.Enabled = true;
				}
				else
				{
					repbtnDeleteMaterial.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteMaterial.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteNutrient.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteNutrient.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteOwner.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteOwner.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteOwnerLocation.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteOwnerLocation.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteSupplier.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteSupplier.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteSupplierLocation.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteSupplierLocation.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteGuarantee.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteGuarantee.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteUnits.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteUnits.Buttons[0].ToolTip = "Re-activate record";
					repbtnDeleteMaterialAgain.Buttons[0].Image = Properties.Resources.Undo_icon;
					repbtnDeleteMaterialAgain.Buttons[0].ToolTip = "Re-activate record";

					txtMaterialCode.Enabled = false;
					txtMaterialName.Enabled = false;
					txtNutrientCode.Enabled = false;
					txtNutrientName.Enabled = false;
					txtShortNameNutrient.Enabled = false;
					txtOwnerCode.Enabled = false;
					txtOwnerName.Enabled = false;
					txtOwnerLocationCode.Enabled = false;
					txtOwnerLocationName.Enabled = false;
					txtSupplierCode.Enabled = false;
					txtSupplierName.Enabled = false;
					txtSupplierLocationCode.Enabled = false;
					txtSupplierLocationName.Enabled = false;
					txtUnitTypeCode.Enabled = false;
					txtUnitTypeDescription.Enabled = false;

					lueMaterialType.Enabled = false;
					lueUnitType.Enabled = false;
					luePriority.Enabled = false;
					lueMethod.Enabled = false;                      //5/29/24
					lueOwner.Enabled = false;
					lueSupplier.Enabled = false;
					lueSupplierName.Enabled = false;        //task 1133 FG 7/21/2026

					btnAddMaterial.Enabled = false;
					btnAddNutrient.Enabled = false;
					btnAddOwner.Enabled = false;
					btnAddOwnerLocation.Enabled = false;
					btnAddSupplier.Enabled = false;
					btnAddSupplierLocation.Enabled = false;
					btnAddUnits.Enabled = false;

					//7/26/19
					lueSupplierLocationCode.Enabled = false;
					lueSupplierLocationName.Enabled = false;
				}
				DataBindMaterial();
				DataBindNutrient();
				DataBindOwner();
				DataBindOwnerLocation();
				DataBindSupplier();
				DataBindSupplierLocation();
				DataBindLueSupplierLocation();      //7/26/19
				DataBindGuarantee();
				DataBindUnits();
				DataBindWarningLimit();             //7/2/20
				DataBindTestTypeDetail();           //10/6/23


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{

			groupBox2.Visible = true;

			//9/23/22 - hide Active/Inactive for Sample Ingredient groups
			//6/18/20 - hide Active/Inactive options for Warning Limits and MaterialStockStatus tabs
			//if (tabControl1.SelectedIndex == 8)
			//if (tabControl1.SelectedIndex == 9 || tabControl1.SelectedIndex == 1)

			//5/18/23 - rearranged tabs so sample ingr/prod are together
			////if (tabControl1.SelectedIndex == 9 || tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 10)
			if (tabControl1.SelectedIndex == 9 || tabControl1.SelectedIndex == 1 || tabControl1.SelectedIndex == 11 || tabControl1.SelectedIndex == 12)

			{
				groupBox2.Visible = false;
			}

			//1/17/22 - commented
			//7/30/20
			//AIMGlobal.aimMaterialTab = false;
			//AIMGlobal.aimMaterialStockStatusTab = false;
			//AIMGlobal.aimNutrientTab = false;
			//AIMGlobal.aimOwnerTab = false;
			//AIMGlobal.aimOwnerLocationTab = false;
			//AIMGlobal.aimSupplierTab = false;
			//AIMGlobal.aimSupplierLocationTab = false;
			//AIMGlobal.aimSupplierGuaranteesTab = false;
			//AIMGlobal.aimUnitTypeTab = false;
			//AIMGlobal.aimWarningLimitsTab = false;

			//------------------------------------------------------------------------- //MATERIAL
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 0) 
			if (tabControl1.SelectedTab.Name == "tpMaterial")   //Task1095 DB250912 
			{
				if (_tabMaterial == false)
				{
					DataBindMaterialType();                                             //12/17/20 - moved from Form Load
					DataBindMaterial();                                                 //12/17/20 - moved from Form Load
					_tabMaterial = true;
				}


				AIMGlobal.aimMaterialTab = true;                                    //7/30/20
				grdMaterial.Focus();
			}

			//------------------------------------------------------------------------- //MATERIAL STOCK STATUS
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 1) 
			if (tabControl1.SelectedTab.Name == "tpMaterialStockStatus")   //Task1095 DB250912 
			{

				if (_tabStockStatus == false)
				{
					DataBindOwnerMaterialStockStatus();                                 //12/17/20 - moved from Form Load
					DataBindMaterialStockStatus();                                      //12/17/20 - moved from Form Load
					_tabStockStatus = true;
				}

				AIMGlobal.aimMaterialStockStatusTab = true;                             //7/30/20
				grdMaterialStockStatus.Focus();
			}

			//------------------------------------------------------------------------- //NUTRIENT
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 2) 
			if (tabControl1.SelectedTab.Name == "tpNutrient")   //Task1095 DB250912 
			{
				if (_tabNutrient == false)
				{
					DataBindlueNutrient();                                              //12/17/20 - moved from Form Load - begin
					DataBindNutrient();
					txtDefaultMethod.Text = "";
					txtDefaultPriority.Text = "";
					List<AIMPriorityUsedValueInfo> priority = new List<AIMPriorityUsedValueInfo>();
					AIMDatabaseInfo aimDatabase = new AIMDatabaseInfo();
					aimDatabase = AIMManager.GetAIMDatabase(CFCAIMCore.Global.aimDatabase.AIMDatabaseID);
					if (aimDatabase != null)
					{
						txtDefaultMethod.Text = aimDatabase.DefaultForPropose.ToString();
						priority = AIMManager.GetAIMPriorityUsedValue(aimDatabase.AIMPriorityUsedValueID);
						if (priority.Count > 0)
						{
							txtDefaultPriority.Text = priority[0].Description;
						}
					}                                                                   //12/17/20 - moved from Form Load - end
					_tabNutrient = true;
				}

				AIMGlobal.aimNutrientTab = true;                                    //7/30/20
				grdNutrient.Focus();
			}

			//------------------------------------------------------------------------- //OWNER
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 3) 
			if (tabControl1.SelectedTab.Name == "tpOwner")   //Task1095 DB250912 
			{
				if (_tabOwner == false)
				{
					DataBindOwner();                                                    //12/17/20 - moved from Form Load										
					_tabOwner = true;
				}

				AIMGlobal.aimOwnerTab = true;                                       //7/30/20
				grdOwner.Focus();
			}

			//------------------------------------------------------------------------- //OWNER LOCATION
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 4) 
			if (tabControl1.SelectedTab.Name == "tpOwnerLocation")   //Task1095 DB250912 
			{
				if (_tabOwnerLocation == false)
				{
					DataBindOneTimeOwnerLocation();                                     //12/17/20 - moved from Form Load
					DataBindOwnerLocation();                                            //12/17/20 - moved from Form Load
					_tabOwnerLocation = true;
				}

				AIMGlobal.aimOwnerLocationTab = true;                                   //7/30/20
				grdOwnerLocation.Focus();
			}

			//------------------------------------------------------------------------- //SUPPLIER
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 5) 
			if (tabControl1.SelectedTab.Name == "tpSupplier")   //Task1095 DB250912 
			{
				if (_tabSupplier == false)
				{
					DataBindSupplier();                                                 //12/17/20 - moved from Form Load
					DataBindlueSupplier_SL();                                           //3/4/25
					_tabSupplier = true;
				}

				AIMGlobal.aimSupplierTab = true;                                        //7/30/20
				grdSupplier.Focus();
			}

			//------------------------------------------------------------------------- //SUPPLIER LOCATION
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 6) 
			if (tabControl1.SelectedTab.Name == "tpSupplierLocation")   //Task1095 DB250912 
			{
				if (_tabSupplierLocation == false)
				{
					DataBindLueSupplierLocation();                                      //12/17/20 - moved from Form Load
					DataBindSupplierLocation();                                         //12/17/20 - moved from Form Load
					_tabSupplierLocation = true;
				}

				AIMGlobal.aimSupplierLocationTab = true;                                //7/30/20
				grdSupplierLocation.Focus();
			}

			//------------------------------------------------------------------------- //SUPPLIER GUARANTEES
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 7) 
			if (tabControl1.SelectedTab.Name == "tpSupplierLocGuarantees")   //Task1095 DB250912 
			{
				if (_tabGuarantees == false)
				{
					DataBindLueGuarantee();
					DataBindGuarantee();
					lueAimSupplier.EditValue = 0;
					lueAimSupplierLocation.EditValue = 0;
					lueAIMMaterialGuarantee.EditValue = 0;
					lueAIMNutrientGuarantee.EditValue = 0;
					_tabGuarantees = true;
				}
				AIMGlobal.aimSupplierGuaranteesTab = true;
				grdGuarantees.Focus();
			}

			//------------------------------------------------------------------------- //UNIT TYPE
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 8) 
			if (tabControl1.SelectedTab.Name == "tpUnits")   //Task1095 DB250912 
			{
				if (_tabUnitType == false)
				{
					DataBindUnits();
					_tabUnitType = true;
				}
				AIMGlobal.aimUnitTypeTab = true;
				grdUnitType.Focus();
			}

			//------------------------------------------------------------------------- //WARNING LIMITS
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 9) 
			if (tabControl1.SelectedTab.Name == "tpWarningLimits")   //Task1095 DB250912 
			{
				if (_tabWarnings == false)
				{
					DataBindlueWarningLimit();
					DataBindWarningLimit();
					_tabWarnings = true;
				}
				AIMGlobal.aimWarningLimitsTab = true;
				grdWarningLimits.Focus();
			}

			//------------------------------------------------------------------------- // TEST TYPES
			//10/6/23																
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 10) 
			if (tabControl1.SelectedTab.Name == "tpTestTypes")   //Task1095 DB250912 
			{
				if (_tabTestType == false)
				{
					//8/10/23
					DataBindTestType();
					_tabTestType = true;
				}
				AIMGlobal.testTypeTab = true;
				grdTestType.Focus();
			}

			//------------------------------------------------------------------------- // ALL GROUPS
			//10/6/23																
			//Task1095 DB250912 if (tabControl1.SelectedIndex == 11) 
			if (tabControl1.SelectedTab.Name == "tpGroups")   //Task1095 DB250912 
			{
				tcGroup.SelectedIndex = 0;
				if (_tabGroupOwnerLocation == false)
				{
					DataBindlueOwnerCode_Group();
					DataBindlueOwnerLocationGroups();
					_tabGroupOwnerLocation = true;
				}
			}

			//Task1121 DB251218
			if (tabControl1.SelectedTab.Name == "tpTest")
			{
				tcTest.SelectedIndex = 0;
				if (_tabTest == false)
				{
					DataBindTest();
					DataBindlueDataSourceTypeTest();
					_tabTest = true;
				}
			}

			//10/6/23 - commented and moved all groups to new tab
			////------------------------------------------------------------------------- //OWNER LOCATION GROUPS
			////11/22/22
			////5/18/23 - rearranged tabs or ingredient and product groups are together
			//////else if (tabControl1.SelectedIndex == 11)           
			//else if (tabControl1.SelectedIndex == 10)
			//{
			//	if (_tab10 == false)
			//	{
			//		//databind dropdowns
			//		DataBindlueOwnerCode_Group();
			//		DataBindlueOwnerLocationGroups();

			//		_tab10 = true;
			//	}
			//}

			//------------------------------------------------------------------------- //SAMPLE INGREDIENT GROUPS
			//9/23/22
			//5/18/23 - rearranged tabs or ingredient and product groups are together
			////else if (tabControl1.SelectedIndex == 10)           
			//else if (tabControl1.SelectedIndex == 11)
			//{
			//	if (_tab11 == false)
			//	{

			//		//10/19/22
			//		DataBindSpecificIngredientsGroup();
			//		DataBindSpecificIngredients();

			//		_tab11 = true;
			//	}
			//}

			////------------------------------------------------------------------------- //SAMPLE PRODUCT GROUPS
			////5/18/23
			//else if (tabControl1.SelectedIndex == 12)
			//{
			//	if (_tab12 == false)
			//	{

			//		DataBindSpecificProductsGroup();
			//		DataBindSpecificProducts();

			//		_tab12 = true;
			//	}
			//}

			////------------------------------------------------------------------------- //SUPPLIER GROUPS		
			////8/24/23
			//else if (tabControl1.SelectedIndex == 13)
			//{
			//	if (_tab13 == false)
			//	{
			//		DataBindlueSupplierGroup();
			//		DataBindlueSupplierCodeGroup();

			//		_tab13 = true;
			//	}
			//}

			////------------------------------------------------------------------------- //SUPPLIER LOCATION GROUPS			
			////8/30/23
			//else if (tabControl1.SelectedIndex == 14)
			//{
			//	if (_tab14 == false)
			//	{					
			//		DataBindlueSupplier_SL();
			//		////DataBindlueSupplierCode_Group();

			//		_tab14 = true;
			//	}
			//}




		}
		private void tcGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			//------------------------------------------------------------------------- //OWNER LOCATION GROUPS
			//11/22/22	
			if (tcGroup.SelectedIndex == 0)
			{
				if (_tabGroupOwnerLocation == false)
				{
					DataBindlueOwnerCode_Group();
					DataBindlueOwnerLocationGroups();
					_tabGroupOwnerLocation = true;
				}
			}

			//------------------------------------------------------------------------- //SAMPLE INGREDIENT GROUPS
			//9/23/22
			else if (tcGroup.SelectedIndex == 1)
			{
				if (_tabGroupIngredients == false)
				{
					DataBindSpecificIngredientsGroup();
					DataBindSpecificIngredients();
					_tabGroupIngredients = true;
				}
			}
			//------------------------------------------------------------------------- //SAMPLE PRODUCT GROUPS
			//5/18/23
			else if (tcGroup.SelectedIndex == 2)
			{
				if (_tabGroupProducts == false)
				{
					DataBindSpecificProductsGroup();
					DataBindSpecificProducts();
					_tabGroupProducts = true;
				}
			}
			//------------------------------------------------------------------------- //SUPPLIER GROUPS		
			//8/24/23
			else if (tcGroup.SelectedIndex == 3)
			{
				if (_tabGroupSuppliers == false)
				{
					DataBindlueSupplierGroup();
					DataBindlueSupplierCodeGroup();
					_tabGroupSuppliers = true;
				}
			}
			//------------------------------------------------------------------------- //SUPPLIER LOCATION GROUPS			
			//8/30/23
			else if (tcGroup.SelectedIndex == 4)
			{
				//task 1133 FG 7/21/2026 - rebind on every entry so group codes added/deleted on the Suppliers tab show up
				//without a refresh; DataBindlueSupplier_SL uses _tabGroupSupplierLocations internally to auto-select the
				//first record only on the first visit, and the current selection is preserved on later rebinds
				DataBindlueSupplier_SL();
				_tabGroupSupplierLocations = true;
			}
			//------------------------------------------------------------------------- //TEST TYPE GROUPS			
			//10/6/23
			else if (tcGroup.SelectedIndex == 5)
			{
				if (_tabGroupTestType == false)
				{
					DataBindlueTestTypeGroup();
					DataBindlueTestType_Group();
				}
				_tabGroupTestType = true;
			}
			//------------------------------------------------------------------------- //EXPORT INGREDIENT GROUPS
			//10/11/23
			else if (tcGroup.SelectedIndex == 6)
			{
				if (_tabGroupExportIngredients == false)
				{
					DataBindExportIngredientGroup();

					//populate parent dropdown
					List<ExportIngredientGroupInfo> group = new List<ExportIngredientGroupInfo>();
					group = AIMManager.GetExportIngredientGroup((Int32)lueGroupCodeExport.EditValue);
					if (group.Count > 0)
					{
						lueAimParentExportCode.EditValue = group[0].AIMMaterialID;
					}

					DataBindExportIngredient();
					_tabGroupExportIngredients = true;
				}
			}

			//------------------------------------------------------------------------- //EXPORT PLANT GROUPS
			//1/10/24
			else if (tcGroup.SelectedIndex == 7)
			{
				if (_tabGroupExportPlants == false)
				{
					DataBindExportPlantGroup();

					//1/25/24 - commented
					////populate parent dropdown
					//List<ExportPlantGroupInfo> group = new List<ExportPlantGroupInfo>();
					//group = AIMManager.GetExportPlantGroup((Int32)lueGroupCodeExportPlant.EditValue);
					//if (group.Count > 0)
					//{
					//	lueOwnerPlantGroup.EditValue = group[0].AIMOwnerID;
					//	lueOwnerLocationCodePlantGroup.EditValue = group[0].AIMOwnerLocationID;
					//}

					DataBindExportPlant();
					_tabGroupExportPlants = true;
				}
			}
			//------------------------------------------------------------------------- //Tab order on Sample screen
			//1/10/24
			else if (tcGroup.SelectedIndex == 8)
			{
				if (_tabGroupExportPlants == false)
				{
					DataBindTabOrderGroup();
					_loadingTabOrderGroup = true;
				}
			}
			//Task1061 DB251027
			else if (tcGroup.SelectedIndex == 9)
			{
				grbNutrientGroupNew.Location = new Point(6, 9);

				if (_tabNutrientGroup == false)
				{


					DataBindNutrientGroup();
					DataBindNutrientGroupAdd();
					_tabNutrientGroup = true;
				}
			}
			//------------------------------------------------------------------------- //SAMPLE SCREEN DISPLAY GROUPS
			//Task1042 FG
			else if (tcGroup.SelectedIndex == 10)
			{
				if (_tabGroupSampleScreenDisplay == false)
				{
					DataBindSampleScreenDisplayGroup();
					DataBindSampleScreenDisplayColumn();
					_tabGroupSampleScreenDisplay = true;
				}
			}
			//------------------------------------------------------------------------- //REQUIRED FIELD
			// task 1155 FG 8/5/26
			else if (tcGroup.SelectedIndex == 11)
			{
				if (_tabGroupRequiredField == false)
				{
					DataBindRequiredFieldColumn();
					DataBindRequiredFieldIngredientGroup();
					DataBindRequiredFieldProductGroup();
					DataBindRequiredFieldLocationGroup();
					DataBindRequiredFieldGroup();
					_tabGroupRequiredField = true;
				}
			}



		}
		//10/6/23
		private void btnInitialLoad_Click(object sender, EventArgs e)
		{
			//10/6/21 - changed to fix resize issue
			//InitialLoadAimNutrientsMaterials initialLoad = new InitialLoadAimNutrientsMaterials();
			//this.Hide();			
			//initialLoad.WindowState = FormWindowState.Normal;
			//initialLoad.ShowDialog();     			
			//DataBindMaterial();            
			//DataBindNutrient();		
			//this.Show();
			InitialLoadAimNutrientsMaterials initialLoad = new InitialLoadAimNutrientsMaterials();
			initialLoad.ShowDialog();
		}
		private void btnResize_Click(Object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
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
		}                   //7/26/19
		private void AIM_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}
		private void CheckGlobalTabs()
		{
			if (AIMGlobal.aimMaterialTab == true)
			{
				tabControl1.SelectedIndex = 0;
				grdMaterial.Focus();
			}
			else if (AIMGlobal.aimMaterialStockStatusTab == true)
			{
				tabControl1.SelectedIndex = 1;
				grdGuarantees.Focus();
			}
			else if (AIMGlobal.aimNutrientTab == true)
			{
				tabControl1.SelectedIndex = 2;
				grdNutrient.Focus();
			}
			else if (AIMGlobal.aimOwnerTab == true)
			{
				tabControl1.SelectedIndex = 3;
				grdOwner.Focus();
			}
			else if (AIMGlobal.aimOwnerLocationTab == true)
			{
				tabControl1.SelectedIndex = 4;
				grdOwnerLocation.Focus();
			}
			else if (AIMGlobal.aimSupplierTab == true)
			{
				tabControl1.SelectedIndex = 5;
				grdSupplier.Focus();
			}
			else if (AIMGlobal.aimSupplierLocationTab == true)
			{
				tabControl1.SelectedIndex = 6;
				grdSupplierLocation.Focus();
			}

			else if (AIMGlobal.aimSupplierGuaranteesTab == true)
			{
				tabControl1.SelectedIndex = 7;
				grdGuarantees.Focus();
			}
			else if (AIMGlobal.aimUnitTypeTab == true)
			{
				tabControl1.SelectedIndex = 8;
				this.grdUnitType.Focus();
			}
			else if (AIMGlobal.aimWarningLimitsTab == true)
			{
				tabControl1.SelectedIndex = 9;
				grdWarningLimits.Focus();
			}

			//10/6/23
			else if (AIMGlobal.testTypeTab == true)
			{
				tabControl1.SelectedIndex = 10;
				grdTestType.Focus();
			}

			//11/22/22
			else if (AIMGlobal.aimOwnerLocationGroupTab == true)
			{
				//tabControl1.SelectedIndex = 10;
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 0;
				grdOwnerLocationGroup.Focus();
			}

			//11/21/22
			else if (AIMGlobal.aimSpecificIngredientsTab == true)
			{
				//tabControl1.SelectedIndex = 11;
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 1;
				grdSpecific.Focus();
			}

			//5/18/23
			else if (AIMGlobal.aimSpecificProductsTab == true)
			{
				//tabControl1.SelectedIndex = 12;
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 2;
				grdSpecificProducts.Focus();
			}

			//8/30/23
			else if (AIMGlobal.aimSupplierGroupTab == true)
			{
				//tabControl1.SelectedIndex = 13;
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 3;
				grdSupplierGroup.Focus();
			}

			//8/30/23
			else if (AIMGlobal.aimSupplierLocationGroupTab == true)
			{
				//tabControl1.SelectedIndex = 14;
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 4;
				grdSupplierLocationGroup.Focus();
			}

			//10/6/23
			else if (AIMGlobal.testTypeGroupTab == true)
			//else if (AIMGlobal.testTypeGroupID != 0)
			{
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 5;
				if (AIMGlobal.testTypeGroupID != 0)
				{
					lueTestTypeGroup.EditValue = AIMGlobal.testTypeGroupID;
				}
			}

			//Task1114 DB251021
			else if (AIMGlobal.analyzerPlantGroupTab == true)
			{
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 7;
				grdPlantExport.Focus();
			}

			//Task1061 DB251027
			else if (AIMGlobal.nutrientGroupTab == true)
			{
				tabControl1.SelectedIndex = 11;
				tcGroup.SelectedIndex = 9;
				grdNutrientGroup.Focus();
			}

			else
			{
				//2/11/21 - changed to see if this helps CSA's speed issue
				//tabControl1.SelectedIndex = 0;		
				//grdMaterialView.Focus();
				tabControl1.SelectedIndex = 3;
				grdOwner.Focus();
			}


		}
		private void ShowUserDefinedImportList(ErrorInfo result, Int32 ID, String fieldName)
		{
			if (result.ErrorNumber == 3)                    //Get list of imports where AIMOwnerID exists
			{
				List<UserDefinedDataSourceDefinitionInfo> def = new List<UserDefinedDataSourceDefinitionInfo>();
				def = AIMManager.GetImportsForRequiredField(ID, fieldName);
				String importNames = "";
				String msg = "";
				if (def.Count > 0)
				{
					foreach (var item in def)
					{
						importNames = importNames + item.ImportFileName + ", ";
					}
					importNames = importNames.Substring(0, importNames.Length - 2);     //remove last comma
				}

				msg = fieldName + " exists on the following " + def.Count + " import(s): ";
				msg += Environment.NewLine;
				msg += Environment.NewLine;
				msg += importNames;
				MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);

			}
		}
		private void AIM_Leave(object sender, EventArgs e)
		{
			//1/17/22 - commented - left this in; however, aimowner is set to true on the load of the form -- ONLY If AIMGlobal.aimMaterialTab = true.
			////2/11/21 -- added the following to try and fix CSA's speed issue -- set all globals to false (because aimowner is set to true on load of form)
			///
			//Task1137 DB251209 
			//		   Note: Moved all the cleanup to the FormClosed event. This event doesn't always fire when closing the form
			//AIMGlobal.aimMaterialTab = false;
			//AIMGlobal.aimMaterialStockStatusTab = false;
			//AIMGlobal.aimNutrientTab = false;
			//AIMGlobal.aimOwnerTab = false;
			//AIMGlobal.aimOwnerLocationTab = false;
			//AIMGlobal.aimSupplierTab = false;
			//AIMGlobal.aimSupplierLocationTab = false;
			//AIMGlobal.aimSupplierGuaranteesTab = false;
			//AIMGlobal.aimUnitTypeTab = false;
			//AIMGlobal.aimWarningLimitsTab = false;

			////5/18/23
			//AIMGlobal.aimOwnerLocationGroupTab = false;
			//AIMGlobal.aimSpecificIngredientsTab = false;
			//AIMGlobal.aimSpecificProductsTab = false;

			////8/30/23
			//AIMGlobal.aimSupplierGroupTab = false;
			//AIMGlobal.aimSupplierLocationGroupTab = false;

			////10/6/23
			//AIMGlobal.testTypeTab = false;
			//AIMGlobal.testTypeGroupTab = false;


		}
        //Task1132 FG 7/10/2026 - Filter grid rows as user types in text boxes
        #region Grid Text Filter

        private String EscapeFilterValue(String value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return String.Empty;
            }
            return value.Trim().Replace("'", "''");
        }

        private String BuildContainsFilter(String fieldName, String text)
        {
            String escaped = EscapeFilterValue(text);
            if (escaped.Length == 0)
            {
                return String.Empty;
            }
            return "Contains([" + fieldName + "], '" + escaped + "')";
        }

        private void ApplyGridFilterFromParts(GridView gridView, List<String> parts)
        {
            if (parts.Count > 0)
            {
                gridView.ActiveFilterString = String.Join(" And ", parts);
            }
            else
            {
                gridView.ActiveFilterString = String.Empty;
            }
        }

        private void AddContainsPart(List<String> parts, String fieldName, TextBox textBox)
        {
            String part = BuildContainsFilter(fieldName, textBox.Text);
            if (part.Length > 0)
            {
                parts.Add(part);
            }
        }

        private void ApplyGridFilterForTab()
        {
            List<String> parts = new List<String>();
            GridView gridView = null;

            if (tabControl1.SelectedTab == null)
            {
                return;
            }

            String tabName = tabControl1.SelectedTab.Name;

            //Task1132 FG 7/10/2026 - map each tab's text boxes to grid FieldNames
            if (tabName == "tpMaterial")
            {
                gridView = grdMaterialView;
                AddContainsPart(parts, "AIMMaterialCode", txtMaterialCode);
                AddContainsPart(parts, "AIMMaterialName", txtMaterialName);
            }
            else if (tabName == "tpNutrient")
            {
                gridView = grdNutrientView;
                AddContainsPart(parts, "AIMNutrientCode", txtNutrientCode);
                AddContainsPart(parts, "AIMNutrientName", txtNutrientName);
                AddContainsPart(parts, "AIMNutrientShortName", txtShortNameNutrient);
            }
            else if (tabName == "tpOwner")
            {
                gridView = grdOwnerView;
                AddContainsPart(parts, "OwnerCode", txtOwnerCode);
                AddContainsPart(parts, "OwnerName", txtOwnerName);
            }
            else if (tabName == "tpOwnerLocation")
            {
                gridView = grdOwnerLocationView;
                AddContainsPart(parts, "LocationCode", txtOwnerLocationCode);
                AddContainsPart(parts, "LocationName", txtOwnerLocationName);
            }
            else if (tabName == "tpSupplier")
            {
                gridView = grdSupplierView;
                AddContainsPart(parts, "AIMSupplierCode", txtSupplierCode);
                AddContainsPart(parts, "SupplierName", txtSupplierName);
            }
            else if (tabName == "tpSupplierLocation")
            {
                gridView = grdSupplierLocationView;
                AddContainsPart(parts, "AIMSupplierLocationCode", txtSupplierLocationCode);
                AddContainsPart(parts, "SupplierLocationName", txtSupplierLocationName);
            }
            else if (tabName == "tpUnits")
            {
                gridView = grdUnitTypeView;
                AddContainsPart(parts, "UnitTypeCode", txtUnitTypeCode);
                AddContainsPart(parts, "UnitTypeDescription", txtUnitTypeDescription);
            }
            else if (tabName == "tpTest")
            {
                //Task1132 FG 7/10/2026 - nested Test tabs
                if (tcTest.SelectedTab == null)
                {
                    return;
                }

                String testTabName = tcTest.SelectedTab.Name;

                if (testTabName == "tpTestTest")
                {
                    gridView = grdTestView;
                    AddContainsPart(parts, "TestCode", txtTestCode);
                    AddContainsPart(parts, "TestName", txtTestName);
                }
                else if (testTabName == "tpTestGroupName")
                {
                    gridView = grdTestGroupNameView;
                    AddContainsPart(parts, "TestGroupName", txtTestGroupName);
                }
            }
            // Optional later: Guarantees / Test / Groups — those tabs mostly use lookups or nested tab controls

            if (gridView != null)
            {
                ApplyGridFilterFromParts(gridView, parts);
            }
        }

        private void GridFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyGridFilterForTab();
        }

        #endregion

        #region Material ------------------------------------------------------------------------
        private void DataBindMaterial()
		{
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

			List<AIMOwnerLocationInfo> aimOwnerLoc = new List<AIMOwnerLocationInfo>();
			aimOwnerLoc = AIMManager.GetAIMOwnerLocation(true);
			aimOwnerLoc.Insert
				(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					OwnerCode = "",
					LocationName = "--Select--"
				}
				);

			lueOwnerLoc.Properties.DataSource = aimOwnerLoc;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLoc, "AIMOwnerLocationID", "LocationName", "OwnerCode");
			lueOwnerLoc.Properties.Columns["OwnerCode"].Caption = "Owner";
			lueOwnerLoc.Properties.Columns["LocationName"].Caption = "Owner Location Name";

			aimOwnerLoc = AIMManager.GetAIMOwnerLocation(true);
			replueOwnerLocation_Material.DataSource = aimOwnerLoc;
			XtraEditors.LoadGridLookUpEdit(ref replueOwnerLocation_Material, "AIMOwnerLocationID", "LocationName", "OwnerCode");
			replueOwnerLocation_Material.Columns["OwnerCode"].Caption = "Owner";
			replueOwnerLocation_Material.Columns["LocationName"].Caption = "Owner Location Name";

			//------------------------------------------------------------			

			//10/5/21 - added check for null
			if (lueMaterialType.EditValue != null)
			{
				//4/1/19 - added to use material-type as filter for grid
				//aimMaterial = AIMManager.GetAIMMaterial(rbActiveRecords.Checked);
				if ((Int32)lueMaterialType.EditValue != 0)
				{
					//10/13/23
					//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(rbActiveRecords.Checked, (Int32)lueMaterialType.EditValue);
					aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(rbActiveRecords.Checked, (Int32)lueMaterialType.EditValue, (Int32)EnumOrderBy.Name);
				}
				else
				{
					aimMaterial = AIMManager.GetAIMMaterial(rbActiveRecords.Checked);
				}
			}
			else
			{
				aimMaterial = AIMManager.GetAIMMaterial(rbActiveRecords.Checked);
			}

			Int32 currentRowNum = 0;
			if (grdMaterialView != null)
			{
				Int32.TryParse(grdMaterialView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdMaterial.DataSource = aimMaterial;
			grdMaterialView.FocusedRowHandle = currentRowNum;

		}
		private void DataBindMaterialStockStatus()
		{
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			List<AIMMaterialStockStatusInfo> aimMaterialStockStatus = new List<AIMMaterialStockStatusInfo>();

			//10/13/23
			//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient);
			aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)EnumOrderBy.Name);

			aimMaterial.Insert
				(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--"
				}
				);

			lueMaterialCode.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
			lueMaterialName.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");

			//task 1133 FG 7/21/2026 - inside the Material Name dropdown, show the Name column first, then the Code column
			lueMaterialName.Properties.Columns.Clear();
			lueMaterialName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialName", "Material Name"));
			lueMaterialName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialCode", "Material Code"));

			replueMaterial.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterial, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");

			aimMaterialStockStatus = AIMManager.GetAIMMaterialStockStatus(rbActiveRecords.Checked);
			Int32 currentRowNum = 0;
			if (grdMaterialStockStatusView != null)
			{
				Int32.TryParse(grdMaterialStockStatusView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdMaterialStockStatus.DataSource = aimMaterialStockStatus;
			grdMaterialStockStatusView.FocusedRowHandle = currentRowNum;


		}           //added 6/18/20        
		private void DataBindMaterialType()
		{
			//4/1/19 - separated this section from DataBindMaterial function (because type dropdown is now used as filter)
			List<MaterialTypeInfo> material = new List<MaterialTypeInfo>();

			material = AIMManager.GetMaterialType(true);
			material.Insert
			(
				0,
				new MaterialTypeInfo
				{
					MaterialTypeID = 0,
					MaterialTypeName = "--Select--"
				}
			);

			lueMaterialType.Properties.DataSource = material;
			XtraEditors.LoadLookUpEdit(ref lueMaterialType, "MaterialTypeID", "MaterialTypeName");
			lueMaterialType.Properties.Columns["MaterialTypeName"].Visible = true;

			replueMaterialType.DataSource = material;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterialType, "MaterialTypeID", "MaterialTypeName");
			replueMaterialType.Columns["MaterialTypeName"].Visible = true;
		}
		private void btnPreviewMaterial_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdMaterial, "AIM Material");
				CFCAIMCore.Global.previewGridResults(this.grdMaterial, "AIM Material");
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
				AIMMaterialInfo aimMaterial = new AIMMaterialInfo();

				aimMaterial.AIMMaterialID = 0;
				aimMaterial.AIMMaterialCode = txtMaterialCode.Text;
				aimMaterial.AIMMaterialName = txtMaterialName.Text;
				aimMaterial.MaterialTypeID = (Int32)lueMaterialType.EditValue;
				aimMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				aimMaterial.AIMOwnerLocationID = (Int32)lueOwnerLoc.EditValue;

				if (string.IsNullOrWhiteSpace(txtMaterialCode.Text) == true)
				{
					MessageBox.Show("Please enter a Material Code. ", "Warning...", MessageBoxButtons.OK);
					txtMaterialCode.Focus();
					return;
				}
				if (txtMaterialName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Material Name. ", "Warning...", MessageBoxButtons.OK);
					txtMaterialName.Focus();
					return;
				}
				if (aimMaterial.MaterialTypeID == 0)
				{
					MessageBox.Show("Please select a Material Type. ", "Warning...", MessageBoxButtons.OK);
					lueMaterialType.Focus();
					lueMaterialType.ShowPopup();
					return;
				}

				if (aimMaterial.MaterialTypeID == (Int32)MaterialTypeEnum.Product && (Int32)lueOwnerLoc.EditValue == 0)
				{
					MessageBox.Show("Please select an Owner Location. ", "Warning...", MessageBoxButtons.OK);
					lueOwnerLoc.Focus();
					lueOwnerLoc.ShowPopup();
					return;
				}


				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMMaterial(ref aimMaterial, false);
				if (error.ErrorNumber > 0)
				{
					lueMaterialType.EditValue = 0;
					lueOwnerLoc.EditValue = 0;
					return;
				}
				txtMaterialCode.Text = String.Empty;
				txtMaterialName.Text = String.Empty;
				DataBindMaterial();

				//7/2/20 - refresh material dropdown on Warning Limits tab
				DataBindlueWarningLimit();

				//7/2/20 - refresh material dropdown on SupplierLoc Guarantee tab
				DataBindLueGuarantee();

				//7/2/20 - refresh material dropdowns on Material Stock Status tab
				DataBindMaterialStockStatus();

				txtMaterialCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdMaterialView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			AIMMaterialInfo def = new AIMMaterialInfo();

			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive materials.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			def = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();

			if (def.AIMMaterialCode == null || def.AIMMaterialCode.Length == 0)
			{
				MessageBox.Show("Material code cannot be empty. Information is not saved.", "Warning...");
				return;
			}

			if (def.MaterialTypeID == 0)
			{
				MessageBox.Show("Material type cannot be empty. Information is not saved.", "Warning...");
				return;
			}

			if (def.MaterialTypeID == (Int32)MaterialTypeEnum.Product && def.AIMOwnerLocationID == 0)
			{
				MessageBox.Show("For Products, an Owner Location must be selected. Information is not saved.", "Warning...");
				DataBindMaterial();
				return;
			}

			if (def.MaterialTypeID == (Int32)MaterialTypeEnum.Ingredient)
			{
				def.AIMOwnerLocationID = 0;
			}



			def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAIMMaterial(ref def, false);
			DataBindMaterial();
		}
		private void grdMaterialView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void repbtnDeleteMaterial_Click(object sender, EventArgs e)
		{
			try
			{
				AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
				aimMaterial = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();

				//7/2/20 - check Material Stock Status and Warning Limits tables -- for this material.
				//          If material exists on either of these tables, do not allow user to inactivate.
				if (rbActiveRecords.Checked)
				{
					AIMMaterialStockStatusInfo aimMaterialStockStatusExists = new AIMMaterialStockStatusInfo();
					aimMaterialStockStatusExists = AIMManager.GetAIMMaterialStockStatus(aimMaterial.AIMMaterialID);
					if (aimMaterialStockStatusExists != null)
					{
						MessageBox.Show("This material cannot be inactivated at this time because it exists on the Material Stock Status table.", "Warning...", MessageBoxButtons.OK);
						return;
					}
					List<WarningLimitsInfo> warning = new List<WarningLimitsInfo>();
					warning = AIMManager.GetWarningLimitsForMaterial(aimMaterial.AIMMaterialID);
					if (warning.Count > 0)
					{
						MessageBox.Show("This material cannot be inactivated at this time because it exists on the Warning Limits table.", "Warning...", MessageBoxButtons.OK);
						return;
					}
				}
				//--------------------------------------------------------------

				aimMaterial.IsActive = !aimMaterial.IsActive;
				aimMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveAIMMaterial(ref aimMaterial, false);
				DataBindMaterial();

				//----------------------------------------------------
				if (rbActiveRecords.Checked)
				{
					try
					{
						//7/2/20 - if material exists on AIMSupplierLocationGuarantees table, inactivate it there as well.
						List<AIMSupplierLocationGuaranteeInfo> supplierlocation = new List<AIMSupplierLocationGuaranteeInfo>();
						supplierlocation = AIMManager.GetSupplierLocationGuarantee(aimMaterial.AIMMaterialID);
						if (supplierlocation != null)
						{
							//8/26/20 - fixed index error
							if (supplierlocation.Count > 0)
							{
								AIMSupplierLocationGuaranteeInfo suppLoc = new AIMSupplierLocationGuaranteeInfo();
								suppLoc.AIMSupplierLocationGuaranteeID = supplierlocation[0].AIMSupplierLocationGuaranteeID;
								suppLoc.IsActive = false;
								suppLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								AIMManager.SaveSupplierLocationGuarantee(ref suppLoc);
								DataBindGuarantee();
							}
						}

						//9/23/22 - if material exists on SpecificIngredients table, inactivate it there also.
						List<SpecificIngredientsInfo> specific = new List<SpecificIngredientsInfo>();
						specific = AIMManager.GetSpecificIngredients(aimMaterial.AIMMaterialID);
						if (specific != null)
						{
							if (specific.Count > 0)
							{
								ErrorInfo updateStatus = new ErrorInfo();
								updateStatus = AIMManager.UpdateSpecificIngredientIsActive(aimMaterial.AIMMaterialID, false, userGlobal.CFCLoginUser.CFCUserID);
								DataBindSpecificIngredientsGroup();
								DataBindSpecificIngredients();
							}
						}

						//5/18/23 - if material exists on SpecificProducts table, inactivate it there also.
						List<SpecificProductsInfo> specificProd = new List<SpecificProductsInfo>();
						specificProd = AIMManager.GetSpecificProducts(aimMaterial.AIMMaterialID);
						if (specificProd != null)
						{
							if (specificProd.Count > 0)
							{
								ErrorInfo updateStatus = new ErrorInfo();
								updateStatus = AIMManager.UpdateSpecificProductIsActive(aimMaterial.AIMMaterialID, false, userGlobal.CFCLoginUser.CFCUserID);
								DataBindSpecificProductsGroup();
								DataBindSpecificProducts();
							}
						}

					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}

				//----------------------------------------------------
				else
				{
					//9/22/21 - see if material is on SpecificIngredients table and reactivate
					List<SpecificIngredientsInfo> specific = new List<SpecificIngredientsInfo>();
					specific = AIMManager.GetSpecificIngredients(aimMaterial.AIMMaterialID);
					if (specific != null)
					{
						if (specific.Count > 0)
						{
							ErrorInfo updateStatus = new ErrorInfo();
							updateStatus = AIMManager.UpdateSpecificIngredientIsActive(aimMaterial.AIMMaterialID, true, userGlobal.CFCLoginUser.CFCUserID);
							DataBindSpecificIngredientsGroup();
							DataBindSpecificIngredients();
						}
					}

					//5/18/23 - see if material is on SpecificProducts table and reactivate
					List<SpecificProductsInfo> specificProd = new List<SpecificProductsInfo>();
					specificProd = AIMManager.GetSpecificProducts(aimMaterial.AIMMaterialID);
					if (specificProd != null)
					{
						if (specificProd.Count > 0)
						{
							ErrorInfo updateStatus = new ErrorInfo();
							updateStatus = AIMManager.UpdateSpecificProductIsActive(aimMaterial.AIMMaterialID, true, userGlobal.CFCLoginUser.CFCUserID);
							DataBindSpecificProductsGroup();
							DataBindSpecificProducts();
						}
					}

				}

				//7/2/20 - refresh material dropdown on Warning Limits tab
				DataBindlueWarningLimit();

				//7/2/20 - refresh material dropdown on SupplierLoc Guarantee tab
				DataBindLueGuarantee();

				//7/2/20 - refresh material dropdowns on Material Stock Status tab
				DataBindMaterialStockStatus();

				//9/23/22 - refresh material dropdown on Specific Ingredients tab
				//DataBindSpecificIngredients();
				DataBindSpecificIngredientsGroup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueMaterialType_Enter(object sender, EventArgs e)
		{
			lueMaterialType.SelectAll();
		}
		private void lueMaterialType_Click(object sender, EventArgs e)
		{
			lueMaterialType.SelectAll();
		}
		private void replueMaterialType_Validating(Object sender, System.ComponentModel.CancelEventArgs e)
		{
			//(3/2/17) - before user can change materialtype, check xref.  If xref exists, don't allow change
			//Check Analyzer Cross-Reference
			AIMMaterialInfo material = new AIMMaterialInfo();
			material = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();

			AIMMaterialRefAnalyzerMaterialInfo xref = new AIMMaterialRefAnalyzerMaterialInfo();
			xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialAIMID(material.AIMMaterialID);
			if (xref != null)
			{
				MessageBox.Show("A cross-reference exists for this material. Before the material type can be changed, you must first delete any cross-references - under Setup > Cross Reference > Analyzer.", "Warning...", MessageBoxButtons.OK);
				e.Cancel = true;
				SendKeys.Send("{ESC}");
				return;
			}
		}
		private void lueMaterialType_EditValueChanged(Object sender, EventArgs e)
		{
			lblOwnerLoc.Visible = false;
			lueOwnerLoc.Visible = false;
			if ((Int32)lueMaterialType.EditValue == (Int32)MaterialTypeEnum.Product)
			{
				lblOwnerLoc.Visible = true;
				lueOwnerLoc.Visible = true;
			}

			//4/1/19
			DataBindMaterial();

		}
		private void lueOwnerLoc_Click(Object sender, EventArgs e)
		{
			lueOwnerLoc.SelectAll();
		}
		private void lueOwnerLoc_Enter(Object sender, EventArgs e)
		{
			lueOwnerLoc.SelectAll();
		}
		private void replueOwnerLocation_Material_ButtonPressed(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			AIMMaterialInfo material = new AIMMaterialInfo();
			material = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();
			if (material.MaterialTypeID == (Int32)MaterialTypeEnum.Ingredient)
			{
				MessageBox.Show("Ingredients cannot be assigned an Owner Location. ", "Warning...", MessageBoxButtons.OK);
			}
		}
		#endregion

		#region Material Stock Status -----------------------------------------------------------
		private void lueMaterialCode_Enter(object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}                               //6/18/20
		private void lueMaterialCode_Click(object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}                               //6/18/20
		private void lueMaterialName_Enter(object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}                               //6/18/20
		private void lueMaterialName_Click(object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}                               //6/18/20
		private void lueMaterialCode_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueMaterialCode, ref lueMaterialName);
		}                   //6/18/20
		private void lueMaterialName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueMaterialName, ref lueMaterialCode);
		}                   //6/18/20
		private void lueOwner_MaterialStockStatus_EditValueChanged(object sender, EventArgs e)
		{
			DataBindLueOwnerLocation();
		}       //6/18/20
		private void btnAddMaterialStockStatus_Click(object sender, EventArgs e)
		{
			try
			{
				AIMMaterialStockStatusInfo aimMaterialStockStatusExists = new AIMMaterialStockStatusInfo();

				//See if material/location combo already exists
				aimMaterialStockStatusExists = AIMManager.GetAIMMaterialStockStatus((Int32)lueMaterialCode.EditValue, (Int32)lueOwnerLocation_MaterialStockStatus.EditValue);
				if (aimMaterialStockStatusExists != null)
				{
					MessageBox.Show("This Material/Owner-Location combination already exists.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				AIMMaterialStockStatusInfo aimMaterialStockStatus = new AIMMaterialStockStatusInfo();

				aimMaterialStockStatus.AIMMaterialStockStatusID = 0;
				aimMaterialStockStatus.AIMMaterialID = (Int32)lueMaterialCode.EditValue;
				aimMaterialStockStatus.AIMOwnerLocationID = (Int32)lueOwnerLocation_MaterialStockStatus.EditValue;
				aimMaterialStockStatus.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				if ((Int32)lueMaterialCode.EditValue == 0)
				{
					MessageBox.Show("Please select a Material. ", "Warning...", MessageBoxButtons.OK);
					lueMaterialCode.Focus();
					lueMaterialCode.ShowPopup();
					return;
				}
				if ((Int32)this.lueOwnerLocation_MaterialStockStatus.EditValue == 0)
				{
					MessageBox.Show("Please select an Owner Location. ", "Warning...", MessageBoxButtons.OK);
					lueOwnerLocation_MaterialStockStatus.Focus();
					lueOwnerLocation_MaterialStockStatus.ShowPopup();
					return;
				}

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMMaterialStockStatus(ref aimMaterialStockStatus);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				lueMaterialCode.EditValue = 0;

				DataBindMaterialStockStatus();
				lueMaterialCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}                       //6/18/20
		private void lueOwner_MaterialStockStatus_Enter(object sender, EventArgs e)
		{
			lueOwner_MaterialStockStatus.SelectAll();
		}                   //6/18/20
		private void lueOwner_MaterialStockStatus_Click(object sender, EventArgs e)
		{
			lueOwner_MaterialStockStatus.SelectAll();
		}                   //6/18/20
		private void lueOwnerLocation_MaterialStockStatus_Enter(object sender, EventArgs e)
		{
			lueOwnerLocation_MaterialStockStatus.SelectAll();
		}           //6/18/20
		private void lueOwnerLocation_MaterialStockStatus_Click(object sender, EventArgs e)
		{
			lueOwnerLocation_MaterialStockStatus.SelectAll();
		}           //6/18/20
		private void grdMaterialStockStatusView_ValidateRow(object sender, ValidateRowEventArgs e)
		{

			//commented this because couldn't figure out how to make work when both material and owner-loc ID's are part of the key
			// instead, made the columns read-only, so user has to either add or delete, can't modify in grid.

			//    try
			//    {

			//        AIMMaterialStockStatusInfo materialLoc = new AIMMaterialStockStatusInfo();
			//        materialLoc = (AIMMaterialStockStatusInfo)grdMaterialStockStatusView.GetFocusedRow();
			//        materialLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;


			//        if (materialLoc.AIMMaterialID == 0 || materialLoc.AIMOwnerLocationID == 0)
			//        {
			//            MessageBox.Show("Need to select both Material and Owner Location. Information is not saved", "Warning...");
			//            return;
			//        }

			//        AIMManager.SaveAIMMaterialStockStatus(ref materialLoc); 
			//        DataBindMaterialStockStatus();
			//    }
			//    catch (Exception ex)
			//    {
			//        MessageBox.Show(ex.Message);
			//    }

		}   //6/18/20
		private void btnPreviewMaterialStockStatus_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdMaterialStockStatus, "AIM Material Stock Status");
				CFCAIMCore.Global.previewGridResults(this.grdMaterialStockStatus, "AIM Material Stock Status");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}                   //6/18/20
		private void grdMaterialStockStatusView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//7/2/20
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void repbtnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				AIMMaterialStockStatusInfo materialStockStatus = new AIMMaterialStockStatusInfo();
				materialStockStatus = (AIMMaterialStockStatusInfo)grdMaterialStockStatusView.GetFocusedRow();
				AIMManager.DeleteAIMMaterialStockStatus(materialStockStatus.AIMMaterialID, materialStockStatus.AIMOwnerLocationID);
				DataBindMaterialStockStatus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}                                   //6/18/20		
		private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
		{
			DataBindLueOwnerLocation();
		}
		private void DataBindLueOwnerLocation()
		{
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			Int32 ownerID = 0;
			if (lueOwner_MaterialStockStatus.EditValue != null && lueOwner_MaterialStockStatus.Text != String.Empty)
			{
				ownerID = (Int32)lueOwner_MaterialStockStatus.EditValue;
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

			lueOwnerLocation_MaterialStockStatus.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocation_MaterialStockStatus, "AIMOwnerLocationID", "LocationName");

			//6/18/20 - replueOwnerLocation is in the grid on the MaterialStockStatus tab
			replueOwnerLocation.DataSource = aimOwnerLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueOwnerLocation, "AIMOwnerLocationID", "LocationName", "LocationCode");

			if (aimOwnerLocation.Count == 2)
			{
				//selecting value if there is only one
				lueOwnerLocation_MaterialStockStatus.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
			}
			else
			{
				lueOwnerLocation_MaterialStockStatus.EditValue = 0; // aimOwnerLocation;  //11/14/24
			}

		}
		private void replueMaterial_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//commented this because couldn't figure out how to make work when both material and owner-loc ID's are part of the key
			// instead, made the columns read-only, so user has to either add or delete, can't modify in grid.
			//try
			//{
			//    grdMaterialStockStatusView.SetFocusedRowCellValue(colAIMMaterialName_MaterialStockStatus, e.NewValue);
			//}
			//catch (Exception ex)
			//{
			//    MessageBox.Show(ex.Message);
			//}
		}          //6/18/20
		private void replueOwnerLocation_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
		{
			//commented this because couldn't figure out how to make work when both material and owner-loc ID's are part of the key
			// instead, made the columns read-only, so user has to either add or delete, can't modify in grid.
			//try
			//{
			//    grdMaterialStockStatusView.SetFocusedRowCellValue(colAIMOwnerLocation_MaterialStockStatus, e.NewValue);
			//}
			//catch (Exception ex)
			//{
			//    MessageBox.Show(ex.Message);
			//}
		}     //6/18/20		
		#endregion

		#region Nutrient ------------------------------------------------------------------------
		private void DataBindNutrient()
		{

			List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
			replueDataSourceTypeNutrient.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceTypeNutrient, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceTypeNutrient.Columns["TypeDisplay"].Caption = "Type";

			List<AIMNutrientInfo> AIMNutrient = new List<AIMNutrientInfo>();
			Int32 currentRowNum = 0;
			AIMNutrient = AIMManager.GetAIMNutrient(rbActiveRecords.Checked);

			// Added banded view only for merged column headers.
			// To switch back to grdNutrientView, on the "Retrieve Details/Run Designer" box, select "Click here to change view"
			// and select "grdNutrientView".

			if (grdNutrient.MainView.Name == "advBandedGridView1")
			{
				currentRowNum = advBandedGridView1.FocusedRowHandle;
				grdNutrient.DataSource = AIMNutrient;
				advBandedGridView1.FocusedRowHandle = currentRowNum;
			}
			else
			{
				currentRowNum = grdNutrientView.FocusedRowHandle;
				grdNutrient.DataSource = AIMNutrient;
				grdNutrientView.FocusedRowHandle = currentRowNum;
			}

		}
		private void DataBindlueNutrient()
		{
			_dataSourceType = AIMManager.GetDataSourceType(true);
			_dataSourceType.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--Select--"
				}
			);
			lueDataSourceType.Properties.DataSource = _dataSourceType;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name", "TypeDisplay");
			lueDataSourceType.Properties.Columns["TypeDisplay"].Caption = "Type";


			//-------------------------------------------------
			List<AIMPriorityUsedValueInfo> priority = new List<AIMPriorityUsedValueInfo>();
			priority = AIMManager.GetAIMPriorityUsedValue();
			priority.Insert
			(
				0,
				new AIMPriorityUsedValueInfo
				{
					AIMPriorityUsedValueID = 0,
					Description = "--Select--"
				}
			);
			luePriority.Properties.DataSource = priority;
			XtraEditors.LoadLookUpEdit(ref luePriority, "AIMPriorityUsedValueID", "Description");
			luePriority.EditValue = 1;  //set to Default

			//-------------------------------------------------
			List<AIMPriorityUsedValueInfo> repluepriority = new List<AIMPriorityUsedValueInfo>();
			repluepriority = AIMManager.GetAIMPriorityUsedValue();
			repluePriority.DataSource = repluepriority;
			XtraEditors.LoadGridLookUpEdit(ref repluePriority, "AIMPriorityUsedValueID", "Description");





			//--------------------------------------------
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

			lueMethod.Properties.DataSource = mathType;                                         //5/29/24
			XtraEditors.LoadLookUpEdit(ref lueMethod, "MathTypeID", "MathTypeName");            //5/29/24

			replueMathTypeNutrient.DataSource = mathType;
			XtraEditors.LoadGridLookUpEdit(ref replueMathTypeNutrient, "MathTypeID", "MathTypeName");

			//-------------------------------------------------
			List<UnitTypeInfo> unit = new List<UnitTypeInfo>();
			unit = AIMManager.GetAllUnitType();
			unit.Insert
			(
				0,
				new UnitTypeInfo
				{
					UnitTypeID = 0,
					UnitTypeDescription = "--Select--",
				}
			);
			lueUnitType.Properties.DataSource = unit;
			XtraEditors.LoadLookUpEdit(ref lueUnitType, "UnitTypeID", "UnitTypeDescription");
			if (unit.Count == 2)
			{
				//selecting value if there is only one
				lueUnitType.EditValue = unit[1].UnitTypeID;
			}
			else
			{
				lueUnitType.EditValue = 0;
			}

			replueUnitTypeNutrient.DataSource = unit;
			XtraEditors.LoadGridLookUpEdit(ref replueUnitTypeNutrient, "UnitTypeID", "UnitTypeDescription");

		}
		private void btnAddNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtNutrientCode.Text) == true)
				{
					MessageBox.Show("Please enter a Nutrient Code.", "Warning...", MessageBoxButtons.OK);
					txtNutrientCode.Focus();
					return;
				}
				if (txtNutrientName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Nutrient Name.", "Warning...", MessageBoxButtons.OK);
					txtNutrientName.Focus();
					return;
				}

				//5/3/22 - commented per Mark
				//if ((Int32)lueUnitType.EditValue == 0)
				//{
				//	MessageBox.Show("Please select a Unit of Measure.", "Warning...", MessageBoxButtons.OK);
				//	lueUnitType.Focus();
				//	lueUnitType.ShowPopup();
				//	return;
				//}

				if ((Int32)luePriority.EditValue == 0)
				{
					MessageBox.Show("Please select an AIM Priority Value.", "Warning...", MessageBoxButtons.OK);
					luePriority.Focus();
					luePriority.ShowPopup();
					return;
				}
				if ((Int32)luePriority.EditValue == (Int32)PriorityUsedValue.DataSource && ((Int32)lueDataSourceType.EditValue == 0))
				{
					MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
					lueDataSourceType.Focus();
					lueDataSourceType.ShowPopup();
					return;
				}


				AIMNutrientInfo AIMNutrient = new AIMNutrientInfo();
				AIMNutrient.AIMNutrientID = 0;
				AIMNutrient.AIMNutrientCode = txtNutrientCode.Text;
				AIMNutrient.AIMNutrientName = txtNutrientName.Text;
				AIMNutrient.AIMNutrientShortName = txtShortNameNutrient.Text;
				AIMNutrient.UnitTypeID = (Int32)lueUnitType.EditValue;
				AIMNutrient.MathTypeID = (Int32)lueMethod.EditValue;                    //5/29/24 
				AIMNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMNutrient.PriorityAIMUsedValue = (Int32)luePriority.EditValue;
				AIMNutrient.DataSourceTypeID = (Int32)lueDataSourceType.EditValue;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMNutrient(ref AIMNutrient, true);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtNutrientCode.Text = String.Empty;
				txtNutrientName.Text = String.Empty;
				txtShortNameNutrient.Text = String.Empty;
				lueDataSourceType.EditValue = 0;
				luePriority.EditValue = 1;  //set to "Default"			
				lueMethod.EditValue = 0;                                                //5/29/24 
				DataBindNutrient();

				//7/2/20 - refresh Nutrient dropdown on Supplier Guarantees tab
				DataBindLueGuarantee();

				//7/2/20 - refresh Nutrient dropdown on Warning Limits tab
				DataBindlueWarningLimit();

				txtNutrientCode.Focus();
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
				//AIMGlobal.previewGridResults(grdNutrient, "AIM Nutrient");
				CFCAIMCore.Global.previewGridResults(grdNutrient, "AIM Nutrient");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdNutrientView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			try
			{
				Int32 databaseTypeID = ((AIMNutrientInfo)(e.Row)).DataSourceTypeID;
				Int32 priorityID = ((AIMNutrientInfo)(e.Row)).PriorityAIMUsedValue;

				if (priorityID == (Int32)PriorityUsedValue.DataSource && databaseTypeID == 0)
				{
					MessageBox.Show("You must select a data source. Changes will be undone.", "Warning...", MessageBoxButtons.OK);
					DataBindNutrient();
					return;
				}

				if (priorityID != (Int32)PriorityUsedValue.DataSource)
				{
					grdNutrientView.SetRowCellValue(e.RowHandle, "DataSourceTypeID", 0);
				}

				if (rbDeletedRecords.Checked == true)
				{
					MessageBox.Show("You may not make changes to inactive Nutrients.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				AIMNutrientInfo AIMNutrient = new AIMNutrientInfo();
				AIMNutrient = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
				AIMNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveAIMNutrient(ref AIMNutrient, true);
				DataBindNutrient();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdNutrientView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void repbtnDeleteNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				AIMNutrientInfo def = new AIMNutrientInfo();
				if (grdNutrient.MainView.Name == "advBandedGridView1")
				{
					def = (AIMNutrientInfo)advBandedGridView1.GetFocusedRow();
				}
				else
				{
					def = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
				}

				//7/2/20 - check Supplier Location Guarantees and Warning Limits tables -- for this nutrient.
				//          If nutrient exists on either of these tables, do not allow user to inactivate.
				if (rbActiveRecords.Checked)
				{
					List<AIMSupplierLocationGuaranteeInfo> guarantee = new List<AIMSupplierLocationGuaranteeInfo>();
					guarantee = AIMManager.GetSupplierLocationGuaranteeNutrient(def.AIMNutrientID, true);
					if (guarantee.Count > 0)
					{
						MessageBox.Show("This nutrient cannot be inactivated at this time because it exists on the Supplier Location Guarantees table.", "Warning...", MessageBoxButtons.OK);
						return;
					}
					List<WarningLimitsInfo> warning = new List<WarningLimitsInfo>();
					warning = AIMManager.GetWarningLimitsForNutrient(def.AIMNutrientID);
					if (warning.Count > 0)
					{
						MessageBox.Show("This nutrient cannot be inactivated at this time because it exists on the Warning Limits table.", "Warning...", MessageBoxButtons.OK);
						return;
					}
				}

				def.IsActive = !def.IsActive;
				def.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveAIMNutrient(ref def, true);
				DataBindNutrient();

				//7/2/20 - refresh Nutrient dropdown on Supplier Guarantees tab
				DataBindLueGuarantee();

				//7/2/20 - refresh Nutrient dropdown on Warning Limits tab
				DataBindlueWarningLimit();



			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void luePriority_EditValueChanged(object sender, EventArgs e)
		{
			lblDataSource.Visible = false;
			lueDataSourceType.Visible = false;
			if ((Int32)luePriority.EditValue == (Int32)PriorityUsedValue.DataSource)
			{
				lblDataSource.Visible = true;
				lueDataSourceType.Visible = true;
			}
		}
		private void enableCell(DataGridViewCell dc, bool enabled)
		{
			//toggle read-only state
			dc.ReadOnly = !enabled;
			if (enabled)
			{
				//restore cell style to the default value
				dc.Style.BackColor = dc.OwningColumn.DefaultCellStyle.BackColor;
				dc.Style.ForeColor = dc.OwningColumn.DefaultCellStyle.ForeColor;
			}
			else
			{
				//gray out the cell
				dc.Style.BackColor = Color.LightGray;
				dc.Style.ForeColor = Color.DarkGray;
			}
		}
		private void lueDataSourceType_EditValueChanged(object sender, EventArgs e)
		{
			if (lueDataSourceType.ItemIndex == 0 || lueDataSourceType.Text == "--Select--")
			{
				DataBindNutrient();
				return;
			}
			List<AIMNutrientInfo> nutrient = new List<AIMNutrientInfo>();
			nutrient = AIMManager.GetAIMNutrient(rbActiveRecords.Checked);
			Int32 currentRowNum = 0;
			if (this.grdNutrientView != null)
			{
				Int32.TryParse(grdNutrientView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdNutrient.DataSource = nutrient;
			grdNutrientView.FocusedRowHandle = currentRowNum;
		}
		private void btnClear_Click(object sender, EventArgs e)
		{
			DataBindlueNutrient();
			DataBindNutrient();
		}
		private void repluePriority_EditValueChanged(object sender, EventArgs e)
		{
			Int32 priority;
			AIMNutrientInfo nutrient = new AIMNutrientInfo();
			nutrient = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
			priority = (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).EditValue;
			if (priority == (Int32)PriorityUsedValue.DataSource && nutrient.DataSourceTypeID != 0)
			{
				UpdatePriority(nutrient.AIMNutrientID, priority, nutrient.DataSourceTypeID, nutrient.AIMNutrientName.ToString());
			}

			else if (priority == (Int32)PriorityUsedValue.First || priority == (Int32)PriorityUsedValue.Last)
			{
				UpdatePriority(nutrient.AIMNutrientID, priority, nutrient.DataSourceTypeID, nutrient.AIMNutrientName.ToString());
			}
		}
		private void replueDataSourceTypeNutrient_EditValueChanged(object sender, EventArgs e)
		{
			Int32 datasourceID;
			AIMNutrientInfo nutrient = new AIMNutrientInfo();
			nutrient = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
			datasourceID = (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).EditValue;
			if (nutrient.PriorityAIMUsedValue == (Int32)PriorityUsedValue.DataSource)
			{
				UpdatePriority(nutrient.AIMNutrientID, nutrient.PriorityAIMUsedValue, datasourceID, nutrient.AIMNutrientName.ToString());
			}
		}
		private void UpdatePriority(Int32 nutrientID, Int32 priority, Int32 databaseID, String nutrientName)
		{
			DialogResult dr = new DialogResult();
			dr = MessageBox.Show("Do you want to update the priority for previously-loaded analyzer data for Aim Nutrient '" + nutrientName + "' ?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dr == DialogResult.Yes)
			{
				//TODO
				//commented until the progress bar is working
				///////////AIMManager.UpdateSampleAimPriority(nutrientID, priority, databaseID);
				MessageBox.Show("Update complete.", "Information...", MessageBoxButtons.OK);
			}
		}
		private void CallSpButton_Click(object sender, EventArgs e)
		{
			progress.Maximum = 500;
			CallSpButton.Enabled = false;
			progress.Visible = true;
			progress.Value = 0;
			SpCaller.RunWorkerAsync();
		}
		private void SpCaller_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			//var self = (BackgroundWorker)sender;
			//_database = (SqlDatabase)EnterpriseLibraryContainer.Current.GetInstance<Database>("ConnectionString");

			//using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ToString()))
			//{
			//	cn.FireInfoMessageEventOnUserErrors = true;

			//	cn.Open();
			//	cn.InfoMessage += (o, args) => self.ReportProgress(0, args.Message);

			//	using (var cmd = cn.CreateCommand())
			//	{
			//		//cmd.CommandText = "usp_LongProcess";
			//		cmd.CommandText = "usp_SampleAim_UPD_Priority";
			//		cmd.CommandType = CommandType.StoredProcedure;
			//		cmd.CommandTimeout = 0;
			//		cmd.ExecuteNonQuery();
			//	}
			//}

		}
		private void SpCaller_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			CallSpButton.Enabled = true;
			progress.Visible = false;
		}
		private void SpCaller_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			var message = Convert.ToString(e.UserState);
			Debug.WriteLine(message);
			statusLabel.Text = message;

			if (message.EndsWith(" PERCENT COMPLETE"))
			{
				int percent;
				if (int.TryParse(message.Split(' ')[0], out percent))
					progress.Value = percent;
			}
		}
		private void lueUnitType_Click(object sender, EventArgs e)
		{
			lueUnitType.SelectAll();
		}
		private void lueUnitType_Enter(object sender, EventArgs e)
		{
			lueUnitType.SelectAll();
		}
		private void luePriority_Enter(object sender, EventArgs e)
		{
			luePriority.SelectAll();
		}
		private void luePriority_Click(object sender, EventArgs e)
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
		#endregion

		#region Owner ---------------------------------------------------------------------------
		private void DataBindOwner()
		{
			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
			aimOwner = AIMManager.GetAIMOwner(rbActiveRecords.Checked);
			Int32 currentRowNum = 0;
			currentRowNum = grdOwnerView.FocusedRowHandle;
			grdOwner.DataSource = aimOwner;
			grdOwnerView.FocusedRowHandle = currentRowNum;


		}
		private void DataBindOwnerMaterialStockStatus()
		{
			//6/18/20 - Owner dropdown on Material Stock Status tab                                  

			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
			aimOwner = AIMManager.GetAIMOwner(rbActiveRecords.Checked);
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
			lueOwner_MaterialStockStatus.Properties.DataSource = aimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwner_MaterialStockStatus, "AIMOwnerID", "OwnerName");        //task 1133 FG 7/21/2026 - show only Owner Name in the dropdown instead of Owner Code

			if (aimOwner.Count == 2)
			{
				//selecting value if there is only one
				lueOwner_MaterialStockStatus.EditValue = aimOwner[1].AIMOwnerID;
			}
			else
			{
				lueOwner_MaterialStockStatus.EditValue = 0;
			}
		}
		private void btnAddOwner_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtOwnerCode.Text) == true)
				{
					MessageBox.Show("Please enter an Owner Code.", "Warning...", MessageBoxButtons.OK);
					txtOwnerCode.Focus();
					return;
				}

				if (txtOwnerName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter an Owner Name.", "Warning...", MessageBoxButtons.OK);
					txtOwnerName.Focus();
					return;
				}

				AIMOwnerInfo owner = new AIMOwnerInfo();
				owner.AIMOwnerID = 0;
				owner.OwnerCode = txtOwnerCode.Text;
				owner.OwnerName = txtOwnerName.Text;
				owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//4/28/17 - check system setting to determine stp
				if (_allowAIMInactive == false)
				{
					AIMManager.SaveAIMOwner_WithSampleCheck(ref owner, false);
				}
				else
				{
					AIMManager.SaveAIMOwner(ref owner, false);
				}

				txtOwnerCode.Text = String.Empty;
				txtOwnerName.Text = String.Empty;
				DataBindOwner();

				//7/1/20
				DataBindOwnerMaterialStockStatus();

				//7/2/20 - refresh owner dropdown on ownerlocation tab
				DataBindOneTimeOwnerLocation();

				txtOwnerCode.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewOwner_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdOwner, "AIM Owner");
				CFCAIMCore.Global.previewGridResults(grdOwner, "AIM Owner");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteOwner_Click(object sender, EventArgs e)
		{
			try
			{
				ErrorInfo exists = new ErrorInfo();
				ErrorInfo result = new ErrorInfo();
				ErrorInfo resultInactivate = new ErrorInfo();
				AIMOwnerInfo owner = new AIMOwnerInfo();
				owner = (AIMOwnerInfo)grdOwnerView.GetFocusedRow();
				owner.IsActive = !owner.IsActive;
				owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//10/13/17
				if (rbActiveRecords.Checked == true)
				{
					DialogResult dr = new DialogResult();
					dr = MessageBox.Show("Inactivate Owner '" + owner.OwnerName + "' and any associated Owner Locations?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)
					{
						return;
					}
				}

				//4/28/17 - check system setting to determine stp
				if (_allowAIMInactive == false)
				{
					result = AIMManager.SaveAIMOwner_WithSampleCheck(ref owner, false);
				}
				else
				{
					result = AIMManager.SaveAIMOwner(ref owner, false);

					//12/16/20
					if (result.ErrorNumber == 3)
					{
						ShowUserDefinedImportList(result, owner.AIMOwnerID, "AIMOWNER");
					}
				}

				CheckSampleTable(result.ErrorNumber, "Owner", owner.AIMOwnerID, 0);
				DataBindOwner();

				//7/2/20
				DataBindOwnerMaterialStockStatus();

				//7/2/20 - refresh owner dropdown on ownerlocation tab
				DataBindOneTimeOwnerLocation();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdOwnerView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				AIMOwnerInfo owner = new AIMOwnerInfo();
				owner = (AIMOwnerInfo)grdOwnerView.GetFocusedRow();
				owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//4/28/17 - check system setting to determine stp
				if (_allowAIMInactive == false)
				{
					AIMManager.SaveAIMOwner_WithSampleCheck(ref owner, false);
				}
				else
				{
					AIMManager.SaveAIMOwner(ref owner, false);
				}
				DataBindOwner();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdOwnerView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void repbtnFinalDeleteOwner_Click(Object sender, EventArgs e)
		{
			try
			{
				ErrorInfo result = new ErrorInfo();
				AIMOwnerInfo owner = new AIMOwnerInfo();
				owner = (AIMOwnerInfo)grdOwnerView.GetFocusedRow();

				DialogResult dr = new DialogResult();
				dr = MessageBox.Show("Delete Owner '" + owner.OwnerName + "' and any inactive Owner Locations associated with this Owner?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr != DialogResult.Yes)
				{
					return;
				}
				result = AIMManager.DeleteAIMOwner(owner.AIMOwnerID);
				if (result.ErrorNumber == 3)
				{
					//12/16/20
					ShowUserDefinedImportList(result, owner.AIMOwnerID, "AIMOWNER");

					return;
				}
				DataBindOwner();

				//7/1/20
				DataBindOwnerMaterialStockStatus();

				//7/2/20 - refresh owner dropdown on ownerlocation tab
				DataBindOneTimeOwnerLocation();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Owner Location ------------------------------------------------------------------
		private void DataBindOneTimeOwnerLocation()
		{
			List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
			owner = AIMManager.GetAIMOwner(true);
			owner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--"
				}
			);

			lueOwner.Properties.DataSource = owner;
			XtraEditors.LoadLookUpEdit(ref lueOwner, "AIMOwnerID", "OwnerCode");
			lueOwner.Properties.Columns["OwnerCode"].Visible = true;
			lueOwner.Properties.Columns["OwnerName"].Visible = true;

			//9/1/17 
			if (owner.Count == 2)
			{
				//selecting value if there is only one
				lueOwner.EditValue = owner[1].AIMOwnerID;
			}
			else
			{
				lueOwner.EditValue = 0;
			}

			//populating replueSource
			replueOwner.DataSource = owner;
			XtraEditors.LoadGridLookUpEdit(ref replueOwner, "AIMOwnerID", "OwnerCode");
			replueOwner.Columns["OwnerCode"].Visible = true;
			replueOwner.Columns["OwnerName"].Visible = true;

			//3/17/17
			//-- tried using enum, couldn't get to work (but left EnumYesNo in CFCAIMCORE to test this later)
			List<YesNoInfo> yesNo = new List<YesNoInfo>();
			yesNo.Insert
			(
				0,
				new YesNoInfo
				{
					YesNoID = 0,
					YesNoString = "False"
				}
			);
			yesNo.Insert
			(
				1,
				new YesNoInfo
				{
					YesNoID = 1,
					YesNoString = "True"
				}
			);
			lueIsFormulationPlant.Properties.DataSource = yesNo;
			XtraEditors.LoadLookUpEdit(ref lueIsFormulationPlant, "YesNoID", "YesNoString");
			lueIsFormulationPlant.Properties.Columns["YesNoString"].Caption = " ";
			lueIsFormulationPlant.Properties.Columns["YesNoID"].Visible = false;
			lueIsFormulationPlant.EditValue = 1;

			//populating replue
			replueIsFormulationPlant.DataSource = yesNo;
			XtraEditors.LoadGridLookUpEdit(ref replueIsFormulationPlant, "YesNoID", "YesNoString");
			replueIsFormulationPlant.Columns["YesNoString"].Visible = true;



		}
		private void DataBindOwnerLocation()
		{
			//grid has to populated after replue
			Int32 rowNum = 0;
			Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out rowNum);
			grdOwnerLocation.DataSource = AIMManager.GetAIMOwnerLocation(rbActiveRecords.Checked);
			grdOwnerLocationView.FocusedRowHandle = rowNum;
		}
		private void btnPreviewOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdOwnerLocation, "AIM Owner Location");
				CFCAIMCore.Global.previewGridResults(this.grdOwnerLocation, "AIM Owner Location");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{

				if ((Int32)lueOwner.EditValue == 0)
				{
					MessageBox.Show("Please select an Owner Code.", "Warning...", MessageBoxButtons.OK);
					lueOwner.Focus();
					lueOwner.ShowPopup();
					return;
				}

				if (string.IsNullOrWhiteSpace(txtOwnerLocationCode.Text) == true)
				{
					MessageBox.Show("Please enter an Owner Location Code.", "Warning...", MessageBoxButtons.OK);
					txtOwnerLocationCode.Focus();
					return;
				}

				if (txtOwnerLocationName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter an Owner Location Name.", "Warning...", MessageBoxButtons.OK);
					txtOwnerLocationName.Focus();
					return;
				}

				AIMOwnerLocationInfo ownerLocation = new AIMOwnerLocationInfo();
				ownerLocation.AIMOwnerLocationID = 0;
				ownerLocation.AIMOwnerID = (Int32)lueOwner.EditValue;
				ownerLocation.LocationCode = txtOwnerLocationCode.Text.Trim();
				ownerLocation.LocationName = txtOwnerLocationName.Text.Trim();
				ownerLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;


				Boolean yesNo = true;
				if ((Int32)lueIsFormulationPlant.EditValue == 0)
				{
					yesNo = false;
				}
				ownerLocation.IsFormulationPlant = yesNo;
				ErrorInfo error = new ErrorInfo();

				//4/28/17 - check system setting to determine stp
				if (_allowAIMInactive == false)
				{
					error = AIMManager.SaveAIMOwnerLocation_WithSampleCheck(ref ownerLocation);
				}
				else
				{
					error = AIMManager.SaveAIMOwnerLocation(ref ownerLocation);
				}
				if (error.ErrorNumber > 0)
				{
					return;
				}
				lueOwner.EditValue = 0;
				txtOwnerLocationName.Text = String.Empty;
				txtOwnerLocationCode.Text = String.Empty;

				//3/17/17
				lueIsFormulationPlant.EditValue = 1;

				lueOwner.Focus();
				DataBindOwnerLocation();

				//7/1/20
				DataBindOwnerMaterialStockStatus();


				lueOwner.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdOwnerLocationView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				if (rbDeletedRecords.Checked == true)
				{
					MessageBox.Show("You cannot make changes to inactive records.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				AIMOwnerLocationInfo location = new AIMOwnerLocationInfo();
				location = (AIMOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
				location.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//4/28/17 - check system setting to determine stp
				if (_allowAIMInactive == false)
				{
					AIMManager.SaveAIMOwnerLocation_WithSampleCheck(ref location);
				}
				else
				{
					AIMManager.SaveAIMOwnerLocation(ref location);
				}
				DataBindOwnerLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdOwnerLocationView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void repbtnDeleteOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{
				//6/13/22 - added warning. Anel at Perfection ran into this - someone there inactivated an owner-location, 
				//			so any samples with that owner-location no longer showed up in Analysis.				
				DialogResult dr = new DialogResult();
				String msg;
				msg = "By inactivating this owner-location, any sample data linked to this location will no longer show in Analysis. Continue?";
				dr = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if (dr != DialogResult.Yes)
				{
					return;
				}


				ErrorInfo result = new ErrorInfo();
				AIMOwnerLocationInfo location = new AIMOwnerLocationInfo();
				location = (AIMOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
				location.IsActive = !location.IsActive;
				location.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;


				//check system setting to determine stp
				if (_allowAIMInactive == false)
				{
					result = AIMManager.SaveAIMOwnerLocation_WithSampleCheck(ref location);
				}
				else
				{
					result = AIMManager.SaveAIMOwnerLocation(ref location);

					//11/28/22 - if owner-location exists on OwnerLocationGroup table, inactivate it there also.
					List<AIMOwnerLocationGroupDetailInfo> group = new List<AIMOwnerLocationGroupDetailInfo>();
					group = AIMManager.GetOwnerLocationGroupDetail_AIMOwnerLocationID(location.AIMOwnerLocationID);
					if (group != null)
					{
						if (group.Count > 0)
						{
							ErrorInfo updateStatus = new ErrorInfo();
							updateStatus = AIMManager.UpdateOwnerLocationGroupDetailIsActive(location.AIMOwnerLocationID, false, userGlobal.CFCLoginUser.CFCUserID);
							DataBindOwnerLocationGroupsDetail();
						}
					}


					//12/16/20
					if (result.ErrorNumber == 3)
					{
						ShowUserDefinedImportList(result, location.AIMOwnerLocationID, "AIMOWNERLOCATION");
					}

				}
				CheckSampleTable(result.ErrorNumber, "OwnerLocation", location.AIMOwnerID, location.AIMOwnerLocationID);
				DataBindOwnerLocation();

				//Refresh ownerlocation dropdown on OwnerLocationGroup tab				
				DataBindlueOwnerCode_Group();
				DataBindlueOwnerLocationGroups();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwner_Enter(object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}
		private void lueOwner_Click(object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}
		private void repbtnFinalDeleteOwnerLocation_Click(Object sender, EventArgs e)
		{
			DialogResult dr = new DialogResult();

			//6/13/22 - added warning. Anel at Perfection ran into this - someone there inactivated an owner-location, 
			//			so any samples with that owner-location no longer showed up in Analysis.				
			String msg;
			msg = "By deleting this owner-location, any sample data linked to this location will no longer show in Analysis. Continue?";
			dr = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
			if (dr != DialogResult.Yes)
			{
				return;
			}

			ErrorInfo result = new ErrorInfo();
			AIMOwnerLocationInfo location = new AIMOwnerLocationInfo();
			location = (AIMOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();

			dr = MessageBox.Show("Delete Owner Location '" + location.LocationName + "' ?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dr != DialogResult.Yes)
			{
				return;
			}

			//7/2/20 - delete any records in the AIMMaterialStockStatus table for this AIMLocationID
			AIMManager.DeleteAIMMaterialStockStatus(location.AIMOwnerLocationID);


			result = AIMManager.DeleteAIMOwnerLocation(location.AIMOwnerID, location.AIMOwnerLocationID);
			if (result.ErrorNumber == 3)
			{
				//12/16/20
				ShowUserDefinedImportList(result, location.AIMOwnerLocationID, "AIMOWNERLOCATION");

				return;
			}
			DataBindOwnerLocation();

			//7/2/20
			DataBindOwnerMaterialStockStatus();
			DataBindMaterialStockStatus();



		}
		private void lueIsFormulationPlant_Click(object sender, EventArgs e)
		{
			lueIsFormulationPlant.SelectAll();
		}

		private void lueIsFormulationPlant_Enter(object sender, EventArgs e)
		{
			lueIsFormulationPlant.SelectAll();
		}


		#endregion

		#region Supplier ------------------------------------------------------------------------
		private void DataBindSupplier()
		{
			{
				int rowNum = 0;
				if (grdSupplierView != null)
				{
					Int32.TryParse(grdSupplierView.FocusedRowHandle.ToString(), out rowNum);
				}
				grdSupplier.DataSource = AIMManager.GetSupplier(rbActiveRecords.Checked);
				grdSupplierView.FocusedRowHandle = rowNum;
			}
		}
		private void btnAddSupplier_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierInfo aimExists = new AIMSupplierInfo();

				if (txtSupplierCode.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Supplier Code.", "Warning...", MessageBoxButtons.OK);
					txtSupplierCode.Focus();
					return;
				}
				if (txtSupplierName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Supplier Name.", "Warning...", MessageBoxButtons.OK);
					txtSupplierName.Focus();
					return;
				}

				//10/12/17 - see if code already exists			 				
				aimExists = AIMManager.GetSupplierSupplierCode(txtSupplierCode.Text);
				if (aimExists != null)
				{
					MessageBox.Show("This Supplier Code already exists. Please enter a different code.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				aimExists = new AIMSupplierInfo();
				aimExists = AIMManager.GetSupplierSupplierName(txtSupplierName.Text);
				if (aimExists != null)
				{
					MessageBox.Show("This Supplier Name already exists. Please enter a different name.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				AIMSupplierInfo supplier = new AIMSupplierInfo();
				supplier.AIMSupplierID = 0;
				supplier.AIMSupplierCode = txtSupplierCode.Text;
				supplier.SupplierName = txtSupplierName.Text;
				supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (string.IsNullOrWhiteSpace(txtSupplierName.Text) == false)
				{

					//4/28/17 - check system setting to determine which stp to use
					if (_allowAIMInactive == false)
					{
						AIMManager.SaveAIMSupplier_WithSampleCheck(ref supplier, false);
					}
					else
					{
						AIMManager.SaveAIMSupplier(ref supplier, false);
					}

					txtSupplierCode.Text = String.Empty;
					txtSupplierName.Text = String.Empty;
					DataBindSupplier();
					txtSupplierCode.Focus();

					//7/2/20 - refresh supplier dropdown on Supplier-Location tab
					DataBindSupplierLocation();

					//7/2/20 - refresh supplier dropdown on Guarantees tab
					DataBindLueGuarantee();

					//8/30/23 - refresh supplier dropdown on Supplier-Groups tab
					DataBindlueSupplierCodeGroup();
					DataBindlueSupplierCode_Group();
					DataBindlueSupplier_SL();

				}
				else
				{
					MessageBox.Show("Please enter a Supplier Name.", "Warning...", MessageBoxButtons.OK);
					txtSupplierName.Focus();
				}
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
				Boolean isActive;
				ErrorInfo result = new ErrorInfo();
				AIMSupplierInfo supplier = new AIMSupplierInfo();

				supplier = (AIMSupplierInfo)grdSupplierView.GetFocusedRow();
				supplier.IsActive = !supplier.IsActive;
				supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				if (rbActiveRecords.Checked == true)
				{
					DialogResult dr = new DialogResult();
					dr = MessageBox.Show("Inactivate Supplier '" + supplier.SupplierName + "' and any associated Supplier Locations?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)
					{
						return;
					}
				}

				if (_allowAIMInactive == false)
				{
					result = AIMManager.SaveAIMSupplier_WithSampleCheck(ref supplier, false);
				}
				else
				{
					result = AIMManager.SaveAIMSupplier(ref supplier, false);
					if (result.ErrorNumber == 3)                //12/16/20
					{
						ShowUserDefinedImportList(result, supplier.AIMSupplierID, "AIMSUPPLIER");
					}
				}

				CheckSampleTable(result.ErrorNumber, "Supplier", supplier.AIMSupplierID, 0);
				DataBindSupplier();

				//---------------------------------------------------------------------------
				//8/30/23 - if supplier exists on AIMSupplierGroupDetail table, inactivate/re-activate it there also.
				if (rbActiveRecords.Checked)
				{
					isActive = false;
				}
				else
				{
					isActive = true;
				}

				try
				{
					List<AIMSupplierGroupDetailInfo> loc = new List<AIMSupplierGroupDetailInfo>();
					loc = AIMManager.GetSupplierGroupDetail_AIMSupplierID(supplier.AIMSupplierID);
					if (loc != null)
					{
						if (loc.Count > 0)
						{
							ErrorInfo updateStatus = new ErrorInfo();
							AIMManager.UpdateSupplierGroupDetailIsActive(supplier.AIMSupplierID, isActive, userGlobal.CFCLoginUser.CFCUserID);
							DataBindSupplierGroupDetail();
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				DataBindlueSupplierCodeGroup();
				DataBindlueSupplierCode_Group();
				//---------------------------------------------------------------------------

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
				//AIMGlobal.previewGridResults(this.grdSupplier, "AIM Supplier");
				CFCAIMCore.Global.previewGridResults(this.grdSupplier, "AIM Supplier");
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
				if (rbDeletedRecords.Checked == true)
				{
					MessageBox.Show("You cannot make any changes to inactive Suppliers.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				}
				else
				{
					AIMSupplierInfo supplier = new AIMSupplierInfo();
					supplier = (AIMSupplierInfo)grdSupplierView.GetFocusedRow();
					supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					AIMManager.SaveAIMSupplier(ref supplier, false);
				}
				DataBindSupplier();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void repbtnFinalDeleteSupplier_Click(Object sender, EventArgs e)
		{

			try
			{
				ErrorInfo result = new ErrorInfo();
				AIMSupplierInfo supplier = new AIMSupplierInfo();
				supplier = (AIMSupplierInfo)grdSupplierView.GetFocusedRow();

				DialogResult dr = new DialogResult();
				dr = MessageBox.Show("Delete Supplier '" + supplier.SupplierName + "' and any inactive Supplier Locations associated with this Supplier?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr != DialogResult.Yes)
				{
					return;
				}
				result = AIMManager.DeleteAIMSupplier(supplier.AIMSupplierID);
				if (result.ErrorNumber == 3)
				{
					//12/16/20
					ShowUserDefinedImportList(result, supplier.AIMSupplierID, "AIMSUPPLIER");

					return;
				}
				DataBindSupplier();

				//7/2/20 - refresh supplier dropdown on Supplier-Location tab
				DataBindSupplierLocation();

				//7/2/20 - refresh supplier dropdown on Guarantees tab
				DataBindLueGuarantee();

				//----------------------------------------------------------------------------
				//8/30/23
				List<AIMSupplierGroupDetailInfo> loc = new List<AIMSupplierGroupDetailInfo>();
				loc = AIMManager.GetSupplierGroupDetail_AIMSupplierID(supplier.AIMSupplierID);
				if (loc != null)
				{
					if (loc.Count > 0)
					{
						AIMManager.DeleteSupplierLocationGroupAIMSupplierGroupID(loc[0].AIMSupplierGroupID);
					}
				}
				AIMManager.DeleteSupplierGroupDetail_AIMSupplierID(supplier.AIMSupplierID);
				DataBindSupplierGroupDetail();
				DataBindSuppliersForSelectedSupplierGroup();
				DataBindlueSupplierCodeGroup();
				///////DataBindlueSupplierCode_Group();
				//----------------------------------------------------------------------------

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Supplier Location ---------------------------------------------------------------
		private void DataBindSupplierLocation()
		{
			//populating lueSupplier
			Int32 rowNum = 0;
			List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
			List<AIMSupplierInfo> supplierNew = new List<AIMSupplierInfo>();

			supplierNew = AIMManager.GetSupplier(true);

			if (rbDeletedRecords.Checked == true)
			{
				supplier = AIMManager.GetAllSupplier();
			}
			else
			{
				supplier = AIMManager.GetSupplier(true);
			}

			supplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--"
				}
			);

			supplierNew.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"

				}
			);

			lueSupplier.Properties.DataSource = supplierNew;
			XtraEditors.LoadLookUpEdit(ref lueSupplier, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			lueSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			//task 1133 FG 7/21/2026 - bind the Supplier Name dropdown to the same list as the Supplier Code dropdown
			lueSupplierName.Properties.DataSource = supplierNew;
			XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
			lueSupplierName.Properties.Columns["AIMSupplierCode"].Caption = "AIM Supplier Code";

			//9/1/17 
			if (supplierNew.Count == 2)
			{
				//selecting value if there is only one
				lueSupplier.EditValue = supplierNew[1].AIMSupplierID;
			}
			else
			{
				lueSupplier.EditValue = 0;
			}

			//populating replueSource
			replueSupplier.DataSource = supplier;
			XtraEditors.LoadGridLookUpEdit(ref replueSupplier, "AIMSupplierID", "AIMSupplierCode");

			if (grdSupplierLocationView != null)
			{
				Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out rowNum);
			}
			grdSupplierLocation.DataSource = AIMManager.GetAllSupplierLocation(rbActiveRecords.Checked);
			grdSupplierLocationView.FocusedRowHandle = rowNum;
		}
		private void DataBindLueSupplierLocation()
		{
			List<AIMSupplierLocationGroupedInfo> aimSupplierLocation = new List<AIMSupplierLocationGroupedInfo>();
			aimSupplierLocation = AIMManager.GetAllSupplierLocationGrouped(true);

			lueSupplierLocationCode.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationCode, "ID", "AIMSupplierLocationCode");
			lueSupplierLocationCode.Properties.Columns["AIMSupplierLocationCode"].Caption = "AIM Supplier Location Code";

			lueSupplierLocationName.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName, "ID", "SupplierLocationName");
			lueSupplierLocationName.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			lueSupplierLocationCode.Text = "";
			lueSupplierLocationName.Text = "";

		}                                                   //added //7/26/19
		private void btnAddSupplierLocation_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierLocationInfo supplierLocation = new AIMSupplierLocationInfo();

				if (lueSupplier.Text.ToString() == "--Select--" || lueSupplier.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select a Supplier Code.", "Warning...", MessageBoxButtons.OK);
					lueSupplier.ShowPopup();
					return;
				}

				if (string.IsNullOrWhiteSpace(txtSupplierLocationCode.Text) == true)
				{
					MessageBox.Show("Please enter a Supplier Location Code.", "Warning...", MessageBoxButtons.OK);
					txtSupplierLocationCode.Focus();
					return;
				}

				if (string.IsNullOrWhiteSpace(txtSupplierLocationName.Text) == true)
				{
					MessageBox.Show("Please enter a Supplier Location Name.", "Warning...", MessageBoxButtons.OK);
					txtSupplierLocationName.Focus();
					return;
				}

				supplierLocation.AIMSupplierLocationID = 0;
				supplierLocation.AIMSupplierID = (Int32)lueSupplier.EditValue;
				supplierLocation.AIMSupplierLocationCode = txtSupplierLocationCode.Text;
				supplierLocation.SupplierLocationName = txtSupplierLocationName.Text;
				supplierLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				if (txtSupplierLocationName.Text.Trim().Length > 0)
				{

					//4/28/17 - check system setting to determine stp
					if (_allowAIMInactive == false)
					{
						AIMManager.SaveSupplierLocation_WithSampleCheck(ref supplierLocation);
					}
					else
					{
						AIMManager.SaveSupplierLocation(ref supplierLocation);
					}

					lueSupplier.EditValue = 0;
					txtSupplierLocationCode.Text = String.Empty;
					txtSupplierLocationName.Text = String.Empty;
					DataBindSupplierLocation();

					//7/26/19
					lueSupplierLocationCode.Text = String.Empty;
					lueSupplierLocationName.Text = String.Empty;
					DataBindLueSupplierLocation();


					lueSupplier.Focus();
				}
				else
				{
					MessageBox.Show("Please select Supplier Code, and enter both Supplier Location Code and Supplier Location Name.", "Warning...", MessageBoxButtons.OK);
					txtSupplierLocationCode.Focus();
				}


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
				Boolean isActive;
				ErrorInfo result = new ErrorInfo();
				AIMSupplierLocationInfo supplier = new AIMSupplierLocationInfo();
				supplier = (AIMSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
				supplier.IsActive = !supplier.IsActive;
				supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (_allowAIMInactive == false)
				{
					result = AIMManager.SaveSupplierLocation_WithSampleCheck(ref supplier);
				}
				else
				{
					result = AIMManager.SaveSupplierLocation(ref supplier);

					//12/16/20
					if (result.ErrorNumber == 3)
					{
						ShowUserDefinedImportList(result, supplier.AIMSupplierLocationID, "AIMSUPPLIERLOCATION");
					}
				}

				CheckSampleTable(result.ErrorNumber, "SupplierLocation", supplier.AIMSupplierID, supplier.AIMSupplierLocationID);
				DataBindSupplierLocation();
				DataBindLueSupplierLocation();

				//---------------------------------------------------------------------------
				//8/30/23 - if supplier-location exists on AIMSupplierLocationGroup table, inactivate/re-activate it there also.
				if (rbActiveRecords.Checked)
				{
					isActive = false;
				}
				else
				{
					isActive = true;
				}

				try
				{
					List<AIMSupplierLocationGroupInfo> loc = new List<AIMSupplierLocationGroupInfo>();
					loc = AIMManager.GetSupplierLocationGroup_AIMSupplierLocationID(supplier.AIMSupplierLocationID);
					if (loc != null)
					{
						if (loc.Count > 0)
						{
							ErrorInfo updateStatus = new ErrorInfo();
							AIMManager.UpdateSupplierLocationGroupIsActive(supplier.AIMSupplierLocationID, isActive, userGlobal.CFCLoginUser.CFCUserID);
							DataBindSuppliersForSelectedSupplierGroup();
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				DataBindlueSupplierCodeGroup();
				DataBindlueSupplierCode_Group();
				//---------------------------------------------------------------------------

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewSupplierLocation_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdSupplierLocation, "AIM Supplier Location");
				CFCAIMCore.Global.previewGridResults(this.grdSupplierLocation, "AIM Supplier Location");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierLocationView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				if (rbDeletedRecords.Checked == true)
				{
					MessageBox.Show("You cannot make any changes to inactive Supplier Locations.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				}
				else
				{
					AIMSupplierLocationInfo supplier = new AIMSupplierLocationInfo();
					supplier = (AIMSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
					supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

					//check system setting to determine stp
					if (_allowAIMInactive == false)
					{
						AIMManager.SaveSupplierLocation_WithSampleCheck(ref supplier);
					}
					else
					{
						AIMManager.SaveSupplierLocation(ref supplier);
					}
				}
				DataBindSupplierLocation();

				//7/26/19                
				DataBindLueSupplierLocation();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierLocationView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		//task 1133 FG 7/21/2026 - keep the Supplier Code and Supplier Name dropdowns in sync
		private void lueSupplier_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueSupplier, ref lueSupplierName);
		}
		private void lueSupplierName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueSupplierName, ref lueSupplier);
		}
		private void lueSupplierName_Click(object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}
		private void lueSupplierName_Enter(object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}
		private void lueSupplier_Click(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}
		private void lueSupplier_Enter(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}
		private void lueSupplierLocationCode_Click(object sender, EventArgs e)
		{
			lueSupplierLocationCode.SelectAll();
		}                       //7/26/19
		private void lueSupplierLocationName_Click(object sender, EventArgs e)
		{
			lueSupplierLocationName.SelectAll();
		}                       //7/26/19
		private void lueSupplierLocationCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{

				if (lueSupplierLocationCode.Text == "")
				{
					return;
				}

				SyncValueLue(ref lueSupplierLocationCode, ref lueSupplierLocationName);
				txtSupplierLocationCode.Text = lueSupplierLocationCode.Text;
				txtSupplierLocationName.Text = lueSupplierLocationName.Text;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}           //7/26/19
		private void lueSupplierLocationName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (lueSupplierLocationName.Text == "")
				{
					return;
				}

				SyncValueLue(ref lueSupplierLocationName, ref lueSupplierLocationCode);
				txtSupplierLocationCode.Text = lueSupplierLocationCode.Text;
				txtSupplierLocationName.Text = lueSupplierLocationName.Text;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}           //7/26/19
		private void repbtnFinalDeleteSupplierLocation_Click(Object sender, EventArgs e)
		{
			ErrorInfo result = new ErrorInfo();
			AIMSupplierLocationInfo location = new AIMSupplierLocationInfo();
			location = (AIMSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();

			DialogResult dr = new DialogResult();
			dr = MessageBox.Show("Delete Supplier Location '" + location.SupplierLocationName + "' ?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dr != DialogResult.Yes)
			{
				return;
			}
			result = AIMManager.DeleteAIMSupplierLocation(location.AIMSupplierID, location.AIMSupplierLocationID);
			if (result.ErrorNumber == 3)
			{

				//12/16/20
				ShowUserDefinedImportList(result, location.AIMSupplierLocationID, "AIMSUPPLIERLOCATION");

				return;
			}
			DataBindSupplierLocation();

			//7/26/19
			DataBindLueSupplierLocation();

			//8/30/23
			//Delete any records in the AIMSupplierLocationGroup for this SupplierLocationID				
			AIMManager.DeleteSupplierLocationGroupAIMSupplierLocationID(location.AIMSupplierLocationID);
			DataBindSuppliersForSelectedSupplierGroup();

			//8/30/23
			DataBindlueSupplierCodeGroup();

			//---------------------------------------------------------------------------


		}
		#endregion

		#region Supplier Location Guarantees ----------------------------------------------------
		private void DataBindLueGuarantee()
		{
			List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
			List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
			List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

			supplier = AIMManager.GetSupplier(true);
			aimNutrient = AIMManager.GetAIMNutrient(true);

			//TODO get only Ingredient not product
			aimMaterial = AIMManager.GetAIMMaterial(true);

			supplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"
				}
			);

			lueAimSupplier.Properties.DataSource = supplier;
			XtraEditors.LoadLookUpEdit(ref lueAimSupplier, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			lueAimSupplier.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			//task 1133 FG 7/21/2026 - bind the Supplier Name dropdown to the same list as the Supplier Code dropdown
			lueAimSupplierName.Properties.DataSource = supplier;
			XtraEditors.LoadLookUpEdit(ref lueAimSupplierName, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
			lueAimSupplierName.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			supplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					SupplierLocationName = "--Select--"
				}
			);

			_aimSupplierLocation = supplierLocation;

			aimNutrient.Insert
			(
				0,
				new AIMNutrientInfo
				{
					AIMNutrientID = 0,
					AIMNutrientName = "--Select--",
					AIMNutrientCode = "--Select--"
				}
			);

			lueAIMNutrientGuarantee.Properties.DataSource = aimNutrient;
			//task 1133 FG 7/21/2026 - existing dropdown now shows the Nutrient Code; the new dropdown next to it shows the Name
			XtraEditors.LoadLookUpEdit(ref lueAIMNutrientGuarantee, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

			//task 1133 FG 7/21/2026 - bind the Nutrient Name dropdown to the same list as the Nutrient Code dropdown
			lueAIMNutrientGuaranteeName.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMNutrientGuaranteeName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			//8/11/20 commented
			//lueAIMNutrientGuarantee.Properties.Columns["AIMNutrientCode"].Visible = true;
			//lueAIMNutrientGuarantee.Properties.Columns["AIMNutrientName"].Visible = true;

			aimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialName = "--Select--",
					AIMMaterialCode = "--Select--"
				}
			);
			lueAIMMaterialGuarantee.Properties.DataSource = aimMaterial;
			//task 1133 FG 7/21/2026 - existing dropdown now shows the Material Code; the new dropdown next to it shows the Name
			XtraEditors.LoadLookUpEdit(ref lueAIMMaterialGuarantee, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

			//task 1133 FG 7/21/2026 - bind the Material Name dropdown to the same list as the Material Code dropdown
			lueAIMMaterialGuaranteeName.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMMaterialGuaranteeName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");

			//8/11/20 commented
			//lueAIMMaterialGuarantee.Properties.Columns["AIMMaterialCode"].Visible = true;
			//lueAIMMaterialGuarantee.Properties.Columns["AIMMaterialName"].Visible = true;


		}
		private void DataBindSupplierLocationLueGuarantee()
		{
			List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
			Int32 aimsupplierLocationID = 0;
			Int32 aimsupplierID = 0;
			String select = "";
			if (lueAimSupplier.EditValue != null)
			{
				Int32.TryParse(lueAimSupplier.EditValue.ToString(), out aimsupplierID);
			}
			supplierLocation = AIMManager.GetSupplierLocationForSupplier(aimsupplierID);

			if ((Int32)lueAimSupplier.EditValue == 0)
			{
				select = "--Select--";
			}
			else
			{
				select = "--All--";
			}
			supplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					AIMSupplierID = 0,
					AIMSupplierLocationCode = select,
					SupplierLocationName = select
				}
			);
			lueAimSupplierLocation.Properties.DataSource = supplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAimSupplierLocation, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
			lueAimSupplierLocation.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			//task 1133 FG 7/21/2026 - bind the Supplier Location Name dropdown to the same list as the Location Code dropdown
			lueAimSupplierLocationName.Properties.DataSource = supplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAimSupplierLocationName, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");
			lueAimSupplierLocationName.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";
			if (supplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueAimSupplierLocation.EditValue = supplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueAimSupplierLocation.EditValue = 0; // aimsupplierLocationID;	//11/14/24
			}
		}
		private void DataBindGuarantee()
		{

			List<AIMSupplierLocationGuaranteeInfo> guarantee = new List<AIMSupplierLocationGuaranteeInfo>();
			int rowNum = 0;
			if (grdGuaranteeView != null)
			{
				Int32.TryParse(grdGuaranteeView.FocusedRowHandle.ToString(), out rowNum);
			}

			//12/3/10 - changed stp to filter grid according to dropdown selections --> one stp for all the variations
			//guarantee = AIMManager.GetSupplierLocationGuarantee(rbActiveRecords.Checked);
			if (lueAimSupplier.EditValue != null && lueAimSupplierLocation.EditValue != null && lueAIMMaterialGuarantee.EditValue != null && lueAIMNutrientGuarantee.EditValue != null)
			{
				guarantee = AIMManager.GetSupplierLocationGuaranteesFiltered(rbActiveRecords.Checked, (Int32)lueAimSupplier.EditValue, (Int32)lueAimSupplierLocation.EditValue, (Int32)lueAIMMaterialGuarantee.EditValue, (Int32)lueAIMNutrientGuarantee.EditValue);
			}
			grdGuarantees.DataSource = guarantee;
			grdGuaranteeView.FocusedRowHandle = rowNum;


			//------------------------------------------------------------------------
			//fill GRID lookup edits	
			List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
			supplier = AIMManager.GetSupplier(true);
			replueAimSupplierGuarantee.DataSource = supplier;
			XtraEditors.LoadGridLookUpEdit(ref replueAimSupplierGuarantee, "AIMSupplierID", "SupplierName");
			replueAimSupplierGuarantee.Columns["SupplierName"].Visible = true;

			List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
			aimNutrient = AIMManager.GetAIMNutrient(true);
			replueAIMNutrientGuarantee.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMNutrientGuarantee, "AIMNutrientID", "AIMNutrientName");
			replueAIMNutrientGuarantee.Columns["AIMNutrientCode"].Visible = true;
			replueAIMNutrientGuarantee.Columns["AIMNutrientName"].Visible = true;

			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			//TODO get only Ingredient not product
			aimMaterial = AIMManager.GetAIMMaterial(true);
			replueAIMMaterialGuarantee.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMMaterialGuarantee, "AIMMaterialID", "AIMMaterialName");
			replueAIMMaterialGuarantee.Columns["AIMMaterialCode"].Visible = true;
			replueAIMMaterialGuarantee.Columns["AIMMaterialName"].Visible = true;



		}
		private void grdGuaranteeView_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
		{
			//5/17/17 - disabled filter on grid	 - was causing fatal error when clicking on any column filter in column header

			grdGuaranteeView.OptionsCustomization.AllowFilter = false;

			if (e.Column.FieldName == "AIMSupplierID") return;
			GridView gv = sender as GridView;

			Int32 supplierLocationID = 0;

			if (gv.Columns["AIMSupplierID"] != null)
			{
				//7/2/20
				if (gv.GetRowCellValue(e.RowHandle, gv.Columns["AIMSupplierID"]) != null)

				{
					supplierLocationID = Int32.Parse(gv.GetRowCellValue(e.RowHandle, gv.Columns["AIMSupplierID"]).ToString());
				}

			}

			if (gv.Columns["AIMSupplierLocationID"] != null && e.Column.FieldName == "AIMSupplierLocationID")
			{
				if (e.RowHandle == grdGuaranteeView.FocusedRowHandle)
				{
					List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
					supplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierLocationID);
					supplierLocation.Insert
					(
						0,
						new AIMSupplierLocationInfo
						{
							AIMSupplierLocationID = 0,
							SupplierLocationName = "--All--"
						}
					);
					replueAimSupplierLocationGuarantee.DataSource = supplierLocation;
					XtraEditors.LoadGridLookUpEdit(ref replueAimSupplierLocationGuarantee, "AIMSupplierLocationID", "SupplierLocationName");
					e.RepositoryItem = replueAimSupplierLocationGuarantee;
				}
				else
				{
					List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
					supplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierLocationID);
					supplierLocation.Insert
					(
						0,
						new AIMSupplierLocationInfo
						{
							AIMSupplierLocationID = 0,
							SupplierLocationName = "--All--"
						}
					);
					replueTempSupplierLocationGuarantee.DataSource = supplierLocation;
					XtraEditors.LoadGridLookUpEdit(ref replueTempSupplierLocationGuarantee, "AIMSupplierLocationID", "SupplierLocationName");
					e.RepositoryItem = replueTempSupplierLocationGuarantee;
				}
			}

		}
		private void btnAddGuarantee_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierLocationGuaranteeInfo supplierLocationGuarantee = new AIMSupplierLocationGuaranteeInfo();
				List<AIMSupplierLocationGuaranteeInfo> guarantee = new List<AIMSupplierLocationGuaranteeInfo>();
				String msg = "";

				if (lueAimSupplier.Text.ToString() == "--Select--" || lueAimSupplier.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select a Supplier Name.", "Warning...", MessageBoxButtons.OK);
					lueAimSupplier.ShowPopup();
					return;
				}
				if (lueAimSupplierLocation.Text.ToString() == "--Select--" || lueAimSupplierLocation.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select a Supplier Location Name.", "Warning...", MessageBoxButtons.OK);
					lueAimSupplierLocation.ShowPopup();
					return;
				}

				if (lueAIMMaterialGuarantee.Text.ToString() == "--Select--" || lueAIMMaterialGuarantee.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select a Material.", "Warning...", MessageBoxButtons.OK);
					lueAIMMaterialGuarantee.ShowPopup();
					return;
				}
				if (lueAIMNutrientGuarantee.Text.ToString() == "--Select--" || lueAIMNutrientGuarantee.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select a Nutrient.", "Warning...", MessageBoxButtons.OK);
					lueAIMNutrientGuarantee.ShowPopup();
					return;
				}

				//-------------------------------------------------------------------------------------------------------
				//7/28/20
				// User wants to add "ALL" supplier locations
				// 1) check for an existing "ALL" record that might be inactive (for the supplier-material-nutrient)
				//      a)  if an inactive "ALL" record exists, user must attempt to reactivate the "ALL" record.
				//          (it can only be reactivated when all the existing supplier-locations are either deleted or inactived.)
				// 2) check for existing supplier-locations (for the supplier-material-nutrient)

				//User wants to add specific supplier-supplier-loc-material-nutrient
				// 1) check for an active "ALL" record.
				//      a)  if an active "ALL" record exists, user must first delete/inactivate existing supplier-locations (for the supplier-material-nutrient)                

				if ((Int32)lueAimSupplierLocation.EditValue == 0)
				{
					//get guarantee records, no matter the status
					guarantee = AIMManager.GetSupplierLocationGuarantee((Int32)lueAimSupplier.EditValue, 0, (Int32)lueAIMMaterialGuarantee.EditValue, (Int32)lueAIMNutrientGuarantee.EditValue);
					if (guarantee != null)
					{
						if (guarantee.Count > 0 && guarantee[0].IsActive == false)
						{
							//An ALL record exists, but its status is inactive. User must first delete/inactivate any active guarantees for supplier/material/nutrient and then
							//reactivate the ALL record
							msg = "An INACTIVE 'ALL' supplier-location record exists on the table for: ";
							msg += Environment.NewLine;
							msg += Environment.NewLine + "     Supplier:  " + guarantee[0].SourceName;
							msg += Environment.NewLine;
							msg += Environment.NewLine + "     Material:  " + guarantee[0].AIMMaterialName;
							msg += Environment.NewLine + "     Nutrient:  " + guarantee[0].AIMNutrientName;
							msg += Environment.NewLine;
							msg += Environment.NewLine + "To add the record, you must re-activate the existing 'ALL' record. ";
							MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
							return;
						}
					}
				}

				//----------------------------------------------------------------
				if ((Int32)lueAimSupplierLocation.EditValue == 0)
				{
					guarantee = AIMManager.GetSupplierLocationGuarantee(true, (Int32)lueAimSupplier.EditValue, (Int32)lueAIMMaterialGuarantee.EditValue, (Int32)lueAIMNutrientGuarantee.EditValue);
					if (guarantee != null)
					{
						if (guarantee.Count > 0)
						{
							msg = "A supplier-location exists on the table for: ";
							msg += Environment.NewLine;
							msg += Environment.NewLine + "     Supplier:  " + guarantee[0].SourceName;
							msg += Environment.NewLine;
							msg += Environment.NewLine + "     Material:  " + guarantee[0].AIMMaterialName;
							msg += Environment.NewLine + "     Nutrient:  " + guarantee[0].AIMNutrientName;
							msg += Environment.NewLine;
							msg += Environment.NewLine + "Before adding ALL supplier-locations, you must first remove any existing supplier-locations for the above supplier/material/nutrient. ";
							MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
							return;
						}
					}
				}

				//7/28/20 - user wants to add supplier-location, check for "ALL"
				else
				{
					guarantee = AIMManager.GetSupplierLocationGuarantee(true, (Int32)lueAimSupplier.EditValue, 0, (Int32)lueAIMMaterialGuarantee.EditValue, (Int32)lueAIMNutrientGuarantee.EditValue);
					if (guarantee != null)
					{
						if (guarantee.Count > 0)
						{
							msg = "An 'ALL' supplier-location record exists on the table for: ";
							msg += Environment.NewLine;
							msg += Environment.NewLine + "     Supplier:  " + guarantee[0].SourceName;
							msg += Environment.NewLine;
							msg += Environment.NewLine + "     Material:  " + guarantee[0].AIMMaterialName;
							msg += Environment.NewLine + "     Nutrient:  " + guarantee[0].AIMNutrientName;
							msg += Environment.NewLine;
							msg += Environment.NewLine + "Before adding a specific supplier-location, you must first remove the 'ALL' record for the above supplier/material/nutrient. ";
							MessageBox.Show(msg, "Warning...", MessageBoxButtons.OK);
							return;
						}
					}
				}
				//-------------------------------------------------------------------------------------------------------                

				if (txtMinGuarantee.Text.Trim().Length == 0 && txtMaxGuarantee.Text.Trim().Length == 0 && txtTarget.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter Minimum, Maximum or Target.", "Warning...", MessageBoxButtons.OK);
					if (txtMinGuarantee.Text.Trim().Length == 0)
					{
						txtMinGuarantee.Focus();
					}
					else if (txtMaxGuarantee.Text.Trim().Length == 0)
					{
						txtMaxGuarantee.Focus();
					}
					else
					{
						txtTarget.Focus();
					}

					return;
				}

				supplierLocationGuarantee.AIMSupplierLocationGuaranteeID = 0;
				supplierLocationGuarantee.AIMSupplierID = (Int32)lueAimSupplier.EditValue;
				supplierLocationGuarantee.AIMSupplierLocationID = (Int32)lueAimSupplierLocation.EditValue;
				supplierLocationGuarantee.AIMNutrientID = (Int32)(lueAIMNutrientGuarantee.EditValue);
				supplierLocationGuarantee.AIMMaterialID = (Int32)(lueAIMMaterialGuarantee.EditValue);

				if (txtMinGuarantee.Text.Length > 0)
				{
					supplierLocationGuarantee.Min = double.Parse(txtMinGuarantee.Text);
				}
				if (txtMaxGuarantee.Text.Length > 0)
				{
					supplierLocationGuarantee.Max = double.Parse(txtMaxGuarantee.Text);
				}
				if (txtTarget.Text.Length > 0)
				{
					supplierLocationGuarantee.Target = double.Parse(txtTarget.Text);
				}

				//7/28/20
				supplierLocationGuarantee.IsActive = true;

				//11/21/22 
				if (txtMoisture.Text.Length > 0)
				{
					supplierLocationGuarantee.Moisture = double.Parse(txtMoisture.Text);
				}


				supplierLocationGuarantee.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveSupplierLocationGuarantee(ref supplierLocationGuarantee);

				lueAIMNutrientGuarantee.EditValue = 0;
				txtMinGuarantee.Text = String.Empty;
				txtMaxGuarantee.Text = String.Empty;
				txtTarget.Text = String.Empty;
				DataBindGuarantee();
				lueAimSupplier.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewGuarantee_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdGuarantees, "AIM Supplier Location Guarantees");
				CFCAIMCore.Global.previewGridResults(this.grdGuarantees, "AIM Supplier Location Guarantees");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteGuarantee_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierLocationGuaranteeInfo supplierlocation = new AIMSupplierLocationGuaranteeInfo();
				supplierlocation = (AIMSupplierLocationGuaranteeInfo)grdGuaranteeView.GetFocusedRow();

				//7/2/20 - if trying to re-activate a guarantee record, make sure AIMMaterial is active on the AIMMaterial table 
				// and the AIMNutrient is active on the AIMNutrient table before reactivating.
				if (rbActiveRecords.Checked == false)
				{
					AIMMaterialInfo material = new AIMMaterialInfo();
					material = AIMManager.GetAIMMaterial(supplierlocation.AIMMaterialID, false);
					if (material != null)
					{
						if (material.IsActive == false)
						{
							MessageBox.Show("This supplier guarantee cannot be activated at this time because the material is inactive on the Material table.", "Warning...", MessageBoxButtons.OK);
							return;
						}
					}
					AIMNutrientInfo nutrient = new AIMNutrientInfo();
					nutrient = AIMManager.GetAIMNutrientIsActive(supplierlocation.AIMNutrientID, false);
					if (nutrient != null)
					{
						if (nutrient.IsActive == false)
						{
							MessageBox.Show("This supplier guarantee cannot be activated at this time because the nutrient is inactive on the Nutrient table.", "Warning...", MessageBoxButtons.OK);
							return;
						}
					}

					//7/28/20
					List<AIMSupplierLocationGuaranteeInfo> guarantee = new List<AIMSupplierLocationGuaranteeInfo>();

					//If trying to reactive 'ALL' record, make sure there are no active supplier-supplierlocation-material-nutrient records
					if (supplierlocation.AIMSupplierLocationID == 0)
					{
						guarantee = AIMManager.GetSupplierLocationGuarantee(true, supplierlocation.AIMSupplierID, supplierlocation.AIMMaterialID, supplierlocation.AIMNutrientID);
						if (guarantee != null)
						{
							if (guarantee.Count > 0)
							{
								MessageBox.Show("The 'ALL' record for this supplier guarantee cannot be activated at this time because active supplier guarantees for this supplier/material/nutrient exist. You must first remove those active records before reactivating the ALL record.", "Warning...", MessageBoxButtons.OK);
								return;
							}
						}

					}
					//If trying to reactive supplier-supplierlocation-material-nutrient record, make sure there is no active 'ALL' record.
					if (supplierlocation.AIMSupplierLocationID != 0)
					{
						guarantee = AIMManager.GetSupplierLocationGuarantee(true, supplierlocation.AIMSupplierID, 0, supplierlocation.AIMMaterialID, supplierlocation.AIMNutrientID);
						if (guarantee != null)
						{
							if (guarantee.Count > 0)
							{
								MessageBox.Show("This supplier guarantee cannot be activated at this time because an active 'ALL' record exists.", "Warning...", MessageBoxButtons.OK);
								return;
							}
						}
					}

				}


				supplierlocation.IsActive = !supplierlocation.IsActive;
				supplierlocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveSupplierLocationGuarantee(ref supplierlocation);
				DataBindGuarantee();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdGuaranteeView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			AIMSupplierLocationGuaranteeInfo supplierlocation = new AIMSupplierLocationGuaranteeInfo();
			supplierlocation = (AIMSupplierLocationGuaranteeInfo)grdGuaranteeView.GetFocusedRow();
			supplierlocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveSupplierLocationGuarantee(ref supplierlocation);
			DataBindGuarantee();
		}
		private void grdGuaranteeView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void lueAimSupplier_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAimSupplier, ref lueAimSupplierName);       //task 1133 FG 7/21/2026
				DataBindSupplierLocationLueGuarantee();

				//12/3/20 - added to filter for selected Supplier
				DataBindGuarantee();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		//task 1133 FG 7/21/2026 - keep the Supplier Name dropdown in sync with the Supplier Code dropdown
		private void lueAimSupplierName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAimSupplierName, ref lueAimSupplier);
		}
		private void lueAimSupplierName_Enter(object sender, EventArgs e)
		{
			lueAimSupplierName.SelectAll();
		}
		private void lueAimSupplierName_Click(object sender, EventArgs e)
		{
			lueAimSupplierName.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Supplier Location Name dropdown in sync with the Location Code dropdown
		private void lueAimSupplierLocationName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAimSupplierLocationName, ref lueAimSupplierLocation);
		}
		private void lueAimSupplierLocationName_Enter(object sender, EventArgs e)
		{
			lueAimSupplierLocationName.SelectAll();
		}
		private void lueAimSupplierLocationName_Click(object sender, EventArgs e)
		{
			lueAimSupplierLocationName.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Material Name dropdown in sync with the Material Code dropdown
		private void lueAIMMaterialGuaranteeName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMMaterialGuaranteeName, ref lueAIMMaterialGuarantee);
		}
		private void lueAIMMaterialGuaranteeName_Enter(object sender, EventArgs e)
		{
			lueAIMMaterialGuaranteeName.SelectAll();
		}
		private void lueAIMMaterialGuaranteeName_Click(object sender, EventArgs e)
		{
			lueAIMMaterialGuaranteeName.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Nutrient Name dropdown in sync with the Nutrient Code dropdown
		private void lueAIMNutrientGuaranteeName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMNutrientGuaranteeName, ref lueAIMNutrientGuarantee);
		}
		private void lueAIMNutrientGuaranteeName_Enter(object sender, EventArgs e)
		{
			lueAIMNutrientGuaranteeName.SelectAll();
		}
		private void lueAIMNutrientGuaranteeName_Click(object sender, EventArgs e)
		{
			lueAIMNutrientGuaranteeName.SelectAll();
		}
		private void lueAimSupplier_Enter(object sender, EventArgs e)
		{
			lueAimSupplier.SelectAll();
		}
		private void lueAimSupplier_Click(object sender, EventArgs e)
		{
			lueAimSupplier.SelectAll();
		}
		private void lueAimSupplierLocation_Enter(object sender, EventArgs e)
		{
			lueAimSupplierLocation.SelectAll();
		}
		private void lueAimSupplierLocation_Click(object sender, EventArgs e)
		{
			lueAimSupplierLocation.SelectAll();
		}
		private void lueAIMMaterialGuarantee_Click(object sender, EventArgs e)
		{
			lueAIMMaterialGuarantee.SelectAll();
		}
		private void lueAIMMaterialGuarantee_Enter(object sender, EventArgs e)
		{
			lueAIMMaterialGuarantee.SelectAll();
		}
		private void lueAIMNutrientGuarantee_Enter(object sender, EventArgs e)
		{
			lueAIMNutrientGuarantee.SelectAll();
		}
		private void lueAIMNutrientGuarantee_Click(object sender, EventArgs e)
		{
			lueAIMNutrientGuarantee.SelectAll();
		}
		private void repbtnFinalDeleteSupplierGuarantee_Click(object sender, EventArgs e)
		{
			String msg = "";
			ErrorInfo result = new ErrorInfo();
			AIMSupplierLocationGuaranteeInfo guarantee = new AIMSupplierLocationGuaranteeInfo();
			guarantee = (AIMSupplierLocationGuaranteeInfo)grdGuaranteeView.GetFocusedRow();

			DialogResult dr = new DialogResult();
			msg = "Delete Supplier Location Guarantee for: ";
			msg += Environment.NewLine;
			msg += Environment.NewLine + "     Supplier:  " + guarantee.SourceName;

			if (guarantee.AIMSupplierLocationID == 0)
			{
				msg += Environment.NewLine + "     Location:  ALL";
			}
			else
			{
				msg += Environment.NewLine + "     Location:  " + guarantee.SourceLocationName;
			}

			msg += Environment.NewLine;
			msg += Environment.NewLine + "     Material:  " + guarantee.AIMMaterialName;
			msg += Environment.NewLine + "     Nutrient:  " + guarantee.AIMNutrientName;
			dr = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dr != DialogResult.Yes)
			{
				return;
			}
			result = AIMManager.DeleteAIMSupplierLocationGuarantee(guarantee.AIMSupplierLocationGuaranteeID);
			if (result.ErrorNumber != 0)
			{
				return;
			}
			DataBindGuarantee();

		}       //7/28/20
		private void lueAIMMaterialGuarantee_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMMaterialGuarantee, ref lueAIMMaterialGuaranteeName);     //task 1133 FG 7/21/2026

			//12/3/20 - added to filter for selected Material
			DataBindGuarantee();
		}
		private void lueAIMNutrientGuarantee_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMNutrientGuarantee, ref lueAIMNutrientGuaranteeName);     //task 1133 FG 7/21/2026

			//12/3/20 - added to filter for selected Nutrient
			DataBindGuarantee();
		}
		private void lueAimSupplierLocation_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAimSupplierLocation, ref lueAimSupplierLocationName);       //task 1133 FG 7/21/2026

			//12/3/20 - added to filter for selected SupplierLocation
			DataBindGuarantee();
		}
		#endregion

		#region Units ---------------------------------------------------------------------------
		private void DataBindUnits()
		{
			List<UnitTypeInfo> units = new List<UnitTypeInfo>();

			units = AIMManager.GetUnitType(rbActiveRecords.Checked);
			Int32 currentRowNum = 0;
			currentRowNum = grdUnitTypeView.FocusedRowHandle;
			grdUnitType.DataSource = units;
			grdUnitTypeView.FocusedRowHandle = currentRowNum;
		}
		private void btnAddUnits_Click(object sender, EventArgs e)
		{
			try
			{
				UnitTypeInfo units = new UnitTypeInfo();

				units.UnitTypeID = 0;
				units.UnitTypeCode = txtUnitTypeCode.Text;
				units.UnitTypeDescription = txtUnitTypeDescription.Text;
				units.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				if (txtUnitTypeDescription.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Unit Type Description.", "Warning...", MessageBoxButtons.OK);
					txtUnitTypeDescription.Focus();
					return;
				}


				AIMManager.SaveUnitType(ref units, true);
				txtUnitTypeCode.Text = String.Empty;
				txtUnitTypeDescription.Text = String.Empty;
				DataBindUnits();
				txtUnitTypeDescription.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteUnits_Click(object sender, EventArgs e)
		{
			try
			{
				UnitTypeInfo unit = new UnitTypeInfo();
				unit = (UnitTypeInfo)grdUnitTypeView.GetFocusedRow();
				unit.IsActive = !unit.IsActive;
				unit.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveUnitType(ref unit, true);
				DataBindUnits();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewUnits_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdUnitType, "Unit Type");
				CFCAIMCore.Global.previewGridResults(grdUnitType, "Unit Type");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdUnitTypeView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		#endregion

		#region Warning Limits ------------------------------------------------------------------
		private void DataBindlueWarningLimit()
		{
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			aimMaterial = AIMManager.GetAIMMaterial(true);
			aimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--"
				}
			);

			lueAIMMaterial.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMMaterial, "AIMMaterialID", "AIMMaterialCode");
			lueAIMMaterial.Properties.Columns["AIMMaterialCode"].Visible = true;
			lueAIMMaterial.Properties.Columns["AIMMaterialName"].Visible = true;

			//task 1133 FG 7/21/2026 - bind the Material Name dropdown to the same list as the Material Code dropdown
			lueAIMMaterialName.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMMaterialName, "AIMMaterialID", "AIMMaterialName");
			lueAIMMaterialName.Properties.Columns["AIMMaterialCode"].Visible = true;
			lueAIMMaterialName.Properties.Columns["AIMMaterialName"].Visible = true;

			List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
			aimNutrient = AIMManager.GetAIMNutrient(true);
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
			lueAIMNutrient.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMNutrient, "AIMNutrientID", "AIMNutrientCode");
			lueAIMNutrient.Properties.Columns["AIMNutrientCode"].Visible = true;
			lueAIMNutrient.Properties.Columns["AIMNutrientName"].Visible = true;

			//task 1133 FG 7/21/2026 - bind the Nutrient Name dropdown to the same list as the Nutrient Code dropdown
			lueAIMNutrientName.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMNutrientName, "AIMNutrientID", "AIMNutrientName");
			lueAIMNutrientName.Properties.Columns["AIMNutrientCode"].Visible = true;
			lueAIMNutrientName.Properties.Columns["AIMNutrientName"].Visible = true;


			replueMaterialCodeWarning.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterialCodeWarning, "AIMMaterialID", "AIMMaterialCode");
			replueMaterialCodeWarning.Columns["AIMMaterialCode"].Visible = true;
			replueMaterialCodeWarning.Columns["AIMMaterialName"].Visible = true;

			replueMaterialNameWarning.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterialNameWarning, "AIMMaterialID", "AIMMaterialName");
			replueMaterialNameWarning.Columns["AIMMaterialCode"].Visible = true;
			replueMaterialNameWarning.Columns["AIMMaterialName"].Visible = true;

			replueNutrientCodeWarning.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueNutrientCodeWarning, "AIMNutrientID", "AIMNutrientCode");
			replueNutrientCodeWarning.Columns["AIMNutrientCode"].Visible = true;
			replueNutrientCodeWarning.Columns["AIMNutrientName"].Visible = true;

			replueNutrientNameWarning.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueNutrientNameWarning, "AIMNutrientID", "AIMNutrientName");
			replueNutrientNameWarning.Columns["AIMNutrientCode"].Visible = true;
			replueNutrientNameWarning.Columns["AIMNutrientName"].Visible = true;

		}
		private void DataBindWarningLimit()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdWarningLimitsView.FocusedRowHandle.ToString(), out rowNum);
			grdWarningLimits.DataSource = AIMManager.GetAllWarningLimits();
			grdWarningLimitsView.FocusedRowHandle = rowNum;
		}
		private void btnAddWarningLimit_Click(object sender, EventArgs e)
		{
			try
			{
				WarningLimitsInfo warningLimits = new WarningLimitsInfo();



				if (lueAIMMaterial.Text.ToString() == "--Select--" || lueAIMMaterial.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select Material.", "Warning...", MessageBoxButtons.OK);
					lueAIMMaterial.ShowPopup();
					return;
				}
				if (lueAIMNutrient.Text.ToString() == "--Select--" || lueAIMNutrient.Text.ToString().Length == 0)
				{
					MessageBox.Show("Please select Nutrient.", "Warning...", MessageBoxButtons.OK);
					lueAIMNutrient.ShowPopup();
					return;
				}
				if (txtMin.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Minimum.", "Warning...", MessageBoxButtons.OK);
					txtMin.Focus();
					return;
				}
				if (txtMax.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Maximum.", "Warning...", MessageBoxButtons.OK);
					txtMax.Focus();
					return;
				}

				warningLimits.AIMMaterialID = (Int32)lueAIMMaterial.EditValue;
				warningLimits.AIMNutrientID = (Int32)lueAIMNutrient.EditValue;
				warningLimits.Min = Decimal.Parse(txtMin.Text);
				warningLimits.Max = Decimal.Parse(txtMax.Text);
				warningLimits.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveWarningLimits(ref warningLimits);
				if (error.ErrorNumber > 0)
				{
					return;
				}

				lueAIMMaterial.EditValue = 0;
				lueAIMNutrient.EditValue = 0;
				txtMin.Text = String.Empty;
				txtMax.Text = String.Empty;
				DataBindWarningLimit();
				lueAIMMaterial.Focus();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void repbtnDeleteWarning_Click(object sender, EventArgs e)
		{
			try
			{
				WarningLimitsInfo warningLimits = new WarningLimitsInfo();
				warningLimits = (WarningLimitsInfo)grdWarningLimitsView.GetFocusedRow();
				warningLimits.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteWarningLimits(warningLimits.WarningLimitsID);
				DataBindWarningLimit();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewWarningLimit_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdWarningLimits, "Warning Limits");
				CFCAIMCore.Global.previewGridResults(grdWarningLimits, "Warning Limits");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdWarningLimitsView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				WarningLimitsInfo warning = new WarningLimitsInfo();
				warning = (WarningLimitsInfo)grdWarningLimitsView.GetFocusedRow();
				warning.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveWarningLimits(ref warning);
				DataBindWarningLimit();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdWarningLimitsView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			//5/29/19
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void lueAIMMaterial_Click(object sender, EventArgs e)
		{
			lueAIMMaterial.SelectAll();
		}
		private void lueAIMMaterial_Enter(object sender, EventArgs e)
		{
			lueAIMMaterial.SelectAll();
		}
		private void lueAIMNutrient_Enter(object sender, EventArgs e)
		{
			lueAIMNutrient.SelectAll();
		}
		private void lueAIMNutrient_Click(object sender, EventArgs e)
		{
			lueAIMNutrient.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Material Code and Material Name dropdowns in sync
		private void lueAIMMaterial_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMMaterial, ref lueAIMMaterialName);
		}
		private void lueAIMMaterialName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMMaterialName, ref lueAIMMaterial);
		}
		private void lueAIMMaterialName_Click(object sender, EventArgs e)
		{
			lueAIMMaterialName.SelectAll();
		}
		private void lueAIMMaterialName_Enter(object sender, EventArgs e)
		{
			lueAIMMaterialName.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Nutrient Code and Nutrient Name dropdowns in sync
		private void lueAIMNutrient_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMNutrient, ref lueAIMNutrientName);
		}
		private void lueAIMNutrientName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMNutrientName, ref lueAIMNutrient);
		}
		private void lueAIMNutrientName_Click(object sender, EventArgs e)
		{
			lueAIMNutrientName.SelectAll();
		}
		private void lueAIMNutrientName_Enter(object sender, EventArgs e)
		{
			lueAIMNutrientName.SelectAll();
		}
		#endregion

		//9/23/22		
		#region Sample Ingredients Groups ------------------------------------------------------------
		private void DataBindSpecificIngredientsByGroup()
		{
			List<SpecificIngredientsInfo> specific = new List<SpecificIngredientsInfo>();
			specific = AIMManager.GetSpecificIngredients_GroupID((Int32)lueGroupCode.EditValue);
			Int32 currentRowNum = 0;
			if (grdSpecificView != null)
			{
				Int32.TryParse(grdSpecificView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdSpecific.DataSource = specific;
			grdSpecificView.FocusedRowHandle = currentRowNum;
		}
		private void DataBindSpecificIngredients()
		{
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

			//10/13/23
			//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient);
			aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)EnumOrderBy.Name);

			aimMaterial.Insert
				(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--"
				}
				);

			lueAIMCode.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
			lueAIMName.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");

			//task 1133 FG 7/21/2026 - inside the AIM Ingredient Name dropdown, show the Name column first, then the Code column
			lueAIMName.Properties.Columns.Clear();
			lueAIMName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialName", "Material Name"));
			lueAIMName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialCode", "Material Code"));


			List<SpecificIngredientsInfo> specific = new List<SpecificIngredientsInfo>();
			specific = AIMManager.GetSpecificIngredients_GroupID((Int32)lueGroupCode.EditValue);

			Int32 currentRowNum = 0;
			if (grdSpecificView != null)
			{
				Int32.TryParse(grdSpecificView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdSpecific.DataSource = specific;
			grdSpecificView.FocusedRowHandle = currentRowNum;
		}                                                   //10/19/22
		private void DataBindSpecificIngredientsGroup()
		{
			List<SpecificIngredientsGroupInfo> group = new List<SpecificIngredientsGroupInfo>();
			group = AIMManager.GetSpecificIngredientsGroup(true);
			group.Insert
				(
				0,
				new SpecificIngredientsGroupInfo
				{
					SpecificIngredientsGroupID = 0,
					SpecificIngredientsGroupCode = "--Select--"

				}
				);
			lueGroupCode.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueGroupCode, "SpecificIngredientsGroupID", "SpecificIngredientsGroupCode");

			//11/23/22
			////5/18/23 - rearranged tabs so ingredient and product group tabs are together

			if (_tabGroupIngredients == false)
			{
				//10/12/23 - selecting first record
				//if (group.Count == 2)
				if (group.Count > 1)
				{
					lueGroupCode.EditValue = group[1].SpecificIngredientsGroupID;
				}
				else
				{
					lueGroupCode.EditValue = 0;
				}
			}
		}
		private void repDeleteSpecificIngredient_Click(object sender, EventArgs e)
		{
			try
			{
				SpecificIngredientsInfo specific = new SpecificIngredientsInfo();
				specific = (SpecificIngredientsInfo)grdSpecificView.GetFocusedRow();
				specific.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteSpecificIngredients(specific.SpecificIngredientsID);

				//filter grid for group code		
				DataBindSpecificIngredientsByGroup();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}                   //10/19/22
		private void btnPreviewSpecific_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdSpecific, "Ingredients to display in Sample screen dropdown");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btbAddSpecific_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueGroupCode.EditValue == 0)
				{
					MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueGroupCode.ShowPopup();
					return;
				}

				if ((Int32)lueAIMCode.EditValue == 0)
				{
					MessageBox.Show("Please select an Ingredient. ", "Warning...", MessageBoxButtons.OK);
					lueAIMCode.ShowPopup();
					return;
				}

				SpecificIngredientsInfo specific = new SpecificIngredientsInfo();
				specific.SpecificIngredientsID = 0;

				//10/19/22
				specific.SpecificIngredientsGroupID = (Int32)lueGroupCode.EditValue;

				specific.AIMMaterialID = (Int32)lueAIMCode.EditValue;
				specific.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				specific.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveSpecificIngredient(ref specific);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindSpecificIngredientsByGroup();

				lueAIMCode.EditValue = 0;
				lueAIMName.EditValue = 0;
				lueAIMName.ShowPopup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}                               //9/23/22
										//if material is inactive, make row readonly and change background color
		private void grdSpecificView_ShownEditor(object sender, EventArgs e)
		{
			List<SpecificIngredientsInfo> list = new List<SpecificIngredientsInfo>();
			GridView view = (GridView)sender;



		}
		private void grdSpecificView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}
		private void btnAddNewGroupCode_Click(object sender, EventArgs e)                               //10/19/22
		{
			if (txtGroupCode.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtGroupCode.Focus();
				return;
			}

			//save group code
			SpecificIngredientsGroupInfo group = new SpecificIngredientsGroupInfo();
			group.SpecificIngredientsGroupID = 0;
			group.SpecificIngredientsGroupCode = txtGroupCode.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveSpecificIngredientGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtGroupCode.Text = String.Empty;
				return;
			}

			DataBindSpecificIngredientsGroup();
			lueGroupCode.EditValue = error.RecordID;
			txtGroupCode.Text = String.Empty;

			gpAddGroupCode.Visible = false;
			gbGroupCode.Visible = true;

		}
		private void lueGroupCode_EditValueChanged(object sender, EventArgs e)
		{
			//filter grid for group code		
			DataBindSpecificIngredientsByGroup();

			//5/18/23
			if ((Int32)lueGroupCode.EditValue != 0)
			{
				grpSaveGroupAs.Location = new Point(356, 9);
				grpSaveGroupAs.Visible = true;
			}
			else
			{
				grpSaveGroupAs.Visible = false;
			}


		}
		private void rbDeletedRecords_CheckedChanged(object sender, EventArgs e)
		{

		}
		private void btnDeleteGroup_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueGroupCode.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueGroupCode.ShowPopup();
				return;
			}
			name = lueGroupCode.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueGroupCode.EditValue;

				//Delete from SpecificIngredients table
				AIMManager.DeleteSpecificIngredients_GroupID(groupID);

				//Delete from SpecificIngredientsGroup table
				AIMManager.DeleteSpecificIngredientsGroup(groupID);

				//11/28/22
				//delete from cfcuser (set GroupID = 0)
				AIMManager.UpdateCFCUsersSpecificIngredientsGroup(groupID, 0);

				DataBindSpecificIngredientsGroup();
				DataBindSpecificIngredients();

			}
		}                               //10/19/22
		private void btnAddNewGroup_Click(object sender, EventArgs e)
		{
			//11/28/22
			gpAddGroupCode.Location = new Point(7, 9);
			gbGroupCode.Visible = false;

			gpAddGroupCode.Visible = true;
			lueGroupCode.EditValue = 0;
			txtGroupCode.Focus();
		}                               //10/19/22
		private void btnCancelSaveGroupAs_Click(object sender, EventArgs e)                             //5/18/23
		{
			txtSaveGroupAs.Text = String.Empty;
		}
		private void btnSaveGroupAs_Click(object sender, EventArgs e)                                   //5/18/23
		{
			if (txtSaveGroupAs.Text == String.Empty)
			{
				MessageBox.Show("Please enter a Group Code.", "Warning...", MessageBoxButtons.OK);
				txtSaveGroupAs.Focus();
				return;
			}

			//Save the group
			SpecificIngredientsGroupInfo group = new SpecificIngredientsGroupInfo();
			group.SpecificIngredientsGroupID = 0;
			group.SpecificIngredientsGroupCode = txtSaveGroupAs.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveSpecificIngredientGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtSaveGroupAs.Text = String.Empty;
				return;
			}

			//Save the ingredients			
			AIMManager.SpecificIngredientsSaveAs((Int32)lueGroupCode.EditValue, error.RecordID);

			DataBindSpecificIngredientsGroup();
			lueGroupCode.EditValue = error.RecordID;
			txtSaveGroupAs.Text = String.Empty;

			gpAddGroupCode.Visible = false;
			gbGroupCode.Visible = true;


			//filter grid for group code		
			DataBindSpecificIngredientsByGroup();

			lueAIMCode.EditValue = 0;
			lueAIMName.EditValue = 0;


		}
		private void lueAIMCode_Enter(object sender, EventArgs e)
		{
			lueAIMCode.SelectAll();
		}
		private void lueAIMCode_Click(object sender, EventArgs e)
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
		private void grdSpecificView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}
		#endregion

		//5/18/23
		#region Sample Product Groups ----------------------------------------------------------------
		private void DataBindSpecificProductsByGroup()
		{
			List<SpecificProductsInfo> specific = new List<SpecificProductsInfo>();
			specific = AIMManager.GetSpecificProducts_GroupID((Int32)lueProductGroupCode.EditValue);
			Int32 currentRowNum = 0;
			if (grdSpecificProductsView != null)
			{
				Int32.TryParse(grdSpecificProductsView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdSpecificProducts.DataSource = specific;
			grdSpecificProductsView.FocusedRowHandle = currentRowNum;
		}
		private void DataBindSpecificProducts()
		{
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

			//10/13/23
			//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Product);
			aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Product, (Int32)EnumOrderBy.Name);

			aimMaterial.Insert
				(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--"
				}
				);

			List<AIMOwnerLocationInfo> aimOwnerLoc = new List<AIMOwnerLocationInfo>();
			aimOwnerLoc = AIMManager.GetAIMOwnerLocation(true);
			aimOwnerLoc.Insert
				(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					OwnerCode = "",
					LocationName = "--Select--"
				}
				);

			lueOwnerLoc_SpecificProducts.Properties.DataSource = aimOwnerLoc;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLoc_SpecificProducts, "AIMOwnerLocationID", "LocationName", "OwnerCode");
			lueOwnerLoc_SpecificProducts.Properties.Columns["OwnerCode"].Caption = "Owner";
			lueOwnerLoc_SpecificProducts.Properties.Columns["LocationName"].Caption = "Owner Location Name";

			//task 1133 FG 7/21/2026 - removed the AIMOwnerLocationName column; show only Code and Name columns inside the dropdowns
			lueAIMProductCode.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMProductCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
			lueAIMProductCode.Properties.Columns.Clear();
			lueAIMProductCode.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialCode", "Product Code"));
			lueAIMProductCode.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialName", "Product Name"));

			lueAIMProductName.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMProductName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
			lueAIMProductName.Properties.Columns.Clear();
			lueAIMProductName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialName", "Product Name"));
			lueAIMProductName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialCode", "Product Code"));

			List<SpecificProductsInfo> specific = new List<SpecificProductsInfo>();
			specific = AIMManager.GetSpecificProducts_GroupID((Int32)lueProductGroupCode.EditValue);

			Int32 currentRowNum = 0;
			if (grdSpecificProductsView != null)
			{
				Int32.TryParse(grdSpecificProductsView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdSpecificProducts.DataSource = specific;
			grdSpecificProductsView.FocusedRowHandle = currentRowNum;
		}
		private void DataBindSpecificProductsGroup()
		{
			List<SpecificProductsGroupInfo> group = new List<SpecificProductsGroupInfo>();
			group = AIMManager.GetSpecificProductsGroup(true);
			group.Insert
				(
				0,
				new SpecificProductsGroupInfo
				{
					SpecificProductsGroupID = 0,
					SpecificProductsGroupCode = "--Select--"

				}
				);
			lueProductGroupCode.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueProductGroupCode, "SpecificProductsGroupID", "SpecificProductsGroupCode");
			if (_tabGroupProducts == false)
			{
				//10/12/23 - selecting first record
				//if (group.Count == 2)
				if (group.Count > 1)
				{
					lueProductGroupCode.EditValue = group[1].SpecificProductsGroupID;
				}
				else
				{
					lueProductGroupCode.EditValue = 0;
				}
			}
		}

        // Task1169 DB260408 
        // Somehow this code existed for the delete of a record from the grid but the object tied to the column in grdSpecificProducts designer 
        // was tied to repbtnDeleteSpecificProduct NOT repDeleteSpecificProduct
        //private void repDeleteSpecificProduct_Click(object sender, EventArgs e)
        //{
        //	try
        //	{
        //		SpecificProductsGroupInfo specific = new SpecificProductsGroupInfo();
        //		specific = (SpecificProductsGroupInfo)grdSpecificProductsView.GetFocusedRow();
        //		specific.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
        //		AIMManager.DeleteSpecificProducts(specific.SpecificProductsGroupID);

        //		//filter grid for group code		
        //		DataBindSpecificProductsByGroup();
        //	}
        //	catch (Exception ex)
        //	{
        //		MessageBox.Show(ex.Message);
        //	}
        //}
        private void btnSaveProductGroupAs_Click(object sender, EventArgs e)
		{
			if (txtSaveProductGroupAs.Text == String.Empty)
			{
				MessageBox.Show("Please enter a Product Group Code.", "Warning...", MessageBoxButtons.OK);
				txtSaveProductGroupAs.Focus();
				return;
			}

			//Save the group
			SpecificProductsGroupInfo group = new SpecificProductsGroupInfo();
			group.SpecificProductsGroupID = 0;
			group.SpecificProductsGroupCode = txtSaveProductGroupAs.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveSpecificProductGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtSaveProductGroupAs.Text = String.Empty;
				return;
			}

			//Save the products			
			AIMManager.SpecificProductsSaveAs((Int32)lueProductGroupCode.EditValue, error.RecordID);

			DataBindSpecificProductsGroup();
			lueProductGroupCode.EditValue = error.RecordID;
			txtSaveProductGroupAs.Text = String.Empty;

			gpAddProductGroupCode.Visible = false;
			gbProductGroupCode.Visible = true;


			//filter grid for group code		
			DataBindSpecificProductsByGroup();

			lueAIMProductCode.EditValue = 0;
			lueAIMProductName.EditValue = 0;

		}                           //5/18/23
		private void btnPreviewSpecificProducts_Click(object sender, EventArgs e)                       //5/18/23
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdSpecificProducts, "Products to display in Sample screen dropdown");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddSpecificProduct_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueProductGroupCode.EditValue == 0)
				{
					MessageBox.Show("Please select a Product Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueProductGroupCode.ShowPopup();
					return;
				}

				if ((Int32)lueAIMProductCode.EditValue == 0)
				{
					MessageBox.Show("Please select a Product. ", "Warning...", MessageBoxButtons.OK);
					lueAIMProductCode.ShowPopup();
					return;
				}

				SpecificProductsInfo specific = new SpecificProductsInfo();
				specific.SpecificProductsID = 0;
				specific.SpecificProductsGroupID = (Int32)lueProductGroupCode.EditValue;
				specific.AIMMaterialID = (Int32)lueAIMProductCode.EditValue;
				specific.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				specific.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveSpecificProduct(ref specific);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindSpecificProductsByGroup();

				lueAIMProductCode.EditValue = 0;
				lueAIMProductName.EditValue = 0;
				lueAIMProductName.ShowPopup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}                           //5/18/23
		private void grdSpecificProductsView_ShownEditor(object sender, EventArgs e)
		{
			List<SpecificProductsInfo> list = new List<SpecificProductsInfo>();
			GridView view = (GridView)sender;
		}                   //5/18/23
		private void grdSpecificProductsView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}           //5/18/23
		private void btnAddNewProductGroupCode_Click(object sender, EventArgs e)
		{
			if (txtProductGroupCode.Text == "")
			{
				MessageBox.Show("Please enter a Product Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtProductGroupCode.Focus();
				return;
			}

			//save group code
			SpecificProductsGroupInfo group = new SpecificProductsGroupInfo();
			group.SpecificProductsGroupID = 0;
			group.SpecificProductsGroupCode = txtProductGroupCode.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveSpecificProductGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtProductGroupCode.Text = String.Empty;
				return;
			}

			DataBindSpecificProductsGroup();
			lueProductGroupCode.EditValue = error.RecordID;
			txtProductGroupCode.Text = String.Empty;

			gpAddProductGroupCode.Visible = false;
			gbProductGroupCode.Visible = true;


		}
		private void lueProductGroupCode_EditValueChanged(object sender, EventArgs e)
		{
			//filter grid for group code		
			DataBindSpecificProductsByGroup();

			//5/18/23
			if ((Int32)lueProductGroupCode.EditValue != 0)
			{
				grpSaveProductGroupAs.Location = new System.Drawing.Point(356, 9);
				grpSaveProductGroupAs.Visible = true;
			}
			else
			{
				grpSaveProductGroupAs.Visible = false;
			}

		}               //5/18/23
		private void lueAIMProductCode_Enter(object sender, EventArgs e)
		{
			lueAIMProductCode.SelectAll();
		}
		private void lueAIMProductCode_Click(object sender, EventArgs e)
		{
			lueAIMProductCode.SelectAll();
		}
		private void lueAIMProductName_Enter(object sender, EventArgs e)
		{
			lueAIMProductName.SelectAll();
		}
		private void lueAIMProductName_Click(object sender, EventArgs e)
		{
			lueAIMProductName.SelectAll();
		}
		private void lueAIMProductCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMProductCode, ref lueAIMProductName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMProductName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMProductName, ref lueAIMProductCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddNewProductGroup_Click(object sender, EventArgs e)
		{

			gpAddProductGroupCode.Location = new Point(7, 9);
			gbProductGroupCode.Visible = false;

			gpAddProductGroupCode.Visible = true;
			lueProductGroupCode.EditValue = 0;
			txtProductGroupCode.Focus();
		}
		private void btnDeleteProductGroup_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueProductGroupCode.EditValue == 0)
			{
				MessageBox.Show("Please select a Product Group to delete.");
				lueProductGroupCode.ShowPopup();
				return;
			}
			name = lueProductGroupCode.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Product Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueProductGroupCode.EditValue;

				//Delete from SpecificProducts table
				AIMManager.DeleteSpecificProducts_GroupID(groupID);

				//Delete from SpecificIngredientsGroup table
				AIMManager.DeleteSpecificProductsGroup(groupID);

				//delete from cfcuser (set GroupID = 0)
				AIMManager.UpdateCFCUsersSpecificProductsGroup(groupID, 0);

				DataBindSpecificProductsGroup();
				DataBindSpecificProducts();

			}
		}
		private void cmdCancelAddProduct_Click(object sender, EventArgs e)
		{
			gpAddProductGroupCode.Visible = false;
			gbProductGroupCode.Visible = true;
		}
		private void btnCancelSaveProductGroupAs_Click(object sender, EventArgs e)
		{
			txtSaveProductGroupAs.Text = String.Empty;
		}
		private void repDeleteSpecificProductInGrid_Click(object sender, EventArgs e)
		{
			try
			{
				SpecificProductsInfo specific = new SpecificProductsInfo();
				specific = (SpecificProductsInfo)grdSpecificProductsView.GetFocusedRow();
				specific.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteSpecificProducts(specific.SpecificProductsID);

				//filter grid for group code		
				DataBindSpecificProductsByGroup();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerLoc_SpecificProducts_EditValueChanged(object sender, EventArgs e)
		{
			if ((Int32)lueOwnerLoc_SpecificProducts.EditValue != 0)
			{
				List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
				aimMaterial = AIMManager.GetAIMMaterial(true, (Int32)lueOwnerLoc_SpecificProducts.EditValue, (Int32)MaterialTypeEnum.Product);
				aimMaterial.Insert
					(
					0,
					new AIMMaterialInfo
					{
						AIMMaterialID = 0,
						AIMMaterialCode = "--Select--",
						AIMMaterialName = "--Select--"
					}
					);

				//task 1133 FG 7/21/2026 - removed the AIMOwnerLocationName column; show only Code and Name columns inside the dropdowns
				lueAIMProductCode.Properties.DataSource = aimMaterial;
				XtraEditors.LoadLookUpEdit(ref lueAIMProductCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
				lueAIMProductCode.Properties.Columns.Clear();
				lueAIMProductCode.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialCode", "Product Code"));
				lueAIMProductCode.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialName", "Product Name"));

				lueAIMProductName.Properties.DataSource = aimMaterial;
				XtraEditors.LoadLookUpEdit(ref lueAIMProductName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
				lueAIMProductName.Properties.Columns.Clear();
				lueAIMProductName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialName", "Product Name"));
				lueAIMProductName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialCode", "Product Code"));
			}
			else
			{
				DataBindSpecificProducts();
			}
		}
		private void grdSpecificProductsView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void lueOwnerLoc_SpecificProducts_Click(object sender, EventArgs e)
		{
			lueOwnerLoc_SpecificProducts.SelectAll();
		}

		private void lueOwnerLoc_SpecificProducts_Enter(object sender, EventArgs e)
		{
			lueOwnerLoc_SpecificProducts.SelectAll();
		}
		private void lueProductGroupCode_Enter(object sender, EventArgs e)
		{
			lueProductGroupCode.SelectAll();
		}

		private void lueProductGroupCode_Click(object sender, EventArgs e)
		{
			lueProductGroupCode.SelectAll();
		}

		#endregion

		//11/22/22		
		#region Owner Location Groups ------------------------------------------------------------
		private void DataBindOwnerLocationGroupsDetail()
		{
			List<AIMOwnerLocationGroupDetailInfo> group = new List<AIMOwnerLocationGroupDetailInfo>();

			if (lueOwnerLocationGroup.EditValue != null)
			{
				group = AIMManager.GetOwnerLocationGroupDetail_GroupID((Int32)lueOwnerLocationGroup.EditValue);

				Int32 currentRowNum = 0;
				if (grdOwnerLocationGroupView != null)
				{
					Int32.TryParse(grdOwnerLocationGroupView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdOwnerLocationGroup.DataSource = group;
				grdOwnerLocationGroupView.FocusedRowHandle = currentRowNum;
			}
		}
		private void DataBindlueOwnerLocationGroups()
		{
			List<AIMOwnerLocationGroupInfo> group = new List<AIMOwnerLocationGroupInfo>();
			group = AIMManager.GetOwnerLocationGroup(true);
			group.Insert
				(
				0,
				new AIMOwnerLocationGroupInfo
				{
					AIMOwnerLocationGroupID = 0,
					AIMOwnerLocationGroupCode = "--Select--"

				}
				);
			lueOwnerLocationGroup.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationGroup, "AIMOwnerLocationGroupID", "AIMOwnerLocationGroupCode");

			//11/23/22
			//5/18/23 - rearranged tabs so ingredient and product groups are together

			if (_tabGroupOwnerLocation == false)
			{
				//10/12/23 - selecting first record
				//if (group.Count == 2)
				if (group.Count > 1)
				{
					lueOwnerLocationGroup.EditValue = group[1].AIMOwnerLocationGroupID;
				}
				else
				{
					lueOwnerLocationGroup.EditValue = 0;
				}
			}

		}
		private void DataBindlueOwnerCode_Group()
		{
			List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
			owner = AIMManager.GetAIMOwner(true);
			owner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--"
				}
			);
			lueOwnerCode_Group.Properties.DataSource = owner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerCode_Group, "AIMOwnerID", "OwnerCode");
		}
		private void DataBindlueOwnerLocation_Group(Int32 ownerID)
		{
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
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
			lueOwnerLocationCode_Group.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationCode_Group, "AIMOwnerLocationID", "LocationCode");
			lueOwnerLocationName_Group.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationName_Group, "AIMOwnerLocationID", "LocationName");

			if (aimOwnerLocation.Count == 2)
			{
				//selecting value if there is only one
				lueOwnerLocationCode_Group.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
			}
			else
			{
				lueOwnerLocationCode_Group.EditValue = 0; // aimOwnerLocation;	//11/14/24
			}

		}
		private void lueOwnerCode_Group_EditValueChanged(object sender, EventArgs e)
		{
			DataBindlueOwnerLocation_Group((Int32)lueOwnerCode_Group.EditValue);
		}
		private void lueOwnerLocationCode_Group_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueOwnerLocationCode_Group, ref lueOwnerLocationName_Group);
		}
		private void lueOwnerLocationName_Group_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueOwnerLocationName_Group, ref lueOwnerLocationCode_Group);
		}
		private void lueOwnerLocationGroup_Click(object sender, EventArgs e)
		{
			lueOwnerLocationGroup.SelectAll();
		}
		private void lueOwnerLocationGroup_Enter(object sender, EventArgs e)
		{
			lueOwnerLocationGroup.SelectAll();
		}
		private void lueOwnerCode_Group_Click(object sender, EventArgs e)
		{
			lueOwnerCode_Group.SelectAll();
		}
		private void lueOwnerCode_Group_Enter(object sender, EventArgs e)
		{
			lueOwnerCode_Group.SelectAll();
		}
		private void lueOwnerLocationCode_Group_Click(object sender, EventArgs e)
		{
			lueOwnerLocationCode_Group.SelectAll();
		}
		private void lueOwnerLocationCode_Group_Enter(object sender, EventArgs e)
		{
			lueOwnerLocationCode_Group.SelectAll();
		}
		private void lueOwnerLocationName_Group_Click(object sender, EventArgs e)
		{
			lueOwnerLocationName_Group.SelectAll();
		}
		private void lueOwnerLocationName_Group_Enter(object sender, EventArgs e)
		{
			lueOwnerLocationName_Group.SelectAll();
		}
		private void btnAddNewOwnerLocationGroup_Click(object sender, EventArgs e)
		{
			gbAddOwnerLocationGroup.Location = new Point(7, 9);
			gbOwnerLocationGroupCode.Visible = false;

			gbAddOwnerLocationGroup.Visible = true;
			lueOwnerLocationGroup.EditValue = 0;
			txtOwnerLocationGroupCode.Focus();
		}
		private void btnAddNewOwnerLocationGroupCode_Click(object sender, EventArgs e)
		{
			if (txtOwnerLocationGroupCode.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtOwnerLocationGroupCode.Focus();
				return;
			}

			//save group code
			AIMOwnerLocationGroupInfo group = new AIMOwnerLocationGroupInfo();
			group.AIMOwnerLocationGroupID = 0;
			group.AIMOwnerLocationGroupCode = txtOwnerLocationGroupCode.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveOwnerLocationGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtOwnerLocationGroupCode.Text = String.Empty;
				return;
			}

			DataBindlueOwnerLocationGroups();
			lueOwnerLocationGroup.EditValue = error.RecordID;
			txtOwnerLocationGroupCode.Text = String.Empty;
			gbAddOwnerLocationGroup.Visible = false;
			gbOwnerLocationGroupCode.Visible = true;
		}
		private void btnDeleteOwnerLocationGroup_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueOwnerLocationGroup.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueOwnerLocationGroup.ShowPopup();
				return;
			}
			name = lueOwnerLocationGroup.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueOwnerLocationGroup.EditValue;

				//Delete from AIMOwnerLocationGroupDetail table
				AIMManager.DeleteOwnerLocationGroupDetail_GroupID(groupID);

				//Delete from AIMOwnerLocationGroup table
				AIMManager.DeleteOwnerLocationGroup(groupID);

				//11/28/22
				//delete from cfcuser (set GroupID = 0)
				AIMManager.UpdateCFCUsersAIMOwnerLocationGroup(groupID, 0);

				DataBindlueOwnerLocationGroups();

			}
		}
		private void btnPreviewOwnerLocationGroup_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdOwnerLocationGroup, "AIM Owner Location Groups");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddOwnerLocationGroup_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueOwnerLocationGroup.EditValue == 0)
				{
					MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueOwnerLocationGroup.ShowPopup();
					return;
				}

				if ((Int32)lueOwnerCode_Group.EditValue == 0)
				{
					MessageBox.Show("Please select an Owner. ", "Warning...", MessageBoxButtons.OK);
					lueOwnerCode_Group.ShowPopup();
					return;
				}

				if ((Int32)lueOwnerLocationCode_Group.EditValue == 0)
				{
					MessageBox.Show("Please select an Owner Location. ", "Warning...", MessageBoxButtons.OK);
					lueOwnerLocationCode_Group.ShowPopup();
					return;
				}

				AIMOwnerLocationGroupDetailInfo loc = new AIMOwnerLocationGroupDetailInfo();
				loc.AIMOwnerLocationGroupDetailID = 0;
				loc.AIMOwnerLocationGroupID = (Int32)lueOwnerLocationGroup.EditValue;
				loc.AIMOwnerLocationID = (Int32)lueOwnerLocationCode_Group.EditValue;
				loc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				loc.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveOwnerLocationGroupDetail(ref loc);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindOwnerLocationGroupsDetail();

				lueOwnerLocationCode_Group.EditValue = 0;
				lueOwnerLocationName_Group.EditValue = 0;
				lueOwnerLocationName_Group.ShowPopup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerLocationGroup_EditValueChanged(object sender, EventArgs e)
		{
			DataBindOwnerLocationGroupsDetail();
		}
		private void repDeleteOwnerLocationGroupDetail_Click(object sender, EventArgs e)
		{
			try
			{
				AIMOwnerLocationGroupDetailInfo group = new AIMOwnerLocationGroupDetailInfo();
				group = (AIMOwnerLocationGroupDetailInfo)grdOwnerLocationGroupView.GetFocusedRow();
				group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteOwnerLocationGroupDetail(group.AIMOwnerLocationGroupDetailID);
				DataBindOwnerLocationGroupsDetail();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void cmdCancel_Click(object sender, EventArgs e)
		{
			gpAddGroupCode.Visible = false;
			gbGroupCode.Visible = true;
		}
		private void button1_Click(object sender, EventArgs e)
		{
			gbAddOwnerLocationGroup.Visible = false;
			gbOwnerLocationGroupCode.Visible = true;
		}
		private void grdOwnerLocationGroupView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}
		//if owner-location is inactive, make row readonly and change background color
		private void grdOwnerLocationGroupView_ShownEditor(object sender, EventArgs e)
		{
			List<AIMOwnerLocationGroupDetailInfo> list = new List<AIMOwnerLocationGroupDetailInfo>();
			GridView view = (GridView)sender;
		}
		private void grdOwnerLocationGroupView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}
		#endregion

		#region Supplier Group -------------------------------------------------------------------				//8/30/23
		private void lueSupplierGroup_Click(object sender, EventArgs e)
		{
			lueSupplierGroup.SelectAll();
		}
		private void lueSupplierGroup_Enter(object sender, EventArgs e)
		{
			lueSupplierGroup.SelectAll();
		}
		private void lueSupplierGroup_EditValueChanged(object sender, EventArgs e)
		{
			DataBindSupplierGroupDetail();
		}
		private void DataBindlueSupplierGroup()
		{
			List<AIMSupplierGroupInfo> group = new List<AIMSupplierGroupInfo>();
			group = AIMManager.GetSupplierGroup(true);
			group.Insert
				(
				0,
				new AIMSupplierGroupInfo
				{
					AIMSupplierGroupID = 0,
					AIMSupplierGroupCode = "--Select--"

				}
				);
			lueSupplierGroup.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueSupplierGroup, "AIMSupplierGroupID", "AIMSupplierGroupCode");

			if (_tabGroupSuppliers == false)
			{
				//10/12/23 - selecting first record
				//if (group.Count == 2)
				if (group.Count > 1)
				{
					lueSupplierGroup.EditValue = group[1].AIMSupplierGroupID;
				}
				else
				{
					lueSupplierGroup.EditValue = 0;
				}
			}

		}
		private void DataBindSupplierGroupDetail()
		{
			List<AIMSupplierGroupDetailInfo> group = new List<AIMSupplierGroupDetailInfo>();

			if (lueSupplierGroup.EditValue != null)
			{
				group = AIMManager.GetSupplierGroupDetail_GroupID((Int32)lueSupplierGroup.EditValue);
				Int32 currentRowNum = 0;
				if (grdSupplierGroupView != null)
				{
					Int32.TryParse(grdSupplierGroupView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdSupplierGroup.DataSource = group;
				grdSupplierGroupView.FocusedRowHandle = currentRowNum;
			}
		}
		private void DataBindlueSupplierCodeGroup()
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
			lueSupplierCodeGroup.Properties.DataSource = aimSupplier;
			//task 1133 FG 7/21/2026 - show both the Code and Name columns inside the Supplier Code dropdown
			XtraEditors.LoadLookUpEdit(ref lueSupplierCodeGroup, "AIMSupplierID", "AIMSupplierCode", "SupplierName");

			//task 1133 FG 7/21/2026 - bind the Supplier Name dropdown to the same list as the Supplier Code dropdown
			lueSupplierNameGroup.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierNameGroup, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
			lueSupplierNameGroup.Properties.Columns.Clear();
			lueSupplierNameGroup.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("SupplierName", "Supplier Name"));
			lueSupplierNameGroup.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMSupplierCode", "Supplier Code"));

			if (aimSupplier.Count == 2)
			{
				lueSupplierCodeGroup.EditValue = aimSupplier[1].AIMSupplierID;
			}
			else
			{
				lueSupplierCodeGroup.EditValue = 0; // aimSupplier;	//11/14/24
			}

		}
		private void btnAddNewSupplierGroup_Click(object sender, EventArgs e)
		{
			gbAddSupplierGroup.Location = new Point(7, 9);
			gbSupplierGroupCode.Visible = false;
			gbAddSupplierGroup.Visible = true;
			gbSupplier.Visible = false;
			lueSupplierGroup.EditValue = 0;
			lueSupplierCodeGroup.EditValue = 0;
			txtSupplierGroupCode.Focus();
		}
		private void btnDeleteSupplierGroup_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueSupplierGroup.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueSupplierGroup.ShowPopup();
				return;
			}
			name = lueSupplierGroup.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueSupplierGroup.EditValue;

				//Delete from AIMSupplierGroup table				
				AIMManager.DeleteSupplierGroup(groupID);

				//Delete from AIMSupplierGroupDetail table					
				AIMManager.DeleteSupplierGroupDetail_GroupID(groupID);

				//Delete from AIMSupplierLocationGroup table
				AIMManager.DeleteSupplierLocationGroupAIMSupplierGroupID(groupID);

				//delete from cfcuser (set GroupID = 0)				
				AIMManager.UpdateCFCUsersAIMSupplierGroup(groupID, 0);

				DataBindlueSupplierGroup();
				DataBindlueSupplier_SL();


			}
		}
		private void btnAddNewSupplierGroupCode_Click(object sender, EventArgs e)
		{
			if (txtSupplierGroupCode.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtSupplierGroupCode.Focus();
				return;
			}

			//save group code
			AIMSupplierGroupInfo group = new AIMSupplierGroupInfo();
			group.AIMSupplierGroupID = 0;
			group.AIMSupplierGroupCode = txtSupplierGroupCode.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveSupplierGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtSupplierGroupCode.Text = String.Empty;
				return;
			}

			DataBindlueSupplierGroup();
			lueSupplierGroup.EditValue = error.RecordID;
			txtSupplierGroupCode.Text = String.Empty;
			gbAddSupplierGroup.Visible = false;
			gbSupplierGroupCode.Visible = true;
			gbSupplier.Visible = true;
		}
		private void btnCancelSupplierGroup_Click(object sender, EventArgs e)
		{
			gbAddSupplierGroup.Visible = false;
			gbSupplierGroupCode.Visible = true;
			gbSupplier.Visible = true;
		}
		private void lueSupplierCodeGroup_EditValueChanged(object sender, EventArgs e)
		{
			//DataBindlueSupplierCodeGroup((Int32)lueSupplierCodeGroup.EditValue);
			//DataBindlueSupplierCodeGroup();
			SyncValueLue(ref lueSupplierCodeGroup, ref lueSupplierNameGroup);       //task 1133 FG 7/21/2026
		}
		//task 1133 FG 7/21/2026 - keep the Supplier Name dropdown in sync with the Supplier Code dropdown
		private void lueSupplierNameGroup_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueSupplierNameGroup, ref lueSupplierCodeGroup);
		}
		private void lueSupplierNameGroup_Click(object sender, EventArgs e)
		{
			lueSupplierNameGroup.SelectAll();
		}
		private void lueSupplierNameGroup_Enter(object sender, EventArgs e)
		{
			lueSupplierNameGroup.SelectAll();
		}
		private void btnAddSupplierGroup_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueSupplierGroup.EditValue == 0)
				{
					MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueSupplierGroup.ShowPopup();
					return;
				}

				if ((Int32)lueSupplierCodeGroup.EditValue == 0)
				{
					MessageBox.Show("Please select a Supplier. ", "Warning...", MessageBoxButtons.OK);
					lueSupplierCodeGroup.ShowPopup();
					return;
				}

				AIMSupplierGroupDetailInfo loc = new AIMSupplierGroupDetailInfo();
				loc.AIMSupplierGroupDetailID = 0;
				loc.AIMSupplierGroupID = (Int32)lueSupplierGroup.EditValue;
				loc.AIMSupplierID = (Int32)lueSupplierCodeGroup.EditValue;
				loc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				loc.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveSupplierGroupDetail(ref loc);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindSupplierGroupDetail();

				lueSupplierCodeGroup.EditValue = 0;
				lueSupplierCodeGroup.ShowPopup();

				DataBindlueSupplierLocation_Group(loc.AIMSupplierID);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewSupplierGroup_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdSupplierGroup, "AIM Supplier Groups");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierGroupView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}
		private void grdSupplierGroupView_ShownEditor(object sender, EventArgs e)
		{
			//if supplier is inactive, make row readonly and change background color
			List<AIMSupplierGroupDetailInfo> list = new List<AIMSupplierGroupDetailInfo>();
			GridView view = (GridView)sender;
		}
		private void repDeleteSupplierGroupDetail_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierGroupDetailInfo group = new AIMSupplierGroupDetailInfo();
				group = (AIMSupplierGroupDetailInfo)grdSupplierGroupView.GetFocusedRow();
				group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteSupplierGroupDetail(group.AIMSupplierGroupDetailID);
				DataBindSupplierGroupDetail();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierGroupView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void lueSupplierCodeGroup_Click(object sender, EventArgs e)
		{
			lueSupplierCodeGroup.SelectAll();
		}

		private void lueSupplierCodeGroup_Enter(object sender, EventArgs e)
		{
			lueSupplierCodeGroup.SelectAll();
		}

		#endregion

		#region Supplier Location Groups	-----------------------------------------------------				//8/30/23
		private void DataBindlueSupplier_SL()
		{
			List<AIMSupplierGroupInfo> group = new List<AIMSupplierGroupInfo>();
			group = AIMManager.GetSupplierGroup(true);
			group.Insert
				(
				0,
				new AIMSupplierGroupInfo
				{
					AIMSupplierGroupID = 0,
					AIMSupplierGroupCode = "--Select--"

				}
				);
			lueSupplierGroup_SL.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueSupplierGroup_SL, "AIMSupplierGroupID", "AIMSupplierGroupCode");
			if (_tabGroupSupplierLocations == false)
			{
				//10/12/23 - selecting first record
				//if (group.Count == 2)
				if (group.Count > 1)
				{
					lueSupplierGroup_SL.EditValue = group[1].AIMSupplierGroupID;
				}
				else
				{
					lueSupplierGroup_SL.EditValue = 0;
				}
			}

		}
		private void DataBindSuppliersForSelectedSupplierGroup()
		{
			List<AIMSupplierLocationGroupInfo> group = new List<AIMSupplierLocationGroupInfo>();
			if (lueSupplierGroup_SL.EditValue != null)
			{
				group = AIMManager.GetSupplierLocationGroup_GroupID((Int32)lueSupplierGroup_SL.EditValue);
				Int32 currentRowNum = 0;
				if (grdSupplierLocationGroupView != null)
				{
					Int32.TryParse(grdSupplierLocationGroupView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdSupplierLocationGroup.DataSource = group;
				grdSupplierLocationGroupView.FocusedRowHandle = currentRowNum;
			}
		}
		private void DataBindlueSupplierLocation_Group(Int32 supplierID)
		{
			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			if (supplierID == 0)
			{
				aimSupplierLocation.Clear();
			}
			else
			{
				aimSupplierLocation = AIMManager.GetAIMSupplierLocationForSupplier(supplierID);
			}
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
			lueSupplierLocationCode_Group.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationCode_Group, "AIMSupplierLocationID", "AIMSupplierLocationCode");
			lueSupplierLocationName_Group.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName_Group, "AIMSupplierLocationID", "SupplierLocationName");
			if (aimSupplierLocation.Count == 2)
			{
				lueSupplierLocationCode_Group.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueSupplierLocationCode_Group.EditValue = 0; // aimSupplierLocation;	//11/14/24
			}

		}
		private void lueSupplierLocationGroup_Click(object sender, EventArgs e)
		{
			lueSupplierGroup_SL.SelectAll();
		}
		private void lueSupplierLocationGroup_Enter(object sender, EventArgs e)
		{
			lueSupplierGroup_SL.SelectAll();
		}
		private void lueSupplierCode_Group_EditValueChanged(object sender, EventArgs e)
		{
			DataBindlueSupplierLocation_Group((Int32)lueSupplierCode_Group.EditValue);
		}
		private void lueSupplierCode_Group_Click(object sender, EventArgs e)
		{
			lueSupplierCode_Group.SelectAll();
		}
		private void lueSupplierCode_Group_Enter(object sender, EventArgs e)
		{
			lueSupplierCode_Group.SelectAll();
		}
		private void lueSupplierLocationCode_Group_Enter(object sender, EventArgs e)
		{
			lueSupplierLocationCode_Group.SelectAll();
		}
		private void lueSupplierLocationCode_Group_Click(object sender, EventArgs e)
		{
			lueSupplierLocationCode_Group.SelectAll();
		}
		private void lueSupplierLocationCode_Group_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueSupplierLocationCode_Group, ref lueSupplierLocationName_Group);
		}
		private void lueSupplierLocationName_Group_Click(object sender, EventArgs e)
		{
			lueSupplierLocationName.SelectAll();
		}
		private void lueSupplierLocationName_Group_Enter(object sender, EventArgs e)
		{
			lueSupplierLocationName.SelectAll();
		}
		private void lueSupplierLocationName_Group_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueSupplierLocationName_Group, ref lueSupplierLocationCode_Group);
		}
		private void btnAddSupplierLocationGroup_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueSupplierGroup_SL.EditValue == 0)
				{
					MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueSupplierGroup_SL.ShowPopup();
					return;
				}

				if ((Int32)lueSupplierCode_Group.EditValue == 0)
				{
					MessageBox.Show("Please select a Supplier. ", "Warning...", MessageBoxButtons.OK);
					lueSupplierCode_Group.ShowPopup();
					return;
				}

				if ((Int32)lueSupplierLocationCode_Group.EditValue == 0)
				{
					MessageBox.Show("Please select a Supplier Location. ", "Warning...", MessageBoxButtons.OK);
					lueSupplierLocationCode_Group.ShowPopup();
					return;
				}

				AIMSupplierLocationGroupInfo loc = new AIMSupplierLocationGroupInfo();
				loc.AIMSupplierLocationGroupID = 0;
				loc.AIMSupplierGroupID = (Int32)lueSupplierGroup_SL.EditValue;
				loc.AIMSupplierID = (Int32)lueSupplierCode_Group.EditValue;
				loc.AIMSupplierLocationID = (Int32)lueSupplierLocationCode_Group.EditValue;
				loc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				loc.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveSupplierLocationGroup(ref loc);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindSuppliersForSelectedSupplierGroup();

				lueSupplierLocationCode_Group.EditValue = 0;
				lueSupplierLocationName_Group.EditValue = 0;
				lueSupplierLocationCode_Group.ShowPopup();     

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewSupplierLocationGroup_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdSupplierLocationGroup, "AIM Supplier Location Groups");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierLocationGroupView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}
		private void grdSupplierLocationGroupView_ShownEditor(object sender, EventArgs e)
		{
			//if supplier-location is inactive, make row readonly and change background color
			List<AIMSupplierLocationGroupInfo> list = new List<AIMSupplierLocationGroupInfo>();
			GridView view = (GridView)sender;
		}
		private void DataBindlueSupplierCode_Group()
		{
			List<AIMSupplierGroupDetailInfo> supplier = new List<AIMSupplierGroupDetailInfo>();
			if ((Int32)lueSupplierGroup_SL.EditValue != null)
			{
				supplier = AIMManager.GetSupplierGroupDetail_GroupID((Int32)lueSupplierGroup_SL.EditValue);
				supplier.Insert
				(
					0,
					new AIMSupplierGroupDetailInfo
					{
						AIMSupplierID = 0,
						AIMSupplierCode = "--Select--"
					}
				);
				lueSupplierCode_Group.Properties.DataSource = supplier;
				XtraEditors.LoadLookUpEdit(ref lueSupplierCode_Group, "AIMSupplierID", "AIMSupplierCode");

			}
		}
		private void lueSupplierLocationGroup_EditValueChanged(object sender, EventArgs e)
		{
			DataBindSuppliersForSelectedSupplierGroup();
			DataBindlueSupplierCode_Group();
		}
		private void repDeleteSupplierLocationGroup_Click(object sender, EventArgs e)
		{
			try
			{
				AIMSupplierLocationGroupInfo group = new AIMSupplierLocationGroupInfo();
				group = (AIMSupplierLocationGroupInfo)grdSupplierLocationGroupView.GetFocusedRow();
				group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteSupplierLocationGroup(group.AIMSupplierLocationGroupID);
				DataBindSuppliersForSelectedSupplierGroup();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnHelpSupplierLocationGroups_Click(object sender, EventArgs e)                        //9/6/23
		{
			AIMSupplierLocationGroupHelp supplierGroupHelp = new AIMSupplierLocationGroupHelp();
			supplierGroupHelp.ShowDialog();
		}
		private void grdSupplierLocationGroupView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}
		#endregion

		#region SortGrids
		//2/28/20 - this sorts alphanumeric column the way that makes the most sense!  
		// For this to work, set the GridColumn.SortMode property to ColumnSortMode.Custom and then add the columns to the function below
		private void grdNutrientView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//4/12/22 - changed to global function
			//Columns set to Custom SortMode: colAIMNutrientCode
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
			//if (e.Column.FieldName == "AIMNutrientCode")
			//{
			//	int value1, value2;
			//	if (int.TryParse(e.Value1.ToString(), out value1) && int.TryParse(e.Value2.ToString(), out value2))
			//	{
			//		e.Handled = true;
			//		e.Result = value1.CompareTo(value2);
			//	}
			//}
		}
		private void grdMaterialView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)                       //4/12/22
		{
			//Columns set to Custom SortMode: "colAIMMaterialCode"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}
		private void grdMaterialStockStatusView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colAIMOwnerLocationCode_MaterialStockStatus", "colAIMMaterialCode_MaterialStockStatus"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}         //4/12/22
		private void grdOwnerView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colOwnerCode"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}                       //4/12/22
		private void grdOwnerLocationView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colOwnerLocation_OwnerCode", "colOwnerLocationCode"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}               //4/12/22
		private void grdSupplierView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colSupplierCode"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}                    //4/12/22
		private void grdSupplierLocationView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colAIMSupplierIDSupplierLocation", "colAIMSupplierLocationCode"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}            //4/12/22
		private void grdGuaranteeView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colAIMSupplierIDGuarantee", "colAIMSupplierLocationIDGuarantee", "colAIMMaterialIDGuarantee", "colAIMNutrientIDGuarantee"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}                   //4/12/22
		private void grdWarningLimitsView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colAIMMaterialCodeWarning", "colAIMNutrientCodeWarning"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}               //4/12/22		
		#endregion

		//8/10/23
		#region TEST TYPE -----------------------------------------------------------------------------------------------		
		private void DataBindTestType()
		{
			_testTypeHeader = AIMManager.GetSampleTestType();
			_testTypeHeader.Insert
				(
					0,
					new SampleTestTypeInfo
					{
						SampleTestTypeID = 0,
						SampleTestTypeName = "--Select--"
					}
				);
			lueTestType.Properties.DataSource = _testTypeHeader;
			XtraEditors.LoadLookUpEdit(ref lueTestType, "SampleTestTypeID", "SampleTestTypeName");
			lueTestType.Properties.Columns[1].Caption = "Test Type Name";

			SampleTestTypeDetailInfo clearResults = new SampleTestTypeDetailInfo();
			grdTestType.DataSource = clearResults;
			gbAddDataSource.Visible = false;
			grdTestType.Visible = false;
		}
		private void DataBindTestTypeDetail()
		{
			_testTypeDetail = AIMManager.GetSampleTestTypeDetail(_testTypeID, rbActiveRecords.Checked);
			grdTestType.DataSource = _testTypeDetail;
		}
		private void DataBindLueTestTypeDatasource()
		{
			List<DataSourceTypeInfo> datasource = new List<DataSourceTypeInfo>();
			datasource = AIMManager.GetDataSourceType(true);
			datasource.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--Select--"
				}
			);
			lueTestTypeDatasource.Properties.DataSource = datasource;
			XtraEditors.LoadLookUpEdit(ref lueTestTypeDatasource, "DataSourceTypeID", "Name", "TypeDisplay");
		}
		private void btnTestTypeAddNew_Click(object sender, EventArgs e)
		{
			panelExistingTestType.Visible = false;
			panelExistingTestType.Location = new Point(479, 157);
			panelExistingTestType.Size = new Size(407, 8);

			panelNewTestType.Visible = true;
			panelNewTestType.Location = new Point(9, 57);
			panelNewTestType.Size = new Size(877, 69);

			gbAddDataSource.Visible = false;
			grdTestType.Visible = false;
			_saveTestTypeAs = false;
			txtSampleTestTypeNameNew.Text = "";
			txtSampleTestTypeNameNew.ReadOnly = false;
			txtSampleTestTypeNameNew.Focus();
		}
		private void btnCancelSaveTestType_Click(object sender, EventArgs e)
		{
			txtSampleTestTypeNameNew.Text = "";
			panelNewTestType.Visible = false;
			if (_saveTestTypeAs == true)
			{
				panelExistingTestType.Visible = true;
			}
		}
		private void btnSaveTestType_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtSampleTestTypeNameNew.Text.Length == 0)
				{
					MessageBox.Show("Please enter a Test Type Name.");
					txtSampleTestTypeNameNew.Focus();
					return;
				}

				//check to see if name already exists (use new name to search template table)
				List<SampleTestTypeInfo> testType = new List<SampleTestTypeInfo>();
				testType = AIMManager.GetSampleTestType(txtSampleTestTypeNameNew.Text.ToString());
				if (testType == null)
				{
				}
				else if (testType.Count > 0)
				{
					MessageBox.Show("This test type name already exists. ", "Warning...", MessageBoxButtons.OK);
					txtEditTestTypeName.Text = lueTestType.Text;
					return;
				}

				SampleTestTypeInfo type = new SampleTestTypeInfo();
				type.SampleTestTypeID = 0;
				type.SampleTestTypeName = txtSampleTestTypeNameNew.Text;
				type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo outPut = AIMManager.SaveSampleTestType(ref type);

				if (_saveTestTypeAs == true)
				{
					SampleTestTypeDetailInfo detail = new SampleTestTypeDetailInfo();
					List<SampleTestTypeDetailInfo> copyResult = new List<SampleTestTypeDetailInfo>();
					AIMManager.GetSampleTestTypeDetail(_saveAsCopyTestTypeID, true);
					foreach (var item in copyResult)
					{
						detail.SampleTestTypeDetailID = 0;
						detail.SampleTestTypeID = outPut.RecordID;
						detail.DataSourceTypeID = item.DataSourceTypeID;
						detail.SortOrder = item.SortOrder;
						detail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						detail.UpdateStamp = DateTime.Now;
						ErrorInfo outPutResult = AIMManager.SaveSampleTestTypeDetail(ref detail);
						DataBindTestTypeDetail();
					}
					_saveTestTypeAs = false;
					panelExistingTestType.Visible = true;
					panelNewTestType.Visible = false;
				}

				SampleTestTypeDetailInfo clearResults = new SampleTestTypeDetailInfo();
				grdTestType.DataSource = clearResults;

				DataBindTestType();
				_testTypeID = outPut.RecordID;
				txtSampleTestTypeNameNew.Text = "";

				DataBindLueTestTypeDatasource();

				//10/3/23
				DataBindlueTestType_Group();

				panelNewTestType.Visible = false;

				panelExistingTestType.Visible = true;
				panelExistingTestType.Location = new Point(9, 57);
				panelExistingTestType.Size = new Size(877, 69);


				txtSampleTestTypeNameNew.ReadOnly = true;
				lueTestType.EditValue = _testTypeID;
				lueTestTypeDatasource.SelectAll();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnDelete_TestType_Click(object sender, EventArgs e)
		{
			String name = "";
			try
			{
				if (panelExistingTestType.Visible == true)
				{
					if ((Int32)lueTestType.EditValue == 0)
					{
						MessageBox.Show("Please select Test Type to delete.");
						lueTestType.Focus();
						return;
					}
					name = lueTestType.Text;
				}
				else
				{
					name = txtSampleTestTypeNameNew.Text;
				}
				DialogResult dialogResult = MessageBox.Show("Delete Test Type '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dialogResult == DialogResult.Yes)
				{
					Int32 ID = 0;
					ID = (Int32)lueTestType.EditValue;
					AIMManager.DeleteSampleTestType(ID);

					DataBindTestType();
					DataBindTestTypeDetail();
					txtEditTestTypeName.Text = String.Empty;
					txtSampleTestTypeNameNew.Text = String.Empty;
					lueTestTypeDatasource.EditValue = 0;

					gbAddDataSource.Visible = false;
					grdTestType.Visible = false;

					panelNewTestType.Visible = false;
					panelNewTestType.Location = new Point(479, 135);
					panelNewTestType.Size = new Size(407, 8);

					panelExistingTestType.Visible = true;
					panelExistingTestType.Location = new Point(9, 57);
					panelExistingTestType.Size = new Size(877, 69);


				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnTestTypeSaveAs_Click(object sender, EventArgs e)
		{
			_saveTestTypeAs = true;
			_saveAsCopyTestTypeID = _testTypeID;
			panelExistingTestType.Visible = false;
			panelNewTestType.Visible = true;
		}
		private void btnTestTypeExisting_Click(object sender, EventArgs e)
		{
			panelNewTestType.Visible = false;
			panelNewTestType.Location = new Point(479, 135);
			panelNewTestType.Size = new Size(407, 8);

			panelExistingTestType.Visible = true;
			panelExistingTestType.Location = new Point(9, 57);
			panelExistingTestType.Size = new Size(877, 69);

			lueTestType.ShowPopup();

			if ((Int32)lueTestType.EditValue != 0)
			{
				gbAddDataSource.Visible = true;
				grdTestType.Visible = true;
				_testTypeID = (Int32)lueTestType.EditValue;
				DataBindLueTestTypeDatasource();
			}
		}
		private void btnTestTypeDelete_Click(object sender, EventArgs e)
		{
			String name = "";
			try
			{
				if (panelExistingTestType.Visible == true)
				{
					if ((Int32)lueTestType.EditValue == 0)
					{
						MessageBox.Show("Please select Test Type to delete.");
						lueTestType.Focus();
						lueTestType.ShowPopup();
						return;
					}
					name = lueTestType.Text;
				}
				else
				{
					name = txtSampleTestTypeNameNew.Text;
				}

				//If TestType is tied to a sample, do not allow delete				
				String msg;
				SampleInfo sample = new SampleInfo();
				sample = AIMManager.GetSampleUsingTestType((Int32)lueTestType.EditValue);
				if (sample != null)
				{
					msg = "This Test Type cannot be deleted because it is linked to a Sample.";
					msg = msg + Environment.NewLine + Environment.NewLine;
					msg = msg + "Instead, you may inactivate all the datasourcs for this Test Type by clicking the Active icon for each datasource.";
					MessageBox.Show(msg, "Information...", MessageBoxButtons.OK);
					return;
				}

				DialogResult dialogResult = MessageBox.Show("Delete Test Type '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dialogResult == DialogResult.Yes)
				{
					Int32 ID = 0;
					ID = (Int32)lueTestType.EditValue;
					AIMManager.DeleteSampleTestType(ID);

					DataBindTestType();
					DataBindTestTypeDetail();
					txtEditTestTypeName.Text = String.Empty;
					txtSampleTestTypeNameNew.Text = String.Empty;
					lueTestTypeDatasource.EditValue = 0;

					gbAddDataSource.Visible = false;
					grdTestType.Visible = false;

					panelNewTestType.Visible = false;
					panelNewTestType.Location = new Point(479, 135);
					panelNewTestType.Size = new Size(407, 8);

					panelExistingTestType.Visible = true;
					panelExistingTestType.Location = new Point(9, 57);
					panelExistingTestType.Size = new Size(877, 69);

					//10/3/23 - delete from group detail, if test-type exists there
					AIMManager.DeleteTestTypeGroupDetail_TestTypeID(ID);
					DataBindlueTestType_Group();
					DataBindTestTypeGroupDetail();

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueTestType_EditValueChanged(object sender, EventArgs e)
		{
			if ((Int32)lueTestType.EditValue != 0)
			{
				gbAddDataSource.Visible = true;
				grdTestType.Visible = true;
				_testTypeID = (Int32)lueTestType.EditValue;

				DataBindLueTestTypeDatasource();
				DataBindTestTypeDetail();

			}
		}
		private void txtEditTestTypeName_Validated(object sender, EventArgs e)
		{
			Int32 testTypeID = 0;
			try
			{
				if ((txtEditTestTypeName.Text != lueTestType.Text))
				{

					//get current test type id							
					testTypeID = (Int32)lueTestType.EditValue;

					//check to see if name already exists (use new name to search template table)
					List<SampleTestTypeInfo> type = new List<SampleTestTypeInfo>();
					type = AIMManager.GetSampleTestType(txtEditTestTypeName.Text.ToString());
					if (type == null)
					{
					}
					else if (type.Count > 0)
					{
						MessageBox.Show("This Test Type name already exists. ", "Warning...", MessageBoxButtons.OK);
						txtEditTestTypeName.Text = lueTestType.Text;
					}
					else
					{
						SampleTestTypeInfo result = new SampleTestTypeInfo();
						result.SampleTestTypeID = testTypeID;
						result.SampleTestTypeName = txtEditTestTypeName.Text;
						result.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						ErrorInfo outPut = AIMManager.SaveSampleTestType(ref result);

						btnRefresh.PerformClick();
						DataBindTestType();
						lueTestType.EditValue = testTypeID;

					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnTestTypeAddDatasource_Click(object sender, EventArgs e)
		{
			try
			{
				btnTestTypeAddDatasource.BackColor = SystemColors.Control;

				if ((Int32)lueTestTypeDatasource.EditValue == 0)
				{
					MessageBox.Show("Please select Data Source.");
					lueTestTypeDatasource.Focus();
					lueTestTypeDatasource.ShowPopup();
					return;
				}

				//get max SortOrder# from _testTypeDetail	                
				ErrorInfo maxSortNumber = AIMManager.GetSampleTestTypeDetailMaxSortNo(_testTypeID);
				SampleTestTypeDetailInfo type = new SampleTestTypeDetailInfo();
				type.SampleTestTypeDetailID = 0;
				type.SampleTestTypeID = _testTypeID;
				type.DataSourceTypeID = (Int32)lueTestTypeDatasource.EditValue;
				type.SortOrder = maxSortNumber.RecordID + 1;
				type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				type.UpdateStamp = DateTime.Now;
				type.IsActive = true;
				ErrorInfo outPut = AIMManager.SaveSampleTestTypeDetail(ref type);
				DataBindTestTypeDetail();

				lueTestTypeDatasource.Focus();
				lueTestTypeDatasource.SelectAll();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdTestTypeView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				ErrorInfo rtn = new ErrorInfo();
				SampleTestTypeDetailInfo type = new SampleTestTypeDetailInfo();
				type = (SampleTestTypeDetailInfo)grdTestTypeView.GetFocusedRow();

				//Check to make sure sort order# isn't already assigned                
				rtn = AIMManager.GetSampleTestTypeDetailSortOrder(type.SampleTestTypeID, type.SortOrder);
				if (rtn.ErrorNumber == 0)
				{
					type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					type.UpdateStamp = DateTime.Now;
					AIMManager.SaveSampleTestTypeDetail(ref type);
				}
				else
				{
					MessageBox.Show("This sort number is already assigned.");
				}
				DataBindTestTypeDetail();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repItemDeleteTestTypeDetail_Click(object sender, EventArgs e)
		{
			try
			{
				String msg;
				DialogResult dialogResult;
				SampleTestTypeDetailInfo type = new SampleTestTypeDetailInfo();
				type = (SampleTestTypeDetailInfo)grdTestTypeView.GetFocusedRow();
				type.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//make sure this test-type/datasource is not assiged to a sample.
				SampleInfo sample = new SampleInfo();
				sample = AIMManager.GetSampleUsingTestType(type.SampleTestTypeID);
				if (sample != null)
				{
					msg = "This datasource cannot be deleted because the Test Type is linked to a Sample.";
					msg = msg + Environment.NewLine + Environment.NewLine;
					msg = msg + "Instead, you may inactivate the datasource by clicking the Active icon.";
					MessageBox.Show(msg, "Information...", MessageBoxButtons.OK);
					return;
				}
				dialogResult = MessageBox.Show("Are you sure you want to delete this datasource from the Test Type?", "Warning...", MessageBoxButtons.YesNo);
				if (dialogResult == DialogResult.Yes)
				{
					AIMManager.DeleteSampleTestTypeDetail(type.SampleTestTypeDetailID);
					DataBindTestTypeDetail();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdTestTypeView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colImageTestType)
			{
				//get value in IsActive column for this row
				if (((SampleTestTypeDetailInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
				}
			}
		}
		private void repItemPictureEditTestType_Click(object sender, EventArgs e)
		{
			try
			{
				SampleTestTypeDetailInfo testType = new SampleTestTypeDetailInfo();
				testType = (SampleTestTypeDetailInfo)grdTestTypeView.GetFocusedRow();
				testType.IsActive = !testType.IsActive;
				AIMManager.SaveTestTypeDetailIsActive(testType.SampleTestTypeDetailID, testType.IsActive, userGlobal.CFCLoginUser.CFCUserID);
				DataBindTestTypeDetail();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdTestTypeView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}
		#endregion

		#region TEST TYPE GROUPS --------------------------------------------------------------------------------------
		private void btnAddNewTestTypeGroup_Click(object sender, EventArgs e)
		{
			gbAddTestTypeGroup.Location = new Point(7, 9);
			gbTestTypeGroupCode.Visible = false;
			gbAddTestTypeGroup.Visible = true;
			lueTestTypeGroup.EditValue = 0;
			txtTestTypeGroupCode.Focus();
		}
		private void btnDeleteTestTypeGroup_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueTestTypeGroup.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueTestTypeGroup.ShowPopup();
				return;
			}
			name = lueTestTypeGroup.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueTestTypeGroup.EditValue;

				//Delete from AIMTestTypeGroup table
				AIMManager.DeleteTestTypeGroup(groupID);

				//delete from cfcuser (set GroupID = 0)
				AIMManager.UpdateCFCUsersSampleTestTypeGroup(groupID, 0);

				DataBindlueTestTypeGroup();

			}
		}
		private void DataBindlueTestTypeGroup()
		{
			List<SampleTestTypeGroupInfo> group = new List<SampleTestTypeGroupInfo>();
			group = AIMManager.GetTestTypeGroup(true);
			group.Insert
				(
				0,
				new SampleTestTypeGroupInfo
				{
					SampleTestTypeGroupID = 0,
					SampleTestTypeGroupCode = "--Select--"

				}
				);
			lueTestTypeGroup.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueTestTypeGroup, "SampleTestTypeGroupID", "SampleTestTypeGroupCode");

			//10/12/23 - selecting first record
			//if (group.Count == 2)
			if (group.Count > 1)
			{
				lueTestTypeGroup.EditValue = group[1].SampleTestTypeGroupID;
			}
			else
			{
				lueTestTypeGroup.EditValue = 0;
			}


		}
		private void btnAddTestType_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueTestTypeGroup.EditValue == 0)
				{
					MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueTestTypeGroup.ShowPopup();
					return;
				}

				if ((Int32)lueTestType_Group.EditValue == 0)
				{
					MessageBox.Show("Please select a Test Type. ", "Warning...", MessageBoxButtons.OK);
					lueTestType_Group.ShowPopup();
					return;
				}

				SampleTestTypeGroupDetailInfo group = new SampleTestTypeGroupDetailInfo();
				group.SampleTestTypeGroupDetailID = 0;
				group.SampleTestTypeGroupID = (Int32)lueTestTypeGroup.EditValue;
				group.SampleTestTypeID = (Int32)lueTestType_Group.EditValue;
				group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				group.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveTestTypeGroupDetail(ref group);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindTestTypeGroupDetail();

				lueTestType_Group.EditValue = 0;
				lueTestType_Group.ShowPopup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddNewTestTypeGroupCode_Click(object sender, EventArgs e)
		{
			if (txtTestTypeGroupCode.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtTestTypeGroupCode.Focus();
				return;
			}

			//save group code
			SampleTestTypeGroupInfo group = new SampleTestTypeGroupInfo();
			group.SampleTestTypeGroupID = 0;
			group.SampleTestTypeGroupCode = txtTestTypeGroupCode.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveTestTypeGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtTestTypeGroupCode.Text = String.Empty;
				return;
			}

			DataBindlueTestTypeGroup();
			lueTestTypeGroup.EditValue = error.RecordID;
			txtTestTypeGroupCode.Text = String.Empty;
			gbAddTestTypeGroup.Visible = false;
			gbTestTypeGroupCode.Visible = true;
			gbTestType.Visible = true;
		}
		private void btnCancelTestTypeGroup_Click(object sender, EventArgs e)
		{
			gbAddTestTypeGroup.Visible = false;
			gbTestTypeGroupCode.Visible = true;
			gbTestType.Visible = true;
		}
		private void lueTestTypeGroup_EditValueChanged(object sender, EventArgs e)
		{
			DataBindTestTypeGroupDetail();
		}
		private void DataBindTestTypeGroupDetail()
		{
			List<SampleTestTypeGroupDetailInfo> group = new List<SampleTestTypeGroupDetailInfo>();

			if (lueTestTypeGroup.EditValue != null)
			{
				group = AIMManager.GetTestTypeGroupDetail_GroupID((Int32)lueTestTypeGroup.EditValue);
				Int32 currentRowNum = 0;
				if (grdTestTypeGroupView != null)
				{
					Int32.TryParse(grdTestTypeGroupView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdTestTypeGroup.DataSource = group;
				grdTestTypeGroupView.FocusedRowHandle = currentRowNum;
			}
		}
		private void lueTestTypeGroup_Enter(object sender, EventArgs e)
		{
			lueTestTypeGroup.SelectAll();
		}
		private void lueTestTypeGroup_Click(object sender, EventArgs e)
		{
			lueTestTypeGroup.SelectAll();
		}
		private void DataBindlueTestType_Group()
		{
			List<SampleTestTypeInfo> testType = new List<SampleTestTypeInfo>();
			testType = AIMManager.GetSampleTestType();
			testType.Insert
			(
				0,
				new SampleTestTypeInfo
				{
					SampleTestTypeID = 0,
					SampleTestTypeName = "--Select--"

				}
			);
			lueTestType_Group.Properties.DataSource = testType;
			XtraEditors.LoadLookUpEdit(ref lueTestType_Group, "SampleTestTypeID", "SampleTestTypeName");

		}
		private void btnPreviewTestTypeGroup_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdTestTypeGroup, "Test Type Groups");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repDeleteTestTypeGroupDetail_Click(object sender, EventArgs e)
		{
			try
			{
				SampleTestTypeGroupDetailInfo group = new SampleTestTypeGroupDetailInfo();
				group = (SampleTestTypeGroupDetailInfo)grdTestTypeGroupView.GetFocusedRow();
				group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteTestTypeGroupDetail_DetailID(group.SampleTestTypeGroupDetailID);
				DataBindTestTypeGroupDetail();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdTestTypeGroupView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}
		private void grdTestTypeGroupView_ShownEditor(object sender, EventArgs e)
		{
			//if test type is inactive, make row readonly and change background color
			List<SampleTestTypeGroupDetailInfo> list = new List<SampleTestTypeGroupDetailInfo>();
			GridView view = (GridView)sender;
		}
		private void grdTestTypeGroupView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}

		private void lueTestType_Click(object sender, EventArgs e)
		{
			lueTestType.SelectAll();
		}

		private void lueTestType_Enter(object sender, EventArgs e)
		{
			lueTestType.SelectAll();
		}

		private void lueTestTypeDatasource_Click(object sender, EventArgs e)
		{
			lueTestTypeDatasource.SelectAll();
		}

		private void lueTestTypeDatasource_Enter(object sender, EventArgs e)
		{
			lueTestTypeDatasource.SelectAll();
		}
		private void lueTestType_Group_Click(object sender, EventArgs e)
		{
			lueTestType_Group.SelectAll();
		}

		private void lueTestType_Group_Enter(object sender, EventArgs e)
		{
			lueTestType_Group.SelectAll();
		}
		#endregion

		//10/11/23
		#region INGREDIENT EXPORT GROUPS --------------------------------------------------------------------------------------		
		private void DataBindExportIngredientByGroup()
		{
			List<ExportIngredientInfo> export = new List<ExportIngredientInfo>();
			export = AIMManager.GetExportIngredient_GroupID((Int32)lueGroupCodeExport.EditValue);
			Int32 currentRowNum = 0;
			if (grdIngredientExportView != null)
			{
				Int32.TryParse(grdIngredientExportView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdIngredientExport.DataSource = export;
			grdIngredientExportView.FocusedRowHandle = currentRowNum;
		}
		private void DataBindExportIngredient()
		{
			List<CFCIngredientInfo> cfcIngredient = new List<CFCIngredientInfo>();
			cfcIngredient = AIMManager.GetAllCFCIngredientAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
			cfcIngredient.Insert
				(
				0,
				new CFCIngredientInfo
				{
					CFCIngredientID = 0,
					CFCIngredientCode = "--Select--",
					CFCIngredientName = "--Select--"
				}
				);

			lueCFCIngredientCode.Properties.DataSource = cfcIngredient;
			XtraEditors.LoadLookUpEdit(ref lueCFCIngredientCode, "CFCIngredientID", "CFCIngredientCode", "CFCIngredientName");
			lueCFCIngredientName.Properties.DataSource = cfcIngredient;
			XtraEditors.LoadLookUpEdit(ref lueCFCIngredientName, "CFCIngredientID", "CFCIngredientName", "CFCIngredientCode");

			List<ExportIngredientInfo> export = new List<ExportIngredientInfo>();
			export = AIMManager.GetExportIngredient_GroupID((Int32)lueGroupCodeExport.EditValue);

			Int32 currentRowNum = 0;
			if (grdIngredientExportView != null)
			{
				Int32.TryParse(grdIngredientExportView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdIngredientExport.DataSource = export;
			grdIngredientExportView.FocusedRowHandle = currentRowNum;
		}
		private void DataBindExportIngredientGroup()
		{
			//-------------------------------------------------------------
			//Group
			List<ExportIngredientGroupInfo> group = new List<ExportIngredientGroupInfo>();
			group = AIMManager.GetExportIngredientGroup(true);
			group.Insert
				(
				0,
				new ExportIngredientGroupInfo
				{
					ExportIngredientGroupID = 0,
					ExportIngredientGroupCode = "--Select--"
				}
				);
			lueGroupCodeExport.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueGroupCodeExport, "ExportIngredientGroupID", "ExportIngredientGroupCode");

			//-------------------------------------------------------------
			//Parent
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

			//10/13/23
			//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient);
			aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)EnumOrderBy.Name);

			aimMaterial.Insert
				(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--"
				}
				);

			lueAimParentExportCode.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAimParentExportCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
			lueAimParentExportName.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAimParentExportName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");



			//-------------------------------------------------------------
			if (_tabGroupIngredients == false)
			{
				if (group.Count > 1)
				{
					lueGroupCodeExport.EditValue = group[1].ExportIngredientGroupID;
				}
				else
				{
					lueGroupCodeExport.EditValue = 0;
				}
			}


		}
		private void btnAddNewGroupExport_Click(object sender, EventArgs e)
		{
			gpAddGroupCodeExport.Location = new Point(7, 9);
			gpAddGroupCodeExport.Visible = true;
			lueGroupCodeExport.EditValue = 0;

			gbExportIngredient.Visible = false;
			btnAddIngredientExport.Visible = false;
			btnPreviewIngredientExport.Visible = false;

			txtGroupCodeExport.Focus();
		}
		private void btnDeleteGroupExport_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueGroupCodeExport.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueGroupCodeExport.ShowPopup();
				return;
			}
			name = lueGroupCodeExport.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueGroupCodeExport.EditValue;

				//Delete from ExportIngredient table
				AIMManager.DeleteExportIngredient_GroupID(groupID);

				//Delete from ExportIngredientGroup table
				AIMManager.DeleteExportIngredientGroup(groupID);

				DataBindExportIngredientGroup();
				DataBindExportIngredient();

			}
		}
		private void btnAddNewGroupCodeExport_Click(object sender, EventArgs e)
		{
			if (txtGroupCodeExport.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtGroupCodeExport.Focus();
				return;
			}

			if ((Int32)lueAimParentExportCode.EditValue == 0)
			{
				MessageBox.Show("Please select an AIM Ingredient Code. ", "Warning...", MessageBoxButtons.OK);
				lueAimParentExportCode.Focus();
				return;
			}

			//save group code
			ExportIngredientGroupInfo group = new ExportIngredientGroupInfo();
			group.ExportIngredientGroupID = 0;
			group.ExportIngredientGroupCode = txtGroupCodeExport.Text;
			group.AIMMaterialID = (Int32)lueAimParentExportCode.EditValue;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveExportIngredientGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtGroupCode.Text = String.Empty;
				return;
			}

			DataBindExportIngredientGroup();
			lueGroupCodeExport.EditValue = error.RecordID;
			lueGroupCodeExport.Text = String.Empty;


			gpAddGroupCodeExport.Visible = false;
			gbGroupCodeExport.Visible = true;

			gbExportIngredient.Visible = true;
			btnAddIngredientExport.Visible = true;
			btnPreviewIngredientExport.Visible = true;


		}
		private void cmdCancelExport_Click(object sender, EventArgs e)
		{
			gpAddGroupCodeExport.Visible = false;
			gbGroupCodeExport.Visible = true;
			gbExportIngredient.Visible = true;
			btnAddIngredientExport.Visible = true;
			btnPreviewIngredientExport.Visible = true;

		}
		private void btnSaveGroupAsExport_Click(object sender, EventArgs e)
		{
			if (txtSaveAsGroupCodeExport.Text == String.Empty)
			{
				MessageBox.Show("Please enter a Group Code.", "Warning...", MessageBoxButtons.OK);
				txtSaveAsGroupCodeExport.Focus();
				return;
			}

			//Save the group
			ExportIngredientGroupInfo group = new ExportIngredientGroupInfo();
			group.ExportIngredientGroupID = 0;
			group.ExportIngredientGroupCode = txtSaveAsGroupCodeExport.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveExportIngredientGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtSaveAsGroupCodeExport.Text = String.Empty;
				return;
			}

			//Save the ingredients			
			//AIMManager.ExportIngredientSaveAs((Int32)lueGroupCode.EditValue, error.RecordID);
			AIMManager.ExportIngredientSaveAs((Int32)lueGroupCodeExport.EditValue, error.RecordID);

			DataBindExportIngredientGroup();
			lueGroupCodeExport.EditValue = error.RecordID;
			txtSaveAsGroupCodeExport.Text = String.Empty;

			gpAddGroupCodeExport.Visible = false;
			gbGroupCodeExport.Visible = true;

			//11/20/23
			grpSaveGroupAsExport.Visible = false;
			btnSaveAsGroupExport.Visible = true;

			//filter grid for group code		
			DataBindExportIngredientByGroup();

			lueCFCIngredientCode.EditValue = 0;
			lueCFCIngredientName.EditValue = 0;

		}
		private void btnCancelSaveGroupAsExport_Click(object sender, EventArgs e)
		{
			txtSaveAsGroupCodeExport.Text = String.Empty;

			//11/20/23
			grpSaveGroupAsExport.Visible = false;
			btnSaveAsGroupExport.Visible = true;
		}
		private void btnAddIngredientExport_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueGroupCodeExport.EditValue == 0)
				{
					MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueGroupCodeExport.ShowPopup();
					return;
				}
				if ((Int32)lueCFCIngredientCode.EditValue == 0)
				{
					MessageBox.Show("Please select an Ingredient. ", "Warning...", MessageBoxButtons.OK);
					lueCFCIngredientCode.ShowPopup();
					return;
				}

				ExportIngredientInfo export = new ExportIngredientInfo();
				export.ExportIngredientID = 0;
				export.ExportIngredientGroupID = (Int32)lueGroupCodeExport.EditValue;
				export.CFCIngredientID = (Int32)lueCFCIngredientCode.EditValue;
				export.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				export.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveExportIngredient(ref export);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindExportIngredientByGroup();

				lueCFCIngredientCode.EditValue = 0;
				lueCFCIngredientName.EditValue = 0;
				lueCFCIngredientName.ShowPopup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewIngredientExport_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdIngredientExport, "Ingredient Export Groups");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAimMaterialCodeExport_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueCFCIngredientCode, ref lueCFCIngredientName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAimMaterialNameExport_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueCFCIngredientName, ref lueCFCIngredientCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdIngredientExportView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}
		private void repDeleteExportIngredient_Click(object sender, EventArgs e)
		{
			try
			{
				ExportIngredientInfo export = new ExportIngredientInfo();
				export = (ExportIngredientInfo)this.grdIngredientExportView.GetFocusedRow();
				export.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteExportIngredient(export.ExportIngredientID);

				//filter grid for group code		
				DataBindExportIngredientByGroup();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueGroupCodeExport_EditValueChanged(object sender, EventArgs e)
		{
			_loadingExportGroup = true;
			//populate parent dropdown
			List<ExportIngredientGroupInfo> group = new List<ExportIngredientGroupInfo>();
			group = AIMManager.GetExportIngredientGroup((Int32)lueGroupCodeExport.EditValue);
			if (group.Count > 0)
			{
				lueAimParentExportCode.EditValue = group[0].AIMMaterialID;
			}

			//filter grid for group code		
			DataBindExportIngredientByGroup();

			if ((Int32)lueGroupCodeExport.EditValue != 0)
			{
				//11/20/23
				//grpSaveGroupAsExport.Location = new Point(377, 9);									
				//grpSaveGroupAsExport.Visible = true;
				btnSaveAsGroupExport.Location = new Point(377, 25);
				btnSaveAsGroupExport.Visible = true;
			}
			else
			{
				//11/20/23
				//grpSaveGroupAsExport.Visible = false;
				btnSaveAsGroupExport.Visible = false;
			}
			lueCFCIngredientCode.Focus();
			_loadingExportGroup = false;
		}
		private void lueAimParentExport_Enter(object sender, EventArgs e)
		{
			lueAimParentExportCode.SelectAll();
		}
		private void lueAimParentExport_Click(object sender, EventArgs e)
		{
			lueAimParentExportCode.SelectAll();
		}
		private void lueAimMaterialCodeExport_Click(object sender, EventArgs e)
		{
			lueCFCIngredientCode.SelectAll();
		}
		private void lueAimMaterialCodeExport_Enter(object sender, EventArgs e)
		{
			lueCFCIngredientCode.SelectAll();
		}
		private void lueAimMaterialNameExport_Click(object sender, EventArgs e)
		{
			lueCFCIngredientName.SelectAll();
		}
		private void lueAimMaterialNameExport_Enter(object sender, EventArgs e)
		{
			lueCFCIngredientName.SelectAll();
		}
		private void lueGroupCodeExport_Click(object sender, EventArgs e)
		{
			lueGroupCodeExport.SelectAll();
		}
		private void lueGroupCodeExport_Enter(object sender, EventArgs e)
		{
			lueGroupCodeExport.SelectAll();
		}
		private void grdIngredientExportView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}
		private void lueAimParentExportCode_Click(object sender, EventArgs e)
		{
			lueAimParentExportCode.SelectAll();
		}
		private void lueAimParentExportCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (_loadingExportGroup == false)
				{
					//update group table
					AIMManager.UpdateExportIngredientGroupAIMMaterialID((Int32)lueGroupCodeExport.EditValue, (Int32)lueAimParentExportCode.EditValue, userGlobal.CFCLoginUser.CFCUserID);
				}

				SyncValueLue(ref lueAimParentExportCode, ref lueAimParentExportName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAimParentExportCode_Enter(object sender, EventArgs e)
		{
			lueAimParentExportCode.SelectAll();
		}
		private void lueAimParentExportName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAimParentExportName, ref lueAimParentExportCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAimParentExportName_Click(object sender, EventArgs e)
		{
			lueAimParentExportName.SelectAll();
		}
		private void lueAimParentExportName_Enter(object sender, EventArgs e)
		{
			lueAimParentExportName.SelectAll();
		}
		private void lueGroupCode_Click(object sender, EventArgs e)
		{
			lueGroupCode.SelectAll();
		}
		private void lueGroupCode_Enter(object sender, EventArgs e)
		{
			lueGroupCode.SelectAll();
		}
		//10/25/23
		private void grdIngredientExportView_ShownEditor(object sender, EventArgs e)
		{
			List<ExportIngredientGroupInfo> list = new List<ExportIngredientGroupInfo>();
			GridView view = (GridView)sender;
		}
		//11/20/23			
		private void btnSaveAsGroupExport_Click(object sender, EventArgs e)
		{
			grpSaveGroupAsExport.Location = new Point(377, 9);
			grpSaveGroupAsExport.Visible = true;
			btnSaveAsGroupExport.Visible = false;
		}
		#endregion

		//1/11/24
		#region PLANT EXPORT GROUPS --------------------------------------------------------------------------------------		1/10/24
		private void DataBindExportPlantByGroup()
		{
			List<ExportPlantInfo> export = new List<ExportPlantInfo>();
			export = AIMManager.GetExportPlant_GroupID((Int32)lueGroupCodeExportPlant.EditValue);
			Int32 currentRowNum = 0;
			if (grdPlantExportView != null)
			{
				Int32.TryParse(grdPlantExportView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdPlantExport.DataSource = export;
			grdPlantExportView.FocusedRowHandle = currentRowNum;
		}
		private void DataBindExportPlant()
		{
			List<CFCPlantInfo> cfcPlant = new List<CFCPlantInfo>();
			cfcPlant = AIMManager.GetCFCPlantAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
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

			lueCFCPlantCode.Properties.DataSource = cfcPlant;
			XtraEditors.LoadLookUpEdit(ref lueCFCPlantCode, "CFCPlantID", "CFCPlantCode", "CFCPlantName");
			lueCFCPlantName.Properties.DataSource = cfcPlant;
			XtraEditors.LoadLookUpEdit(ref lueCFCPlantName, "CFCPlantID", "CFCPlantName", "CFCPlantCode");

			List<ExportPlantInfo> export = new List<ExportPlantInfo>();
			export = AIMManager.GetExportPlant_GroupID((Int32)lueGroupCodeExportPlant.EditValue);

			Int32 currentRowNum = 0;
			if (grdPlantExportView != null)
			{
				Int32.TryParse(grdPlantExportView.FocusedRowHandle.ToString(), out currentRowNum);
			}
			grdPlantExport.DataSource = export;
			grdPlantExportView.FocusedRowHandle = currentRowNum;
		}
		private void DataBindExportPlantGroup()
		{
			//-------------------------------------------------------------
			//Group
			List<ExportPlantGroupInfo> group = new List<ExportPlantGroupInfo>();
			group = AIMManager.GetExportPlantGroup(true);
			group.Insert
				(
				0,
				new ExportPlantGroupInfo
				{
					ExportPlantGroupID = 0,
					ExportPlantGroupCode = "--Select--"
				}
				);
			lueGroupCodeExportPlant.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueGroupCodeExportPlant, "ExportPlantGroupID", "ExportPlantGroupCode");


			//1/25/24 - commented
			////-------------------------------------------------------------
			////Parent
			//List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
			//owner = AIMManager.GetAIMOwner(true);
			//owner.Insert
			//(
			//	0,
			//	new AIMOwnerInfo
			//	{
			//		AIMOwnerID = 0,
			//		OwnerCode = "--Select--"
			//	}
			//);

			//lueOwnerPlantGroup.Properties.DataSource = owner;
			//XtraEditors.LoadLookUpEdit(ref lueOwnerPlantGroup, "AIMOwnerID", "OwnerCode");
			//lueOwnerPlantGroup.Properties.Columns["OwnerCode"].Visible = true;
			//lueOwnerPlantGroup.Properties.Columns["OwnerName"].Visible = true;

			//if (owner.Count == 2)
			//{
			//	//selecting value if there is only one
			//	lueOwnerPlantGroup.EditValue = owner[1].AIMOwnerID;
			//}
			//else
			//{
			//	lueOwnerPlantGroup.EditValue = 0;
			//}


			if (group.Count > 1)
			{
				lueGroupCodeExportPlant.EditValue = group[1].ExportPlantGroupID;
			}
			else
			{
				lueGroupCodeExportPlant.EditValue = 0;
			}


		}
		//Task1041	MW250526
		private void DataBindTabOrderGroup()
		{
			//-------------------------------------------------------------
			//Group
			List<TabOrderGroupInfo> tabOrderGroups = new List<TabOrderGroupInfo>();
			List<TabOrderGroupDetailInfo> tabOrderGroupDetails = new List<TabOrderGroupDetailInfo>();
			Int32 tabOrderGroupID = 0;

			tabOrderGroups = AIMManager.GetTabOrderGroup();

			tabOrderGroups.Insert
				(
				0,
				new TabOrderGroupInfo
				{
					TabOrderGroupID = 0,
					TabOrderGroupCode = "--Select--"
				}
				);

			lueTabOrderGroup.Properties.DataSource = tabOrderGroups;

			XtraEditors.LoadLookUpEdit(ref lueTabOrderGroup, "TabOrderGroupID", "TabOrderGroupCode");



			if (tabOrderGroups.Count > 1)
			{
				lueTabOrderGroup.EditValue = tabOrderGroups[1].TabOrderGroupID;
				tabOrderGroupID = (Int32)lueTabOrderGroup.EditValue;
				DataBindTabOrderGroupDetail(tabOrderGroupID);
			}
			else
			{
				lueTabOrderGroup.EditValue = 0;
			}
		}


		//1/25/24 - commented
		//private void DataBindExportPlantlueOwnerLocation()
		//	{
		//	List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();			
		//	aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner((Int32)lueOwnerPlantGroup.EditValue);

		//	aimOwnerLocation.Insert
		//		(
		//		0,
		//		new AIMOwnerLocationInfo
		//		{
		//			AIMOwnerLocationID = 0,
		//			LocationCode = "--Select--",
		//			LocationName = "--Select--"
		//		}
		//		);

		//	lueOwnerLocationCodePlantGroup.Properties.DataSource = aimOwnerLocation;
		//	XtraEditors.LoadLookUpEdit(ref lueOwnerLocationCodePlantGroup, "AIMOwnerLocationID", "LocationCode", "LocationName");
		//	lueOwnerLocationNamePlantGroup.Properties.DataSource = aimOwnerLocation;
		//	XtraEditors.LoadLookUpEdit(ref lueOwnerLocationNamePlantGroup, "AIMOwnerLocationID", "LocationName", "LocationCode");

		//	//-------------------------------------------------------------

		//	if (aimOwnerLocation.Count == 2)
		//	{
		//		lueOwnerLocationCodePlantGroup.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
		//	}
		//	else
		//	{
		//		lueOwnerLocationCodePlantGroup.EditValue = 0;
		//	}

		//}
		private void btnAddNewGroupExportPlant_Click(object sender, EventArgs e)
		{
			gpAddGroupCodeExportPlant.Location = new Point(7, 9);
			gpAddGroupCodeExportPlant.Visible = true;
			lueGroupCodeExportPlant.EditValue = 0;

			gbExportPlant.Visible = false;
			btnAddPlantExport.Visible = false;
			btnPreviewPlantExport.Visible = false;

			txtGroupCodeExportPlant.Text = "";              //1/25/24
			txtGroupCodeExportPlant.Focus();
		}
		private void btnDeleteGroupExportPlant_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueGroupCodeExportPlant.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueGroupCodeExportPlant.ShowPopup();
				return;
			}
			name = lueGroupCodeExportPlant.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueGroupCodeExportPlant.EditValue;

				//Delete from ExportPlant table
				AIMManager.DeleteExportPlant_GroupID(groupID);

				//Delete from ExportPlantGroup table
				AIMManager.DeleteExportPlantGroup(groupID);

				DataBindExportPlantGroup();
				DataBindExportPlant();

			}
		}
		private void btnAddNewGroupCodeExportPlant_Click(object sender, EventArgs e)
		{
			if (txtGroupCodeExportPlant.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtGroupCodeExportPlant.Focus();
				return;
			}

			//1/25/24 - commented
			//if ((Int32)lueOwnerPlantGroup.EditValue == 0)
			//{
			//	MessageBox.Show("Please select an AIM Owner. ", "Warning...", MessageBoxButtons.OK);
			//	lueOwnerPlantGroup.Focus();
			//	lueOwnerPlantGroup.ShowPopup();
			//	return;
			//}
			//if ((Int32)lueOwnerLocationCodePlantGroup.EditValue == 0)
			//{
			//	MessageBox.Show("Please select an AIM Owner Location Code. ", "Warning...", MessageBoxButtons.OK);
			//	lueOwnerLocationCodePlantGroup.Focus();
			//	lueOwnerLocationCodePlantGroup.ShowPopup();
			//	return;
			//}

			//save group code
			ExportPlantGroupInfo group = new ExportPlantGroupInfo();
			group.ExportPlantGroupID = 0;
			group.ExportPlantGroupCode = txtGroupCodeExportPlant.Text;

			//1/25/24 - commented
			//group.AIMOwnerID = (Int32)lueOwnerPlantGroup.EditValue;
			//group.AIMOwnerLocationID = (Int32)lueOwnerLocationCodePlantGroup.EditValue;

			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveExportPlantGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtGroupCode.Text = String.Empty;
				return;
			}

			DataBindExportPlantGroup();
			lueGroupCodeExportPlant.EditValue = error.RecordID;

			gpAddGroupCodeExportPlant.Visible = false;
			gbGroupCodeExportPlant.Visible = true;

			gbExportPlant.Visible = true;
			btnAddPlantExport.Visible = true;
			btnPreviewPlantExport.Visible = true;


		}
		private void cmdCancelExportPlant_Click(object sender, EventArgs e)
		{
			gpAddGroupCodeExportPlant.Visible = false;
			gbGroupCodeExportPlant.Visible = true;
			gbExportPlant.Visible = true;
			btnAddPlantExport.Visible = true;
			btnPreviewPlantExport.Visible = true;

		}
		private void btnSaveGroupAsExportPlant_Click(object sender, EventArgs e)
		{
			if (txtSaveAsGroupCodeExportPlant.Text == String.Empty)
			{
				MessageBox.Show("Please enter a Group Code.", "Warning...", MessageBoxButtons.OK);
				txtSaveAsGroupCodeExportPlant.Focus();
				return;
			}

			//Save the group
			ExportPlantGroupInfo group = new ExportPlantGroupInfo();
			group.ExportPlantGroupID = 0;
			group.ExportPlantGroupCode = txtSaveAsGroupCodeExportPlant.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			group.IsActive = true;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveExportPlantGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtSaveAsGroupCodeExportPlant.Text = String.Empty;
				return;
			}

			//Save the plants						
			AIMManager.ExportPlantSaveAs((Int32)lueGroupCodeExportPlant.EditValue, error.RecordID);

			DataBindExportPlantGroup();
			lueGroupCodeExportPlant.EditValue = error.RecordID;
			txtSaveAsGroupCodeExportPlant.Text = String.Empty;

			gpAddGroupCodeExportPlant.Visible = false;
			gbGroupCodeExportPlant.Visible = true;

			grpSaveGroupAsExportPlant.Visible = false;
			btnSaveAsGroupExportPlant.Visible = true;

			//filter grid for group code		
			DataBindExportPlantByGroup();

			lueCFCPlantCode.EditValue = 0;
			lueCFCPlantName.EditValue = 0;

		}
		private void btnCancelSaveGroupAsExportPlant_Click(object sender, EventArgs e)
		{
			txtSaveAsGroupCodeExportPlant.Text = String.Empty;
			grpSaveGroupAsExportPlant.Visible = false;
			btnSaveAsGroupExportPlant.Visible = true;
		}
		private void btnAddPlantExport_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueGroupCodeExportPlant.EditValue == 0)
				{
					MessageBox.Show("Please select a Group Code. ", "Warning...", MessageBoxButtons.OK);
					lueGroupCodeExportPlant.ShowPopup();
					return;
				}
				if ((Int32)lueCFCPlantCode.EditValue == 0)
				{
					MessageBox.Show("Please select a Plant. ", "Warning...", MessageBoxButtons.OK);
					lueCFCPlantCode.ShowPopup();
					return;
				}

				ExportPlantInfo export = new ExportPlantInfo();
				export.ExportPlantID = 0;
				export.ExportPlantGroupID = (Int32)lueGroupCodeExportPlant.EditValue;
				export.CFCPlantID = (Int32)lueCFCPlantCode.EditValue;
				export.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				export.IsActive = true;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveExportPlant(ref export);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//filter grid for group code		
				DataBindExportPlantByGroup();

				lueCFCPlantCode.EditValue = 0;
				lueCFCPlantName.EditValue = 0;
				lueCFCPlantName.ShowPopup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewPlantExport_Click(object sender, EventArgs e)
		{
			try
			{
				CFCAIMCore.Global.previewGridResults(grdPlantExport, "Plant Export Groups");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdPlantExportView_RowStyle(object sender, RowStyleEventArgs e)
		{
			GridView view = sender as GridView;
			Boolean active = true;

			active = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, "IsActive"));
			if (active == false)
			{
				e.Appearance.BackColor = Color.PaleGoldenrod;
				e.HighPriority = true;
			}
		}
		private void repDeleteExportPlant_Click(object sender, EventArgs e)
		{
			try
			{
				ExportPlantInfo export = new ExportPlantInfo();
				export = (ExportPlantInfo)this.grdPlantExportView.GetFocusedRow();
				export.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteExportPlant(export.ExportPlantID);

				//filter grid for group code		
				DataBindExportPlantByGroup();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueGroupCodeExportPlant_EditValueChanged(object sender, EventArgs e)
		{
			_loadingExportGroup = true;

			//populate parent dropdowns

			//1/25/24 - commented
			//List<ExportPlantGroupInfo> group = new List<ExportPlantGroupInfo>();
			//group = AIMManager.GetExportPlantGroup((Int32)lueGroupCodeExportPlant.EditValue);
			//if (group.Count > 0)
			//{
			//	lueOwnerPlantGroup.EditValue = group[0].AIMOwnerID;
			//	lueOwnerLocationCodePlantGroup.EditValue = group[0].AIMOwnerLocationID;

			//}



			//filter grid for group code		
			DataBindExportPlantByGroup();

			if ((Int32)lueGroupCodeExportPlant.EditValue != 0)
			{
				btnSaveAsGroupExportPlant.Location = new Point(410, 25);
				btnSaveAsGroupExportPlant.Visible = true;
			}
			else
			{
				btnSaveAsGroupExportPlant.Visible = false;
			}
			lueCFCPlantCode.Focus();
			_loadingExportGroupPlant = false;
		}
		private void lueGroupCodeExportPlant_Click(object sender, EventArgs e)
		{
			lueGroupCodeExportPlant.SelectAll();
		}
		private void lueGroupCodeExportPlant_Enter(object sender, EventArgs e)
		{
			lueGroupCodeExportPlant.SelectAll();
		}
		private void grdPlantExportView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			AIMGlobal.focusedRowGreen(sender, e);
		}

		//1/25/24 - commented
		//private void lueOwnerLocationCodePlantGroup_Click(object sender, EventArgs e)
		//{
		//	lueOwnerLocationCodePlantGroup.SelectAll();
		//}

		//1/25/24 - commented
		//private void lueOwnerLocationCodePlantGroup_EditValueChanged(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		if (_loadingExportGroupPlant == false)
		//		{
		//			//update group table
		//			AIMManager.UpdateExportPlantGroupAIMOwnerLocationID((Int32)lueGroupCodeExportPlant.EditValue, (Int32)lueOwnerLocationCodePlantGroup.EditValue, userGlobal.CFCLoginUser.CFCUserID);
		//		}

		//		SyncValueLue(ref lueOwnerLocationCodePlantGroup, ref lueOwnerLocationNamePlantGroup);
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message);
		//	}
		//}

		//1/25/24 - commented
		//private void lueOwnerLocationCodePlantGroup_Enter(object sender, EventArgs e)
		//{
		//	lueOwnerLocationCodePlantGroup.SelectAll();
		//}

		//1/25/24 - commented
		//private void lueOwnerLocationNamePlantGroup_EditValueChanged(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		SyncValueLue(ref lueOwnerLocationNamePlantGroup, ref lueOwnerLocationCodePlantGroup);
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message);
		//	}
		//}

		//1/25/24 - commented
		//private void lueOwnerLocationNamePlantGroup_Click(object sender, EventArgs e)
		//{
		//	lueOwnerLocationNamePlantGroup.SelectAll();
		//}

		//1/25/24 - commented
		//private void lueOwnerLocationNamePlantGroup_Enter(object sender, EventArgs e)
		//{
		//	lueOwnerLocationNamePlantGroup.SelectAll();
		//}
		private void grdPlantExportView_ShownEditor(object sender, EventArgs e)
		{
			List<ExportPlantGroupInfo> list = new List<ExportPlantGroupInfo>();
			GridView view = (GridView)sender;
		}
		private void btnSaveAsGroupExportPlant_Click(object sender, EventArgs e)
		{
			grpSaveGroupAsExportPlant.Location = new Point(410, 9);
			grpSaveGroupAsExportPlant.Visible = true;
			btnSaveAsGroupExportPlant.Visible = false;
		}
		private void lueCFCPlantCode_Click(object sender, EventArgs e)
		{
			lueCFCPlantCode.SelectAll();
		}
		private void lueCFCPlantName_Click(object sender, EventArgs e)
		{
			lueCFCPlantName.SelectAll();
		}
		private void lueCFCPlantCode_Enter(object sender, EventArgs e)
		{
			lueCFCPlantCode.SelectAll();
		}
		private void lueCFCPlantName_Enter(object sender, EventArgs e)
		{
			lueCFCPlantName.SelectAll();
		}
		private void lueCFCPlantCode_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueCFCPlantCode, ref lueCFCPlantName);
		}
		private void lueCFCPlantName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueCFCPlantName, ref lueCFCPlantCode);
		}

		private void btnAddTabOrderGroup_Click(object sender, EventArgs e)
		{
			gbAddTabOrderGroup.Location = new Point(7, 9);
			gbAddTabOrderGroup.Visible = true;
			lueTabOrderGroup.EditValue = 0;

			gbTabOrderGroup.Visible = false;
			//btnAddPlantExport.Visible = false;
			//btnPreviewPlantExport.Visible = false;

			txtTabOrderGroupCode.Text = "";
			txtTabOrderGroupCode.Focus();
		}

		private void btnDeleteTabOrderGroup_Click(object sender, EventArgs e)
		{
			String name = string.Empty;
			Int32 groupID = 0;

			if ((Int32)lueTabOrderGroup.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueTabOrderGroup.ShowPopup();
				return;
			}
			name = lueTabOrderGroup.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{

				groupID = (Int32)lueTabOrderGroup.EditValue;

				//Delete from AIMTestTypeGroup table
				AIMManager.DeleteTabOrderGroup(groupID);

				DataBindTabOrderGroup();

			}
		}

		private void btnSaveTabOrderGroup_Click(object sender, EventArgs e)                 //Task1041 DB250528
		{
			if (txtTabOrderGroupCode.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtTabOrderGroupCode.Focus();
				return;
			}

			//save group code
			TabOrderGroupInfo group = new TabOrderGroupInfo();
			group.TabOrderGroupID = 0;
			group.TabOrderGroupCode = txtTabOrderGroupCode.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveTabOrderGroup(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				txtOwnerLocationGroupCode.Text = String.Empty;
				return;
			}

			error = AIMManager.SaveTabOrderDetail(error.RecordID, userGlobal.CFCLoginUser.CFCUserID);

			DataBindTabOrderGroup();
			lueTabOrderGroup.EditValue = error.RecordID;
			txtTabOrderGroupCode.Text = String.Empty;
			gbAddTabOrderGroup.Visible = false;
			gbTabOrderGroup.Visible = true;
		}

		private void btnCancelTabOrderGroup_Click(object sender, EventArgs e)
		{
			gbAddTabOrderGroup.Visible = false;
			gbTabOrderGroup.Visible = true;
			//gbExportPlant.Visible = true;
			//btnAddPlantExport.Visible = true;
			//            btnPreviewPlantExport.Visible = true;
		}

		private void lueTabOrderGroup_EditValueChanged(object sender, EventArgs e)
		{
			Int32 tabOrderGroupID = 0;
			try
			{
				if (lueTabOrderGroup.Text == "")
				{
					return;
				}

				tabOrderGroupID = (Int32)lueTabOrderGroup.EditValue;
				if (tabOrderGroupID > 0)
				{
					DataBindTabOrderGroupDetail(tabOrderGroupID);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueTabOrderGroup_Click(object sender, EventArgs e)
		{
			lueTabOrderGroup.SelectAll();
		}

		private void DataBindTabOrderGroupDetail(Int32 tabOrderGroupID)
		{
			List<TabOrderGroupDetailInfo> tabOrderGroupDetails = new List<TabOrderGroupDetailInfo>();

			tabOrderGroupDetails = AIMManager.GetTabOrderGroupDetail(tabOrderGroupID);
			grdTabOrder.DataSource = tabOrderGroupDetails;
		}

		private void grdTabOrderView_ValidateRow(object sender, ValidateRowEventArgs e)             //Task1041 DB250529
		{
			try
			{
				Int32 groupDetailID = 0;
				Int32 tabOrderGroupID = 0;
				ErrorInfo error = new ErrorInfo();
				tabOrderGroupID = (Int32)lueTabOrderGroup.EditValue;

				TabOrderGroupDetailInfo tabOrderData = new TabOrderGroupDetailInfo();
				tabOrderData = (TabOrderGroupDetailInfo)grdTabOrderView.GetFocusedRow();
				tabOrderData.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				error = AIMManager.SaveTabOrderDetail(tabOrderData.TabOrderGroupDetailID, tabOrderData.TabOrder, userGlobal.CFCLoginUser.CFCUserID);

				DataBindTabOrderGroupDetail(tabOrderGroupID);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}




		//1/25/24 - commented
		//private void lueOwnerPlantGroup_EditValueChanged(object sender, EventArgs e)
		//{
		//	DataBindExportPlantlueOwnerLocation();
		//}
		#endregion

		#region Sample Screen Display Group                 //Task1042 FG
		private void DataBindSampleScreenDisplayGroup()
		{
			List<SampleScreenDisplayGroupInfo> sampleScreenDisplayGroups = AIMManager.GetSampleScreenDisplay();

			sampleScreenDisplayGroups.Insert
				(
				0,
				new SampleScreenDisplayGroupInfo
				{
					SampleScreenDisplayGroupID = 0,
					SampleScreenDisplayGroupCode = "--Select--"
				}
				);

			lueSampleScreenDisplayGroup.Properties.DataSource = sampleScreenDisplayGroups;
			XtraEditors.LoadLookUpEdit(ref lueSampleScreenDisplayGroup, "SampleScreenDisplayGroupID", "SampleScreenDisplayGroupCode");   

			if (sampleScreenDisplayGroups.Count > 1)
			{
				lueSampleScreenDisplayGroup.EditValue = sampleScreenDisplayGroups[1].SampleScreenDisplayGroupID;
				DataBindSampleScreenDisplayDetail((Int32)lueSampleScreenDisplayGroup.EditValue);
			}
			else
			{
				lueSampleScreenDisplayGroup.EditValue = 0;
				grdSampleScreenDisplayDetail.DataSource = null;
			}
		}

		private void DataBindSampleScreenDisplayColumn()
		{
			//Columns that live on the Sample screen (same source used to show/hide fields there).
			List<AlternateNameInfo> sampleColumns = AIMManager.GetAlternateName((Int32)EnumFormID.Sample);

			//Task1042 FG - required columns always show on Sample; only optional columns belong in the picker.
			sampleColumns.RemoveAll(column => column.RequiredField == true);

			//cursor change 08/25/2026 - the fallback moved into AlternateNameInfo.FieldNameDisplay so it is not
			//                          duplicated here and on the Required Field tab, and so the entity is not modified
			//foreach (AlternateNameInfo column in sampleColumns)
			//{
			//	if (String.IsNullOrEmpty(column.AlternateName))
			//	{
			//		column.AlternateName = column.UserControlName;
			//	}
			//}

			sampleColumns.Insert
				(
				0,
				new AlternateNameInfo
				{
					ControlID = 0,
					AlternateName = "--Select--"
				}
				);

			lueSampleScreenDisplayColumn.Properties.DataSource = sampleColumns;
			//cursor change 08/25/2026 XtraEditors.LoadLookUpEdit(ref lueSampleScreenDisplayColumn, "ControlID", "AlternateName");
			XtraEditors.LoadLookUpEdit(ref lueSampleScreenDisplayColumn, "ControlID", "FieldNameDisplay");         //cursor change 08/25/2026
			lueSampleScreenDisplayColumn.EditValue = 0;
		}

		private void DataBindSampleScreenDisplayDetail(Int32 sampleScreenDisplayID)
		{
			List<SampleScreenDisplayGroupDetailInfo> details = AIMManager.GetSampleScreenDisplayDetail(sampleScreenDisplayID);
			grdSampleScreenDisplayDetail.DataSource = details;
		}

		private void lueSampleScreenDisplayGroup_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (lueSampleScreenDisplayGroup.Text == "")
				{
					return;
				}

				Int32 sampleScreenDisplayID = (Int32)lueSampleScreenDisplayGroup.EditValue;
				if (sampleScreenDisplayID > 0)
				{
					DataBindSampleScreenDisplayDetail(sampleScreenDisplayID);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddSampleScreenDisplayGroup_Click(object sender, EventArgs e)
		{
			gbAddSampleScreenDisplayGroup.Visible = true;
			txtSampleScreenDisplayGroupCode.Text = "";
			txtSampleScreenDisplayGroupCode.Focus();
		}

		private void btnSaveSampleScreenDisplayGroup_Click(object sender, EventArgs e)
		{
			if (txtSampleScreenDisplayGroupCode.Text == "")
			{
				MessageBox.Show("Please enter a Group Code. ", "Warning...", MessageBoxButtons.OK);
				txtSampleScreenDisplayGroupCode.Focus();
				return;
			}

			SampleScreenDisplayGroupInfo group = new SampleScreenDisplayGroupInfo();
			group.SampleScreenDisplayGroupID = 0;
			group.SampleScreenDisplayGroupCode = txtSampleScreenDisplayGroupCode.Text;
			group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

			ErrorInfo error = AIMManager.SaveSampleScreenDisplay(ref group);
			if (error.ErrorNumber > 0)
			{
				MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
				return;
			}

			DataBindSampleScreenDisplayGroup();
			lueSampleScreenDisplayGroup.EditValue = error.RecordID;
			txtSampleScreenDisplayGroupCode.Text = String.Empty;
			gbAddSampleScreenDisplayGroup.Visible = false;
		}

		private void btnCancelSampleScreenDisplayGroup_Click(object sender, EventArgs e)
		{
			gbAddSampleScreenDisplayGroup.Visible = false;
		}

		private void btnDeleteSampleScreenDisplayGroup_Click(object sender, EventArgs e)
		{
			if ((Int32)lueSampleScreenDisplayGroup.EditValue == 0)
			{
				MessageBox.Show("Please select a Group to delete.");
				lueSampleScreenDisplayGroup.ShowPopup();
				return;
			}

			String name = lueSampleScreenDisplayGroup.Text;
			DialogResult dialogResult = MessageBox.Show("Delete Group Code '" + name + "'?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{
				Int32 sampleScreenDisplayID = (Int32)lueSampleScreenDisplayGroup.EditValue;
				AIMManager.DeleteSampleScreenDisplay(sampleScreenDisplayID);
				DataBindSampleScreenDisplayGroup();
			}
		}
		//FG task 1042
        private void btnSaveToGrid_Click(object sender, EventArgs e)
        {
            if ((Int32)lueSampleScreenDisplayGroup.EditValue == 0)
            {
                MessageBox.Show("Please select a Group Code first.");
                lueSampleScreenDisplayGroup.ShowPopup();
                return;
            }
            if (lueSampleScreenDisplayColumn.EditValue == null || (Int32)lueSampleScreenDisplayColumn.EditValue == 0)
            {
                MessageBox.Show("Please select a Column to add.");
                lueSampleScreenDisplayColumn.ShowPopup();
                return;
            }

            Int32 sampleScreenDisplayID = (Int32)lueSampleScreenDisplayGroup.EditValue;
            Int32 controlID = (Int32)lueSampleScreenDisplayColumn.EditValue;

            List<AlternateNameInfo> selectedColumn = AIMManager.GetAlternateName((Int32)EnumFormID.Sample, controlID);
            if (selectedColumn.Count > 0 && selectedColumn[0].RequiredField == true)
            {
                MessageBox.Show("Required columns cannot be added to a display group.", "Warning...", MessageBoxButtons.OK);
                return;
            }

            ErrorInfo error = AIMManager.SaveSampleScreenDisplayDetail(sampleScreenDisplayID, controlID, userGlobal.CFCLoginUser.CFCUserID);
            if (error.ErrorNumber > 0)
            {
                MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
                return;
            }

            DataBindSampleScreenDisplayDetail(sampleScreenDisplayID);
            lueSampleScreenDisplayColumn.EditValue = 0;
        }

        private void btnDeleteSampleScreenDisplayColumn_Click(object sender, EventArgs e)                //Task1042 FG
		{
			SampleScreenDisplayGroupDetailInfo detail = (SampleScreenDisplayGroupDetailInfo)grdSampleScreenDisplayDetailView.GetFocusedRow();
			if (detail == null)
			{
				MessageBox.Show("Please select a column in the grid to remove.");
				return;
			}

			DialogResult dialogResult = MessageBox.Show("Remove column '" + detail.UserControlName + "' from this group?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
			if (dialogResult == DialogResult.Yes)
			{
				AIMManager.DeleteSampleScreenDisplayDetail(detail.SampleScreenDisplayGroupDetailID);
				DataBindSampleScreenDisplayDetail((Int32)lueSampleScreenDisplayGroup.EditValue);
			}
		}

		private void repbtnDeleteSampleScreenDisplayDetail_Click(object sender, EventArgs e)             //Task1042 FG
		{
			try
			{
				SampleScreenDisplayGroupDetailInfo detail = (SampleScreenDisplayGroupDetailInfo)grdSampleScreenDisplayDetailView.GetFocusedRow();
				if (detail == null)
				{
					return;
				}

				DialogResult dialogResult = MessageBox.Show("Remove column '" + detail.UserControlName + "' from this group?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dialogResult == DialogResult.Yes)
				{
					AIMManager.DeleteSampleScreenDisplayDetail(detail.SampleScreenDisplayGroupDetailID);
					DataBindSampleScreenDisplayDetail((Int32)lueSampleScreenDisplayGroup.EditValue);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		// task 1155 FG 8/5/26
		#region REQUIRED FIELD -----------------------------------------------------------------------------------
		//A required field group works like the other group tabs: pick or create a group code, save the
		//group's scope (an ingredient group OR a product group - never both - plus an optional owner
		//location group), then build the list of Sample screen fields that are required for that scope.
		//Nothing picked for scope = the group's fields are required for every material at every location.

		List<SampleRequiredFieldGroupInfo> _requiredFieldGroups = new List<SampleRequiredFieldGroupInfo>();
		Boolean _loadingRequiredFieldGroup = false;     //true while a group selection loads the scope dropdowns

		private void DataBindRequiredFieldGroup()
		{
			_requiredFieldGroups = AIMManager.GetSampleRequiredFieldGroup(true);

			List<SampleRequiredFieldGroupInfo> group = new List<SampleRequiredFieldGroupInfo>(_requiredFieldGroups);
			group.Insert
				(
				0,
				new SampleRequiredFieldGroupInfo
				{
					SampleRequiredFieldGroupID = 0,
					SampleRequiredFieldGroupCode = "--Select--"
				}
				);

			lueRequiredFieldGroup.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueRequiredFieldGroup, "SampleRequiredFieldGroupID", "SampleRequiredFieldGroupCode");
			lueRequiredFieldGroup.EditValue = 0;
		}

		private void DataBindRequiredFieldColumn()
		{
			//Same list the Sample Screen Display picker uses, so the admin sees the customer's own captions.
			List<AlternateNameInfo> sampleColumns = AIMManager.GetAlternateName((Int32)EnumFormID.Sample);

			//Columns already flagged as required on the Sample screen are validated there, so leave them out.
			sampleColumns.RemoveAll(column => column.RequiredField == true);

			//cursor change 08/25/2026 - the fallback moved into AlternateNameInfo.FieldNameDisplay so it is not
			//                          duplicated here and on the Sample Screen Display tab
			//foreach (AlternateNameInfo column in sampleColumns)
			//{
			//	if (String.IsNullOrEmpty(column.AlternateName))
			//	{
			//		column.AlternateName = column.UserControlName;
			//	}
			//}

			sampleColumns.Insert
				(
				0,
				new AlternateNameInfo
				{
					ControlID = 0,
					AlternateName = "--Select--"
				}
				);

			lueRequiredFieldColumn.Properties.DataSource = sampleColumns;
			//cursor change 08/25/2026 XtraEditors.LoadLookUpEdit(ref lueRequiredFieldColumn, "ControlID", "AlternateName");
			XtraEditors.LoadLookUpEdit(ref lueRequiredFieldColumn, "ControlID", "FieldNameDisplay");               //cursor change 08/25/2026
			lueRequiredFieldColumn.EditValue = 0;
		}

		private void DataBindRequiredFieldIngredientGroup()
		{
			List<SpecificIngredientsGroupInfo> group = AIMManager.GetSpecificIngredientsGroup(true);
			group.Insert
				(
				0,
				new SpecificIngredientsGroupInfo
				{
					SpecificIngredientsGroupID = 0,
					SpecificIngredientsGroupCode = "--Select--"
				}
				);

			lueRequiredFieldIngredientGroup.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueRequiredFieldIngredientGroup, "SpecificIngredientsGroupID", "SpecificIngredientsGroupCode");
			lueRequiredFieldIngredientGroup.EditValue = 0;
		}

		private void DataBindRequiredFieldProductGroup()
		{
			List<SpecificProductsGroupInfo> group = AIMManager.GetSpecificProductsGroup(true);
			group.Insert
				(
				0,
				new SpecificProductsGroupInfo
				{
					SpecificProductsGroupID = 0,
					SpecificProductsGroupCode = "--Select--"
				}
				);

			lueRequiredFieldProductGroup.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueRequiredFieldProductGroup, "SpecificProductsGroupID", "SpecificProductsGroupCode");
			lueRequiredFieldProductGroup.EditValue = 0;
		}

		private void DataBindRequiredFieldLocationGroup()
		{
			List<AIMOwnerLocationGroupInfo> group = AIMManager.GetOwnerLocationGroup(true);
			group.Insert
				(
				0,
				new AIMOwnerLocationGroupInfo
				{
					AIMOwnerLocationGroupID = 0,
					AIMOwnerLocationGroupCode = "--Select--"
				}
				);

			lueRequiredFieldLocationGroup.Properties.DataSource = group;
			XtraEditors.LoadLookUpEdit(ref lueRequiredFieldLocationGroup, "AIMOwnerLocationGroupID", "AIMOwnerLocationGroupCode");
			lueRequiredFieldLocationGroup.EditValue = 0;
		}

		private void DataBindRequiredFieldDetail()
		{
			Int32 groupID = GetSelectedRequiredFieldGroupID();
			if (groupID == 0)
			{
				grdRequiredFieldDetail.DataSource = null;
				return;
			}
			List<SampleRequiredFieldGroupDetailInfo> details = AIMManager.GetSampleRequiredFieldGroupDetail(groupID);
			grdRequiredFieldDetail.DataSource = details;
		}

		private Int32 GetSelectedRequiredFieldGroupID()
		{
			Int32 groupID = 0;
			if (lueRequiredFieldGroup.EditValue != null)
			{
				Int32.TryParse(lueRequiredFieldGroup.EditValue.ToString(), out groupID);
			}
			return groupID;
		}

		private void lueRequiredFieldGroup_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				Int32 groupID = GetSelectedRequiredFieldGroupID();

				//while the stored scope loads, keep the mutual-exclusion handlers from clearing it
				_loadingRequiredFieldGroup = true;

				if (groupID == 0)
				{
					lueRequiredFieldIngredientGroup.EditValue = 0;
					lueRequiredFieldProductGroup.EditValue = 0;
					lueRequiredFieldLocationGroup.EditValue = 0;
					grdRequiredFieldDetail.DataSource = null;
				}
				else
				{
					SampleRequiredFieldGroupInfo group = _requiredFieldGroups.Find(g => g.SampleRequiredFieldGroupID == groupID);
					if (group != null)
					{
						lueRequiredFieldIngredientGroup.EditValue = group.SpecificIngredientsGroupID;
						lueRequiredFieldProductGroup.EditValue = group.SpecificProductsGroupID;
						lueRequiredFieldLocationGroup.EditValue = group.AIMOwnerLocationGroupID;
					}
					DataBindRequiredFieldDetail();
				}

				//the scope and field sections only make sense with a group selected
				gbRequiredFieldSettings.Enabled = groupID != 0;
				gbRequiredFieldAdd.Enabled = groupID != 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				_loadingRequiredFieldGroup = false;
			}
		}

		//a material is either an ingredient or a product, a group cannot use both scopes at the same time
		private void lueRequiredFieldIngredientGroup_EditValueChanged(object sender, EventArgs e)
		{
			if (_loadingRequiredFieldGroup)
			{
				return;
			}
			if (lueRequiredFieldIngredientGroup.EditValue != null && (Int32)lueRequiredFieldIngredientGroup.EditValue != 0)
			{
				lueRequiredFieldProductGroup.EditValue = 0;
			}
		}

		private void lueRequiredFieldProductGroup_EditValueChanged(object sender, EventArgs e)
		{
			if (_loadingRequiredFieldGroup)
			{
				return;
			}
			if (lueRequiredFieldProductGroup.EditValue != null && (Int32)lueRequiredFieldProductGroup.EditValue != 0)
			{
				lueRequiredFieldIngredientGroup.EditValue = 0;
			}
		}

		private void btnAddRequiredFieldGroup_Click(object sender, EventArgs e)
		{
			txtRequiredFieldGroupCode.Text = String.Empty;
			gbAddRequiredFieldGroup.Visible = true;
			txtRequiredFieldGroupCode.Focus();
		}

		private void btnCancelRequiredFieldGroup_Click(object sender, EventArgs e)
		{
			txtRequiredFieldGroupCode.Text = String.Empty;
			gbAddRequiredFieldGroup.Visible = false;
		}

		private void btnSaveRequiredFieldGroup_Click(object sender, EventArgs e)
		{
			try
			{
				String groupCode = txtRequiredFieldGroupCode.Text.Trim();
				if (groupCode == String.Empty)
				{
					MessageBox.Show("Please enter a group code.", "Warning...", MessageBoxButtons.OK);
					txtRequiredFieldGroupCode.Focus();
					return;
				}

				//group codes are unique (also enforced by the proc and a unique constraint)
				if (_requiredFieldGroups.Exists(g => String.Equals(g.SampleRequiredFieldGroupCode, groupCode, StringComparison.OrdinalIgnoreCase)))
				{
					MessageBox.Show("Group code '" + groupCode + "' already exists.", "Warning...", MessageBoxButtons.OK);
					txtRequiredFieldGroupCode.Focus();
					return;
				}

				SampleRequiredFieldGroupInfo requiredFieldGroup = new SampleRequiredFieldGroupInfo();
				requiredFieldGroup.SampleRequiredFieldGroupID = 0;
				requiredFieldGroup.SampleRequiredFieldGroupCode = groupCode;
				requiredFieldGroup.SpecificIngredientsGroupID = 0;
				requiredFieldGroup.SpecificProductsGroupID = 0;
				requiredFieldGroup.AIMOwnerLocationGroupID = 0;
				requiredFieldGroup.IsActive = true;
				requiredFieldGroup.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = AIMManager.SaveSampleRequiredFieldGroup(ref requiredFieldGroup);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				txtRequiredFieldGroupCode.Text = String.Empty;
				gbAddRequiredFieldGroup.Visible = false;

				//reload and select the new group so the user can set scope and start adding fields
				DataBindRequiredFieldGroup();
				lueRequiredFieldGroup.EditValue = error.RecordID;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnDeleteRequiredFieldGroup_Click(object sender, EventArgs e)
		{
			try
			{
				Int32 groupID = GetSelectedRequiredFieldGroupID();
				if (groupID == 0)
				{
					MessageBox.Show("Please select a group to delete.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				DialogResult dialogResult = MessageBox.Show(
					"Delete group '" + lueRequiredFieldGroup.Text + "' and all required fields under it?",
					"Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dialogResult == DialogResult.Yes)
				{
					AIMManager.DeleteSampleRequiredFieldGroup(groupID);
					DataBindRequiredFieldGroup();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSaveRequiredFieldSettings_Click(object sender, EventArgs e)
		{
			try
			{
				Int32 groupID = GetSelectedRequiredFieldGroupID();
				if (groupID == 0)
				{
					MessageBox.Show("Please select a group first.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				//0 = not scoped: no material group means all materials, no location group means all locations
				Int32 ingredientGroupID = 0;
				if (lueRequiredFieldIngredientGroup.EditValue != null)
				{
					ingredientGroupID = (Int32)lueRequiredFieldIngredientGroup.EditValue;
				}
				Int32 productGroupID = 0;
				if (lueRequiredFieldProductGroup.EditValue != null)
				{
					productGroupID = (Int32)lueRequiredFieldProductGroup.EditValue;
				}
				Int32 locationGroupID = 0;
				if (lueRequiredFieldLocationGroup.EditValue != null)
				{
					locationGroupID = (Int32)lueRequiredFieldLocationGroup.EditValue;
				}

				//the dropdowns already clear each other, this is just a safety net
				if (ingredientGroupID != 0 && productGroupID != 0)
				{
					MessageBox.Show("Please pick an Ingredient Group or a Product Group, not both.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				SampleRequiredFieldGroupInfo requiredFieldGroup = new SampleRequiredFieldGroupInfo();
				requiredFieldGroup.SampleRequiredFieldGroupID = groupID;
				requiredFieldGroup.SampleRequiredFieldGroupCode = lueRequiredFieldGroup.Text;
				requiredFieldGroup.SpecificIngredientsGroupID = ingredientGroupID;
				requiredFieldGroup.SpecificProductsGroupID = productGroupID;
				requiredFieldGroup.AIMOwnerLocationGroupID = locationGroupID;
				requiredFieldGroup.IsActive = true;
				requiredFieldGroup.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = AIMManager.SaveSampleRequiredFieldGroup(ref requiredFieldGroup);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				//refresh the cached scope so re-selecting this group shows the saved values
				DataBindRequiredFieldGroup();
				lueRequiredFieldGroup.EditValue = groupID;

				MessageBox.Show("Group settings saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnSaveRequiredFieldDetail_Click(object sender, EventArgs e)
		{
			try
			{
				Int32 groupID = GetSelectedRequiredFieldGroupID();
				if (groupID == 0)
				{
					MessageBox.Show("Please select a group first.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				if (lueRequiredFieldColumn.EditValue == null || (Int32)lueRequiredFieldColumn.EditValue == 0)
				{
					MessageBox.Show("Please select the field to make required.");
					lueRequiredFieldColumn.ShowPopup();
					return;
				}

				Int32 requiredControlID = (Int32)lueRequiredFieldColumn.EditValue;

				List<SampleRequiredFieldGroupDetailInfo> existingDetails = AIMManager.GetSampleRequiredFieldGroupDetail(groupID);
				if (existingDetails.Exists(d => d.RequiredControlID == requiredControlID))
				{
					MessageBox.Show("This field is already in the group.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				SampleRequiredFieldGroupDetailInfo requiredFieldGroupDetail = new SampleRequiredFieldGroupDetailInfo();
				requiredFieldGroupDetail.SampleRequiredFieldGroupDetailID = 0;
				requiredFieldGroupDetail.SampleRequiredFieldGroupID = groupID;
				requiredFieldGroupDetail.RequiredControlID = requiredControlID;
				requiredFieldGroupDetail.IsActive = true;
				requiredFieldGroupDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = AIMManager.SaveSampleRequiredFieldGroupDetail(ref requiredFieldGroupDetail);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				DataBindRequiredFieldDetail();
				lueRequiredFieldColumn.EditValue = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDeleteRequiredFieldDetail_Click(object sender, EventArgs e)
		{
			try
			{
				SampleRequiredFieldGroupDetailInfo requiredFieldGroupDetail = (SampleRequiredFieldGroupDetailInfo)grdRequiredFieldDetailView.GetFocusedRow();
				if (requiredFieldGroupDetail == null)
				{
					return;
				}

				//removes the single field from the group; the trash can on the header deletes the whole group
				//cursor change 08/25/2026 - name the field the same way the grid does, so the prompt matches the row clicked
				DialogResult dialogResult = MessageBox.Show(
					//cursor change 08/25/2026 "Remove required field '" + requiredFieldGroupDetail.FieldDisplayName + "' from this group?",
					"Remove required field '" + requiredFieldGroupDetail.FieldNameDisplay + "' from this group?",
					"Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dialogResult == DialogResult.Yes)
				{
					AIMManager.DeleteSampleRequiredFieldGroupDetail(requiredFieldGroupDetail.SampleRequiredFieldGroupDetailID);
					DataBindRequiredFieldDetail();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		//Task1061 DB251027
		#region NUTRIENT GROUPS ----------------------------------------------------------------------------------

		private void DataBindNutrientGroupAdd()                                                 //Task1061 DB251028
		{
			List<AIMNutrientInfo> AIMNutrient = new List<AIMNutrientInfo>();
			AIMNutrient = AIMManager.GetAIMNutrient(true);

			if (AIMNutrient != null)
			{
				lueNameNutrient.Properties.DataSource = AIMNutrient;
				XtraEditors.LoadLookUpEdit(ref lueNameNutrient, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

				//task 1133 FG 7/21/2026 - inside the Nutrient Name dropdown, show the Name column first, then the Code column
				lueNameNutrient.Properties.Columns.Clear();
				lueNameNutrient.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMNutrientName", "Nutrient Name"));
				lueNameNutrient.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMNutrientCode", "Nutrient Code"));

				//task 1133 FG 7/21/2026 - bind the Nutrient Code dropdown to the same list as the Nutrient Name dropdown
				lueCodeNutrient.Properties.DataSource = AIMNutrient;
				XtraEditors.LoadLookUpEdit(ref lueCodeNutrient, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
			}
		}

		private void DataBindNutrientGroup()                                                    //Task1061 DB251028
		{

			List<NutrientGroupInfo> nutrientGroup = new List<NutrientGroupInfo>();

			nutrientGroup = AIMManager.GetNutrientGroupList();
			nutrientGroup.Insert
			(
				0,
				new NutrientGroupInfo
				{
					NutrientGroupID = 0,
					NutrientGroupName = "--Select--"
				}
			);
			lueNutrientGroup.Properties.DataSource = nutrientGroup;
			XtraEditors.LoadLookUpEdit(ref lueNutrientGroup, "NutrientGroupID", "NutrientGroupName");
		}

		private void DataBindNutrientGroupDetail(Int32 nutrientGroupID)                     //Task1061 DB251028 
		{
			List<NutrientGroupDetailInfo> nutrientGroupDetail = new List<NutrientGroupDetailInfo>();

			nutrientGroupDetail = AIMManager.GetNutrientGroupDetail(nutrientGroupID);
			grdNutrientGroup.DataSource = nutrientGroupDetail;
		}
		private void btnNewNutrientGroup_Click(object sender, EventArgs e)                  //Task1061 DB251027
		{
			grbNutrientGroup.Visible = false;
			grbNutrientGroupNew.Visible = true;
			gbNutrient.Visible = false;
			grdNutrientGroup.Visible = false;
		}

		private void btnSaveNutrientGroup_Click(object sender, EventArgs e)                     //Task1061 DB251028
		{
			List<NutrientGroupInfo> nutrientGroup = new List<NutrientGroupInfo>();
			String nutrientGroupName = String.Empty;
			Int32 nutrientGroupID = 0;

			nutrientGroupName = txtNewNutrientGroup.Text;

			nutrientGroup = AIMManager.GetNutrientGroup(nutrientGroupName);
			if (nutrientGroup.Count > 0)
			{
				MessageBox.Show("Nutrient Group with name '" + nutrientGroupName + " already exists. Please use unique name.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			else
			{
				NutrientGroupInfo nutrientGroupNew = new NutrientGroupInfo();
				nutrientGroupNew.NutrientGroupID = 0;
				nutrientGroupNew.IsActive = true;
				nutrientGroupNew.NutrientGroupName = nutrientGroupName;
				nutrientGroupNew.UpdateStamp = DateTime.Now;
				nutrientGroupNew.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo result = AIMManager.SaveNutrientGroup(ref nutrientGroupNew);

				if (result.ErrorNumber > 0)
				{
					MessageBox.Show(result.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				DataBindNutrientGroup();
				lueNutrientGroup.EditValue = result.RecordID;
				nutrientGroupID = result.RecordID;
				DataBindNutrientGroupDetail(nutrientGroupID);
				grbNutrientGroupNew.Visible = false;
				grbNutrientGroup.Visible = true;
			}

		}

		private void btnAddNutrientToList_Click(object sender, EventArgs e)                 //Task1061 DB251028
		{
			Int32 nutrientGroupID = 0;

			try
			{
				if ((Int32)lueNameNutrient.EditValue == 0)
				{
					MessageBox.Show("Please select a Nutrient. ", "Warning...", MessageBoxButtons.OK);
					lueNameNutrient.ShowPopup();
					return;
				}
				nutrientGroupID = (Int32)lueNutrientGroup.EditValue;

				NutrientGroupDetailInfo nutrientGroupDetail = new NutrientGroupDetailInfo();
				nutrientGroupDetail.NutrientGroupDetailID = 0;
				nutrientGroupDetail.NutrientGroupID = nutrientGroupID;
				nutrientGroupDetail.AIMNutrientID = (Int32)lueNameNutrient.EditValue;
				nutrientGroupDetail.SortOrder = 0;
				nutrientGroupDetail.UpdateStamp = DateTime.Now;
				nutrientGroupDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.UpdateNutrientGroupDetail(nutrientGroupDetail);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Error...", MessageBoxButtons.OK);
					return;
				}

				DataBindNutrientGroupDetail(nutrientGroupID);

				lueNameNutrient.EditValue = 0;
				lueNameNutrient.ShowPopup();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueNutrientGroup_EditValueChanged(object sender, EventArgs e)              //Task1061 DB251028
		{
			if (lueNutrientGroup.EditValue != null)
			{
				if ((Int32)lueNutrientGroup.EditValue > 0)
				{
					DataBindNutrientGroupDetail((Int32)lueNutrientGroup.EditValue);
					gbNutrient.Visible = true;
					grdNutrientGroup.Visible = true;
				}
			}
		}

		//task 1133 FG 7/21/2026 - keep the Nutrient Code and Nutrient Name dropdowns in sync
		private void lueCodeNutrient_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueCodeNutrient, ref lueNameNutrient);
		}
		private void lueNameNutrient_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueNameNutrient, ref lueCodeNutrient);
		}
		private void lueCodeNutrient_Click(object sender, EventArgs e)
		{
			lueCodeNutrient.SelectAll();
		}
		private void lueCodeNutrient_Enter(object sender, EventArgs e)
		{
			lueCodeNutrient.SelectAll();
		}

		private void btnCancelNewNutrientGroup_Click(object sender, EventArgs e)                //Task1061 DB251028
		{
			grbNutrientGroupNew.Visible = false;
			grbNutrientGroup.Visible = true;

			if ((Int32)lueNutrientGroup.EditValue > 0)
			{
				gbNutrient.Visible = true;
				grdNutrientGroup.Visible = true;
			}
		}

		private void grdNutrientGroupView_ValidateRow(object sender, ValidateRowEventArgs e)                //Task1061 DB251028
		{
			try
			{
				Int32 nutrientGroupID = 0;
				ErrorInfo error = new ErrorInfo();
				nutrientGroupID = (Int32)lueNutrientGroup.EditValue;

				NutrientGroupDetailInfo nutrientGroupDetail = new NutrientGroupDetailInfo();
				nutrientGroupDetail = (NutrientGroupDetailInfo)grdNutrientGroupView.GetFocusedRow();
				nutrientGroupDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				error = AIMManager.UpdateNutrientGroupDetail(nutrientGroupDetail);

				DataBindNutrientGroupDetail(nutrientGroupID);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void repbtnDeleteNutrientFromGroup_Click(object sender, EventArgs e)                //Task1061 DB251028
		{
			try
			{
				NutrientGroupDetailInfo group = new NutrientGroupDetailInfo();
				group = (NutrientGroupDetailInfo)grdNutrientGroupView.GetFocusedRow();
				group.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteNutrientGroupDetail(group.NutrientGroupDetailID);
				DataBindNutrientGroupDetail((Int32)lueNutrientGroup.EditValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnDeleteNutrientGroup_Click(object sender, EventArgs e)                       //Task1061 DB251028
		{
			try
			{
				if (lueNutrientGroup.EditValue != null)
				{
					AIMManager.DeleteNutrientGroup((Int32)lueNutrientGroup.EditValue);
					gbNutrient.Visible = false;
					grdNutrientGroup.Visible = false;
					DataBindNutrientGroup();
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//Task1137 DB251209
		//		   Note: Using the FormClosed function to perform cleanup. Previously was using Leave function which doesn't always fire when closing form
		private void AIM_FormClosed(object sender, FormClosedEventArgs e)
		{
			AIMGlobal.aimMaterialTab = false;
			AIMGlobal.aimMaterialStockStatusTab = false;
			AIMGlobal.aimNutrientTab = false;
			AIMGlobal.aimOwnerTab = false;
			AIMGlobal.aimOwnerLocationTab = false;
			AIMGlobal.aimSupplierTab = false;
			AIMGlobal.aimSupplierLocationTab = false;
			AIMGlobal.aimSupplierGuaranteesTab = false;
			AIMGlobal.aimUnitTypeTab = false;
			AIMGlobal.aimWarningLimitsTab = false;
			AIMGlobal.aimOwnerLocationGroupTab = false;
			AIMGlobal.aimSpecificIngredientsTab = false;
			AIMGlobal.aimSpecificProductsTab = false;
			AIMGlobal.aimSupplierGroupTab = false;
			AIMGlobal.aimSupplierLocationGroupTab = false;
			AIMGlobal.testTypeTab = false;
			AIMGlobal.testTypeGroupTab = false;
			AIMGlobal.analyzerPlantGroupTab = false;
			AIMGlobal.nutrientGroupTab = false;

		}
        #endregion

        //---------------------------------------------------------------
        //LIST OF TODO's: -- 2/28/20 -- This stuff was started, or is on the list, but stopped because of SQL conversion:        
        //Analyzer Cross Reference - add material-type to AIM Material dropdowns - add use as filter (just like on AIM Maintenance screen)
        //Analyzer Cross Reference - add material-type to grid(cannot be modified)
        //
        //Maintenance screens and ANYWHERE there is a GRID (like reports) - add custom sort (like the one above - grdNutrientView_CustomColumnSort - to any alphanumeric columns
        //      that need to be sorted correctly.
        //
        //Chart Supplier Comparison - after user selects material and nutrient, populate suppliers with just those that have material/nutrient selected(and date-range),
        //      so you're not looking at ALL the suppliers/supplier-locations, just ones with that material and nutrient
        //		
        //Sample Screen - initially, when user first starts to use AIM, there may be no materials and/or cross-references set up.This was the case with Sanderson Farms.
        //      so, on sample screen, if Source Filter says "AllSources" and there are no ingredients or products, 
        //          need to show hyperlink "Material", which user will click on to add materials.
        //          But...the materials still won't show in the dropdown unless they're cross-referenced so, when user returns to the sample screen,
        //              after entering materials, hide the "Material" hyperlink but show "Material Cross-Ref" hyperlink -- which will direct user
        //            to the cross-ref screen.After user has enter cross-references and returns back to Sample screen, hide the "Material Cross-Reference" hyperlink.
        //
        //Main Menu, under Utilities - added "Upload Database" and "Restore Database"
        //        Mark is doing the Upload and Download of the Zipped file.
        //        Hard-coded directories -- needs to be changed.
        //        Need to add code to keep 2 backup's.
        //        Mark needs to check my code
        //
        //Sample Comparison Report - fix sort; change "Sample ID" to "Sample Code"; add owner-location column.
        //Sample Activity Report - add options to sort sample code, oldest to newest, vice versa; on the grid, check "Allow to Grow" property or add horizontal scroll bar,
        //                         because, when Andy tried to resize a column, every column would change size slightly.  He was trying to size columns prior to printing.
        //User-Defined Sample Data - create report for just the header info.  The current report includes all the nutrient/results.  Need report with just header data.
        //                          Either add new report or add option -- to run for header and detail -- or just header.
        //		
        //Smithfield - Hernan Alejandro Cordova wants to see alerts/notifications -- Mark will talk to Hernan about this.



        //Task1121 DB251218 Start
        #region Test

        //Task1121 DB251218 Start
        private void grdTestView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            TestInfo test = new TestInfo();
            test = (TestInfo)grdTestView.GetFocusedRow();
            test.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveTest(ref test);
            DataBindTest();
        }
        private void DataBindTest()                 //Task1121 DB251218 
		{
			List<TestInfo> test = AIMManager.GetTest();            //Task1061 DB251022
			grdTest.DataSource = test;

		}

		private void DataBindlueDataSourceTypeTest()
		{
			List<DataSourceTypeInfo> dataSourceType = new List<DataSourceTypeInfo>();
			dataSourceType = AIMManager.GetDataSourceType(true);
			dataSourceType.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--Select--"
				}
			);
			lueTestDataSource.Properties.DataSource = dataSourceType;
			XtraEditors.LoadLookUpEdit(ref lueTestDataSource, "DataSourceTypeID", "Name", "TypeDisplay");
		}

		private void tcTest_SelectedIndexChanged(object sender, EventArgs e)
		{
			//------------------------------------------------------------------------- //TEST
			if (tcTest.SelectedIndex == 0)
			{
				if (_tabTestTest == false)
				{
					DataBindTest();
					_tabTestTest = true;
				}
			}

			//------------------------------------------------------------------------- //TEST GROUP NAME
			else if (tcTest.SelectedIndex == 1)
			{
                if (_tabTestMaterial == false)
                {
                    DataBindlueMaterialTestMaterial();
                    DataBindlueTestMaterial();
                    DataBindTestMaterial();
                    _tabTestMaterial = true;
                }
            }
            
            //------------------------------------------------------------------------- //TEST MATERIAL
            else if (tcTest.SelectedIndex == 2)
			{
                if (_tabTestGroupName == false)
                {
					DataBindTestGroupName();
                    _tabTestGroupName = true;
                }

			}

			//------------------------------------------------------------------------- //TEST NUTRIENT RESULT
			else if (tcTest.SelectedIndex == 3)
			{
				if (_tabTestNutrientResult == false)
				{
					DataBindTestNutrientResult();
					DataBindlueTestTestNutrientResult();
                    DataBindlueAIMNutrientTestNutrientDetail();
                    DataBindlueTestGroupTestNutrientResult();
                    _tabTestNutrientResult = true;
				}
			}
		}

		private void repbtnDeleteTest_Click(object sender, EventArgs e)
		{
            try
            {
                TestInfo test = new TestInfo();
                test = (TestInfo)grdTestView.GetFocusedRow();
                test.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteTest(test.TestID);
				DataBindTest();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		private void btnAddTestToGrid_Click(object sender, EventArgs e)
		{
			TestInfo test = new TestInfo();
            Decimal currentCost = 0;
            Decimal previousCost = 0;
			ErrorInfo error = new ErrorInfo();

			try
			{
				if (txtTestCode.Text.Length == 0)
				{
					MessageBox.Show("Please enter a Test Code.");
					txtTestCode.Focus();
					return;
				}
				if (txtTestName.Text.Length == 0)
				{
					MessageBox.Show("Please enter a Test Name.");
					txtTestName.Focus();
					return;
				}
				if ((Int32)lueTestDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select a Data Source.");
					lueTestDataSource.Focus();
					lueTestDataSource.ShowPopup();
					return;
				}
				if (txtTestCurrentCost.Text.Length == 0)
				{
					MessageBox.Show("Please enter a Current Cost.");
					txtTestCurrentCost.Focus();
					return;
				}
				else
				{
					Decimal cost = 0;
					if (Decimal.TryParse(txtTestCurrentCost.Text, out cost) == false)
					{
						MessageBox.Show("Please enter a valid number for Current Cost.");
						txtTestCurrentCost.Focus();
						return;
					}
				}


                Decimal.TryParse(txtTestCurrentCost.Text, out currentCost);

				test.TestID = 0;
				test.TestCode = txtTestCode.Text;
				test.TestName = txtTestName.Text;
				test.DataSourceTypeID = (Int32)lueTestDataSource.EditValue;
				test.TestCurrentCost = currentCost;
				test.TestPreviousCost = previousCost;
				test.UpdateStamp = DateTime.Now;
				test.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				error = AIMManager.SaveTest(ref test);

				if (error.ErrorNumber > 0)
				{
					return;
				}

                txtTestCode.Text = String.Empty;
                txtTestName.Text = String.Empty;
                txtTestCurrentCost.Text = String.Empty;

                DataBindTest();
			}
			catch
			{

			}

            
        }

        private void grdTestView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            Decimal previousCost = new Int32();

            if (grdTestView.FocusedColumn.FieldName == "TestCurrentCost")
			{             
				previousCost = Convert.ToDecimal(grdTestView.GetRowCellValue(e.RowHandle, "TestCurrentCost"));
				grdTestView.SetRowCellValue(e.RowHandle, "TestPreviousCost", previousCost);
            }
        }

        #endregion
        private void lueTestMaterial_EditValueChanged(object sender, EventArgs e)
        {
			TestInfo test = new TestInfo();

			SyncValueLue(ref lueTestMaterial, ref lueTestCodeMaterial);       //task 1133 FG 7/21/2026

			if (lueTestMaterial.EditValue != null && (Int32)lueTestMaterial.EditValue > 0)
			{
                test = AIMManager.GetTest((Int32)lueTestMaterial.EditValue);
                txtTestCost.Text = test.TestCurrentCost.ToString("0.0000");
            }

        }

		//task 1133 FG 7/21/2026 - keep the Test Code and Test Name dropdowns in sync on the Test Material tab
		private void lueTestCodeMaterial_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueTestCodeMaterial, ref lueTestMaterial);
		}
		private void lueTestCodeMaterial_Click(object sender, EventArgs e)
		{
			lueTestCodeMaterial.SelectAll();
		}
		private void lueTestCodeMaterial_Enter(object sender, EventArgs e)
		{
			lueTestCodeMaterial.SelectAll();
		}
		private void lueTestMaterial_Click(object sender, EventArgs e)
		{
			lueTestMaterial.SelectAll();
		}
		private void lueTestMaterial_Enter(object sender, EventArgs e)
		{
			lueTestMaterial.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Material Code and Material Name dropdowns in sync on the Test Material tab
		private void lueMaterialCodeTestMaterial_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueMaterialCodeTestMaterial, ref lueMaterialTestMaterial);
		}
		private void lueMaterialTestMaterial_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueMaterialTestMaterial, ref lueMaterialCodeTestMaterial);
		}
		private void lueMaterialCodeTestMaterial_Click(object sender, EventArgs e)
		{
			lueMaterialCodeTestMaterial.SelectAll();
		}
		private void lueMaterialCodeTestMaterial_Enter(object sender, EventArgs e)
		{
			lueMaterialCodeTestMaterial.SelectAll();
		}
		private void lueMaterialTestMaterial_Click(object sender, EventArgs e)
		{
			lueMaterialTestMaterial.SelectAll();
		}
		private void lueMaterialTestMaterial_Enter(object sender, EventArgs e)
		{
			lueMaterialTestMaterial.SelectAll();
		}

        #region Test Material

        private void grdTestMaterialView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            Decimal totalCost = 0;
            Decimal currentCost = 0;
            Decimal differenceCost = 0;
			Decimal previousUpcharge = 0;

			if (_testMaterialLastColumnName == "TestMaterialCurrentCost")
            {
                previousUpcharge = Convert.ToDecimal(grdTestMaterialView.GetRowCellValue(e.RowHandle, "TestMaterialCurrentUpcharge"));
                totalCost = Convert.ToDecimal(grdTestMaterialView.GetRowCellValue(e.RowHandle, "TestMaterialCurrentCost"));
                currentCost = Convert.ToDecimal(grdTestMaterialView.GetRowCellValue(e.RowHandle, "TestCurrentCost"));
                differenceCost = totalCost - currentCost;
                grdTestMaterialView.SetRowCellValue(e.RowHandle, "TestMaterialCurrentUpcharge", differenceCost);
                grdTestMaterialView.SetRowCellValue(e.RowHandle, "TestMaterialPreviousUpcharge", previousUpcharge);

            }

            TestMaterialInfo test = new TestMaterialInfo();
            test = (TestMaterialInfo)grdTestMaterialView.GetFocusedRow();
            test.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveTestMaterial(ref test);
            DataBindTestMaterial();
        }

        private void DataBindTestMaterial()
		{
            List<TestMaterialInfo> testMaterial = AIMManager.GetTestMaterial();            //Task1061 DB251022
            grdTestMaterial.DataSource = testMaterial;
        }

		private void DataBindlueMaterialTestMaterial()
		{
            List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
            aimMaterial = AIMManager.GetAIMMaterial(true);
            aimMaterial.Insert
            (
                0,
                new AIMMaterialInfo
                {
                    AIMMaterialID = 0,
                    AIMMaterialCode = "--Select--",
                    AIMMaterialName = "--Select--"
                }
            );
            //task 1133 FG 7/21/2026 - bind Material Code and Name dropdowns; Code popup = Code then Name, Name popup = Name then Code
            lueMaterialCodeTestMaterial.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterialCodeTestMaterial, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");

            lueMaterialTestMaterial.Properties.DataSource = aimMaterial;
            XtraEditors.LoadLookUpEdit(ref lueMaterialTestMaterial, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
            lueMaterialTestMaterial.Properties.Columns.Clear();
            lueMaterialTestMaterial.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialName", "Material Name"));
            lueMaterialTestMaterial.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMMaterialCode", "Material Code"));
        }

		private void DataBindlueTestMaterial()
        {
            List<TestInfo> test = new List<TestInfo>();
            test = AIMManager.GetTest();
            test.Insert
            (
                0,
                new TestInfo
                {
                    TestID = 0,
                    TestCode = "--Select--",
                    TestName = "--Select--"
                }
            );
            //task 1133 FG 7/21/2026 - bind Test Code and Name dropdowns; Code popup = Code then Name, Name popup = Name then Code
            lueTestCodeMaterial.Properties.DataSource = test;
            XtraEditors.LoadLookUpEdit(ref lueTestCodeMaterial, "TestID", "TestCode", "TestName");

            lueTestMaterial.Properties.DataSource = test;
            XtraEditors.LoadLookUpEdit(ref lueTestMaterial, "TestID", "TestName", "TestCode");
            lueTestMaterial.Properties.Columns.Clear();
            lueTestMaterial.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TestName", "Test Name"));
            lueTestMaterial.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TestCode", "Test Code"));
        }

        private void btnAddTestMaterialToGrid_Click(object sender, EventArgs e)
        {
            TestMaterialInfo testMaterial = new TestMaterialInfo();
            Decimal currentUpcharge = 0;
            Decimal previousUpcharge = 0;
            ErrorInfo error = new ErrorInfo();

            try
            {
                if ((Int32)lueTestMaterial.EditValue == 0)
                {
                    MessageBox.Show("Please select a Test.");
                    lueTestMaterial.Focus();
                    lueTestMaterial.ShowPopup();
                    return;
                }
                if ((Int32)lueMaterialTestMaterial.EditValue == 0)
                {
                    MessageBox.Show("Please select an AIM Material.");
                    lueMaterialTestMaterial.Focus();
                    lueMaterialTestMaterial.ShowPopup();
                    return;
                }
                if (txtMaterialCurrentUpcharge.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Current Cost.");
                    txtTestCurrentCost.Focus();
                    return;
                }
                else
                {
                    Decimal cost = 0;
                    if (Decimal.TryParse(txtMaterialCurrentUpcharge.Text, out cost) == false)
                    {
                        MessageBox.Show("Please enter a valid number for Current Cost.");
                        txtMaterialCurrentUpcharge.Focus();
                        return;
                    }
                }

                Decimal.TryParse(txtMaterialCurrentUpcharge.Text, out currentUpcharge);

                testMaterial.TestID = (Int32)lueTestMaterial.EditValue;
				testMaterial.TestMaterialID = 0;
                testMaterial.AIMMaterialID = (Int32)lueMaterialTestMaterial.EditValue;
                testMaterial.TestMaterialCurrentUpcharge = currentUpcharge;
                testMaterial.TestMaterialPreviousUpcharge = previousUpcharge;
                testMaterial.UpdateStamp = DateTime.Now;
                testMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                error = AIMManager.SaveTestMaterial(ref testMaterial);

                if (error.ErrorNumber > 0)
                {
                    return;
                }

                txtMaterialCurrentUpcharge.Text = String.Empty;

                DataBindTestMaterial();
            }
            catch
            {

            }
        }

        private void repbtnTestMaterialDelete_Click(object sender, EventArgs e)
        {
            try
            {
                TestMaterialInfo testMaterial = new TestMaterialInfo();
                testMaterial = (TestMaterialInfo)grdTestMaterialView.GetFocusedRow();
                testMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteTestMaterial(testMaterial.TestMaterialID);
                DataBindTestMaterial();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void grdTestMaterialView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            Decimal previousCost = new Decimal();

           if (grdTestMaterialView.FocusedColumn.FieldName == "TestMaterialCurrentUpcharge")
            {
                previousCost = Convert.ToDecimal(grdTestMaterialView.GetRowCellValue(e.RowHandle, "TestMaterialCurrentUpcharge"));
                grdTestMaterialView.SetRowCellValue(e.RowHandle, "TestMaterialPreviousUpcharge", previousCost);
            }

			_testMaterialLastColumnName = grdTestMaterialView.FocusedColumn.FieldName.ToString();

        }

        #endregion

        #region Test Group Name

        private void grdTestGroupNameView_ValidateRow(object sender, ValidateRowEventArgs e)
        {


            TestGroupNameInfo test = new TestGroupNameInfo();
            test = (TestGroupNameInfo)grdTestGroupNameView.GetFocusedRow();
            test.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveTestGroupName(ref test);
            DataBindTestGroupName();
        }

        private void btnAddTestGroupNameToGrid_Click(object sender, EventArgs e)
        {
            TestGroupNameInfo testGroupName = new TestGroupNameInfo();
            ErrorInfo error = new ErrorInfo();

            try
            {
                if (txtTestGroupName.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Group Name Code.");
                    txtTestGroupName.Focus();
                    return;
                }

                testGroupName.TestGroupNameID = 0;
				testGroupName.TestGroupName = txtTestGroupName.Text;
                testGroupName.UpdateStamp = DateTime.Now;
                testGroupName.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

                error = AIMManager.SaveTestGroupName(ref testGroupName);

                if (error.ErrorNumber > 0)
                {
                    return;
                }

                txtTestGroupName.Text = String.Empty;
                DataBindTestGroupName();
            }
            catch
            {

            }
        }
        private void DataBindTestGroupName()
        {
            List<TestGroupNameInfo> testGroupName = AIMManager.GetTestGroupName();            
            grdTestGroupName.DataSource = testGroupName;
        }

        private void repbtnDeleteTestGroupName_Click(object sender, EventArgs e)
        {
            try
            {
                TestGroupNameInfo testGroupName = new TestGroupNameInfo();
                testGroupName = (TestGroupNameInfo)grdTestGroupNameView.GetFocusedRow();
                testGroupName.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteTestGroupName(testGroupName.TestGroupNameID);
                DataBindTestGroupName();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Test Nutrient Result
        private void btnAddTestNutrientResultToGrid_Click(object sender, EventArgs e)
        {
            TestNutrientResultInfo testNutrientResult = new TestNutrientResultInfo();
            ErrorInfo error = new ErrorInfo();
			Int32 testGroupNameID = 0;
            Decimal currentUpcharge = 0;
            Decimal previousUpcharge = 0;

            try
            {
                if ((Int32)lueAIMNutrientTestNutrientResult.EditValue == 0)
                {
                    MessageBox.Show("Please select an Nutrient.");
                    lueAIMNutrientTestNutrientResult.Focus();
                    lueAIMNutrientTestNutrientResult.ShowPopup();
                    return;
                }
                if ((Int32)lueTestTestNutrientResult.EditValue == 0)
                {
                    MessageBox.Show("Please select a valid Test.");
                    lueTestTestNutrientResult.Focus();
                    lueTestTestNutrientResult.ShowPopup();
                    return;
                }
                if (txtTestNutrientResultCurrentUpcharge.Text.Length == 0)
                {
                    MessageBox.Show("Please enter a Current Cost.");
                    txtTestNutrientResultCurrentUpcharge.Focus();
                    return;
                }
                else
                {
                    Decimal cost = 0;
                    if (Decimal.TryParse(txtTestNutrientResultCurrentUpcharge.Text, out cost) == false)
                    {
                        MessageBox.Show("Please enter a valid number for Current Cost.");
                        txtTestNutrientResultCurrentUpcharge.Focus();
                        return;
                    }
                }

                if ((Int32)lueTestGroupTestNutrientResult.EditValue >  0)
                {
					testGroupNameID = (Int32)lueTestGroupTestNutrientResult.EditValue;
                }

                Decimal.TryParse(txtTestNutrientResultCurrentUpcharge.Text, out currentUpcharge);

                testNutrientResult.TestNutrientResultID = 0;
                testNutrientResult.TestID = (Int32)lueTestTestNutrientResult.EditValue;
                testNutrientResult.AIMNutrientID = (Int32)lueAIMNutrientTestNutrientResult.EditValue;
				testNutrientResult.TestGroupNameID = testGroupNameID;
				testNutrientResult.TestNutrientResultCurrentUpcharge = currentUpcharge;
                testNutrientResult.TestNutrientResultPreviousUpcharge = previousUpcharge;
				testNutrientResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				error = AIMManager.SaveTestNutrientResult(ref testNutrientResult);

				if (error.ErrorNumber > 0)
                {
                    return;
                }

                txtTestNutrientResultCurrentUpcharge.Text = String.Empty;
                txtTestNutrientResultTotalCost.Text = String.Empty;
                DataBindTestNutrientResult();

            }
            catch
            {

            }
        }

		private void DataBindTestNutrientResult()
		{
            List<TestNutrientResultInfo> testNutrientResult = AIMManager.GetTestNutrientResult();
            grdTestNutrientResult.DataSource = testNutrientResult;
        }

		private void DataBindlueTestTestNutrientResult()
		{
            List<TestInfo> test = new List<TestInfo>();
            test = AIMManager.GetTest();
            test.Insert
            (
                0,
                new TestInfo
                {
                    TestID = 0,
                    TestCode = "--Select--",
                    TestName = "--Select--"
                }
            );
            //task 1133 FG 7/21/2026 - bind Test Code and Name dropdowns; Code popup = Code then Name, Name popup = Name then Code
            lueTestCodeTestNutrientResult.Properties.DataSource = test;
            XtraEditors.LoadLookUpEdit(ref lueTestCodeTestNutrientResult, "TestID", "TestCode", "TestName");

            lueTestTestNutrientResult.Properties.DataSource = test;
            XtraEditors.LoadLookUpEdit(ref lueTestTestNutrientResult, "TestID", "TestName", "TestCode");
            lueTestTestNutrientResult.Properties.Columns.Clear();
            lueTestTestNutrientResult.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TestName", "Test Name"));
            lueTestTestNutrientResult.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TestCode", "Test Code"));
        }

		private void DataBindlueAIMNutrientTestNutrientDetail()
		{
            List<AIMNutrientInfo> nutrient = new List<AIMNutrientInfo>();
            nutrient = AIMManager.GetAIMNutrient(true);
            nutrient.Insert
            (
                0,
                new AIMNutrientInfo
                {
                    AIMNutrientID = 0,
                    AIMNutrientCode = "--Select--",
                    AIMNutrientName = "--Select--"
                }
            );
            //task 1133 FG 7/21/2026 - bind Nutrient Code and Name dropdowns; Code popup = Code then Name, Name popup = Name then Code
            lueAIMNutrientCodeTestNutrientResult.Properties.DataSource = nutrient;
            XtraEditors.LoadLookUpEdit(ref lueAIMNutrientCodeTestNutrientResult, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

            lueAIMNutrientTestNutrientResult.Properties.DataSource = nutrient;
            XtraEditors.LoadLookUpEdit(ref lueAIMNutrientTestNutrientResult, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");
            lueAIMNutrientTestNutrientResult.Properties.Columns.Clear();
            lueAIMNutrientTestNutrientResult.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMNutrientName", "Nutrient Name"));
            lueAIMNutrientTestNutrientResult.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AIMNutrientCode", "Nutrient Code"));
        }

		private void DataBindlueTestGroupTestNutrientResult()
		{
            List<TestGroupNameInfo> testGroupName = new List<TestGroupNameInfo>();
            testGroupName = AIMManager.GetTestGroupName();
            testGroupName.Insert
            (
                0,
                new TestGroupNameInfo
                {
                    TestGroupNameID = 0,
                    TestGroupName = "--Select--"
                }
            );
            lueTestGroupTestNutrientResult.Properties.DataSource = testGroupName;
            XtraEditors.LoadLookUpEdit(ref lueTestGroupTestNutrientResult, "TestGroupNameID", "TestGroupName", "TestGroupName");
        }

        private void repbtnDeleteTestNutrientResult_Click(object sender, EventArgs e)
        {
            try
            {
                TestNutrientResultInfo testNutrientResult = new TestNutrientResultInfo();
                testNutrientResult = (TestNutrientResultInfo)grdTestNutrientResultView.GetFocusedRow();
                testNutrientResult.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteTestNutrientResult(testNutrientResult.TestNutrientResultID);
                DataBindTestNutrientResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lueTestTestNutrientResult_EditValueChanged(object sender, EventArgs e)
        {
            TestInfo test = new TestInfo();

			SyncValueLue(ref lueTestTestNutrientResult, ref lueTestCodeTestNutrientResult);       //task 1133 FG 7/21/2026

            if (lueTestTestNutrientResult.EditValue != null && (Int32)lueTestTestNutrientResult.EditValue > 0)
            {
                test = AIMManager.GetTest((Int32)lueTestTestNutrientResult.EditValue);
                txtTestCostNutrientResult.Text = test.TestCurrentCost.ToString("0.0000");
            }
        }

		//task 1133 FG 7/21/2026 - keep the Test Code and Test Name dropdowns in sync on the Test Nutrient Result tab
		private void lueTestCodeTestNutrientResult_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueTestCodeTestNutrientResult, ref lueTestTestNutrientResult);
		}
		private void lueTestCodeTestNutrientResult_Click(object sender, EventArgs e)
		{
			lueTestCodeTestNutrientResult.SelectAll();
		}
		private void lueTestCodeTestNutrientResult_Enter(object sender, EventArgs e)
		{
			lueTestCodeTestNutrientResult.SelectAll();
		}
		private void lueTestTestNutrientResult_Click(object sender, EventArgs e)
		{
			lueTestTestNutrientResult.SelectAll();
		}
		private void lueTestTestNutrientResult_Enter(object sender, EventArgs e)
		{
			lueTestTestNutrientResult.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Nutrient Code and Nutrient Name dropdowns in sync on the Test Nutrient Result tab
		private void lueAIMNutrientCodeTestNutrientResult_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMNutrientCodeTestNutrientResult, ref lueAIMNutrientTestNutrientResult);
		}
		private void lueAIMNutrientTestNutrientResult_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueAIMNutrientTestNutrientResult, ref lueAIMNutrientCodeTestNutrientResult);
		}
		private void lueAIMNutrientCodeTestNutrientResult_Click(object sender, EventArgs e)
		{
			lueAIMNutrientCodeTestNutrientResult.SelectAll();
		}
		private void lueAIMNutrientCodeTestNutrientResult_Enter(object sender, EventArgs e)
		{
			lueAIMNutrientCodeTestNutrientResult.SelectAll();
		}
		private void lueAIMNutrientTestNutrientResult_Click(object sender, EventArgs e)
		{
			lueAIMNutrientTestNutrientResult.SelectAll();
		}
		private void lueAIMNutrientTestNutrientResult_Enter(object sender, EventArgs e)
		{
			lueAIMNutrientTestNutrientResult.SelectAll();
		}

        private void grdTestNutrientResultView_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            Decimal totalCost = 0;
            Decimal currentCost = 0;
            Decimal differenceCost = 0;
            Decimal previousUpcharge = 0;

			if (_testNutrientResultLastColumnName == "TestNutrientResultCurrentCost")
			{
                previousUpcharge = Convert.ToDecimal(grdTestNutrientResultView.GetRowCellValue(e.RowHandle, "TestNutrientResultCurrentUpcharge"));
                totalCost = Convert.ToDecimal(grdTestNutrientResultView.GetRowCellValue(e.RowHandle, "TestNutrientResultCurrentCost"));
                currentCost = Convert.ToDecimal(grdTestNutrientResultView.GetRowCellValue(e.RowHandle, "TestCurrentCost"));
                differenceCost = totalCost - currentCost;
                grdTestNutrientResultView.SetRowCellValue(e.RowHandle, "TestNutrientResultCurrentUpcharge", differenceCost);
                grdTestNutrientResultView.SetRowCellValue(e.RowHandle, "TestNutrientResultPreviousUpcharge", previousUpcharge);
            }


            TestNutrientResultInfo test = new TestNutrientResultInfo();
            test = (TestNutrientResultInfo)grdTestNutrientResultView.GetFocusedRow();
            test.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
            AIMManager.SaveTestNutrientResult(ref test);
            DataBindTestNutrientResult();
        }

        private void grdTestNutrientResultView_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            Decimal previousCost = new Decimal();

            if (grdTestNutrientResultView.FocusedColumn.FieldName == "TestNutrientResultCurrentUpcharge")
            {
                previousCost = Convert.ToDecimal(grdTestNutrientResultView.GetRowCellValue(e.RowHandle, "TestNutrientResultCurrentUpcharge"));
                grdTestNutrientResultView.SetRowCellValue(e.RowHandle, "TestNutrientResultPreviousUpcharge", previousCost);
            }

			_testNutrientResultLastColumnName = grdTestNutrientResultView.FocusedColumn.FieldName.ToString();

        }


        private void txtMaterialCurrentTotalCost_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Decimal currentCost = 0;
            Decimal currentUpcharge = 0;
            Decimal currentTotalCost = 0;

            if (Decimal.TryParse(txtTestCost.Text, out currentCost))
            {
                if (Decimal.TryParse(txtMaterialCurrentTotalCost.Text, out currentTotalCost))
                {
                    currentUpcharge = currentTotalCost - currentCost;
                    txtMaterialCurrentUpcharge.Text = currentUpcharge.ToString("0.0000");
                }
            }
        }

        private void txtMaterialCurrentUpcharge_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Decimal currentCost = 0;
            Decimal currentUpcharge = 0;
            Decimal currentTotalCost = 0;

            if (Decimal.TryParse(txtTestCost.Text, out currentCost))
            {
                if (Decimal.TryParse(txtMaterialCurrentUpcharge.Text, out currentUpcharge))
                {
                    currentTotalCost = currentCost + currentUpcharge;
                    txtMaterialCurrentTotalCost.Text = currentTotalCost.ToString("0.0000");
                }
            }
        }

        private void txtTestNutrientResultTotalCost_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Decimal currentCost = 0;
            Decimal currentUpcharge = 0;
            Decimal currentTotalCost = 0;

            if (Decimal.TryParse(txtTestCostNutrientResult.Text, out currentCost))
            {
                if (Decimal.TryParse(txtTestNutrientResultTotalCost.Text, out currentTotalCost))
                {
                    currentUpcharge = currentTotalCost - currentCost;
                    txtTestNutrientResultCurrentUpcharge.Text = currentUpcharge.ToString("0.0000");
                }
            }
        }

        private void txtTestNutrientResultCurrentUpcharge_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Decimal currentCost = 0;
            Decimal currentUpcharge = 0;
            Decimal currentTotalCost = 0;

            if (Decimal.TryParse(txtTestCostNutrientResult.Text, out currentCost))
            {
                if (Decimal.TryParse(txtTestNutrientResultCurrentUpcharge.Text, out currentUpcharge))
                {
                    currentTotalCost = currentCost + currentUpcharge;
                    txtTestNutrientResultTotalCost.Text = currentTotalCost.ToString("0.0000");
                }
            }
        }

        private void repbtnDeleteSpecificProduct_Click(object sender, EventArgs e)			//Task1169  DB260408
        {
            try
            {
                SpecificProductsInfo specific = new SpecificProductsInfo();
                specific = (SpecificProductsInfo)grdSpecificProductsView.GetFocusedRow();
                specific.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                AIMManager.DeleteSpecificProducts(specific.SpecificProductsID);				 // Task 1205 FG

                //filter grid for group code		
                DataBindSpecificProductsByGroup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        //Task1121 DB251218 End
    }
}
