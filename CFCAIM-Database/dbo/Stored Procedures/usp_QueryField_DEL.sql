create PROCEDURE [dbo].[usp_QueryField_DEL]
				@QueryFieldID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.QueryField WITH(ROWLOCK)
WHERE
	QueryFieldID = @QueryFieldID

END
