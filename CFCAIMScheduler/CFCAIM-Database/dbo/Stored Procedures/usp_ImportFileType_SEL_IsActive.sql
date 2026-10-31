CREATE PROCEDURE [dbo].[usp_ImportFileType_SEL_IsActive]
		@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  FT.ImportFileTypeID
    , FT.Name
    , FT.IsActive
    , FT.UpdateStamp
    , FT.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.ImportFileType FT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON FT.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	FT.IsActive = @IsActive

END