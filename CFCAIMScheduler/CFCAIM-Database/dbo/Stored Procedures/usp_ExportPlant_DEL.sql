CREATE PROCEDURE [dbo].[usp_ExportPlant_DEL]
				@ExportPlantID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.ExportPlant WITH(ROWLOCK)
WHERE
	ExportPlantID = @ExportPlantID

END