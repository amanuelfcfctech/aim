CREATE PROCEDURE [dbo].[usp_UserAccess_SEL_CFCUserID_PermissionID]
			  @CFCUserID		INT
			, @PermissionID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	UA.*
	, U.UserName

FROM 
	dbo.UserAccess UA WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON UA.CFCUserID = U.CFCUserID
WHERE 
		UA.CFCUserID = @CFCUserID
	AND UA.PermissionID = @PermissionID
END