CREATE PROCEDURE [dbo].[usp_AnalyzerMaterial_SEL_DataSourceTypeID_IsActive]
			@DataSourceTypeID			INT
			,@IsActive					BIT
			

--exec usp_AnalyzerMaterial_SEL_DataSourceTypeID_IsActive 2, true

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
	, DT.Name as DataSourceName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName
	
FROM 
	dbo.AnalyzerMaterial NI WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON NI.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN
			dbo.DataSourceType DT with (nolock)
				ON NI.DataSourceTypeID = DT.DataSourceTypeID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
		NI.DataSourceTypeID = @DataSourceTypeID
		and NI.IsActive = @IsActive 

ORDER BY NI.AnalyzerMaterialCode

END
