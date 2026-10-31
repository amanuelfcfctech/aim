
CREATE PROCEDURE [dbo].[usp_AnalyzerPendingSample_SEL_SampleID]
				@SampleID			INT
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1
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
		APS.SampleID = @SampleID
	AND APS.NIRAnalysis IS NULL

END
