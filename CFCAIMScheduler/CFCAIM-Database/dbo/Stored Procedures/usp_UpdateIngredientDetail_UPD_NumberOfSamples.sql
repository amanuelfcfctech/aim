CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_UPD_NumberOfSamples]
			  @UpdateIngredientDetailID		INT							
			, @NumberOfSamples				INT				= NULL
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

	BEGIN
		UPDATE 
			dbo.UpdateIngredientDetail
		SET
			NumberOfSamples = ISNULL(@NumberOfSamples, NumberOfSamples)
		WHERE
			UpdateIngredientDetailID = @UpdateIngredientDetailID
			
		SET @RecordID = @UpdateIngredientDetailID
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END