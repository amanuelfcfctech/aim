CREATE PROCEDURE [dbo].[usp_ProductToleranceNutrient_DEL_ProductToleranceNutrientID]
	  @ProductToleranceNutrientID		INT
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.ProductToleranceNutrient	
WHERE 
	ProductToleranceNutrientID = @ProductToleranceNutrientID
END