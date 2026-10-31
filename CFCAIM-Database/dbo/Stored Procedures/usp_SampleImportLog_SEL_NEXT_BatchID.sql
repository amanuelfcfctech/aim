CREATE PROCEDURE [dbo].[usp_SampleImportLog_SEL_NEXT_BatchID]
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

SET @RecordID = (

	SELECT TOP 1 Isnull(BatchID,0)

	FROM 
		dbo.SampleImportLog L WITH(NOLOCK)

	ORDER BY
		UpdateStamp DESC )

IF @RecordID IS NULL	
	SET @RecordID = 0

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END