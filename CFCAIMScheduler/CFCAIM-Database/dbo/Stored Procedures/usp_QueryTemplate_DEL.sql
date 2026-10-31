CREATE PROCEDURE [dbo].[usp_QueryTemplate_DEL]
				@QueryTemplateID		INT
AS
BEGIN
SET NOCOUNT ON;


DELETE
FROM 
	dbo.QueryTemplateDetail WITH(ROWLOCK)
WHERE
	QueryTemplateID = @QueryTemplateID

DELETE
FROM 
	dbo.QueryTemplate WITH(ROWLOCK)
WHERE
	QueryTemplateID = @QueryTemplateID

END
