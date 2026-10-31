CREATE PROCEDURE [dbo].[usp_EventMonthly_SAV]
			  @EventMonthlyID					INT
			, @EventID							INT			
			, @MonthID							INT
			, @SpecificDays						BIT
			, @SpecificWeek						BIT
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EventMonthlyID = 0
	BEGIN
		INSERT
			dbo.EventMonthly
				(
				  EventID
				, MonthID		
				, SpecificDays	
				, SpecificWeek	
				, UpdateStamp				
				)
		VALUES
				(
				  @EventID
				, @MonthID
				, @SpecificDays
				, @SpecificWeek
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EventMonthly WITH(ROWLOCK)
				SET 
					  EventID = ISNULL(@EventID, EventID)
					, MonthID = ISNULL(@MonthID, MonthID)
					, SpecificDays = ISNULL(@SpecificDays, SpecificDays)				
					, SpecificWeek = ISNULL(@SpecificWeek, SpecificWeek)
					, UpdateStamp = GETDATE()
					
				WHERE 
					EventMonthlyID = @EventMonthlyID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END