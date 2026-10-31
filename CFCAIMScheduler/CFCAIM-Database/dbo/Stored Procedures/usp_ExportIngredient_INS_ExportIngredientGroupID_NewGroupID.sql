CREATE PROCEDURE  [dbo].[usp_ExportIngredient_INS_ExportIngredientGroupID_NewGroupID]
			@ExportIngredientGroupID		INT
			, @NewGroupID					INT
			, @CFCUserID					INT
	

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0


	BEGIN
		INSERT INTO
			dbo.ExportIngredient
				(ExportIngredientGroupID
				, CFCIngredientID						
				, UpdateStamp
				, IsActive
				, CFCUserID)
		SELECT 			
				@NewGroupID
				, CFCIngredientID				
				, GETDATE()
				, IsActive
				, @CFCUserID		
		FROM
			dbo.ExportIngredient

		WHERE ExportIngredientGroupID = @ExportIngredientGroupID		
	END
		

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END