CREATE PROCEDURE [dbo].[usp_Report_SEL_IsActive]
				
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    R.*
	  , U.UserName
FROM 
	dbo.Report R WITH(NOLOCK)
	LEFT OUTER JOIN
			CFCUser.CFCUser U WITH(NOLOCK)
				ON R.CFCUserID = U.CFCUserID


WHERE
	R.IsActive = 1 


ORDER BY 	
	R.ReportListSortOrder, 
	R.ReportName
END
