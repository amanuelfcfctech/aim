create PROCEDURE [dbo].[usp_SampleExport_DEL]
				@SampleExportID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleExport WITH(ROWLOCK)
WHERE
	SampleExportID = @SampleExportID

END