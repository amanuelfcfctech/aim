--USE [CFCAIMElmira]
--GO
--/****** Object:  StoredProcedure [dbo].[usp_Report_Analysis_Pivot_Data_Product]    Script Date: 4/15/2026 1:55:23 PM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
CREATE PROCEDURE [dbo].[usp_Report_Analysis_Pivot_Data_Product]
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
AS

--DECLARE		  @SampleOwnerID			INT			= 1
--		, @AIMOwnerLocationID		INT			= 0
--		, @AIMSourceID				INT			= 0
--		, @AIMSourceLocationID		INT			= 0
--		, @ToDate					DATE		= '09-Apr-26 12:00:00 AM'
--		, @FromDate					DATE		= '09-Apr-26 12:00:00 AM'
--		, @AIMMaterialID			INT			= 0
--		, @Ticket					NVARCHAR(20) = ''		
--		, @AimDataBaseID			INT			= 1
--		, @QueryTemplateID			INT			= NULL

--		--2/16/21
--		, @GroupMaterials		BIT			= 0

--exec usp_Report_Analysis_Pivot_Data_Product 0, 0, 0, 0, '2021-09-05', '2021-09-01', 0, '', 1, 63, 2

--2/16/21 - added AIMMaterialGroup - header and detail tables

--2/26/21 - removed CFC tables in Select and Joins.  We think these are not used, mostly likely copied from another stp
--11/1/21 - uncommented PART of code that was removed on 2/26/21. User-Defined Sample Pivot Data report uses CPAN.NutrientValue (so need select fields and joins afterall)



BEGIN
SET NOCOUNT ON;

--10/17/23
--DECLARE @SQL			VARCHAR(6000) = ''
--DECLARE @Total		VARCHAR(1000) = ''
--DECLARE @Where		VARCHAR(1000) = ''
--DECLARE @WhereValue	VARCHAR(1000) = ''
--DECLARE @MatNutWhere	VARCHAR(1000) = ''
DECLARE @SQL			VARCHAR(MAX) = ''
DECLARE @Total			VARCHAR(MAX) = ''
DECLARE @Where			VARCHAR(MAX) = ''
DECLARE @WhereValue		VARCHAR(MAX) = ''
DECLARE @MatNutWhere	VARCHAR(MAX) = ''

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
			
		SET @Where = 'WHERE AM.MaterialTypeID = 2 and '

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
		EXEC @MatNutWhere = [dbo].[AnalysisReports_MaterialNutrient_WhereStatement] @QueryTemplateID 	

		SET @Where = ' WHERE S.IsActive = 1 ' + @Where 
		IF LEN(@MatNutWhere) > 0
			SET @Where += @MatNutWhere 	
	END

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
	, AN.AIMNutrientName

	, S.ContractNumber 
	, S.PoNumber 
	, S.InvoiceNumber 
	, S.TicketNumber 
	, S.LoadNumber 
	, S.BillOfLoad 
	, S.VehicleNumber 
	, S.LotNumber 
	
	, CP.CFCProductID
	, CP.CFCProductCode
	, CP.CFCProductName
	, CPAN.NutrientValue
	, CPA.ProductionVersion
	, 0 as CFCIngredientID
	, '''' as CFCIngredientCode
	, '''' as CFCIngredientName
	, UT.UnitTypeDescription as Unit 
	
	, AM.AIMMaterialCode AS MaterialCode
	, AM.AIMMaterialName AS MaterialName

	, AN.AIMNutrientCode AS AnalyzerNutrientCode ' +
	-- Ran into issue where Non US date format was causing an error when converting string to date
	--, convert(varchar(20),S.SampleDate, 101) as StringSampleDate
	--, convert(varchar(20),SR.AnalysisDate, 101) as ResultAnalysisDate

	', S.SampleDate
	 , SR.AnalysisDate
	, ST.SampleTypeName
	, WL.Min AS WLMin 					
	, WL.Max AS WLMax					
	, ISNULL(CASE WHEN SR.ResultValue < WL.Min THEN 1 ELSE CASE WHEN SR.ResultValue > WL.Max THEN 2 ELSE 0 END END, 0) AS ResultValueFlag
	, WL.AIMNutrientID
	
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
		dbo.CFCProductArchive CPA with (nolock)
			ON S.CFCProductArchiveID = CPA.CFCProductArchiveID
	LEFT OUTER JOIN 
		dbo.CFCProduct CP WITH(NOLOCK)
			ON CP.CFCProductID = CPA.CFCProductID 
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
			ON AN.AIMNutrientID = SA.AIMNutrientID
			AND	AN.IsActive = 1
	LEFT OUTER JOIN
		dbo.CFCNutrientRefAIMNutrient CNRAN
			ON CNRAN.AIMNutrientID = AN.AIMNutrientID
	LEFT OUTER JOIN
		dbo.CFCProductArchiveNutrient CPAN
			ON CPAN.CFCProductArchiveID = CPA.CFCProductArchiveID AND
				CPAN.CFCNutrientID = CNRAN.CFCNutrientID
	LEFT OUTER JOIN
		dbo.UnitType UT with (nolock)
			ON AN.UnitTypeID = UT.UnitTypeID	
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

' ORDER BY  
	AO.OwnerName 
	, AOL.LocationName
	, SS.SourceName
	, SL.SourceLocationName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName	
'


EXEC (@SQL)
--print @SQL



END