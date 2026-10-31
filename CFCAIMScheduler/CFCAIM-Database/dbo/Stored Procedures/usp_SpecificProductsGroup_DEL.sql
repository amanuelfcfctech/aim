CREATE PROCEDURE [dbo].[usp_SpecificProductsGroup_DEL]
				@SpecificProductsGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SpecificProductsGroup WITH(ROWLOCK)
WHERE
	SpecificProductsGroupID = @SpecificProductsGroupID

END