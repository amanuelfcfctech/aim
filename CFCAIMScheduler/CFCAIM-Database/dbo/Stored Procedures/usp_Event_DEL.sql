CREATE PROCEDURE [dbo].[usp_Event_DEL]
				@EventID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE FROM  dbo.EventMonthlySpecificDays Where EventMonthlyID IN 
	(SELECT EventMonthlyID from dbo.EventMonthly EM where EventID = @EventID)

DELETE FROM  dbo.EventMonthlySpecificWeekDays Where EventMonthlyID IN 
	(SELECT EventMonthlyID from dbo.EventMonthly EM where EventID = @EventID)

DELETE FROM  dbo.EventMonthlySpecificWeekNumber Where EventMonthlyID IN 
	(SELECT EventMonthlyID from dbo.EventMonthly EM where EventID = @EventID)


DELETE FROM  dbo.EventDaily WITH(ROWLOCK) WHERE 	EventID = @EventID
DELETE FROM  dbo.EventWeekly WITH(ROWLOCK) WHERE 	EventID = @EventID
DELETE FROM  dbo.EventMonthly WITH(ROWLOCK) WHERE 	EventID = @EventID




DELETE
FROM 
	dbo.Event WITH(ROWLOCK)
WHERE
	EventID = @EventID

END