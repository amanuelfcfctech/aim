
CREATE PROCEDURE [dbo].[usp_SampleResult_DEL]
			  @SampleResultID		INT
			, @SampleID				INT
			, @AnalyzerNutrientID		INT
			, @CFCUserID			INT
			, @IsActive				BIT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		dbo.SampleResult
			SET
					IsActive = @IsActive
				, CFCUserID = @CFCUserID
				, UpdateStamp = GETDATE()
			WHERE
				SampleResultID = @SampleResultID
END
IF @IsActive = 1
BEGIN
	UPDATE
		dbo.SampleResult
			SET
					IsActive = 0
				, CFCUserID = @CFCUserID
				, UpdateStamp = GETDATE()
			WHERE
				SampleResultID != @SampleResultID
				AND SampleID = @SampleID
				AND AnalyzerNutrientID = @AnalyzerNutrientID
END

	
SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
