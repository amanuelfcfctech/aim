
CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_SAV_ALL_Status]
(@UpdateIngredientHeaderID as Int,
@Status as Int,
@Target as Int)

AS
BEGIN

SET NOCOUNT ON;


DECLARE @ResultNumber INT = 0
DECLARE @ResultMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

--IF @Target = 1 
IF @Target = 1 or @Target = 4							--1=Base; 4=BaseFor

	Begin		
		UPDATE dbo.UpdateIngredientDetail 
		SET	Status = 
		case when CN.IsComputed = 1 then 3 Else @Status End
		FROM dbo.CFCNutrient CN 
		INNER JOIN 
			dbo.UpdateIngredientDetail D
				ON  D.CFCNutrientID = CN.CFCNutrientID		
		WHERE 
			UpdateIngredientHeaderID = @UpdateIngredientHeaderID
	End

ELSE
	Begin
		UPDATE dbo.UpdateIngredientDetail 
		SET	Status = 
		case when CN.IsComputed = 1 then 3 
		Else case when D.IsOverlay = 0 then 4 
		Else @Status End End
		FROM dbo.CFCNutrient CN 
		INNER JOIN 
			dbo.UpdateIngredientDetail D
				ON  D.CFCNutrientID = CN.CFCNutrientID		
		WHERE 
			UpdateIngredientHeaderID = @UpdateIngredientHeaderID
	End


SELECT 
	  @RecordID AS RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END
