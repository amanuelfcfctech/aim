CREATE PROCEDURE [dbo].[usp_CFCPlantRefAIMOwnerLocation_SEL_All]
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  CPAOL.*	
	, CP.CFCPlantCode
	, CP.CFCPlantName	
	, AOL.LocationCode
	, AOL.LocationName	
	, AOL.AIMOwnerID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.CFCPlantRefAIMOwnerLocation CPAOL WITH(NOLOCK)
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL WITH(NOLOCK)
			ON AOL.AIMOwnerLocationID = CPAOL.AIMOwnerLocationID
	LEFT OUTER JOIN 
		dbo.CFCPlant CP WITH(NOLOCK)
			ON CP.CFCPlantID = CPAOL.CFCPlantID		
	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CPAOL.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID	

WHERE AOL.LocationCode IS NOT NULL and AOL.LocationName IS NOT NULL

END
