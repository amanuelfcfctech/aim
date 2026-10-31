CREATE PROCEDURE [CFCUser].[usp_UserMenuAccess_SEL_CFCUserID_MenuDescription]
		@CFCUserID	INT
  	  , @Description	nvarchar(50)

-- exec cfcuser.usp_UserMenuAccess_SEL_CFCUserID_MenuDescription 36, 'Analysis'
			
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	 UMA.*
FROM 	
	dbo.UserMenuAccess UMA WITH (NOLOCK)
LEFT OUTER JOIN
	dbo.MenuItems MI
		ON UMa.MenuItemsID = MI.MenuItemsID

WHERE 
	UMA.CFCUserID = @CFCUserID and
	MI.Description = @Description

	
END