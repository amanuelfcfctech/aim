CREATE PROCEDURE [dbo].[usp_PNEMaterialType_SAV]
			  @PNEMaterialTypeID			INT
			, @AnalyzerMaterialID			INT		= NULL
			, @PNEMaterialSampleTypeID		INT		= NULL	
			, @IsActive						BIT		= NULL
			, @CFCUserID					INT		= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''


IF @PNEMaterialTypeID = 0 
	BEGIN
		INSERT INTO 
			dbo.PNEMaterialType 
				(
				  AnalyzerMaterialID
				, PNEMaterialSampleTypeID
				, IsActive 
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @AnalyzerMaterialID
				, @PNEMaterialSampleTypeID
				, 1
				, GETDATE()
				, @CFCUserID					
				)
		SET	@PNEMaterialTypeID = @@IDENTITY
		
	END
ELSE
	BEGIN
		UPDATE
			dbo.PNEMaterialType 
				SET
				  AnalyzerMaterialID = ISNULL(@AnalyzerMaterialID, AnalyzerMaterialID)
				, PNEMaterialSampleTypeID = ISNULL(@PNEMaterialSampleTypeID, PNEMaterialSampleTypeID)
				, IsActive = ISNULL(@IsActive, IsActive)
				, UpdateStamp = GETDATE()
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)

		WHERE	
			PNEMaterialTypeID = @PNEMaterialTypeID
	END



SELECT 
	  @PNEMaterialTypeID AS RecordID
	, @@ERROR AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END