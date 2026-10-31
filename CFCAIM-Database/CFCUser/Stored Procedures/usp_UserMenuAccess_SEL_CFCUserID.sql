CREATE PROCEDURE [CFCUser].[usp_UserMenuAccess_SEL_CFCUserID]
	@CFCUserID	INT

-- exec cfcuser.usp_UserMenuAccess_SEL_CFCUserID 13
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	 UM.*
	 , MI.Description
	 , MI.ParentNo
	 , MI.Level1
	 , MI.Level2
	 , MI.Level3
	 , MI.Level4
	 , MI.Level5
	 , MI.SortOrder

FROM 
	dbo.UserMenuAccess UM
LEFT OUTER JOIN
	dbo.MenuItems MI
		ON UM.MenuItemsID = MI.MenuItemsID

WHERE 
	UM.CFCUserID = @CFCUserID
	and MI.IsActive = 1

ORDER BY MI.SortOrder
	
END