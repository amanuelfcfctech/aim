CREATE PROCEDURE [dbo].[usp_SpecificProducts_DEL]
				@SpecificProductsID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SpecificProducts WITH(ROWLOCK)
WHERE
	SpecificProductsID = @SpecificProductsID

END