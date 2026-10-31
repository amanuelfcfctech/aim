CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_DEL_QueryTemplateID_QueryFieldID]
				@QueryTemplateID		INT
			  , @QueryFieldID			INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.QueryTemplateDetail WITH(ROWLOCK)
WHERE
	QueryTemplateID = @QueryTemplateID
	and QueryFieldID = @QueryFieldID
END