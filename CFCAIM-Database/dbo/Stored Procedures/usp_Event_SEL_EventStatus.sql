CREATE PROCEDURE [dbo].[usp_Event_SEL_EventStatus]

	@EventStatus			BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	E.EventID
	, E.EventName
	, E.EventDescription
	, E.EventRunTypeID
	, E.EventTypeID
	, E.StartDateTime	
	, E.LastRunDateTime	
	, E.TemplateID	
	, E.EventStatus
	, E.UpdateStamp
	, E.CFCUserID
	, ET.EventRunTypeName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	
FROM 
	dbo.Event E WITH(NOLOCK)

LEFT OUTER JOIN
	dbo.EventRunType ET with (nolock)
		ON E.EventRunTypeID = ET.EventRunTypeID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON E.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

WHERE E.EventStatus = @EventStatus

END