CREATE PROCEDURE [dbo].[usp_Sample_SEL_Chart_SampleList]
						  @StartDate			NVARCHAR(20)	= NULL
						, @EndDate				NVARCHAR(20)	= NULL
						, @AIMMaterialID		INT			= NULL
						, @AIMNutrientID		INT			= NULL
						, @AIMSupplierID			INT			= NULL
						, @AIMSupplierLocationID	INT			= NULL
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Sample_SEL_Chart_SampleList '2020-01-13', '2020-02-13', 1, 2, 2, 0

DECLARE @SQL			VARCHAR(8000) = ''

SET @SQL = 'SELECT  
	 
	 S.Sampleid
	 , S.SampleCode
	 , S.SampleDate 
	 , AM.AIMMaterialCode
	 , AM.AIMMaterialName
	 , AN.AIMNutrientCode
	 , AN.AIMNutrientName
	 , sum(SR.ResultValue) AS ResultValue
	 
FROM dbo.Sample S
                        
INNER JOIN 
	dbo.SampleResult SR 
		on S.SampleID = SR.SampleID
INNER JOIN
	dbo.AIMMaterial AM
		on S.AIMMaterialID = AM.AIMMaterialID
INNER JOIN 
	dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)
			ON SR.AnalyzerNutrientID = x.AnalyzerNutrientID
INNER JOIN 
		dbo.AIMNutrient AN with(nolock)
			ON x.AIMNutrientID = AN.AIMNutrientID 
'
						
SET @SQL += 'WHERE 
	S.SampleDate >= ''' + convert(varchar, @StartDate) + '''' + 
	' and S.SampleDate <= ''' + convert(varchar, @EndDate) + '''' + 
	' and S.AIMMaterialID = ' + convert(varchar, @AIMMaterialID) + 
	' and X.AIMNutrientID = ' + convert(varchar, @AIMNutrientID) + 
	' and SR.IsActive = 1 '

If @AIMSupplierID <> 0
	SET @SQL += ' and S.AIMSourceID = ' + convert(varchar, @AIMSupplierID) 

If @AIMSupplierLocationID <> 0
	SET @SQL += ' and S.AIMSourceLocationID = ' + convert(varchar, @AIMSupplierLocationID)                         
						
SET @SQL += ' 
	
GROUP BY 
	   S.Sampleid
	 , S.SampleCode
	 , S.SampleDate 
	 , AM.AIMMaterialCode
	 , AM.AIMMaterialName
	 , AN.AIMNutrientCode
	 , AN.AIMNutrientName '

EXEC (@SQL)
--print @sql


END