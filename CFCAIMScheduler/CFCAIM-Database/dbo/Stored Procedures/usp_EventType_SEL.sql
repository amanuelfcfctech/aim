CREATE PROCEDURE [dbo].[usp_EventType_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	ET.EventTypeID
	, ET.EventTypeName
	, ET.UpdateStamp
	
FROM 
	dbo.EventType ET WITH(NOLOCK)

END