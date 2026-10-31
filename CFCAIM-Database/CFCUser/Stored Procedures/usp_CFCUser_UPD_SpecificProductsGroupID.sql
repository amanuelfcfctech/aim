CREATE PROCEDURE [CFCUser].[usp_CFCUser_UPD_SpecificProductsGroupID]
	@SpecificProductsGroupID		INT
	, @Value						INT

AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	CFCUser.CFCUser 
SET 
	SpecificProductsGroupID = @Value
WHERE 
	SpecificProductsGroupID = @SpecificProductsGroupID


END