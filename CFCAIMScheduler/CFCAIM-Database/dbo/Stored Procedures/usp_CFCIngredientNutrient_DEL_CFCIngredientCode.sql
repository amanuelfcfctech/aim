
create PROCEDURE [dbo].[usp_CFCIngredientNutrient_DEL_CFCIngredientCode]
	  @CFCIngredientCode				VARCHAR(8)
AS
BEGIN
SET NOCOUNT ON;
DECLARE @CFCIngredientID	INT

SELECT 
	@CFCIngredientID = CFCIngredientID					
FROM
	dbo.CFCIngredient CI WITH(NOLOCK)
WHERE
	CI.CFCIngredientCode = @CFCIngredientCode

DELETE
	dbo.CFCIngredientNutrient WITH(ROWLOCK)
WHERE 
	CFCIngredientID = @CFCIngredientID

END

