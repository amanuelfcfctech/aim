CREATE PROCEDURE [dbo].[usp_SampleQualityEquation_SAV]
			  @SampleQualityEquationID			INT
			, @SampleQualityEquationName		VARCHAR(50) = NULL						
			, @AnalyzerNutrientID				INT			= NULL
			, @DataSourceTypeID					INT			= NULL
			, @Equation							VARCHAR(200) = NULL			
			, @IsActive							BIT			= NULL												
			, @CFCUserID						INT			= NULL			
			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleQualityEquationID = 0
	BEGIN
		INSERT
			dbo.SampleQualityEquation
				(
				  SampleQualityEquationName	
				, AnalyzerNutrientID
				, DataSourceTypeID
				, Equation					
				, IsActive				
				, CFCUserID
				, UpdateStamp				
							
				)
		VALUES
				(
				  @SampleQualityEquationName				
				, @AnalyzerNutrientID
				, @DataSourceTypeID
				, @Equation				
				, @IsActive				
				, @CFCUserID
				, GETDATE()				
				
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.SampleQualityEquation WITH(ROWLOCK)
				SET 
					  SampleQualityEquationName = ISNULL(@SampleQualityEquationName, SampleQualityEquationName)					
					, AnalyzerNutrientID = ISNULL(@AnalyzerNutrientID, AnalyzerNutrientID)			
					, DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)								
					, Equation = ISNULL(@Equation, Equation)								
					, IsActive = ISNULL(@IsActive, IsActive)											
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()					
					
					
				WHERE 
					SampleQualityEquationID = @SampleQualityEquationID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END