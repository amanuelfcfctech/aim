
CREATE PROCEDURE [dbo].[usp_Sample_SEL_FurtherAnalysisIDMaterialTypeIDCreatedStampMissingFurtherAnalysis]
		  @FurtherAnalysisID	INT
		, @MaterialTypeID		INT
		, @CreatedStamp			DATETIME
AS
BEGIN
SET NOCOUNT ON;

IF @MaterialTypeID = 0 
	BEGIN
		SET @MaterialTypeID = NULL
	END

SELECT 
	S.*
FROM 
	dbo.Sample S
	INNER JOIN 
		dbo.AIMMaterial AM
			ON AM.AIMMaterialID = S.AIMMaterialID 
	INNER JOIN	
		dbo.AIMMaterialRefAnalyzerMaterial AMRAM
			ON AMRAM.AIMMaterialID = AM.AIMMaterialID
	INNER JOIN 
		dbo.AnalyzerMaterial AML
			ON AML.AnalyzerMaterialID = AMRAM.AnalyzerMaterialID 
			AND AML.DataSourceTypeID = @FurtherAnalysisID
	INNER JOIN 
		dbo.APILog AL
			ON AL.SampleID = S.SampleID 
	LEFT OUTER JOIN 
		dbo.AnalyzerPendingSample APS
			ON APS.SampleID = S.SampleID 
WHERE
	S.FurtherAnalysisID = @FurtherAnalysisID
	AND AM.MaterialTypeID = ISNULL(@MaterialTypeID, AM.MaterialTypeID)
	AND APS.SampleID IS NULL
	AND S.CreatedStamp > @CreatedStamp

END