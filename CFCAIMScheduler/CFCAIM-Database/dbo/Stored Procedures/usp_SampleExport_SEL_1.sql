
CREATE PROCEDURE [dbo].[usp_SampleExport_SEL]
AS
BEGIN

--exec usp_SampleExport_SEL

SET NOCOUNT ON;
SELECT 
	  SE.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.SampleExport SE WITH(NOLOCK)
		LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON SE.CFCUserID = U.CFCUserID

			-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
			--LEFT OUTER JOIN
			--	dbo.aspnet_Users AU WITH(NOLOCK)
			--		ON AU.UserId = U.UserID


END