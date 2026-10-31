CREATE PROCEDURE [dbo].[usp_Report_SEL_IsActive_IsAdvanced]
		
	@IsActive		BIT
	, @IsAdvanced	BIT


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
	R.IsActive = @IsActive and 
	R.IsAdvanced = @IsAdvanced

ORDER BY 	
	R.ReportListSortOrder, 
	R.ReportName
END