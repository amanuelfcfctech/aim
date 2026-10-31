
CREATE PROCEDURE [dbo].[usp_PNEForAnalyzerMaterialRefAnalyzerNutrient_SAV]
			  @PNEForAnalyzerMaterialRefAnalyzerNutrientID		INT
			, @AnalyzerMaterialID								INT		= NULL
			, @AnalyzerNutrientID								INT		= NULL	
			, @IsActive											BIT		= NULL
			, @CFCUserID										INT		= NULL
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber				INT = 0
DECLARE @ErrorMessage				VARCHAR(200) = ''


IF @PNEForAnalyzerMaterialRefAnalyzerNutrientID = 0 
	BEGIN
		INSERT INTO 
			dbo.PNEForAnalyzerMaterialRefAnalyzerNutrient 
				(
				  AnalyzerMaterialID
				, AnalyzerNutrientID
				, IsActive 
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @AnalyzerMaterialID
				, @AnalyzerNutrientID
				, 1
				, GETDATE()
				, @CFCUserID					
				)
		SET	@PNEForAnalyzerMaterialRefAnalyzerNutrientID = @@IDENTITY
		
	END
ELSE
	BEGIN
		UPDATE
			dbo.PNEForAnalyzerMaterialRefAnalyzerNutrient 
				SET
				  AnalyzerMaterialID = ISNULL(@AnalyzerMaterialID, AnalyzerMaterialID)
				, AnalyzerNutrientID = ISNULL(@AnalyzerNutrientID, @AnalyzerNutrientID)
				, IsActive = ISNULL(@IsActive, IsActive)
				, UpdateStamp = GETDATE()
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)

		WHERE	
			PNEForAnalyzerMaterialRefAnalyzerNutrientID = @PNEForAnalyzerMaterialRefAnalyzerNutrientID
	END

SELECT 
	  @PNEForAnalyzerMaterialRefAnalyzerNutrientID AS RecordID
	, @@ERROR AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END