CREATE PROCEDURE [dbo].[usp_SampleResultTemplateDetailGroup_SEL_SampleResultTemplateID]
	  @SampleResultTemplateID INT
AS
BEGIN

	SELECT 
				STG.*	
			, ANX.AnalyzerNutrientCode
			, ANX.AnalyzerNutrientName
			, ANX.AnalyzerNutrientID
			, ANX.UnitType
			, AIMN.AIMNutrientName	
			, AIMN.AIMNutrientID
			, 0 AS CurrentValue
			, U.UserName
			, SortOrder
			, NGD.NutrientGroupDetailID
		FROM 
			SampleResultTemplateDetailGroup STG
			INNER JOIN
				NutrientGroup NG
					ON NG.NutrientGroupID = STG.NutrientGroupID
			INNER JOIN
				NutrientGroupDetail NGD
					ON NG.NutrientGroupID = NGD.NutrientGroupID
			INNER JOIN
				SampleResultTemplate ST
					ON ST.SampleResultTemplateID = STG.SampleResultTemplateID
			INNER JOIN
				dbo.AIMNutrient AIMN
					ON NGD.AIMNutrientID = AIMN.AIMNutrientID	
			LEFT OUTER JOIN
				(SELECT 
						ANX.AnalyzerNutrientID 
						,ANX.AIMNutrientID
						,AN.AnalyzerNutrientCode
						,AN.AnalyzerNutrientName
						,AN.UnitType
						,AN.DataSourceTypeID
					FROM
						dbo.AIMNutrientRefAnalyzerNutrient ANX
					INNER JOIN
						dbo.AnalyzerNutrient AN
						ON ANX.AnalyzerNutrientID = AN.AnalyzerNutrientID
					) ANX
					ON ANX.AIMNutrientID = NGD.AIMNutrientID
					AND ANX.DataSourceTypeID = ST.DataSourceTypeID
			LEFT OUTER JOIN
				CFCUser.CFCUser U 
					ON STG.CFCUserID = U.CFCUserID
		WHERE
			STG.SampleResultTemplateID = @SampleResultTemplateID
		ORDER BY
				CASE WHEN SortOrder = 0 THEN 1 ELSE 0 END,
						SortOrder
					, NGD.NutrientGroupDetailID


END