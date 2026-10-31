CREATE PROCEDURE [Vitapro].[usp_Lot_SAV_ExportStatus]
			  @SampleID					INT
			, @ExportStatus				BIT
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		Vitapro.Lot WITH(ROWLOCK)
			SET 
				ExportStatus = @ExportStatus													
			WHERE 
				SampleID = @SampleID
	SET @RecordID = @SampleID
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END