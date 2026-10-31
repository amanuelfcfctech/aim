CREATE PROCEDURE [dbo].[usp_CFCCompareNameToAIM_SAV]
	@Table			INT, 
	@AIMID			INT,
	@AIMName		VARCHAR(30)

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL			VARCHAR(MAX) = ''

--exec sp_CFCCrossReferenceToAIM_SAV 1

-- 1 = Plant
-- 2 = Nutrient
-- 3 = Ingredient
-- 4 = Product

------------------------------------------ PLANT
IF @Table = 1
	BEGIN
		SET @SQL = '
		UPDATE dbo.AIMOwnerLocation
		SET LocationName = ''' + TRIM(@AIMName) + '''
		WHERE AIMOwnerLocationID = ' + Cast(@AIMID as varchar)		
	END

------------------------------------------ NUTRIENT
IF @Table = 2
	BEGIN
		SET @SQL = '
		UPDATE dbo.AIMNutrient
		SET AIMNutrientName = ''' + TRIM(@AIMName) + '''
		WHERE AIMNutrientID = ' + Cast(@AIMID as varchar)
	END

------------------------------------------ INGREDIENT
IF @Table = 3
	BEGIN		
		SET @SQL = '
		UPDATE dbo.AIMMaterial
		SET AIMMaterialName = ''' + TRIM(@AIMName) + '''
		WHERE AIMMaterialID = ' + Cast(@AIMID as varchar)
	END

------------------------------------------ PRODUCT
IF @Table = 4
	BEGIN
		SET @SQL = '
		UPDATE dbo.AIMMaterial
		SET AIMMaterialName = ''' + TRIM(@AIMName) + '''
		WHERE AIMMaterialID = ' + Cast(@AIMID as varchar)
	END

EXEC (@SQL)

END