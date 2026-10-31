create PROCEDURE [dbo].[usp_SampleAIMRefCFCUser_SAV]
			  @SampleAIMRefCFCUserID		INT				
			, @SampleAIMID					INT				= NULL
			, @CFCUserID					INT				= NULL
			, @IsUserFlag					BIT				= NULL					

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

--checking if AnalyzerMaterial is not there than we will add

IF @SampleAIMRefCFCUserID > 0
	BEGIN
		UPDATE
			dbo.SampleAIMRefCFCUser WITH(ROWLOCK)
				SET
					  SampleAIMID = ISNULL(@SampleAIMID, SampleAIMID)
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, IsUserFlag = ISNULL(@IsUserFlag, IsUserFlag)
					, UpdateStamp = GETDATE()
			WHERE
				SampleAIMRefCFCUserID = @SampleAIMRefCFCUserID
			SET @RecordID = @SampleAIMRefCFCUserID
	END
ELSE
	BEGIN

		INSERT
			dbo.SampleAIMRefCFCUser WITH(ROWLOCK)
				(
				  SampleAIMID
				, CFCUserID
				, IsUserFlag
				, UpdateStamp
				)
		VALUES
				(
				  @SampleAIMID
				, @CFCUserID
				, @IsUserFlag
				, GETDATE()
				)

		SET @RecordID = @@IDENTITY
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END