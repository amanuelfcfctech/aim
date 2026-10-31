CREATE PROCEDURE [dbo].[usp_Sample_SEL_GroupBy_NoOne]
	  	  @StartDate			DATE
		, @EndDate				DATE
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ST.AIMMaterialID
	, ST.AIMNutrientID
	, Q.SampleSize
	, Q.Quartile1
	, Q.Quartile3	
	, ST.Mean
	, ST.StandardDeviation
	, ST.Min
	, ST.Max
	, ST.Upper95Mean
	, ST.Lower95Mean
	, ST.Upper90Mean
	, ST.Lower90Mean
	, ST.MeanPlusStandardDeviation
	, ST.MeanMinusStandardDeviation
INTO #Statis
FROM
	(
	SELECT 
		  S.AIMMaterialID
		, SA.AIMNutrientID
		, ROUND(AVG(SA.AIMUsedValue), 4) AS Mean
		, ROUND(STDEV(SA.AIMUsedValue), 4) AS StandardDeviation
		, MIN(SA.AIMUsedValue) AS Min
		, MAX(SA.AIMUsedValue) AS Max
		, ROUND(AVG(SA.AIMUsedValue) + (1.96 * STDEV(SA.AIMUsedValue) / SQRT(COUNT(*))), 4) AS Upper95Mean
		, ROUND(AVG(SA.AIMUsedValue) - (1.96 * STDEV(SA.AIMUsedValue) / SQRT(COUNT(*))), 4) AS Lower95Mean
		, ROUND(AVG(SA.AIMUsedValue) + (1.645 * STDEV(SA.AIMUsedValue) / SQRT(COUNT(*))), 4) AS Upper90Mean
		, ROUND(AVG(SA.AIMUsedValue) - (1.645 * STDEV(SA.AIMUsedValue) / SQRT(COUNT(*))), 4) AS Lower90Mean
		, ROUND(AVG(SA.AIMUsedValue) + .5 * STDEV(SA.AIMUsedValue), 4) AS MeanPlusStandardDeviation
		, ROUND(AVG(SA.AIMUsedValue) - .5 * STDEV(SA.AIMUsedValue), 4) AS MeanMinusStandardDeviation
	FROM      
		dbo.Sample S WITH(NOLOCK)
		INNER JOIN 
			dbo.SampleAIM SA WITH(NOLOCK)
				ON SA.SampleID = S.SampleID
	WHERE
			S.IsActive = 1
		AND S.SampleDate BETWEEN @StartDate and @EndDate
		AND SA.IsActive = 1
	GROUP BY 
		  S.AIMMaterialID
		, SA.AIMNutrientID
	) ST
	INNER JOIN
		(
	SELECT 
		  C.AIMMaterialID
		, C.AIMNutrientID
		, COUNT(*) AS SampleSize
		, ROUND((Max(Q1NextVal) - MAX(Q1Val)) * Max(Q1Frac) + Max(Q1Val), 4) AS Quartile1
		, ROUND((Max(Q3NextVal) - MAX(Q3Val)) * Max(Q3Frac) + Max(Q3Val), 4) AS Quartile3
	FROM     
		(
		SELECT 
			  D.AIMMaterialID
			, D.AIMNutrientID
			, CASE WHEN RowNum = Q1Idx THEN AIMUsedValue ELSE NULL END AS Q1Val
			, CASE WHEN RowNum = Q1Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q1NextVal
			, CASE WHEN RowNum = Q3Idx THEN AIMUsedValue ELSE NULL END AS Q3Val
			, CASE WHEN RowNum = Q3Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q3NextVal
			, Q1Frac
			, Q3Frac
			, AIMUsedValue
		FROM      /*Calculate a row number sorted by measure for each group.*/ 
			(
			SELECT 
				  SA.AIMUsedValue 
				, S.AIMMaterialID 
				, SA.AIMNutrientID 
				, ROW_NUMBER() 
				OVER (PARTITION BY S.AIMMaterialID, SA.AIMNutrientID
				ORDER BY AIMUsedValue) AS RowNum
			FROM      
				dbo.Sample S WITH(NOLOCK)
				INNER JOIN 
					dbo.SampleAIM SA WITH(NOLOCK)
						ON SA.SampleID = S.SampleID
			WHERE
					S.IsActive = 1
				AND S.SampleDate BETWEEN @StartDate and @EndDate
			) D 
			INNER JOIN
				(/*the values at the quartile points.*/ 
				SELECT 
						S.AIMMaterialID
					, SA.AIMNutrientID
					, FLOOR((COUNT(*) + 3) / 4.0) AS Q1Idx
					, ((COUNT(*) + 3) / 4.0) - FLOOR((COUNT(*) + 3) / 4.0) AS Q1Frac
					, FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Idx
					, ((COUNT(*) * 3 + 1) / 4.0) - FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Frac
				FROM
					dbo.Sample S WITH(NOLOCK)
					INNER JOIN 
						dbo.SampleAIM SA WITH(NOLOCK)
							ON SA.SampleID = S.SampleID 
				WHERE   
						S.IsActive = 1
					AND S.SampleDate BETWEEN @StartDate and @EndDate
				GROUP BY 
						S.AIMMaterialID
					, SA.AIMNutrientID
				HAVING  COUNT(*) > 1
				) AS S 
					ON	D.AIMMaterialID = S.AIMMaterialID 
					AND D.AIMNutrientID = S.AIMNutrientID
		) AS C
	GROUP BY 
		  C.AIMMaterialID
		, C.AIMNutrientID
	) Q
		ON	Q.AIMMaterialID = ST.AIMMaterialID
		AND	Q.AIMNutrientID = ST.AIMNutrientID 
ORDER BY 
	  ST.AIMMaterialID
	, ST.AIMNutrientID

SELECT
		  T.IsActive
		, SR.SampleSize		  
		, T.CFCIngredientName
		, T.CurrentNutrientVal
		, T.SampleValue
		, T.CFCIngredientCode
		, T.Unit
		, T.AIMMaterialID 
		, T.AIMMaterialCode 
		, T.AIMMaterialName 
		, T.AIMNutrientID
		, T.AIMNutrientCode 
		, T.AIMNutrientName 
		, T.MathTypeID
		, T.CFCNutrientCode
		, T.CFCNutrientName
		, T.NumberOfSample
		, ISNULL(SR.Quartile1, T.SampleValue) AS Quartile1 
		, ISNULL(SR.Quartile3, T.SampleValue) AS Quartile3 
		, ISNULL(SR.Mean, T.SampleValue) AS Mean
		, ISNULL(SR.StandardDeviation, 0) AS StandardDeviation
		, ISNULL(SR.Min, T.SampleValue) AS Min
		, ISNULL(SR.Max, T.SampleValue) AS Max
		, ISNULL(SR.Upper95Mean, T.SampleValue) AS Upper95Mean 
		, ISNULL(SR.Lower95Mean, T.SampleValue) AS Lower95Mean 
		, ISNULL(SR.Upper90Mean, T.SampleValue) AS Upper90Mean 
		, ISNULL(SR.Lower90Mean, T.SampleValue) AS Lower90Mean 
		, ISNULL(SR.MeanPlusStandardDeviation, T.SampleValue) AS MeanPlusStandardDeviation 
		, ISNULL(SR.MeanMinusStandardDeviation, T.SampleValue) AS MeanMinusStandardDeviation 
FROM 
	(
	SELECT
			  SA.IsActive
			, CI.CFCIngredientName
			, AVG(CIN.Value) AS CurrentNutrientVal
			, CI.CFCIngredientID
			, CI.CFCIngredientCode
			, CN.Unit
			, S.AIMMaterialID 
			, AM.AIMMaterialCode 
			, AM.AIMMaterialName 
			, AN.AIMNutrientID 
			, AN.AIMNutrientCode 
			, AN.AIMNutrientName 
			, AN.MathTypeID
			, CN.CFCNutrientID
			, CN.CFCNutrientCode
			, CN.CFCNutrientName
			, AVG(SA.AIMUsedValue) AS SampleValue
			, COUNT(*) AS NumberOfSample
	FROM 
		dbo.Sample S WITH(NOLOCK)
		INNER JOIN 
			dbo.SampleAIM SA WITH(NOLOCK)
				ON SA.SampleID = S.SampleID 
		INNER JOIN 
			dbo.AIMMaterial AM WITH(NOLOCK)
				ON	AM.AIMMaterialID = S.AIMMaterialID 
				AND AM.IsActive = 1
		INNER JOIN 
			dbo.AIMNutrient AN WITH(NOLOCK)
				ON	AN.AIMNutrientID = SA.AIMNutrientID  
				AND AN.IsActive = 1
		LEFT OUTER JOIN 
			dbo.CFCNutrientRefAIMNutrient CNRAN 
				ON CNRAN.AIMNutrientID = SA.AIMNutrientID 
		LEFT OUTER JOIN 
			dbo.CFCNutrient CN
				ON CN.CFCNutrientID = CNRAN.CFCNutrientID 
		LEFT OUTER JOIN 
			dbo.CFCIngredientRefAIMMaterial CIRAM
				ON	CIRAM.AIMMaterialID = S.AIMMaterialID 
		LEFT OUTER JOIN 
			dbo.CFCIngredient CI
				ON CI.CFCIngredientID = CIRAM.CFCIngredientID 
		LEFT OUTER JOIN 
			dbo.CFCIngredientNutrient CIN
				ON	CIN.CFCIngredientID = CI.CFCIngredientID 
				AND	CIN.CFCNutrientID = CNRAN.CFCNutrientID 
	WHERE 
			S.IsActive = 1
			AND S.SampleDate BETWEEN @StartDate and @EndDate

	GROUP BY
			  SA.IsActive
			, CI.CFCIngredientName
			, CI.CFCIngredientCode
			, CI.CFCIngredientID
			, S.AIMMaterialID 
			, AM.AIMMaterialCode 
			, AM.AIMMaterialName 
			, AN.AIMNutrientID 
			, AN.AIMNutrientCode 
			, AN.AIMNutrientName
			, AN.MathTypeID 
			, CN.Unit
			, CI.CFCIngredientCode
			, CN.CFCNutrientID
			, CN.CFCNutrientCode
			, CN.CFCNutrientName
		) T
	LEFT OUTER JOIN	
		#Statis SR 
			ON	SR.AIMMaterialID = T.AIMMaterialID
			AND SR.AIMNutrientID = T.AIMNutrientID
ORDER BY 
	  T.AIMMaterialID
	, T.AIMNutrientID 

DROP TABLE #Statis

END
