using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using Core.Library.Data;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using System.IO;
using CFCAIM.DataExchange;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;


namespace CFCAIM.CrossReference
{
	public partial class ImportInternalCrossReferenceUpdate : Form
	{													 
		CFCUser.Global userGlobal = new CFCUser.Global();
		Int32 _numCharacters = 0;
		Boolean _allowAIMInactive = true;

		public ImportInternalCrossReferenceUpdate()
		{
			InitializeComponent();
		}

		private void InternalCrossReferenceUpdate_Load(Object sender, EventArgs e)
		{
			DataBindMaterial();
			DataBindMaterialType();

			tabControl1.TabPages.Remove(tpOwner);
			tabControl1.TabPages.Remove(tpOwnerLocation);
			DataBindOwner();
			DataBindOwnerLocation();
			DataBindSupplier();
			DataBindSupplierLocation();

			SystemSettingsInfo systemSettings = new SystemSettingsInfo();
			systemSettings = AIMManager.GetSystemSettings();
			if (systemSettings != null)
			{
				_allowAIMInactive = systemSettings.AllowAIMInactive;
			}

			SetToolTips();
			RemoveTabs();
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
		private void btnExit_Click(Object sender, EventArgs e)
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

		private void DataBindMaterial()
		{

			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			aimMaterial = AIMManager.GetAIMMaterial(true);
			aimMaterial.Insert
			(
				0,
				new AIMMaterialInfo
				{
					AIMMaterialID = 0,
					AIMMaterialName = "--Select--"
				}
			);
			replueAIMName.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMMaterialID", "AIMMaterialName");

			List<AIMMaterialRefAnalyzerMaterialInfo> materialResults = new List<AIMMaterialRefAnalyzerMaterialInfo>();			
			foreach (InternalImportValidateSampleInfo item in CFCAIM.AIMGlobal.internalImportValidateSampleHeader)
				{
				if (item.AIMMaterialID == 0 && item.XrefMaterialStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _materialResults. If MaterialName already exists, skip add
					var results = from g in materialResults
								  where g.AnalyzerMaterialName == item.AnalyzerMaterialName
								  select g;
					if (results.Count() == 0)
					{
						materialResults.Add(new AIMMaterialRefAnalyzerMaterialInfo
						{
							AnalyzerMaterialName = item.AnalyzerMaterialName,
							AnalyzerMaterialCode = item.AnalyzerMaterialCode,
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
				grdMaterialView.Columns["AnalyzerMaterialName"].Caption = AIMGlobal.internalDataSourceName + " Material";
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
			replueMaterialType.DataSource = material;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterialType, "MaterialTypeID", "MaterialTypeName");
			replueMaterialType.Columns["MaterialTypeName"].Visible = true;

		}
		private void DataBindOwner()
		{
			List<AIMOwnerRefAnalyzerOwnerInfo> ownerResults = new List<AIMOwnerRefAnalyzerOwnerInfo>();
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
			
			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();			
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

			List<AIMSupplierRefAnalyzerSupplierInfo> supplierResults = new List<AIMSupplierRefAnalyzerSupplierInfo>();
			foreach (InternalImportValidateSampleInfo item in CFCAIM.AIMGlobal.internalImportValidateSampleHeader)
			{
				if (item.AIMSupplierID == 0 && item.XrefSupplierStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _supplierResults. If SourceName already exists, skip add
					var results = from g in supplierResults
									  //where g.AnalyzerSupplierName == item.AnalyzerSupplierSupplierName								 
								  where g.AnalyzerSupplierID == item.AnalyzerSupplierID
								  select g;
					if (results.Count() == 0)
					{
						supplierResults.Add(new AIMSupplierRefAnalyzerSupplierInfo
						{
							AnalyzerSupplierName = item.AnalyzerSupplierName,
							AnalyzerSupplierCode = item.AnalyzerSupplierCode,
							AnalyzerSupplierID = item.AnalyzerSupplierID,
							AIMSupplierID = 0
						});
					}
				}
			}
			int rowNum = 0;
			if (supplierResults.Count() != 0)
			{
				grdSupplier.DataSource = supplierResults;
				grdSupplierView.Columns["AnalyzerSupplierName"].Caption = AIMGlobal.internalDataSourceName + " Supplier";
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

			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> supplierLocationResults = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			foreach (InternalImportValidateSampleInfo item in CFCAIM.AIMGlobal.internalImportValidateSampleHeader)
			{
				if (item.AIMSupplierLocationID == 0 && item.XrefSupplierLocationStatus == 1 && item.Message == "Missing Cross-Reference")
				{
					//query _supplierlocResults. If SupplierLocationName already exists, skip add
					var results = from g in supplierLocationResults
								  where g.AnalyzerSupplierLocationName == item.AnalyzerSupplierLocationName
								  select g;
					if (results.Count() == 0)
					{
						supplierLocationResults.Add(new AIMSupplierLocationRefAnalyzerSupplierLocationInfo
						{
							AnalyzerSupplierLocationName = item.AnalyzerSupplierLocationName,
							AnalyzerSupplierLocationCode = item.AnalyzerSupplierLocationCode,
							AnalyzerSupplierLocationID = item.AnalyzerSupplierLocationID,
							AnalyzerSupplierID = item.AnalyzerSupplierID,
							AIMSupplierLocationID = 0,
							AIMSupplierID = 0
						});
					}
				}
			}

			int rowNum = 0;
			if (supplierLocationResults.Count() != 0)
			{
				grdSupplierLocation.DataSource = supplierLocationResults;
				grdSupplierLocationView.Columns["AnalyzerSupplierLocationName"].Caption = AIMGlobal.internalDataSourceName + " Supplier Location";
				Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out rowNum);
				grdSupplierLocationView.FocusedRowHandle = rowNum;
				txtSupplierLocationNumber.Text = "0";
			}
			else
			{
				tabControl1.TabPages.Remove(tpSupplierLocation);
			}

		}

		private void btnUpdateMaterial_Click(Object sender, EventArgs e)
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
						for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
						{
							if (item.AnalyzerMaterialName == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerMaterialName)
							{
								CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMMaterialID = item.AIMMaterialID;
							}
						}
					}
				}
			}
			RefreshMaterial();
			RemoveTabs();
			AIMGlobal.SplashScreenHide();
		}
		private void btnUpdateOwner_Click(Object sender, EventArgs e)
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
		private void btnUpdateOwnerLocation_Click(Object sender, EventArgs e)
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
										CFCAIM.AIMGlobal.crossRef[i].OwnerID = item.AIMOwnerID;
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
		private void btnUpdateSupplier_Click(Object sender, EventArgs e)
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
						AIMGlobal.SplashScreenHide();
						return;
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
						{
							if (item.AnalyzerSupplierName == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierName)
							{
								CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierID = item.AIMSupplierID;
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
								for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
								{
									if (item.AnalyzerSupplierName == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierName)
									{
										CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierID = aim.AIMSupplierID;
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


			RefreshSupplier();
			RemoveTabs();
			AIMGlobal.SplashScreenHide();
		}
		private void btnUpdateSupplierLocation_Click(Object sender, EventArgs e)
		{
			AIMSupplierLocationRefAnalyzerSupplierLocationInfo xref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
			List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> gridSupplierLocation = new List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>();
			gridSupplierLocation = (List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo>)grdSupplierLocation.DataSource;
			foreach (AIMSupplierLocationRefAnalyzerSupplierLocationInfo item in gridSupplierLocation)
			{
				if (item.AIMSupplierLocationID != 0 && item.AIMSupplierID != 0)
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
						for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
						{
							if (item.AnalyzerSupplierLocationName == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierLocationName)
							{
								CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierLocationID = item.AIMSupplierLocationID;
								CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierID = item.AIMSupplierID;
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
								for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
								{
									if (item.AnalyzerSupplierLocationName == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierLocationName)
									{
										CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierLocationID = aim.AIMSupplierLocationID;									   
										CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierID = item.AIMSupplierID;
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

		private void repbtnQuickAddMaterial_Click(Object sender, EventArgs e)
		{
			try
			{
				Int32 aimID = 0;
				ErrorInfo error = new ErrorInfo();
				AIMMaterialRefAnalyzerMaterialInfo xref = new AIMMaterialRefAnalyzerMaterialInfo();
				xref = (AIMMaterialRefAnalyzerMaterialInfo)grdMaterialView.GetFocusedRow();
				if (xref.AnalyzerMaterialID == 0)
				{
					MessageBox.Show("Unable to find " + AIMGlobal.internalDataSourceName + " Material. Update not completed.", "Warning...");
					return;
				}
				//if x-ref exists, get aimID
				List<AIMMaterialRefAnalyzerMaterialInfo> xrefExists = new List<AIMMaterialRefAnalyzerMaterialInfo>();
				xrefExists = AIMManager.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAnalyzerMaterialID(AIMGlobal.internalDataSourceTypeID, xref.AnalyzerMaterialID);
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
						//add user-defined material to AIM material table - get record id to use below for x-ref update
						AIMMaterialInfo material = new AIMMaterialInfo();
						material.AIMMaterialID = 0;
						material.AIMMaterialCode = xref.AnalyzerMaterialCode;
						material.AIMMaterialName = xref.AnalyzerMaterialName;
						material.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
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


					for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
					{
						if (xref.AnalyzerMaterialID == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerMaterialID)
						{
							CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMMaterialID = aimID;
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
		private void repbtnQuickAddSupplier_Click(Object sender, EventArgs e)
		{
			try
			{

				Int32 aimID = 0;
				ErrorInfo error = new ErrorInfo();
				AIMSupplierRefAnalyzerSupplierInfo xref = new AIMSupplierRefAnalyzerSupplierInfo();
				xref = (AIMSupplierRefAnalyzerSupplierInfo)grdSupplierView.GetFocusedRow();
				if (xref.AnalyzerSupplierID == 0)
				{
					MessageBox.Show("Unable to find " + AIMGlobal.internalDataSourceName + " Supplier. Update not completed.", "Warning...");
					return;
				}

				//if x-ref exists, get aimID
				List<AIMSupplierRefAnalyzerSupplierInfo> xrefExists = new List<AIMSupplierRefAnalyzerSupplierInfo>();
				xrefExists = AIMManager.GetAIMSupplierRefAnalyzerSupplierDataSourceTypeIDAnalyzerSupplierID(AIMGlobal.internalDataSourceTypeID, xref.AnalyzerSupplierID);
				if (xrefExists.Count != 0)
				{
					aimID = xrefExists[0].AIMSupplierID;
				}
				else
				{
					//check if aim source exists -- if so, use that aimID, otherwise add new aimsource
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
							error = AIMManager.SaveSupplier_WithSampleCheck(ref supplier, true);
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
						return;
					}


					for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
					{
						if (xref.AnalyzerSupplierID == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierID)
						{
							CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierID = aimID;
						}
					}
				}

				DataBindSupplier();
				RemoveTabs();
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnQuickAddSupplierLocation_Click(Object sender, EventArgs e)
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
			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveAnalyzerSupplierLocationToAim(xref.AnalyzerSupplierLocationID, xref.AnalyzerSupplierID, xref.CFCUserID);
			if (error.ErrorNumber > 0)
			{
				if (error.ErrorNumber == 1)
				{
					dr = MessageBox.Show("Supplier Location already exists on the AIM table. Generate cross-reference?", "Warning...", MessageBoxButtons.YesNo);
					if (dr != DialogResult.Yes)
					{
						return;
					}

					//update x-ref
					AIMSupplierLocationRefAnalyzerSupplierLocationInfo supplierLocXref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
					supplierLocXref.AIMSupplierLocationRefAnalyzerSupplierLocationID = 0;
					supplierLocXref.AnalyzerSupplierLocationID = xref.AnalyzerSupplierLocationID;
					supplierLocXref.AnalyzerSupplierID = xref.AnalyzerSupplierID;
					supplierLocXref.AIMSupplierID = error.ReturnSecondIntegerValue;
					supplierLocXref.AIMSupplierLocationID = error.ReturnIntegerValue;
					supplierLocXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					supplierLocXref.UpdateStamp = DateTime.Now;
					ErrorInfo errorSource = new ErrorInfo();
					errorSource = AIMManager.SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref supplierLocXref);
					if (errorSource.ErrorNumber > 0)
					{
						dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
						return;
					}
					else
					{
						for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
						{
							if (xref.AnalyzerSupplierLocationID == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierLocationID)
							{
								CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AIMSupplierLocationID = error.ReturnIntegerValue;
							}
						}
					}
				}
				else
				{
					dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
					return;
				}
			}
			else
			{
				for (int i = 0; i < CFCAIM.AIMGlobal.internalImportValidateSampleHeader.Count; i++)
				{
					if (xref.AnalyzerSupplierLocationID == CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierLocationID)
					{
						CFCAIM.AIMGlobal.internalImportValidateSampleHeader[i].AnalyzerSupplierLocationID = error.RecordID;
					}
				}
			}


			DataBindSupplierLocation();
			RemoveTabs();

		}
		

		private void grdSupplierLocationView_ShownEditor(Object sender, EventArgs e)
		{
			ColumnView view = (ColumnView)sender;
			if (view.FocusedColumn.FieldName == "AIMSupplierLocationID")
			{
				LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
				String supplierIDText = Convert.ToString(view.GetFocusedRowCellValue("AIMSupplierID"));
				Int32 supplierID = 0;
				Int32.TryParse(supplierIDText, out supplierID);
				editor.Properties.DataSource = AIMManager.GetSupplierLocationForSupplier(supplierID);
			}
		}

		private void replueMaterialType_Enter(Object sender, EventArgs e)
		{
						
		}
	}
}
