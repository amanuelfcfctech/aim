CREATE PROCEDURE [dbo].[usp_NutrientToleranceDetailTarget_DEL_NutrientToleranceHeaderID]
	  @NutrientToleranceHeaderID		INT
	
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.NutrientToleranceDetailTarget
WHERE 
	NutrientToleranceHeaderID  = @NutrientToleranceHeaderID 
	
	
END