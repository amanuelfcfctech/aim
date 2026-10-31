CREATE PROCEDURE [dbo].[usp_AIMDatabase_DEL]
	  @AIMDatabaseID		INT

--exec usp_AIMDatabase_DEL 1003
	
AS
BEGIN
	SET NOCOUNT ON;			
		
	DELETE dbo.CFCPlantRefAIMOwnerLocation		
		FROM dbo.CFCPlantRefAIMOwnerLocation with (rowlock) 
		INNER JOIN dbo.CFCPlant with (nolock) ON dbo.CFCPlantRefAIMOwnerLocation.CFCPlantID = dbo.CFCPlant.CFCPlantID
		WHERE dbo.CFCPlant.AIMDatabaseID = @AIMDatabaseID

	DELETE dbo.CFCIngredientRefAIMMaterial
		FROM dbo.CFCIngredientRefAIMMaterial with (rowlock) 
		INNER JOIN dbo.CFCIngredient with (nolock) ON dbo.CFCIngredientRefAIMMaterial.CFCIngredientID = dbo.CFCIngredient.CFCIngredientID
		WHERE dbo.CFCIngredient.AIMDatabaseID = @AIMDatabaseID

	DELETE dbo.CFCNutrientRefAIMNutrient
		FROM dbo.CFCNutrientRefAIMNutrient with (rowlock) 
		INNER JOIN dbo.CFCNutrient with (nolock) ON dbo.CFCNutrientRefAIMNutrient.CFCNutrientID = dbo.CFCNutrient.CFCNutrientID
		WHERE dbo.CFCNutrient.AIMDatabaseID = @AIMDatabaseID

	DELETE dbo.CFCProductRefAIMMaterial
		FROM dbo.CFCProductRefAIMMaterial with (rowlock) 
		INNER JOIN dbo.CFCProduct with (nolock)	ON dbo.CFCProductRefAIMMaterial.CFCProductID = dbo.CFCProduct.CFCProductID
		WHERE dbo.CFCProduct.AIMDatabaseID = @AIMDatabaseID			
			
	DELETE FROM dbo.CFCPlant with (rowlock) WHERE AIMDatabaseID = @AIMDatabaseID
	DELETE FROM dbo.CFCIngredient with (rowlock) WHERE AIMDatabaseID = @AIMDatabaseID
	DELETE FROM dbo.CFCNutrient with (rowlock) WHERE AIMDatabaseID = @AIMDatabaseID
	DELETE FROM dbo.CFCProduct with (rowlock) WHERE AIMDatabaseID = @AIMDatabaseID
	
	DELETE FROM dbo.AIMConfig	with (rowlock) WHERE AIMDatabaseID = @AIMDatabaseID
	DELETE FROM dbo.AIMDatabase	with (rowlock) WHERE AIMDatabaseID = @AIMDatabaseID

END