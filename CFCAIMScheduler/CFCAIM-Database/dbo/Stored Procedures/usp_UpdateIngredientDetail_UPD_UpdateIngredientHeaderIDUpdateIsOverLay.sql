CREATE PROCEDURE [dbo].[usp_UpdateIngredientDetail_UPD_UpdateIngredientHeaderIDUpdateIsOverLay]
				@UpdateIngredientHeaderID		INT
AS
BEGIN

SET NOCOUNT ON;

DECLARE @ErrorNumber		INT = 0
DECLARE @ErrorMessage		VARCHAR(200) = ''
DECLARE @RecordID			INT = 0

UPDATE 
	dbo.UpdateIngredientDetail 
		SET IsOverlay = CASE WHEN ISNULL(CNO.CFCIngredientNutrientOverlayID, 0) = 0 THEN 0 ELSE 1 END 
FROM 
	dbo.UpdateIngredientDetail D 
	LEFT OUTER JOIN 
		dbo.CFCIngredientNutrientOverlay CNO 
			ON CNO.CFCPlantID = D.CFCPlantID 
			AND CNO.CFCIngredientID = D.CFCIngredientID
			AND CNO.CFCNutrientID = D.CFCNutrientID
WHERE 
	D.UpdateIngredientHeaderID = @UpdateIngredientHeaderID
	AND D.IsOverlay != CASE WHEN ISNULL(CNO.CFCIngredientNutrientOverlayID, 0) = 0 THEN 0 ELSE 1 END 

SET @ErrorNumber = @@ERROR 

IF	@ErrorNumber > 0 
	BEGIN
		SET @ErrorMessage = 'Update of Overlay failed.'
	END

SELECT 
	  @RecordID AS RecordID
	, @@ERROR AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END