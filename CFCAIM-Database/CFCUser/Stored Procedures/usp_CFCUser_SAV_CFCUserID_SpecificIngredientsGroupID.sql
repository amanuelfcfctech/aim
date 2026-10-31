CREATE PROCEDURE [CFCUser].[usp_CFCUser_SAV_CFCUserID_SpecificIngredientsGroupID]
	@CFCUserID							INT
	,@SpecificIngredientsGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

UPDATE CFCUser.CFCUser with (ROWLOCK)
			SET SpecificIngredientsGroupID = @SpecificIngredientsGroupID
			WHERE CFCUserID = @CFCUserID

END