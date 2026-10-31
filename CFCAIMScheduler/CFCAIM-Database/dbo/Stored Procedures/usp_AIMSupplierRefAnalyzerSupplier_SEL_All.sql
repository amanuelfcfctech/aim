CREATE PROCEDURE [dbo].[usp_AIMSupplierRefAnalyzerSupplier_SEL_All]
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMSupplierRefAnalyzerSupplier_SEL_All

SELECT
	    XREF.AIMSupplierRefAnalyzerSupplierID
	  , XREF.AIMSupplierID
	  , XREF.AnalyzerSupplierID
	  , XREF.UpdateStamp	
	  , A.SourceName as AIMSupplierName 	 	
	  , N.AnalyzerSupplierName	  		  
	  , DST.Name as DataSourceTypeName	
	  , N.DataSourceTypeID
	  , A.AIMSupplierCode
	  , N.AnalyzerSupplierCode
	  
	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName	  
	  , U.UserName	  
FROM 
	dbo.AIMSupplierRefAnalyzerSupplier XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMSource A 
			ON XREF.AIMSupplierID = A.AIMSourceID
	LEFT OUTER JOIN	dbo.AnalyzerSupplier N	
			ON XREF.AnalyzerSupplierID = N.AnalyzerSupplierID
	LEFT OUTER JOIN dbo.DataSourceType DST with (nolock)
			ON N.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN 	CFCUser.CFCUser U WITH (NOLOCK) 
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN 	dbo.aspnet_Users AU WITH (NOLOCK)	
	--		ON AU.UserId = U.UserID

END