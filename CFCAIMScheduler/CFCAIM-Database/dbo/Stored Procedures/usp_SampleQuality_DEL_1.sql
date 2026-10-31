CREATE PROCEDURE [dbo].[usp_SampleQuality_DEL]
			  @SampleQualityID				INT
			, @SampleID						INT
			, @SampleQualityParameterID		INT
			, @CFCUserID					INT
			, @IsActive						BIT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		dbo.SampleQuality
			SET
				IsActive = @IsActive
				, CFCUserID = @CFCUserID
				, UpdateStamp = GETDATE()
			WHERE
				SampleQualityID = @SampleQualityID
END
IF @IsActive = 1
BEGIN
	UPDATE
		dbo.SampleQuality
			SET
					IsActive = 0
				, CFCUserID = @CFCUserID
				, UpdateStamp = GETDATE()
			WHERE
				SampleQualityID != @SampleQualityID
				AND SampleID = @SampleID
				AND SampleQualityParameterID = @SampleQualityParameterID
END

	
SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END