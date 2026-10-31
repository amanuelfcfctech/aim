using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

namespace CFCAIM.CrossReference
{
	public partial class ImportExternalCrossReferenceUpdate : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		Int32 _numCharacters = 0;
		Boolean _allowAIMInactive = true;

		//3/2/21 commented, not used
		//Boolean _blnCombined = false;

		public ImportExternalCrossReferenceUpdate()
		{
			InitializeComponent();
		}

		private void ExternalAllCrossRefUpdate_Load(object sender, EventArgs e)
		{

			//12/24/20
			//Check to see if this import uses "Combined" Source and Source-Location. (If so, the Source tab is hidden, caption is changed, etc)
			UserDefinedDataSourceTemplateInfo temp = new UserDefinedDataSourceTemplateInfo();
			temp = AIMManager.GetUserDefinedDataSourceTemplateMapColumnNameTop(AIMGlobal.userDefinedDefID, "SupplierLocationCombined");
			if (temp != null)
			{
				//3/2/21 commented, not used
				//_blnCombined = true;

				tabControl1.TabPages.Remove(tpSupplier);
				tabControl1.TabPages.Remove(tpSupplierLocation);
			}

            //8/6/21 - hide CombinedSupplier tab
            else
            {
				tabControl1.TabPages.Remove(tpSupplierLocCombined);
			}


			DataBindMaterial();			

			DataBindOwner();
			DataBindOwnerLocation();

			DataBindSupplier();
			DataBindSupplierLocation();

			//12/28/20
			DataBindSupplierLocationCombined();

			SetToolTips();
			RemoveTabs();            

            SystemSettingsInfo systemSettings = new SystemSettingsInfo();
			systemSettings = AIMManager.GetSystemSettings();
			if (systemSettings != null)
			{
				_allowAIMInactive = systemSettings.AllowAIMInactive;
			}

			//12/30/20
			if (grdMaterialView.RowCount > 0)
			{
				DataBindMaterialType();
				DataBindMaterialLue();		//--> this takes a long time
			}

			//hiding this button until code is added to filter for product or ingredient. If ingredient, then button can be shown (because there will be only 1 material code for a material name).  
			//                                                                            If product, hide (because the same material name may exist at multiple plants.)
			btnMaterialQuickAddAll.Visible = false;
            lueMaterialType.ShowPopup();		           
            WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
		}

		private void RemoveTabs()
		{

			if (grdMaterialView.RowCount == 0)
			{
				tabControl1.TabPages.Remove(tpMaterial);			
			}
			if (grdOwnerView.RowCount == 0)
			{
				tabControl1.TabPages.Remove(tpOwner);
			}
			if (grdOwnerLocationView.RowCount == 0)
			{
				tabControl1.TabPages.Remove(tpOwnerLocation);
			}
			if (grdSupplierView.RowCount == 0)
			{
				tabControl1.TabPages.Remove(tpSupplier);
			}
			if (grdSupplierLocationView.RowCount == 0)
			{
				tabControl1.TabPages.Remove(tpSupplierLocation);
			}

			//12/28/20
			if (grdSupplierLocationCombinedView.RowCount == 0)
			{
				tabControl1.TabPages.Remove(tpSupplierLocCombined);
			}
		}
		private void SetToolTips()
		{
			try
			{
				Icon = (Icon)Properties.Resources.CFCAIM;
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

		private void RefreshMaterial()
		{
			List<AIMMaterialRefAnalyzerMaterialInfo> materialResults = new List<AIMMaterialRefAnalyzerMaterialInfo>();			
			for (int i = 0; i < grdMaterialView.DataRowCount; i++)
			{
				if (grdMaterialView.GetRowCellValue(i, "AIMMaterialID").ToString() == "0")
				{
					materialResults.Add(new AIMMaterialRefAnalyzerMaterialInfo
					{
						AnalyzerMaterialName = grdMaterialView.GetRowCellValue(i, "AnalyzerMaterialName").ToString(),
						AnalyzerMaterialID = (Int32)grdMaterialView.GetRowCellValue(i, "AnalyzerMaterialID"),
						AIMMaterialID = 0
					});
				}
			}
			grdMaterial.DataSource = materialResults;
		}
		private void RefreshOwner()
		{
			List<AIMOwnerRefAnalyzerOwnerInfo> ownerResults = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			for (int i = 0; i < grdOwnerView.DataRowCount; i++)
			{
				if (grdOwnerView.GetRowCellValue(i, "AIMOwnerID").ToString() == "0")
				{
					ownerResults.Add(new AIMOwnerRefAnalyzerOwnerInfo
					{
						AnalyzerOwnerName = grdOwnerView.GetRowCellValue(i, "AnalyzerOwnerName").ToString(),
						AnalyzerOwnerID = (Int32)grdOwnerView.GetRowCellValue(i, "AnalyzerOwnerID"),
						AIMOwnerID = 0
					});
				}

			}
			grdOwner.DataSource = ownerResults;
		}
		private void RefreshOwnerLocation()
		{
			List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> ownerLocResults = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
			for (int i = 0; i < grdOwnerLocationView.DataRowCount; i++)
			{
				if (grdOwnerLocationView.GetRowCellValue(i, "AIMOwnerLocationID").ToString() == "0")
				{
					ownerLocResults.Add(new AIMOwnerLocationRefAnalyzerOwnerLocationInfo
					{
						AnalyzerOwnerLocationName = grdOwnerLocationView.GetRowCellValue(i, "AnalyzerOwnerLocationName").ToString(),
						AnalyzerOwnerLocationID = (Int32)grdOwnerLocationView.GetRowCellValue(i, "AnalyzerOwnerLocationID"),
						AIMOwnerLocationID = 0
					});
				}
			}
			grdOwnerLocation.DataSource = ownerLocResults;
		}
		private void RefreshSupplier()
		{
			List<AIMSupplierRefAnalyzerSupplierInfo> supplierResults = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			for (int i = 0; i < grdSupplierView.DataRowCount; i++)
			{
				if (grdSupplierView.GetRowCellValue(i, "AIMSupplierID").ToString() == "0")
				{
					supplierResults.Add(new AIMSupplierRefAnalyzerSupplierInfo
					{
						AnalyzerSupplierName = grdSupplierView.GetRowCellValue(i, "AnalyzerSupplierName").ToString(),
						AnalyzerSupplierID = (Int32)grdSupplierView.GetRowCellValue(i, "AnalyzerSupplierID"),
						AIMSupplierID = 0
					});
				}
			}
			grdSupplier.DataSource = supplierResults;
		}
		private void RefreshSupplierLocation()
		{
			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> supplierLocResults = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			for (int i = 0; i < grdSupplierLocationView.DataRowCount; i++)
			{
				if (grdSupplierLocationView.GetRowCellValue(i, "AIMSupplierLocationID").ToString() == "0")
				{
					supplierLocResults.Add(new AIMSupplierLocationRefAnalyzerSupplierLocationInfo
					{
						AnalyzerSupplierLocationName = grdSupplierLocationView.GetRowCellValue(i, "AnalyzerSupplierLocationName").ToString(),
						AnalyzerSupplierLocationID = (Int32)grdSupplierLocationView.GetRowCellValue(i, "AnalyzerSupplierLocationID"),
						AIMSupplierLocationID = 0
					});
				}
			}
			grdSupplierLocation.DataSource = supplierLocResults;
		}

		//12/24/20
		private void RefreshSupplierLocationCombined()
		{
			List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> supplierLocResults = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
			for (int i = 0; i < grdSupplierLocationCombinedView.DataRowCount; i++)
			{
				if (grdSupplierLocationCombinedView.GetRowCellValue(i, "AIMSupplierLocationID").ToString() == "0")
				{
					supplierLocResults.Add(new AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo
					{
						AnalyzerSupplierLocationCombinedName = grdSupplierLocationCombinedView.GetRowCellValue(i, "AnalyzerSupplierLocationCombinedName").ToString(),						
						AnalyzerSupplierLocationCombinedID = (Int32)grdSupplierLocationCombinedView.GetRowCellValue(i, "AnalyzerSupplierLocationCombinedID"),
						AIMSupplierLocationID = 0
					});


				}
			}
			grdSupplierLocationCombined.DataSource = supplierLocResults;
		}

		//12/30/20 - separated from DataBindMaterial 
		private void DataBindMaterialLue()
		{
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			aimMaterial = AIMManager.GetAIMMaterial(true);
			aimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialName = "--Select--",

					//11/27/19
					AIMOwnerLocationName = "--Select--"
				}
			);
			replueAIMName.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMMaterialID", "AIMMaterialName", "AIMOwnerLocationName");

		}

		private void DataBindMaterial()
		{
			
			//12/30/20 - moved to its own function
			//List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			//aimMaterial = AIMManager.GetAIMMaterial(true);
			//aimMaterial.Insert
			//(
			//	0,
			//	new AIMMaterialInfo
			//	{
			//		AIMMaterialID = 0,
			//		AIMMaterialName = "--Select--",
   //                 AIMOwnerLocationName = "--Select--"
			//	}
			//);
			//replueAIMName.DataSource = aimMaterial;
   //         XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMMaterialID", "AIMMaterialName","AIMOwnerLocationName");


            List<AIMMaterialRefAnalyzerMaterialInfo> materialResults = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			foreach (ImportHeaderInfo item in CFCAIM.AIMGlobal.crossRef)
			{				
				if (item.MaterialID == 0 && item.XrefMaterialStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _materialResults. If MaterialName already exists, skip add
					var results = from g in materialResults
								  where g.AnalyzerMaterialName == item.MaterialName
								  select g;
					if (results.Count() == 0)
					{
						materialResults.Add(new AIMMaterialRefAnalyzerMaterialInfo
						{
							AnalyzerMaterialName = item.MaterialName,	
							AnalyzerMaterialCode = item.MaterialCode,
							AnalyzerMaterialID = item.AnalyzerMaterialID,
							AIMMaterialID = 0,
							MaterialTypeID = 0
						});
					}
				}
			}
			int rowNum = 0;
			if (materialResults.Count() != 0)
			{
				grdMaterial.DataSource = materialResults;
				grdMaterialView.Columns["AnalyzerMaterialName"].Caption = AIMGlobal.analyzerDataSourceName + " Material";
				Int32.TryParse(grdMaterialView.FocusedRowHandle.ToString(), out rowNum);
				grdMaterialView.FocusedRowHandle = rowNum;				
			}
			else
			{
				tabControl1.TabPages.Remove(tpMaterial);
			}
		}
		private void DataBindMaterialType()
		{
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

            //11/27/19
            lueMaterialType.Properties.DataSource = material;
            XtraEditors.LoadLookUpEdit(ref lueMaterialType, "MaterialTypeID", "MaterialTypeName");
            lueMaterialType.Properties.Columns["MaterialTypeName"].Visible = true;

            replueMaterialType.DataSource = material;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterialType, "MaterialTypeID", "MaterialTypeName");
			replueMaterialType.Columns["MaterialTypeName"].Visible = true;

            


        }

        //11/27/19
        private void DataBindMaterialByType()
        {

            List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
            
			//10/13/23
			//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)lueMaterialType.EditValue);
			aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)lueMaterialType.EditValue, (Int32)EnumOrderBy.Name);

			if ((Int32)lueMaterialType.EditValue == (Int32)MaterialTypeEnum.Ingredient)            {
                
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
                replueAIMName.DataSource = aimMaterial;
                XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");
            }
            else
            {                
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
                replueAIMName.DataSource = aimMaterial;

                //11/29/19
                XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMOwnerLocationName");
                
                
            }
            
            

            List<AIMMaterialRefAnalyzerMaterialInfo> materialResults = new List<AIMMaterialRefAnalyzerMaterialInfo>();
            foreach (ImportHeaderInfo item in CFCAIM.AIMGlobal.crossRef)
            {
                if (item.MaterialID == 0 && item.XrefMaterialStatus == 1 && item.Message == "Missing Cross-Reference")
                {
                    //query _materialResults. If MaterialName already exists, skip add
                    var results = from g in materialResults
                                  where g.AnalyzerMaterialName == item.MaterialName
                                  select g;
                    if (results.Count() == 0)
                    {
                        materialResults.Add(new AIMMaterialRefAnalyzerMaterialInfo
                        {
                            AnalyzerMaterialName = item.MaterialName,
                            AnalyzerMaterialCode = item.MaterialCode,
                            AnalyzerMaterialID = item.AnalyzerMaterialID,
                            AIMMaterialID = 0,
                            MaterialTypeID = 0
                        });
                    }
                }
            }
            int rowNum = 0;
            if (materialResults.Count() != 0)
            {
                grdMaterial.DataSource = materialResults;
                grdMaterialView.Columns["AnalyzerMaterialName"].Caption = AIMGlobal.analyzerDataSourceName + " Material";
                Int32.TryParse(grdMaterialView.FocusedRowHandle.ToString(), out rowNum);
                grdMaterialView.FocusedRowHandle = rowNum;
            }
            else
            {
                tabControl1.TabPages.Remove(tpMaterial);
            }


            //replueAIMName

        }

        private void DataBindOwner()
		{
			List<AIMOwnerRefAnalyzerOwnerInfo> ownerResults = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
			aimOwner = AIMManager.GetAIMOwner(true);
			aimOwner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerName = "--Select--"
				}
			);
			replueAIMOwner.DataSource = aimOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMOwner, "AIMOwnerID", "OwnerName");

			foreach (ImportHeaderInfo item in CFCAIM.AIMGlobal.crossRef)
			{
				if (item.OwnerID == 0 && item.XrefOwnerStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _ownerResults. If OwnerName already exists, skip add
					var results = from g in ownerResults
								  where g.AnalyzerOwnerName == item.OwnerName
								  select g;
					if (results.Count() == 0)
					{
						ownerResults.Add(new AIMOwnerRefAnalyzerOwnerInfo
						{
							AnalyzerOwnerName = item.OwnerName,
							AnalyzerOwnerCode = item.OwnerCode,
							AnalyzerOwnerID = item.AnalyzerOwnerID,
							AIMOwnerID = 0
						});
					}

				}
			}
			int rowNum = 0;
			if (ownerResults.Count() != 0)
			{
				grdOwner.DataSource = ownerResults;
				grdOwnerView.Columns["AnalyzerOwnerName"].Caption = AIMGlobal.analyzerDataSourceName + " Owner";
				Int32.TryParse(grdOwnerView.FocusedRowHandle.ToString(), out rowNum);
				grdOwnerView.FocusedRowHandle = rowNum;
				txtOwnerNumber.Text = "0";
			}
			else
			{
				tabControl1.TabPages.Remove(tpOwner);
			}
		}
		private void DataBindOwnerLocation()
		{
			List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> ownerLocationResults = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();

			//10/13/16
			//aimOwner = AIMManager.GetAIMOwner(true);
			aimOwner = AIMManager.GetAIMOwnerWithValidOwnerLocations(true);	  
			
			aimOwner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerName = "--Select--"
				}
			);
			replueAimOwner_OwnerLocation.DataSource = aimOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAimOwner_OwnerLocation, "AIMOwnerID", "OwnerName");

			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);
			aimOwnerLocation.Insert
			(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					LocationName = "--Select--"
				}
			);
			replueAIMOwnerLocation.DataSource = aimOwnerLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMOwnerLocation, "AIMOwnerLocationID", "LocationName");

			foreach (ImportHeaderInfo item in CFCAIM.AIMGlobal.crossRef)
			{
				if (item.OwnerLocationID == 0 && item.XrefOwnerLocationStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _ownerlocResults. If OwnerLocationName already exists, skip add
					var results = from g in ownerLocationResults
								  where g.AnalyzerOwnerLocationName == item.OwnerLocationName
								  select g;
					if (results.Count() == 0)
					{
						ownerLocationResults.Add(new AIMOwnerLocationRefAnalyzerOwnerLocationInfo
						{
                                                       
                            //4/2/18
                            AnalyzerOwnerName = item.OwnerName,

                            AnalyzerOwnerLocationName = item.OwnerLocationName,
							AnalyzerOwnerLocationCode = item.OwnerLocationCode,
							AnalyzerOwnerLocationID = item.AnalyzerOwnerLocationID,
							AnalyzerOwnerID = item.AnalyzerOwnerID,
							AIMOwnerLocationID = 0,
							AIMOwnerID = 0
						});
					}
				}
			}

			int rowNum = 0;
			if (ownerLocationResults.Count() != 0)
			{
				grdOwnerLocation.DataSource = ownerLocationResults;
				grdOwnerLocationView.Columns["AnalyzerOwnerLocationName"].Caption = AIMGlobal.analyzerDataSourceName + " Owner Location";

                //4/2/18
                ////grdOwnerLocationView.Columns["AnalyzerOwnerName"].Caption = AIMGlobal.analyzerDataSourceName + " Owner";

                Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out rowNum);
				grdOwnerLocationView.FocusedRowHandle = rowNum;
				txtOwnerLocationNumber.Text = "0";
			}
			else
			{
				tabControl1.TabPages.Remove(tpOwnerLocation);
			}
		}
		private void DataBindSupplier()
		{
			List<AIMSupplierRefAnalyzerSupplierInfo> supplierResults = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();

			//10/13/16
			//aimSupplier = AIMManager.GetSupplier(true);
			aimSupplier = AIMManager.GetAIMSupplierWithValidSupplierLocations(true);

			aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					SupplierName = "--Select--"
				}
			);
			replueAIMSupplier.DataSource = aimSupplier;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMSupplier, "AIMSupplierID", "SupplierName");

			foreach (ImportHeaderInfo item in CFCAIM.AIMGlobal.crossRef)
			{
				if (item.SourceID == 0 && item.XrefSupplierStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _supplierResults. If SourceName already exists, skip add
					var results = from g in supplierResults
								  where g.AnalyzerSupplierID == item.UserDefinedSupplierID
								  select g;
					if (results.Count() == 0)
					{
						supplierResults.Add(new AIMSupplierRefAnalyzerSupplierInfo
						{
							AnalyzerSupplierName = item.SourceName,
							AnalyzerSupplierCode = item.SourceCode,
							AnalyzerSupplierID = item.UserDefinedSupplierID,
							AIMSupplierID = 0
						});
					}
				}
			}
			int rowNum = 0;
			if (supplierResults.Count() != 0)
			{
				grdSupplier.DataSource = supplierResults;
				grdSupplierView.Columns["AnalyzerSupplierName"].Caption = AIMGlobal.analyzerDataSourceName + " Supplier";
				Int32.TryParse(grdSupplierView.FocusedRowHandle.ToString(), out rowNum);
				grdSupplierView.FocusedRowHandle = rowNum;
				txtSupplierNumber.Text = "0";
			}
			else
			{
				tabControl1.TabPages.Remove(tpSupplier);
			}
		}
		private void DataBindSupplierLocation()
		{
			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> supplierLocationResults = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
			aimSupplier = AIMManager.GetSupplier(true);
			aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					SupplierName = "--Select--"
				}
			);

			replueAimSupplier_SupplierLocation.DataSource = aimSupplier;
			XtraEditors.LoadGridLookUpEdit(ref replueAimSupplier_SupplierLocation, "AIMSupplierID", "SupplierName");

			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			aimSupplierLocation = AIMManager.GetAllSupplierLocation(true);
			aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					SupplierLocationName = "--Select--"
				}
			);
			replueAIMSupplierLocation.DataSource = aimSupplierLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMSupplierLocation, "AIMSupplierLocationID", "SupplierLocationName");

			foreach (ImportHeaderInfo item in CFCAIM.AIMGlobal.crossRef)
			{
				if (item.SourceLocationID == 0 && item.XrefSupplierLocationStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _supplierlocResults. If SupplierLocationName already exists, skip add
					var results = from g in supplierLocationResults
								  where g.AnalyzerSupplierLocationName == item.SourceLocationName
								  select g;
					if (results.Count() == 0)
					{
						supplierLocationResults.Add(new AIMSupplierLocationRefAnalyzerSupplierLocationInfo
						{                            
                            AnalyzerSupplierName = item.SourceName,
							AnalyzerSupplierLocationName = item.SourceLocationName,
							AnalyzerSupplierLocationCode = item.SourceLocationCode,
							AnalyzerSupplierLocationID = item.UserDefinedSupplierLocationID,
							AnalyzerSupplierID = item.UserDefinedSupplierID,
							AIMSupplierLocationID = 0,
							AIMSupplierID = 0
						});
					}
				}
			}

			int rowNum = 0;
			if (supplierLocationResults.Count() != 0)
			{				
				grdSupplierLocationView.Columns["AnalyzerSupplierLocationName"].Caption = AIMGlobal.analyzerDataSourceName + " Supplier Location";
				grdSupplierLocationView.Columns["AnalyzerSupplierName"].Visible = true;
				grdSupplierLocationView.Columns["AnalyzerSupplierName"].Caption = AIMGlobal.analyzerDataSourceName + " Supplier";				
				grdSupplierLocation.DataSource = supplierLocationResults;								
				Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out rowNum);
				grdSupplierLocationView.FocusedRowHandle = rowNum;
				txtSupplierLocationNumber.Text = "0";
			}
			else
			{
				tabControl1.TabPages.Remove(tpSupplierLocation);
			}

		}

		//12/28/20
		private void DataBindSupplierLocationCombined()
		{
			List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> supplierLocCombinedResults = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();

			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
			aimSupplier = AIMManager.GetSupplier(true);
			aimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					SupplierName = "--Select--"
				}
			);

			replueAimSupplier_SupplierLocationCombined.DataSource = aimSupplier;
			XtraEditors.LoadGridLookUpEdit(ref replueAimSupplier_SupplierLocationCombined, "AIMSupplierID", "SupplierName");

			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			aimSupplierLocation = AIMManager.GetAllSupplierLocation(true);
			aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,					
					SupplierLocationName = "--Select--",
					SourceName = "--Select--"
				}
			);
			replueAIMSupplierLocationCombined.DataSource = aimSupplierLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMSupplierLocationCombined, "AIMSupplierLocationID","SupplierLocationName", "SourceName" );

			foreach (ImportHeaderInfo item in CFCAIM.AIMGlobal.crossRef)
			{
				if (item.SourceLocationID == 0 && item.XrefSupplierLocationStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query supplierLocCombinedResults. If SupplierLocationName already exists, skip add
					var results = from g in supplierLocCombinedResults
								  where g.AnalyzerSupplierLocationCombinedName == item.SourceLocationName
								  select g;
					if (results.Count() == 0)
					{
						supplierLocCombinedResults.Add(new AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo	
						{																				
							AnalyzerSupplierLocationCombinedID = item.SupplierLocationCombinedID,
							AnalyzerSupplierLocationCombinedName = item.SourceLocationName,
							AIMSupplierLocationID = 0,
							AIMSupplierID = 0
						});
					}
				}
			}

			int rowNum = 0;
			if (supplierLocCombinedResults.Count() != 0)
			{
				
				grdSupplierLocationCombinedView.Columns["AnalyzerSupplierLocationCombinedName"].Caption = AIMGlobal.analyzerDataSourceName + " Supplier Location Combined";				
				grdSupplierLocationCombined.DataSource = supplierLocCombinedResults;
				Int32.TryParse(grdSupplierLocationCombinedView.FocusedRowHandle.ToString(), out rowNum);
				grdSupplierLocationCombinedView.FocusedRowHandle = rowNum;				
			}
			else
			{
				tabControl1.TabPages.Remove(tpSupplierLocCombined);
			}

		}

		private void btnUpdateMaterial_Click(object sender, EventArgs e)
		{
			AIMMaterialRefAnalyzerMaterialInfo xref = new AIMMaterialRefAnalyzerMaterialInfo();
			List<AIMMaterialRefAnalyzerMaterialInfo> gridMaterial = new List<AIMMaterialRefAnalyzerMaterialInfo>();
			gridMaterial = (List<AIMMaterialRefAnalyzerMaterialInfo>)grdMaterial.DataSource;
			foreach (AIMMaterialRefAnalyzerMaterialInfo item in gridMaterial)
			{
				if (item.AIMMaterialID != 0)
				{
					xref.AIMMaterialRefAnalyzerMaterialID = 0;
					xref.AnalyzerMaterialID = item.AnalyzerMaterialID;
					xref.AIMMaterialID = item.AIMMaterialID;
					xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					xref.UpdateStamp = DateTime.Now;
					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAIMMaterialRefAnalyzerMaterial(ref xref);
					if (error.ErrorNumber > 0)
					{
						return;
					}
					else
					{			  					
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (item.AnalyzerMaterialName == CFCAIM.AIMGlobal.crossRef[i].MaterialName)
							{
								CFCAIM.AIMGlobal.crossRef[i].MaterialID = item.AIMMaterialID;
							}
						}
					}
				}
			}
			RefreshMaterial();
			RemoveTabs();
		}
		private void btnUpdateOwner_Click(object sender, EventArgs e)
		{
			AIMOwnerRefAnalyzerOwnerInfo xref = new AIMOwnerRefAnalyzerOwnerInfo();
			List<AIMOwnerRefAnalyzerOwnerInfo> gridOwner = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			gridOwner = (List<AIMOwnerRefAnalyzerOwnerInfo>)grdOwner.DataSource;
			foreach (AIMOwnerRefAnalyzerOwnerInfo item in gridOwner)
			{
				if (item.AIMOwnerID != 0)
				{
					xref.AIMOwnerRefAnalyzerOwnerID = 0;
					xref.AnalyzerOwnerID = item.AnalyzerOwnerID;
					xref.AIMOwnerID = item.AIMOwnerID;
					xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					xref.UpdateStamp = DateTime.Now;
					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAIMOwnerRefAnalyzerOwner(ref xref);
					if (error.ErrorNumber > 0)
					{
						return;
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (item.AnalyzerOwnerName == CFCAIM.AIMGlobal.crossRef[i].OwnerName)
							{
								CFCAIM.AIMGlobal.crossRef[i].OwnerID = item.AIMOwnerID;
							}
						}
					}
				}
			}

			//----------------------------------------------------------------------
			//Use number entered by user to match up first characters for cross-ref	
			if (Int32.TryParse(txtOwnerNumber.Text, out _numCharacters) == true && _numCharacters != 0)
			{
				String shortName = "";
				foreach (AIMOwnerRefAnalyzerOwnerInfo item in gridOwner)
				{
					if (item.AnalyzerOwnerName.Length >= _numCharacters)

					{
						shortName = item.AnalyzerOwnerName.Substring(0, _numCharacters);
						AIMOwnerInfo aim = new AIMOwnerInfo();
						aim = AIMManager.GetOneOwner(shortName, _numCharacters);
						if (aim != null)
						{
							xref.AIMOwnerRefAnalyzerOwnerID = item.AIMOwnerRefAnalyzerOwnerID;
							xref.AnalyzerOwnerID = item.AnalyzerOwnerID;
							xref.AIMOwnerID = aim.AIMOwnerID;
							xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							xref.UpdateStamp = DateTime.Now;
							ErrorInfo error = new ErrorInfo();
							error = AIMManager.SaveAIMOwnerRefAnalyzerOwner(ref xref);
							if (error.ErrorNumber > 0)
							{
								AIMGlobal.SplashScreenHide();
								return;
							}
							else
							{
								for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
								{
									if (item.AnalyzerOwnerName == CFCAIM.AIMGlobal.crossRef[i].OwnerName)
									{
										CFCAIM.AIMGlobal.crossRef[i].OwnerID = aim.AIMOwnerID;
									}
								}
							}


						}
					}
				}
				DataBindOwner();
			}
			txtOwnerNumber.Text = "0";

			//----------------------------------------------------------------------
			Int32 test = grdOwnerView.RowCount;
			RefreshOwner();
			RemoveTabs();
			AIMGlobal.SplashScreenHide();
		}
		private void btnUpdateOwnerLocation_Click(object sender, EventArgs e)
		{
			AIMOwnerLocationRefAnalyzerOwnerLocationInfo xref = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();
			List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> gridOwnerLocation = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
			gridOwnerLocation = (List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>)grdOwnerLocation.DataSource;
			foreach (AIMOwnerLocationRefAnalyzerOwnerLocationInfo item in gridOwnerLocation)
			{
				if (item.AIMOwnerLocationID != 0)
				{
					xref.AIMOwnerLocationRefAnalyzerOwnerLocationID = 0;
					xref.AnalyzerOwnerLocationID = item.AnalyzerOwnerLocationID;
					xref.AnalyzerOwnerID = item.AnalyzerOwnerID;
					xref.AIMOwnerLocationID = item.AIMOwnerLocationID;
					xref.AIMOwnerID = item.AIMOwnerID;
					xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					xref.UpdateStamp = DateTime.Now;
					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAnalyzerCrossRefAimOwnerLocation(ref xref);
					if (error.ErrorNumber > 0)
					{
						return;
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (item.AnalyzerOwnerLocationName == CFCAIM.AIMGlobal.crossRef[i].OwnerLocationName)
							{
								CFCAIM.AIMGlobal.crossRef[i].OwnerLocationID = item.AIMOwnerLocationID;
								CFCAIM.AIMGlobal.crossRef[i].OwnerID = item.AIMOwnerID;
							}
						}
					}
				}
			}

			//----------------------------------------------------------------------
			//Use number entered by user to match up first characters for cross-ref
			if (Int32.TryParse(txtOwnerLocationNumber.Text, out _numCharacters) == true && _numCharacters != 0)
			{

				

				String shortName = "";
				foreach (AIMOwnerLocationRefAnalyzerOwnerLocationInfo item in gridOwnerLocation)
				{
					if (item.AnalyzerOwnerLocationName.Length >= _numCharacters)
					{
						shortName = item.AnalyzerOwnerLocationName.Substring(0, _numCharacters);
						AIMOwnerLocationInfo aim = new AIMOwnerLocationInfo();
						aim = AIMManager.GetOneOwnerLocation(shortName, _numCharacters);
						if (aim != null)
						{
							xref.AIMOwnerLocationRefAnalyzerOwnerLocationID = item.AIMOwnerLocationRefAnalyzerOwnerLocationID;
							xref.AnalyzerOwnerLocationID = item.AnalyzerOwnerLocationID;
							xref.AnalyzerOwnerID = item.AnalyzerOwnerID;
							xref.AIMOwnerID = aim.AIMOwnerID;
							xref.AIMOwnerLocationID = aim.AIMOwnerLocationID;
							xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							xref.UpdateStamp = DateTime.Now;
							ErrorInfo error = new ErrorInfo();
							error = AIMManager.SaveAnalyzerCrossRefAimOwnerLocation(ref xref);
							if (error.ErrorNumber > 0)
							{
								AIMGlobal.SplashScreenHide();
								return;
							}
							else
							{
								for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
								{
									if (item.AnalyzerOwnerLocationName == CFCAIM.AIMGlobal.crossRef[i].OwnerLocationName)
									{
										CFCAIM.AIMGlobal.crossRef[i].OwnerLocationID = aim.AIMOwnerLocationID;
										CFCAIM.AIMGlobal.crossRef[i].OwnerID = aim.AIMOwnerID;
									}
								}
							}
						}
					}
				}
				DataBindOwnerLocation();
			}
			txtOwnerLocationNumber.Text = "0";
			//----------------------------------------------------------------------


			RefreshOwnerLocation();
			RemoveTabs();
			AIMGlobal.SplashScreenHide();
		}
		private void btnUpdateSupplier_Click(object sender, EventArgs e)
		{			
			AIMSupplierRefAnalyzerSupplierInfo xref = new AIMSupplierRefAnalyzerSupplierInfo();
			List<AIMSupplierRefAnalyzerSupplierInfo> gridSupplier = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			gridSupplier = (List<AIMSupplierRefAnalyzerSupplierInfo>)grdSupplier.DataSource;
			foreach (AIMSupplierRefAnalyzerSupplierInfo item in gridSupplier)
			{
				if (item.AIMSupplierID != 0)
				{
					xref.AIMSupplierRefAnalyzerSupplierID = 0;
					xref.AnalyzerSupplierID = item.AnalyzerSupplierID;
					xref.AIMSupplierID = item.AIMSupplierID;
					xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					xref.UpdateStamp = DateTime.Now;
					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref xref);
					if (error.ErrorNumber > 0)
					{
						return;
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (item.AnalyzerSupplierName == CFCAIM.AIMGlobal.crossRef[i].SourceName)
							{
								CFCAIM.AIMGlobal.crossRef[i].SourceID = item.AIMSupplierID;
							}
						}
					}
				}
			}

			//----------------------------------------------------------------------
			//Use number entered by user to match up first characters for cross-ref	
			if (Int32.TryParse(txtSupplierNumber.Text, out _numCharacters) == true && _numCharacters != 0)
			{
				String shortName = "";
				foreach (AIMSupplierRefAnalyzerSupplierInfo item in gridSupplier)
				{
					if (item.AnalyzerSupplierName.Length >= _numCharacters)

					{
						shortName = item.AnalyzerSupplierName.Substring(0, _numCharacters);
						AIMSupplierInfo aim = new AIMSupplierInfo();
						aim = AIMManager.GetOneSupplier(shortName, _numCharacters);
						if (aim != null)
						{
							xref.AIMSupplierRefAnalyzerSupplierID = item.AIMSupplierRefAnalyzerSupplierID;
							xref.AnalyzerSupplierID = item.AnalyzerSupplierID;
							xref.AIMSupplierID = aim.AIMSupplierID;
							xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							xref.UpdateStamp = DateTime.Now;
							ErrorInfo error = new ErrorInfo();
							error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref xref);
							if (error.ErrorNumber > 0)
							{
								AIMGlobal.SplashScreenHide();
								return;
							}
							else
							{
								for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
								{
									if (item.AnalyzerSupplierName == CFCAIM.AIMGlobal.crossRef[i].SourceName)
									{
										CFCAIM.AIMGlobal.crossRef[i].SourceID = aim.AIMSupplierID;
									}
								}
							}


						}
					}
				}
				DataBindSupplier();
			}
			txtSupplierNumber.Text = "0";

			//----------------------------------------------------------------------
			Int32 test = grdSupplierView.RowCount;
			RefreshSupplier();
			RemoveTabs();
			AIMGlobal.SplashScreenHide();
		}
		private void btnUpdateSupplierLocation_Click(object sender, EventArgs e)
		{
			
			AIMSupplierLocationRefAnalyzerSupplierLocationInfo xref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> gridSupplierLocation = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			gridSupplierLocation = (List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>)grdSupplierLocation.DataSource;
			foreach (AIMSupplierLocationRefAnalyzerSupplierLocationInfo item in gridSupplierLocation)
			{
				if (item.AIMSupplierLocationID != 0)
				{
					xref.AIMSupplierLocationRefAnalyzerSupplierLocationID = 0;
					xref.AnalyzerSupplierLocationID = item.AnalyzerSupplierLocationID;
					xref.AnalyzerSupplierID = item.AnalyzerSupplierID;
					xref.AIMSupplierLocationID = item.AIMSupplierLocationID;
					xref.AIMSupplierID = item.AIMSupplierID;
					xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					xref.UpdateStamp = DateTime.Now;
					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref xref);
					if (error.ErrorNumber > 0)
					{
						return;
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (item.AnalyzerSupplierLocationName == CFCAIM.AIMGlobal.crossRef[i].SourceLocationName)
							{
								CFCAIM.AIMGlobal.crossRef[i].SourceLocationID = item.AIMSupplierLocationID;
								CFCAIM.AIMGlobal.crossRef[i].SourceID = item.AIMSupplierID;
							}
						}
					}
				}
			}
			

			//----------------------------------------------------------------------
			//Use number entered by user to match up first characters for cross-ref
			if (Int32.TryParse(txtSupplierLocationNumber.Text, out _numCharacters) == true && _numCharacters != 0)
			{
				String shortName = "";
				foreach (AIMSupplierLocationRefAnalyzerSupplierLocationInfo item in gridSupplierLocation)
				{
					if (item.AnalyzerSupplierLocationName.Length >= _numCharacters)
					{
						shortName = item.AnalyzerSupplierLocationName.Substring(0, _numCharacters);

						AIMSupplierLocationInfo aim = new AIMSupplierLocationInfo();
						aim = AIMManager.GetOneSupplierLocation(shortName, _numCharacters);
						if (aim != null)
						{
							xref.AIMSupplierLocationRefAnalyzerSupplierLocationID = item.AIMSupplierLocationRefAnalyzerSupplierLocationID;
							xref.AnalyzerSupplierLocationID = item.AnalyzerSupplierLocationID;
							xref.AnalyzerSupplierID = item.AnalyzerSupplierID;
							xref.AIMSupplierID = aim.AIMSupplierID;
							xref.AIMSupplierLocationID = aim.AIMSupplierLocationID;
							xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							xref.UpdateStamp = DateTime.Now;
							ErrorInfo error = new ErrorInfo();
							error = AIMManager.SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref xref);
							if (error.ErrorNumber > 0)
							{
								AIMGlobal.SplashScreenHide();
								return;
							}
							else
							{
								for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
								{
									if (item.AnalyzerSupplierLocationName == CFCAIM.AIMGlobal.crossRef[i].SourceLocationName)
									{
										CFCAIM.AIMGlobal.crossRef[i].SourceLocationID = aim.AIMSupplierLocationID;
										CFCAIM.AIMGlobal.crossRef[i].SourceID = item.AIMSupplierID;
									}
								}
							}
						}
					}
				}
				DataBindSupplierLocation();
			}
			
			txtSupplierLocationNumber.Text = "0";
			//----------------------------------------------------------------------

			RefreshSupplierLocation();
			RemoveTabs();
			AIMGlobal.SplashScreenHide();
		}

		private void repbtnQuickAddMaterial_Click(object sender, EventArgs e)
		{
			try
			{			
				QuickAddMaterial(false);			
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnQuickAddOwner_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	Int32 aimID = 0;
			//	ErrorInfo error = new ErrorInfo();
			//	AIMOwnerRefAnalyzerOwnerInfo xref = new AIMOwnerRefAnalyzerOwnerInfo();
			//	xref = (AIMOwnerRefAnalyzerOwnerInfo)grdOwnerView.GetFocusedRow();
			//	if (xref.AnalyzerOwnerID == 0)
			//	{
			//		MessageBox.Show("Unable to find " + AIMGlobal.analyzerDataSourceName + " Owner. Update not completed.", "Warning...");
			//		return;
			//	}

			//	//if x-ref exists, get aimID
			//	List<AIMOwnerRefAnalyzerOwnerInfo> xrefExists = new List<AIMOwnerRefAnalyzerOwnerInfo>();
			//	xrefExists = AIMManager.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeIDAnalyzerOwnerID(AIMGlobal.analyzerDataSourceTypeID, xref.AnalyzerOwnerID);

			//	if (xrefExists.Count != 0)
			//	{
			//		aimID = xrefExists[0].AIMOwnerID;
			//	}
			//	else
			//	{
			//		//check if aim owner exists -- if so, use that aimID, otherwise add new aim owner
			//		AIMOwnerInfo aimExists = new AIMOwnerInfo();
			//		aimExists = AIMManager.GetOwnerName(xref.AnalyzerOwnerName);
			//		if (aimExists != null)
			//		{
			//			aimID = aimExists.AIMOwnerID;
			//		}
			//		else
			//		{
			//			//add analyzer owner to AIM owner table - get record id to use below for x-ref update
			//			AIMOwnerInfo owner = new AIMOwnerInfo();
			//			owner.AIMOwnerID = 0;
			//			owner.OwnerCode = xref.AnalyzerOwnerCode;
			//			owner.OwnerName = xref.AnalyzerOwnerName;
			//			owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//			error = AIMManager.SaveAIMOwner(ref owner, true);
			//			if (error.ErrorNumber < 2)   //if errorNumber = 1, bypassing error message
			//			{
			//				if (error.ErrorNumber == 0)
			//				{
			//					aimID = error.RecordID;
			//				}
			//				else
			//				{
			//					aimID = error.ReturnIntegerValue;
			//				}
			//			}
			//		}
			//		AIMOwnerRefAnalyzerOwnerInfo ownerXref = new AIMOwnerRefAnalyzerOwnerInfo();
			//		ownerXref.AIMOwnerRefAnalyzerOwnerID = xref.AIMOwnerRefAnalyzerOwnerID;
			//		ownerXref.AnalyzerOwnerID = xref.AnalyzerOwnerID;
			//		ownerXref.AIMOwnerID = aimID;
			//		ownerXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//		error = AIMManager.SaveAIMOwnerRefAnalyzerOwner(ref ownerXref);
			//		if (error.ErrorNumber > 0)
			//		{
			//			return;
			//		}


			//		for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
			//		{
			//			if (xref.AnalyzerOwnerID == CFCAIM.AIMGlobal.crossRef[i].AnalyzerOwnerID)
			//			{
			//				CFCAIM.AIMGlobal.crossRef[i].OwnerID = aimID;
			//			}
			//		}
			//	}

			//	DataBindOwner();
			//	RemoveTabs();


			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void repbtnQuickAddSupplier_Click(object sender, EventArgs e)
		{
			QuickAddSupplier(false);			
		}
		private void repbtnCopyOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{			
				QuickAddOwnerLocation(false);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnCopySupplierLocation_Click(object sender, EventArgs e)
		{
			try
			{			
				QuickAddSupplierLocation(false);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnMaterialQuickAddAll_Click(Object sender, EventArgs e)
		{           
            try
            {

				DialogResult dr;
				dr = MessageBox.Show("Quick Add All Material cross-references?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.Yes)
				{
					AIMGlobal.SplashScreenShow();

					//loop thru grid
					List<AIMMaterialRefAnalyzerMaterialInfo> material = new List<AIMMaterialRefAnalyzerMaterialInfo>();
					material = (List<AIMMaterialRefAnalyzerMaterialInfo>)grdMaterial.DataSource;
					if (material != null)
					{
						foreach (var item in material)
						{
							QuickAddMaterial(true);
						}
					}
                                       

                    AIMGlobal.SplashScreenHide();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void btnOwnerLocationQuickAddAll_Click(Object sender, EventArgs e)
		{            
            try
            {	
				DialogResult dr;
				dr = MessageBox.Show("Quick Add All Owner Location cross-references?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.Yes)
				{
					AIMGlobal.SplashScreenShow();

					//loop thru grid
					List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> ownerLoc = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
					ownerLoc = (List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>)grdOwnerLocation.DataSource;
					if (ownerLoc != null)
					{
						foreach (var item in ownerLoc)
						{
							QuickAddOwnerLocation(true);
						}
					}

                    //2/5/18 - added in case there are additional owner locations added under Quick Add that need xref updated
                    if (grdOwnerLocationView.DataRowCount > 0)
                    {
                        List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> ownerLocAgain = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
                        ownerLocAgain = (List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>)grdOwnerLocation.DataSource;
                        if (ownerLocAgain != null)
                        {
                            foreach (var item in ownerLocAgain)
                            {
                                QuickAddOwnerLocation(true);
                            }
                        }
                    }
                    AIMGlobal.SplashScreenHide();

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnSupplierQuickAddAll_Click(Object sender, EventArgs e)
		{            
            try
            {				
							
				DialogResult dr;
				dr = MessageBox.Show("Quick Add All Supplier cross-references?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.Yes)
				{
					AIMGlobal.SplashScreenShow();

					//loop thru grid
					List<AIMSupplierRefAnalyzerSupplierInfo> supplier = new List<AIMSupplierRefAnalyzerSupplierInfo>();
					supplier = (List<AIMSupplierRefAnalyzerSupplierInfo>)grdSupplier.DataSource;
					if (supplier != null)
					{
						foreach (var item in supplier)
						{
							QuickAddSupplier(true);
						}
					}

                    //2/5/18 - added in case there are additional suppliers added under Quick Add that need xref updated
                    if (grdSupplierView.DataRowCount > 0)
                    {
                        List<AIMSupplierRefAnalyzerSupplierInfo> supplierAgain = new List<AIMSupplierRefAnalyzerSupplierInfo>();
                        supplierAgain = (List<AIMSupplierRefAnalyzerSupplierInfo>)grdSupplier.DataSource;
                        if (supplierAgain != null)
                        {
                            foreach (var item in supplierAgain)
                            {
                                QuickAddSupplier(true);
                            }
                        }
                    }

                    AIMGlobal.SplashScreenHide();

				}
                
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnSupplierLocationQuickAddAll_Click(Object sender, EventArgs e)
		{
			try
			{			
				DialogResult dr;
				dr = MessageBox.Show("Quick Add All Supplier Location cross-references?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.Yes)
				{

					AIMGlobal.SplashScreenShow();

					//loop thru grid
					List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> supplierLoc = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
					supplierLoc = (List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>)grdSupplierLocation.DataSource;
					if (supplierLoc != null)
					{
						foreach (var item in supplierLoc)
						{
							QuickAddSupplierLocation(true);
						}
					}                   

                    //2/5/18 - added in case there are additional supplier locations added under Quick Add that need xref updated
                    if (grdSupplierLocationView.DataRowCount > 0)
                    {
                        List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> supplierLocAgain = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
                        supplierLocAgain = (List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>)grdSupplierLocation.DataSource;
                        if (supplierLocAgain != null)
                        {
                            foreach (var item in supplierLocAgain)
                            {
                                QuickAddSupplierLocation(true);
                            }
                        }
                    }

                    AIMGlobal.SplashScreenHide();


                }                              

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}


            


		}

		private void QuickAddMaterial(Boolean allRecs)
		{
			try
			{
				Int32 aimID = 0;
				ErrorInfo error = new ErrorInfo();
				AIMMaterialRefAnalyzerMaterialInfo xref = new AIMMaterialRefAnalyzerMaterialInfo();
				xref = (AIMMaterialRefAnalyzerMaterialInfo)grdMaterialView.GetFocusedRow();
				if (xref.AnalyzerMaterialID == 0)
				{
					if (allRecs == true)
					{
						MessageBox.Show("Unable to find " + AIMGlobal.analyzerDataSourceName + " Material. Update not completed.", "Warning...");
					}
					
					return;
				}
				//if x-ref exists, get aimID
				List<AIMMaterialRefAnalyzerMaterialInfo> xrefExists = new List<AIMMaterialRefAnalyzerMaterialInfo>();
				xrefExists = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAnalyzerMaterialID(AIMGlobal.analyzerDataSourceTypeID, xref.AnalyzerMaterialID);
				if (xrefExists.Count != 0)
				{
					aimID = xrefExists[0].AIMMaterialID;
				}
				else
				{
					//check if aim material exists -- if so, use that aimID, otherwise add new aim material
					AIMMaterialInfo aimExists = new AIMMaterialInfo();
					aimExists = AIMManager.GetAIMMaterialName(xref.AnalyzerMaterialName);
					if (aimExists != null)
					{
						aimID = aimExists.AIMMaterialID;
					}
					else
					{
						//add analyzer material to AIM material table - get record id to use below for x-ref update
						AIMMaterialInfo material = new AIMMaterialInfo();
						material.AIMMaterialID = 0;
						material.AIMMaterialCode = xref.AnalyzerMaterialCode;
						material.AIMMaterialName = xref.AnalyzerMaterialName;
						material.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;                        
                        material.MaterialTypeID = (Int32)MaterialTypeEnum.Ingredient;

						error = AIMManager.SaveAIMMaterial(ref material, true);
						if (error.ErrorNumber < 2)   //if errorNumber = 1, bypassing error message
						{
							if (error.ErrorNumber == 0)
							{
								aimID = error.RecordID;
							}
							else
							{
								aimID = error.ReturnIntegerValue;
							}
						}
					}
					AIMMaterialRefAnalyzerMaterialInfo materialXref = new AIMMaterialRefAnalyzerMaterialInfo();
					materialXref.AIMMaterialRefAnalyzerMaterialID = xref.AIMMaterialRefAnalyzerMaterialID;
					materialXref.AnalyzerMaterialID = xref.AnalyzerMaterialID;
					materialXref.AIMMaterialID = aimID;
					materialXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					error = AIMManager.SaveAIMMaterialRefAnalyzerMaterial(ref materialXref);
					if (error.ErrorNumber > 0)
					{
						return;
					}


					for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
					{
						if (xref.AnalyzerMaterialID == CFCAIM.AIMGlobal.crossRef[i].AnalyzerMaterialID)
						{
							CFCAIM.AIMGlobal.crossRef[i].MaterialID = aimID;
						}
					}
				}

				DataBindMaterial();
				RemoveTabs();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}				        
        private void QuickAddOwner(Boolean allRecs)
		{
			try
			{
				Int32 aimID = 0;
				ErrorInfo error = new ErrorInfo();
				AIMOwnerRefAnalyzerOwnerInfo xref = new AIMOwnerRefAnalyzerOwnerInfo();
				xref = (AIMOwnerRefAnalyzerOwnerInfo)grdOwnerView.GetFocusedRow();
				if (xref.AnalyzerOwnerID == 0)
				{
					if (allRecs == false)
					{
						MessageBox.Show("Unable to find " + AIMGlobal.analyzerDataSourceName + " Owner. Update not completed.", "Warning...");
						return;
					}		   					
				}

				//if x-ref exists, get aimID
				List<AIMOwnerRefAnalyzerOwnerInfo> xrefExists = new List<AIMOwnerRefAnalyzerOwnerInfo>();
				xrefExists = AIMManager.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeIDAnalyzerOwnerID(AIMGlobal.analyzerDataSourceTypeID, xref.AnalyzerOwnerID);

				if (xrefExists.Count != 0)
				{
					aimID = xrefExists[0].AIMOwnerID;
				}
				else
				{
					//check if aim owner exists -- if so, use that aimID, otherwise add new aim owner
					AIMOwnerInfo aimExists = new AIMOwnerInfo();
					aimExists = AIMManager.GetOwnerName(xref.AnalyzerOwnerName);
					if (aimExists != null)
					{
						aimID = aimExists.AIMOwnerID;
					}
					else
					{
						//add analyzer owner to AIM owner table - get record id to use below for x-ref update
						AIMOwnerInfo owner = new AIMOwnerInfo();
						owner.AIMOwnerID = 0;
						owner.OwnerCode = xref.AnalyzerOwnerCode;
						owner.OwnerName = xref.AnalyzerOwnerName;
						owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
						
						if (_allowAIMInactive == false)
						{
							error = AIMManager.SaveAIMOwner_WithSampleCheck(ref owner, true);
						}
						else
						{
							error = AIMManager.SaveAIMOwner(ref owner, true);
						}

						if (error.ErrorNumber < 2)   //if errorNumber = 1, bypassing error message
						{
							if (error.ErrorNumber == 0)
							{
								aimID = error.RecordID;
							}
							else
							{
								aimID = error.ReturnIntegerValue;
							}
						}
					}
					AIMOwnerRefAnalyzerOwnerInfo ownerXref = new AIMOwnerRefAnalyzerOwnerInfo();
					ownerXref.AIMOwnerRefAnalyzerOwnerID = xref.AIMOwnerRefAnalyzerOwnerID;
					ownerXref.AnalyzerOwnerID = xref.AnalyzerOwnerID;
					ownerXref.AIMOwnerID = aimID;
					ownerXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					error = AIMManager.SaveAIMOwnerRefAnalyzerOwner(ref ownerXref);
					if (error.ErrorNumber > 0)
					{
						return;
					}


					for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
					{
						if (xref.AnalyzerOwnerID == CFCAIM.AIMGlobal.crossRef[i].AnalyzerOwnerID)
						{
							CFCAIM.AIMGlobal.crossRef[i].OwnerID = aimID;
						}
					}
				}

				DataBindOwner();
				RemoveTabs();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void QuickAddOwnerLocation(Boolean allRecs)
		{
			try
			{			
				DialogResult dr = new DialogResult();
				ErrorInfo result = new ErrorInfo();

				Int32 rowNum = 0;
				Int32 index = 0;
				if (grdOwnerLocationView != null)
				{
					Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out rowNum);
				}
				index = grdOwnerLocationView.GetDataSourceRowIndex(rowNum);
				AIMOwnerLocationRefAnalyzerOwnerLocationInfo xref = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();
				xref = (AIMOwnerLocationRefAnalyzerOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
				if (xref != null)
				{		  
					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAnalyzerOwnerLocationToAim(xref.AnalyzerOwnerLocationID, xref.AnalyzerOwnerID, xref.CFCUserID);
					if (error.ErrorNumber > 0)
					{
						if (error.ErrorNumber == 1)
						{
							if (allRecs == false)
							{
								dr = MessageBox.Show("Owner Location already exists on the AIM table. Generate cross-reference?", "Warning...", MessageBoxButtons.YesNoCancel);
								if (dr != DialogResult.Yes)
								{
									return;
								}
							}
							//update x-ref
							AIMOwnerLocationRefAnalyzerOwnerLocationInfo ownerLocXref = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();
							ownerLocXref.AIMOwnerLocationRefAnalyzerOwnerLocationID = 0;
							ownerLocXref.AnalyzerOwnerLocationID = xref.AnalyzerOwnerLocationID;
							ownerLocXref.AnalyzerOwnerID = xref.AnalyzerOwnerID;
							ownerLocXref.AIMOwnerID = xref.AIMOwnerID;
							ownerLocXref.AIMOwnerLocationID = error.ReturnIntegerValue;
							ownerLocXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							ownerLocXref.UpdateStamp = DateTime.Now;
							ErrorInfo errorOwner = new ErrorInfo();
							errorOwner = AIMManager.SaveAnalyzerCrossRefAimOwnerLocation(ref ownerLocXref);
							if (errorOwner.ErrorNumber > 0)
							{
								if (allRecs == false)
								{
									dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
								}
								return;
							}
							else
							{
								for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
								{
									if (xref.AnalyzerOwnerLocationID == CFCAIM.AIMGlobal.crossRef[i].AnalyzerOwnerLocationID)
									{
										CFCAIM.AIMGlobal.crossRef[i].OwnerLocationID = error.ReturnIntegerValue;
									}
								}
							}
						}

						else
						{
							if (allRecs == false)
							{
								dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
							}
							return;
						}

					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (xref.AnalyzerOwnerLocationID == CFCAIM.AIMGlobal.crossRef[i].AnalyzerOwnerLocationID)
							{
								CFCAIM.AIMGlobal.crossRef[i].OwnerLocationID = error.RecordID;
							}
						}
					}		 

					DataBindOwnerLocation();
					RemoveTabs();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void QuickAddSupplier(Boolean allRecs)
		{					
			try
			{
				Int32 aimID = 0;
				ErrorInfo error = new ErrorInfo();
				AIMSupplierRefAnalyzerSupplierInfo xref = new AIMSupplierRefAnalyzerSupplierInfo();
				xref = (AIMSupplierRefAnalyzerSupplierInfo)grdSupplierView.GetFocusedRow();
				if (xref.AnalyzerSupplierID == 0)
				{
					MessageBox.Show("Unable to find " + AIMGlobal.analyzerDataSourceName + " Supplier. Update not completed.", "Warning...");
					return;
				}

				//if x-ref exists, get aimID
				List<AIMSupplierRefAnalyzerSupplierInfo> xrefExists = new List<AIMSupplierRefAnalyzerSupplierInfo>();
				xrefExists = AIMManager.GetAIMSupplierRefAnalyzerSupplierDataSourceTypeIDAnalyzerSupplierID(AIMGlobal.analyzerDataSourceTypeID, xref.AnalyzerSupplierID);
				if (xrefExists.Count != 0)
				{
					aimID = xrefExists[0].AIMSupplierID;
				}
				else
				{
					//check if aim supplier exists -- if so, use that aimID, otherwise add new aim supplier
					AIMSupplierInfo aimExists = new AIMSupplierInfo();
					aimExists = AIMManager.GetSupplierSupplierName(xref.AnalyzerSupplierName);
					if (aimExists != null)
					{
						aimID = aimExists.AIMSupplierID;
					}
					else
					{
						//add analyzer supplier to AIM supplier table - get record id to use below for x-ref update
						AIMSupplierInfo supplier = new AIMSupplierInfo();
						
						supplier.AIMSupplierID = 0;						
						supplier.SupplierName = xref.AnalyzerSupplierName;
						supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

						if (_allowAIMInactive == false)
						{
							error = AIMManager.SaveAIMSupplier_WithSampleCheck(ref supplier, true);
						}
						else
						{
							error = AIMManager.SaveAIMSupplier(ref supplier, true);
						}
							
						if (error.ErrorNumber < 2)   //if errorNumber = 1, bypassing error message
						{
							if (error.ErrorNumber == 0)
							{
								aimID = error.RecordID;
							}
							else
							{
								aimID = error.ReturnIntegerValue;
							}
						}
					}
					AIMSupplierRefAnalyzerSupplierInfo supplierXref = new AIMSupplierRefAnalyzerSupplierInfo();
					supplierXref.AIMSupplierRefAnalyzerSupplierID = xref.AIMSupplierRefAnalyzerSupplierID;
					supplierXref.AnalyzerSupplierID = xref.AnalyzerSupplierID;
					supplierXref.AIMSupplierID = aimID;
					supplierXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;	  
					error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref supplierXref);
					if (error.ErrorNumber > 0)
					{
						AIMGlobal.SplashScreenHide();
						return;
					}
						
					for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
					{
						if (xref.AnalyzerSupplierID == CFCAIM.AIMGlobal.crossRef[i].UserDefinedSupplierID)
						{
							CFCAIM.AIMGlobal.crossRef[i].SourceID = aimID;
						}
					}
						
				}

				DataBindSupplier();							
				RefreshSupplier();
				RemoveTabs();
				AIMGlobal.SplashScreenHide();

				//AIMSupplierRefAnalyzerSupplierInfo xref = new AIMSupplierRefAnalyzerSupplierInfo();
				//List<AIMSupplierRefAnalyzerSupplierInfo> gridSupplier = new List<AIMSupplierRefAnalyzerSupplierInfo>();
				//gridSupplier = (List<AIMSupplierRefAnalyzerSupplierInfo>)grdSupplier.DataSource;
				//foreach (AIMSupplierRefAnalyzerSupplierInfo item in gridSupplier)
				//{
				//	if (item.AIMSupplierID != 0)
				//	{
				//		xref.AIMSupplierRefAnalyzerSupplierID = 0;
				//		xref.AnalyzerSupplierID = item.AnalyzerSupplierID;
				//		xref.AIMSupplierID = item.AIMSupplierID;
				//		xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				//		xref.UpdateStamp = DateTime.Now;
				//		ErrorInfo error = new ErrorInfo();
				//		error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref xref);
				//		if (error.ErrorNumber > 0)
				//		{
				//			AIMGlobal.SplashScreenHide();
				//			return;
				//		}
				//		else
				//		{
				//			for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
				//			{
				//				if (item.AnalyzerSupplierName == CFCAIM.AIMGlobal.crossRef[i].SourceName)
				//				{
				//					CFCAIM.AIMGlobal.crossRef[i].SourceID = item.AIMSupplierID;
				//				}
				//			}
				//		}
				//	}
				//}

				////----------------------------------------------------------------------
				////Use number entered by user to match up first characters for cross-ref
				//if (Int32.TryParse(txtSupplierNumber.Text, out _numCharacters) == true && _numCharacters != 0)
				//{
				//	String shortName = "";
				//	foreach (AIMSupplierRefAnalyzerSupplierInfo item in gridSupplier)
				//	{
				//		if (item.AnalyzerSupplierName.Length >= _numCharacters)
				//		{
				//			shortName = item.AnalyzerSupplierName.Substring(0, _numCharacters);
				//			AIMSupplierInfo aim = new AIMSupplierInfo();
				//			aim = AIMManager.GetOneSupplier(shortName, _numCharacters);
				//			if (aim != null)
				//			{
				//				xref.AIMSupplierRefAnalyzerSupplierID = item.AIMSupplierRefAnalyzerSupplierID;
				//				xref.AnalyzerSupplierID = item.AnalyzerSupplierID;
				//				xref.AIMSupplierID = aim.AIMSupplierID;
				//				xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				//				xref.UpdateStamp = DateTime.Now;
				//				ErrorInfo error = new ErrorInfo();
				//				error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref xref);
				//				if (error.ErrorNumber > 0)
				//				{
				//					AIMGlobal.SplashScreenHide();
				//					return;
				//				}
				//				else
				//				{
				//					for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
				//					{
				//						if (item.AnalyzerSupplierName == CFCAIM.AIMGlobal.crossRef[i].SourceName)
				//						{
				//							CFCAIM.AIMGlobal.crossRef[i].SourceID = aim.AIMSupplierID;
				//						}
				//					}
				//				}
				//			}
				//		}
				//	}
				//	DataBindSupplier();
				//}
				//txtSupplierNumber.Text = "0";
				////----------------------------------------------------------------------			

				//RefreshSupplier();
				//RemoveTabs();
				//AIMGlobal.SplashScreenHide();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void QuickAddSupplierLocation(Boolean allRecs)
		{
			try
			{			
				DialogResult dr = new DialogResult();
				ErrorInfo result = new ErrorInfo();
								
				Int32 rowNum = 0;
				Int32 index = 0;
				if (grdSupplierLocationView != null)
				{
					Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out rowNum);
				}
				index = grdSupplierLocationView.GetDataSourceRowIndex(rowNum);
				AIMSupplierLocationRefAnalyzerSupplierLocationInfo xref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
				xref = (AIMSupplierLocationRefAnalyzerSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
				if (xref != null)
				{						
                    ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAnalyzerSupplierLocationToAim(xref.AnalyzerSupplierLocationID, xref.AnalyzerSupplierID, xref.CFCUserID);
					if (error.ErrorNumber > 0)
					{
						if (error.ErrorNumber == 1)
						{
							if (allRecs == false)
							{
								dr = MessageBox.Show("Supplier Location already exists on the AIM table. Generate cross-reference?", "Warning...", MessageBoxButtons.YesNoCancel);
								if (dr != DialogResult.Yes)
								{
									return;
								}
							}
							//update x-ref
							AIMSupplierLocationRefAnalyzerSupplierLocationInfo supplierLocXref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
							supplierLocXref.AIMSupplierLocationRefAnalyzerSupplierLocationID = 0;
							supplierLocXref.AnalyzerSupplierLocationID = xref.AnalyzerSupplierLocationID;
							supplierLocXref.AnalyzerSupplierID = xref.AnalyzerSupplierID;							
                            supplierLocXref.AIMSupplierID = xref.AIMSupplierID;			 
							supplierLocXref.AIMSupplierLocationID = error.ReturnIntegerValue;
							supplierLocXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
							supplierLocXref.UpdateStamp = DateTime.Now;
							ErrorInfo errorSupplier = new ErrorInfo();
							errorSupplier = AIMManager.SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref supplierLocXref);
							if (errorSupplier.ErrorNumber > 0)
							{
								if (allRecs == false)
								{
									dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
								}
								return;
							}
							else
							{
								for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
								{
									if (xref.AnalyzerSupplierLocationID == CFCAIM.AIMGlobal.crossRef[i].UserDefinedSupplierLocationID)
									{
										CFCAIM.AIMGlobal.crossRef[i].SourceLocationID = error.ReturnIntegerValue;
									}
								}
							}
						}
						else
						{
							if (allRecs == false)
							{
								dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
							}
							return;
						}
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (xref.AnalyzerSupplierLocationID == CFCAIM.AIMGlobal.crossRef[i].UserDefinedSupplierLocationID)
							{
								CFCAIM.AIMGlobal.crossRef[i].SourceLocationID = error.RecordID;
							}
						}
					}
					DataBindSupplierLocation();
					RemoveTabs();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void grdMaterialView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			String aimName = "";
			if (e.Column == colAIMNameMaterial)
			{
				aimName = Convert.ToString(grdMaterialView.GetRowCellValue(e.RowHandle, colAIMNameMaterial));
				if (aimName == "0")
				{
					e.Appearance.BackColor = SystemColors.Info;
				}
			}
		}
		private void grdOwnerView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			String aimName = "";
			if (e.Column == colAIMNameMaterial)
			{
				aimName = Convert.ToString(grdOwnerView.GetRowCellValue(e.RowHandle, colAIMNameMaterial));
				if (aimName == "0")
				{
					e.Appearance.BackColor = SystemColors.Info;
				}
			}
		}									
		private void grdOwnerLocationView_ShownEditor(object sender, EventArgs e)
		{
			ColumnView view = (ColumnView)sender;
			if (view.FocusedColumn.FieldName == "AIMOwnerLocationID")
			{
				LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
				String ownerIDText = Convert.ToString(view.GetFocusedRowCellValue("AIMOwnerID"));
				Int32 ownerID = 0;
				Int32.TryParse(ownerIDText, out ownerID);
				editor.Properties.DataSource = AIMManager.GetAIMOwnerLocationForOwner(ownerID);
			}
		}
		private void grdSupplierLocationView_ShownEditor(object sender, EventArgs e)
		{
			ColumnView view = (ColumnView)sender;
			if (view.FocusedColumn.FieldName == "AIMSupplierLocationID")
			{			
				LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
				String supplierIDText = Convert.ToString(view.GetFocusedRowCellValue("AIMSupplierID"));
				Int32 supplierID = 0;
				Int32.TryParse(supplierIDText, out supplierID);

				List<AIMSupplierLocationInfo> def = new List<AIMSupplierLocationInfo>();				
				def = AIMManager.GetSupplierLocationForSupplier(supplierID);
				if (def.Count == 0)
				{
					editor.Properties.DataSource = AIMManager.GetAllSupplierLocation(true);
				}
				else
				{
					editor.Properties.DataSource = AIMManager.GetSupplierLocationForSupplier(supplierID);
				}			   
			}

			
				
		}
		
		private void replueAimOwner_OwnerLocation_EditValueChanged(object sender, EventArgs e)
		{
			grdOwnerLocationView.SetFocusedRowCellValue("AIMOwnerLocationID", 0);			
		}
		private void replueAimSupplier_SupplierLocation_EditValueChanged(object sender, EventArgs e)
		{
			//Trying to set the supplier-location if there's only one.  This doesn't work.
			//List<AIMSupplierLocationInfo> def = new List<AIMSupplierLocationInfo>();							
			//Int32 supplierID = (Int32)((DevExpress.XtraEditors.LookUpEdit)(sender)).Properties.KeyValue;					
			//def = AIMManager.GetSupplierLocationForSupplier(supplierID);
			//if (def.Count == 1)
			//{
			//	replueAIMSupplierLocation.DataSource = def[0].AIMSupplierID;
			//}
						
			grdSupplierLocationView.SetFocusedRowCellValue("AIMSupplierLocationID", 0);

		}

        
        private void btnOwnerQuickAddAll_Click(Object sender, EventArgs e)
		{
			try
			{
				DialogResult dr;
				dr = MessageBox.Show("Quick Add All Owner cross-references?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if (dr == DialogResult.Yes)
				{
					AIMGlobal.SplashScreenShow();

					//loop thru grid
					List<AIMOwnerRefAnalyzerOwnerInfo> owner = new List<AIMOwnerRefAnalyzerOwnerInfo>();
					owner = (List<AIMOwnerRefAnalyzerOwnerInfo>)grdOwner.DataSource;
					if (owner != null)
					{
						foreach (var item in owner)
						{
							QuickAddOwner(true);
						}
					}

					AIMGlobal.SplashScreenHide();

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnResize_Click(Object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		private void ImportExternalCrossReferenceUpdate_SizeChanged(Object sender, EventArgs e)
		{			
			btnResize.Visible = true;
		}

        //11/27/19
        private void lueMaterialType_EditValueChanged(object sender, EventArgs e)
        {
            
            btnUpdateMaterial.Visible = true;
            grdMaterial.Visible = true;
            if ((Int32)lueMaterialType.EditValue == 0)
            {
                btnUpdateMaterial.Visible = false;
                grdMaterial.Visible = false;

            }
            if (btnUpdateMaterial.Visible == true)
            {
                DataBindMaterialByType();
            }
            
            

        }

        private void btnUpdateSupplierLocationCombined_Click(object sender, EventArgs e)
        {
			AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo xref = new AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo();
			List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> gridSupplierLocationCombined = new List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>();
			gridSupplierLocationCombined = (List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo>)grdSupplierLocationCombined.DataSource;
			foreach (AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo item in gridSupplierLocationCombined)
			{
				if (item.AIMSupplierLocationID != 0)
				{
					xref.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID = 0;
					xref.AnalyzerSupplierLocationCombinedID = item.AnalyzerSupplierLocationCombinedID;
					xref.AIMSupplierLocationID = item.AIMSupplierLocationID;
					xref.AIMSupplierID = item.AIMSupplierID;
					xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					xref.UpdateStamp = DateTime.Now;
					ErrorInfo error = new ErrorInfo();
					error = AIMManager.SaveAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(ref xref);
					if (error.ErrorNumber > 0)
					{
						return;
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
						{
							if (item.AnalyzerSupplierLocationCombinedName == CFCAIM.AIMGlobal.crossRef[i].SourceLocationName)
							{
								CFCAIM.AIMGlobal.crossRef[i].SourceLocationID = item.AIMSupplierLocationID;
								CFCAIM.AIMGlobal.crossRef[i].SourceID = item.AIMSupplierID;
							}
						}
					}
				}
			}			
			//----------------------------------------------------------------------

			RefreshSupplierLocationCombined();
			RemoveTabs();
			AIMGlobal.SplashScreenHide();
		}
	       

        private void replueAimSupplier_SupplierLocationCombined_EditValueChanged(object sender, EventArgs e)
        {
			grdSupplierLocationCombinedView.SetFocusedRowCellValue("AIMSupplierLocationID", 0);
		}

        private void replueAimSupplier_SupplierLocationCombined_Enter(object sender, EventArgs e)
        {
			
		}

        private void replueAIMSupplierLocationCombined_PopupFilter(object sender, DevExpress.XtraEditors.Controls.PopupFilterEventArgs e)
        {
						
			Int32 supplierID = 0;
			supplierID = ((CFCAIMCore.Entity.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo)grdSupplierLocationCombinedView.GetFocusedRow()).AIMSupplierID;			
            if (supplierID != 0)
            {
				e.Criteria = DevExpress.Data.Filtering.CriteriaOperator.Parse("AIMSupplierID == " + supplierID);
			}
			
			
		}



        //private void test()
        //{
        //	ObjectFiller<List<AIMMaterialRefAnalyzerMaterialInfo>> item = new ObjectFiller<List<AIMMaterialRefAnalyzerMaterialInfo>>();
        //	for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
        //	{						
        //		if (item.AnalyzerMaterialName == CFCAIM.AIMGlobal.crossRef[i].MaterialName)
        //		{
        //			CFCAIM.AIMGlobal.crossRef[i].MaterialID = item.AIMMaterialID;
        //		}
        //	}

        //}	 
        //public void GetGenericCollection<T>(String filename)
        //{
        //}

        //public static T GetEnumeratedType<T>(this IEnumerable<T> _)
        //{
        //	return default(T);
        //}

        //public static Type GetListType<T>(this List<T> _)
        //{
        //	return typeof(T);
        //}		 		

        //private void GetGenericCollection(ref List<T> container, SqlEnumerator<T> filler)
        //{
        //	for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
        //	{
        //		//if (item.AnalyzerMaterialName == CFCAIM.AIMGlobal.crossRef[i].MaterialName)
        //		//{
        //		//	CFCAIM.AIMGlobal.crossRef[i].MaterialID = item.AIMMaterialID;
        //		//}
        //	}
        //}


        //private void Test()
        //{
        //	for (int i = 0; i < CFCAIM.AIMGlobal.crossRef.Count; i++)
        //	{
        //		//if (item.AnalyzerMaterialName == CFCAIM.AIMGlobal.crossRef[i].MaterialName)
        //		//{
        //		//	CFCAIM.AIMGlobal.crossRef[i].MaterialID = item.AIMMaterialID;
        //		//}
        //	}
        //}


        //private static Type GetElementTypeOfEnumerable(object o)
        //{
        //	var enumerable = o as IEnumerable;
        //	// if it's not an enumerable why do you call this method all ?
        //	if (enumerable == null)
        //		return null;

        //Type[] interfaces = enumerable.GetType().GetInterfaces();

        //	return (from i in interfaces
        //			where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
        //			select i.GetGenerguments()[0]).FirstOrDefault();
        //}

        //ObjectFiller<List<AIMMaterialRefAnalyzerMaterialInfo>> item = new ObjectFiller<List<AIMMaterialRefAnalyzerMaterialInfo>>();

        //public static List<T> test <T>(string sJson)
        //{
        //	//do something with the object like below 
        //	return (List<T>)JsonConvert.DeserializeObject<List<T>>(sJson);		 
        //}

        //ObjectFiller<List<AIMMaterialRefAnalyzerMaterialInfo>> entity = new ObjectFiller<List<AIMMaterialRefAnalyzerMaterialInfo>>();
        //	foreach (var item in entity)
        //	{

        //	}





    }
}