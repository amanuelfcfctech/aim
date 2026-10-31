CREATE PROCEDURE [dbo].[usp_AnalyzerAnalyse_DEL]
			  @AnalyzerAnalyseID			INT
AS
BEGIN
SET NOCOUNT ON;

EXEC dbo.usp_AnalyzerMaterialRefAnalyzerAnalyse_DEL @AnalyzerAnalyseID

DELETE 
	dbo.AnalyzerAnalyse
FROM 
	dbo.AnalyzerAnalyse AA WITH(ROWLOCK)
WHERE
	AA.AnalyzerAnalyseID = @AnalyzerAnalyseID

END
