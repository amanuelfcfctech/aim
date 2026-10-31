CREATE PROCEDURE [dbo].[usp_AIMSourceLocation_SEL_AIMSourceID]
		@AIMSourceID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	    S.AIMSourceLocationID
	  , S.AIMSourceID
	  , S.AIMSupplierLocationCode
	  , S.SourceLocationName
	  , S.UpdateStamp
	  , S.IsActive

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
FROM 
	dbo.AIMSourceLocation S WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON S.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE
		S.AIMSourceID = @AIMSourceID 
	AND S.IsActive = 1

ORDER BY
	S.SourceLocationName

END

