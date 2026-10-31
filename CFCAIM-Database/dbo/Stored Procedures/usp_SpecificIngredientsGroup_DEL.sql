CREATE PROCEDURE [dbo].[usp_SpecificIngredientsGroup_DEL]
				@SpecificIngredientsGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SpecificIngredientsGroup WITH(ROWLOCK)
WHERE
	SpecificIngredientsGroupID = @SpecificIngredientsGroupID

END