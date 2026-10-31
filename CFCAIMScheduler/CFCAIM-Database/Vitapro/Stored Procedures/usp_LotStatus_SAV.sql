


CREATE PROCEDURE Vitapro.[usp_LotStatus_SAV]
			  @LotStatusID						INT
			, @LotStatusName					NVARCHAR(20)
			, @CFCUserID						INT
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @LotStatusID = 0 
			BEGIN
				INSERT
					Vitapro.LotStatus
						(
						  LotStatusName
						, UpdateStamp
						, CFCUserID
						
						)
				VALUES
						(			
						  @LotStatusName
						, GETDATE()
						, @CFCUserID
						
						)
				SET @RecordID = @@IDENTITY 
			END	
ELSE
	BEGIN
		UPDATE
			Vitapro.LotStatus WITH(ROWLOCK)
				SET 
					  LotStatusName = ISNULL(@LotStatusName, LotStatusName)
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					
				WHERE 
					LotStatusID = @LotStatusID
		SET @RecordID = @LotStatusID
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END