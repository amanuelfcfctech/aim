CREATE PROCEDURE [dbo].[usp_AnalyzerPendingSample_SAV]
			  @AnalyzerPendingSampleID		INT
			, @SampleID						INT				
			, @SentStamp					DATETIME		= NULL
			, @RequestCode					NVARCHAR(50)	= NULL
			, @ReceivedStamp				DATETIME		= NULL
			, @SpectraName					NVARCHAR(50)	= NULL
			, @NIRAnalysis					DATETIME		= NULL
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @AnalyzerPendingSampleID = 0 
	BEGIN
		INSERT
			dbo.AnalyzerPendingSample
				(
				  SampleID
				, CreatedStamp
				, SentStamp
				, RequestCode
				, ReceivedStamp
				, SpectraName
				, NIRAnalysis
				)
		VALUES
				(
				  @SampleID
				, GETDATE()
				, @SentStamp
				, @RequestCode
				, @ReceivedStamp
				, @SpectraName
				, @NIRAnalysis
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.AnalyzerPendingSample WITH(ROWLOCK)
				SET 
					  SentStamp = ISNULL(@SentStamp, SentStamp)
					, RequestCode = ISNULL(@RequestCode, RequestCode)
					, ReceivedStamp = ISNULL(@ReceivedStamp, ReceivedStamp)
					, SpectraName = ISNULL(@SpectraName, SpectraName)
					, NIRAnalysis = ISNULL(@NIRAnalysis, NIRAnalysis)
				WHERE 
					AnalyzerPendingSampleID = @AnalyzerPendingSampleID
		SET @RecordID = @AnalyzerPendingSampleID
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
