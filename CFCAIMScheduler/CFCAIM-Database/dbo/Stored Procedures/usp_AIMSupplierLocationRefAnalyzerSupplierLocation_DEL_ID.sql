CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationRefAnalyzerSupplierLocation_DEL_ID]
	    @ID					INT
	  , @CFCUserID			INT
AS
BEGIN
SET NOCOUNT ON;


DECLARE @ProcName nvarchar(128);
SET @ProcName = OBJECT_NAME(@@PROCID);  

--4/17/18 ----------------------------------------
INSERT INTO AIMSupplierLocationRefAnalyzerSupplierLocationLog (AIMSupplierID, AIMSupplierLocationID, AnalyzerSupplierID, AnalyzerSupplierLocationID, LogStatus, UpdateStamp, CFCUserID, Comment)
SELECT		AIMSupplierID, AIMSupplierLocationID, AnalyzerSupplierID, AnalyzerSupplierLocationID, 'D', Getdate(), @CFCUserID, @ProcName 
FROM		dbo.AIMSupplierLocationRefAnalyzerSupplierLocation
WHERE		AIMSupplierLocationRefAnalyzerSupplierLocationID = @ID
--------------------------------------------------


DELETE
	dbo.AIMSupplierLocationRefAnalyzerSupplierLocation	WITH(ROWLOCK)
WHERE 
	AIMSupplierLocationRefAnalyzerSupplierLocationID = @ID
END