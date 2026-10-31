CREATE PROCEDURE [dbo].[usp_CFCIngredientRefAIMMaterial_DEL_Orphans]

AS
BEGIN
SET NOCOUNT ON;

DELETE 
FROM 
	dbo.CFCIngredientRefAIMMaterial 
WHERE 
	CFCIngredientID IN
		
		(SELECT 
			X.CFCIngredientID 
		FROM 
			dbo.CFCIngredientRefAIMMaterial X
		LEFT OUTER JOIN 
			dbo.CFCIngredient C 
				on X.CFCIngredientID = C.CFCIngredientID
		WHERE 
			C.CFCIngredientID is null)

END