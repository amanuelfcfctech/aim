using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace CFCAIM
{
	public partial class NOTUsed_UserDefinedXref : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		MainMenu _MainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		Int32 _lueDataSourceCount = 0;

			
		
		public NOTUsed_UserDefinedXref()
		{
			InitializeComponent();
		}
				
		private void UserDefinedCrossRef_Load(object sender, EventArgs e)
		{

			tabControl.TabPages.Remove(tpMaterial);
			tabControl.TabPages.Remove(tpNutrient);
			tabControl.TabPages.Remove(tpOwner);
			tabControl.TabPages.Remove(tpOwnerLocation);

			SetToolTips();
			DataBindDataSource();

			// this was causing a fatal error on the Keystone server, so commented out 7/22/12.
			//List<UserDefinedDataSourceDefinitionInfo> dataUser = new List<UserDefinedDataSourceDefinitionInfo>();			
			//if (_lueDataSourceCount == 2)
			//{
			//	lueDataSource.EditValue = dataUser[1].DataSourceTypeID;
			//}			
			//lueDataSource.ShowPopup();
			
			if (AIMGlobal.userdefinedImportError != 0)
			{
				switch (AIMGlobal.userdefinedImportError)
				{
					case (Int32)SampleImportSpecificMessageDetail.MissingMaterialXref:
						tabControl.SelectedIndex = 0;
						break;
					case (Int32)SampleImportSpecificMessageDetail.MissingOwnerXref:
						tabControl.SelectedIndex = 1;
						break;
					case (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocationXref:
						tabControl.SelectedIndex = 2;
						break;
					case (Int32)SampleImportSpecificMessageDetail.MissingSupplierXref:
						tabControl.SelectedIndex = 3;
						break;
					case (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocationXref:
						tabControl.SelectedIndex = 4;
						break;
					
					
					default:
						break;
				}		
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
				ttip.SetToolTip(btnAddMaterial, "Add Record");
				ttip.SetToolTip(btnAddOwner, "Add Record");
				ttip.SetToolTip(btnAddOwnerLocation, "Add Record");
				ttip.SetToolTip(btnAddSource, "Add Record");
				ttip.SetToolTip(btnAddSourceLocation, "Add Record");
				ttip.SetToolTip(btnAddNutient, "Add Record");
				
				ttip.SetToolTip(btnPreviewGridResultsMaterial, "Preview Grid Results");
				ttip.SetToolTip(btnPreviewGridResultsOwner, "Preview Grid Results");
				ttip.SetToolTip(btnPreviewGridResultsOwnerLocation, "Preview Grid Results");							
				ttip.SetToolTip(btnPreviewGridResultsSource, "Preview Grid Results");
				ttip.SetToolTip(btnPreviewGridResultsSourceLocation, "Preview Grid Results");
				ttip.SetToolTip(btnPreviewGridResultsNutrient, "Preview Grid Results");

				ttip.SetToolTip(btnUpdateMaterial, "Update Material Cross References and AIM Materials");
				ttip.SetToolTip(btnUpdateOwner, "Update Owner Cross References and AIM Owners");
				ttip.SetToolTip(btnUpdateOwnerLocation, "Update Owner Location Cross References and AIM Owner Locations");
				ttip.SetToolTip(btnUpdateSupplier, "Update Supplier Cross References and AIM Suppliers");
				ttip.SetToolTip(btnUpdateSupplierLocation, "Update Supplier Location Cross References and AIM Supplier Locations");				
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

		#region Bind Data
		private void DataBindGrid()
		{
			DataBindGrid_Material();
			DataBindGrid_Owner();
			DataBindGrid_OwnerLocation();			
			DataBindGrid_Source();
			DataBindGrid_SourceLocation();
			DataBindNutrientGrid();
			
		}
		
		private void DataBindlue()
		{
			DataBindAIMMaterialLue();
			DataBindAIMOwnerLue();
			/////DataBindAIMOwnerLocationLue();
			DataBindAIMSourceLue();			
			/////DataBindAIMSourceLocationLue();
			DataBindAIMNutrientLue();

			DataBindMaterialLue();
			DataBindOwnerLue();
			////DataBindOwnerLocationLue();
			DataBindSourceLue();
			////DataBindSourceLocationLue();
			DataBindNutrientLue();
			
		}
		private void DataBindSourceLocation()
		{
			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			Int32 supplierLocationID = 0;
			Int32 supplierID = 0;
			if (lueAIMSourceLocationName.EditValue != null)
			{
				Int32.TryParse(lueAIMSourceLocationName.EditValue.ToString(), out supplierID);
			}
			aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplierID);
			aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					SupplierLocationName = "--Select--"
				}
			);
			lueAIMSourceLocationName.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMSourceLocationName, "AIMSupplierLocationID", "SupplierLocationName");
			lueAIMSourceLocationName.Properties.Columns["SupplierLocationName"].Visible = true;

			//8/3/16
			//lueAIMSourceLocationName.EditValue = supplierLocationID;
			if (aimSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueAIMSourceLocationName.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueAIMSourceLocationName.EditValue = supplierLocationID;
			}




		}
		private void DataBindOwnerLocation()
		{
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			Int32 OwnerLocationID = 0;
			Int32 OwnerID = 0;
			
			aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(OwnerID);
			aimOwnerLocation.Insert
			(
				0,
				new AIMOwnerLocationInfo
				{
					AIMOwnerLocationID = 0,
					LocationCode = "--Select--"
				}
			);
			lueAIMOwnerLocationName.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerLocationName, "AIMOwnerLocationID", "LocationCode");
			lueAIMOwnerLocationName.Properties.Columns["LocationCode"].Visible = true;
			lueAIMOwnerLocationName.EditValue = OwnerLocationID;

		}
		#endregion

		#region LookUpEdits
		private void DataBindDataSource()
		{
			List<DataSourceTypeInfo> dataUser = new List<DataSourceTypeInfo>();
			dataUser = AIMManager.GetDataSourceTypeUserDefined(true);
			dataUser.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--Select--"
				}
			);
			lueDataSource.Properties.DataSource = dataUser;
			XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name", "TypeDisplay");
			lueDataSource.BackColor = Color.White;

			_lueDataSourceCount = dataUser.Count();
			if (dataUser.Count == 2)
			{
				lueDataSource.EditValue = dataUser[1].DataSourceTypeID;
			}
			else 
			{
				lueDataSource.EditValue = 0;
			}

			if ((Int32)lueDataSource.EditValue == 0)
			{
				lueDataSource.BackColor = Color.Aqua;

				// this was causing a fatal error on the Keystone server, so commented out 7/22/12.
				////lueDataSource.ShowPopup();
			}

			lueDataSource.Refresh();
			
		}		

		private void DataBindAIMMaterialLue()
		{
			//MATERIAL ------------------------------------------------
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
			
			//lueAIMMaterialCode.Properties.DataSource = aimMaterial;
			//XtraEditors.LoadLookUpEdit(ref lueAIMMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
								
			//List<AIMMaterialInfo> aimMaterialName = new List<AIMMaterialInfo>();
			//aimMaterialName = AIMManager.GetAIMMaterial(true);
			//aimMaterialName.Insert
			//(
			//	0,
			//	new AIMMaterialInfo
			//	{
			//		AIMMaterialID = 0,
			//		AIMMaterialName = "--Select--",
			//		AIMMaterialCode = "--Select--"
			//	}
			//);
			//lueAIMMaterialName.Properties.DataSource = aimMaterialName;
			//XtraEditors.LoadLookUpEdit(ref lueAIMMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");


			gueAIMMaterialCode.Properties.DataSource = aimMaterial;
			gueAIMMaterialCode.Properties.PopulateViewColumns();
			gueAIMMaterialCode.Properties.View.Columns["AIMMaterialID"].Visible = false;
			gueAIMMaterialCode.Properties.View.Columns["UpdateStamp"].Visible = false;
			gueAIMMaterialCode.Properties.View.Columns["IsActive"].Visible = false;
			gueAIMMaterialCode.Properties.View.Columns["CFCUserID"].Visible = false;
			gueAIMMaterialCode.Properties.View.Columns["UserName"].Visible = false;
			gueAIMMaterialCode.Properties.View.Columns["MaterialTypeID"].Visible = false;
			gueAIMMaterialCode.Properties.View.Columns["MaterialTypeName"].Visible = false;
			gueAIMMaterialCode.Properties.ValueMember = "AIMMaterialID";
			gueAIMMaterialCode.Properties.DisplayMember = "AIMMaterialCode";
			gueAIMMaterialCode.EditValue = 0;

			gueAIMMaterialName.Properties.DataSource = aimMaterial;
			gueAIMMaterialName.Properties.PopulateViewColumns();
			gueAIMMaterialName.Properties.View.Columns["AIMMaterialID"].Visible = false;
			gueAIMMaterialName.Properties.View.Columns["UpdateStamp"].Visible = false;
			gueAIMMaterialName.Properties.View.Columns["IsActive"].Visible = false;
			gueAIMMaterialName.Properties.View.Columns["CFCUserID"].Visible = false;
			gueAIMMaterialName.Properties.View.Columns["UserName"].Visible = false;
			gueAIMMaterialName.Properties.View.Columns["MaterialTypeID"].Visible = false;
			gueAIMMaterialName.Properties.View.Columns["MaterialTypeName"].Visible = false;
			gueAIMMaterialName.Properties.ValueMember = "AIMMaterialID";
			gueAIMMaterialName.Properties.DisplayMember = "AIMMaterialName";
			gueAIMMaterialName.EditValue = 0;

						
		}


		private void DataBindAIMOwnerLue()
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
			//XtraEditors.LoadLookUpEdit(ref lueOwnerCode, "AIMOwnerID", "OwnerCode");
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
		private void DataBindAIMSourceLue()
		{			
			//SUPPLIER -------------------------------------------------
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
			lueAIMSourceName.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueAIMSourceName, "AIMSupplierID", "SupplierName");
						
			lueSourceName.Properties.DataSource = aimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSourceName, "AIMSupplierID", "SupplierName");	
	
			//SourceLocation
			List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
			supplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					SupplierLocationName = "--Select--"
				}
			);

									
		}
		private void DataBindAIMOwnerLocationLue()
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
		private void DataBindAIMSourceLocationLue()
		{					
					
			//SUPPLIER LOCATION -----------------------------------------
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
			lueAIMSourceLocationName.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMSourceLocationName, "AIMSupplierLocationID", "SupplierLocationName");				

		}
		private void DataBindAIMNutrientLue()
		{
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
			lueAIMNutrientCode.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMNutrientCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
			lueAIMNutrientName.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAIMNutrientName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			replueAIMNutrientName.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMNutrientName, "AIMNutrientID", "AIMNutrientName");

		}

		private void DataBindMaterialLue()
		{
			List<AnalyzerMaterialInfo> userMaterial = new List<AnalyzerMaterialInfo>();
			Int32 userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userMaterial.Clear();
			}
			else
			{
				userMaterial = AIMManager.GetAnalyzerMaterial(userType, true);				
			}

			userMaterial.Insert
			(
				0,
				new AnalyzerMaterialInfo
				{
					AnalyzerMaterialID = 0,
					AnalyzerMaterialCode = "--Select--",
					AnalyzerMaterialName = "--Select--"
				}
			);

			//lueUserMaterialCode.Properties.DataSource = userMaterial;
			//XtraEditors.LoadLookUpEdit(ref lueUserMaterialCode, "AnalyzerMaterialID", "AnalyzerMaterialCode", "AnalyzerMaterialName");						

			//lueUserMaterialName.Properties.DataSource = userMaterial;
			//XtraEditors.LoadLookUpEdit(ref lueUserMaterialName, "AnalyzerMaterialID", "AnalyzerMaterialName", "AnalyzerMaterialCode");

			gueUserMaterialCode.Properties.DataSource = userMaterial;
			gueUserMaterialCode.Properties.PopulateViewColumns();
			gueUserMaterialCode.Properties.View.Columns["AnalyzerMaterialID"].Visible = false;
			gueUserMaterialCode.Properties.View.Columns["UpdateStamp"].Visible = false;
			gueUserMaterialCode.Properties.View.Columns["IsActive"].Visible = false;
			gueUserMaterialCode.Properties.View.Columns["CFCUserID"].Visible = false;
			gueUserMaterialCode.Properties.View.Columns["UserName"].Visible = false;
			gueUserMaterialCode.Properties.View.Columns["DataSourceTypeID"].Visible = false;
			gueUserMaterialCode.Properties.View.Columns["DataSourceName"].Visible = false;
			gueUserMaterialCode.Properties.ValueMember = "AnalyzerMaterialID";
			gueUserMaterialCode.Properties.DisplayMember = "AnalyzerMaterialCode";
			gueUserMaterialCode.EditValue = 0;

			gueUserMaterialName.Properties.DataSource = userMaterial;
			gueUserMaterialName.Properties.PopulateViewColumns();
			gueUserMaterialName.Properties.View.Columns["AnalyzerMaterialID"].Visible = false;
			gueUserMaterialName.Properties.View.Columns["UpdateStamp"].Visible = false;
			gueUserMaterialName.Properties.View.Columns["IsActive"].Visible = false;
			gueUserMaterialName.Properties.View.Columns["CFCUserID"].Visible = false;
			gueUserMaterialName.Properties.View.Columns["UserName"].Visible = false;
			gueUserMaterialName.Properties.View.Columns["DataSourceTypeID"].Visible = false;
			gueUserMaterialName.Properties.View.Columns["DataSourceName"].Visible = false;
			gueUserMaterialName.Properties.ValueMember = "AnalyzerMaterialID";
			gueUserMaterialName.Properties.DisplayMember = "AnalyzerMaterialName";
			gueUserMaterialName.EditValue = 0;



			
		}
		private void DataBindOwnerLue()
		{
			List<AnalyzerOwnerInfo> userOwner = new List<AnalyzerOwnerInfo>();
			Int32 userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userOwner.Clear();
			}
			else
			{
				userOwner = AIMManager.GetAnalyzerOwner(userType);
			}

			userOwner.Insert
			(
				0,
				new AnalyzerOwnerInfo
				{
					AnalyzerOwnerID = 0,
					AnalyzerOwnerCode = "--Select--",
					AnalyzerOwnerName = "--Select--"
				}
			);

			lueUserOwnerCode.Properties.DataSource = userOwner;
			XtraEditors.LoadLookUpEdit(ref lueUserOwnerCode, "AnalyzerOwnerID", "AnalyzerOwnerCode");
			lueUserOwnerName.Properties.DataSource = userOwner;
			XtraEditors.LoadLookUpEdit(ref lueUserOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");
			lueUserDefinedOwnerName.Properties.DataSource = userOwner;
			XtraEditors.LoadLookUpEdit(ref lueUserDefinedOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");

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
		private void DataBindSourceLue()
		{
			List<UserDefinedSupplierInfo> userSupplier = new List<UserDefinedSupplierInfo>();
			Int32 userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userSupplier.Clear();
			}
			else
			{
				userSupplier = AIMManager.GetUserDefinedSupplier(userType);
			}			
			userSupplier.Insert
			(
				0,
				new UserDefinedSupplierInfo
				{
					UserDefinedSupplierID = 0,
					UserDefinedSupplierName = "--Select--"
				}
			);
			lueUserSupplierName.Properties.DataSource = userSupplier;
			XtraEditors.LoadLookUpEdit(ref lueUserSupplierName, "UserDefinedSupplierID", "UserDefinedSupplierName");

			lueUserDefinedSupplierName.Properties.DataSource = userSupplier;
			XtraEditors.LoadLookUpEdit(ref lueUserDefinedSupplierName, "UserDefinedSupplierID", "UserDefinedSupplierName");

			//SourceLocation
			List<UserDefinedSupplierLocationInfo> userSupplierLocation = new List<UserDefinedSupplierLocationInfo>();
			userSupplierLocation.Insert
			(
				0,
				new UserDefinedSupplierLocationInfo
				{
					UserDefinedSupplierLocationID = 0,
					UserDefinedSupplierLocationName = "--Select--"
				}
			);

		
		}
		private void DataBindOwnerLocationLue()
		{
			List<AnalyzerOwnerLocationInfo> analyzerOwnerLocation = new List<AnalyzerOwnerLocationInfo>();
			Int32 userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				analyzerOwnerLocation.Clear();
			}
			else
			{
				analyzerOwnerLocation = AIMManager.GetAnalyzerOwnerLocationByDataSource(userType);				
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
			lueUserOwnerLocationName.Properties.DataSource = analyzerOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueUserOwnerLocationName, "AnalyzerOwnerLocationID", "AnalyzerOwnerLocationName");

		}
		private void DataBindSourceLocationLue()
		{
			List<UserDefinedSupplierLocationInfo> userSupplierLocation = new List<UserDefinedSupplierLocationInfo>();
			Int32 userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userSupplierLocation.Clear();
			}
			else
			{
				userSupplierLocation = AIMManager.GetUserDefinedSupplierLocationByDataSource(userType);
			}						
			userSupplierLocation.Insert
			(
				0,
				new UserDefinedSupplierLocationInfo
				{
					UserDefinedSupplierLocationID = 0,
					UserDefinedSupplierLocationName = "--Select--"
				}
			);
			lueUserSourceLocationName.Properties.DataSource = userSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueUserSourceLocationName, "UserDefinedSupplierLocationID", "UserDefinedSupplierLocationName");

		}
		private void DataBindNutrientLue()
		{
			List<AnalyzerNutrientInfo> AnalyzerNutrient = new List<AnalyzerNutrientInfo>();
			Int32 dataSourceTypeID = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				dataSourceTypeID = (Int32)lueDataSource.EditValue;
			}
			if (dataSourceTypeID == 0)
			{
				AnalyzerNutrient.Clear();
			}
			else
			{
				AnalyzerNutrient = AIMManager.GetAnalyzerNutrient(dataSourceTypeID, true);						
			}
			AnalyzerNutrient.Insert
			(
				0,
				new AnalyzerNutrientInfo
				{
					AnalyzerNutrientID = 0,
					AnalyzerNutrientCode = "--Select--",
					AnalyzerNutrientName = "--Select--"
				}
			);
			lueAnalyzerCodeNutrient.Properties.DataSource = AnalyzerNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerCodeNutrient, "AnalyzerNutrientID", "AnalyzerNutrientCode", "AnalyzerNutrientName");

			lueAnalyzerNameNutrient.Properties.DataSource = AnalyzerNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAnalyzerNameNutrient, "AnalyzerNutrientID", "AnalyzerNutrientName", "AnalyzerNutrientCode");
			

		}

		#endregion
				
		#region Material
		private void DataBindGrid_Material()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdMaterialView.FocusedRowHandle.ToString(), out rowNum);
			if (lueDataSource.EditValue != null)
			{				
				grdMaterial.DataSource = AIMManager.GetAIMMaterialWithAnalyzerMaterial((Int32)lueDataSource.EditValue);
				grdMaterialView.FocusedRowHandle = rowNum;
			}
		}
		private void btnAddMaterial_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}
								
				if ((Int32)gueAIMMaterialCode.EditValue == 0 || (Int32)gueUserMaterialCode.EditValue == 0)
				{
					MessageBox.Show("Please select both " + lueDataSource.Text + " and AIM Materials.", "Warning...");

					if ((Int32)gueUserMaterialCode.EditValue == 0)
					{
						gueUserMaterialCode.ShowPopup();
						
					}
					else
					{
						gueAIMMaterialCode.ShowPopup();
					}
					return;
				}
								
				AIMMaterialRefAnalyzerMaterialInfo materialXref = new AIMMaterialRefAnalyzerMaterialInfo();
				materialXref.AnalyzerMaterialID = (Int32)gueUserMaterialCode.EditValue;
				materialXref.AIMMaterialID = (Int32)gueAIMMaterialCode.EditValue;
				materialXref.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				materialXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();				
				error = AIMManager.SaveAIMMaterialRefAnalyzerMaterial(ref materialXref); //2
				
				if (error.ErrorNumber > 0)
				{
					DataBindAIMMaterialLue();
					return;
				}
				DataBindGrid_Material();

				gueUserMaterialCode.EditValue = 0;
				gueAIMMaterialCode.EditValue = 0;
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void repbtnDeleteMaterial_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{				
				AIMMaterialRefAnalyzerMaterialInfo materialRef = new AIMMaterialRefAnalyzerMaterialInfo();
				materialRef = (AIMMaterialRefAnalyzerMaterialInfo)grdMaterialView.GetFocusedRow();
				AIMManager.DeleteAIMMaterialRefAnalyzerMaterial(materialRef.AIMMaterialRefAnalyzerMaterialID);
				DataBindAIMMaterialLue();
				DataBindGrid_Material();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewGridResultsMaterial_Click(object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.previewGridResults(grdMaterial, lueDataSource.Text + " Material Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		

		#endregion

		#region Owner
		private void DataBindGrid_Owner()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdOwnerView.FocusedRowHandle.ToString(), out rowNum);
			if (lueDataSource.EditValue != null)
			{
				grdOwner.DataSource = AIMManager.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeID((Int32)lueDataSource.EditValue);				
				grdOwnerView.FocusedRowHandle = rowNum;
			}

			List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
			owner = AIMManager.GetAIMOwner(true);
			replueAIMOwner_OwnerLocation.DataSource = owner;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMOwner_OwnerLocation, "AIMOwnerID", "OwnerName");
			replueAIMOwner_OwnerLocation.Columns["OwnerName"].Visible = true;

			List<AnalyzerOwnerInfo> userOwner = new List<AnalyzerOwnerInfo>();
			userOwner = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue);
			replueAnalyzerOwner_OwnerLocation.DataSource = userOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwner_OwnerLocation, "AnalyzerOwnerID", "AnalyzerOwnerName");
			replueAnalyzerOwner_OwnerLocation.Columns["AnalyzerOwnerName"].Visible = true;
		}
		private void btnAddOwner_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}
				if ((Int32)lueAIMOwnerCode.EditValue == 0 || (Int32)lueUserOwnerCode.EditValue == 0)
				{
					MessageBox.Show("Please select both " + lueDataSource.Text + " and AIM Owners.", "Warning...");

					if ((Int32)lueUserOwnerCode.EditValue == 0)
					{
						lueUserOwnerCode.ShowPopup();
					}
					else
					{
						lueAIMOwnerCode.ShowPopup();
					}
					return;
				}

				AIMOwnerRefAnalyzerOwnerInfo ownerXref = new AIMOwnerRefAnalyzerOwnerInfo();
				ownerXref.AnalyzerOwnerID = (Int32)lueUserOwnerCode.EditValue;
				ownerXref.AIMOwnerID = (Int32)lueAIMOwnerCode.EditValue;
				ownerXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAIMOwnerRefAnalyzerOwner(ref ownerXref);						 				
				if (error.ErrorNumber > 0)
				{
					DataBindAIMOwnerLue();
					return;
				}
				DataBindGrid_Owner();
				lueUserOwnerCode.EditValue = 0;
				lueAIMOwnerCode.EditValue = 0;
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				AIMOwnerRefAnalyzerOwnerInfo ownerRef = new AIMOwnerRefAnalyzerOwnerInfo();
				ownerRef = (AIMOwnerRefAnalyzerOwnerInfo)grdOwnerView.GetFocusedRow();
				AIMManager.DeleteAIMOwnerRefAnalyzerOwner(ownerRef.AIMOwnerRefAnalyzerOwnerID);						

				DataBindAIMOwnerLue();
				DataBindGrid_Owner();
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
				AIMGlobal.previewGridResults(grdOwner, lueDataSource.Text + " Owner Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueAIMOwnerCode_EditValueChanged(object sender, EventArgs e)
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
		private void lueAIMOwnerName_EditValueChanged(object sender, EventArgs e)
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
		private void lueUserOwnerCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueUserOwnerCode, ref lueUserOwnerName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueUserOwnerName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueUserOwnerName, ref lueUserOwnerCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueUserOwnerCode_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}
		#endregion

		#region Source
		private void DataBindGrid_Source()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdSourceView.FocusedRowHandle.ToString(), out rowNum);

			if (lueDataSource.EditValue != null)
			{
				grdSource.DataSource = AIMManager.GetUserDefinedCrossRefAimSupplierDataSourceTypeID((Int32)lueDataSource.EditValue);
				grdSourceView.FocusedRowHandle = rowNum;
			}

			List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
			supplier = AIMManager.GetSupplier(true);
			replueAIMSupplier_SupplierLocation.DataSource = supplier;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMSupplier_SupplierLocation, "AIMSupplierID", "SupplierName");
			replueAIMSupplier_SupplierLocation.Columns["SupplierName"].Visible = true;

			List<UserDefinedSupplierInfo> userSupplier = new List<UserDefinedSupplierInfo>();
			userSupplier = AIMManager.GetUserDefinedSupplier((Int32)lueDataSource.EditValue);
			replueUserDefinedSupplier_SupplierLocation.DataSource = userSupplier;
			XtraEditors.LoadGridLookUpEdit(ref replueUserDefinedSupplier_SupplierLocation, "UserDefinedSupplierID", "UserDefinedSupplierName");
			replueUserDefinedSupplier_SupplierLocation.Columns["UserDefinedSupplierName"].Visible = true;
		
		}
		private void btnAddSource_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}
				if ((Int32)lueAIMSourceName.EditValue == 0 || (Int32)lueUserSupplierName.EditValue == 0)
				{
					MessageBox.Show("Please select both " + lueDataSource.Text + " and AIM Suppliers.", "Warning...");

					if ((Int32)lueUserSupplierName.EditValue == 0)
					{
						lueUserSupplierName.ShowPopup();
					}
					else
					{						
						lueAIMSourceName.ShowPopup();
					}
					return;
				}

				UserDefinedCrossRefAIMSupplierInfo supplierXref = new UserDefinedCrossRefAIMSupplierInfo();
				supplierXref.UserDefinedSupplierID = (Int32)lueUserSupplierName.EditValue;
				supplierXref.AIMSupplierID = (Int32)lueAIMSourceName.EditValue;
				supplierXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveUserDefinedCrossRefAimSupplier(ref supplierXref);

				if (error.ErrorNumber > 0)
				{
					DataBindAIMSourceLue();
					lueUserSupplierName.EditValue = 0;
					return;
				}				
				DataBindGrid_Source();
				lueUserSupplierName.EditValue = 0;
				lueAIMSourceName.EditValue = 0;
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void repbtnDeleteSource_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				UserDefinedCrossRefAIMSupplierInfo supplierRef = new UserDefinedCrossRefAIMSupplierInfo();
				supplierRef = (UserDefinedCrossRefAIMSupplierInfo)grdSourceView.GetFocusedRow();
				AIMManager.DeleteUserDefinedCrossRefAimSupplier(supplierRef.UserDefinedCrossRefAIMSupplierID);
				DataBindGrid_Source();
				DataBindAIMSourceLue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewGridResultsSource_Click(object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.previewGridResults(grdSource, lueDataSource.Text + " Supplier Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueUserSourceName_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}
		#endregion

		#region SourceLocation
		private void DataBindGrid_SourceLocation()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdSourceLocationView.FocusedRowHandle.ToString(), out rowNum);

			if (lueDataSource.EditValue != null)
			{
				grdSourceLocation.DataSource = AIMManager.GetUserDefinedCrossRefAimSupplierLocationDataSourceTypeID((Int32)lueDataSource.EditValue);
				grdSourceLocationView.FocusedRowHandle = rowNum;
			}
		}
		private void btnAddSourceLocation_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}

				if ((Int32)lueUserDefinedSupplierName.EditValue == 0)
				{
					MessageBox.Show("Please select " + lueDataSource.Text + " Supplier.", "Warning...");
					lueUserDefinedSupplierName.ShowPopup();
					return;
				}
				if ((Int32)lueUserSourceLocationName.EditValue == 0)
				{
					MessageBox.Show("Please select " + lueDataSource.Text + " Supplier Location.", "Warning...");
					lueUserSourceLocationName.ShowPopup();
					return;
				}
				if ((Int32)lueSourceName.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier.", "Warning...");
					lueSourceName.ShowPopup();
					return;
				}
				if ((Int32)lueAIMSourceLocationName.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Supplier Location.", "Warning...");
					lueAIMSourceLocationName.ShowPopup();
					return;
				}
				
				UserDefinedCrossRefAIMSupplierLocationInfo locXref = new UserDefinedCrossRefAIMSupplierLocationInfo();
				locXref.UserDefinedSupplierLocationID = (Int32)lueUserSourceLocationName.EditValue;
				locXref.UserDefinedSourceID = (Int32)lueUserDefinedSupplierName.EditValue;
				locXref.AIMSupplierLocationID = (Int32)lueAIMSourceLocationName.EditValue;
				locXref.AIMSupplierID = (Int32)lueSourceName.EditValue;
				locXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveUserDefinedCrossRefAimSupplierLocation(ref locXref);

				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindGrid_SourceLocation();

				lueUserDefinedSupplierName.EditValue = 0;
				lueUserSourceLocationName.EditValue = 0;
				lueSourceName.EditValue = 0;
				lueAIMSourceLocationName.EditValue = 0;
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteSourceLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				UserDefinedCrossRefAIMSupplierLocationInfo locRef = new UserDefinedCrossRefAIMSupplierLocationInfo();
				locRef = (UserDefinedCrossRefAIMSupplierLocationInfo)grdSourceLocationView.GetFocusedRow();
				AIMManager.DeleteUserDefinedCrossRefAimSupplierLocation(locRef.UserDefinedCrossRefAIMSupplierLocationID);
				DataBindGrid_SourceLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewGridResultsSourceLocation_Click(object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.previewGridResults(grdSourceLocation, lueDataSource.Text + " Supplier Location Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAimSource_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				DataBindSourceLocation();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void lueUserSourceLocationName_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}

		#endregion
		
		#region OwnerLocation
		private void DataBindGrid_OwnerLocation()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out rowNum);

			if (lueDataSource.EditValue != null)
			{
				grdOwnerLocation.DataSource = AIMManager.GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeID((Int32)lueDataSource.EditValue);
				grdOwnerLocationView.FocusedRowHandle = rowNum;
			}
		}
		private void btnAddOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}
				if ((Int32)lueUserDefinedOwnerName.EditValue == 0)
				{
					MessageBox.Show("Please select " + lueDataSource.Text + " Owner.", "Warning...");
					lueUserDefinedOwnerName.ShowPopup();
					return;
				}
				if ((Int32)lueUserOwnerLocationName.EditValue == 0)
				{
					MessageBox.Show("Please select " + lueDataSource.Text + " Owner Location.", "Warning...");
					lueUserOwnerLocationName.ShowPopup();
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

				AIMOwnerLocationRefAnalyzerOwnerLocationInfo locXref = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();
				locXref.AnalyzerOwnerID = (Int32)lueUserDefinedOwnerName.EditValue;
				locXref.AnalyzerOwnerLocationID = (Int32)lueUserOwnerLocationName.EditValue;
				locXref.AIMOwnerID = (Int32)lueOwnerName.EditValue;
				locXref.AIMOwnerLocationID = (Int32)lueAIMOwnerLocationName.EditValue;
				locXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAnalyzerCrossRefAimOwnerLocation(ref locXref);

				if (error.ErrorNumber > 0)
				{					
					return;
				}
				DataBindGrid_OwnerLocation();
				lueUserDefinedOwnerName.EditValue = 0;
				lueUserOwnerLocationName.EditValue = 0;
				lueOwnerName.EditValue = 0;
				lueAIMOwnerLocationName.EditValue = 0;
				
				
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
				AIMManager.DeleteAnalyzerCrossRefAimOwnerLocation(locRef.AIMOwnerLocationRefAnalyzerOwnerLocationID);

				DataBindAIMOwnerLocationLue();
				DataBindGrid_OwnerLocation();
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
				AIMGlobal.previewGridResults(grdOwnerLocation, lueDataSource.Text + " Owner Location Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void lueAimOwner_EditValueChanged(object sender, EventArgs e)
		{
			
		}
		
		private void lueUserOwnerLocationCode_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}
		#endregion		

		#region Nutrient
		
		private void btnAddNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select data source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}
				if ((Int32)lueAIMNutrientCode.EditValue == 0 || (Int32)lueAnalyzerCodeNutrient.EditValue == 0)
				{
					MessageBox.Show("Please select both " + lueDataSource.Text + " and AIM Nutrients.", "Warning...");

					if ((Int32)lueAnalyzerCodeNutrient.EditValue == 0)
					{
						lueAnalyzerCodeNutrient.ShowPopup();
					}
					else
					{						
						lueAIMNutrientCode.ShowPopup();
					}
					return;
				}


				//UserDefinedCrossRefAIMNutrientInfo nutrientXref = new UserDefinedCrossRefAIMNutrientInfo();
				//nutrientXref.UserDefinedNutrientID = (Int32)lueAnalyzerCodeNutrient.EditValue;
				//nutrientXref.AIMNutrientID = (Int32)lueAIMNutrientCode.EditValue;
				//nutrientXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				//ErrorInfo error = new ErrorInfo();
				//error = AIMManager.SaveUserDefinedCrossRefAimNutrient(ref nutrientXref);				
				//if (error.ErrorNumber > 0)
				//{
				//	DataBindAIMNutrientLue();
				//	return;
				//}
				AIMNutrientRefAnalyzerNutrientInfo nutrientRef = new AIMNutrientRefAnalyzerNutrientInfo();
				nutrientRef.AnalyzerNutrientID = (Int32)lueAnalyzerCodeNutrient.EditValue;
				nutrientRef.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				nutrientRef.AIMNutrientID = (Int32)lueAIMNutrientCode.EditValue;
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
				if (AIMGlobal.aimDatabase != null)
				{
					aimDatabaseID = AIMGlobal.aimDatabase.AIMDatabaseID;
				}
				AIMGlobal.SplashScreenShow();
				errorSampleAim = AIMManager.SaveSampleAIM((Int32)lueAnalyzerCodeNutrient.EditValue, (Int32)lueAIMNutrientCode.EditValue, userGlobal.CFCLoginUser.CFCUserID, aimDatabaseID, (Int32)lueDataSource.EditValue);
				AIMGlobal.SplashScreenHide();
				if (errorSampleAim.ErrorNumber > 0)
				{
					return;
				}
				



				DataBindNutrientGrid();
				lueAnalyzerCodeNutrient.EditValue = 0;
				lueAIMNutrientCode.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteNutrient_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				AIMNutrientRefAnalyzerNutrientInfo nutrientRef = new AIMNutrientRefAnalyzerNutrientInfo();
				nutrientRef = (AIMNutrientRefAnalyzerNutrientInfo)grdNutrientView.GetFocusedRow();
				AIMManager.DeleteAIMNutrientRefAnalyzerNutrient(nutrientRef.AIMNutrientRefAnalyzerNutrientID);
				AIMManager.DeleteSampleAim(nutrientRef.AIMNutrientID);
				DataBindAIMNutrientLue();
				DataBindNutrientGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewGridResultsNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.previewGridResults(grdNutrient, lueDataSource.Text + " Nutrient Cross Reference");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMNutrientCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMNutrientCode, ref lueAIMNutrientName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueAIMNutrientName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueAIMNutrientName, ref lueAIMNutrientCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueUserNutrientCode_EditValueChanged(object sender, EventArgs e)
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
		private void lueUserNutrientName_EditValueChanged(object sender, EventArgs e)
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
		private void lueAIMNutrientCode_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}
		private void lueUserNutrientCode_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}

		#endregion

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

		private void SyncValueGridLue(ref GridLookUpEdit from, ref GridLookUpEdit to)
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
			//lueAIMOwnerLocationCode.Properties.DataSource = aimOwnerLocation;
			//XtraEditors.LoadLookUpEdit(ref lueAIMOwnerLocationCode, "AIMOwnerLocationID", "LocationCode");

			lueAIMOwnerLocationName.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMOwnerLocationName, "AIMOwnerLocationID", "LocationName");

		}
		
		private void lueDataSource_EditValueChanged(object sender, EventArgs e)
		{
			DataBindlue();
			DataBindreplue();
			DataBindGrid();

			lueDataSource.BackColor = Color.White;
			if (lueDataSource.EditValue != null)
			{				
				if ((Int32)lueDataSource.EditValue == 0)
				{
					lueDataSource.BackColor = Color.Yellow;
					// this was causing a fatal error on the Keystone server, so commented out 7/22/12.
					////lueDataSource.ShowPopup();
				}
			}
			

			btnUpdateMaterial.Enabled = false;
			btnUpdateOwner.Enabled = false;
			btnUpdateOwnerLocation.Enabled = false;
			btnUpdateSupplier.Enabled = false;
			btnUpdateSupplierLocation.Enabled = false;					
			if ((Int32)lueDataSource.EditValue != 0)
			{
				btnUpdateMaterial.Enabled = true;
				btnUpdateOwner.Enabled = true;
				btnUpdateOwnerLocation.Enabled = true;
				btnUpdateSupplier.Enabled = true;
				btnUpdateSupplierLocation.Enabled = true;
			}

			if ((Int32)lueDataSource.EditValue != 0)
			{
				tabControl.Visible = true;
				tabControl.SelectedIndex = 1;
				tabControl.SelectedIndex = 0;
			}
			
		}		
		private void ChecklueDataSource()
		{

			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{					
					lueDataSource.ShowPopup();
				}
			}
		}

		private void DataBindlueUserSourceLocationName()
		{

			List<UserDefinedSupplierLocationInfo> userSupplierLocation = new List<UserDefinedSupplierLocationInfo>();
			Int32 supplierID = 0;
			if (lueUserDefinedSupplierName.EditValue != null && lueUserDefinedSupplierName.Text != String.Empty)
			{
				supplierID = (Int32)lueUserDefinedSupplierName.EditValue;
			}
			if (supplierID == 0)
			{
				userSupplierLocation.Clear();
			}
			else
			{
				userSupplierLocation = AIMManager.GetUserDefinedSupplierLocationForSupplier(supplierID);			
			}
			userSupplierLocation.Insert
			(
				0,
				new UserDefinedSupplierLocationInfo
				{
					UserDefinedSupplierLocationID = 0,
					UserDefinedSupplierLocationName = "--Select--"
				}
			);

			lueUserSourceLocationName.Properties.DataSource = userSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueUserSourceLocationName, "UserDefinedSupplierLocationID", "UserDefinedSupplierLocationName");

		}

		private void DataBindLueSourceLocation()
		{


			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			Int32 supplierID = 0;
			if (lueSourceName.EditValue != null && lueSourceName.Text != String.Empty)
			{
				supplierID = (Int32)lueSourceName.EditValue;
			}
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
					SupplierLocationName = "--Select--"
				}
			);
			
			lueAIMSourceLocationName.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueAIMSourceLocationName, "AIMSupplierLocationID", "SupplierLocationName");

		}

		private void tabControl_Click(object sender, EventArgs e)
		{
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue == 0)
				{
					lueDataSource.ShowPopup();
				}
			}
		}
			

		private void lueOwnerName_EditValueChanged(object sender, EventArgs e)
		{
			//8/3/16
			//try
			//{
			//	SyncValueLue(ref lueOwnerName, ref lueOwnerCode);
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
			DataBindLueOwnerLocation();
		}

		private void lueSource_EditValueChanged(object sender, EventArgs e)
		{
			try
			{						
				DataBindSourceLocation();
				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void lueSourceName_EditValueChanged(object sender, EventArgs e)
		{						
			DataBindLueSourceLocation();
		}
		
		private void openCrossRefUpdate()
		{
			AIMGlobal.analyzerDataSourceTypeID = (Int32)lueDataSource.EditValue;
			AIMGlobal.analyzerDataSourceName = lueDataSource.Text;
			AnalyzerCrossReferenceMassUpdate update = new AnalyzerCrossReferenceMassUpdate();
			this.Hide();
			update.WindowState = FormWindowState.Normal;
			update.ShowDialog();
			this.Show();
		}
		private void btnUpdateSource_Click(object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "SUPPLIER";
			openCrossRefUpdate();

		}
		private void btnUpdateSourceLocation_Click(object sender, EventArgs e)
		{			
			AIMGlobal.analyzerRequiredField = "SUPPLIERLOCATION";
			openCrossRefUpdate();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "MATERIAL";
			openCrossRefUpdate();

		}

		private void btnUpdateOwner_Click(object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "OWNER";
			openCrossRefUpdate();

		}

		private void btnUpdateOwnerLocation_Click(object sender, EventArgs e)
		{
			AIMGlobal.analyzerRequiredField = "OWNERLOCATION";
			openCrossRefUpdate();

		}

		private void lueUserSupplierName_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{			
			switch (tabControl.SelectedIndex)
			{
				case 0:
					gbUserMaterial.Text = lueDataSource.Text.ToString() + " Material";
					colUserDefinedMaterialCode.Caption = lueDataSource.Text.ToString() + " Material Code";
					colUserDefinedMaterialName.Caption = lueDataSource.Text.ToString() + " Material Name";
					break;
				case 1:
					gbUserOwner.Text = lueDataSource.Text.ToString() + " Owner";
					colAnalyzerOwnerCode.Caption = lueDataSource.Text.ToString() + " Owner Code";
					colAnalyzerOwnerName.Caption = lueDataSource.Text.ToString() + " Owner Name";
					break;
				case 2:
					gbUserOwnerLocation.Text = lueDataSource.Text.ToString() + " Owner Location";
					colAnalyzerOwnerLocationCode.Caption = lueDataSource.Text.ToString() + " Owner Location Code";
					colAnalyzerOwnerLocationName.Caption = lueDataSource.Text.ToString() + " Owner Location";
					colAnalyzerOwner_OwnerLocation.Caption = lueDataSource.Text.ToString() + " Owner";						
					break;
				case 3:
					gbUserSupplier.Text = lueDataSource.Text.ToString() + " Supplier";
					colUserDefinedSupplierName.Caption = lueDataSource.Text.ToString() + " Supplier Name";
					break;
				case 4:
					
					gbUserSupplierLocation.Text = lueDataSource.Text.ToString();
					colUserDefinedSupplierLocationCode.Caption = lueDataSource.Text.ToString() + " Supplier Location Code";					
					colUserDefinedSupplierLocationName.Caption = lueDataSource.Text.ToString() + " Supplier Location";
					colUserDefinedSupplier_SupplierLocation.Caption = lueDataSource.Text.ToString() + " Supplier";

					break;
				case 5:
					gbUserNutrient.Text = lueDataSource.Text.ToString() + " Nutrient";
					colAnalyzerNutrientCode.Caption = lueDataSource.Text.ToString() + " Nutrient Code";
					colAnalyzerNutrientName.Caption = lueDataSource.Text.ToString() + " Nutrient Name";
					break;
				default:
					break;
			}

		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			DataBindGrid();
		}
		private void DataBindreplue()
		{
			Int32 userType = 0;

			//MATERIAL -----------------------------------------------
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			aimMaterial = AIMManager.GetAIMMaterial(true);
			replueAimMaterialCode.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAimMaterialCode, "AIMMaterialID", "AIMMaterialCode");
			replueAimMaterialName.DataSource = aimMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueAimMaterialName, "AIMMaterialID", "AIMMaterialName");		

			//OWNER --------------------------------------------------
			List<AIMOwnerInfo> aimOwner = new List<AIMOwnerInfo>();
			aimOwner = AIMManager.GetAIMOwner(true);
			replueAimOwnerCode.DataSource = aimOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAimOwnerCode, "AIMOwnerID", "OwnerCode");
			replueAIMOwnerName.DataSource = aimOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMOwnerName, "AIMOwnerID", "OwnerName");

			//OWNER LOCATION -----------------------------------------
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			aimOwnerLocation = AIMManager.GetAIMOwnerLocation(true);
			replueAimOwnerLocationCode.DataSource = aimOwnerLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAimOwnerLocationCode, "AIMOwnerLocationID", "LocationCode");
			replueAimOwnerLocationName.DataSource = aimOwnerLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAimOwnerLocationName, "AIMOwnerLocationID", "LocationName");
		
			//SUPPLIER -------------------------------------------------
			List<AIMSupplierInfo> aimSupplier = new List<AIMSupplierInfo>();
			aimSupplier = AIMManager.GetSupplier(true);	
			replueAIMSupplierName.DataSource = aimSupplier;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMSupplierName, "AIMSupplierID", "SupplierName");

			//SUPPLIER LOCATION -----------------------------------------
			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			aimSupplierLocation = AIMManager.GetAllSupplierLocation(true);			
			replueAIMSupplierLocationName.DataSource = aimSupplierLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAIMSupplierLocationName, "AIMSupplierLocationID", "SupplierLocationName");
						
			//USER MATERIAL ---------------------------------------------
			List<AnalyzerMaterialInfo> userMaterial = new List<AnalyzerMaterialInfo>();
			userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userMaterial.Clear();
			}
			else
			{
				userMaterial = AIMManager.GetAnalyzerMaterial(userType, true);
			}
			replueUserMaterialCode.DataSource = userMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueUserMaterialCode, "AnalyzerMaterialID", "AnalyzerMaterialCode");
			replueUserMaterialName.DataSource = userMaterial;
			XtraEditors.LoadGridLookUpEdit(ref replueUserMaterialName, "AnalyzerMaterialID", "AnalyzerMaterialName");

			//USER OWNER ------------------------------------------------
			List<AnalyzerOwnerInfo> userOwner = new List<AnalyzerOwnerInfo>();
			userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userOwner.Clear();
			}
			else
			{
				userOwner = AIMManager.GetAnalyzerOwner(userType);
			}

			replueAnalyzerOwnerCode.DataSource = userOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerCode, "AnalyzerOwnerID", "AnalyzerOwnerCode");
			replueAnalyzerOwnerName.DataSource = userOwner;
			XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");

			//USER OWNER LOCATION ---------------------------------------
			List<AnalyzerOwnerLocationInfo> analyzerOwnerLocation = new List<AnalyzerOwnerLocationInfo>();
			userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				analyzerOwnerLocation.Clear();
			}
			else
			{
				analyzerOwnerLocation = AIMManager.GetAnalyzerOwnerLocationByDataSource(userType);
			}
			replueAnalyzerOwnerLocationCode.DataSource = analyzerOwnerLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerLocationCode, "AnalyzerOwnerLocationID", "AnalyzerOwnerLocationCode");
			replueAnalyzerOwnerLocationName.DataSource = analyzerOwnerLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerLocationName, "AnalyzerOwnerLocationID", "AnalyzerOwnerLocationName");

			//USER SUPPLIER ---------------------------------------------
			List<UserDefinedSupplierInfo> userSupplier = new List<UserDefinedSupplierInfo>();
			userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userSupplier.Clear();
			}
			else
			{
				userSupplier = AIMManager.GetUserDefinedSupplier(userType);
			}
			replueUserSupplierName.DataSource = userSupplier;
			XtraEditors.LoadGridLookUpEdit(ref replueUserSupplierName, "UserDefinedSupplierID", "UserDefinedSupplierName");

			//USER SUPPLIER LOCATION ------------------------------------			
			List<UserDefinedSupplierLocationInfo> userSupplierLocation = new List<UserDefinedSupplierLocationInfo>();
			userType = 0;
			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				userType = (Int32)lueDataSource.EditValue;
			}
			if (userType == 0)
			{
				userSupplierLocation.Clear();
			}
			else
			{
				userSupplierLocation = AIMManager.GetUserDefinedSupplierLocationByDataSource(userType);
			}			
			replueUserSupplierLocationName.DataSource = userSupplierLocation;
			XtraEditors.LoadGridLookUpEdit(ref replueUserSupplierLocationName, "UserDefinedSupplierLocationID", "UserDefinedSupplierLocationName");
						
		}

		private void gbUserOwner_Enter(object sender, EventArgs e)
		{

		}

		private void DataBindLueAnalyzerNutrient()
		{

			List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
			Int32 dataType = 0;

			if (lueDataSource.EditValue != null && lueDataSource.Text != String.Empty)
			{
				dataType = (Int32)lueDataSource.EditValue;
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

		private void checkfilterNutrient()
		{
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue != 0)
				{
					List<AIMNutrientRefAnalyzerNutrientInfo> xref = new List<AIMNutrientRefAnalyzerNutrientInfo>();
					xref = AIMManager.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeID((Int32)lueDataSource.EditValue);
					Int32 currentRowNum = 0;
					if (grdNutrientView != null)
					{
						Int32.TryParse(grdNutrientView.FocusedRowHandle.ToString(), out currentRowNum);
					}
					grdNutrient.DataSource = xref;
					grdNutrientView.FocusedRowHandle = currentRowNum;

				}
			}
		}


		private void repbtnDeleteNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				AIMNutrientRefAnalyzerNutrientInfo nutrientRef = new AIMNutrientRefAnalyzerNutrientInfo();
				nutrientRef = (AIMNutrientRefAnalyzerNutrientInfo)grdNutrientView.GetFocusedRow();
				AIMManager.DeleteAIMNutrientRefAnalyzerNutrient(nutrientRef.AIMNutrientRefAnalyzerNutrientID);
				AIMManager.DeleteSampleAim(nutrientRef.AIMNutrientID);

				DataBindNutrientGrid();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void DataBindNutrientGrid()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdNutrientView.FocusedRowHandle.ToString(), out rowNum);
			if (lueDataSource.EditValue != null)
			{
				grdNutrient.DataSource = AIMManager.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeID((Int32)lueDataSource.EditValue);
				grdNutrientView.FocusedRowHandle = rowNum;
			}			
			Int32.TryParse(grdNutrientView.FocusedRowHandle.ToString(), out rowNum);
			
			checkfilterNutrient();
		}

		private void gueUserMaterialCode_Popup(object sender, EventArgs e)
		{
			GridLookUpEdit editor = (GridLookUpEdit)sender;
			RepositoryItemGridLookUpEdit properties = editor.Properties;
			properties.PopupFormSize = new Size(editor.Width - 4, properties.PopupFormSize.Height);
			gueUserMaterialCode.Properties.View.Columns["AnalyzerMaterialCode"].VisibleIndex = 1;
			gueUserMaterialCode.Properties.View.Columns["AnalyzerMaterialName"].VisibleIndex = 2;
		}

		private void gueUserMaterialName_Popup(object sender, EventArgs e)
		{
			GridLookUpEdit editor = (GridLookUpEdit)sender;
			RepositoryItemGridLookUpEdit properties = editor.Properties;
			properties.PopupFormSize = new Size(editor.Width - 4, properties.PopupFormSize.Height);
			gueUserMaterialName.Properties.View.Columns["AnalyzerMaterialCode"].VisibleIndex = 2;
			gueUserMaterialName.Properties.View.Columns["AnalyzerMaterialName"].VisibleIndex = 1;
		}

		private void gueAIMMaterialName_Popup(object sender, EventArgs e)
		{
			GridLookUpEdit editor = (GridLookUpEdit)sender;
			RepositoryItemGridLookUpEdit properties = editor.Properties;
			properties.PopupFormSize = new Size(editor.Width - 4, properties.PopupFormSize.Height);
			gueAIMMaterialName.Properties.View.Columns["AIMMaterialCode"].VisibleIndex = 2;
			gueAIMMaterialName.Properties.View.Columns["AIMMaterialName"].VisibleIndex = 1;
		}

		private void gueAIMMaterialCode_Popup(object sender, EventArgs e)
		{
			GridLookUpEdit editor = (GridLookUpEdit)sender;
			RepositoryItemGridLookUpEdit properties = editor.Properties;			
			properties.PopupFormSize = new Size(editor.Width - 4, properties.PopupFormSize.Height);
			gueAIMMaterialName.Properties.View.Columns["AIMMaterialCode"].VisibleIndex = 1;
			gueAIMMaterialName.Properties.View.Columns["AIMMaterialName"].VisibleIndex = 2;
		}

		private void gueAIMMaterialCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueGridLue(ref gueAIMMaterialCode, ref gueAIMMaterialName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void gueUserMaterialCode_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueGridLue(ref gueUserMaterialCode, ref gueUserMaterialName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void gueAIMMaterialName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueGridLue(ref gueAIMMaterialName, ref gueAIMMaterialCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void gueUserMaterialName_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueGridLue(ref gueUserMaterialName, ref gueUserMaterialCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void gueUserMaterialCode_Click(object sender, EventArgs e)
		{
			ChecklueDataSource();
		}

		private void lueUserDefinedSupplierName_EditValueChanged(object sender, EventArgs e)
		{
			DataBindlueUserSourceLocationName();
		}

		private void lueUserDefinedOwnerName_EditValueChanged(object sender, EventArgs e)
		{			
			DataBindOwnerLocationLue();
		}

		private void lueUserOwnerLocationName_EditValueChanged(Object sender, EventArgs e)
		{

		}
	}
}
