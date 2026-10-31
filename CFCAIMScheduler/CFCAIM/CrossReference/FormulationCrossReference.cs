using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace CFCAIM.Maintenance
{
	public partial class FormulationCrossReference : Form
	{

		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];

		CFCUser.Global userGlobal = new CFCUser.Global();
		Boolean _ingrLoaded = false;
		Boolean _nutrLoaded = false;
		Boolean _ownerLoaded = false;
		Boolean _prodLoaded = false;
		
		public FormulationCrossReference()
		{
			InitializeComponent();
		}
		private void FormulationCrossReference_Load(object sender, EventArgs e)
		{

			//1/27/21 - moved this code to the TabControl1.SelectedIndexChanged event (for CSA speed issue)
			//DataBindLuePlant_Product();
			//DataBindLueOwner_Material();
			//DataBindLueProduct();
			//DataBindLueIngredient();
			//DataBindLueNutrient();
			//DataBindLueOwner();
			//DataBindLueMaterial();
			//DataBindReplueIngredient();
			//DataBindReplueNutrient();
			//DataBindReplueOwner();
			//DataBindReplueProduct();
			//DataBindGridIngredient();
			//DataBindGridNutrient();
			//DataBindGridOwner();
			//DataBindGridProduct();
			//replueCFCCodeNutrient.Columns["CFCNutrientCode"].SortOrder = DevExpress.Data.ColumnSortOrder.None;
			//replueCFCCodeNutrient.Columns["CFCNutrientCode"].SortOrder = (DevExpress.Data.ColumnSortOrder)DevExpress.XtraGrid.ColumnSortMode.Custom;
			//grdNutrientRefView.Columns["CFCNutrientCode"].SortOrder = DevExpress.Data.ColumnSortOrder.None;
			//grdNutrientRefView.Columns["CFCNutrientCode"].SortOrder = (DevExpress.Data.ColumnSortOrder)DevExpress.XtraGrid.ColumnSortMode.Custom;

			//6/5/24
			//5/23/23 - hide until done with sql
			////tabControl1.TabPages.Remove(tpProductsMissingXRef);

			if (AIMGlobal.ownerLocationTab == true)
			{
				tabControl1.SelectedIndex = 2;
			}
			else if (AIMGlobal.formulationCrossReference != "")
			{
				if (AIMGlobal.formulationCrossReference == "Ingredient")
				{
					tabControl1.SelectedIndex = 0;
				}
				if (AIMGlobal.formulationCrossReference == "Nutrient")
				{
					tabControl1.SelectedIndex = 1;
				}
				if (AIMGlobal.formulationCrossReference == "Plant")
				{
					tabControl1.SelectedIndex = 2;
				}
				if (AIMGlobal.formulationCrossReference == "Product")
				{
					tabControl1.SelectedIndex = 3;
				}
                else
                {
					tabControl1.SelectedIndex = 0;
				}              				

			}

			//1/20/22 - set to Owner-Location tab because it will, most likely, have the least records -- and be the quickest
			else
			{
				tabControl1.SelectedIndex = 2;
			}

			//1/27/21
			DataBindUsingTabControlIndex(tabControl1.SelectedIndex);

			SetToolTips();
			WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
		}

		//1/27/21 - moved load of all the grids and dropdowns to tab-control (trying to improve CSA speed issue)
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			DataBindUsingTabControlIndex(tabControl1.SelectedIndex);
		}
		private void DataBindUsingTabControlIndex(Int32 index)
		{
			switch (index)
			{

				//INGREDIENT
				case 0:
					if (_ingrLoaded == false)
					{

						DataBindLueIngredientFRM();
						DataBindLueIngredientAIM();
						DataBindReplueIngredient();
						DataBindGridIngredient();
						_ingrLoaded = true;
					}
					break;

				//NUTRIENT
				case 1:
					if (_nutrLoaded == false)
					{
						//10/18/23
						//DataBindLueNutrient();
						DataBindLueNutrientFRM();
						DataBindLueNutrientAIM();

						DataBindReplueNutrient();
						DataBindGridNutrient();
						replueCFCCodeNutrient.Columns["CFCNutrientCode"].SortOrder = DevExpress.Data.ColumnSortOrder.None;
						replueCFCCodeNutrient.Columns["CFCNutrientCode"].SortOrder = (DevExpress.Data.ColumnSortOrder)DevExpress.XtraGrid.ColumnSortMode.Custom;
						grdNutrientRefView.Columns["CFCNutrientCode"].SortOrder = DevExpress.Data.ColumnSortOrder.None;
						grdNutrientRefView.Columns["CFCNutrientCode"].SortOrder = (DevExpress.Data.ColumnSortOrder)DevExpress.XtraGrid.ColumnSortMode.Custom;
						_nutrLoaded = true;
					}
					break;

				//OWNER-LOCATION
				case 2:
					if (_ownerLoaded == false)
					{
						//10/18/32
						//DataBindLueOwner();
						DataBindLueOwnerFRM();
						DataBindLueOwnerAIM();

						DataBindReplueOwner();
						DataBindGridOwner();
						_ownerLoaded = true;
					}
					break;

				//PRODUCT
				case 3:
					if (_prodLoaded == false)
					{
						DataBindLuePlant_Product();
						DataBindLueOwner_Material();
						DataBindLueMaterial();
						DataBindLueProduct();
						DataBindReplueProduct();
						DataBindGridProduct();
						_prodLoaded = true;
					}
					break;

				//6/5/24
				//PRODUCTS MISSING X-REF
				case 4:
					DataBindProductMissingXrefGrid();
					break;
			}
		}

		//6/5/24
		private void DataBindProductMissingXrefGrid()
		{			
			Int32 rowNum = 0;
			Int32.TryParse(grdIngredientRefView.FocusedRowHandle.ToString(), out rowNum);
			grdProdMissing.DataSource = AIMManager.GetMissingProductXRefs();
			grdProdMissingView.FocusedRowHandle = rowNum;
		}

		private void DataBindLueOwnerLocation()
		{
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			Int32 ownerID = 0;
			if (lueOwner_Material.EditValue != null && lueOwner_Material.Text != String.Empty)
			{
				ownerID = (Int32)lueOwner_Material.EditValue;
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

			lueOwnerLocation_Material.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocation_Material, "AIMOwnerLocationID", "LocationCode", "LocationName");
			lueOwnerLocation_Material.Properties.Columns["AIMOwnerLocationID"].Visible = false;

			if (aimOwnerLocation.Count == 2)
			{
				lueOwnerLocation_Material.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
			}

		}

		private void DataBindLueOwnerLocation_Owner()
		{

			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			Int32 ownerID = 0;
			if (lueOwnerCode.EditValue != null && lueOwnerCode.Text != String.Empty)
			{
				ownerID = (Int32)lueOwnerCode.EditValue;
			}
			if (ownerID == 0)
			{
				aimOwnerLocation.Clear();
			}
			else
			{				         
                aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(ownerID);    //This stp gets only ACTIVE owner-locations for the owner
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
			lueAIMLocationCode.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMLocationCode, "AIMOwnerLocationID", "LocationCode");

			lueAIMLocationName.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMLocationName, "AIMOwnerLocationID", "LocationName");

			//1/23/18
			if (aimOwnerLocation.Count == 2)
			{
				lueAIMLocationCode.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
				lueAIMLocationName.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
			}

		}



		private void DataBindGridIngredient()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdIngredientRefView.FocusedRowHandle.ToString(), out rowNum);
			grdIngredientRef.DataSource = AIMManager.GetCFCIngredientRefAIMMaterial();
			grdIngredientRefView.FocusedRowHandle = rowNum;
		}
		private void DataBindGridNutrient()
		{
			//grid has to populated after replue
			Int32 rowNum = 0;
			Int32.TryParse(grdNutrientRefView.FocusedRowHandle.ToString(), out rowNum);
			grdNutrientRef.DataSource = AIMManager.GetAllCFCNutrientRefAIMNutrientAimDatabase((Int32)_mainMenu.lueDataMap.EditValue);
			grdNutrientRefView.FocusedRowHandle = rowNum;

		}
		private void DataBindGridOwner()
		{
			Int32 currentRefTableRow = 0;
			if (grdReferencePlantView != null)
			{
				Int32.TryParse(grdReferencePlantView.FocusedRowHandle.ToString(), out currentRefTableRow);
			}
			grdReferencePlant.DataSource = AIMManager.GetAllCFCPlantAIMOwnerLocation();
			grdReferencePlantView.FocusedRowHandle = currentRefTableRow;
		}
		private void DataBindGridProduct()
		{
			//grid has to populated after replue
			Int32 rowNum = 0;
			if (grdProductView != null)
			{
				Int32.TryParse(grdProductView.FocusedRowHandle.ToString(), out rowNum);
			}
			grdProduct.DataSource = AIMManager.GetAllCFCProductRefAIMMaterial();
			grdProductView.FocusedRowHandle = rowNum;
		}

		//10/9/23 - split DataBindLueIngredient into 2 subs (so when clicking with or w/o references won't wipe out selections)
		private void DataBindLueIngredientFRM()
		{
			List<CFCIngredientInfo> cfcIngredient = new List<CFCIngredientInfo>();
			
			//1/5/22 - added filter to dropdowns, to show materials with or without x-refs  (true = WITH x-refs)
			Boolean blnFrmIsXref = true;			
			if (rbFrmFilterWithXrefIngr.Checked == false)
			{
				blnFrmIsXref = false;
			}
			
			//1/5/22 - added filter to dropdowns, to show materials with or without x-refs  (true = WITH x-refs)
			//cfcIngredient = AIMManager.GetAllCFCIngredientAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);			
			cfcIngredient = AIMManager.GetAllCFCIngredientAimDatabaseIDWithOrMissingXref(true, (Int32)_mainMenu.lueDataMap.EditValue, blnFrmIsXref);		

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
			lueCFCCodeIngredient.Properties.DataSource = cfcIngredient;
			XtraEditors.LoadLookUpEdit(ref lueCFCCodeIngredient, "CFCIngredientID", "CFCIngredientCode");
			lueCFCCodeIngredient.Properties.Columns["CFCIngredientCode"].Visible = true;
			lueCFCCodeIngredient.Properties.Columns["CFCIngredientName"].Visible = true;
			lueCFCCodeIngredient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			lueCFCCodeIngredient.Properties.Columns["CFCIngredientCode"].Caption = "FRM Ingredient Code";
			lueCFCCodeIngredient.Properties.Columns["CFCIngredientName"].Caption = "FRM Ingredient Name";			

			lueCFCNameIngredient.Properties.DataSource = cfcIngredient;
			XtraEditors.LoadLookUpEdit(ref lueCFCNameIngredient, "CFCIngredientID", "CFCIngredientName");
			lueCFCNameIngredient.Properties.Columns["CFCIngredientCode"].Visible = true;
			lueCFCNameIngredient.Properties.Columns["CFCIngredientName"].Visible = true;
			lueCFCNameIngredient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			lueCFCNameIngredient.Properties.Columns["CFCIngredientName"].Caption = "FRM Ingredient Name";
		}

		//10/9/23 - split DataBindLueIngredient into 2 subs (so when clicking with or w/o references won't wipe out selections)
		private void DataBindLueIngredientAIM()
		{
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();

			//1/5/22 - added filter to dropdowns, to show materials with or without x-refs  (true = WITH x-refs)			
			Boolean blnAimIsXref = true;
			if (rbAimFilterWithXrefIngr.Checked == false)
			{
				blnAimIsXref = false;
			}

			//1/5/22 - added filter to dropdowns, to show materials with or without x-refs  (true = WITH x-refs)			
			//aimMaterial = AIMManager.GetAIMMaterial(true);
			aimMaterial = AIMManager.GetAIMMaterialWithOrMissingXref(true, blnAimIsXref);

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
			lueAIMCodeMaterial.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMCodeMaterial, "AIMMaterialID", "AIMMaterialCode");
			lueAIMCodeMaterial.Properties.Columns["AIMMaterialName"].Visible = true;
			lueAIMCodeMaterial.Properties.Columns["AIMMaterialCode"].Visible = true;			

			lueAIMNameMaterial.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueAIMNameMaterial, "AIMMaterialID", "AIMMaterialName");
			lueAIMNameMaterial.Properties.Columns["AIMMaterialCode"].Visible = true;
			lueAIMNameMaterial.Properties.Columns["AIMMaterialName"].Visible = true;			

		}

			
		private void SetSortOrder(LookUpEdit lue, String field)
		{
			lue.Properties.Columns[field].AllowSort = DevExpress.Utils.DefaultBoolean.True;
			lue.Properties.Columns[field].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
			lue.Properties.SortColumnIndex = lue.Properties.Columns.IndexOf(lue.Properties.Columns[field]);

		}

		//10/18/23
		private void DataBindLueNutrientFRM()
		{
			List<CFCNutrientInfo> cfcNutrient = new List<CFCNutrientInfo>();			

			//1/5/22 - added filter to dropdowns, to show nutrients with or without x-refs  (true = WITH x-refs)
			Boolean blnFrmIsXref = true;			
			if (rbFrmFilterWithXrefNut.Checked == false)
			{
				blnFrmIsXref = false;
			}
			
			//1/5/22 - added filter to dropdowns, to show nutrients with or without x-refs  (true = WITH x-refs)			
			cfcNutrient = AIMManager.GetAllCFCNutrientAimDatabaseIDWithOrMissingXref(true, (Int32)_mainMenu.lueDataMap.EditValue, blnFrmIsXref);
								
			cfcNutrient.Insert
			(
				0,
				new CFCNutrientInfo
				{
					CFCNutrientID = 0,
					CFCNutrientCode = "--Select--",
					CFCNutrientName = "--Select--"
				}
			);
			lueFrmCodeNutrient.Properties.DataSource = cfcNutrient;
			XtraEditors.LoadLookUpEdit(ref lueFrmCodeNutrient, "CFCNutrientID", "CFCNutrientCode");
			lueFrmCodeNutrient.Properties.Columns["CFCNutrientCode"].Visible = true;
			lueFrmCodeNutrient.Properties.Columns["CFCNutrientName"].Visible = true;
			lueFrmCodeNutrient.Properties.Columns["CFCNutrientCode"].Caption = "FRM Nutrient Code";
			lueFrmCodeNutrient.Properties.Columns["CFCNutrientName"].Caption = "FRM Nutrient Name";

			lueFrmNameNutrient.Properties.DataSource = cfcNutrient;
			XtraEditors.LoadLookUpEdit(ref lueFrmNameNutrient, "CFCNutrientID", "CFCNutrientName");
			lueFrmNameNutrient.Properties.Columns["CFCNutrientName"].Visible = true;
			lueFrmNameNutrient.Properties.Columns["CFCNutrientCode"].Visible = true;
			lueFrmNameNutrient.Properties.Columns["CFCNutrientName"].Caption = "FRM Nutrient Name";
			lueFrmNameNutrient.Properties.Columns["CFCNutrientCode"].Caption = "FRM Nutrient Code";
					
		}

		//10/18/23
		private void DataBindLueNutrientAIM()
		{			
			List<AIMNutrientInfo> AimNutrient = new List<AIMNutrientInfo>();

			//1/5/22 - added filter to dropdowns, to show nutrients with or without x-refs  (true = WITH x-refs)		
			Boolean blnAimIsXref = true;			
			if (rbAimFilterWithXrefNut.Checked == false)
			{
				blnAimIsXref = false;
			}
						
			//1/5/22 - added filter to dropdowns, to show nutrients with or without x-refs  (true = WITH x-refs)						
			AimNutrient = AIMManager.GetAllAIMNutrientWithOrMissingXref(true, blnAimIsXref);			
			
			AimNutrient.Insert
			(
				0,
				new AIMNutrientInfo
				{
					AIMNutrientID = 0,
					AIMNutrientCode = "--Select--",
					AIMNutrientName = "--Select--"
				}
			);
			lueAIMCodeNutrient.Properties.DataSource = AimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMCodeNutrient, "AIMNutrientID", "AIMNutrientCode");
			lueAIMCodeNutrient.Properties.Columns["AIMNutrientCode"].Visible = true;
			lueAIMCodeNutrient.Properties.Columns["AIMNutrientName"].Visible = true;

			lueAIMNameNutrient.Properties.DataSource = AimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMNameNutrient, "AIMNutrientID", "AIMNutrientName");
			lueAIMNameNutrient.Properties.Columns["AIMNutrientCode"].Visible = true;
			lueAIMNameNutrient.Properties.Columns["AIMNutrientName"].Visible = true;

		}

		
		private void DataBindLueOwner_Material()
		{
			List<AIMOwnerInfo> aimOwner_Material = new List<AIMOwnerInfo>();
			aimOwner_Material = AIMManager.GetAIMOwner(true);
			aimOwner_Material.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--",
					OwnerName = "--Select--"
				}
			);
			lueOwner_Material.Properties.DataSource = aimOwner_Material;
			XtraEditors.LoadLookUpEdit(ref lueOwner_Material, "AIMOwnerID", "OwnerCode", "OwnerName");
			lueOwner_Material.Properties.Columns["AIMOwnerID"].Visible = false;

			if (aimOwner_Material.Count == 2)
			{
				lueOwner_Material.EditValue = aimOwner_Material[1].AIMOwnerID;
			}

		}

		private void DataBindLuePlant_Product()
		{
			List<CFCPlantInfo> cfcPlant_Product = new List<CFCPlantInfo>();
			cfcPlant_Product = AIMManager.GetCFCPlantAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);				
			cfcPlant_Product.Insert
			(
				0,
				new CFCPlantInfo
				{
					CFCPlantID = 0,
					CFCPlantCode = "--Select--",
					CFCPlantName = "--Select--"
				}
			);

			luePlant_Product.Properties.DataSource = cfcPlant_Product;
			XtraEditors.LoadLookUpEdit(ref luePlant_Product, "CFCPlantID", "CFCPlantCode", "CFCPlantName");
			luePlant_Product.Properties.Columns["CFCPlantID"].Visible = false;

			if (cfcPlant_Product.Count == 2)
			{
				luePlant_Product.EditValue = cfcPlant_Product[1].CFCPlantID;
			}

		}

		//10/18/23
		private void DataBindLueOwnerFRM()
		{
			List<CFCPlantInfo> cfcplant = new List<CFCPlantInfo>();
			
			//1/5/22 - added filter to dropdowns, to show plants with or without x-refs  (true = WITH x-refs)
			Boolean blnFrmIsXref = true;
			if (rbFrmFilterWithXrefLoc.Checked == false)
			{
				blnFrmIsXref = false;
			}

			//1/5/22 - added filter to dropdowns, to show plants with or without x-refs  (true = WITH x-refs)
			//cfcplant = AIMManager.GetCFCPlantAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
			cfcplant = AIMManager.GetCFCPlantAimDatabaseIDWithOrMissingXref(true, (Int32)_mainMenu.lueDataMap.EditValue, blnFrmIsXref);

			cfcplant.Insert
			(
				0,
				new CFCPlantInfo
				{
					CFCPlantID = 0,
					CFCPlantCode = "--Select--",
					CFCPlantName = "--Select--"
				}
			);

			lueCFCPlantCode.Properties.DataSource = cfcplant;
			XtraEditors.LoadLookUpEdit(ref lueCFCPlantCode, "CFCPlantID", "CFCPlantCode");
			lueCFCPlantCode.Properties.Columns["CFCPlantCode"].Caption = "FRM Plant Code";
			lueCFCPlantCode.Properties.Columns["CFCPlantName"].Caption = "FRM Plant Name";

			lueCFCPlantName.Properties.DataSource = cfcplant;
			XtraEditors.LoadLookUpEdit(ref lueCFCPlantName, "CFCPlantID", "CFCPlantName");
			lueCFCPlantName.Properties.Columns["CFCPlantCode"].Caption = "FRM Plant Code";
			lueCFCPlantName.Properties.Columns["CFCPlantName"].Caption = "FRM Plant Name";
						
		}

		//10/18/23
		private void DataBindLueOwnerAIM()
		{			
			List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
			owner = AIMManager.GetAIMOwner(true);
			owner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--",
					OwnerName = "--Select--"
				}
			);
			lueOwnerCode.Properties.DataSource = owner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerCode, "AIMOwnerID", "OwnerCode");

			lueOwnerName.Properties.DataSource = owner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName");

		}

		
		private void DataBindLueOwnerLocation_Material()
		{
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			Int32 ownerID = 0;
			if (lueOwner_Material.EditValue != null && lueOwner_Material.Text != String.Empty)
			{
				ownerID = (Int32)lueOwner_Material.EditValue;
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

			lueOwnerLocation_Material.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocation_Material, "AIMOwnerLocationID", "LocationCode", "LocationName");
			lueOwnerLocation_Material.Properties.Columns["AIMOwnerLocationID"].Visible = false;

		}

		private void DataBindLueMaterial()
		{
			List<AIMMaterialInfo> AimMaterial = new List<AIMMaterialInfo>();

			//6/5/24 - added filter to dropdowns, to show nutrients with or without x-refs  (true = WITH x-refs)		
			Boolean blnAimIsXref = true;
			if (rbAimFilterWithXrefProd.Checked == false)
			{
				blnAimIsXref = false;
			}
					
			//6/5/24 - added filter to dropdowns, to show nutrients with or without x-refs  (true = WITH x-refs)					
			//AimMaterial = AIMManager.GetAIMMaterial(true, (Int32)lueOwnerLocation_Material.EditValue, (Int32)MaterialTypeEnum.Product);
			AimMaterial = AIMManager.GetAIMMaterialProductWithOrMissingXref(true, blnAimIsXref);	

			AimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialCode = "--Select--",
					AIMMaterialName = "--Select--"
				}
			);

			lueMaterialCode.Properties.DataSource = AimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode");
			lueMaterialCode.Properties.Columns["AIMMaterialCode"].Visible = true;
			lueMaterialCode.Properties.Columns["AIMMaterialName"].Visible = true;

			lueMaterialName.Properties.DataSource = AimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName");
			lueMaterialName.Properties.Columns["AIMMaterialCode"].Visible = true;
			lueMaterialName.Properties.Columns["AIMMaterialName"].Visible = true;

			replueAIMMaterialCode.DataSource = AimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMMaterialCode, "AIMMaterialID", "AIMMaterialCode");
			replueAIMMaterialCode.Columns["AIMMaterialCode"].Visible = true;
			replueAIMMaterialCode.Columns["AIMMaterialName"].Visible = true;

			replueAIMMaterialName.DataSource = AimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMMaterialName, "AIMMaterialID", "AIMMaterialName");
			replueAIMMaterialName.Columns["AIMMaterialCode"].Visible = true;
			replueAIMMaterialName.Columns["AIMMaterialName"].Visible = true;

		}
				
		private void DataBindLueProduct()
		{
			List<CFCProductInfo> cfcProduct = new List<CFCProductInfo>();

			//6/5/24 - added filter to dropdowns, to show nutrients with or without x-refs  (true = WITH x-refs)
			Boolean blnFrmIsXref = true;
			if (rbFrmFilterWithXrefProd.Checked == false)
			{
				blnFrmIsXref = false;
			}

			//6/5/24 - added filter to dropdowns, to show products with or without x-refs  (true = WITH x-refs)			
			//cfcProduct = AIMManager.GetAllCFCProductAimDatabaseIDCFCPlantID(true, (Int32)_mainMenu.lueDataMap.EditValue, (Int32)luePlant_Product.EditValue);
			cfcProduct = AIMManager.GetAllCFCProductAimDatabaseIDWithOrMissingXref(true, (Int32)_mainMenu.lueDataMap.EditValue, blnFrmIsXref);

			cfcProduct.Insert
			(
				0,
				new CFCProductInfo
				{
					CFCProductID = 0,
					CFCProductCode = "--Select--",
					CFCProductName = "--Select--"				
				}
			);

			lueProductCode.Properties.DataSource = cfcProduct;
			XtraEditors.LoadLookUpEdit(ref lueProductCode, "CFCProductID", "CFCProductCode");
			lueProductCode.Properties.Columns["CFCProductCode"].Visible = true;
			lueProductCode.Properties.Columns["CFCProductName"].Visible = true;			
			lueProductCode.Properties.Columns["CFCProductCode"].Caption = "FRM Product Code";
			lueProductCode.Properties.Columns["CFCProductName"].Caption = "FRM Product Name";
			
			lueProductName.Properties.DataSource = cfcProduct;
			XtraEditors.LoadLookUpEdit(ref lueProductName, "CFCProductID", "CFCProductName");
			lueProductName.Properties.Columns["CFCProductCode"].Visible = true;
			lueProductName.Properties.Columns["CFCProductName"].Visible = true;
			lueProductName.Properties.Columns["CFCProductCode"].Caption = "FRM Product Code";
			lueProductName.Properties.Columns["CFCProductName"].Caption = "FRM Product Name";
			
		}

		private void DataBindReplueIngredient()
		{
			List<CFCIngredientInfo> cfcIngredient = new List<CFCIngredientInfo>();
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();           
						
            cfcIngredient = AIMManager.GetAllCFCIngredientAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
            aimMaterial = AIMManager.GetAIMMaterial(true);
   
			replueCFCCode.DataSource = cfcIngredient;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCCode, "CFCIngredientID", "CFCIngredientCode");
			replueCFCCode.Columns["CFCIngredientCode"].Visible = true;
			replueCFCCode.Columns["CFCIngredientName"].Visible = true;
			replueCFCCode.Columns["CFCIngredientCode"].Caption = "FRM Ingredient Code";
			replueCFCCode.Columns["CFCIngredientName"].Caption = "FRM Ingredient Name";

			replueCFCName.DataSource = cfcIngredient;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCName, "CFCIngredientID", "CFCIngredientName");
			replueCFCName.Columns["CFCIngredientCode"].Visible = true;
			replueCFCName.Columns["CFCIngredientName"].Visible = true;
			replueCFCName.Columns["CFCIngredientCode"].Caption = "FRM Ingredient Code";
			replueCFCName.Columns["CFCIngredientName"].Caption = "FRM Ingredient Name";

			replueAIMCode.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMCode, "AIMMaterialID", "AIMMaterialCode");
			replueAIMCode.Columns["AIMMaterialCode"].Visible = true;
			replueAIMCode.Columns["AIMMaterialName"].Visible = true;

			replueAIMName.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMMaterialID", "AIMMaterialName");
			replueAIMName.Columns["AIMMaterialCode"].Visible = true;
			replueAIMName.Columns["AIMMaterialName"].Visible = true;
		}
		private void DataBindReplueNutrient()
		{
			List<CFCNutrientInfo> cfcNutrient = new List<CFCNutrientInfo>();			
			List<AIMNutrientInfo> AimNutrient = new List<AIMNutrientInfo>();
						
            cfcNutrient = AIMManager.GetAllCFCNutrientAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
            AimNutrient = AIMManager.GetAllAIMNutrient(true);

            replueCFCCodeNutrient.DataSource = cfcNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCCodeNutrient, "CFCNutrientID", "CFCNutrientCode", "CFCNutrientName");
			replueCFCCodeNutrient.Columns["CFCNutrientID"].Visible = false;
			replueCFCCodeNutrient.Columns["CFCNutrientCode"].Caption = "FRM Nutrient Code";
			replueCFCCodeNutrient.Columns["CFCNutrientName"].Caption = "FRM Nutrient Name";


			replueCFCNameNutrient.DataSource = cfcNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCNameNutrient, "CFCNutrientID", "CFCNutrientName", "CFCNutrientCode");
			replueCFCNameNutrient.Columns["CFCNutrientCode"].Caption = "FRM Nutrient Code";
			replueCFCNameNutrient.Columns["CFCNutrientName"].Caption = "FRM Nutrient Name";

			replueAIMCodeNutrient.DataSource = AimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMCodeNutrient, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

			replueAIMNameNutrient.DataSource = AimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMNameNutrient, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

		}
		private void DataBindReplueOwner()
		{
			List<CFCPlantInfo> cfcplant = new List<CFCPlantInfo>();			
			cfcplant = AIMManager.GetCFCPlantAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);

			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
			aimOwner = AIMManager.GetAIMOwner(true);
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);

			replueCFCPlantCode.DataSource = cfcplant;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCPlantCode, "CFCPlantID", "CFCPlantCode");
			replueCFCPlantCode.Columns["CFCPlantCode"].Caption = "FRM Plant Code";
			replueCFCPlantCode.Columns["CFCPlantName"].Caption = "FRM Plant Name";

			replueCFCPlantName.DataSource = cfcplant;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCPlantName, "CFCPlantID", "CFCPlantName");
			replueCFCPlantName.Columns["CFCPlantCode"].Caption = "FRM Plant Code";
			replueCFCPlantName.Columns["CFCPlantName"].Caption = "FRM Plant Name";

			replueAIMLocationCode.DataSource = aimOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMLocationCode, "AIMOwnerID", "OwnerCode");
			replueAIMLocationName.DataSource = aimOwnerLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMLocationName, "AIMOwnerLocationID", "LocationName");

		}
		private void DataBindReplueProduct()
		{
			List<CFCProductInfo> cfcProduct = new List<CFCProductInfo>();
			cfcProduct = AIMManager.GetAllCFCProductAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);

			//1/19/22 - changed so product dropdown shows only products!
			//AIMMaterialInfo AimMaterial = new AIMMaterialInfo();
			//AimMaterial = AIMManager.GetAIMMaterial(true);
			List<AIMMaterialInfo> AimMaterial = new List<AIMMaterialInfo>();
			//AimMaterial = AIMManager.GetAIMMaterial_MaterialTypeID_IsActive((Int32)MaterialTypeEnum.Product, true);
			AimMaterial = AIMManager.GetAIMMaterial_MaterialTypeID_IsActive((Int32)MaterialTypeEnum.Product, true, 1);

			replueCFCProductCode.DataSource = cfcProduct;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCProductCode, "CFCProductID", "CFCProductCode");
			replueCFCProductCode.Columns["CFCProductCode"].Visible = true;
			replueCFCProductCode.Columns["CFCProductName"].Visible = true;
			replueCFCProductCode.Columns["CFCProductCode"].Caption = "FRM Product Code";
			replueCFCProductCode.Columns["CFCProductName"].Caption = "FRM Product Name";

			replueCFCProductName.DataSource = cfcProduct;
			XtraEditors.LoadGridLookUpEdit(ref replueCFCProductName, "CFCProductID", "CFCProductName");
			replueCFCProductName.Columns["CFCProductCode"].Visible = true;
			replueCFCProductName.Columns["CFCProductName"].Visible = true;
			replueCFCProductName.Columns["CFCProductCode"].Caption = "FRM Product Code";
			replueCFCProductName.Columns["CFCProductName"].Caption = "FRM Product Name";

			replueAIMMaterialCode.DataSource = AimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMMaterialCode, "AIMMaterialID", "AIMMaterialCode");
			replueAIMMaterialCode.Columns["AIMMaterialCode"].Visible = true;
			replueAIMMaterialCode.Columns["AIMMaterialName"].Visible = true;

			replueAIMMaterialName.DataSource = AimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMMaterialName, "AIMMaterialID", "AIMMaterialName");
			replueAIMMaterialName.Columns["AIMMaterialCode"].Visible = true;
			replueAIMMaterialName.Columns["AIMMaterialName"].Visible = true;
		}

		private void btnPreviewIngredient_Click(object sender, EventArgs e)
		{
			try
			{				
				CFCAIMCore.Global.previewGridResults(grdIngredientRef, "Formulation Ingredient Cross Reference");
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
				CFCAIMCore.Global.previewGridResults(grdNutrientRef, "Formulation Nutrient Cross Reference");
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
				CFCAIMCore.Global.previewGridResults(grdReferencePlant, "Formulation Owner Location Cross Reference");
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
				CFCAIMCore.Global.previewGridResults(this.grdProduct, "Formulation Product Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddIngredient_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueCFCCodeIngredient.EditValue == 0 || (Int32)lueAIMCodeMaterial.EditValue == 0)
				{
					MessageBox.Show("Please select both FRM Ingredient and AIM Material.", "Warning...");
					if ((Int32)lueCFCCodeIngredient.EditValue == 0)
					{
						lueCFCCodeIngredient.ShowPopup();
					}
					else
					{
						lueAIMCodeMaterial.ShowPopup();
					}

					return;
				}

				CFCIngredientRefAIMMaterialInfo ingredientRef = new CFCIngredientRefAIMMaterialInfo();
				ingredientRef.AIMMaterialID = (Int32)lueAIMCodeMaterial.EditValue;
				ingredientRef.CFCIngredientID = (Int32)lueCFCCodeIngredient.EditValue;
				ingredientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveCFCIngredientRefAIMMaterial(ref ingredientRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindGridIngredient();
				lueCFCCodeIngredient.EditValue = 0;
				lueAIMCodeMaterial.EditValue = 0;
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
				if ((Int32)lueFrmCodeNutrient.EditValue == 0 || (Int32)lueAIMCodeNutrient.EditValue == 0)
				{
					MessageBox.Show("Please select both FRM and AIM Nutrients.", "Warning...");

					if ((Int32)lueFrmCodeNutrient.EditValue == 0)
					{
						lueFrmCodeNutrient.ShowPopup();
					}
					else
					{
						lueAIMCodeNutrient.ShowPopup();
					}
					return;
				}
				CFCNutrientRefAIMNutrientInfo nutrientRef = new CFCNutrientRefAIMNutrientInfo();
				nutrientRef.CFCNutrientRefAIMNutrientID = 0;
				nutrientRef.AIMNutrientID = (Int32)lueAIMCodeNutrient.EditValue;
				nutrientRef.CFCNutrientID = (Int32)lueFrmCodeNutrient.EditValue;
				nutrientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveCFCNutrientRefAIMNutrient(ref nutrientRef);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindGridNutrient();
				lueFrmCodeNutrient.EditValue = 0;
				lueAIMCodeNutrient.EditValue = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnAddOwner_Click(object sender, EventArgs e)
		{
			try
			{
				CFCPlantRefAIMOwnerLocationInfo refCFCAIM = new CFCPlantRefAIMOwnerLocationInfo();
				Int32 cfcPlantID = 0;
				Int32 aimOwnerLocationID = 0;
				Int32.TryParse(lueAIMLocationCode.EditValue.ToString(), out aimOwnerLocationID);
				Int32.TryParse(lueCFCPlantCode.EditValue.ToString(), out cfcPlantID);

				if (cfcPlantID == 0)
				{
					MessageBox.Show("Please select FRM plant.", "Warning...");
					lueCFCPlantCode.ShowPopup();
					return;
				}

				if (aimOwnerLocationID == 0)
				{
					MessageBox.Show("Please select AIM owner location.", "Warning...");
					lueAIMLocationCode.ShowPopup();
					return;
				}

				if (IsCrossReferenceUnique(cfcPlantID, aimOwnerLocationID) == false)
				{
					MessageBox.Show("This combination already exists. Information is not saved.", "Warning...");
					return;
				}
				refCFCAIM.CFCPlantID = cfcPlantID;
				refCFCAIM.AIMOwnerLocationID = aimOwnerLocationID;
				refCFCAIM.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;


				AIMManager.InsertCFCPlantRefAIMOwnerLocation(ref refCFCAIM);
				DataBindGridOwner();

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
				if ((Int32)lueProductCode.EditValue == 0 || (Int32)lueMaterialCode.EditValue == 0)
				{
					MessageBox.Show("Please select both FRM Product and AIM Material.", "Warning...");

					if ((Int32)lueProductCode.EditValue == 0)
					{
						lueProductCode.ShowPopup();
					}
					else
					{
						lueMaterialCode.ShowPopup();
					}

					return;
				}
				CFCProductRefAIMMaterialInfo productRef = new CFCProductRefAIMMaterialInfo();
				productRef.AIMMaterialID = (Int32)lueMaterialCode.EditValue;
				productRef.CFCProductID = (Int32)lueProductCode.EditValue;
				productRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveCFCProductRefAIMMaterial(ref productRef);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Warning...");
					return;
				}
				DataBindGridProduct();
				lueProductCode.EditValue = 0;
				lueMaterialCode.EditValue = 0;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private Boolean IsCrossReferenceUnique(Int32 cfcPlantID, Int32 aimOwnerLocationID)
		{
			CFCPlantRefAIMOwnerLocationInfo crossReference = new CFCPlantRefAIMOwnerLocationInfo();
			Boolean result = true;

			crossReference = AIMManager.GetOneCFCPlantAIMOwnerLocation(cfcPlantID, aimOwnerLocationID);
			if (crossReference != null)
			{
				result = false;
			}

			return result;
		}
		private Boolean IsCFCPlantCodeUniqueInRefTable(CFCPlantRefAIMOwnerLocationInfo refTable)
		{
			CFCPlantRefAIMOwnerLocationInfo refTableLocal = new CFCPlantRefAIMOwnerLocationInfo();
			Boolean valid = true;

			refTableLocal = AIMManager.GetOneCFCPlantAIMSourceLocation(refTable.CFCPlantID);
			if (refTableLocal != null)
			{
				if (refTable.CFCPlantRefAIMOwnerLocationID != refTableLocal.CFCPlantRefAIMOwnerLocationID)
				{
					valid = false;
				}
			}
			return valid;
		}
		private Boolean IsCFCPlantUnique(CFCPlantInfo cfcPlant)
		{
			CFCPlantInfo cfcPlantLocal = new CFCPlantInfo();
			Boolean valid = true;

			cfcPlantLocal = AIMManager.GetCFCPlant(cfcPlant.CFCPlantCode);

			if (cfcPlantLocal != null)
			{
				if (cfcPlantLocal.CFCPlantID != cfcPlant.CFCPlantID)
				{
					valid = false;
				}
			}
			return valid;
		}
		private Boolean ValidateCFCPlant(String cfcPlantCode)
		{
			CFCPlantInfo crossReference = new CFCPlantInfo();
			Boolean result = true;

			crossReference = AIMManager.GetCFCPlant(cfcPlantCode);
			if (crossReference == null)
			{
				result = false;
			}

			return result;
		}
		private Boolean ValidateCrossReference(Int32 cfcPlantID, Int32 aimOwnerLocatonID)
		{
			CFCPlantRefAIMOwnerLocationInfo crossReference = new CFCPlantRefAIMOwnerLocationInfo();

			crossReference = AIMManager.GetOneCFCPlantAIMOwnerLocation(cfcPlantID, aimOwnerLocatonID);
			if (crossReference == null)
			{
				return false;
			}
			return true;
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
			ttip.SetToolTip(btnPreviewIngredient, "Preview Ingredient Cross-Reference");
			ttip.SetToolTip(btnPreviewNutrient, "Preview Nutrient Cross-Reference");
			ttip.SetToolTip(btnPreviewOwner, "Preview Owner Cross-Reference");
			ttip.SetToolTip(btnPreviewProduct, "Preview Product Cross-Reference");
			ttip.SetToolTip(btnAddIngredient, "Add Ingredient Cross-Reference");
			ttip.SetToolTip(btnAddNutrient, "Add Nutrient Cross-Reference");
			ttip.SetToolTip(btnAddOwner, "Add Owner Cross-Reference");
			ttip.SetToolTip(btnAddProduct, "Add Product Cross-Reference");
			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");
		}
		private void SyncValueLue(ref LookUpEdit from, ref LookUpEdit to)
		{
			if (from != null && to != null)
			{
				to.EditValue = from.EditValue;
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

		private void repbtnDeleteIngredient_Click(object sender, EventArgs e)
		{
			try
			{
				CFCIngredientRefAIMMaterialInfo ingredientRef = new CFCIngredientRefAIMMaterialInfo();
				ingredientRef = (CFCIngredientRefAIMMaterialInfo)grdIngredientRefView.GetFocusedRow();
				AIMManager.DeleteCFCIngredientRefAIMMaterial(ingredientRef.CFCIngredientRefAIMMaterialID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindGridIngredient();
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
				CFCNutrientRefAIMNutrientInfo nutrientRef = new CFCNutrientRefAIMNutrientInfo();
				nutrientRef = (CFCNutrientRefAIMNutrientInfo)grdNutrientRefView.GetFocusedRow();
				AIMManager.DeleteCFCNutrientRefAIMNutrient(nutrientRef.CFCNutrientRefAIMNutrientID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindGridNutrient();
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
				CFCPlantRefAIMOwnerLocationInfo refPlant = new CFCPlantRefAIMOwnerLocationInfo();
				refPlant = (CFCPlantRefAIMOwnerLocationInfo)grdReferencePlantView.GetFocusedRow();
				AIMManager.DeleteCFCPlantRefAIMOwnerLocation(refPlant.CFCPlantRefAIMOwnerLocationID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindGridOwner();
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
				CFCProductRefAIMMaterialInfo productRef = new CFCProductRefAIMMaterialInfo();
				productRef = (CFCProductRefAIMMaterialInfo)grdProductView.GetFocusedRow();
				AIMManager.DeleteCFCProductRefAIMMaterial(productRef.CFCProductRefAIMMaterialID, userGlobal.CFCLoginUser.CFCUserID);
				DataBindGridProduct();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void grdIngredientRefView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			try
			{
				CFCIngredientRefAIMMaterialInfo ingredientRef = new CFCIngredientRefAIMMaterialInfo();
				ingredientRef = (CFCIngredientRefAIMMaterialInfo)grdIngredientRefView.GetFocusedRow();
				ingredientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (ingredientRef.CFCIngredientID == 0 || ingredientRef.AIMMaterialID == 0)
				{
					MessageBox.Show("Need to select both FRM Ingredient and AIM Ingredient. Information is not saved", "Warning...");
					return;
				}
				AIMManager.SaveCFCIngredientRefAIMMaterial(ref ingredientRef);
				DataBindGridIngredient();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdNutrientRefView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			try
			{
				CFCNutrientRefAIMNutrientInfo nutrientRef = new CFCNutrientRefAIMNutrientInfo();
				nutrientRef = (CFCNutrientRefAIMNutrientInfo)grdNutrientRefView.GetFocusedRow();
				nutrientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (nutrientRef.CFCNutrientID == 0 || nutrientRef.AIMNutrientID == 0)
				{
					MessageBox.Show("Need to select both FRM Nutrient and AIM Nutrient. Information is not saved", "Warning...");
					return;
				}
				AIMManager.SaveCFCNutrientRefAIMNutrient(ref nutrientRef);
				DataBindGridNutrient();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdProductView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			try
			{

				CFCProductRefAIMMaterialInfo productRef = new CFCProductRefAIMMaterialInfo();
				productRef = (CFCProductRefAIMMaterialInfo)grdProductView.GetFocusedRow();
								
				if (productRef.AIMOwnerLocationID == 0)
				{
					if ((Int32)lueOwnerLocation_Material.EditValue != 0)
					{
						productRef.AIMOwnerLocationID = (Int32)lueOwnerLocation_Material.EditValue;
					}
					else
					{
						MessageBox.Show("Please select AIM Owner Location above. Information is not saved", "Warning...");
						DataBindGridProduct();
						return;
					}

				}

				productRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (productRef.CFCProductID == 0 || productRef.AIMMaterialID == 0)
				{
					MessageBox.Show("Need to select both FRM Product and AIM Material. Information is not saved", "Warning...");
					return;
				}

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveCFCProductRefAIMMaterial(ref productRef);
				if (error.ErrorNumber > 0)
				{
					MessageBox.Show(error.ErrorMessage, "Warning...");
				}
				DataBindGridProduct();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdReferencePlantView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			try
			{
				CFCPlantRefAIMOwnerLocationInfo refPlant = new CFCPlantRefAIMOwnerLocationInfo();
				refPlant = (CFCPlantRefAIMOwnerLocationInfo)grdReferencePlantView.GetFocusedRow();
				refPlant.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				if (refPlant.CFCPlantID == 0 || refPlant.AIMOwnerLocationID == 0)
				{
					MessageBox.Show("You have to select both.", "Warning...");
				}
				else if (IsCrossReferenceUnique(refPlant.CFCPlantID, refPlant.AIMOwnerLocationID) == false)
				{
					MessageBox.Show("This combination was already created. Information is not saved.", "Warning...");
					DataBindGridOwner();
				}
				else
				{
					if (IsCFCPlantCodeUniqueInRefTable(refPlant) == false)
					{
						MessageBox.Show("This FRM plant has already been assigned. Please choose another one.", "Warning...");
						DataBindGridOwner();
					}
					else
					{
						AIMManager.SaveCFCPlantRefAIMOwnerLocation(ref refPlant);
						DataBindGridOwner();
					}
				}
				DataBindGridOwner();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void replueCFCCode_EditValueChanging(object sender, ChangingEventArgs e)
		{
			try
			{
				grdIngredientRefView.SetFocusedRowCellValue(colCFCIngredientName, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAIMCode_EditValueChanging(object sender, ChangingEventArgs e)
		{
			try
			{
				grdIngredientRefView.SetFocusedRowCellValue(colAIMMaterialName, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueCFCName_EditValueChanging(object sender, ChangingEventArgs e)
		{
			try
			{
				grdIngredientRefView.SetFocusedRowCellValue(colCFCIngredientCode, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAIMName_EditValueChanging(object sender, ChangingEventArgs e)
		{
			try
			{
				grdIngredientRefView.SetFocusedRowCellValue(colAIMMaterialCode, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueCFCCodeIngredient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueCFCCodeIngredient, ref lueCFCNameIngredient);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueCFCNameIngredient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueCFCNameIngredient, ref lueCFCCodeIngredient);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMCodeMaterial_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMCodeMaterial, ref lueAIMNameMaterial);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMNameMaterial_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMNameMaterial, ref lueAIMCodeMaterial);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void replueCFCCodeNutrient_EditValueChanging(object sender, ChangingEventArgs e)
		{
			try
			{
				grdNutrientRefView.SetFocusedRowCellValue(colCFCNutrientName, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAIMCodeNutrient_EditValueChanging(object sender, ChangingEventArgs e)
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
		private void replueCFCNameNutrient_EditValueChanging(object sender, ChangingEventArgs e)
		{
			try
			{
				grdNutrientRefView.SetFocusedRowCellValue(colCFCNutrientCode, e.NewValue);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void replueAIMNameNutrient_EditValueChanging(object sender, ChangingEventArgs e)
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

		private void lueFrmCodeNutrient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueFrmCodeNutrient, ref lueFrmNameNutrient);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueFrmNameNutrient_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueFrmNameNutrient, ref lueFrmCodeNutrient);
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

		private void lueCFCProductCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueProductCode, ref lueProductName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueCFCProductName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueProductName, ref lueProductCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMCodeProduct_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueMaterialCode, ref lueMaterialName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMNameProduct_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueMaterialName, ref lueMaterialCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueCFCPlantCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueCFCPlantCode, ref lueCFCPlantName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueCFCPlantName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueCFCPlantName, ref lueCFCPlantCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerCode, ref lueOwnerName);
				DataBindLueOwnerLocation_Owner();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerName, ref lueOwnerCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMLocationCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMLocationCode, ref lueAIMLocationName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMLocationName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMLocationName, ref lueAIMLocationCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueCFCCodeIngredient_Click(object sender, EventArgs e)
		{
			lueCFCCodeIngredient.SelectAll();
		}

		private void lueCFCCodeIngredient_Enter(object sender, EventArgs e)
		{
			lueCFCCodeIngredient.SelectAll();
		}

		private void lueCFCNameIngredient_Enter(object sender, EventArgs e)
		{
			lueCFCNameIngredient.SelectAll();
		}

		private void lueCFCNameIngredient_Click(object sender, EventArgs e)
		{
			lueCFCNameIngredient.SelectAll();
		}

		private void lueAIMCodeMaterial_Click(object sender, EventArgs e)
		{
			lueAIMCodeMaterial.SelectAll();
		}

		private void lueAIMCodeMaterial_Enter(object sender, EventArgs e)
		{
			lueAIMCodeMaterial.SelectAll();
		}

		private void lueAIMNameMaterial_Enter(object sender, EventArgs e)
		{
			lueAIMNameMaterial.SelectAll();
		}

		private void lueAIMNameMaterial_Click(object sender, EventArgs e)
		{
			lueAIMNameMaterial.SelectAll();
		}

		private void lueFrmCodeNutrient_Enter(object sender, EventArgs e)
		{
			lueFrmCodeNutrient.SelectAll();
		}

		private void lueFrmCodeNutrient_Click(object sender, EventArgs e)
		{
			lueFrmCodeNutrient.SelectAll();
		}

		private void lueFrmNameNutrient_Click(object sender, EventArgs e)
		{
			lueFrmNameNutrient.SelectAll();
		}

		private void lueFrmNameNutrient_Enter(object sender, EventArgs e)
		{
			lueFrmNameNutrient.SelectAll();
		}

		private void lueAIMCodeNutrient_Enter(object sender, EventArgs e)
		{
			lueAIMCodeNutrient.SelectAll();
		}

		private void lueAIMCodeNutrient_Click(object sender, EventArgs e)
		{
			lueAIMCodeNutrient.SelectAll();
		}

		private void lueAIMNameNutrient_Click(object sender, EventArgs e)
		{
			lueAIMNameNutrient.SelectAll();
		}

		private void lueAIMNameNutrient_Enter(object sender, EventArgs e)
		{
			lueAIMNameNutrient.SelectAll();
		}

		private void lueCFCPlantCode_Enter(object sender, EventArgs e)
		{
			lueCFCPlantCode.SelectAll();
		}

		private void lueCFCPlantCode_Click(object sender, EventArgs e)
		{
			lueCFCPlantCode.SelectAll();
		}

		private void lueCFCPlantName_Click(object sender, EventArgs e)
		{
			lueCFCPlantName.SelectAll();
		}

		private void lueCFCPlantName_Enter(object sender, EventArgs e)
		{
			lueCFCPlantName.SelectAll();
		}

		private void lueOwnerCode_Enter(object sender, EventArgs e)
		{
			lueOwnerCode.SelectAll();
		}

		private void lueOwnerCode_Click(object sender, EventArgs e)
		{
			lueOwnerCode.SelectAll();
		}

		private void lueOwnerName_Click(object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}

		private void lueOwnerName_Enter(object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}

		private void lueAIMLocationCode_Click(object sender, EventArgs e)
		{
			lueAIMLocationCode.SelectAll();
		}

		private void lueAIMLocationCode_Enter(object sender, EventArgs e)
		{
			lueAIMLocationCode.SelectAll();
		}

		private void lueAIMLocationName_Enter(object sender, EventArgs e)
		{
			lueAIMLocationName.SelectAll();
		}

		private void lueAIMLocationName_Click(object sender, EventArgs e)
		{
			lueAIMLocationName.SelectAll();
		}

		private void btnResize_Click(Object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		private void FormulationCrossReference_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}

		private void lueOwner_Material_EditValueChanged(Object sender, EventArgs e)
		{
			DataBindLueOwnerLocation_Material();
		}

		private void luePlant_Product_EditValueChanged(Object sender, EventArgs e)
		{
			DataBindLueProduct();
		}

		private void lueOwnerLocation_Material_EditValueChanged(Object sender, EventArgs e)
		{
			DataBindLueMaterial();
		}
			

		private void repopulateMaterialDropdown()
		{
			//TODO 3/26/18 - NEED TO ADD THIS TO THE PRODUCT CODE AND NAME DROPDOWNS IN THE GRDPRODUCT -- JUST NOT SURE WHICH
			//EVENT TO CALL IT FROM.  TRIED BUTTON-CLICK AND ENTER. NO GOOD.
			//If record's aimownerlocation <> 0 and it's <> to the aimownerlocation dropdown
			// repopulate with materials for the aimOwnerLocationID for the record
			CFCProductRefAIMMaterialInfo productRef = new CFCProductRefAIMMaterialInfo();
			productRef = (CFCProductRefAIMMaterialInfo)grdProductView.GetFocusedRow();
			if (productRef.AIMOwnerLocationID != 0 && productRef.AIMOwnerLocationID != (Int32)lueOwnerLocation_Material.EditValue)
			{
				List<AIMMaterialInfo> AimMaterial = new List<AIMMaterialInfo>();
				AimMaterial = AIMManager.GetAIMMaterial(true, productRef.AIMOwnerLocationID, (Int32)MaterialTypeEnum.Product);

				AimMaterial.Insert
				(
					0,
					new AIMMaterialInfo
					{
						AIMMaterialID = 0,
						AIMMaterialCode = "--Select--",
						AIMMaterialName = "--Select--"
					}
				);
				replueAIMMaterialCode.DataSource = AimMaterial;
				XtraEditors.LoadGridLookUpEdit(ref replueAIMMaterialCode, "AIMMaterialID", "AIMMaterialCode");
				replueAIMMaterialName.DataSource = AimMaterial;
				XtraEditors.LoadGridLookUpEdit(ref replueAIMMaterialName, "AIMMaterialID", "AIMMaterialName");
			}
		}
				
		//-----------------------------------------------------------------------
		//1/5/22 - Ingredient filters       
        private void rbFrmFilterWithXrefIngr_Click(object sender, EventArgs e)
        {
			DataBindLueIngredientFRM();			
		}		
		private void rbFrmFilterMissingXrefIngr_Click(object sender, EventArgs e)
        {
			DataBindLueIngredientFRM();			
		}		
		private void rbAimFilterWithXrefIngr_Click(object sender, EventArgs e)
        {
			DataBindLueIngredientAIM();			
		}		
		private void rbAimFilterMissingXrefIngr_Click(object sender, EventArgs e)
        {
			DataBindLueIngredientAIM();			
		}


		//-----------------------------------------------------------------------
		//1/5/22 - Nutrient filters       
		private void rbFrmFilterWithXrefNut_Click(object sender, EventArgs e)
        {			
			DataBindLueNutrientFRM();
		}
        private void rbFrmFilterMissingXrefNut_Click(object sender, EventArgs e)
        {
			DataBindLueNutrientFRM();
		}
        private void rbAimFilterWithXrefNut_Click(object sender, EventArgs e)
        {
			DataBindLueNutrientAIM();
		}
        private void rbAimFilterMissingXrefNut_Click(object sender, EventArgs e)
        {
			DataBindLueNutrientAIM();
		}

		//-----------------------------------------------------------------------
		//1/5/22 - OwnerLocation filters      
		private void rbFrmFilterWithXrefLoc_Click(object sender, EventArgs e)
        {
			DataBindLueOwnerFRM();
		}
        private void rbFrmFilterMissingXrefLoc_Click(object sender, EventArgs e)
        {
			DataBindLueOwnerFRM();
		}
        private void rbAimFilterWithXrefLoc_Click(object sender, EventArgs e)
        {
			
			DataBindLueOwnerAIM();
			DataBindLueOwnerLocation_Owner();
		}
        private void rbAimFilterMissingXrefLoc_Click(object sender, EventArgs e)
        {			
			DataBindLueOwnerAIM();
			DataBindLueOwnerLocation_Owner();
		}

		//-----------------------------------------------------------------------
		//6/5/24 - Product filters      		
		private void rbFrmFilterWithXrefProd_Click(object sender, EventArgs e)
		{
			DataBindLueProduct();
		}		
		private void rbFrmFilterMissingXrefProd_Click(object sender, EventArgs e)
		{
			DataBindLueProduct();
		}
		private void rbAimFilterWithXrefProd_Click(object sender, EventArgs e)
		{
			DataBindLueMaterial();
		}
		private void rbAimFilterMissingXrefProd_Click(object sender, EventArgs e)
		{
			DataBindLueMaterial();
		}

		//-----------------------------------------------------------------------
		//4/12/22
		private void grdProductView_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
		{
			//Columns set to Custom SortMode: "colCFCProductCode"
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}

		private void luePlant_Product_Click(object sender, EventArgs e)
		{
			luePlant_Product.SelectAll();
		}

		private void luePlant_Product_Enter(object sender, EventArgs e)
		{
			luePlant_Product.SelectAll();
		}

		private void lueProductCode_Click(object sender, EventArgs e)
		{
			lueProductCode.SelectAll();
		}

		private void lueProductCode_Enter(object sender, EventArgs e)
		{
			lueProductCode.SelectAll();
		}

		private void lueProductName_Click(object sender, EventArgs e)
		{
			lueProductName.SelectAll();
		}

		private void lueProductName_Enter(object sender, EventArgs e)
		{
			lueProductName.SelectAll();
		}

		private void lueOwner_Material_Click(object sender, EventArgs e)
		{
			lueOwner_Material.SelectAll();
		}

		private void lueOwner_Material_Enter(object sender, EventArgs e)
		{
			lueOwner_Material.SelectAll();
		}

		private void lueOwnerLocation_Material_Click(object sender, EventArgs e)
		{
			lueOwnerLocation_Material.SelectAll();
		}

		private void lueOwnerLocation_Material_Enter(object sender, EventArgs e)
		{
			lueOwnerLocation_Material.SelectAll();
		}

		private void lueMaterialCode_Click(object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}

		private void lueMaterialCode_Enter(object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}

		private void lueMaterialName_Click(object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}

		private void lueMaterialName_Enter(object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}

		//6/5/24		
		private void btnUpdateXref_Click(object sender, EventArgs e)
		{
			DialogResult dr = new DialogResult();
			dr = MessageBox.Show("Create Product cross-references?", "Warning...", MessageBoxButtons.YesNoCancel);
			if (dr != DialogResult.Yes)
			{
				return;
			}

			//loop thru grid - add xrefs
			ErrorInfo error = new ErrorInfo();
			List<CFCProductRefAIMMaterialInfo> xref = new List<CFCProductRefAIMMaterialInfo>();
			xref = (List<CFCProductRefAIMMaterialInfo>)grdProdMissing.DataSource;
			if (xref != null)
			{
				foreach (var item in xref)
				{
					if (item.StatusDisplay == "")
					{
						//Create cross-ref						
						error = AIMManager.UpdateCFCProductAimMaterialWithMissingXref(item.CFCProductID, item.CFCProductCode, item.AIMOwnerLocationID, userGlobal.CFCLoginUser.CFCUserID);
					}
				}
			}
			//refresh grid				
			DataBindProductMissingXrefGrid();

		}

		//6/6/24
		private void repHyperStatus_Click(object sender, EventArgs e)
		{
			//set tab to plant
			try
			{
				tabControl1.SelectedIndex = 2;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}
	}
}
