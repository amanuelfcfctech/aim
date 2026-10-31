CREATE PROCEDURE [dbo].[usp_SampleFurtherAnalysis_SEL_BeginDate_EndDate_DateField_DateSourceTypeID]
	  @BegDate			DATE
	, @EndDate			DATE
	, @DateField		INT
	, @DataSourceTypeID	INT
			
AS
BEGIN

-- 1 = Sample Date
-- 2 = Created Date
-- 3 = Process Date

--exec usp_SampleFurtherAnalysis_SEL_BeginDate_EndDate_DateField_DateSourceTypeID '20220901', '20220901', 1, 0

DECLARE @SQL				VARCHAR(MAX) = ''

SET @SQL = 'SELECT 
	SFA.SampleFurtherAnalysisID
	, SFA.SampleID
	, SFA.DataSourceTypeID
	, SFA.AIMMessageID
	, SFA.ProcessDate
	, SFA.UpdateStamp
	, SFA.CFCUserID
	, S.SampleCode
	, S.SampleDate
	, AM.Message 
	, DST.Name as DataSourceTypeName
	, U.UserName	

FROM 
	dbo.SampleFurtherAnalysis SFA 
	LEFT OUTER JOIN 
		dbo.Sample S
			ON SFA.SampleID = S.SampleID
	LEFT OUTER JOIN 
		dbo.AIMMessage AM
			ON AM.AIMMessageID = SFA.AIMMessageID
	LEFT OUTER JOIN
		dbo.DataSourceType DST
			ON SFA.DataSourceTypeID = DST.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON SFA.CFCUserID = U.CFCUserID
WHERE 
'
IF @DateField = 1 
	Begin
		SET @SQL += ' (S.SampleDate >= ''' + CAST(@BegDate as varchar)  + ' 00:00:00.000'''
		SET @SQL += ' AND S.SampleDate <= ''' + CAST(@EndDate as varchar) + ' 23:59:59.999'')'
	End
IF @DateField = 2
	Begin
		SET @SQL += ' (SFA.UpdateStamp >= ''' + CAST(@BegDate as varchar)  + ' 00:00:00.000'''
		SET @SQL += ' AND SFA.UpdateStamp <= ''' + CAST(@EndDate as varchar) + ' 23:59:59.999'')'
	End
IF @DateField = 3
	Begin
		SET @SQL += ' (SFA.ProcessDate >= ''' + CAST(@BegDate as varchar)  + ' 00:00:00.000'''
		SET @SQL += ' AND SFA.ProcessDate <= ''' + CAST(@EndDate as varchar) + ' 23:59:59.999'')'
	End

IF @DataSourceTypeID > 0
	SET @SQL += ' AND SFA.DataSourceTypeID = ' + CAST(@DataSourceTypeID as varchar) 

EXEC (@SQL)
--Print @SQL

END