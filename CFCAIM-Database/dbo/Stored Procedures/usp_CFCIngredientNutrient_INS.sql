create PROCEDURE [dbo].[usp_CFCIngredientNutrient_INS]
			  @CFCIngredientCode		VARCHAR(10)
			, @CFCNutrientCode			VARCHAR(10)
			, @Value					NUMERIC(12,4)
AS
BEGIN
SET NOCOUNT ON;
DECLARE @CFCIngredientID	INT
DECLARE @CFCNutrientID		INT

SELECT 
	@CFCIngredientID = I.CFCIngredientID
FROM 
	dbo.CFCIngredient I
WHERE 
	I.CFCIngredientCode = @CFCIngredientCode 

SELECT 
	@CFCNutrientID = N.CFCNutrientID
FROM 
	dbo.CFCNutrient N
WHERE 
	N.CFCNutrientCode = @CFCNutrientCode 

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
