
CREATE PROCEDURE [CFCUser].[usp_Roles_INS_DefaultRole]
	@ApplicationName		VarChar(50)

AS
BEGIN
SET NOCOUNT ON;


IF NOT EXISTS(SELECT 
				RoleName
			FROM
				dbo.aspnet_Roles
			WHERE
				RoleName = 'Administrator')

	BEGIN
		EXEC dbo.aspnet_Roles_CreateRole
			  @ApplicationName
			, 'Administrator'
	END


IF NOT EXISTS(SELECT 
				RoleName
			FROM
				dbo.aspnet_Roles
			WHERE
				RoleName = 'User')

	BEGIN
		EXEC dbo.aspnet_Roles_CreateRole
			  @ApplicationName
			, 'User'
	END

END


