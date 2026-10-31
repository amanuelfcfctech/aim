CREATE PROCEDURE [dbo].[usp_PNEMaterialSampleTypeRefGroundUnGround_SAV]
		@PNEMaterialSampleTypeRefGroundUnGroundID	INT
		, @PNEMaterialSampleTypeID					INT
		, @GroundUnGroundID							INT			
		, @CFCUserID								INT				
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''


IF @PNEMaterialSampleTypeRefGroundUnGroundID = 0
	BEGIN
		INSERT
			dbo.PNEMaterialSampleTypeRefGroundUnGround 
				(
				PNEMaterialSampleTypeID
				, GroundUnGroundID				
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				@PNEMaterialSampleTypeID
				, @GroundUnGroundID												
				, @CFCUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.PNEMaterialSampleTypeRefGroundUnGround WITH(ROWLOCK)
				SET 
					PNEMaterialSampleTypeID = ISNULL(@PNEMaterialSampleTypeID, PNEMaterialSampleTypeID)
					, GroundUnGroundID = ISNULL(@GroundUnGroundID, GroundUnGroundID)														
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					
				WHERE 
					PNEMaterialSampleTypeRefGroundUnGroundID  = @PNEMaterialSampleTypeRefGroundUnGroundID 

			SET @RecordID = @@IDENTITY 
	END


SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END