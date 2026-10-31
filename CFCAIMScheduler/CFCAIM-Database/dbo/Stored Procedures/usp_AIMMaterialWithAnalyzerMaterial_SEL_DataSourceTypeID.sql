CREATE PROCEDURE [dbo].[usp_AIMMaterialWithAnalyzerMaterial_SEL_DataSourceTypeID]
				@DataSourceTypeID				INT = NULL		 

--exec usp_AIMMaterialWithAnalyzerMaterial_SEL_DataSourceTypeID 13008

AS
BEGIN
SET NOCOUNT ON

IF	@DataSourceTypeID IS NULL
	SET @DataSourceTypeID = 0

SELECT
	 XREF.AIMMaterialRefAnalyzerMaterialID
	, AMM.AIMMaterialID
	, AMM.MaterialTypeID
	, AMM.AIMMaterialCode
	, AMM.AIMMaterialName
	, AMM.IsActive
	, AMM.UpdateStamp
	, AMM.CFCUserID	
	, MT.MaterialTypeName
	, AN.AnalyzerMaterialID 
	, AN.AnalyzerMaterialCode 
	, AN.AnalyzerMaterialName 
	, AN.DataSourceTypeID
	, DT.Name as DataSourceTypeName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	  
FROM 
	dbo.AIMMaterialRefAnalyzerMaterial XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMMaterial AMM
			ON XREF.AIMMaterialID = AMM.AIMMaterialID
	LEFT OUTER JOIN	dbo.AnalyzerMaterial AN
			ON XREF.AnalyzerMaterialID = AN.AnalyzerMaterialID
	LEFT OUTER JOIN dbo.DataSourceType DT
			ON AN.DataSourceTypeID = DT.DataSourceTypeID
	LEFT OUTER JOIN dbo.MaterialType MT 
			ON AMM.MaterialTypeID = MT.MaterialTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE AN.DataSourceTypeID = @DataSourceTypeID

ORDER BY AN.AnalyzerMaterialName	

END