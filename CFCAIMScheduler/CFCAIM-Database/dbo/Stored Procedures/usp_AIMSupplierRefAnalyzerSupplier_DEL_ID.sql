CREATE PROCEDURE [dbo].[usp_AIMSupplierRefAnalyzerSupplier_DEL_ID]
	  @ID				INT
	  , @CFCUserID		INT

AS
BEGIN
SET NOCOUNT ON;


--4/17/18 ----------------------------------------
INSERT INTO AIMSupplierRefAnalyzerSupplierLog (AIMSupplierID, AnalyzerSupplierID, LogStatus, UpdateStamp, CFCUserID)
SELECT		AIMSupplierID, AnalyzerSupplierID, 'D', Getdate(), @CFCUserID 
FROM		dbo.AIMSupplierRefAnalyzerSupplier
WHERE		AIMSupplierRefAnalyzerSupplierID = @ID
--------------------------------------------------


DELETE
	dbo.AIMSupplierRefAnalyzerSupplier	WITH(ROWLOCK)
WHERE 
	AIMSupplierRefAnalyzerSupplierID = @ID
END