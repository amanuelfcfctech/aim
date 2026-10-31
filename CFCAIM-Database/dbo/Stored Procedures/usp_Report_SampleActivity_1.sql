CREATE PROCEDURE [dbo].[usp_Report_SampleActivity]

	  @FromDate				VARCHAR(23)
	, @ToDate				VARCHAR(23)
	, @CFCUserID			INT

	--4/26/22
	, @MaterialTypeID		INT
	
--exec usp_Report_SampleActivity '2018-01-01 00:00:00.000', '2018-03-31 00:00:00.000', 1, 2
--exec usp_Report_SampleActivity '2018-01-15 00:00:00.000', '2018-01-16 23:59:59.999', 1
--exec usp_Report_SampleActivity '2016-12-21 00:00:00.000', '2016-12-27 23:59:59.999', 1
--exec usp_Report_SampleActivity '12-20-2016 00:00:00.000', '', 1
--exec usp_Report_SampleActivity '', '', 1


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

SET @Where = ' WHERE S.CreatedStamp >= ''' + @FromDate + ''' '
if @ToDate <> '' 
	SET @Where = @Where + ' and S.CreatedStamp <=  ''' + @ToDate + ''' '

--4/26/22
SET @Where = @Where + ' and AM.MaterialTypeID = ' + convert(varchar, @MaterialTypeID)

SET @SQL = 'SELECT 

	S.SampleID	
	, S.SampleDate
	, S.SampleCode
	, AOL.LocationName as OwnerLocationName	
	, CASE WHEN AM.MaterialTypeID = 1 THEN 
		AM.AIMMaterialName Else '''' END as IngredientName 
	, CASE WHEN AM.MaterialTypeID = 2 THEN
		AM.AIMMaterialName Else '''' END as ProductName
	, S.CreatedStamp
	, convert(varchar,convert(date, S.CreatedStamp)) as CreatedStampGroup
		
FROM dbo.sample S with (nolock)
LEFT OUTER JOIN 
	dbo.AIMMaterial AM with (nolock) 
		ON S.AIMMaterialID = AM.AIMMaterialID
LEFT OUTER JOIN
	dbo.AIMOwner AMO with (nolock)
		ON S.AIMOwnerID = AMO.AIMOwnerID
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

 GROUP BY 	
	S.SampleID	
	, S.SampleDate
	, S.SampleCode
	, AOL.LocationName
	, AMS.SourceName
	, AMSL.SourceLocationName 
	, AM.AIMMaterialName
	, AM.MaterialTypeID
	, S.CreatedStamp
	
 ORDER BY S.CreatedStamp DESC, S.SampleCode	DESC'



--print @sql
EXEC (@SQL)

END