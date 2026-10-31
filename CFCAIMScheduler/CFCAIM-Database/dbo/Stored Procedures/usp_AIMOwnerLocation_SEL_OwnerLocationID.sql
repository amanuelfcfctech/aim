CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_OwnerLocationID]
		@OwnerLocationID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  OL.*

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMOwnerLocation OL WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON OL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
		OL.AIMOwnerLocationID = @OwnerLocationID 
	AND OL.IsActive = 1
END

