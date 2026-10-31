CREATE PROCEDURE [dbo].[usp_SpecificIngredients_DEL]
				@SpecificIngredientsID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SpecificIngredients WITH(ROWLOCK)
WHERE
	SpecificIngredientsID = @SpecificIngredientsID

END