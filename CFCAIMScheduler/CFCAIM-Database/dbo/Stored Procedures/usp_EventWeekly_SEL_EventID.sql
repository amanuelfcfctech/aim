CREATE PROCEDURE [dbo].[usp_EventWeekly_SEL_EventID]

	@EventID				INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	EW.EventID
	, EW.EventWeeklyID
	, EW.RecurEvery
	, EW.UpdateStamp
	, EW.WeekID
	, RTRIM(W.WeekName) as WeekDayName
	
FROM 
	dbo.EventWeekly EW WITH(NOLOCK)

LEFT OUTER JOIN	
	dbo.Weeks W 
		ON EW.WeekID = W.WeekID

WHERE
	EW.EventID = @EventID

END