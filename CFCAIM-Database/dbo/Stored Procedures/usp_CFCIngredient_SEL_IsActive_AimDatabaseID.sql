CREATE PROCEDURE [dbo].[usp_CFCIngredient_SEL_IsActive_AimDatabaseID]
				@IsActive				BIT
				, @AimDatabaseID		INT 
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCIngredient_SEL_IsActive_AimDatabaseID true, 1019

SELECT 
	  CI.CFCIngredientID
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, CI.IsActive
	, CI.UpdateStamp	
	, CI.AIMDatabaseID 
	, AD.Name as AimDatabaseName
	, CI.MathType 

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName


FROM 
	dbo.CFCIngredient CI WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMDatabase AD with (nolock) 
			ON CI.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CI.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID


WHERE 
	CI.IsActive = @IsActive
	and CI.AIMDatabaseID = @AimDatabaseID
ORDER BY 
	CI.CFCIngredientCode
END
