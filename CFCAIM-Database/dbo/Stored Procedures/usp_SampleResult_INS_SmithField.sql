CREATE PROCEDURE [dbo].[usp_SampleResult_INS_SmithField]
			  @SampleID					INT
			, @AnalyzerNutrientID		INT	

			--10/30/18
			--, @AnalyzerNutrientCode		NVARCHAR(30)
			, @AnalyzerNutrientCode		NVARCHAR(100)

			, @ResultValue				NUMERIC(18,8)	
			, @AnalysisDate				DATETIME
			, @IsActive					BIT	
			, @CFCUserID				INT						
			, @Unit						NVARCHAR		= NULL
			, @IsResultPredict			BIT				= NULL
			, @IsInPredictionRange		BIT				= NULL
			, @GlobalH					NUMERIC(18,8)	= NULL
			, @Limit					NUMERIC(18,8)	= NULL
			, @IsInMaterialRange		BIT				= NULL
			, @MaxMaterialRange			NUMERIC(18,8)	= NULL
			, @MinMaterialRange			NUMERIC(18,8)	= NULL	
			, @UnitType					NVARCHAR(15)	= NULL			
			, @DataSourceTypeID			INT				= NULL
			, @AimDatabaseID			INT				= NULL
			, @InstrumentID				INT				= NULL
			, @AnalyzerNutrientName		NVARCHAR(50)	= NULL
			, @SQLResultID				VARCHAR(100)	= NULL

--exec usp_SampleResult_INS 1, 0, 'Protein', 39.09, '2015-11-09', 1, 2, 'g/kg', null, null, null, null, null, null, null, 'g/kg', 6, 1, 0, null
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''
DECLARE @SampleResultID			INT = 0
DECLARE @AIMNutrientID			INT = 0
DECLARE @SaveUnits				NVARCHAR(15) = '    '
DECLARE @Insert_Sw				INT = 0


--Insert AnalyzerNutrient ------------------------------------------
IF @AnalyzerNutrientID = 0 
	BEGIN	
		SELECT TOP 1
			@AnalyzerNutrientID = N.AnalyzerNutrientID
		FROM
			dbo.AnalyzerNutrient N WITH(NOLOCK)
		WHERE
			N.AnalyzerNutrientCode = @AnalyzerNutrientCode	and
			N.DataSourceTypeID = @DataSourceTypeID							

		IF @AnalyzerNutrientID = 0 
			BEGIN			

				INSERT INTO 
					dbo.AnalyzerNutrient 
						(
						  AnalyzerNutrientCode 
						, AnalyzerNutrientName
						, CFCUserID				
						, DataSourceTypeID	
						, UnitType	
						)
				VALUES
						(
						  @AnalyzerNutrientCode
						, ISNULL(@AnalyzerNutrientName, @AnalyzerNutrientCode) 
						, @CFCUserID						
						, @DataSourceTypeID
						, @Unit
						)				
				SET	@AnalyzerNutrientID = @@IDENTITY
			END
	END


--Update Sample Result ---------------------------------------------
IF EXISTS
		(
		SELECT
			SR.SampleID
		FROM 
			dbo.SampleResult SR
		WHERE
				SR.SampleID = @SampleID
			AND SR.AnalyzerNutrientID = @AnalyzerNutrientID
			AND SR.IsActive = 1
		)
	BEGIN
		UPDATE
			dbo.SampleResult
				SET
					  IsActive = 0
					, CFCUserID = @CFCUserID
					, UpdateStamp = GETDATE()
				WHERE
						SampleID = @SampleID
					AND AnalyzerNutrientID = @AnalyzerNutrientID
					AND IsActive = 1
		
	END			

--Insert Sample Result ---------------------------------------------
BEGIN
IF Len(IsNull(@Unit,'')) > 0
	SET @SaveUnits = IsNull(@Unit,'')		-- 'Unit' gets populated in the import
ELSE
	SET @SaveUnits = IsNull(@UnitType, '')	-- 'UnitType' gets populated when manually entering result
END						
INSERT
	dbo.SampleResult WITH(ROWLOCK)
		(
		  SampleID 
		, AnalyzerNutrientID 
		, AnalyzerNutrientCode
		, ResultValue 
		, AnalysisDate
		, UpdateStamp 
		, IsActive 
		, CFCUserID		
		, Unit	
		, InstrumentID
		, SQLResultID
		
		)
VALUES
		(
		  @SampleID
		, @AnalyzerNutrientID
		, @AnalyzerNutrientCode
		, @ResultValue
		, @AnalysisDate
		, GETDATE()
		, 1
		, @CFCUserID		
		, @SaveUnits
		, @InstrumentID
		, @SQLResultID
		)
SET @SampleResultID	= @@IDENTITY


--Insert Sample AIM ------------------------------------------------
	BEGIN					

		INSERT INTO	
			dbo.SampleAIM 
				(
				  SampleID
				, SampleResultID
				, AIMNutrientID 
				, AIMUsedValue 
				, UpdateStamp 
				, CFCUserID 
				)
		VALUES
				(
				  @SampleID
				, @SampleResultID
				, @AnalyzerNutrientID				 
				, @ResultValue 
				, GETDATE()
				, @CFCUserID 
				)		
	END	

SELECT 
	  @SampleResultID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END