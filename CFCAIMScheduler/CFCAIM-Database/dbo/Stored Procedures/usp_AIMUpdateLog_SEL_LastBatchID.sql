CREATE PROCEDURE [dbo].[usp_AIMUpdateLog_SEL_LastBatchID]
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  AUL.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	
FROM 
	dbo.AIMUpdateLog AUL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AUL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
			
ORDER BY BatchID DESC
END