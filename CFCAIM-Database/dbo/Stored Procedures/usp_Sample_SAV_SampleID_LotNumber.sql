CREATE PROCEDURE [dbo].[usp_Sample_SAV_SampleID_LotNumber]
			  @SampleID						INT							
			, @LotNumber					NVARCHAR(20)	
			, @CFCUserID					INT
			
AS

BEGIN

SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''


UPDATE
	dbo.Sample WITH(ROWLOCK)
		SET
			  LotNumber = ISNULL(@LotNumber, LotNumber)
			, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			, UpdateStamp = GETDATE()
	WHERE
		SampleID = @SampleID
	


SELECT 
	  @SampleID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END