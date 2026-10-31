CREATE PROCEDURE [dbo].[usp_SampleAIM_DEL_AimNutrientID]
			    @AIMNutrientID		INT	

--exec usp_SampleAIM_DEL_AimNutrientID 10

			
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.SampleAIM with (rowlock)
WHERE 	
	AIMNutrientID = @AIMNutrientID 
	

END