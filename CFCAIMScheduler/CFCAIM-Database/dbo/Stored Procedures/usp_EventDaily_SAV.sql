CREATE PROCEDURE [dbo].[usp_EventDaily_SAV]
			  @EventDailyID					INT
			, @EventID						INT	= NULL		
			, @DayID						INT = NULL
			, @RecurEvery					INT = NULL
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EventDailyID = 0
	BEGIN
		INSERT
			dbo.EventDaily
				(
				  EventID
				, DayID		
				, RecurEvery		
				, UpdateStamp				
				)
		VALUES
				(
				  @EventID
				, @DayID
				, @RecurEvery
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EventDaily WITH(ROWLOCK)
				SET 
					  EventID = ISNULL(@EventID, EventID)
					, DayID = ISNULL(@DayID, DayID)		
					, RecurEvery = ISNULL(@RecurEvery, RecurEvery)			
					, UpdateStamp = GETDATE()
					
				WHERE 
					EventID = @EventID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END