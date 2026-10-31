
CREATE PROCEDURE [dbo].[usp_Test_SEL_IsGrouped]
	 @IsGrouped BIT
	,@StartDate DATETIME 
	,@EndDate DATETIME 
	,@AIMOwnerLocationID INT

AS

--DECLARE	 @IsGrouped BIT = 0
--	,@StartDate DATETIME = '3/25/26'
--	,@EndDate DATETIME  = '3/27/26'
--	,@AIMOwnerLocationID INT = 7

IF OBJECT_ID('tempdb..#CostAnalysis') IS NOT NULL
    DROP TABLE #CostAnalysis

SELECT 
	 S.SampleID
	,S.SampleCode
	,S.SampleDate
	,AOL.LocationName
	,T.TestCode
	,T.TestName
	,T.DataSourceTypeID
	,S.AIMMaterialID
	,M.AIMMaterialCode
	,MT.MaterialTypeName
	,ISNULL(NC.AIMNutrientID, 0) AS AIMNutrientID
	,CONCAT(ISNULL(NC.TestGroupName, ''), ISNULL(NC.AIMNutrientName, '')) AS AIMNutrientName
	,T.TestCurrentCost AS TestCurrentCost
	,ISNULL(MC.TestMaterialCurrentUpcharge, 0) AS TestMaterialCurrentUpcharge
	,ISNULL(NC.TestNutrientResultCurrentUpcharge, 0) AS TestNutrientResultCurrentUpcharge

	,T.TestCurrentCost
		+ ISNULL(MC.TestMaterialCurrentUpcharge, 0)
		+ ISNULL(NC.TestNutrientResultCurrentUpcharge, 0) AS TotalCost

INTO 
	#CostAnalysis
FROM dbo.Sample S

INNER JOIN dbo.AIMOwnerLocation AOL
	ON AOL.AIMOwnerLocationID = S.AIMOwnerLocationID

INNER JOIN dbo.AIMMaterial M
	ON M.AIMMaterialID = S.AIMMaterialID

LEFT JOIN dbo.MaterialType MT
	ON MT.MaterialTypeID = M.MaterialTypeID

INNER JOIN
(
	SELECT DISTINCT
		 SR.SampleID
		,T.TestID
	FROM dbo.SampleResult SR
	INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient ANRAN
		ON ANRAN.AnalyzerNutrientID = SR.AnalyzerNutrientID
	INNER JOIN dbo.AnalyzerNutrient AN
		ON AN.AnalyzerNutrientID = ANRAN.AnalyzerNutrientID
	INNER JOIN dbo.Test T
		ON T.DataSourceTypeID = AN.DataSourceTypeID
	WHERE SR.IsActive = 1
		AND SR.AnalysisDate > @StartDate
		AND SR.AnalysisDate < @EndDate
) ST
	ON ST.SampleID = S.SampleID

INNER JOIN dbo.Test T
	ON T.TestID = ST.TestID

OUTER APPLY
(
	SELECT 
		SUM(TM.TestMaterialCurrentUpcharge) AS TestMaterialCurrentUpcharge
	FROM dbo.TestMaterial TM
	WHERE TM.TestID = T.TestID
		AND TM.AIMMaterialID = S.AIMMaterialID
) MC

OUTER APPLY
(
	SELECT 
		 SUM(TNR.TestNutrientResultCurrentUpcharge) AS TestNutrientResultCurrentUpcharge
		,STRING_AGG(CAST(N.AIMNutrientID AS VARCHAR(20)), ',') AS AIMNutrientID
		,STRING_AGG(N.AIMNutrientName, ', ') AS AIMNutrientName
		,STRING_AGG(TGN.TestGroupName, ',') AS TestGroupName
	FROM dbo.SampleResult SR
	INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient ANRAN
		ON ANRAN.AnalyzerNutrientID = SR.AnalyzerNutrientID
	INNER JOIN dbo.AIMNutrient N
		ON N.AIMNutrientID = ANRAN.AIMNutrientID
	INNER JOIN dbo.TestNutrientResult TNR
		ON TNR.TestID = T.TestID
		AND TNR.AIMNutrientID = ANRAN.AIMNutrientID
	LEFT OUTER JOIN dbo.TestGroupName TGN
		ON TGN.TestGroupNameID = TNR.TestGroupNameID
	WHERE SR.SampleID = S.SampleID
		AND SR.IsActive = 1
		AND SR.AnalysisDate > @StartDate
		AND SR.AnalysisDate < @EndDate
) NC

WHERE 
	S.AIMOwnerLocationID = @AIMOwnerLocationID
	AND (
		T.TestCurrentCost
		+ ISNULL(MC.TestMaterialCurrentUpcharge, 0)
		+ ISNULL(NC.TestNutrientResultCurrentUpcharge, 0)
	) > 0


IF @IsGrouped = 1 
	BEGIN
		SELECT 
			  0 as SampleID
			 ,'' AS SampleCode
			 ,CAST('0001-01-01' AS datetime2) AS SampleDate
			 ,LocationName
			,TestCode
			,TestName
			,DataSourceTypeID
			,AIMMaterialID
			,AIMMaterialCode
			,MaterialTypeName
			,ISNULL(AIMNutrientID, 0) AS AIMNutrientID
			,ISNULL(AIMNutrientName, '') AS AIMNutrientName			
			,Count(*) AS NumberOfTest
			,TestCurrentCost AS TestCurrentCost
			,ISNULL(TestMaterialCurrentUpcharge, 0) AS TestMaterialCurrentUpcharge
			,ISNULL(TestNutrientResultCurrentUpcharge, 0) AS TestNutrientResultCurrentUpcharge
			,TestCurrentCost
				+ ISNULL(TestMaterialCurrentUpcharge, 0)
				+ ISNULL(TestNutrientResultCurrentUpcharge, 0) AS Cost
			,SUM(TestCurrentCost
				+ ISNULL(TestMaterialCurrentUpcharge, 0)
				+ ISNULL(TestNutrientResultCurrentUpcharge, 0)) AS CostTotal

		FROM 
			#CostAnalysis

	
		GROUP BY
			 LocationName
			,TestCode
			,TestName
			,DataSourceTypeID
			,AIMMaterialID
			,AIMMaterialCode
			,MaterialTypeName
			,AIMNutrientID
			,AIMNutrientName
			,TestCurrentCost
			,TestMaterialCurrentUpcharge
			,TestNutrientResultCurrentUpcharge
			

		ORDER BY
			 MaterialTypeName DESC
			,AIMMaterialCode
			,TestCode
			,AIMNutrientName

	END
ELSE
	BEGIN
		SELECT 
			 SampleID
			,SampleCode
			,SampleDate
			,LocationName
			,TestCode
			,TestName
			,DataSourceTypeID
			,AIMMaterialID
			,AIMMaterialCode
			,MaterialTypeName
			,ISNULL(AIMNutrientID, 0) AS AIMNutrientID
			,ISNULL(AIMNutrientName, '') AS AIMNutrientName			
			,1 AS NumberOfTest
			,TestCurrentCost AS TestCurrentCost
			,ISNULL(TestMaterialCurrentUpcharge, 0) AS TestMaterialCurrentUpcharge
			,ISNULL(TestNutrientResultCurrentUpcharge, 0) AS TestNutrientResultCurrentUpcharge
			,TestCurrentCost
				+ ISNULL(TestMaterialCurrentUpcharge, 0)
				+ ISNULL(TestNutrientResultCurrentUpcharge, 0) AS Cost
			,TestCurrentCost
				+ ISNULL(TestMaterialCurrentUpcharge, 0)
				+ ISNULL(TestNutrientResultCurrentUpcharge, 0) AS CostTotal			
		FROM
			#CostAnalysis
		ORDER BY
			 MaterialTypeName DESC
			,AIMMaterialCode
			,TestCode
			,TestName
			,SampleCode
	END

IF OBJECT_ID('tempdb..#CostAnalysis') IS NOT NULL
    DROP TABLE #CostAnalysis