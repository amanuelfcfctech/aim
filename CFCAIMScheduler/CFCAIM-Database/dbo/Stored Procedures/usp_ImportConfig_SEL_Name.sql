CREATE PROCEDURE [dbo].[usp_ImportConfig_SEL_Name]
		@Name			NVARCHAR(50)
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	IC.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.ImportConfig IC WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON IC.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
	IC.Name = @Name

END
