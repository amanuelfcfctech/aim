using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using CFCAIM.ReportDevExpress;
using DevExpress.XtraReports.UI;
using Core.Library.DevExpress;


namespace CFCAIM.Maintenance
{
	public partial class InitialLoadAimNutrientsMaterials : Form
	{

		CFCUser.Global userGlobal = new CFCUser.Global();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		List<CFCNutrientInfo> _cfcNutrient = new List<CFCNutrientInfo>();
		List<CFCIngredientProductCombinedInfo> _cfcIngredientProductCombined = new List<CFCIngredientProductCombinedInfo>();

		public InitialLoadAimNutrientsMaterials()
		{
			InitializeComponent();
		}

		private void InitialLoadAimNutrients_Load(object sender, EventArgs e)
		{
			DataBindMaterials();
			DataBindNutrients();
			SetToolTips();

			//10/6/21 - added for resize issue
			/////WindowState = FormWindowState.Maximized;					
		}

		private void DataBindMaterials()
		{
			Int32 currentCFCPlantRow = 0;
			if (grdMaterialsView != null)
			{
				Int32.TryParse(grdMaterialsView.FocusedRowHandle.ToString(), out currentCFCPlantRow);
			}

			//If there happens to be some AimMaterials already set up, the stored procedure will bypass selecting those.
			grdMaterials.DataSource = AIMManager.GetAllCFCProductInitialLoadAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
			grdMaterialsView.FocusedRowHandle = currentCFCPlantRow;
			_cfcIngredientProductCombined = (List<CFCIngredientProductCombinedInfo>)grdMaterials.DataSource;
		}
		private void DataBindNutrients()
		{			
			Int32 currentCFCPlantRow = 0;
			if (grdNutrientsView != null)
			{
				Int32.TryParse(grdNutrientsView.FocusedRowHandle.ToString(), out currentCFCPlantRow);
			}

			//If there happens to be some AimNutrients already set up, the stored procedure will bypass selecting those.
			grdNutrients.DataSource = AIMManager.GetAllCFCNutrientInitialLoadAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
			grdNutrientsView.FocusedRowHandle = currentCFCPlantRow;
			_cfcNutrient = (List<CFCNutrientInfo>)grdNutrients.DataSource;
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
		private void SetToolTips()
		{
			try
			{
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
				Icon = ico;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void AddMaterials()
		{
			try
			{                
                AIMGlobal.SplashScreenShow();

				ErrorInfo result = new ErrorInfo();
				UnitTypeInfo units = new UnitTypeInfo();
				AIMMaterialInfo AIMMaterial = new AIMMaterialInfo();
				List<CFCIngredientProductCombinedInfo> cfcCombined = new List<CFCIngredientProductCombinedInfo>();
				cfcCombined = (List<CFCIngredientProductCombinedInfo>)grdMaterials.DataSource;

				Int32 row = 0;				
				Int32 aimMaterialID = 0;

				if (cfcCombined != null)
				{
					foreach (var item in cfcCombined)
					{
						if (cfcCombined[row].CreateMaterial == true)
						{							

							//----------------------------------------------------------
							//Save AIMMaterial							
							AIMMaterial.AIMMaterialID = 0;
							AIMMaterial.MaterialTypeID = cfcCombined[row].Type;

							//10/26/21
							AIMMaterial.AIMOwnerLocationID = 0;
                            if (cfcCombined[row].Type == (Int32)MaterialTypeEnum.Product)
                            {
								AIMMaterial.AIMOwnerLocationID = cfcCombined[row].PlantID;								
                            }

							//10/26/21
							//AIMMaterial.AIMMaterialCode = cfcCombined[row].Name;
							AIMMaterial.AIMMaterialCode = cfcCombined[row].Code;

							AIMMaterial.AIMMaterialName = cfcCombined[row].Name;							
							AIMMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
                            AIMMaterial.InitialLoad_Sw = true;
                            result = AIMManager.SaveAIMMaterial(ref AIMMaterial, true);

							aimMaterialID = result.RecordID;

                            //for initial load, if there are duplicates, the errorNumber is set to 2 (don't want to see error message for these)
                            //if (result.ErrorNumber > 0)
                            if (result.ErrorNumber == 1)
								{
								MessageBox.Show("Error saving AIMMaterial " + AIMMaterial.AIMMaterialName + ".", "Warning...");
								aimMaterialID = 0;
							}

							//----------------------------------------------------------
							//Save AIM/FRM Material Cross-Reference
							if (cfcCombined[row].CreateXref == true)
							{
								if (aimMaterialID != 0)
								{
									switch (cfcCombined[row].Type)
									{
										case (Int32)MaterialTypeEnum.Ingredient:
											CFCIngredientRefAIMMaterialInfo ingredientRef = new CFCIngredientRefAIMMaterialInfo();
											ingredientRef.CFCIngredientRefAIMMaterialID = 0;
											ingredientRef.AIMMaterialID = aimMaterialID;
											ingredientRef.CFCIngredientID = cfcCombined[row].ID;
											ingredientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
											result = AIMManager.SaveCFCIngredientRefAIMMaterial(ref ingredientRef);
											if (result.ErrorNumber > 0)
											{
												MessageBox.Show("Error saving ingredient cross-reference for AIMMaterial " + AIMMaterial.AIMMaterialName + ".", "Warning...");
												return;
											}
											break;
										case (Int32)MaterialTypeEnum.Product:
											CFCProductRefAIMMaterialInfo productRef = new CFCProductRefAIMMaterialInfo();
											productRef.CFCProductRefAIMMaterialID = 0;
											productRef.AIMMaterialID = aimMaterialID;
											productRef.CFCProductID = cfcCombined[row].ID;
											productRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
											result = AIMManager.SaveCFCProductRefAIMMaterial(ref productRef);
											if (result.ErrorNumber > 0)
											{
												MessageBox.Show("Error saving product cross-reference for AIMMaterial " + AIMMaterial.AIMMaterialName + ".", "Warning...");
												return;
											}
											break;
										default:
											break;
									}									
								}
							}
						}
						row++;
					}
				}

                //VERSION 2017-02-02 (2/1/17)
                AIMGlobal.SplashScreenHide();

				MessageBox.Show("Update Complete");
				DataBindMaterials();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void AddNutrients()
		{
			try
			{                
                AIMGlobal.SplashScreenShow();

				ErrorInfo result = new ErrorInfo();
				UnitTypeInfo units = new UnitTypeInfo();
				AIMNutrientInfo AIMNutrient = new AIMNutrientInfo();
				List<CFCNutrientInfo> nutrient = new List<CFCNutrientInfo>();
				nutrient = (List<CFCNutrientInfo>)grdNutrients.DataSource;

				Int32 row = 0;
				Int32 unitID = 0;
				Int32 aimNutrientID = 0;

				if (nutrient != null)
				{
					foreach (var item in nutrient)
					{
						if (nutrient[row].CreateNutrient == true)
						{
							//----------------------------------------------------------
							//Save UnitType
							unitID = 0;
							units.UnitTypeID = 0;
							units.UnitTypeCode = nutrient[row].Unit;
							units.UnitTypeDescription = nutrient[row].Unit;
							units.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							result = AIMManager.SaveUnitType(ref units, false);
							unitID = result.RecordID;

							//----------------------------------------------------------
							//Save AIMNutrient							
							AIMNutrient.AIMNutrientID = 0;

							//10/25/21
							//AIMNutrient.AIMNutrientCode = nutrient[row].CFCNutrientName;
							AIMNutrient.AIMNutrientCode = nutrient[row].CFCNutrientCode;

							AIMNutrient.AIMNutrientName = nutrient[row].CFCNutrientName;
							AIMNutrient.AIMNutrientShortName = "";
							AIMNutrient.UnitTypeID = unitID;
							AIMNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							AIMNutrient.PriorityAIMUsedValue = (Int32)PriorityUsedValue.Default;
							AIMNutrient.DataSourceTypeID = 0;  //populating with 0 because priorityUsedValue is set to default above
							result = AIMManager.SaveAIMNutrient(ref AIMNutrient, false);
							aimNutrientID = result.RecordID;
							if (result.ErrorNumber > 0)
							{
								MessageBox.Show("Error saving AIMNutrient " + AIMNutrient.AIMNutrientName + ".", "Warning...");
								aimNutrientID = 0;
							}

							//----------------------------------------------------------
							//Save AIM/FRM Nutrient Cross-Reference
							if (nutrient[row].CreateXref == true)
							{
								if (aimNutrientID != 0)
								{
									CFCNutrientRefAIMNutrientInfo nutrientRef = new CFCNutrientRefAIMNutrientInfo();
									nutrientRef.CFCNutrientRefAIMNutrientID = 0;
									nutrientRef.AIMNutrientID = aimNutrientID;
									nutrientRef.CFCNutrientID = nutrient[row].CFCNutrientID;
									nutrientRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
									result = AIMManager.SaveCFCNutrientRefAIMNutrient(ref nutrientRef);
									if (result.ErrorNumber > 0)
									{
										MessageBox.Show("Error saving cross-reference for AIMNutrient " + AIMNutrient.AIMNutrientName + ".", "Warning...");
										return;
									}
								}
							}
						}
						row++;
					}
				}
                
                AIMGlobal.SplashScreenHide();
				MessageBox.Show("Update Complete");
				DataBindNutrients();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnRun_Click(object sender, EventArgs e)
		{
			try
			{
				DialogResult dr = new DialogResult();				
				dr = MessageBox.Show("Running this update may affect current cross-references.  Are you sure you want to do this?", "Warning ...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);				
				if (dr != DialogResult.Yes)
				{
					return;
				}

				

				if (tabControl1.SelectedIndex == 0)
				{
					AddMaterials();
				}
				else if (tabControl1.SelectedIndex == 1)
				{
					AddNutrients();	
				}

				


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		

		private void grdLoadNutrientsView_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == "CreateXref")
			{
				if ((Boolean)e.Value == true)
				{
					grdNutrientsView.SetRowCellValue(e.RowHandle, "CreateNutrient", e.Value);	
				}						
			}
			if (e.Column.FieldName == "CreateNutrient")
			{
				if ((Boolean)e.Value == false)
				{
					grdNutrientsView.SetRowCellValue(e.RowHandle, "CreateXref", e.Value);
				}
			}
		}

		private void btnClearChecks_Click(object sender, EventArgs e)
		{	
			DataBindNutrients();
		}

		private void btnCheckAllNutrients_Click(object sender, EventArgs e)
		{
			foreach (var item in _cfcNutrient)
			{
				item.CreateNutrient = true;				
			}
			grdNutrients.DataSource = _cfcNutrient;
			grdNutrientsView.RefreshData();			
		}

		private void btnCheckAllCrossRef_Click(object sender, EventArgs e)
		{
			foreach (var item in _cfcNutrient)
			{				
				item.CreateXref = true;
				item.CreateNutrient = true;
			}
			grdNutrients.DataSource = _cfcNutrient;
			grdNutrientsView.RefreshData();			
		}

		private void grdNutrientsView_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == "CreateXref")
			{
				if ((Boolean)e.Value == true)
				{
					grdNutrientsView.SetRowCellValue(e.RowHandle, "CreateNutrient", e.Value);
				}
			}
			if (e.Column.FieldName == "CreateNutrient")
			{
				if ((Boolean)e.Value == false)
				{
					grdNutrientsView.SetRowCellValue(e.RowHandle, "CreateXref", e.Value);
				}
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			DataBindMaterials();
			DataBindNutrients();
			btnClearChecksNutrient.PerformClick();
			btnClearChecksMaterial.PerformClick();
		}

		private void grdMaterialsView_CellValueChanging(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == "CreateXref")
			{
				if ((Boolean)e.Value == true)
				{
					grdMaterialsView.SetRowCellValue(e.RowHandle, "CreateMaterial", e.Value);
				}
			}
			if (e.Column.FieldName == "CreateMaterial")
			{
				if ((Boolean)e.Value == false)
				{
					grdMaterialsView.SetRowCellValue(e.RowHandle, "CreateXref", e.Value);
				}
			}
		}

		private void btnClearChecksMaterial_Click(object sender, EventArgs e)
		{
			DataBindMaterials();
		}

		private void btnCheckAllMaterials_Click(object sender, EventArgs e)
		{
			foreach (var item in _cfcIngredientProductCombined)
			{
				item.CreateMaterial = true;
			}
			grdMaterials.DataSource = _cfcIngredientProductCombined;
			grdMaterialsView.RefreshData();		
		}

		private void btnCheckAllCrossRefMaterials_Click(object sender, EventArgs e)
		{
			foreach (var item in _cfcIngredientProductCombined)
			{
				item.CreateXref = true;
				item.CreateMaterial = true;
			}
			grdMaterials.DataSource = _cfcIngredientProductCombined;
			grdMaterialsView.RefreshData();	
		}

		
    }
}
