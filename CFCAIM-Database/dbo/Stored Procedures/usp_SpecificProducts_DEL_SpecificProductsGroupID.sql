CREATE PROCEDURE [dbo].[usp_SpecificProducts_DEL_SpecificProductsGroupID]
				@SpecificProductsGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SpecificProducts WITH(ROWLOCK)
WHERE
	SpecificProductsGroupID = @SpecificProductsGroupID

END