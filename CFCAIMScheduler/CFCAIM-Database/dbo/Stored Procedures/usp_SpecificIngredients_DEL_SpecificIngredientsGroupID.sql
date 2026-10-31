CREATE PROCEDURE [dbo].[usp_SpecificIngredients_DEL_SpecificIngredientsGroupID]
				@SpecificIngredientsGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SpecificIngredients WITH(ROWLOCK)
WHERE
	SpecificIngredientsGroupID = @SpecificIngredientsGroupID

END