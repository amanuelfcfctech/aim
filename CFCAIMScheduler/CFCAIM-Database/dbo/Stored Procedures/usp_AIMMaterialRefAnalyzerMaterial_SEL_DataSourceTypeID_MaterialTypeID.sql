CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID]
	  @DataSourceTypeID INT	
	  ,@MaterialTypeID	INT

AS
BEGIN
SET NOCOUNT ON;


SELECT 
	    XREF.AIMMaterialRefAnalyzerMaterialID
	  , XREF.AIMMaterialID
	  , XREF.AnalyzerMaterialID
	  , XREF.UpdateStamp
	  , A.AIMMaterialCode
	  , A.AIMMaterialName	  
	  , M.AnalyzerMaterialCode
	  , M.AnalyzerMaterialName	 
	  , M.DataSourceTypeID 	  	  
	  , DST.Name as DataSourceTypeName
	  , IsNull(AOL.LocationName, '') as LocationName
	  , U.UserName	  
	  
	  
FROM 
	dbo.AIMMaterialRefAnalyzerMaterial XREF WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMMaterial A 
			ON XREF.AIMMaterialID = A.AIMMaterialID
	LEFT OUTER JOIN	dbo.AnalyzerMaterial M	
			ON XREF.AnalyzerMaterialID = M.AnalyzerMaterialID
	LEFT OUTER JOIN dbo.DataSourceType DST with (nolock)
			ON M.DataSourceTypeID = DST.DataSourceTypeID	
	LEFT OUTER JOIN 	CFCUser.CFCUser U WITH (NOLOCK) 
			ON XREF.CFCUserID = U.CFCUserID	
	LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL with (nolock)
			ON A.AIMOwnerLocationID = AOL.AIMOwnerLocationID

WHERE M.DataSourceTypeID = @DataSourceTypeID 
	AND A.AIMMaterialCode is not null
	AND A.MaterialTypeID = @MaterialTypeID


END