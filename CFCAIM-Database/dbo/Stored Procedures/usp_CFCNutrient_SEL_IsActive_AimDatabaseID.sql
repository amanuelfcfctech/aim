
CREATE PROCEDURE [dbo].[usp_CFCNutrient_SEL_IsActive_AimDatabaseID]
		@IsActive			BIT
		, @AimDatabaseID	INT
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
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	CN.IsActive = @IsActive and
	CN.AIMDatabaseID = @AimDatabaseID

ORDER BY CN.CFCNutrientName

END

