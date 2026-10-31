create PROCEDURE [dbo].[usp_QueryOperation_DEL]
				@QueryOperationID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.QueryOperation WITH(ROWLOCK)
WHERE
	QueryOperationID = @QueryOperationID

END
