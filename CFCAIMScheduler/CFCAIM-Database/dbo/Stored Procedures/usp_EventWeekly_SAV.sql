CREATE PROCEDURE [dbo].[usp_EventWeekly_SAV]
			  @EventWeeklyID					INT
			, @EventID							INT = NULL			
			, @WeekID							INT = NULL
			, @RecurEvery						INT = NULL
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EventWeeklyID = 0
	BEGIN
		INSERT
			dbo.EventWeekly
				(
				  EventID
				, WeekID
				, RecurEvery				
				, UpdateStamp				
				)
		VALUES
				(
				  @EventID
				, @WeekID
				, @RecurEvery
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EventWeekly WITH(ROWLOCK)
				SET 
					  EventID = ISNULL(@EventID, EventID)
					, WeekID = ISNULL(@WeekID, WeekID)		
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