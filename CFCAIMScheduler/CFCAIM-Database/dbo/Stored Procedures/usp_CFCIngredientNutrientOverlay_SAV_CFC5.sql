CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrientOverlay_SAV_CFC5]
	  @CFCPlantCode						VARCHAR(20)		= NULL
	, @CFCIngredientCode				VARCHAR(20)		= NULL
	, @CFCIngNutOvrJoined				VARCHAR(8000)	= NULL
	, @AIMDatabaseID					INT				= NULL
	, @CFCUserID						INT				= NULL
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ResultNumber						INT = 0
DECLARE @ResultMessage						VARCHAR(200) = ''
DECLARE @SplitOn							VARCHAR(2) = '~'
DECLARE @SplitRecord						VARCHAR(2) = '|'
DECLARE @PathInd							INT = 0
DECLARE @RecordID							INT = 0
DECLARE @CFCPlantID							INT	= 0
DECLARE @CFCIngredientID					INT = 0
DECLARE @CFCNutrientID						INT = 0
DECLARE @AIMNutrientID						INT = 0 
DECLARE @CFCNutrientRefAIMNutrientID		INT = 0
DECLARE @CFCIngredientNutrientOverlayID		INT = 0 
DECLARE @NutrientValue						NUMERIC(12, 4)

SELECT
	@CFCPlantID = CP.CFCPlantID 
FROM
	dbo.CFCPlant CP
WHERE
	CP.CFCPlantCode = @CFCPlantCode
	AND	CP.AIMDatabaseID = @AIMDatabaseID

SELECT
	@CFCIngredientID = CI.CFCIngredientID
FROM
	dbo.CFCIngredient CI
WHERE
	CI.CFCIngredientCode = TRIM(@CFCIngredientCode)
	AND	CI.AIMDatabaseID = @AIMDatabaseID



IF @CFCIngredientID = 0 OR @CFCPlantID = 0
	BEGIN
		SET @ResultNumber = 1
		SET	@ResultMessage = 'Ingr Code = ' + @CFCIngredientCode + ' or Plant Code = ' + @CFCPlantCode + ' does not exists. Information is not saved!'
	END
ELSE
	BEGIN
		IF LEN(@CFCIngNutOvrJoined) > 0 
			BEGIN				
				SELECT
					  @CFCPlantID AS CFCPlantID
					, @CFCIngredientID AS CFCIngredientID
					, @AIMDatabaseID AS AIMDatabaseID
					, SUBSTRING(value, 1, CHARINDEX(@SplitOn, value) - 1) AS NutrientCode
					, SUBSTRING(value, CHARINDEX(@SplitOn, value) + 1, 100) AS Amount
				INTO
					#Overlay
				FROM 
					string_split(@CFCIngNutOvrJoined, @SplitRecord)
				WHERE 
					Value != ''
					AND SUBSTRING(value, CHARINDEX('~', Value) + 1, 100) != ''

				UPDATE 
					dbo.CFCIngredientNutrientOverlay	
						SET NutrientValue = O.Amount 
				FROM
					#Overlay O
					INNER JOIN 
						dbo.CFCNutrient CN
							ON CN.CFCNutrientCode = O.NutrientCode 
							AND CN.AIMDatabaseID = O.AIMDatabaseID
					INNER JOIN 
						dbo.CFCIngredientNutrientOverlay CINO
							ON  CINO.CFCPlantID = O.CFCPlantID 
							AND CINO.CFCIngredientID = O.CFCIngredientID 
							AND CINO.CFCNutrientID = CN.CFCNutrientID 


				INSERT INTO 
						dbo.CFCIngredientNutrientOverlay 
							(
							  CFCPlantID 
							, CFCIngredientID 
							, CFCNutrientID 
							, NutrientValue
							, UpdateStamp
							, CFCUserID
							)
				SELECT 
					  O.CFCPlantID 
					, O.CFCIngredientID 
					, CN.CFCNutrientID 
					, O.Amount 
					, GETDATE()
					, @CFCUserID
				FROM 
					#Overlay O
					INNER JOIN 
						dbo.CFCNutrient CN
							ON CN.CFCNutrientCode = O.NutrientCode 
							AND CN.AIMDatabaseID = O.AIMDatabaseID
					LEFT OUTER JOIN 
						dbo.CFCIngredientNutrientOverlay CINO
							ON  CINO.CFCPlantID = O.CFCPlantID 
							AND CINO.CFCIngredientID = O.CFCIngredientID 
							AND CINO.CFCNutrientID = CN.CFCNutrientID 
				WHERE
					CINO.CFCIngredientNutrientOverlayID IS NULL

				DELETE
					dbo.CFCIngredientNutrientOverlay
				FROM 
					#Overlay O
					INNER JOIN 
						dbo.CFCNutrient CN
							ON CN.CFCNutrientCode = O.NutrientCode 
							AND CN.AIMDatabaseID = O.AIMDatabaseID
					RIGHT OUTER JOIN 
						dbo.CFCIngredientNutrientOverlay CINO
							ON  CINO.CFCPlantID = O.CFCPlantID 
							AND CINO.CFCIngredientID = O.CFCIngredientID 
							AND CINO.CFCNutrientID = CN.CFCNutrientID 
				WHERE
					CINO.CFCPlantID = @CFCPlantID
					AND CINO.CFCIngredientID = @CFCIngredientID
					AND O.CFCPlantID IS NULL

				DROP TABLE #Overlay
			END
	END

SELECT 
	  @RecordID AS RecordID
	, @ResultNumber AS ErrorNumber
	, @ResultMessage AS ErrorMessage

END