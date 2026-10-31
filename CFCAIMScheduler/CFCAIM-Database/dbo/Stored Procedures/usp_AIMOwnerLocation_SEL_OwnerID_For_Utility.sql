CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_OwnerID_For_Utility]
		@AIMOwnerID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  OL.AIMOwnerLocationID
	, OL.AIMOwnerID
	, OL.IsActive
	, CASE WHEN OL.LocationCode IS NULL THEN LEFT(OL.LocationName,20) ELSE OL.LocationCode end as LocationCode	  
	, CASE WHEN OL.LocationName IS NULL THEN OL.LocationCode ELSE OL.LocationName end as LocationName	  

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
		OL.AIMOwnerID = @AIMOwnerID 
	AND OL.IsActive = 1

ORDER BY
	OL.LocationName
END