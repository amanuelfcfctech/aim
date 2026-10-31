CREATE PROCEDURE [dbo].[usp_CFCPlant_SEL_IsActive_AimDatabaseID]
				@IsActive				BIT
				, @AIMDatabaseID		INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  CP.*	
	, AD.Name as AimDatabaseName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName	
	, U.UserName	


	FROM 
		dbo.CFCPlant CP WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMDatabase AD with (nolock) 
			ON CP.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CP.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID
WHERE 
	CP.IsActive = @IsActive
	and CP.AIMDatabaseID = @AIMDatabaseID
END
