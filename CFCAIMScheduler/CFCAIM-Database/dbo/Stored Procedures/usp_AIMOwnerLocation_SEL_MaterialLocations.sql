CREATE PROCEDURE [dbo].[usp_AIMOwnerLocation_SEL_MaterialLocations]

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	    OL.AIMOwnerLocationID
	  , OL.AIMOwnerID
	  , OL.LocationCode
	  , OL.LocationName
	  , OL.IsActive
	  , O.OwnerCode
	  , U.UserName
FROM 
	dbo.AIMOwnerLocation OL 
	INNER JOIN
		dbo.AIMOwner O
			ON OL.AIMOwnerID = O.AIMOwnerID
	INNER JOIN	
		dbo.AIMMaterial M 
			ON OL.AIMOwnerLocationID = M.AIMOwnerLocationID

	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON OL.CFCUserID = U.CFCUserID

GROUP BY
  OL.AIMOwnerLocationID
	  , OL.AIMOwnerID
	  , OL.LocationCode
	  , OL.LocationName
	  , OL.IsActive
	  , O.OwnerCode
	  , U.UserName
END