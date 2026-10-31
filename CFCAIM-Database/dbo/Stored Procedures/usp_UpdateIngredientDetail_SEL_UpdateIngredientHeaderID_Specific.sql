CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID_Specific]
			@UpdateIngredientHeaderID	INT
AS
BEGIN

SET NOCOUNT ON;

--exec usp_UpdateIngredientDetail_SEL_UpdateIngredientHeaderID_DefaultOverlay 1

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
	, ISNULL(CNO.NutrientValue, ISNULL(CN.Value,0)) as CurrentNutrientVal	
	, CP.CFCPlantName 
	, CP.CFCPlantCode	 
	, U.UserName  
	
FROM 
	dbo.UpdateIngredientDetail D 
	INNER JOIN
		dbo.UpdateIngredientHeader H
			on D.UpdateIngredientHeaderID = H.UpdateIngredientHeaderID
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
		dbo.CFCIngredientNutrientOverlay CNO 
			ON CNO.CFCPlantID = D.CFCPlantID					--02/29/24 was joining on H.CFCPlantID, should be D.CFCPlantID
			AND CNO.CFCIngredientID = D.CFCIngredientID
			AND CNO.CFCNutrientID = D.CFCNutrientID
	LEFT OUTER JOIN
		dbo.CFCPlant CP 
			ON D.CFCPlantID = CP.CFCPlantID 
	LEFT OUTER JOIN 
		CFCUser.CFCUser U 
			ON D.CFCUserID = U.CFCUserID

WHERE 
	D.UpdateIngredientHeaderID = @UpdateIngredientHeaderID


ORDER BY 
	I.CFCIngredientCode,
	N.CFCNutrientCode


END