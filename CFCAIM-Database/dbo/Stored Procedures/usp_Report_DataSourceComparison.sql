CREATE PROCEDURE [dbo].[usp_Report_DataSourceComparison]		
	 

	  @AIMOwnerID					INT			= NULL
	, @AIMOwnerLocationID			INT			= NULL
	, @AIMSourceID					INT			= NULL
	, @AIMSourceLocationID			INT			= NULL
	, @ToDate						DATE		= NULL
	, @FromDate						DATE		= NULL
	, @AIMMaterialID				INT			= NULL
	, @Ticket						NVARCHAR(20) = NULL

	, @QueryTemplateID				INT			= NULL
	, @DataSourceTypeID1			INT			= null		
	, @DataSourceTypeID2			INT			= null

	, @Datasource1AIMNutrientID1	INT			= null
	, @Datasource1AIMNutrientID2	INT			= null
	, @Datasource1AIMNutrientID3	INT			= null

	, @Datasource2AIMNutrientID1	INT			= null
	, @Datasource2AIMNutrientID2	INT			= null
	, @Datasource2AIMNutrientID3	INT			= null
	
	, @UseAnalysisDate				BIT			= null

	--10/14/22
	, @DriverAIMNutrientID			INT			= null
	
-- exec usp_Report_DataSourceComparison null, null, null, null, '2022-04-14', '2020-04-14', null, '', 10, 4, 10000, 1, 2, 3, 66, 67, 68, 1
-- exec usp_Report_DataSourceComparison null, null, null, null, '2022-04-14', '2020-04-14', null, '', 15, 4, 10000, 1, 2, 3, 66, 67, 68, 1, 4

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
--DECLARE @DateString	VARCHAR(1000) = ''
DECLARE @SQL			VARCHAR(MAX) = ''
DECLARE @Where			VARCHAR(MAX) = ''
DECLARE @WhereValue		VARCHAR(MAX) = ''
DECLARE @MatNutWhere	VARCHAR(MAX) = ''
DECLARE @CFCWhere		VARCHAR(MAX) = ''
DECLARE @DateString		VARCHAR(MAX) = ''


-----------------------------------------------------------------------------
--10/14/22 - added temptable and DriverAIMNutrientID
IF @UseAnalysisDate = 1
	
	-- Get AnalysisDate string
	Begin
		SET @DateString = (
			SELECT QF.FieldName + ' ' +  
							CASE WHEN QTD.Value > 0 THEN REPLACE(QO.Command, '@VALUE', QTD.Value) 
							ELSE CASE WHEN QTD.DateValue IS NULL THEN QO.Command
							ELSE CASE WHEN QTD.SecondDate IS NULL THEN REPLACE(QO.Command, '@VALUE', '''' + CAST(QTD.DateValue AS VARCHAR) + '''')
							ELSE REPLACE(REPLACE(QO.Command, '@DateValue', '''' + CAST(QTD.DateValue AS VARCHAR) + '''') , '@SecondDate', '''' + CAST(QTD.SecondDate AS VARCHAR) + '''')						
							END END END AS WhereValue
			FROM	
				dbo.QueryTemplateDetail QTD 
				INNER JOIN dbo.QueryField QF ON QF.QueryFieldID = QTD.QueryFieldID 
				INNER JOIN dbo.QueryOperation QO ON QO.QueryOperationID = QTD.QueryOperationID 	
			WHERE QTD.QueryTemplateID = @QueryTemplateID AND QF.QueryFieldID = 12)			
	
		-- Create Temp table
		IF OBJECT_ID('tempdb..#tempTable') IS NOT NULL DROP TABLE #tempTable	
		CREATE TABLE #tempTable
		  ( 
			SampleID INT,
			AnalysisDate DATETIME,
			DataSourceTypeid INT)

		SET @SQL = '
			INSERT INTO #tempTable (SampleID, AnalysisDate, DataSourceTypeID)					
			SELECT 
				SR.SampleID
				, SR.AnalysisDate
				, AN.DataSourceTypeid			
			FROM
				dbo.SampleResult SR
			INNER JOIN
				dbo.AIMNutrientRefAnalyzerNutrient X
					ON SR.AnalyzerNutrientID = X.AnalyzerNutrientID
			INNER JOIN
				dbo.AnalyzerNutrient AN 
					ON X.AnalyzerNutrientID = AN.AnalyzerNutrientID
			WHERE
				X.AIMNutrientID = ' + Cast(@DriverAIMNutrientID as varchar) + ' and ' + @DateString	
		EXEC (@SQL)
		
	End	

-----------------------------------------------------------------------------
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
		--SET @Where = ' WHERE S.IsActive = 1 and sr.isactive = 1  and s.sampleid = 20861 ' + @Where 
		--SET @Where = ' WHERE S.IsActive = 1 and sr.isactive = 1 and (s.ticketnumber = ''736261'' or s.ticketnumber = ''ADMX 7198'')  ' + @Where 
		
		IF LEN(@MatNutWhere) > 0
			SET @Where += @MatNutWhere 	
	END




SET @SQL = 'SELECT '

IF @UseAnalysisDate = 1

	--10/14/22
	--SET @SQL += ' convert(date,SR.AnalysisDate) as SampleDate '
	SET @SQL += ' #tempTable.AnalysisDate as SampleDate '

Else
	SET @SQL += ' S.SampleDate '

SET @SQL += '
		, AMM.AIMMaterialCode
		, AMS.AIMSupplierCode
		, AOL.LocationName
		, S.TicketNumber
		, S.ExpirationDate
		, left(case when AMM.materialtypeid = 1 then AimSupplierLocationCode else AimMaterialCode end, 12) as MaterialSupplier
		, left(AMM.AIMMaterialName, 21) as AIMMaterialName
		, S.SampleCode 

		, (select am.AIMNutrientName from dbo.AIMNutrient am Where am.AIMNutrientID = ' + cast(@Datasource1AIMNutrientID1 as varchar) + ' ) as Datasource1AIMNutrientName1 
		, (select am.AIMNutrientName from dbo.AIMNutrient am Where am.AIMNutrientID = ' + cast(@Datasource2AIMNutrientID1 as varchar) + ' ) as Datasource2AIMNutrientName1		
		, (select am.AIMNutrientName from dbo.AIMNutrient am Where am.AIMNutrientID = ' + cast(@Datasource1AIMNutrientID2 as varchar) + ' ) as Datasource1AIMNutrientName2		
		, (select am.AIMNutrientName from dbo.AIMNutrient am Where am.AIMNutrientID = ' + cast(@Datasource2AIMNutrientID2 as varchar) + ' ) as Datasource2AIMNutrientName2		
		, (select am.AIMNutrientName from dbo.AIMNutrient am Where am.AIMNutrientID = ' + cast(@Datasource1AIMNutrientID3 as varchar) + ' ) as Datasource1AIMNutrientName3 
		, (select am.AIMNutrientName from dbo.AIMNutrient am Where am.AIMNutrientID = ' + cast(@Datasource2AIMNutrientID3 as varchar) + ' ) as Datasource2AIMNutrientName3


		, sum(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID1 as varchar) + ' and AMRAN.AIMNutrientID = ' + cast(@Datasource1AIMNutrientID1 as varchar) + ' then resultValue end) as Datasource1Value1
		, sum(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID2 as varchar) + ' and AMRAN.AIMNutrientID = ' + cast(@Datasource2AIMNutrientID1 as varchar) + ' then resultValue end) as Datasource2Value1
		, sum(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID1 as varchar) + ' and AMRAN.AIMNutrientID = ' + cast(@Datasource1AIMNutrientID2 as varchar) + ' then resultValue end) as Datasource1Value2
		, sum(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID2 as varchar) + ' and AMRAN.AIMNutrientID = ' + cast(@Datasource2AIMNutrientID2 as varchar) + ' then resultValue end) as Datasource2Value2
		, sum(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID1 as varchar) + ' and AMRAN.AIMNutrientID = ' + cast(@Datasource1AIMNutrientID3 as varchar) + ' then resultValue end) as Datasource1Value3
		, sum(case when dst.DataSourceTypeID = ' + cast(@DataSourceTypeID2 as varchar) + ' and AMRAN.AIMNutrientID = ' + cast(@Datasource2AIMNutrientID3 as varchar) + ' then resultValue end) as Datasource2Value3
		

		, (select name from datasourcetype where datasourcetypeid = ' + cast(@DataSourceTypeID1 as varchar) + ') as DataSourceTypeName1
		, (select name from datasourcetype where datasourcetypeid = ' + cast(@DataSourceTypeID2 as varchar) + ') as DataSourceTypeName2
		, S.SampleCode
		, S.AIMOwnerID
		, S.AIMOwnerLocationID
		, S.AIMSourceID
		, S.AIMSourceLocationID
		, S.SampleID 
		, S.AIMMaterialID	
		, AMM.MaterialTypeID
				
		
	FROM dbo.sample S with (nolock) '

	--10/14/22
	IF @UseAnalysisDate = 1
		SET @SQL += ' INNER JOIN #tempTable ON S.SampleID = #tempTable.SampleID '

	SET @SQL += '
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

	--10/14/22
	--SET @SQL += ' convert(date,SR.AnalysisDate) '
	SET @SQL += ' #tempTable.AnalysisDate '

Else
	SET @SQL += ' S.SampleDate '

SET @SQL += '		
	, AMM.AIMMaterialCode
	, AMS.AIMSupplierCode
	, AOL.LocationName
	, S.TicketNumber	
	, S.ExpirationDate
	, S.SampleCode
	, S.AIMOwnerID
	, S.AIMOwnerLocationID
	, S.AIMSourceID
	, S.AIMSourceLocationID
	, S.SampleID 
	, S.AIMMaterialID	
		
	, AMM.MaterialTypeID
	, AMSL.AimSupplierLocationCode
	, AMM.AIMMaterialName
'
--------------------------------------------------------------------------------------------------------------------------------
-- This is how Butterball would like the report sorted; however, on the DevExpress report itself, I could not figure out how to
-- sort products one way and ingredients another .. so, Butterball said it was ok to sort all materials by MaterialTypeID desc (so products 
-- show first), and then by material name, material/supplier, and sample code.  The sorting is done on the DevExpress report and so the 
-- lines below aren't used, but thought I'd leave for reference.  8/10/22.
SET @SQL += ' ORDER BY 
	AMM.MaterialTypeID DESC,
	case when AMM.MaterialTypeID = 1 then left(AMM.AIMMaterialName, 21) end,
	left(case when AMM.MaterialTypeID = 1 then AimSupplierLocationCode else AimMaterialCode end, 12),
	S.SampleCode '
--------------------------------------------------------------------------------------------------------------------------------

--print @sql
EXEC (@SQL)

continu:

END