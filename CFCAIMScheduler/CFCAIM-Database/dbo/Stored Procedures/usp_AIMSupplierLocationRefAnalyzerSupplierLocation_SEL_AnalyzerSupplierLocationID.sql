CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationRefAnalyzerSupplierLocation_SEL_AnalyzerSupplierLocationID]

 @AnalyzerSupplierLocationID INT
 
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	    XREF.AIMSupplierLocationRefAnalyzerSupplierLocationID
	  , XREF.AIMSupplierLocationID
	  , XREF.AnalyzerSupplierLocationID
	  , XREF.UpdateStamp	  	  
	  , NL.DataSourceTypeID  

	  --, AL.SourceLocationName as AIMSupplierLocationName
	  --, NL.AnalyzerSupplierLocationName

	  --5/23/18
	  , A.AIMSupplierCode
	  , A.SourceName as AIMSupplierName
	  , AL.AIMSupplierLocationCode	  
	  , AL.SourceLocationName as AIMSupplierLocationName
	  , N.AnalyzerSupplierCode
	  , N.AnalyzerSupplierName
	  , NL.AnalyzerSupplierLocationCode	  
	  , NL.AnalyzerSupplierLocationName
	  
	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName

FROM 

	dbo.AIMSupplierLocationRefAnalyzerSupplierLocation XREF 
	LEFT OUTER JOIN dbo.AIMSourceLocation AL 
			ON XREF.AIMSupplierLocationID = AL.AIMSourceLocationID
	LEFT OUTER JOIN	dbo.AnalyzerSupplierLocation NL	
			ON XREF.AnalyzerSupplierLocationID = NL.AnalyzerSupplierLocationID
	LEFT OUTER JOIN dbo.AIMSource A 
			ON AL.AIMSourceID = A.AIMSourceID
	LEFT OUTER JOIN	dbo.AnalyzerSupplier N	
			ON NL.AnalyzerSupplierID = N.AnalyzerSupplierID
	LEFT OUTER JOIN 	CFCUser.CFCUser U 
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN 	dbo.aspnet_Users AU 
	--		ON AU.UserId = U.UserID

WHERE NL.AnalyzerSupplierLocationID = @AnalyzerSupplierLocationID

END