CREATE PROCEDURE [dbo].[usp_AlternateName_SEL_FormID]
		    @FormID			INT

--exec [usp_AlternateName_SEL_FormID] 1

AS
BEGIN
SET NOCOUNT ON;

SELECT

	AT.FormID	
	, AT.ControlID
	, AT.AlternateName
	, AT.Visible 	
	, C.UserControlName
	, C.ControlName	
	, C.AllowAlternateName	
	, C.UserControlName
	, C.RequiredField
	, C.LinkToControlID
	--, AT.LabelFieldFirst				-- 6/26/20 - commented
	--, AT.LabelFieldLast				-- 6/26/20 - commented
	, AT.AlternateNameID
	, AT.UpdateStamp					-- 1/6/20
	, AT.ShowCodeNameOrBothID			-- 6/19/20
	, U.UserName

FROM 
	dbo.AlternateName AT

INNER JOIN dbo.Control C 
	ON	AT.ControlID = C.ControlID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)

		--ON C.CFCUserID = U.CFCUserID	-- 8/9/21
		ON AT.CFCUserID = U.CFCUserID	-- 8/9/21
WHERE
	C.AllowAlternateName = 1
	AND C.FormID =  @FormID

ORDER BY AT.AlternateName

END