CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrientOverlay_DEL_UpdateStamp]
				@UpdateStamp		DATETIME

AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.CFCIngredientNutrientOverlay 
WHERE 
	UpdateStamp < @UpdateStamp

END