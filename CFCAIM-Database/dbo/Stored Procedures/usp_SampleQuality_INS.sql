CREATE PROCEDURE [dbo].[usp_SampleQuality_INS]
			 @SampleID					INT			
			, @SampleQualityID			INT 
			, @SampleQualityParameterID	INT				
			, @ResultValueNumeric		NUMERIC(18,8)						
			, @IsActive					BIT	
			, @CFCUserID				INT			

			
AS

BEGIN

SET NOCOUNT ON;

DECLARE @ErrorNumber			INT = 0
DECLARE @ErrorMessage			VARCHAR(200) = ''
DECLARE @ReturnSampleQualityID		INT = 0

--Update Sample Quality ---------------------------------------------
IF EXISTS
		(
		SELECT
			SQ.SampleID
		FROM 
			dbo.SampleQuality SQ
		WHERE
				SQ.SampleID = @SampleID
			AND SQ.SampleQualityParameterID = @SampleQualityParameterID
			AND SQ.IsActive = 1
		)
	BEGIN
		UPDATE
			dbo.SampleQuality
				SET
						IsActive = 0
					, CFCUserID = @CFCUserID
					, UpdateStamp = GETDATE()
				WHERE
						SampleID = @SampleID
					AND SampleQualityParameterID = @SampleQualityParameterID
					AND IsActive = 1
		
	END			

--Insert Sample Quality ---------------------------------------------
	BEGIN
					
		INSERT
			dbo.SampleQuality WITH(ROWLOCK)
				(
					SampleID 
				, SampleQualityParameterID 		
				, ResultValueNumeric 		
				, UpdateStamp 
				, IsActive 
				, CFCUserID		
		
				)
		VALUES
				(
					@SampleID
				, @SampleQualityParameterID
				, @ResultValueNumeric		
				, GETDATE()
				, 1
				, @CFCUserID				
				)
		SET @ReturnSampleQualityID	= @@IDENTITY

	END
	
	SELECT 
			@ReturnSampleQualityID AS RecordID
		, @ErrorNumber AS ErrorNumber
		, @ErrorMessage AS ErrorMessage

END