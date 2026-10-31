create PROCEDURE [CFCUser].[usp_CFCUser_UPD_AIMSupplierGroupID]
	@AIMSupplierGroupID		INT
	, @Value						INT

AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	CFCUser.CFCUser 
SET 
	AIMSupplierGroupID = @Value
WHERE 
	AIMSupplierGroupID = @AIMSupplierGroupID


END