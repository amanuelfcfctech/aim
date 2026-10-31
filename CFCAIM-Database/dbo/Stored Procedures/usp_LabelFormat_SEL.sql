

-------------------------------------------------
CREATE PROCEDURE [dbo].[usp_LabelFormat_SEL]
	
AS
BEGIN

--exec usp_LabelFormat_SEL

SET NOCOUNT ON;

SELECT
	LF.LabelFormatID
	, LF.LabelID
	, LF.MaterialTypeID
	, LF.IsBold
	, LF.FontSize
	, LF.OrderNumber	
	, C.UserControlName
	, A.AlternateName
	, LF.UpdateStamp
	, U.UserName
FROM 
	dbo.LabelFormat LF	
INNER JOIN  
	dbo.Label L
		ON LF.LabelID = L.LabelID
INNER JOIN 
	dbo.Control C 
		ON	L.ControlID = C.ControlID
INNER JOIN
	dbo.AlternateName A
		ON C.ControlID = A.ControlID
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON C.CFCUserID = U.CFCUserID 



END