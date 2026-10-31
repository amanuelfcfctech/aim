using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using CFCAIM.SampleQualityControl;
using System.Drawing;

//5/26/20
using CFCAIM.Maintenance;

namespace CFCAIM
{
	public partial class SystemSettings : Form
	{
		CFCUser.Global userGlobal = new CFCUser.Global();
		Int32 _SystemSettingsID = 0;
		MathType mathType;
		Boolean _formLoad = true;
		Boolean _changesMade = false;
		SampleQuality sampleQuality;							//1/2/18		
		AIM aimMaintenance;                                     //5/26/20
		CompanyName companyName;								//1/8/24

		public SystemSettings()
		{
			InitializeComponent();
		}

		private void SystemSettings_Load(object sender, EventArgs e)
		{
			DataBindlue();
			DataBind();
			SetToolTips();
			AIMGlobal.aimSupplierGuaranteesTab = false;         //5/26/20			
			_formLoad = false;                                  //9/23/22
			WindowState = FormWindowState.Maximized;			//--> MUST BE LAST COMMAND IN THIS SECTION
		}

		private void DataBind()
		{
			SystemSettingsInfo systemSettings = new SystemSettingsInfo();
			systemSettings = AIMManager.GetSystemSettings();
			if (systemSettings != null)
			{
				_SystemSettingsID = systemSettings.SystemSettingsID;
				lueAimNutrient.EditValue = systemSettings.AIMNutrientID;
				lueAimNutrientDryMatter.EditValue = systemSettings.DryMatterAIMNutrientID;      //1/3/23
				if (systemSettings.AllowAIMInactive == false)
				{
					rbNoAllowAIMInactive.Checked = true;
				}
				else
				{
					rbYesAllowAIMInactive.Checked = true;
				}

				if (systemSettings.ShowAnalyzerMaterials == true)
				{
					rbYesShowAnalyzerMaterials.Checked = true;
				}
				else
				{
					//3/27/23
					//rbYesShowAnalyzerMaterials.Checked = false;
					rbNoShowAnalyzerMaterials.Checked = true;
				}

				//12/19/17								  
				if (systemSettings.SampleNumberOfDays == 0)
				{
					txtDays.Text = "60";
				}
				else
				{
					txtDays.Text = systemSettings.SampleNumberOfDays.ToString();
				}

				if (systemSettings.ShowNameFirstInDropdown == true)
				{
					rbYesShowNameFirst.Checked = true;
				}
				else
				{
					//3/27/23
					//rbYesShowNameFirst.Checked = false;
					rbNoShowNameFirst.Checked = true;
				}

				//5/22/18
				txtLotNumber.ForeColor = SystemColors.ControlDark;
				txtLotNumber.BackColor = SystemColors.MenuBar;
				txtLotNumber.ReadOnly = true;
				if (systemSettings.AutoGenerateLotNumber == false)
				{
					rbAutoLotNo.Checked = true;
				}
				else
				{
					rbAutoLotYes.Checked = true;
					txtLotNumber.ForeColor = System.Drawing.Color.Black;
					txtLotNumber.BackColor = System.Drawing.Color.White;
					txtLotNumber.ReadOnly = false;
				}

				//5/22/18
				txtLotNumber.Text = systemSettings.NextLotNumber.ToString();


				//4/14/22
				rb1inchLabel.Checked = false;
				rb2inchLabel.Checked = false;

				if (systemSettings.LabelSize == 1)
				{
					rb1inchLabel.Checked = true;
				}
				else
				{
					rb2inchLabel.Checked = true;
				}
								
				lueFurtherAnalysis.EditValue = (Int32)systemSettings.FurtherAnalysisDefaultID;              //8/31/22

				if (systemSettings.GuaranteeReportDecimals == 0)                                            //11/21/22
				{
					txtGuaranteeDecimals.Text = "8";
				}
				else
				{
					txtGuaranteeDecimals.Text = systemSettings.GuaranteeReportDecimals.ToString();
				}
								
				lueGroundUnGround.EditValue = (Int32)systemSettings.GroundUnGroundDefaultID;                //11/21/22
				
				txtUserName.Text = systemSettings.UserName;                                                 //12/9/22
				txtUpdateStamp.Text = systemSettings.UpdateStamp.ToString();                                //12/9/22
								
				if (systemSettings.AnalysisFilteredDataDefault == false)                                    //10/19/23
				{
					rbNoFilteredData.Checked = true;
				}
				else
				{
					rbYesFilteredData.Checked = true;
				}
								
				if (systemSettings.AutoFillSampleTestTypeIncludeBox == false)                               //10/19/23
				{
					rbNoTestType.Checked = true;
				}
				else
				{
					rbYesTestType.Checked = true;
				}
			}
		}

		private void DataBindlue()
		{
			List<AIMNutrientInfo> aimNutrient = new List<AIMNutrientInfo>();
			aimNutrient = AIMManager.GetAllAIMNutrient(true);
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
			lueAimNutrient.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAimNutrient, "AIMNutrientID", "AIMNutrientCode");
			lueAimNutrient.Properties.Columns["AIMNutrientCode"].Visible = true;
			lueAimNutrient.Properties.Columns["AIMNutrientName"].Visible = true;

			//1/3/23
			lueAimNutrientDryMatter.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueAimNutrientDryMatter, "AIMNutrientID", "AIMNutrientCode");
			lueAimNutrientDryMatter.Properties.Columns["AIMNutrientCode"].Visible = true;
			lueAimNutrientDryMatter.Properties.Columns["AIMNutrientName"].Visible = true;


			//8/31/22
			List<DataSourceTypeInfo> furtherAnalyses = new List<DataSourceTypeInfo>();
			furtherAnalyses = AIMManager.GetDataSourceFurtherAnalyses(true);
			furtherAnalyses.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--None--"
				}
			);
			lueFurtherAnalysis.Properties.DataSource = furtherAnalyses;
			XtraEditors.LoadLookUpEdit(ref lueFurtherAnalysis, "DataSourceTypeID", "Name", "TypeDisplay");

			//11/21/22
			List<GroundUnGroundInfo> groundUnGround = new List<GroundUnGroundInfo>();
			groundUnGround = AIMManager.GetGroundUnGround();

			//1/19/24 - uncommented insert
			groundUnGround.Insert
			(
				0,
				new GroundUnGroundInfo
				{
					GroundUnGroundID = 0,
					GroundUnGroundString = "--Select--"
				}
			);

			lueGroundUnGround.Properties.DataSource = groundUnGround;
			XtraEditors.LoadLookUpEdit(ref lueGroundUnGround, "GroundUnGroundID", "GroundUnGroundString");

		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			{
				try
				{
					//Close();
					if (_changesMade == true)
					{
						DialogResult dr = new DialogResult();
						dr = MessageBox.Show("Save Changes?", "Warning...", MessageBoxButtons.YesNoCancel);
						if (dr != DialogResult.Yes)
						{
							_changesMade = false;
							Close();
						}
						else
						{
							btnSave.PerformClick();
							Close();
						}
					}
					else
					{
						Close();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (_formLoad == true)
			{
				return;
			}

			Int32 days = 0;
			Int32 decNumber = 0;

			if ((Int32)lueAimNutrient.EditValue == 0)
			{
				MessageBox.Show("Please select Moisture Code.");
				lueAimNutrient.Focus();
				lueAimNutrient.ShowPopup();
				return;
			}
			if ((Int32)lueAimNutrientDryMatter.EditValue == 0)
			{
				MessageBox.Show("Please select Dry Matter Code.");
				lueAimNutrientDryMatter.Focus();
				lueAimNutrientDryMatter.ShowPopup();
				return;
			}

			SystemSettingsInfo settings = new SystemSettingsInfo();
			settings.SystemSettingsID = _SystemSettingsID;
			settings.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			settings.AIMNutrientID = (Int32)lueAimNutrient.EditValue;

			if (rbYesAllowAIMInactive.Checked == true)									//4/28/17
			{
				settings.AllowAIMInactive = true;
			}
			else
			{
				settings.AllowAIMInactive = false;
			}
			
			if (rbYesShowAnalyzerMaterials.Checked == true)								//8/11/17
			{
				settings.ShowAnalyzerMaterials = true;
			}
			else
			{
				settings.ShowAnalyzerMaterials = false;
			}
			
			Int32.TryParse(txtDays.Text, out days);										//12/19/17			
			settings.SampleNumberOfDays = days;
						
			if (rbYesShowNameFirst.Checked == true)										//1/12/18
			{
				settings.ShowNameFirstInDropdown = true;
			}
			else
			{
				settings.ShowNameFirstInDropdown = false;
			}
						
			if (rbAutoLotYes.Checked == true)											//5/22/18
			{
				settings.AutoGenerateLotNumber = true;
			}
			else
			{
				settings.AutoGenerateLotNumber = false;
			}
						
			Int32 lotNumber = 0;														//5/22/18
			Int32.TryParse(txtLotNumber.Text, out lotNumber);
			settings.NextLotNumber = lotNumber;
						
			if (rb1inchLabel.Checked == true)											//4/14/22
			{
				settings.LabelSize = 1;
			}
			else
			{
				settings.LabelSize = 2;
			}
						
			settings.FurtherAnalysisDefaultID = (Int32)lueFurtherAnalysis.EditValue;    //8/31/22						
			Int32.TryParse(txtGuaranteeDecimals.Text, out decNumber);                   //11/21/22
			settings.GuaranteeReportDecimals = decNumber;                               //11/21/22			
			settings.GroundUnGroundDefaultID = (Int32)lueGroundUnGround.EditValue;      //11/21/22			
			settings.DryMatterAIMNutrientID = (Int32)lueAimNutrientDryMatter.EditValue; //1/3/23
			
			if (rbNoFilteredData.Checked == true)                                       //10/19/23
			{
				settings.AnalysisFilteredDataDefault = false;
			}
			else
			{
				settings.AnalysisFilteredDataDefault = true;
			}
						
			if (rbNoTestType.Checked == true)                                           //10/19/23
			{
				settings.AutoFillSampleTestTypeIncludeBox = false;
			}
			else
			{
				settings.AutoFillSampleTestTypeIncludeBox = true;
			}

			ErrorInfo error = new ErrorInfo();
			error = AIMManager.SaveSystemSettings(ref settings);
			if (error.ErrorNumber > 0)
			{
				return;
			}
			
			DataBind();                                                                 //12/9/22						
			_changesMade = false;                                                       //12/9/22
		}

		private void lueAimNutrient_EditValueChanged(object sender, EventArgs e)
		{
			if (lueAimNutrient.GetColumnValue("AIMNutrientName") != null)
			{
				txtNutrientName.Text = lueAimNutrient.GetColumnValue("AIMNutrientName").ToString();
			}

			if (_formLoad == true)
			{ }
			else
			{
				if ((Int32)lueAimNutrient.EditValue != 0)
				{
					//12/9/22
					//btnSave.PerformClick();
					_changesMade = true;
					
				}
				_formLoad = false;
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
			ttip.SetToolTip(btnSave, "Save System Settings");
			ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

		}



		private void btnMathTypes_Click(Object sender, EventArgs e)
		{
			Close();
			AIMGlobal.open_form(mathType = new MathType());
		}



		private void btnResize_Click(Object sender, EventArgs e)
		{
			//6/14/17
			WindowState = FormWindowState.Normal;
			btnResize.Visible = false;
		}

		private void SystemSettings_SizeChanged(Object sender, EventArgs e)
		{
			//6/14/17
			btnResize.Visible = true;
		}


		private void rbYesAllowAIMInactive_CheckedChanged(Object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}


		private void txtDays_Leave(Object sender, EventArgs e)
		{
			Int32 days = 0;
			Int32.TryParse(txtDays.Text, out days);
			if (days == 0)
			{
				MessageBox.Show("Sample days must be greater than 0. Days will be set to the default (60 days).");
				txtDays.Text = "60";
			}
		}

		private void btnQualityControlParameters_Click(Object sender, EventArgs e)
		{
			//1/8/24 -- this was missing??
			Close();
			AIMGlobal.open_form(sampleQuality = new SampleQuality());
		}


		private void rbAutoLotYes_Click(Object sender, EventArgs e)
		{
			//5/22/18
			txtLotNumber.ForeColor = System.Drawing.Color.Black;
			txtLotNumber.BackColor = System.Drawing.Color.White;
			txtLotNumber.ReadOnly = false;
		}

		private void rbAutoLotNo_Click(Object sender, EventArgs e)
		{
			//5/22/18
			txtLotNumber.ForeColor = SystemColors.ControlDark;
			txtLotNumber.BackColor = SystemColors.MenuBar;
			txtLotNumber.ReadOnly = true;
		}


		private void txtLotNumber_Validating(Object sender, System.ComponentModel.CancelEventArgs e)
		{
			Int32 i = 0;
			Boolean result = Int32.TryParse(txtLotNumber.Text, out i);
			if (result == false)
			{
				MessageBox.Show("Please enter a numeric value.");
				txtLotNumber.Text = String.Empty;
				return;
			}
		}

		//5/26/20
		private void btnSupplierLocationGuarantees_Click(object sender, EventArgs e)
		{
			try
			{
				Close();
				AIMGlobal.aimSupplierGuaranteesTab = true;
				AIMGlobal.open_form(aimMaintenance = new AIM());

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}

		private void rbYesSpecific_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		private void rbNoSpecific_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		private void rbYesShowNameFirst_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		private void rbNoShowNameFirst_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		private void rbYesShowAnalyzerMaterials_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		private void rbNoShowAnalyzerMaterials_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		private void rb1inchLabel_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		private void rb2inchLabel_CheckedChanged(object sender, EventArgs e)
		{
			//12/9/22
			//btnSave.PerformClick();
			if (_formLoad == false)
			{
				_changesMade = true;
			}
			

				
		}

		private void txtGuaranteeDecimals_Leave(object sender, EventArgs e)
		{
			Int32 decNumber = 0;
			Int32.TryParse(txtGuaranteeDecimals.Text, out decNumber);
			if (decNumber == 0)
			{
				MessageBox.Show("Decimal number must be greater than 0 -- will be set to 8.");
				txtGuaranteeDecimals.Text = "8";
			}
		}

		//12/9/22
		private void txtGuaranteeDecimals_TextChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}
		//12/9/22
		private void rbAutoLotYes_CheckedChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}
		//12/9/22
		private void rbAutoLotNo_CheckedChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}
		//12/9/22
		private void rbNoAllowAIMInactive_CheckedChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}
		//12/9/22
		private void txtDays_TextChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}
		//12/9/22
		private void lueFurtherAnalysis_EditValueChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}
		//12/9/22
		private void lueGroundUnGround_EditValueChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}
		//12/9/22
		private void txtLotNumber_TextChanged(object sender, EventArgs e)
		{
			if (_formLoad == false)
			{
				_changesMade = true;
			}
		}

		//1/3/23
		private void lueAimNutrientDryMatter_EditValueChanged(object sender, EventArgs e)
		{
			if (lueAimNutrientDryMatter.GetColumnValue("AIMNutrientName") != null)
			{
				txtNutrientNameDryMatter.Text = lueAimNutrientDryMatter.GetColumnValue("AIMNutrientName").ToString();
			}

			if (_formLoad == true)
			{ }
			else
			{
				if ((Int32)lueAimNutrientDryMatter.EditValue != 0)
				{					
					_changesMade = true;
				}
				_formLoad = false;
			}
		}

		//1/8/24
		private void btnCompanyName_Click(object sender, EventArgs e)
		{			
			try
			{
				Close();				
				AIMGlobal.open_form(companyName = new CompanyName());

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Warning...", MessageBoxButtons.OK);
			}
		}
	}
		
}
