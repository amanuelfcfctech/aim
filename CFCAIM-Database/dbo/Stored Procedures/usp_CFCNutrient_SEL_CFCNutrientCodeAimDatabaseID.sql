CREATE PROCEDURE [dbo].[usp_CFCNutrient_SEL_CFCNutrientCodeAimDatabaseID]
			  @CFCNutrientCode	VARCHAR(10)
			, @AimDatabaseID	INT
AS
BEGIN
SET NOCOUNT ON;

SELECT
	  CN.CFCNutrientID
	, CN.CFCNutrientCode
	, CN.CFCNutrientName
	, CN.IsActive
	, CN.UpdateStamp
	, CN.Unit
	, CN.AIMDatabaseID
	, CN.CFCUserID
	, CN.MathTypeID
	, CN.IsComputed	
	, AD.Name as AimDatabaseName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.CFCNutrient CN 
	LEFT OUTER JOIN dbo.AIMDatabase AD  
			ON CN.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON CN.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU 
	--		ON AU.UserId = U.UserID
WHERE 
	CN.CFCNutrientCode = @CFCNutrientCode 
	AND CN.AIMDatabaseID = @AimDatabaseID

ORDER BY 
	CN.CFCNutrientName

END