CREATE PROCEDURE [dbo].[usp_ReportQuery_SEL_IsActive]
				@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    R.*

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.ReportQuery R WITH(NOLOCK)
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON R.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE
	R.IsActive = @IsActive


END
