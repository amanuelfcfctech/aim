using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;

namespace CFCAIM
{
	public partial class ExportBatchMaintenancePopUp : Form
	{

		CFCUser.Global userGlobal = new CFCUser.Global();
        //MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];\
        Int32 _updateDetailID = 0;
		
		#region Form Functions
		public ExportBatchMaintenancePopUp()
		{
			InitializeComponent();
		}
		private void ExportBatchMaintenancePopUp_Load(object sender, EventArgs e)
		{
			System.Drawing.Icon ico = Properties.Resources.CFCAIM;
			this.Icon = ico;
			DataBindlue();
			SetRadioButtons();
					

			//------------------------------------------------
			//10/5/23 - if there are filtered rows, show the Filter Y/N box
			if (AIMGlobal.analysisDataListFiltered == null)	
			{
				//1/9/24
				//rbYes.Checked = false;
				pnlFilter.Visible = false;
			}
			else if (AIMGlobal.analysisDataListFiltered.Count > 0)
			{				
				pnlFilter.Visible = true;
			}

			//------------------------------------------------

			//1/9/24
			if (pnlFilter.Visible == true)
			{

				//10/19/23 - check system setting for Y/N default
				SystemSettingsInfo systemSettings = new SystemSettingsInfo();
				systemSettings = AIMManager.GetSystemSettings();
				rbNo.Checked = true;
				if (systemSettings != null)
				{
					if (systemSettings.AnalysisFilteredDataDefault == true)
					{
						rbYes.Checked = true;
					}
				}
			}

			//------------------------------------------------
			ToolTip ttip = new ToolTip();
			ttip.SetToolTip(btnExit, "Exit");			
				
		
		}
		private void DataBindlue()
		{

			List<AnalysisExportBatchMaintenance> exportHeaderName = new List<AnalysisExportBatchMaintenance>();
			exportHeaderName = AIMManager.GetExportBatchMaintenance();
			exportHeaderName.Insert
			(

				0,
				new AnalysisExportBatchMaintenance
				{
					UpdateIngredientHeaderID = 0,
					UpdateName = "--Select--"
				}
			);
		

		}
		private void SetRadioButtons()
		{
			//1/9/24
			//if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay)
			//{
			//	rdbSpecificOverlay.Checked = true;
			//	txtCFCPlantName.Text = AIMGlobal.exportbatchCFCPlantName;
			//}
			//else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay)
			//{
			//	rdbDefaultOverlay.Checked = true;
			//	txtCFCPlantName.Visible = false;
			//}
			//else
			//{
			//	rdbBase.Checked = true;
			//	txtCFCPlantName.Visible = false;
			//}

			txtCFCPlantName.Text = "";
			txtCFCPlantName.Visible = false;
			txtPlantGroupName.Text = "";
			txtPlantGroupName.Visible = false;
			txtExpandName.Text = "";
			txtExpandName.Visible = false;
			chkExpand.Checked = false;
			chkExpand.Visible = false;
			lblExpand.Visible = false;
			lblBase.Font = new System.Drawing.Font(lblBase.Font, System.Drawing.FontStyle.Regular);
			lblDefaultOverlay.Font = new System.Drawing.Font(lblDefaultOverlay.Font, System.Drawing.FontStyle.Regular);
			lblSpecificOverlay.Font = new System.Drawing.Font(lblSpecificOverlay.Font, System.Drawing.FontStyle.Regular);
			rbPlantSingle.Checked = false;
			rbPlantGroup.Checked = false;

			//--------------------------------------
			//Check Expand
			if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand ||
					CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand ||
					CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand)
			{
				chkExpand.Visible = true;
				chkExpand.Checked = true;
				txtExpandName.Visible = true;
				txtExpandName.Text = AIMGlobal.exportbatchIngredientGroupName; 
				lblExpand.Visible = true;
			}

			//--------------------------------------
			if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay ||
				CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand)
			{
				rdbSpecificOverlay.Checked = true;
				lblSpecificOverlay.Font = new System.Drawing.Font(lblSpecificOverlay.Font, System.Drawing.FontStyle.Bold);

				if (AIMGlobal.exportbatchPlantSingleOrGroup == "Single") 
				{
					txtCFCPlantName.Visible = true;
					txtCFCPlantName.Text = AIMGlobal.exportbatchCFCPlantName;
					rbPlantSingle.Checked = true;
				}				
				else
				{
					txtPlantGroupName.Visible = true;
					txtPlantGroupName.Text = AIMGlobal.exportbatchPlantGroupName;
					rbPlantGroup.Checked = true;
				}
				
			}	
			else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay ||
				CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand) 
			{
				rdbDefaultOverlay.Checked = true;
				lblDefaultOverlay.Font = new System.Drawing.Font(lblDefaultOverlay.Font, System.Drawing.FontStyle.Bold);
			}
			else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.Base ||
				CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand) 
			{
				rdbBase.Checked = true;
				lblBase.Font = new System.Drawing.Font(lblBase.Font, System.Drawing.FontStyle.Bold);
			}
			else
			{
				rdbBase.Checked = true;				
			}

		}
		
		#endregion

		#region Form Buttons
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
		#endregion

		private void btnAddBatch_Click(object sender, EventArgs e)
		{
			Int32 saveIngredientID = 0;
			Int32 saveNutrientID = 0;
			Int32 savePlantID = 0;
			Int32 sampleCount = 0;

			try
			{
				Int32 headerID = 0;
				AnalysisExportBatchMaintenance exportBatch = new AnalysisExportBatchMaintenance();
				ExportBatchMaintenance exportForm = new ExportBatchMaintenance();
				List<ExportIngredientInfo> group = new List<ExportIngredientInfo>();                            //10/16/23

				List<ExportPlantInfo> groupPlant = new List<ExportPlantInfo>();                                 //1/12/24
				List<ExportIngredientInfo> groupIngredient = new List<ExportIngredientInfo>();                  //1/15/24

				ErrorInfo result = new ErrorInfo();

				if (txtBatchName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please enter a Batch Name.", "Warning...", MessageBoxButtons.OK);
					txtBatchName.Focus();
				}
				else
				{
					//----------------- HEADER -------------------------------------------- 	
					AIMGlobal.SplashScreenShow();
					exportBatch.UpdateIngredientHeaderID = 0;
					exportBatch.UpdateName = txtBatchName.Text;
					exportBatch.Status = 1;
					exportBatch.AIMDatabaseID = CFCAIMCore.Global.aimDatabase.AIMDatabaseID;
					exportBatch.ExportTargetID = CFCAIMCore.Global.exportbatchTargetID;

					if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand ||                  //10/18/23
						CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand ||        //1/10/24
						CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand)         //1/10/24
					{
						exportBatch.ExportExpandID = AIMGlobal.exportbatchIngredientGroupID;                            //1/10/24
						exportBatch.ExportExpandName = AIMGlobal.exportbatchIngredientGroupName;                        //1/10/24						
					}

					//1/12/24
					if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay ||
						CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand)
					{
						if (AIMGlobal.exportbatchPlantSingleOrGroup == "Group")
						{
							exportBatch.ExportPlantGroupID = AIMGlobal.exportbatchPlantGroupID;
							exportBatch.ExportPlantGroupName = AIMGlobal.exportbatchPlantGroupName;
							exportBatch.ExportTargetID = (Int32)ExportTargetEnum.SpecificOverlayPlantGroup;                 //1/26/24
						}
					}

					exportBatch.UpdateCFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					exportBatch.AIMOwnerLocationID = AIMGlobal.exportbatchAIMOwnerLocationID;
					exportBatch.CFCPlantID = AIMGlobal.exportbatchAIMOwnerLocationID;
					exportBatch.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					exportBatch.CreateDate = DateTime.Now;
					exportBatch.ExportStartDate = CFCAIMCore.Global.ExportStartDate;
					exportBatch.ExportEndDate = CFCAIMCore.Global.ExportEndDate;
					
					result = AIMManager.SaveIngredientHeader(ref exportBatch);
					if (result.ErrorNumber != 0)
					{
						MessageBox.Show("Unable to save the export Batch.", "Critical...", MessageBoxButtons.OK);
						this.Close();
						return;
					}

					if (rbYes.Checked == true)
					{
						AIMGlobal.analysisDataList = AIMGlobal.analysisDataListFiltered;
					}

					//----------------- DETAIL -------------------------------------------- 
					headerID = result.RecordID;  //save header ID 
					foreach (AnalysisInfo item in AIMGlobal.analysisDataList)
					{
						//11/22/23
						if ((CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay ||
							CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand) && item.CFCPlantID == 0)
						{
							// skip defaultOverlay records that are not cross-referenced (Mark found error)
						}

						else
						{
							if (item.CFCNutrientID > 0)
							{
                                #region Base
                                //BASE ----------------------------------------------------
                                if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.Base ||
									CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand)
								{
									if (item.CFCIngredientID != saveIngredientID || item.CFCNutrientID != saveNutrientID)
									{
										if (sampleCount == 0)
										{
											updateDetailSampleCount(1);
										}
										else
										{
											updateDetailSampleCount(sampleCount);
										}
										saveIngredientID = item.CFCIngredientID;
										saveNutrientID = item.CFCNutrientID;
										updateDetail(headerID, item);
										sampleCount = 0;
									}
									else
									{
										sampleCount = sampleCount + item.NumberOfSample;
									}

									//----------------------
									//11/17/23
									//Expand Base
									if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.BaseExpand)
									{
										group = AIMManager.GetExportIngredient_GroupID(AIMGlobal.exportbatchIngredientGroupID);
										foreach (var groupItem in group)
										{
											if (item.AIMMaterialID == groupItem.AIMMaterialID)
											{
												{
													updateDetailSampleCount(sampleCount);
												}
												saveIngredientID = item.CFCIngredientID;
												saveNutrientID = item.CFCNutrientID;
												updateDetailIngredientGroup(headerID, item, groupItem.CFCIngredientID, "Base", 0);
											}
										}
									}
								}
                                #endregion

                                #region Default
                                //DEFAULT -------------------------------------------------								
                                else if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlay ||
										CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand)
								{
									if (item.CFCIngredientID != saveIngredientID || item.CFCNutrientID != saveNutrientID || item.CFCPlantID != savePlantID)
									{
										if (sampleCount != 0)
										{
											updateDetailSampleCount(sampleCount);
										}
										saveIngredientID = item.CFCIngredientID;
										saveNutrientID = item.CFCNutrientID;
										savePlantID = item.CFCPlantID;
										updateDetail(headerID, item);
										sampleCount = 0;
										sampleCount = sampleCount + item.NumberOfSample;
									}
									else
									{
										sampleCount = sampleCount + item.NumberOfSample;
									}

									//----------------------
									//1/10/24
									//Expand Default
									if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.DefaultOverlayExpand)
									{
										group = AIMManager.GetExportIngredient_GroupID(AIMGlobal.exportbatchIngredientGroupID);
										foreach (var groupItem in group)
										{
											if (item.AIMMaterialID == groupItem.AIMMaterialID)
											{
												{
													updateDetailSampleCount(sampleCount);
												}
												saveIngredientID = item.CFCIngredientID;
												saveNutrientID = item.CFCNutrientID;
												updateDetailIngredientGroup(headerID, item, groupItem.CFCIngredientID, "Overlay", 0);
											}
										}
									}
								}
                                #endregion

                                #region Specific
                                //SPECIFIC-------------------------------------------------
                                else
                                {
									if (AIMGlobal.exportbatchPlantSingleOrGroup == "Single")        //1/25/24 - added check for "Single"
									{
										//-----------------------------------------------
										if (item.CFCIngredientID != saveIngredientID || item.CFCNutrientID != saveNutrientID || item.CFCPlantID != savePlantID)
										{
											if (sampleCount != 0)
											{
												updateDetailSampleCount(sampleCount);
											}

											saveIngredientID = item.CFCIngredientID;
											saveNutrientID = item.CFCNutrientID;
											savePlantID = item.CFCPlantID;

											updateDetail(headerID, item);
											sampleCount = 0;
											sampleCount = sampleCount + item.NumberOfSample;

										}
										else
										{
											sampleCount = sampleCount + item.NumberOfSample;
										}
									}

									//----------------------------
									//Get Ingredient Group
									if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand)
									{
										groupIngredient = AIMManager.GetExportIngredient_GroupID(AIMGlobal.exportbatchIngredientGroupID);
									}
																	
									if (AIMGlobal.exportbatchPlantSingleOrGroup == "Single")
									{
										//----------------------------
										//Expand Ingredients for Specific Single Plant
										foreach (var iGroup in groupIngredient)
										{
											if (item.AIMMaterialID == iGroup.AIMMaterialID)
											{
												{
													updateDetailSampleCount(sampleCount);
												}
												saveIngredientID = item.CFCIngredientID;
												saveNutrientID = item.CFCNutrientID;
												updateDetailIngredientGroup(headerID, item, iGroup.CFCIngredientID, "Overlay", 0);
											}
										}
									}
																	
									else if (AIMGlobal.exportbatchPlantSingleOrGroup == "Group")
									{										
										groupPlant = AIMManager.GetExportPlant_GroupID(AIMGlobal.exportbatchPlantGroupID);
										foreach (var gPlant in groupPlant)
										{
											updateDetailSampleCount(sampleCount);
											saveIngredientID = item.CFCIngredientID;
											saveNutrientID = item.CFCNutrientID;
											updateDetailPlantGroup(headerID, item, gPlant.CFCPlantID);

											//----------------------------									
											//Expand Ingredients for Specific Plant Group	
											if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand)
											{												
												//for each ingredient in Ingredient group													
												foreach (var iGroup in groupIngredient)
												{
													if (item.AIMMaterialID == iGroup.AIMMaterialID)
													{
														{
															updateDetailSampleCount(sampleCount);
														}
														saveIngredientID = item.CFCIngredientID;
														saveNutrientID = item.CFCNutrientID;
														updateDetailIngredientGroup(headerID, item, iGroup.CFCIngredientID, "Overlay", gPlant.CFCPlantID);
													}
												}												
											}
										}
									}									
								}
                                #endregion
                            }
                        }

					}
										
					//2/29/24
					//For all but base exports, update IsOverlay field on UpdateIngredientDetail table (added for Maschhoffs)
					if (CFCAIMCore.Global.exportbatchTargetID != (Int32)ExportTargetEnum.Base &&
					    CFCAIMCore.Global.exportbatchTargetID != (Int32)ExportTargetEnum.BaseExpand)
					{
						ErrorInfo overlay = new ErrorInfo();
						overlay = AIMManager.UpdateIngredientDetailIsOverlay(headerID);
						if (overlay.ErrorNumber > 0)
						{
							MessageBox.Show(overlay.ErrorMessage, "Critical...", MessageBoxButtons.OK);
							Close();
							return;
						}
					}
					
					updateDetailSampleCount(sampleCount);
					txtBatchName.Text = String.Empty;

					CFCAIMCore.Global.exportbatchmaintenanceHeaderID = headerID;
					CFCAIMCore.Global.exportbatchmaintenanceHeaderName = exportBatch.UpdateName;
					AIMGlobal.exportComingFromAnalysis = true;

					this.Hide();
					AIMGlobal.SplashScreenHide();
					exportForm.ShowDialog();

					this.Close();

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				AIMGlobal.SplashScreenHide();
			}
		}


		private void updateDetailSampleCount(Int32 sampleCount)
        {
            if (sampleCount == 0)
            {
                sampleCount = 1;
            }
            UpdateIngredientDetail updateDetail = new UpdateIngredientDetail();
            ErrorInfo updateResult = new ErrorInfo();
            updateDetail.UpdateIngredientDetailID = _updateDetailID;            
            updateDetail.NumberOfSamples = sampleCount;
            updateResult = AIMManager.SaveIngredientDetailNumberOfSamples(_updateDetailID, sampleCount);
            if (updateResult.ErrorNumber != 0)
            {
                MessageBox.Show("Unable to save the export batch detail (when updating sample count).", "Critical...", MessageBoxButtons.OK);
                Close();
                return;
            }            
        }

		private void updateDetail(Int32 headerID, AnalysisInfo item)
		{		          
            UpdateIngredientDetail exportDetail = new UpdateIngredientDetail();
			ErrorInfo resultDetail = new ErrorInfo();
			exportDetail.UpdateIngredientDetailID = 0;
			exportDetail.UpdateIngredientHeaderID = headerID;						
			exportDetail.CFCIngredientID = item.CFCIngredientID;
			exportDetail.CFCNutrientID = item.CFCNutrientID;
			exportDetail.ProposedValue = item.ProposedNutrientValue;
			exportDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;			
			exportDetail.Override = 0;
            exportDetail.Mean = item.TrimmedMean;
            exportDetail.StandardDeviation = item.TrimmedStandardDeviation;
			exportDetail.Status = (Int32)ExportBatchDetailStatusEnum.Unassigned;

			//---------------------------------------------------
			//1/26/24
			//exportDetail.CFCPlantID = item.CFCPlantID;			
			if (CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlay ||
				CFCAIMCore.Global.exportbatchTargetID == (Int32)ExportTargetEnum.SpecificOverlayExpand)
			{
				if (AIMGlobal.exportbatchPlantSingleOrGroup == "Single")
				{
					exportDetail.CFCPlantID = AIMGlobal.exportbatchAIMOwnerLocationID;
				}
				else
				{
					exportDetail.CFCPlantID = item.CFCPlantID;
				}
			}
			else
			{
				exportDetail.CFCPlantID = item.CFCPlantID;
			}				
			//---------------------------------------------------

			exportDetail.NumberOfSamples = item.NumberOfSample;
			exportDetail.IsOverlay = item.IsOverlay;            

			resultDetail = AIMManager.SaveIngredientDetail(ref exportDetail);			
            if (resultDetail.ErrorNumber != 0)                
			{
				MessageBox.Show("Unable to save the export batch detail.", "Critical...", MessageBoxButtons.OK);
				Close();
				return;
			}           
            _updateDetailID = resultDetail.RecordID;

        }

		//10/17/23
		private void updateDetailIngredientGroup(Int32 headerID, AnalysisInfo item, Int32 groupCFCIngredientID, String BaseOrOverlay, Int32 groupCFCPlantID)
		{
			UpdateIngredientDetail exportDetail = new UpdateIngredientDetail();
			ErrorInfo resultDetail = new ErrorInfo();
			exportDetail.UpdateIngredientDetailID = 0;
			exportDetail.UpdateIngredientHeaderID = headerID;
			exportDetail.CFCIngredientID = groupCFCIngredientID;
			exportDetail.CFCNutrientID = item.CFCNutrientID;
			exportDetail.ProposedValue = item.ProposedNutrientValue;
			exportDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			exportDetail.Override = 0;
			exportDetail.Mean = item.TrimmedMean;
			exportDetail.StandardDeviation = item.TrimmedStandardDeviation;
			exportDetail.Status = (Int32)ExportBatchDetailStatusEnum.Unassigned;

			//----------------------------------------------
			//exportDetail.CFCPlantID = item.CFCPlantID;									//2/5/24
			if (AIMGlobal.exportbatchPlantSingleOrGroup == "Single")
			{
				exportDetail.CFCPlantID = AIMGlobal.exportbatchAIMOwnerLocationID;
			}

			//2/5/24
			else if (AIMGlobal.exportbatchPlantSingleOrGroup == "Group")
			{
				exportDetail.CFCPlantID = groupCFCPlantID;
			}

			else
			{				
				exportDetail.CFCPlantID = item.CFCPlantID;				
			}
			//----------------------------------------------


			exportDetail.NumberOfSamples = item.NumberOfSample;
			
			//11/6/23
			//exportDetail.IsOverlay = item.IsOverlay;
			if (BaseOrOverlay == "Base")                                        //1/10/24
			{
				exportDetail.IsOverlay = false;
			}
			else
			{
				exportDetail.IsOverlay = item.IsOverlay;                        //1/10/24
			}			

			resultDetail = AIMManager.SaveIngredientDetail(ref exportDetail);
			if (resultDetail.ErrorNumber != 0)
			{
				MessageBox.Show("Unable to save the export batch detail.", "Critical...", MessageBoxButtons.OK);
				Close();
				return;
			}
			_updateDetailID = resultDetail.RecordID;

		}

		//1/12/24
		private void updateDetailPlantGroup(Int32 headerID, AnalysisInfo item, Int32 groupCFCPlantID)
		{
			UpdateIngredientDetail exportDetail = new UpdateIngredientDetail();
			ErrorInfo resultDetail = new ErrorInfo();
			exportDetail.UpdateIngredientDetailID = 0;
			exportDetail.UpdateIngredientHeaderID = headerID;
			exportDetail.CFCIngredientID = item.CFCIngredientID;
			exportDetail.CFCNutrientID = item.CFCNutrientID;
			exportDetail.ProposedValue = item.ProposedNutrientValue;
			exportDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			exportDetail.Override = 0;
			exportDetail.Mean = item.TrimmedMean;
			exportDetail.StandardDeviation = item.TrimmedStandardDeviation;
			exportDetail.Status = (Int32)ExportBatchDetailStatusEnum.Unassigned;
			exportDetail.CFCPlantID = groupCFCPlantID;
			exportDetail.NumberOfSamples = item.NumberOfSample;
			exportDetail.IsOverlay = item.IsOverlay;

			resultDetail = AIMManager.SaveIngredientDetail(ref exportDetail);
			if (resultDetail.ErrorNumber != 0)
			{
				MessageBox.Show("Unable to save the export batch detail.", "Critical...", MessageBoxButtons.OK);
				Close();
				return;
			}
			_updateDetailID = resultDetail.RecordID;

		}

	}
}
