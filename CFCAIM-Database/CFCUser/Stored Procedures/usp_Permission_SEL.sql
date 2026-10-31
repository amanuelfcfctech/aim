CREATE PROCEDURE [CFCUser].[usp_Permission_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  P.*
FROM 
	dbo.Permission P WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON P.CFCUserID = U.CFCUserID

			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--LEFT OUTER JOIN
			--	dbo.aspnet_Users AU WITH(NOLOCK)
			--		ON AU.UserId = U.UserID

END