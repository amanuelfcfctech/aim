CREATE PROCEDURE [dbo].[usp_AlternateName_SEL_FormID_ControlName]

	@FormID				INT
	, @ControlName		VARCHAR(50)

--exec dbo.usp_AlternateName_SEL_FormID_ControlName 1, 'lblOwner'

AS
BEGIN
SET NOCOUNT ON;

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
	--, AN.LabelFieldFirst				-- 6/26/20 - commented
	--, AN.LabelFieldLast				-- 6/26/20 - commented
	, AN.AlternateNameID
	, AN.UpdateStamp					-- 1/6/20
	, AN.ShowCodeNameOrBothID			-- 6/19/20

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AlternateName AN with (nolock)
INNER JOIN 
	dbo.[Control] C with (nolock)		
		ON AN.ControlID = C.ControlID and AN.FormID = C.FormID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON C.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN
--	dbo.aspnet_Users AU WITH(NOLOCK)
--		ON AU.UserId = U.UserID


WHERE AN.FormID = @FormID and C.ControlName = @ControlName


END