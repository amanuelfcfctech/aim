create PROCEDURE [dbo].[usp_EventMonthlySpecificWeekDays_SAV]
			  @EventMonthlySpecificWeekDaysID		INT
			, @EventMonthlyID					INT			
			, @WeekID							INT
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EventMonthlySpecificWeekDaysID = 0
	BEGIN
		INSERT
			dbo.EventMonthlySpecificWeekDays
				(
				  EventMonthlyID
				, WeekID		
				, UpdateStamp				
				)
		VALUES
				(
				  @EventMonthlyID
				, @WeekID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EventMonthlySpecificWeekDays WITH(ROWLOCK)
				SET 
					  EventMonthlyID = ISNULL(@EventMonthlyID, EventMonthlyID)
					, WeekID = ISNULL(@WeekID, WeekID)
					, UpdateStamp = GETDATE()
					
				WHERE 
					EventMonthlySpecificWeekDaysID = @EventMonthlySpecificWeekDaysID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END