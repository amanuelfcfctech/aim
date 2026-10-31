CREATE PROCEDURE [dbo].[usp_Report_AIMMaterialRefAnalyzerMaterial_Orphans]

AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Report_AIMMaterialRefAnalyzerMaterial_Orphans


SELECT 'AIM' as [Type]
	, m.AimMaterialID as MaterialID
	, m.AimMaterialCode as MaterialCode
	, m.AimMaterialName as MaterialName
	, NULL as DataSourceName
	, case when m.IsActive = 1 then 'Y' else 'N' end as IsActive 
		
	
FROM dbo.AimMaterial m

LEFT JOIN dbo.AimMaterialRefAnalyzerMaterial x 
	ON m.AIMMaterialID = x.AIMMaterialID

WHERE x.AIMMaterialID is null and m.MaterialTypeID = 1

UNION

SELECT 'Analyzer' as [Type]
	, m.AnalyzerMaterialID as MaterialID
	, m.AnalyzerMaterialCode as MaterialCode
	, m.AnalyzerMaterialName as MaterialName
	, d.Name as DataSourceName
	, case when m.IsActive = 1 then 'Y' else 'N' end as IsActive 
	

FROM dbo.AnalyzerMaterial m

LEFT JOIN dbo.AimMaterialRefAnalyzerMaterial x 
	ON m.AnalyzerMaterialID = x.AnalyzerMaterialID
LEFT JOIN dbo.DataSourceType d 
	ON d.DataSourceTypeID = m.DataSourceTypeID 

WHERE x.AnalyzerMaterialID is null




END