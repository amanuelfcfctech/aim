
CREATE PROCEDURE [dbo].[usp_Sample_SEL_FurtherAnalysisIDMaterialTypeIDCreatedStampSQLResultIDMissingResult]
		  @FurtherAnalysisID	INT
		, @MaterialTypeID		INT
		, @CreatedStamp			DATETIME
		, @SQLResultID			NVARCHAR(300)
AS
BEGIN
SET NOCOUNT ON;

IF @MaterialTypeID = 0 
	BEGIN
		SET @MaterialTypeID = NULL
	END

SET @SQLResultID = @SQLResultID + '%'

SELECT 
	  S.*
FROM 
	dbo.Sample S
	INNER JOIN 
		dbo.AIMMaterial AM
			ON AM.AIMMaterialID = S.AIMMaterialID 
	INNER JOIN 
		dbo.APILog AL
			ON AL.SampleID = S.SampleID 
	INNER JOIN	
		dbo.AIMMaterialRefAnalyzerMaterial AMRAM
			ON AMRAM.AIMMaterialID = AM.AIMMaterialID
	INNER JOIN 
		dbo.AnalyzerMaterial AML
			ON AML.AnalyzerMaterialID = AMRAM.AnalyzerMaterialID 
			AND AML.DataSourceTypeID = @FurtherAnalysisID
	INNER JOIN 
		dbo.AnalyzerPendingSample APS
			ON APS.SampleID = S.SampleID 
	LEFT OUTER JOIN 
		dbo.SampleResult SR
			ON SR.SampleID = S.SampleID 
			AND SR.SQLResultID LIKE @SQLResultID
			AND SUBSTRING(SR.SQLResultID, 7, LEN(SR.SQLResultID)) = APS.RequestCode 

WHERE
	S.FurtherAnalysisID = @FurtherAnalysisID
	AND AM.MaterialTypeID = ISNULL(@MaterialTypeID, AM.MaterialTypeID)
	AND SR.SampleResultID IS NULL
	AND S.CreatedStamp > @CreatedStamp

END