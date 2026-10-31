CREATE PROCEDURE [dbo].[usp_SampleFurtherAnalysisAll_SEL]
			
AS
BEGIN

--exec usp_SampleFurtherAnalysis_SEL

SELECT 
	SFA.SampleFurtherAnalysisID
	, SFA.SampleID
	, SFA.DataSourceTypeID
	, SFA.ProcessDate
	, SFA.UpdateStamp
	, SFA.CFCUserID
	, U.UserName
	, S.SampleCode
	, DST.Name as DataSourceTypeName
	, S.SampleDate

FROM 
	dbo.SampleFurtherAnalysis SFA WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.Sample S 
			ON S.SampleID = SFA.SampleID
	LEFT OUTER JOIN
		dbo.DataSourceType DST
			ON DST.DataSourceTypeID = SFA.DataSourceTypeID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SFA.CFCUserID = U.CFCUserID

ORDER BY SFA.SampleID DESC

END