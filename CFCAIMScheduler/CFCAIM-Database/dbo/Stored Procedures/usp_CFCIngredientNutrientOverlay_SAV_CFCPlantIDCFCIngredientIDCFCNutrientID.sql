create PROCEDURE [dbo].[usp_CFCIngredientNutrientOverlay_SAV_CFCPlantIDCFCIngredientIDCFCNutrientID]
	  @CFCIngredientNutrientOverlayID		INT				= NULL
	, @CFCPlantID							INT				= NULL
	, @CFCIngredientID						INT				= NULL
	, @CFCNutrientID						INT				= NULL
	, @NutrientValue						NUMERIC(12,4)	= NULL
	, @CFCUserID							INT				= NULL
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @CFCIngredientNutrientOverlayID IS NULL
	BEGIN
		SET @CFCIngredientNutrientOverlayID = 0
	END

IF @CFCIngredientNutrientOverlayID = 0
	BEGIN
		SELECT
			@CFCIngredientNutrientOverlayID = CFCIngredientNutrientOverlayID
		FROM
			dbo.CFCIngredientNutrientOverlay
		WHERE 
			CFCPlantID = @CFCPlantID
			AND CFCIngredientID = @CFCIngredientID
			AND CFCNutrientID = @CFCNutrientID
	END

IF @CFCIngredientNutrientOverlayID = 0
	BEGIN
		INSERT 
			dbo.CFCIngredientNutrientOverlay
				(
				  CFCPlantID
				, CFCIngredientID
				, CFCNutrientID
				, NutrientValue
				, UpdateStamp
				, CFCUserID
				)
		VALUES
				(
				  @CFCPlantID
				, @CFCIngredientID
				, @CFCNutrientID
				, @NutrientValue
				, GETDATE()
				, @CFCUserID
				)
		SET @RecordID = @@IDENTITY
	END
ELSE
	BEGIN
		UPDATE
			dbo.CFCIngredientNutrientOverlay
		SET
			  NutrientValue = @NutrientValue
			, UpdateStamp = GETDATE()
			, CFCUserID = @CFCUserID
		WHERE 
			CFCIngredientNutrientOverlayID = @CFCIngredientNutrientOverlayID
		SET @RecordID = @CFCIngredientNutrientOverlayID
	END

SELECT 
	  @RecordID as RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage
END