CREATE PROCEDURE [dbo].[usp_SampleResult_INS]
			  @SampleID					INT
			, @AnalyzerNutrientID		INT			
			, @AnalyzerNutrientCode		NVARCHAR(100)					--10/30/18 changed from (30) to (100)
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
			, @AnalyzerNutrientName		NVARCHAR(100)	= NULL			--10/30/18	changed from (50) to (100)
			, @SQLResultID				VARCHAR(300)	= NULL			
			, @ReturnMessage	BIT										-- 6/25/19 - added because of memory issues at Smithfield			
			, @ImportType		INT						= NULL			-- 8/17/22 - added for Romer results

--EXEC usp_SampleResult_INS @SampleID=35346, @AnalyzerNutrientID=81, @AnalyzerNutrientCode='Moisture', @ResultValue=99.99, @AnalysisDate='2023-03-31', @IsActive=1, @CFCUserID=0, @DataSourceTypeID=10003, @AimDatabaseID=1, @ReturnMessage=0			


AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''
DECLARE @SampleResultID			INT = 0
DECLARE @AIMNutrientID			INT = 0
DECLARE @SaveUnits				NVARCHAR(15) = '    '
DECLARE @Insert_Sw				INT = 0

IF @AimDatabaseID = 0
    BEGIN
        SELECT TOP 1
            @AimDatabaseID = AimDatabaseID
        FROM
            dbo.AIMDatabase
        WHERE
            IsActive = 1
    END

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

-- Romer Results -------------------------------------------------------------------
IF @ImportType = 100
	Begin	
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
				AND SR.SQLResultID = @SQLResultID
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
						AND SQLResultID = @SQLResultID
		
		END
	End
	

-- Regular Sample Results ----------------------------------------------------------
Else
	Begin	
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

	End

--Insert Sample Result -------------------------------------------------------------
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


--Insert Sample AIM ----------------------------------------------------------------

SELECT TOP 1
	@AIMNutrientID = AIMNutrientID 
FROM
	dbo.AIMNutrientRefAnalyzerNutrient ANRAN WITH(NOLOCK)
WHERE 
	ANRAN.AnalyzerNutrientID =  @AnalyzerNutrientID

--IF ISNULL(@AIMNutrientID, 0) > 0 and dbo.CheckPriority(@Sampleid, @AimNutrientID, @DataSourceTypeID, @AimDatabaseID) = 1
IF ISNULL(@AIMNutrientID, 0) > 0 and dbo.CheckPriority(@Sampleid, @AimNutrientID, @DataSourceTypeID, @AimDatabaseID, @AnalyzerNutrientID) = 1

	BEGIN		
					
		DELETE
			dbo.SampleAIM 
		WHERE
				SampleID = @SampleID
			AND AIMNutrientID = @AIMNutrientID 

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
				, @AIMNutrientID 
				, @ResultValue 
				, GETDATE()
				, @CFCUserID 
				)		
	END	


--Insert SampleResultPredict -------------------------------------------------------
IF @IsResultPredict = 1
	BEGIN
		EXEC dbo.usp_SampleResultPredict_INS 
											  @SampleResultID
											, @IsInPredictionRange
											, @GlobalH
											, @Limit	
											, @IsInMaterialRange
											, @MaxMaterialRange	
											, @MinMaterialRange	
	END

-- 6/25/19 - added because of memory issues at Smithfield
IF @ReturnMessage = 1
	BEGIN

		SELECT 
			  @SampleResultID AS RecordID
			, @ErrorNumber AS ErrorNumber
			, @ErrorMessage AS ErrorMessage
	END

END
