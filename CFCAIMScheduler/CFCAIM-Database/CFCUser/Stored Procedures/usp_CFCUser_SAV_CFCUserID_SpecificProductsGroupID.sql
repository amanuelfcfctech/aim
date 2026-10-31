CREATE PROCEDURE [CFCUser].[usp_CFCUser_SAV_CFCUserID_SpecificProductsGroupID]
	@CFCUserID							INT
	,@SpecificProductsGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

UPDATE CFCUser.CFCUser with (ROWLOCK)
			SET SpecificProductsGroupID = @SpecificProductsGroupID
			WHERE CFCUserID = @CFCUserID

END