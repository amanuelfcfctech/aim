CREATE PROCEDURE [dbo].[usp_AnalyzerCrossRefAimSupplier_SEL_DataSourceTypeID_AnalyzerSupplierID]
	 @DataSourceTypeID				INT = NULL
	,@AnalyzerSupplierID			INT = NULL

-- exec usp_AnalyzerCrossRefAimSupplier_SEL_DataSourceTypeID_AnalyzerSupplierID 13008, 2151

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  XREF.*,
	  ANS.*	

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	  	  
FROM 

	dbo.AnalyzerSupplier ANS
	LEFT OUTER JOIN 
		dbo.AIMSupplierRefAnalyzerSupplier XREF 
			on ANS.AnalyzerSupplierID = XREF.AnalyzerSupplierID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN
	--	dbo.aspnet_Users AU WITH(NOLOCK)
	--		ON AU.UserId = U.UserID

WHERE ANS.DataSourceTypeID = @DataSourceTypeID and XREF.AnalyzerSupplierID = @AnalyzerSupplierID

ORDER BY ANS.AnalyzerSupplierName	

END