
CREATE PROCEDURE [dbo].[usp_SampleFurtherAnalysis_SEL_UpdateStamp]
			  @BegDate		DATETIME
			, @EndDate		DATETIME
			, @IsAll		BIT			
AS
BEGIN

--exec usp_SampleFurtherAnalysis_SEL_UpdateStamp '20220901', '20220901'
DECLARE @ProcessDate	DATETIME	

SET @ProcessDate = GETDATE()

SELECT 
	SFA.SampleFurtherAnalysisID
	, SFA.SampleID
	, SFA.DataSourceTypeID
	, SFA.ProcessDate
	, SFA.AIMMessageID
	, SFA.UpdateStamp
	, SFA.CFCUserID
	, S.SampleCode
	, S.SampleDate
	, DST.Name as DataSourceTypeName
	, U.UserName	
FROM 
	dbo.SampleFurtherAnalysis SFA 
	LEFT OUTER JOIN 
		dbo.Sample S
			ON SFA.SampleID = S.SampleID
	LEFT OUTER JOIN
		dbo.DataSourceType DST
			ON SFA.DataSourceTypeID = DST.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON SFA.CFCUserID = U.CFCUserID
WHERE
	SFA.UpdateStamp >= @BegDate + ' 00:00:00.000' 
	AND SFA.UpdateStamp <= @EndDate + ' 23:59:59.999'
	AND ISNULL(SFA.ProcessDate, @ProcessDate) = CASE WHEN @IsAll = 1 THEN ISNULL(SFA.ProcessDate, @ProcessDate) ELSE @ProcessDate END

END