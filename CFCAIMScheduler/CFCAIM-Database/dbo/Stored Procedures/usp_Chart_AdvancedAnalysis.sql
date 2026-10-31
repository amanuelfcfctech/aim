CREATE PROCEDURE [dbo].[usp_Chart_AdvancedAnalysis]
	@AIMMaterialID INT = NULL
	, @AIMNutrientID INT = NULL	
	, @AIMNutrientID2 INT = NULL	

	, @AIMOwnerID INT = NULL
	, @AIMOwnerLocationID INT = NULL

	, @AIMSourceID INT = NULL
	, @AIMSourceLocationID INT = NULL

	, @TicketNumber NVARCHAR(20)
	, @SampleCodeFrom NVARCHAR(20) = NULL
	, @SampleCodeTo NVARCHAR(20) = NULL
	, @StartDate Date = NULL
	, @EndDate Date = NULL	

	-- 6/28/21
	, @AIMSourceID2 INT = NULL
	, @AIMSourceLocationID2 INT = NULL
	, @AIMSourceID3 INT = NULL
	, @AIMSourceLocationID3 INT = NULL
	, @AIMSourceID4 INT = NULL
	, @AIMSourceLocationID4 INT = NULL	
	
	-- 7/12/21	
	, @AIMOwnerID2 INT = NULL
	, @AIMOwnerLocationID2 INT = NULL

	, @AIMOwnerID3 INT = NULL
	, @AIMOwnerLocationID3 INT = NULL

	, @AIMOwnerID4 INT = NULL
	, @AIMOwnerLocationID4 INT = NULL	

	, @SupplierOwnerOption INT = NULL		-- 1=Supplier only;  2=Owner only;  3=Both Supplier and Owner

	
--exec usp_Chart_AdvancedAnalysis 376, 49, 11,  1,1,  0,0, '','','', '2021-01-01', '2021-09-08', 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 2

AS
BEGIN
SET NOCOUNT ON;

DECLARE @FROM			VARCHAR(3000) = ''
DECLARE @SQL			VARCHAR(6000) = ''
DECLARE @Where			VARCHAR(1000) = ''
DECLARE @AndOr			Int = 0
Declare @WhereSuppliers	VARCHAR(1000) = ''
Declare @WhereOwners	VARCHAR(1000) = ''

------------------------------------------------------------------
--9/8/21 - Line1 deals with @AIMOwnerID and @AIMOwnerLocationID
--		   Line2 deals with @AIMOwnerID2 and @AIMOwnerLocationID2
--		   Etc.
Declare @Line1			varchar(1000) = ''
Declare @Line2			varchar(1000) = ''
Declare @Line3			varchar(1000) = ''
Declare @Line4			varchar(1000) = ''
------------------------------------------------------------------

IF @AIMMaterialID = 0
	BEGIN
		SET @AIMMaterialID = NULL
	END
IF @AIMNutrientID = 0
	BEGIN
		SET @AIMNutrientID = NULL
	END
IF @AIMNutrientID2 = 0
	BEGIN
		SET @AIMNutrientID2 = NULL
	END
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

--6/28/21 ------------------------------------
IF @AIMSourceID2 = 0
	BEGIN
		SET @AIMSourceID2 = NULL
	END
IF @AIMSourceLocationID2 = 0
	BEGIN
		SET @AIMSourceLocationID2 = NULL
	END

IF @AIMSourceID3 = 0
	BEGIN
		SET @AIMSourceID3 = NULL
	END
IF @AIMSourceLocationID3 = 0
	BEGIN
		SET @AIMSourceLocationID3 = NULL
	END

IF @AIMSourceID4 = 0
	BEGIN
		SET @AIMSourceID4 = NULL
	END
IF @AIMSourceLocationID4 = 0
	BEGIN
		SET @AIMSourceLocationID4 = NULL
	END

-- 7/9/21 --------------------------------
IF @AIMOwnerID2 = 0
	BEGIN
		SET @AIMOwnerID2 = NULL
	END
IF @AIMOwnerLocationID2 = 0
	BEGIN
		SET @AIMOwnerLocationID2 = NULL
	END
IF @AIMOwnerID3 = 0
	BEGIN
		SET @AIMOwnerID3 = NULL
	END
IF @AIMOwnerLocationID3 = 0
	BEGIN
		SET @AIMOwnerLocationID3 = NULL
	END
IF @AIMOwnerID4 = 0
	BEGIN
		SET @AIMOwnerID4 = NULL
	END
IF @AIMOwnerLocationID4 = 0
	BEGIN
		SET @AIMOwnerLocationID4 = NULL
	END
------------------------------------------

SET @Where = 'WHERE S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' '			
if (@SampleCodeFrom IS NOT NULL)
	SET @Where += ' and S.SampleCode >= ''' + Cast(@SampleCodeFrom as varchar) + ' '''
if (@SampleCodeTo IS NOT NULL)
	SET @Where += ' and S.SampleCode <= ''' + Cast(@SampleCodeTo as varchar) + ' '''

-- 7/12/21 commented ---------------------
--if @AIMOwnerID IS NOT NULL
--	SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar) + ' '			
--if @AIMOwnerLocationID IS NOT NULL
--	SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '				
------------------------------------------

if @TicketNumber IS NOT NULL
	SET @Where += ' and S.TicketNumber = ' + Cast(@TicketNumber as varchar) + ' '	
If @StartDate IS NOT NULL
	SET @Where += ' and S.SampleDate >= ''' + convert(varchar,@StartDate) + ''' '
If @EndDate IS NOT NULL
	SET @Where += ' and S.SampleDate <= ''' + convert(varchar,@EndDate) + ''' '


-- 7/9/21 --------------------------------	
if @SupplierOwnerOption = 2		--OwnerOnly
	Begin
		if @AIMOwnerID IS NOT NULL		
			Set @Line1 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID as varchar) + ' '	
		if @AIMOwnerLocationID IS NOT NULL	
			Set @Line1 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '	
		if @Line1 <> ''
			Set @Line1 = '(' + @Line1 + ')'	
		if @AIMOwnerID2 IS NOT NULL		
			Set @Line2 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID2 as varchar) + ' '	
		if @AIMOwnerLocationID2 IS NOT NULL	
			Set @Line2 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID2 as varchar) + ' '	
		if @Line2 <> ''
			Set @Line2 = '(' + @Line2 + ')'
		if @AIMOwnerID3 IS NOT NULL		
			Set @Line3 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID3 as varchar) + ' '	
		if @AIMOwnerLocationID3 IS NOT NULL	
			Set @Line3 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID3 as varchar) + ' '	
		if @Line3 <> ''
			Set @Line3 = '(' + @Line3 + ')'	
		if @AIMOwnerID4 IS NOT NULL		
			Set @Line4 = ' S.AIMOwnerID = '+ Cast(@AIMOwnerID4 as varchar) + ' '	
		if @AIMOwnerLocationID4 IS NOT NULL	
			Set @Line4 += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID4 as varchar) + ' '	
		if @Line4 <> ''
			Set @Line4 = '(' + @Line4 + ')'
	End 

If @AIMSourceID is not null OR @AIMSourceID2 is not null OR @AIMSourceID3 is not null OR @AIMSourceID4 is not null 
	SET @WhereSuppliers = ' AND ( '

If @AIMSourceID is not null
	Begin					
		SET @WhereSuppliers += ' ((S.AIMSourceID = ' + Cast(@AIMSourceID as varchar) + ' '
		SET @AndOr = 1
		If @AIMSourceLocationID is not null
			SET @WhereSuppliers += ' and S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID as varchar) + ''

			--7/12/21
			If @AIMOwnerID is not null
				Begin					
					SET @WhereSuppliers += ' and (S.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar) + ' '				
					If @AIMOwnerLocationID is not null
						SET @WhereSuppliers += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ''
					SET @WhereSuppliers += ') '
				End	

		SET @WhereSuppliers += ')) '
	End	

if @AIMSourceID2 IS NOT NULL
	Begin
		if @AndOr = 1
			SET @WhereSuppliers += ' or '
		SET @WhereSuppliers += ' ((S.AIMSourceID = ' + Cast(@AIMSourceID2 as varchar) + ' '
		SET @AndOr = 1
		If @AIMSourceLocationID2 is not null
			SET @WhereSuppliers += ' and S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID2 as varchar) + ''

			--7/12/21
			If @AIMOwnerID2 is not null
				Begin					
					SET @WhereSuppliers += ' and (S.AIMOwnerID = ' + Cast(@AIMOwnerID2 as varchar) + ' '				
					If @AIMOwnerLocationID2 is not null
						SET @WhereSuppliers += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID2 as varchar) + ''
					SET @WhereSuppliers += ') '
				End	

		SET @WhereSuppliers += ')) '
	End

if @AIMSourceID3 IS NOT NULL
	Begin
		if @AndOr = 1
			SET @WhereSuppliers += ' or '
		SET @WhereSuppliers += ' ((S.AIMSourceID = ' + Cast(@AIMSourceID3 as varchar) + ' '
		SET @AndOr = 1
		If @AIMSourceLocationID3 is not null
			SET @WhereSuppliers += ' and S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID3 as varchar) + ''

			--7/12/21
			If @AIMOwnerID3 is not null
				Begin					
					SET @WhereSuppliers += ' and (S.AIMOwnerID = ' + Cast(@AIMOwnerID3 as varchar) + ' '				
					If @AIMOwnerLocationID3 is not null
						SET @WhereSuppliers += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID3 as varchar) + ''
					SET @WhereSuppliers += ') '
				End	

		SET @WhereSuppliers += ')) '

	End

if @AIMSourceID4 IS NOT NULL
	Begin
		if @AndOr = 1
			SET @WhereSuppliers += ' or '
		SET @WhereSuppliers += ' ((S.AIMSourceID = ' + Cast(@AIMSourceID4 as varchar) + ' '
		SET @AndOr = 1
		If @AIMSourceLocationID4 is not null
			SET @WhereSuppliers += ' and S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID4 as varchar) + ''

			--7/12/21
			If @AIMOwnerID4 is not null
				Begin					
					SET @WhereSuppliers += ' and (S.AIMOwnerID = ' + Cast(@AIMOwnerID4 as varchar) + ' '				
					If @AIMOwnerLocationID4 is not null
						SET @WhereSuppliers += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID4 as varchar) + ''
					SET @WhereSuppliers += ') '
				End	

		SET @WhereSuppliers += ')) '

	End

If len(@WhereSuppliers) > 1 
	SET @WhereSuppliers += ') '


if @SupplierOwnerOption = 2
	Begin
		SET @AndOr = 0
		Set @WhereOwners += ' and '
		if @Line1 <> ''				
			SET @WhereOwners += @Line1
			SET @AndOr = 1
		if @Line2 <> ''
			if @AndOr = 1
				SET @WhereOwners += ' or '
			SET @WhereOwners += @Line2
			SET @AndOr = 1
		if @Line3 <> ''
			if @AndOr = 1
				SET @WhereOwners += ' or '
			SET @WhereOwners += @Line3
			SET @AndOr = 1
		if @Line4 <> ''
			if @AndOr = 1
				SET @WhereOwners += ' or '
			SET @WhereOwners += @Line4
	End
	


SET @FROM = ' FROM dbo.Sample S with(nolock) 
	LEFT JOIN (Select S.SampleID, 
	S.SampleCode, S.AlternateCode,
	x.AimNutrientID, AN.AimNutrientName, ResultValue  FROM dbo.Sample S with(nolock) 
	INNER JOIN dbo.SampleResult sr with(nolock) ON S.SampleID = sr.SampleID
	INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)	ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
	INNER JOIN dbo.aimnutrient AN with(nolock) ON x.AIMNutrientID = AN.AIMNutrientID '
	+ @Where + @WhereSuppliers + @WhereOwners +' and ResultValue is NOT NULL and AN.IsActive = 1 and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar)
	+ ') x ON x.SampleID = S.SampleID

	LEFT JOIN (Select S.SampleID, 
	S.SampleCode, S.AlternateCode,
	x.AimNutrientID, AN.AimNutrientName, ResultValue  FROM dbo.Sample S with(nolock) 
	INNER JOIN dbo.SampleResult sr with(nolock) ON S.SampleID = sr.SampleID
	INNER JOIN dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)	ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
	INNER JOIN dbo.aimnutrient AN with(nolock) ON x.AIMNutrientID = AN.AIMNutrientID '
	+ @Where + @WhereSuppliers + @WhereOwners + ' and ResultValue is NOT NULL and AN.IsActive = 1 and AN.AIMNutrientID = ' + Cast(@AIMNutrientID2 as varchar)
	+ ') x2 ON x2.SampleID = S.SampleID '

SET @SQL = 'SELECT '
If @StartDate IS NOT NULL
	SET @SQL += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
If @EndDate is NOT NULL
	SET @SQL += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
SET @SQL += ' 	S.SampleCode, S.AlternateCode,
				S.SampleID, SampleDate, x.AimNutrientID, x.AIMNutrientName, x.ResultValue, x2.AIMNutrientID as AIMNutrientID2, 
				x2.AIMNutrientName as AIMNutrientName2, x2.ResultValue as ResultValue2'
	+ @FROM + @Where + @WhereSuppliers + ' and x.ResultValue IS NOT NULL ORDER BY S.CREATEDSTAMP '

--print @Sql
EXEC (@SQL)

END