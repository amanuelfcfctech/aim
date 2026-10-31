CREATE PROCEDURE [dbo].[usp_AIMSupplierGroupDetail_DEL]
				@AIMSupplierGroupDetailID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.AIMSupplierGroupDetail WITH(ROWLOCK)
WHERE
	AIMSupplierGroupDetailID = @AIMSupplierGroupDetailID

END