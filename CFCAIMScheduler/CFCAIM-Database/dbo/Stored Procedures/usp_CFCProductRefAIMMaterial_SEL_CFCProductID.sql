CREATE PROCEDURE [dbo].[usp_CFCProductRefAIMMaterial_SEL_CFCProductID]

@CFCProductID INT

AS
BEGIN
SET NOCOUNT ON;

SELECT

	--3/26/18
	--  CPNP.*
	--, CP.*
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

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

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
	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CPNP.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

	-- 3/26/18
	LEFT OUTER JOIN 
		dbo.CFCPlant P
			ON CP.CFCPlantID = P.CFCPlantID

	-- 3/26/18
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AIMM.AIMOwnerLocationID = AOL.AIMOwnerLocationID

	WHERE CP.CFCProductID = @CFCProductID

END