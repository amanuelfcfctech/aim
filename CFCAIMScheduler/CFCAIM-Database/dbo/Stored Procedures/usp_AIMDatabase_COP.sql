CREATE PROCEDURE [dbo].[usp_AIMDatabase_COP]
	  @AimDatabaseID			INT
	  , @CopyFromAimDatabaseID	INT
	  , @CFCUserID				INT
				
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

--INGREDIENT ---------------------------------------------------------------------
INSERT INTO CFCIngredientRefAIMMaterial with (rowlock)
	(  [CFCIngredientID]
      ,[AIMMaterialID]
      ,[UpdateStamp]
      ,[IsActive]
      ,[CFCUserID] )
SELECT new.CFCIngredientID, x.AIMMaterialID, getdate(), new.IsActive, @CFCUserID 
FROM CFCIngredient old
INNER JOIN 
	dbo.CFCIngredient new
	on old.CFCIngredientCode = new.CFCIngredientCode
	and  new.AIMDatabaseID = @AimDatabaseID
INNER JOIN 
	dbo.CFCIngredientRefAIMMaterial x
	on x.CFCIngredientID = old.CFCIngredientID 
WHERE old.AIMDatabaseID = @CopyFromAimDatabaseID

--NUTRIENT -----------------------------------------------------------------------
INSERT INTO CFCNutrientRefAIMNutrient with (rowlock)
	(  [CFCNutrientID]
      ,[AIMNutrientID]
      ,[UpdateStamp]      
      ,[CFCUserID] )
SELECT new.CFCNutrientID, x.AIMNutrientID, getdate(), @CFCUserID 
FROM CFCNutrient old
INNER JOIN 
	dbo.CFCNutrient new
	on old.CFCNutrientCode = new.CFCNutrientCode
	and  new.AIMDatabaseID = @AimDatabaseID
INNER JOIN 
	dbo.CFCNutrientRefAIMNutrient x
	on x.CFCNutrientID = old.CFCNutrientID 
WHERE old.AIMDatabaseID = @CopyFromAimDatabaseID


--PRODUCT ------------------------------------------------------------------------
INSERT INTO CFCProductRefAIMMaterial with (rowlock)
	(  [CFCProductID]
      ,[AIMMaterialID]
      ,[UpdateStamp]      
      ,[CFCUserID] )
SELECT new.CFCProductID, x.AIMMaterialID, getdate(), @CFCUserID 
FROM CFCProduct old
INNER JOIN 
	dbo.CFCProduct new
	on old.CFCProductCode = new.CFCProductCode
	and  new.AIMDatabaseID = @AimDatabaseID
INNER JOIN 
	dbo.CFCProductRefAIMMaterial x
	on x.CFCProductID = old.CFCProductID 
WHERE old.AIMDatabaseID = @CopyFromAimDatabaseID

--PLANT --------------------------------------------------------------------------
INSERT INTO CFCPlantRefAIMOwnerLocation with (rowlock)
	(  [CFCPlantID]
      ,[AIMOwnerLocationID]
      ,[UpdateStamp]      
      ,[CFCUserID] )
SELECT new.CFCPlantID, x.AIMOwnerLocationID, getdate(), @CFCUserID 
FROM CFCPlant old
INNER JOIN 
	dbo.CFCPlant new
	on old.CFCPlantCode = new.CFCPlantCode
	and  new.AIMDatabaseID = @AimDatabaseID
INNER JOIN 
	dbo.CFCPlantRefAIMOwnerLocation x
	on x.CFCPlantID = old.CFCPlantID 
WHERE old.AIMDatabaseID = @CopyFromAimDatabaseID

END