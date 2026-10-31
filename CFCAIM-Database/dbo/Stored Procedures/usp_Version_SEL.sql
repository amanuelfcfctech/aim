
CREATE PROCEDURE [dbo].[usp_Version_SEL]
			
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	  ID
	, [Version]
	, CreatedDate
FROM 
	dbo.[Version] WITH(NOLOCK)

ORDER BY
	
	--8/23/18
	--CreatedDate DESC
	[Version] DESC

END