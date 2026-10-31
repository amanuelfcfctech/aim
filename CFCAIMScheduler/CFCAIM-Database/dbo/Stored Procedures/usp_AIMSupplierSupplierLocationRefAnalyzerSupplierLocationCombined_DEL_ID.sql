CREATE PROCEDURE [dbo].[usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_DEL_ID]
	    @ID					INT
	  , @CFCUserID			INT
AS
BEGIN
SET NOCOUNT ON;


DECLARE @ProcName nvarchar(128);
SET @ProcName = OBJECT_NAME(@@PROCID);  

INSERT INTO AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedLog (AIMSupplierID, AIMSupplierLocationID, AnalyzerSupplierLocationCombinedID, LogStatus, UpdateStamp, CFCUserID, Comment)
SELECT		AIMSupplierID, AIMSupplierLocationID, AnalyzerSupplierLocationCombinedID, 'D', Getdate(), @CFCUserID, @ProcName 
FROM		dbo.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined
WHERE		AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID = @ID
--------------------------------------------------


DELETE
	dbo.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined	WITH(ROWLOCK)
WHERE 
	AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID = @ID
END