CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceType_SEL_IsActive]

@IsActive INT
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  DST.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.DataSourceType DST WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DST.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	DST.IsUserDefined = 1 and DST.IsActive = @IsActive
END