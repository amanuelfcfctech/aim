CREATE PROCEDURE  [dbo].[usp_SpecificIngredients_INS_SpecificIngredientsGroupID_NewGroupID]
			@SpecificIngredientsGroupID		INT
			, @NewGroupID					INT
			, @CFCUserID					INT
	
	
-- exec usp_SpecificIngredients_INS_SpecificIngredientsGroupID_NewGroupID 4, 10, 1
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0


	BEGIN
		INSERT INTO
			dbo.SpecificIngredients
				(SpecificIngredientsGroupID
				, AIMMaterialID						
				, UpdateStamp
				, IsActive
				, CFCUserID)
		SELECT 			
				@NewGroupID
				, AIMMaterialID						
				, GETDATE()
				, IsActive
				, @CFCUserID		
		FROM
			dbo.SpecificIngredients 

		WHERE SpecificIngredientsGroupID = @SpecificIngredientsGroupID		
	END
		

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END