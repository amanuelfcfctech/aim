CREATE PROCEDURE [dbo].[usp_SampleResultTemplate_DEL]
				@SampleResultTemplateID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleResultTemplateDetail WITH(ROWLOCK)
WHERE
	SampleResultTemplateID = @SampleResultTemplateID


DELETE
FROM 
	dbo.SampleResultTemplate WITH(ROWLOCK)
WHERE
	SampleResultTemplateID = @SampleResultTemplateID

END