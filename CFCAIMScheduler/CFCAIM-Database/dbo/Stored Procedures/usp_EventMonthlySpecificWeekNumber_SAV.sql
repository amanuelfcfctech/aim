CREATE PROCEDURE [dbo].[usp_EventMonthlySpecificWeekNumber_SAV]
			  @EventMonthlySpecificWeekNumberID		INT
			, @EventMonthlyID					INT			
			, @WeekNumberID							INT
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EventMonthlySpecificWeekNumberID = 0
	BEGIN
		INSERT
			dbo.EventMonthlySpecificWeekNumber
				(
				  EventMonthlyID
				, WeekNumberID		
				, UpdateStamp				
				)
		VALUES
				(
				  @EventMonthlyID
				, @WeekNumberID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EventMonthlySpecificWeekNumber WITH(ROWLOCK)
				SET 
					  EventMonthlyID = ISNULL(@EventMonthlyID, EventMonthlyID)
					, WeekNumberID = ISNULL(@WeekNumberID, WeekNumberID)
					, UpdateStamp = GETDATE()
					
				WHERE 
					EventMonthlySpecificWeekNumberID = @EventMonthlySpecificWeekNumberID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END