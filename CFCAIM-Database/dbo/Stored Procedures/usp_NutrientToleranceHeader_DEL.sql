CREATE PROCEDURE [dbo].[usp_NutrientToleranceHeader_DEL]
	  @NutrientToleranceHeaderID		INT
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.NutrientToleranceHeader	
WHERE 
	NutrientToleranceHeaderID = @NutrientToleranceHeaderID
	
	
END