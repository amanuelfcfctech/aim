CREATE PROCEDURE [dbo].[usp_Application_SEL_AppName]

@AppName	NVARCHAR(50)
			
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
	 A.*
FROM 
	dbo.[Application] A WITH(NOLOCK)

WHERE A.AppName = @AppName

ORDER BY
	UpdateStamp DESC

END