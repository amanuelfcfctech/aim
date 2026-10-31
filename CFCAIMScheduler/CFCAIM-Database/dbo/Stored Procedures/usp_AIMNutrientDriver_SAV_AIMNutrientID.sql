CREATE PROCEDURE [dbo].[usp_AIMNutrientDriver_SAV_AIMNutrientID]
			  @AIMNutrientDriverID			    INT
			, @QueryTemplateID					INT
			, @DataSourceTypeID					INT
			, @AIMNutrientID					INT
			, @AIMNutrientCode					VARCHAR(10) = NULL
			, @AIMNutrientName					VARCHAR(30) = NULL
						
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	IF EXISTS(SELECT AIMNutrientID FROM dbo.AIMNutrientDriver WHERE AIMNutrientID = @AIMNutrientID)	
		SET @ErrorNumber = 1					
		
	ELSE
		BEGIN
			INSERT
				dbo.AIMNutrientDriver 
					(
					  QueryTemplateID
					, DataSourceTypeID
					, AIMNutrientID
					, AIMNutrientCode
					, AIMNutrientName						
					)
			VALUES
					(
					  @QueryTemplateID
					, @DataSourceTypeID
					, @AIMNutrientID
					, @AIMNutrientCode
					, @AIMNutrientName						
					)
			SET @RecordID = @@IDENTITY
		END
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END