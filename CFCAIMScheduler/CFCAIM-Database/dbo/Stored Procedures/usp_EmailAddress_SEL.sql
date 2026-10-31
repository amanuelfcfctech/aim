CREATE PROCEDURE [dbo].[usp_EmailAddress_SEL]

AS
BEGIN
SET NOCOUNT ON;

SELECT
	    EA.*

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)	
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.EmailAddress EA WITH(NOLOCK)
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON EA.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID

END

