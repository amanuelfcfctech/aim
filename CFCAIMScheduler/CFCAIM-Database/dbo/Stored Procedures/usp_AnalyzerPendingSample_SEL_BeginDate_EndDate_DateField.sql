CREATE PROCEDURE [dbo].[usp_AnalyzerPendingSample_SEL_BeginDate_EndDate_DateField]
		
		  @BegDate			DATE
		, @EndDate			DATE
		, @DateField		INT

AS
BEGIN

DECLARE @SQL				VARCHAR(MAX) = ''

-- 2 = Created Date
-- 4 = Sent Date
-- 5 = Received Date

--exec usp_AnalyzerPendingSample_SEL_BeginDate_EndDate_DateField '20220901', '20230901', 2


SET NOCOUNT ON;

SET @SQL = 'SELECT 
	  APS.AnalyzerPendingSampleID
    , APS.SampleID
    , APS.CreatedStamp
    , APS.SentStamp
    , APS.RequestCode
    , APS.ReceivedStamp
    , APS.SpectraName
	, APS.NIRAnalysis
	, S.SampleCode

FROM
	dbo.AnalyzerPendingSample APS 
LEFT OUTER JOIN 
	dbo.Sample S
		on S.SampleID = APS.SampleID

WHERE 
'

IF @DateField = 2 
	Begin
		SET @SQL += ' (APS.CreatedStamp >= ''' + CAST(@BegDate as varchar)  + ' 00:00:00.000'''
		SET @SQL += ' AND APS.CreatedStamp <= ''' + CAST(@EndDate as varchar) + ' 23:59:59.999'')'
	End
IF @DateField = 4
	Begin
		SET @SQL += ' (APS.SentStamp >= ''' + CAST(@BegDate as varchar)  + ' 00:00:00.000'''
		SET @SQL += ' AND APS.SentStamp <= ''' + CAST(@EndDate as varchar) + ' 23:59:59.999'')'
	End
IF @DateField = 5
	Begin
		SET @SQL += ' (APS.ReceivedStamp >= ''' + CAST(@BegDate as varchar)  + ' 00:00:00.000'''
		SET @SQL += ' AND APS.ReceivedStamp <= ''' + CAST(@EndDate as varchar) + ' 23:59:59.999'')'
	End


EXEC (@SQL)
--Print @SQL


END