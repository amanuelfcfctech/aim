CREATE PROCEDURE [dbo].[usp_Sample_SEL_Export]	
	
	  @SampleCodeBeg			NVARCHAR(20) = NULL
	, @SampleCodeEnd			NVARCHAR(20) = NULL
	, @DateBeg					NVARCHAR(23) = NULL
	, @DateEnd					NVARCHAR(23) = NULL

	

--exec [usp_Sample_SEL_Export] '7700', '7776'
--exec [usp_Sample_SEL_Export] '', '', '2017-10-03', '2017-10-03'
		
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL	VARCHAR(2000) = ''
DECLARE @Where	VARCHAR(500) = '' 
DECLARE @InnerJoin VARCHAR(500) = ''
DECLARE @WhereDate VARCHAR(500) = ''
DECLARE @Fieldname VARCHAR(50) = ''
DECLARE @SearchTable VARCHAR(25) = ''
DECLARE @SearchField VARCHAR(25) = ''

Declare @SqlSampleCodeBegin varchar(100)
Declare @SqlSampleCodeEnd   varchar(100)



Begin

	If len(@SampleCodeBeg) > 0 AND len(@SampleCodeEnd) > 0 
		SET @Where = '(S.SampleCode >= ''' + @SampleCodeBeg + ''' AND S.SampleCode <= ''' + @SampleCodeEnd + ''')'	
	Else If len(@SampleCodeBeg) > 0 
		--SET @Where = '(S.SampleCode LIKE ''%' + @SampleCodeBeg + '%'')'
		SET @Where = '(S.SampleCode >= ''' + @SampleCodeBeg + ''')'
	Else If len(@SampleCodeEnd) > 0 
		--SET @Where = '(S.SampleCode LIKE ''%' + @SampleCodeBeg + '%'')'
		SET @Where = '(S.SampleCode <= ''' + @SampleCodeEnd + ''')'
		
		
	If Len(@DateBeg) > 0 AND len(@DateEnd) > 0	
		SET @WhereDate = '(SampleDate >= Cast(''' + @DateBeg + ''' as date) AND SampleDate <= Cast(''' + @DateEnd + ''' as date) )'	
	Else if Len(@DateBeg) > 0
		SET @WhereDate = '(SampleDate >= Cast(''' + @DateBeg + ''' as date) )'	
	Else if Len(@DateEnd) > 0
		SET @WhereDate = '(SampleDate <= Cast(''' + @DateEnd + ''' as date) )'				

	if Len(@Where) > 0 
		SET @Where = ' WHERE ' + @Where 

	if Len(@Where) > 0 and Len(@WhereDate) > 0
		SET @Where = @Where + ' AND ' + @WhereDate

	if Len(@Where) = 0 and Len(@WhereDate) > 0
		SET @Where = ' WHERE ' + @WhereDate 
		
SET @SQL = '
	SELECT 
		S.SampleID, 0 as SampleExportDetailID
		, CASE WHEN S.SampleCode IS NULL or len(S.SampleCode) = 0 then	
			convert(varchar, S.SampleID) else S.SampleCode End as SampleCode      		
		,S.SampleDate
		, MT.MaterialTypeName as MaterialType
		, MT.MaterialTypeID 
		, AM.AIMMaterialCode as MaterialCode
		, AM.AIMMaterialName as MaterialName
		  
	FROM 
		dbo.Sample S WITH(NOLOCK) 
	INNER JOIN
		dbo.AIMMaterial AM with (nolock)
			ON S.AIMMaterialID = AM.AIMMaterialID	
	LEFT JOIN
		dbo.MaterialType MT with (nolock) on AM.MaterialTypeID = MT.MaterialTypeID
			
		'

	if len(@Where) > 0
		SET @SQL += @Where
	

	SET @SQL += ' ORDER BY CASE WHEN S.SampleCode IS NULL or len(S.SampleCode) = 0 then	
			convert(varchar, S.SampleID) else S.SampleCode End '

	EXEC(@SQL)
	--print @SQL
		
	END
	
END