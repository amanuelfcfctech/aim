CREATE PROCEDURE [dbo].[usp_Report_CFCIngredientRefAIMMaterial_Orphans]

AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Report_CFCIngredientRefAIMMaterial_Orphans

SELECT 'AIM' as [Type]
	, m.AimMaterialID as MaterialID
	, m.AimMaterialCode as MaterialCode
	, m.AimMaterialName as MaterialName	
	, case when m.IsActive = 1 then 'Y' else 'N' end as IsActive 
	, '' as DataSourceName
		
	
FROM dbo.AimMaterial m

LEFT JOIN dbo.CFCIngredientRefAIMMaterial x 
	ON m.AIMMaterialID = x.AIMMaterialID

WHERE x.AIMMaterialID is null and m.MaterialTypeID = 1

--UNION

--SELECT 'Formulation' as [Type]
--	, m.CFCIngredientID as MaterialID
--	, m.CFCIngredientCode as MaterialCode
--	, m.CFCIngredientName as MaterialName	
--	, case when m.IsActive = 1 then 'Y' else 'N' end as IsActive 	

--FROM dbo.CFCIngredient m

--LEFT JOIN dbo.CFCIngredientRefAIMMaterial x 
--	ON m.CFCIngredientID = x.CFCIngredientID

--WHERE x.CFCIngredientID is null




END