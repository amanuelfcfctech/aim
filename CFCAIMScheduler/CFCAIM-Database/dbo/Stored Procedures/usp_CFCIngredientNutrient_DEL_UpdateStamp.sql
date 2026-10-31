
CREATE PROCEDURE [dbo].[usp_CFCIngredientNutrient_DEL_UpdateStamp]
					@UpdateStamp		DATETIME
AS
BEGIN
SET NOCOUNT ON;

--6/5/19
--DELETE 
--	dbo.CFCNutrientRefAIMNutrient
--FROM
--	dbo.CFCIngredientNutrient CIN
--	INNER JOIN 
--		dbo.CFCNutrientRefAIMNutrient CNRAM
--			ON CNRAM.CFCNutrientID = CIN.CFCNutrientID 
--WHERE
--	CIN.UpdateStamp < @UpdateStamp
DELETE 
	dbo.CFCIngredient 
WHERE
	UpdateStamp < @UpdateStamp


DELETE
	dbo.CFCIngredientNutrient 
WHERE 
	UpdateStamp < @UpdateStamp


END