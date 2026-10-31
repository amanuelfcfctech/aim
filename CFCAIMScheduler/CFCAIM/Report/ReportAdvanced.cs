using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CFCAIMCore;
using CFCAIMCore.Entity;
using CFCAIMCore.Manager;
using Core.Library.DevExpress;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using System.Drawing;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System.Collections;
using CFCUser.Entity;
using CFCUser.Manager;

namespace CFCAIM.Report
{
	public partial class ReportAdvanced : Form
	{
		//6/22/22 - commented, not used?
		//ReportView reportView;		

		CFCUser.Global userGlobal = new CFCUser.Global();
		MainMenu _mainMenu = (MainMenu)Application.OpenForms["MainMenu"];
		Int32 _selectedQueryTempleteID = 0;		
		List<ReportQueryInfo> _repQuery = new List<ReportQueryInfo>();
		List<ReportQueryDetailInfo> _repQueryDetail = new List<ReportQueryDetailInfo>();
		List<SampleInfo> _sample = new List<SampleInfo>();			   				
		List<ReportInfo> _reports = new List<ReportInfo>();
		Boolean _isAnalysis = false;
		Boolean _saveAs = false;

		
		Int32 _dataSourceTypeLimit = 0;											//5/31/22		
		Boolean _loadingForm = true;											//10/31/22

		Int32 _ownerLocationGroupID = 0;                                        //10/17/23
		Int32 _supplierGroupID = 0;                                             //10/17/23
		Int32 _supplierLocationGroupID = 0;                                     //10/17/23
		CFCUser.Global _userGlobal = new CFCUser.Global();                      //10/17/23
		UserInfo _user = new UserInfo();                                        //10/17/23

		Report_Designer report_Designer;										//12/28/23

		//-------------------------------------------------------------------------------

		public ReportAdvanced()
		{
			InitializeComponent();
		}

		private void ReportQuery_Load(object sender, EventArgs e)
		{
			{
				try
				{
					SetUserGroups();											//10/17/23											

					DataBindLue();
					SetToolTips();

					DataBindQueryTabLue();
					DataBindMaterial();
					DataBindOwnerLocationlue();
					DataBindSupplierLocationlue();

					//5/31/22
					_dataSourceTypeLimit = 0;

					//10/14/22
					DataBindNutrientDriverlue();

					//6/14/22 - hide datasource tab for now (this was set up for Butterball; however, it wasn't what they wanted)
					tabControlQuery.TabPages.Remove(tpDataSource);

					lueQueryTemplateInTab.EditValue = _selectedQueryTempleteID;
					btnDeleteQueryTemplate.Visible = false;
					WindowState = FormWindowState.Maximized;    //--> MUST BE LAST COMMAND IN THIS SECTION

				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void cboReportName_SelectedIndexChanged(object sender, EventArgs e)
		{
			//5/31/22
			String msg = "";
			_dataSourceTypeLimit = 0;


			//5/31/22 - set datasource limit field - added this field because Butterball's analysis date report requires only 2 datasources but, in the future,
			//											other reports may require more/less.  CFCTech populates this field.
			ReportInfo reportInfo;
			Int32 reportID = 0;
			Int32.TryParse(cboReportName.SelectedValue.ToString(), out reportID);
			reportInfo = AIMManager.GetReportReportID(reportID);
			if (reportInfo == null) return;
			_dataSourceTypeLimit = reportInfo.DataSourceLimit;

			//------------------------------------------------------------------------
			//6/3/22 - currently, the ability to choose between Sample Date and Results Analysis Date is offered ONLY with the Sample Results Comparison report.
			pnlDates.Visible = false;
			rbSampleDate.Checked = true;
			if (reportInfo.ReportID == (Int32)ReportEnum.DataSourceComparisonReport)
			{
				pnlDates.Visible = true;
				rbResultsAnalysisDate.Checked = true;
			}

			//------------------------------------------------------------------------
			//5/31/22
			if (_dataSourceTypeLimit > 0)
			{
				ErrorInfo countField = new ErrorInfo();
				countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceType);
				if (countField.RecordID > _dataSourceTypeLimit)
				{
					msg = "This report does not allow more than " + _dataSourceTypeLimit + " data sources. ";
					msg += Environment.NewLine + Environment.NewLine + "Please select another report or remove data source types.";
					MessageBox.Show(msg, "Warning...");
					cboReportName.SelectedIndex = 0;
					return;
				}
			}

		}

		private void btnPrintPreview_Click(object sender, EventArgs e)
		{
			try
			{
				//6/22/22 - added for DataSourceComparison report
				Int32 datasourcetypeID1 = 0;
				Int32 datasourcetypeID2 = 0;
				Int32 ds1_nut1 = 0;
				Int32 ds1_nut2 = 0;
				Int32 ds1_nut3 = 0;
				Int32 ds2_nut1 = 0;
				Int32 ds2_nut2 = 0;
				Int32 ds2_nut3 = 0;

				if (cboReportName.Text == "" || cboReportName.Text == "--Select--")
				{
					MessageBox.Show("Please select a Report.", "Warning...");
					cboReportName.DroppedDown = true;
					return;
				}
				if (lueQueryTemplateInTab.Text == "" || lueQueryTemplateInTab.Text == "--Select--")
				{
					MessageBox.Show("Please select a Query.", "Warning...");
					lueQueryTemplateInTab.ShowPopup();
					return;
				}

				AIMGlobal.SplashScreenShow();
				List<AnalysisReportsInfo> analysisSummaryToSet = new List<AnalysisReportsInfo>();
				Int32 reportID = 0;
				Int32.TryParse(cboReportName.SelectedValue.ToString(), out reportID);

				//6/22/22 - commented
				#region SampleResultsComparison report - currently not used
				//6/15/22 - Butterball wanted this report rearranged, so ... kept the SampleResultsComparisonReport as-is, and created an entirely new report (DataSourceComparisonReport)
				////Int32 datasourcetypeID1 = 0;
				////Int32 datasourcetypeID2 = 0;
				//////6/1/22
				////if (reportID == (Int32)ReportEnum.SampleResultsComparisonReport)
				////{					
				////	List<DataSourceTypeInfo> dst = new List<DataSourceTypeInfo>();
				////	dst = (List<DataSourceTypeInfo>)grdDataSourceType.DataSource;
				////	if (dst == null)
				////	{
				////		AIMGlobal.SplashScreenHide();
				////		MessageBox.Show("This report requires two datasources to be selected.", "Warning...");
				////		return;
				////	}
				////	else if (dst != null)
				////	{
				////		if (dst.Count < 2)
				////		{
				////			AIMGlobal.SplashScreenHide();
				////			MessageBox.Show("This report requires two datasources to be selected.", "Warning...");
				////			return;
				////		}
				////	}
				////	//grid is sorting by QueryTemplateDetailID, no matter what, so doing this to get datasouces 1 and 2			
				////	datasourcetypeID1 = dst[0].DataSourceTypeID;
				////	datasourcetypeID2 = dst[1].DataSourceTypeID;
				////	if (dst[0].OrderBy > dst[1].OrderBy)
				////	{
				////		datasourcetypeID1 = dst[1].DataSourceTypeID;
				////		datasourcetypeID2 = dst[0].DataSourceTypeID;
				////	}
				////	AIMGlobal.useResultAnalysisDate = false;
				////	if (rbResultsAnalysisDate.Checked == true)
				////	{						
				////		AIMGlobal.useResultAnalysisDate = true;
				////	}
				////	List<SampleResultsComparisonReportInfo> results = new List<SampleResultsComparisonReportInfo>();
				////	results = AIMManager.GetSampleResultsComparison((Int32)lueQueryTemplateInTab.EditValue, datasourcetypeID1, datasourcetypeID2, AIMGlobal.useResultAnalysisDate);
				////	ReportDevExpress.SampleResultsComparisonReport report = new ReportDevExpress.SampleResultsComparisonReport();
				////	report.DataSource = results;
				////	ReportPrintTool tool = new ReportPrintTool(report);
				////	AIMGlobal.SplashScreenHide();
				////	tool.ShowPreviewDialog();
				////}
				#endregion

				//6/22/22
				#region DataSourceComparison report
				if (reportID == (Int32)ReportEnum.DataSourceComparisonReport)
				{
					
					List<DataSourceComparisonInfo> dst = new List<DataSourceComparisonInfo>();
					dst = (List<DataSourceComparisonInfo>)grdDataSourceComparison.DataSource;
					if (dst == null)
					{
						AIMGlobal.SplashScreenHide();
						MessageBox.Show("There are no datasources/nutrients added to the grid.", "Warning...");
						return;
					}					

					//10/14/22					
					if (rbResultsAnalysisDate.Checked == true && (Int32)lueDriverNutrientCode.EditValue == 0)
					{
						AIMGlobal.SplashScreenHide();
						MessageBox.Show("Please select an AIM Nutrient as the driver for this report.", "Warning...");
						lueDriverNutrientCode.ShowPopup();
						return;
					}

					AIMGlobal.useResultAnalysisDate = false;
					if (rbResultsAnalysisDate.Checked == true)
					{
						AIMGlobal.useResultAnalysisDate = true;
					}

					List<DataSourceComparisonInfo> results = new List<DataSourceComparisonInfo>();
					results = (List<DataSourceComparisonInfo>)grdDataSourceComparison.DataSource;
					if (results.Count == 0)
					{
						AIMGlobal.SplashScreenHide();
						return;
					}
					datasourcetypeID1 = results[0].DataSourceTypeID1;
					datasourcetypeID2 = results[0].DataSourceTypeID2;
					if (results.Count == 3)
					{
						ds1_nut1 = results[0].DS1_NutID;
						ds2_nut1 = results[0].DS2_NutID;
						ds1_nut2 = results[1].DS1_NutID;
						ds2_nut2 = results[1].DS2_NutID;
						ds1_nut3 = results[2].DS1_NutID;
						ds2_nut3 = results[2].DS2_NutID;
					}
					if (results.Count == 2)
					{
						ds1_nut1 = results[0].DS1_NutID;
						ds2_nut1 = results[0].DS2_NutID;
						ds1_nut2 = results[1].DS1_NutID;
						ds2_nut2 = results[1].DS2_NutID;
					}
					if (results.Count == 1)
					{
						ds1_nut1 = results[0].DS1_NutID;
						ds2_nut1 = results[0].DS2_NutID;
					}
					AIMGlobal.resultCount = results.Count;

					List<DataSourceComparisonInfo> results2 = new List<DataSourceComparisonInfo>();
					results2 = AIMManager.GetDataSourceComparison((Int32)lueQueryTemplateInTab.EditValue, datasourcetypeID1, datasourcetypeID2, AIMGlobal.useResultAnalysisDate,
								ds1_nut1, ds1_nut2, ds1_nut3, ds2_nut1, ds2_nut2, ds2_nut3

								//10/14/22 - added driver nutrient
								, (Int32)lueDriverNutrientCode.EditValue);

					ReportDevExpress.DatasourceComparisonReport report = new ReportDevExpress.DatasourceComparisonReport();
					report.DataSource = results2;
					ReportPrintTool tool = new ReportPrintTool(report);
					AIMGlobal.SplashScreenHide();
					tool.ShowPreviewDialog();
				}
				#endregion
				else
				{
					//-----------------------------------
					switch (reportID)
					{
						case (Int32)ReportEnum.AnalysisSummary:
						case (Int32)ReportEnum.AnalysisPivotReport:
						case (Int32)ReportEnum.AnalysisPivotSummary:

							//4/20/18
							//analysisSummaryToSet = AIMManager.GetAnalysisSummary((Int32)lueQueryTemplateInTab.EditValue, AIMCoreGlobal.AIMDatabaseID);
							if (rbProducts.Checked == true)
							{
								analysisSummaryToSet = AIMManager.GetAnalysisSummary_Product((Int32)lueQueryTemplateInTab.EditValue, AIMCoreGlobal.AIMDatabaseID);
							}
							else
							{
								analysisSummaryToSet = AIMManager.GetAnalysisSummary((Int32)lueQueryTemplateInTab.EditValue, AIMCoreGlobal.AIMDatabaseID);
							}

							break;
						case (Int32)ReportEnum.AnalysisDetail:

							//4/20/18
							//analysisSummaryToSet = AIMManager.GetAnalysisDetail((Int32)lueQueryTemplateInTab.EditValue, AIMCoreGlobal.AIMDatabaseID);
							if (rbProducts.Checked == true)
							{
								analysisSummaryToSet = AIMManager.GetAnalysisDetail_Product((Int32)lueQueryTemplateInTab.EditValue, AIMCoreGlobal.AIMDatabaseID);
							}
							else
							{
								analysisSummaryToSet = AIMManager.GetAnalysisDetail((Int32)lueQueryTemplateInTab.EditValue, AIMCoreGlobal.AIMDatabaseID);
							}

							break;
						case (Int32)ReportEnum.AnalysisDateComparisonReport:

							//4/20/18
							//analysisSummaryToSet = AIMManager.GetAnalysisSummaryForAnalysisDateComparison((Int32)lueQueryTemplateInTab.EditValue);
							if (rbProducts.Checked == true)
							{
								analysisSummaryToSet = AIMManager.GetAnalysisSummaryForAnalysisDateComparison_Product((Int32)lueQueryTemplateInTab.EditValue);
							}
							else
							{
								analysisSummaryToSet = AIMManager.GetAnalysisSummaryForAnalysisDateComparison((Int32)lueQueryTemplateInTab.EditValue);
							}

							break;
						case (Int32)ReportEnum.AnalysisSupplierComparison:
							analysisSummaryToSet = AIMManager.GetAnalysisSourceComparison((Int32)lueQueryTemplateInTab.EditValue);
							break;
					}

					if (analysisSummaryToSet.Count == 0)
					{
						AIMGlobal.SplashScreenHide();
						MessageBox.Show("There are no records to show with this criteria. Change criteria and try again.", "Warning...");
						return;
					}

					ReportFunctions.DataSetAnalysisSummary = analysisSummaryToSet;
					AIMGlobal.SplashScreenHide();
					ReportFunctions.LoadReport(reportID);
					this.Show();

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			AIMGlobal.SplashScreenHide();
		}


		//-------------------------------------------------------------------------------
		#region Databind

		//10/17/23
		private void SetUserGroups()
		{
			_user = UserManager.GetUser(_userGlobal.CFCLoginUser.CFCUserID);
			if (_user != null)
			{
				_ownerLocationGroupID = _user.AIMOwnerLocationGroupID;
				_supplierGroupID = _user.AIMSupplierGroupID;
				_supplierLocationGroupID = _user.AIMSupplierLocationGroupID;
			}
		}

		private void DataBindLue()
		{

			List<QueryTemplateInfo> queryTemplate = new List<QueryTemplateInfo>();

			//6/22/22
			//queryTemplate = AIMManager.GetQueryTemplate();
			queryTemplate = AIMManager.GetQueryTemplateForReportsScreen();

			queryTemplate.Insert
			(
				0,
				new QueryTemplateInfo
				{
					QueryTemplateID = 0,
					Name = "--Select--"
				}
			);

			//Initializing report combo box
			List<ReportInfo> reports = new List<ReportInfo>();

			//6/22/22
			//reports = AIMManager.GetAllReportAnalysis();
			reports = AIMManager.GetAllReport(true, true);

			reports.Insert
			(
				0,
				new ReportInfo
				{
					ReportID = 0,
					ReportName = "--Select--"
				}
			);
			cboReportName.DataSource = reports;
			cboReportName.DisplayMember = "ReportName";
			cboReportName.ValueMember = "ReportID";

			lueQueryTemplateInTab.Properties.DataSource = queryTemplate;
			XtraEditors.LoadLookUpEdit(ref lueQueryTemplateInTab, "QueryTemplateID", "Name");

			btnDeleteQueryTemplate.Visible = false;
			AIMGlobal.SplashScreenHide();

		}
		private void DataBindTemplateLue()
		{
			List<QueryFieldInfo> queryField = new List<QueryFieldInfo>();
			queryField = AIMManager.GetQueryField(true);
			queryField.Insert
			(
				0,
				new QueryFieldInfo
				{
					QueryFieldID = 0,
					Field = "--Select--"
				}
			);

			List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
			queryOperation = AIMManager.GetQueryOperation(true);
			queryOperation.Insert
			(
				0,
				new QueryOperationInfo
				{
					QueryOperationID = 0,
					Operation = "--Select--"
				}
			);

		}
		private void DataBindTemplateGrids()
		{
			grdMaterial.DataSource = AIMManager.GetAIMMaterialForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material);
			grdNutrient.DataSource = AIMManager.GetAIMNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.NutrientCode);
			grdOwner.DataSource = AIMManager.GetAIMOwnerForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Owner);
			grdSupplier.DataSource = AIMManager.GetAIMSupplierForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Supplier);
			grdOwnerLocation.DataSource = AIMManager.GetAIMOwnerLocationForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.OwnerLocation);
			grdSupplierLocation.DataSource = AIMManager.GetAIMSupplierLocationForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SupplierLocation);
			grdMaterialNutrient.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
			grdIngredient.DataSource = AIMManager.GetCFCIngredientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.IngredientCode);

			//1/25/21
			grdSampleType.DataSource = AIMManager.GetSampleTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleType);

			//5/26/22 - currently, this tab and grid is not used
			////grdDataSourceType.DataSource = AIMManager.GetDataSourceTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceType);

			//6/14/22
			grdDataSourceComparison.DataSource = AIMManager.GetDataSourceComparisonForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceComparison);
		}

		//2/6/18]
		private void DataBindIncludeExcludeBoxes()
		{
			DataBindBox(grdMaterialView, rbMaterialInclude, rbMaterialExclude, "Material");
			DataBindBox(grdNutrientView, rbNutrientInclude, rbNutrientExclude, "Nutrient");
			DataBindBox(grdOwnerView, rbOwnerInclude, rbOwnerExclude, "Owner");
			DataBindBox(grdOwnerLocationView, rbOwnerLocInclude, rbOwnerLocExclude, "OwnerLoc");
			DataBindBox(grdSupplierView, rbSupplierInclude, rbSupplierExclude, "Supplier");
			DataBindBox(grdSupplierLocationView, rbSupplierLocInclude, rbSupplierLocExclude, "SupplierLoc");
			DataBindBox(grdIngredientView, rbIngredientInclude, rbIngredientExclude, "Ingredient");

			//1/25/21
			DataBindBox(grdSampleTypeView, rbSampleTypeInclude, rbSampleTypeExclude, "SampleType");
		}

		//2/6/18
		private void DataBindBox(GridView view, RadioButton rbInclude, RadioButton rbExclude, String type)
		{
			if (view.DataRowCount != 0)
			{

				rbInclude.Checked = true;
				Int32 queryOperationID = (Int32)QueryOperationEnum.Equal;

				switch (type)
				{
					case "Material":
						queryOperationID = ((List<AIMMaterialInfo>)view.DataSource)[0].QueryOperationID;
						break;
					case "Nutrient":
						queryOperationID = ((List<AIMNutrientInfo>)view.DataSource)[0].QueryOperationID;
						break;
					case "Owner":
						queryOperationID = ((List<AIMOwnerInfo>)view.DataSource)[0].QueryOperationID;
						break;
					case "OwnerLoc":
						queryOperationID = ((List<AIMOwnerLocationInfo>)view.DataSource)[0].QueryOperationID;
						break;
					case "Supplier":
						queryOperationID = ((List<AIMSupplierInfo>)view.DataSource)[0].QueryOperationID;
						break;
					case "SupplierLoc":
						queryOperationID = ((List<AIMSupplierLocationInfo>)view.DataSource)[0].QueryOperationID;
						break;
					case "MaterialNutrient":
						queryOperationID = ((List<TemplateMaterialNutrientInfo>)view.DataSource)[0].QueryOperationID;
						break;
					case "Ingredient":
						queryOperationID = ((List<CFCIngredientInfo>)view.DataSource)[0].QueryOperationID;
						break;

					//1/25/21
					case "SampleType":
						queryOperationID = ((List<SampleTypeInfo>)view.DataSource)[0].QueryOperationID;
						break;
				}

				if (queryOperationID == (Int32)QueryOperationEnum.NotEqual)
				{
					rbExclude.Checked = true;
				}
			}
		}

		//9/7/17		
		private void DataBindSampleDate()
		{
			List<QueryTemplateDetailInfo> query = new List<QueryTemplateDetailInfo>();

			//For templates that use SampleDate
			query = AIMManager.GetAIMSampleDateForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
			btnRemoveDate.Visible = false;
			if (query.Count > 0)
			{
				if (query[0].StringDate != "")
				{
					dtpValue.Text = query[0].DateValue.ToString();
				}

				if (query[0].StringSecondDate != "")
				{
					dtpSecondValue.Text = query[0].SecondDate.ToString();
				}

				btnRemoveDate.Visible = true;
				List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
				queryOperation = AIMManager.GetQueryOperation(QueryFeildEnum.SampleDate);

				String dateText = "";
				dateText = queryOperation[query[0].QueryOperationID - 1].Operation;
				SetDateTextField((QueryOperationEnum)query[0].QueryOperationID, query[0].Value);
			}

			//---------------------------------------------------------------------------
			//6/13/22
			//For templates that use ResultsAnalysisDate
			query = AIMManager.GetAIMAnalysisDateForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.ResultsAnalysisDate);
			btnRemoveDate.Visible = false;
			if (query.Count > 0)
			{
				if (query[0].StringDate != "")
				{
					dtpValue.Text = query[0].DateValue.ToString();			
				}

				if (query[0].StringSecondDate != "")
				{
					dtpSecondValue.Text = query[0].SecondDate.ToString();
				}

				btnRemoveDate.Visible = true;
				List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
				queryOperation = AIMManager.GetQueryOperation(QueryFeildEnum.ResultsAnalysisDate);

				String dateText = "";
				dateText = queryOperation[query[0].QueryOperationID - 1].Operation;
				SetDateTextField((QueryOperationEnum)query[0].QueryOperationID, query[0].Value);
			}
		}

		//4/20/18 moved this to its own section -- to separate out ingredients and products
		private void DataBindMaterial()
		{
			//MATERIAL --------------------------------------------------------------------
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			List<AIMMaterialRefAnalyzerMaterialInfo> aimMaterialProducts = new List<AIMMaterialRefAnalyzerMaterialInfo>();

			//4/20/18
			//AimMaterial = AIMManager.GetAIMMaterial(true);
			if (rbProducts.Checked == true)
			{
				aimMaterialProducts = AIMManager.GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(0, (Int32)MaterialTypeEnum.Product, 0);
			}
			else
			{
				//10/13/23
				//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient);
				aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)EnumOrderBy.Name);
			}

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

			if (rbProducts.Checked == true)
			{
				lueMaterialCode.Properties.DataSource = aimMaterialProducts;
				XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
				lueMaterialName.Properties.DataSource = aimMaterialProducts;
				XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName", "AIMLocationName");
			}
			else
			{
				lueMaterialCode.Properties.DataSource = aimMaterial;
				XtraEditors.LoadLookUpEdit(ref lueMaterialCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
				lueMaterialName.Properties.DataSource = aimMaterial;
				XtraEditors.LoadLookUpEdit(ref lueMaterialName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");

				//10/9/23
				lueMaterialBeg.Properties.DataSource = aimMaterial;
				XtraEditors.LoadLookUpEdit(ref lueMaterialBeg, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
				lueMaterialEnd.Properties.DataSource = aimMaterial;
				XtraEditors.LoadLookUpEdit(ref lueMaterialEnd, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
			}

		}
		private void DataBindQueryTabLue()
		{

			//5/26/22
			//DATA SOURCE TYPE -------------------------------------------------------------
			List<DataSourceTypeInfo> datasource = new List<DataSourceTypeInfo>();
			datasource = AIMManager.GetDataSourceType(true);
			datasource.Insert
			(
				0,
				new DataSourceTypeInfo
				{
					DataSourceTypeID = 0,
					Name = "--Select--"
				}
			);

			lueDataSourceType.Properties.DataSource = datasource;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceType, "DataSourceTypeID", "Name");
			lueDataSourceType.Properties.Columns["Name"].Caption = "DataSource Name";

			//6/14/22
			lueDataSourceCompare1.Properties.DataSource = datasource;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceCompare1, "DataSourceTypeID", "Name");
			lueDataSourceCompare1.Properties.Columns["Name"].Caption = "DataSource Name";

			//6/14/22
			lueDataSourceCompare2.Properties.DataSource = datasource;
			XtraEditors.LoadLookUpEdit(ref lueDataSourceCompare2, "DataSourceTypeID", "Name");
			lueDataSourceCompare2.Properties.Columns["Name"].Caption = "DataSource Name";

			//SAMPLE DATE -----------------------------------------------------------------
			List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
			queryOperation = AIMManager.GetQueryOperation(QueryFeildEnum.SampleDate);
			queryOperation.Insert
			(
				0,
				new QueryOperationInfo
				{
					QueryOperationID = 0,
					Operation = "--Select--"
				}
			);
			lueOperator.Properties.DataSource = queryOperation;
			XtraEditors.LoadLookUpEdit(ref lueOperator, "QueryOperationID", "Operation");
			lueOperator.EditValue = 0;

			//6/13/22
			//RESULTS ANALYSIS DATE -------------------------------------------------------			
			List<QueryOperationInfo> queryDate = new List<QueryOperationInfo>();
			queryDate = AIMManager.GetQueryOperation(QueryFeildEnum.ResultsAnalysisDate);
			queryDate.Insert
			(
				0,
				new QueryOperationInfo
				{
					QueryOperationID = 0,
					Operation = "--Select--"
				}
			);
			lueOperator.Properties.DataSource = queryDate;
			XtraEditors.LoadLookUpEdit(ref lueOperator, "QueryOperationID", "Operation");
			lueOperator.EditValue = 0;
						
			//MATERIAL/NUTRIENT -----------------------------------------------------------
			List<AIMMaterialInfo> aimMaterial = new List<AIMMaterialInfo>();
			
			//10/13/23
			//aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient);
			aimMaterial = AIMManager.GetAIMMaterial_Active_MaterialType(true, (Int32)MaterialTypeEnum.Ingredient, (Int32)EnumOrderBy.Name);

			lueMaterialNutCode.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialNutCode, "AIMMaterialID", "AIMMaterialCode", "AIMMaterialName");
			lueMaterialNutName.Properties.DataSource = aimMaterial;
			XtraEditors.LoadLookUpEdit(ref lueMaterialNutName, "AIMMaterialID", "AIMMaterialName", "AIMMaterialCode");


			//NUTRIENT --------------------------------------------------------------------
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
			lueNutrientCode.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
			lueNutrientName.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			replueMaterialNutrientCode.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterialNutrientCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
			replueMaterialNutrientName.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueMaterialNutrientName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			//6/14/22
			lueNutrientCodeCompare1.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientCodeCompare1, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
			lueNutrientNameCompare1.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientNameCompare1, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			//6/14/22
			lueNutrientCodeCompare2.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientCodeCompare2, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");
			lueNutrientNameCompare2.Properties.DataSource = aimNutrient;
			XtraEditors.LoadLookUpEdit(ref lueNutrientNameCompare2, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			//6/22/22			
			replueDataSource1NutrientCode.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSource1NutrientCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

			replueDataSource2NutrientCode.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSource2NutrientCode, "AIMNutrientID", "AIMNutrientCode", "AIMNutrientName");

			//6/22/22			
			replueDataSource1NutrientName.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSource1NutrientName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			replueDataSource2NutrientName.DataSource = aimNutrient;
			XtraEditors.LoadGridLookUpEdit(ref replueDataSource2NutrientName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");


			//replueMaterialNutrientName.DataSource = aimNutrient;
			//XtraEditors.LoadGridLookUpEdit(ref replueMaterialNutrientName, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			//OWNER -----------------------------------------------------------------------
			List<AIMOwnerInfo> AimOwner = new List<AIMOwnerInfo>();
			
			//10/17/23
			//AimOwner = AIMManager.GetAIMOwner(true);
			AimOwner = AIMManager.GetAIMOwner(_ownerLocationGroupID, true);

			AimOwner.Insert
			(
				0,
				new AIMOwnerInfo
				{
					AIMOwnerID = 0,
					OwnerCode = "--Select--",
					OwnerName = "--Select--"
				}
			);
			lueOwnerCode.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerCode, "AIMOwnerID", "OwnerCode", "OwnerName");
			lueOwnerName.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerName, "AIMOwnerID", "OwnerName", "OwnerCode");

			lueOwnerCode_Loc.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerCode_Loc, "AIMOwnerID", "OwnerCode", "OwnerName");
			lueOwnerCode_Loc.Properties.Columns["OwnerCode"].Visible = true;

			lueOwnerCode.EditValue = 0;
			lueOwnerCode_Loc.EditValue = 0;
			if (AimOwner.Count == 2)
			{
				//selecting value if there is only one
				lueOwnerCode.EditValue = AimOwner[1].AIMOwnerID;
				lueOwnerCode_Loc.EditValue = AimOwner[1].AIMOwnerID;
			}

			lueOwnerName_Loc.Properties.DataSource = AimOwner;
			XtraEditors.LoadLookUpEdit(ref lueOwnerName_Loc, "AIMOwnerID", "OwnerName", "OwnerCode");
			lueOwnerName_Loc.Properties.Columns["OwnerName"].Visible = true;

			//SUPPLIER --------------------------------------------------------------------
			List<AIMSupplierInfo> AimSupplier = new List<AIMSupplierInfo>();

			//10/17/23
			//AimSupplier = AIMManager.GetSupplier(true);
			AimSupplier = AIMManager.GetSupplier(true, _supplierGroupID);

			AimSupplier.Insert
			(
				0,
				new AIMSupplierInfo
				{
					AIMSupplierID = 0,
					AIMSupplierCode = "--Select--",
					SupplierName = "--Select--"
				}
			);
			lueSupplierCode.Properties.DataSource = AimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierCode, "AIMSupplierID", "AIMSupplierCode", "SupplierName");
			lueSupplierName.Properties.DataSource = AimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierName, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
			lueSupplierCode_Loc.Properties.DataSource = AimSupplier;
			XtraEditors.LoadLookUpEdit(ref lueSupplierCode_Loc, "AIMSupplierID", "AIMSupplierCode", "SupplierName");

			lueSupplierCode_Loc.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";
			lueSupplierName_Loc.Properties.DataSource = AimSupplier;

			XtraEditors.LoadLookUpEdit(ref lueSupplierName_Loc, "AIMSupplierID", "SupplierName", "AIMSupplierCode");
			lueSupplierCode_Loc.Properties.Columns["SupplierName"].Caption = "AIM Supplier Name";

			//INGREDIENT --------------------------------------------------------------------
			List<CFCIngredientInfo> cfcIngredient = new List<CFCIngredientInfo>();
			cfcIngredient = AIMManager.GetAllCFCIngredientAimDatabaseID(true, (Int32)_mainMenu.lueDataMap.EditValue);
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
			lueIngredientCode.Properties.DataSource = cfcIngredient;
			XtraEditors.LoadLookUpEdit(ref lueIngredientCode, "CFCIngredientID", "CFCIngredientCode", "CFCIngredientName");
			lueIngredientName.Properties.DataSource = cfcIngredient;
			XtraEditors.LoadLookUpEdit(ref lueIngredientName, "CFCIngredientID", "CFCIngredientName", "CFCIngredientCode");

			//1/25/21
			//SAMPLE TYPE ------------------------------------------------------------------
			List<SampleTypeInfo> sampleType = new List<SampleTypeInfo>();
			sampleType = AIMManager.GetSampleType(true);
			sampleType.Insert
			(
				0,
				new SampleTypeInfo
				{
					SampleTypeID = 0,
					SampleTypeName = "--Select--"
				}
			);
			lueSampleTypeName.Properties.DataSource = sampleType;
			XtraEditors.LoadLookUpEdit(ref lueSampleTypeName, "SampleTypeID", "SampleTypeName");

		}
		private void DataBindOwnerLocationlue()
		{
			//OWNER LOCATION -------------------------------------------------------------
			Int32 owner = 0;

			//------------------------------------------------------
			List<AIMOwnerLocationInfo> aimOwnerLocation = new List<AIMOwnerLocationInfo>();
			if (lueOwnerName_Loc.EditValue != null)
			{
				Int32.TryParse(lueOwnerName_Loc.EditValue.ToString(), out owner);
			}
			
			//10/17/23
			//aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(owner);
			aimOwnerLocation = AIMManager.GetAIMOwnerLocationForOwner(owner, _ownerLocationGroupID);

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

			lueOwnerLocationName.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationName, "AIMOwnerLocationID", "LocationName", "LocationCode");
			lueOwnerLocationName.Properties.Columns["LocationName"].Visible = true;


			lueOwnerLocationCode.Properties.DataSource = aimOwnerLocation;
			XtraEditors.LoadLookUpEdit(ref lueOwnerLocationCode, "AIMOwnerLocationID", "LocationCode", "LocationName");
			lueOwnerLocationCode.Properties.Columns["LocationCode"].Visible = true;


			if (aimOwnerLocation.Count == 2)
			{
				//selecting value if there is only one                
				lueOwnerLocationCode.EditValue = aimOwnerLocation[1].AIMOwnerLocationID;
			}
			else
			{
				lueOwnerLocationCode.EditValue = 0;
			}
		}

		//10/14/22
		private void DataBindNutrientDriverlue()
		{

			List<AIMNutrientDriverInfo> driver = new List<AIMNutrientDriverInfo>();
			driver = AIMManager.GetAIMNutrientDriver((Int32)lueQueryTemplateInTab.EditValue);
			lueDriverNutrientCode.Properties.DataSource = driver;
			XtraEditors.LoadLookUpEdit(ref lueDriverNutrientCode, "AIMNutrientID", "AIMNutrientName", "AIMNutrientCode");

			if (grdDataSourceComparisonView.RowCount > 0)
			{
				gpDriver.Visible = true;
			}
			else
			{
				gpDriver.Visible = false;
			}

			GetDriverAIMNutientID();

		}

		//10/14/22
		private void UpdateDriverAIMNutrientID()
		{
			AIMManager.UpdateQueryTemplateDetailNutrient((Int32)lueQueryTemplateInTab.EditValue, (Int32)lueDriverNutrientCode.EditValue);
		}

		//10/14/22
		private void GetDriverAIMNutientID()
		{
			QueryTemplateDetailNutrientInfo nutrient = new QueryTemplateDetailNutrientInfo();

			nutrient = AIMManager.GetQueryTemplateDetailNutrient((Int32)lueQueryTemplateInTab.EditValue);
			if (nutrient != null)
			{
				lueDriverNutrientCode.EditValue = nutrient.DriverAIMNutrientID;
			}
		}

		private void DataBindSupplierLocationlue()
		{
			//SUPPLIER LOCATION -------------------------------------------------------------
			Int32 supplier = 0;

			//------------------------------------------------------
			List<AIMSupplierLocationInfo> aimSupplierLocation = new List<AIMSupplierLocationInfo>();
			if (lueSupplierName_Loc.EditValue != null)
			{
				Int32.TryParse(lueSupplierName_Loc.EditValue.ToString(), out supplier);
			}
			
			//10/17/23
			//aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier);
			aimSupplierLocation = AIMManager.GetSupplierLocationForSupplier(supplier, _supplierGroupID, _supplierLocationGroupID);

			aimSupplierLocation.Insert
			(
				0,
				new AIMSupplierLocationInfo
				{
					AIMSupplierLocationID = 0,
					AIMSupplierLocationCode = "--Select--",
					SupplierLocationName = "--Select--"
				}
			);

			lueSupplierLocationCode.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationCode, "AIMSupplierLocationID", "AIMSupplierLocationCode", "SupplierLocationName");
			lueSupplierLocationCode.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			lueSupplierLocationName.Properties.DataSource = aimSupplierLocation;
			XtraEditors.LoadLookUpEdit(ref lueSupplierLocationName, "AIMSupplierLocationID", "SupplierLocationName", "AIMSupplierLocationCode");
			lueSupplierLocationName.Properties.Columns["SupplierLocationName"].Caption = "AIM Supplier Location Name";

			if (aimSupplierLocation.Count == 2)
			{
				//selecting value if there is only one
				lueSupplierLocationCode.EditValue = aimSupplierLocation[1].AIMSupplierLocationID;
			}
			else
			{
				lueSupplierLocationCode.EditValue = aimSupplierLocation;
			}
		}
		#endregion

		//-------------------------------------------------------------------------------
		#region Misc Functions
		private void SetToolTips()
		{
			try
			{
				System.Drawing.Icon ico = Properties.Resources.CFCAIM;
				this.Icon = ico;
				ToolTip ttip = new ToolTip();
				foreach (Control ctrl in pnlTopBtns.Controls)
				{
					ttip.SetToolTip(ctrl, ctrl.Name.Substring(3));
				}
				ttip.SetToolTip(btnNewQueryTemplate, "New Template");
				ttip.SetToolTip(btnCancelQueryTemplateSave, "Cancel ");
				ttip.SetToolTip(btnSaveQueryTemplateAs, "Save Template As");
				ttip.SetToolTip(btnDeleteQueryTemplate, "Delete Template");				
				ttip.SetToolTip(btnSaveQueryTemplate, "Save Template");
				ttip.SetToolTip(btnCancelQueryTemplateSave, "Cancel ");
				ttip.SetToolTip(btnPrintPreview, "Print Preview");
                ttip.SetToolTip(btnResize, "Back to Normal Screen Size");

				ttip.SetToolTip(this.btnAddMaterial, "Add Material to Query");
				ttip.SetToolTip(this.btnAddNutrient, "Add Nutrient to Query");
				ttip.SetToolTip(this.btnAddOwner, "Add Owner to Query");
				ttip.SetToolTip(this.btnAddOwnerLocation, "Add Owner Location to Query");
				ttip.SetToolTip(this.btnAddSupplier, "Add Supplier to Query");
				ttip.SetToolTip(this.btnAddSupplierLocation, "Add Supplier Location to Query");
				ttip.SetToolTip(this.btnAddSampleDate, "Add Sample Date to Query");
				ttip.SetToolTip(this.btnAddMaterialNutrient, "Add Material to Query");
				ttip.SetToolTip(this.btnAddIngredient, "Add Ingredient to Query");
				ttip.SetToolTip(this.btnRemoveDate, "Remove Sample Date from Query");
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
		private void btnHelp_Click(object sender, EventArgs e)
		{
			try
			{
				AIMGlobal.youtubeVideoKey = "4_GEYG9lwgo";
				Movie movie = new Movie();
				movie.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void CheckYellowBox()
		{
			//If template detail exists, show yellow box help text	   
			lblYellowBox.Visible = false;
			txtYellowBox.Visible = false;
			ErrorInfo countField = new ErrorInfo();
			countField = AIMManager.CountQueryTemplateTotal((Int32)lueQueryTemplateInTab.EditValue);
			if (countField.RecordID > 0)
			{
				lblYellowBox.Visible = true;
				txtYellowBox.Visible = true;
			}
		}
		private void showLastUpdate(String userID, String updateDate)
		{
			try
			{
				if (userID.Length > 0)
				{
					lblLastUpdated.Visible = true;
					lblBy.Visible = true;
					txtLastUpdated.Visible = true;
					txtBy.Visible = true;
					txtLastUpdated.Text = updateDate;
					txtBy.Text = userID;
				}
				else
				{
					lblLastUpdated.Visible = false;
					lblBy.Visible = false;
					txtLastUpdated.Visible = false;
					txtBy.Visible = false;
					txtLastUpdated.Text = "";
					txtBy.Text = "";
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
		private void ReportQuery_SizeChanged(Object sender, EventArgs e)
		{
			btnResize.Visible = true;
		}
		private void rbProducts_CheckedChanged(Object sender, EventArgs e)
		{
			DataBindMaterial();
		}
		private void grdMultipleView_ValidateRow(Object sender, ValidateRowEventArgs e)
		{
			try
			{

				TemplateMaterialNutrientInfo nutRef = new TemplateMaterialNutrientInfo();
				nutRef = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetFocusedRow();

				nutRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetailNutrient(0, nutRef.QueryTemplateDetailID, nutRef.AIMNutrientID, userGlobal.CFCLoginUser.CFCUserID);
				grdMaterialNutrient.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnRefreshMaterialNutrient_Click(Object sender, EventArgs e)
		{
			RemoveZeroAimMaterialNutrientRecords();
			grdMaterialNutrient.Refresh();
			if (((List<TemplateMaterialNutrientInfo>)grdMaterialNutrient.DataSource).Count == 0)
			{
				lueMaterialNutCode.EditValue = 0;
				lueMaterialNutName.EditValue = 0;
			}
		}
		private void RemoveZeroAimMaterialNutrientRecords()
		{
			//loop thru grid - delete any records with AIMNutrientID = 0
			List<TemplateMaterialNutrientInfo> multi = new List<TemplateMaterialNutrientInfo>();
			multi = (List<TemplateMaterialNutrientInfo>)grdMaterialNutrient.DataSource;
			if (multi != null)
			{
				foreach (var item in multi)
				{
					if (item.AIMNutrientID == 0)
					{
						DeleteQueryTemplateDetail(item.QueryTemplateDetailID);
					}
				}
			}
			grdMaterialNutrient.DataSource = AIMManager.GetMaterialNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
		}
		private void tabControlQuery_DrawItem(Object sender, DrawItemEventArgs e)
		{
			// This event is called once for each tab button in your tab control

			ErrorInfo countField = new ErrorInfo();


			// First paint the background with a color based on the current tab		

			#region Tab BACKGROUND           
			switch (e.Index)    // e.Index is the index of the tab in the TabPages collection.
			{
				case 0:         //SampleDate/AnalysisDate

					//6/8/22
					//countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
					//if (lblDate.Text == "Results Analysis Date")
					if (rbResultsAnalysisDate.Checked == true)
					{
						countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.ResultsAnalysisDate);
					}
					else
					{
						countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
					}


					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;

				case 1:         //Material

					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}

					break;
				case 2:         //Nutrient
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.NutrientCode);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 3:         //Owner
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Owner);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 4:         //OwnerLocation
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.OwnerLocation);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 5:         //Supplier			
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Supplier);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 6:         //SupplierLocation
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SupplierLocation);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 7:         //Material-Nutrient
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.MaterialNutrient);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;
				case 8:         //Ingredient
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.IngredientCode);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;

				case 9:         //SampleType
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleType);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;

				//case 10:         //Datasource Type
				//	countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceType);
				//	if (countField.RecordID != 0)
				//	{
				//		e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
				//	}
				//	else
				//	{
				//		e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
				//	}
				//	break;

				//This SHOULD be case 11 but, since the Datasource Type tab is currently hidden, the index = 10 for Datasource Comparison
				case 10:         //Datasource Comparison
					countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceComparison);
					if (countField.RecordID != 0)
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.LightYellow), e.Bounds);
					}
					else
					{
						e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					}
					break;

				default:
					e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke), e.Bounds);
					break;
			}
			#endregion


			#region Tab TEXT
			// Then draw the current tab button text 
			Rectangle paddedBounds = e.Bounds;
			paddedBounds.Inflate(1, -1);
			//e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, this.Font, SystemBrushes.HighlightText, paddedBounds);
			//e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, e.Bounds);

			//6/8/22
			//e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, paddedBounds);
			if (e.Index == 0)
			{
				//if (lblDate.Text == "Results Analysis Date")
				if (rbResultsAnalysisDate.Checked == true)
				{
					e.Graphics.DrawString("Analysis Date", new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, paddedBounds);
				}
				else
				{
					e.Graphics.DrawString("Sample Date", new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, paddedBounds);
				}

			}
			else
			{
				e.Graphics.DrawString(tabControlQuery.TabPages[e.Index].Text, new Font(tabControlQuery.Font, FontStyle.Bold), Brushes.Black, paddedBounds);
			}


			#endregion

			//9/14/17
			CheckYellowBox();

		}
		private void replueMaterialNutrientCode_Validating(Object sender, CancelEventArgs e)
		{
			try
			{
				CheckForMaterialNutrientDuplicates(sender, e);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void CheckForMaterialNutrientDuplicates(Object sender, CancelEventArgs e)
		{
			try
			{
				//9/5/17 - check to see if this material-nutrient combination already exists.
				Int32 aimNutrientID = 0;
				TemplateMaterialNutrientInfo matNut = new TemplateMaterialNutrientInfo();
				QueryTemplateDetailNutrientInfo matNutrient = new QueryTemplateDetailNutrientInfo();
				matNut = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetFocusedRow();

				//10/1/19 - changed from obsolete "Properties.KeyValue" to ".EditValue"
				//aimNutrientID = (Int32)((LookUpEdit)(sender)).Properties.KeyValue;
				aimNutrientID = (Int32)((LookUpEdit)(sender)).EditValue;


				matNutrient = AIMManager.GetQueryTemplateDetailNutrient(matNut.QueryTemplateID, matNut.AIMMaterialID, aimNutrientID);
				if (matNutrient != null)
				{
					MessageBox.Show("This material/nutrient combination has already been selected.", "Information...", MessageBoxButtons.OK);
					e.Cancel = true;
					SendKeys.Send("{ESC}");
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void tabControlQuery_SelectedIndexChanged(Object sender, EventArgs e)
		{
			if (CreatedInAnalysisCheck(false) == 0)
			{
				RemoveZeroAimMaterialNutrientRecords();
			}

			//6/22/22 - DataSourceComparison report -- this SHOULD be 11 but, since the Datasource Type tab is currently hidden, the index = 10 for Datasource Comparison
			if (tabControlQuery.SelectedIndex == 10)
			{
				checkGridNutrientCount();
			}

		}
		private void replueMaterialNutrientName_Validating(Object sender, CancelEventArgs e)
		{
			CheckForMaterialNutrientDuplicates(sender, e);
		}
		private void lueOperator_EditValueChanged(Object sender, EventArgs e)
		{
			QueryOperationEnum queryOperation;
			Int32 operation = 0;

			//cm
			//lblValue.Visible = true;

			Int32.TryParse(lueOperator.EditValue.ToString(), out operation);
			queryOperation = (QueryOperationEnum)operation;

			switch (queryOperation)
			{
				case QueryOperationEnum.PastNDaysToDate:
					lblDays.Visible = true;
					txtValue.Visible = true;
					dtpValue.Visible = false;
					dtpSecondValue.Visible = false;
					break;

				case QueryOperationEnum.MonthToDate:
				case QueryOperationEnum.YearToDate:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = false;
					dtpSecondValue.Visible = false;
					break;

				case QueryOperationEnum.Equal:
				case QueryOperationEnum.NotEqual:
				case QueryOperationEnum.GreaterThan:
				case QueryOperationEnum.LessThan:
				case QueryOperationEnum.GreaterThanEqual:
				case QueryOperationEnum.LessThanEqual:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = true;
					dtpSecondValue.Visible = false;
					break;
				case QueryOperationEnum.Between:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = true;
					dtpSecondValue.Visible = true;
					break;
				default:
					lblDays.Visible = false;
					txtValue.Visible = false;
					dtpValue.Visible = false;
					dtpSecondValue.Visible = false;

					break;
			}

		}
		private void grdMaterialNutrientView_ShownEditor(Object sender, EventArgs e)
		{
			GridView view = sender as GridView;
			if (view.ActiveEditor is LookUpEdit)
			{
				LookUpEdit lookUp = ((LookUpEdit)view.ActiveEditor);

				if (!lookUp.IsPopupOpen)
					lookUp.ShowPopup();
			}
		}
		private void grdMaterialNutrientView_GotFocus(Object sender, EventArgs e)
		{
			grdMaterialNutrientView.ShowEditor();
		}

		//2/6/18
		private void ClearIncludeExclude()
		{
			rbMaterialInclude.Checked = true;
			rbNutrientInclude.Checked = true;
			rbOwnerInclude.Checked = true;
			rbOwnerLocInclude.Checked = true;
			rbSupplierInclude.Checked = true;
			rbSupplierLocInclude.Checked = true;
			rbIngredientInclude.Checked = true;

			//1/25/21
			rbSampleTypeInclude.Checked = true;
		}

		//6/22/22 - commented, not used?
		//private void DataBindOneTime()
		//{
		//	backgroundWorkerGetData.RunWorkerAsync();
		//}

		//6/14/22
		private void SetDateToolTips()
		{
			try
			{					
				ToolTip ttip = new ToolTip();
				if (this.rbResultsAnalysisDate.Checked == true)
				{
					ttip.SetToolTip(this.btnRemoveDate, "Remove Analysis Date from Query");
					ttip.SetToolTip(this.btnAddSampleDate, "Add Analysis Date to Query  ");
				}
				else
				{
					ttip.SetToolTip(this.btnRemoveDate, "Remove Sample Date from Query");
					ttip.SetToolTip(this.btnAddSampleDate, "Add Sample Date to Query  ");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//6/3/22
		private void rbResultsAnalysisDate_CheckedChanged(object sender, EventArgs e)
		{
			if (rbResultsAnalysisDate.Checked == true)
			{
				lblDate.Text = "Results Analysis Date";
			}
			else
			{
				lblDate.Text = "Sample Date";
			}

			//6/8/22 - need to see if date field (QueryFieldID) exists on querytemplatedetail -- and make sure it's pointing to the correct one			
			//		   1=Sample Date  12=Analysis Date
			List<QueryTemplateDetailInfo> template = new List<QueryTemplateDetailInfo>();
			template = AIMManager.GetQueryTemplateDetail((Int32)lueQueryTemplateInTab.EditValue);
			if (template.Count > 0)
			{
				foreach (var item in template)
				{
					if (item.QueryFieldID == ((Int32)QueryFeildEnum.SampleDate))
					{
						if (rbResultsAnalysisDate.Checked == true)
						{
							AIMManager.UpdateTemplateDetailQueryFieldID(item.QueryTemplateDetailID, (Int32)QueryFeildEnum.ResultsAnalysisDate, userGlobal.CFCLoginUser.CFCUserID);
							return;
						}
					}
					if (item.QueryFieldID == ((Int32)QueryFeildEnum.ResultsAnalysisDate))
					{
						if (rbSampleDate.Checked == true)
						{
							AIMManager.UpdateTemplateDetailQueryFieldID(item.QueryTemplateDetailID, (Int32)QueryFeildEnum.SampleDate, userGlobal.CFCLoginUser.CFCUserID);
							return;
						}
					}
				}
			}

			//6/14/22
			SetDateToolTips();

		}
		private void rbSampleDate_CheckedChanged(object sender, EventArgs e)
		{
			//6/14/22
			SetDateToolTips();
		}


		#region move cursor
		private void MoveCursorOnEnter(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				SelectNextControl(ActiveControl, true, true, true, true);
			}
		}

		private void cboReportName_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}

		private void lueQueryTemplateInTab_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}

		private void cboeAndOr_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}

		private void lueQueryField_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}

		private void lueQueryOperator_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}

		private void dtpValue_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}

		private void lueValue_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}

		private void txtValue_KeyDown(object sender, KeyEventArgs e)
		{
			MoveCursorOnEnter(sender, e);
		}
		#endregion

		private void listBoxField_DragOver(Object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
		}

		#endregion
		
		//-------------------------------------------------------------------------------
		#region Query Template functions
		private void DataBindSelectedQueryDetail()
		{
			Int32 fromAnalysisTemplateID = 0;
			fromAnalysisTemplateID = (Int32)lueQueryTemplateInTab.EditValue;

			if (CreatedInAnalysisCheck(false) == 0)
			{
				btnDeleteQueryTemplate.Visible = true;
			}

		}
		private void lueQueryTemplateInTab_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				tabControlQuery.Visible = false;

				lblCannotUpdate.Visible = false;
				lblCannotUpdate.SendToBack();
				btnDeleteQueryTemplate.Enabled = false;

				dtpValue.Text = "";
				dtpSecondValue.Text = "";
				txtDateOption.Text = "";

				_saveAs = false;
				if (lueQueryTemplateInTab.EditValue != null)
				{

					if ((Int32)lueQueryTemplateInTab.EditValue != 0)
					{
						tabControlQuery.Visible = true;
					}

					if ((Int32)lueQueryTemplateInTab.EditValue == 0)
					{
						btnSaveQueryTemplateAs.Enabled = false;
					}
					else
					{
						btnSaveQueryTemplateAs.Enabled = true;
					}

					if (CreatedInAnalysisCheck(false) == 0)
					{
						btnDeleteQueryTemplate.Enabled = true;
					}
					else
					{
						btnDeleteQueryTemplate.Enabled = false;
					}

				}

				QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
				if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
				{
					if (analysisTemplate.IsAnalysis == true)
					{
						_isAnalysis = true;
						btnDeleteQueryTemplate.Visible = true;
						lblCannotUpdate.Visible = true;
						lblCannotUpdate.BringToFront();
					}
					else
					{
						_isAnalysis = false;
						btnDeleteQueryTemplate.Visible = false;
					}

					showLastUpdate(analysisTemplate.UserName, analysisTemplate.UpdateStamp.ToString());

				}
				else
				{
					showLastUpdate("", "");
				}


				DataBindSelectedQueryDetail();
				DataBindTemplateLue();
				DataBindTemplateGrids();
				DataBindSampleDate();
				CheckYellowBox();

				//2/6/18
				ClearIncludeExclude();
				DataBindIncludeExcludeBoxes();

				//10/14/22
				DataBindNutrientDriverlue();
				
				//6/14/22
				SetDateToolTips();

				//-----------------------------------------------------------
				//10/31/22		
				if (_loadingForm == false)
				{

					tabControlQuery.TabPages.Remove(tpSupplier);
					tabControlQuery.TabPages.Remove(tpSupplierLocation);
					tabControlQuery.TabPages.Remove(tpIngredient);
					tabControlQuery.TabPages.Remove(tpDataSourceComparison);		
					if (analysisTemplate.IngredientOrProduct == "I")
					{
						tabControlQuery.TabPages.Add(tpSupplier);
						tabControlQuery.TabPages.Add(tpSupplierLocation);
						tabControlQuery.TabPages.Add(tpIngredient);
						tabControlQuery.TabPages.Add(tpDataSourceComparison);
					}					
				}
				_loadingForm = false;
				//-----------------------------------------------------------


				tabControlQuery.SelectedIndex = 0;


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		
		private void btnDeleteQueryTemplate_Click(object sender, EventArgs e)
		{
			try
			{

				if ((Int32)lueQueryTemplateInTab.EditValue != 0)
				{

					if (_isAnalysis == true)
					{
						MessageBox.Show("This template was created in Analysis and cannot be deleted here. ", "Warning...");
						return;
					}
					DialogResult dialogResult = MessageBox.Show("Delete Template?", "Warning...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (dialogResult != DialogResult.Yes)
					{
						return;
					}

					QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
					if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
					{

						AIMManager.DeleteQueryTemplate(analysisTemplate.QueryTemplateID);
						showLastUpdate("", "");
						DataBindLue();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnCancelQueryTemplateSave_Click(object sender, EventArgs e)
		{
			try
			{
				if (_saveAs == true)
				{
					pnlLastUpdated.Visible = true;
				}

				pnlQueryButtons.Visible = true;
				pnlNewQueryTemplate.Visible = false;
				txtNewQueryTemplateName.Text = String.Empty;
				_saveAs = false;

				lblReport.Visible = true;
				cboReportName.Visible = true;

				//2/6/18
				tabControlQuery.Visible = false;

				//10/24/17                                
				CheckYellowBox();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnNewQueryTemplate_Click_1(object sender, EventArgs e)
		{
			try
			{
				panelReports.Visible = false;
				pnlQueryButtons.Visible = false;
				pnlNewQueryTemplate.Visible = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void btnSaveQueryTemplateAs_Click(object sender, EventArgs e)
		{
			try
			{
				_saveAs = true;
				QueryTemplateInfo temp = new QueryTemplateInfo();
				temp = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();

				if (temp == null)
				{
					return;
				}

				if (temp.QueryTemplateID == 0)
				{
					MessageBox.Show("Please select the template first.", "Warning...", MessageBoxButtons.OK);
					lueQueryTemplateInTab.ShowPopup();
					return;
				}

				lblYellowBox.Visible = false;
				txtYellowBox.Visible = false;
				tabControlQuery.Visible = false;
				pnlQueryButtons.Visible = false;
				pnlNewQueryTemplate.Visible = true;
				lblReport.Visible = false;
				cboReportName.Visible = false;
				lblCannotUpdate.Visible = false;

				pnlNewQueryTemplate.Location = new Point(12, 78);
				pnlNewQueryTemplate.Size = new Size(609, 47);
				pnlNewQueryTemplate.BringToFront();
				txtNewQueryTemplateName.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnCancelQueryTemplateSave_Click_1(object sender, EventArgs e)
		{
			try
			{
				pnlNewQueryTemplate.Visible = false;
				panelReports.Visible = true;
				pnlQueryButtons.Visible = true;
				txtNewQueryTemplateName.Text = String.Empty;
				lblReport.Visible = true;
				cboReportName.Visible = true;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void btnSaveQueryTemplate_Click(object sender, EventArgs e)
		{

			QueryTemplateInfo queryTemplate = new QueryTemplateInfo();
			Int32 newAnalysisTemplateID = 0;

			try
			{
				if (txtNewQueryTemplateName.Text.Trim().Length == 0)
				{
					MessageBox.Show("Please provide a template name.", "Warning...", MessageBoxButtons.OK);
					return;
				}
				if (txtNewQueryTemplateName.Text.Trim().Length > 30)
				{
					MessageBox.Show("Template name is too long. It cannot be more than 30 characters.", "Warning...", MessageBoxButtons.OK);
					return;
				}

				QueryTemplateInfo templatename = new QueryTemplateInfo();
				templatename = AIMManager.GetQueryTemplateName(txtNewQueryTemplateName.Text);
				if (templatename != null && templatename.QueryTemplateID != 0)
				{
					MessageBox.Show("This template name already exists. Please enter another name.", "Warning...", MessageBoxButtons.OK);
					txtNewQueryTemplateName.Text = "";
					txtNewQueryTemplateName.Focus();
					return;
				}

				queryTemplate.QueryTemplateID = 0;
				queryTemplate.Name = txtNewQueryTemplateName.Text;
				queryTemplate.IsAnalysis = false;
				queryTemplate.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//6/22/22 - if datasourcecomparison is selected, set HideInAnalysis = 0 (datasourcecomparison uses sampleresult.analysis date)
				queryTemplate.HideInAnalysis = false;
				ErrorInfo countField = new ErrorInfo();
				countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceComparison);
				if (countField.RecordID > 0)
				{
					queryTemplate.HideInAnalysis = true;
				}

				//10/31/22
				queryTemplate.IngredientOrProduct = "I";
				if (rbReportTypeProduct.Checked == true)
				{
					queryTemplate.IngredientOrProduct = "P";
				}
							

				newAnalysisTemplateID = AIMManager.SaveQueryTemplate(ref queryTemplate).RecordID;
				if (newAnalysisTemplateID == 0)
				{
					//couldn't save the template
					pnlNewQueryTemplate.Visible = false;
					panelReports.Visible = true;
					pnlQueryButtons.Visible = true;
				}
				else
				{
					if (_saveAs == true)
					{
						AIMManager.CopyQueryTemplateDetail(newAnalysisTemplateID, (Int32)lueQueryTemplateInTab.EditValue, userGlobal.CFCLoginUser.CFCUserID);
					}
					_saveAs = false;
					DataBindLue();
					lueQueryTemplateInTab.EditValue = newAnalysisTemplateID;

					pnlNewQueryTemplate.Visible = false;
					pnlQueryButtons.Visible = true;

					//2/6/18
					tabControlQuery.Visible = true;

				}

				lblReport.Visible = true;
				cboReportName.Visible = true;

				_selectedQueryTempleteID = newAnalysisTemplateID;
				txtNewQueryTemplateName.Text = "";
				_isAnalysis = false;
				DataBindSelectedQueryDetail();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}


		}
		private void btnNewQueryTemplate_Click(object sender, EventArgs e)
		{
			try
			{
				DataBindLue();
				pnlNewQueryTemplate.Visible = true;
				pnlLastUpdated.Visible = false;

				dtpValue.Text = "";
				dtpSecondValue.Text = "";
				txtDateOption.Text = "";
				tabControlQuery.Visible = false;

				//5/26/22
				//pnlNewQueryTemplate.Location = new Point(11,65);
				pnlNewQueryTemplate.Location = new Point(1, 65);

				pnlNewQueryTemplate.Size = new Size(511, 88);

				pnlNewQueryTemplate.BringToFront();
				txtNewQueryTemplateName.Focus();

				//10/31/22
				rbReportTypeIngredient.Checked = true;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void DeleteQueryTemplateDetail(Int32 queryTemplateDetailID)
		{
			try
			{

				if (_isAnalysis == true && (Int32)lueQueryTemplateInTab.EditValue != 0)
				{
					MessageBox.Show("This template was created in Analysis and cannot be modified here. ", "Warning...");
					return;
				}
				AIMManager.DeleteQueryTemplateDetail(queryTemplateDetailID);
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
				if (_isAnalysis == true)
				{
					MessageBox.Show("This template was created in Analysis and cannot be modified here. ", "Warning...");
					return;
				}
				QueryTemplateDetailInfo queryDetail = new QueryTemplateDetailInfo();
				queryDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.DeleteQueryTemplateDetail(queryDetail.QueryTemplateDetailID);
				DataBindSelectedQueryDetail();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		//2/6/18
		private void updateQueryOperation(Int32 field, RadioButton rb, Int32 operationID, Boolean isAnd)
		{
			AIMManager.UpdateQueryOperationIDIsAnd((Int32)lueQueryTemplateInTab.EditValue, field, operationID, isAnd, userGlobal.CFCLoginUser.CFCUserID);
		}

		private Int32 CreatedInAnalysisCheck(Boolean showMsg)
		{
			Int32 rtn = 0;

			QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
			if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
			{
				if (analysisTemplate.IsAnalysis == true && (Int32)lueQueryTemplateInTab.EditValue != 0)
				{
					if (showMsg == true)
					{
						MessageBox.Show("This template was created in Analysis and cannot be modified here. ", "Warning...");
					}
					rtn = -1;
				}
			}
			return rtn;
		}
		//6/22/22 commented, not used?
		//private void btnDeleteQueryTemplate_Click_1(object sender, EventArgs e)
		//{
		//	try
		//	{
		//		QueryTemplateInfo analysisTemplate = (QueryTemplateInfo)lueQueryTemplateInTab.GetSelectedDataRow();
		//		if (analysisTemplate != null && analysisTemplate.QueryTemplateID > 0)
		//		{
		//			if (analysisTemplate.IsAnalysis == false)
		//			{
		//				AIMManager.DeleteQueryTemplate(analysisTemplate.QueryTemplateID);
		//				showLastUpdate("", "");

		//			}
		//			else
		//			{
		//				MessageBox.Show("This template was created in Analysis and cannot be deleted here. ", "Warning...");
		//				return;
		//			}
		//			DataBindLue();
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message);
		//	}
		//}

		//6/22/22 commented, not used?
		//private void grdQueryView_CustomRowCellEdit_1(object sender, CustomRowCellEditEventArgs e)
		//{
		//	try
		//	{
		//		GridView gv = sender as GridView;

		//		QueryFeildEnum fieldName;
		//		Enum.TryParse(gv.GetRowCellValue(e.RowHandle, gv.Columns["QueryFieldID"]).ToString().Trim(), out fieldName);

		//		QueryOperationEnum operatorName;
		//		Enum.TryParse(gv.GetRowCellValue(e.RowHandle, gv.Columns["QueryOperationID"]).ToString().Trim(), out operatorName);

		//		if (e.Column.FieldName == "QueryOperationID")
		//		{
		//			if (gv.GetRowCellValue(e.RowHandle, gv.Columns["QueryOperationID"]) != null)
		//			{
		//				List<QueryOperationInfo> queryOperation = new List<QueryOperationInfo>();
		//				queryOperation = AIMManager.GetQueryOperation(fieldName);
		//				queryOperation.Insert
		//				(
		//					0,
		//					new QueryOperationInfo
		//					{
		//						QueryOperationID = 0,
		//						Operation = "--Select--"
		//					}
		//				);						
		//			}
		//		}						
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show("we are inside cell edit! " + ex.Message + "Data: " + ex.Data);
		//	}
		//}				
		#endregion

		//-------------------------------------------------------------------------------
		#region Add/Delete buttons

		private Int32 ChecksBeforeAdding(String type, DevExpress.XtraEditors.LookUpEdit lue)
		{
			Int32 rtn = 0;

			if (CreatedInAnalysisCheck(true) != 0)
			{
				rtn = -1;
			}
			else
			{
				if ((Int32)lue.EditValue == 0)
				{
					MessageBox.Show("Please select AIM " + type + ".", "Warning...");
					if (type == "Supplier Location" || type == "Owner Location") { }
					else
					{
						lue.ShowPopup();
					}
					rtn = -1;
				}

				//Check to see if item has already been selected
				switch (type)
				{
					case "Material":
						List<AIMMaterialInfo> material = new List<AIMMaterialInfo>();
						material = AIMManager.GetAIMMaterialForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material, (Int32)lueMaterialCode.EditValue);
						if (material.Count != 0)
						{
							MessageBox.Show("This material has been selected.", "Information...");
							rtn = -1;
						}
						break;

					case "Nutrient":
						List<AIMNutrientInfo> nutrient = new List<AIMNutrientInfo>();
						nutrient = AIMManager.GetAIMNutrientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.NutrientCode, (Int32)lueNutrientCode.EditValue);
						if (nutrient.Count != 0)
						{
							MessageBox.Show("This nutrient has been selected.", "Information...");
							rtn = -1;
						}
						break;
					case "Owner":
						List<AIMOwnerInfo> owner = new List<AIMOwnerInfo>();
						owner = AIMManager.GetAIMOwnerForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Owner, (Int32)lueOwnerCode.EditValue);
						if (owner.Count != 0)
						{
							MessageBox.Show("This owner has been selected.", "Information...");
							rtn = -1;
						}
						break;
					case "Owner Location":
						List<AIMOwnerLocationInfo> ownerLocation = new List<AIMOwnerLocationInfo>();
						ownerLocation = AIMManager.GetAIMOwnerLocationForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.OwnerLocation, (Int32)lueOwnerLocationCode.EditValue);
						if (ownerLocation.Count != 0)
						{
							MessageBox.Show("This owner location has been selected.", "Information...");
							rtn = -1;
						}
						break;
					case "Supplier":
						List<AIMSupplierInfo> supplier = new List<AIMSupplierInfo>();
						supplier = AIMManager.GetAIMSupplierForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Supplier, (Int32)lueSupplierCode.EditValue);
						if (supplier.Count != 0)
						{
							MessageBox.Show("This supplier has been selected.", "Information...");
							rtn = -1;
						}
						break;
					case "Supplier Location":
						List<AIMSupplierLocationInfo> supplierLocation = new List<AIMSupplierLocationInfo>();
						supplierLocation = AIMManager.GetAIMSupplierLocationForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SupplierLocation, (Int32)lueSupplierLocationCode.EditValue);
						if (supplierLocation.Count != 0)
						{
							MessageBox.Show("This supplier location has been selected.", "Information...");
							rtn = -1;
						}
						break;

					case "Ingredient":
						List<CFCIngredientInfo> ingredient = new List<CFCIngredientInfo>();
						ingredient = AIMManager.GetCFCIngredientForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.IngredientCode, (Int32)lueIngredientCode.EditValue);
						if (ingredient.Count != 0)
						{
							MessageBox.Show("This ingredient has been selected.", "Information...");
							rtn = -1;
						}
						break;

					//1/25/21
					case "SampleType":
						List<SampleTypeInfo> sampleType = new List<SampleTypeInfo>();
						sampleType = AIMManager.GetSampleTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleType, (Int32)lueSampleTypeName.EditValue);
						if (sampleType.Count != 0)
						{
							MessageBox.Show("This sample type has been selected.", "Information...");
							rtn = -1;
						}
						break;

					//5/26/22
					case "DataSourceType":
						List<DataSourceTypeInfo> datasource = new List<DataSourceTypeInfo>();
						datasource = AIMManager.GetDataSourceTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceType, (Int32)lueDataSourceType.EditValue);
						if (datasource.Count != 0)
						{
							MessageBox.Show("This datasource has already been selected.", "Information...");
							rtn = -1;
						}
						break;

					default:
						break;
				}
			}

			return rtn;
		}


		private void btnAddMaterial_Click(Object sender, EventArgs e)
		{
			try
			{

				if (rbSingleMaterial.Checked == true)                                               //10/9/23
				{                                                                                   //10/9/23
					if (ChecksBeforeAdding("Material", lueMaterialCode) != 0)
					{
						return;
					}
					QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
					queryTemplateDetail.QueryTemplateDetailID = 0;
					queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
					queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Material;
					queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
					queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
					if (rbMaterialExclude.Checked == true)
					{
						queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
						queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
					}
					queryTemplateDetail.Value = (Int32)lueMaterialCode.EditValue;
					queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
				}                                                                                   //10/9/23

				//-------------------------------------------------------------
				else if (rbRangeMaterial.Checked == true)
				{
					if ((Int32)lueMaterialBeg.EditValue == 0)
					{
						MessageBox.Show("Please select Range Begin value.", "Warning...");
						lueMaterialBeg.ShowPopup();
						return;
					}
					else if ((Int32)lueMaterialEnd.EditValue == 0)
					{
						MessageBox.Show("Please select Range End value.", "Warning...");
						lueMaterialEnd.ShowPopup();
						return;
					}

					//Get all materials in the range
					List<AIMMaterialInfo> range = new List<AIMMaterialInfo>();
					List<AIMMaterialInfo> material = new List<AIMMaterialInfo>();
					range = AIMManager.GetAIMMaterialAIMMaterialCodeRange(lueMaterialBeg.Text, lueMaterialEnd.Text);

					//Send each material to this stp (checks to see if material is already added)
					if (range != null)
					{
						foreach (var item in range)
						{
							material = AIMManager.GetAIMMaterialForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.Material, item.AIMMaterialID);
							if (material.Count == 0)
							{
								AnalysisSharedFunctions.SaveQueryTemplateDetailValue((Int32)QueryFeildEnum.Material, lueQueryTemplateInTab, rbMaterialExclude, item.AIMMaterialID, userGlobal.CFCLoginUser.CFCUserID);
							}

						}
					}
				}


				DataBindTemplateGrids();

				lueMaterialCode.EditValue = 0;
				lueMaterialName.EditValue = 0;
				lueMaterialBeg.EditValue = 0;                   //10/9/23
				lueMaterialEnd.EditValue = 0;                   //10/9/23


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteMaterial_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{

				AIMMaterialInfo aimMaterial = new AIMMaterialInfo();
				aimMaterial = (AIMMaterialInfo)grdMaterialView.GetFocusedRow();
				if (aimMaterial != null)
				{
					DeleteQueryTemplateDetail(aimMaterial.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

                //2/6/18
                if (((List<AIMMaterialInfo>)grdMaterial.DataSource).Count == 0)
                {
                    rbMaterialInclude.Checked = true;
                }

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddOwner_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Owner", lueOwnerCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Owner;

                //3/6/18
				//queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;

                //2/6/18
                if (rbOwnerExclude.Checked == true)
                {

                    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND

                    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                }

                queryTemplateDetail.Value = (Int32)lueOwnerCode.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueOwnerCode.EditValue = 0;
				lueOwnerName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteOwner_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{

				AIMOwnerInfo aimOwner = new AIMOwnerInfo();
				aimOwner = (AIMOwnerInfo)grdOwnerView.GetFocusedRow();
				if (aimOwner != null)
				{
					DeleteQueryTemplateDetail(aimOwner.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

                //2/6/18
                if (((List<AIMOwnerInfo>)grdOwner.DataSource).Count == 0)
                {
                    rbOwnerInclude.Checked = true;
                }

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddOwnerLocation_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Owner Location", lueOwnerLocationCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.OwnerLocation;

                //3/6/18
				//queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;

                //2/6/18
                if (rbOwnerLocExclude.Checked == true)
                {
                    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND

                    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                }

                queryTemplateDetail.Value = (Int32)lueOwnerLocationCode.EditValue;
				queryTemplateDetail.SecondValue = (Int32)lueOwnerCode_Loc.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				//7/27/22 - commented
				//lueOwnerCode_Loc.EditValue = 0;
				//lueOwnerName_Loc.EditValue = 0;

				lueOwnerLocationCode.EditValue = 0;
				lueOwnerLocationName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void repbtnDeleteOwnerLocation_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{

				AIMOwnerLocationInfo aimOwnerLocation = new AIMOwnerLocationInfo();
				aimOwnerLocation = (AIMOwnerLocationInfo)grdOwnerLocationView.GetFocusedRow();
				if (aimOwnerLocation != null)
				{
					DeleteQueryTemplateDetail(aimOwnerLocation.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

                //2/6/18
                if (((List<AIMOwnerLocationInfo>)grdOwnerLocation.DataSource).Count == 0)
                {
                    rbOwnerLocInclude.Checked = true;
                }

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddSupplier_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Supplier", lueSupplierName) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.Supplier;

                //3/6/18
				//queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;

                //2/6/18
                if (rbSupplierExclude.Checked == true)
                {
                    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND

                    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                }

                queryTemplateDetail.Value = (Int32)lueSupplierName.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueSupplierName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteSupplier_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{

				AIMSupplierInfo aimSupplier = new AIMSupplierInfo();
				aimSupplier = (AIMSupplierInfo)grdSupplierView.GetFocusedRow();
				if (aimSupplier != null)
				{
					DeleteQueryTemplateDetail(aimSupplier.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

                //2/6/18
                if (((List<AIMSupplierInfo>)grdSupplier.DataSource).Count == 0)
                {
                    rbSupplierInclude.Checked = true;
                }
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddSupplierLocation_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Supplier Location", lueSupplierLocationName) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SupplierLocation;

                //3/6/18
				//queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;

                //2/6/18
                if (rbSupplierLocExclude.Checked == true)
                {
                    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND

                    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                }
                
                queryTemplateDetail.Value = (Int32)lueSupplierLocationCode.EditValue;
				queryTemplateDetail.SecondValue = (Int32)lueSupplierCode_Loc.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueSupplierCode_Loc.EditValue = 0;
				lueSupplierName_Loc.EditValue = 0;
				lueSupplierLocationCode.EditValue = 0;
				lueSupplierLocationName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteSupplierLocation_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				AIMSupplierLocationInfo aimSupplierLocation = new AIMSupplierLocationInfo();
				aimSupplierLocation = (AIMSupplierLocationInfo)grdSupplierLocationView.GetFocusedRow();
				if (aimSupplierLocation != null)
				{
					DeleteQueryTemplateDetail(aimSupplierLocation.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

                //2/6/18
                if (((List<AIMSupplierLocationInfo>)grdSupplierLocation.DataSource).Count == 0)
                {
                    rbSupplierLocInclude.Checked = true;
                }
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddNutrient_Click(Object sender, EventArgs e)
		{
			try
			{
				if (ChecksBeforeAdding("Nutrient", lueNutrientCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.NutrientCode;                

                //3/6/18
				//queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND               
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND               

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;

                //2/6/18
                if (rbNutrientExclude.Checked == true)
                {
                    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND

                    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                }

                queryTemplateDetail.Value = (Int32)lueNutrientCode.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueNutrientCode.EditValue = 0;
				lueNutrientName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteNutrient_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{

				AIMNutrientInfo aimNutrient = new AIMNutrientInfo();
				aimNutrient = (AIMNutrientInfo)grdNutrientView.GetFocusedRow();
				if (aimNutrient != null)
				{
					DeleteQueryTemplateDetail(aimNutrient.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

                //2/6/18
                if (((List<AIMNutrientInfo>)grdNutrient.DataSource).Count == 0)
                {
                    rbNutrientInclude.Checked = true;
                }
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddMaterialMulti_Click(Object sender, EventArgs e)
		{
			try
			{
				if ((Int32)lueMaterialNutCode.EditValue == 0)
				{
					MessageBox.Show("Please select AIM Material.", "Warning...");
					lueMaterialNutCode.ShowPopup();
					return;
				}

				if (ChecksBeforeAdding("MaterialNutrient", lueMaterialNutCode) != 0)
				{
					return;
				}


				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.MaterialNutrient;
				queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
                queryTemplateDetail.Value = (Int32)lueMaterialNutCode.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
				DataBindTemplateGrids();

				//set row and column, and then ShowEditor opens the Nutrient dropdown
				Int32 row = grdMaterialNutrientView.RowCount - 1;
				grdMaterialNutrientView.FocusedRowHandle = row;
				grdMaterialNutrientView.FocusedColumn = grdMaterialNutrientView.VisibleColumns[2];
				grdMaterialNutrientView.ShowEditor();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteMultiple_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				TemplateMaterialNutrientInfo materialNutrient = new TemplateMaterialNutrientInfo();
				materialNutrient = (TemplateMaterialNutrientInfo)grdMaterialNutrientView.GetFocusedRow();
				if (materialNutrient != null)
				{
					DeleteQueryTemplateDetail(materialNutrient.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnAddIngredient_Click(Object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("Ingredient", lueIngredientCode) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.IngredientCode;

                //3/6/18
				//queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND                
                queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                

                queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;

                //2/6/18
                if (rbIngredientExclude.Checked == true)
                {
                    //3/6/18 - If QueryOperationID = 9 (exclude), IsAnd s/b 1; If QueryOperationID = 4 (include), IsAnd s/b 0
                    //queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND
                    queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND

                    queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
                }

                queryTemplateDetail.Value = (Int32)lueIngredientCode.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				DataBindTemplateGrids();

				lueIngredientCode.EditValue = 0;
				lueIngredientName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteIngredient_ButtonClick(Object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				CFCIngredientInfo cfcIngredient = new CFCIngredientInfo();
				cfcIngredient = (CFCIngredientInfo)grdIngredientView.GetFocusedRow();
				if (cfcIngredient != null)
				{
					DeleteQueryTemplateDetail(cfcIngredient.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

                //2/6/18
                if (((List<CFCIngredientInfo>)grdIngredient.DataSource).Count == 0)
                {
                    rbIngredientInclude.Checked = true;
                }

            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//1/25/21
		private void btnAddSampleType_Click(object sender, EventArgs e)
		{
			try
			{

				if (ChecksBeforeAdding("SampleType", lueSampleTypeName) != 0)
				{
					return;
				}

				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SampleType;
				queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
				queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				if (rbSampleTypeExclude.Checked == true)
				{
					queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
					queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
				}

				queryTemplateDetail.Value = (Int32)lueSampleTypeName.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
				DataBindTemplateGrids();
				lueSampleTypeName.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		
		private void repbtnDeleteSampleType_Click(object sender, EventArgs e)
		{
			try
			{
				SampleTypeInfo sampleType = new SampleTypeInfo();
				sampleType = (SampleTypeInfo)grdSampleTypeView.GetFocusedRow();
				if (sampleType != null)
				{
					DeleteQueryTemplateDetail(sampleType.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
				if (((List<SampleTypeInfo>)grdSampleType.DataSource).Count == 0)
				{
					rbSampleTypeInclude.Checked = true;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion				
		
		//-------------------------------------------------------------------------------
		#region Date functions
		private void btnSaveDateForQuery_Click(Object sender, EventArgs e)
		{
			QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
			Int32 days = 0;
			QueryOperationEnum queryOperation;
			Int32 operation = 0;

			if (CreatedInAnalysisCheck(true) != 0)
			{
				return;
			}
			if ((Int32)lueOperator.EditValue == 0)
			{
				MessageBox.Show("Please select a Date Option.", "Message...", MessageBoxButtons.OK);
				lueOperator.Focus();
				lueOperator.ShowPopup();
				return;
			}
			if (dtpValue.Visible == true && dtpValue.Text.Length == 0)
			{
				MessageBox.Show("Please select a Date.", "Message...", MessageBoxButtons.OK);
				dtpValue.Focus();
				return;
			}
			if (txtValue.Visible == true)
			{
				Int32.TryParse(txtValue.Text, out days);
				if (days == 0)
				{
					MessageBox.Show("Please enter Number of Days (number must be greater than zero).", "Warning...");
					txtValue.Focus();
					txtValue.SelectAll();
					return;
				}
				else if (days < 0)
				{
					MessageBox.Show("Number of Days must be greater than zero.", "Warning...");
					txtValue.Focus();
					txtValue.SelectAll();
					return;
				}
			}

			//--------------------------------------------------------------------------------------
			//If SampleDate or AnalysisDate option exists for this query, delete it before adding a new SampleDate/AnalysisDate
			AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);

			//6/8/22
			//AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.ResultsAnalysisDate);
			//if (lblDate.Text == "Results Analysis Date")
			if (rbResultsAnalysisDate.Checked == true)
			{
				AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.ResultsAnalysisDate);
			}
			else
			{
				AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
			}


			//--------------------------------------------------------------------------------------
			if (dtpValue.Visible == true)
			{
				queryTemplateDetail.DateValue = dtpValue.Value;

				//9/8/17
				if (dtpSecondValue.Visible == true)
				{
					queryTemplateDetail.SecondDate = dtpSecondValue.Value;
				}
			}
			else if (txtValue.Visible == true)
			{
				queryTemplateDetail.Value = days;
			}

			queryTemplateDetail.QueryTemplateDetailID = 0;
			queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;

			queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.SampleDate;

			//6/3/22
			if (rbResultsAnalysisDate.Checked == true)
			{
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.ResultsAnalysisDate;
			}



			Int32.TryParse(lueOperator.EditValue.ToString(), out operation);
			queryOperation = (QueryOperationEnum)operation;
			queryTemplateDetail.QueryOperationID = (Int32)queryOperation;
			queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
			AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

			DataBindSampleDate();
			SetDateTextField(queryOperation, days);

			
			lueOperator.EditValue = 0;
			txtValue.Text = "";
		}
		private void SetDateTextField(QueryOperationEnum queryOperation, Int32 days)
		{
			switch (queryOperation)
			{
				case QueryOperationEnum.PastNDaysToDate:
					txtDateOption.Text = days + " Days Past Current Date";
					break;
				case QueryOperationEnum.MonthToDate:
					txtDateOption.Text = "Month-To-Date";
					break;
				case QueryOperationEnum.YearToDate:
					txtDateOption.Text = "Year-To-Date";
					break;
				case QueryOperationEnum.Equal:
					txtDateOption.Text = "Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");					
					break;
				case QueryOperationEnum.GreaterThan:
					txtDateOption.Text = "Greater Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.LessThan:
					txtDateOption.Text = "Less Than " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.GreaterThanEqual:
					txtDateOption.Text = "Greater Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.LessThanEqual:
					txtDateOption.Text = "Less Than or Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.NotEqual:
					txtDateOption.Text = "Not Equal To " + dtpValue.Value.Date.ToString("MM/dd/yyyy");
					break;
				case QueryOperationEnum.Between:
					txtDateOption.Text = queryOperation.ToString() + " " + dtpValue.Value.Date.ToString("MM/dd/yyyy") + " and " + dtpSecondValue.Value.Date.ToString("MM/dd/yyyy");
					break;

				default:
					txtDateOption.Text = queryOperation.ToString();

					break;
			}
		}
		private void btnRemoveDate_Click(Object sender, EventArgs e)
		{
			//6/13/22
			//AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.SampleDate);
			Int32 queryField;
			if (rbSampleDate.Checked == true)
			{
				queryField = (Int32)QueryFeildEnum.SampleDate;
			}
			else
			{
				queryField = (Int32)QueryFeildEnum.ResultsAnalysisDate;
			}
			AIMManager.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID((Int32)lueQueryTemplateInTab.EditValue, queryField);
			//-----------------------------------------------------

			DataBindSampleDate();
			txtDateOption.Text = "";
		}
		#endregion

		//-------------------------------------------------------------------------------
		#region Include-Exclude Boxes
		private void rbMaterialInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbMaterialExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.Material, rbMaterialExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        private void rbNutrientInclude_Click(Object sender, EventArgs e)
        {           
            updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbNutrientExclude_Click(Object sender, EventArgs e)
        {         
            updateQueryOperation((Int32)QueryFeildEnum.NutrientCode, rbNutrientExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        private void rbOwnerInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbOwnerExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.Owner, rbOwnerExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        private void rbOwnerLocInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbOwnerLocExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.OwnerLocation, rbOwnerLocExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        private void rbSupplierInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbSupplierExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.Supplier, rbSupplierExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

        private void rbSupplierLocInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbSupplierLocExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.SupplierLocation, rbSupplierLocExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }
               
        private void rbIngredientInclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 4 (include), IsAnd s/b 0
            //updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientInclude, (Int32)QueryOperationEnum.Equal, true);
            updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientInclude, (Int32)QueryOperationEnum.Equal, false);
        }
        private void rbIngredientExclude_Click(Object sender, EventArgs e)
        {
            //3/6/18 - QueryOperationID = 9 (exclude), IsAnd s/b 1
            //updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientExclude, (Int32)QueryOperationEnum.NotEqual, false);
            updateQueryOperation((Int32)QueryFeildEnum.IngredientCode, rbIngredientExclude, (Int32)QueryOperationEnum.NotEqual, true);
        }

		//1/25/21
		private void rbSampleTypeInclude_CheckedChanged(object sender, EventArgs e)
		{
			//QueryOperationID = 4 (include), IsAnd s/b 0			
			updateQueryOperation((Int32)QueryFeildEnum.SampleType, rbSampleTypeInclude, (Int32)QueryOperationEnum.Equal, false);
		}
		private void rbSampleTypeExclude_CheckedChanged(object sender, EventArgs e)
		{
			//QueryOperationID = 9 (exclude), IsAnd s/b 1			
			updateQueryOperation((Int32)QueryFeildEnum.SampleType, rbSampleTypeExclude, (Int32)QueryOperationEnum.NotEqual, true);
		}
		#endregion
			
		//-------------------------------------------------------------------------------
		//5/26/22		
		#region DataSource functions - currently not used
		private void btnAddDataSource_Click(object sender, EventArgs e)
		{
			try
			{
				Int32 orderby = 0;
				Int32 saveOrderBy = 0;

				//--------------------------------------------------------------
				//check datasource limit
				ErrorInfo countField = new ErrorInfo();
				countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceType);			
				if (_dataSourceTypeLimit > 0)
				{					
					if (countField.RecordID == _dataSourceTypeLimit)
					{
						MessageBox.Show("This report does not allow more than " + _dataSourceTypeLimit + " data sources. ", "Warning...", MessageBoxButtons.OK);						
						return;
					}					
				}

				//--------------------------------------------------------------
				//loop thru grid to get next OrderBy number				
				List<DataSourceTypeInfo> dst = new List<DataSourceTypeInfo>();
				dst = (List<DataSourceTypeInfo>)grdDataSourceType.DataSource;
				if (dst != null)
				{
					if (dst.Count == 0)
					{
						orderby = 1;
					}
					else
					{
						foreach (var item in dst)
						{
							saveOrderBy = item.OrderBy;
						}
						orderby = saveOrderBy + 1;
					}					
				}

				//--------------------------------------------------------------
				if (ChecksBeforeAdding("DataSourceType", lueDataSourceType) != 0)
				{
					return;
				}

				//--------------------------------------------------------------
				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.DataSourceType;
				queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
				queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				if (rbDataSourceTypeExclude.Checked == true)
				{
					queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
					queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
				}															
				queryTemplateDetail.Value = (Int32)lueDataSourceType.EditValue;				
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				queryTemplateDetail.OrderBy = orderby;
				AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);
				DataBindTemplateGrids();
				lueDataSourceType.EditValue = 0;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteDataSourceType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			try
			{
				DataSourceTypeInfo datasourceType = new DataSourceTypeInfo();
				datasourceType = (DataSourceTypeInfo)grdDataSourceTypeView.GetFocusedRow();
				if (datasourceType != null)
				{
					DeleteQueryTemplateDetail(datasourceType.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();
				if (((List<DataSourceTypeInfo>)grdDataSourceType.DataSource).Count == 0)
				{
					rbDataSourceTypeInclude.Checked = true;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void grdDataSourceTypeView_ShownEditor(object sender, EventArgs e)
		{
			GridView view = sender as GridView;
			if (view.ActiveEditor is LookUpEdit)
			{
				LookUpEdit lookUp = ((LookUpEdit)view.ActiveEditor);

				if (!lookUp.IsPopupOpen)
					lookUp.ShowPopup();
			}
		}
		private void grdDataSourceTypeView_GotFocus(object sender, EventArgs e)
		{
			grdDataSourceTypeView.ShowEditor();
		}
		private void grdDataSourceTypeView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{							
				DataSourceTypeInfo datasource = new DataSourceTypeInfo();
				datasource = (DataSourceTypeInfo)grdDataSourceTypeView.GetFocusedRow();			
				AIMManager.UpdateQueryTemplateDetailOrderBy(datasource.QueryTemplateDetailID, datasource.OrderBy, userGlobal.CFCLoginUser.CFCUserID);				
				grdDataSourceType.DataSource = AIMManager.GetDataSourceTypeForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceType);				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion
				
		//-------------------------------------------------------------------------------
		//6/14/22
		#region DataSourceComparison functions							
		private void btnAddDataSourceComparison_Click(object sender, EventArgs e)
		{
			try
			{
				
				QueryTemplateDetailInfo queryTemplateDetail = new QueryTemplateDetailInfo();
				QueryTemplateDetailNutrientInfo nutrient = new QueryTemplateDetailNutrientInfo();
				ErrorInfo result = new ErrorInfo();
				Int32 nutrientID1 = 0;
				Int32 nutrientID2 = 0;


				//--------------------------------------------------------------
				//check nutrient limit - this report can show up to 3 datasource/nutrient comparisons
				ErrorInfo countField = new ErrorInfo();
				countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceComparison);				
				
				if (countField.RecordID == 3)
				{
					MessageBox.Show("This report does not allow more than 3 DataSource/Nutrient comparisons. ", "Warning...", MessageBoxButtons.OK);
					return;
				}			

				nutrientID1 = (Int32)lueNutrientCodeCompare1.EditValue;
				nutrientID2 = (Int32)lueNutrientCodeCompare2.EditValue;

				if ((Int32)lueDataSourceCompare1.EditValue == 0)
				{
					MessageBox.Show("Please select the first Data Source to compare. ", "Warning...", MessageBoxButtons.OK);
					lueDataSourceCompare1.ShowPopup();
					return;
				}
				else if ((Int32)lueNutrientCodeCompare1.EditValue == 0)
				{
					MessageBox.Show("Please select a Nutrient for the first data source to compare. ", "Warning...", MessageBoxButtons.OK);
					lueNutrientCodeCompare1.ShowPopup();
					return;
				}
				else if (gbComparison2.Visible == false)
				{
					MessageBox.Show("Please select the first Data Source/Nutrient to compare. ", "Warning...", MessageBoxButtons.OK);
					lueDataSourceCompare1.ShowPopup();
					return;
				}				
				else if ((Int32)lueDataSourceCompare2.EditValue == 0)
				{
					MessageBox.Show("Please select a second Data Source/Nutrient to compare. ", "Warning...", MessageBoxButtons.OK);
					lueDataSourceCompare2.ShowPopup();
					return;
				}
				else if ((Int32)lueDataSourceCompare1.EditValue == (Int32)lueDataSourceCompare2.EditValue)
				{
					MessageBox.Show("Both data sources are the same. Please change one of the selections.", "Warning...", MessageBoxButtons.OK);
					return;
				}
				else if ((Int32)lueNutrientCodeCompare2.EditValue == 0)
				{
					MessageBox.Show("Please select a Nutrient for the second data source to compare. ", "Warning...", MessageBoxButtons.OK);
					lueNutrientCodeCompare2.ShowPopup();
					return;
				}				


				//--------------------------------------------------------------
				//Save Datasource1 selections to QueryTemplateDetail table				
				queryTemplateDetail.QueryTemplateDetailID = 0;
				queryTemplateDetail.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				queryTemplateDetail.QueryFieldID = (Int32)QueryFeildEnum.DataSourceComparison;
				queryTemplateDetail.IsAnd = 0; // 0 = OR, 1 = AND                
				queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.Equal;
				if (rbDataSourceTypeExclude.Checked == true)
				{
					queryTemplateDetail.IsAnd = 1; // 0 = OR, 1 = AND
					queryTemplateDetail.QueryOperationID = (Int32)QueryOperationEnum.NotEqual;
				}
				queryTemplateDetail.Value = (Int32)lueDataSourceCompare1.EditValue;	
				queryTemplateDetail.SecondValue = (Int32)lueDataSourceCompare2.EditValue;
				queryTemplateDetail.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;			
				result = AIMManager.SaveQueryTemplateDetail(ref queryTemplateDetail);

				//Save Nutrient selections for datasource1 to QueryTemplateDetailNutrient table								
				AIMManager.SaveQueryTemplateDetailNutrientDataSource(0, result.RecordID, (Int32)lueDataSourceCompare1.EditValue, nutrientID1, userGlobal.CFCLoginUser.CFCUserID);
				AIMManager.SaveQueryTemplateDetailNutrientDataSource(0, result.RecordID, (Int32)lueDataSourceCompare2.EditValue, nutrientID2, userGlobal.CFCLoginUser.CFCUserID);

				//Update QueryTemplate table -- set HideInAnalysis = true, since a datasourcecomparison was added to this template			
				QueryTemplateInfo query = new QueryTemplateInfo();
				query.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				query.Name = lueQueryTemplateInTab.Text;
				query.IsAnalysis = false;
				query.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
				query.HideInAnalysis = true;

				//10/31/22
				query.IngredientOrProduct = "I";


				AIMManager.SaveQueryTemplate(ref query);
								
				//---------------------------------------------------------------------

				//10/14/22
				//Add nutrient1 to driver-dropdown
				AIMNutrientDriverInfo driver = new AIMNutrientDriverInfo();
				ErrorInfo error = new ErrorInfo();
				driver.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				driver.DataSourceTypeID = (Int32)lueDataSourceCompare1.EditValue;
				driver.AIMNutrientID = nutrientID1;
				driver.AIMNutrientCode = lueNutrientCodeCompare1.Text;
				driver.AIMNutrientName = lueNutrientNameCompare1.Text;				
				error = AIMManager.SaveAIMNutrientDriver(ref driver);

				//10/14/22
				//Add nutrient2 to driver-dropdown	
				driver.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
				driver.DataSourceTypeID = (Int32)lueDataSourceCompare2.EditValue;
				driver.AIMNutrientID = nutrientID2;
				driver.AIMNutrientCode = lueNutrientCodeCompare2.Text;
				driver.AIMNutrientName = lueNutrientNameCompare2.Text;				
				error = AIMManager.SaveAIMNutrientDriver(ref driver);		

				//---------------------------------------------------------------------

				DataBindTemplateGrids();

				//10/14/22
				DataBindNutrientDriverlue();

				checkGridNutrientCount();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		
		private void grdDataSourceComparisonView_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			try
			{
				//10/14/22 - commented, was not getting used
				//Int32 DST1 = 0;				
				//Int32 DST2 = 0;				
				//Int32 Nut1 = 0;
				//Int32 Nut2 = 0;

				Int32 row = 0;
				row = ((DevExpress.XtraGrid.Views.Base.ColumnView)sender).FocusedRowHandle;

				DataSourceComparisonInfo nutRef = new DataSourceComparisonInfo();
				nutRef = (DataSourceComparisonInfo)grdDataSourceComparisonView.GetFocusedRow();

				//nutRef.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;

				//if (row == 0)
				//{
				//	DST1 = nutRef.DataSourceTypeID1;					
				//	DST2 = nutRef.DataSourceTypeID2;
				//	Nut1 = nutRef.AIMNutrientID1;
				//	Nut2 = nutRef.AIMNutrientID2;

				//}
				//else if (row == 1)
				//{
				//	DST1 = nutRef.DataSourceTypeID1;					
				//	DST2 = nutRef.DataSourceTypeID2;
				//	Nut1 = nutRef.AIMNutrientID1;
				//	Nut2 = nutRef.AIMNutrientID2;
				//}
				//else if (row == 2)
				//{
				//	DST1 = nutRef.DataSourceTypeID1;					
				//	DST2 = nutRef.DataSourceTypeID2;
				//	Nut1 = nutRef.AIMNutrientID1;
				//	Nut2 = nutRef.AIMNutrientID2;
				//}

				
				//AIMManager.SaveQueryTemplateDetailNutrientDataSource(0, nutRef.QueryTemplateDetailID, DST1, Nut1, userGlobal.CFCLoginUser.CFCUserID);
				//AIMManager.SaveQueryTemplateDetailNutrientDataSource(0, nutRef.QueryTemplateDetailID, DST2, Nut2, userGlobal.CFCLoginUser.CFCUserID);
				//grdDataSourceComparison.DataSource = AIMManager.GetDataSourceComparisonForTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceComparison);

				


			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void repbtnDeleteDataSourceComparison_Click(object sender, EventArgs e)
		{
			try
			{
				DataSourceComparisonInfo datasource = new DataSourceComparisonInfo();
				datasource = (DataSourceComparisonInfo)grdDataSourceComparisonView.GetFocusedRow();
				if (datasource != null)
				{

					//10/14/22
					//delete both datasource nutrients from driver dropdown					
					AIMManager.DeleteAIMNutrientDriver((Int32)lueQueryTemplateInTab.EditValue, datasource.DS1_NutID);
					AIMManager.DeleteAIMNutrientDriver((Int32)lueQueryTemplateInTab.EditValue, datasource.DS2_NutID);


					DeleteQueryTemplateDetail(datasource.QueryTemplateDetailID);
				}
				DataBindTemplateGrids();

				//10/14/22
				DataBindNutrientDriverlue();

				checkGridNutrientCount();
								
				//check count of datasourcecomparison records. If no records, exists, set HideInAnalysis = false.
				ErrorInfo countField = new ErrorInfo();
				countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceComparison);
				if (countField.RecordID == 0)
				{				
					QueryTemplateInfo query = new QueryTemplateInfo();
					query.QueryTemplateID = (Int32)lueQueryTemplateInTab.EditValue;
					query.Name = lueQueryTemplateInTab.Text;
					query.IsAnalysis = false;
					query.CFCUserID = userGlobal.CFCLoginUser.CFCUserID;
					query.HideInAnalysis = false;

					//10/31/22
					query.IngredientOrProduct = "I";

					AIMManager.SaveQueryTemplate(ref query);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void checkGridNutrientCount()
		{
			try
			{
				ErrorInfo countField = new ErrorInfo();

				//--------------------------------------------------------------
				//if 3 nutrients have been selected, hide the first datasource/nutrient selection dropdowns;				
				countField = AIMManager.CountQueryTemplate((Int32)lueQueryTemplateInTab.EditValue, (Int32)QueryFeildEnum.DataSourceComparison);
				gbComparison1.Visible = true;
				btnAddDataSourceComparison.Visible = true;
				if (countField.RecordID == 3)
				{
					gbComparison1.Visible = false;
					gbComparison2.Visible = false;
					btnAddDataSourceComparison.Visible = false;
					lueDataSourceCompare1.EditValue = 0;
					lueNutrientCodeCompare1.EditValue = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueDataSourceCompare1_EditValueChanged(object sender, EventArgs e)
		{
			gbComparison2.Visible = false;
			if ((Int32)lueDataSourceCompare1.EditValue != 0 && (Int32)lueNutrientCodeCompare1.EditValue != 0)
			{
				gbComparison2.Visible = true;
			}
		}
		//decided not to go this route
		//////6/22/22
		////private void grdDataSourceComparisonView_GotFocus(object sender, EventArgs e)
		////{
		////	grdDataSourceComparisonView.ShowEditor();
		////}

		//6/22/22 - decided not to go this route
		//////6/22/22
		////private void grdDataSourceComparisonView_ShownEditor(object sender, EventArgs e)
		////{
		////	GridView view = sender as GridView;
		////	if (view.ActiveEditor is LookUpEdit)
		////	{
		////		LookUpEdit lookUp = ((LookUpEdit)view.ActiveEditor);

		////		if (!lookUp.IsPopupOpen)
		////			lookUp.ShowPopup();
		////	}
		////}

		#endregion

		//-------------------------------------------------------------------------------
		#region Sync Values

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

		private void lueMaterialCode_EditValueChanged(Object sender, EventArgs e)
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
		private void lueMaterialName_EditValueChanged(Object sender, EventArgs e)
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
		private void lueSupplierCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierCode, ref lueSupplierName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerCode, ref lueOwnerName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerName_EditValueChanged(Object sender, EventArgs e)
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
		private void lueOwnerCode_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerCode_Loc, ref lueOwnerName_Loc);
				DataBindOwnerLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerName_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerName_Loc, ref lueOwnerCode_Loc);
				DataBindOwnerLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerLocationCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerLocationCode, ref lueOwnerLocationName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueOwnerLocationName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueOwnerLocationName, ref lueOwnerLocationCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueSupplierName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierName, ref lueSupplierCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueSupplierCode_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierCode_Loc, ref lueSupplierName_Loc);
				DataBindSupplierLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueSupplierLocationName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierLocationName, ref lueSupplierLocationCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueSupplierLocationCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierLocationCode, ref lueSupplierLocationName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueSupplierName_Loc_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueSupplierName_Loc, ref lueSupplierCode_Loc);
				DataBindSupplierLocationlue();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueMaterialMultiCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueMaterialNutCode, ref lueMaterialNutName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueMaterialMultiName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueMaterialNutName, ref lueMaterialNutCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueIngredientCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueIngredientCode, ref lueIngredientName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueIngredientName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueIngredientName, ref lueIngredientCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueNutrientCode_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientCode, ref lueNutrientName);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		private void lueNutrientName_EditValueChanged(Object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientName, ref lueNutrientCode);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//6/14/22
		private void lueNutrientCodeCompare1_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientCodeCompare1, ref lueNutrientNameCompare1);

				gbComparison2.Visible = false;
				if ((Int32)lueDataSourceCompare1.EditValue != 0 && (Int32)lueNutrientCodeCompare1.EditValue != 0)
				{
					gbComparison2.Visible = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//6/14/22
		private void lueNutrientNameCompare1_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientNameCompare1, ref lueNutrientCodeCompare1);

				gbComparison2.Visible = false;
				if ((Int32)lueDataSourceCompare1.EditValue != 0 && (Int32)lueNutrientCodeCompare1.EditValue != 0)
				{
					gbComparison2.Visible = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//6/14/22
		private void lueNutrientCodeCompare2_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientCodeCompare2, ref lueNutrientNameCompare2);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//6/14/22
		private void lueNutrientNameCompare2_EditValueChanged(object sender, EventArgs e)
		{
			try
			{
				SyncValueLue(ref lueNutrientNameCompare2, ref lueNutrientCodeCompare2);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		//-------------------------------------------------------------------------------
		#region Lue SelectAll's 

		private void lueMaterialCode_Click(Object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}
		private void lueMaterialCode_Enter(Object sender, EventArgs e)
		{
			lueMaterialCode.SelectAll();
		}
		private void lueMaterialName_Click(Object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}
		private void lueMaterialName_Enter(Object sender, EventArgs e)
		{
			lueMaterialName.SelectAll();
		}
		private void lueOwnerCode_Enter(Object sender, EventArgs e)
		{
			lueOwnerCode.SelectAll();
		}
		private void lueOwnerCode_Click(Object sender, EventArgs e)
		{
			lueOwnerCode.SelectAll();
		}
		private void lueOwnerName_Click(Object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}
		private void lueOwnerName_Enter(Object sender, EventArgs e)
		{
			lueOwnerName.SelectAll();
		}
		private void lueOwnerCode_Loc_Enter(Object sender, EventArgs e)
		{
			lueOwnerCode_Loc.SelectAll();
		}
		private void lueOwnerCode_Loc_Click(Object sender, EventArgs e)
		{
			lueOwnerCode_Loc.SelectAll();
		}
		private void lueOwnerName_Loc_Click(Object sender, EventArgs e)
		{
			lueOwnerName_Loc.SelectAll();
		}
		private void lueOwnerName_Loc_Enter(Object sender, EventArgs e)
		{
			lueOwnerName_Loc.SelectAll();
		}
		private void lueOwnerLocationCode_Enter(Object sender, EventArgs e)
		{
			lueOwnerLocationCode.SelectAll();
		}
		private void lueOwnerLocationCode_Click(Object sender, EventArgs e)
		{
			lueOwnerLocationCode.SelectAll();
		}
		private void lueOwnerLocationName_Click(Object sender, EventArgs e)
		{
			lueOwnerLocationName.SelectAll();
		}
		private void lueOwnerLocationName_Enter(Object sender, EventArgs e)
		{
			lueOwnerLocationName.SelectAll();
		}
		private void lueSupplierName_Enter(Object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}
		private void lueSupplierName_Click(Object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}
		private void lueSupplierName_Loc_Enter(Object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}
		private void lueSupplierName_Loc_Click(Object sender, EventArgs e)
		{
			lueSupplierName.SelectAll();
		}
		private void lueSupplierLocationName_Click(Object sender, EventArgs e)
		{
			lueSupplierLocationName.SelectAll();
		}
		private void lueSupplierLocationName_Enter(Object sender, EventArgs e)
		{
			lueSupplierLocationName.SelectAll();
		}
		private void lueIngredientCode_Click(Object sender, EventArgs e)
		{
			lueIngredientCode.SelectAll();
		}
		private void lueIngredientCode_Enter(Object sender, EventArgs e)
		{
			lueIngredientCode.SelectAll();
		}
		private void lueIngredientName_Enter(Object sender, EventArgs e)
		{
			lueIngredientName.SelectAll();
		}
		private void lueIngredientName_Click(Object sender, EventArgs e)
		{
			lueIngredientName.SelectAll();
		}
		private void lueNutrientCode_Enter(Object sender, EventArgs e)
		{
			lueNutrientCode.SelectAll();
		}
		private void lueNutrientCode_Click(Object sender, EventArgs e)
		{
			lueNutrientCode.SelectAll();
		}
		private void lueNutrientName_Enter(Object sender, EventArgs e)
		{
			lueNutrientName.SelectAll();
		}
		private void lueNutrientName_Click(Object sender, EventArgs e)
		{
			lueNutrientName.SelectAll();
		}
		private void lueMaterialMultiCode_Enter(Object sender, EventArgs e)
		{
			lueMaterialNutCode.SelectAll();
		}
		private void lueMaterialMultiCode_Click(Object sender, EventArgs e)
		{
			lueMaterialNutCode.SelectAll();
		}
		private void lueMaterialMultiName_Click(Object sender, EventArgs e)
		{
			lueMaterialNutName.SelectAll();
		}
		private void lueMaterialMultiName_Enter(Object sender, EventArgs e)
		{
			lueMaterialNutName.SelectAll();
		}
		private void lueSupplierCode_Click(Object sender, EventArgs e)
		{
			lueSupplierCode.SelectAll();
		}
		private void lueSupplierCode_Enter(Object sender, EventArgs e)
		{
			lueSupplierCode.SelectAll();
		}
		private void lueSupplierCode_Loc_Click(Object sender, EventArgs e)
		{
			lueSupplierCode_Loc.SelectAll();
		}
		private void lueSupplierCode_Loc_Enter(Object sender, EventArgs e)
		{
			lueSupplierCode_Loc.SelectAll();
		}
		private void lueSupplierLocationCode_Click(Object sender, EventArgs e)
		{
			lueSupplierLocationCode.SelectAll();
		}
		private void lueSupplierLocationCode_Enter(Object sender, EventArgs e)
		{
			lueSupplierLocationCode.SelectAll();
		}
		private void lueSampleTypeName_Click(object sender, EventArgs e)
		{
			lueSampleTypeName.SelectAll();
		}
		private void lueSampleTypeName_Enter(object sender, EventArgs e)
		{
			lueSampleTypeName.SelectAll();
		}
		private void lueQueryTemplateInTab_Click(object sender, EventArgs e)
		{
			lueQueryTemplateInTab.SelectAll();
		}
		private void lueQueryTemplateInTab_Enter(object sender, EventArgs e)
		{
			lueQueryTemplateInTab.SelectAll();
		}
		//6/14/22
		private void lueNutrientCodeCompare1_Enter(object sender, EventArgs e)
		{
			lueNutrientCodeCompare1.SelectAll();
		}

		//6/14/22
		private void lueNutrientCodeCompare1_Click(object sender, EventArgs e)
		{
			lueNutrientCodeCompare1.SelectAll();
		}

		//6/14/22
		private void lueNutrientCodeCompare2_Enter(object sender, EventArgs e)
		{
			lueNutrientCodeCompare2.SelectAll();
		}

		//6/14/22
		private void lueNutrientCodeCompare2_Click(object sender, EventArgs e)
		{
			lueNutrientCodeCompare2.SelectAll();
		}

		//6/14/22
		private void lueNutrientNameCompare1_Click(object sender, EventArgs e)
		{
			lueNutrientNameCompare1.SelectAll();
		}

		//6/14/22
		private void lueNutrientNameCompare1_Enter(object sender, EventArgs e)
		{
			lueNutrientNameCompare1.SelectAll();
		}

		//6/14/22
		private void lueNutrientNameCompare2_Click(object sender, EventArgs e)
		{
			lueNutrientNameCompare2.SelectAll();
		}

		//6/14/22
		private void lueNutrientNameCompare2_Enter(object sender, EventArgs e)
		{
			lueNutrientNameCompare2.SelectAll();
		}

		//6/14/22
		private void lueDataSourceCompare1_Enter(object sender, EventArgs e)
		{
			lueDataSourceCompare1.SelectAll();
		}

		//6/14/22
		private void lueDataSourceCompare1_Click(object sender, EventArgs e)
		{
			lueDataSourceCompare1.SelectAll();
		}

		//6/14/22
		private void lueDataSourceCompare2_Click(object sender, EventArgs e)
		{
			lueDataSourceCompare2.SelectAll();
		}

		//6/14/22
		private void lueDataSourceCompare2_Enter(object sender, EventArgs e)
		{
			lueDataSourceCompare2.SelectAll();
		}

		#endregion

		//10/14/22
		private void btnSaveDriver_Click(object sender, EventArgs e)
		{
			AIMManager.UpdateQueryTemplateDetailNutrient((Int32)lueQueryTemplateInTab.EditValue, (Int32)lueDriverNutrientCode.EditValue);
		}

		//10/9/23
		private void rbSingleMaterial_CheckedChanged(object sender, EventArgs e)
		{			
			if (rbSingleMaterial.Checked == true)
			{
				gbMaterialSingle.Enabled = true;
				gbMaterialRange.Enabled = false;

			}
		}

		//10/9/23
		private void rbRangeMaterial_CheckedChanged(object sender, EventArgs e)
		{
			if (rbRangeMaterial.Checked == true)
			{
				gbMaterialRange.Enabled = true;
				gbMaterialSingle.Enabled = false;
			}
		}

		private void lueMaterialBeg_Click(object sender, EventArgs e)
		{
			lueMaterialBeg.SelectAll();
		}

		private void lueMaterialBeg_Enter(object sender, EventArgs e)
		{
			lueMaterialBeg.SelectAll();
		}

		private void lueMaterialEnd_Click(object sender, EventArgs e)
		{
			lueMaterialEnd.SelectAll();
		}

		private void lueMaterialEnd_Enter(object sender, EventArgs e)
		{
			lueMaterialEnd.SelectAll();
		}

		private void lueDataSourceType_Click(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

		private void lueDataSourceType_Enter(object sender, EventArgs e)
		{
			lueDataSourceType.SelectAll();
		}

		private void lueDriverNutrientCode_Click(object sender, EventArgs e)
		{
			lueDriverNutrientCode.SelectAll();
		}

		private void lueDriverNutrientCode_Enter(object sender, EventArgs e)
		{
			lueDriverNutrientCode.SelectAll();
		}

		private void btnCreateUserReport_Click(object sender, EventArgs e)
		{
			report_Designer = new Report_Designer();
			report_Designer.Show();
		}
	}

}