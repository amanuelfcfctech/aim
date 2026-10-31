CREATE PROCEDURE [dbo].[usp_NutrientToleranceDetail_DEL_NutrientToleranceHeaderID_AIMNutrientID]
	  @NutrientToleranceHeaderID		INT
	  , @AIMNutrientID					INt
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.NutrientToleranceDetail
WHERE 
	NutrientToleranceHeaderID = @NutrientToleranceHeaderID AND
	AIMNutrientID = @AIMNutrientID
	
	
END