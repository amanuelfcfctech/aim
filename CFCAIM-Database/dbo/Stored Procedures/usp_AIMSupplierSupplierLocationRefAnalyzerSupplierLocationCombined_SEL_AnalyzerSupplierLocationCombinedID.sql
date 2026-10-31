CREATE PROCEDURE [dbo].[usp_AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined_SEL_AnalyzerSupplierLocationCombinedID]

 @AnalyzerSupplierLocationCombinedID INT
 
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	    XREF.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombinedID
	  , XREF.AIMSupplierLocationID
	  , XREF.AnalyzerSupplierLocationCombinedID
	  , XREF.UpdateStamp	  	  
	  , NL.DataSourceTypeID  	  	  
	  , A.AIMSupplierCode
	  , A.SourceName as AIMSupplierName
	  , AL.AIMSupplierLocationCode	  
	  , AL.SourceLocationName as AIMSupplierLocationName	  
	  , NL.AnalyzerSupplierLocationCombinedName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName
	  , U.UserName
	  
FROM 

	dbo.AIMSupplierSupplierLocationRefAnalyzerSupplierLocationCombined XREF 
	LEFT OUTER JOIN dbo.AIMSourceLocation AL 
			ON XREF.AIMSupplierLocationID = AL.AIMSourceLocationID
	LEFT OUTER JOIN	dbo.AnalyzerSupplierLocationCombined NL	
			ON XREF.AnalyzerSupplierLocationCombinedID = NL.AnalyzerSupplierLocationCombinedID
	LEFT OUTER JOIN dbo.AIMSource A 
			ON AL.AIMSourceID = A.AIMSourceID
	LEFT OUTER JOIN 	CFCUser.CFCUser U 
			ON XREF.CFCUserID = U.CFCUserID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN 	dbo.aspnet_Users AU 
	--		ON AU.UserId = U.UserID

WHERE NL.AnalyzerSupplierLocationCombinedID = @AnalyzerSupplierLocationCombinedID

END