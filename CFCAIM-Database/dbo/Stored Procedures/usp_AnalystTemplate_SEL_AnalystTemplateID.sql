CREATE PROCEDURE [dbo].[usp_AnalystTemplate_SEL_AnalystTemplateID]
				@AnalystTemplateID				INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  AT.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalystTemplate AT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AT.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	AT.AnalysisTemplateID = @AnalystTemplateID
END
