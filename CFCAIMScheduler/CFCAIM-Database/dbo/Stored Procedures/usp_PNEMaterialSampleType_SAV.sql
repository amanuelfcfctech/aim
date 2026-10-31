CREATE PROCEDURE [dbo].[usp_PNEMaterialSampleType_SAV]
			  @PNEMaterialSampleTypeID		INT
			, @PNEMaterialSampleTypeCode	VARCHAR(20)	= NULL	
			, @IsActive						BIT			= NULL
			, @CFCUserID					INT			= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''

IF @PNEMaterialSampleTypeID = 0
	BEGIN
		INSERT INTO 
			dbo.PNEMaterialSampleType 
				(
				  PNEMaterialSampleTypeCode
				, IsActive
				, UpdateStamp 
				, CFCUserID
				)
		VALUES
				(
				  @PNEMaterialSampleTypeCode
				, @IsActive
				, GETDATE()
				, @CFCUserID					
				)

		SET	@PNEMaterialSampleTypeID = @@IDENTITY
	END
ELSE
	BEGIN
		UPDATE
			dbo.PNEMaterialSampleType 
				SET
					  PNEMaterialSampleTypeCode = ISNULL(@PNEMaterialSampleTypeCode, PNEMaterialSampleTypeCode)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
					, CFCUserID  = ISNULL(@CFCUserID, @CFCUserID)
		WHERE				
			PNEMaterialSampleTypeID = @PNEMaterialSampleTypeID
	END

SELECT 
	  @PNEMaterialSampleTypeID AS RecordID
	, @@ERROR AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END