CREATE PROCEDURE [dbo].[usp_Report_SEL_IsActive_HasComments]
		
	@IsActive		BIT
	, @HasComments	BIT

--exec usp_Report_SEL_IsActive_HasComments 1, 1

AS
BEGIN
SET NOCOUNT ON;

SELECT
	    R.*
	  , U.UserName	
	  , RC.ReportCommentsID
FROM 
	dbo.Report R WITH(NOLOCK)
LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON R.CFCUserID = U.CFCUserID
LEFT OUTER JOIN
	dbo.ReportComments RC 
		ON R.ReportID = RC.ReportID

WHERE
	R.IsActive = @IsActive and 
	R.HasComments = @HasComments

ORDER BY 	
	R.ReportListSortOrder, 
	R.ReportName
END