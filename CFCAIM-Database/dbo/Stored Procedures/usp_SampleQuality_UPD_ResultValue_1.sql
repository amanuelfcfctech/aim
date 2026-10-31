CREATE PROCEDURE [dbo].[usp_SampleQuality_UPD_ResultValue]
			  @SampleQualityID		INT
			, @ResultValueNumeric	NUMERIC(18,8)					
			, @CFCUserID			INT

		
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SampleID		INT = 0
DECLARE @DataSourceTypeID	INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''


IF EXISTS
		(
		SELECT
			SampleQualityID
		FROM 
			dbo.SampleQuality SQ
		WHERE
			SQ.SampleQualityID = @SampleQualityID
		)
	BEGIN
		UPDATE
			dbo.SampleQuality
				SET
					  IsActive = 0
					, CFCUserID = @CFCUserID
					, UpdateStamp = GETDATE()
				WHERE
						SampleQualityID = @SampleQualityID
						
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
		SELECT 
			  SR.SampleID 
			, SR.SampleQualityParameterID			
			, @ResultValueNumeric			
			, GETDATE()
			, 1
			, @CFCUserID
					
		FROM 
			dbo.SampleQuality SR WITH(NOLOCK)
		WHERE 
			SR.SampleQualityID = @SampleQualityID

		SET @SampleQualityID = @@IDENTITY
				
	END
	
SELECT 
	  @SampleQualityID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END