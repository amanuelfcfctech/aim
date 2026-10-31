CREATE PROCEDURE [dbo].[usp_LicensingConfig_SEL_ConfigID]
		@ConfigID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	LC.*
FROM 
	licensing.Config LC
WHERE 
	LC.ConfigID =@ConfigID
END