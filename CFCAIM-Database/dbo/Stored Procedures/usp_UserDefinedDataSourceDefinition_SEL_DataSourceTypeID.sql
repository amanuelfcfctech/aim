
CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceDefinition_SEL_DataSourceTypeID]
 @DataSourceTypeID INT			


AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  DU.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.UserDefinedDataSourceDefinition DU WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DU.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	DataSourceTypeID = @DataSourceTypeID

ORDER BY DU.ImportFileName

END