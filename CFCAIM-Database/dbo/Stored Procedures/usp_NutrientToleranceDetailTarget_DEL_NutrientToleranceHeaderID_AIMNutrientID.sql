CREATE PROCEDURE [dbo].[usp_NutrientToleranceDetailTarget_DEL_NutrientToleranceHeaderID_AIMNutrientID]
	  @NutrientToleranceHeaderID		INT
	  , @AIMNutrientID					INt
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.NutrientToleranceDetailTarget	
WHERE 
	NutrientToleranceHeaderID = @NutrientToleranceHeaderID AND
	AIMNutrientID = @AIMNutrientID
	
	
END