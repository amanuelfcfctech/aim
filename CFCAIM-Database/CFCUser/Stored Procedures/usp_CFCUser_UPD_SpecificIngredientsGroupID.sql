CREATE PROCEDURE [CFCUser].[usp_CFCUser_UPD_SpecificIngredientsGroupID]
	@SpecificIngredientsGroupID		INT
	, @Value						INT

AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	CFCUser.CFCUser 
SET 
	SpecificIngredientsGroupID = @Value
WHERE 
	SpecificIngredientsGroupID = @SpecificIngredientsGroupID


END