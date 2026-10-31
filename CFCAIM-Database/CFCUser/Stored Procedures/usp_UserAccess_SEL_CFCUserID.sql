
CREATE PROCEDURE [CFCUser].[usp_UserAccess_SEL_CFCUserID]
@CFCUserID	INT
	

-- exec cfcUser.usp_UserAccess_SEL_CFCUserID 3
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	 UA.*, P.Description
FROM 
	dbo.UserAccess UA WITH (NOLOCK)
LEFT OUTER JOIN
	dbo.Permission P with (nolock)
		ON UA.PermissionID = P.PermissionID

WHERE 
	UA.CFCUserID = @CFCUserID

ORDER BY P.Description DESC
	
END