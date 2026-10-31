CREATE PROCEDURE [dbo].[usp_AnalysisViewTemplate_DEL]
	  @AnalysisViewTemplateID		INT
	
AS
BEGIN
	SET NOCOUNT ON;			

	DELETE 
	
	FROM 
		dbo.AnalysisViewTemplate with (rowlock) 
	
	WHERE 
		AnalysisViewTemplateID = @AnalysisViewTemplateID

END