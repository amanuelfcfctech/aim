CREATE PROCEDURE [dbo].[usp_ExportPlant_DEL_ExportPlantGroupID]
				@ExportPlantGroupID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.ExportPlant WITH(ROWLOCK)
WHERE
	ExportPlantGroupID = @ExportPlantGroupID

END