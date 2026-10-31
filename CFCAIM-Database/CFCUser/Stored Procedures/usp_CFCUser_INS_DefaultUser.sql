

CREATE PROCEDURE [CFCUser].[usp_CFCUser_INS_DefaultUser]
	@ApplicationName		VarChar(50)

AS
BEGIN
SET NOCOUNT ON;

IF NOT EXISTS(SELECT
				UserName
			FROM
				dbo.aspnet_Users
			WHERE
				LoweredUserName = 'admin')
	BEGIN
		EXEC CFCUser.usp_CFCUser_SAV
			  0
			, @ApplicationName
			, 'admin'
			, 'admin'
			, ''
			, '1/1/1990'
			, 'ADMIN'
			, 'ADMIN'
	END

EXEC dbo.aspnet_Membership_UnlockUser
	  @ApplicationName
	, 'admin'

EXEC dbo.aspnet_UsersInRoles_AddUsersToRoles
	  @ApplicationName
	, 'admin'
	, 'Administrator'
	, '1/1/1990'





END



