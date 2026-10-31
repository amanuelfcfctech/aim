CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrientOverlay_UPD_CFCPlantIDCFCIngredientIDCFCNutrientID]
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

UPDATE
	dbo.CFCIngredientNutrientOverlay
SET
	  NutrientValue = @NutrientValue
	, UpdateStamp = GETDATE()
	, CFCUserID = @CFCUserID
WHERE 
	CFCPlantID = @CFCPlantID
	AND CFCIngredientID = @CFCIngredientID
	AND CFCNutrientID = @CFCNutrientID

SELECT 
	  @RecordID as RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage
END