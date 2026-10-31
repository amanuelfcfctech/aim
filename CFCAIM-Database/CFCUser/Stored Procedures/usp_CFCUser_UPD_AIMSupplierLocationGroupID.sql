CREATE PROCEDURE [CFCUser].[usp_CFCUser_UPD_AIMSupplierLocationGroupID]
	@AIMSupplierLocationGroupID		INT
	, @Value						INT

AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	CFCUser.CFCUser 
SET 
	AIMSupplierLocationGroupID = @Value
WHERE 
	AIMSupplierLocationGroupID = @AIMSupplierLocationGroupID


END