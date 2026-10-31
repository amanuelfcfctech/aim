CREATE PROCEDURE [dbo].[usp_AnalyzerMaterial_SEL_MaterialID_DataSourceTypeID_IsActive]
			@AnalyzerMaterialID			INT
			, @DataSourceTypeID			INT
			, @IsActive					BIT
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  NI.AnalyzerMaterialID
	, NI.DataSourceTypeID
	, NI.AnalyzerMaterialCode
	, NI.AnalyzerMaterialName
	, NI.IsActive
	, NI.UpdateStamp
	, NI.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
FROM 
	dbo.AnalyzerMaterial NI WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON NI.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
		NI.AnalyzerMaterialID = @AnalyzerMaterialID
		and NI.DataSourceTypeID = @DataSourceTypeID
		and NI.IsActive = @IsActive
END