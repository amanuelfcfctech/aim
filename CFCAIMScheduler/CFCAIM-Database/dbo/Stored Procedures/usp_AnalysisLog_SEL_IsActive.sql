CREATE PROCEDURE [dbo].[usp_AnalysisLog_SEL_IsActive]
		@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT
	    S.*

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.AnalysisLog S WITH(NOLOCK)
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON S.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--		dbo.aspnet_Users AU WITH(NOLOCK)
	--			ON AU.UserId = U.UserID
WHERE 
	S.IsActive = @IsActive
END
