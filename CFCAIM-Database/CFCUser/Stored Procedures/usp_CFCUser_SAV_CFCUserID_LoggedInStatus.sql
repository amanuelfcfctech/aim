CREATE PROCEDURE [CFCUser].[usp_CFCUser_SAV_CFCUserID_LoggedInStatus]
	@CFCUserID				INT
	,@LoggedInStatus		BIT
AS
BEGIN
SET NOCOUNT ON;

UPDATE CFCUser.CFCUser with (ROWLOCK)
			SET LoggedInStatus = @LoggedInStatus
			WHERE CFCUserID = @CFCUserID

END