CREATE PROCEDURE [dbo].[usp_NutrientToleranceDetail_DEL_NutrientToleranceHeaderID]
	  @NutrientToleranceHeaderID		INT
	
	
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.NutrientToleranceDetail
WHERE 
	NutrientToleranceHeaderID = @NutrientToleranceHeaderID 
	
END