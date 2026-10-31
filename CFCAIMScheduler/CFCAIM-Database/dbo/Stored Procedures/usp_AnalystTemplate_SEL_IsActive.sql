CREATE PROCEDURE [dbo].[usp_AnalystTemplate_SEL_IsActive]
	@IsActive		BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  AT.*


	--, AU.UserName
	, U.UserName
FROM 
	dbo.AnalystTemplate AT WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AT.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	AT.IsActive = @IsActive
END
