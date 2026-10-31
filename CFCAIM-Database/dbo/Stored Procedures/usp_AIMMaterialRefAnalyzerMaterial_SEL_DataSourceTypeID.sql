CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID]
	  @DataSourceTypeID INT	

AS
BEGIN
SET NOCOUNT ON;


--exec usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID 1

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

	  --3/8/18
	  , IsNull(AOL.LocationName, '') as LocationName

	  -- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	  --, AU.UserName	  
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

	--3/8/18
	LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL with (nolock)
			ON A.AIMOwnerLocationID = AOL.AIMOwnerLocationID

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--LEFT OUTER JOIN 	dbo.aspnet_Users AU WITH (NOLOCK)	
	--		ON AU.UserId = U.UserID

WHERE M.DataSourceTypeID = @DataSourceTypeID 

	--3/8/18
	AND A.AIMMaterialCode is not null


END