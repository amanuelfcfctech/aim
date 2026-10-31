CREATE PROCEDURE [dbo].[usp_Chart_SupplierCompare_save_for_reference]
	@AIMMaterialID INT = NULL
	, @AIMNutrientID INT = NULL	
	, @AIMSupplierID INT = NULL	
	, @AIMSupplierID2 INT = NULL	
	, @AIMOwnerID INT = NULL
	, @AIMOwnerLocationID INT = NULL	
	, @AIMSupplierLocationID INT = NULL
	, @TicketNumber NVARCHAR(20)
	, @SampleCodeFrom NVARCHAR(20) = NULL
	, @SampleCodeTo NVARCHAR(20) = NULL
	, @StartDate Date = NULL
	, @EndDate Date = NULL	

--exec usp_Chart_SupplierCompare 5, 1, 8, 9, 0, 0, 0, '', '','', '20151215', '20160620'
-- for fse:
-- 5 = 'MBM50%'
-- 8 = 'Darling'
-- 9 = 'CBP'

AS
BEGIN
SET NOCOUNT ON;

DECLARE @FROM			VARCHAR(3000) = ''
DECLARE @SQL			VARCHAR(6000) = ''
DECLARE @Where			VARCHAR(1000) = ''

IF @AIMMaterialID = 0
	BEGIN
		SET @AIMMaterialID = NULL
	END
IF @AIMNutrientID = 0
	BEGIN
		SET @AIMNutrientID = NULL
	END
IF @AIMSupplierID = 0
	BEGIN
		SET @AIMSupplierID = NULL
	END
IF @AIMSupplierID2 = 0
	BEGIN
		SET @AIMSupplierID2 = NULL
	END
IF @AIMOwnerID = 0
	BEGIN
		SET @AIMOwnerID = NULL
	END
IF @AIMOwnerLocationID = 0
	BEGIN
		SET @AIMOwnerLocationID = NULL
	END

IF @AIMSupplierLocationID = 0
	BEGIN
		SET @AIMSupplierLocationID = NULL
	END
IF Len(@TicketNumber) = 0
	BEGIN
		SET @TicketNumber = NULL
	END
IF Len(@SampleCodeFrom) = 0
	BEGIN
		SET @SampleCodeFrom = NULL
	END
IF Len(@SampleCodeTo) = 0
	BEGIN
		SET @SampleCodeTo = NULL
	END

----------------------------------------------
SET @Where = 'WHERE S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' '			
if (@SampleCodeFrom IS NOT NULL)
	SET @Where += ' and S.SampleCode >= ''' + Cast(@SampleCodeFrom as varchar) + ' '''
if (@SampleCodeTo IS NOT NULL)
	SET @Where += ' and S.SampleCode <= ''' + Cast(@SampleCodeTo as varchar) + ' '''
if @AIMOwnerID IS NOT NULL
	SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar) + ' '			
if @AIMOwnerLocationID IS NOT NULL
	SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '			
--if @AIMSupplierID IS NOT NULL
--	SET @Where += ' and S.AIMSourceID = ' + Cast(@AIMSupplierID as varchar) + ' '			
if @AIMSupplierLocationID IS NOT NULL
	SET @Where += ' and S.AIMSourceLocationID = ' + Cast(@AIMSupplierLocationID as varchar) + ' '				
if @TicketNumber IS NOT NULL
	SET @Where += ' and S.TicketNumber = ' + Cast(@TicketNumber as varchar) + ' '	
If @StartDate IS NOT NULL
	SET @Where += ' and S.SampleDate >= ''' + convert(varchar,@StartDate) + ''' '
If @EndDate IS NOT NULL
	SET @Where += ' and S.SampleDate <= ''' + convert(varchar,@EndDate) + ''' '

----------------------------------------------

SET @FROM = ' FROM dbo.Sample S with(nolock) 
	LEFT JOIN (Select S.SampleID, 
	S.SampleCode, S.AlternateCode,
	x.AimNutrientID, AN.AimNutrientName, S.AIMSourceID, AMS.SourceName, ResultValue  FROM dbo.Sample S with(nolock) 
	INNER JOIN dbo.SampleResult sr with(nolock) ON S.SampleID = sr.SampleID
	INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)	ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
	INNER JOIN dbo.aimnutrient AN with(nolock) ON x.AIMNutrientID = AN.AIMNutrientID 
	INNER JOIN dbo.AIMSource AMS with (nolock) on S.AIMSourceID = AMS.AIMSourceID '
	+ @Where + ' and AN.IsActive = 1 and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) 
	+ ' and S.AIMSourceID = ' + Cast(@AIMSupplierID as varchar) 
	+ ') x ON x.SampleID = S.SampleID

	LEFT JOIN (Select S.SampleID, 
	S.SampleCode, S.AlternateCode,
	x.AimNutrientID, AN.AimNutrientName, S.AIMSourceID, AMS.SourceName, ResultValue  FROM dbo.Sample S with(nolock) 
	INNER JOIN dbo.SampleResult sr with(nolock) ON S.SampleID = sr.SampleID
	INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)	ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
	INNER JOIN dbo.aimnutrient AN with(nolock) ON x.AIMNutrientID = AN.AIMNutrientID 
	INNER JOIN dbo.AIMSource AMS with (nolock) on S.AIMSourceID = AMS.AIMSourceID '
	+ @Where + ' and AN.IsActive = 1 and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar)
	+ ' and S.AIMSourceID = ' + Cast(@AIMSupplierID2 as varchar) 
	+ ') x2 ON x2.SampleID = S.SampleID '

SET @SQL = 'SELECT '
If @StartDate IS NOT NULL
	SET @SQL += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
If @EndDate is NOT NULL
	SET @SQL += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
SET @SQL += ' 	S.SampleCode, S.AlternateCode,
				S.SampleID, SampleDate, x.AimNutrientID, x.AIMNutrientName, x.AIMSourceID, x.SourceName, x.ResultValue, 
				x2.AIMNutrientID as AIMNutrientID2, x2.AIMNutrientName as AIMNutrientName2, x2.AIMSourceID as AIMSourceID2,
				x2.SourceName as SourceName2, x2.ResultValue as ResultValue2'
  + @FROM + @Where + ' and x.ResultValue IS NOT NULL  ORDER BY S.CREATEDSTAMP DESC'

--print @Sql
EXEC (@SQL)

END