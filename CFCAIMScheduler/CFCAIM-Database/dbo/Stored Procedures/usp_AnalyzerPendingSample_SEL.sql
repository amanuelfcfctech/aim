CREATE PROCEDURE [dbo].[usp_AnalyzerPendingSample_SEL]
				
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
	, S.SampleCode

FROM
	dbo.AnalyzerPendingSample APS 
LEFT OUTER JOIN 
	dbo.Sample S
		on S.SampleID = APS.SampleID

ORDER BY APS.CreatedStamp DESC

END