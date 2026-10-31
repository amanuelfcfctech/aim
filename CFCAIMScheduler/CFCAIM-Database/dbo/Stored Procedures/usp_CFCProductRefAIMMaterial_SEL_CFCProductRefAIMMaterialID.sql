CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_SEL_CFCProductRefAIMMaterialID]

@CFCProductRefAIMMaterialID INT


--exec usp_CFCProductRefAIMMaterial_SEL_CFCProductRefAIMMaterialID 1593
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 

	  CPNP.CFCProductRefAIMMaterialID
	, CPNP.CFCProductID
	, CPNP.AIMMaterialID
	, CPNP.UpdateStamp
	, CPNP.CFCUserID
	, CP.CFCPlantID
	, CP.CFCProductCode
	, CP.CFCProductName
	, CP.AIMDatabaseID
	, AM.AnalyzerMaterialID
	, AM.DataSourceTypeID
	, AM.AnalyzerMaterialCode
	, AM.AnalyzerMaterialName
	, AM.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID	
	, P.CFCPlantCode
	, AOL.LocationCode

	-- 4/12/22 - added when Butterball started AIM	
	, AOL.LocationName
		
FROM 
	dbo.CFCProductRefAIMMaterial CPNP WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.CFCProduct CP
			ON CPNP.CFCProductID = CP.CFCProductID
	LEFT OUTER JOIN
		dbo.AIMMaterial AIMM 
			ON CPNP.AIMMaterialID = AIMM.AIMMaterialID
	LEFT OUTER JOIN
		dbo.AnalyzerMaterial AM
			ON CPNP.AIMMaterialID = AM.AnalyzerMaterialID
	LEFT OUTER JOIN 
		dbo.CFCPlant P
			ON CP.CFCPlantID = P.CFCPlantID	
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AIMM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
				
	
	WHERE CPNP.CFCProductRefAIMMaterialID = @CFCProductRefAIMMaterialID

END