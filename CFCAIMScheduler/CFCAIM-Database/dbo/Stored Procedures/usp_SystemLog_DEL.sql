CREATE PROCEDURE [dbo].[usp_SystemLog_DEL]
				
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SystemLog WITH(ROWLOCK)
WHERE
	UpdateStamp <= GetDate() - 60

END
