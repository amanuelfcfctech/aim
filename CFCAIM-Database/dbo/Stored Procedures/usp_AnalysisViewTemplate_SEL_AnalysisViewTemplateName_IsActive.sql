
CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateName_IsActive]
				  @AnalysisViewTemplateName				VARCHAR(30)
				


AS
BEGIN
SET NOCOUNT ON;

SELECT
	R.AnalysisViewTemplateName
	
FROM 
	dbo.AnalysisViewTemplate R WITH(NOLOCK)
	
WHERE
	R.AnalysisViewTemplateName = @AnalysisViewTemplateName 
	
END