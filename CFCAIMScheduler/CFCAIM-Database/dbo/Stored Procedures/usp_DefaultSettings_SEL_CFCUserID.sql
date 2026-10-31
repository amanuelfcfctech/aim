CREATE PROCEDURE [dbo].[usp_DefaultSettings_SEL_CFCUserID]
				@CFCUserID INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  DS.*
	
FROM 
	dbo.DefaultSettings DS WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DS.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	DS.CFCUserID = @CFCUserID

END