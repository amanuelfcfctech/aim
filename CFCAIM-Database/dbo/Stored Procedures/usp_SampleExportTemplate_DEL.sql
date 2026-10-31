CREATE PROCEDURE [dbo].[usp_SampleExportTemplate_DEL]
				@SampleExportTemplateID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleExportTemplate WITH(ROWLOCK)
WHERE
	SampleExportTemplateID = @SampleExportTemplateID

END