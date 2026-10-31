CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID_UpdateIngredientDetailID]
			@UpdateIngredientHeaderID	INT
		  , @UpdateIngredientDetailID	INT
AS
BEGIN

SET NOCOUNT ON;


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
	, U.UserName  
	
FROM 
	dbo.UpdateIngredientDetail D 
	INNER JOIN 
		dbo.CFCIngredient I 
			ON D.CFCIngredientID = I.CFCIngredientID
	INNER JOIN 
		dbo.CFCNutrient N 
			ON D.CFCNutrientID = N.CFCNutrientID
	LEFT OUTER JOIN 
		dbo.CFCIngredientNutrient CN 
			ON	CN.CFCNutrientID = N.CFCNutrientID 
			AND CN.CFCIngredientID = I.CFCIngredientID 
	LEFT OUTER JOIN
		dbo.CFCPlant CP 
			ON D.CFCPlantID = CP.CFCPlantID 
	LEFT OUTER JOIN 
		CFCUser.CFCUser U 
			ON D.UpdateCFCUserID = U.CFCUserID
WHERE 
	D.UpdateIngredientHeaderID = @UpdateIngredientHeaderID
	and D.UpdateIngredientDetailID = @UpdateIngredientDetailID

ORDER BY 
	I.CFCIngredientCode,
	N.CFCNutrientCode


END