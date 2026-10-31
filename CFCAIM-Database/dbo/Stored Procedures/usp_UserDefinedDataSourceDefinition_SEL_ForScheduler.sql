CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceDefinition_SEL_ForScheduler]
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  UD.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.UserDefinedDataSourceDefinition UD WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON UD.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	UD.AIMSourceMap = 2 and UD.AIMSourceLocationMap = 2 and UD.AIMOwnerMap = 2 and UD.AIMOwnerLocationMap = 2

ORDER BY UD.ImportFileName 

END