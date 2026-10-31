CREATE PROCEDURE [dbo].[usp_AIMConfig_SEL_CFCUserID]
		@CFCUserID			INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  *
	  -- 1/11/21 - changed to remove aspnet_User table
	  --, AU.UserName

	  , U.UserName
FROM 
	dbo.AIMConfig C WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON C.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE
	C.CFCUserID = @CFCUserID
END

