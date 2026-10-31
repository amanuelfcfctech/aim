
CREATE VIEW [dbo].[vwSampleResult]
AS

SELECT 
C.IsActive
, C.AIMMaterialID
, C.AIMNutrientID
, ROUND((Max(Q1NextVal) - MAX(Q1Val)) * Max(Q1Frac) + Max(Q1Val), 4) AS Quartile1
, ROUND((Max(Q3NextVal) - MAX(Q3Val)) * Max(Q3Frac) + Max(Q3Val), 4) AS Quartile3
, ROUND(AVG(AIMUsedValue), 4) AS Mean
, ROUND(STDEV(AIMUsedValue), 4) AS StandardDeviation
, MIN(AIMUsedValue) AS Min
, MAX(AIMUsedValue) AS Max
, ROUND(AVG(AIMUsedValue) + (1.96 * STDEV(AIMUsedValue) / SQRT(COUNT(*))), 4) AS Upper95Mean
, ROUND(AVG(AIMUsedValue) - (1.96 * STDEV(AIMUsedValue) / SQRT(COUNT(*))), 4) AS Lower95Mean
, ROUND(AVG(AIMUsedValue) + (1.645 * STDEV(AIMUsedValue) / SQRT(COUNT(*))), 4) AS Upper90Mean
, ROUND(AVG(AIMUsedValue) - (1.645 * STDEV(AIMUsedValue) / SQRT(COUNT(*))), 4) AS Lower90Mean
, ROUND(AVG(AIMUsedValue) + .5 * STDEV(AIMUsedValue), 4) AS MeanPlusStandardDeviation
, ROUND(AVG(AIMUsedValue) - .5 * STDEV(AIMUsedValue), 4) AS MeanMinusStandardDeviation
FROM     
	(SELECT 
		  D.IsActive
		, D.AIMMaterialID
		, D.AIMNutrientID
		, CASE WHEN RowNum = Q1Idx THEN AIMUsedValue ELSE NULL END AS Q1Val
		, CASE WHEN RowNum = Q1Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q1NextVal
		, CASE WHEN RowNum = Q3Idx THEN AIMUsedValue ELSE NULL END AS Q3Val
		, CASE WHEN RowNum = Q3Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q3NextVal
		, Q1Frac
		, Q3Frac
		, AIMUsedValue
	FROM      /*Calculate a row number sorted by measure for each group.*/ 
	(SELECT 
		  SA.AIMUsedValue 
		, SA.IsActive 
		, S.AIMMaterialID 
		, SA.AIMNutrientID 
		, ROW_NUMBER() 
		OVER (PARTITION BY SA.IsActive, S.AIMMaterialID, SA.AIMNutrientID
		ORDER BY AIMUsedValue) AS RowNum
	FROM      
		dbo.Sample S WITH(NOLOCK)
		INNER JOIN 
			dbo.SampleAIM SA WITH(NOLOCK)
				ON SA.SampleID = S.SampleID) D 
		INNER JOIN
                      (/*the values at the quartile points.*/ 
			SELECT 
				  SA.IsActive
				, S.AIMMaterialID
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
            GROUP BY 
				  SA.IsActive
				, S.AIMMaterialID
				, SA.AIMNutrientID
			HAVING  COUNT(*) > 1) AS S 
				ON	D.IsActive = S.IsActive 
				AND D.AIMMaterialID = S.AIMMaterialID 
				AND D.AIMNutrientID = S.AIMNutrientID) AS C
GROUP BY 
	  C.IsActive
	, C.AIMMaterialID
	, C.AIMNutrientID


