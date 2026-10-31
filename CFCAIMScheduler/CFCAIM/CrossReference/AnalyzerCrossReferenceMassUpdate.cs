using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;


namespace CFCAIM
{
	public partial class AnalyzerCrossReferenceMassUpdate : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		Boolean _needsUpdating = false;
		
				
		public AnalyzerCrossReferenceMassUpdate()
		{
			InitializeComponent();
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

		private void AnalyzerCrossReferenceMassUpdate_Load(object sender, EventArgs e)
		{
			

			switch (AIMGlobal.analyzerRequiredField)
			{
				case "MATERIAL":					
				case "OWNER":				
				case "OWNERLOCATION":				
					colCopy.Visible = false;
					break;
				case "SUPPLIER":									
				case "SUPPLIERLOCATION":					
					colCopy.Visible = true;
					break;
				default:
					break;
			}
			DataBind();			
			SetToolTips();
			_needsUpdating = false;

			//10/6/21 - added for resize issue
			WindowState = FormWindowState.Maximized;
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
				ttip.SetToolTip(btnUpdate, "Update " + AIMGlobal.analyzerRequiredField + " Cross References");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DataBind()
		{
			grdCrossRefView.Columns["AIMIDSecond"].Visible = false;
			switch (AIMGlobal.analyzerRequiredField)
			{
				#region Material
				case "MATERIAL":
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
					grdCrossRefView.Columns["AIMID"].Caption = "Select AIM Material";
					grdCrossRefView.Columns["AnalyzerName"].Caption = AIMGlobal.analyzerDataSourceName + " Material";
					lblHeading.Text = AIMGlobal.analyzerDataSourceName + " Materials";

					SetCopyCaption("Material");

					break;
				#endregion
				#region Owner
				case "OWNER":
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
					replueAIMName.DataSource = aimOwner;
					XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMOwnerID", "OwnerName");
					grdCrossRefView.Columns["AIMID"].Caption = "AIM Owner";
					grdCrossRefView.Columns["AnalyzerName"].Caption = AIMGlobal.analyzerDataSourceName + " Owner";
					lblHeading.Text = AIMGlobal.analyzerDataSourceName + " Owners";

					SetCopyCaption("Owner");

					break;
				#endregion
				#region OwnerLocation
				case "OWNERLOCATION":

					List<AIMOwnerInfo> aimOwnerSecond = new List<AIMOwnerInfo>();
					aimOwnerSecond = AIMManager.GetAIMOwner(true);
					aimOwnerSecond.Insert
					(
						0,
						new AIMOwnerInfo
						{
							AIMOwnerID = 0,
							OwnerName = "--Select--"
						}
					);
					replueAIMNameSecond.DataSource = aimOwnerSecond;
					XtraEditors.LoadGridLookUpEdit(ref replueAIMNameSecond, "AIMOwnerID", "OwnerName");


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
					replueAIMName.DataSource = aimOwnerLocation;
					XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMOwnerLocationID", "LocationName");	
				
					grdCrossRefView.Columns["AIMID"].Caption = "AIM Owner Location";
					grdCrossRefView.Columns["AnalyzerName"].Caption = AIMGlobal.analyzerDataSourceName + " Owner Location";
					lblHeading.Text = AIMGlobal.analyzerDataSourceName + " Owner Locations";
					grdCrossRefView.Columns["AIMIDSecond"].Caption = "AIM Owner";
					grdCrossRefView.Columns["AIMIDSecond"].Visible = true;

					SetCopyCaption("Owner Location");

					break;
				#endregion
				#region Supplier
				case "SUPPLIER":
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
					replueAIMName.DataSource = aimSupplier;
					XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMSupplierID", "SupplierName");
					grdCrossRefView.Columns["AIMID"].Caption = "AIM Supplier";
					grdCrossRefView.Columns["AnalyzerName"].Caption = AIMGlobal.analyzerDataSourceName + " Supplier";
					lblHeading.Text = AIMGlobal.analyzerDataSourceName + " Suppliers";

					SetCopyCaption("Supplier");

					break;
				#endregion
				#region SupplierLocation
				case "SUPPLIERLOCATION":

					List<AIMSupplierInfo> aimSupplierSecond = new List<AIMSupplierInfo>();
					aimSupplierSecond = AIMManager.GetSupplier(true);
					aimSupplierSecond.Insert
					(
						0,
						new AIMSupplierInfo
						{
							AIMSupplierID = 0,
							SupplierName = "--Select--"
						}
					);
					replueAIMNameSecond.DataSource = aimSupplierSecond;
					XtraEditors.LoadGridLookUpEdit(ref replueAIMNameSecond, "AIMSupplierID", "SupplierName");

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
					replueAIMName.DataSource = aimSupplierLocation;
					XtraEditors.LoadGridLookUpEdit(ref replueAIMName, "AIMSupplierLocationID", "SupplierLocationName");

					grdCrossRefView.Columns["AIMID"].Caption = "AIM Supplier Location";
					grdCrossRefView.Columns["AnalyzerName"].Caption = AIMGlobal.analyzerDataSourceName + " Supplier Location";
					lblHeading.Text = AIMGlobal.analyzerDataSourceName + " Supplier Locations";
					grdCrossRefView.Columns["AIMIDSecond"].Caption = "AIM Supplier";
					grdCrossRefView.Columns["AIMIDSecond"].Visible = true;

					SetCopyCaption("Supplier Location");
					
					break;
				#endregion
				default:
					break;										
			}		


			int rowNum = 0;
			if (grdCrossRefView != null)
			{
				Int32.TryParse(grdCrossRefView.FocusedRowHandle.ToString(), out rowNum);
			}
			grdCrossRef.DataSource = AIMManager.GetAnalyzerCrossReferenceMassUpdate(AIMGlobal.analyzerDataSourceTypeID, AIMGlobal.analyzerRequiredField);
			grdCrossRefView.FocusedRowHandle = rowNum;
			

		}
		
		private void btnUpdate_Click(object sender, EventArgs e)
		{
			try
			{		
							
				switch (AIMGlobal.analyzerRequiredField)
				{
					#region Material
					case "MATERIAL":
						AIMMaterialRefAnalyzerMaterialInfo xrefMaterial = new AIMMaterialRefAnalyzerMaterialInfo();
						List<AnalyzerCrossReferenceMassUpdateInfo> gridMaterial = new List<AnalyzerCrossReferenceMassUpdateInfo>();
						gridMaterial = (List<AnalyzerCrossReferenceMassUpdateInfo>)grdCrossRef.DataSource;
						foreach (AnalyzerCrossReferenceMassUpdateInfo item in gridMaterial)
						{
							if (item.AIMID != 0)
							{								
								xrefMaterial.AIMMaterialRefAnalyzerMaterialID = item.AnalyzerCrossReferenceMassUpdateID;
								xrefMaterial.AnalyzerMaterialID = item.AnalyzerID;
								xrefMaterial.AIMMaterialID = item.AIMID;
								xrefMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								xrefMaterial.UpdateStamp = DateTime.Now;
								ErrorInfo error = new ErrorInfo();
								error = AIMManager.SaveAIMMaterialRefAnalyzerMaterial(ref xrefMaterial); 
								if (error.ErrorNumber > 0)
								{
									return;
								}
							}
						}
						break;
					#endregion
					#region Owner
					case "OWNER":
						AIMOwnerRefAnalyzerOwnerInfo xrefOwner = new AIMOwnerRefAnalyzerOwnerInfo();
						List<AnalyzerCrossReferenceMassUpdateInfo> gridOwner = new List<AnalyzerCrossReferenceMassUpdateInfo>();
						gridOwner = (List<AnalyzerCrossReferenceMassUpdateInfo>)grdCrossRef.DataSource;
						foreach (AnalyzerCrossReferenceMassUpdateInfo item in gridOwner)
						{
							if (item.AIMID != 0)
							{
								xrefOwner.AIMOwnerRefAnalyzerOwnerID = item.AnalyzerCrossReferenceMassUpdateID;
								xrefOwner.AnalyzerOwnerID = item.AnalyzerID;
								xrefOwner.AIMOwnerID = item.AIMID;	 						
								xrefOwner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								xrefOwner.UpdateStamp = DateTime.Now;
								ErrorInfo error = new ErrorInfo();								
								error = AIMManager.SaveAIMOwnerRefAnalyzerOwner(ref xrefOwner);
								if (error.ErrorNumber > 0)
								{
									return;
								}
							}
						}
						break;
					#endregion
					#region OwnerLocation
					case "OWNERLOCATION":						
						AIMOwnerLocationRefAnalyzerOwnerLocationInfo xrefOwnerLoc = new AIMOwnerLocationRefAnalyzerOwnerLocationInfo();						
						List<AnalyzerCrossReferenceMassUpdateInfo> gridOwnerLoc = new List<AnalyzerCrossReferenceMassUpdateInfo>();
						gridOwnerLoc = (List<AnalyzerCrossReferenceMassUpdateInfo>)grdCrossRef.DataSource;
						foreach (AnalyzerCrossReferenceMassUpdateInfo item in gridOwnerLoc)
						{
							if (item.AIMID != 0)
							{
								xrefOwnerLoc.AIMOwnerLocationRefAnalyzerOwnerLocationID = item.AnalyzerCrossReferenceMassUpdateID;
								xrefOwnerLoc.AnalyzerOwnerLocationID = item.AnalyzerID;
								xrefOwnerLoc.AnalyzerOwnerID = item.AnalyzerIDSecond;
								xrefOwnerLoc.AIMOwnerID = item.AIMIDSecond;
								xrefOwnerLoc.AIMOwnerLocationID = item.AIMID;
								xrefOwnerLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								xrefOwnerLoc.UpdateStamp = DateTime.Now;
								ErrorInfo error = new ErrorInfo();
								error = AIMManager.SaveAnalyzerCrossRefAimOwnerLocation(ref xrefOwnerLoc);
								if (error.ErrorNumber > 0)
								{
									return;
								}
							}
						}
						break;

					#endregion
					#region Supplier
					case "SUPPLIER":
						AIMSupplierRefAnalyzerSupplierInfo xref = new AIMSupplierRefAnalyzerSupplierInfo();
						List<AnalyzerCrossReferenceMassUpdateInfo> grid = new List<AnalyzerCrossReferenceMassUpdateInfo>();
						grid = (List<AnalyzerCrossReferenceMassUpdateInfo>)grdCrossRef.DataSource;
						foreach (AnalyzerCrossReferenceMassUpdateInfo item in grid)
						{
							if (item.AIMID != 0)
							{
								xref.AIMSupplierRefAnalyzerSupplierID = item.AnalyzerCrossReferenceMassUpdateID;
								xref.AnalyzerSupplierID = item.AnalyzerID;
								xref.AIMSupplierID = item.AIMID;
								xref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								xref.UpdateStamp = DateTime.Now;
								ErrorInfo error = new ErrorInfo();
								error = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref xref);
								if (error.ErrorNumber > 0)
								{
									return;
								}
							}
						}
						break;
					#endregion
					#region SupplierLocation
					case "SUPPLIERLOCATION":
						AIMSupplierLocationRefAnalyzerSupplierLocationInfo xrefSupplierLoc = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
						List<AnalyzerCrossReferenceMassUpdateInfo> gridSupplierLoc = new List<AnalyzerCrossReferenceMassUpdateInfo>();
						gridSupplierLoc = (List<AnalyzerCrossReferenceMassUpdateInfo>)grdCrossRef.DataSource;
						foreach (AnalyzerCrossReferenceMassUpdateInfo item in gridSupplierLoc)
						{
							if (item.AIMID != 0)
							{
								xrefSupplierLoc.AIMSupplierLocationRefAnalyzerSupplierLocationID = item.AnalyzerCrossReferenceMassUpdateID;
								xrefSupplierLoc.AnalyzerSupplierLocationID = item.AnalyzerID;
								xrefSupplierLoc.AnalyzerSupplierID = item.AnalyzerIDSecond;
								xrefSupplierLoc.AIMSupplierID = item.AIMIDSecond;
								xrefSupplierLoc.AIMSupplierLocationID = item.AIMID;
								xrefSupplierLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								xrefSupplierLoc.UpdateStamp = DateTime.Now;
								ErrorInfo error = new ErrorInfo();
								error = AIMManager.SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref xrefSupplierLoc);
								if (error.ErrorNumber > 0)
								{
									return;
								}
							}
						}
						break;
					#endregion
					default:
						break;
				}
				_needsUpdating = false;
				btnRefresh.PerformClick();
						
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}		
		}

		private void replueAIMNameSecond_EditValueChanged(object sender, EventArgs e)
		{
			grdCrossRefView.PostEditor();
			grdCrossRefView.SetFocusedRowCellValue(colAimSecondField, null);
						
		}
		
		private void repbtnCopy_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{	

				//Material, Owner and Ownerlocation currently do not have the quick-update ability on this form.					

				DialogResult dr = new DialogResult();
				ErrorInfo result = new ErrorInfo();

				Int32 rowNum = 0;			
				Int32 index = 0;			
				if (grdCrossRefView != null)
				{
					Int32.TryParse(grdCrossRefView.FocusedRowHandle.ToString(), out rowNum);
				}
				index = grdCrossRefView.GetDataSourceRowIndex(rowNum);

				AnalyzerCrossReferenceMassUpdateInfo xref = new AnalyzerCrossReferenceMassUpdateInfo();
				xref = (AnalyzerCrossReferenceMassUpdateInfo)grdCrossRefView.GetFocusedRow();
				ErrorInfo error = new ErrorInfo();											 
				switch (AIMGlobal.analyzerRequiredField)
				{ 
					case "SUPPLIER":
						error = AIMManager.SaveAnalyzerSupplierToAim(xref.AnalyzerID, xref.CFCUserID);
						if (error.ErrorNumber > 0)
						{
							if (error.ErrorNumber == 1)
							{
								dr = MessageBox.Show("Supplier already exists on the AIM table. Generate cross-reference?", "Warning...", MessageBoxButtons.YesNoCancel);
								if (dr != DialogResult.Yes)
								{
									return;
								}

								//update x-ref
								AIMSupplierRefAnalyzerSupplierInfo supplierXref = new AIMSupplierRefAnalyzerSupplierInfo();
								supplierXref.AIMSupplierRefAnalyzerSupplierID = 0;
								supplierXref.AnalyzerSupplierID = xref.AnalyzerID;
								supplierXref.AIMSupplierID = error.ReturnIntegerValue;
								supplierXref.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
								supplierXref.UpdateStamp = DateTime.Now;
								ErrorInfo errorSource = new ErrorInfo();
								errorSource = AIMManager.SaveAIMSupplierRefAnalyzerSupplier(ref supplierXref);
								if (errorSource.ErrorNumber > 0)
								{
									dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
									return;
								}
								else 
								{
									DataBind();
								}
							}

							else
							{
								dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
								return;
							}
							return;
						}
						break;
					case "SUPPLIERLOCATION":																
						error = AIMManager.SaveAnalyzerSupplierLocationToAim(xref.AnalyzerID, xref.AnalyzerIDSecond, xref.CFCUserID);
						if (error.ErrorNumber > 0)
						{
							if (error.ErrorNumber == 1)
							{
								dr = MessageBox.Show("Supplier Location already exists on the AIM table. Generate cross-reference?", "Warning...", MessageBoxButtons.YesNoCancel);
								if (dr != DialogResult.Yes)
								{
									return;
								}

								//update x-ref
								AIMSupplierLocationRefAnalyzerSupplierLocationInfo supplierLocXref = new AIMSupplierLocationRefAnalyzerSupplierLocationInfo();
								supplierLocXref.AIMSupplierLocationRefAnalyzerSupplierLocationID = 0;
								supplierLocXref.AnalyzerSupplierLocationID = xref.AnalyzerID;
								supplierLocXref.AnalyzerSupplierID = xref.AnalyzerIDSecond;	 
								supplierLocXref.AIMSupplierLocationID = error.ReturnIntegerValue;
								supplierLocXref.AIMSupplierID = error.ReturnSecondIntegerValue;
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
									DataBind();
								}
							}
							else
							{
								dr = MessageBox.Show(error.ErrorMessage, "Warning...", MessageBoxButtons.OK);
								return;
							}
							return;
						}
						break;
					default:
						break;
				}
			
				DataBind();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}		
		}
		
		private void btnRefresh_Click(object sender, EventArgs e)
		{
			DataBind();
		}

		private void SetCopyCaption(String strField)
		{
			if (colCopy.Visible == true)
			{
				colCopy.Caption = "Create AIM " + strField + " and Cross Reference";
			}
		}

		private void grdCrossRefView_RowUpdated(Object sender, RowObjectEventArgs e)
		{
			_needsUpdating = true;
		}

		private void AnalyzerCrossReferenceMassUpdate_FormClosing(Object sender, FormClosingEventArgs e)
		{
			if (_needsUpdating == true)
			{
				DialogResult dr = new DialogResult();

				dr = MessageBox.Show("Cross references have NOT been updated. Continue closing this form?", "Warning...", MessageBoxButtons.YesNo);
				if (dr == DialogResult.No) e.Cancel = true;
				else e.Cancel = false;
			}
		}

		private void grdCrossRefView_ShownEditor(Object sender, EventArgs e)
		{
			if ((AIMGlobal.analyzerRequiredField) == "OWNERLOCATION" || (AIMGlobal.analyzerRequiredField) == "SUPPLIERLOCATION")
			{
				ColumnView view = (ColumnView)sender;
				if (view.FocusedColumn.FieldName == "AIMID")
				{
					Int32 id = 0;
					LookUpEdit editor = (LookUpEdit)view.ActiveEditor;
					Int32.TryParse(view.GetFocusedRowCellValue(colAimSecondField).ToString(), out id);
					switch (AIMGlobal.analyzerRequiredField)
					{
						case "OWNERLOCATION":
							List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
							aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(id);
							aimOwnerLocation.Insert
							(
								0,
								new AIMOwnerLocationInfo
								{
									AIMOwnerLocationID = 0,
									LocationName = "--Select--"
								}
							);
							editor.Properties.DataSource = aimOwnerLocation;
							break;

						case "SUPPLIERLOCATION":
							List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
							aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(id);
							aimSupplierLocation.Insert
							(
								0,
								new AIMSupplierLocationInfo
								{
									AIMSupplierLocationID = 0,
									SupplierLocationName = "--Select--"
								}
							);
							editor.Properties.DataSource = aimSupplierLocation;
							break;

						default:
							break;
					}

				}
			}

		}
	}
}
