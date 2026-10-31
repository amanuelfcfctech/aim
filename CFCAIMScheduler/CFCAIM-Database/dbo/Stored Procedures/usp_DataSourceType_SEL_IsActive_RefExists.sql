CREATE PROCEDURE [dbo].[usp_DataSourceType_SEL_IsActive_RefExists]
		@IsActive			BIT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_DataSourceType_SEL_IsActive_RefExists true

SELECT 
		  DST.DataSourceTypeID
		, DST.Name
		, DST.IsActive	
		, DST.UpdateStamp		
		, DST.IsUserDefined	
		
FROM 
		dbo.DataSourceType DST WITH(NOLOCK)

		INNER JOIN 
			(Select DataSourceTypeID From dbo.AnalyzerMaterial AM with (nolock)
				Inner Join dbo.AIMMaterialRefAnalyzerMaterial X with (nolock)
				On AM.AnalyzerMaterialID = X.AnalyzerMaterialID
				Group by DataSourceTypeID) X 
			ON DST.DataSourceTypeID = X.DataSourceTypeID
				
WHERE
		DST.IsActive = @IsActive

ORDER BY Name

END