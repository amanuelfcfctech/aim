CREATE PROCEDURE [dbo].[usp_CFCPlantRefAimOwnerLocation_SEL_AimOwnerLocationID]
				@AIMOwnerLocationID				INT
AS
BEGIN
SET NOCOUNT ON

-- exec usp_CFCPlantRefAimOwnerLocation_SEL_AimOwnerLocationID 1

SELECT	
	AOL.CFCPlantRefAIMOwnerLocationID
	, AOL.AIMOwnerLocationID
	, AOL.CFCPlantID
	, CFCPlantCode 
	, CFCPlantName
	, CP.IsActive
	, CP.UpdateStamp
	, CP.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
		
FROM 
	dbo.CFCPlantRefAIMOwnerLocation AOL WITH(NOLOCK)
	INNER JOIN
		dbo.CFCPlant CP WITH(NOLOCK)
			ON AOL.CFCPlantID = CP.CFCPlantID 	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AOL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
		AOL.AIMOwnerLocationID = @AIMOwnerLocationID
	AND CP.IsActive = 1 
END


