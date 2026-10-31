CREATE PROCEDURE [dbo].[usp_CFCNutrient_SEL_UpdateIngredientDetailID]
		@UpdateIngredientDetailID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    CN.*
	    , U.UserName
FROM 
	dbo.CFCNutrient CN 

INNER JOIN 
	dbo.UpdateIngredientDetail D 
		ON CN.CFCNutrientID = D.CFCNutrientID
		
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON CN.CFCUserID = U.CFCUserID

WHERE 
	D.UpdateIngredientDetailID = @UpdateIngredientDetailID
END