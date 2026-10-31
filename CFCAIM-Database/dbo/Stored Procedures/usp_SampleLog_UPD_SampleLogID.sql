create PROCEDURE [dbo].[usp_SampleLog_UPD_SampleLogID]
			  @SampleLogID					INT
			, @SampleID						INT
			, @LogMessage					VARCHAR(200)
			, @CFCUserID					INT
			, @IsActive						BIT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @SampleLogID > 0
BEGIN
	UPDATE
		dbo.SampleLog WITH(ROWLOCK)
			SET 
				  LogMessage = @LogMessage
				, SampleID = @SampleID
				, CFCUserID = @CFCUserID
				, IsActive = @IsActive
			WHERE 
				SampleLogID = @SampleLogID
END
ELSE
BEGIN
	INSERT INTO
		dbo.SampleLog
			(
			  LogMessage
			, SampleID
			, CFCUserID
			, IsActive
			, CreatedStamp
			)
		VALUES
			(
			  @LogMessage
			, @SampleID
			, @CFCUserID
			, 1
			, GETDATE()
			)
END

SELECT 
	  @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END
