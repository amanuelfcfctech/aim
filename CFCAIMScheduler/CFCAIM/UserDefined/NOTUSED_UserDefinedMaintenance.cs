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

namespace CFCAIM
{
	public partial class UserDefinedMaintenance : Form
	{
			

		CFCUser.Global userGlobal = new CFCUser.Global();
		MainMenu _MainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		List<DataSourceTypeInfo> _dataUser = new List<DataSourceTypeInfo>();			

		public UserDefinedMaintenance()
		{
			InitializeComponent();
		}
		private void UserDefinedMaintenance_Load(object sender, EventArgs e)
		{
			//tabControl.TabPages.Remove(tpMaterial);
			//tabControl.TabPages.Remove(tpOwner);
			//tabControl.TabPages.Remove(tpOwnerLocation);
			//tabControl.TabPages.Remove(tpNutrient);

			//SetToolTips();
			//DataBindDataSource();	
			

			//if (AIMGlobal.userdefinedImportError != 0)
			//{
			//	switch (AIMGlobal.userdefinedImportError)
			//	{
			//		case (Int32)SampleImportSpecificMessageDetail.MissingMaterial:
			//			tabControl.SelectedIndex = 0;
			//			break;
			//		case (Int32)SampleImportSpecificMessageDetail.MissingOwner:
			//			tabControl.SelectedIndex = 1;
			//			break;
			//		case (Int32)SampleImportSpecificMessageDetail.MissingOwnerLocation:
			//			tabControl.SelectedIndex = 2;
			//			break;
			//		case (Int32)SampleImportSpecificMessageDetail.MissingSupplier:
			//			tabControl.SelectedIndex = 3;
			//			break;
			//		case (Int32)SampleImportSpecificMessageDetail.MissingSupplierLocation:
			//			tabControl.SelectedIndex = 4;
			//			break;

			//		default:
			//			break;
			//	}
			//}

			//if (_dataUser.Count == 2)
			//{
			//	//selecting value if there is only one
			//	lueDataSource.EditValue = _dataUser[1].DataSourceTypeID;
			//}
			//else
			//{
			//	if (AIMGlobal.userdefinedDataSourceTypeID != 0)
			//	{
			//		lueDataSource.EditValue = AIMGlobal.userdefinedDataSourceTypeID;
			//		lueDataSource.ShowPopup();
			//	}
			//}			
			

		}

		private void ChecklueDataSource()
		{

			//if (lueDataSource.EditValue != null)
			//{
			//	if ((Int32)lueDataSource.EditValue == 0)
			//	{
			//		lueDataSource.ShowPopup();
			//	}
			//}
		}
		private void SetToolTips()
		{
			//try
			//{
			//	Icon = (Icon)Properties.Resources.CFCAIM;
			//	ToolTip ttip = new ToolTip();
			//	foreach (Control ctrl in pnlTopBtns.Controls)
			//	{
			//		ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
			//	}
			//	ttip.SetToolTip(btnAddOwner, "Add Record");
			//	ttip.SetToolTip(btnPreviewGridResultsOwner, "Preview Grid Results");
			//	ttip.SetToolTip(btnAddMaterial, "Add Record");
			//	ttip.SetToolTip(btnPreviewGridResultsMaterial, "Preview Grid Results");
			//	ttip.SetToolTip(btnAddSupplier, "Add Record");
			//	ttip.SetToolTip(btnPreviewGridResultsSupplier, "Preview Grid Results");
			//	ttip.SetToolTip(btnAddSupplierLocation, "Add Record");
			//	ttip.SetToolTip(btnPreviewGridResultsSupplierLocation, "Preview Grid Results");
			//	ttip.SetToolTip(btnAddOwnerLocation, "Add Record");
			//	ttip.SetToolTip(btnPreviewGridResultsOwnerLocation, "Preview Grid Results");
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	Close();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			//DataBindlue();
			//DataBindGrid();
			//SetToolTips();
			//ClearTxtFields();
		}
		private void ClearTxtFields()
		{
			//txtMaterialCode.Text = String.Empty;
			//txtMaterialName.Text = String.Empty;

			//txtOwnerCode.Text = String.Empty;
			//txtOwnerName.Text = String.Empty;

			//txtSupplierCode.Text = String.Empty;
			//txtSupplierName.Text = String.Empty;

			//lueSupplier.EditValue = 0;
			//txtSupplierLocationCode.Text = String.Empty;
			//txtSupplierLocationName.Text = String.Empty;

			//lueOwner.EditValue = 0;
			//txtOwnerLocationCode.Text = String.Empty;
			//txtOwnerLocationName.Text = String.Empty;

		}
		private void lueDataSource_EditValueChanged(object sender, EventArgs e)
		{
		//	DataBindlue();
		//	DataBindGrid();

		//	lueDataSource.BackColor = Color.White;

		//	if (lueDataSource.EditValue != null)
		//	{
		//		if ((Int32)lueDataSource.EditValue == 0)
		//		{
		//			lueDataSource.BackColor = Color.Yellow;
		//			lueDataSource.ShowPopup();
		//		}
		//	}
		}
		private void PreviewGridResults(DevExpress.XtraPrinting.IPrintable grid, String title)
		{
			//try
			//{
			//	AIMGlobal.previewGridResults(grid, title);
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void tabControl_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}


		#region Bind Data
		private void DataBindGrid()
		{
			//DataBindGrid_Owner();
			//DataBindGrid_OwnerLocation();
			//DataBindGrid_Material();
			//DataBindGrid_Supplier();
			//DataBindGrid_SupplierLocation();
			
		}
		private void DataBindlue()
		{
			//DataBindOwnerlue();
			//DataBindSourcelue();			
		}
		private void DataBindSourcelue()
		{
			//SUPPLIER -------------------------------------------------

			//10/7/16 -- commented not using this form

			//List<UserDefinedSupplierInfo> userSupplier = new List<UserDefinedSupplierInfo>();
			//if (lueDataSource.EditValue != null)
			//{
			//	userSupplier = AIMManager.GetUserDefinedSupplier((Int32)lueDataSource.EditValue);
			//	userSupplier.Insert
			//	(
			//		0,
			//		new UserDefinedSupplierInfo
			//		{
			//			UserDefinedSupplierID = 0,
			//			UserDefinedSupplierCode = "--Select--",
			//			UserDefinedSupplierName = "--Select--"
			//		}
			//	);
			//	lueSupplier.Properties.DataSource = userSupplier;
			//	XtraEditors.LoadLookUpEdit(ref lueSupplier, "UserDefinedSupplierID", "UserDefinedSupplierName");
			//	lueSupplier.Properties.Columns["UserDefinedSupplierCode"].Visible = true;
			//	lueSupplier.Properties.Columns["UserDefinedSupplierName"].Visible = true;

			//	//SUPPLIER LOCATION -----------------------------------------
			//	replueUserDefinedSupplierName.DataSource = userSupplier;
			//	XtraEditors.LoadGridLookUpEdit(ref replueUserDefinedSupplierName, "UserDefinedSupplierID", "UserDefinedSupplierName");
			//	replueUserDefinedSupplierCode.DataSource = userSupplier;
			//	XtraEditors.LoadGridLookUpEdit(ref replueUserDefinedSupplierCode, "UserDefinedSupplierID", "UserDefinedSupplierCode");
			
		}
		private void DataBindOwnerlue()
		{
			////OWNER --------------------------------------------------

			////9/30/16
			////List<UserDefinedOwnerInfo> userOwner = new List<UserDefinedOwnerInfo>();
			//List<AnalyzerOwnerInfo> userOwner = new List<AnalyzerOwnerInfo>();


			//if (lueDataSource.EditValue != null)
			//{	
			//	//9/30/16			
			//	//userOwner = AIMManager.GetUserDefinedOwner((Int32)lueDataSource.EditValue);
			//	userOwner = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue);

			//	//9/30/16
			//	//userOwner.Insert
			//	//(
			//	//	0,
			//	//	new UserDefinedOwnerInfo
			//	//	{
			//	//		UserDefinedOwnerID = 0,
			//	//		UserDefinedOwnerCode = "--Select--",
			//	//		UserDefinedOwnerName = "--Select--"
			//	//	}
			//	//);
			//	userOwner.Insert
			//	(
			//		0,
			//		new AnalyzerOwnerInfo
			//		{
			//			AnalyzerOwnerID = 0,
			//			AnalyzerOwnerCode = "--Select--",
			//			AnalyzerOwnerName = "--Select--"
			//		}
			//	);

			//	//9/30/16
			//	//lueOwner.Properties.DataSource = userOwner;
			//	//XtraEditors.LoadLookUpEdit(ref lueOwner, "UserDefinedOwnerID", "UserDefinedOwnerName");
			//	//lueOwner.Properties.Columns["UserDefinedOwnerCode"].Visible = true;
			//	//lueOwner.Properties.Columns["UserDefinedOwnerName"].Visible = true;
			//	lueOwner.Properties.DataSource = userOwner;
			//	XtraEditors.LoadLookUpEdit(ref lueOwner, "AnalyzerOwnerID", "AnalyzerOwnerName");
			//	lueOwner.Properties.Columns["AnalyzerOwnerCode"].Visible = true;
			//	lueOwner.Properties.Columns["AnalyzerOwnerName"].Visible = true;

			//	//OWNER LOCATION -----------------------------------------

			//	//9/30/16
			//	//replueUserDefinedOwnerName.DataSource = userOwner;
			//	//XtraEditors.LoadGridLookUpEdit(ref replueUserDefinedOwnerName, "UserDefinedOwnerID", "UserDefinedOwnerName");
			//	//replueUserDefinedOwnerCode.DataSource = userOwner;
			//	//XtraEditors.LoadGridLookUpEdit(ref replueUserDefinedOwnerCode, "UserDefinedOwnerID", "UserDefinedOwnerCode");
			//	replueAnalyzerOwnerName.DataSource = userOwner;
			//	XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");
			//	replueAnalyzerOwnerCode.DataSource = userOwner;
			//	XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerCode, "AnalyzerOwnerID", "AnalyzerOwnerCode");
			//}
		}
		private void DataBindDataSource()
		{

			//_dataUser = AIMManager.GetDataSourceTypeUserDefined(true);		
			//_dataUser.Insert
			//(
			//	0,
			//	new DataSourceTypeInfo
			//	{
			//		DataSourceTypeID = 0,
			//		Name = "--Select--"
			//	}
			//);
			//lueDataSource.Properties.DataSource = _dataUser;
			//XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name", "TypeDisplay");
			//lueDataSource.BackColor = Color.White;

			//if (_dataUser.Count == 2)
			//{
			//	//selecting value if there is only one
			//	lueDataSource.EditValue = _dataUser[1].DataSourceTypeID;
			//}

			//if ((Int32)lueDataSource.EditValue == 0)
			//{
			//	lueDataSource.BackColor = Color.Yellow;
			//	lueDataSource.ShowPopup();				
			//}
			
		}
		#endregion

		#region Material
		private void DataBindGrid_Material()
		{
		//	Int32 rowNum = 0;
		//	Int32.TryParse(grdMaterialView.FocusedRowHandle.ToString(), out rowNum);

		//	if (lueDataSource.EditValue != null)
		//	{
		//		//grdMaterial.DataSource = AIMManager.GetUserDefinedMaterial((Int32)lueDataSource.EditValue);
		//		grdMaterial.DataSource = AIMManager.GetAnalyzerMaterial((Int32)lueDataSource.EditValue, true);

		//		grdMaterialView.FocusedRowHandle = rowNum;
		//	}
		}
		private void btnAddMaterial_Click(object sender, EventArgs e)
		{

			//try
			//{
			//	if (string.IsNullOrWhiteSpace(txtMaterialCode.Text) == true)
			//	{
			//		MessageBox.Show("Please enter Material Code.", "Warning...");
			//		txtMaterialCode.Focus();
			//		return;
			//	}
			//	if (txtMaterialName.Text.Length == 0)
			//	{
			//		MessageBox.Show("Please enter Material Name.", "Warning...");
			//		txtMaterialName.Focus();
			//		return;
			//	}
			//	if ((Int32)lueDataSource.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Data Source.", "Warning...");
			//		lueDataSource.ShowPopup();
			//		return;
			//	}
								
			//	AnalyzerMaterialInfo material = new AnalyzerMaterialInfo();
			//	material.AnalyzerMaterialID = 0;
			//	material.AnalyzerMaterialCode = txtMaterialCode.Text.ToString();
			//	material.AnalyzerMaterialName = txtMaterialName.Text.ToString();
			//	material.DataSourceTypeID = (Int32)lueDataSource.EditValue;
			//	material.IsActive = true;
			//	material.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	ErrorInfo error = new ErrorInfo();
			//	error = AIMManager.SaveAnalyzerMaterial(ref material, true);

			//	if (error.ErrorNumber > 0)
			//	{
			//		return;
			//	}
			//	DataBindGrid_Material();
			//	txtMaterialCode.Text = "";
			//	txtMaterialName.Text = "";
				
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}

		}
		private void repbtnDeleteMaterial_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			//try
			//{

			//	AnalyzerMaterialInfo materialRef = new AnalyzerMaterialInfo();
			//	materialRef = (AnalyzerMaterialInfo)grdMaterialView.GetFocusedRow();
								
			//	//if material exists on cross-ref, do not allow delete		
			//	AIMMaterialRefAnalyzerMaterialInfo xref = new AIMMaterialRefAnalyzerMaterialInfo();
			//	xref = AIMManager.GetAIMMaterialRefAnalyzerMaterialID(materialRef.AnalyzerMaterialID);
								
			//	if (xref != null)
			//	{
			//		MessageBox.Show("This material exists on the Material cross-reference table. You must first delete it under Setup > Cross Reference > Analyzer > External menu item.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}

			//	AIMManager.DeleteAnalyzerMaterial(materialRef.AnalyzerMaterialID);				
			//	DataBindGrid_Material();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}		
		private void btnPreviewGridResultsMaterial_Click(object sender, EventArgs e)
		{
			//PreviewGridResults(grdMaterial, "User-Defined Materials");						
		}
		private void txtMaterialCode_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void txtMaterialName_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		#endregion

		#region Owner
		private void DataBindGrid_Owner()
		{
			//Int32 rowNum = 0;
			//Int32.TryParse(grdOwnerView.FocusedRowHandle.ToString(), out rowNum);

			//if (lueDataSource.EditValue != null)
			//{	
			//	//9/13/16
			//	//grdOwner.DataSource = AIMManager.GetUserDefinedOwner((Int32)lueDataSource.EditValue);
			//	grdOwner.DataSource = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue);

			//	grdOwnerView.FocusedRowHandle = rowNum;
			//}
		}
		private void btnAddOwner_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	if (string.IsNullOrWhiteSpace(txtOwnerCode.Text) == true)
			//	{
			//		MessageBox.Show("Please enter Owner Code.", "Warning...");
			//		txtOwnerCode.Focus();
			//		return;
			//	}
			//	if (txtOwnerName.Text.Length == 0)
			//	{
			//		MessageBox.Show("Please enter Owner Name.", "Warning...");
			//		txtOwnerName.Focus();
			//		return;
			//	}
			//	if ((Int32)lueDataSource.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Data Source.", "Warning...");
			//		lueDataSource.ShowPopup();
			//		return;
			//	}

			//	//9/30/16
			//	//UserDefinedOwnerInfo owner = new UserDefinedOwnerInfo();
			//	//owner.UserDefinedOwnerID = 0;
			//	//owner.UserDefinedOwnerCode = txtOwnerCode.Text.ToString();
			//	//owner.UserDefinedOwnerName = txtOwnerName.Text.ToString();
			//	//owner.DataSourceTypeID = (Int32)lueDataSource.EditValue;
			//	//owner.IsActive = true;
			//	//owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	//ErrorInfo error = new ErrorInfo();
			//	//error = AIMManager.SaveUserDefinedOwner(ref owner, true);
			//	AnalyzerOwnerInfo owner = new AnalyzerOwnerInfo();
			//	owner.AnalyzerOwnerID = 0;
			//	owner.AnalyzerOwnerCode = txtOwnerCode.Text.ToString();
			//	owner.AnalyzerOwnerName = txtOwnerName.Text.ToString();
			//	owner.DataSourceTypeID = (Int32)lueDataSource.EditValue;
			//	owner.IsActive = true;
			//	owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	ErrorInfo error = new ErrorInfo();
			//	error = AIMManager.SaveAnalyzerOwner(ref owner, true);

			//	if (error.ErrorNumber > 0)
			//	{
			//		return;
			//	}
			//	DataBindGrid_Owner();
			//	DataBindOwnerlue();												
			//	txtOwnerCode.Text = "";
			//	txtOwnerName.Text = "";
				
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}

		}
		private void repbtnDeleteOwner_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			//try
			//{
			//	//9/30/16
			//	//UserDefinedOwnerInfo ownerRef = new UserDefinedOwnerInfo();
			//	//ownerRef = (UserDefinedOwnerInfo)grdOwnerView.GetFocusedRow();
			//	AnalyzerOwnerInfo ownerRef = new AnalyzerOwnerInfo();
			//	ownerRef = (AnalyzerOwnerInfo)grdOwnerView.GetFocusedRow();

			//	//if owner exists on cross-ref, do not allow delete

			//	//10/3/16
			//	//List<UserDefinedCrossRefAIMOwnerInfo> xref = new List<UserDefinedCrossRefAIMOwnerInfo>();
			//	List<AIMOwnerRefAnalyzerOwnerInfo> xref = new List<AIMOwnerRefAnalyzerOwnerInfo>();

			//	//9/30/16
			//	//xref = AIMManager.GetUserDefinedCrossRefAimOwner(ownerRef.UserDefinedOwnerID);
			//	xref = AIMManager.GetAIMOwnerRefAnalyzerOwner(ownerRef.AnalyzerOwnerID);

			//	if (xref != null && xref.Count != 0)
			//	{
			//		MessageBox.Show("This owner exists on the Owner cross-reference table. You must first delete it under Setup > Cross Reference > Analyzer > External menu item.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}

			//	//if owner exists on owner-location table, do not allow delete

			//	//9/30/16
			//	//List<UserDefinedOwnerLocationInfo> result = new List<UserDefinedOwnerLocationInfo>();
			//	//result = AIMManager.GetUserDefinedOwnerLocation(ownerRef.UserDefinedOwnerID);
			//	List<AnalyzerOwnerLocationInfo> result = new List<AnalyzerOwnerLocationInfo>();
			//	result = AIMManager.GetAnalyzerOwnerLocation(ownerRef.AnalyzerOwnerID);
				

			//	if (result != null && result.Count != 0)
			//	{
			//		MessageBox.Show("This owner exists on the Owner Location table. You must first delete it under the Owner Location tab before deleting it here.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}			 											

			//	//9/30/16
			//	//AIMManager.DeleteUserDefinedOwner(ownerRef.UserDefinedOwnerID);
			//	AIMManager.DeleteAnalyzerOwner(ownerRef.AnalyzerOwnerID);

			//	DataBindGrid_Owner();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void btnPreviewGridResultsOwner_Click(object sender, EventArgs e)
		{
			//PreviewGridResults(grdOwner, "Analyzer Owners");			
		}
		private void txtOwnerCode_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void txtOwnerName_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void lueOwner_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		#endregion

		#region Supplier
		private void DataBindGrid_Supplier()
		{
			//Int32 rowNum = 0;
			//Int32.TryParse(grdSupplierView.FocusedRowHandle.ToString(), out rowNum);

			//if (lueDataSource.EditValue != null)
			//{
			//	grdSupplier.DataSource = AIMManager.GetUserDefinedSupplier((Int32)lueDataSource.EditValue);
			//	grdSupplierView.FocusedRowHandle = rowNum;
			//}

		}
		private void btnAddSupplier_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	if (string.IsNullOrWhiteSpace(txtSupplierCode.Text) == true)
			//	{
			//		MessageBox.Show("Please enter Supplier Code.", "Warning...");
			//		txtSupplierCode.Focus();
			//		return;
			//	}
			//	if (txtSupplierName.Text.Length == 0)
			//	{
			//		MessageBox.Show("Please enter Supplier Name.", "Warning...");
			//		txtSupplierName.Focus();
			//		return;
			//	}
			//	if ((Int32)lueDataSource.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Data Source.", "Warning...");
			//		lueDataSource.ShowPopup();
			//		return;
			//	}

			//	UserDefinedSupplierInfo supplier = new UserDefinedSupplierInfo();
			//	supplier.UserDefinedSupplierID = 0;
			//	supplier.UserDefinedSupplierCode = txtSupplierCode.Text.ToString();
			//	supplier.UserDefinedSupplierName = txtSupplierName.Text.ToString();
			//	supplier.DataSourceTypeID = (Int32)lueDataSource.EditValue;
			//	supplier.IsActive = true;
			//	supplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	ErrorInfo error = new ErrorInfo();
			//	error = AIMManager.SaveUserDefinedSupplier(ref supplier, true);

			//	if (error.ErrorNumber > 0)
			//	{
			//		return;
			//	}
			//	DataBindGrid_Supplier();
			//	DataBindSourcelue();
			//	txtSupplierCode.Text = "";
			//	txtSupplierName.Text = "";

			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}

		}
		private void repbtnDeleteSupplier_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			//try
			//{
			//	UserDefinedSupplierInfo supplierRef = new UserDefinedSupplierInfo();
			//	supplierRef = (UserDefinedSupplierInfo)grdSupplierView.GetFocusedRow();

			//	//if supplier exists on cross-ref, do not allow delete		
			//	List<UserDefinedCrossRefAIMSupplierInfo> xref = new List<UserDefinedCrossRefAIMSupplierInfo>();
			//	xref = AIMManager.GetUserDefinedCrossRefAimSupplier(supplierRef.UserDefinedSupplierID);
			//	if (xref != null && xref.Count != 0)
			//	{
			//		MessageBox.Show("This supplier exists on the Supplier cross-reference table. You must first delete it under Setup > Cross Reference > Analyzer > External menu item.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}

			//	//if supplier exists on supplier-location table, do not allow delete
			//	List<UserDefinedSupplierLocationInfo> result = new List<UserDefinedSupplierLocationInfo>();
			//	result = AIMManager.GetUserDefinedSupplierLocation(supplierRef.UserDefinedSupplierID);
			//	if (result != null && result.Count != 0)
			//	{
			//		MessageBox.Show("This supplier exists on the Supplier Location table. You must first delete it under the Supplier Location tab before deleting it here.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}

			//	AIMManager.DeleteUserDefinedSupplier(supplierRef.UserDefinedSupplierID);
			//	DataBindGrid_Supplier();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void btnPreviewGridResultsSupplier_Click(object sender, EventArgs e)
		{
			//PreviewGridResults(grdSupplier, "User-Defined Suppliers");
		}
		private void txtSupplierCode_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void txtSupplierName_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void lueSupplier_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}

		#endregion

		#region Supplier Location
		private void DataBindGrid_SupplierLocation()
		{
			//Int32 rowNum = 0;
			//Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out rowNum);

			//if (lueDataSource.EditValue != null)
			//{

			//	grdSupplierLocation.DataSource = AIMManager.GetUserDefinedSupplierLocationByDataSource((Int32)lueDataSource.EditValue);
			//	grdSupplierLocationView.FocusedRowHandle = rowNum;
			//}
		}
		private void txtSourceLocationName_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void btnAddSupplierLocation_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	if ((Int32)lueSupplier.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Supplier Code.", "Warning...");
			//		lueSupplier.ShowPopup();
			//		return;
			//	}
			//	if (string.IsNullOrWhiteSpace(txtSupplierLocationCode.Text) == true)
			//	{
			//		MessageBox.Show("Please enter Supplier Location Code.", "Warning...");
			//		txtSupplierLocationCode.Focus();
			//		return;
			//	}
			//	if (txtSupplierLocationName.Text.Length == 0)
			//	{
			//		MessageBox.Show("Please enter Supplier Location Name.", "Warning...");
			//		txtSupplierLocationName.Focus();
			//		return;
			//	}
			//	if ((Int32)lueDataSource.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Data Source.", "Warning...");
			//		lueDataSource.ShowPopup();
			//		return;
			//	}

			//	UserDefinedSupplierLocationInfo supplierLoc = new UserDefinedSupplierLocationInfo();
			//	supplierLoc.UserDefinedSupplierLocationID = 0;
			//	supplierLoc.UserDefinedSupplierID = (Int32)lueSupplier.EditValue;
			//	supplierLoc.UserDefinedSupplierLocationCode = txtSupplierLocationCode.Text.ToString();
			//	supplierLoc.UserDefinedSupplierLocationName = txtSupplierLocationName.Text.ToString();
			//	supplierLoc.DataSourceTypeID = (Int32)lueDataSource.EditValue;
			//	supplierLoc.IsActive = true;
			//	supplierLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	ErrorInfo error = new ErrorInfo();
			//	error = AIMManager.SaveUserDefinedSupplierLocation(ref supplierLoc, true);

			//	if (error.ErrorNumber > 0)
			//	{
			//		return;
			//	}
			//	DataBindGrid_SupplierLocation();
			//	lueSupplier.EditValue = 0;
			//	txtSupplierLocationCode.Text = "";
			//	txtSupplierLocationName.Text = "";

			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void repbtnDeleteSupplierLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			//try
			//{
			//	UserDefinedSupplierLocationInfo supplierLocRef = new UserDefinedSupplierLocationInfo();
			//	supplierLocRef = (UserDefinedSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();

			//	//if supplier-location exists on cross-ref, do not allow delete		
			//	List<UserDefinedCrossRefAIMSupplierLocationInfo> xref = new List<UserDefinedCrossRefAIMSupplierLocationInfo>();
			//	xref = AIMManager.GetUserDefinedCrossRefAimSupplierLocation(supplierLocRef.UserDefinedSupplierLocationID);
			//	if (xref != null && xref.Count != 0)
			//	{
			//		MessageBox.Show("This supplier location exists on the Supplier-Location cross-reference table. You must first delete it under Setup > Cross Reference > Analyzer > External menu item.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}

			//	AIMManager.DeleteUserDefinedSupplierLocation(supplierLocRef.UserDefinedSupplierLocationID);
			//	DataBindGrid_SupplierLocation();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void btnPreviewGridResultsSupplierLocation_Click(object sender, EventArgs e)
		{
			//PreviewGridResults(grdSupplierLocation, "User-Defined Supplier Location");

		}
		private void txtSupplierLocationCode_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void txtSupplierLocationName_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		#endregion

		#region OwnerLocation
		private void DataBindGrid_OwnerLocation()
		{
			//Int32 rowNum = 0;
			//Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out rowNum);

			//if (lueDataSource.EditValue != null)
			//{	
			//	//10/3/16					
			//	//grdOwnerLocation.DataSource = AIMManager.GetUserDefinedOwnerLocationByDataSource((Int32)lueDataSource.EditValue);
			//	grdOwnerLocation.DataSource = AIMManager.GetAnalyzerOwnerLocationByDataSource((Int32)lueDataSource.EditValue);

			//	grdOwnerLocationView.FocusedRowHandle = rowNum;
			//}
		}
		private void btnAddOwnerLocation_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	if ((Int32)lueOwner.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Owner Code.", "Warning...");
			//		lueOwner.ShowPopup();
			//		return;
			//	}
			//	if (string.IsNullOrWhiteSpace(txtOwnerLocationCode.Text) == true)
			//	{
			//		MessageBox.Show("Please enter Owner Location Code.", "Warning...");
			//		txtOwnerLocationCode.Focus();
			//		return;
			//	}
			//	if (txtOwnerLocationName.Text.Length == 0)
			//	{
			//		MessageBox.Show("Please enter Owner Location Name.", "Warning...");
			//		txtOwnerLocationName.Focus();
			//		return;
			//	}
			//	if ((Int32)lueDataSource.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Data Source.", "Warning...");
			//		lueDataSource.ShowPopup();
			//		return;
			//	}

			//	//10/3/16
			//	//UserDefinedOwnerLocationInfo ownerLoc = new UserDefinedOwnerLocationInfo();
			//	//ownerLoc.UserDefinedOwnerLocationID = 0;
			//	//ownerLoc.UserDefinedOwnerID = (Int32)lueOwner.EditValue;
			//	//ownerLoc.UserDefinedOwnerLocationCode = txtOwnerLocationCode.Text.ToString();
			//	//ownerLoc.UserDefinedOwnerLocationName = txtOwnerLocationName.Text.ToString();
			//	AnalyzerOwnerLocationInfo ownerLoc = new AnalyzerOwnerLocationInfo();
			//	ownerLoc.AnalyzerOwnerLocationID = 0;
			//	ownerLoc.AnalyzerOwnerID = (Int32)lueOwner.EditValue;					
			//	ownerLoc.AnalyzerOwnerLocationCode = txtOwnerLocationCode.Text.ToString();
			//	ownerLoc.AnalyzerOwnerLocationName = txtOwnerLocationName.Text.ToString();

			//	ownerLoc.DataSourceTypeID = (Int32)lueDataSource.EditValue;
			//	ownerLoc.IsActive = true;
			//	ownerLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	ErrorInfo error = new ErrorInfo();

			//	//10/3/16
			//	//error = AIMManager.SaveUserDefinedOwnerLocation(ref ownerLoc, true);
			//	error = AIMManager.SaveAnalyzerOwnerLocation(ref ownerLoc, true);


			//	if (error.ErrorNumber > 0)
			//	{
			//		return;
			//	}
			//	DataBindGrid_OwnerLocation();
			//	lueOwner.EditValue = 0;
			//	txtOwnerLocationCode.Text = "";
			//	txtOwnerLocationName.Text = "";				
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}

		}
		private void repbtnDeleteOwnerLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			//try
			//{
			//	//10/3/16
			//	//UserDefinedOwnerLocationInfo ownerLocRef = new UserDefinedOwnerLocationInfo();
			//	//ownerLocRef = (UserDefinedOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
			//	AnalyzerOwnerLocationInfo ownerLocRef = new AnalyzerOwnerLocationInfo();
			//	ownerLocRef = (AnalyzerOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();

			//	//if owner-location exists on cross-ref, do not allow delete		


			//	//10/3/16
			//	//List<UserDefinedCrossRefAIMOwnerLocationInfo> xref = new List<UserDefinedCrossRefAIMOwnerLocationInfo>();
			//	//xref = AIMManager.GetUserDefinedCrossRefAimOwnerLocation(ownerLocRef.UserDefinedOwnerLocationID);
			//	List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> xref = new List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo>();
			//	xref = AIMManager.GetAnalyzerCrossRefAimOwnerLocation(ownerLocRef.AnalyzerOwnerLocationID);

			//	if (xref != null && xref.Count != 0)
			//	{
			//		MessageBox.Show("This owner location exists on the Owner-Location cross-reference table. You must first delete it under Setup > Cross Reference > Analyzer > External menu item.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}

			//	//10/3/16
			//	//AIMManager.DeleteUserDefinedOwnerLocation(ownerLocRef.UserDefinedOwnerLocationID);
			//	AIMManager.DeleteAnalyzerOwnerLocation(ownerLocRef.AnalyzerOwnerLocationID);

			//	DataBindGrid_OwnerLocation();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void btnPreviewGridResultsOwnerLocation_Click(object sender, EventArgs e)
		{
			//PreviewGridResults(grdOwnerLocation, "User-Defined Owner Locations");							
		}
		private void txtOwnerLocationCode_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		private void txtOwnerLocationName_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}

		#endregion

		#region Nutrient
		
		private void DataBindGrid_Nutrient()
		{
			//Int32 rowNum = 0;
			//Int32.TryParse(grdNutrientView.FocusedRowHandle.ToString(), out rowNum);

			//if (lueDataSource.EditValue != null)
			//{
			//	grdNutrient.DataSource = AIMManager.GetUserDefinedNutrient((Int32)lueDataSource.EditValue);
			//	grdNutrientView.FocusedRowHandle = rowNum;
			//}
		}
		private void btnAddNutrient_Click(object sender, EventArgs e)
		{
			//try
			//{
			//	if (string.IsNullOrWhiteSpace(txtNutrientCode.Text) == true)
			//	{
			//		MessageBox.Show("Please enter Nutrient Code.", "Warning...");
			//		txtNutrientCode.Focus();
			//		return;
			//	}
			//	if (txtNutrientName.Text.Length == 0)
			//	{
			//		MessageBox.Show("Please enter Nutrient Name.", "Warning...");
			//		txtNutrientName.Focus();
			//		return;
			//	}
			//	if ((Int32)lueDataSource.EditValue == 0)
			//	{
			//		MessageBox.Show("Please select Data Source.", "Warning...");
			//		lueDataSource.ShowPopup();
			//		return;
			//	}


			//	UserDefinedNutrientInfo owner = new UserDefinedNutrientInfo();
			//	owner.UserDefinedNutrientID = 0;
			//	owner.UserDefinedNutrientCode = txtNutrientCode.Text.ToString();
			//	owner.UserDefinedNutrientName = txtNutrientName.Text.ToString();
			//	owner.DataSourceTypeID = (Int32)lueDataSource.EditValue;
			//	owner.IsActive = true;
			//	owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//	ErrorInfo error = new ErrorInfo();
			//	error = AIMManager.SaveUserDefinedNutrient(ref owner);

			//	if (error.ErrorNumber > 0)
			//	{
			//		return;
			//	}
			//	DataBindGrid_Nutrient();
			//	txtNutrientCode.Text = "";
			//	txtNutrientName.Text = "";

			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void repbtnDeleteNutrient_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			//try
			//{
			//	UserDefinedNutrientInfo nutrientRef = new UserDefinedNutrientInfo();
			//	nutrientRef = (UserDefinedNutrientInfo)grdNutrientView.GetFocusedRow();

			//	//if nutrient exists on cross-ref, do not allow delete
			//	List<UserDefinedCrossRefAIMNutrientInfo> xref = new List<UserDefinedCrossRefAIMNutrientInfo>();
			//	xref = AIMManager.GetUserDefinedCrossRefAimNutrient(nutrientRef.UserDefinedNutrientID);
			//	if (xref != null && xref.Count != 0)
			//	{
			//		MessageBox.Show("This nutrient exists on the Nutrient cross-reference table. You must first delete it under Setup > Cross Reference > Analyzer > External menu item.", "Warning...", MessageBoxButtons.OK);
			//		return;
			//	}

			//	AIMManager.DeleteUserDefinedNutrient(nutrientRef.UserDefinedNutrientID);
			//	DataBindGrid_Nutrient();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(ex.Message);
			//}
		}
		private void btnPreviewGridResultsNutrient_Click(object sender, EventArgs e)
		{
			//PreviewGridResults(grdNutrient, "User-Defined Nutrients");
		}
		private void grdNutrientView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
		{
			//UserDefinedNutrientInfo nutrient = new UserDefinedNutrientInfo();
			//nutrient = (UserDefinedNutrientInfo)grdNutrientView.GetFocusedRow();
			//nutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			//AIMManager.SaveUserDefinedNutrient(ref nutrient);
			//DataBindGrid_Nutrient();
		}
		private void txtNutrientCode_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}

		private void txtNutrientName_Click(object sender, EventArgs e)
		{
			//ChecklueDataSource();
		}
		
	
	#endregion

		
		

		

		

	


	}
}
