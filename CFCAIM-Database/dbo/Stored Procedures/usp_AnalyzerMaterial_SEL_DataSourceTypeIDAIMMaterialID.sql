CREATE PROCEDURE [dbo].[usp_AnalyzerMaterial_SEL_DataSourceTypeIDAIMMaterialID]
			  @DataSourceTypeID			INT
			, @AIMMaterialID			INT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  AM.AnalyzerMaterialID
	, AM.DataSourceTypeID
	, AM.AnalyzerMaterialCode
	, AM.AnalyzerMaterialName
	, AM.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID
	
	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.AnalyzerMaterial AM WITH(NOLOCK)
	INNER JOIN 
		dbo.AIMMaterialRefAnalyzerMaterial AMRAM WITH(NOLOCK)
			ON AMRAM.AnalyzerMaterialID = AM.AnalyzerMaterialID 
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
		AM.DataSourceTypeID = @DataSourceTypeID
	AND AMRAM.AIMMaterialID = @AIMMaterialID
END
