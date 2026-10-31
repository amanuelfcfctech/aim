CREATE PROCEDURE [dbo].[usp_ExportPlantGroup_DEL]
				@ExportPlantGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.ExportPlantGroup WITH(ROWLOCK)
WHERE
	ExportPlantGroupID = @ExportPlantGroupID

END