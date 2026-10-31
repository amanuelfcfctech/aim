CREATE PROCEDURE [dbo].[usp_CFCCompareNameToAIM_SEL]
	@TABLE			INT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL			VARCHAR(MAX) = ''

--exec usp_CFCCrossReferenceToAIM_SEL 3

-- 1 = Plant
-- 2 = Nutrient
-- 3 = Ingredient
-- 4 = Product

------------------------------------------ PLANT
IF @TABLE = 1
	BEGIN
		SET @SQL = '
		SELECT 
			CP.CFCPlantID as FRMID,
			CP.CFCPlantCode as FRMCode,
			CP.CFCPlantName as FRMName,
			AOL.AIMOwnerLocationID as AIMID,			
			AOL.LocationCode as AIMCode,
			AOL.LocationName as AIMName,
			X.CFCPlantRefAIMOwnerLocationID	as NameUpdateID			
		FROM 
			dbo.CFCPlant CP
		INNER JOIN 
			dbo.CFCPlantRefAIMOwnerLocation X 
				ON CP.CFCPlantID = X.CFCPlantID
		INNER JOIN
			dbo.AIMOwnerLocation AOL 
				ON X.AIMOwnerLocationID = AOL.AIMOwnerLocationID 
		WHERE
			CP.CFCPlantName <> AOL.LocationName '
	END

------------------------------------------ NUTRIENT
IF @TABLE = 2
	BEGIN
		SET @SQL = '
		SELECT 
			CN.CFCNutrientID as FRMID,
			CN.CFCNutrientCode as FRMCode,
			CN.CFCNutrientName as FRMName,
			AN.AIMNutrientID as AIMID,					
			AN.AIMNutrientCode as AIMCode,
			AN.AIMNutrientName as AIMName,
			X.CFCNutrientRefAIMNutrientID as NameUpdateID
		FROM 
			dbo.CFCNutrient CN
		INNER JOIN 
			dbo.CFCNutrientRefAIMNutrient X 
				ON CN.CFCNutrientID = X.CFCNutrientID
		INNER JOIN
			dbo.AIMNutrient AN
				ON X.AIMNutrientID = AN.AIMNutrientID 
		WHERE
			CN.CFCNutrientName <> AN.AIMNutrientName '
	END

------------------------------------------ INGREDIENT
IF @TABLE = 3
	BEGIN
		SET @SQL = '
		SELECT 
			CI.CFCIngredientID as FRMID,
			CI.CFCIngredientCode as FRMCode,
			CI.CFCIngredientName as FRMName,
			AM.AIMMaterialID as AIMID,					
			AM.AIMMaterialCode as AIMCode,
			AM.AIMMaterialName as AIMName,
			X.CFCIngredientRefAIMMaterialID as NameUpdateID
		FROM 
			dbo.CFCIngredient CI
		INNER JOIN 
			dbo.CFCIngredientRefAIMMaterial X 
				ON CI.CFCIngredientID = X.CFCIngredientID
		INNER JOIN
			dbo.AIMMaterial AM
				ON X.AIMMaterialID = AM.AIMMaterialID
		WHERE 
			AM.MaterialTypeID = 1 and			
			SubString(CI.CFCIngredientName, 1, 30) <> AM.AIMMaterialName '
	END
	
------------------------------------------ PRODUCT
IF @TABLE = 4
	BEGIN
		SET @SQL = '
		SELECT 
			CP.CFCProductID as FRMID,
			CP.CFCProductCode as FRMCode,
			CP.CFCProductName as FRMName,
			CPL.CFCPlantName as FRMPlant,
			AM.AIMMaterialID as AIMID,					
			AM.AIMMaterialCode as AIMCode,
			AM.AIMMaterialName as AIMName,
			AOL.LocationName as AIMOwnerLocation,
			X.CFCProductRefAIMMaterialID as NameUpdateID
		FROM 
			dbo.CFCProduct CP
		INNER JOIN 
			dbo.CFCProductRefAIMMaterial X 
				ON CP.CFCProductID = X.CFCProductID
		INNER JOIN
			dbo.AIMMaterial AM
				ON X.AIMMaterialID = AM.AIMMaterialID
		INNER JOIN 
			dbo.CFCPlant CPL
				ON CP.CFCPlantID = CPL.CFCPlantID
		INNER JOIN 
			dbo.AIMOwnerLocation AOL
				ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
		WHERE 
			AM.MaterialTypeID = 2 and			
			SubString(CP.CFCProductName, 1, 30) <> AM.AIMMaterialName '
	END
	
EXEC (@SQL)

END