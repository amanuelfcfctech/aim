CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_OwnerID]
		@AIMOwnerID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  OL.*
	, O.OwnerName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName	
	, U.UserName	

FROM 
	dbo.AIMOwnerLocation OL WITH(NOLOCK)
	LEFT JOIN dbo.AIMOwner O with (nolock)
		ON OL.AIMOwnerID = O.AIMOwnerID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON OL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE
		OL.AIMOwnerID = @AIMOwnerID 
	AND OL.IsActive = 1

ORDER BY
	OL.LocationName
END

