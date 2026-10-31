CREATE PROCEDURE [dbo].[usp_CFCProduct_SEL_IsActive_AimDatabaseID_CFCPlantID]
				@IsActive				BIT
				, @AIMDatabaseID		INT
				, @CFCPlantID			INT
AS				
BEGIN
SET NOCOUNT ON;

SELECT 

	  CP.*
	, CPL.CFCPlantCode
	, CPL.CFCPlantName	
	, AD.Name as AimDatabaseName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.CFCProduct CP WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMDatabase AD with (nolock) 
			ON CP.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CP.CFCUserID = U.CFCUserID

		-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
		--LEFT OUTER JOIN
		--	dbo.aspnet_Users AU WITH(NOLOCK)
		--		ON AU.UserId = U.UserID

		LEFT OUTER JOIN
			dbo.CFCPlant CPL WITH(NOLOCK)
				ON CP.CFCPlantID = CPL.CFCPlantID

WHERE 
	CP.IsActive = @IsActive AND
	CP.AIMDatabaseID = @AIMDatabaseID AND 
	CP.CFCPlantID = @CFCPlantID

END