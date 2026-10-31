CREATE PROCEDURE [dbo].[usp_Report_SampleActivity_QualityControl]

	  @FromDate				VARCHAR(23)
	, @ToDate				VARCHAR(23)
	, @CFCUserID			INT
	, @MaterialTypeID		INT
	
--exec usp_Report_SampleActivity_QualityControl '2022-03-01 00:00:00.000', '2022-04-30 00:00:00.000', 1, 1
  
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

SET @Where = ' WHERE SQ.IsActive = 1 and S.CreatedStamp >= ''' + @FromDate + ''' '
if @ToDate <> '' 
	SET @Where = @Where + ' and S.CreatedStamp <=  ''' + @ToDate + ''' '
SET @Where = @Where + ' and AM.MaterialTypeID = ' + convert(varchar, @MaterialTypeID)

SET @SQL = 'SELECT 

	S.SampleID	
	, S.SampleDate
	, S.SampleCode	
	, SQP.SampleQualityParameterName as ParameterName
	, SQ.ResultValueNumeric as ResultValue
	, AM.AIMMaterialName
	, AOL.LocationName as OwnerLocationName	
	, CASE WHEN AM.MaterialTypeID = 1 THEN 
		AM.AIMMaterialName Else '''' END as IngredientName 
	, CASE WHEN AM.MaterialTypeID = 2 THEN
		AM.AIMMaterialName Else '''' END as ProductName
	, S.CreatedStamp
	, convert(varchar,convert(date, S.CreatedStamp)) as CreatedStampGroup
	, S.TicketNumber
		
FROM dbo.sample S with (nolock)
INNER JOIN 
	dbo.SampleQuality SQ 
		ON S.SampleID = SQ.SampleID
LEFT OUTER JOIN
	dbo.SampleQualityParameter SQP 
		ON SQ.SampleQualityParameterID = SQP.SampleQualityParameterID
LEFT OUTER JOIN 
	dbo.AIMMaterial AM with (nolock) 
		ON S.AIMMaterialID = AM.AIMMaterialID
LEFT OUTER JOIN	
	dbo.AIMOwnerLocation AOL with (nolock)
		ON S.AIMOwnerLocationID = AOL.AIMOwnerLocationID

'
SET @SQL = @SQL + @Where

SET @SQL = @SQL + '

 GROUP BY 	
	S.SampleID	
	, S.SampleDate
	, S.SampleCode
	, S.TicketNumber
	, SQP.SampleQualityParameterName
	, SQ.ResultValueNumeric
	, AOL.LocationName	
	, AM.AIMMaterialName
	, AM.MaterialTypeID
	, S.CreatedStamp
	
 ORDER BY S.CreatedStamp DESC, S.SampleCode	DESC'

--print @sql
EXEC (@SQL)

END