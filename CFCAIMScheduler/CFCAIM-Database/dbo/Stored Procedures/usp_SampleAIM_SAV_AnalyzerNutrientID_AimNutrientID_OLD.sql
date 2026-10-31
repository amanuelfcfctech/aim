CREATE PROCEDURE [dbo].[usp_SampleAIM_SAV_AnalyzerNutrientID_AimNutrientID_OLD]
				@AnalyzerNutrientID	INT				
			  , @AIMNutrientID		INT			  
			  , @CFCUserID			INT	= NULL	
			  , @AimDatabaseID		INT 		  

--exec usp_SampleAIM_SAV_AnalyzerNutrientID_AimNutrientID_OLD 1, 2, 2
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID			INT = 0
DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''
DECLARE @SampleID			INT = 0
DECLARE @SampleResultID		INT = 0
DECLARE @DataSourceTypeID	INT = 0
DECLARE @ResultValue		NUMERIC(18,8) = 0

DECLARE TEMPCUR SCROLL CURSOR FOR
SELECT 
	SR.SampleID, SR.SampleResultID, IsNull(S.DataSourceTypeID, 0) as DataSourceTypeID, SR.ResultValue
FROM 
	dbo.AnalyzerNutrient AN 
LEFT JOIN 
	dbo.SampleResult SR 
		ON AN.AnalyzerNutrientID = SR.AnalyzerNutrientID
LEFT JOIN 
	dbo.Sample S 
		ON SR.SampleID = S.SampleID
WHERE 	
	AN.AnalyzerNutrientID = @AnalyzerNutrientID
	
OPEN TEMPCUR
FETCH NEXT FROM TEMPCUR INTO @SampleID, @SampleResultID, @DataSourceTypeID, @ResultValue
WHILE @@FETCH_STATUS = 0
	BEGIN	
	IF ISNULL(@AIMNutrientID, 0) > 0 and dbo.CheckPriority(@Sampleid, @AimNutrientID, @DataSourceTypeID, @AimDatabaseID) = 1
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
			SET @RecordID = @@IDENTITY
			
		END	
		FETCH NEXT FROM TEMPCUR INTO @SampleID, @SampleResultID, @DataSourceTypeID, @ResultValue	

	END
CLOSE TEMPCUR
DEALLOCATE TEMPCUR

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage


END