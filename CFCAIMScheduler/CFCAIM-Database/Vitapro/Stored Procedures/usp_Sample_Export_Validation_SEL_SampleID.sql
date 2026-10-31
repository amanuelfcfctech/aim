CREATE PROCEDURE [Vitapro].[usp_Sample_Export_Validation_SEL_SampleID]
			@SampleID	INT
AS

--exec [Vitapro].[usp_Sample_Export_Validation_SEL_SampleID] 5

BEGIN

SET NOCOUNT ON;
DECLARE @RecordID		INT = 0
DECLARE @ErrorNumber	INT = 0
DECLARE @ErrorMessage	VARCHAR(200) = ''
DECLARE @ReturnIntegerValue INT = 0

-- 5/9/19 - commented
--Declare @AIMOwnerCode nvarchar(20)
--Declare @AIMOwnerLocationCode nvarchar(20)

Declare @AIMMaterialCode nvarchar(20)
Declare @AIMNutrientCode nvarchar(20)
Declare @NutrientCount int 

-- 5/9/19 - commented
--SET @AIMOwnerCode = (SELECT ISNULL(A.OwnerCode, '') FROM dbo.AIMOwner A Inner Join dbo.Sample S on A.AIMOwnerID = S.AIMOwnerID WHERE A.AIMOwnerID = S.AIMOwnerID and S.SampleID = @SampleID) 
--SET @AIMOwnerLocationCode = (SELECT ISNULL(A.LocationCode, '') FROM dbo.AIMOwnerLocation A Inner Join dbo.Sample S on A.AIMOwnerLocationID = S.AIMOwnerLocationID WHERE A.AIMOwnerLocationID = S.AIMOwnerLocationID and S.SampleID = @SampleID) 

SET @AIMMaterialCode = (SELECT ISNULL(A.AIMMaterialCode, '') FROM dbo.AIMMaterial A Inner Join dbo.Sample S on A.AIMMaterialID = S.AIMMaterialID WHERE S.SampleID = @SampleID) 
SET @AIMNutrientCode = (SELECT TOP 1 ISNULL(SA.AIMNutrientID, '') FROM dbo.SampleAIM SA WHERE SA.SampleID = @SampleID)
SET @NutrientCount = 0 

----------------------------------------------------------------------------------
-- SAMPLE AIM
IF NOT EXISTS (SELECT 
		SA.SampleID 
	FROM 
		Vitapro.Lot V
	INNER JOIN 
		dbo.SampleAIM SA 
			ON V.SampleID = SA.SampleID	
	WHERE 
		V.SampleID = @SampleID)
	BEGIN		
		SET @ErrorNumber = 1
		--SET @ErrorMessage = 'Sample does not exist in Sample AIM table - SampleAIM table rebuild may be necessary.'		
		SET @ErrorMessage = 'La muestra no existe en la tabla AIM de muestra; la reconstrucción de la tabla SampleAIM puede ser necesaria.  (Sample does not exist in Sample AIM table - SampleAIM table rebuild may be necessary.)'

	END	

----------------------------------------------------------------------------------
-- 5/9/19 - moved this to the program, after it calls usp_lineRecCFCPlant_SEL_LineID  (because Vitapro doesn't use the AIM/CFC plant cross-ref, it uses Line/CFC xref table.
------ FORMULATION PLANT XREF
----ELSE IF NOT EXISTS (SELECT 
----		S.AIMOwnerLocationID 
----	FROM 
----		Vitapro.Lot V
----	INNER JOIN 
----		dbo.Sample S 
----			ON V.SampleID = S.SampleID
----	INNER JOIN
----		dbo.CFCPlantRefAIMOwnerLocation CFCX
----			ON S.AIMOwnerLocationID = CFCX.AIMOwnerLocationID
----	WHERE 
----		V.SampleID = @SampleID)
----	BEGIN		
----		SET @ErrorNumber = 2
----		--SET @ErrorMessage = 'Formulation Plant cross-reference missing for Owner/OwnerLocationCode:  ' + @AIMOwnerCode + '/' + @AIMOwnerLocationCode + '.'		
----		SET @ErrorMessage = 'Falta la referencia cruzada de la planta de formulación para Owner / OwnerLocationCode:  ' + @AIMOwnerCode + '/' + @AIMOwnerLocationCode + '.'		
----	END	

----------------------------------------------------------------------------------
-- FORMULATION INGREDIENT XREF
ELSE IF NOT EXISTS (SELECT
		S.AIMMaterialID
	FROM	
		Vitapro.Lot V			
	INNER JOIN 
		dbo.Sample S 
			ON V.SampleID = S.SampleID
	INNER JOIN
		dbo.CFCIngredientRefAIMMaterial CIRAM 
			on S.AIMMaterialID = CIRAM.AIMMaterialID
	WHERE 
		V.SampleID = @SampleID)
	BEGIN					
		SET @ErrorNumber = 3
		--SET @ErrorMessage = 'Formulation Ingredient cross-reference missing for Material:  ' + @AIMMaterialCode + '.'	
		SET @ErrorMessage = 'Faltan referencias cruzadas de ingredientes de formulación para el material:  ' + @AIMMaterialCode + '.   (Formulation Ingredient cross-reference.)'	
	END	

----------------------------------------------------------------------------------
-- FORMULATION NUTRIENT XREF
ELSE
	BEGIN	
		SET @NutrientCount = 
			(SELECT count(*) 		
			FROM 
				Vitapro.Lot V
			INNER JOIN 
				dbo.SampleAIM SA
					ON V.SampleID = SA.SampleID
			LEFT OUTER JOIN
				dbo.CFCNutrientRefAIMNutrient CNRAN
					ON SA.AIMNutrientID = CNRAN.AIMNutrientID
			WHERE 
				V.SampleID = @SampleID AND CNRAN.AIMNutrientID IS NOT NULL)
		IF @NutrientCount = 0 
			BEGIN							
				SET @ErrorNumber = 4
				--SET @ErrorMessage = 'Formulation Nutrient cross-reference missing for Nutrient code:  ' + @AIMNutrientCode		
				SET @ErrorMessage = 'Falta la referencia cruzada de la fórmula de nutrientes para el código de nutrientes:  ' + @AIMNutrientCode + '.   (Formulation Nutrient cross-reference.)'	
			END
	END


SELECT 
	  @SampleID as RecordID
	, @ReturnIntegerValue as ReturnIntegerValue
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END