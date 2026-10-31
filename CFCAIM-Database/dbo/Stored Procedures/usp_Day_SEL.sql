create PROCEDURE [dbo].[usp_Day_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  D.DayID
	, D.Day
	, D.UpdateStamp
FROM 
	dbo.Day D WITH(NOLOCK)

END