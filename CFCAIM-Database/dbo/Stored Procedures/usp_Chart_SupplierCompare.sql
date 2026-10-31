CREATE PROCEDURE [dbo].[usp_Chart_SupplierCompare]
	@AIMMaterialID INT = NULL
	, @AIMNutrientID INT = NULL	
	, @AIMSupplierID INT = NULL	
	, @AIMSupplierID2 INT = NULL		
	, @AIMSupplierID3 INT = NULL				-- added 5/21/20
	, @AIMSupplierID4 INT = NULL				-- added 5/21/20
	, @AIMOwnerID INT = NULL
	, @AIMOwnerLocationID INT = NULL	
	, @AIMSupplierLocationID INT = NULL
	, @TicketNumber NVARCHAR(20)
	, @SampleCodeFrom NVARCHAR(20) = NULL
	, @SampleCodeTo NVARCHAR(20) = NULL
	, @StartDate Date = NULL
	, @EndDate Date = NULL

	-- added 6/28/21
	, @AIMSupplierLocationID2 INT = NULL
	, @AIMSupplierLocationID3 INT = NULL
	, @AIMSupplierLocationID4 INT = NULL

--exec usp_Chart_SupplierCompare 96, 9, 8, 0, 8, 0, 0, 0, 0, '', '','', '20200501', '20200501'

-- for fse:
-- 5 = 'MBM50%'
-- 8 = 'Darling'
-- 9 = 'CBP'

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SELECT1		VARCHAR(1000) = ''
DECLARE @SELECT2		VARCHAR(1000) = ''
DECLARE @SELECT3		VARCHAR(1000) = ''		-- added 5/21/20
DECLARE @SELECT4		VARCHAR(1000) = ''		-- added 5/21/20
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

IF @AIMSupplierID3 = 0						-- added 5/21/20
	BEGIN
		SET @AIMSupplierID3 = NULL
	END
IF @AIMSupplierID4 = 0						-- added 5/21/20
	BEGIN
		SET @AIMSupplierID4 = NULL
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

-- 6/28/21 -----------------------------------
IF @AIMSupplierLocationID2 = 0
	BEGIN
		SET @AIMSupplierLocationID2 = NULL
	END
IF @AIMSupplierLocationID3 = 0
	BEGIN
		SET @AIMSupplierLocationID3 = NULL
	END
IF @AIMSupplierLocationID4 = 0
	BEGIN
		SET @AIMSupplierLocationID4 = NULL
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
--if @AIMSupplierLocationID IS NOT NULL
--	SET @Where += ' and S.AIMSourceLocationID = ' + Cast(@AIMSupplierLocationID as varchar) + ' '				
if @TicketNumber IS NOT NULL
	SET @Where += ' and S.TicketNumber = ' + Cast(@TicketNumber as varchar) + ' '	
If @StartDate IS NOT NULL
	SET @Where += ' and S.SampleDate >= ''' + convert(varchar,@StartDate) + ''' '
If @EndDate IS NOT NULL
	SET @Where += ' and S.SampleDate <= ''' + convert(varchar,@EndDate) + ''' '

----------------------------------------------
SET @SELECT1 = 'SELECT '
If @StartDate IS NOT NULL
	SET @SELECT1 += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
If @EndDate is NOT NULL
	SET @SELECT1 += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
SET @SELECT1 += 'S.SampleCode, S.AlternateCode, S.SampleID, SampleDate, AN.AimNutrientID, AIMNutrientName, S.AIMSourceID, SourceName, 
				sr.ResultValue, 0 AS ResultValue2, 0 AS ResultValue3, 0 as ResultValue4 '

----------------------------------------------
if @AIMSupplierID2 IS NOT NULL
Begin
	SET @SELECT2 = 'SELECT '
	If @StartDate IS NOT NULL
		SET @SELECT2 += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
	If @EndDate is NOT NULL
		SET @SELECT2 += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
	SET @SELECT2 += 'S.SampleCode, S.AlternateCode, S.SampleID, SampleDate, AN.AimNutrientID, AIMNutrientName, S.AIMSourceID, SourceName, 
					0 as ResultValue, sr.ResultValue as ResultValue2, 0 AS ResultValue3, 0 as ResultValue4 '
End
----------------------------------------------
if @AIMSupplierID3 IS NOT NULL
Begin
	SET @SELECT3 = 'SELECT '
	If @StartDate IS NOT NULL
		SET @SELECT3 += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
	If @EndDate is NOT NULL
		SET @SELECT3 += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
	SET @SELECT3 += 'S.SampleCode, S.AlternateCode, S.SampleID, SampleDate, AN.AimNutrientID, AIMNutrientName, S.AIMSourceID, SourceName, 
					0 as ResultValue, 0 as ResultValue2, sr.ResultValue as ResultValue3, 0 as ResultValue4 '
End

----------------------------------------------
if @AIMSupplierID4 IS NOT NULL
Begin
	SET @SELECT4 = 'SELECT '
	If @StartDate IS NOT NULL
		SET @SELECT4 += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
	If @EndDate is NOT NULL
		SET @SELECT4 += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
	SET @SELECT4 += 'S.SampleCode, S.AlternateCode, S.SampleID, SampleDate, AN.AimNutrientID, AIMNutrientName, S.AIMSourceID, SourceName, 
					0 as ResultValue, 0 as ResultValue2, 0 as ResultValue3, sr.ResultValue as ResultValue4 '
End

----------------------------------------------
SET @FROM = ' FROM dbo.Sample S with(nolock) 	
	INNER JOIN dbo.SampleResult sr with(nolock) ON S.SampleID = sr.SampleID
	INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)	ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
	INNER JOIN dbo.aimnutrient AN with(nolock) ON x.AIMNutrientID = AN.AIMNutrientID 
	INNER JOIN dbo.AIMSource AMS with (nolock) on S.AIMSourceID = AMS.AIMSourceID '	

----------------------------------------------

if @AIMSupplierID IS NOT NULL
Begin
	SET @SQL += @SELECT1 
	+ @FROM + @Where + ' and AN.IsActive = 1 and SR.IsActive = 1
					 and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) + ' 
					 and S.AIMSourceID = ' + Cast(@AIMSupplierID as varchar) + ''
	-- 6/28/21				
	if @AimSupplierLocationID IS NOT NULL 
		SET @SQL += ' and S.AIMSourceLocationID = ' + Cast(@AIMSupplierLocationID as varchar) + ''
End

if @AIMSupplierID2 IS NOT NULL
Begin
	SET @SQL += ' UNION ALL '
	SET @SQL += @SELECT2 
	+ @FROM + @Where + ' and AN.IsActive = 1 and SR.IsActive = 1
						and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) + ' 
						and S.AIMSourceID = ' + Cast(@AIMSupplierID2 as varchar) + ''
	-- 6/28/21				
	if @AimSupplierLocationID2 IS NOT NULL 
		SET @SQL += ' and S.AIMSourceLocationID = ' + Cast(@AIMSupplierLocationID2 as varchar) + ''

End

if @AIMSupplierID3 IS NOT NULL
Begin

	SET @SQL += ' UNION ALL '
	SET @SQL += @SELECT3 
	+ @FROM + @Where + ' and AN.IsActive = 1 and SR.IsActive = 1
						and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) + ' 
						and S.AIMSourceID = ' + Cast(@AIMSupplierID3 as varchar) + ''
	-- 6/28/21				
	if @AimSupplierLocationID3 IS NOT NULL 
		SET @SQL += ' and S.AIMSourceLocationID = ' + Cast(@AIMSupplierLocationID3 as varchar) + ''

End

if @AIMSupplierID4 IS NOT NULL
Begin
	SET @SQL += ' UNION ALL '
	SET @SQL += @SELECT4 
	+ @FROM + @Where + ' and AN.IsActive = 1 and SR.IsActive = 1
						and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) + ' 
						and S.AIMSourceID = ' + Cast(@AIMSupplierID4 as varchar) + ''
	-- 6/28/21				
	if @AimSupplierLocationID4 IS NOT NULL 
		SET @SQL += ' and S.AIMSourceLocationID = ' + Cast(@AIMSupplierLocationID4 as varchar) + ''

End

--print (@SQL)
EXEC (@SQL)

END