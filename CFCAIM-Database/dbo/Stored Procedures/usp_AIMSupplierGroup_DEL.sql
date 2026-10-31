CREATE PROCEDURE [dbo].[usp_AIMSupplierGroup_DEL]
				@AIMSupplierGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.AIMSupplierGroup WITH(ROWLOCK)
WHERE
	AIMSupplierGroupID = @AIMSupplierGroupID

END