CREATE PROCEDURE [dbo].[usp_AnalyzerCrossRefAimSupplierLocation_SEL_DataSourceTypeID_AnalyzerSupplierLocationID]
	 @DataSourceTypeID						INT = NULL
	,@AnalyzerSupplierLocationID			INT = NULL
	,@AnalyzerSupplierID					INT = NULL
	

-- 3092, 5
-- exec usp_AnalyzerCrossRefAimSupplierLocation_SEL_DataSourceTypeID_AnalyzerSupplierLocationID 10000, 1, 2

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  XREF.*,
	  ANSL.*	

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	  	  
FROM 
		dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF 
	INNER JOIN 
		dbo.AnalyzerSupplierLocation ANSL
			ON ANSL.AnalyzerSupplierLocationID = XREF.AnalyzerSupplierLocationID	
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE ANSL.DataSourceTypeID = @DataSourceTypeID and 
		XREF.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID and
		XREF.AnalyzerSupplierID	= @AnalyzerSupplierID

ORDER BY ANSL.AnalyzerSupplierLocationName	

END