CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_DistinctMaterial_DataSourceTypeID_AIMMaterialTypeID_AIMOwnerLocationID]
	    @DataSourceTypeID INT	
	  , @MaterialTypeID	INT
	  , @AimOwnerLocationID INT

AS
BEGIN
SET NOCOUNT ON;

--exec [usp_AIMMaterialRefAnalyzerMaterial_SEL_DistinctMaterial_DataSourceTypeID] 2

SELECT DISTINCT
	  	  
	   XREF.AIMMaterialID	  
	  , A.AIMMaterialCode
	  , A.AIMMaterialName
	  , M.DataSourceTypeID 	  

	  -- these are on the entity so had to include them here (?)
	  , 0 as AIMMaterialRefAnalyzerMaterialID
	  , 0 as AnalyzerMaterialID
	  , GETDATE() AS UpdateStamp	  
	  , 0 as AnalyzerMaterialCode
	  , '' as AnalyzerMaterialName	 	  
	  , '' as UserName	  
	  , '' as DataSourceTypeName 


	  
FROM 
	dbo.AIMMaterial A 
	LEFT OUTER JOIN Dbo.AIMMaterialRefAnalyzerMaterial XREF WITH(NOLOCK)	
			ON XREF.AIMMaterialID = A.AIMMaterialID
	LEFT OUTER JOIN	dbo.AnalyzerMaterial M	
			ON XREF.AnalyzerMaterialID = M.AnalyzerMaterialID
	

WHERE M.DataSourceTypeID = @DataSourceTypeID	
		and A.MaterialTypeID = @MaterialTypeID
		and A.AIMOwnerLocationID = @AimOwnerLocationID
		

ORDER BY A.AIMMaterialCode

END