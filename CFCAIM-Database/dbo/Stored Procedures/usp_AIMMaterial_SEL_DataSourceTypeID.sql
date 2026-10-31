CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_DataSourceTypeID]
				@DataSourceTypeID				INT
AS
BEGIN
SET NOCOUNT ON

SELECT 
	  AM.AIMMaterialRefAnalyzerMaterialID
	, AMM.AIMMaterialID
	, AMM.MaterialTypeID
	, AMM.AIMMaterialCode
	, AMM.AIMMaterialName
	, AMM.IsActive
	, AMM.UpdateStamp
	, AMM.CFCUserID	
	, MT.MaterialTypeName
	, AM.AnalyzerMaterialID 
	, AM.AnalyzerMaterialCode 
	, AM.AnalyzerMaterialName 

	-- 1/11/21 - changed to remove aspnet_User table
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AIMMaterial AMM WITH(NOLOCK)
	INNER JOIN 
		dbo.MaterialType MT WITH(NOLOCK)
			ON MT.MaterialTypeID = AMM.MaterialTypeID 
	LEFT OUTER JOIN 
		(SELECT 	
			  AMRAM.AIMMaterialRefAnalyzerMaterialID 
			, AMRAM.AIMMaterialID 
			, AM.AnalyzerMaterialID 
			, AM.AnalyzerMaterialCode 
			, AM.AnalyzerMaterialName 
		FROM
			dbo.AIMMaterialRefAnalyzerMaterial AMRAM WITH(NOLOCK)			
		INNER JOIN 
			dbo.AnalyzerMaterial AM WITH(NOLOCK)
				ON	AM.AnalyzerMaterialID = AMRAM.AnalyzerMaterialID 
				AND AM.DataSourceTypeID = @DataSourceTypeID
				AND AM.IsActive = 1) AM
			ON AMM.AIMMaterialID = AM.AIMMaterialID 
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AMM.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE 
		AMM.IsActive = 1

ORDER BY 
	AMM.AIMMaterialName

END
