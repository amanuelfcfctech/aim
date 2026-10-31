CREATE PROCEDURE [dbo].[usp_SampleFurtherAnalysis_SEL_SampleID]			
				@SampleID AS INT

AS
BEGIN

--exec usp_SampleFurtherAnalysis_SEL_SampleID 17956

SELECT 
	SFA.SampleFurtherAnalysisID
	, SFA.SampleID
	, SFA.DataSourceTypeID
	, SFA.AIMMessageID
	, SFA.ProcessDate
	, SFA.UpdateStamp
	, SFA.CFCUserID
	, U.UserName
	, S.SampleCode
	, S.SampleDate
	, AM.Message 
	, DST.Name as DataSourceTypeName
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
	SFA.SampleID = @SampleID 
	AND SFA.ProcessDate IS NULL

END