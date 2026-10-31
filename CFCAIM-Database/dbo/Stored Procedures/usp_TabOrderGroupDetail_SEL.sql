CREATE PROCEDURE [dbo].[usp_TabOrderGroupDetail_SEL]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  TOGD.TabOrderGroupDetailID
	, TOGD.TabOrderGroupID
	, TOGD.ControlID
	, C.ControlName
	, TOGD.TabOrder
	, TOGD.UpdateStamp
	, TOGD.CFCUserID
	, U.UserName
FROM 
	dbo.TabOrderGroupDetail TOGD
	INNER JOIN
		dbo.Control C
			ON C.ControlID = TOGD.ControlID
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON U.CFCUserID = TOGD.CFCUserID
ORDER BY 
	TOGD.TabOrder

END