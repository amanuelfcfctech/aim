CREATE PROCEDURE [dbo].[usp_AIMDatabase_SEL_IsActive]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SL.*

    -- 1/11/21 - changed to remove aspnet_User table
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMDatabase SL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	SL.IsActive = @IsActive
END
