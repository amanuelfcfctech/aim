CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrientOverlay_SEL]

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  CINO.CFCIngredientNutrientOverlayID
	, CINO.CFCPlantID
	, CINO.CFCIngredientID
	, CINO.CFCNutrientID
	, CINO.NutrientValue
	, CINO.CFCUserID
	, CINO.UpdateStamp	
	, CP.CFCPlantCode
	, CP.CFCPlantName
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, CN.CFCNutrientCode	
	, CN.CFCNutrientName
	, U.UserName

FROM 
	dbo.CFCIngredientNutrientOverlay CINO 
	LEFT OUTER JOIN
		CFCPlant CP
			ON CINO.CFCPlantID = CP.CFCPlantID
	LEFT OUTER JOIN
		CFCIngredient CI
			ON CINO.CFCIngredientID = CI.CFCIngredientID
	LEFT OUTER JOIN
		CFCNutrient CN
			ON CINO.CFCNutrientID = CN.CFCNutrientID
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON CINO.CFCUserID = U.CFCUserID

	
END