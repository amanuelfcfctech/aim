CREATE PROCEDURE [dbo].[usp_Report_SamplesByOwnerLocation]

	  @FromDate					VARCHAR(23)
	, @ToDate					VARCHAR(23)		= NULL
	, @AIMOwnerID				INT				= NULL
	, @AIMOwnerLocationID		INT				= NULL
	, @AIMSupplierID			INT				= NULL
	, @AIMSupplierLocationID	INT				= NULL
	, @AIMMaterialID			INT				= NULL
	, @CFCUserID				INT				= NULL

-- exec usp_Report_SamplesByOwnerLocation '2015-06-15', '', NULL, 2, NULL, NULL, NULL 

AS
BEGIN

SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

--10/17/23
--DECLARE @SQL			  VARCHAR(6000) = ''
--DECLARE @Where		  VARCHAR(1000) = ''
DECLARE @SQL			  VARCHAR(MAX) = ''
DECLARE @Where			  VARCHAR(MAX) = ''


SET @Where = ' WHERE S.SampleDate >= ''' + @FromDate + ''' '

if @ToDate <> '' 
	SET @Where = @Where + ' and S.SampleDate <=  ''' + @ToDate + ''' '
if @AIMOwnerID <> 0
	SET @Where = @Where + ' and S.AIMOwnerID =  ' + Cast(@AIMOwnerID as varchar)
if @AIMOwnerLocationID <> 0
	SET @Where = @Where + ' and S.AIMOwnerLocationID =  ' + Cast(@AIMOwnerLocationID as varchar) 
if @AIMSupplierID <> 0
	SET @Where = @Where + ' and S.AIMSourceID =  ' + Cast(@AIMSupplierID as varchar)
if @AIMSupplierLocationID <> 0
	SET @Where = @Where + ' and S.AIMSourceLocationID =  ' + Cast(@AIMSupplierLocationID as varchar)
if @AIMMaterialID <> 0
	SET @Where = @Where + ' and S.AIMMaterialID =  ' + Cast(@AIMMaterialID as varchar)


SET @SQL = 'SELECT 

	S.SampleID	
	, S.SampleDate
	, S.SampleCode
	, S.AlternateCode
	, S.Notes
	, S.AIMOwnerID
	, S.AIMOwnerLocationID
	, AOL.LocationName as OwnerLocationName
	, S.AIMSourceID
	, S.AIMSourceLocationID
	, AMS.SourceName as SupplierName
	, AMSL.SourceLocationName as SupplierLocationName
	, S.AIMMaterialID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, S.ContractNumber
	, S.PoNumber
	, S.InvoiceNumber
	, S.TicketNumber
	, S.LoadNumber
	, S.BillOfLoad
	, S.VehicleNumber
	, S.LotNumber
		
		
FROM dbo.Sample S with (nolock)
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
LEFT OUTER JOIN
	dbo.AIMMaterial AM 
		ON S.AIMMaterialID = AM.AIMMaterialID

'
SET @SQL = @SQL + @Where

SET @SQL = @SQL + '
 	
 ORDER BY AOL.LocationName, S.SampleDate, S.SampleCode	'

--print @sql
EXEC (@SQL)



END