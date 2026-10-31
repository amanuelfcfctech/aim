create PROCEDURE [dbo].[usp_AnalyzerPendingSample_SEL_SampleIDAll]
				@SampleID			INT
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
	dbo.AnalyzerPendingSample APS 
WHERE 
	APS.SampleID = @SampleID

END