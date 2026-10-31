CREATE PROCEDURE [dbo].[usp_WarningLimits_SAV]
			  @WarningLimitsID	INT
			, @AIMMaterialID	INT
			, @AIMNutrientID	INT
			, @Min				Numeric(18,8)
			, @Max				Numeric(18,8)	
			, @CFCUserID		INT				
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''


IF EXISTS(
			SELECT
				WL.*
			FROM
				WarningLimits WL
			WHERE
					WL.AIMMaterialID = @AIMMaterialID
				AND WL.AIMNutrientID = @AIMNutrientID
				AND WL.WarningLimitsID != @WarningLimitsID
		)
BEGIN
	SET @ErrorNumber = 1
	SET	@ErrorMessage = 'This warning already exists. Information is not saved!'
END

IF @ErrorNumber = 0
BEGIN
	IF @WarningLimitsID = 0
		BEGIN
			INSERT
				dbo.WarningLimits 
					(
					  AIMMaterialID
					, AIMNutrientID
					, [Min]
					, [Max]	
					, CFCUserID
					, UpdateStamp				
					)
			VALUES
					(
					  @AIMMaterialID
					, @AIMNutrientID
					, @Min
					, @Max										
					, @CFCUserID
					, GETDATE()
					)
			SET @RecordID = @@IDENTITY 
		END
	ELSE
		BEGIN
			UPDATE
				dbo.WarningLimits WITH(ROWLOCK)
					SET 
						  AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
						, AIMNutrientID = ISNULL(@AIMNutrientID, AIMNutrientID)
						, [Min] = ISNULL(@Min, [Min])
						, [Max] = ISNULL(@Max, [Max])									
						, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
						, UpdateStamp = GETDATE()
					
					WHERE 
						WarningLimitsID = @WarningLimitsID

				SET @RecordID = @@IDENTITY 
		END
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

