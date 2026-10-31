CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_SEL_AnalysisViewTemplateName_IsActive_TemplateType]
				  @AnalysisViewTemplateName				VARCHAR(30)
				  , @TemplateType						INT
				


AS
BEGIN
SET NOCOUNT ON;

SELECT
	R.AnalysisViewTemplateName
	
FROM 
	dbo.AnalysisViewTemplate R WITH(NOLOCK)
	
WHERE
	R.AnalysisViewTemplateName = @AnalysisViewTemplateName AND
	R.TemplateType = @TemplateType
	
END