CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceUser_SEL_IsActive]
		@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_UserDefinedDataSourceUser_SEL_IsActive true


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
	DST.IsUserDefined = 1 and 
	DST.IsActive = @IsActive

ORDER BY Name 
END