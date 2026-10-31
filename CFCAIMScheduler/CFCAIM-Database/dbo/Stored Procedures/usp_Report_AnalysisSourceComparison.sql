CREATE PROCEDURE [dbo].[usp_Report_AnalysisSourceComparison]
		  @SampleOwnerID			INT			= NULL
		, @AIMOwnerLocationID		INT			= NULL
		, @AIMSourceID				INT			= NULL
		, @AIMSourceLocationID		INT			= NULL
		, @ToDate					DATE		= NULL
		, @FromDate					DATE		= NULL
		, @AIMMaterialID			INT			= NULL
		, @Ticket					NVARCHAR(20) = NULL
		, @QueryTemplateID			INT			= NULL

--exec usp_Report_AnalysisSourceComparison null, null, null, null, null, null, null, null, 21
--exec usp_Report_AnalysisSourceComparison 4, 1, 21, 51, '2016-07-01', '2016-07-01', 10, '', null
--exec usp_Report_AnalysisSourceComparison 0, 0, 0, 0, '2014-07-01', '2016-09-01', 0, '', null

--exec usp_Report_AnalysisSourceComparison 0, 0, 0, 0, '2018-04-20', '2023-10-01', 0, '', 7

AS
BEGIN
SET NOCOUNT ON;

--10/17/23
--DECLARE @SQL			VARCHAR(6000) = ''
--DECLARE @Total		VARCHAR(1000) = ''
--DECLARE @Where		VARCHAR(1000) = ''
--DECLARE @WhereValue	VARCHAR(1000) = ''
--DECLARE @MatNutWhere	VARCHAR(1000) = ''
--DECLARE @CFCWhere		VARCHAR(1000) = ''
DECLARE @SQL			VARCHAR(MAX) = ''
DECLARE @Total			VARCHAR(MAX) = ''
DECLARE @Where			VARCHAR(MAX) = ''
DECLARE @WhereValue		VARCHAR(MAX) = ''
DECLARE @MatNutWhere	VARCHAR(MAX) = ''
DECLARE @CFCWhere		VARCHAR(MAX) = ''

IF @SampleOwnerID = 0
	BEGIN
		SET @SampleOwnerID = NULL
	END

IF @AIMOwnerLocationID = 0
	BEGIN
		SET @AIMOwnerLocationID = NULL
	END

IF @AIMSourceID = 0
	BEGIN
		SET @AIMSourceID = NULL
	END

IF @AIMSourceLocationID = 0
	BEGIN
		SET @AIMSourceLocationID = NULL
	END

IF @AIMMaterialID = 0
	BEGIN
		SET @AIMMaterialID = NULL
	END

IF Len(@Ticket) = 0
	BEGIN
		SET @Ticket = NULL
	END

IF @QueryTemplateID IS NULL 
	BEGIN

		SET @Where = 'WHERE S.IsActive = 1 '

		if @SampleOwnerID IS NOT NULL
			SET @Where += ' and S.AIMOwnerID = ' + Cast(@SampleOwnerID as varchar) + ' '			
		if @AIMOwnerLocationID IS NOT NULL
			SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '			
		if @AIMSourceID IS NOT NULL
			SET @Where += ' and S.AIMSourceID = ' + Cast(@AIMSourceID as varchar) + ' '			
		if @AIMSourceLocationID IS NOT NULL
			SET @Where += ' and S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID as varchar) + ' '			
		if @AIMMaterialID IS NOT NULL
			SET @Where += ' and S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' '			
		if @Ticket IS NOT NULL
			SET @Where += ' and S.TicketNumber = ' + Cast(@Ticket as varchar) + ' '							
		if @FromDate IS NOT NULL
			SET @Where += ' and S.SampleDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' '
		if @ToDate IS NOT NULL
			SET @Where += ' and S.SampleDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' '

	END
ELSE
	BEGIN			

		-- 9/12/17
		-- These functions create the where statements the query will use
		EXEC @Where = [dbo].[AnalysisReports_WhereStatement] @QueryTemplateID 
		EXEC @CFCWhere = [dbo].[AnalysisReports_CFCIngredient_WhereStatement] @QueryTemplateID
		EXEC @MatNutWhere = [dbo].[AnalysisReports_MaterialNutrient_WhereStatement] @QueryTemplateID 	

		SET @Where = ' WHERE S.IsActive = 1 ' + @Where 
		IF LEN(@MatNutWhere) > 0
			SET @Where += @MatNutWhere 	
	END

print @Where
	
SET @SQL = 'SELECT '

If @ToDate is NOT NULL
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, '
If @FromDate IS NOT NULL
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '
SET @SQL += '	  		
	  AO.OwnerName 
	, AOL.LocationName
	, AM.AIMMaterialCode AS MaterialCode
	, AM.AIMMaterialName AS MaterialName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, UT.UnitTypeDescription as Unit 
	, SS.SourceName
	, SL.SourceLocationName
	, STDEV(SA.AIMUsedValue) AS StandardDeviation
	, SUM(SA.AIMUsedValue) as ResultValue	
	, AVG(SA.AIMUsedValue) AS Mean
	, XX.count
	
	FROM 
	dbo.Sample S WITH(NOLOCK)
	INNER JOIN 
		dbo.SampleResult SR WITH(NOLOCK)
			ON	SR.SampleID = S.SampleID 
			AND	SR.IsActive = 1
	INNER JOIN 
		dbo.SampleAIM SA WITH(NOLOCK)
			ON	SA.SampleID = S.SampleID 
			AND SA.SampleResultID = SR.SampleResultID
			AND	SA.IsActive = 1	
	LEFT OUTER JOIN 
		dbo.AIMOwner AO WITH(NOLOCK)
			ON AO.AIMOwnerID = S.AIMOwnerID
	LEFT OUTER JOIN 
		dbo.AIMMaterial AM WITH(NOLOCK)
			ON AM.AIMMaterialID = S.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL WITH(NOLOCK)
			ON AOL.AIMOwnerLocationID = S.AIMOwnerLocationID 
	LEFT OUTER JOIN 
		dbo.AIMSource SS WITH(NOLOCK)
			ON SS.AIMSourceID = S.AIMSourceID
	LEFT OUTER JOIN 
		dbo.AIMSourceLocation SL WITH(NOLOCK)
			ON SL.AIMSourceLocationID = S.AIMSourceLocationID									
	LEFT OUTER JOIN 
		dbo.AIMNutrient AN WITH(NOLOCK)
			ON	AN.AIMNutrientID = SA.AIMNutrientID
			AND AN.IsActive = 1  
	LEFT OUTER JOIN 
		dbo.AIMNutrientRefAnalyzerNutrient x 
			on AN.AIMNutrientID = x.AIMNutrientID
	LEFT OUTER JOIN
		dbo.UnitType UT with (nolock)
			ON AN.UnitTypeID = UT.UnitTypeID

	LEFT OUTER JOIN
		( SELECT aimownerID, aimownerlocationid, aimmaterialid, aimsourceid, aimsourcelocationid, SA.aimnutrientid, count(*) as count
			FROM dbo.SAMPLE S
			INNER JOIN dbo.SAMPLERESULT SR ON S.SAMPLEID = SR.SAMPLEID
			INNER JOIN dbo.SampleAIM SA WITH(NOLOCK) ON	SA.SampleID = S.SampleID  
			INNER join dbo.AnalyzerNutrient AN on sr.AnalyzerNutrientID = AN.AnalyzerNutrientID
			INNER join dbo.AIMNutrientRefAnalyzerNutrient x on AN.AnalyzerNutrientID = x.AnalyzerNutrientID '
			+ @Where 
			+ ' 
			GROUP BY aimownerID, aimownerlocationid, aimmaterialid, aimsourceid, aimsourcelocationid, SA.aimnutrientid

		) XX 
		on s.aimownerID = XX.aimownerID and
			s.AIMOwnerLocationID = XX.AIMOwnerLocationID and
			s.AIMSourceID = XX.AIMSourceID and
			s.AIMSourceLocationID = XX.AIMSourceLocationID and
			s.AIMMaterialID = XX.AIMMaterialID and 
			x.AIMNutrientID = XX.AIMNutrientID 	

  ' + @Where + ' ' +  
 

'
GROUP BY	  
	  AO.OwnerName 
	, AOL.LocationName
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, UT.UnitTypeDescription	
	, SS.SourceName
	, SL.SourceLocationName	
	, XX.count

	'




EXEC (@SQL)
--print @sql



END
