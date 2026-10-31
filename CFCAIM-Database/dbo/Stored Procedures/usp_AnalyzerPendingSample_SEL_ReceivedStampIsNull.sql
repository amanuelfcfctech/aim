CREATE PROCEDURE [dbo].[usp_AnalyzerPendingSample_SEL_ReceivedStampIsNull]
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
		APS.SentStamp IS NOT NULL
	AND APS.ReceivedStamp IS NULL

END
