CREATE PROCEDURE [dbo].[usp_ExportIngredient_DEL_ExportIngredientGroupID]
				@ExportIngredientGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.ExportIngredient WITH(ROWLOCK)
WHERE
	ExportIngredientGroupID = @ExportIngredientGroupID

END