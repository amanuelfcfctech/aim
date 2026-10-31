
CREATE PROCEDURE [dbo].[usp_EventMonthly_SEL_EventID]

	@EventID				INT

--exec dbo.usp_EventMonthly_SEL_EventID 8

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	EM.EventMonthlyID
	, EM.EventID
	, EM.MonthID
	, EM.SpecificDays
	, EM.SpecificWeek
	, EMSD.DayID	
	, EMSWD.WeekID
	, RTrim(W.WeekName) as WeekName
	, WN.WeekNumberID
	, RTrim(WN.WeekNumberName) as WeekNumberName
	, RTRIM(M.[MonthName]) as [MonthName]
	
FROM 
	dbo.EventMonthly EM WITH(NOLOCK)
LEFT OUTER JOIN
	dbo.Months M
		ON EM.MonthID = M.MonthID
LEFT OUTER JOIN
	dbo.EventMonthlySpecificDays EMSD
		ON EM.EventMonthlyID = EMSD.EventMonthlyID
LEFT OUTER JOIN
	dbo.EventMonthlySpecificWeekDays EMSWD
		ON EM.EventMonthlyID = EMSWD.EventMonthlyID
LEFT OUTER JOIN
	dbo.Weeks W
		ON EMSWD.WeekID = W.WeekID
LEFT OUTER JOIN	
	dbo.EventMonthlySpecificWeekNumber EMSWN
		ON EM.EventMonthlyID = EMSWN.EventMonthlyID
LEFT OUTER JOIN
	dbo.WeekNumber WN
		ON EMSWN.WeekNumberID = WN.WeekNumberID

WHERE
	EM.EventID = @EventID

END