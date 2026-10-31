CREATE PROCEDURE [CFCUser].[usp_UserMenuAccess_INS_CFCUserID]
	@CFCUserID	INT

			
AS
BEGIN
SET NOCOUNT ON;

INSERT INTO	
	dbo.UserMenuAccess
	(
		  CFCUserID
		, MenuItemsID
		, UpdateStamp
		, UpdateUserID
		, IsNotAllowed 
	)
SELECT
       @CFCUserID
     , MI.MenuItemsID
	 , GETDATE()
	 , 0
	 , 0
FROM
	dbo.MenuItems MI
LEFT OUTER JOIN
	dbo.UserMenuAccess UM	
			ON UM.MenuItemsID = MI.MenuItemsID
			and CFCUserID = @CFCUserID
WHERE
    UM.UserMenuAccessID IS NULL



END