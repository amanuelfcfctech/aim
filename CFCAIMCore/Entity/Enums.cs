namespace CFCAIMCore.Entity
{
	public enum MathTypeEnum
	{		
		Default = 0,		
		Lower95Mean = 1,
		Upper95Mean = 2,
		Lower90Mean = 3,
		Upper90Mean = 4,
		Mean = 5,
		MeanPlusHalfStandardDeviation = 6,
		MeanMinusHalfStandardDeviation = 7,
		MeanPlusOneStandardDeviation = 8,
		MeanMinusOneStandardDeviation = 9,
		Custom1 = 10,
		Custom2 = 11,
		Custom3 = 12,
		Custom4 = 13,
		Custom5 = 14,
		Custom6 = 15,
		Custom7 = 16,
		Custom8 = 17,
		Custom9 = 18,
		Custom10 = 19,
	}

	public enum EnumSpecFrom
	{ 
		Formulation = 1,
		Aim = 2
	}

	//10/13/23
	public enum EnumOrderBy
	{ 
		Name = 1,
		Code = 2
	}

	public enum EnumPermissionTable
	{ 
        //Enum values MUST MATCH PermissionID in Permission table!

		AnalysisExport = 1,        
        FormulationOptions = 2,             //3/12/18
		CrossReferenceMaintenance = 3,      //3/12/18
		AllowChangeSampleMaterial = 4,      //3/12/18
		AccessToSampleSettings = 5          //10/27/22
	}

    //1/12/18
    public enum EnumFormID
    {
        Sample = 1,
        Analysis = 2       
    }

    //4/4/18
    public enum EnumGridNumber
    {
        AnalysisIngredient = 1,
        AnalysisProduct = 2,
        UserDefinedSampleReportData = 3
    }

	//4/15/22
	public enum EnumOrphanXRefReport
	{
		AnalyzerIngredient = 1,
		AnalyzerProduct = 2,
		FormulationIngredient = 3,
		FormulationProduct = 4
	}

	//12/23/22
	public enum EnumIngredientOrProduct
	{
		AnalysisIngredient = 1,
		AnalysisProduct = 2
	}

	//6/19/20
	public enum EnumShowCodeNameOrBoth
    {
        Code = 1,
        Name = 2,
        Both = 3
    }

	//1/18/24
	public enum EnumDateFields
	{ 
		SampleDate = 1,
		CreatedDate = 2,
		ProcessDate = 3,
		SentDate = 4,
		ReceivedDate = 5
	}

    //4/2/19
    public enum EnumDecimalSeparator
    {
        Comma = 1,
        Period = 2
    }
		
	public enum EnumPlusMinus
	{	
		Plus = 1,           //3/18/21
		Minus = 2,          //3/18/21
		None = 3            //12/1/22
	}

	public enum EnumSqlCommandType
	{
		Unassigned = 99,
		Print = 0,
		Insert = 1,
		Update = 2,
		Delete = 3,	 
		CreateTable = 4,
		AlterTable = 5,
		DropTable = 6,
		CreateProcedure = 7,
		AlterProcedure = 8,
		DropProcedure = 9,
		CreateFunction = 10,
		AlterFunction = 11,
		DropFunction = 12
	}

	public enum PriorityUsedValue
	{		
		Default = 1,
		First = 2,
		Last = 3,
		DataSource = 4
	}

	//5/19/17
	public enum EnumGuaranteeStatus
	{
		Accept = 1,			//submitted
		Reject = 2,
		ClaimAccpted = 3,
		ClaimDenied = 4

	}


	public enum PrinterTypeEnum
	{		
		Other = 1,	
		Label = 2
	}

	public enum SampleMessageType
	{
		//total count of messages
		TotalCount = 1,
                                                            
        //group messages by SampleResultPredictID
        ResultPredict = 2,

		//detail of messages
		Detail = 3,

		//group messages by AnalyzerNutrientID
		AnalyzerNutrient = 4
	}

	public enum ExportBatchDetailStatusEnum
	{
		Unassigned = 0,		
		Cancelled = 1,		
		Accepted = 2,
        Computed = 3,           //2/9/18
		NoOverlay = 4           //2/9/18
	}

	public enum EmailMessageEnum
	{
		/* These are the message descriptions set up for cfcusers (x-ref)
		   - used to populate EmailAddressXRefMessage table
		*/

		GlobalH = 1,
		MinMax = 2
	}

	//6/15/16
	public enum EnumSampleResultStatus 
	{ 
		Active = 1,
		Inactive = 2,
		Both = 3
	}

	//6/12/17
	public enum EnumActiveIgnoreStatus
	{
		Active = 1,
		Ignore = 2,
		Both = 3
	}

	public enum EnumMessageType
	{
		System = 1,
		User = 2,
		Nutritionist = 3
	}

	public enum EnumSystemLogType
	{
		Info = 1,
		Warning = 2,
		Fatal = 3,
		Debug = 4
	}

	public enum EnumAIMMessageType
	{
		Info = 1,
		Warning = 2,
		Error = 3
	}

	public enum EnumSampleExport
	{
		Cancel = 0,
		Overwrite = 1,
		Append = 2,
	}

	public enum ReportEnum
	{
		AnalysisSupplierComparison = 1,
		AnalysisSummary = 2,
		GuaranteeComparisonByMean = 3,
		GuaranteeComparisonBySample = 4,
		ProductSpecificationComparisonByMean = 5,
		ProductSpecificationComparisonBySample = 6,
		AnalysisDateComparisonReport = 7,
		AimNutrientRefAnalyzerNutrientOphans = 8,
		AimMaterialRefAnalyzerMaterialOphans = 9,
		ExportBatchDetail = 10,
		AnalysisPivotReport = 11,
		SupplierLocationGuarantees = 12,
		AnalysisDetail = 13,
		SampleComparisonReport = 14,
		AnalysisPivotSummary = 15,       
        ManufacturedProductComplianceReport = 16,
        SampleActivityReport = 17,
        SampleResultsBySupplier = 18,
        UserDefinedSampleData = 19,
        UserDefinedSamplePivotData = 20

		//1/19/21
		, AnalysisPivotReportGroupSummary = 21
		, AnalysisPivotReportGroupSummaryWithNoSubTotals = 22   // had to split these 2 apart because when the user selects the "Version 3" report, but then
																// elects to hide all the sub-groups, the supplier and supplier location names would not show
																// up on report. Could not figure how to fix this -- so made another report altogether.
		
		, SampleResultsComparisonReport = 23                    //5/19/22
		, DataSourceComparisonReport = 24                       //6/22/22
		, SamplesByOwnerLocation = 25                           //6/28/22
		, SamplesSubmittedReport = 26                           //8/16/23
		, SampleAnalysisRequestFormEvonik = 27                  //9/1/23
		, SampleAnalysisRequestFormAjinomoto = 28               //9/5/23
		, SampleAnalysisRequestFormUnitedAnimalHealth = 29      //9/5/23

		, ExportLog = 30										//TASK951 6/3/24
		, AnalysisCost = 31										//Task1150 DB251208

	}

    //5/8/18
    public enum EnumProgressBarType
    {
        None = 0,
        RebuildSampleAIM = 1,
        SyncConcept5 = 2,
        ExportToConcept5 = 3
    }


    //4/12/18 TemplateType 0 = report doesn't use a gridControl or a pivotControl
    //        TemplateType 1 = report uses gridControl 
    //        TemplateType 2 = report uses pivotControl
    public enum EnumReportDisplayType
    {
        None = 0,
        GridControl = 1,
        PivotControl = 2

    }

	public enum DataSourceTypeEnum
	{
		/*
		  these numbers are being used as ID in Database
		  please dont reassign anything here, 
		  reassigning will show wrong value in table
		*/
		
		Foss = 1,		
		Adisseo = 2,		
		Evonik = 3,		
		Bruker = 4,		
		LabAdvantage = 5,		
		ABVista = 6,		
		Specman = 7,		
		Perten = 8,						//Perten NIR		
		NutriQuest = 9,					//Iowa Select		
		CFCTech = 10,					//Export-Other - CSV		
		AdisseoAPI = 11,		
		Romer = 12,                     //8/11/22
		AdisseoAPIAnalyseCode = 13

		, Ajinomoto = 14
		, UnitedAnimalHealth = 15

		, ATC = 16


	}

	public enum InternalExternalEnum
	{
		Internal = 0,
		External = 1
	}

	public enum ExportTargetEnum
	{
		/*
		  these numbers are being used as ID in Database
		  please dont reassign anything here, 
		  reassigning will show wrong value in table
		*/
		
		Base = 1,		
		DefaultOverlay = 2,		
		SpecificOverlay = 3,		
		BaseExpand = 4,					//10/13/23 - Expand ingredients for Base		
		DefaultOverlayExpand = 5,       //1/9/24 - Expand ingredients for Default
		SpecificOverlayExpand = 6,      //1/9/24 - Expand ingredients for Specific
		SpecificOverlayPlantGroup = 7	//1/26/24 - Specific plant group		

		// could have added one more enum for: SpecificOverlayPlantGroup + Expand Ingredients

	}

	public enum FileTypeEnum
	{
		/*
		  these numbers are being used as ID in Database
		  please dont reassign anything here, 
		  reassigning will show wrong  value in table
		*/
		
		CSV = 1,		
		XML = 2,		
		TXT = 3,		
		API = 4,		
		NIR = 5,
		XLSX = 6,        
        SQL = 7,																							//6/22/17                
        XLS = 8                                                                                             //5/15/20

    }

    public enum EnumEventRunType
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        OneTime = 4
    }

    public enum EnumEventType
    {
        ImportUserDefined = 1,
        ImportSystem = 2,
        Export = 3
    }

    public enum ImportFileTypeEnum
	{
		CSV = 1,
		XML = 2,
		Text = 3,
		API = 4,
		NIR = 5,		
		Excel = 6,        
		SQL = 7				
	}
        
    public enum EnumImportType
    {
        UpdateResultsOnly = 1,                                  
        AddNewSamplesAndResults = 2,                            
        AddNewSamplesAndResultsORUpdateResults = 3,             
        ForExistingSamplesUpdateResultsAndHeaderFields = 4,     
        UpdateHeaderOnly = 5,
		
		//8/12/22 - similar to "AddNewSamplesAndResultsORUpdateResults" ... except looks for existing samples using SqlReportID field
		RomerResults = 100        
    }

	public enum DelimiterTypeEnum
	{			
		Comma = 1,		
		Semicolon = 2,		
		Verticalbar = 3,		
		Tab = 4
	}

	   
    public enum EnumYesNo
    {
        No = 0,
        Yes = 1
    }

	//1/19/24 - commented, not used
	//9/30/22
	//public enum EnumGroundUnGround
	//{
	//	UnGround = 0,
	//	Ground = 1
	//}

	public enum AnalysisQueryEnum
	{
		
		NoGroup = 1,

		//When SampleID is on, it does not matter if anything else is turned on
		SampleID = 2,		

		SampleDate = 3,		
		SampleDateOwner = 4,		
		SampleDateOwnerSupplier = 5,		
		SampleDateOwnerSupplierLocation = 6,		
		SampleDateOwnerLocation = 7,		
		SampleDateOwnerLocationSupplier = 8,		
		SampleDateOwnerLocationSupplierLocation = 9,		
		SampleDateSupplier = 10,		
		SampleDateSupplierLocation = 11,		
		Owner = 12,		
		OwnerSupplier = 13,		
		OwnerSupplierLocation = 14,		
		OwnerLocation = 15,		
		OwnerLocationSupplier = 16,		
		OwnerLocationSupplierLocation = 17,		
		Supplier = 18,		
		SupplierLocation = 19,

		//3/2/21 - added for AnalysisProduct form and stp
		Version = 26,
		VersionSampleID = 27,
		VersionSampleIDSampleDate = 28,
		VersionSampleIDSampleDateOwner = 29,
		VersionSampleIDSampleDateOwnerOwnerLocation = 30,
		VersionSampleIDOwner = 31,
		VersionSampleIDOwnerOwnerLocation = 32,
		VersionSampleDate = 33,
		VersionSampleDateOwner = 34,
		VersionSampleDateOwnerOwnerLocation = 35

	}
		
	public enum QueryFeildEnum
	{
		/*
		  these numbers are being used as ID in Database
		  please dont reassign anything here, 
		  reassigning will show wrong value in table
		*/
		///<summary>Sample Date</summary>
		SampleDate = 1,		
		Material = 2,		
		Owner = 3,		
		OwnerLocation = 4,		
		Supplier = 5,		
		SupplierLocation = 6,		
		IngredientCode = 7,       
        NutrientCode = 8,		
		MaterialNutrient = 9		

		, SampleType = 10                   //1/25/21
		, DataSourceType = 11               //5/26/22
		, ResultsAnalysisDate = 12          //6/3/22
		, DataSourceComparison = 13         //6/14/22
		, ProductType = 14                  //12/13/22
	}

	public enum QueryOperationEnum
	{
		/*
		  these numbers are being used as ID in Database
		  please dont reassign anything here, 
		  reassigning will show wrong value in table
		*/
		
		PastNDaysToDate = 1,		
		MonthToDate = 2,		
		YearToDate = 3,		
		Equal = 4,		
		GreaterThan = 5,		
		LessThan = 6,		
		GreaterThanEqual = 7,		
		LessThanEqual = 8,		
		NotEqual = 9,
		Between = 10
	}

	public enum FunctionCallFromEnum
	{		
		Button = 1,	
		Grid = 2
	}

	public enum AcceptReject
	{
		Accept = 1,
		Reject = 2,		
        Computed = 3,
        NoOverlay = 4
	}

	public enum MaterialTypeEnum
	{		
		Ingredient = 1,	
		Product = 2
	}

	public enum AnalyzerStatusEnum
	{		
		NotSent = 1,	
		NotReceived = 2,		
		NotSentOrNotReceived = 3
	}

	public enum AnalysisGroupByForUpdateEnum
	{
		//SampleID is selected
		SampleID = 1,
		//SampleID and OwnerLocation NOT selected
		Date = 2,
		//SampleID NOT selected
		DateAndOwner = 3,
		//SampleID and Date NOT selected
		Owner = 4,
		//Nothing selected
		None = 5
	}

	//12/1/22
	public enum EnumTolerance
	{
		None = 1,
		ProductTolerance = 2,
		NutrientTolerance = 3
	}
		
	public enum SampleImportUserDefinedMapSelection
	{
		UseMapping = 1,
		SelectOnImport = 2,
		SelectNow = 3			//12/15/20 - added
	}
	
	public enum SampleImportSpecificMessageDetail
	{
		Unassigned = 0,
		MissingMaterialXref = 1,
		MissingOwnerXref = 2,
		MissingOwnerLocationXref = 3,
		MissingSupplierXref = 4,
		MissingSupplierLocationXref = 5,
		MissingMaterial = 6,
		MissingOwner = 7,
		MissingOwnerLocation = 8,
		MissingSupplier = 9,
		MissingSupplierLocation = 10,
		DateError = 11,
		SampleAlreadyExists = 12,
		SampleDoesNotExist = 13,
		CreateSample = 14,
		MergeResults = 15,
		Rejected = 16,
		MissingInternalCrossReference = 17,
		MissingRequiredFields = 18,
		InactiveMaterial = 19,
		InactiveOwner = 20,
		InactiveOwnerLocation = 21,
		InactiveSupplier = 22,
		InactiveSupplierLocation = 23,
		MissingDatasourceCrossReference = 24,           //4/4/17
		CreateAndOrMergeResults = 25,                   //2/19/18
		AddResultsAndUpdateHeaderFields = 26,           //4/1/19
		UpdateHeaderFields = 27,                        //7/29/19
		MissingSupplierLocationCombinedXref = 28,       //12/28/20
		MissingOwnerLocationCombined = 29,              //12/28/20
		InactiveSupplierLocationCombined = 30           //12/28/20
	}

	public enum SampleImport
	{
		SampleDate = 1,
		LotNumber = 2,
		SampleCode = 3,
		PoNumber = 4
	}

	public enum ImportHeaderMapStatus
	{
		Map = 0,
		Import = 1,
		Delete = 2
	}
	

	public enum SampleImportStatus
	{
		Unassigned = 0,		
		Accepted = 1,			//green checkmark		
		Cancelled = 2,          //red "x"
		Warning = 3,
        Ignore = 4
	}

	public enum SampleImportPreviewReturnValues
	{
		Integer = 1,
		Numeric = 2,
		Truncation = 3,
		Date = 4,
		Bit = 5
	}

	public enum SampleAIMRefCFCUserEnum
	{		
		SampleAIMRefCFCUserID = 1,		
		SampleAIMID_CFCUserID = 2,		
		CFCUserID = 3,
	}

	public enum OutlierTypeEnum
	{		
		NoOutlierExcluded = 1,          //No outliers are exclude from results		
		ExtremeAndMildExcluded = 2,     //Extreme and mild outliers are excluded from results		
		ExtremeExcluded = 3,			//Extreme outliers are excluded from results
	}

    public enum EnumIsAnd
    {
        Or = 0,
        And = 1
    }

	//01/18/23  APINIRType 0 = No NIR
	//          APINIRType 1 = Bruker NIR 
	//          APINIRType 2 = Foss NIR
	public enum EnumAPINIRType
	{
		None = 0,
		Bruker = 1,
		Foss = 2

	}

	//2/18/25
	public enum EnumUpdateTable
	{ 
		Plant = 1,
		Nutrient = 2,
		Ingredient = 3,
		Product = 4
	}
}