CREATE PROCEDURE [dbo].[usp_SampleResult_UPD_ResultValue]
			  @SampleResultID		INT
			, @ResultValue			NUMERIC(18,8)		
			, @AimDatabaseID		INT 
			, @CFCUserID			INT

-- exec usp_SampleResult_UPD_ResultValue 25500, 8.88, 1016, 2			
		
AS
BEGIN
SET NOCOUNT ON;
DECLARE @AIMNutrientID	INT = 0
DECLARE @SampleID		INT = 0
DECLARE @DataSourceTypeID	INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''

--2/7/24
DECLARE @AnalyzerNutrientID INT = 0				--2/7/24


IF EXISTS
		(
		SELECT
			SampleResultID
		FROM 
			dbo.SampleResult SR
		WHERE
			SR.SampleResultID = @SampleResultID
		)
	BEGIN
		UPDATE
			dbo.SampleResult
				SET
						IsActive = 0
					, CFCUserID = @CFCUserID
					, UpdateStamp = GETDATE()
				WHERE
						SampleResultID = @SampleResultID
						
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
				)
		SELECT 
			  SR.SampleID 
			, SR.AnalyzerNutrientID 
			, SR.AnalyzerNutrientCode 
			, @ResultValue
			, SR.AnalysisDate 
			, GETDATE()
			, 1
			, @CFCUserID
					
		FROM 
			dbo.SampleResult SR WITH(NOLOCK)
		WHERE 
			SR.SampleResultID = @SampleResultID

		SET @SampleResultID = @@IDENTITY

		SELECT TOP 1
			@AIMNutrientID = ANRAN.AIMNutrientID 
		FROM
			dbo.AIMNutrientRefAnalyzerNutrient ANRAN WITH(NOLOCK)
			INNER JOIN 
				dbo.SampleResult SR
					ON SR.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID 
		WHERE 
			SR.SampleResultID = @SampleResultID

		SET @SampleID = (SELECT SampleID FROM dbo.SampleResult with (nolock) WHERE SampleResultID = @SampleResultID)
		SET @DataSourceTypeID = (SELECT DataSourceTypeID FROM dbo.Sample with (nolock) WHERE SampleID = @SampleID)

		SET @AnalyzerNutrientID = (SELECT AnalyzerNutrientID from dbo.SampleResult WHERE SampleResultID = @SampleResultID)								--2/7/24
		
		--IF ISNULL(@AIMNutrientID, 0) > 0 and dbo.CheckPriority(@Sampleid, @AimNutrientID, @DataSourceTypeID, @AimDatabaseID) = 1						--2/7/24
		IF ISNULL(@AIMNutrientID, 0) > 0 and dbo.CheckPriority(@Sampleid, @AimNutrientID, @DataSourceTypeID, @AimDatabaseID, @AnalyzerNutrientID) = 1	--2/7/24

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
	END
	
SELECT 
	  @SampleResultID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
