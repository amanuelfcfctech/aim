CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID_AIMOwnerLocationID_For_Utility]
	    @DataSourceTypeID			INT			 	 
		, @MaterialTypeID			INT
		, @AIMOwnerLocationID		INT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL VARCHAR(5000) = ''

--exec usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID_AIMOwnerLocationID_For_Utility 0, 1, 0 


IF @MaterialTypeID = 0 or @MaterialTypeID = 1
Begin

	---------------------------------- INGREDIENTS ---------------------------------
	SET @SQL = '
	
	SELECT DISTINCT 
		AMM.AIMMaterialID	  
		, AMM.AIMMaterialCode
		, AMM.AIMMaterialName
		, AMM.AIMOwnerLocationID
		, X.DataSourceTypeID
		, AMM.MaterialTypeID 	  
		, X.DataSourceTypeName	
		, '''' as AIMLocationName			
		, 0 as AIMOwnerLocationID
		, X.AnalyzerMaterialCode 
		, X.AnalyzerMaterialID
		
	
	FROM dbo.aimMaterial AMM with (nolock)
	LEFT OUTER JOIN
			(SELECT 
				AN.*, X.AIMMaterialID, DST.Name as DataSourceTypeName 
			FROM  
				dbo.AIMMaterialRefAnalyzerMaterial X with (nolock) 
			LEFT OUTER JOIN 
				dbo.AnalyzerMaterial AN with (nolock) 		
					on AN.AnalyzerMaterialID = x.AnalyzerMaterialID 
			LEFT OUTER JOIN
				dbo.DataSourceType DST 
					ON AN.DataSourceTypeID = DST.DataSourceTypeID
			WHERE AN.DataSourceTypeID IS NOT NULL '

			if @DataSourceTypeID <> 0
				SET @SQL = @SQL + ' AND AN.DataSourceTypeID = ' + CAST(@DataSourceTypeID AS VARCHAR) 

			SET @SQL = @SQL + '
			) X
		ON AMM.AIMMaterialID = X.AIMMaterialID
	WHERE AMM.MaterialTypeID = 1 and X.DataSourceTypeID IS NOT NULL
	
	'
End

	
SET @SQL = @SQL + ' ORDER BY AIMMaterialCode '
 
--PRINT @SQL
EXEC (@SQL)

END