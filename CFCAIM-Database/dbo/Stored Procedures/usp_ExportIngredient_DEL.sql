CREATE PROCEDURE [dbo].[usp_ExportIngredient_DEL]
				@ExportIngredientID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.ExportIngredient WITH(ROWLOCK)
WHERE
	ExportIngredientID = @ExportIngredientID

END