CREATE PROCEDURE [dbo].[usp_ExportIngredientGroup_UPD_AIMMaterialID]			  
			 @ExportIngredientGroupID				INT			
			, @AIMMaterialID						INT			= NULL			
			, @CFCUserID							INT			= NULL			
			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		dbo.ExportIngredientGroup  WITH(ROWLOCK)
			SET 
				 AIMMaterialID = ISNULL(@AIMMaterialID, IsActive)														
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
				, UpdateStamp = GETDATE()								
					
			WHERE 
				ExportIngredientGroupID = @ExportIngredientGroupID

		SET @RecordID = @@IDENTITY 
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END