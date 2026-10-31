CREATE PROCEDURE [dbo].[usp_AIMSupplierGroupDetail_DEL_AIMSupplierID]
				@AIMSupplierID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.AIMSupplierGroupDetail WITH(ROWLOCK)
WHERE
	AIMSupplierID = @AIMSupplierID

END