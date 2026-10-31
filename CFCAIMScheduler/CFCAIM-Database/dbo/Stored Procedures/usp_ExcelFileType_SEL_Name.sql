CREATE PROCEDURE [dbo].[usp_ExcelFileType_SEL_Name]
		@Name			NVARCHAR(10)
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  EFT.ExcelFileTypeID
    , EFT.Name
    , EFT.IsActive
    , EFT.UpdateStamp
    , EFT.CFCUserID
	
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.ExcelFileType EFT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON EFT.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	EFT.Name = @Name and EFT.IsActive = 1

END