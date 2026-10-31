CREATE PROCEDURE [dbo].[usp_CFCNutrientInitialLoad_SEL_IsActive_AimDatabaseID]
		@IsActive			BIT
		, @AimDatabaseID	INT

-- exec [usp_CFCNutrientInitialLoad_SEL_IsActive_AimDatabaseID] 1, 1

AS
BEGIN
SET NOCOUNT ON;

SELECT
	    CN.*	  
	  , AD.Name as AimDatabaseName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 
	dbo.CFCNutrient CN WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMDatabase AD with (nolock) 
			ON CN.AIMDatabaseID = AD.AIMDatabaseID
	LEFT JOIN dbo.AIMNutrient AM with (nolock) 
			ON CN.CFCNutrientName = AM.AIMNutrientName 
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	CN.IsActive = @IsActive AND 
	CN.AIMDatabaseID = @AimDatabaseID 
	--AND	AM.AIMNutrientID IS NULL

ORDER BY CN.CFCNutrientName

END