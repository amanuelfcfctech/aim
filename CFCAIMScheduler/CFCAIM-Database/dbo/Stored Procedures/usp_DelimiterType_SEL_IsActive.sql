CREATE PROCEDURE [dbo].[usp_DelimiterType_SEL_IsActive]
		@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_DelimiterType_SEL_IsActive true

SELECT 
	  DT.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.DelimiterType DT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DT.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	DT.IsActive = @IsActive

END