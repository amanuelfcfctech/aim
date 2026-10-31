CREATE PROCEDURE [dbo].[usp_Report_SampleResultsComparison]		
	 
	  @AIMOwnerID				INT			= NULL
	, @AIMOwnerLocationID		INT			= NULL
	, @AIMSourceID				INT			= NULL
	, @AIMSourceLocationID		INT			= NULL
	, @ToDate					DATE		= NULL
	, @FromDate					DATE		= NULL
	, @AIMMaterialID			INT			= NULL
	, @Ticket					NVARCHAR(20) = NULL
	, @QueryTemplateID			INT			= NULL
	, @DataSourceTypeID1		INT			= null		
	, @DataSourceTypeID2		INT			= null
	, @UseAnalysisDate			BIT			= null

	
-- exec usp_Report_SampleResultsComparison null, null, null, null, '2022-01-03', '2022-01-03', null, '', 13, 10000, 4
-- exec usp_Report_SampleResultsComparison null, null, null, null, null, null, null, '', 13, 10000, 4, 1



AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

--10/17/23
--DECLARE @SQL			VARCHAR(6000) = ''
--DECLARE @Where		VARCHAR(1000) = ''
--DECLARE @WhereValue	VARCHAR(1000) = ''
--DECLARE @MatNutWhere	VARCHAR(1000) = ''
--DECLARE @CFCWhere		VARCHAR(1000) = ''
DECLARE @SQL			VARCHAR(MAX) = ''
DECLARE @Where			VARCHAR(MAX) = ''
DECLARE @WhereValue		VARCHAR(MAX) = ''
DECLARE @MatNutWhere	VARCHAR(MAX) = ''
DECLARE @CFCWhere		VARCHAR(MAX) = ''

IF @AIMOwnerID = 0
	BEGIN
		SET @AIMOwnerID = NULL
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

		SET @Where = 'WHERE AN.IsActive = 1 and S.IsActive = 1 and '

		if @AIMOwnerID IS NOT NULL
			SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar) + ' '			
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
	END
ELSE
	BEGIN			

		-- These functions create the where statements the query will use
		EXEC @Where = [dbo].[AnalysisReports_WhereStatement] @QueryTemplateID 
		EXEC @CFCWhere = [dbo].[AnalysisReports_CFCIngredient_WhereStatement] @QueryTemplateID
		EXEC @MatNutWhere = [dbo].[AnalysisReports_MaterialNutrient_WhereStatement] @QueryTemplateID 	

		SET @Where = ' WHERE S.IsActive = 1 and sr.isactive = 1 ' + @Where 
		--SET @Where = ' WHERE S.IsActive = 1 and sr.isactive = 1 and aimmaterialcode = ''71300''  ' + @Where 
		
		IF LEN(@MatNutWhere) > 0
			SET @Where += @MatNutWhere 	
	END


SET @SQL = 'SELECT '

IF @UseAnalysisDate = 1
	SET @SQL += ' convert(date,SR.AnalysisDate) as SampleDate '
Else
	SET @SQL += ' S.SampleDate '

SET @SQL += '
		, AMM.AIMMaterialCode
		, AMS.AIMSupplierCode
		, AOL.LocationName
		, S.TicketNumber			
		, AMN.AIMNutrientName 
		, max(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID1 as varchar) + ' then resultValue end) as val1
		, max(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID2 as varchar) + ' then resultValue end) as val2
		, (select name from datasourcetype where datasourcetypeid = ' + cast(@DataSourceTypeID1 as varchar) + ') as DataSourceName1
		, (select name from datasourcetype where datasourcetypeid = ' + cast(@DataSourceTypeID2 as varchar) + ') as DataSourceName2
		, s.samplecode
		, s.aimownerid
		, s.aimownerlocationid
		, s.aimsourceid
		, s.aimsourcelocationid
		 , S.SampleID 
		, S.AimMaterialID	
		, AMN.AIMNutrientID		
		, AMN.AIMNutrientCode		
		
		
	FROM dbo.sample S with (nolock)
	INNER JOIN 
		dbo.SampleResult SR with (nolock)
				ON S.SampleID = SR.SampleID			
	INNER JOIN 
		dbo.SampleAIM SA WITH(NOLOCK)
			ON	SA.SampleID = S.SampleID 
			AND SA.SampleResultID = SR.SampleResultID
			AND	SA.IsActive = 1
	INNER JOIN 
		dbo.AIMNutrientRefAnalyzerNutrient AMRAN with (nolock)
			ON SR.AnalyzerNutrientID = AMRAN.AnalyzerNutrientID 
	LEFT OUTER JOIN 
		dbo.AnalyzerNutrient AN with (nolock)
			ON AMRAN.AnalyzerNutrientID = AN.AnalyzerNutrientID
	LEFT OUTER JOIN
		dbo.AIMNutrient AMN with (nolock)
			ON AMRAN.AimNutrientID = AMN.AimNutrientID
	LEFT OUTER JOIN
		dbo.AIMMaterial AMM with (nolock)
			ON S.AIMMaterialID = AMM.AIMMaterialID
	LEFT OUTER JOIN
		dbo.DataSourceType DST 
			ON AN.DataSourceTypeID = DST.DataSourceTypeID
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL WITH(NOLOCK)
			ON AOL.AIMOwnerLocationID = S.AIMOwnerLocationID 
	LEFT OUTER JOIN
		dbo.AIMSource AMS 
			ON S.AIMSourceID = AMS.AIMSourceID
	LEFT OUTER JOIN
		dbo.AIMSourceLocation AMSL
			ON S.AIMSourceLocationID = AMSL.AIMSourceLocationID	

' + @Where + ' ' +  


'GROUP BY '

IF @UseAnalysisDate = 1
	SET @SQL += ' SR.AnalysisDate '
Else
	SET @SQL += ' S.SampleDate '

SET @SQL += '		
	, AMM.AIMMaterialCode
	, AMS.AIMSupplierCode
	, AOL.LocationName
	, S.TicketNumber		
		
	, AMN.AIMNutrientName 
	, s.samplecode
	, s.aimownerid
	, s.aimownerlocationid
	, s.aimsourceid
	, s.aimsourcelocationid
	, S.SampleID 
	, S.AimMaterialID	
	, AMN.AIMNutrientID		
	, AMN.AIMNutrientCode	
		




'
	 
----	 having max(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID1 as varchar) + ' then resultValue end) > 0 
----     and max(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID2 as varchar) + ' then resultValue end) > 0


--print @sql
EXEC (@SQL)

END