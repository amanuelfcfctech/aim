CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_SEL_SourceID]
		@AIMSourceID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  SL.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.AIMSourceLocation SL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
		SL.AIMSourceID = @AIMSourceID 
	AND SL.IsActive = 1
END