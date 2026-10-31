CREATE PROCEDURE [Vitapro].[usp_Lot_SAV_Concept5ExportStatus]
			  @SampleID					INT
			, @Concept5ExportStatus		BIT
			
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
				Concept5ExportStatus = @Concept5ExportStatus													
			WHERE 
				SampleID = @SampleID
	SET @RecordID = @SampleID
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END