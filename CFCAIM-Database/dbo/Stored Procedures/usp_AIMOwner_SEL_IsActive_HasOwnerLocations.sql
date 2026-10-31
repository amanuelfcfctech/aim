CREATE PROCEDURE [dbo].[usp_AIMOwner_SEL_IsActive_HasOwnerLocations]
		@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT DISTINCT
	  AO.AIMOwnerID
	, AO.OwnerCode
	, AO.OwnerName
	, AO.IsActive
	, AO.UpdateStamp

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName	
	, U.UserName	

FROM 
	dbo.AIMOwner AO WITH(NOLOCK)
	INNER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AO.AIMOwnerID = AOL.AIMOwnerID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AO.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
	AO.IsActive = @IsActive
	and AOL.IsActive = @IsActive;
END