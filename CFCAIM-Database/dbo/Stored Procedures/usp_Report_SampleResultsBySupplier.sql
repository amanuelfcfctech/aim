CREATE PROCEDURE [dbo].[usp_Report_SampleResultsBySupplier]

	  @FromDate				VARCHAR(23)
	, @ToDate				VARCHAR(23)		= NULL
	, @OwnerID				INT				= NULL
	, @OwnerLocationID		INT				= NULL
	, @SupplierID			INT				= NULL
	, @SupplierLocationID	INT				= NULL
	, @CFCUserID			INT				= NULL

-- exec usp_Report_SampleResultsBySupplier '2015-06-15', '', NULL, NULL, 53, NULL, NULL 

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @SQL			  VARCHAR(6000) = ''
DECLARE @Where			  VARCHAR(1000) = ''

SET @Where = ' WHERE S.SampleDate >= ''' + @FromDate + ''' '

if @ToDate <> '' 
	SET @Where = @Where + ' and S.SampleDate <=  ''' + @ToDate + ''' '
if @OwnerID <> 0
	SET @Where = @Where + ' and S.AIMOwnerID =  ' + Cast(@OwnerID as varchar)
if @OwnerLocationID <> 0
	SET @Where = @Where + ' and S.AIMOwnerLocationID =  ' + Cast(@OwnerLocationID as varchar) 
if @SupplierID <> 0
	SET @Where = @Where + ' and S.AIMSourceID =  ' + Cast(@SupplierID as varchar)
if @SupplierLocationID <> 0
	SET @Where = @Where + ' and S.AIMSourceLocationID =  ' + Cast(@SupplierLocationID as varchar)

SET @Where = @Where + ' and SR.ResultValue IS NOT NULL '

SET @SQL = 'SELECT 

	S.SampleID	
	, S.SampleDate
	, S.SampleCode
	, S.Notes
	, AOL.LocationName as OwnerLocationName
	, AMS.SourceName as SupplierName
	, AMSL.SourceLocationName as SupplierLocationName
	, SR.ResultValue 
	, SR.AnalyzerNutrientCode
	, AN.AnalyzerNutrientName 
	
		
FROM dbo.sample S with (nolock)
INNER JOIN 
	dbo.sampleresult SR 
		on S.sampleid = SR.sampleid
INNER JOIN 
	dbo.AnalyzerNutrient AN 
		on SR.AnalyzerNutrientID = AN.AnalyzerNutrientID
LEFT OUTER JOIN
	dbo.AIMOwner AO with (nolock)
		ON S.AIMOwnerID = AO.AIMOwnerID
LEFT OUTER JOIN	
	dbo.AIMOwnerLocation AOL with (nolock)
		ON S.AIMOwnerID = AOL.AIMOwnerID and
		   S.AIMOwnerLocationID = AOL.AIMOwnerLocationID
LEFT OUTER JOIN
	dbo.AIMSource AMS with (nolock)
		ON S.AIMSourceID = AMS.AIMSourceID
LEFT OUTER JOIN	
	dbo.AIMSourceLocation AMSL with (nolock)
		ON S.AIMSourceID = AMSL.AIMSourceID and
		   S.AIMSourceLocationID = AMSL.AIMSourceLocationID

'
SET @SQL = @SQL + @Where

SET @SQL = @SQL + '
 	
 ORDER BY AMS.SourceName, AMSL.SourceLocationName, S.SampleDate, S.SampleCode	'

--print @sql
EXEC (@SQL)



END