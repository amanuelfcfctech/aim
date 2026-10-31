CREATE PROCEDURE [dbo].[usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_SupplierLocationName]
			  @DataSourceTypeID						INT
			, @AnalyzerSupplierLocationName		NVARCHAR(30)
AS
BEGIN
SET NOCOUNT ON;

-- exec [usp_AnalyzerSupplierLocation_SEL_DataSourceTypeID_SupplierLocationName] 12016

SELECT 
	  ANSL.*	
	, DT.Name as DataSourceUserName

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName
	, U.UserName

FROM 
	dbo.AnalyzerSupplierLocation ANSL WITH(NOLOCK)

	LEFT OUTER JOIN
			dbo.DataSourceType DT with (nolock)
				ON ANSL.DataSourceTypeID = DT.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON ANSL.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID
WHERE 
	ANSL.DataSourceTypeID = @DataSourceTypeID
	AND ANSL.AnalyzerSupplierLocationName = @AnalyzerSupplierLocationName

ORDER BY ANSL.AnalyzerSupplierLocationName

END