CREATE PROCEDURE [dbo].[usp_CFCPlant_SEL_IsActive]
				@IsActive				BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  CP.CFCPlantID
	, CP.CFCPlantCode
	, CP.CFCPlantName
	, CP.IsActive
	, CP.UpdateStamp	
	, CP.AIMDatabaseID 

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

	FROM 
	dbo.CFCPlant CP WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CP.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE 
	CP.IsActive = @IsActive
END