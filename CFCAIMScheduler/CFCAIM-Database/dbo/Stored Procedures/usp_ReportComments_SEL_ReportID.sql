CREATE PROCEDURE [dbo].[usp_ReportComments_SEL_ReportID]
		@ReportID					INT
		
-- exec usp_ReportComments_SEL_ReportID 37
		
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	RC.ReportCommentsID
	, RC.ReportID
	, RC.ReportComment1
	, RC.ReportComment2
	, RC.ReportComment3
	, RC.ReportComment4
	, RC.ReportComment5


FROM dbo.ReportComments RC

WHERE 
	RC.ReportID = @ReportID


END