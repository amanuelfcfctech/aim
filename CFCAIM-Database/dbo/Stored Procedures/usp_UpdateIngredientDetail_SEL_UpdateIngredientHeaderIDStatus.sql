CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderIDStatus]
			  @UpdateIngredientHeaderID		INT
			, @Status						INT
AS
BEGIN

SET NOCOUNT ON;

--exec usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID 51

SELECT 
	  D.UpdateIngredientDetailID
	, D.UpdateIngredientHeaderID
	, D.CFCIngredientID
	, D.CFCNutrientID
	, D.ProposedValue
	, D.CreateStamp
	, D.CFCUserID
	, D.UpdateStamp
	, D.UpdateCFCUserID
	, D.Override
	, D.Mean
	, D.StandardDeviation
	, D.Status
	, D.CFCPlantID
	, D.NumberOfSamples
	, D.IsOverlay
	, I.CFCIngredientCode
	, I.CFCIngredientName
	, N.CFCNutrientCode
	, N.CFCNutrientName
	, N.AIMDatabaseID 
	, ISNULL(CN.Value, 0) as CurrentNutrientVal	
	, CP.CFCPlantName 
	, CP.CFCPlantCode

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName  
	, U.UserName  

FROM 
	dbo.UpdateIngredientDetail D WITH(NOLOCK)
	INNER JOIN 
		dbo.CFCIngredient I WITH(NOLOCK)
			ON D.CFCIngredientID = I.CFCIngredientID
	INNER JOIN 
		dbo.CFCNutrient N WITH(NOLOCK)
			ON D.CFCNutrientID = N.CFCNutrientID
	LEFT OUTER JOIN 
		dbo.CFCIngredientNutrient CN WITH(NOLOCK)
			ON	CN.CFCNutrientID = N.CFCNutrientID 
			AND CN.CFCIngredientID = I.CFCIngredientID 
	LEFT OUTER JOIN
		dbo.CFCPlant CP with (nolock)
			ON D.CFCPlantID = CP.CFCPlantID 
	LEFT OUTER JOIN 
		CFCUser.CFCUser U WITH(NOLOCK) 
			ON D.UpdateCFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN	
	--	dbo.aspnet_Users AU WITH(NOLOCK) 
	--		ON AU.UserId = U.UserID
WHERE 
	D.UpdateIngredientHeaderID = @UpdateIngredientHeaderID
	AND D.Status = @Status
ORDER BY 
	  I.CFCIngredientCode
	, N.CFCNutrientCode
	, CP.CFCPlantCode

END