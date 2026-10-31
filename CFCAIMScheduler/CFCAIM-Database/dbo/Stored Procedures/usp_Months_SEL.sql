CREATE PROCEDURE [dbo].[usp_Months_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  M.MonthID
	, M.MonthName
	, M.UpdateStamp
FROM 
	dbo.Months M WITH(NOLOCK)

ORDER BY
	M.SortOrder

END