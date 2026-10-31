
CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_DEL]
				@QueryTemplateDetailID		INT
AS
BEGIN
SET NOCOUNT ON;


DELETE
FROM 
	dbo.QueryTemplateDetailNutrient 
WHERE
	QueryTemplateDetailID = @QueryTemplateDetailID


DELETE
FROM 
	dbo.QueryTemplateDetail 
WHERE
	QueryTemplateDetailID = @QueryTemplateDetailID

END
