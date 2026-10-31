CREATE PROCEDURE [dbo].[usp_Sample_UPD_SampleFields]
			  @AIMSourceID							INT				= NULL
			, @AIMSourceLocationID					INT				= NULL
			, @AnalyzerMaterialID					INT				= NULL
			, @AIMSourceIDtoBeReplaced				INT				= NULL
			, @AIMSourceLocationIDtoBeReplaced		INT				= NULL
			, @AnalyzerMaterialIDtoBeReplaced		INT				= NULL
			, @SampleCode							NVARCHAR(20)	= NULL
			, @SampleCodetoBeReplaced				NVARCHAR(20)	= NULL
			, @CFCUserID							INT


AS
BEGIN
SET NOCOUNT ON;

IF @AIMSourceID = 0
BEGIN
	SET @AIMSourceID = NULL;
END
IF @AIMSourceLocationID = 0
BEGIN
	SET @AIMSourceLocationID = NULL;
END
IF @AnalyzerMaterialID = 0
BEGIN
	SET @AnalyzerMaterialID = NULL;
END
IF @AIMSourceIDtoBeReplaced = 0
BEGIN
	SET @AIMSourceIDtoBeReplaced = NULL;
END
IF @AIMSourceLocationIDtoBeReplaced = 0
BEGIN
	SET @AIMSourceLocationIDtoBeReplaced = NULL;
END
IF @AnalyzerMaterialIDtoBeReplaced = 0
BEGIN
	SET @AnalyzerMaterialIDtoBeReplaced = NULL;
END
IF @AnalyzerMaterialIDtoBeReplaced = 0
BEGIN
	SET @AnalyzerMaterialIDtoBeReplaced = NULL;
END

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

	UPDATE
		dbo.Sample WITH(ROWLOCK)
			SET
				  AIMSourceID = ISNULL(@AIMSourceID, AIMSourceID)
				, AIMSourceLocationID = ISNULL(@AIMSourceLocationID, AIMSourceLocationID)
				, AnalyzerMaterialID = ISNULL(@AnalyzerMaterialID, AnalyzerMaterialID)
				, SampleCode = ISNULL(@SampleCode, SampleCode)
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
				, UpdateStamp = GETDATE()
				, @ErrorMessage = 'Successfully Saved'
		WHERE
				AIMSourceID = ISNULL(@AIMSourceIDtoBeReplaced, AIMSourceID)
			AND AIMSourceLocationID = ISNULL(@AIMSourceLocationIDtoBeReplaced, AIMSourceLocationID)
			AND AnalyzerMaterialID = ISNULL(@AnalyzerMaterialIDtoBeReplaced, AnalyzerMaterialID)
			AND SampleCode = ISNULL(@SampleCodetoBeReplaced, SampleCode)
	
	
SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END