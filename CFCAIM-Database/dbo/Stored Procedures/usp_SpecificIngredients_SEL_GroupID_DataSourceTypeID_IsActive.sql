CREATE PROCEDURE [dbo].[usp_SpecificIngredients_SEL_GroupID_DataSourceTypeID_IsActive]
		  @SpecificIngredientsGroupID			INT			 	 
		, @DataSourceTypeID						INT			 	 
		, @IsActive								BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT DISTINCT 
	SP.SpecificIngredientsID
	, SP.SpecificIngredientsGroupID
	, AM.AIMMaterialID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AM.AIMOwnerLocationID
	, X.DataSourceTypeID	
	, X.DataSourceTypeName	
	, '''' as AIMLocationName			
	, 0 as AIMOwnerLocationID
	, X.AnalyzerMaterialCode 

FROM
	dbo.SpecificIngredients SP
INNER JOIN 
	dbo.AIMMaterial AM
		ON SP.AIMMaterialID = AM.AIMMaterialID

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
			WHERE AN.DataSourceTypeID IS NOT NULL AND AN.DataSourceTypeID = 1) X

	ON AM.AIMMaterialID = X.AIMMaterialID

WHERE SP.SpecificIngredientsGroupID = @SpecificIngredientsGroupID and 
	  AM.IsActive = @IsActive

ORDER BY AM.AIMMaterialCode

END