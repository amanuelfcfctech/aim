using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using DevExpress.XtraGrid.Views.Base;
using Core.Library.DevExpress;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;


namespace CFCAIM.Maintenance
{
    public partial class Analyzer : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		List<DataSourceTypeInfo> _dataUser = new List<DataSourceTypeInfo>();
		Int32 _status = (Int32)EnumSampleResultStatus.Active;
		

		public Analyzer()
		{
			InitializeComponent();        
			CFCAIMCore.Global.EnableDropDownSelectAll(this);        //task 1119 FG 7/21/2026
        }  		

		private void Analyzer_Load(object sender, EventArgs e)
		{
			try
			{				
				//7/30/20
				Int32 datasourceID = 0;
                if (lueDataSource.EditValue != null)
                {
                    datasourceID = (Int32)lueDataSource.EditValue;
                }
                		
				rbAll.Checked = true;
				DataBindDataSource("All", 0);	 				
				lueDataSource.EditValue = 0;				
				SetToolTips();				
				ClearTextFields();
                
                if (AIMGlobal.importDataSourceTypeID != 0 || AIMGlobal.analyzerDataSourceTypeID != 0)
                {
                    if (AIMGlobal.importDataBindDataSourceType == "UserDefined")
                    {
                        rbUserDefined.Checked = true;						
						lueDataSource.EditValue = AIMGlobal.importDataSourceTypeID;				                        						
					}
                }
                else
                {
                    //7/30/20
                    //lueDataSource.EditValue = 0;
                    //rbAll.Checked = true;
                    lueDataSource.EditValue = datasourceID;
                    if (datasourceID == 0)
                    {
                        rbAll.Checked = true;
                    }
                    
                }
                                
                //if (AIMGlobal.analyzerRequiredField == "MATERIAL")
                if (AIMGlobal.analyzerRequiredField == "MATERIAL" || AIMGlobal.analyzerMaterialTab == true)     //7/30/20
                    {
                    tabControl1.SelectedIndex = 0;
                    grdMaterial.Focus();
                }
                //else if (AIMGlobal.analyzerRequiredField == "NUTRIENT")
                else if (AIMGlobal.analyzerRequiredField == "NUTRIENT" || AIMGlobal.analyzerNutrientTab == true)                //7/30/20
                {
                    tabControl1.SelectedIndex = 1;
                    grdNutrient.Focus();
                }
                //else if (AIMGlobal.analyzerRequiredField == "OWNER")
                if (AIMGlobal.analyzerRequiredField == "OWNER" || AIMGlobal.analyzerOwnerTab == true)           //7/30/20
                {
                    tabControl1.SelectedIndex = 2;
                    grdOwner.Focus();
                }
                //else if (AIMGlobal.analyzerRequiredField == "OWNERLOCATION")
                else if (AIMGlobal.analyzerRequiredField == "OWNERLOCATION" || AIMGlobal.analyzerOwnerLocationTab == true)      //7/30/20
                {
                    tabControl1.SelectedIndex = 3;
                    grdOwnerLocation.Focus();
                }
                //else if (AIMGlobal.analyzerRequiredField == "SUPPLIER")
                else if (AIMGlobal.analyzerRequiredField == "SUPPLIER" || AIMGlobal.analyzerSupplierTab == true)                //7/30/20
                {
                    tabControl1.SelectedIndex = 4;
                    grdSupplier.Focus();
                }
                //else if (AIMGlobal.analyzerRequiredField == "SUPPLIERLOCATION")
                else if (AIMGlobal.analyzerRequiredField == "SUPPLIERLOCATION" || AIMGlobal.analyzerSupplierLocationTab == true)    //7/30/20
                {
                    tabControl1.SelectedIndex = 5;
                    grdSupplierLocation.Focus();
                }                
                else if (AIMGlobal.analyzerRequiredField == "EQUATIONPARAMETER" || AIMGlobal.analyzerEquationParameterTab == true)  //7/30/20
                {
                    tabControl1.SelectedIndex = 6;
                    grdQualityParameters.Focus();
                }

				//12/21/20
				else if (AIMGlobal.analyzerRequiredField == "SUPPLIERLOCATIONCOMBINED" || AIMGlobal.analyzerSupplierLocationCombinedTab == true)
				{
					tabControl1.SelectedIndex = 7;
					grdSupplierLocationCombined.Focus();
				}

				else
                {
                    tabControl1.SelectedIndex = 0;
                    grdMaterial.Focus();
                }

                WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DataBindGrid()
		{
			DataBindGrid_Material();
			DataBindGrid_Owner();
			DataBindGrid_OwnerLocation();
			DataBindGrid_Supplier();
			DataBindGrid_SupplierLocation();
			DataBindGrid_Nutrient();
			DataBindGrid_QualityControl();

			//12/21/20
			DataBindGrid_SupplierLocationCombined();

		}
		private void DataBindlue()
		{
			DataBindOwnerlue();
			DataBindSupplierlue();

			//1/5/18
			DataBindQualityControllue();
		}
		private void DataBindDataSource(String type, Int32 id)
		{
		
			//6/19/17 - added to auto set the datasource dropdown (if coming from import)
			if (AIMGlobal.importDataSourceTypeID != 0 || AIMGlobal.analyzerDataSourceTypeID != 0)
			{
				if (AIMGlobal.importDataBindDataSourceType == "UserDefined")
				{
					rbUserDefined.Checked = true;					
					id = AIMGlobal.importDataSourceTypeID;
					
				}
			}			
			//---------------------------------------

			if (AIMGlobal.importDataBindDataSourceType != "")
			{
				type = AIMGlobal.importDataBindDataSourceType;
			}

			//--------------------------------------------------------
			if (type == "All")
			{
				_dataUser = AIMManager.GetDataSourceType(true);
			}
			if (type == "System")
			{
				_dataUser = AIMManager.GetDataSourceTypeByUserDefinedSwitch(false);
			}
			if (type == "UserDefined")
			{
				_dataUser = AIMManager.GetDataSourceTypeByUserDefinedSwitch(true);
			}

			//--------------------------------------------------------
			if (type == "All")
            {
                _dataUser.Insert
             (
                 0,
                    new DataSourceTypeInfo
                    {
                        DataSourceTypeID = 0,
                        Name = "--Select ALL--",
                     }
                );
            }			
            else
            {
                _dataUser.Insert
              (
                  0,
                  new DataSourceTypeInfo
                  {
                      DataSourceTypeID = 0,
                      Name = "--Select--"
                  }
              );
            }

			//--------------------------------------------------------
			lueDataSource.Properties.DataSource = _dataUser;			
			XtraEditors.LoadLookUpEdit(ref lueDataSource, "DataSourceTypeID", "Name", "TypeDisplay");																											

			lueDataSource.Properties.Columns["TypeDisplay"].Caption = "Type";
			lueDataSource.BackColor = Color.White;
			lueDataSource.EditValue = id;

			if ((Int32)lueDataSource.EditValue == 0)
            {
                lueDataSource.BackColor = Color.LightYellow;
            }

            if (_dataUser.Count == 2)
			{
				//selecting value if there is only one
				lueDataSource.EditValue = _dataUser[1].DataSourceTypeID;
			}


			//--------------------------------------------------------
			//show or hide tab control
			if ((type == "System" || type == "UserDefined") && (Int32)lueDataSource.EditValue == 0)
            {
                tabControl1.Visible = false;
            }
            else
            {
                tabControl1.Visible = true;
            }
        }
		private void DataBindQualityControllue()
		{
			List<YesNoInfo> yesNo = new List<YesNoInfo>();
			yesNo.Insert
			(
				0,
				new YesNoInfo
				{
					YesNoID = 0,
					YesNoString = "No"
				}
			);
			yesNo.Insert
			(
				1,
				new YesNoInfo
				{
					YesNoID = 1,
					YesNoString = "Yes"
				}
			);

			lueParameterIsNumeric.Properties.DataSource = yesNo;
			XtraEditors.LoadLookUpEdit(ref lueParameterIsNumeric, "YesNoID", "YesNoString");
			lueParameterIsNumeric.Properties.Columns["YesNoString"].Caption = " ";
			lueParameterIsNumeric.Properties.Columns["YesNoID"].Visible = false;
			lueParameterIsNumeric.EditValue = 1;

			//populating replue            
			replueNumericParameter.DataSource = yesNo;
			XtraEditors.LoadGridLookUpEdit(ref replueNumericParameter, "YesNoID", "YesNoString");
			replueNumericParameter.Columns["YesNoString"].Visible = true;

		}				
		
		private void rbBoth_CheckedChanged(Object sender, EventArgs e)
		{
			SetStatus();
			ShowHideFields();
			DataBindGrid_Material();
			DataBindGrid_Nutrient();
			DataBindGrid_Owner();
			DataBindGrid_OwnerLocation();
			DataBindGrid_Supplier();
			DataBindGrid_SupplierLocation();

			//12/21/20
			DataBindGrid_SupplierLocationCombined();
		}
		private void rbDeletedRecords_CheckedChanged(Object sender, EventArgs e)
		{
			//6/15/17			
			SetStatus();
			ShowHideFields();
			DataBindGrid_Material();
			DataBindGrid_Nutrient();
			DataBindGrid_Owner();
			DataBindGrid_OwnerLocation();
			DataBindGrid_Supplier();
			DataBindGrid_SupplierLocation();

			//12/21/20
			DataBindGrid_SupplierLocationCombined();
		}
		private void rbActiveRecords_CheckedChanged(object sender, EventArgs e)
		{
			try
			{									
				SetStatus();
				ShowHideFields();	  										
				DataBindGrid_Material();
				DataBindGrid_Nutrient();
				DataBindGrid_Owner();
				DataBindGrid_OwnerLocation();
				DataBindGrid_Supplier();
				DataBindGrid_SupplierLocation();

				//12/21/20
				DataBindGrid_SupplierLocationCombined();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void rbAll_CheckedChanged(Object sender, EventArgs e)
		{
			try
			{
				
				if (rbAll.Checked == true)
				{
					lueDataSource.EditValue = 0;
					DataBindDataSource("All", 0);					
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
					if (lueDataSource.EditValue != null)
					{  

						//if a datasource has already been selected, see if it's user-defined or system
						if ((Int32)lueDataSource.EditValue != 0)
						{
							//currently system datasource is selected
							if (checkUserDefinedSwitch() == false)
							{
								lueDataSource.EditValue = 0;
								DataBindDataSource("UserDefined", (Int32)lueDataSource.EditValue);
								lueDataSource.ShowPopup();
							}
							else
							{
								DataBindDataSource("UserDefined", (Int32)lueDataSource.EditValue);
							}
						}
						else
						{
							DataBindDataSource("UserDefined", 0);
							lueDataSource.ShowPopup();
						}					
					}
					else
					{
						DataBindDataSource("UserDefined", 0);
						lueDataSource.ShowPopup();
					}
				}
				else
				{
					lueDataSource.EditValue = 0;
					DataBindDataSource("System", 0);
					lueDataSource.ShowPopup();
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
					if ((Int32)lueDataSource.EditValue != 0)
					{
						//currently user-defined datasource is selected
						if (checkUserDefinedSwitch() == true)
						{
							lueDataSource.EditValue = 0;
							DataBindDataSource("System", (Int32)lueDataSource.EditValue);
							lueDataSource.ShowPopup();
						}
						else
						{
							DataBindDataSource("System", (Int32)lueDataSource.EditValue);
						}
					}
					else
					{
						DataBindDataSource("System", 0);
						lueDataSource.ShowPopup();
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

		private Boolean checkUserDefinedSwitch()
		{
			Boolean rtn = false;

			DataSourceTypeInfo type = new DataSourceTypeInfo();
			type = AIMManager.GetDataSourceType_DataSourceTypeID((Int32)lueDataSource.EditValue);
			if (type != null)
			{
				if (type.IsUserDefined == true)
				{
					rtn = true;
				}
			}
			return rtn;
		}
		private void lueDataSource_EditValueChanged(Object sender, EventArgs e)
		{
			tabControl1.Visible = true;

			//------------------------------------------------
			//If "All" is selected as datasource, do not allow user to ADD 
			LockUnlockPanels(true);
			SetColumnsReadOnly(false);
			if ((Int32)lueDataSource.EditValue == 0)
			{
				LockUnlockPanels(false);
				SetColumnsReadOnly(true);
			}
			//------------------------------------------------

			DataBindlue();
			DataBindGrid();
			lueDataSource.BackColor = Color.White;

			if ((Int32)lueDataSource.EditValue != 0)
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
		private void LockUnlockPanels(Boolean ans)
		{
			pnlAddMaterial.Enabled = ans;
			pnlAddNutrient.Enabled = ans;
			pnlOwner.Enabled = ans;
			pnlOwnerLocation.Enabled = ans;
			pnlAddSupplier.Enabled = ans;
			pnlAddSupplierLocation.Enabled = ans;
			pnlAddQualityControlParameter.Enabled = ans;

			//12/21/20
			pnlAddSupplierLocationCombined.Enabled = ans;
		}
		private void SetColumnsReadOnly(Boolean ans)
		{
			//material		 			
			grdMaterialView.Columns.ColumnByName("colAnalyzerMaterialCode").OptionsColumn.ReadOnly = ans;
			grdMaterialView.Columns.ColumnByName("colAnalyzerMaterialName").OptionsColumn.ReadOnly = ans;
			grdMaterialView.Columns.ColumnByName("colDataSourceTypeMaterial").OptionsColumn.ReadOnly = ans;
			grdMaterialView.Columns.ColumnByName("colImageMaterial").OptionsColumn.ReadOnly = ans;

			//nutrient
			grdNutrientView.Columns.ColumnByName("colAnalyzerNutrientCode").OptionsColumn.ReadOnly = ans;
			grdNutrientView.Columns.ColumnByName("colAnalyzerNutrientName").OptionsColumn.ReadOnly = ans;
			grdNutrientView.Columns.ColumnByName("colUnits").OptionsColumn.ReadOnly = ans;
			grdNutrientView.Columns.ColumnByName("colDataSourceTypeNutrient").OptionsColumn.ReadOnly = ans;
			grdNutrientView.Columns.ColumnByName("colConversionFactor").OptionsColumn.ReadOnly = ans;
			grdNutrientView.Columns.ColumnByName("colNutrientStatus").OptionsColumn.ReadOnly = ans;

			//owner
			grdOwnerView.Columns.ColumnByName("colAnalyzerOwnerCode").OptionsColumn.ReadOnly = ans;
			grdOwnerView.Columns.ColumnByName("colAnalyzerOwnerName").OptionsColumn.ReadOnly = ans;
			grdOwnerView.Columns.ColumnByName("colDataSourceUserIDOwner").OptionsColumn.ReadOnly = ans;
			grdOwnerView.Columns.ColumnByName("colOwnerStatus").OptionsColumn.ReadOnly = ans;

			//ownerlocation
			grdOwnerLocationView.Columns.ColumnByName("colAnalyzerOwnerID_OwnerLocation").OptionsColumn.ReadOnly = ans;
			grdOwnerLocationView.Columns.ColumnByName("colAnalyzerOwnerLocationCode").OptionsColumn.ReadOnly = ans;
			grdOwnerLocationView.Columns.ColumnByName("colAnalyzerOwnerLocationName").OptionsColumn.ReadOnly = ans;
			grdOwnerLocationView.Columns.ColumnByName("colDataSourceUserIDOwnerLocation").OptionsColumn.ReadOnly = ans;
			grdOwnerLocationView.Columns.ColumnByName("colOwnerLocationStatus").OptionsColumn.ReadOnly = ans;

			//supplier
			grdSupplierView.Columns.ColumnByName("colAnalyzerSupplierCode").OptionsColumn.ReadOnly = ans;
			grdSupplierView.Columns.ColumnByName("colAnalyzerSupplierName").OptionsColumn.ReadOnly = ans;
			grdSupplierView.Columns.ColumnByName("colDataSourceType_Supplier").OptionsColumn.ReadOnly = ans;
			grdSupplierView.Columns.ColumnByName("colSupplierStatus").OptionsColumn.ReadOnly = ans;

			//supplierlocation
			grdSupplierLocationView.Columns.ColumnByName("colAnalyzerSupplierCode_SL").OptionsColumn.ReadOnly = ans;
			grdSupplierLocationView.Columns.ColumnByName("colAnalyzerSupplierLocationCode").OptionsColumn.ReadOnly = ans;
			grdSupplierLocationView.Columns.ColumnByName("colAnalyzerSupplierLocationName").OptionsColumn.ReadOnly = ans;
			grdSupplierLocationView.Columns.ColumnByName("colDataSourceType_SupplierLocation").OptionsColumn.ReadOnly = ans;
			grdSupplierLocationView.Columns.ColumnByName("colSupplierLocationStatus").OptionsColumn.ReadOnly = ans;

			//12/21/20
			//supplier/supplier-loc combined			
			grdSupplierLocationCombinedView.Columns.ColumnByName("colAnalyzerSupplierLocationCombinedName").OptionsColumn.ReadOnly = ans;
			grdSupplierLocationCombinedView.Columns.ColumnByName("colDataSourceType_SupplierLocationCombined").OptionsColumn.ReadOnly = ans;
			grdSupplierLocationCombinedView.Columns.ColumnByName("colSupplierLocationCombinedStatus").OptionsColumn.ReadOnly = ans;


			if (ans == true)
			{
				grdMaterialView.Columns.ColumnByName("colImageMaterial").Visible = false;
				grdNutrientView.Columns.ColumnByName("colNutrientStatus").Visible = false;
				grdOwnerView.Columns.ColumnByName("colOwnerStatus").Visible = false;
				grdOwnerLocationView.Columns.ColumnByName("colOwnerLocationStatus").Visible = false;
				grdSupplierView.Columns.ColumnByName("colSupplierStatus").Visible = false;
				grdSupplierLocationView.Columns.ColumnByName("colSupplierLocationStatus").Visible = false;

				//12/21/20
				grdSupplierLocationCombinedView.Columns.ColumnByName("colSupplierLocationCombinedStatus").Visible = false;
			}
			else
			{
				grdMaterialView.Columns.ColumnByName("colImageMaterial").Visible = true;
				grdNutrientView.Columns.ColumnByName("colNutrientStatus").Visible = true;
				grdOwnerView.Columns.ColumnByName("colOwnerStatus").Visible = true;
				grdOwnerLocationView.Columns.ColumnByName("colOwnerLocationStatus").Visible = true;
				grdSupplierView.Columns.ColumnByName("colSupplierStatus").Visible = true;
				grdSupplierLocationView.Columns.ColumnByName("colSupplierLocationStatus").Visible = true;

				//12/21/20
				grdSupplierLocationCombinedView.Columns.ColumnByName("colSupplierLocationCombinedStatus").Visible = true;

			}

			if (ans == true)
			{
				grdQualityParametersView.Columns.ColumnByName("colSampleQualityParameterName").OptionsColumn.ReadOnly = ans;
				grdQualityParametersView.Columns.ColumnByName("colIsNumeric").OptionsColumn.ReadOnly = ans;
				grdQualityParametersView.Columns.ColumnByName("colDataSource_QualityControl").OptionsColumn.ReadOnly = ans;
				grdQualityParametersView.Columns.ColumnByName("colIsActiveQualityControlParameters").OptionsColumn.ReadOnly = ans;

			}
		}
		private void tabControl1_SelectedIndexChanged(Object sender, EventArgs e)
		{
			groupBox1.Visible = true;
			if (tabControl1.SelectedIndex == 6)
			{
				groupBox1.Visible = false;
			}

			//7/30/20
			AIMGlobal.analyzerMaterialTab = false;
			AIMGlobal.analyzerNutrientTab = false;
			AIMGlobal.analyzerOwnerTab = false;
			AIMGlobal.analyzerOwnerLocationTab = false;
			AIMGlobal.analyzerSupplierTab = false;
			AIMGlobal.analyzerSupplierLocationTab = false;
			AIMGlobal.analyzerEquationParameterTab = false;

			//12/21/20
			AIMGlobal.analyzerSupplierLocationCombinedTab = false;

			//---------------------------------------
			//5/30/19
			if (tabControl1.SelectedIndex == 0)
			{
				AIMGlobal.analyzerMaterialTab = true;                       //7/30/20
				grdMaterial.Focus();
			}
			else if (tabControl1.SelectedIndex == 1)
			{
				AIMGlobal.analyzerNutrientTab = true;                       //7/30/20
				grdNutrient.Focus();
			}
			else if (tabControl1.SelectedIndex == 2)
			{
				AIMGlobal.analyzerOwnerTab = true;                          //7/30/20
				grdOwner.Focus();
			}
			else if (tabControl1.SelectedIndex == 3)
			{
				AIMGlobal.analyzerOwnerLocationTab = true;                  //7/30/20
				grdOwnerLocation.Focus();
			}
			else if (tabControl1.SelectedIndex == 4)
			{
				AIMGlobal.analyzerSupplierTab = true;                       //7/30/20
				grdSupplier.Focus();
			}
			else if (tabControl1.SelectedIndex == 5)
			{
				AIMGlobal.analyzerSupplierLocationTab = true;               //7/30/20
				grdSupplierLocation.Focus();
			}
			else if (tabControl1.SelectedIndex == 6)
			{
				AIMGlobal.analyzerEquationParameterTab = true;              //7/30/20
				grdQualityParameters.Focus();
			}

			//12/21/20
			else if (tabControl1.SelectedIndex == 7)
			{
				AIMGlobal.analyzerSupplierLocationCombinedTab = true;
				grdSupplierLocationCombined.Focus();
			}
		}
		private void btnResize_Click(Object sender, EventArgs e)
		{
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}
		private void Analyzer_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}
		private void SetStatus()
		{
			if (rbActiveRecords.Checked)
				_status = (Int32)EnumSampleResultStatus.Active;
			if (rbDeletedRecords.Checked)
				_status = (Int32)EnumSampleResultStatus.Inactive;
			if (rbBoth.Checked)
				_status = (Int32)EnumSampleResultStatus.Both;
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
			ttip.SetToolTip(btnAddMaterial, "Add Record");
			ttip.SetToolTip(btnPreviewNutrient, "Preview Grid");
			ttip.SetToolTip(btnAddNutrient, "Add Record");
			ttip.SetToolTip(btnPreviewGridResultsOwner, "Preview Grid");
			ttip.SetToolTip(btnAddOwner, "Add Record");
			ttip.SetToolTip(btnPreviewGridResultsOwnerLocation, "Preview Grid");
			ttip.SetToolTip(btnAddOwnerLocation, "Add Record");
			ttip.SetToolTip(btnPreviewSupplier, "Preview Grid");
			ttip.SetToolTip(btnAddSupplier, "Add Record");
			ttip.SetToolTip(btnPreviewSupplierLocation, "Preview Grid");
			ttip.SetToolTip(btnAddSupplierLocation, "Add Record");

			//12/21/20
			ttip.SetToolTip(btnAddSupplierLocationCombined, "Add Record");
			ttip.SetToolTip(btnPreviewSupplierLocationCombined, "Preview Grid");

			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

			//1/2/18
			ttip.SetToolTip(btnPreviewParameters, "Preview Grid");
			ttip.SetToolTip(btnAddParameter, "Add Record");


		}
		private void ClearTextFields()
		{
			txtMaterialCode.Text = String.Empty;
			txtMaterialName.Text = String.Empty;
			txtNutrientCode.Text = String.Empty;
			txtNutrientName.Text = String.Empty;
			txtOwnerCode.Text = String.Empty;
			txtOwnerName.Text = String.Empty;
			txtOwnerLocationCode.Text = String.Empty;
			txtOwnerLocationName.Text = String.Empty;
			txtSupplierCode.Text = String.Empty;
			txtSupplierName.Text = String.Empty;
			txtSupplierLocationCode.Text = String.Empty;
			txtSupplierLocationName.Text = String.Empty;
			txtUnits.Text = String.Empty;

			//1/5/18
			txtParameterName.Text = String.Empty;
			lueParameterIsNumeric.EditValue = 1;

			//12/21/20
			txtSupplierLocationNameCombined.Text = String.Empty;


		}
		private void btnExit_Click(object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.analyzerRequiredField = "";
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void ShowHideFields()
		{
			if (rbActiveRecords.Checked)
			{
				txtMaterialCode.Enabled = true;
				txtMaterialName.Enabled = true;
				btnAddMaterial.Enabled = true;

				txtNutrientCode.Enabled = true;
				txtNutrientName.Enabled = true;
				txtUnits.Enabled = true;
				btnAddNutrient.Enabled = true;

				txtOwnerCode.Enabled = true;
				txtOwnerName.Enabled = true;
				btnAddOwner.Enabled = true;

				txtOwnerLocationCode.Enabled = true;
				txtOwnerLocationName.Enabled = true;
				btnAddOwnerLocation.Enabled = true;

				txtSupplierCode.Enabled = true;
				txtSupplierName.Enabled = true;
				btnAddSupplier.Enabled = true;

				txtSupplierLocationCode.Enabled = true;
				txtSupplierLocationName.Enabled = true;
				btnAddSupplierLocation.Enabled = true;

				//12/21/20
				txtSupplierLocationNameCombined.Enabled = true;
				btnAddSupplierLocationCombined.Enabled = true;
			}
			else
			{

				txtMaterialCode.Enabled = false;
				txtMaterialName.Enabled = false;
				btnAddMaterial.Enabled = false;

				txtNutrientCode.Enabled = false;
				txtNutrientName.Enabled = false;
				txtUnits.Enabled = false;
				btnAddNutrient.Enabled = false;

				txtOwnerCode.Enabled = false;
				txtOwnerName.Enabled = false;
				btnAddOwner.Enabled = false;

				txtOwnerLocationCode.Enabled = false;
				txtOwnerLocationName.Enabled = false;
				btnAddOwnerLocation.Enabled = false;

				txtSupplierCode.Enabled = false;
				txtSupplierName.Enabled = false;
				btnAddSupplier.Enabled = false;

				txtSupplierLocationCode.Enabled = false;
				txtSupplierLocationName.Enabled = false;
				btnAddSupplierLocation.Enabled = false;

				//12/21/20
				txtSupplierLocationNameCombined.Enabled = false;
				btnAddSupplierLocationCombined.Enabled = false;

			}
		}
        //1132 FG 7/1/2026 - Filter grid rows as user types in text boxes
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

        private void ApplyGridFilterForTab(Int32 tabIndex)
        {
            List<String> parts = new List<String>();
            GridView gridView = null;

            switch (tabIndex)
            {
                case 0: // Material
                    gridView = grdMaterialView;
                    AddContainsPart(parts, "AnalyzerMaterialCode", txtMaterialCode);
                    AddContainsPart(parts, "AnalyzerMaterialName", txtMaterialName);
                    break;

                case 1: // Nutrient
                    gridView = grdNutrientView;
                    AddContainsPart(parts, "AnalyzerNutrientCode", txtNutrientCode);
                    AddContainsPart(parts, "AnalyzerNutrientName", txtNutrientName);
                    AddContainsPart(parts, "UnitType", txtUnits);
                    break;

                case 2: // Owner
                    gridView = grdOwnerView;
                    AddContainsPart(parts, "AnalyzerOwnerCode", txtOwnerCode);
                    AddContainsPart(parts, "AnalyzerOwnerName", txtOwnerName);
                    break;

                case 3: // Owner Location
                    gridView = grdOwnerLocationView;
                    AddContainsPart(parts, "AnalyzerOwnerLocationCode", txtOwnerLocationCode);
                    AddContainsPart(parts, "AnalyzerOwnerLocationName", txtOwnerLocationName);
                    break;

                case 4: // Supplier
                    gridView = grdSupplierView;
                    AddContainsPart(parts, "AnalyzerSupplierCode", txtSupplierCode);
                    AddContainsPart(parts, "AnalyzerSupplierName", txtSupplierName);
                    break;

                case 5: // Supplier Location
                    gridView = grdSupplierLocationView;
                    AddContainsPart(parts, "AnalyzerSupplierLocationCode", txtSupplierLocationCode);
                    AddContainsPart(parts, "AnalyzerSupplierLocationName", txtSupplierLocationName);
                    break;

                case 6: // Quality Control
                    gridView = grdQualityParametersView;
                    AddContainsPart(parts, "SampleQualityParameterName", txtParameterName);
                    break;

                case 7: // Supplier Location Combined
                    gridView = grdSupplierLocationCombinedView;
                    AddContainsPart(parts, "AnalyzerSupplierLocationCombinedName", txtSupplierLocationNameCombined);
                    break;
            }

            if (gridView != null)
            {
                ApplyGridFilterFromParts(gridView, parts);
            }
        }       
        private void GridFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            ApplyGridFilterForTab(tabControl1.SelectedIndex);
        }
        #endregion

        #region Material ------------------------------------------------------------------------------
        private void DataBindGrid_Material()
		{	
			List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
			replueDataSourceTypeMat.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceTypeMat, "DataSourceTypeID", "Name", "TypeDisplay");	  
			List<AnalyzerMaterialInfo> analyzerMaterial = new List<AnalyzerMaterialInfo>();
			if (lueDataSource.EditValue != null)
			{																			   				
				if ((Int32)lueDataSource.EditValue != 0)
				{
                    //6/12/17 (Task 381)																				   
                    //analyzerMaterial = AIMManager.GetAnalyzerMaterial((Int32)lueDataSource.EditValue, rbActiveRecords.Checked);
                    if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerMaterial = AIMManager.GetAnalyzerMaterial((Int32)lueDataSource.EditValue, true);
					}

					//6/14/17
					//else 
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)

					{
						analyzerMaterial = AIMManager.GetAnalyzerMaterial((Int32)lueDataSource.EditValue, false);
					}

					else
					{
						analyzerMaterial = AIMManager.GetAnalyzerMaterial((Int32)lueDataSource.EditValue);
					}		   				
				}
				else
				{
                    //6/12/17 (Task 381)																				   
                    //analyzerMaterial = AIMManager.GetAnalyzerMaterial(rbActiveRecords.Checked);
                    if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerMaterial = AIMManager.GetAnalyzerMaterial(true);
					}

					//6/15/17 
					//else
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzerMaterial = AIMManager.GetAnalyzerMaterial(false);
					}
					else
					{
						analyzerMaterial = AIMManager.GetAnalyzerMaterial();
					}
				}

				Int32 currentRowNum = 0;
				if (grdMaterialView != null)
				{
					Int32.TryParse(grdMaterialView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdMaterial.DataSource = analyzerMaterial;
				grdMaterialView.FocusedRowHandle = currentRowNum;                
            }	
		}		
		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtMaterialCode.Text) == true)
				{
					MessageBox.Show("Please enter a material code. ", "Warning...", MessageBoxButtons.OK);
					txtMaterialCode.Focus();
					return;
				}

				if (txtMaterialName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a material name.", "Warning...", MessageBoxButtons.OK);
					txtMaterialName.Focus();
					return;
				}

				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
					lueDataSource.Focus();
					lueDataSource.ShowPopup();
					return;
				}

				AnalyzerMaterialInfo analyzerMaterial = new AnalyzerMaterialInfo();
				analyzerMaterial.AnalyzerMaterialID = 0;
				analyzerMaterial.AnalyzerMaterialCode = txtMaterialCode.Text;
				analyzerMaterial.AnalyzerMaterialName = txtMaterialName.Text;
				analyzerMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				analyzerMaterial.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				analyzerMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();							   			   
				error = AIMManager.SaveAnalyzerMaterial(ref analyzerMaterial, true);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtMaterialCode.Text = String.Empty;
				txtMaterialName.Text = String.Empty;
				DataBindGrid_Material();
				txtMaterialCode.Focus();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		
		private void btnPreviewGridResults_Click(object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(this.grdMaterial, "Analyzer Materials");
				CFCAIMCore.Global.previewGridResults(this.grdMaterial, "Analyzer Materials");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdMaterialView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Materials.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AnalyzerMaterialInfo analyzerMaterial = new AnalyzerMaterialInfo();
			analyzerMaterial = (AnalyzerMaterialInfo)grdMaterialView.GetFocusedRow();
			if (analyzerMaterial.AnalyzerMaterialCode.Length == 0)
			{
				MessageBox.Show("Material code cannot be empty. Information is not saved.", "Warning...");
				return;
			}
			analyzerMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerMaterial(ref analyzerMaterial, true);
			DataBindGrid_Material();

		}
		private void grdMaterialView_CustomUnboundColumnData(Object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colImageMaterial)
			{
				//get value in IsActive column for this row
				if (((AnalyzerMaterialInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
					e.Column.ToolTip = "Inactivate/Ignore on import";
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.ToolTip = "Re-activate record";
				}
			}
		}
		private void grdMaterialView_Click(Object sender, EventArgs e)
		{
			//    Point p = grdMaterialView.GridControl.PointToClient(MousePosition);
			//    DataGridView grid = sender as DataGridView;

			//    GridHitInfo info = grdMaterialView.CalcHitInfo(p);
			//    if (info.HitTest == GridHitTest.Column)
			//    {

			//    }

			GridView gv = sender as GridView;
		}
		private void grdMaterial_Click(Object sender, EventArgs e)
		{
			GridView gv = sender as GridView;
		}		
		private void grdMaterialView_CustomColumnSort(object sender, CustomColumnSortEventArgs e)    //added 4/12/22
		{
			//Columns set to Custom SortMode: colAnalyzerMaterialCode
			CFCAIM.AIMGlobal.sortGridColumn(sender, e);
		}
		#endregion

		#region Nutrient ------------------------------------------------------------------------------
		private void DataBindGrid_Nutrient()
		{
			List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
			replueDataSourceTypeNutrient.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceTypeNutrient, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceTypeNutrient.Columns["TypeDisplay"].Caption = "Type";

			List<AnalyzerNutrientInfo> analyzerNutrient = new List<AnalyzerNutrientInfo>();
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue != 0)
				{
					//6/15/17
					//analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)lueDataSource.EditValue, rbActiveRecords.Checked);
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)lueDataSource.EditValue, true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)lueDataSource.EditValue, false);
					}
					else
					{
						analyzerNutrient = AIMManager.GetAnalyzerNutrient((Int32)lueDataSource.EditValue);
					}
				}
				else
				{
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerNutrient = AIMManager.GetAllAnalyzerNutrient(true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzerNutrient = AIMManager.GetAllAnalyzerNutrient(false);
					}															  
					else
					{
						analyzerNutrient = AIMManager.GetAllAnalyzerNutrient();
					}
						
				}
				Int32 currentRowNum = 0;
				if (grdNutrientView != null)
				{
					Int32.TryParse(grdNutrientView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdNutrient.DataSource = analyzerNutrient;
				grdNutrientView.FocusedRowHandle = currentRowNum;
				
			}


		}
		private void btnAddNutrient_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtNutrientCode.Text) == true)
				{
					MessageBox.Show("Please enter a nutrient code. ", "Warning...", MessageBoxButtons.OK);
					txtNutrientCode.Focus();
					return;
				}
				if (txtNutrientName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a nutrient name.", "Warning...", MessageBoxButtons.OK);
					txtNutrientName.Focus();
					return;
				}				
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
					lueDataSource.Focus();
					lueDataSource.ShowPopup();
					return;
				}

				if (txtUnits.Text.Trim().Length == 0)
				{
					//4/25/22
					//MessageBox.Show("Please enter a unit of measure.", "Warning...", MessageBoxButtons.OK);
					//txtUnits.Focus();
					//return;
					DialogResult dr = new DialogResult();
					dr = MessageBox.Show("A Unit Of Measure has not been entered. Continue?", "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dr != DialogResult.Yes)
					{
						txtUnits.Focus();
						return;
					}

				}

				AnalyzerNutrientInfo AnalyzerNutrient = new AnalyzerNutrientInfo();
				AnalyzerNutrient.AnalyzerNutrientID = 0;
				AnalyzerNutrient.AnalyzerNutrientCode = txtNutrientCode.Text;
				AnalyzerNutrient.AnalyzerNutrientName = txtNutrientName.Text;
				AnalyzerNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AnalyzerNutrient.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				AnalyzerNutrient.UnitType = txtUnits.Text;
				AnalyzerNutrient.ConversionFactor = 1;
				AnalyzerNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAnalyzerNutrient(ref AnalyzerNutrient);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtNutrientCode.Text = String.Empty;
				txtNutrientName.Text = String.Empty;
				txtUnits.Text = String.Empty;
				DataBindGrid_Nutrient();
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
				CFCAIMCore.Global.previewGridResults(grdNutrient, "Analyzer Nutrients");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdNutrientView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Nutrients.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AnalyzerNutrientInfo AnalyzerNutrient = new AnalyzerNutrientInfo();
			AnalyzerNutrient = (AnalyzerNutrientInfo)grdNutrientView.GetFocusedRow();
			AnalyzerNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerNutrient(ref AnalyzerNutrient);
			DataBindGrid_Nutrient();

		}
		private void grdNutrientView_CustomUnboundColumnData(Object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colNutrientStatus)
			{
				//get value in IsActive column for this row
				if (((AnalyzerNutrientInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
					e.Column.ToolTip = "Inactivate/Ignore on import";
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.ToolTip = "Re-activate record";
				}
			}
		}
		#endregion

		#region Owner ---------------------------------------------------------------------------------
		private void DataBindGrid_Owner()
		{
			List<DataSourceTypeInfo> owner = new List<DataSourceTypeInfo>();
			owner = AIMManager.GetDataSourceType(true);
			replueDataSourceType_Owner.DataSource = owner;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType_Owner, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceType_Owner.Columns["TypeDisplay"].Caption = "Type";

			Int32 rowNum = 0;
			Int32.TryParse(grdOwnerView.FocusedRowHandle.ToString(), out rowNum);

            if (lueDataSource.EditValue != null)
            {
				if ((Int32)lueDataSource.EditValue != 0)
				{
					//grdOwner.DataSource = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue, rbActiveRecords.Checked);
					//grdOwnerView.FocusedRowHandle = rowNum;
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						grdOwner.DataSource = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue, true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						grdOwner.DataSource = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue, false);
					}
					else
					{
						grdOwner.DataSource = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue);
					}
				}
				else
				{
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						grdOwner.DataSource = AIMManager.GetAllAnalyzerOwner(true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						grdOwner.DataSource = AIMManager.GetAllAnalyzerOwner(false);
					}
					else
					{
						grdOwner.DataSource = AIMManager.GetAllAnalyzerOwner();
					}
					
				}
            }
		}		
		private void btnAddOwner_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(txtOwnerCode.Text) == true)
				{
					MessageBox.Show("Please enter Owner Code.", "Warning...");
					txtOwnerCode.Focus();
					return;
				}
				if (txtOwnerName.Text.Length == 0)
				{
					MessageBox.Show("Please enter Owner Name.", "Warning...");
					txtOwnerName.Focus();
					return;
				}
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select Data Source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}
								

				AnalyzerOwnerInfo owner = new AnalyzerOwnerInfo();
				owner.AnalyzerOwnerID = 0;
				owner.AnalyzerOwnerCode = txtOwnerCode.Text.ToString();
				owner.AnalyzerOwnerName = txtOwnerName.Text.ToString();
				owner.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				owner.IsActive = true;
				owner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAnalyzerOwner(ref owner, true);

				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindGrid_Owner();
				DataBindOwnerlue();	 				
				txtOwnerCode.Text = "";
				txtOwnerName.Text = "";

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}		
		private void btnPreviewGridResultsOwner_Click(object sender, EventArgs e)
		{
			PreviewGridResults(grdOwner, "Analyzer Owners");
		}
		private void PreviewGridResults(DevExpress.XtraPrinting.IPrintable grid, String title)
		{
			try
			{				
				CFCAIMCore.Global.previewGridResults(grid, title);
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdOwnerView_ValidateRow(Object sender, ValidateRowEventArgs e)
		{
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Owners.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AnalyzerOwnerInfo AnalyzerOwner = new AnalyzerOwnerInfo();
			AnalyzerOwner = (AnalyzerOwnerInfo)grdOwnerView.GetFocusedRow();
			AnalyzerOwner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerOwner(ref AnalyzerOwner, true);
			DataBindGrid_Owner();
		}
		private void grdOwnerView_CustomUnboundColumnData(Object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colOwnerStatus)
			{
				//get value in IsActive column for this row
				if (((AnalyzerOwnerInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
					e.Column.ToolTip = "Inactivate/Ignore on import";
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.ToolTip = "Re-activate record";
				}
			}
		}
		#endregion

		#region OwnerLocation -------------------------------------------------------------------------
		private void DataBindOwnerlue()
		{
			//OWNER --------------------------------------------------		   
			List<AnalyzerOwnerInfo> userOwner = new List<AnalyzerOwnerInfo>();
			if (lueDataSource.EditValue != null)
			{	
				//1/6/21
				//userOwner = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue);
				userOwner = AIMManager.GetAnalyzerOwner((Int32)lueDataSource.EditValue, rbActiveRecords.Checked);

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
				
				lueOwner.Properties.DataSource = userOwner;
				//task 1133 FG 7/21/2026 - Owner Code dropdown shows Code; Owner Name dropdown shows Name; both popups include Code and Name columns
				XtraEditors.LoadLookUpEdit(ref lueOwner, "AnalyzerOwnerID", "AnalyzerOwnerCode", "AnalyzerOwnerName");
				lueOwner.Properties.Columns["AnalyzerOwnerCode"].Caption = "Analyzer Owner Code";
				lueOwner.Properties.Columns["AnalyzerOwnerName"].Caption = "Analyzer Owner Name";

				lueOwnerName.Properties.DataSource = userOwner;
				XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName", "AnalyzerOwnerCode");
				lueOwnerName.Properties.Columns.Clear();
				lueOwnerName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AnalyzerOwnerName", "Analyzer Owner Name"));
				lueOwnerName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AnalyzerOwnerCode", "Analyzer Owner Code"));


                //OWNER LOCATION -----------------------------------------			   
                replueAnalyzerOwnerName.DataSource = userOwner;
				XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerName, "AnalyzerOwnerID", "AnalyzerOwnerName");
				replueAnalyzerOwnerCode.DataSource = userOwner;
				XtraEditors.LoadGridLookUpEdit(ref replueAnalyzerOwnerCode, "AnalyzerOwnerID", "AnalyzerOwnerCode");				
                replueAnalyzerOwnerName.Columns["AnalyzerOwnerName"].Caption = "Analyzer Owner Name";
                replueAnalyzerOwnerCode.Columns["AnalyzerOwnerCode"].Caption = "Analyzer Owner Code";

                List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
				source = AIMManager.GetDataSourceType(true);
				replueDataSourceType_OwnerLocation.DataSource = source;
				XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType_OwnerLocation, "DataSourceTypeID", "Name", "TypeDisplay");
				replueDataSourceType_OwnerLocation.Columns["TypeDisplay"].Caption = "Type";
			}
		}
		private void DataBindGrid_OwnerLocation()
		{
			Int32 rowNum = 0;
			Int32.TryParse(grdOwnerLocationView.FocusedRowHandle.ToString(), out rowNum);

			if (lueDataSource.EditValue != null)
			{
                if ((Int32)lueDataSource.EditValue != 0)
                {
					//grdOwnerLocation.DataSource = AIMManager.GetAnalyzerOwnerLocation((Int32)lueDataSource.EditValue, rbActiveRecords.Checked);
					//grdOwnerLocationView.FocusedRowHandle = rowNum;
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						grdOwnerLocation.DataSource = AIMManager.GetAnalyzerOwnerLocation((Int32)lueDataSource.EditValue, true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						grdOwnerLocation.DataSource = AIMManager.GetAnalyzerOwnerLocation((Int32)lueDataSource.EditValue, false);
					}
					else
					{
						grdOwnerLocation.DataSource = AIMManager.GetAnalyzerOwnerLocationByDataSource((Int32)lueDataSource.EditValue);
					}
                    
                }
                else
                {
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						grdOwnerLocation.DataSource = AIMManager.GetAllAnalyzerOwnerLocation(true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						grdOwnerLocation.DataSource = AIMManager.GetAllAnalyzerOwnerLocation(false);
					}	
					else
					{
						grdOwnerLocation.DataSource = AIMManager.GetAllAnalyzerOwnerLocation();
					}
						
                }
            }
		}
		private void btnAddOwnerLocation_Click(object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueOwner.EditValue == 0)
				{
					MessageBox.Show("Please select Owner Code.", "Warning...");
					lueOwner.ShowPopup();
					return;
				}
				if (string.IsNullOrWhiteSpace(txtOwnerLocationCode.Text) == true)
				{
					MessageBox.Show("Please enter Owner Location Code.", "Warning...");
					txtOwnerLocationCode.Focus();
					return;
				}
				if (txtOwnerLocationName.Text.Length == 0)
				{
					MessageBox.Show("Please enter Owner Location Name.", "Warning...");
					txtOwnerLocationName.Focus();
					return;
				}
				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select Data Source.", "Warning...");
					lueDataSource.ShowPopup();
					return;
				}
				
				AnalyzerOwnerLocationInfo ownerLoc = new AnalyzerOwnerLocationInfo();
				ownerLoc.AnalyzerOwnerLocationID = 0;
				ownerLoc.AnalyzerOwnerID = (Int32)lueOwner.EditValue;
				ownerLoc.AnalyzerOwnerLocationCode = txtOwnerLocationCode.Text.ToString();
				ownerLoc.AnalyzerOwnerLocationName = txtOwnerLocationName.Text.ToString();	  
				ownerLoc.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				ownerLoc.IsActive = true;
				ownerLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				ErrorInfo error = new ErrorInfo();				
				error = AIMManager.SaveAnalyzerOwnerLocation(ref ownerLoc, true);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				DataBindGrid_OwnerLocation();
				lueOwner.EditValue = 0;
				lueOwnerName.EditValue = 0;      //task 1133 FG 7/21/2026
				txtOwnerLocationCode.Text = "";
				txtOwnerLocationName.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}			
		private void btnPreviewGridResultsOwnerLocation_Click(object sender, EventArgs e)
		{
			PreviewGridResults(grdOwnerLocation, "Analyzer Owner Locations");
		}
		private void grdOwnerLocationView_ValidateRow(Object sender, ValidateRowEventArgs e)
		{
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Owner Locations.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AnalyzerOwnerLocationInfo AnalyzerOwnerLocation = new AnalyzerOwnerLocationInfo();
			AnalyzerOwnerLocation = (AnalyzerOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
			AnalyzerOwnerLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerOwnerLocation(ref AnalyzerOwnerLocation, true);
			DataBindGrid_OwnerLocation();
		}
		private void lueOwner_Enter(Object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}
		private void lueOwner_Click(Object sender, EventArgs e)
		{
			lueOwner.SelectAll();
		}
		//task 1133 FG 7/21/2026 - keep the Owner Code and Owner Name dropdowns in sync
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
		private void lueOwner_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueOwner, ref lueOwnerName);
		}
		private void lueOwnerName_EditValueChanged(object sender, EventArgs e)
		{
			SyncValueLue(ref lueOwnerName, ref lueOwner);
		}
		private void lueOwnerName_Click(object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}
		private void lueOwnerName_Enter(object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}
		private void grdOwnerLocationView_CustomUnboundColumnData(Object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colOwnerLocationStatus)
			{
				//get value in IsActive column for this row
				if (((AnalyzerOwnerLocationInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
					e.Column.ToolTip = "Inactivate/Ignore on import";
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.ToolTip = "Re-activate record";
				}
			}
		}
		#endregion

		#region Supplier ------------------------------------------------------------------------------
		private void DataBindGrid_Supplier()
		{
			List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
			replueDataSourceType_Supplier.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType_Supplier, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceType_Supplier.Columns["TypeDisplay"].Caption = "Type";

			List<AnalyzerSupplierInfo> analyzerSupplier = new List<AnalyzerSupplierInfo>();
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue != 0)
				{					
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerSupplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue, true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzerSupplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue, false);
					}
					else
					{
						analyzerSupplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue);
					}

				}
				else
				{
					//analyzerSupplier = AIMManager.GetAllAnalyzerSupplier(rbActiveRecords.Checked);
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerSupplier = AIMManager.GetAllAnalyzerSupplier(true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzerSupplier = AIMManager.GetAllAnalyzerSupplier(false);
					}
					else
					{
						analyzerSupplier = AIMManager.GetAllAnalyzerSupplier();
					}
				}
				Int32 currentRowNum = 0;
				if (grdSupplierView != null)
				{
					Int32.TryParse(grdSupplierView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdSupplier.DataSource = analyzerSupplier;
				grdSupplierView.FocusedRowHandle = currentRowNum;

				
			}
		}
		private void btnAddSupplier_Click(object sender, EventArgs e)
		{
			try
			{

				if (txtSupplierCode.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a supplier code.", "Warning...", MessageBoxButtons.OK);
					txtSupplierCode.Focus();
					return;
				}
				if (txtSupplierName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a supplier name.", "Warning...", MessageBoxButtons.OK);
					txtSupplierName.Focus();
					return;
				}

				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
					lueDataSource.Focus();
					lueDataSource.ShowPopup();
					return;
				}

				AnalyzerSupplierInfo analyzerExists = new AnalyzerSupplierInfo();
				analyzerExists = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue, txtSupplierCode.Text);
				if (analyzerExists != null)
				{
					MessageBox.Show("This Supplier Code already exists. Please enter a different code.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}						

				AnalyzerSupplierInfo analyzerSupplier = new AnalyzerSupplierInfo();
				analyzerSupplier.AnalyzerSupplierID = 0;
				analyzerSupplier.AnalyzerSupplierCode = txtSupplierCode.Text;
				analyzerSupplier.AnalyzerSupplierName = txtSupplierName.Text;
				analyzerSupplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				analyzerSupplier.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				analyzerSupplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAnalyzerSupplier(ref analyzerSupplier);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtSupplierCode.Text = String.Empty;
				txtSupplierName.Text = String.Empty;
				DataBindGrid_Supplier();
				txtSupplierCode.Focus();


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
				CFCAIMCore.Global.previewGridResults(grdSupplier, "Analyzer Suppliers");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		
		private void grdSupplierView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Suppliers.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AnalyzerSupplierInfo AnalyzerSupplier = new AnalyzerSupplierInfo();
			AnalyzerSupplier = (AnalyzerSupplierInfo)grdSupplierView.GetFocusedRow();
			AnalyzerSupplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerSupplier(ref AnalyzerSupplier);
			DataBindGrid_Supplier();

		}
		private void grdSupplierView_CustomUnboundColumnData(Object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colSupplierStatus)
			{
				//get value in IsActive column for this row
				if (((AnalyzerSupplierInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
					e.Column.ToolTip = "Inactivate/Ignore on import";
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.ToolTip = "Re-activate record";
				}
			}
		}
		#endregion

		#region SupplierLocation ----------------------------------------------------------------------
		private void DataBindSupplierlue()
		{
			//SUPPLIER --------------------------------------------------		   
			List<AnalyzerSupplierInfo> supplier = new List<AnalyzerSupplierInfo>();
			if (lueDataSource.EditValue != null)
			{
				supplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue, true);
				supplier.Insert
				(
					0,
					new AnalyzerSupplierInfo
					{
						AnalyzerSupplierID = 0,
						AnalyzerSupplierCode = "--Select--",
						AnalyzerSupplierName = "--Select--"
					}
				);

				lueSupplier.Properties.DataSource = supplier;
				//task 1133 FG 7/21/2026 - Supplier Code dropdown shows Code; Supplier Name dropdown shows Name; both popups include Code and Name columns
				XtraEditors.LoadLookUpEdit(ref lueSupplier, "AnalyzerSupplierID", "AnalyzerSupplierCode", "AnalyzerSupplierName");
				lueSupplier.Properties.Columns["AnalyzerSupplierCode"].Caption = "Analyzer Supplier Code";
				lueSupplier.Properties.Columns["AnalyzerSupplierName"].Caption = "Analyzer Supplier Name";

				lueSupplierName.Properties.DataSource = supplier;
				XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AnalyzerSupplierID", "AnalyzerSupplierName", "AnalyzerSupplierCode");
				lueSupplierName.Properties.Columns.Clear();
				lueSupplierName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AnalyzerSupplierName", "Analyzer Supplier Name"));
				lueSupplierName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AnalyzerSupplierCode", "Analyzer Supplier Code"));


                //SUPPLIER LOCATION -----------------------------------------			   
                replueSupplier.DataSource = supplier;
				XtraEditors.LoadGridLookUpEdit(ref replueSupplier, "AnalyzerSupplierID", "AnalyzerSupplierCode");
			}
		}
		private void DataBindGrid_SupplierLocation()
		{

			List<AnalyzerSupplierInfo> supplier = new List<AnalyzerSupplierInfo>();			

			//1/6/21 -- WHY??
            //3/3/17 - on supplier location grid, need to show supplier code, whether or not supplier is active or inactive.
			//supplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue, true);           			
			////supplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue);
			supplier = AIMManager.GetAnalyzerSupplier((Int32)lueDataSource.EditValue, rbActiveRecords.Checked);

			supplier.Insert
			(
				0,
				new AnalyzerSupplierInfo
				{
					AnalyzerSupplierID = 0,
					AnalyzerSupplierCode = "--Select--",
					AnalyzerSupplierName = "--Select--"
				}
			);

			//task 1133 FG 7/21/2026 - rebind both Supplier Code and Supplier Name dropdowns when the grid is refreshed
			lueSupplier.Properties.DataSource = supplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplier, "AnalyzerSupplierID", "AnalyzerSupplierCode", "AnalyzerSupplierName");
			lueSupplier.Properties.Columns["AnalyzerSupplierCode"].Caption = "Analyzer Supplier Code";
			lueSupplier.Properties.Columns["AnalyzerSupplierName"].Caption = "Analyzer Supplier Name";

			lueSupplierName.Properties.DataSource = supplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AnalyzerSupplierID", "AnalyzerSupplierName", "AnalyzerSupplierCode");
			lueSupplierName.Properties.Columns.Clear();
			lueSupplierName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AnalyzerSupplierName", "Analyzer Supplier Name"));
			lueSupplierName.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AnalyzerSupplierCode", "Analyzer Supplier Code"));

            //populating replueSupplier
            replueSupplier.DataSource = supplier;
			XtraEditors.LoadGridLookUpEdit(ref replueSupplier, "AnalyzerSupplierID", "AnalyzerSupplierCode");
			replueSupplier.Columns["AnalyzerSupplierCode"].Visible = true;
			replueSupplier.Columns["AnalyzerSupplierName"].Visible = true;			
            replueSupplier.Columns["AnalyzerSupplierCode"].Caption = "Analyzer Supplier Code";
            replueSupplier.Columns["AnalyzerSupplierName"].Caption = "Analyzer Supplier Name";


            List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
			replueDataSourceType_SupplierLocation.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType_SupplierLocation, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceType_SupplierLocation.Columns["TypeDisplay"].Caption = "Type";

			List<AnalyzerSupplierLocationInfo> analyzerSupplierLocation = new List<AnalyzerSupplierLocationInfo>();
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue != 0)
				{
					//analyzerSupplierLocation = AIMManager.GetAnalyzerSupplierLocation((Int32)lueDataSource.EditValue, rbActiveRecords.Checked);
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerSupplierLocation = AIMManager.GetAnalyzerSupplierLocation((Int32)lueDataSource.EditValue, true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzerSupplierLocation = AIMManager.GetAnalyzerSupplierLocation((Int32)lueDataSource.EditValue, false);
					}
					else
					{
						analyzerSupplierLocation = AIMManager.GetAnalyzerSupplierLocation((Int32)lueDataSource.EditValue);
					}
					
				}
				else
				{
					//analyzerSupplierLocation = AIMManager.GetAllAnalyzerSupplierLocation(rbActiveRecords.Checked);
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzerSupplierLocation = AIMManager.GetAllAnalyzerSupplierLocation(true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzerSupplierLocation = AIMManager.GetAllAnalyzerSupplierLocation(false);
					}
					else
					{
						analyzerSupplierLocation = AIMManager.GetAllAnalyzerSupplierLocation();
					}
                    
                }
				Int32 currentRowNum = 0;
				if (grdSupplierLocationView != null)
				{
					Int32.TryParse(grdSupplierLocationView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdSupplierLocation.DataSource = analyzerSupplierLocation;
				grdSupplierLocationView.FocusedRowHandle = currentRowNum;
				
			}
		}
		private void btnAddSupplierLocation_Click(object sender, EventArgs e)
		{
			try
			{

				if ((Int32)lueSupplier.EditValue == 0)
				{
					MessageBox.Show("Please select a Supplier Code.", "Warning...", MessageBoxButtons.OK);
					lueSupplier.Focus();
					lueSupplier.ShowPopup();
					return;
				}

				if (txtSupplierLocationCode.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a supplier location code.", "Warning...", MessageBoxButtons.OK);
					txtSupplierLocationCode.Focus();
					return;
				}

				if (txtSupplierLocationName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a supplier location name.", "Warning...", MessageBoxButtons.OK);
					txtSupplierLocationName.Focus();
					return;
				}

				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
					lueDataSource.Focus();
					lueDataSource.ShowPopup();
					return;
				}


				AnalyzerSupplierLocationInfo analyzerCodeExists = new AnalyzerSupplierLocationInfo();
				analyzerCodeExists = AIMManager.GetAnalyzerSupplierLocation((Int32)lueDataSource.EditValue, (Int32)lueSupplier.EditValue, txtSupplierLocationCode.Text);
				if (analyzerCodeExists != null)
				{
					MessageBox.Show("This Supplier Location Code already exists for this supplier. Please enter a different code.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				AnalyzerSupplierLocationInfo analyzerNameExists = new AnalyzerSupplierLocationInfo();
				analyzerNameExists = AIMManager.GetAnalyzerSupplierLocationName((Int32)lueDataSource.EditValue, (Int32)lueSupplier.EditValue, txtSupplierLocationName.Text);
				if (analyzerNameExists != null)
				{
					MessageBox.Show("This Supplier Location Name already exists for this supplier. Please enter a different name.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}


				AnalyzerSupplierLocationInfo analyzerSupplierLocation = new AnalyzerSupplierLocationInfo();
				analyzerSupplierLocation.AnalyzerSupplierLocationID = 0;
				analyzerSupplierLocation.AnalyzerSupplierID = (Int32)lueSupplier.EditValue;
				analyzerSupplierLocation.AnalyzerSupplierLocationCode = txtSupplierLocationCode.Text;
				analyzerSupplierLocation.AnalyzerSupplierLocationName = txtSupplierLocationName.Text;
				analyzerSupplierLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				analyzerSupplierLocation.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				analyzerSupplierLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAnalyzerSupplierLocation(ref analyzerSupplierLocation);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtSupplierLocationCode.Text = String.Empty;
				txtSupplierLocationName.Text = String.Empty;
				DataBindGrid_SupplierLocation();
				lueSupplier.Focus();


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
				CFCAIMCore.Global.previewGridResults(grdSupplierLocation, "Analyzer Supplier Locations");
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierLocationView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Supplier Locations.  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AnalyzerSupplierLocationInfo AnalyzerSupplierLocation = new AnalyzerSupplierLocationInfo();
			AnalyzerSupplierLocation = (AnalyzerSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
			AnalyzerSupplierLocation.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerSupplierLocation(ref AnalyzerSupplierLocation);
			DataBindGrid_SupplierLocation();
		}
		private void lueSupplier_Enter(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}
		private void lueSupplier_Click(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
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
		private void grdSupplierLocationView_CustomUnboundColumnData(Object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colSupplierLocationStatus)
			{
				//get value in IsActive column for this row
				if (((AnalyzerSupplierLocationInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
					e.Column.ToolTip = "Inactivate/Ignore on import";
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.ToolTip = "Re-activate record";
				}
			}
		}
		#endregion

		//12/21/20
		#region SupplierLocationCombined --------------------------------------------------------------
		private void DataBindGrid_SupplierLocationCombined()
		{
			List<DataSourceTypeInfo> source = new List<DataSourceTypeInfo>();
			source = AIMManager.GetDataSourceType(true);
						
			replueDataSourceType_SupplierLocationCombined.DataSource = source;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType_SupplierLocationCombined, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceType_SupplierLocationCombined.Columns["TypeDisplay"].Caption = "Type";


			List<AnalyzerSupplierLocationCombinedInfo> analyzer = new List<AnalyzerSupplierLocationCombinedInfo>();
			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue != 0)
				{
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzer = AIMManager.GetAnalyzerSupplierLocationCombined((Int32)lueDataSource.EditValue, true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzer = AIMManager.GetAnalyzerSupplierLocationCombined((Int32)lueDataSource.EditValue, false);
					}
					else
					{
						analyzer = AIMManager.GetAnalyzerSupplierLocationCombined((Int32)lueDataSource.EditValue);
					}

				}
				else
				{					
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						analyzer = AIMManager.GetAllAnalyzerSupplierLocationCombined(true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						analyzer = AIMManager.GetAllAnalyzerSupplierLocationCombined(false);
					}
					else
					{
						analyzer = AIMManager.GetAllAnalyzerSupplierLocationCombined();
					}
				}
				Int32 currentRowNum = 0;
				if (grdSupplierLocationCombinedView != null)
				{
					Int32.TryParse(grdSupplierLocationCombinedView.FocusedRowHandle.ToString(), out currentRowNum);
				}
				grdSupplierLocationCombined.DataSource = analyzer;
				grdSupplierLocationCombinedView.FocusedRowHandle = currentRowNum;


			}
		}
		private void btnAddSupplierLocationCombined_Click(object sender, EventArgs e)
		{
			try
			{

				if (txtSupplierLocationNameCombined.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a supplier/supplier-location combined name.", "Warning...", MessageBoxButtons.OK);
					txtSupplierCode.Focus();
					return;
				}				

				if ((Int32)lueDataSource.EditValue == 0)
				{
					MessageBox.Show("Please select a data source.", "Warning...", MessageBoxButtons.OK);
					lueDataSource.Focus();
					lueDataSource.ShowPopup();
					return;
				}


				txtSupplierLocationNameCombined.Text = txtSupplierLocationNameCombined.Text.ToUpper();

				AnalyzerSupplierLocationCombinedInfo analyzerExists = new AnalyzerSupplierLocationCombinedInfo();
				analyzerExists = AIMManager.GetAnalyzerSupplierLocationCombined((Int32)lueDataSource.EditValue, txtSupplierLocationNameCombined.Text);
				if (analyzerExists != null)
				{
					MessageBox.Show("This Supplier-SupplierLocation combined name already exists. Please enter a different name.", "Warning...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				AnalyzerSupplierLocationCombinedInfo analyzer = new AnalyzerSupplierLocationCombinedInfo();
				analyzer.AnalyzerSupplierLocationCombinedID = 0;
				analyzer.AnalyzerSupplierLocationCombinedName = txtSupplierLocationNameCombined.Text;
				analyzer.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				analyzer.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				analyzer.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				ErrorInfo error = new ErrorInfo();
				error = AIMManager.SaveAnalyzerSupplierLocationCombined(ref analyzer);
				if (error.ErrorNumber > 0)
				{
					return;
				}
				txtSupplierLocationNameCombined.Text = String.Empty;			
								
				DataBindGrid_SupplierLocationCombined();
				txtSupplierLocationNameCombined.Focus();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewSupplierLocationCombined_Click(object sender, EventArgs e)
		{
			try
			{				
				CFCAIMCore.Global.previewGridResults(grdSupplierLocationCombined, "Analyzer Supplier/Supplier Locations Combined");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdSupplierLocationCombinedView_RowCellStyle(object sender, RowCellStyleEventArgs e) { AIMGlobal.focusedRowGreen(sender, e); }
		private void grdSupplierLocationCombinedView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
		{
			if (e.Column == colSupplierLocationCombinedStatus)
			{
				//get value in IsActive column for this row
				if (((AnalyzerSupplierLocationCombinedInfo)(e.Row)).IsActive)
				{
					e.Value = Properties.Resources.delete;
					e.Column.ToolTip = "Inactivate/Ignore on import";
				}
				else
				{
					e.Value = Properties.Resources.Undo_icon;
					e.Column.ToolTip = "Re-activate record";
				}
			}
		}
		private void grdSupplierLocationCombinedView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			if (rbDeletedRecords.Checked == true)
			{
				MessageBox.Show("You cannot make changes to inactive Supplier Locations (combined).  Changes will be undone.", "Warning...", MessageBoxButtons.OK);
				return;
			}
			AnalyzerSupplierLocationCombinedInfo analyzer = new AnalyzerSupplierLocationCombinedInfo();
			analyzer = (AnalyzerSupplierLocationCombinedInfo)grdSupplierLocationCombinedView.GetFocusedRow();
			analyzer.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerSupplierLocationCombined(ref analyzer);
			DataBindGrid_SupplierLocationCombined();
		}
		#endregion

		//1/5/18
		#region Quality Control -----------------------------------------------------------------------
		private void DataBindGrid_QualityControl()
		{
			List<DataSourceTypeInfo> qcDataSource = new List<DataSourceTypeInfo>();
			qcDataSource = AIMManager.GetDataSourceType(true);
			replueDataSourceType_QC.DataSource = qcDataSource;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSourceType_QC, "DataSourceTypeID", "Name", "TypeDisplay");
			replueDataSourceType_QC.Columns["TypeDisplay"].Caption = "Type";

			Int32 rowNum = 0;
			Int32.TryParse(grdQualityParametersView.FocusedRowHandle.ToString(), out rowNum);

			if (lueDataSource.EditValue != null)
			{
				if ((Int32)lueDataSource.EditValue != 0)
				{

					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						grdQualityParameters.DataSource = AIMManager.GetSampleQualityParameter((Int32)lueDataSource.EditValue, true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						grdQualityParameters.DataSource = AIMManager.GetSampleQualityParameter((Int32)lueDataSource.EditValue, false);
					}
					else
					{
						grdQualityParameters.DataSource = AIMManager.GetSampleQualityParameter((Int32)lueDataSource.EditValue);
					}
				}
				else
				{
					if (_status == (Int32)EnumActiveIgnoreStatus.Active)
					{
						grdQualityParameters.DataSource = AIMManager.GetSampleQualityParameter(true);
					}
					else if (_status == (Int32)EnumActiveIgnoreStatus.Ignore)
					{
						grdQualityParameters.DataSource = AIMManager.GetSampleQualityParameter(false);
					}
					else
					{
						grdQualityParameters.DataSource = AIMManager.GetAllQualityParameters();
					}

				}
			}

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
			lueUnits_QC.Properties.DataSource = unit;
			XtraEditors.LoadLookUpEdit(ref lueUnits_QC, "UnitTypeID", "UnitTypeDescription");
			if (unit.Count == 2)
			{
				//selecting value if there is only one
				lueUnits_QC.EditValue = unit[1].UnitTypeID;
			}
			else
			{
				lueUnits_QC.EditValue = 0;
			}

			replueUnitType_QC.DataSource = unit;
			XtraEditors.LoadGridLookUpEdit(ref replueUnitType_QC, "UnitTypeID", "UnitTypeDescription");


		}
		private void btnAddParameter_Click(Object sender, EventArgs e)
		{
			try
			{


				if (txtParameterName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Parameter Name.", "Warning...", MessageBoxButtons.OK);
					txtParameterName.Focus();
					return;
				}

				SampleQualityParameterInfo parameter = new SampleQualityParameterInfo();
				parameter.SampleQualityParameterID = 0;
				parameter.SampleQualityParameterName = txtParameterName.Text;
				parameter.DataSourceTypeID = (Int32)lueDataSource.EditValue;
				parameter.ParameterIsNumeric = true;
				Boolean yesNo = true;
				if ((Int32)lueParameterIsNumeric.EditValue == 0)
				{
					yesNo = false;
				}
				parameter.ParameterIsNumeric = yesNo;
				parameter.IsActive = true;

				//4/25/22
				parameter.UnitTypeID = (Int32)lueUnits_QC.EditValue;


				AIMManager.SaveSampleQualityParameter(ref parameter);

				txtParameterName.Text = String.Empty;
				lueParameterIsNumeric.EditValue = 1;
				DataBindGrid_QualityControl();
				txtParameterName.Focus();
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
		}
		private void btnPreviewParameters_Click(Object sender, EventArgs e)
		{
			try
			{
				//9/9/20 - change to use AIMCoreGlobal
				//AIMGlobal.previewGridResults(grdQualityParameters, "Sample Control Parameters");
				CFCAIMCore.Global.previewGridResults(grdQualityParameters, "Sample Control Parameters");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdQualityParametersView_ValidateRow(Object sender, ValidateRowEventArgs e)
		{
			try
			{
				SampleQualityParameterInfo parameter = new SampleQualityParameterInfo();
				parameter = (SampleQualityParameterInfo)grdQualityParametersView.GetFocusedRow();
				parameter.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveSampleQualityParameter(ref parameter);
				DataBindGrid_QualityControl();


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repDeleteQualityControlParameter_Click(Object sender, EventArgs e)
		{
			try
			{
				SampleQualityParameterInfo parameter = new SampleQualityParameterInfo();
				parameter = (SampleQualityParameterInfo)grdQualityParametersView.GetFocusedRow();
				parameter.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteSampleQualityParameter(parameter.SampleQualityParameterID);
				DataBindGrid_QualityControl();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Activate/Inactivate -------------------------------------------------------------------
		//Analyzer cross-references are never deleted here because 
		//the Analyzer Material, Nutrient, Owner, etc., is never deleted, just inactivated.
		private void repItemPictureEditMaterial_Click(Object sender, EventArgs e)
		{
			try
			{
				AnalyzerMaterialInfo AnalyzerMaterial = new AnalyzerMaterialInfo();
				AnalyzerMaterial = (AnalyzerMaterialInfo)grdMaterialView.GetFocusedRow();
				AnalyzerMaterial.IsActive = !AnalyzerMaterial.IsActive;
				AnalyzerMaterial.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveAnalyzerMaterial(ref AnalyzerMaterial, true);
				DataBindGrid_Material();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repItemPictureEditNutrient_Click(Object sender, EventArgs e)
		{
			//6/13/22 - added warning. Theresa at Smithfield ran into this - someone there inactivated a nutrient, 
			//			so the nutrient sample data no longer showed up in sample results or on reports.				
			DialogResult dr = new DialogResult();
			String msg;

			if (rbActiveRecords.Checked == true)
			{
				msg = "By inactivating this nutrient, any sample data linked to this nutrient will no longer show on the Sample screen or on reports. Continue?";
				dr = MessageBox.Show(msg, "Warning...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
				if (dr != DialogResult.Yes)
				{
					return;
				}
			}

			AnalyzerNutrientInfo AnalyzerNutrient = new AnalyzerNutrientInfo();
			AnalyzerNutrient = (AnalyzerNutrientInfo)grdNutrientView.GetFocusedRow();
			AnalyzerNutrient.IsActive = !AnalyzerNutrient.IsActive;
			AnalyzerNutrient.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerNutrient(ref AnalyzerNutrient);
			DataBindGrid_Nutrient();			

		}
		private void repositoryItemPictureEditOwner_Click(Object sender, EventArgs e)
		{			
			AnalyzerOwnerInfo AnalyzerOwner = new AnalyzerOwnerInfo();
			AnalyzerOwner = (AnalyzerOwnerInfo)grdOwnerView.GetFocusedRow();			
			AnalyzerOwner.IsActive = !AnalyzerOwner.IsActive;						
			AnalyzerOwner.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerOwner(ref AnalyzerOwner, true);
			DataBindGrid_Owner();
		}		
		private void repositoryItemPictureEditOwnerLocation_Click(Object sender, EventArgs e)
		{
			AnalyzerOwnerLocationInfo AnalyzerOwnerLoc = new AnalyzerOwnerLocationInfo();
			AnalyzerOwnerLoc = (AnalyzerOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
			AnalyzerOwnerLoc.IsActive = !AnalyzerOwnerLoc.IsActive;
			AnalyzerOwnerLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerOwnerLocation(ref AnalyzerOwnerLoc, true);
			DataBindGrid_OwnerLocation();
		}
		private void repositoryItemPictureEditSupplier_Click(Object sender, EventArgs e)
		{			
			AnalyzerSupplierInfo AnalyzerSupplier = new AnalyzerSupplierInfo();
			AnalyzerSupplier = (AnalyzerSupplierInfo)grdSupplierView.GetFocusedRow();
			AnalyzerSupplier.IsActive = !AnalyzerSupplier.IsActive;
			AnalyzerSupplier.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerSupplier(ref AnalyzerSupplier, true);
			DataBindGrid_Supplier();

		}
		private void repItemPictureEditSupplierLocation_Click(Object sender, EventArgs e)
		{			
			AnalyzerSupplierLocationInfo AnalyzerSupplierLoc = new AnalyzerSupplierLocationInfo();
			AnalyzerSupplierLoc = (AnalyzerSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
			AnalyzerSupplierLoc.IsActive = !AnalyzerSupplierLoc.IsActive;
			AnalyzerSupplierLoc.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerSupplierLocation(ref AnalyzerSupplierLoc, true);
			DataBindGrid_SupplierLocation();
		}
		private void repositoryItemPictureEditSupplierLocationCombined_Click(object sender, EventArgs e)
		{	
			AnalyzerSupplierLocationCombinedInfo combined = new AnalyzerSupplierLocationCombinedInfo();
			combined = (AnalyzerSupplierLocationCombinedInfo)grdSupplierLocationCombinedView.GetFocusedRow();
			combined.IsActive = !combined.IsActive;
			combined.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveAnalyzerSupplierLocationCombined(ref combined, true);
			DataBindGrid_SupplierLocationCombined();
		}
		#endregion

		private void grdMaterialView_RowCellStyle(object sender, RowCellStyleEventArgs e)          {AIMGlobal.focusedRowGreen(sender, e);}
        private void grdNutrientView_RowCellStyle(object sender, RowCellStyleEventArgs e)          {AIMGlobal.focusedRowGreen(sender, e);}
        private void grdOwnerView_RowCellStyle(object sender, RowCellStyleEventArgs e)             {AIMGlobal.focusedRowGreen(sender, e);}
        private void grdOwnerLocationView_RowCellStyle(object sender, RowCellStyleEventArgs e)     {AIMGlobal.focusedRowGreen(sender, e);}
        private void grdSupplierView_RowCellStyle(object sender, RowCellStyleEventArgs e)          {AIMGlobal.focusedRowGreen(sender, e);}
        private void grdSupplierLocationView_RowCellStyle(object sender, RowCellStyleEventArgs e)  {AIMGlobal.focusedRowGreen(sender, e);}
        private void grdQualityParametersView_RowCellStyle(object sender, RowCellStyleEventArgs e) {AIMGlobal.focusedRowGreen(sender, e);}

		private void lueDataSource_Click(object sender, EventArgs e)
		{
			lueDataSource.SelectAll();
		}

		private void lueDataSource_Enter(object sender, EventArgs e)
		{
			lueDataSource.SelectAll();
		}

		private void lueSupplier_Click_1(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}

		private void lueSupplier_Enter_1(object sender, EventArgs e)
		{
			lueSupplier.SelectAll();
		}

		private void lueParameterIsNumeric_Click(object sender, EventArgs e)
		{
			lueParameterIsNumeric.SelectAll();
		}

		private void lueParameterIsNumeric_Enter(object sender, EventArgs e)
		{
			lueParameterIsNumeric.SelectAll();
		}

		private void lueUnits_QC_Click(object sender, EventArgs e)
		{
			lueUnits_QC.SelectAll();
		}

		private void lueUnits_QC_Enter(object sender, EventArgs e)
		{
			lueUnits_QC.SelectAll();
		}

    }
}
