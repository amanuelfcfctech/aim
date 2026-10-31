CREATE PROCEDURE [dbo].[usp_Report_SEL_IsActive_IsAnalysis]
				
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    R.*

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.Report R WITH(NOLOCK)
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON R.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE
	R.IsActive = 1 and R.IsAnalysis = 1 and R.TemplateType = 0

ORDER BY R.ReportName 

END
