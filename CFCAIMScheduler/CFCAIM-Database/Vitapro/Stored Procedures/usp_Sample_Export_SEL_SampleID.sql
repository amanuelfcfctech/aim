
CREATE PROCEDURE [Vitapro].[usp_Sample_Export_SEL_SampleID]
			@SampleID	INT
			, @LineID   INT

AS
BEGIN

SET NOCOUNT ON;

--exec [Vitapro].[usp_Sample_Export_SEL_SampleID] 3, 1
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '#Line1%')
	DROP TABLE #Line1


SELECT 
	  CINO.CFCIngredientID
	, CINO.CFCNutrientID
	, CINO.NutrientValue
	INTO #Line1
FROM 
	Vitapro.LineRefCFCPlant LRCP
	INNER JOIN 
		dbo.CFCIngredientNutrientOverlay CINO
			ON CINO.CFCPlantID = LRCP.CFCPlantID
WHERE 
	LRCP.IsActive = 1
	AND LRCP.LineID = @LineID


SELECT 

	S.LotNumber
	, CI.CFCIngredientID
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, N.CFCNutrientID
	, N.CFCNutrientCode
	, N.CFCNutrientName
	, N.AIMDatabaseID 	
	, SA.AIMUsedValue			
	, ISNULL(L.NutrientValue, ISNULL(CN.Value,0)) as CurrentNutrientVal	
	, CASE WHEN L.NutrientValue IS NULL THEN 0 ELSE 1 END AS IsOverlay	 	   	
	, (SELECT TOP 1 CP.CFCPlantCode FROM Vitapro.LineRefCFCPlant LRCP INNER JOIN dbo.CFCPlant CP ON CP.CFCPlantID = LRCP.CFCPlantID  WHERE LRCP.LineID = @LineID AND LRCP.IsActive = 1) Plant	
FROM 
	Vitapro.Lot V
	INNER JOIN 
		dbo.Sample S 
			on V.SampleID = S.SampleID
	INNER JOIN 
		dbo.SampleAIM SA
			on S.SampleID = SA.SampleID
	INNER JOIN 
		dbo.AIMMaterial AM 
			on S.AIMMaterialID = AM.AIMMaterialID
	INNER JOIN 
		dbo.CFCIngredientRefAIMMaterial CIRAM 
			on AM.AIMMaterialID = CIRAM.AIMMaterialID
	INNER JOIN 
		dbo.CFCIngredient CI 
			on CIRAM.CFCIngredientID = CI.CFCIngredientID	
	INNER JOIN 
		dbo.CFCNutrientRefAIMNutrient CNRAN 
			on sa.AIMNutrientID = CNRAN.AIMNutrientID
	INNER JOIN 
		dbo.CFCNutrient N 
			on CNRAN.CFCNutrientID = N.CFCNutrientID	
	LEFT OUTER JOIN
		#Line1 L
		ON L.CFCIngredientID = CI.CFCIngredientID
		AND L.CFCNutrientID = N.CFCNutrientID

	LEFT OUTER JOIN 
		dbo.CFCIngredientNutrient CN 
			ON	CN.CFCNutrientID = N.CFCNutrientID 
			AND CN.CFCIngredientID = CI.CFCIngredientID 

WHERE 
	V.SampleID = @SampleID

ORDER BY 
	CI.CFCIngredientCode,
	N.CFCNutrientCode

IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '#Line1%')
	DROP TABLE #Line1



END