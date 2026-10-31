
CREATE PROCEDURE [dbo].[usp_EventDay_SEL_EventID]
				@EventID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ED.EventDailyID
	, ED.EventID 
	, ED.DayID
	, ED.RecurEvery
	, ED.UpdateStamp
FROM 
	dbo.EventDaily ED WITH(NOLOCK)
WHERE 
	ED.EventID = @EventID

END