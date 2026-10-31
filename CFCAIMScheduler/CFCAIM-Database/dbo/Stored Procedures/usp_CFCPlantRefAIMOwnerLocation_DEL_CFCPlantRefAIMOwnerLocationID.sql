CREATE PROCEDURE [dbo].[usp_CFCPlantRefAIMOwnerLocation_DEL_CFCPlantRefAIMOwnerLocationID]
	  @CFCPlantRefAIMOwnerLocationID		INT
	  , @CFCUserID							INT
AS
BEGIN
SET NOCOUNT ON;


--4/17/18 ----------------------------------------
INSERT INTO CFCPlantRefAIMOwnerLocationLog (CFCPlantID, AIMOwnerLocationID, LogStatus, UpdateStamp, CFCUserID)
SELECT		CFCPlantID, AIMOwnerLocationID, 'D', Getdate(), @CFCUserID 
FROM		dbo.CFCPlantRefAIMOwnerLocation
WHERE		CFCPlantRefAIMOwnerLocationID = @CFCPlantRefAIMOwnerLocationID
--------------------------------------------------

DELETE
	dbo.CFCPlantRefAIMOwnerLocation	WITH(ROWLOCK)
WHERE 
	CFCPlantRefAIMOwnerLocationID = @CFCPlantRefAIMOwnerLocationID
END
