CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_DEL_ID]
	    @ID		    INT
	  , @CFCUserID	INT
AS
BEGIN
SET NOCOUNT ON;

--4/17/18 ----------------------------------------
INSERT INTO AIMMaterialRefAnalyzerMaterialLog (AIMMaterialID, AnalyzerMaterialID, LogStatus, UpdateStamp, CFCUserID)
SELECT		AIMMaterialID, AnalyzerMaterialID, 'D', Getdate(), @CFCUserID 
FROM		dbo.AIMMaterialRefAnalyzerMaterial
WHERE		AIMMaterialRefAnalyzerMaterialID = @ID
--------------------------------------------------

DELETE
	dbo.AIMMaterialRefAnalyzerMaterial	WITH(ROWLOCK)
WHERE 
	AIMMaterialRefAnalyzerMaterialID = @ID




END
