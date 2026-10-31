CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_SEL_DataSourceDefinitionID]
	@UserDefinedDataSourceDefinitionID  INT		

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  DU.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.UserDefinedDataSourceTemplate DU WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DU.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	UserDefinedDataSourceDefinitionID = @UserDefinedDataSourceDefinitionID

ORDER BY DU.SourceColumnNumber

END