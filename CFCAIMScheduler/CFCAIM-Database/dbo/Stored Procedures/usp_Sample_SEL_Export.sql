CREATE PROCEDURE [dbo].[usp_Sample_SEL_Export]	
	
	  @SampleCodeBeg			NVARCHAR(20) = NULL
	, @SampleCodeEnd			NVARCHAR(20) = NULL
	, @DateBeg					NVARCHAR(23) = NULL
	, @DateEnd					NVARCHAR(23) = NULL

	

--exec [usp_Sample_SEL_Export] '36853-03', '36860-29'
		
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
		S.SampleID
		, CASE WHEN S.SampleCode IS NULL or len(S.SampleCode) = 0 then	
			convert(varchar, S.SampleID) else S.SampleCode End as SampleCode      		
		,S.SampleDate
		, AM.MaterialTypeID
		, AM.AIMMaterialCode
		, AM.AIMMaterialName
		  
	FROM 
		dbo.Sample S WITH(NOLOCK) 
	INNER JOIN
		dbo.AIMMaterial AM with (nolock)
			ON S.AIMMaterialID = AM.AIMMaterialID	
		
		'

	if len(@Where) > 0
		SET @SQL += @Where
	
	EXEC(@SQL)
	--print @SQL
		
	END
	
END