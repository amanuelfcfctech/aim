CREATE PROCEDURE [dbo].[usp_NutrientGroup_DEL_NutrientGroupID]
			  @NutrientGroupID	INT
AS
	
DELETE FROM 
	dbo.NutrientGroupDetail
WHERE 
	NutrientGroupID = @NutrientGroupID

DELETE FROM	
	dbo.NutrientGroup 
WHERE
	NutrientGroupID = @NutrientGroupID