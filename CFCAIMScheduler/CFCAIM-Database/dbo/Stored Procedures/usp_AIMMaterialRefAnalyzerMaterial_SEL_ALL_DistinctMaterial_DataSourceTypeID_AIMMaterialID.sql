CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_ALL_DistinctMaterial_DataSourceTypeID_AIMMaterialID]
	      @DataSourceTypeID INT		  
		, @MaterialTypeID	INT

AS
BEGIN
SET NOCOUNT ON;

--exec [usp_AIMMaterialRefAnalyzerMaterial_SEL_ALL_DistinctMaterial_DataSourceTypeID] 1

SELECT DISTINCT 

	AMM.AIMMaterialID	  
	, AMM.AIMMaterialCode
	, AMM.AIMMaterialName
	, X.DataSourceTypeID 	  

	-- these are on the entity so had to include them here (?)
	, 0 as AIMMaterialRefAnalyzerMaterialID
	, 0 as AnalyzerMaterialID
	, GETDATE() AS UpdateStamp	  
	, 0 as AnalyzerMaterialCode
	, '' as AnalyzerMaterialName	 	  
	, '' as UserName	  
	, '' as DataSourceTypeName 


FROM dbo.aimMaterial AMM with (nolock)
LEFT OUTER JOIN
		(SELECT 
			am.*, x.aimmaterialid 
		FROM  
			dbo.AIMMaterialRefAnalyzerMaterial X with (nolock) 
		LEFT OUTER JOIN 
			dbo.AnalyzerMaterial AM with (nolock) 		
				on AM.AnalyzerMaterialID = x.AnalyzerMaterialID
		WHERE AM.DataSourceTypeID = @DataSourceTypeID) X

	ON AMM.AIMMaterialID = X.AIMMaterialID

WHERE AMM.MaterialTypeID = @MaterialTypeID

ORDER BY AIMMaterialCode



END