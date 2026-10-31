CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_SEL]

AS
BEGIN

SELECT
	 -- 1/10/22 - removed AnalyzerMaterial fields
	 --CPNP.*
	 --, CP.*
	 --, AM.AnalyzerMaterialID
	 --, AM.DataSourceTypeID
	 --, AM.AnalyzerMaterialCode
	 --, AM.AnalyzerMaterialName
	 --, AM.IsActive
	 --, AM.UpdateStamp
	 --, AM.CFCUserID	
	 CPNP.CFCProductRefAIMMaterialID
	 , CPNP.CFCProductID
	 , CPNP.AIMMaterialID
	 , CPNP.UpdateStamp
	 , CPNP.CFCUserID
	 , CP.CFCProductCode
	 , CP.CFCProductName
	 , AIMM.AIMMaterialCode
	 , AIMM.AIMMaterialName
	 , AIMM.AIMOwnerLocationID	
	 , AIMM.MaterialTypeID
	
	, P.CFCPlantCode
	, AOL.LocationCode
	, U.UserName

	-- 4/12/22 - added when Butterball started AIM
	, P.CFCPlantName
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
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CPNP.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN 
		dbo.CFCPlant P
			ON CP.CFCPlantID = P.CFCPlantID
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AIMM.AIMOwnerLocationID = AOL.AIMOwnerLocationID			

WHERE
	AIMM.MaterialTypeID = 2
	
	--1/10/22 - commented
	--LEFT OUTER JOIN
	--	dbo.AnalyzerMaterial AM
	--		ON CPNP.AIMMaterialID = AM.AnalyzerMaterialID

END
