CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrient_SAV]
			  @CFCIngredientNutrientID	INT				= NULL
			, @CFCIngredientID			INT				= NULL
			, @CFCNutrientID			INT				= NULL
			, @Value					NUMERIC(12,4)	= NULL
			, @IsComputed				BIT				= NULL
AS
BEGIN
SET NOCOUNT ON;

IF @CFCIngredientNutrientID IS NULL
	BEGIN
		SET @CFCIngredientNutrientID = 0 
	END

IF @CFCIngredientNutrientID = 0 
	BEGIN
		SELECT 
			@CFCIngredientNutrientID = CIN.CFCIngredientNutrientID
		FROM 
			dbo.CFCIngredientNutrient CIN
		WHERE 
			CIN.CFCIngredientID = @CFCIngredientID
			AND CIN.CFCNutrientID = @CFCNutrientID
	END

IF @CFCIngredientNutrientID = 0 
	BEGIN
		INSERT
			dbo.CFCIngredientNutrient 
				(
				  CFCIngredientID
				, CFCNutrientID
				, Value 
				, UpdateStamp
				)
		VALUES
				(
				  @CFCIngredientID
				, @CFCNutrientID
				, @Value
				, GETDATE()
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.CFCIngredientNutrient 
				SET
					  Value = ISNULL(@Value, Value)
					, UpdateStamp = GETDATE()
					, IsComputed = ISNULL(@IsComputed, IsComputed)
		WHERE 
			CFCIngredientNutrientID = @CFCIngredientNutrientID
	END
END