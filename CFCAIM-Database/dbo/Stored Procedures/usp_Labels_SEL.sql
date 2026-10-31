CREATE PROCEDURE [dbo].[usp_Labels_SEL]

--exec [usp_Labels_SEL] 

AS
BEGIN
SET NOCOUNT ON;

SELECT
	
	  L.LabelsID	
	, L.ControlID
	, C.ControlName	
	, AT.AlternateNameID	
	, L.LabelFieldFirst
	, L.LabelFieldLast	
	, C.UserControlName		
	, AT.AlternateName		
	, L.UpdateStamp

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName	
	, U.UserName

	-- 6/23/21 - added for Vitapro
	, L.LabelFieldLine2
	, L.LabelFieldLine3
	, L.LabelFieldLine4
	, Line1Bold
	, Line2Bold
	, Line3Bold
	, Line4Bold
	, Line5Bold

	
FROM 
	dbo.Labels L
INNER JOIN dbo.Control C 
	ON	L.ControlID = C.ControlID
INNER JOIN 	dbo.AlternateName AT
	ON L.AlternateNameID = AT.AlternateNameID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON C.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID

ORDER BY C.UserControlName

END