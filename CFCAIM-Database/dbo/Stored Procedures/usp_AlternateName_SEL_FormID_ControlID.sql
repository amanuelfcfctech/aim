CREATE PROCEDURE [dbo].[usp_AlternateName_SEL_FormID_ControlID]

	@FormID				INT
	, @ControlID		INT
	

--exec usp_AlternateName_SEL_FormID_ControlID 1, 41

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(MAX) = ''
DECLARE @LinkID				INT

SET @LinkID = 0
SET @LinkID = (SELECT LinkToControlID from dbo.Control WHERE ControlID = @ControlID)




SET @SQL = '
	SELECT TOP 1

	AN.FormID	
	, AN.ControlID
	, AN.AlternateName
	, AN.Visible 	
	, C.UserControlName
	, C.ControlName	
	, C.AllowAlternateName	
	, C.UserControlName
	, C.RequiredField
	, C.LinkToControlID
	, AN.AlternateNameID
	, AN.UpdateStamp					
	, AN.ShowCodeNameOrBothID			
	, U.UserName
	
FROM 
	dbo.[Control] C 
LEFT OUTER JOIN 
	dbo.AlternateName AN '

IF @LinkID > 0 
	SET @SQL = @SQL + ' ON AN.ControlID = C.LinkToControlID and AN.FormID = C.FormID '
ELSE
	SET @SQL = @SQL + ' ON AN.ControlID = C.ControlID and AN.FormID = C.FormID '

SET @SQL = @SQL + '
		
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON C.CFCUserID = U.CFCUserID

WHERE AN.FormID = ' + CAST(@FormID AS VARCHAR) + ' and C.ControlID = ' + CAST(@ControlID AS VARCHAR)

EXEC(@SQL)


END