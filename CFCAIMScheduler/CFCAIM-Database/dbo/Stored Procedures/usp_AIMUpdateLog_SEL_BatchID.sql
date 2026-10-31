CREATE PROCEDURE [dbo].[usp_AIMUpdateLog_SEL_BatchID]

@BatchID Int

AS
BEGIN
SET NOCOUNT ON;

SELECT 
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

WHERE
	AUL.BatchID = @BatchID and (AUL.ErrorNumber <> 0 or Len(AUL.Message) > 0)
END