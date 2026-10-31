CREATE PROCEDURE [dbo].[usp_EventMonthlySpecificDays_SAV]
			  @EventMonthlySpecificDaysID		INT
			, @EventMonthlyID					INT			
			, @DayID							INT
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EventMonthlySpecificDaysID = 0
	BEGIN
		INSERT
			dbo.EventMonthlySpecificDays
				(
				  EventMonthlyID
				, DayID		
				, UpdateStamp				
				)
		VALUES
				(
				  @EventMonthlyID
				, @DayID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EventMonthlySpecificDays WITH(ROWLOCK)
				SET 
					  EventMonthlyID = ISNULL(@EventMonthlyID, EventMonthlyID)
					, DayID = ISNULL(@DayID, DayID)
					, UpdateStamp = GETDATE()
					
				WHERE 
					EventMonthlySpecificDaysID = @EventMonthlySpecificDaysID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END