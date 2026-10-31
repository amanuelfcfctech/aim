CREATE  PROCEDURE [dbo].[usp_Report_SampleAnalysisRequestForm]
		  @FromDate					DATE		= NULL
		, @ToDate					DATE		= NULL
		, @DataSourceTypeID			INT
		, @CFCUserID				INT			= NULL
		, @AIMMaterialID			INT			= NULL
		, @SupplierID				INT			= NULL
		
-- exec usp_Report_SampleAnalysisRequestForm '2023-10-27', '2023-10-27', 15, 1, 0
		
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL			VARCHAR(MAX) = ''
DECLARE @Where			VARCHAR(MAX) = ''
DECLARE @WhereValue		VARCHAR(MAX) = ''
Declare @SampleID		INT
declare @AnalysisString varchar(500)

if @ToDate = ''
	BEGIN
		SET @ToDate = null
	END

SET @Where = ' WHERE DST.DataSourceTypeID = ' + CONVERT(VARCHAR, @DataSourceTypeID) 

if @CFCUserID <> 0
	BEGIN
		SET @Where += ' AND S.CFCUserID = ' + CONVERT(VARCHAR, @CFCUserID)
	END

IF @FromDate IS NOT NULL
	BEGIN
		SET @Where += ' AND S.CreatedStamp >= ''' + CONVERT(VARCHAR, @FromDate) + ''''	
	END

IF @ToDate IS NOT NULL
	BEGIN
		SET @Where += ' AND S.CreatedStamp <= ''' + CONVERT(VARCHAR, @ToDate) + ' 23:59:59.999'''	
	END

IF @AIMMaterialID > 0
	BEGIN
		SET @Where += ' AND AM.AIMMaterialID = ' + CONVERT(VARCHAR, @AIMMaterialID) 
	END

IF @SupplierID > 0
	BEGIN
		SET @Where += ' AND AMS.AIMSourceID = ' + CONVERT(VARCHAR, @SupplierID) 
	END
------------------------------------------------------------------------------------
-- Put SampleID and Sample Test Type into a temp table to use below
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '#TestType%')
	DROP TABLE #TestType
CREATE TABLE #TestType (SampleID int, AnalysisString varchar(500))

SET @SQL = ' DECLARE SampleCur scroll cursor for
SELECT S.SampleID
FROM 
	dbo.Sample S 
	INNER JOIN 
		dbo.SampleTestTypeResult STTR
			ON STTR.SampleID = S.SampleID 
	INNER JOIN
		dbo.SampleTestTypeDetail STTD
			ON STTD.SampleTestTypeDetailID = STTR.SampleTestTypeDetailID
	INNER JOIN 
		dbo.AIMMaterial AM
			ON AM.AIMMaterialID = S.AIMMaterialID 
	INNER JOIN	
		dbo.DataSourceType DST
			ON DST.DataSourceTypeID = STTD.DataSourceTypeID 
	INNER JOIN
		dbo.AIMSource AMS
			ON AMS.AIMSourceID = S.AIMSourceID
	INNER JOIN
		dbo.AIMSourceLocation AMSL
			ON AMSL.AIMSourceID = S.AIMSourceID
			' 
+ @Where	
EXEC (@SQL)

open SampleCur
	fetch next from SampleCur into @SampleID
		while @@fetch_status = 0
			
			begin
				declare tempcur scroll cursor for 
				SELECT
					STUFF(( Select '; ' + STT.SampleTestTypeName 
					FROM dbo.SampleTestTypeResult STTR	
					INNER JOIN dbo.SampleTestTypeDetail STTD ON STTD.SampleTestTypeDetailID = STTR.SampleTestTypeDetailID
					INNER JOIN dbo.SampleTestType STT ON STTD.SampleTestTypeID = STT.SampleTestTypeID
					WHERE sttr.isincluded = 1 and sttr.isactive = 1 and STTR.Sampleid = @SampleID and STTD.DataSourceTypeID = @DataSourceTypeID
					GROUP BY STT.SampleTestTypeName
					FOR XML PATH('')), 1, 1, '') as AnalysisString
				FROM dbo.SampleTestTypeResult STTR
				
				open tempcur
				fetch next from tempcur into @AnalysisString
				if @@FETCH_STATUS = 0
					Begin								
						INSERT into #TestType (SampleID, AnalysisString) Select @SampleID, @AnalysisString	
					fetch next from tempcur into @AnalysisString
					end
			close tempcur
			deallocate tempcur			
			fetch next from SampleCur into @SampleID
		end 
	close SampleCur
	deallocate SampleCur
------------------------------------------------------------------------------------

SET @SQL = 'SELECT 
	  S.SampleID
	, CASE WHEN LEN(S.SampleCode) > 0 THEN S.SampleCode ELSE CONVERT(NVARCHAR, S.SampleID) END AS SampleCode 		
	, S.SampleDate
	, S.CreatedStamp
	, AM.AIMMaterialID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, DST.DataSourceTypeID
	, DST.DataSourceTypeCode
	, DST.Name as DataSourceName	
	, T.AnalysisString
	, AMS.AIMSourceID as SupplierID
	, AMS.SourceName as Supplier
	, AMSL.SourceLocationName as SupplierLocation
 FROM 
	dbo.Sample S 
	INNER JOIN 
		dbo.SampleTestTypeResult STTR
			ON STTR.SampleID = S.SampleID 
	INNER JOIN
		dbo.SampleTestTypeDetail STTD
			ON STTD.SampleTestTypeDetailID = STTR.SampleTestTypeDetailID
	INNER JOIN 
		dbo.AIMMaterial AM
			ON AM.AIMMaterialID = S.AIMMaterialID 
	INNER JOIN	
		dbo.DataSourceType DST
			ON DST.DataSourceTypeID = STTD.DataSourceTypeID
	INNER JOIN
		dbo.AIMSource AMS
			ON AMS.AIMSourceID = S.AIMSourceID
	INNER JOIN
		dbo.AIMSourceLocation AMSL
			ON AMSL.AIMSourceLocationID = S.AIMSourceLocationID
	LEFT OUTER JOIN
		#TestType T 
			ON S.SampleID = T.SampleID 
			
			' 
+ @Where +  
' GROUP BY
	  S.SampleID
	, S.SampleCode 		
	, S.SampleDate
	, S.CreatedStamp
	, AM.AIMMaterialID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, DST.DataSourceTypeID
	, DST.DataSourceTypeCode
	, DST.[Name]
	, T.AnalysisString
	, AMS.AIMSourceID 
	, AMS.SourceName
	, AMSL.SourceLocationName 
ORDER BY  
	S.SampleCode
'
EXEC (@SQL)
--print @SQL

END