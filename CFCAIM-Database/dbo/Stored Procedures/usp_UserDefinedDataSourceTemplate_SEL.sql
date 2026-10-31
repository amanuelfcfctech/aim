CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceTemplate_SEL]
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  UT.*
	
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.UserDefinedDataSourceTemplate UT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON UT.CFCUserID = U.CFCUserID

	
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

END