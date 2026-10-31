CREATE PROCEDURE [dbo].[usp_CFCPlant_DEL_UpdateStamp]
					@UpdateStamp		DATETIME
AS
BEGIN
SET NOCOUNT ON;

DELETE 
	dbo.CFCPlantRefAIMOwnerLocation
FROM
	dbo.CFCPlant CP
	INNER JOIN 
		dbo.CFCPlantRefAIMOwnerLocation CPRAM
			ON CPRAM.CFCPlantID = CP.CFCPlantID 
WHERE
	CP.UpdateStamp < @UpdateStamp

DELETE
	dbo.CFCPlant
WHERE 
	UpdateStamp < @UpdateStamp

END