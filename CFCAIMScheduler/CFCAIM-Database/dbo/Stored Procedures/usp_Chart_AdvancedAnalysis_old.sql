create PROCEDURE [dbo].[usp_Chart_AdvancedAnalysis_old]
	@AIMMaterialID INT = NULL
	, @AIMNutrientID INT = NULL	
	, @AIMNutrientID2 INT = NULL	
	, @AIMOwnerID INT = NULL
	, @AIMOwnerLocationID INT = NULL
	, @AIMSourceID INT = NULL
	, @AIMSourceLocationID INT = NULL
	, @TicketNumber NVARCHAR(20)
	, @SampleIDFrom INT = NULL
	, @SampleIDTo INT = NULL
	, @StartDate Date = NULL
	, @EndDate Date = NULL	


--exec usp_Chart_AdvancedAnalysis 1, 2, 4, 0, 0, 0, 0, '', 0, 0, '20151201', '20160111'

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SELECT			VARCHAR(1000) = ''
DECLARE @SELECT2			VARCHAR(1000) = ''
DECLARE @FROM			VARCHAR(1000) = ''
DECLARE @SQL			VARCHAR(6000) = ''
DECLARE @SQL2			VARCHAR(6000) = ''
DECLARE @Where			VARCHAR(1000) = ''

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
IF Len(@SampleIDFrom) = 0
	BEGIN
		SET @SampleIDFrom = NULL
	END
IF Len(@SampleIDTo) = 0
	BEGIN
		SET @SampleIDTo = NULL
	END

----------------------------------------------
SET @SELECT = 'SELECT '
If @StartDate IS NOT NULL
	SET @SELECT += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
If @EndDate is NOT NULL
	SET @SELECT += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
SET @SELECT += ' 
	   S.SampleID, SampleDate, x.AimNutrientID, AN.AimNutrientName, ResultValue, S.CreatedStamp '
	   
SET @SELECT2 = 'SELECT '
If @StartDate IS NOT NULL
	SET @SELECT2 += '''' + convert(varchar,@StartDate) + ''' as FromDate, '
If @EndDate is NOT NULL
	SET @SELECT2 += '''' + convert(varchar,@EndDate) + ''' as ToDate, '
SET @SELECT2 += ' 
	   S.SampleID, SampleDate, x.AimNutrientID, AN.AimNutrientName, ResultValue, S.CreatedStamp '

----------------------------------------------
SET @Where = 'WHERE AN.IsActive = 1 '
if @AIMMaterialID IS NOT NULL
	SET @Where += ' and S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' '			
if (@SampleIDFrom IS NOT NULL and @SampleIDFrom <> 0)
	SET @Where += ' and S.SampleID >= ' + Cast(@SampleIDFrom as varchar) + ' '
if (@SampleIDTo IS NOT NULL	and @SampleIDTo <> 0)
	SET @Where += ' and S.SampleID <= ' + Cast(@SampleIDTo as varchar) + ' '
if @AIMSourceID IS NOT NULL
	SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMSourceID as varchar) + ' '			
if @AIMOwnerLocationID IS NOT NULL
	SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '			
if @AIMSourceID IS NOT NULL
	SET @Where += ' and S.AIMSourceID = ' + Cast(@AIMSourceID as varchar) + ' '			
if @AIMSourceLocationID IS NOT NULL
	SET @Where += ' and S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID as varchar) + ' '				
if @TicketNumber IS NOT NULL
	SET @Where += ' and S.TicketNumber = ' + Cast(@TicketNumber as varchar) + ' '	
If @StartDate IS NOT NULL
	SET @Where += ' and S.SampleDate >= ''' + convert(varchar,@StartDate) + ''' '
If @EndDate IS NOT NULL
	SET @Where += ' and S.SampleDate <= ''' + convert(varchar,@EndDate) + ''' '

----------------------------------------------
SET @FROM = 'FROM 
	dbo.Sample S with(nolock)
INNER JOIN 
	dbo.SampleResult sr with(nolock)
		ON S.SampleID = sr.SampleID
INNER JOIN 
	dbo.AIMNutrientRefAnalyzerNutrient x with(nolock)
		ON sr.AnalyzerNutrientID = x.AnalyzerNutrientID
INNER JOIN 
	dbo.aimnutrient AN with(nolock)
		ON x.AIMNutrientID = AN.AIMNutrientID
INNER JOIN 
	dbo.AIMMaterialRefAnalyzerMaterial AMRAM with(nolock)
		ON s.AIMMaterialID = AMRAM.AIMMaterialID
INNER JOIN 
	dbo.AIMMaterial AM with(nolock)
		ON AMRAM.AIMMaterialID = AM.AIMMaterialID
'
----------------------------------------------
SET @SQL = @Select + @From + @Where + ' and AN.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar) 
	+ ' UNION '
	+ @Select2 + @From + @Where +  ' and AN.AIMNutrientID = ' + Cast(@AIMNutrientID2 as varchar) + ' ' +

'ORDER BY S.CREATEDSTAMP DESC'

--print @Sql
EXEC (@SQL)

END