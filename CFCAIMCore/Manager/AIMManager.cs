using CFCAIMCore.Data;
using CFCAIMCore.Entity;
using CFCAIMCore.Entity.CFCAPI;
using CFCAIMCore.Entity.Group;
using CFCAIMCore.Entity.Test;
using CFCAPICore.Entity;
using CFCAPICore.Entity.Adisseo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CFCAIMCore.Manager
{
	public class AIMManager
	{
		#region  Member Variables

		public static String _host;

		private static String _databaseConnection;
		private static Boolean _isMultipleDatabase = false;
		private static AIMDataAccess _aimData;

		#endregion

		#region  Properties
		protected static AIMDataAccess AIMData
		{
			get
			{
				if (_aimData == null)
				{
					if (IsMultipleDatabase)
					{
						_aimData = new Data.AIMDataAccess(String.Empty, DatabaseConnection);
					}
					else
					{
						if (_host == "CFCAIMDEMO")
						{
							_aimData = new Data.AIMDataAccess("DemoConnectionString", String.Empty);
						}
						else
						{
							_aimData = new Data.AIMDataAccess("ConnectionString", String.Empty);
						}
					}

					//test
					///////_databaseConnection = "Server=CFC-04\\SQL2014;Database=CFCAIMPerfection;User Id=sa;Password=SQL2014";


				}
				return _aimData;
			}
		}

		public static String Host
		{
			get
			{
				return _host;
			}

			set
			{
				_host = value;
			}
		}

		public static Boolean IsMultipleDatabase
		{
			get
			{
				return _isMultipleDatabase;
			}

			set
			{
				_isMultipleDatabase = value;
			}
		}

		public static String DatabaseConnection
		{
			get
			{
				return _databaseConnection;
			}

			set
			{
				_databaseConnection = value;
			}
		}

		public static void CloseAIMData()
		{
			_aimData = null;
		}
		#endregion

		#region Funcation
		public static void CreateConnection(String serverName, String databaseName, Boolean isSqlAuthentication, String userName, String password)
		{
			if (isSqlAuthentication == true)
			{
				DatabaseConnection = String.Format("Data Source = {0}; Initial Catalog = {1}; User Id = {2}; Password = {3}", serverName, databaseName, userName, password);
			}
		}
		#endregion

		#region Get
		public static List<PNECreditInfo> GetPNECredit()
		{
			return AIMData.GetPNECredit();
		}
		//01/18/23
		public static List<APISprectrumLogInfo> GetAPISprectrumLog(Int32 sampleID, Int32 apiSpectrumID)
		{
			return AIMData.GetAPISprectrumLog(sampleID, apiSpectrumID);
		}
		public static List<APISprectrumLogInfo> GetAPISprectrumLog(Int32 sampleID, EnumAPINIRType apiNIRType)
		{
			return AIMData.GetAPISprectrumLog(sampleID, (Int32)apiNIRType);
		}

		//2/21/20g
		//public static void BackupDbase(String bkSql)
		public static Int32 BackupDbase(String bkSql)
		{
			return AIMData.BackupDbase(bkSql);

		}

		//2/25/20                
		public static ErrorInfo GetNewID()
		{
			return AIMData.GetNewID();
		}		

		public static VersionInfo GetVersion()
		{
			return AIMData.GetVersion();
		}

		public static List<ApplicationInfo> GetAppID(String appName)
		{
			return AIMData.GetAppID(appName);
		}

		//4/11/18  
		public static List<PivotGridTestInfo> GetPivotGrid()
		{
			return AIMData.GetPivotGrid();
		}

		//11/30/22 
		public static List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysisPending()
		{
			return AIMData.GetSampleFurtherAnalysisPending();
		}

		//7/28/22		
		public static List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis()
		{
			return AIMData.GetSampleFurtherAnalysis();
		}

		//1/18/24
		public static List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate, Int32 dateField, Int32 dataSourceTypeID)
		{
			return AIMData.GetSampleFurtherAnalysis(begDate, endDate, dateField, dataSourceTypeID);
		}

		//2/6/24
		//7/11/23		
		//public static List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate)
		public static List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate, Boolean isAll)
		{
			return AIMData.GetSampleFurtherAnalysis(begDate, endDate, isAll);
		}

		//2/23/24
		public static List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(DateTime begDate, DateTime endDate, Boolean isAll, Int32 sampleID)
		{
			return AIMData.GetSampleFurtherAnalysis(begDate, endDate, isAll, sampleID);
		}

		//8/24/22	
		public static List<SampleFurtherAnalysisInfo> GetSampleFurtherAnalysis(Int32 sampleID)
		{
			return AIMData.GetSampleFurtherAnalysis(sampleID);
		}

		//8/1/22		
		public static ErrorInfo  UpdateSampleFurtherAnalysis(Int32 sampleFurtherAnalysisID)
		{
			return AIMData.UpdateSampleFurtherAnalysis(sampleFurtherAnalysisID);
		}


		//5/13/19
		public static List<UpdateIngredientLogInfo> GetUpdateIngredientLog(Int32 updateIngredientHeaderID)
		{
			return AIMData.GetUpdateIngredientLog(updateIngredientHeaderID);
		}

		public static List<ColumnDescriptionOverrideInfo> GetColumnDescriptionOverride(String tableName)
		{
			return AIMData.GetColumnDescriptionOverride(tableName);
		}

		public static List<EventDayInfo> GetEventDay(Int32 eventID)
		{
			return AIMData.GetEventDay(eventID);
		}
		public static List<DayInfo> GetDay()
		{
			return AIMData.GetDay();
		}

		public static List<WeekInfo> GetWeek()
		{
			return AIMData.GetWeek();
		}

		public static List<EventWeeklyInfo> GetEventWeekly(Int32 eventID)
		{
			return AIMData.GetEventWeekly(eventID);
		}

		public static List<EventMonthlyInfo> GetEventMonthly(Int32 eventID)
		{
			return AIMData.GetEventMonthly(eventID);
		}

		//4/27/22		
		public static List<ProductToleranceProductCodesInfo> GetAIMMaterialProductCodes()
		{
			return AIMData.GetAIMMaterialProductCodes();
		}

		public static List<MonthInfo> GetMonth()
		{
			return AIMData.GetMonth();
		}

		public static List<EventsInfo> GetEvents()
		{
			return AIMData.GetEvents();
		}

		public static List<EventsInfo> GetEvents(Boolean eventStatus)
		{
			return AIMData.GetEvents(eventStatus);
		}

		public static List<EventRunTypeInfo> GetEventRunType()
		{
			return AIMData.GetEventRunType();
		}
		public static List<EventTypeInfo> GetEventType()
		{
			return AIMData.GetEventType();
		}

		//12/1/22
		//public static List<PlusMinusInfo> GetPlusMinus()
		public static List<PlusMinusInfo> GetPlusMinus(Boolean includeNone)
		{
			return AIMData.GetPlusMinus(includeNone);
		}

		//6/28/21
		public static List<YesNoInfo> GetYesNo()
		{
			return AIMData.GetYesNo();
		}
						
		public static List<GroundUnGroundInfo> GetGroundUnGround()
		{
			return AIMData.GetGroundUnGround();
		}

		//2/8/24
		public static List<GroundUnGroundInfo> GetGroundUnGround(Int32 groundUngroundID)
		{
			return AIMData.GetGroundUnGround(groundUngroundID);
		}

		//////////1/17/24
		////////public static List<GroundUnGroundInfo> GetGroundUnGroundDefault()
		////////{
		////////	return AIMData.GetGroundUnGroundDefault();
		////////}

		//1/17/24
		public static List<GroundUnGroundInfo> GetGroundUnGroundPNE()
		{
			return AIMData.GetGroundUnGroundPNE();
		}

		//3/15/19
		public static ErrorInfo  SaveEvent(ref EventsInfo eventInfo)
		{
			return AIMData.SaveEvent(ref eventInfo);
		}

		public static ErrorInfo  SaveEventWeekly(Int32 eventWeeklyID, Int32 eventID, Int32 weekID, Int32 recurEvery)
		{
			return AIMData.SaveEventWeekly(eventWeeklyID, eventID, weekID, recurEvery);
		}
		public static ErrorInfo  SaveEventDaily(Int32 eventDailyID, Int32 eventID, Int32 DayID, Int32 RecurEvery)
		{
			return AIMData.SaveEventDaily(eventDailyID, eventID, DayID, RecurEvery);
		}


		public static ErrorInfo  SaveEventMonthly(Int32 eventMonthlyID, Int32 eventID, Int32 monthID, Boolean specificDays, Boolean specificWeek)
		{
			return AIMData.SaveEventMonthly(eventMonthlyID, eventID, monthID, specificDays, specificWeek);
		}

		public static ErrorInfo  SaveEventMonthlySpecificDays(Int32 ID, Int32 eventMonthlyID, Int32 dayID)
		{
			return AIMData.SaveEventMonthlySpecificDays(ID, eventMonthlyID, dayID);
		}


		public static ErrorInfo  SaveEventMonthlySpecificWeekDays(Int32 ID, Int32 eventMonthlyID, Int32 weekID)
		{
			return AIMData.SaveEventMonthlySpecificWeekDays(ID, eventMonthlyID, weekID);
		}

		public static ErrorInfo  SaveEventMonthlySpecificWeekNumber(Int32 ID, Int32 eventMonthlyID, Int32 weekNumberID)
		{
			return AIMData.SaveEventMonthlySpecificWeekNumber(ID, eventMonthlyID, weekNumberID);
		}

		public static void DeleteEvent(Int32 ID)
		{
			AIMData.DeleteEvent(ID);
		}

		//1/4/24
		public static List<SystemLogInfo> GetSystemLog()
		{
			return AIMData.GetSystemLog();
		}


		public static List<UserAccessInfo> GetUserAccess(Int32 cfcUserID, Int32 permissionID)
		{
			return AIMData.GetUserAccess(cfcUserID, permissionID);
		}
		public static List<SampleLogInfo> GetSampleLog(Boolean isActive)
		{
			return AIMData.GetSampleLog(isActive);
		}
		public static List<SampleLogInfo> GetSampleLog(Int32 sampleID)
		{
			return AIMData.GetSampleLog(sampleID);
		}

		//2/4/20        
		public static List<ChartSampleListInfo> GetChartSampleList(DateTime startDate, DateTime endDate, Int32 materialID, Int32 nutrientID, Int32 supplierID, Int32 supplierLocationID)
		{
			return AIMData.GetChartSampleList(startDate, endDate, materialID, nutrientID, supplierID, supplierLocationID);
		}


		public static List<GraphInfo> GetSampleForGraphTest()
		{
			return AIMData.GetSampleForGraphTest();
		}
		public static List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAnalyzerSupplierLocation(DataSourceTypeID, isActive);
		}

		public static List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 DataSourceTypeID)
		{
			return AIMData.GetAnalyzerSupplierLocation(DataSourceTypeID);
		}

		public static List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 analyzerSupplierLocationID, Int32 DataSourceTypeID, Int32 isActive)
		{
			return AIMData.GetAnalyzerSupplierLocation(analyzerSupplierLocationID, DataSourceTypeID, isActive);
		}

		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive)
		{
			return AIMData.GetSupplierLocationGuarantee(isActive);
		}

		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Int32 aimMaterialID)
		{
			return AIMData.GetSupplierLocationGuarantee(aimMaterialID);
		}

		//7/2/20
		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuaranteeNutrient(Int32 aimNutrientID, Boolean isActive)
		{
			return AIMData.GetSupplierLocationGuaranteeNutrient(aimNutrientID, isActive);
		}

		//7/28/20
		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive, Int32 aimSupplierID)
		{
			return AIMData.GetSupplierLocationGuarantee(isActive, aimSupplierID);
		}

		//7/28/20
		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive, Int32 aimSupplierID, Int32 aimSupplierLocationID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			return AIMData.GetSupplierLocationGuarantee(isActive, aimSupplierID, aimSupplierLocationID, aimMaterialID, aimNutrientID);
		}

		//7/28/20
		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Boolean isActive, Int32 aimSupplierID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			return AIMData.GetSupplierLocationGuarantee(isActive, aimSupplierID, aimMaterialID, aimNutrientID);
		}

		//7/28/20
		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuarantee(Int32 aimSupplierID, Int32 aimSupplierLocationID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			return AIMData.GetSupplierLocationGuarantee(aimSupplierID, aimSupplierLocationID, aimMaterialID, aimNutrientID);
		}

		//7/28/20
		public static ErrorInfo  DeleteAIMSupplierLocationGuarantee(Int32 aimSupplierLocationGuaranteeID)
		{
			return AIMData.DeleteAIMSupplierLocationGuarantee(aimSupplierLocationGuaranteeID);
		}

		//2/23/24		
		public static ErrorInfo  DeletePNECredit(Int32 pneCreditID)
		{
			return AIMData.DeletePNECredit(pneCreditID);
		}


		//12/3/20
		public static List<AIMSupplierLocationGuaranteeInfo> GetSupplierLocationGuaranteesFiltered(Boolean isActive, Int32 aimSupplierID, Int32 aimSupplierLocationID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			return AIMData.GetSupplierLocationGuaranteesFiltered(isActive, aimSupplierID, aimSupplierLocationID, aimMaterialID, aimNutrientID);
		}


		//12/21/20
		public static List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombined(Int32 analyzerSupplierLocationCombinedID, Int32 datasourceTypeID, Int32 isActive)
		{
			return AIMData.GetAnalyzerSupplierLocationCombined(analyzerSupplierLocationCombinedID, datasourceTypeID, isActive);
		}

		public static AnalyzerSupplierLocationCombinedInfo GetAnalyzerSupplierLocationCombined(Int32 analyzerSupplierLocationCombinedID, Int32 datasourceTypeID)
		{
			return AIMData.GetAnalyzerSupplierLocationCombined(analyzerSupplierLocationCombinedID, datasourceTypeID);
		}

		//2/18/22
		public static QueryOperationSampleDateInfo GetTemplateSampleDates(Int32 queryOperationID)
		{
			return AIMData.GetTemplateSampleDates(queryOperationID);
		}

		public static AnalyzerSupplierInfo GetAnalyzerSupplier(Int32 analyzerSupplierID, Int32 datasourceTypeID)
		{
			return AIMData.GetAnalyzerSupplier(analyzerSupplierID, datasourceTypeID);
		}

		public static List<AnalyzerSupplierInfo> GetAnalyzerSupplier(Int32 analyzerSupplierID, Int32 datasourceTypeID, Int32 isActive)
		{
			return AIMData.GetAnalyzerSupplier(analyzerSupplierID, datasourceTypeID, isActive);
		}


		public static AnalyzerSupplierLocationInfo GetAnalyzerSupplierLocation(Int32 datasourceTypeID, String analyzerSupplierLocationCode)
		{
			return AIMData.GetAnalyzerSupplierLocation(datasourceTypeID, analyzerSupplierLocationCode);
		}


		public static AnalyzerOwnerLocationInfo GetAnalyzerOwnerLocationDataSourceTypeOwnerCode(Int32 datasourceTypeID, String analyzerOwnerLocationCode)
		{
			return AIMData.GetAnalyzerOwnerLocationDataSourceTypeOwnerCode(datasourceTypeID, analyzerOwnerLocationCode);
		}


		public static AnalyzerOwnerInfo GetAnalyzerOwnerDataSourceTypeOwnerCode(Int32 datasourceTypeID, String analyzeOwnerCode)
		{
			return AIMData.GetAnalyzerOwnerDataSourceTypeOwnerCode(datasourceTypeID, analyzeOwnerCode);
		}


		public static AnalyzerSupplierLocationInfo GetAnalyzerSupplierLocation(Int32 datasourceTypeID, Int32 analyzerSupplierID, String analyzerSupplierLocationCode)
		{
			return AIMData.GetAnalyzerSupplierLocation(datasourceTypeID, analyzerSupplierID, analyzerSupplierLocationCode);
		}

		public static AnalyzerSupplierLocationInfo GetAnalyzerSupplierLocationName(Int32 datasourceTypeID, Int32 analyzerSupplierID, String analyzerSupplierLocationName)
		{
			return AIMData.GetAnalyzerSupplierLocationName(datasourceTypeID, analyzerSupplierID, analyzerSupplierLocationName);
		}

		//1/17/18
		public static List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocationNameList(Int32 datasourceTypeID, Int32 analyzerSupplierID, String analyzerSupplierLocationName)
		{
			return AIMData.GetAnalyzerSupplierLocationNameList(datasourceTypeID, analyzerSupplierID, analyzerSupplierLocationName);
		}

		//12/24/20		
		public static List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombinedList(Int32 datasourceTypeID, String analyzerSupplierLocationName)
		{
			return AIMData.GetAnalyzerSupplierLocationCombinedList(datasourceTypeID, analyzerSupplierLocationName);
		}

		public static AnalyzerNutrientInfo GetAnalyzerNutrientUnits(Int32 analyzerNutrientID, Int32 datasourceTypeID)
		{
			return AIMData.GetAnalyzerNutrientUnits(analyzerNutrientID, datasourceTypeID);
		}

		public static AnalyzerNutrientInfo GetAnalyzerNutrient(Int32 datasourceTypeID, String analyzerNutrientCode)
		{
			return AIMData.GetAnalyzerNutrient(datasourceTypeID, analyzerNutrientCode);
		}

		public static List<AnalyzerNutrientInfo> GetAnalyzerNutrient(Int32 datasourceTypeID)
		{
			return AIMData.GetAnalyzerNutrient(datasourceTypeID);
		}

		public static List<MathTypeInfo> GetMathType(Boolean isActive)
		{
			return AIMData.GetMathType(isActive);
		}

		public static List<MathTypeInfo> GetMathTypeID(Int32 mathTypeID)
		{
			return AIMData.GetMathTypeID(mathTypeID);
		}

		//1/3/18
		public static List<SampleQualityEquationInfo> GetEquationID(Int32 ID)
		{
			return AIMData.GetEquationID(ID);
		}

		//1/4/18       
		public static List<SampleQualityEquationBuildInfo> GetSampleEquationBuild_SampleEquationBuildID(Int32 ID)
		{
			return AIMData.GetSampleEquationBuild_SampleEquationBuildID(ID);
		}

		public static List<MathTypeInfo> GetMathTypeSorted(Boolean isActive)
		{
			return AIMData.GetMathTypeSorted(isActive);
		}

		//1/3/18
		public static List<SampleQualityEquationInfo> GetQualityControlEquations(Boolean isActive)
		{
			return AIMData.GetQualityControlEquations(isActive);
		}

		public static List<AIMMaterialInfo> GetAIMMaterial(Boolean isActive)
		{
			return AIMData.GetAIMMaterial(isActive);
		}

		//1/5/22
		public static List<AIMMaterialInfo> GetAIMMaterialWithOrMissingXref(Boolean isActive, Boolean IsXref)
		{
			return AIMData.GetAIMMaterialWithOrMissingXref(isActive, IsXref);
		}

        //2/13/25
        //6/5/24
        //public static List<AIMMaterialInfo> GetAIMMaterialProductWithOrMissingXref(Boolean isActive, Boolean IsXref)
        public static List<AIMMaterialInfo> GetAIMMaterialProductWithOrMissingXref(Boolean isActive, Boolean IsXref, Int32 ownerLocationID)
        {
			return AIMData.GetAIMMaterialProductWithOrMissingXref(isActive, IsXref, ownerLocationID);
		}

		//1/5/22
		public static List<CFCIngredientInfo> GetAllCFCIngredientAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean IsXref)
		{
			return AIMData.GetAllCFCIngredientAimDatabaseIDWithOrMissingXref(isActive, aimDatabaseID, IsXref);
		}

		//1/6/23 - added materialtypeid
		public static List<AIMMaterialInfo> GetAIMMaterialForSampleDates(Boolean isActive, DateTime startDate, DateTime endDate, Int32 materialTypeID)
		{
			return AIMData.GetAIMMaterialForSampleDates(isActive, startDate, endDate, materialTypeID);
		}

		public static List<AIMMaterialInfo> GetAIMMaterialOrderByMaterialName(Boolean isActive)
		{
			return AIMData.GetAIMMaterialOrderByMaterialName(isActive);
		}

		public static List<AIMMaterialInfo> GetAIMMaterialAIMMaterialCode(String aimMaterialCode)
		{
			return AIMData.GetAIMMaterialAIMMaterialCode(aimMaterialCode);
		}

		//10/9/23
		public static List<AIMMaterialInfo> GetAIMMaterialAIMMaterialCodeRange(String aimMaterialCodeBeg, String aimMaterialCodeEnd)
		{
			return AIMData.GetAIMMaterialAIMMaterialCodeRange(aimMaterialCodeBeg, aimMaterialCodeEnd);
		}

		public static List<AIMMaterialInfo> GetAIMMaterial(String aimMaterialCode, Int32 aimOwnerLocationID)
		{
			return AIMData.GetAIMMaterial(aimMaterialCode, aimOwnerLocationID);
		}

		//10/25/23
		//public static List<AIMMaterialInfo> GetAIMMaterial_MaterialTypeID_IsActive(Int32 MaterialTypeID, Boolean isActive)
		public static List<AIMMaterialInfo> GetAIMMaterial_MaterialTypeID_IsActive(Int32 MaterialTypeID, Boolean isActive, Int32 orderBy)
		{
			//10/25/23
			//return AIMData.GetAIMMaterial_MaterialTypeID_IsActive(MaterialTypeID, isActive);
			return AIMData.GetAIMMaterial_MaterialTypeID_IsActive(MaterialTypeID, isActive, orderBy);
		}

		public static List<AIMMaterialInfo> GetAIMMaterial(Boolean isActive, Int32 aimOwnerLocationID, Int32 materialTypeID)
		{
			return AIMData.GetAIMMaterial(isActive, aimOwnerLocationID, materialTypeID);
		}

		//5/19/23
		public static List<SpecificProductsInfo> GetSpecificProducts(Int32 groupID, Boolean isActive, Int32 aimOwnerLocationID)
		{
			return AIMData.GetSpecificProducts(groupID, isActive, aimOwnerLocationID);
		}

		//10/13/23
		//public static List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType(Boolean isActive, Int32 materialTypeID)
		//{
		//	return AIMData.GetAIMMaterial_Active_MaterialType(isActive, materialTypeID);
		//}
		public static List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType(Boolean isActive, Int32 materialTypeID, Int32 orderBy)
		{
			return AIMData.GetAIMMaterial_Active_MaterialType(isActive, materialTypeID, orderBy);
		}

		//9/23/22
		public static List<SpecificIngredientsInfo> GetSpecificIngredients()
		{
			return AIMData.GetSpecificIngredients();
		}

		//9/23/22
		public static List<SpecificIngredientsInfo> GetSpecificIngredients(Int32 groupID, Boolean isActive)
		{
			return AIMData.GetSpecificIngredients(groupID, isActive);
		}

		//10/19/22
		public static List<SpecificIngredientsGroupInfo> GetSpecificIngredientsGroup(Boolean isActive)
		{
			return AIMData.GetSpecificIngredientsGroup(isActive);
		}

		//5/18/23
		public static List<SpecificProductsGroupInfo> GetSpecificProductsGroup(Boolean isActive)
		{
			return AIMData.GetSpecificProductsGroup(isActive);
		}

		//9/23/22
		public static List<SpecificIngredientsInfo> GetSpecificIngredients(Int32 aimMaterialID)
		{
			return AIMData.GetSpecificIngredients(aimMaterialID);
		}

		//5/18/23
		public static List<SpecificProductsInfo> GetSpecificProducts(Int32 aimMaterialID)
		{
			return AIMData.GetSpecificProducts(aimMaterialID);
		}

		//10/19/22
		public static List<SpecificIngredientsInfo> GetSpecificIngredients_GroupID(Int32 groupID)
		{
			return AIMData.GetSpecificIngredients_GroupID(groupID);
		}

		//5/18/23
		public static List<SpecificProductsInfo> GetSpecificProducts_GroupID(Int32 groupID)
		{
			return AIMData.GetSpecificProducts_GroupID(groupID);
		}

		//6/18/20
		public static List<AIMMaterialStockStatusInfo> GetAIMMaterialStockStatus(Boolean isActive)
		{
			return AIMData.GetAIMMaterialStockStatus(isActive);
		}

		//6/5/20
		public static List<AIMMaterialInfo> GetAIMMaterial_Active_Products(Boolean isActive)
		{
			return AIMData.GetAIMMaterial_Active_Products(isActive);
		}

		////6/18/20 - added ownerlocationid
		//public static List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType_For_Sample(Boolean isActive, Int32 materialTypeID, Int32 ownerLocationID)
		//{
		//    return AIMData.GetAIMMaterial_Active_MaterialType_For_Sample(isActive, materialTypeID, ownerLocationID);
		//}

		//10/7/20 - renamed stp above (to include ownerlocation)
		public static List<AIMMaterialInfo> GetAIMMaterial_Active_MaterialType_OwnerLocation_For_Sample(Boolean isActive, Int32 materialTypeID, Int32 ownerLocationID)
		{
			return AIMData.GetAIMMaterial_Active_MaterialType_OwnerLocation_For_Sample(isActive, materialTypeID, ownerLocationID);
		}


		public static List<WarningLimitsInfo> GetAllWarningLimits()
		{
			return AIMData.GetAllWarningLimits();
		}

		//7/2/20
		public static List<WarningLimitsInfo> GetWarningLimitsForMaterial(Int32 aimMaterialID)
		{
			return AIMData.GetWarningLimitsForMaterial(aimMaterialID);
		}
		//7/2/20
		public static List<WarningLimitsInfo> GetWarningLimitsForNutrient(Int32 aimNutrientID)
		{
			return AIMData.GetWarningLimitsForNutrient(aimNutrientID);
		}

		//2/25/20
		public static SystemMasterAndDatabaseFilesInfo GetMasterAndDatabaseInfo(String databaseName, String fileType)
		{
			return AIMData.GetMasterAndDatabaseInfo(databaseName, fileType);
		}


		//1/2/18
		public static List<SampleQualityParameterInfo> GetAllQualityParameters()
		{
			return AIMData.GetAllQualityParameters();
		}

		//1/4/18
		public static SampleQualityParameterInfo GetQualityParameterID(Int32 id)
		{
			return AIMData.GetQualityParameterID(id);
		}

		//3/22/18 - added dataSourceTypeID
		//3/15/18        
		public static SampleQualityParameterInfo GetQualityParameterName(String name, Int32 dataSourceTypeID)
		{
			return AIMData.GetQualityParameterName(name, dataSourceTypeID);
		}

		//1/23/18        
		public static ErrorInfo  UserDefinedDataSourceTemplate_UpdateMap(Int32 defID, Int32 templateID, Int32 mapColumnID, String mapColumnName)
		{
			return AIMData.UserDefinedDataSourceTemplate_UpdateMap(defID, templateID, mapColumnID, mapColumnName);
		}


		public static List<UserDefinedDataSourceTemplateInfo> GetAllUserDefinedDataSourceTemplate()
		{
			return AIMData.GetAllUserDefinedDataSourceTemplate();
		}
		public static List<UserDefinedDataSourceDefinitionInfo> GetAllUserDefinedDataSourceDefinition()
		{
			return AIMData.GetAllUserDefinedDataSourceDefinition();
		}

		public static List<UserDefinedDataSourceDefinitionInfo> GetAllUserDefinedDataSourceDefinition_ForScheduler()
		{
			return AIMData.GetAllUserDefinedDataSourceDefinition_ForScheduler();
		}

		public static List<UserDefinedDataSourceTemplateInfo> GetUserDefinedDataSourceTemplate(Int32 ID)
		{
			return AIMData.GetUserDefinedDataSourceTemplate(ID);
		}

		public static List<DataSourceTypeInfo> GetAllDataSourceUser()
		{
			return AIMData.GetAllDataSourceUser();
		}
		public static List<EmailAddressXRefMessageInfo> GetAllEmailMessageXrefMessage()
		{
			return AIMData.GetAllEmailMessageXrefMessage();
		}

		public static LicensingConfigInfo GetCompanyName(Int32 configID)
		{
			return AIMData.GetCompanyName(configID);
		}

		//1/8/24
		public static LicensingConfigInfo SaveCompanyName(Int32 configID, String companyName)
		{
			return AIMData.SaveCompanyName(configID, companyName);
		}

		public static ProgressBarInfo GetProgressBarValue(Int32 progressBarID)
		{
			return AIMData.GetProgressBarValue(progressBarID);
		}

		//4/23/18
		public static ProgressBarInfo GetProgressBarValue(String progressBarDescription, Int32 cfcUserID)
		{
			return AIMData.GetProgressBarValue(progressBarDescription, cfcUserID);
		}

		public static List<ProgressBarInfo> GetProgressBarValueByCFCUserID(Int32 CFCUserID)
		{
			return AIMData.GetProgressBarValueByCFCUserID(CFCUserID);
		}

		public static List<WebServiceInfo> GetAllWebService()
		{
			return AIMData.GetAllWebService();
		}
		public static List<UnitTypeInfo> GetAllUnitType()
		{
			return AIMData.GetAllUnitType();
		}
		public static List<MaterialTypeInfo> GetMaterialType(Boolean isActive)
		{
			return AIMData.GetMaterialType(isActive);
		}
		public static List<AnalyzerAnalyseInfo> GetAnalyzerAnalyse(Boolean isActive)
		{
			return AIMData.GetAnalyzerAnalyse(isActive);
		}
		public static List<AnalyzerAnalyseInfo> GetAnalyzerAnalyse(Int32 analyzerMaterialID)
		{
			return AIMData.GetAnalyzerAnalyse(analyzerMaterialID);
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialList(Int32 analyzerMaterialID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialList(analyzerMaterialID);
		}

		//7/11/23
		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialListMaterialTypeID(Int32 materialTypeID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialListMaterialTypeID(materialTypeID);
		}

		public static List<AnalyzerAnalyseInfo> GetAnalyzerAnalyse()
		{
			return AIMData.GetAnalyzerAnalyse();
		}
		public static AIMConfigInfo GetAIMConfig(Int32 CFCUserID)
		{
			return AIMData.GetAIMConfig(CFCUserID);
		}
		public static List<AnalysisLogInfo> GetAllAnalysisLog(Boolean isActive)
		{
			return AIMData.GetAllAnalysisLog(isActive);
		}

		//4/4/18
		//public static List<AnalysisViewTemplateInfo> GetAllAnalysisViewTemplate(Boolean isActive)
		public static List<AnalysisViewTemplateInfo> GetAllAnalysisViewTemplate(Boolean isActive, Int32 gridNumber, Int32 templateType)
		{
			return AIMData.GetAllAnalysisViewTemplate(isActive, gridNumber, templateType);
		}


		//4/12/18        
		public static List<AnalysisViewTemplateInfo> GetAnalysisViewTemplate_TemplateType(Int32 templateType)
		{
			return AIMData.GetAnalysisViewTemplate_TemplateType(templateType);
		}

		//5/21/19        
		public static AnalysisViewTemplateInfo GetAnalysisViewTemplate(Int32 analysisViewTemplateID)
		{
			return AIMData.GetAnalysisViewTemplate(analysisViewTemplateID);
		}


		//Task1075 DB250715

		public static ReportUserDefaultSettingInfo GetReportUserDefaultSetting(Int32 cfcUserID)
		{
			return AIMData.GetReportUserDefaultSetting(cfcUserID);
        }

		

        public static List<AnalysisViewTemplateNameInfo> GetTemplateName(String name)
		{
			return AIMData.GetTemplateName(name);
		}

		//4/12/18
		public static List<AnalysisViewTemplateNameInfo> GetTemplateName(String name, Int32 templateType)
		{
			return AIMData.GetTemplateName(name, templateType);
		}

		//4/13/18
		public static List<AnalysisViewTemplateNameInfo> GetTemplateName(String name, Int32 gridNumber, Int32 templateType)
		{
			return AIMData.GetTemplateName(name, gridNumber, templateType);
		}

		public static List<ReportQueryInfo> GetAllReportQuery(Boolean isActive)
		{
			return AIMData.GetAllReportQuery(isActive);
		}
		public static List<AnalysisReportsInfo> GetDataThroughReportQuery(String query)
		{
			return AIMData.GetDataThroughReportQuery(query);
		}
		public static List<AnalysisInfo> GetDataThroughAnalysisQuery(String query)
		{
			return AIMData.GetDataThroughAnalysisQuery(query);
		}
		public static List<ReportQueryDetailInfo> GetOneReportQueryDetail(Int32 reportQueryID, Boolean isActive)
		{
			return AIMData.GetOneReportQueryDetail(reportQueryID, isActive);
		}

		public static List<ReportQueryDetailInfo> GetAllReportQueryDetail(Boolean isActive)
		{
			return AIMData.GetAllReportQueryDetail(isActive);
		}

		public static List<TableColumnInfo> GetAllTablesColumnsFromDatabse()
		{
			return AIMData.GetAllTablesColumnsFromDatabse();
		}

		//2/13/25
		//public static List<CFCProductInfo> GetAllCFCProductAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean isXref)
        public static List<CFCProductInfo> GetAllCFCProductAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean isXref, Int32 cfcPlantID)
        {			
            return AIMData.GetAllCFCProductAimDatabaseIDWithOrMissingXref(isActive, aimDatabaseID, isXref, cfcPlantID);
        }

		//6/5/24
		public static List<CFCProductRefAIMMaterialInfo> GetMissingProductXRefs()
		{
			return AIMData.GetMissingProductXRefs();
		}		        

        //6/5/24		
        public static ErrorInfo  UpdateCFCProductAimMaterialWithMissingXref(Int32 cfcProductID, String cfcProductCode, String cfcProductName, Int32 aimOwnerLocationID, Int32 cfcUserID)
		{
			return AIMData.UpdateCFCProductAimMaterialWithMissingXref(cfcProductID, cfcProductCode, cfcProductName, aimOwnerLocationID, cfcUserID);
		}

        //2/17/25
        public static List<CFCIngredientRefAIMMaterialInfo> GetMissingIngredientXRefs()
        {
            return AIMData.GetMissingIngredientXRefs();
        }

        //2/17/25		
        public static ErrorInfo  UpdateCFCIngredientAimMaterialWithMissingXref(Int32 cfcIngredientID, String cfcIngredientCode, String cfcIngredientName, Int32 cfcUserID)
        {
            return AIMData.UpdateCFCIngredientAimMaterialWithMissingXref(cfcIngredientID, cfcIngredientCode, cfcIngredientName, cfcUserID);
        }

		//2/18/25		
		public static List<NameUpdateInfo> GetNameDifferences(Int32 table)
        {
            return AIMData.GetNameDifferences(table);
        }

		//2/18/25
        public static ErrorInfo  SaveAIMName(Int32 table, Int32 aimID, String aimName)
        {
			return AIMData.SaveAIMName(table, aimID, aimName);
        }
		        
        public static List<CFCProductRefAIMMaterialInfo> GetAllCFCProductRefAIMMaterial()
		{
			return AIMData.GetAllCFCProductRefAIMMaterial();
		}
		public static List<CFCNutrientInfo> GetAllCFCNutrient(Boolean isActive)
		{
			return AIMData.GetAllCFCNutrient(isActive);
		}

		//5/31/18
		public static List<CFCNutrientInfo> GetCFCNutrientID(Int32 cfcNutrientID)
		{
			return AIMData.GetCFCNutrientID(cfcNutrientID);
		}

		//2/12/24
		public static List<CFCNutrientInfo> GetCFCNutrientUpdateIngredientDetail(Int32 updateIngredientDetailID)
		{
			return AIMData.GetCFCNutrientUpdateIngredientDetail(updateIngredientDetailID);
		}


		public static List<CFCNutrientInfo> GetAllCFCNutrientAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			return AIMData.GetAllCFCNutrientAimDatabaseID(isActive, aimDatabaseID);
		}

		//1/5/22
		public static List<CFCNutrientInfo> GetAllCFCNutrientAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean isXref)
		{
			return AIMData.GetAllCFCNutrientAimDatabaseIDWithOrMissingXref(isActive, aimDatabaseID, isXref);
		}

		public static List<CFCNutrientInfo> GetAllCFCNutrientInitialLoadAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			return AIMData.GetAllCFCNutrientInitialLoadAimDatabaseID(isActive, aimDatabaseID);
		}

		public static List<AIMNutrientInfo> GetAllAIMNutrient(Boolean isActive)
		{
			return AIMData.GetAllAIMNutrient(isActive);
		}

		//6/5/24
		public static List<AIMMaterialInfo> GetAllAIMMaterialWithOrMissingXref(Int32 materialTypeID, Boolean isActive, Boolean isXref)
		{
			return AIMData.GetAllAIMMaterialWithOrMissingXref(materialTypeID, isActive, isXref);
		}

		//1/5/22
		public static List<AIMNutrientInfo> GetAllAIMNutrientWithOrMissingXref(Boolean isActive, Boolean isXref)
		{
			return AIMData.GetAllAIMNutrientWithOrMissingXref(isActive, isXref);
		}

		public static List<AIMNutrientRefAnalyzerNutrientInfo> GetAllAIMNutrientRefAnalyzerNutrient()
		{
			return AIMData.GetAllAIMNutrientRefAnalyzerNutrient();
		}
		public static List<AIMSupplierRefAnalyzerSupplierInfo> GetAllAIMSupplierRefAnalyzerSupplier()
		{
			return AIMData.GetAllAIMSupplierRefAnalyzerSupplier();
		}
		public static List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> GetAllAIMSupplierLocationRefAnalyzerSupplierLocation()
		{
			return AIMData.GetAllAIMSupplierLocationRefAnalyzerSupplierLocation();
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAllAIMMaterialRefAnalyzerMaterial()
		{
			return AIMData.GetAllAIMMaterialRefAnalyzerMaterial();
		}

		public static List<CFCIngredientRefAIMMaterialInfo> GetCFCIngredientRefAIMMaterial()
		{
			return AIMData.GetCFCIngredientRefAIMMaterial();
		}

		//11/30/23
		public static List<CFCIngredientNutrientOverlayInfo> GetCFCIngredientNutrientOverlay()
		{
			return AIMData.GetCFCIngredientNutrientOverlay();
		}

		//6/4/24
		public static List<CFCProductArchiveInfo> GetCFCProductArchive_ProductID(Int32 cfcProductID, Int32 isActive)
		{
			return AIMData.GetCFCProductArchive_ProductID(cfcProductID, isActive);
		}

		public static CFCIngredientRefAIMMaterialInfo GetCFCIngredientRefAIMMaterial(Int32 cfcIngredientID)
		{
			return AIMData.GetCFCIngredientRefAIMMaterial(cfcIngredientID);
		}
		public static CFCIngredientRefAIMMaterialInfo GetCFCIngredientRefAIMMaterialAimMaterialID(Int32 aimMaterialID)
		{
			return AIMData.GetCFCIngredientRefAIMMaterialAimMaterialID(aimMaterialID);
		}


		public static CFCNutrientRefAIMNutrientInfo GetCFCNutrientRefAIMNutrient(Int32 cfcNutrientID)
		{
			return AIMData.GetCFCNutrientRefAIMNutrient(cfcNutrientID);
		}

		public static CFCNutrientRefAIMNutrientInfo GetCFCNutrientRefAIMNutrientAIMID(Int32 aimNutrientID)
		{
			return AIMData.GetCFCNutrientRefAIMNutrientAIMID(aimNutrientID);
		}

		//2/6/18
		public static ErrorInfo  UpdateQueryOperationIDIsAnd(Int32 queryTemplateID, Int32 queryFieldID, Int32 queryOperationID, Boolean isAnd, Int32 cfcUserID)
		{
			return AIMData.UpdateQueryOperationIDIsAnd(queryTemplateID, queryFieldID, queryOperationID, isAnd, cfcUserID);
		}

		//10/14/22   
		public static ErrorInfo  UpdateQueryTemplateDetailNutrient(Int32 queryTemplateID, Int32 driverAIMNutrientID)
		{
			return AIMData.UpdateQueryTemplateDetailNutrient(queryTemplateID, driverAIMNutrientID);
		}

		//6/1/22
		public static ErrorInfo  UpdateQueryTemplateDetailOrderBy(Int32 queryTemplateDetailID, Int32 orderBy, Int32 cfcUserID)
		{
			return AIMData.UpdateQueryTemplateDetailOrderBy(queryTemplateDetailID, orderBy, cfcUserID);
		}

		//6/8/22
		public static ErrorInfo  UpdateTemplateDetailQueryFieldID(Int32 queryTemplateDetailID, Int32 queryFieldID, Int32 cfcUserID)
		{
			return AIMData.UpdateTemplateDetailQueryFieldID(queryTemplateDetailID, queryFieldID, cfcUserID);
		}

		public static CFCPlantRefAIMOwnerLocationInfo GetCFCPlantRefAIMOwnerLocation(Int32 cfcPlantID)
		{
			return AIMData.GetCFCPlantRefAIMOwnerLocation(cfcPlantID);
		}

		public static CFCProductRefAIMMaterialInfo GetCFCProductRefAIMMaterial(Int32 cfcProductID)
		{
			return AIMData.GetCFCProductRefAIMMaterial(cfcProductID);
		}

		//3/16/21
		public static List<CFCProductRefAIMMaterialInfo> GetCFCProductRefAIMMaterial_AIMMaterialID(Int32 aimMaterialID)
		{
			return AIMData.GetCFCProductRefAIMMaterial_AIMMaterialID(aimMaterialID);
		}


		public static CFCProductRefAIMMaterialInfo GetCFCProductRefAIMMaterialAimMaterialID(Int32 aimMaterialID)
		{
			return AIMData.GetCFCProductRefAIMMaterialAimMaterialID(aimMaterialID);
		}

		//3/15/21
		public static CFCProductRefAIMMaterialInfo GetCFCProductRefAIMMaterialXRefID(Int32 ID)
		{
			return AIMData.GetCFCProductRefAIMMaterialXRefID(ID);
		}



		public static List<CFCNutrientRefAIMNutrientInfo> GetAllCFCNutrientRefAIMNutrient()
		{
			return AIMData.GetAllCFCNutrientRefAIMNutrient();
		}

		public static List<CFCNutrientRefAIMNutrientInfo> GetAllCFCNutrientRefAIMNutrientAimDatabase(Int32 aimDatabaseID)
		{
			return AIMData.GetAllCFCNutrientRefAIMNutrientAimDatabase(aimDatabaseID);
		}

		public static List<ReportInfo> GetAllReport()
		{
			return AIMData.GetAllReport();
		}

		//9/1/23
		public static List<ReportCommentsInfo> GetReportComments(Int32 reportID)
		{
			return AIMData.GetReportComments(reportID);
		}

		//5/31/22
		public static List<ReportInfo> GetAllReport(Boolean isActive, Boolean isAdvanced)
		{
			return AIMData.GetAllReport(isActive, isAdvanced);
		}

		//9/5/23									   
		public static List<ReportInfo> GetAllReportsAllowingComments(Boolean isActive, Boolean hasComments)
		{
			return AIMData.GetAllReportsAllowingComments(isActive, hasComments);
		}
		public static List<ReportInfo> GetAllReportAnalysis()
		{
			return AIMData.GetAllReportAnalysis();
		}

		public static List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeReportData(ref AimSourceLocationGuaranteeReportInfo guarantee)
		{
			return AIMData.GetSourceLocationGuaranteeReportData(guarantee);
		}

		//1/19/23 - commented, not used
		////4/20/18
		//public static List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeReportData_Product(ref AimSourceLocationGuaranteeReportInfo guarantee)
		//{
		//	return AIMData.GetSourceLocationGuaranteeReportData_Product(guarantee);
		//}


		public static List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeReportData(Int32 batchID, String sampleCode, Int32 status, Int32 flag)
		{
			return AIMData.GetSourceLocationGuaranteeReportData(batchID, sampleCode, status, flag);
		}


		//1/19/23 - commented, not used currently
		//public static List<AimSourceLocationGuaranteeReportInfo> GetSourceLocationGuaranteeSpecificNutrientsReportData(ref AimSourceLocationGuaranteeReportInfo guarantee)
		//{
		//	return AIMData.GetSourceLocationGuaranteeSpecificNutrientsReportData(guarantee);
		//}

		public static List<AimNutrientRefAnalyzerNutrientOrphanInfo> GetAimAnalyzerNutrientRefReportData()
		{
			return AIMData.GetAimAnalyzerNutrientRefReportData();
		}

		public static List<ChartInfo> GetTrendChartData(ref ChartInfo chart)
		{
			return AIMData.GetTrendChartData(chart);
		}

		//5/22/20
		public static List<ChartInfo> GetDataSourceComparisonChartData(ref ChartInfo chart)
		{
			return AIMData.GetDataSourceComparisonChartData(chart);
		}

		public static List<ChartInfo> GetAdvancedAnalysisChartData(ref ChartInfo chart)
		{
			return AIMData.GetAdvancedAnalysisChartData(chart);
		}

		public static List<ChartInfo> GetSupplierCompareChartData(ref ChartInfo chart)
		{
			return AIMData.GetSupplierCompareChartData(chart);
		}

		public static List<AimMaterialRefAnalyzerMaterialOrphanInfo> GetAimAnalyzerMaterialRefReportData()
		{
			return AIMData.GetAimAnalyzerMaterialRefReportData();
		}

		//4/15/22
		public static List<AimMaterialRefAnalyzerMaterialOrphanInfo> GetMaterialOrphanReportData(Int32 reportNumber)
		{
			return AIMData.GetMaterialOrphanReportData(reportNumber);
		}

		public static List<ManufacturedProductComplianceReportInfo> GetManufacturedProductComplianceReportData(Int32 dataSourceTypeID, DateTime toDate, DateTime fromDate)
		{
			return AIMData.GetManufacturedProductComplianceReportData(dataSourceTypeID, toDate, fromDate);
		}

		public static List<ManufacturedProductComplianceReportInfo> GetManufacturedProductComplianceViolations(Int32 dataSourceTypeID, DateTime toDate, DateTime fromDate)
		{
			return AIMData.GetManufacturedProductComplianceViolations(dataSourceTypeID, toDate, fromDate);
		}

		public static List<SampleComparisonReportInfo> GetSampleComparison(ref SampleComparisonReportInfo sampleResult)
		{
			return AIMData.GetSampleComparison(sampleResult);
		}

		public static List<SampleActivityReportInfo> GetSampleActivity(ref SampleActivityReportInfo activity)
		{
			return AIMData.GetSampleActivity(activity);
		}

		//4/26/22
		public static List<SampleActivityReportInfo> GetSampleActivityQualityControl(ref SampleActivityReportInfo activity)
		{
			return AIMData.GetSampleActivityQualityControl(activity);
		}

		//4/25/18
		public static List<ReportAlternateNamesInfo> GetReportAlternateNames()
		{
			return AIMData.GetReportAlternateNames();
		}

		//1/6/20
		public static List<ReportAlternateNamesInfo> GetReportAlternateNamesForImport()
		{
			return AIMData.GetReportAlternateNamesForImport();
		}

		//6/15/23		
		public static List<ReportAlternateNamesInfo> GetReportAlternateNamesForUserDefinedSamplePivotReport()
		{
			return AIMData.GetReportAlternateNamesForUserDefinedSamplePivotReport();
		}

		//2/16/21
		//public static List<SampleCustomReportInfo> GetSampleCustom(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID)
		//{
		//    return AIMData.GetSampleCustom(fromDate, toDate, materialTypeID, aimOwnerLocationID);
		//}
		//Task1076 DB250711 public static List<SampleCustomReportInfo> GetSampleCustom(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID, Boolean groupMaterials)
        public static List<SampleCustomReportInfo> GetSampleCustom(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID, Boolean groupMaterials, Int32 sampleResultOption)	//Task1076 DB250711 
        {
            //Task1076 DB250711 return AIMData.GetSampleCustom(fromDate, toDate, materialTypeID, aimOwnerLocationID, groupMaterials);
            return AIMData.GetSampleCustom(fromDate, toDate, materialTypeID, aimOwnerLocationID, groupMaterials, sampleResultOption);	//Task1076 DB250711 
        }

        //TASK951 6/3/24
        public static List<ExportBatchLogInfo> GetExportBatchLog(DateTime fromDate, DateTime toDate)
		{
			return AIMData.GetExportBatchLog(fromDate, toDate);
		}




        //2/16/21
        //public static List<SampleCustomReportInfo> GetSampleCustomProduct(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID)		
        //{
        //    return AIMData.GetSampleCustomProduct(fromDate, toDate, materialTypeID, aimOwnerLocationID);
        //}		
        //Task1076 DB250711 public static List<SampleCustomReportInfo> GetSampleCustomProduct(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID, Boolean groupMaterials)
        public static List<SampleCustomReportInfo> GetSampleCustomProduct(DateTime fromDate, DateTime toDate, Int32 materialTypeID, Int32 aimOwnerLocationID, Boolean groupMaterials, Int32 sampleResultType)
        {
            //Task1076 DB250711 return AIMData.GetSampleCustomProduct(fromDate, toDate, materialTypeID, aimOwnerLocationID, groupMaterials);
            return AIMData.GetSampleCustomProduct(fromDate, toDate, materialTypeID, aimOwnerLocationID, groupMaterials, sampleResultType);		//Task1076 DB250711
        }

		//2/12/19
		public static List<SampleResultsBySupplierInfo> GetSampleResultsBySupplier(ref SampleResultsBySupplierInfo results)
		{
			return AIMData.GetSampleResultsBySupplier(results);
		}

		//6/27/22
		public static List<SampleHeaderInfo> GetSamplesByOwnerLoc(ref SampleHeaderInfo results)
		{
			return AIMData.GetSamplesByOwnerLoc(results);
		}

		public static List<ExportBatchMaintenanceReportInfo> GetExportBatchDetailReportData(Int32 updateIngredientHeaderID, Int32 targetID)
		{
			return AIMData.GetExportBatchDetailReportData(updateIngredientHeaderID, targetID);
		}

		public static List<ExportBatchMaintenanceReportInfo> GetExportBatchDetailReportData()
		{
			return AIMData.GetExportBatchDetailReportData();
		}


		public static List<AnalysisReportsInfo> GetAnalysisSummary(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)
		{
			return AIMData.GetAnalysisSummary(analysisSummary, aimDataBaseID);
		}

		//2/16/21
		//public static List<AnalysisReportsInfo> GetAnalysisPivotData(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)
		//{
		//    return AIMData.GetAnalysisPivotData(analysisSummary, aimDataBaseID);
		//}
		public static List<AnalysisReportsInfo> GetAnalysisPivotData(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)
		{
			return AIMData.GetAnalysisPivotData(analysisSummary, aimDataBaseID, groupMaterials);
		}

		public static List<AnalysisReportsInfo> GetAnalysisDetail(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)
		{
			return AIMData.GetAnalysisDetail(analysisSummary, aimDataBaseID);
		}

		//4/19/18
		public static List<AnalysisReportsInfo> GetAnalysisDetail_Product(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)
		{
			return AIMData.GetAnalysisDetail_Product(analysisSummary, aimDataBaseID);
		}

		//2/16/21
		//public static List<AnalysisReportsInfo> GetAnalysisPivotData_Product(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)
		//{
		//    return AIMData.GetAnalysisPivotData_Product(analysisSummary, aimDataBaseID);
		//}
		public static List<AnalysisReportsInfo> GetAnalysisPivotData_Product(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)
		{
			return AIMData.GetAnalysisPivotData_Product(analysisSummary, aimDataBaseID, groupMaterials);
		}


		//6/20/23
		public static List<AnalysisReportsInfo> GetAnalysisPivotDataWithoutSampleAIM(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)
		{
			return AIMData.GetAnalysisPivotDataWithoutSampleAIM(analysisSummary, aimDataBaseID, groupMaterials);
		}

		//6/26/23
		public static List<AnalysisReportsInfo> GetAnalysisPivotData_ProductWithoutSampleAIM(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID, Boolean groupMaterials)
		{
			return AIMData.GetAnalysisPivotData_ProductWithoutSampleAIM(analysisSummary, aimDataBaseID, groupMaterials);
		}

		public static List<AnalysisReportsInfo> GetAnalysisDetail(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			return AIMData.GetAnalysisDetail(queryTemplateID, aimDatabaseID);
		}

		//4/20/18
		public static List<AnalysisReportsInfo> GetAnalysisDetail_Product(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			return AIMData.GetAnalysisDetail_Product(queryTemplateID, aimDatabaseID);
		}

		// Version 1 of report
		public static List<AnalysisReportsInfo> GetAnalysisPivot(ref AnalysisReportsInfo analysisSummary, Boolean dryMatterBase, String aimMaterialList, String aimMaterialList2, String aimMaterialList3)
		{
			return AIMData.GetAnalysisPivot(analysisSummary, dryMatterBase, aimMaterialList, aimMaterialList2, aimMaterialList3);
		}

		//1/19/21 - Version 2 of report
		public static List<AnalysisReportsInfo> GetAnalysisPivotOwnerLocation(ref AnalysisReportsInfo analysisSummary, Boolean dryMatterBase, String aimMaterialList, String aimMaterialList2, String aimMaterialList3)
		{
			return AIMData.GetAnalysisPivotOwnerLocation(analysisSummary, dryMatterBase, aimMaterialList, aimMaterialList2, aimMaterialList3);
		}

		//1/18/21 - Version 3 of report
		public static List<AnalysisReportsInfo> GetAnalysisPivotSupplierLocation(ref AnalysisReportsInfo analysisSummary, Boolean dryMatterBase, String aimMaterialList, String aimMaterialList2, String aimMaterialList3)
		{
			return AIMData.GetAnalysisPivotSupplierLocation(analysisSummary, dryMatterBase, aimMaterialList, aimMaterialList2, aimMaterialList3);
		}

		public static List<AnalysisReportsInfo> GetAnalysisSourceComparison(ref AnalysisReportsInfo analysisSummary)
		{
			return AIMData.GetAnalysisSourceComparison(analysisSummary);
		}

		public static List<AnalysisReportsInfo> GetAnalysisSourceComparison(Int32 queryTemplateID)
		{
			return AIMData.GetAnalysisSourceComparison(queryTemplateID);
		}

		//5/20/22
		public static List<SampleResultsComparisonReportInfo> GetSampleResultsComparison(Int32 queryTemplateID, Int32 datasourcetypeID1, Int32 datasourcetypeID2, Boolean useAnalysisDate)
		{
			return AIMData.GetSampleResultsComparison(queryTemplateID, datasourcetypeID1, datasourcetypeID2, useAnalysisDate);
		}

		//6/22/22
		public static List<DataSourceComparisonInfo> GetDataSourceComparison(Int32 queryTemplateID, Int32 datasourcetypeID1, Int32 datasourcetypeID2, Boolean useAnalysisDate
				, Int32 DS1_Nut1, Int32 DS1_Nut2, Int32 DS1_Nut3
				, Int32 DS2_Nut1, Int32 DS2_Nut2, Int32 DS2_Nut3

				//10/14/22
				, Int32 driverAIMNutrientID)
		{
			return AIMData.GetDataSourceComparison(queryTemplateID, datasourcetypeID1, datasourcetypeID2, useAnalysisDate,
				DS1_Nut1, DS1_Nut2, DS1_Nut3, DS2_Nut1, DS2_Nut2, DS2_Nut3, driverAIMNutrientID);
		}

		public static List<AnalysisReportsInfo> GetAnalysisSummary(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			return AIMData.GetAnalysisSummary(queryTemplateID, aimDatabaseID);
		}

		//4/20/18
		public static List<AnalysisReportsInfo> GetAnalysisSummary_Product(Int32 queryTemplateID, Int32 aimDatabaseID)
		{
			return AIMData.GetAnalysisSummary_Product(queryTemplateID, aimDatabaseID);
		}

		//4/20/18
		public static List<AnalysisReportsInfo> GetAnalysisSummary_Product(ref AnalysisReportsInfo analysisSummary, Int32 aimDataBaseID)
		{
			return AIMData.GetAnalysisSummary_Product(analysisSummary, aimDataBaseID);
		}

		public static List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison(ref AnalysisReportsInfo analysisSummary)
		{
			return AIMData.GetAnalysisSummaryForAnalysisDateComparison(analysisSummary);
		}

		//4/20/18
		public static List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison_Product(ref AnalysisReportsInfo analysisSummary)
		{
			return AIMData.GetAnalysisSummaryForAnalysisDateComparison_Product(analysisSummary);
		}

		public static List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison(Int32 queryTemplateID)
		{
			return AIMData.GetAnalysisSummaryForAnalysisDateComparison(queryTemplateID);
		}

		//4/20/18
		public static List<AnalysisReportsInfo> GetAnalysisSummaryForAnalysisDateComparison_Product(Int32 queryTemplateID)
		{
			return AIMData.GetAnalysisSummaryForAnalysisDateComparison_Product(queryTemplateID);
		}


		public static List<SampleResultInfo> GetAllSampleResultForImport(Int32 sampleID)
		{
			return AIMData.GetAllSampleResultForImport(sampleID);
		}

		public static List<SampleQualityControlInfo> GetSampleQualityControl(Int32 sampleID)
		{
			return AIMData.GetSampleQualityControl(sampleID);
		}

		public static SampleQualityControlInfo GetSampleQualityControlSampleIDNutrientID(Int32 sampleID, Int32 aimNutrientID)
		{
			return AIMData.GetSampleQualityControlSampleIDNutrientID(sampleID, aimNutrientID);
		}

		//3/7/18
		public static ErrorInfo  SaveSampleResultUsingEquation(Int32 sampleID, Int32 cfcUserID, Int32 aimDatabaseID)
		{
			return AIMData.SaveSampleResultUsingEquation(sampleID, cfcUserID, aimDatabaseID);
		}




		public static List<SampleResultInfo> GetAllSampleResult(Int32 sampleID, Boolean isActive, Int32 sampleResultMessageType)
		{
			return AIMData.GetAllSampleResult(sampleID, isActive, sampleResultMessageType);
		}
		public static List<SampleResultInfo> GetAllSampleResult(Int32 sampleID)
		{
			return AIMData.GetAllSampleResult(sampleID);
		}
		public static List<SampleResultInfo> GetAllSampleResult(Boolean isActive)
		{
			return AIMData.GetAllSampleResult(isActive);
		}

		//1/9/18
		public static List<SampleQualityInfo> GetAllSampleResultQualityControl(Int32 sampleID, Boolean isActive)
		{
			return AIMData.GetAllSampleResultQualityControl(sampleID, isActive);
		}
		//1/9/18
		public static List<SampleQualityInfo> GetAllSampleResultQualityControl(Int32 sampleID)
		{
			return AIMData.GetAllSampleResultQualityControl(sampleID);
		}

		public static List<SampleResultInfo> GetSampleResultForSQLResultID(Int32 sampleID, String sqlResultID)
		{
			return AIMData.GetSampleResultForSQLResultID(sampleID, sqlResultID);
		}

		public static List<SampleResultTemplateDetailInfo> GetSampleResultTemplateDetail(Int32 templateID)
		{
			return AIMData.GetSampleResultTemplateDetail(templateID);
		}

		//4/19/22		
		public static ErrorInfo  GetQualityControlMassEntryDetailSortOrder(Int32 sampleQualityControlMassEntryID, Int32 sortOrder)
		{
			return AIMData.GetQualityControlMassEntryDetailSortOrder(sampleQualityControlMassEntryID, sortOrder);
		}


		//1/4/18
		public static List<SampleQualityControlMassEntryDetailInfo> GetSampleQualityControlMassEntryDetail(Int32 ID)
		{
			return AIMData.GetSampleQualityControlMassEntryDetail(ID);
		}


		public static ErrorInfo  GetSampleResultTemplateDetailMaxSortNo(Int32 templateID)
		{
			return AIMData.GetSampleResultTemplateDetailMaxSortNo(templateID);
		}

		//1/28/20
		public static List<AlternateNameInfo> GetAlternateImportFieldName(Int32 defID, String mapColumnName)
		{
			return AIMData.GetAlternateImportFieldName(defID, mapColumnName);
		}

		//1/28/20
		public static MapColumnNamesInfo GetOriginalMapColumnName(Int32 defID, String altName)
		{
			return AIMData.GetOriginalMapColumnName(defID, altName);
		}

		//12/22/20

		public static MapColumnNamesInfo GetMapColumnName(Int32 mapID)
		{
			return AIMData.GetMapColumnName(mapID);
		}

		//1/4/18
		public static ErrorInfo  GetSampleQualityControlDetailMaxSortNo(Int32 massID)
		{
			return AIMData.GetSampleQualityControlDetailMaxSortNo(massID);
		}

		public static ErrorInfo  RunSqlScript(String str)
		{
			return AIMData.RunSqlScript(str);
		}

		//6/27/19	
		//public static ErrorInfo  RePopulateSampleAimTable(Int32 progressBarID, Int32 cfcUserID)
		//{
		//	return AIMData.RePopulateSampleAIMTable(progressBarID, cfcUserID);
		//}
		public static ErrorInfo  RePopulateSampleAimTable(Int32 progressBarID, Int32 cfcUserID, String rebuildDate)
		{
			return AIMData.RePopulateSampleAIMTable(progressBarID, cfcUserID, rebuildDate);
		}

		//8/15/22   			
		public static ErrorInfo  RomerImportInsertTemplateRecords(Int32 defID, Boolean isComposite)
		{
			return AIMData.RomerImportInsertTemplateRecords(defID, isComposite);
		}

		//5/9/18
		//11/4/22 - added rebuildDate
		public static ErrorInfo  ReCalculateEquations(Int32 progressBarID, Int32 cfcUserID, Int32 aimDatabaseID, String rebuildDate)
		{
			return AIMData.ReCalculateEquations(progressBarID, cfcUserID, aimDatabaseID, rebuildDate);
		}


		public static List<SampleResultInfo> GetAllSampleResultSampleIDDataSourceTypeID(Int32 sampleID, Int32 dataSourceTypeID, Boolean isActive, Int32 sampleResultMessageType)
		{
			return AIMData.GetAllSampleResultSampleIDDataSourceTypeID(sampleID, dataSourceTypeID, isActive, sampleResultMessageType);
		}

		public static List<SampleResultInfo> GetAllSampleResultSampleIDDataSourceTypeID(Int32 sampleID, Int32 dataSourceTypeID, Int32 sampleResultMessageType)
		{
			return AIMData.GetAllSampleResultSampleIDDataSourceTypeID(sampleID, dataSourceTypeID, sampleResultMessageType);
		}

		//1/9/18
		public static List<SampleQualityInfo> GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(Int32 sampleID, Int32 dataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(sampleID, dataSourceTypeID, isActive);
		}
		//1/9/18
		public static List<SampleQualityInfo> GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(Int32 sampleID, Int32 dataSourceTypeID)
		{
			return AIMData.GetAllSampleResultSampleIDDataSourceTypeID_QualityControl(sampleID, dataSourceTypeID);
		}

		public static List<SampleResultInfo> GetAllSampleResultDataSourceTypeID(Int32 dataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAllSampleResultDataSourceTypeID(dataSourceTypeID, isActive);
		}



		public static List<SampleResultPredictInfo> GetAllSampleWarning(Int32 sampleID, Int32 sampleResultMessageType)
		{
			return AIMData.GetAllSampleWarning(sampleID, sampleResultMessageType);
		}

		public static List<SampleMessageDetailInfo> GetSampleMessageDetailOutside(Int32 sampleID, Int32 sampleResultPredictID, Int32 sampleResultMessageType)
		{
			return AIMData.GetSampleMessageDetailOutside(sampleID, sampleResultPredictID, sampleResultMessageType);
		}

		public static List<SampleMessageDetailInfo> GetSampleMessageDetailOutsideAnalyzerNutrientID(Int32 sampleID, Int32 analyzerNutrientID, Int32 sampleResultMessageType)
		{
			return AIMData.GetSampleMessageDetailOutsideAnalyzerNutrientID(sampleID, analyzerNutrientID, sampleResultMessageType);
		}


		public static List<SampleMessageDetailInfo> GetSampleMessageDetailInside(Int32 sampleID, Int32 sampleResultPredictID, Int32 sampleResultMessageType)
		{
			return AIMData.GetSampleMessageDetailInside(sampleID, sampleResultPredictID, sampleResultMessageType);
		}

		public static List<SampleMessageDetailInfo> GetSampleMessageDetailInsideAnalyzerNutrientID(Int32 sampleID, Int32 analyzerNutrientID, Int32 sampleResultMessageType, Boolean IsActive)
		{
			return AIMData.GetSampleMessageDetailInsideAnalyzerNutrientID(sampleID, analyzerNutrientID, sampleResultMessageType, IsActive);
		}

		public static PrinterUserDefaultInfo GetUserPrinterDefault(Int32 cfcUserID, Int32 printerTypeID)
		{
			return AIMData.GetUserPrinterDefault(cfcUserID, printerTypeID);
		}

		public static PrinterUserDefaultInfo GetUserPrinterDefault(Int32 printerTypeID)
		{
			return AIMData.GetUserPrinterDefault(printerTypeID);
		}

		//11/19/24
        public static List<PrinterUserDefaultInfo> GetPrinterList(Int32 printerTypeID)
        {
            return AIMData.GetPrinterList(printerTypeID);
        }

        public static PrinterDefaultInfo GetPrinterDefault(Int32 PrinterTypeID)
		{
			return AIMData.GetPrinterDefault(PrinterTypeID);
		}

		//6/15/18
		//public static PrinterTypeInfo GetOnePrinterType(Int32 PrinterTypeID)
		//{
		//	return AIMData.GetOnePrinterType(PrinterTypeID);
		//}
		public static PrinterTypeInfo GetOnePrinterType(Int32 PrinterTypeID, Int32 cfcUserID)
		{
			return AIMData.GetOnePrinterType(PrinterTypeID, cfcUserID);
		}

		public static ReportInfo GetReportReportID(Int32 reportID)
		{
			return AIMData.GetReportReportID(reportID);
		}

		public static ReportQueryInfo GetOneReportQuery(Int32 reportQuery)
		{
			return AIMData.GetOneReportQuery(reportQuery);
		}

		public static List<PrinterTypeInfo> GetPrinterType()
		{
			return AIMData.GetPrinterType();
		}

		public static List<PrinterTypeInfo> GetPrinterTypeFromDefaultPrinters()
		{
			return AIMData.GetPrinterTypeFromDefaultPrinters();
		}
		        
        public static List<PrinterDefaultInfo> GetPrinterDefaultAll()
		{
			return AIMData.GetPrinterDefaultAll();
		}

		public static List<PrinterUserDefaultInfo> GetPrinterUserDefaultAll()
		{
			return AIMData.GetPrinterUserDefaultAll();
		}


		public static List<EmailAddressInfo> GetEmailAddress()
		{
			return AIMData.GetEmailAddress();
		}

		public static List<AIMSupplierInfo> GetSupplier(Boolean IsActive)
		{
			return AIMData.GetSupplier(IsActive);
		}


		//8/30/23
		public static List<AIMSupplierInfo> GetSupplier(Boolean IsActive, Int32 AIMSupplierGroupID)
		{
			return AIMData.GetSupplier(IsActive, AIMSupplierGroupID);
		}

		//8/30/23
		public static List<AIMSupplierLocationInfo> GetSupplierLocationForSupplier(Int32 aimSupplierID, Int32 aimSupplierGroupID, Int32 aimSupplierLocationGroupID)
		{
			return AIMData.GetSupplierLocationForSupplier(aimSupplierID, aimSupplierGroupID, aimSupplierLocationGroupID);
		}


		//10/17/23
		//public static List<AIMSupplierInfo> GetSupplierWithMaterials(Boolean IsActive, Int32 aimMaterialID)
		//{
		//	return AIMData.GetSupplierWithMaterials(IsActive, aimMaterialID);
		//}
		public static List<AIMSupplierInfo> GetSupplierWithMaterials(Boolean IsActive, Int32 groupID, Int32 aimMaterialID)
		{
			return AIMData.GetSupplierWithMaterials(IsActive, groupID, aimMaterialID);
		}

		//6/5/20
		public static List<AIMSupplierInfo> GetSupplierWithMaterialCodes(Boolean IsActive, String aimMaterialCode)
		{
			return AIMData.GetSupplierWithMaterialCodes(IsActive, aimMaterialCode);
		}

		//12/19/18
		public static List<AIMSupplierInfo> GetSupplierForUtility(Boolean IsActive)
		{
			return AIMData.GetSupplierForUtility(IsActive);
		}

		public static AIMSupplierInfo GetOneSupplier(Int32 aimSourceID)
		{
			return AIMData.GetOneSupplier(aimSourceID);
		}

		//8/30/23
		public static List<AIMSupplierInfo> GetAimSupplier(Int32 aimSourceID)
		{
			return AIMData.GetAimSupplier(aimSourceID);
		}


		//-----------------------------------------------------------------
		public static AIMSupplierInfo GetOneSupplier(String partialName, Int32 num)
		{
			return AIMData.GetOneSupplier(partialName, num);
		}
		public static AIMSupplierInfo GetSupplierSupplierName(String supplierName)
		{
			return AIMData.GetSupplierSupplierName(supplierName);
		}

		public static AIMSupplierInfo GetSupplierSupplierCode(String supplierCode)
		{
			return AIMData.GetSupplierSupplierCode(supplierCode);
		}

		//6/18/20
		public static AIMMaterialStockStatusInfo GetAIMMaterialStockStatus(Int32 materialID, Int32 ownerLocationID)
		{
			return AIMData.GetAIMMaterialStockStatus(materialID, ownerLocationID);
		}

		//7/2/20
		public static AIMMaterialStockStatusInfo GetAIMMaterialStockStatus(Int32 materialID)
		{
			return AIMData.GetAIMMaterialStockStatus(materialID);
		}

		public static AIMOwnerInfo GetOwnerName(String ownerName)
		{
			return AIMData.GetOwnerName(ownerName);
		}

		public static AIMOwnerInfo GetAIMOwnerCode(String ownerCode)
		{
			return AIMData.GetAIMOwnerCode(ownerCode);
		}

		public static AIMMaterialInfo GetAIMMaterialName(String materialName)
		{
			return AIMData.GetAIMMaterialName(materialName);
		}

		//7/2/20 - added active status
		public static AIMMaterialInfo GetAIMMaterial(Int32 materialID, Boolean isActive)
		{
			return AIMData.GetAIMMaterial(materialID, isActive);
		}

		public static AIMSupplierLocationInfo GetSupplierLocationSupplierIDLocationName(Int32 aimSupplierID, String supplierLocationName)
		{
			return AIMData.GetSupplierLocationSupplierIDLocationName(aimSupplierID, supplierLocationName);
		}

		public static AIMSupplierLocationInfo GetSupplierLocationSupplierIDLocationCode(Int32 aimSupplierID, String supplierLocationCode)
		{
			return AIMData.GetSupplierLocationSupplierIDLocationCode(aimSupplierID, supplierLocationCode);
		}

		public static AIMOwnerLocationInfo GetOwnerLocationOwnerIDLocationName(Int32 aimOwnerID, String ownerLocationName)
		{
			return AIMData.GetOwnerLocationOwnerIDLocationName(aimOwnerID, ownerLocationName);
		}

		public static AIMOwnerLocationInfo GetOwnerLocationOwnerIDLocationCode(Int32 aimOwnerID, String ownerLocationCode)
		{
			return AIMData.GetOwnerLocationOwnerIDLocationCode(aimOwnerID, ownerLocationCode);
		}

		public static AIMSupplierLocationInfo GetOneSupplierLocation(String partialName, Int32 num)
		{
			return AIMData.GetOneSupplierLocation(partialName, num);
		}
		public static AIMOwnerInfo GetOneOwner(String partialName, Int32 num)
		{
			return AIMData.GetOneOwner(partialName, num);
		}
		public static AIMOwnerLocationInfo GetOneOwnerLocation(String partialName, Int32 num)
		{
			return AIMData.GetOneOwnerLocation(partialName, num);
		}
		//-----------------------------------------------------------------

		public static List<AIMSupplierInfo> GetAllSupplier()
		{
			return AIMData.GetAllSupplier();
		}

		public static List<AIMSupplierLocationInfo> GetAllSupplierLocation(Boolean IsActive)
		{
			return AIMData.GetAllSupplierLocation(IsActive);
		}

		//7/26/19
		public static List<AIMSupplierLocationGroupedInfo> GetAllSupplierLocationGrouped(Boolean IsActive)
		{
			return AIMData.GetAllSupplierLocationGrouped(IsActive);
		}



		public static List<AIMSupplierLocationInfo> GetSupplierLocationForSupplier(Int32 aimSupplierID)
		{
			return AIMData.GetSupplierLocationForSupplier(aimSupplierID);
		}

		//12/19/18
		public static List<AIMSupplierLocationInfo> GetSupplierLocationForSupplierForUtility(Int32 aimSupplierID)
		{
			return AIMData.GetSupplierLocationForSupplierForUtility(aimSupplierID);
		}

		//12/5/19
		public static List<AIMOwnerLocationInfo> GetOwnerLocationForOwnerForUtility(Int32 aimOwnerID)
		{
			return AIMData.GetOwnerLocationForOwnerForUtility(aimOwnerID);
		}

		public static List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation(Int32 datasourceTypeID, Int32 analyzerSupplierID)
		{
			return AIMData.GetAnalyzerSupplierLocation(datasourceTypeID, analyzerSupplierID);
		}

		public static UserDefinedDataSourceTemplateInfo GetMapColumnName(Int32 userDefinedDataSourceDefinitionID, String datasourceColumnName)
		{
			return AIMData.GetMapColumnName(userDefinedDataSourceDefinitionID, datasourceColumnName);
		}


		//public static SampleImportColumnNamesInfo GetMapCrossReferenceValue(String columnName, String columnValue)
		//{
		//	return AIMData.GetMapCrossReferenceValue(columnName, columnValue);
		//}

		public static AIMSupplierLocationInfo GetOneSourceLocation(Int32 aimSourceLocationID)
		{
			return AIMData.GetOneSupplierLocation(aimSourceLocationID);
		}

		//3/4/21, parameter not necessary (and actually, this function is not currently used)
		//public static List<AnalysisInfo> GetAnalyst(Boolean IsActive)
		//{
		//	return AIMData.GetAnalysis(IsActive);
		//}
		public static List<AnalysisInfo> GetAnalyst()
		{
			return AIMData.GetAnalysis();
		}


		public static List<AnalysisTemplateInfo> GetActiveAnalysisTemplate(Boolean isActive)
		{
			return AIMData.GetAnalysisTemplate(isActive);
		}

		public static List<QueryFieldInfo> GetQueryField(Boolean isActive)
		{
			return AIMData.GetQueryField(isActive);
		}

		public static List<QueryOperationInfo> GetQueryOperation(Boolean isActive)
		{
			return AIMData.GetQueryOperation(isActive);
		}

		public static List<QueryOperationInfo> GetQueryOperation(QueryFeildEnum queryField)
		{
			return AIMData.GetQueryOperation(queryField);
		}

		//10/14/22
		public static QueryTemplateDetailNutrientInfo GetQueryTemplateDetailNutrient(Int32 queryTemplatelID)
		{
			return AIMData.GetQueryTemplateDetailNutrient(queryTemplatelID);
		}

		public static QueryTemplateDetailNutrientInfo GetQueryTemplateDetailNutrient(Int32 queryTemplatelID, Int32 aimMaterialID, Int32 aimNutrientID)
		{
			return AIMData.GetQueryTemplateDetailNutrient(queryTemplatelID, aimMaterialID, aimNutrientID);
		}

		//6/15/22
		public static QueryTemplateDetailNutrientInfo GetQueryTemplateDetailNutrientDataSource(Int32 queryTemplatelID, Int32 dataSourceTypeID, Int32 aimNutrientID)
		{
			return AIMData.GetQueryTemplateDetailNutrientDataSource(queryTemplatelID, dataSourceTypeID, aimNutrientID);
		}

		public static QueryTemplateInfo GetQueryTemplate(Int32 queryTemplateID)
		{
			return AIMData.GetQueryTemplate(queryTemplateID);
		}

		//10/31/22		
		//public static List<QueryTemplateInfo> GetQueryTemplate()
		//{
		//	return AIMData.GetQueryTemplate();
		//}
		public static List<QueryTemplateInfo> GetQueryTemplate(String ingredientOrProduct)
		{
			return AIMData.GetQueryTemplate(ingredientOrProduct);
		}

		//6/22/22
		public static List<QueryTemplateInfo> GetQueryTemplateForReportsScreen()
		{
			return AIMData.GetQueryTemplateForReportsScreen();
		}

		public static List<QueryTemplateDetailInfo> GetQueryTemplateDetail(Int32 queryTemplateID)
		{
			return AIMData.GetQueryTemplateDetail(queryTemplateID);
		}

		public static List<TemplateMaterialNutrientInfo> GetMaterialNutrientForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetMaterialNutrientForTemplate(queryTemplateID, queryFieldID);
		}

		public static List<QueryTemplateDetailInfo> GetAIMSampleDateForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMSampleDateForTemplate(queryTemplateID, queryFieldID);
		}

		//6/13/22
		public static List<QueryTemplateDetailInfo> GetAIMAnalysisDateForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMAnalysisDateForTemplate(queryTemplateID, queryFieldID);
		}

		public static List<AIMMaterialInfo> GetAIMMaterialForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMMaterialForTemplate(queryTemplateID, queryFieldID);
		}
		public static List<AIMMaterialInfo> GetAIMMaterialForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetAIMMaterialForTemplate(queryTemplateID, queryFieldID, value);
		}

		public static List<CFCIngredientInfo> GetCFCIngredientForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetCFCIngredientForTemplate(queryTemplateID, queryFieldID);
		}

		//1/25/21
		public static List<SampleTypeInfo> GetSampleTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetSampleTypeForTemplate(queryTemplateID, queryFieldID, value);
		}

		//12/21/22
		public static List<CFCProductTypeInfo> GetCFCProductTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetCFCProductTypeForTemplate(queryTemplateID, queryFieldID, value);
		}

		//1/25/21
		public static List<SampleTypeInfo> GetSampleTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetSampleTypeForTemplate(queryTemplateID, queryFieldID);
		}

		//12/21/22
		public static List<CFCProductTypeInfo> GetCFCProductTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetCFCProductTypeForTemplate(queryTemplateID, queryFieldID);
		}


		//5/26/22
		public static List<DataSourceTypeInfo> GetDataSourceTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetDataSourceTypeForTemplate(queryTemplateID, queryFieldID, value);
		}

		//5/26/22
		public static List<DataSourceTypeInfo> GetDataSourceTypeForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetDataSourceTypeForTemplate(queryTemplateID, queryFieldID);
		}

        //6/14/22
        public static List<DataSourceComparisonInfo> GetDataSourceComparisonForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetDataSourceComparisonForTemplate(queryTemplateID, queryFieldID);
		}

		public static List<CFCIngredientInfo> GetCFCIngredientForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetCFCIngredientForTemplate(queryTemplateID, queryFieldID, value);
		}

		public static List<AIMNutrientInfo> GetAIMNutrientForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMNutrientForTemplate(queryTemplateID, queryFieldID);
		}
		public static List<AIMNutrientInfo> GetAIMNutrientForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetAIMNutrientForTemplate(queryTemplateID, queryFieldID, value);
		}

		public static List<AIMOwnerInfo> GetAIMOwnerForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMOwnerForTemplate(queryTemplateID, queryFieldID);
		}
		public static List<AIMOwnerInfo> GetAIMOwnerForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetAIMOwnerForTemplate(queryTemplateID, queryFieldID, value);
		}

		public static List<AIMSupplierInfo> GetAIMSupplierForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMSupplierForTemplate(queryTemplateID, queryFieldID);
		}
		public static List<AIMSupplierInfo> GetAIMSupplierForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetAIMSupplierForTemplate(queryTemplateID, queryFieldID, value);
		}

		public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMOwnerLocationForTemplate(queryTemplateID, queryFieldID);
		}
		public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetAIMOwnerLocationForTemplate(queryTemplateID, queryFieldID, value);
		}

		public static List<AIMSupplierLocationInfo> GetAIMSupplierLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.GetAIMSupplierLocationForTemplate(queryTemplateID, queryFieldID);
		}
		public static List<AIMSupplierLocationInfo> GetAIMSupplierLocationForTemplate(Int32 queryTemplateID, Int32 queryFieldID, Int32 value)
		{
			return AIMData.GetAIMSupplierLocationForTemplate(queryTemplateID, queryFieldID, value);
		}


		public static QueryTemplateInfo GetQueryTemplateName(String name)
		{
			return AIMData.GetQueryTemplateName(name);
		}

		public static DataSourceTypeInfo GetDataSourceUserName(String name)
		{
			return AIMData.GetDataSourceUserName(name);
		}

		//1/22/21
		public static SampleTypeInfo GetSampleTypeName(String name)
		{
			return AIMData.GetSampleTypeName(name);
		}


		public static QueryTemplateDetailInfo GetQueryTemplateDetailForSaveAs(Int32 queryTemplateID)
		{
			return AIMData.GetQueryTemplateDetailForSaveAs(queryTemplateID);
		}

		public static AnalysisTemplateInfo GetOneAnalysisTemplate(Int32 analystTemplateID)
		{
			return AIMData.GetOneAnalysisTemplate(analystTemplateID);
		}
		public static CFCPlantRefAIMOwnerLocationInfo GetCFCPlantAimOwnerLocationCrossReference(Int32 aimOwnerLocationID)
		{
			return AIMData.GetCFCPlantAimOwnerLocationCrossReference(aimOwnerLocationID);
		}

		public static List<CFCPlantInfo> GetCFCPlant(Boolean isActive)
		{
			return AIMData.GetCFCPlant(isActive);
		}


		//3/15/21		
		public static List<CFCProductRefAIMMaterialInfo> GetCFCProductRefAIMMaterial()
		{
			return AIMData.GetCFCProductRefAIMMaterial();
		}

		//3/15/21		
		public static List<CFCProductArchiveSpecInfo> GetAIMNutrientCFCProductArchiveSpec(Int32 cfcProductArchiveID)
		{
			return AIMData.GetAIMNutrientCFCProductArchiveSpec(cfcProductArchiveID);
		}





		public static List<CFCPlantInfo> GetCFCPlantAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			return AIMData.GetCFCPlantAimDatabaseID(isActive, aimDatabaseID);
		}

		//1/5/22
		public static List<CFCPlantInfo> GetCFCPlantAimDatabaseIDWithOrMissingXref(Boolean isActive, Int32 aimDatabaseID, Boolean isXref)
		{
			return AIMData.GetCFCPlantAimDatabaseIDWithOrMissingXref(isActive, aimDatabaseID, isXref);
		}

		public static List<CFCPlantInfo> GetCFCPlant(Int32 aimDatabaseID)
		{
			return AIMData.GetCFCPlant(aimDatabaseID);
		}

		public static CFCPlantInfo Get_CFCPlant(Int32 aimDatabaseID, Int32 cfcplantID)
		{
			return AIMData.Get_CFCPlant(aimDatabaseID, cfcplantID);
		}

		public static CFCPlantInfo GetCFCPlant(String CFCPlantCode)
		{
			return AIMData.GetCFCPlant(CFCPlantCode);
		}

		public static CFCPlantInfo GetCFCPlant(String CFCPlantCode, Int32 AimDatabaseID)
		{
			return AIMData.GetCFCPlant(CFCPlantCode, AimDatabaseID);
		}

		public static List<AIMDatabaseInfo> GetAIMDatabase(Boolean isActive)
		{
			return AIMData.GetAIMDatabase(isActive);
		}

		public static AIMDatabaseInfo GetAIMDatabase(Int32 AIMDatabaseID)
		{
			return AIMData.GetAIMDatabase(AIMDatabaseID);
		}


		public static AIMDatabaseInfo GetAIMDatabase(String name)
		{
			return AIMData.GetAIMDatabase(name);
		}


		public static List<CFCPlantRefAIMOwnerLocationInfo> GetAllCFCPlantAIMOwnerLocation()
		{
			return AIMData.GetAllCFCPlantAIMOwnerLocation();
		}

		public static CFCPlantRefAIMOwnerLocationInfo GetOneCFCPlantAIMSourceLocation(Int32 CFCPlantID)
		{
			return AIMData.GetOneCFCPlantAIMOwnerLocation(CFCPlantID);
		}

		public static CFCPlantRefAIMOwnerLocationInfo GetOneCFCPlantAIMOwnerLocation(Int32 cfcPlantID, Int32 aimOwnerLocationID)
		{
			return AIMData.GetOneCFCPlantAIMOwnerLocation(cfcPlantID, aimOwnerLocationID);
		}


		//12/21/20
		public static List<AnalyzerSupplierLocationCombinedInfo> GetAllAnalyzerSupplierLocationCombined(Boolean isActive)
		{
			return AIMData.GetAllAnalyzerSupplierLocationCombined(isActive);
		}

		public static List<AnalyzerSupplierInfo> GetAllAnalyzerSupplier(Boolean isActive)
		{
			return AIMData.GetAllAnalyzerSupplier(isActive);
		}
		public static List<AnalyzerSupplierLocationInfo> GetAllAnalyzerSupplierLocation(Boolean isActive)
		{
			return AIMData.GetAllAnalyzerSupplierLocation(isActive);
		}

		//12/21/20
		public static List<AnalyzerSupplierLocationCombinedInfo> GetAllAnalyzerSupplierLocationCombined()
		{
			return AIMData.GetAllAnalyzerSupplierLocationCombined();
		}

		public static List<AnalyzerSupplierInfo> GetAllAnalyzerSupplier()
		{
			return AIMData.GetAllAnalyzerSupplier();
		}

		public static List<AnalyzerSupplierLocationInfo> GetAllAnalyzerSupplierLocation()
		{
			return AIMData.GetAllAnalyzerSupplierLocation();
		}

		public static List<AnalyzerNutrientInfo> GetAllAnalyzerNutrient(Boolean isActive)
		{
			return AIMData.GetAllAnalyzerNutrient(isActive);
		}

		public static List<AnalyzerNutrientInfo> GetAllAnalyzerNutrient()
		{
			return AIMData.GetAllAnalyzerNutrient();
		}

		public static List<AnalyzerOwnerInfo> GetAllAnalyzerOwner(Boolean isActive)
		{
			return AIMData.GetAllAnalyzerOwner(isActive);
		}

		public static List<AnalyzerOwnerInfo> GetAllAnalyzerOwner()
		{
			return AIMData.GetAllAnalyzerOwner();
		}

		public static List<AnalyzerOwnerLocationInfo> GetAllAnalyzerOwnerLocation(Boolean isActive)
		{
			return AIMData.GetAllAnalyzerOwnerLocation(isActive);
		}

		public static List<AnalyzerOwnerLocationInfo> GetAllAnalyzerOwnerLocation()
		{
			return AIMData.GetAllAnalyzerOwnerLocation();
		}

		//12/21/20
		public static List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombined(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAnalyzerSupplierLocationCombined(DataSourceTypeID, isActive);
		}

		//12/21/20
		public static List<AnalyzerSupplierLocationCombinedInfo> GetAnalyzerSupplierLocationCombined(Int32 DataSourceTypeID)
		{
			return AIMData.GetAnalyzerSupplierLocationCombined(DataSourceTypeID);
		}

		public static List<AnalyzerSupplierInfo> GetAnalyzerSupplier(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAnalyzerSupplier(DataSourceTypeID, isActive);
		}

		public static List<AnalyzerSupplierInfo> GetAnalyzerSupplier(Int32 DataSourceTypeID)
		{
			return AIMData.GetAnalyzerSupplier(DataSourceTypeID);
		}

		public static List<AnalyzerNutrientInfo> GetAnalyzerNutrient(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAllAnalyzerNutrient(DataSourceTypeID, isActive);
		}

		public static List<AnalyzerNutrientInfo> GetAnalyzerNutrientTemplateDetail(Int32 dataSourceTypeID, Int32 sampleResultTemplateID, Boolean isActive)
		{
			return AIMData.GetAnalyzerNutrientTemplateDetail(dataSourceTypeID, sampleResultTemplateID, isActive);
		}

		//1/4/18
		public static List<SampleQualityParameterInfo> GetSampleQualityParameterDetail(Int32 dataSourceTypeID, Int32 sampleQualityControlMassEntryID, Boolean isActive)
		{
			return AIMData.GetSampleQualityParameterDetail(dataSourceTypeID, sampleQualityControlMassEntryID, isActive);
		}

		public static List<AIMNutrientRefAnalyzerNutrientInfo> GetAIMNutrientRefAnalyzerNutrientDataSourceTypeID(Int32 DataSourceTypeID)
		{
			return AIMData.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeID(DataSourceTypeID);
		}

		public static List<AIMNutrientRefAnalyzerNutrientInfo> GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientID(Int32 DataSourceTypeID, Int32 aimNutrientID)
		{
			return AIMData.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientID(DataSourceTypeID, aimNutrientID);
		}

		//4/18/18
		public static List<AIMNutrientRefAnalyzerNutrientInfo> GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientIDAnalyzerNutrientID(Int32 DataSourceTypeID, Int32 aimNutrientID, Int32 analyzerNutrientID)
		{
			return AIMData.GetAIMNutrientRefAnalyzerNutrientDataSourceTypeIDAIMNutrientIDAnalyzerNutrientID(DataSourceTypeID, aimNutrientID, analyzerNutrientID);
		}

		public static List<AIMSupplierRefAnalyzerSupplierInfo> GetAIMSupplierRefAnalyzerSupplierDataSourceTypeID(Int32 DataSourceTypeID)
		{
			return AIMData.GetAIMSupplierRefAnalyzerSupplierDataSourceTypeID(DataSourceTypeID);
		}

		public static List<AIMSupplierRefAnalyzerSupplierInfo> GetAIMSupplierRefAnalyzerSupplierDataSourceTypeIDAnalyzerSupplierID(Int32 DataSourceTypeID, Int32 analyzerSupplierID)
		{
			return AIMData.GetAIMSupplierRefAnalyzerSupplierDataSourceTypeIDAnalyzerSupplierID(DataSourceTypeID, analyzerSupplierID);
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeID(Int32 DataSourceTypeID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeID(DataSourceTypeID);
		}

		//7/11/23
		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDMaterialTypeID(Int32 DataSourceTypeID, Int32 materialTypeID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDMaterialTypeID(DataSourceTypeID, materialTypeID);
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAnalyzerMaterialID(Int32 DataSourceTypeID, Int32 analyzerMaterialID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAnalyzerMaterialID(DataSourceTypeID, analyzerMaterialID);
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAIMMaterialID(Int32 DataSourceTypeID, Int32 aimMaterialID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialDataSourceTypeIDAIMMaterialID(DataSourceTypeID, aimMaterialID);
		}

		public static List<AnalyzerMaterialInfo> GetAllAnalyzerMaterial(Boolean isActive)
		{
			return AIMData.GetAllAnalyzerMaterial(isActive);
		}

		//1/17/24
		public static List<AnalyzerMaterialInfo> GetPNEAnalyzerMaterial(Int32 aimMaterialID, Int32 dataSourceTypeID)
		{
			return AIMData.GetPNEAnalyzerMaterial(aimMaterialID, dataSourceTypeID);
		}

		public static ErrorInfo  UpdateSampleAimPriority(Int32 aimNutrientID, Int32 aimPriorityUsedValue, Int32 databaseTypeID)
		{
			return AIMData.UpdateSampleAimPriority(aimNutrientID, aimPriorityUsedValue, databaseTypeID);
		}

		//7/2/20
		//public static AIMNutrientInfo GetAIMNutrient(Int32 aimNutrientID)
		public static AIMNutrientInfo GetAIMNutrientIsActive(Int32 aimNutrientID, Boolean isActive)
		{
			return AIMData.GetAIMNutrientIsActive(aimNutrientID, isActive);
		}

		//10/13/22
		public static List<AIMNutrientDriverInfo> GetAIMNutrientDriver(Int32 queryTemplateID)
		{
			return AIMData.GetAIMNutrientDriver(queryTemplateID);
		}

		public static List<AIMNutrientInfo> GetAIMNutrient(Boolean isActive)
		{
			return AIMData.GetAIMNutrient(isActive);
		}

		public static List<AIMNutrientInfo> GetAIMNutrient(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAIMNutrient(DataSourceTypeID, isActive);
		}

		public static List<ImportConfigInfo> GetImportConfig()
		{
			return AIMData.GetImportConfig();
		}

		public static List<ImportConfigInfo> GetImportConfig(DataSourceTypeEnum dataSourceType)
		{
			return AIMData.GetImportConfig(dataSourceType);
		}


		public static ImportConfigInfo GetImportConfig(String name)
		{
			return AIMData.GetImportConfig(name);
		}

		public static List<DataSourceTypeInfo> GetDataSourceType(Boolean isActive)
		{
			return AIMData.GetDataSourceType(isActive);
		}

		//1/22/21
		public static List<SampleTypeInfo> GetSampleType(Boolean isActive)
		{
			return AIMData.GetSampleType(isActive);
		}

		// task 1155 FG 8/5/26 - required field groups (header rows incl. scope IDs)
		public static List<SampleRequiredFieldGroupInfo> GetSampleRequiredFieldGroup(Boolean isActive)
		{
			return AIMData.GetSampleRequiredFieldGroup(isActive);
		}

		// task 1155 FG 8/5/26 - the required fields inside one group
		public static List<SampleRequiredFieldGroupDetailInfo> GetSampleRequiredFieldGroupDetail(Int32 sampleRequiredFieldGroupID)
		{
			return AIMData.GetSampleRequiredFieldGroupDetail(sampleRequiredFieldGroupID);
		}

		// task 1155 FG 8/5/26 - required fields from the user's assigned group that apply to
		//one sample's material and location
		public static List<SampleRequiredFieldGroupDetailInfo> GetSampleRequiredFieldRuleForSample(Int32 aimMaterialID, Int32 aimOwnerLocationID, Int32 sampleRequiredFieldGroupID)
		{
			return AIMData.GetSampleRequiredFieldRuleForSample(aimMaterialID, aimOwnerLocationID, sampleRequiredFieldGroupID);
		}

		//12/21/22
		public static List<CFCProductTypeInfo> GetCFCProductType(Int32 aimDatabaseID)
		{
			return AIMData.GetCFCProductType(aimDatabaseID);
		}


		//1/8/18
		public static List<DataSourceTypeInfo> GetDataSourceTypeHavingAnalyzerNutrients(Boolean isActive)
		{
			return AIMData.GetDataSourceTypeHavingAnalyzerNutrients(isActive);
		}

		//1/10/18
		public static List<DataSourceTypeInfo> GetDataSourceTypeHavingQualityControlParameters(Boolean isActive)
		{
			return AIMData.GetDataSourceTypeHavingQualityControlParameters(isActive);
		}


		public static List<DataSourceTypeInfo> GetDataSourceTypeRefExists(Boolean isActive)
		{
			return AIMData.GetDataSourceTypeRefExists(isActive);
		}


		public static List<DataSourceTypeInfo> GetDataSourceTypeByUserDefinedSwitch(Boolean isUserDefined)
		{
			return AIMData.GetDataSourceTypeByUserDefinedSwitch(isUserDefined);
		}

		public static List<DataSourceAllTypesInfo> GetDataSourceAllTypes(Boolean isActive)
		{
			return AIMData.GetDataSourceAllTypes(isActive);
		}

		public static List<DataSourceTypeInfo> GetDataSourceTypeUserDefined(Boolean isActive)
		{
			return AIMData.GetDataSourceTypeUserDefined(isActive);
		}

		public static List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType()
		{
			return AIMData.GetDataSourceTypeRefFileType();
		}

		public static List<AIMPriorityUsedValueInfo> GetAIMPriorityUsedValue()
		{
			return AIMData.GetAIMPriorityUsedValue();
		}

		//6/19/20
		public static List<ShowCodeNameOrBothInfo> GetCodeNameOrBothValues()
		{
			return AIMData.GetCodeNameOrBothValues();
		}

		public static List<AIMPriorityUsedValueInfo> GetAIMPriorityUsedValue(Int32 aimPriorityUsedValueID)
		{
			return AIMData.GetAIMPriorityUsedValue(aimPriorityUsedValueID);
		}
		public static List<AIMPriorityUsedValueInfo> GetAIMPriorityUsedValueExcludeDefault()
		{
			return AIMData.GetAIMPriorityUsedValueExcludeDefault();
		}

		public static DataSourceTypeInfo GetDataSourceType_DataSourceTypeID(Int32 dataSourceTypeID)
		{
			return AIMData.GetDataSourceType_DataSourceTypeID(dataSourceTypeID);
		}

		public static List<DataSourceTypeInfo> GetDataSourceFurtherAnalyses(Boolean isFurtherAnalyses)
		{
			return AIMData.GetDataSourceFurtherAnalyses(isFurtherAnalyses);
		}

		public static List<FileTypeInfo> GetFileType(Boolean isActive)
		{
			return AIMData.GetFileType(isActive);
		}
		public static List<FileTypeInfo> GetFileType(Int32 fileTypeID)
		{
			return AIMData.GetFileType(fileTypeID);
		}
		public static List<ExcelFileTypeInfo> GetExcelFileType(Boolean isActive)
		{
			return AIMData.GetExcelFileType(isActive);
		}
		public static List<ExcelFileTypeInfo> GetExcelFileTypeName(String name)
		{
			return AIMData.GetExcelFileTypeName(name);
		}

		public static List<ImportFileTypeInfo> GetImportFileType(Boolean isActive)
		{
			return AIMData.GetImportFileType(isActive);
		}

		public static List<DelimiterTypeInfo> GetDelimiterType(Boolean isActive)
		{
			return AIMData.GetDelimiterType(isActive);
		}

		//4/1/18
		public static List<DecimalSeparatorInfo> GetDecimalSeparator(Boolean isActive)
		{
			return AIMData.GetDecimalSeparator(isActive);
		}

		//6/3/19
		public static DecimalSeparatorInfo GetDecimalSeparator(Int32 decimalSeparatorID)
		{
			return AIMData.GetDecimalSeparator(decimalSeparatorID);
		}

		public static List<DateFormatInfo> GetDateFormat()
		{
			return AIMData.GetDateFormat();
		}

		public static DateFormatInfo GetDateFormat(Int32 dateFormatID)
		{
			return AIMData.GetDateFormat(dateFormatID);
		}


		public static List<UserDefaultsInfo> GetUserDefaultsForCFCUserID(int cfcUserID)
		{
			return AIMData.GetUserDefaultsForCFCUserID(cfcUserID);
		}

		//12/7/18
		public static List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType(Int32 dataSourceTypeID, Boolean isSystem)
		{
			return AIMData.GetDataSourceTypeRefFileType(dataSourceTypeID, isSystem);
		}

		public static List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType(Int32 dataSourceTypeID)
		{
			return AIMData.GetDataSourceTypeRefFileType(dataSourceTypeID);
		}

		public static List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeRefFileType(Int32 dataSourceTypeID, Int32 fileTypeID)
		{
			return AIMData.GetDataSourceTypeRefFileType(dataSourceTypeID, fileTypeID);
		}


		public static List<FileTypeRefDelimiterTypeInfo> GetFileTypeRefDelimiterType(Int32 fileTypeID)
		{
			return AIMData.GetFileTypeRefDelimiterType(fileTypeID);
		}



		public static AIMNutrientInfo GetAIMNutrient(String aimNutrientCode)
		{
			return AIMData.GetAIMNutrient(aimNutrientCode);
		}

		//2/9/18   
		public static ErrorInfo  GetExportBatchMaintenanceCheckIsOverlay(Int32 updateIngredientHeaderID, Int32 status, Boolean isOverlay)
		{
			return AIMData.GetExportBatchMaintenanceCheckIsOverlay(updateIngredientHeaderID, status, isOverlay);
		}


		public static List<UpdateIngredientDetail> GetExportBatchMaintenance(Int32 UpdateIngredientHeaderID)
		{
			return AIMData.GetExportBatchMaintenance(UpdateIngredientHeaderID);
		}

		//2/12/24
		public static List<UpdateIngredientDetail> GetExportBatchMaintenance(Int32 updateIngredientHeaderID, Int32 updateIngredientDetailID)
		{
			return AIMData.GetExportBatchMaintenance(updateIngredientHeaderID, updateIngredientDetailID);
		}

		public static List<UpdateIngredientDetail> GetExportBatchMaintenanceDefault(Int32 UpdateIngredientHeaderID)
		{
			return AIMData.GetExportBatchMaintenanceDefault(UpdateIngredientHeaderID);
		}

		public static List<UpdateIngredientDetail> GetExportBatchMaintenanceSpecific(Int32 UpdateIngredientHeaderID)
		{
			return AIMData.GetExportBatchMaintenanceSpecific(UpdateIngredientHeaderID);
		}

		//1/25/24
		//public static ErrorInfo  GetIngredientDetailStatusCount(ref AnalysisExportBatchMaintenance exportBatch)
		//{
		//	return AIMData.GetIngredientDetailStatusCount(ref exportBatch);
		//}
		public static ErrorInfo  GetIngredientDetailStatusCount(Int32 updateIngredientHeaderID)
		{
			return AIMData.GetIngredientDetailStatusCount(updateIngredientHeaderID);
		}

		public static ErrorInfo  GetImportHeaderStatusCount()
		{
			return AIMData.GetImportHeaderStatusCount();
		}

		public static ErrorInfo  GetSampleResultMessageCount(Int32 type, Int32 sampleID, DateTime startDate, DateTime endDate)
		{
			return AIMData.GetSampleResultMessageCount(type, sampleID, startDate, endDate);
		}


		public static ErrorInfo  GetCFCIngredientNutrientOverlayCount()
		{
			return AIMData.GetCFCIngredientNutrientOverlayCount();
		}

		//6/27/19
		public static ErrorInfo  GetSampleOldestSampleDate()
		{
			return AIMData.GetSampleOldestSampleDate();
		}

		public static ErrorInfo  GetUserDefinedDataSourceTemplateCount(Int32 defID, String colName)
		{
			return AIMData.GetUserDefinedDataSourceTemplateCount(defID, colName);
		}

		public static ErrorInfo  GetSampleImportLogNextBatchID()
		{
			return AIMData.GetSampleImportLogNextBatchID();
		}

		public static List<ImportSQLSampleLogInfo> GetImportSQLSampleLog(Int32 importConfigID)
		{
			return AIMData.GetImportSQLSampleLog(importConfigID);
		}

		public static List<ImportSQLSampleMissingInfo> GetImportSQLSampleMissing()
		{
			return AIMData.GetImportSQLSampleMissing();
		}

		public static ErrorInfo  GetDataSourceHeaderDetail(String analyzerDataSourceTypeName)
		{
			return AIMData.GetDataSourceHeaderDetail(analyzerDataSourceTypeName);
		}

		public static ErrorInfo  GetUserDefinedDataSourceTemplateMapColumnName(Int32 defID, String mapColumnName)
		{
			return AIMData.GetUserDefinedDataSourceTemplateMapColumnName(defID, mapColumnName);
		}

		//12/23/20
		public static UserDefinedDataSourceTemplateInfo GetUserDefinedDataSourceTemplateMapColumnNameTop(Int32 defID, String mapColumnName)
		{
			return AIMData.GetUserDefinedDataSourceTemplateMapColumnNameTop(defID, mapColumnName);
		}

		public static ErrorInfo  GetNextGeneratedSampleNumber(Int32 dataSourceTypeID)
		{
			return AIMData.GetNextGeneratedSampleNumber(dataSourceTypeID);
		}

		public static ErrorInfo  GetSampleImportStatusCount(Int32 UserDefinedDataSourceDefinitionID)
		{
			return AIMData.GetSampleImportStatusCount(UserDefinedDataSourceDefinitionID);
		}


		public static AnalysisExportBatchMaintenance GetUpdateIngredientHeader(Int32 UpdateIngredientHeaderID)
		{
			return AIMData.GetUpdateIngredientHeader(UpdateIngredientHeaderID);
		}
		public static List<AnalysisExportBatchMaintenance> GetUpdateIngredient(Int32 Status)
		{
			return AIMData.GetUpdateIngredient(Status);
		}

		public static List<AnalysisExportBatchMaintenance> GetExportBatchMaintenance()
		{
			return AIMData.GetExportBatchMaintenance();
		}

		public static List<AIMOwnerLocationInfo> GetAllAIMOwnerLocation(Boolean isActive)
		{
			return AIMData.GetAllAIMOwnerLocation(isActive);
		}

		////////11/30/22
		//////public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationMaterialTemplate()
		//////{
		//////	return AIMData.GetAIMOwnerLocationMaterialTemplate();
		//////}

		public static List<CFCProductArchiveInfo> GetCFCProductArchive(Int32 aimMaterialID, Int32 aimOwnerLocationID)
		{
			return AIMData.GetCFCProductArchive(aimMaterialID, aimOwnerLocationID);
		}

		//1/5/23
		public static List<CFCProductArchiveInfo> GetCFCProductArchive(Int32 aimMaterialID)
		{
			return AIMData.GetCFCProductArchive(aimMaterialID);
		}

		public static List<CFCProductInfo> GetAllCFCProduct(Boolean isActive)
		{
			return AIMData.GetAllCFCProduct(isActive);
		}

		public static List<CFCProductSpecInfo> GetProductSpec(String plantCode, String productCode, String productionVersion)
		{
			return AIMData.GetProductSpec(plantCode, productCode, productionVersion);
		}

		public static List<CFCProductInfo> GetAllCFCProductAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			return AIMData.GetAllCFCProductAimDatabaseID(isActive, aimDatabaseID);
		}

		//4/13/22
		public static List<CFCProductInfo> GetCFCProduct(Int32 plantID, String productCode)
		{
			return AIMData.GetCFCProduct(plantID, productCode);
		}

		public static List<CFCProductInfo> GetAllCFCProductAimDatabaseIDCFCPlantID(Boolean isActive, Int32 aimDatabaseID, Int32 cfcPlantID)
		{
			return AIMData.GetAllCFCProductAimDatabaseIDCFCPlantID(isActive, aimDatabaseID, cfcPlantID);
		}


		public static List<CFCIngredientProductCombinedInfo> GetAllCFCProductInitialLoadAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			return AIMData.GetAllCFCProductInitialLoadAimDatabaseID(isActive, aimDatabaseID);
		}


		public static List<CFCIngredientInfo> GetAllCFCIngredient(Boolean isActive)
		{
			return AIMData.GetAllCFCIngredient(isActive);
		}

		public static List<CFCIngredientInfo> GetAllCFCIngredientAimDatabaseID(Boolean isActive, Int32 aimDatabaseID)
		{
			return AIMData.GetAllCFCIngredientAimDatabaseID(isActive, aimDatabaseID);
		}

		public static List<AnalyzerMaterialInfo> GetAnalyzerMaterial(Boolean isActive)
		{
			return AIMData.GetAnalyzerMaterial(isActive);
		}

		public static List<DefaultSettingsInfo> GetDefaultSettings(Int32 CFCUserID)
		{
			return AIMData.GetDefaultSettings(CFCUserID);
		}

		//12/16/20
		public static AnalysisUserDefaultSettingsInfo GetAnalysisUserDefaultSettings(Int32 CFCUserID)
		{
			return AIMData.GetAnalysisUserDefaultSettings(CFCUserID);
		}


		public static SystemSettingsInfo GetSystemSettings()
		{
			return AIMData.GetSystemSettings();
		}

		public static List<AnalyzerInstrumentInfo> GetAllAnalyzerInstrument()
		{
			return AIMData.GetAllAnalyzerInstrument();
		}
		public static List<AnalyzerInstrumentInfo> GetAnalyzerInstrument(Int32 dataSourceTypeID)
		{
			return AIMData.GetAnalyzerInstrument(dataSourceTypeID);
		}

		public static List<AnalyzerMaterialInfo> GetAnalyzerMaterial(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAnalyzerMaterial(DataSourceTypeID, isActive);
		}

		public static List<AnalyzerMaterialInfo> GetAnalyzerMaterial(
																	DataSourceTypeEnum dataSourceType,
																	Int32 aimMaterialID)
		{
			return AIMData.GetAnalyzerMaterial(dataSourceType, aimMaterialID);
		}

		public static AnalyzerMaterialInfo GetAnalyzerMaterial(Int32 dataSourceTypeID, String analyzerMaterialCode)
		{
			return AIMData.GetAnalyzerMaterial(dataSourceTypeID, analyzerMaterialCode);
		}


		public static AnalyzerSupplierInfo GetAnalyzerSupplier(DataSourceTypeEnum dataSourceType, String analyzerSupplierCode)
		{
			return AIMData.GetAnalyzerSupplier(dataSourceType, analyzerSupplierCode);
		}


		//12/21/20
		public static AnalyzerSupplierLocationCombinedInfo GetAnalyzerSupplierLocationCombined(Int32 dataSourceTypeID, String analyzerSupplierLocationCombinedName)
		{
			return AIMData.GetAnalyzerSupplierLocationCombined(dataSourceTypeID, analyzerSupplierLocationCombinedName);
		}

		public static AnalyzerSupplierInfo GetAnalyzerSupplier(Int32 dataSourceTypeID, String analyzerSupplierCode)
		{
			return AIMData.GetAnalyzerSupplier(dataSourceTypeID, analyzerSupplierCode);
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(Int32 dataSourceType, Int32 materialTypeID, Int32 aimOwnerLocationID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterial_DataSource_MaterialType_OwnerLocation(dataSourceType, materialTypeID, aimOwnerLocationID);
		}

		//10/25/22
		public static List<SpecificIngredientsInfo> GetSpecificIngredients_GroupID_DataSourceType_IsActive(Int32 groupID, Int32 dataSourceType, Boolean isActive)
		{
			return AIMData.GetSpecificIngredients_GroupID_DataSourceType_IsActive(groupID, dataSourceType, isActive);
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAnalyzerMaterialsForUtility(Int32 dataSourceType, Int32 materialTypeID, Int32 aimOwnerLocationID)
		{
			return AIMData.GetAnalyzerMaterialsForUtility(dataSourceType, materialTypeID, aimOwnerLocationID);
		}

		public static List<AIMMaterialRefAnalyzerMaterialInfo> GetAIMMaterialWithAnalyzerMaterial(Int32 dataSourceType)
		{
			return AIMData.GetAIMMaterialWithAnalyzerMaterial(dataSourceType);
		}

		public static List<AnalyzerMaterialInfo> GetAnalyzerMaterial()
		{
			return AIMData.GetAnalyzerMaterial();
		}

		public static List<AnalyzerPendingSampleInfo> GetAnalyzerPendingSample(AnalyzerStatusEnum status)
		{
			return AIMData.GetAnalyzerPendingSample(status);
		}

		public static AnalyzerPendingSampleInfo GetAnalyzerPendingSample(Int32 sampleID)
		{
			return AIMData.GetAnalyzerPendingSample(sampleID);
		}

		public static List<AnalyzerPendingSampleInfo> GetAnalyzerPendingSampleList(Int32 sampleID)
		{
			return AIMData.GetAnalyzerPendingSampleList(sampleID);
		}

		//1/18/24
		public static List<AnalyzerPendingSampleInfo> GetAnalyzerPendingSample(DateTime begDate, DateTime endDate, Int32 dateField)
		{
			return AIMData.GetAnalyzerPendingSample(begDate, endDate, dateField);
		}

		public static List<AnalyzerMaterialInfo> GetAnalyzerMaterial(Int32 dataSourceTypeID)
		{
			return AIMData.GetAnalyzerMaterial(dataSourceTypeID);
		}
		public static List<PNEMaterialSampleTypeInfo> GetPNEMaterialSampleType()
		{
			return AIMData.GetPNEMaterialSampleType();
		}
		public static List<PNEMaterialTypeInfo> GetPNEMaterialType()
		{
			return AIMData.GetPNEMaterialType();
		}
		public static List<PNEMaterialTypeInfo> GetPNEMaterialType(Int32 analyzerMaterialID)
		{
			return AIMData.GetPNEMaterialType(analyzerMaterialID);
		}
		public static List<PNEForAnalyzerMaterialRefAnalyzerNutrientInfo> GetPNEForAnalyzerMaterialRefAnalyzerNutrient(Int32 analyzerMaterialID)
		{
			return AIMData.GetPNEForAnalyzerMaterialRefAnalyzerNutrient(analyzerMaterialID);
		}
		public static List<AnalyzerMaterialRefAnalyzerAnalyseInfo> GetAnalyzerMaterialAnalyse(Int32 analyzerMaterialID, Int32 dataSourceTypeID)
		{
			return AIMData.GetAnalyzerMaterialAnalyse(analyzerMaterialID, dataSourceTypeID);
		}

		public static AIMOwnerInfo GetOneAIMOwner(Int32 aimOwnerID)
		{
			return AIMData.GetOneAIMOwner(aimOwnerID);
		}

		public static List<AnalysisInfo> GetAnalysisByDate(DateTime startDate, DateTime endDate)
		{
			return AIMData.GetAnalysisByDate(startDate, endDate);
		}


		public static List<AnalysisInfo> GetAnalysisByNoOne(DateTime startDate, DateTime endDate)
		{
			return AIMData.GetAnalysisByNoOne(startDate, endDate);
		}

		public static List<AnalysisInfo> GetAnalysis(
				AnalysisQueryEnum analysisQuery,
				Int32 queryTemplateID,
				DateTime startDate,
				DateTime endDate,
				Boolean isDefaultLocation,
				Int32 aimDataBaseID,
				OutlierTypeEnum outlierType,
				Int32 specificCFCPlantID
			)
		{
			return AIMData.GetAnalysis(
				analysisQuery,
				queryTemplateID,
				startDate,
				endDate,
				isDefaultLocation,
				false,
				0,
				0,
				DateTime.MinValue,
				0,
				0,
				0,
				0,
				0,
				0
				, 0
				, 0
				, ""
				, aimDataBaseID
				, outlierType
				, 0
				, specificCFCPlantID

				//6/28/21
				, false
				, 0         //supplier2
				, 0         //supplier3
				, 0         //supplier4
				, 0         //supplierloc2
				, 0         //supplierloc3
				, 0         //supplierloc4

				, 0         //owner2
				, 0         //owner3
				, 0         //owner4
				, 0         //ownerloc2
				, 0         //ownerloc3
				, 0         //ownerloc4

				, 0         //supplierOwnerOption --> for charts

				);
		}

		public static List<AnalysisInfo> GetAnalysis(
				AnalysisQueryEnum analysisQuery,
				Int32 queryTemplateID,
				DateTime startDate,
				DateTime endDate,
				Boolean isDefaultLocation,
				Boolean IsSubSet,
				Int32 aimMaterialID,
				Int32 aimNutrientID,
				DateTime sampleDate,
				Int32 sampleID,
				Int32 aimOwnerID,
				Int32 aimOwnerLocationID,
				Int32 aimSupplierID,
				Int32 aimSupplierLocationID,
				Int32 cfcPlantID,
				Int32 warningGlobalH,
				Int32 warningMinMaxLimit,
				String sampleCode,
				Int32 aimDataBaseID,
				OutlierTypeEnum outlierType,
				Int32 cfcIngredientID,
				Int32 specificCFCPlantID,

				//6/28/21
				Boolean fromCharts,
				Int32 aimSupplierID2,
				Int32 aimSupplierID3,
				Int32 aimSupplierID4,
				Int32 aimSupplierLocationID2,
				Int32 aimSupplierLocationID3,
				Int32 aimSupplierLocationID4,
				Int32 aimOwnerID2,
				Int32 aimOwnerID3,
				Int32 aimOwnerID4,
				Int32 aimOwnerLocationID2,
				Int32 aimOwnerLocationID3,
				Int32 aimOwnerLocationID4,
				Int32 supplierOwnerOption,

				//task 1035 8/10/26 cursor change - old version kept for reference:  one combined list
				//String chartsSupplierOwnerPairList = ""

				//task 1035 8/10/26 cursor change - supplier rows and owner rows are two separate delimited lists
				//									(supplier: 'SourceID,SourceLocationID;...'  owner: 'OwnerID,OwnerLocationID;...')
				//									matched by entry position when both are sent -- any number of rows
				String chartsSupplierPairList = "",
				String chartsOwnerPairList = ""

			)
		{
			return AIMData.GetAnalysis(
					analysisQuery,
					queryTemplateID,
					startDate,
					endDate,
					isDefaultLocation,
					IsSubSet,
					aimMaterialID,
					aimNutrientID,
					sampleDate,
					sampleID,
					aimOwnerID,
					aimOwnerLocationID,
					aimSupplierID,
					aimSupplierLocationID,
					cfcPlantID,
					warningGlobalH,
					warningMinMaxLimit,
					sampleCode,
					aimDataBaseID,
					outlierType,
					cfcIngredientID,
					specificCFCPlantID,

					//6/28/21
					fromCharts,
					aimSupplierID2,
					aimSupplierID3,
					aimSupplierID4,
					aimSupplierLocationID2,
					aimSupplierLocationID3,
					aimSupplierLocationID4,
					aimOwnerID2,
					aimOwnerID3,
					aimOwnerID4,
					aimOwnerLocationID2,
					aimOwnerLocationID3,
					aimOwnerLocationID4,
					supplierOwnerOption,

					//task 1035 8/10/26 cursor change - old version kept for reference:  one combined list
					//chartsSupplierOwnerPairList

					//task 1035 8/10/26 cursor change - two separate lists (supplier rows and owner rows)
					chartsSupplierPairList,
					chartsOwnerPairList


					);
		}


		//this one is main was used on Product Analysis
		public static List<AnalysisProductInfo> GetAnalysisProduct(
				AnalysisQueryEnum analysisQuery,
				Int32 queryTemplateID,
				DateTime startDate,
				DateTime endDate,
				Int32 groupMaterials,
				Int32 nutrientToleranceHeaderID,
				Int32 aimDataBaseID,
				OutlierTypeEnum outlierType
			)
		{
			return AIMData.GetAnalysisProduct(
					analysisQuery,
					queryTemplateID,
					startDate,
					endDate,
					groupMaterials,
					nutrientToleranceHeaderID,
					false,
					0,
					0,
					DateTime.MinValue,
					0,
					0,
					0,
					"",
					aimDataBaseID,
					outlierType

					//1/4/23
					, ""
					);
		}

		//----------------------------------------------------------------------------------
		//12/27/22
		public static List<AnalysisProductInfo> GetAnalysisProduct(
				AnalysisQueryEnum analysisQuery,
				Int32 queryTemplateID,
				DateTime startDate,
				DateTime endDate,
				Int32 groupMaterials,
				Int32 nutrientToleranceHeaderID,
				Boolean isSubset,
				Int32 aimMaterialID,
				Int32 aimNutrientID,
				DateTime sampleDate,
				Int32 sampleID,
				Int32 aimOwnerID,
				Int32 aimOwnerLocationID,
				String sampleCode,
				Int32 aimDataBaseID,
				OutlierTypeEnum outlierType

				//1/4/23
				, String archiveVersion
			)
		{
			return AIMData.GetAnalysisProduct(
				analysisQuery,
				queryTemplateID,
				startDate,
				endDate,
				groupMaterials,
				nutrientToleranceHeaderID,
				isSubset,
				aimMaterialID,
				aimNutrientID,
				sampleDate,
				sampleID,
				aimOwnerID,
				aimOwnerLocationID,
				sampleCode,
				aimDataBaseID,
				outlierType

				//1/4/23
				, archiveVersion

				);
		}

		public static List<SampleInfo> GetAllSample(Boolean isActive)
		{
			return AIMData.GetAllSample(isActive);
		}

		public static List<SampleInfo> GetAllSampleUseSystemDateSetting(Boolean isActive, Int32 days)
		{
			return AIMData.GetAllSampleUseSystemDateSetting(isActive, days);
		}

		//11/11/22
		public static List<SampleInfo> GetAllSamples(Boolean isActive)
		{
			return AIMData.GetAllSamples(isActive);
		}



		public static List<SampleInfo> GetAllSampleFilter(Int32 filterControlID, String value, String dateBeg, String dateEnd)
		{
			return AIMData.GetAllSampleFilter(filterControlID, value, dateBeg, dateEnd);
		}

		public static List<SampleTableStructureInfo> GetTableNames(String tableName)
		{
			return AIMData.GetTableNames(tableName);
		}

		public static List<SampleExportHeaderInfo> GetAllSampleFilterCSVExport(Int32 exportTemplateID)
		{
			return AIMData.GetAllSampleFilterCSVExport(exportTemplateID);
		}

		public static List<SampleExportDetailInfo> GetAllSampleFilterCSVExportDetail(Int32 exportTemplateID)
		{
			return AIMData.GetAllSampleFilterCSVExportDetail(exportTemplateID);
		}


		public static List<SampleExportDetailInfo> GetAllSampleForExport(String sampleCodeBeg, String sampleCodeEnd, String dateBeg, String dateEnd)
		{
			return AIMData.GetAllSampleForExport(sampleCodeBeg, sampleCodeEnd, dateBeg, dateEnd);
		}

		public static SampleInfo GetSample(String SampleCode)
		{
			return AIMData.GetSample(SampleCode);
		}

		public static List<SampleInfo> GetSampleForUtility(String SampleCode)
		{
			return AIMData.GetSampleForUtility(SampleCode);
		}

		//2/3/20
		public static SampleInfo GetSampleForCharts(String SampleCode)
		{
			return AIMData.GetSampleForCharts(SampleCode);
		}

		//------------------------------------------------------------------
		//7/13/21 - Question: Datasource on Sample is used with auto-generated samples, to get the next available sample# for the datasource,
		//			because there may be multiple datasources on an import file -- so shouldn't datasourcetypeid be included?

		//3/12/21 - changed this back, because datasourcetypeid on the sample header shouldn't be used -- because
		//			one sample can have several datasourcetypes(which are on the AnalyzerNutrient and part of the results)
		//2/15/21
		//public static SampleInfo GetSampleCodeForImports(String SampleCode, Int32 datasourceTypeID)
		//{
		//	return AIMData.GetSampleCodeForImports(SampleCode, datasourceTypeID);
		//}
		public static SampleInfo GetSampleCodeForImports(String SampleCode)
		{
			return AIMData.GetSampleCodeForImports(SampleCode);
		}

		//8/16/22		
		public static SampleInfo GetSampleCodeForRomerImports(String SQLResultID, Int32 OwnerID, Int32 OwnerLocID, Int32 SupplierID, Int32 SupplierLocID)
		{
			return AIMData.GetSampleCodeForRomerImports(SQLResultID, OwnerID, OwnerLocID, SupplierID, SupplierLocID);
		}

		//------------------------------------------------------------------

		public static SampleInfo GetSample(String sampleCode, Int32 dataSourceTypeID)
		{
			return AIMData.GetSample(sampleCode, dataSourceTypeID);
		}

		public static SampleInfo GetSampleAlternateCode(String AlternateCode)
		{
			return AIMData.GetSampleAlternateCode(AlternateCode);
		}

		public static SampleInfo GetSample(Int32 selectedSampleNumber)
		{
			return AIMData.GetSample(selectedSampleNumber);
		}

		public static List<SampleInfo> GetSampleMissingFurtherAnalysis(Int32 furtherAnalysisID, MaterialTypeEnum materialTypeID, DateTime createdStamp)
		{
			return AIMData.GetSampleMissingFurtherAnalysis(furtherAnalysisID, materialTypeID, createdStamp);
		}

		public static List<SampleInfo> GetSampleMissingFurtherAnalysisResult(Int32 furtherAnalysisID, MaterialTypeEnum materialTypeID, DateTime createdStamp, String sqlResultID)
		{
			return AIMData.GetSampleMissingFurtherAnalysisResult(furtherAnalysisID, materialTypeID, createdStamp, sqlResultID);
		}
		public static SampleInfo GetSampleUsingSampleType(Int32 sampleTypeID)
		{
			return AIMData.GetSampleUsingSampleType(sampleTypeID);
		}

		public static UserDefinedDataSourceDefinitionInfo GetDataSourceUserDefinition(Int32 UserDefinedDataSourceDefinitionID)
		{
			return AIMData.GetDataSourceUserDefinition(UserDefinedDataSourceDefinitionID);
		}
		public static UserDefinedDataSourceDefinitionInfo GetDataSourceUserID(Int32 DataSourceUserID)
		{
			return AIMData.GetDataSourceTypeID(DataSourceUserID);
		}


		//12/16/20
		public static List<UserDefinedDataSourceDefinitionInfo> GetImportsForRequiredField(Int32 ID, String fieldName)
		{
			return AIMData.GetImportsForRequiredField(ID, fieldName);
		}

		public static List<UserDefinedDataSourceDefinitionInfo> GetUserDefinedDefinitionImportFileName(String importFileName)
		{
			return AIMData.GetUserDefinedDefinitionImportFileName(importFileName);
		}


		public static UserDefinedDataSourceDefinitionInfo GetUserDefinedDataSourceDefinitionDataSourceUserIDFileTypeID(Int32 dataSourceTypeID, Int32 fileTypeID)
		{
			return AIMData.GetUserDefinedDataSourceDefinitionDataSourceUserIDFileTypeID(dataSourceTypeID, fileTypeID);
		}

		public static List<DataSourceTypeRefFileTypeInfo> GetDataSourceTypeIDFileID(Int32 DataSourceUserID, Int32 FileTypeID)
		{
			return AIMData.GetDataSourceTypeIDFileID(DataSourceUserID, FileTypeID);
		}

		//1/19/24
		public static List<PNEMaterialSampleTypeRefGroundUnGroundInfo> GetPNEMaterialSampleTypeRefGroundUnGround()
		{
			return AIMData.GetPNEMaterialSampleTypeRefGroundUnGround();
		}
			

		//1/19/24  
		public static List<PNEMaterialSampleTypeRefGroundUnGroundInfo> GetPNEMaterialSampleTypeRefGroundUnGround(Int32 PNEMaterialSampleTypeID)
		{
			return AIMData.GetPNEMaterialSampleTypeRefGroundUnGround(PNEMaterialSampleTypeID);
		}

		//2/8/24
		public static List<PNEMaterialSampleTypeRefGroundUnGroundInfo> GetPNEMaterialSampleTypeRefGroundUnGround(String aimMaterialCode)
		{
			return AIMData.GetPNEMaterialSampleTypeRefGroundUnGround(aimMaterialCode);
		}

		public static DataSourceTypeInfo GetUserDefinedDataSourceTypeID(Int32 DataSourceUserID)
		{
			return AIMData.GetUserDefinedDataSourceTypeID(DataSourceUserID);
		}		

        //3/25/25
        public static List<SampleLabelFormatInfo> PrintLabel(Int32 selectedSampleNumber, Boolean printNameFirst, Int32 materialTypeID)
        {
            return AIMData.PrintLabel(selectedSampleNumber, printNameFirst, materialTypeID);
        }

        
        public static List<PertenSQLInfo> GetPertenSqlData(String sampleNumber, DateTime startDate, DateTime endDate)
		{
			return AIMData.GetPertenSqlData(sampleNumber, startDate, endDate);
		}

		//2/26/20
		public static List<BrukerSQLInfo> GetBrukerSqlData(String sampleCode, DateTime startDate, DateTime endDate)
		{
			return AIMData.GetBrukerSqlData(sampleCode, startDate, endDate);
		}

		public static List<AIMOwnerInfo> GetAIMOwner(Boolean isActive)
		{
			return AIMData.GetAIMOwner(isActive);
		}

		//11/23/22
		public static List<AIMOwnerInfo> GetAIMOwner(Int32 OwnerLocationGroupID, Boolean isActive)
		{
			return AIMData.GetAIMOwner(OwnerLocationGroupID, isActive);
		}

		public static List<AIMOwnerInfo> GetAIMOwnerWithValidOwnerLocations(Boolean isActive)
		{
			return AIMData.GetAIMOwnerWithValidOwnerLocations(isActive);
		}
		public static List<AIMSupplierInfo> GetAIMSupplierWithValidSupplierLocations(Boolean isActive)
		{
			return AIMData.GetAIMSupplierWithValidSupplierLocations(isActive);
		}

		//11/23/22
		//public static List<AIMOwnerInfo> GetAIMOwnerAll()
		//{
		//	return AIMData.GetAIMOwnerAll();
		//}
		public static List<AIMOwnerInfo> GetAIMOwnerAll(Int32 AIMOwnerLocationGroupID)
		{
			return AIMData.GetAIMOwnerAll(AIMOwnerLocationGroupID);
		}

		//8/30/23
		//public static List<AIMSupplierInfo> GetAIMSupplierAll()
		//{
		//	return AIMData.GetAIMSupplierAll();
		//}
		public static List<AIMSupplierInfo> GetAIMSupplierAllStatus(Int32 aimSupplierGroupID)
		{
			return AIMData.GetAIMSupplierAllStatus(aimSupplierGroupID);
		}

		public static List<SampleImportUserDefinedMapSelectionInfo> GetMapSelection(Boolean isActive)
		{
			return AIMData.GetMapSelection(isActive);
		}

		//8/19/22 - added for Romer Imports
		public static List<SampleImportUserDefinedMapSelectionInfo> GetMapSelectionSkipUseMapping(Boolean isActive)
		{
			return AIMData.GetMapSelectionSkipUseMapping(isActive);
		}

		//12/3/19
		//public static List<SampleImportColumnNamesInfo> GetSampleColumnNames(Boolean swIncludeDataSourceColumn)
		//{
		//	return AIMData.GetSampleColumnNames(swIncludeDataSourceColumn);
		//}
		public static List<SampleImportColumnNamesInfo> GetSampleColumnNames(Boolean swIncludeDataSourceColumn, Boolean swUseSystemSampleCodes)
		{
			return AIMData.GetSampleColumnNames(swIncludeDataSourceColumn, swUseSystemSampleCodes);
		}

		//12/4/19
		//public static List<SampleImportColumnNamesInfo> GetSampleColumnNames_CanAlter(Boolean swIncludeDataSourceColumn)
		//{
		//    return AIMData.GetSampleColumnNames_CanAlter(swIncludeDataSourceColumn);
		//}
		public static List<SampleImportColumnNamesInfo> GetSampleColumnNames_CanAlter(Boolean swIncludeDataSourceColumn, Boolean swUseSystemSampleCodes)
		{
			return AIMData.GetSampleColumnNames_CanAlter(swIncludeDataSourceColumn, swUseSystemSampleCodes);
		}

		//7/26/19
		public static List<SampleImportColumnNamesInfo> GetSampleColumnNames_HeaderOnly()
		{
			return AIMData.GetSampleColumnNames_HeaderOnly();
		}

		//TODO - 4/3/18 - this stp is not used anymore -- can be deleted when ImportTextFile.cs is removed.
		public static SampleImportColumnNamesInfo GetSampleColumnNames(String mapColumnName)
		{
			return AIMData.GetSampleColumnNames(mapColumnName);
		}

		public static List<UnitTypeInfo> GetUnitType(Boolean isActive)
		{
			return AIMData.GetUnitType(isActive);
		}

		public static List<AIMOwnerLocationInfo> GetAIMOwnerLocation(Boolean isActive)
		{
			return AIMData.GetAIMOwnerLocation(isActive);
		}

		public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwner(Int32 ownerID)
		{
			return AIMData.GetAIMOwnerLocationForOwner(ownerID);
		}

		//11/23/22
		public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwner(Int32 ownerID, Int32 aimOwnerLocationGroupID)
		{
			return AIMData.GetAIMOwnerLocationForOwner(ownerID, aimOwnerLocationGroupID);
		}

		//1/5/22
		public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwnerWithOrMissingXref(Int32 ownerID, Boolean isXref)
		{
			return AIMData.GetAIMOwnerLocationForOwnerWithOrMissingXref(ownerID, isXref);
		}

		//11/23/22
		//public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwnerAll(Int32 ownerID)
		//{
		//	return AIMData.GetAIMOwnerLocationForOwnerAll(ownerID);
		//}
		public static List<AIMOwnerLocationInfo> GetAIMOwnerLocationForOwnerAll(Int32 ownerID, Int32 aimOwnerLocationGroupID)
		{
			return AIMData.GetAIMOwnerLocationForOwnerAll(ownerID, aimOwnerLocationGroupID);
		}

		//User-Defined Codes --------------------------------------
		public static List<AnalyzerMaterialInfo> GetAnalyzerMaterial_AnalyzerMaterialID(Int32 analyzerMaterialID)
		{
			return AIMData.GetAnalyzerMaterial_AnalyzerMaterialID(analyzerMaterialID);
		}
		public static List<AnalyzerMaterialInfo> GetAnalyzerMaterial_MaterialID_DataSourceTypeID_IsActive(Int32 analyzerMaterialID, Int32 datasourceTypeID, Int32 isActive)
		{
			return AIMData.GetAnalyzerMaterial_MaterialID_DataSourceTypeID_IsActive(analyzerMaterialID, datasourceTypeID, isActive);
		}



		public static AIMOwnerLocationInfo GetOneAIMOwnerLocation(Int32 ownerLocationID)
		{
			return AIMData.GetOneAIMOwnerLocation(ownerLocationID);
		}

		public static List<AnalyzerSupplierInfo> GetAnalyzerSupplier_SupplierID(Int32 analyzerSupplierID)
		{
			return AIMData.GetAnalyzerSupplier_SupplierID(analyzerSupplierID);
		}

		public static List<AnalyzerSupplierInfo> GetAnalyzerSupplier_DataSourceType_SupplierName(Int32 dataSourceTypeID, String supplierName)
		{
			return AIMData.GetAnalyzerSupplier_DataSourceType_SupplierName(dataSourceTypeID, supplierName);
		}

		public static List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation_DataSourceType_SupplierLocationName(Int32 dataSourceTypeID, String supplierName)
		{
			return AIMData.GetAnalyzerSupplierLocation_DataSourceType_SupplierLocationName(dataSourceTypeID, supplierName);
		}

		public static List<AnalyzerSupplierLocationInfo> GetAnalyzerSupplierLocation_SupplierLocationID(Int32 analyzerSupplierLocationID)
		{
			return AIMData.GetAnalyzerSupplierLocation_SupplierLocationID(analyzerSupplierLocationID);
		}

		//-----------------------------------------------------------
		public static List<AIMSupplierLocationInfo> GetAIMSupplierLocationForSupplierAllStatus(Int32 supplierID)
		{
			return AIMData.GetAIMSupplierLocationForSupplierAllStatus(supplierID);
		}
		public static List<AIMSupplierLocationInfo> GetAIMSupplierLocationForSupplier(Int32 supplierID)
		{
			return AIMData.GetAIMSupplierLocationForSupplier(supplierID);
		}
		public static List<AIMSupplierLocationInfo> GetAIMSupplierLocationForSupplierAllStatus(Int32 supplierID, Int32 aimSupplierGroupID, Int32 aimSupplierLocationGroupID)
		{
			return AIMData.GetAIMSupplierLocationForSupplierAllStatus(supplierID, aimSupplierGroupID, aimSupplierLocationGroupID);
		}
        public static List<TabOrderGroupInfo> GetTabOrderGroup()                    //Task1041	MW250526
        {
            return AIMData.GetTabOrderGroup();
        }
        public static List<TabOrderGroupDetailInfo> GetTabOrderGroupDetail(Int32 tabOrderGroupID)   //Task1041	DB250528
        {
            return AIMData.GetTabOrderGroupDetail(tabOrderGroupID);
        }

        //Task1042 FG 7/7/26 - Sample Screen Display group
        public static List<SampleScreenDisplayGroupInfo> GetSampleScreenDisplay()
        {
            return AIMData.GetSampleScreenDisplay();
        }
        public static List<SampleScreenDisplayGroupDetailInfo> GetSampleScreenDisplayDetail(Int32 sampleScreenDisplayID)
        {
            return AIMData.GetSampleScreenDisplayDetail(sampleScreenDisplayID);
        }
        public static ErrorInfo SaveSampleScreenDisplay(ref SampleScreenDisplayGroupInfo sampleScreenDisplayData)
        {
            return AIMData.SaveSampleScreenDisplay(ref sampleScreenDisplayData);
        }
        public static ErrorInfo SaveSampleScreenDisplayDetail(Int32 sampleScreenDisplayID, Int32 controlID, Int32 userID)
        {
            return AIMData.SaveSampleScreenDisplayDetail(sampleScreenDisplayID, controlID, userID);
        }
        public static ErrorInfo DeleteSampleScreenDisplay(Int32 sampleScreenDisplayID)
        {
            return AIMData.DeleteSampleScreenDisplay(sampleScreenDisplayID);
        }
        public static ErrorInfo DeleteSampleScreenDisplayDetail(Int32 sampleScreenDisplayDetailID)
        {
            return AIMData.DeleteSampleScreenDisplayDetail(sampleScreenDisplayDetailID);
        }
        public static List<TabOrderInfo> GetTabOrder(Int32 formID, Int32 userID)
        {
            return AIMData.GetTabOrder(formID, userID);
        }
		//3/14/23
		public static List<TabOrderInfo> GetTabOrder(Int32 formID, Int32 userID, Int32 tabOrder)
		{
			return AIMData.GetTabOrder(formID, userID, tabOrder);
		}

		public static List<ControlInfo> GetControl(Int32 controlID)
		{
			return AIMData.GetControl(controlID);
		}

		public static List<ControlInfo> GetControlUsingAllowUserDefault(Boolean allowUserDefault)
		{
			return AIMData.GetControlUsingAllowUserDefault(allowUserDefault);
		}

		public static List<AlternateNameInfo> GetAlternateName(Int32 formID)
		{
			return AIMData.GetAlternateName(formID);
		}
				

		//3/24/25
		public static List<LabelInfo> GetLabel()
		{
			return AIMData.GetLabel();
		}

        //Task1141 DB251216 
        public static List<LabelInfo> GetLabelIsVisibleToPrinterLabel()
        {
            return AIMData.GetLabelIsVisibleToPrinterLabel();
        }
        //3/24/25
        public static List<LabelFormatInfo> GetLabelFormat()
        {
            return AIMData.GetLabelFormat();
        }

        //8/21/23
        public static List<SampleTabControlPageInfo> GetSampleTabControlPage()
		{
			return AIMData.GetSampleTabControlPage();
		}

		//8/21/23
		public static void SaveSampleTabControlPage(Int32 SampleTabControlPageID, Int32 SortOrder, Int32 cfcUserID)
		{
			AIMData.SaveSampleTabControlPage(SampleTabControlPageID, SortOrder, cfcUserID);
		}

		//8/21/23
		public static List<SampleTabControlPageInfo> GetSampleTabControlPageSortOrder(Int32 SortOrder)
		{
			return AIMData.GetSampleTabControlPageSortOrder(SortOrder);
		}

		//8/23/23   
		public static SampleTabControlPageInfo GetSampleTabControlPageName(String pageName)
		{
			return AIMData.GetSampleTabControlPageName(pageName);
		}

		//5/2/23
		public static List<XLabelsInfo_old> GetLabels_IngrOrProd(String ingrOrProd)
		{
			return AIMData.GetLabels_IngrOrProd(ingrOrProd);
		}

		//6/19/20
		public static List<AlternateNameInfo> GetAlternateName(Int32 formID, String controlName)
		{
			return AIMData.GetAlternateName(formID, controlName);
		}

		//3/14/23
		public static List<AlternateNameInfo> GetAlternateName(Int32 formID, Int32 controlID)
		{
			return AIMData.GetAlternateName(formID, controlID);
		}

        //6/26/20 - commented, label fields moved from AlternateName table to Labels table
        //public static List<AlternateNameInfo> GetAlternateNameLabelFieldFirst(Int32 formID)
        //{
        //	return AIMData.GetAlternateNameLabelFieldFirst(formID);
        //}

        //6/26/20 - commented, label fields moved from AlternateName table to Labels table
        //public static List<AlternateNameInfo> GetAlternateNameLabelFieldLast(Int32 formID)
        //{
        //	return AIMData.GetAlternateNameLabelFieldLast(formID);
        //}

		public static List<AlternateNameInfo> GetAlternateNameFilter(Int32 formID)
		{
			return AIMData.GetAlternateNameFilter(formID);
		}

		public static APIPendingAIMMaterialInfo GetAPIPendingAIMMaterial(Int32 aimMaterialID, Int32 dataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAPIPendingAIMMaterial(aimMaterialID, dataSourceTypeID, isActive);
		}
		#endregion

		#region Insert

		//01/18/23
		public static ErrorInfo  InsertAPISprectrumLog(Int32 sampleID, Int32 apiSpectrumID, Int32 apiNIRType)
		{
			return AIMData.InsertAPISprectrumLog(sampleID, apiSpectrumID, apiNIRType);
		}
		public static void InsertSystemLog(Int32 typeID, String message, Int32 lineNumber, String methodName, Int32 CFCUserID)
		{
			AIMData.InsertSystemLog(typeID, message, lineNumber, methodName, CFCUserID);
		}
		public static ErrorInfo  InsertAIMMessage(Int32 typeID, String message, Int32 CFCUserID)
		{
			return AIMData.InsertAIMMessage(typeID, message, CFCUserID);
		}
		public static ErrorInfo  InsertAIMMessage(Int32 typeID, String message)
		{
			return InsertAIMMessage(typeID, message, 0);
		}
		public static ErrorInfo  InsertAIMUpdateLog(ref AIMUpdateLogInfo refInfo)
		{
			return AIMData.InsertAIMUpdateLog(ref refInfo);
		}
		public static AIMUpdateLogInfo AIMUpdateCheckSqlStatement(String sql)
		{
			return AIMData.AIMUpdateCheckSqlStatement(sql);
		}

		public static void InsertTabOrder(Int32 formID, Int32 userID)
		{
			AIMData.InsertTabOrder(formID, userID);
		}
		public static void InsertAlternateName(Int32 formID)
		{
			AIMData.InsertAlternateName(formID);
		}
		public static void InsertCFCPlantRefAIMOwnerLocation(ref CFCPlantRefAIMOwnerLocationInfo refInfo)
		{
			AIMData.InsertCFCPlantRefAIMOwnerLocation(ref refInfo);
		}
		//10/20/20 - commented. AnalyzerMaterialRefAnalyse table does not exist
		//public static void InsertAnalyzerMaterialRefAnalyse(Int32 AnalyzerMaterialID, DataSourceTypeEnum dataSourceTypeID, String analyzerAnalyseCode)
		//{
		//	AIMData.InsertAnalyzerMaterialRefAnalyse(AnalyzerMaterialID, dataSourceTypeID, analyzerAnalyseCode);
		//}

		public static void InsertCFCPlant(ref CFCPlantInfo refInfo)
		{
			AIMData.InsertCFCPlant(ref refInfo);
		}

        //8/18/25 farag
        public static ErrorInfo  InsertDataSourceType(ref DataSourceTypeInfo dataSourceType)
        {
            return AIMData.InsertDataSourceType(ref dataSourceType);
        }
        #endregion

        #region Delete

        public static void DeleteTabOrdreGroup(Int32 TabOrdreGroupID)
        {
            AIMData.DeleteTabOrdreGroup(TabOrdreGroupID);
        }

        public static void DeleteTabOrdreGroupDetail(Int32 TabOrdreGroupDetailID)
        {
            AIMData.DeleteTabOrdreGroupDetail(TabOrdreGroupDetailID);
        }


        public static void DeletePNEMaterialSampleTypeRefGroundUnGround(Int32 ID)
        {
            AIMData.DeletePNEMaterialSampleTypeRefGroundUnGround(ID);
        }

        public static void DeleteSystemLog()
		{
			AIMData.DeleteSystemLog();
		}

		public static void DeleteSampleResult(ref SampleResultInfo sr)
		{
			AIMData.DeleteSampleResult(ref sr);
		}

		//1/10/18
		public static void DeleteSampleQualityResult(ref SampleQualityInfo sr)
		{
			AIMData.DeleteSampleQualityResult(ref sr);
		}


		public static void DeleteQueryField(Int32 queryFieldID)
		{
			AIMData.DeleteQueryField(queryFieldID);
		}

		public static void DeleteQueryOperation(Int32 queryOperationID)
		{
			AIMData.DeleteQueryOperation(queryOperationID);
		}

		public static void DeleteGuaranteeBatch(Int32 batchID)
		{
			AIMData.DeleteGuaranteeBatch(batchID);
		}

		public static void DeleteQueryTemplate(Int32 queryTemplateID)
		{
			AIMData.DeleteQueryTemplate(queryTemplateID);
		}

		public static void DeleteQueryTemplateDetail(Int32 queryTemplateDetailID)
		{
			AIMData.DeleteQueryTemplateDetail(queryTemplateDetailID);
		}

		public static void DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID(Int32 queryTemplateID, Int32 queryFieldID)
		{
			AIMData.DeleteQueryTemplateDetail_QueryTemplateIDAndFieldID(queryTemplateID, queryFieldID);
		}

		public static void DeleteSample(Int32 sampleID)
		{
			AIMData.DeleteSample(sampleID);
		}

		public static void DeleteSampleAIMRefCFCUser(Int32 value, SampleAIMRefCFCUserEnum sampleAIMRefCFCUser)
		{
			AIMData.DeleteSampleAIMRefCFCUser(value, sampleAIMRefCFCUser);
		}

		public static void DeleteSampleExportTemplate(Int32 sampleExportID)
		{
			AIMData.DeleteSampleExportTemplate(sampleExportID);
		}

		public static void DeleteSampleResultTemplateDetail(Int32 ID)
		{
			AIMData.DeleteSampleResultTemplateDetail(ID);
		}

		//1/4/18
		public static void DeleteSampleQualityControlMassEntryDetail(Int32 ID)
		{
			AIMData.DeleteSampleQualityControlMassEntryDetail(ID);
		}

		public static void DeleteSampleResultTemplate(Int32 ID)
		{
			AIMData.DeleteSampleResultTemplate(ID);
		}

		//1/5/18
		public static void DeleteSampleQualityControlMassEntry(Int32 ID)
		{
			AIMData.DeleteSampleQualityControlMassEntry(ID);
		}


		public static void DeleteSampleAIMRefCFCUser(Int32 value, SampleAIMRefCFCUserEnum sampleAIMRefCFCUser, Int32 cfcUserID)
		{
			AIMData.DeleteSampleAIMRefCFCUser(value, sampleAIMRefCFCUser, cfcUserID);
		}

		//4/17/18 - added cfcUserID
		public static void DeleteCFCProductRefAIMMaterial(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteCFCProductRefAIMMaterial(ID, cfcUserID);
		}

		//4/17/18 - added cfcUserID
		public static void DeleteCFCIngredientRefAIMMaterial(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteCFCIngredientRefAIMMaterial(ID, cfcUserID);
		}

		public static void DeleteWarningLimits(Int32 ID)
		{
			AIMData.DeleteWarningLimits(ID);
		}

		//9/23/22
		public static void DeleteSpecificIngredients(Int32 ID)
		{
			AIMData.DeleteSpecificIngredients(ID);
		}

		//5/18/23
		public static void DeleteSpecificProducts(Int32 ID)
		{
			AIMData.DeleteSpecificProducts(ID);
		}

		//9/23/22
		public static void DeleteSpecificIngredients_GroupID(Int32 ID)
		{
			AIMData.DeleteSpecificIngredients_GroupID(ID);
		}

		//5/18/23
		public static void DeleteSpecificProducts_GroupID(Int32 ID)
		{
			AIMData.DeleteSpecificProducts_GroupID(ID);
		}

		//10/19/22
		public static void DeleteSpecificIngredientsGroup(Int32 ID)
		{
			AIMData.DeleteSpecificIngredientsGroup(ID);
		}

		//5/18/23
		public static void DeleteSpecificProductsGroup(Int32 ID)
		{
			AIMData.DeleteSpecificProductsGroup(ID);
		}

		//1/2/18
		public static void DeleteSampleQualityParameter(Int32 ID)
		{
			AIMData.DeleteSampleQualityParameter(ID);
		}

		public static void DeleteMathType(Int32 mathTypeID)
		{
			AIMData.DeleteMathType(mathTypeID);
		}

		//1/3/18        
		public static void DeleteSampleQualityEquation(Int32 ID)
		{
			AIMData.DeleteSampleQualityEquation(ID);
		}
		//1/4/18        
		public static void DeleteSampleQualityEquationInactive()
		{
			AIMData.DeleteSampleQualityEquationInactive();
		}



		//1/4/18
		public static void DeleteSampleQualityEquationBuild(Int32 equationID)
		{
			AIMData.DeleteSampleQualityEquationBuild(equationID);
		}

		//1/4/18
		public static void DeleteSampleQualityEquationBuild_Inactive(Int32 equationID)
		{
			AIMData.DeleteSampleQualityEquationBuild_Inactive(equationID);
		}


		public static void DeleteImportHeaderDetailStatus(Int32 statusMap)
		{
			AIMData.DeleteImportHeaderDetailStatus(statusMap);
		}

		public static void DeleteEmailAddressXRefMessage(Int32 ID)
		{
			AIMData.DeleteEmailAddressXRefMessage(ID);
		}
		public static void DeleteDataSourceUserTemplate(Int32 ID)
		{
			AIMData.DeleteDataSourceUserTemplate(ID);
		}
		public static void DeleteDataSourceUserDefinitionAndTemplate(Int32 ID)
		{
			AIMData.DeleteDataSourceUserDefinitionAndTemplate(ID);
		}
		public static void DeleteDataSourceUser(Int32 ID)
		{
			AIMData.DeleteDataSourceUser(ID);
		}

		//1/22/21
		public static void DeleteSampleType(Int32 sampleTypeID)
		{
			AIMData.DeleteSampleType(sampleTypeID);
		}

		// task 1155 FG 8/5/26 - trash can: removes the group and all its detail rows
		public static void DeleteSampleRequiredFieldGroup(Int32 sampleRequiredFieldGroupID)
		{
			AIMData.DeleteSampleRequiredFieldGroup(sampleRequiredFieldGroupID);
		}

		// task 1155 FG 8/5/26 - grid delete: removes one required field from the group
		public static void DeleteSampleRequiredFieldGroupDetail(Int32 sampleRequiredFieldGroupDetailID)
		{
			AIMData.DeleteSampleRequiredFieldGroupDetail(sampleRequiredFieldGroupDetailID);
		}

		public static void DeleteDataSourceUserTemplateUsingDefinitionID(Int32 defID)
		{
			AIMData.DeleteDataSourceUserTemplateUsingDefinitionID(defID);
		}

		public static void DeleteDataSourceUserTemplateZeroSourceColumnNumber(Int32 defID, Int32 sourceColumnNumber)
		{
			AIMData.DeleteDataSourceUserTemplateZeroSourceColumnNumber(defID, sourceColumnNumber);
		}

		public static ErrorInfo  DataSourceUserTemplateCountSourceColumnNumberDuplicates(Int32 defID)
		{
			return AIMData.DataSourceUserTemplateCountSourceColumnNumberDuplicates(defID);
		}


		public static ErrorInfo  DataSourceUserTemplateCheckSourceColumnName(Int32 defID)
		{
			return AIMData.DataSourceUserTemplateCheckSourceColumnName(defID);
		}

		public static void DeleteDataSourceTypeRefFileType(Int32 ID)
		{
			AIMData.DeleteDataSourceTypeRefFileType(ID);
		}


		public static void DeleteDataSourceTypeRefFileTypeUsingDataSourceTypeID(Int32 ID)
		{
			AIMData.DeleteDataSourceTypeRefFileTypeUsingDataSourceTypeID(ID);
		}


		public static void DeleteEmailAddress(Int32 ID)
		{
			AIMData.DeleteEmailAddress(ID);
		}
		public static void DeletePrinterDefault(Int32 printerDefaultID)
		{
			AIMData.DeletePrinterDefault(printerDefaultID);
		}

		//6/15/18        
		public static void DeletePrinterUserDefault(Int32 printerUserDefaultID)
		{
			AIMData.DeletePrinterUserDefault(printerUserDefaultID);
		}

		public static void DeleteWebService(Int32 ID)
		{
			AIMData.DeleteWebService(ID);
		}

		//4/17/18 - added cfcUserID
		public static void DeleteCFCNutrientRefAIMNutrient(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteCFCNutrientRefAIMNutrient(ID, cfcUserID);
		}

		//4/17/18 - added cfcUserID
		public static void DeleteAIMNutrientRefAnalyzerNutrient(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteAIMNutrientRefAnalyzerNutrient(ID, cfcUserID);
		}

		//4/17/18 - added cfcUserID
		public static void DeleteAIMSupplierRefAnalyzerSupplier(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteAIMSupplierRefAnalyzerSupplier(ID, cfcUserID);
		}

		//4/17/18 - added cfcUserID
		public static void DeleteAIMSupplierLocationRefAnalyzerSupplierLocation(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteAIMSupplierLocationRefAnalyzerSupplierLocation(ID, cfcUserID);
		}
		public static void DeleteSampleAim(Int32 aimNutrientID)
		{
			AIMData.DeleteSampleAim(aimNutrientID);
		}

		//4/17/18 - added CFCUserID
		public static void DeleteAIMMaterialRefAnalyzerMaterial(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteAIMMaterialRefAnalyzerMaterial(ID, cfcUserID);
		}

		//6/18/20
		public static void DeleteAIMMaterialStockStatus(Int32 materialID, Int32 ownerLocationID)
		{
			AIMData.DeleteAIMMaterialStockStatus(materialID, ownerLocationID);
		}

		//10/14/22		
		public static void DeleteAIMNutrientDriver(Int32 queryTemplateID, Int32 aimNutrientID)
		{
			AIMData.DeleteAIMNutrientDriver(queryTemplateID, aimNutrientID);
		}

		public static void DeleteDataSourceDetail(Int32 ID)
		{
			AIMData.DeleteDataSourceDetail(ID);
		}

		public static void DeleteDataSourceHeader(Int32 ID)
		{
			AIMData.DeleteDataSourceHeader(ID);
		}

		//10/20/20 - commented. AnalyzerMaterialRefAnalyse table does not exist
		//public static void DeleteAnalyzerMaterialRefAnalyse(Int32 analyzerMaterialRefAnalyzerAnalyseID)
		//{
		//	AIMData.DeleteAnalyzerMaterialRefAnalyse(analyzerMaterialRefAnalyzerAnalyseID);
		//}

		public static void DeleteAnalyzerAnalyse(Int32 analyzerAnalyseID)
		{
			AIMData.DeleteAnalyzerAnalyse(analyzerAnalyseID);
		}

		//4/17/18 - added cfcUserID
		public static void DeleteCFCPlantRefAIMOwnerLocation(Int32 CFCPlantRefAIMPlantID, Int32 cfcUserID)
		{
			AIMData.DeleteCFCPlantRefAIMOwnerLocation(CFCPlantRefAIMPlantID, cfcUserID);
		}

		public static void DeleteImportConfig(Int32 importConfigID)
		{
			AIMData.DeleteImportConfig(importConfigID);
		}
		public static void DeleteInstrument(Int32 instrumentID)
		{
			AIMData.DeleteInstrument(instrumentID);
		}

        //Task1085 DB250813 
        public static void DeleteLabelFormat(Int32 labelFormatID)
        {
            AIMData.DeleteLabelFormat(labelFormatID);
        }

        #endregion

        #region Save

        //1/19/24  
        public static ErrorInfo  SavePNEMaterialSampleTypeRefGroundUnGround(ref PNEMaterialSampleTypeRefGroundUnGroundInfo def)
        {
            return AIMData.SavePNEMaterialSampleTypeRefGroundUnGround(ref def);
        }
        public static ErrorInfo  SavePNEForAnalyzerMaterialRefAnalyzerNutrient(ref PNEForAnalyzerMaterialRefAnalyzerNutrientInfo pneForAnalyzerMaterialRefAnalyzerNutrient)
		{
			return AIMData.SavePNEForAnalyzerMaterialRefAnalyzerNutrient(ref pneForAnalyzerMaterialRefAnalyzerNutrient);
		}
		public static ErrorInfo  SavePNEMaterialSampleType(ref PNEMaterialSampleTypeInfo pneMaterialSampleType)
		{
			return AIMData.SavePNEMaterialSampleType(ref pneMaterialSampleType);
		}
		public static ErrorInfo  SavePNEMaterialType(ref PNEMaterialTypeInfo pneMaterialType)
		{
			return AIMData.SavePNEMaterialType(ref pneMaterialType);
		}
		public static ErrorInfo  SavePNECredit(ref PNECreditInfo pneCredit)
		{
			return AIMData.SavePNECredit(ref pneCredit);
		}
		public static ErrorInfo  SaveSampleLog(ref SampleLogInfo sampleLog)
		{
			return AIMData.SaveSampleLog(ref sampleLog);
		}
		public static ErrorInfo  SaveAPIPendingAIMMaterial(ref APIPendingAIMMaterialInfo apiPendingAIMMaterial)
		{
			return AIMData.SaveAPIPendingAIMMaterial(ref apiPendingAIMMaterial);
		}
		public static ErrorInfo  SaveImportSQLSampleLog(ref ImportSQLSampleLogInfo log)
		{
			return AIMData.SaveImportSQLSampleLog(ref log);
		}

		//Task1075 DB250715
		public static ErrorInfo  SaveReportUserDefaultSetting(ref ReportUserDefaultSettingInfo reportUserDefaultSetting)
		{
			return AIMData.SaveReportUserDefaultSetting(ref reportUserDefaultSetting);
        }

        public static ErrorInfo  SaveImportSQLSampleMissing(Int32 logID, String sampleCode, Int32 cfcUserID)
		{
			return AIMData.SaveImportSQLSampleMissing(logID, sampleCode, cfcUserID);
		}

		//9/23/22
		public static ErrorInfo  SaveSpecificIngredient(ref SpecificIngredientsInfo specific)
		{
			return AIMData.SaveSpecificIngredient(ref specific);
		}

		//5/18/23
		public static ErrorInfo  SaveSpecificProduct(ref SpecificProductsInfo specific)
		{
			return AIMData.SaveSpecificProduct(ref specific);
		}

		//10/20/22
		public static ErrorInfo  UpdateSpecificIngredientIsActive(Int32 aimMaterialID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.UpdateSpecificIngredientIsActive(aimMaterialID, isActive, cfcUserID);
		}

		//5/18/23
		public static ErrorInfo  UpdateSpecificProductIsActive(Int32 aimMaterialID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.UpdateSpecificProductIsActive(aimMaterialID, isActive, cfcUserID);
		}

		//10/19/22
		public static ErrorInfo  SaveSpecificIngredientGroup(ref SpecificIngredientsGroupInfo group)
		{
			return AIMData.SaveSpecificIngredientGroup(ref group);
		}

		//5/18/23
		public static ErrorInfo  SaveSpecificProductGroup(ref SpecificProductsGroupInfo group)
		{
			return AIMData.SaveSpecificProductGroup(ref group);
		}

		//11/11/22
		public static List<AIMOwnerLocationGroupDetailInfo> GetOwnerLocationGroupDetail_GroupID(Int32 groupID, Boolean isActive)
		{
			return AIMData.GetOwnerLocationGroupDetail_GroupID(groupID, isActive);
		}

		//11/28/22
		public static List<AIMOwnerLocationGroupDetailInfo> GetOwnerLocationGroupDetail_GroupID(Int32 groupID)
		{
			return AIMData.GetOwnerLocationGroupDetail_GroupID(groupID);
		}

		//11/11/22
		public static List<AIMOwnerLocationGroupInfo> GetOwnerLocationGroup(Boolean isActive)
		{
			return AIMData.GetOwnerLocationGroup(isActive);
		}

		//11/22/22
		public static void DeleteOwnerLocationGroupDetail(Int32 ID)
		{
			AIMData.DeleteOwnerLocationGroupDetail(ID);
		}

		//11/22/22
		public static ErrorInfo  SaveOwnerLocationGroup(ref AIMOwnerLocationGroupInfo group)
		{
			return AIMData.SaveOwnerLocationGroup(ref group);
		}

		//11/22/22
		public static ErrorInfo  SaveOwnerLocationGroupDetail(ref AIMOwnerLocationGroupDetailInfo group)
		{
			return AIMData.SaveOwnerLocationGroupDetail(ref group);
		}

		//11/22/22		
		public static void DeleteOwnerLocationGroup(Int32 ID)
		{
			AIMData.DeleteOwnerLocationGroup(ID);
		}

		public static ErrorInfo  SaveAIMMaterial(ref AIMMaterialInfo material, Boolean bypassErrorMsg)
		{
			return AIMData.SaveAIMMaterial(ref material, bypassErrorMsg);
		}

		//6/18/20
		public static ErrorInfo  SaveAIMMaterialStockStatus(ref AIMMaterialStockStatusInfo materialLoc)
		{
			return AIMData.SaveAIMMaterialStockStatus(ref materialLoc);
		}

		//1/11/18
		public static ErrorInfo  SaveUserDefaults(ref UserDefaultsInfo defaults)
		{
			return AIMData.SaveUserDefaults(ref defaults);
		}

		//1/11/18
		public static void DeleteUserDefaults(Int32 formID, Int32 cfcUserID)
		{
			AIMData.DeleteUserDefaults(formID, cfcUserID);
		}


		public static ErrorInfo  SaveSampleAIMRefCFCUser(ref SampleAIMRefCFCUserInfo sampleAIMRefCFCUser)
		{
			return AIMData.SaveSampleAIMRefCFCUser(ref sampleAIMRefCFCUser);
		}

		public static ErrorInfo  SaveSampleAIM(Int32 analyzerNutrientID, Int32 aimNutrientID, Int32 CFCUserID, Int32 aimDatabaseID, Int32 dataSourceTypeID)
		{
			return AIMData.SaveSampleAIM(analyzerNutrientID, aimNutrientID, CFCUserID, aimDatabaseID, dataSourceTypeID);
		}

		//6/29/22
		public static ErrorInfo  SaveSampleFurtherAnalysis(Int32 sampleFurtherAnalysisID, Int32 sampleID, Int32 datasourcetypeID, Int32 cfcUserID)
		{
			return AIMData.SaveSampleFurtherAnalysis(sampleFurtherAnalysisID, sampleID, datasourcetypeID, cfcUserID);
		}

		public static ErrorInfo  SaveSampleFurtherAnalysis(ref SampleFurtherAnalysisInfo sampleFurtherAnalysis)
		{
			return AIMData.SaveSampleFurtherAnalysis(ref sampleFurtherAnalysis);
		}

		public static ErrorInfo  SaveEmailAddress(ref EmailAddressInfo emailAddress)
		{
			return AIMData.SaveEmailAddress(ref emailAddress);
		}

		public static ErrorInfo  SaveDataSourceUser(ref DataSourceTypeInfo dataSource)
		{
			return AIMData.SaveDataSourceUser(ref dataSource);
		}

		//1/22/21
		public static ErrorInfo  SaveSampleType(ref SampleTypeInfo sampleType)
		{
			return AIMData.SaveSampleType(ref sampleType);
		}

		// task 1155 FG 8/5/26 - header save (group code and/or scope selections)
		public static ErrorInfo SaveSampleRequiredFieldGroup(ref SampleRequiredFieldGroupInfo requiredFieldGroup)
		{
			return AIMData.SaveSampleRequiredFieldGroup(ref requiredFieldGroup);
		}

		// task 1155 FG 8/5/26 - add one required field to a group
		public static ErrorInfo SaveSampleRequiredFieldGroupDetail(ref SampleRequiredFieldGroupDetailInfo requiredFieldGroupDetail)
		{
			return AIMData.SaveSampleRequiredFieldGroupDetail(ref requiredFieldGroupDetail);
		}

		//5/18/23
		public static ErrorInfo  SpecificIngredientsSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			return AIMData.SpecificIngredientsSaveAs(oldGroupID, newGroupID);
		}

		//5/18/23
		public static ErrorInfo  SpecificProductsSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			return AIMData.SpecificProductsSaveAs(oldGroupID, newGroupID);
		}

		public static ErrorInfo  SaveSampleByOwner(Boolean isActive, Int32 ownerID)
		{
			return AIMData.SaveSampleByOwner(isActive, ownerID);
		}
		public static ErrorInfo  SaveSampleByOwnerAndOwnerLocation(Boolean isActive, Int32 ownerID, Int32 ownerLocationID)
		{
			return AIMData.SaveSampleByOwnerAndOwnerLocation(isActive, ownerID, ownerLocationID);
		}


		public static ErrorInfo  SaveSampleBySupplier(Boolean isActive, Int32 supplierID)
		{
			return AIMData.SaveSampleBySupplier(isActive, supplierID);
		}
		public static ErrorInfo  SaveSampleBySupplierAndSupplierLocation(Boolean isActive, Int32 supplierID, Int32 supplierLocationID)
		{
			return AIMData.SaveSampleBySupplierAndSupplierLocation(isActive, supplierID, supplierLocationID);
		}



		public static ErrorInfo  SaveAIMConfig()
		{
			return AIMData.SaveAIMConfig();
		}

		//5/19/23
		public static ErrorInfo  SaveAIMConfig(Int32 cfcUserID, Int32 activeDatabaseID, Int32 isActive)
		{
			return AIMData.SaveAIMConfig(cfcUserID, activeDatabaseID, isActive);
		}

		public static ErrorInfo  DeleteAIMConfig()
		{
			return AIMData.DeleteAIMConfig();
		}

		public static ErrorInfo  SaveAnalysisLog(ref AnalysisLogInfo log)
		{
			return AIMData.SaveAnalysisLog(ref log);
		}
		public static ErrorInfo  SaveAnalysisViewTemplate(ref AnalysisViewTemplateInfo viewTemplate)
		{
			return AIMData.SaveAnalysisViewTemplate(ref viewTemplate);
		}

		public static Int32 DeleteAnalysisViewTemplate(ref AnalysisViewTemplateInfo viewTemplate)
		{
			return AIMData.DeleteAnalysisViewTemplate(ref viewTemplate);
		}


		public static Int32 DeleteSampleQualityControl(ref SampleQualityControlInfo sampleQuality)
		{
			return AIMData.DeleteSampleQualityControl(ref sampleQuality);
		}

		public static ErrorInfo  SaveReportQuery(ref ReportQueryInfo refInfo)
		{
			return AIMData.SaveReportQuery(ref refInfo);
		}
		public static ErrorInfo  SaveQueryTemplate(ref QueryTemplateInfo queryTemplate)
		{
			return AIMData.SaveQueryTemplate(ref queryTemplate);
		}
		public static ErrorInfo  SaveQueryTemplateDetail(ref QueryTemplateDetailInfo queryTemplateDetail)
		{
			return AIMData.SaveQueryTemplateDetail(ref queryTemplateDetail);
		}

		public static ErrorInfo  SaveQueryTemplateDetailNutrient(Int32 queryTemplateDetailNutrientID, Int32 queryTemplateDetailID, Int32 aimNutrientID, Int32 cfcUserID)
		{
			return AIMData.SaveQueryTemplateDetailNutrient(queryTemplateDetailNutrientID, queryTemplateDetailID, aimNutrientID, cfcUserID);
		}

		//6/15/22
		public static ErrorInfo  SaveQueryTemplateDetailNutrientDataSource(Int32 queryTemplateDetailNutrientID, Int32 queryTemplateDetailID, Int32 dataSourceTypeID, Int32 aimNutrientID, Int32 cfcUserID)
		{
			return AIMData.SaveQueryTemplateDetailNutrientDataSource(queryTemplateDetailNutrientID, queryTemplateDetailID, dataSourceTypeID, aimNutrientID, cfcUserID);
		}

		public static ErrorInfo  CopyQueryTemplateDetail(Int32 queryTemplateID, Int32 CopyFromqueryTemplateID, Int32 CFCUserID)
		{
			return AIMData.CopyQueryTemplateDetail(queryTemplateID, CopyFromqueryTemplateID, CFCUserID);
		}

		public static ErrorInfo  SaveQueryOperation(ref QueryOperationInfo queryOperation)
		{
			return AIMData.SaveQueryOperation(ref queryOperation);
		}
		public static ErrorInfo  SaveQueryField(ref QueryFieldInfo queryField)
		{
			return AIMData.SaveQueryField(ref queryField);
		}
		public static ErrorInfo  SaveAnalysisTemplate(ref AnalysisTemplateInfo refInfo)
		{
			return AIMData.SaveAnalysisTemplate(ref refInfo);
		}
		public static ErrorInfo  SaveReportQueryDetail(ref ReportQueryDetailInfo refInfo)
		{
			return AIMData.SaveReportQueryDetail(ref refInfo);
		}


		public static ErrorInfo  SaveCFCProductRefAIMMaterial(ref CFCProductRefAIMMaterialInfo refInfo)
		{
			return AIMData.SaveCFCProductRefAIMMaterial(ref refInfo);
		}

		public static ErrorInfo  SaveCFCIngredientRefAIMMaterial(ref CFCIngredientRefAIMMaterialInfo refInfo)
		{
			return AIMData.SaveCFCIngredientRefAIMMaterial(ref refInfo);
		}

		public static ErrorInfo  SaveCFCNutrientRefAIMNutrient(ref CFCNutrientRefAIMNutrientInfo refInfo)
		{
			return AIMData.SaveCFCNutrientRefAIMNutrient(ref refInfo);
		}

		public static ErrorInfo  SaveAIMNutrientRefAnalyzerNutrient(ref AIMNutrientRefAnalyzerNutrientInfo refInfo)
		{
			return AIMData.SaveAIMNutrientRefAnalyzerNutrient(ref refInfo);
		}
		public static ErrorInfo  SaveAIMSupplierRefAnalyzerSupplier(ref AIMSupplierRefAnalyzerSupplierInfo refInfo)
		{
			return AIMData.SaveAIMSupplierRefAnalyzerSupplier(ref refInfo);
		}

		//11/27/18
		public static ErrorInfo  UpdateAIMSupplierRefAnalyzerSupplier(ref SampleInfo sampleToBeReplaced, ref SampleInfo sample, Boolean removeSupplierLocation)
		{
			return AIMData.UpdateAIMSupplierRefAnalyzerSupplier(ref sampleToBeReplaced, ref sample, removeSupplierLocation);
		}

		//11/29/18
		public static ErrorInfo  UpdateAIMMaterialRefAnalyzerMaterial(ref SampleInfo sampleToBeReplaced, ref SampleInfo sample, Boolean removeMaterial)
		{
			return AIMData.UpdateAIMMaterialRefAnalyzerMaterial(ref sampleToBeReplaced, ref sample, removeMaterial);
		}

		public static ErrorInfo  SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref AIMSupplierLocationRefAnalyzerSupplierLocationInfo refInfo)
		{
			return AIMData.SaveAIMSupplierLocationRefAnalyzerSupplierLocation(ref refInfo);
		}
		public static ErrorInfo  SaveAIMMaterialRefAnalyzerMaterial(ref AIMMaterialRefAnalyzerMaterialInfo refInfo)
		{
			return AIMData.SaveAIMMaterialRefAnalyzerMaterial(ref refInfo);
		}

		public static ErrorInfo  SaveAnalyzerSupplierToAim(Int32 analyzerSupplierID, Int32 cfcUserID)
		{
			return AIMData.SaveAnalyzerSupplierToAim(analyzerSupplierID, cfcUserID);
		}

		public static ErrorInfo  SaveAnalyzerSupplierLocationToAim(Int32 analyzerSupplierLocationID, Int32 analyzerSupplierID, Int32 cfcUserID)
		{
			return AIMData.SaveAnalyzerSupplierLocationToAim(analyzerSupplierLocationID, analyzerSupplierID, cfcUserID);
		}

		public static ErrorInfo  SaveAnalyzerOwnerLocationToAim(Int32 analyzerOwnerLocationID, Int32 analyzerOwnerID, Int32 cfcUserID)
		{
			return AIMData.SaveAnalyzerOwnerLocationToAim(analyzerOwnerLocationID, analyzerOwnerID, cfcUserID);
		}


		public static ErrorInfo  SaveUserDefinedSourceLocationToAim(Int32 userdefinedSourceLocationID, Int32 userdefinedSourceID, Int32 cfcUserID)
		{
			return AIMData.SaveUserDefinedSourceLocationToAim(userdefinedSourceLocationID, userdefinedSourceID, cfcUserID);
		}

		public static AIMMaterialRefAnalyzerMaterialInfo GetAIMMaterialRefAnalyzerMaterialID(Int32 analyzerMaterialID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialID(analyzerMaterialID);
		}

		public static AIMNutrientRefAnalyzerNutrientInfo GetAIMNutrientRefAnalyzerNutrientAIMID(Int32 aimNutrientID)
		{
			return AIMData.GetAIMNutrientRefAnalyzerNutrientAIMID(aimNutrientID);
		}
		public static AIMNutrientRefAnalyzerNutrientInfo GetAIMNutrientRefAnalyzerNutrientAnalyzerID(Int32 analyzerNutrientID)
		{
			return AIMData.GetAIMNutrientRefAnalyzerNutrientAnalyzerID(analyzerNutrientID);
		}

		public static AIMSupplierRefAnalyzerSupplierInfo GetAIMSupplierRefAnalyzerSupplierAnalyzerID(Int32 analyzerSupplierID)
		{
			return AIMData.GetAIMSupplierRefAnalyzerSupplierAnalyzerID(analyzerSupplierID);
		}

		public static AIMSupplierLocationRefAnalyzerSupplierLocationInfo GetAIMSupplierLocationRefAnalyzerSupplierLocationAnalyzerID(Int32 analyzerSupplierLocationID)
		{
			return AIMData.GetAIMSupplierLocationRefAnalyzerSupplierLocationAnalyzerID(analyzerSupplierLocationID);
		}

		public static AIMMaterialRefAnalyzerMaterialInfo GetAIMMaterialRefAnalyzerMaterialAIMID(Int32 aimMaterialID)
		{
			return AIMData.GetAIMMaterialRefAnalyzerMaterialAIMID(aimMaterialID);
		}


		public static ErrorInfo  SavePrinterDefault(ref PrinterDefaultInfo refInfo)
		{
			return AIMData.SavePrinterDefault(ref refInfo);
		}

		//6/15/18
		public static ErrorInfo  SaveUserPrinterDefault(ref PrinterUserDefaultInfo refInfo)
		{
			return AIMData.SaveUserPrinterDefault(ref refInfo);
		}

		public static ErrorInfo  SaveSampleResultBySampleResultID(ref AnalysisInfo analysis)
		{
			return AIMData.SaveSampleResultBySampleResultID(ref analysis);
		}
		public static ErrorInfo  SaveSampleResultForResultValue(ref SampleResultInfo sampleResult)
		{
			return AIMData.SaveSampleResultForResultValue(ref sampleResult);
		}

		//1/9/18 
		public static ErrorInfo  SaveSampleResultForResultValueQualityControl(ref SampleQualityInfo qualityResult)
		{
			return AIMData.SaveSampleResultForResultValueQualityControl(ref qualityResult);
		}

		public static ErrorInfo  SaveMathType(ref MathTypeInfo mathType)
		{
			return AIMData.SaveMathType(ref mathType);
		}

		//1/3/18        
		public static ErrorInfo  SaveSampleQualityEquation(ref SampleQualityEquationInfo equation)
		{
			return AIMData.SaveSampleQualityEquation(ref equation);
		}

		//1/4/18
		public static ErrorInfo  SaveSampleQualityEquationBuild(ref SampleQualityEquationBuildInfo build)
		{
			return AIMData.SaveSampleQualityEquationBuild(ref build);
		}

		//1/4/18
		public static ErrorInfo  UpdateSampleQualityEquationBuildStatus(Int32 equationID, Boolean status, Int32 cfcUserID)
		{
			return AIMData.UpdateSampleQualityEquationBuildStatus(equationID, status, cfcUserID);
		}

		//2/29/24 
		public static ErrorInfo  UpdateIngredientDetailIsOverlay(Int32 headerID)
		{
			return AIMData.UpdateIngredientDetailIsOverlay(headerID);
		}

		public static ErrorInfo  SaveSampleResultByOwnerLocationID(ref AnalysisSampleResult analysis)
		{
			return AIMData.SaveSampleResultByOwnerLocationID(ref analysis);
		}

		public static ErrorInfo  SaveSampleResult(ref AnalysisSampleResult analysis)
		{
			return AIMData.SaveSampleResult(ref analysis);
		}


		public static ErrorInfo  SaveSupplier_WithSampleCheck(ref AIMSupplierInfo supplier, Boolean bypassErrorMsg)
		{
			return AIMData.SaveSupplier_WithSampleCheck(ref supplier, bypassErrorMsg);
		}



		public static ErrorInfo  SaveSupplierLocation_WithSampleCheck(ref AIMSupplierLocationInfo supplierLocation)
		{
			return AIMData.SaveSupplierLocation_WithSampleCheck(ref supplierLocation);
		}


		public static ErrorInfo  SaveSupplierLocation(ref AIMSupplierLocationInfo supplierLocation)
		{
			return AIMData.SaveSupplierLocation(ref supplierLocation);
		}

		public static void SaveSupplierLocationGuarantee(ref AIMSupplierLocationGuaranteeInfo supplierLocationGuarantee)
		{
			AIMData.SaveSupplierLocationGuarantee(ref supplierLocationGuarantee);
		}
		public static ErrorInfo  SaveCFCPlantRefAIMOwnerLocation(ref CFCPlantRefAIMOwnerLocationInfo crossReference)
		{
			return AIMData.SaveCFCPlantRefAIMOwnerLocation(ref crossReference);
		}

		//4/7/18
		public static ErrorInfo  GenerateAIMProductFromCFC()
		{
			return AIMData.GenerateAIMProductFromCFC();
		}

		//4/7/18
		public static ErrorInfo  GenerateAIMProductXRefFromCFC()
		{
			return AIMData.GenerateAIMProductXRefFromCFC();
		}

		public static ErrorInfo  SaveCFCIngredient(ref CFCIngredientInfo CFCIngredient)
		{
			return AIMData.SaveCFCIngredient(ref CFCIngredient);
		}

		public static ErrorInfo  SaveCFCNutrient(ref CFCNutrientInfo CFCIngredient)
		{
			return AIMData.SaveCFCNutrient(ref CFCIngredient);
		}

		//2/14/18
		public static ErrorInfo  DeleteIngredientHeaderAndDetail(Int32 headerID)
		{
			return AIMData.DeleteIngredientHeaderAndDetail(headerID);
		}


		public static ErrorInfo  SaveIngredientHeader(ref AnalysisExportBatchMaintenance exportBatch)
		{
			return AIMData.SaveIngredientHeader(ref exportBatch);
		}

		public static ErrorInfo  SaveGuaranteeRefundHeader(ref GuaranteeRefundHeaderInfo guaranteeHeader)
		{
			return AIMData.SaveGuaranteeRefundHeader(ref guaranteeHeader);
		}

		public static ErrorInfo  SaveGuaranteeRefundDetail(Int32 batchID, String sampleCode, Int32 status)
		{
			return AIMData.SaveGuaranteeRefundDetail(batchID, sampleCode, status);
		}

		public static ErrorInfo  SaveGuaranteeRefundDetail(Int32 detailID, Int32 status)
		{
			return AIMData.SaveGuaranteeRefundDetail(detailID, status);
		}

		public static ErrorInfo  SaveGuaranteeRefundDetail(ref GuaranteeRefundDetailInfo guaranteeDetail)
		{
			return AIMData.SaveGuaranteeRefundDetail(ref guaranteeDetail);
		}

		public static List<GuaranteeRefundDetailInfo> GetGuaranteeRefundDetail()
		{
			return AIMData.GetGuaranteeRefundDetail();
		}

		public static List<GuaranteeRefundDetailInfo> GetGuaranteeRefund()
		{
			return AIMData.GetGuaranteeRefund();
		}

		public static List<GuaranteeRefundHeaderInfo> GetGuaranteeRefundHeader()
		{
			return AIMData.GetGuaranteeRefundHeader();
		}

		public static List<GuaranteeRefundDetailInfo> GetGuaranteeRefundDetail(Int32 batchID, String sampleCode)
		{
			return AIMData.GetGuaranteeRefundDetail(batchID, sampleCode);
		}

		public static ErrorInfo  SaveDataSourceHeader(ref DataSourceHeaderInfo hdr)
		{
			return AIMData.SaveDataSourceHeader(ref hdr);
		}


		public static ErrorInfo  SaveImportConfig(ref ImportConfigInfo importConfig)
		{
			return AIMData.SaveImportConfig(ref importConfig);
		}


		public static ErrorInfo  SaveImportConfigConnectName(Int32 importConfigID, String connectName, Int32 cfcUserID)
		{
			return AIMData.SaveImportConfigConnectName(importConfigID, connectName, cfcUserID);
		}

		public static ErrorInfo  SaveAnalyzerInstrument(ref AnalyzerInstrumentInfo instrument)
		{
			return AIMData.SaveAnalyzerInstrument(ref instrument);
		}

		public static ErrorInfo  SaveAIMOwner_WithSampleCheck(ref AIMOwnerInfo owner, Boolean bypassErrorMsg)
		{
			return AIMData.SaveAIMOwner_WithSampleCheck(ref owner, bypassErrorMsg);
		}

		public static ErrorInfo  SampleCountAimID(Int32 ID, String Name)
		{
			return AIMData.SampleCountAimID(ID, Name);
		}

		public static ErrorInfo  SaveAIMOwner(ref AIMOwnerInfo owner, Boolean bypassErrorMsg)
		{
			return AIMData.SaveAIMOwner(ref owner, bypassErrorMsg);
		}


		public static ErrorInfo  SaveAIMSupplier_WithSampleCheck(ref AIMSupplierInfo supplier, Boolean bypassErrorMsg)
		{
			return AIMData.SaveAIMSupplier_WithSampleCheck(ref supplier, bypassErrorMsg);
		}


		public static ErrorInfo  SaveAIMSupplier(ref AIMSupplierInfo supplier, Boolean bypassErrorMsg)
		{
			return AIMData.SaveAIMSupplier(ref supplier, bypassErrorMsg);
		}

		public static ErrorInfo  SaveUnitType(ref UnitTypeInfo units, Boolean showErrorMessage)
		{
			return AIMData.SaveUnitType(ref units, showErrorMessage);
		}

		public static ErrorInfo  SaveAIMOwnerLocation_WithSampleCheck(ref AIMOwnerLocationInfo ownerLocation)
		{
			return AIMData.SaveAIMOwnerLocation_WithSampleCheck(ref ownerLocation);
		}

		public static ErrorInfo  DeleteAIMOwner(Int32 aimOwnerID)
		{
			return AIMData.DeleteAIMOwner(aimOwnerID);
		}

		public static ErrorInfo  DeleteAIMSupplier(Int32 aimSupplierID)
		{
			return AIMData.DeleteAIMSupplier(aimSupplierID);
		}


		public static ErrorInfo  DeleteAIMOwnerLocation(Int32 aimOwnerID, Int32 aimOwnerLocationID)
		{
			return AIMData.DeleteAIMOwnerLocation(aimOwnerID, aimOwnerLocationID);
		}

		//7/1/20
		public static void DeleteAIMMaterialStockStatus(Int32 ownerLocationID)
		{
			AIMData.DeleteAIMMaterialStockStatus(ownerLocationID);
		}

		public static ErrorInfo  DeleteAIMSupplierLocation(Int32 aimSupplierID, Int32 aimSupplierLocationID)
		{
			return AIMData.DeleteAIMSupplierLocation(aimSupplierID, aimSupplierLocationID);
		}

		public static ErrorInfo  SaveAIMOwnerLocation(ref AIMOwnerLocationInfo ownerLocation)
		{
			return AIMData.SaveAIMOwnerLocation(ref ownerLocation);
		}

		public static ErrorInfo  SaveAnalyzerPendingSample(ref AnalyzerPendingSampleInfo analyzer)
		{
			return AIMData.SaveAnalyzerPendingSample(ref analyzer);
		}

		//8/1/22
		public static List<APILogInfo> GetAPILog(Int32 sampleID, Int32 datasourcetypeID)
		{
			return AIMData.GetAPILog(sampleID, datasourcetypeID);
		}

		//8/1/22
		public static ErrorInfo  InsertAPILog(Int32 sampleID, Int32 apiSampleID, Int32 datasourcetypeID)
		{
			return AIMData.InsertAPILog(sampleID, apiSampleID, datasourcetypeID);
		}

		//5/24/19
		public static ErrorInfo  WriteToLog(String text)
		{
			return AIMData.WriteToLog(text);
		}

		public static ErrorInfo  SaveDefaultSettings(ref DefaultSettingsInfo defaultSettings)
		{
			return AIMData.SaveDefaultSettings(ref defaultSettings);
		}

		//12/16/20
		public static ErrorInfo  AnalysisUserDefaultSettingsSave(ref AnalysisUserDefaultSettingsInfo analysisUserDefaults)
		{
			return AIMData.AnalysisUserDefaultSettingsSave(ref analysisUserDefaults);
		}


		public static ErrorInfo  SaveSystemSettings(ref SystemSettingsInfo systemSettings)
		{
			return AIMData.SaveSystemSettings(ref systemSettings);
		}

		public static ErrorInfo  SaveAnalyzerMaterial(ref AnalyzerMaterialInfo analyzerMaterial, Boolean returnError)
		{
			return AIMData.SaveAnalyzerMaterial(ref analyzerMaterial, returnError);
		}
		public static ErrorInfo  SaveUserDefinedDataSourceTemplate(ref UserDefinedDataSourceTemplateInfo template)
		{
			return AIMData.SaveUserDefinedDataSourceTemplate(ref template);
		}

		//12/12/18
		public static ErrorInfo  SaveUserDefinedDataSourceTemplate(Int32 defID, Int32 mapColumnID, String mapColumnName, Int32 cfcUserID)
		{
			return AIMData.SaveUserDefinedDataSourceTemplate(defID, mapColumnID, mapColumnName, cfcUserID);
		}

		public static ErrorInfo  SaveSampleImportLog(ref SampleImportLogInfo sample)
		{
			return AIMData.SaveSampleImportLog(ref sample);
		}

		public static ErrorInfo  SaveDataSourceUserDefinition(ref UserDefinedDataSourceDefinitionInfo def)
		{
			return AIMData.SaveDataSourceUserDefinition(ref def);
		}

		public static ErrorInfo  SaveAIMDatabaseReturnID(ref AIMDatabaseInfo def)
		{
			return AIMData.SaveAIMDatabaseReturnID(ref def);
		}


		public static ErrorInfo  SaveDataSourceTypeRefFileType(ref DataSourceTypeRefFileTypeInfo def)
		{
			return AIMData.SaveDataSourceTypeRefFileType(ref def);
		}

		//9/5/23
		public static ErrorInfo  SaveReportComments(ref ReportCommentsInfo comments)
		{
			return AIMData.SaveReportComments(ref comments);
		}

		public static ErrorInfo  SaveDataSourceDetail(ref DataSourceDetailInfo def)
		{
			return AIMData.SaveDataSourceDetail(ref def);
		}

		//12/21/20
		public static ErrorInfo  SaveAnalyzerSupplierLocationCombined(ref AnalyzerSupplierLocationCombinedInfo AnalyzerSupplierLocationCombined)
		{
			return AIMData.SaveAnalyzerSupplierLocationCombined(ref AnalyzerSupplierLocationCombined);
		}

		public static ErrorInfo  SaveAnalyzerSupplier(ref AnalyzerSupplierInfo AnalyzerSupplier)
		{
			return AIMData.SaveAnalyzerSupplier(ref AnalyzerSupplier);
		}

		public static ErrorInfo  SaveAnalyzerSupplierLocation(ref AnalyzerSupplierLocationInfo analyzerSupplierLocation)
		{
			return AIMData.SaveAnalyzerSupplierLocation(ref analyzerSupplierLocation);
		}

		public static ErrorInfo  SaveAnalyzerNutrient(ref AnalyzerNutrientInfo AnalyzerNutrient)
		{
			return AIMData.SaveAnalyzerNutrient(ref AnalyzerNutrient);
		}

		public static ErrorInfo  CountAnalyzerNutrient(Int32 AnalyzerNutrientID)
		{
			return AIMData.CountAnalyzerNutrient(AnalyzerNutrientID);
		}

		public static ErrorInfo  CountQueryTemplate(Int32 queryTemplateID, Int32 queryFieldID)
		{
			return AIMData.CountQueryTemplate(queryTemplateID, queryFieldID);
		}

		public static ErrorInfo  CountQueryTemplateTotal(Int32 queryTemplateID)
		{
			return AIMData.CountQueryTemplateTotal(queryTemplateID);
		}


		public static ErrorInfo  CountSampleCode(String sampleCode)
		{
			return AIMData.CountSampleCode(sampleCode);
		}

		public static ErrorInfo  SaveWarningLimits(ref WarningLimitsInfo warningLimits)
		{
			return AIMData.SaveWarningLimits(ref warningLimits);
		}

		//1/2/18
		public static ErrorInfo  SaveSampleQualityParameter(ref SampleQualityParameterInfo parameters)
		{
			return AIMData.SaveSampleQualityParameter(ref parameters);
		}

		public static ErrorInfo  SaveEmailAddressXRefMessage(ref EmailAddressXRefMessageInfo info)
		{
			return AIMData.SaveEmailAddressXRefMessage(ref info);
		}


		public static List<EmailAddressXRefMessageInfo> GetEmailMessageXrefMessage(Int32 CFCUserID, Int32 messageID)
		{
			return AIMData.GetEmailMessageXrefMessage(CFCUserID, messageID);
		}



		public static ErrorInfo  SaveWebService(ref WebServiceInfo webService)
		{
			return AIMData.SaveWebService(ref webService);
		}


		public static ErrorInfo  SaveIngredientDetail(ref UpdateIngredientDetail exportbatch)
		{
			return AIMData.SaveIngredientDetail(ref exportbatch);
		}


		public static ErrorInfo  SaveIngredientDetailNumberOfSamples(Int32 UpdateIngredientDetailID, Int32 NumberOfSamples)
		{
			return AIMData.SaveIngredientDetailNumberOfSamples(UpdateIngredientDetailID, NumberOfSamples);
		}

		public static ErrorInfo  SaveIngredientDetailStatusAll(Int32 UpdateIngredientHeaderID, Int32 Status, Int32 Target)
		{
			return AIMData.SaveIngredientDetailStatusAll(UpdateIngredientHeaderID, Status, Target);
		}

		public static ErrorInfo  SaveIngredientDetailStatus(Int32 UpdateIngredientDetailID, Int32 Status, Int32 Target, Boolean BypassIsOverlay, Boolean bypassErrorMsg)
		{
			return AIMData.SaveIngredientDetailStatus(UpdateIngredientDetailID, Status, Target, BypassIsOverlay, bypassErrorMsg);
		}

		//2/12/24
		//6/6/19
		//public static ErrorInfo  SaveIngredientDetailStatusOverride(Int32 UpdateIngredientDetailID, Int32 Status, Int32 Target, Boolean BypassIsOverlay, Boolean bypassErrorMsg, Decimal overrideAmt)
		//{
		//	return AIMData.SaveIngredientDetailStatusOverride(UpdateIngredientDetailID, Status, Target, BypassIsOverlay, bypassErrorMsg, overrideAmt);
		//}
		public static ErrorInfo  SaveIngredientDetailStatusOverride(Int32 UpdateIngredientDetailID, Int32 Status, Decimal overrideAmt)
		{
			return AIMData.SaveIngredientDetailStatusOverride(UpdateIngredientDetailID, Status, overrideAmt);
		}


		public static ErrorInfo  SaveAIMNutrient(ref AIMNutrientInfo aimNutrient, Boolean showErrorMessage)
		{
			return AIMData.SaveAIMNutrient(ref aimNutrient, showErrorMessage);
		}

		//10/13/22
		public static ErrorInfo  SaveAIMNutrientDriver(ref AIMNutrientDriverInfo aimNutrient)
		{
			return AIMData.SaveAIMNutrientDriver(ref aimNutrient);
		}

		public static ErrorInfo  SaveAnalyzerAnalyse(ref AnalyzerAnalyseInfo analyse)
		{
			return AIMData.SaveAnalyzerAnalyse(ref analyse);
		}

		public static ErrorInfo  SaveCFCPlant(ref CFCPlantInfo cfcPlant)
		{
			return AIMData.SaveCFCPlant(cfcPlant);
		}

		public static ErrorInfo  SaveCFCProduct(ref CFCProductInfo cfcProduct)
		{
			return AIMData.SaveCFCProduct(cfcProduct);
		}

		public static Int32 SaveAIMDatabase(ref AIMDatabaseInfo aimDatabase)
		{
			return AIMData.SaveAIMDatabase(ref aimDatabase);
		}


		public static Int32 CopyAIMDatabaseXRef(Int32 aimDatabaseID, Int32 CopyFromAimDatabaseID, Int32 CFCUserID)
		{
			return AIMData.CopyAIMDatabaseXRef(aimDatabaseID, CopyFromAimDatabaseID, CFCUserID);
		}

		public static Int32 DeleteAIMDatabase(ref AIMDatabaseInfo aimDatabase)
		{
			return AIMData.DeleteAIMDatabase(ref aimDatabase);
		}


		public static ErrorInfo  UpdateSampleCode(Int32 sampleID, String sampleCode)
		{
			return AIMData.UpdateSampleCode(sampleID, sampleCode);
		}

		public static ErrorInfo  UpdateSampleDate()
		{
			return AIMData.UpdateSampleDate();
		}

		public static ErrorInfo  SaveSample(ref SampleInfo sampleInfo)
		{
			return AIMData.SaveSample(ref sampleInfo);
		}

		//5/22/18
		public static ErrorInfo  SaveSample(Int32 sampleID, String lotNumber, Int32 cfcUserID)
		{
			return AIMData.SaveSample(sampleID, lotNumber, cfcUserID);
		}

		//9/27/19
		public static ErrorInfo  SaveSampleBillOfLoad(Int32 sampleID, String billOfLoad, Int32 cfcUserID)
		{
			return AIMData.SaveSampleBillOfLoad(sampleID, billOfLoad, cfcUserID);
		}

		public static ErrorInfo  SaveSampleExportDetail(ref SampleExportDetailInfo exportDetail)
		{
			return AIMData.SaveSampleDetailExport(ref exportDetail);
		}

		public static ErrorInfo  SaveSampleResultTemplateDetail(ref SampleResultTemplateDetailInfo templateDetail)
		{
			return AIMData.SaveSampleResultTemplateDetail(ref templateDetail);
		}

        public static ErrorInfo  SaveSampleResultTemplateDetailGroup(ref SampleResultTemplateDetailGroupInfo templateDetail)			//Task1061 DB251024
        {
            return AIMData.SaveSampleResultTemplateDetailGroup(ref templateDetail);
        }

        public static ErrorInfo  SaveNutrientGroup(ref NutrientGroupInfo nutrientGroup)			//Task1061 DB251027
        {
            return AIMData.SaveNutrientGroup(ref nutrientGroup);
        }
        //1/4/18
        public static ErrorInfo  SaveSampleQualityControlMassEntryDetail(ref SampleQualityControlMassEntryDetailInfo massDetail)
		{
			return AIMData.SaveSampleQualityControlMassEntryDetail(ref massDetail);
		}

		public static ErrorInfo  SaveSampleExportTemplate(ref SampleExportTemplateInfo sample)
		{
			return AIMData.SaveSampleExportTemplate(ref sample);
		}

		public static ErrorInfo  SaveSampleResultTemplate(ref SampleResultTemplateInfo resultTemplate)
		{
			return AIMData.SaveSampleResultTemplate(ref resultTemplate);
		}

		//1/4/18
		public static ErrorInfo  SaveSampleQualityControlMassEntry(ref SampleQualityControlMassEntryInfo mass)
		{
			return AIMData.SaveSampleQualityControlMassEntry(ref mass);
		}

		#region Test Type

		//8/17/23
		public static List<SampleSubmittedSamplesReportInfo> GetSampleSubmitted(ref SampleSubmittedSamplesReportInfo samples)
		{
			return AIMData.GetSampleSubmitted(samples);
		}

		//9/1/23
		public static List<SampleAnalysisRequestFormInfo> GetSampleAnalysisRequestForm(ref SampleAnalysisRequestFormInfo samples)
		{
			return AIMData.GetSampleAnalysisRequestForm(samples);
		}

		//8/10/23
		public static List<SampleTestTypeInfo> GetSampleTestType()
		{
			return AIMData.GetSampleTestType();
		}

		//8/16/23
		public static List<SampleTestTypeInfo> GetSampleTestType(Int32 sampleTestTypeID)
		{
			return AIMData.GetSampleTestType(sampleTestTypeID);
		}

		//8/10/23
		public static ErrorInfo  SaveSampleTestType(ref SampleTestTypeInfo type)
		{
			return AIMData.SaveSampleTestType(ref type);
		}

		//8/10/23
		public static List<SampleTestTypeDetailInfo> GetSampleTestTypeDetail(Int32 ID, Boolean isActive)
		{
			return AIMData.GetSampleTestTypeDetail(ID, isActive);
		}

		//8/10/23
		public static ErrorInfo  SaveSampleTestTypeDetail(ref SampleTestTypeDetailInfo typeDetail)
		{
			return AIMData.SaveSampleTestTypeDetail(ref typeDetail);
		}

		//8/10/23
		public static ErrorInfo  SaveSampleTestTypeDetail(Int32 sampleTestTypeDetailID)
		{
			return AIMData.SaveSampleTestTypeDetail(sampleTestTypeDetailID);
		}

		//8/10/23
		public static void DeleteSampleTestType(Int32 ID)
		{
			AIMData.DeleteSampleTestType(ID);
		}

		//8/10/23
		public static List<SampleTestTypeInfo> GetSampleTestType(String name)
		{
			return AIMData.GetSampleTestType(name);
		}

		//8/10/23
		public static ErrorInfo  GetSampleTestTypeDetailMaxSortNo(Int32 massID)
		{
			return AIMData.GetSampleTestTypeDetailMaxSortNo(massID);
		}

		//8/10/23
		public static ErrorInfo  GetSampleTestTypeDetailSortOrder(Int32 testTypeID, Int32 sortOrder)
		{
			return AIMData.GetSampleTestTypeDetailSortOrder(testTypeID, sortOrder);
		}

		//8/10/23
		public static void DeleteSampleTestTypeDetail(Int32 ID)
		{
			AIMData.DeleteSampleTestTypeDetail(ID);
		}

		//8/18/23  
		public static void DeleteSampleTestTypeResult(Int32 sampleID)
		{
			AIMData.DeleteSampleTestTypeResult(sampleID);
		}

		//8/11/23
		public static List<SampleTestTypeResultInfo> GetTestTypeResults(Int32 sampleID, Boolean isActive)
		{
			return AIMData.GetTestTypeResults(sampleID, isActive);
		}

		//8/11/23
		public static List<SampleTestTypeResultInfo> GetTestTypeResults(Int32 sampleID, Int32 dataSourceTypeID)
		{
			return AIMData.GetTestTypeResults(sampleID, dataSourceTypeID);
		}

		//8/11/23
		public static ErrorInfo  SaveTestTypeResults(ref SampleTestTypeResultInfo type)
		{
			return AIMData.SaveTestTypeResults(ref type);
		}


		//8/14/23
		public static ErrorInfo  SaveTestTypeResults(Int32 sampleTestTypeResultID, Boolean isIncluded, Int32 cfcUserID)
		{
			return AIMData.SaveTestTypeResults(sampleTestTypeResultID, isIncluded, cfcUserID);
		}

		//8/18/23
		public static ErrorInfo  SaveTestTypeResultsIsActive(Int32 sampleTestTypeResultID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.SaveTestTypeResultsIsActive(sampleTestTypeResultID, isActive, cfcUserID);
		}

		//8/18/23
		public static ErrorInfo  SaveTestTypeDetailIsActive(Int32 sampleTestTypeDetailID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.SaveTestTypeDetailIsActive(sampleTestTypeDetailID, isActive, cfcUserID);
		}

		//8/17/23		
		public static ErrorInfo  SaveSampleTestType(Int32 sampleID, Int32 sampleTestTypeID, Int32 cfcUserID)
		{
			return AIMData.SaveSampleTestType(sampleID, sampleTestTypeID, cfcUserID);
		}

		//8/18/23
		public static SampleInfo GetSampleUsingTestType(Int32 sampleTestTypeID)
		{
			return AIMData.GetSampleUsingTestType(sampleTestTypeID);
		}


		#endregion

		public static ErrorInfo  SaveSample(ref SampleInfo sampleToBeReplaced, ref SampleInfo sample)
		{
			return AIMData.SaveSample(ref sampleToBeReplaced, ref sample);
		}

		//8/17/22 - added importType
		public static ErrorInfo  InsertSampleResult(SampleResultInfo sampleResultInfo, String analyzerNutrientName, Int32 importType)
		{
			return AIMData.InsertSampleResult(ref sampleResultInfo, analyzerNutrientName, importType);
		}

		//1/9/18
		public static ErrorInfo  InsertSampleQuality(SampleQualityInfo quality)
		{
			return AIMData.InsertSampleQuality(ref quality);
		}

		//8/17/22 - added importType
		public static ErrorInfo  InsertSampleResult(ref SampleResultInfo sampleResultInfo, ref SampleResultPredictInfo sampleResultPredict, String analyzerNutrientName, Int32 importType)
		{
			return AIMData.InsertSampleResult(ref sampleResultInfo, ref sampleResultPredict, analyzerNutrientName, importType);
		}

		public static ErrorInfo  SaveSampleQualityControl(SampleQualityControlInfo sampleQuality)
		{
			return AIMData.SaveSampleQualityControl(ref sampleQuality);
		}

		public static ErrorInfo  InsertProgressBar(ref ProgressBarInfo progressBar)
		{
			return AIMData.InsertProgressBar(ref progressBar);
		}

		public static void DeleteProgressBar(Int32 progressBarID)
		{
			AIMData.DeleteProgressBar(progressBarID);
		}


		public static void SaveTabOrder(ref TabOrderInfo tabOrderData)
		{
			AIMData.SaveTabOrder(ref tabOrderData);
		}

        public static ErrorInfo  SaveTabOrderGroup(ref TabOrderGroupInfo tabOrderGroupData)			//Task1041 DB250527
        {
            return AIMData.SaveTabOrderGroup(ref tabOrderGroupData);
        }

		public static ErrorInfo  SaveTabOrderDetail(Int32 tabOrderGroupID, Int32 userID)                     //Task1041 DB250528
        {
            return AIMData.SaveTabOrderGroupDetail(tabOrderGroupID, userID);
        }

        public static ErrorInfo  SaveTabOrderDetail(Int32 tabOrderGroupDetailID, Int32 tabOrder, Int32 userID)                     //Task1041 DB250528
        {
            return AIMData.SaveTabOrderGroupDetail(tabOrderGroupDetailID, tabOrder, userID);
        }
        public static void UpdateTemplateDetailSortOrder(ref SampleResultTemplateDetailInfo templateDetail)
		{
			AIMData.UpdateTemplateDetailSortOrder(ref templateDetail);
		}

		public static void SaveAlternateName(ref AlternateNameInfo alternateName)
		{
			AIMData.SaveAlternateName(ref alternateName);
		}

		//6/29/20 - commented, labels moved to Labels table
		//public static void SaveAlternateNameLabels(Int32 formID, Int32 controlID, Int32 alternateID, Int32 labelField, Boolean labelValue, Int32 cfcUserID)
		//{
		//	AIMData.SaveAlternateNameLabels(formID, controlID, alternateID, labelField, labelValue, cfcUserID);
		//}
			

        public static void SaveLabelFormat(Int32 labelFormatID, Int32 labelID, Int32 materialTypeID, Int32 orderNumber, Boolean isBold, Int32 fontSize, Int32 cfcUserID)
        {
            AIMData.SaveLabelFormat(labelFormatID, labelID, materialTypeID, orderNumber, isBold, fontSize, cfcUserID);
        }	



		//12/7/18
		public static void SaveUseAsSampleCode(Int32 templateID, Boolean value, Int32 cfcUserID)
		{
			AIMData.SaveUseAsSampleCode(templateID, value, cfcUserID);
		}

		public static ErrorInfo  SaveCFCUserDataMap(Int32 CFCUserID, Int32 lastActiveDataMapID)
		{
			return AIMData.SaveCFCUserDataMap(CFCUserID, lastActiveDataMapID);
		}

		public static List<SampleExportTemplateInfo> GetSampleExportTemplate()
		{
			return AIMData.GetSampleExportTemplate();
		}

		public static List<AIMUpdateLogInfo> GetAimUpdateLog(Int32 batchID)
		{
			return AIMData.GetAimUpdateLog(batchID);
		}

		public static List<AIMUpdateLogInfo> GetAimUpdateLogLastBatchID()
		{
			return AIMData.GetAimUpdateLogLastBatchID();
		}

		public static List<GuaranteeRefundHeaderInfo> GetGuaranteeHeaderLastBatchID()
		{
			return AIMData.GetGuaranteeHeaderLastBatchID();
        }

        //Task1061 DB251022 public static List<SampleResultTemplateInfo> GetSampleResultTemplate()
        public static List<SampleResultTemplateInfo> GetSampleResultTemplate(Boolean IsGroupTemplate)		// Task1061 DB251022
        {
            //Task1061 DB251022 return AIMData.GetSampleResultTemplate();           
            return AIMData.GetSampleResultTemplate(IsGroupTemplate);			// Task1061 DB251022
        }

		public static List<SampleResultTemplateDetailGroupInfo> GetSampleResultTemplateDetailGroup(Int32 sampleResultTemplateDetail)    // Task1061 DB251024
        {
			return AIMData.GetSampleResultTemplateDetailGroup(sampleResultTemplateDetail);

        }
        public static List<SampleResultTemplateDetailGroupInfo> GetSampleResultTemplateDetailGroup(Int32 sampleResultTemplateDetail, Int32 nutrientGroupID)    // Task1061 DB251028
        {
            return AIMData.GetSampleResultTemplateDetailGroup(sampleResultTemplateDetail, nutrientGroupID);

        }
        public static List<NutrientGroupInfo> GetNutrientGroupList(Int32 sampleResultTemplateDetail)    // Task1061 DB251024
        {
            return AIMData.GetNutrientGroupList(sampleResultTemplateDetail);
        }

        public static List<NutrientGroupInfo> GetNutrientGroupList()    // Task1061 DB251027
        {
            return AIMData.GetNutrientGroupList();
        }

        public static List<NutrientGroupInfo> GetNutrientGroup(String nutrientGroupName)    // Task1061 DB251027
        {
            return AIMData.GetNutrientGroup(nutrientGroupName);
        }

        public static NutrientGroupInfo GetNutrientGroup(Int32 nutrientGroupID)    // Task1061 DB251024
        {
            return AIMData.GetNutrientGroup(nutrientGroupID);
        }
        public static List<SampleQualityControlMassEntryInfo> GetSampleControlMassEntry()
		{
			return AIMData.GetSampleControlMassEntry();
		}

		public static void DeleteNutrientGroup(Int32 nutrientGroupID)					//Task1061 DB251028
		{
			AIMData.DeleteNutrientGroup(nutrientGroupID);
		}
		public static List<NutrientGroupDetailInfo> GetNutrientGroupDetail(Int32 nutrientGroupID)       // Task1061 DB251028
        {
			return AIMData.GetNutrientGroupDetail(nutrientGroupID);

        }

		public static void DeleteSampleResultTemplateDetailGroup(Int32 sampleResultTemplateID)          // Task1061 DB251028
        {
			AIMData.DeleteSampleResultTemplateDetailGroup(sampleResultTemplateID);
        }

        public static void DeleteSampleResultTemplateDetailGroupDetailID(Int32 sampleResultTemplateDetailID)        // Task1061 DB251029
        {
            AIMData.DeleteSampleResultTemplateDetailGroupDetailID(sampleResultTemplateDetailID);
        }

        public static List<SampleResultTemplateInfo> GetSampleResultTemplate(String name)
		{
			return AIMData.GetSampleResultTemplate(name);
		}

		//1/5/18
		public static List<SampleQualityControlMassEntryInfo> GetSampleQualityControlMassEntryName(String name)
		{
			return AIMData.GetSampleQualityControlMassEntryName(name);
		}

		public static List<SampleResultTemplateInfo> GetSampleResultTemplateTemplateID(Int32 templateID)
		{
			return AIMData.GetSampleResultTemplateTemplateID(templateID);
		}

		public static ErrorInfo  UpdateSampleResultTemplateIDIteration(Int32 sampleResultTemplateID)						//Task1061 DB251027
		{
            return AIMData.UpdateSampleResultTemplateIDIteration(sampleResultTemplateID);
        }

		public static List<SampleResultTemplateInfo> GetSampleResultTemplateAIMMaterialIDDataSourceTypeID(Int32 aimMaterialID, Int32 dataSourceTypeID)		//Task1061 251028
		{
			return AIMData.GetSampleResultTemplateAIMMaterialIDDataSourceTypeID(aimMaterialID, dataSourceTypeID);
        }

        public static ErrorInfo  UpdateNutrientGroupDetail(NutrientGroupDetailInfo nutrientGroupDetail)				//Task1061 DB251028
		{
			return AIMData.UpdateNutrientGroupDetail(nutrientGroupDetail);

        }

		public static void DeleteNutrientGroupDetail(Int32 nutrientGoupDetailID)					//Task1061 DB251028
		{
			AIMData.DeleteNutrientGroupDetail(nutrientGoupDetailID);

        }
        //11/28/22		
        public static List<CFCUserInfo> UpdateCFCUsersSpecificIngredientsGroup(Int32 specificIngredientsGroupID, Int32 value)
		{
			return AIMData.UpdateCFCUsersSpecificIngredientsGroup(specificIngredientsGroupID, value);
		}

		//5/18/23
		public static List<CFCUserInfo> UpdateCFCUsersSpecificProductsGroup(Int32 specificProductsGroupID, Int32 value)
		{
			return AIMData.UpdateCFCUsersSpecificProductsGroup(specificProductsGroupID, value);
		}


		//11/28/22		
		public static List<CFCUserInfo> UpdateCFCUsersAIMOwnerLocationGroup(Int32 aimOwnerLocationGroupID, Int32 value)
		{
			return AIMData.UpdateCFCUsersAIMOwnerLocationGroup(aimOwnerLocationGroupID, value);
		}

		public static List<SampleResultTemplateInfo> GetSampleResultTemplateAIMMaterialID(Int32 aimMaterialID)
		{
			return AIMData.GetSampleResultTemplateAIMMaterialID(aimMaterialID);
		}

		public static List<CFCUserLoginLogInfo> GetCFCUsersLoginLog()			//Task1115 DB251105
		{
            return AIMData.GetCFCUsersLoginLog();
        }

		public static ErrorInfo  InsertCFCUserLoginLog(Int32 cfcUserID, String databaseConnection)         //Task1115 DB251105
        {
			return AIMData.InsertCFCUserLoginLog(cfcUserID, databaseConnection);

        }
        //11/28/22
        public static void DeleteOwnerLocationGroupDetail_GroupID(Int32 ID)
		{
			AIMData.DeleteOwnerLocationGroupDetail_GroupID(ID);
		}

		//11/28/22
		public static List<AIMOwnerLocationGroupDetailInfo> GetOwnerLocationGroupDetail_AIMOwnerLocationID(Int32 aimOwnerLocationID)
		{
			return AIMData.GetOwnerLocationGroupDetail_AIMOwnerLocationID(aimOwnerLocationID);
		}

		//11/28/22
		public static ErrorInfo  UpdateOwnerLocationGroupDetailIsActive(Int32 aimOwnerLocationID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.UpdateOwnerLocationGroupDetailIsActive(aimOwnerLocationID, isActive, cfcUserID);
        }


        //Task1062 DB251031 public static List<SampleResultTemplateDetailInfo> GetSampleResultTemplateDetailTemplateID(Int32 templateID, Int32 sampleID, Int32 datasourceTypeID)
        //Task1135 DB251209 public static List<SampleResultTemplateGridInfo> GetSampleResultTemplateDetailTemplateID(Int32 templateID, Int32 sampleID)			//Task1061 DB251031
        public static List<SampleResultTemplateGridInfo> GetSampleResultTemplateDetailTemplateID(Int32 templateID, Int32 sampleID, Int32 aimMaterialID)			//Task1135 DB251209 
        {
            //Task1061 DB251031 return AIMData.GetSampleResultTemplateDetailTemplateID(templateID, sampleID, datasourceTypeID);
            //Task1135 DB251209 return AIMData.GetSampleResultTemplateDetailTemplateID(templateID, sampleID);													//Task1061 DB251031
            return AIMData.GetSampleResultTemplateDetailTemplateID(templateID, sampleID, aimMaterialID);						//Task1135 DB251209
        }

        public static List<SampleResultTemplateGridInfo> GetSampleResultTemplateDetailGroupTemplateID(Int32 templateID)			//Task1061 DB251104
        {
			return AIMData.GetSampleResultTemplateDetailGroupTemplateID(templateID);
        }

        //1/5/18    
        public static List<SampleQualityControlMassEntryDetailInfo> GetSampleQualityControlMassEntry(Int32 massEntryID, Int32 sampleID, Int32 datasourceTypeID)
		{
			return AIMData.GetSampleQualityControlMassEntry(massEntryID, sampleID, datasourceTypeID);
		}

		public static List<CFCUserInfo> GetCFCUserDataMap(Int32 CFCUserID)
		{
			return AIMData.GetCFCUserDataMap(CFCUserID);
		}

		public static List<CFCUserInfo> GetCFCUsers(Boolean IsActive)
		{
			return AIMData.GetCFCUsers(IsActive);
		}

		//9/7/23
		public static List<CFCUserInfo> GetCFCUsersAllButAimSystem(Boolean IsActive)
		{
			return AIMData.GetCFCUsersAllButAimSystem(IsActive);
		}

		//9/1/23
		public static List<CFCUserInfo> GetCFCUser(Int32 cfcUserID)
		{
			return AIMData.GetCFCUser(cfcUserID);
		}

		//4/25/18  
		public static List<CFCUserInfo> GetCFCUsersLoggedInStatus(Boolean loggedInStatus)
		{
			return AIMData.GetCFCUsersLoggedInStatus(loggedInStatus);
		}

		//4/27/18        
		public static List<CFCUserInfo> UpdateCFCUsersLoggedInStatus(Int32 cfcUserID, Boolean loggedInStatus)
		{
			return AIMData.UpdateCFCUsersLoggedInStatus(cfcUserID, loggedInStatus);
		}


		//4/25/18
		public static List<CFCUserInfo> SaveCFCUsersLoggedInStatus(Int32 cfcUserID, Boolean loggedInStatus)
		{
			return AIMData.SaveCFCUsersLoggedInStatus(cfcUserID, loggedInStatus);
		}

		#endregion

		#region Imports
		public static List<SampleImportLogInfo> GetSampleImportLog(Int32 UserDefinedDataSourceDefinitionID)
		{
			return AIMData.GetSampleImportLog(UserDefinedDataSourceDefinitionID);
		}
		public static List<SampleImportLogInfo> GetSampleImportLog()
		{
			return AIMData.GetSampleImportLog();
		}
		#endregion


		#region Material
		public static void DeleteAnalyzerMaterial(Int32 ID)
		{
			AIMData.DeleteAnalyzerMaterial(ID);
		}
		#endregion

		#region Owner
		public static List<AnalyzerOwnerInfo> GetAnalyzerOwner(Int32 dataSourceUserID)
		{
			return AIMData.GetAnalyzerOwner(dataSourceUserID);
		}
		public static List<SampleQualityParameterInfo> GetSampleQualityParameter(Int32 dataSourceTypeID)
		{
			return AIMData.GetSampleQualityParameter(dataSourceTypeID);
		}
		public static List<SampleQualityParameterInfo> GetSampleQualityParameter(Boolean isActive)
		{
			return AIMData.GetSampleQualityParameter(isActive);
		}
		public static List<SampleQualityParameterInfo> GetSampleQualityParameter(Int32 dataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetSampleQualityParameter(dataSourceTypeID, isActive);
		}
		public static List<AnalyzerOwnerInfo> GetAnalyzerOwner(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAnalyzerOwner(DataSourceTypeID, isActive);
		}
		public static List<AnalyzerOwnerInfo> GetAnalyzerOwner_OwnerID(Int32 analyzerOwnerID)
		{
			return AIMData.GetAnalyzerOwner_OwnerID(analyzerOwnerID);
		}
		public static List<AnalyzerOwnerInfo> GetAnalyzerOwner_OwnerID_DataSourceTypeID_IsActive(Int32 analyzerOwnerID, Int32 datasourceTypeID, Int32 IsActive)
		{
			return AIMData.GetAnalyzerOwner_OwnerID_DataSourceTypeID_IsActive(analyzerOwnerID, datasourceTypeID, IsActive);
		}
		public static List<AnalyzerOwnerInfo> GetAnalyzerOwner_DataSourceType_OwnerName(Int32 dataSourceTypeID, String ownerName)
		{
			return AIMData.GetAnalyzerOwner_DataSourceType_OwnerName(dataSourceTypeID, ownerName);
		}
		public static ErrorInfo  SaveAnalyzerOwner(ref AnalyzerOwnerInfo refInfo, Boolean returnError)
		{
			return AIMData.SaveAnalyzerOwner(ref refInfo, returnError);
		}
		public static void DeleteAnalyzerOwner(Int32 ID)
		{
			AIMData.DeleteAnalyzerOwner(ID);
		}
		public static List<AIMOwnerRefAnalyzerOwnerInfo> GetAllAIMOwnerRefAnalyzerOwner()
		{
			return AIMData.GetAllAIMOwnerRefAnalyzerOwner();
		}
		public static List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAllAIMOwnerLocationRefAnalyzerOwnerLocation()
		{
			return AIMData.GetAllAIMOwnerLocationRefAnalyzerOwnerLocation();
		}
		public static AIMOwnerRefAnalyzerOwnerInfo GetAIMOwnerRefAnalyzerOwnerID(Int32 ID)
		{
			return AIMData.GetAIMOwnerRefAnalyzerOwnerID(ID);
		}
		public static List<AIMOwnerRefAnalyzerOwnerInfo> GetAIMOwnerRefAnalyzerOwner(Int32 ID)
		{
			return AIMData.GetAIMOwnerRefAnalyzerOwner(ID);
		}
		public static List<AIMOwnerRefAnalyzerOwnerInfo> GetAIMOwnerRefAnalyzerOwnerDataSourceTypeID(Int32 ID)
		{
			return AIMData.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeID(ID);
		}
		public static List<AIMOwnerRefAnalyzerOwnerInfo> GetAIMOwnerRefAnalyzerOwnerDataSourceTypeIDAnalyzerOwnerID(Int32 dataSourceTypeID, Int32 analyzerOwnerID)
		{
			return AIMData.GetAIMOwnerRefAnalyzerOwnerDataSourceTypeIDAnalyzerOwnerID(dataSourceTypeID, analyzerOwnerID);
		}
		public static ErrorInfo  SaveAIMOwnerRefAnalyzerOwner(ref AIMOwnerRefAnalyzerOwnerInfo refInfo)
		{
			return AIMData.SaveAIMOwnerRefAnalyzerOwner(ref refInfo);
		}
		public static void DeleteAIMOwnerRefAnalyzerOwner(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteAIMOwnerRefAnalyzerOwner(ID, cfcUserID);
		}
		#endregion

		#region Owner Location
		public static List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAnalyzerOwnerLocation(DataSourceTypeID, isActive);
		}
		public static List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 DataSourceTypeID, Int32 AnalyzerOwnerID, Boolean isActive)
		{
			return AIMData.GetAnalyzerOwnerLocation(DataSourceTypeID, AnalyzerOwnerID, isActive);
		}
		public static List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 ownerID)
		{
			return AIMData.GetAnalyzerOwnerLocation(ownerID);
		}
		public static List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocationByDataSource(Int32 DataSourceUserID)
		{
			return AIMData.GetAnalyzerOwnerLocationByDataSource(DataSourceUserID);
		}
		public static ErrorInfo  SaveAnalyzerOwnerLocation(ref AnalyzerOwnerLocationInfo refInfo, Boolean returnError)
		{
			return AIMData.SaveAnalyzerOwnerLocation(ref refInfo, returnError);
		}
		public static List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocationNameList(Int32 dataSourceTypeID, Int32 analyzerOwnerID, String ownerName)
		{
			return AIMData.GetAnalyzerOwnerLocationNameList(dataSourceTypeID, analyzerOwnerID, ownerName);
		}
		public static List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation_OwnerLocationID(Int32 userDefinedOwnerLocationID)
		{
			return AIMData.GetAnalyzerOwnerLocation_OwnerLocationID(userDefinedOwnerLocationID);
		}
		public static List<AnalyzerOwnerLocationInfo> GetAnalyzerOwnerLocation(Int32 analyzerOwnerLocationID, Int32 datasourceTypeID, Int32 isActive)
		{
			return AIMData.GetAnalyzerOwnerLocation(analyzerOwnerLocationID, datasourceTypeID, isActive);
		}
		public static void DeleteAnalyzerOwnerLocation(Int32 ID)
		{
			AIMData.DeleteAnalyzerOwnerLocation(ID);
		}
		public static AIMOwnerLocationRefAnalyzerOwnerLocationInfo GetAnalyzerCrossRefAimOwnerLocationID(Int32 ID)
		{
			return AIMData.GetAnalyzerCrossRefAimOwnerLocationID(ID);
		}

		//1/8/21 - commented, not used
		//public static List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAnalyzerCrossRefAimOwnerLocation(Int32 ID)
		//{
		//	return AIMData.GetAnalyzerCrossRefAimOwnerLocation(ID);
		//}
		public static List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeID(Int32 ID)
		{
			return AIMData.GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeID(ID);
		}
		public static List<AIMOwnerLocationRefAnalyzerOwnerLocationInfo> GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeIDAnalyzerOwnerLocationID(Int32 dataSourceTypeID, Int32 analyzerOwnerLocationID, Int32 analyzerOwnerID)
		{
			return AIMData.GetAnalyzerCrossRefAimOwnerLocationDataSourceTypeIDAnalyzerOwnerLocationID(dataSourceTypeID, analyzerOwnerLocationID, analyzerOwnerID);
		}
		public static ErrorInfo  SaveAnalyzerCrossRefAimOwnerLocation(ref AIMOwnerLocationRefAnalyzerOwnerLocationInfo refInfo)
		{
			return AIMData.SaveAnalyzerCrossRefAimOwnerLocation(ref refInfo);
		}
		public static void DeleteAnalyzerCrossRefAimOwnerLocation(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteAnalyzerCrossRefAimOwnerLocation(ID, cfcUserID);
		}
		public static ErrorInfo  SaveAnalyzerOwnerLocationToAimFromUser(Int32 analyzerOwnerLocationID, Int32 analyzerOwnerID, Int32 cfcUserID)
		{
			return AIMData.SaveAnalyzerOwnerLocationToAimFromUser(analyzerOwnerLocationID, analyzerOwnerID, cfcUserID);
		}
		#endregion

		#region Analyzer DataSource
		public static List<AnalyzerDataSourceTypeInfo> GetAnalyzerDataSource(Int32 DataSourceTypeID, Boolean isActive)
		{
			return AIMData.GetAnalyzerDataSource(DataSourceTypeID, isActive);
		}
		public static ErrorInfo  SaveAnalyzerDataSource(ref AnalyzerDataSourceTypeInfo refInfo, Boolean returnError)
		{
			return AIMData.SaveAnalyzerDataSource(ref refInfo, returnError);
		}
		public static void DeleteAnalyzerDataSource(Int32 ID)
		{
			AIMData.DeleteAnalyzerDataSource(ID);
		}
		public static List<DataSourceDetailInfo> GetDataSourceDetail(Int32 dataSourceTypeID)
		{
			return AIMData.GetDataSourceDetail(dataSourceTypeID);
		}
		public static List<DataSourceDetailInfo> GetDataSourceCrossReference(Int32 dataSourceTypeID)
		{
			return AIMData.GetDataSourceCrossReference(dataSourceTypeID);
		}
		public static List<DataSourceDetailInfo> GetDataSourceDetail(Int32 dataSourceTypeID, Int32 analyzerDataSourceTypeID)
		{
			return AIMData.GetDataSourceDetail(dataSourceTypeID, analyzerDataSourceTypeID);
		}
		#endregion


		public static List<AIMSupplierRefAnalyzerSupplierInfo> GetAnalyzerCrossRefAimSupplierDataSourceTypeIDAnalyzerSupplierID(Int32 dataSourceTypeID, Int32 analyzerSupplierID)
		{
			return AIMData.GetAnalyzerCrossRefAimSupplierDataSourceTypeIDAnalyzerSupplierID(dataSourceTypeID, analyzerSupplierID);
		}
		public static List<AnalyzerCrossReferenceMassUpdateInfo> GetAnalyzerCrossReferenceMassUpdate(Int32 ID, String requiredField)
		{
			return AIMData.GetAnalyzerCrossReferenceMassUpdate(ID, requiredField);
		}
		public static List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> GetAnalyzerCrossRefAimSupplierLocationDataSourceTypeIDAnalyzerSupplierLocationID(Int32 dataSourceTypeID, Int32 analyzerSupplierLocationID, Int32 analyzerSupplierID)
		{
			return AIMData.GetAnalyzerCrossRefAimSupplierLocationDataSourceTypeIDAnalyzerSupplierLocationID(dataSourceTypeID, analyzerSupplierLocationID, analyzerSupplierID);
		}

		public static ErrorInfo  SaveAnalyzerSupplier(ref AnalyzerSupplierInfo refInfo, Boolean returnError)
		{
			return AIMData.SaveAnalyzerSupplier(ref refInfo, returnError);
		}

		//12/21/20
		public static ErrorInfo  SaveAnalyzerSupplierLocationCombined(ref AnalyzerSupplierLocationCombinedInfo refInfo, Boolean returnError)
		{
			return AIMData.SaveAnalyzerSupplierLocationCombined(ref refInfo, returnError);
		}

		public static ErrorInfo  SaveAnalyzerSupplierLocation(ref AnalyzerSupplierLocationInfo refInfo, Boolean returnError)
		{
			return AIMData.SaveAnalyzerSupplierLocation(ref refInfo, returnError);
		}
		public static List<AIMSupplierLocationRefAnalyzerSupplierLocationInfo> GetAIMSupplierLocationRefAnalyzerSupplierLocation(Int32 dataSourceTypeID)
		{
			return AIMData.GetAIMSupplierLocationRefAnalyzerSupplierLocation(dataSourceTypeID);
		}

		public static Boolean ExecuteSqlScript(StringCollection queryList)
		{
			return AIMData.ExecuteSqlScript(queryList);
		}

		//12/21/20
		#region AnalyzerSupplierLocationCombined Cross-Reference
		public static void DeleteAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(Int32 ID, Int32 cfcUserID)
		{
			AIMData.DeleteAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(ID, cfcUserID);
		}
		public static ErrorInfo  SaveAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(ref AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo refInfo)
		{
			return AIMData.SaveAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(ref refInfo);
		}
		public static List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> GetAllAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined()
		{
			return AIMData.GetAllAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined();
		}
		public static AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedAnalyzerID(Int32 ID)
		{
			return AIMData.GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedAnalyzerID(ID);
		}
		public static List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(Int32 dataSourceTypeID)
		{
			return AIMData.GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(dataSourceTypeID);
		}       //12/24/20
		public static List<AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedInfo> GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(Int32 dataSourceTypeID, Int32 ID)
		{
			return AIMData.GetAIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined(dataSourceTypeID, ID);
		}
		#endregion

		//12/7/22
		#region Nutrient Tolerance

		//Header --------------------------------------
		public static List<NutrientToleranceHeaderInfo> GetNutrientToleranceHeader()
		{
			return AIMData.GetNutrientToleranceHeader();
		}
		public static List<NutrientToleranceHeaderInfo> GetNutrientToleranceHeaderName(String headerName)
		{
			return AIMData.GetNutrientToleranceHeaderName(headerName);
		}
		public static ErrorInfo  SaveNutrientToleranceHeader(ref NutrientToleranceHeaderInfo header)
		{
			return AIMData.SaveNutrientToleranceHeader(ref header);
		}
		public static void DeleteNutrientToleranceHeader(Int32 headerID)
		{
			AIMData.DeleteNutrientToleranceHeader(headerID);
		}

		//Detail --------------------------------------
		public static List<NutrientToleranceDetailInfo> GetNutrientToleranceDetail(Int32 headerID)
		{
			return AIMData.GetNutrientToleranceDetail(headerID);
		}
		public static List<NutrientToleranceDetailInfo> GetNutrientToleranceDetail(Int32 headerID, Int32 aimNutrientID)
		{
			return AIMData.GetNutrientToleranceDetail(headerID, aimNutrientID);
		}
		public static ErrorInfo  SaveNutrientToleranceDetail(ref NutrientToleranceDetailInfo detail)
		{
			return AIMData.SaveNutrientToleranceDetail(ref detail);
		}
		public static void DeleteNutrientToleranceDetail(Int32 headerID)
		{
			AIMData.DeleteNutrientToleranceDetail(headerID);
		}
		public static void DeleteNutrientToleranceDetail(Int32 headerID, Int32 aimNutrientID)
		{
			AIMData.DeleteNutrientToleranceDetail(headerID, aimNutrientID);
		}

		//Target --------------------------------------
		public static List<NutrientToleranceDetailTargetInfo> GetNutrientToleranceDetailTarget(Int32 headerID)
		{
			return AIMData.GetNutrientToleranceDetailTarget(headerID);
		}
		public static List<NutrientToleranceDetailTargetInfo> GetNutrientToleranceDetailTarget(Int32 headerID, Int32 aimNutrientID)
		{
			return AIMData.GetNutrientToleranceDetailTarget(headerID, aimNutrientID);
		}
		public static ErrorInfo  SaveNutrientToleranceDetailTarget(ref NutrientToleranceDetailTargetInfo nutrient)
		{
			return AIMData.SaveNutrientToleranceDetailTarget(ref nutrient);
		}
		public static void DeleteNutrientToleranceDetailTarget(Int32 headerID)
		{
			AIMData.DeleteNutrientToleranceDetailTarget(headerID);
		}
		public static void DeleteNutrientToleranceDetailTarget(Int32 headerID, Int32 aimNutrientID)
		{
			AIMData.DeleteNutrientToleranceDetailTarget(headerID, aimNutrientID);
		}


		#endregion

		//12/8/22
		#region Product Tolerance

		//Header --------------------------------------
		public static List<ProductToleranceHeaderInfo> GetProductToleranceHeader()
		{
			return AIMData.GetProductToleranceHeader();
		}
		public static List<ProductToleranceHeaderInfo> GetProductToleranceHeaderName(String headerName)
		{
			return AIMData.GetProductToleranceHeaderName(headerName);
		}
		public static List<ProductToleranceHeaderInfo> GetProductToleranceHeaderID(Int32 headerID)
		{
			return AIMData.GetProductToleranceHeaderID(headerID);
		}
		public static ErrorInfo  SaveProductToleranceHeader(ref ProductToleranceHeaderInfo header)
		{
			return AIMData.SaveProductToleranceHeader(ref header);
		}
		public static void DeleteProductToleranceHeader(Int32 headerID)
		{
			AIMData.DeleteProductToleranceHeader(headerID);
		}

		//Detail --------------------------------------
		public static List<ProductToleranceDetailInfo> GetProductToleranceDetail(Int32 headerID)
		{
			return AIMData.GetProductToleranceDetail(headerID);
		}
		public static List<ProductToleranceDetailInfo> GetProductToleranceDetailMaterial(Int32 aimMaterialID)
		{
			return AIMData.GetProductToleranceDetailMaterial(aimMaterialID);
		}
		public static ErrorInfo  SaveProductToleranceDetail(ref ProductToleranceDetailInfo detail)
		{
			return AIMData.SaveProductToleranceDetail(ref detail);
		}
		public static void DeleteProductToleranceDetail(Int32 headerID, Int32 detailID)
		{
			AIMData.DeleteProductToleranceDetail(headerID, detailID);
		}
		public static void DeleteProductToleranceDetail(Int32 headerID)
		{
			AIMData.DeleteProductToleranceDetail(headerID);
		}

		//Nutrient --------------------------------------											
		public static List<ProductToleranceNutrientInfo> GetProductToleranceNutrient(Int32 headerID)
		{
			return AIMData.GetProductToleranceNutrient(headerID);
		}
		public static List<ProductToleranceNutrientInfo> GetProductToleranceNutrient(Int32 headerID, Int32 aimNutrientID)
		{
			return AIMData.GetProductToleranceNutrient(headerID, headerID);
		}
		public static ErrorInfo  SaveProductToleranceNutrient(ref ProductToleranceNutrientInfo nutrient)
		{
			return AIMData.SaveProductToleranceNutrient(ref nutrient);
		}
		public static void DeleteProductToleranceNutrient(Int32 headerID)
		{
			AIMData.DeleteProductToleranceNutrient(headerID);
		}
		public static void DeleteProductToleranceNutrient_NutrientID(Int32 headerID)
		{
			AIMData.DeleteProductToleranceNutrient_NutrientID(headerID);
		}
		#endregion

		#region Supplier Groups												//8/30/23
		public static List<AIMSupplierGroupInfo> GetSupplierGroup(Boolean isActive)
		{
			return AIMData.GetSupplierGroup(isActive);
		}
		public static List<AIMSupplierGroupDetailInfo> GetSupplierGroupDetail_GroupID(Int32 groupID)
		{
			return AIMData.GetSupplierGroupDetail_GroupID(groupID);
		}
		public static void DeleteSupplierGroupDetail_GroupID(Int32 ID)
		{
			AIMData.DeleteSupplierGroupDetail_GroupID(ID);
		}
		public static List<CFCUserInfo> UpdateCFCUsersAIMSupplierGroup(Int32 aimSupplierGroupID, Int32 value)
		{
			return AIMData.UpdateCFCUsersAIMSupplierGroup(aimSupplierGroupID, value);
		}
		public static ErrorInfo  SaveSupplierGroup(ref AIMSupplierGroupInfo group)
		{
			return AIMData.SaveSupplierGroup(ref group);
		}
		public static void DeleteSupplierGroupDetail(Int32 ID)
		{
			AIMData.DeleteSupplierGroupDetail(ID);
		}
		public static ErrorInfo  SaveSupplierGroupDetail(ref AIMSupplierGroupDetailInfo group)
		{
			return AIMData.SaveSupplierGroupDetail(ref group);
		}
		public static void DeleteSupplierGroup(Int32 ID)
		{
			AIMData.DeleteSupplierGroup(ID);
		}
		public static List<AIMSupplierGroupDetailInfo> GetSupplierGroupDetail_AIMSupplierID(Int32 supplierID)
		{
			return AIMData.GetSupplierGroupDetail_AIMSupplierID(supplierID);
		}
		public static ErrorInfo  UpdateSupplierGroupDetailIsActive(Int32 aimSupplierID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.UpdateSupplierGroupDetailIsActive(aimSupplierID, isActive, cfcUserID);
		}
		public static void DeleteSupplierGroupDetail_AIMSupplierID(Int32 ID)
		{
			AIMData.DeleteSupplierGroupDetail_AIMSupplierID(ID);
		}
		#endregion

		#region Supplier Location Groups									//8/30/23			
		public static List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroup(Boolean isActive)
		{
			return AIMData.GetSupplierLocationGroup(isActive);
		}
		public static List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroupUserListing(Boolean isActive, Int32 supplierGroupID)
		{
			return AIMData.GetSupplierLocationGroupUserListing(isActive, supplierGroupID);
		}
		public static List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroup_GroupID(Int32 groupID)
		{
			return AIMData.GetSupplierLocationGroup_GroupID(groupID);
		}
		public static List<AIMSupplierLocationGroupInfo> GetSupplierLocationGroup_AIMSupplierLocationID(Int32 aimSupplierLocationID)
		{
			return AIMData.GetSupplierLocationGroup_AIMSupplierLocationID(aimSupplierLocationID);
		}
		public static ErrorInfo  UpdateSupplierLocationGroupIsActive(Int32 aimSupplierLocationID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.UpdateSupplierLocationGroupIsActive(aimSupplierLocationID, isActive, cfcUserID);
		}
		public static List<CFCUserInfo> UpdateCFCUsersAIMSupplierLocationGroup(Int32 aimSupplierLocationGroupID, Int32 value)
		{
			return AIMData.UpdateCFCUsersAIMSupplierLocationGroup(aimSupplierLocationGroupID, value);
		}
		public static ErrorInfo  SaveSupplierLocationGroup(ref xAIMSupplierLocationGroupInfo group)
		{
			return AIMData.SaveSupplierLocationGroup(ref group);
		}
		public static ErrorInfo  SaveSupplierLocationGroup(ref AIMSupplierLocationGroupInfo group)
		{
			return AIMData.SaveSupplierLocationGroupDetail(ref group);
		}
		public static void DeleteSupplierLocationGroup(Int32 ID)
		{
			AIMData.DeleteSupplierLocationGroup(ID);
		}
		public static void DeleteSupplierLocationGroupAIMSupplierGroupID(Int32 aimSupplierGroupID)
		{
			AIMData.DeleteSupplierLocationGroupAIMSupplierGroupID(aimSupplierGroupID);
		}
		public static void DeleteSupplierLocationGroupAIMSupplierLocationID(Int32 aimSupplierLocationID)
		{
			AIMData.DeleteSupplierLocationGroupAIMSupplierLocationID(aimSupplierLocationID);
		}



		#endregion

		#region Test Type Groups
		public static ErrorInfo  SaveTestTypeGroup(ref SampleTestTypeGroupInfo group)
		{
			return AIMData.SaveTestTypeGroup(ref group);
		}
		public static List<SampleTestTypeGroupInfo> GetTestTypeGroup(Boolean isActive)
		{
			return AIMData.GetTestTypeGroup(isActive);
		}
		public static List<SampleTestTypeGroupDetailInfo> GetTestTypeGroupDetail_GroupID(Int32 groupID)
		{
			return AIMData.GetTestTypeGroupDetail_GroupID(groupID);
		}
		public static ErrorInfo  SaveTestTypeGroupDetail(ref SampleTestTypeGroupDetailInfo group)
		{
			return AIMData.SaveTestTypeGroupDetail(ref group);
		}
		public static List<CFCUserInfo> UpdateCFCUsersSampleTestTypeGroup(Int32 testTypeGroupID, Int32 value)
		{
			return AIMData.UpdateCFCUsersSampleTestTypeGroup(testTypeGroupID, value);
		}
		public static void DeleteTestTypeGroup(Int32 ID)
		{
			AIMData.DeleteTestTypeGroup(ID);
		}
		//this is called from the Test Type tab
		public static void DeleteTestTypeGroupDetail_TestTypeID(Int32 ID)
		{
			AIMData.DeleteTestTypeGroupDetail_TestTypeID(ID);
		}
		//this is the delete on the TestTypeGroup GRID
		public static void DeleteTestTypeGroupDetail_DetailID(Int32 ID)
		{
			AIMData.DeleteTestTypeGroupDetail_DetailID(ID);
		}
		public static List<SampleTestTypeGroupInfo> GetTestTypeGroup_GroupID(Int32 groupID)
		{
			return AIMData.GetTestTypeGroup_GroupID(groupID);
		}
		//called from Sample screen
		public static List<SampleTestTypeInfo> GetSampleTestTypeGroup(Int32 groupID)
		{
			return AIMData.GetSampleTestTypeGroup(groupID);
		}

		//12/1/23
		//public static List<SampleTestTypeDetailInfo> GetSampleTestTypeGroupDetail(Int32 ID, Boolean isActive)
		public static List<SampleTestTypeGroupDetailInfo> GetSampleTestTypeGroupDetail(Int32 ID, Boolean isActive)
		{
			return AIMData.GetSampleTestTypeGroupDetail(ID, isActive);
		}
		#endregion

		//10/11/23
		#region Export Ingredient Groups
		public static List<ExportIngredientInfo> GetExportIngredient_GroupID(Int32 groupID)
		{
			return AIMData.GetExportIngredient_GroupID(groupID);
		}
		public static List<ExportIngredientGroupInfo> GetExportIngredientGroup(Boolean isActive)
		{
			return AIMData.GetExportIngredientGroup(isActive);
		}
		public static List<ExportIngredientGroupInfo> GetExportIngredientGroup(Int32 groupID)
		{
			return AIMData.GetExportIngredientGroup(groupID);
		}
		public static void DeleteExportIngredient_GroupID(Int32 ID)
		{
			AIMData.DeleteExportIngredient_GroupID(ID);
		}
		public static void DeleteExportIngredientGroup(Int32 ID)
		{
			AIMData.DeleteExportIngredientGroup(ID);
		}
		public static ErrorInfo  SaveExportIngredientGroup(ref ExportIngredientGroupInfo group)
		{
			return AIMData.SaveExportIngredientGroup(ref group);
		}
		public static ErrorInfo  ExportIngredientSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			return AIMData.ExportIngredientSaveAs(oldGroupID, newGroupID);
		}
		public static ErrorInfo  SaveExportIngredient(ref ExportIngredientInfo export)
		{
			return AIMData.SaveExportIngredient(ref export);
		}
		public static void DeleteExportIngredient(Int32 ID)
		{
			AIMData.DeleteExportIngredient(ID);
		}
		public static List<ExportIngredientInfo> GetExportIngredient(Int32 aimMaterialID)
		{
			return AIMData.GetExportIngredient(aimMaterialID);
		}
		public static ErrorInfo  UpdateExportIngredientIsActive(Int32 cfcIngredientID, Boolean isActive, Int32 cfcUserID)
		{
			return AIMData.UpdateExportIngredientIsActive(cfcIngredientID, isActive, cfcUserID);
		}
		public static ErrorInfo  UpdateExportIngredientGroupAIMMaterialID(Int32 groupID, Int32 aimMaterialID, Int32 cfcUserID)
		{
			return AIMData.UpdateExportIngredientGroupAIMMaterialID(groupID, aimMaterialID, cfcUserID);
		}

		//10/25/23
		public static List<ExportIngredientInfo> GetExportIngredientCFCIngredient(Int32 cfcIngredientID)
		{
			return AIMData.GetExportIngredientCFCIngredient(cfcIngredientID);
		}
		#endregion

		//10/11/23
		#region Export Plant Groups

		public static List<ExportPlantInfo> GetExportPlant_GroupID(Int32 groupID)
		{
			return AIMData.GetExportPlant_GroupID(groupID);
		}
		public static List<ExportPlantGroupInfo> GetExportPlantGroup(Boolean isActive)
		{
			return AIMData.GetExportPlantGroup(isActive);
		}
		public static void DeleteExportPlant_GroupID(Int32 ID)
		{
			AIMData.DeleteExportPlant_GroupID(ID);
		}
		public static void DeleteExportPlantGroup(Int32 ID)
		{
			AIMData.DeleteExportPlantGroup(ID);
		}
		public static ErrorInfo  SaveExportPlant(ref ExportPlantInfo export)
		{
			return AIMData.SaveExportPlant(ref export);
		}
		public static void DeleteExportPlant(Int32 ID)
		{
			AIMData.DeleteExportPlant(ID);
		}
		public static List<ExportPlantGroupInfo> GetExportPlantGroup(Int32 groupID)
		{
			return AIMData.GetExportPlantGroup(groupID);
		}
		public static ErrorInfo  UpdateExportPlantGroupAIMOwnerLocationID(Int32 groupID, Int32 aimOwnerLocationID, Int32 cfcUserID)
		{
			return AIMData.UpdateExportPlantGroupAIMOwnerLocationID(groupID, aimOwnerLocationID, cfcUserID);
		}
		public static ErrorInfo  ExportPlantSaveAs(Int32 oldGroupID, Int32 newGroupID)
		{
			return AIMData.ExportPlantSaveAs(oldGroupID, newGroupID);
		}
		public static ErrorInfo  SaveExportPlantGroup(ref ExportPlantGroupInfo group)
		{
			return AIMData.SaveExportPlantGroup(ref group);
		}
        #endregion


        #region Tab Order Group
        public static void DeleteTabOrderGroup(Int32 tabOrderGroupID)
        {
            AIMData.DeleteTabOrderGroup(tabOrderGroupID);
        }


        #endregion

        #region Test for Butterball
        //Task1121 DB251217 Start
        public static List<TestInfo> GetTest()
        {
            return AIMData.GetTest();
        }
        public static TestInfo GetTest(Int32 testID)
        {
            return AIMData.GetTest(testID);
        }
        public static List<TestMaterialInfo> GetTestMaterial()
        {
            return AIMData.GetTestMaterials();
        }

        public static List<TestGroupNameInfo> GetTestGroupName()
        {
            return AIMData.GetTestGroupNames();
        }

        public static List<TestNutrientResultInfo> GetTestNutrientResult()
        {
            return AIMData.GetTestNutrientResults();
        }

        public static ErrorInfo  SaveTest(ref TestInfo test)
        {
            return AIMData.SaveTest(ref test);
        }

        public static ErrorInfo  SaveTestMaterial(ref TestMaterialInfo material)
        {
            return AIMData.SaveTestMaterial(ref material);
        }

        public static ErrorInfo  SaveTestGroupName(ref TestGroupNameInfo group)
        {
            return AIMData.SaveTestGroupName(ref group);
        }

        public static ErrorInfo  SaveTestNutrientResult(ref TestNutrientResultInfo nutrient)
        {
            return AIMData.SaveTestNutrientResult(ref nutrient);
        }

        public static void DeleteTest(Int32 testID)
        {
            AIMData.DeleteTest(testID);
        }

        public static void DeleteTestMaterial(Int32 testMaterialID)
        {
            AIMData.DeleteTestMaterial(testMaterialID);
        }

        public static void DeleteTestGroupName(Int32 testGroupNameID)
        {
            AIMData.DeleteTestGroupName(testGroupNameID);
        }

        public static void DeleteTestNutrientResult(Int32 testNutrientResultID)
        {
            AIMData.DeleteTestNutrientResult(testNutrientResultID);
        }

		public static List<TestGridInfo> GetTestGrid(Boolean isGrouped, DateTime startDate, DateTime endDate, Int32 ownerLocationID)
		{
			return AIMData.GetTestGrid(isGrouped, startDate, endDate, ownerLocationID);
        }

        //Task1121 DB251217 End
        #endregion

        //3/24/25 - commented, not used
        ////6/26/20
        //public static List<LabelsInfo> GetLabels()
        //{
        //	return AIMData.GetLabels();
        //}

        //3/24/25 - commented, not used
        ////6/24/21      
        //public static ErrorInfo  GetLabelDefault(String controlName)
        //{
        //	return AIMData.GetLabelDefault(controlName);
        //}

        //3/18/25
        //public static List<SampleLabelInfo> GetOneSampleLabel(Int32 selectedSampleNumber, Boolean printNameFirst)
        //{
        //	return AIMData.GetOneSampleLabel(selectedSampleNumber, printNameFirst);
        //}

        //3/26/25 - commented, no longer used
        //public static List<SampleLabelInfo> GetOneSampleLabel(Int32 selectedSampleNumber, Boolean printNameFirst, Boolean Elmira)
        //{
        //    return AIMData.GetOneSampleLabel(selectedSampleNumber, printNameFirst, Elmira);
        //}

        //3/18/25
        //public static List<SampleLabelInfo> GetOneSampleProdLabel(Int32 selectedSampleNumber, Boolean printNameFirst)
        //{
        //	return AIMData.GetOneSampleProdLabel(selectedSampleNumber, printNameFirst);
        //}

        //3/26/25 - commented, no longer used
        //public static List<SampleLabelInfo> GetOneSampleProdLabel(Int32 selectedSampleNumber, Boolean printNameFirst, Boolean Elmira)
        //{
        //    return AIMData.GetOneSampleProdLabel(selectedSampleNumber, printNameFirst, Elmira);
        //}

        //3/26/25 - commented, no longer used
        ////6/29/21 - added boldValue
        //public static void SaveLabels(Int32 labelID, Int32 labelField, Boolean labelValue, Boolean boldValue, Int32 cfcUserID)
        //{
        //	AIMData.SaveLabels(labelID, labelField, labelValue, boldValue, cfcUserID);
        //}

        //3/26/25 - commented, no longer used
        ////5/2/23
        //public static void SaveProdLabels(Int32 labelID, Int32 labelField, Boolean labelValue, Boolean boldValue, Int32 cfcUserID)
        //{
        //	AIMData.SaveProdLabels(labelID, labelField, labelValue, boldValue, cfcUserID);
        //}

        //3/26/25 - commented, no longer used
        ////6/26/20
        //public static List<LabelsInfo> GetLabelFieldFirst()
        //{
        //	return AIMData.GetLabelFieldFirst();
        //}

        //3/26/25 - commented, no longer used
        //      //6/26/20
        //      public static List<LabelsInfo> GetLabelFieldLast()
        //{
        //	return AIMData.GetLabelFieldLast();
        //}

        //3/26/25 - commented, no longer used
        //      //6/23/21
        //      public static List<LabelsInfo> GetLabelFieldLine2()
        //{
        //	return AIMData.GetLabelFieldLine2();
        //}

        //3/26/25 - commented, no longer used
        //      //6/23/21
        //      public static List<LabelsInfo> GetLabelFieldLine3()
        //{
        //	return AIMData.GetLabelFieldLine3();
        //}

        //3/26/25 - commented, no longer used
        //6/23/21
        //      public static List<LabelsInfo> GetLabelFieldLine4()
        //{
        //	return AIMData.GetLabelFieldLine4();
        //}

    }
}