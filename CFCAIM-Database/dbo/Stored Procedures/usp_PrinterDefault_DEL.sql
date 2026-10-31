CREATE PROCEDURE [dbo].[usp_PrinterDefault_DEL]
				@PrinterDefaultID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.PrinterDefault WITH(ROWLOCK)
WHERE
	PrinterDefaultID = @PrinterDefaultID

END