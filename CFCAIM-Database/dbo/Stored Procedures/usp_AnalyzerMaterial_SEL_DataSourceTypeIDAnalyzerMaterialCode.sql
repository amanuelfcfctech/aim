CREATE PROCEDURE [dbo].[usp_AnalyzerMaterial_SEL_DataSourceTypeIDAnalyzerMaterialCode]
			  @DataSourceTypeID			INT
			, @AnalyzerMaterialCode		NVARCHAR(20)
AS
BEGIN
SET NOCOUNT ON;


-- exec usp_AnalyzerMaterial_SEL_DataSourceTypeIDAnalyzerMaterialCode 9, '01'

SELECT TOP 1 
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
	dbo.AnalyzerMaterial AM 
	
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON AM.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU 
	--		ON AU.UserId = U.UserID
WHERE 
		AM.DataSourceTypeID = @DataSourceTypeID
	AND AM.AnalyzerMaterialCode = @AnalyzerMaterialCode
	AND AM.IsActive = 1

END