CREATE PROCEDURE [dbo].[usp_Report_SampleComparison]

	  @DataSource1				INT
	, @DataSource2				INT
	--, @Type1 INT   -- 0 = Internal, 1 = External			--3/12/20 - commented, no longer used
	--, @Type2 INT   -- 0 = Internal, 1 = External			--3/12/20 - commented, no longer used
	, @AIMMaterialID			INT = NULL
	, @SampleOwnerID			INT = NULL				--3/16/20 - added back in
	, @AIMOwnerLocationID		INT = NULL				--3/16/20 - added back in
	, @AIMSupplierID			INT = NULL				--3/16/20 - added back in
	, @AIMSupplierLocationID	INT = NULL				--3/16/20 - added back in
	, @ToDate					DATETIME = NULL			--3/12/20 - added back in
	, @FromDate					DATETIME = NULL			--3/12/20 - added back in
	, @SampleCode				Varchar(20) = null		--3/16/20 - added back in

	-- 6/5/20
	, @BothMustHaveValues		BIT = NULL

--4/20/21 - added AimNutrientCode to select statements (for ordering nutrients in report)
	
-- exec usp_Report_SampleComparison 1, 10012, 5589, 0, 0, 0, 0, '2020-06-04', '2020-05-05', '', 1

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

--10/17/23
--DECLARE @SQL			    VARCHAR(6000) = ''
--DECLARE @Where			VARCHAR(1000) = ''
--DECLARE @WhereDataSource1 VARCHAR(1000) = ''
--DECLARE @WhereDataSource2 VARCHAR(1000) = ''
--DECLARE @WhereRest		VARCHAR(1000) = ''
DECLARE @SQL			  VARCHAR(MAX) = ''
DECLARE @Where			  VARCHAR(MAX) = ''
DECLARE @WhereDataSource1 VARCHAR(MAX) = ''
DECLARE @WhereDataSource2 VARCHAR(MAX) = ''
DECLARE @WhereRest		  VARCHAR(MAX) = ''


/*
 -- DataSource1 and DataSource2 can be a combination of system and user-defined datasources
	ex:  Foss vs. WetLab
		 Foss vs. Adissio
		 WetLab vs. another external datasource

--  TYPES NO LONGER USED  3/12/20 
			-- Type1 and Type2 are bits, 0 and 1.  
						0 = Internal, 1 = External
			-- Type1 and Type2 fields are necessary to use the correct datasource table 
						Internal = DataSourceType table
						External = DataSourceUser table
*/

IF @SampleOwnerID = 0
	BEGIN
		SET @SampleOwnerID = NULL
	END

IF @AIMOwnerLocationID = 0
	BEGIN
		SET @AIMOwnerLocationID = NULL
	END

IF @AIMSupplierID = 0
	BEGIN
		SET @AIMSupplierID = NULL
	END

IF @AIMSupplierLocationID = 0
	BEGIN
		SET @AIMSupplierLocationID = NULL
	END

IF @AIMMaterialID = 0
	BEGIN
		SET @AIMMaterialID = NULL
	END

IF Len(@SampleCode) = 0
	BEGIN
		SET @SampleCode = NULL
	END



SET @Where = ' WHERE SR.IsActive = 1 '


--3/12/20 - Internal/External no longer used
--IF @Type1 = 0	
--	SET @WhereDataSource1 += ' AND AN.DataSourceTypeID = ' + cast(@DataSource1 as varchar) 
--IF @Type1 = 1	
--	SET @WhereDataSource1 += ' AND AN.DataSourceTypeID = ' + cast( @DataSource1 as varchar)
--IF @Type2 = 0	
--	SET @WhereDataSource2 += ' AND AN.DataSourceTypeID = ' + cast(@DataSource2 as varchar)
--IF @Type2 = 1	
--	SET @WhereDataSource2 += ' AND AN.DataSourceTypeID = ' + cast(@DataSource2 as varchar)
SET @WhereDataSource1 += ' AND DST.DataSourceTypeID = ' + cast(@DataSource1 as varchar) 
SET @WhereDataSource2 += ' AND DST.DataSourceTypeID = ' + cast(@DataSource2 as varchar)


IF @SampleOwnerID IS NOT NULL
	SET @WhereRest += ' and S.AIMOwnerID = ' + Cast(@SampleOwnerID as varchar) + ' '			
IF @AIMOwnerLocationID IS NOT NULL
	SET @WhereRest += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '			
IF @AIMSupplierID IS NOT NULL
	SET @WhereRest += ' and S.AIMSourceID = ' + Cast(@AIMSupplierID as varchar) + ' '			
IF @AIMSupplierLocationID IS NOT NULL
	SET @WhereRest += ' and S.AIMSourceLocationID = ' + Cast(@AIMSupplierLocationID as varchar) + ' '			
IF @AIMMaterialID IS NOT NULL
	SET @WhereRest += ' and S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' '			
IF @SampleCode IS NOT NULL
	SET @WhereRest += ' and S.SampleCode = ''' + Cast(@SampleCode as varchar) + ''' '							
IF @FromDate IS NOT NULL
	SET @WhereRest += ' and SR.AnalysisDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' '
IF @ToDate IS NOT NULL
	SET @WhereRest += ' and SR.AnalysisDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' '

SET @SQL = 'SELECT '

If @ToDate is NOT NULL
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, '
If @FromDate IS NOT NULL
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '
SET @SQL += '	  	
	
	  SampleID
	, AimMaterialID, AimMaterialName, AimNutrientID, AimNutrientName, AimNutrientCode
	, case when sum(Val1) is not null then sum(Val1) end as ResultValue1  
	, case when sum(Val2) is not null then sum(Val2) end as ResultValue2  
	, samplecode
	, aimownerid
	, aimownerlocationid
	, aimsourceid
	, aimsourcelocationid

	FROM 
	(
	SELECT
		  S.SampleID 
		, S.AimMaterialID	
		, AMM.AIMMaterialName
		, AMN.AIMNutrientID		
		, AMN.AIMNutrientName
		, AMN.AIMNutrientCode
		, SR.ResultValue as Val1
		, Null as Val2
		, DST.DataSourceTypeID				
		
		, s.samplecode
		, s.aimownerid
		, s.aimownerlocationid
		, s.aimsourceid
		, s.aimsourcelocationid
		
	FROM dbo.sample S with (nolock)
	INNER JOIN 
		dbo.SampleResult SR with (nolock)
				ON S.SampleID = SR.SampleID
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
	' + @Where + @WhereDataSource1 + @WhereRest +

	' UNION
	
	SELECT
		  S.SampleID 
		, S.AimMaterialID	
		, AMM.AIMMaterialName
		, AMN.AIMNutrientID		
		, AMN.AIMNutrientName
		, AMN.AIMNutrientCode
		, Null as Val1
		, SR.ResultValue as Val2
		, DST.DataSourceTypeID			
		
		, s.samplecode
		, s.aimownerid
		, s.aimownerlocationid
		, s.aimsourceid
		, s.aimsourcelocationid	
		
	FROM dbo.sample S with (nolock)
	INNER JOIN 
		dbo.SampleResult SR with (nolock)
				ON S.SampleID = SR.SampleID
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
	' + @Where + @WhereDataSource2 + @WhereRest +

	'
	) x


GROUP BY SampleID, AimMaterialID, AimMaterialName, AimNutrientID, AimNutrientName, AimNutrientCode 
		, samplecode
		, aimownerid
		, aimownerlocationid
		, aimsourceid
		, aimsourcelocationid
		
		'
If @BothMustHaveValues = 1
	SET @SQL += 'HAVING sum(Val1) is not null and sum(Val2) is not null'			  	

--print @sql
EXEC (@SQL)

END