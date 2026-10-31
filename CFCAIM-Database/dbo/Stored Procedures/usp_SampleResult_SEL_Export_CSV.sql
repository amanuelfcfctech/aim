CREATE PROCEDURE [dbo].[usp_SampleResult_SEL_Export_CSV]			
	 @ExportTemplateID		INT	
		
--exec usp_SampleResult_SEL_Export_CSV 3

AS
BEGIN
SET NOCOUNT ON;
	

DECLARE @SQL	VARCHAR(2000) = ''
DECLARE @Where	VARCHAR(500) = '' 
	
EXEC @Where = [dbo].[AnalysisReports_WhereStatement] @ExportTemplateID 	

SET @Where = ' WHERE S.IsActive = 1 ' + @Where 	
		
SET @SQL = '
	SELECT S.SampleID		
		, SR.SampleResultID as SampleExportDetailID
		, SR.AnalyzerNutrientCode
		, SR.ResultValue
		, SR.Unit
		   
	FROM  
		dbo.Sample S WITH(NOLOCK) 
	INNER JOIN
		dbo.SampleResult SR with (nolock)
			ON S.SampleID = SR.SampleID	'
				
	SET @SQL += @Where
	
	EXEC(@SQL)
	--print @Sql
	

 	
	
END