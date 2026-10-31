CREATE PROCEDURE [Vitapro].[usp_Lot_SAV_AmountAvailable]
			  @SampleID					INT
			--, @AmountAvailable			NUMERIC(18,8)
			, @AmountAvailable			INT
			
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
				AmountAvailable = @AmountAvailable													
			WHERE 
				SampleID = @SampleID
	SET @RecordID = @SampleID
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END