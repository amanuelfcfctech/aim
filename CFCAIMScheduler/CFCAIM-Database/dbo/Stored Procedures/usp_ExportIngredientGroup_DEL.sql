CREATE PROCEDURE [dbo].[usp_ExportIngredientGroup_DEL]
				@ExportIngredientGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.ExportIngredientGroup WITH(ROWLOCK)
WHERE
	ExportIngredientGroupID = @ExportIngredientGroupID

END