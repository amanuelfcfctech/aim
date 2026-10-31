CREATE PROCEDURE [Vitapro].[usp_SystemSettingsCustom_SAV]
			  @SystemSettingsCustomID					INT
			, @AIMNutrientID1_ForDisplay				INT
			, @AIMNutrientID2_ForDisplay				INT
			, @AIMNutrientID3_ForDisplay				INT
			, @AIMNutrientID4_ForDisplay				INT
			, @CFCUserID								INT
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SystemSettingsCustomID = 0 
	BEGIN
		IF EXISTS(SELECT 
					@SystemSettingsCustomID 
				FROM 
					Vitapro.SystemSettingsCustom SSC
				WHERE 
						SSC.SystemSettingsCustomID = @SystemSettingsCustomID )
					
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This setting already exists. Information is not saved.'
			END
		ELSE
			BEGIN
				INSERT
					Vitapro.SystemSettingsCustom
						(
						  AIMNutrientID1_ForDisplay
						, AIMNutrientID2_ForDisplay
						, AIMNutrientID3_ForDisplay
						, AIMNutrientID4_ForDisplay
						, UpdateStamp
						, CFCUserID						
						)
				VALUES
						(
						  @AIMNutrientID1_ForDisplay			
						, @AIMNutrientID2_ForDisplay			
						, @AIMNutrientID3_ForDisplay			
						, @AIMNutrientID4_ForDisplay			
						, GETDATE()
						, @CFCUserID
						
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
	BEGIN	
		
		UPDATE
			Vitapro.SystemSettingsCustom WITH(ROWLOCK)
				SET 
					  AIMNutrientID1_ForDisplay = ISNULL(@AIMNutrientID1_ForDisplay, AIMNutrientID1_ForDisplay)					
					, AIMNutrientID2_ForDisplay = ISNULL(@AIMNutrientID2_ForDisplay, AIMNutrientID2_ForDisplay)					
					, AIMNutrientID3_ForDisplay = ISNULL(@AIMNutrientID3_ForDisplay, AIMNutrientID3_ForDisplay)					
					, AIMNutrientID4_ForDisplay = ISNULL(@AIMNutrientID4_ForDisplay, AIMNutrientID4_ForDisplay)					
					, UpdateStamp = GETDATE()
					, CFCUserID = @CFCUserID
					
				WHERE 
					SystemSettingsCustomID = @SystemSettingsCustomID

		SET @RecordID = @SystemSettingsCustomID
		
	END

SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END