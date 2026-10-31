CREATE PROCEDURE [dbo].[usp_EventRunType_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	ET.EventRunTypeID
	, ET.EventRunTypeName
	, ET.UpdateStamp
	
FROM 
	dbo.EventRunType ET WITH(NOLOCK)

END