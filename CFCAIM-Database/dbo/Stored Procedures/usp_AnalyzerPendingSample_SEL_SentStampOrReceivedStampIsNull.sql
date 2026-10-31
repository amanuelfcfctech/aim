--DROP PROCEDURE DBO.usp_AnalyzerPendingSample_SEL_SentStampAndReceivedStampIsNull
CREATE PROCEDURE [dbo].[usp_AnalyzerPendingSample_SEL_SentStampOrReceivedStampIsNull]
AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  APS.AnalyzerPendingSampleID
    , APS.SampleID
    , APS.CreatedStamp
    , APS.SentStamp
    , APS.RequestCode
    , APS.ReceivedStamp
    , APS.SpectraName
	, APS.NIRAnalysis
FROM
	dbo.AnalyzerPendingSample APS WITH(NOLOCK)
WHERE 
		APS.NIRAnalysis IS NOT NULL
	AND	(APS.SentStamp IS NULL
	OR	APS.ReceivedStamp IS NULL)


END


