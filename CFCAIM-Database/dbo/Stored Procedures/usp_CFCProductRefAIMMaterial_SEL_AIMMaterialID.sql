CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_SEL_AIMMaterialID]

@AIMMaterialID INT


--exec [usp_CFCProductRefAIMMaterial_SEL_AIMMaterialID] 5
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 

	-- 3/26/18
	-- CPNP.*, 
	-- CP.*, 
	-- AM.*
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
	
	-- 3/26/18
	, P.CFCPlantCode
	, AOL.LocationCode

	-- 4/12/22 - added when Butterball started AIM	
	, AOL.LocationName
		
FROM 
	dbo.CFCProductRefAIMMaterial CPNP WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.CFCProduct CP
			ON CPNP.CFCProductID = CP.CFCProductID

	--3/26/18
	LEFT OUTER JOIN
		dbo.AIMMaterial AIMM 
			ON CPNP.AIMMaterialID = AIMM.AIMMaterialID

	LEFT OUTER JOIN
		dbo.AnalyzerMaterial AM
			ON CPNP.AIMMaterialID = AM.AnalyzerMaterialID

	-- 3/26/18
	LEFT OUTER JOIN 
		dbo.CFCPlant P
			ON CP.CFCPlantID = P.CFCPlantID

	-- 3/26/18
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AIMM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
				
	
	WHERE CPNP.AIMMaterialID = @AIMMaterialID

END