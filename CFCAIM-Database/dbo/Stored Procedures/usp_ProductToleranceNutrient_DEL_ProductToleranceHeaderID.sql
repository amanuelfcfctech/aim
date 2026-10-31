CREATE PROCEDURE [dbo].[usp_ProductToleranceNutrient_DEL_ProductToleranceHeaderID]
	  @ProductToleranceHeaderID		INT
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.ProductToleranceNutrient	
WHERE 
	ProductToleranceHeaderID = @ProductToleranceHeaderID
END