CREATE PROCEDURE [dbo].[usp_CFCIngredientRefAIMMaterial_DEL_ID]
	  @ID				INT
	  , @CFCUserID		INT
AS
BEGIN
SET NOCOUNT ON;


--4/17/18 ----------------------------------------
INSERT INTO CFCIngredientRefAIMMaterialLog (CFCIngredientID, AIMMaterialID, LogStatus, UpdateStamp, CFCUserID)
SELECT		CFCIngredientID, AIMMaterialID, 'D', Getdate(), @CFCUserID 
FROM		dbo.CFCIngredientRefAIMMaterial
WHERE		CFCIngredientRefAIMMaterialID = @ID
--------------------------------------------------


DELETE
	dbo.CFCIngredientRefAIMMaterial	WITH(ROWLOCK)
WHERE 
	CFCIngredientRefAIMMaterialID = @ID
END
