CREATE PROCEDURE [dbo].[usp_Report_SampleSubmittedSamples]

	  @FromDate				VARCHAR(23)
	, @ToDate				VARCHAR(23) = NULL
	, @SampleTestTypeID		INT = NULL				
	, @UseSampleDate		BIT = NULL
	, @CFCUserID			INT = NULL
	, @AIMOwnerID			INT = NULL
	, @AIMOwnerLocationID	INT = NULL
	, @AIMMaterialID		INT = NULL				--added 1/15/24 (Maschhoffs found)
	, @MaterialTypeID		INT = NULL				--added back in 1/15/24
		
--exec usp_Report_SampleSubmittedSamples '2023-10-17 00:00:00.000', '2024-01-15 23:59:59.999', 3, 1, 1, 0, 0, 386, 0


AS
BEGIN

SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
declare @CurTestTypeID int

declare @DataSourceString varchar(MAX)
DECLARE @SQL			  VARCHAR(MAX) = ''
DECLARE @Where			  VARCHAR(MAX) = ''


IF @AIMOwnerID = 0
	BEGIN
		SET @AIMOwnerID = NULL
	END
IF @AIMOwnerLocationID = 0
	BEGIN
		SET @AIMOwnerLocationID = NULL
	END

--1/15/24
IF @AIMMaterialID = 0
	BEGIN
		SET @AIMMaterialID = NULL
	END

------------------------------------------------------------------
if @UseSampleDate = 1
	SET @Where = ' WHERE S.SampleDate >= ''' + @FromDate + ''' '
else
	SET @Where = ' WHERE S.CreatedStamp >= ''' + @FromDate + ''' '
	   
if @ToDate <> '' 
	Begin
	if @UseSampleDate = 1
		SET @Where = @Where + ' and S.SampleDate <=  ''' + @ToDate + ''' '
	else
		SET @Where = @Where + ' and S.CreatedStamp <=  ''' + @ToDate + ''' '
	End

if @CFCUserID <> 0
	BEGIN
		SET @Where += ' AND S.CFCUserID = ' + CONVERT(VARCHAR, @CFCUserID)
	END

IF @AIMOwnerID IS NOT NULL
	SET @Where += ' and S.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar) + ' '			
IF @AIMOwnerLocationID IS NOT NULL
	SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '	

--if @SampleTestTypeID <> 0
--	SET @Where = @Where + ' and	STTD.SampleTestTypeID = ' + Cast(@SampleTestTypeID as varchar)
if @SampleTestTypeID = 0
	SET @Where = @Where + ' and	STTD.SampleTestTypeID > 0 ' 
if @MaterialTypeID <> 0
	SET @Where = @Where + ' and AM.MaterialTypeID = ' + convert(varchar, @MaterialTypeID)	
if @SampleTestTypeID <> 0
	SET @Where = @Where + ' and STTD.SampleTestTypeID = ' + convert(varchar, @SampleTestTypeID)
	SET @Where = @Where + ' and STTD.IsActive = 1 and STTR.IsActive = 1'
if @AIMMaterialID <> 0
	SET @Where = @Where + ' and AM.AIMMaterialID = ' + convert(varchar, @AIMMaterialID)	

	
------------------------------------------------------------------
-- Put all the test types and datasource strings into a temp table
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '#TestTypes%')
	DROP TABLE #TestTypes
CREATE TABLE #TestTypes (SampleTestTypeID int, DataSourceString varchar(500))

declare TestTypecur scroll cursor for
	SELECT STT.SampleTestTypeID
	FROM dbo.SampleTestType STT
	open testTypecur
	fetch next from TestTypecur into @CurTestTypeID
	while @@fetch_status = 0
		begin
			declare tempcur scroll cursor for 
			SELECT
			STUFF((SELECT '; ' + DST.DataSourceTypeCode
				FROM SampleTestTypeResult STTR
				INNER JOIN dbo.SampleTestTypeDetail STTD on STTR.SampleTestTypeDetailID = STTD.SampleTestTypeDetailID	
				INNER JOIN dbo.DataSourceType DST on STTD.DataSourceTypeID = DST.DataSourceTypeID  
				WHERE STTD.SampleTestTypeID = @CurTestTypeID and STTD.IsActive = 1 and STTR.IsActive = 1
				GROUP BY dst.DataSourceTypeCode				
				FOR XML PATH('')), 1, 1, '') DataSourceString
			FROM SampleTestTypeDetail STTD
			WHERE STTD.SampleTestTypeID = @CurTestTypeID and STTD.IsActive = 1

			open tempcur
			fetch next from tempcur into @DataSourceString
			if @@FETCH_STATUS = 0
				begin
					insert into #TestTypes (SampleTestTypeID, DataSourceString) Select @CurTestTypeID, @DataSourceString						
				fetch next from tempcur into @DataSourceString
				end
			close tempcur
			deallocate tempcur			
			fetch next from TestTypecur into @CurTestTypeID
		end 
	close TestTypecur
	deallocate TestTypecur

------------------------------------------------------------------
SET @SQL = 'SELECT 

	S.SampleID	
	, S.SampleCode	
	, S.LoadNumber
	, AOL.LocationName as OwnerLocationName		
	, AM.AIMMaterialName as IngredientName
	, STT.SampleTestTypeName	'

if @UseSampleDate = 1
	Begin
	SET @SQL += '
		, S.SampleDate
		, S.SampleDate as CreatedStamp
		, convert(varchar,convert(date, S.SampleDate)) as CreatedStampGroup '
	End
Else
	Begin
	SET @SQL += '
		, S.CreatedStamp as SampleDate
		, S.CreatedStamp
		, convert(varchar,convert(date, S.CreatedStamp)) as CreatedStampGroup '
	End

SET @SQL += '
	, S.Notes 
	, T.DataSourceString 

FROM dbo.Sample S 
LEFT OUTER JOIN 
	dbo.SampleTestTypeResult STTR
		ON S.SampleID = STTR.SampleID
LEFT OUTER JOIN
	dbo.SampleTestTypeDetail STTD 
		ON STTR.SampleTestTypeDetailID = STTD.SampleTestTypeDetailID
LEFT OUTER JOIN
	dbo.SampleTestType STT
		ON STTD.SampleTestTypeID = STT.SampleTestTypeID 
LEFT OUTER JOIN
	#TestTypes T 
		ON STTD.SampleTestTypeID = T.SampleTestTypeID 
LEFT OUTER JOIN 
	dbo.AIMMaterial AM 
		ON S.AIMMaterialID = AM.AIMMaterialID
LEFT OUTER JOIN
	dbo.AIMOwner AMO 
		ON S.AIMOwnerID = AMO.AIMOwnerID
LEFT OUTER JOIN	
	dbo.AIMOwnerLocation AOL 
		ON S.AIMOwnerID = AOL.AIMOwnerID and
		   S.AIMOwnerLocationID = AOL.AIMOwnerLocationID
LEFT OUTER JOIN
	dbo.AIMSource AMS 
		ON S.AIMSourceID = AMS.AIMSourceID
LEFT OUTER JOIN	
	dbo.AIMSourceLocation AMSL 
		ON S.AIMSourceID = AMSL.AIMSourceID and
		   S.AIMSourceLocationID = AMSL.AIMSourceLocationID
'
SET @SQL = @SQL + @Where

SET @SQL = @SQL + '

 GROUP BY 	
	S.SampleID	
	, S.SampleCode	
	, AOL.LocationName	
	, STT.SampleTestTypeName
	, AMS.SourceName
	, AMSL.SourceLocationName 
	, AM.AIMMaterialName
	, AM.MaterialTypeID '

if @UseSampleDate = 1
	SET @SQL = @SQL + ', S.SampleDate '
else
	SET @SQL = @SQL + ', S.CreatedStamp '

SET @SQL = @Sql + '
	, S.Notes
	, S.LoadNumber
	, T.DataSourceString
	
 ORDER BY S.SampleCode '

--print @sql
EXEC (@SQL)

END