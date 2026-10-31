CREATE PROCEDURE [dbo].[usp_Label_SEL_IsVisibleToPrinterLabel]

	
AS
BEGIN

DECLARE @SQL				VARCHAR(MAX) = ''
DECLARE @Where				VARCHAR(MAX) = ''


--exec usp_Label_SEL

SET NOCOUNT ON;

SELECT
	
	  L.LabelID	
	, L.ControlID
	, AT.AlternateNameID	
	, C.ControlName	
	, C.UserControlName		
	, AT.AlternateName		
	, L.UpdateStamp
	, U.UserName	
	
FROM 
	dbo.Label L
INNER JOIN dbo.Control C 
	ON	L.ControlID = C.ControlID
INNER JOIN 	dbo.AlternateName AT
	ON C.ControlID = AT.ControlID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON C.CFCUserID = U.CFCUserID 
WHERE
	L.IsVisibleToPrinterLabel = 1

ORDER BY C.UserControlName


END