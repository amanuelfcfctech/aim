CREATE PROCEDURE [dbo].[usp_Weeks_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  W.WeekID
	, W.WeekName
	, W.UpdateStamp
FROM 
	dbo.Weeks W WITH(NOLOCK)

END