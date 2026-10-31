CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationRefAnalyzerOwnerLocation_DEL_ID]
		 @ID			INT
		,@CFCUserID		INT
AS
BEGIN
SET NOCOUNT ON;

--4/17/18 ----------------------------------------
INSERT INTO AIMOwnerLocationRefAnalyzerOwnerLocationLog (AIMOwnerID, AIMOwnerLocationID, AnalyzerOwnerID, AnalyzerOwnerLocationID, LogStatus, UpdateStamp, CFCUserID)
SELECT		AIMOwnerID, AIMOwnerLocationID, AnalyzerOwnerID, AnalyzerOwnerLocationID, 'D', Getdate(), @CFCUserID 
FROM		dbo.AIMOwnerLocationRefAnalyzerOwnerLocation
WHERE		AIMOwnerLocationRefAnalyzerOwnerLocationID = @ID
--------------------------------------------------


DELETE
	dbo.AIMOwnerLocationRefAnalyzerOwnerLocation	WITH(ROWLOCK)
WHERE 
	AIMOwnerLocationRefAnalyzerOwnerLocationID = @ID
END



SELECT TOP 1 * FROM AIMOwnerLocationRefAnalyzerOwnerLocation