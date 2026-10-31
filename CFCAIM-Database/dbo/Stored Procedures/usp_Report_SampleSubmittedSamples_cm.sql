CREATE PROCEDURE [dbo].[usp_Report_SampleSubmittedSamples_cm]

	  @FromDate				VARCHAR(23)
	, @ToDate				VARCHAR(23) = NULL
	, @SampleTestTypeID		INT = NULL
	, @MaterialTypeID		INT = NULL
	, @AIMMaterialID		INT = NULL
	, @CFCUserID			INT = NULL
	
	
--exec usp_Report_SampleSubmittedSamples_cm '2020-08-17 00:00:00.000', '2023-08-17 00:00:00.000', 2, 1, 0, 1



AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @SQL			  VARCHAR(6000) = ''
DECLARE @Where			  VARCHAR(1000) = ''

SET @Where = ' WHERE S.CreatedStamp >= ''' + @FromDate + ''' '
if @ToDate <> '' 
	SET @Where = @Where + ' and S.CreatedStamp <=  ''' + @ToDate + ''' '
if @SampleTestTypeID <> 0
	SET @Where = @Where + ' and	STTD.SampleTestTypeID = ' + Cast(@SampleTestTypeID as varchar)
if @SampleTestTypeID = 0
	SET @Where = @Where + ' and	STTD.SampleTestTypeID > 0 ' 

if @MaterialTypeID <> 0
	SET @Where = @Where + ' and AM.MaterialTypeID = ' + convert(varchar, @MaterialTypeID)
	
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##TestType%')
	DROP TABLE ##TestType
	
SELECT  
	STTD.SampleTestTypeID,
	STUFF((SELECT '; ' + DST.DataSourceTypeCode
	FROM SampleTestTypeResults STTR
	INNER JOIN dbo.SampleTestTypeDetail STTD on STTR.SampleTestTypeDetailID = STTD.SampleTestTypeDetailID	
	INNER JOIN dbo.DataSourceType DST on STTD.DataSourceTypeID = DST.DataSourceTypeID     
	FOR XML PATH('')), 1, 1, '') DataSourceString
INTO ##TestType
FROM SampleTestTypeResults STTR
INNER JOIN SampleTestTypeDetail STTD on STTR.SampleTestTypeDetailID = STTD.SampleTestTypeDetailID
GROUP BY 
STTR.sampleid, STTD.SampleTestTypeID
ORDER BY 1

SET @SQL = 'SELECT 

	S.SampleID	
	, S.SampleCode
	, S.SampleDate
	, S.LoadNumber
	, AOL.LocationName as OwnerLocationName		
	, CASE WHEN AM.MaterialTypeID = 1 THEN 
		AM.AIMMaterialName Else '''' END as IngredientName 
	, CASE WHEN AM.MaterialTypeID = 2 THEN
		AM.AIMMaterialName Else '''' END as ProductName
	, STT.SampleTestTypeName	
	, S.CreatedStamp
	, convert(varchar,convert(date, S.CreatedStamp)) as CreatedStampGroup
	, S.Notes 
	, T.DataSourceString
		
FROM dbo.Sample S 
LEFT OUTER JOIN 
	dbo.SampleTestTypeResults STTR
		ON S.SampleID = STTR.SampleID
LEFT OUTER JOIN
	dbo.SampleTestTypeDetail STTD 
		ON STTR.SampleTestTypeDetailID = STTD.SampleTestTypeDetailID
LEFT OUTER JOIN
	dbo.SampleTestType STT
		ON STTD.SampleTestTypeID = STT.SampleTestTypeID
LEFT OUTER JOIN
	##TestType T 
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
	, S.SampleDate
	, AOL.LocationName	
	, STT.SampleTestTypeName
	, AMS.SourceName
	, AMSL.SourceLocationName 
	, AM.AIMMaterialName
	, AM.MaterialTypeID
	, S.CreatedStamp
	, S.Notes
	, S.LoadNumber
	, T.DataSourceString
	
 ORDER BY S.CreatedStamp DESC, S.SampleCode	DESC'



--print @sql
EXEC (@SQL)

END