-- *******************************
-- SQL UPDATE FOR VERSION 20260429
-- *******************************





CREATE PROCEDURE [dbo].[usp_Report_Analysis_Pivot_Data]
		  @SampleOwnerID			INT			= NULL
		, @AIMOwnerLocationID		INT			= NULL
		, @AIMSourceID				INT			= NULL
		, @AIMSourceLocationID		INT			= NULL
		, @ToDate					DATE		= NULL
		, @FromDate					DATE		= NULL
		, @AIMMaterialID			INT			= NULL
		, @Ticket					NVARCHAR(20) = NULL		
		, @AimDataBaseID			INT			= NULL
		, @QueryTemplateID			INT			= NULL

		--2/16/21
		, @GroupMaterials		BIT			= NULL

--exec usp_Report_Analysis_Pivot_Data 0, 0, 0, 0, '2020-03-31', '2020-01-01', 0, '', 1, null, 1
--exec usp_Report_Analysis_Pivot_Data 1, 0, 32, 34, '2022-06-07', '2015-01-01', 0, '', 1, null, 0

--2/16/21 - added AIMMaterialGroup - header and detail tables

--2/26/21 - removed CFC tables.  We think these are not used, mostly likely copied from another stp


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
	
		SET @Where = 'WHERE AM.MaterialTypeID = 1 AND '
		--SET @Where = 'WHERE AM.MaterialTypeID = 1 and CI.IsActive = 1 and '

		if @SampleOwnerID IS NOT NULL
			SET @Where += ' S.AIMOwnerID = ' + Cast(@SampleOwnerID as varchar) + ' and '			
		if @AIMOwnerLocationID IS NOT NULL
			SET @Where += ' S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' and '			
		if @AIMSourceID IS NOT NULL
			SET @Where += ' S.AIMSourceID = ' + Cast(@AIMSourceID as varchar) + ' and '			
		if @AIMSourceLocationID IS NOT NULL
			SET @Where += ' S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID as varchar) + ' and '			
		if @AIMMaterialID IS NOT NULL
			SET @Where += ' S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' and '			
		if @Ticket IS NOT NULL
			SET @Where += ' S.TicketNumber = ''' + Cast(@Ticket as varchar) + ''' and '							
		if @FromDate IS NOT NULL
			SET @Where += ' S.SampleDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' and '
		if @ToDate IS NOT NULL
			SET @Where += ' S.SampleDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' and '
		if Right(@Where,4) = 'and '
			Set @Where = Left(@Where,len(@Where) - 4)
		
	END
ELSE
	BEGIN			
			
		-- These functions create the where statements the query will use
		EXEC @Where = [dbo].[AnalysisReports_WhereStatement] @QueryTemplateID 	
		
		--2/26/21 - commented, no longer needed
		--EXEC @CFCWhere = [dbo].[AnalysisReports_CFCIngredient_WhereStatement] @QueryTemplateID

		EXEC @MatNutWhere = [dbo].[AnalysisReports_MaterialNutrient_WhereStatement] @QueryTemplateID 	

		SET @Where = ' WHERE S.IsActive = 1 ' + @Where 
		--SET @Where = ' WHERE S.IsActive = 1 and CI.IsActive = 1 ' + @Where 

		IF LEN(@MatNutWhere) > 0
			SET @Where += @MatNutWhere 	
	END


----------------------------------------------
SET @SQL = 'SELECT '

If @ToDate is NOT NULL
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, '
If @FromDate IS NOT NULL
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '	

SET @SQL += '	  	
	 case when len(S.SampleCode) > 0 then S.SampleCode Else convert(nvarchar,S.SampleID) end as SampleCode 	
	, SA.AimUsedValue as ResultValue		
	, AO.OwnerName 
	, AOL.LocationName
	, SS.SourceName
	, SL.SourceLocationName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName '

	SET @SQL += '

	, S.ContractNumber 
	, S.PoNumber 
	, S.InvoiceNumber 
	, S.TicketNumber 
	, S.LoadNumber 
	, S.BillOfLoad 
	, S.VehicleNumber 
	, S.LotNumber 
	, UT.UnitTypeDescription as Unit 
	, ST.SampleTypeName
	, WL.Min AS WLMin 					
	, WL.Max AS WLMax					
	, ISNULL(CASE WHEN SR.ResultValue < WL.Min THEN 1 ELSE CASE WHEN SR.ResultValue > WL.Max THEN 2 ELSE 0 END END, 0) AS ResultValueFlag
	, WL.AIMNutrientID
	'

If @GroupMaterials = 0
	Begin
		SET @SQL += ', AM.AIMMaterialCode as MaterialCode, AM.AIMMaterialName as MaterialName '
	End
Else
	Begin
		SET @SQL += '
		, Case when GH.AIMMaterialGroupHeaderName is not null then '''' Else AM.AIMMaterialCode end as MaterialCode
		, Case when GH.AIMMaterialGroupHeaderName is not null then GH.AIMMaterialGroupHeaderName Else AM.AIMMaterialName end as MaterialName '
	End
SET @SQL += '
	, AN.AIMNutrientCode as AnalyzerNutrientCode ' + 
	-- Ran into issue where Non US date format was causing an error when converting string to date
	--, convert(varchar(20),S.SampleDate, 101) as StringSampleDate
	--, convert(varchar(20),SR.AnalysisDate, 101) as ResultAnalysisDate
	', S.SampleDate
	
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
		dbo.AIMMaterial AM WITH(NOLOCK)
			ON AM.AIMMaterialID = S.AIMMaterialID
    LEFT OUTER JOIN 
		dbo.AIMNutrient AN WITH(NOLOCK)
			ON AN.AIMNutrientID = SA.AIMNutrientID
			AND	AN.IsActive = 1							
	LEFT OUTER JOIN 
		dbo.AIMOwner AO WITH(NOLOCK)
			ON AO.AIMOwnerID = S.AIMOwnerID	
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
		dbo.UnitType UT with (nolock)
			ON AN.UnitTypeID = UT.UnitTypeID '

	SET @SQL += '			

	LEFT OUTER JOIN
		dbo.AIMMaterialGroupDetail GD on S.AimMaterialID = GD.AIMMaterialID
	LEFT OUTER JOIN
		dbo.AIMMaterialGroupHeader GH on GD.AIMMaterialGroupHeaderID = GH.AIMMaterialGroupHeaderID
	LEFT OUTER JOIN
		dbo.SampleType ST
		ON ST.SampleTypeID = S.SampleTypeID
	LEFT OUTER JOIN 
		dbo.AIMNutrientRefAnalyzerNutrient AMNX
		ON SR.AnalyzerNutrientID = AMNX.AnalyzerNutrientID
	LEFT OUTER JOIN			
			dbo.WarningLimits WL
			ON WL.AIMNutrientID = AMNX.AIMNutrientID
			AND WL.AIMMaterialID = S.AIMMaterialID

	
			' 
+ @Where + ' ' +

 @CFCWhere + 

' ORDER BY  
	S.SampleCode
'


EXEC (@SQL)
--print @SQL



END