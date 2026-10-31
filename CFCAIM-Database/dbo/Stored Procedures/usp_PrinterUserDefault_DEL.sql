CREATE PROCEDURE [dbo].[usp_PrinterUserDefault_DEL]
				@PrinterUserDefaultID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.PrinterUserDefault WITH(ROWLOCK)
WHERE
	PrinterUserDefaultID = @PrinterUserDefaultID

END