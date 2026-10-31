CREATE PROCEDURE [dbo].[usp_Report_CFCProductRefAIMMaterial_Orphans]

AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Report_CFCProductRefAIMMaterial_Orphans

SELECT 'AIM' as [Type]
	, m.AimMaterialID as MaterialID
	, m.AimMaterialCode as MaterialCode
	, m.AimMaterialName as MaterialName	
	, case when m.IsActive = 1 then 'Y' else 'N' end as IsActive 
	, '' as DataSourceName
		
	
FROM dbo.AimMaterial m

LEFT JOIN dbo.CFCProductRefAIMMaterial x 
	ON m.AIMMaterialID = x.AIMMaterialID

WHERE x.AIMMaterialID is null and m.MaterialTypeID = 2

--UNION

--SELECT 'Formulation' as [Type]
--	, m.CFCProductID as MaterialID
--	, m.CFCProductCode as MaterialCode
--	, m.CFCProductName as MaterialName	
--	, case when m.IsActive = 1 then 'Y' else 'N' end as IsActive 	

--FROM dbo.CFCProduct m

--LEFT JOIN dbo.CFCProductRefAIMMaterial x 
--	ON m.CFCProductID = x.CFCProductID

--WHERE x.CFCProductID is null




END