CREATE PROCEDURE [dbo].[usp_AIMOwnerRefAnalyzerOwner_DEL_ID]
	  @ID		    INT
	 ,@CFCUserID	INT
AS

BEGIN
SET NOCOUNT ON;

--4/17/18 ----------------------------------------
INSERT INTO AIMOwnerRefAnalyzerOwnerLog (AIMOwnerID, AnalyzerOwnerID, LogStatus, UpdateStamp, CFCUserID)
SELECT		AIMOwnerID, AnalyzerOwnerID, 'D', Getdate(), @CFCUserID 
FROM		dbo.AIMOwnerRefAnalyzerOwner
WHERE		AIMOwnerRefAnalyzerOwnerID = @ID
--------------------------------------------------

DELETE
	dbo.AIMOwnerRefAnalyzerOwner	WITH(ROWLOCK)
WHERE 
	AIMOwnerRefAnalyzerOwnerID = @ID
END