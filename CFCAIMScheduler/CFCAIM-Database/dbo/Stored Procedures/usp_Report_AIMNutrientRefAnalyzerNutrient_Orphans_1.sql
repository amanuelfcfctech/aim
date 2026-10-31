CREATE PROCEDURE [dbo].[usp_Report_AIMNutrientRefAnalyzerNutrient_Orphans]

AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Report_AIMNutrientRefAnalyzerNutrient_Orphans


SELECT 'AIM' as [Type]
	, n.AIMNutrientID as NutrientID
	, n.AIMNutrientCode as NutrientCode
	, n.AIMNutrientName as NutrientName
	, u.UnitTypeDescription as Units
	, d.Name as DataSourceName
	, case when n.IsActive = 1 then 'Y' else 'N' end as IsActive 

FROM dbo.AIMNutrient n

LEFT JOIN dbo.AIMNutrientRefAnalyzerNutrient x 
	ON n.AIMNutrientID = x.AIMNutrientID
LEFT JOIN dbo.UnitType u 
	ON n.UnitTypeID = u.UnitTypeID 
LEFT JOIN dbo.DataSourceType d 
	ON d.DataSourceTypeID = n.DataSourceTypeID 

WHERE x.AIMNutrientID is null

UNION

SELECT 'Analyzer' as [Type]
	, n.AnalyzerNutrientID as NutrientID
	, n.AnalyzerNutrientCode as NutrientCode
	, n.AnalyzerNutrientName as NutrientName
	, n.UnitType as Units
	, d.Name as DataSourceName
	, case when n.IsActive = 1 then 'Y' else 'N' end as IsActive 

FROM dbo.AnalyzerNutrient n

LEFT JOIN dbo.AIMNutrientRefAnalyzerNutrient x 
	ON n.AnalyzerNutrientID = x.AnalyzerNutrientID
LEFT JOIN dbo.DataSourceType d 
	ON d.DataSourceTypeID = n.DataSourceTypeID 

WHERE x.AnalyzerNutrientID is null




END