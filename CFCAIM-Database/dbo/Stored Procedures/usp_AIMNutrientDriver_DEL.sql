CREATE PROCEDURE [dbo].[usp_AIMNutrientDriver_DEL]
	  @QueryTemplateID		INT
	  , @AIMNutrientID		INT
	  
AS
BEGIN
SET NOCOUNT ON;

DELETE
	dbo.AIMNutrientDriver	WITH(ROWLOCK)
WHERE 
	QueryTemplateID = @QueryTemplateID and
	AIMNutrientID = @AIMNutrientID
	
END