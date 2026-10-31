create PROCEDURE [dbo].[usp_PrinterType_DEL]
				@PrinterTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.PrinterType WITH(ROWLOCK)
WHERE
	PrinterTypeID = @PrinterTypeID

END
