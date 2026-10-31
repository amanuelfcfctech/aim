CREATE PROCEDURE [dbo].[usp_Days_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  D.DayID
	, D.DayNumber
	, D.UpdateStamp
FROM 
	dbo.Days D WITH(NOLOCK)

END