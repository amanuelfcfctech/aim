CREATE PROCEDURE [CFCUser].[usp_CFCUser_UPD_CFCUserID_LoggedInStatus]
	@CFCUserID				INT
	,@LoggedInStatus		BIT

AS
BEGIN
SET NOCOUNT ON;

UPDATE 
	CFCUser.CFCUser 
SET 
	LoggedInStatus = @LoggedInStatus
WHERE 
	CFCUser.CFCUSER.CFCUserID = @CFCUserID


END