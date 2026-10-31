CREATE PROCEDURE [dbo].[usp_NutrientGroupDetail_DEL_NutrientGroupDetailID]
			  @NutrientGroupDetailID	INT
AS
	
DELETE FROM 
	dbo.NutrientGroupDetail
WHERE 
	NutrientGroupDetailID = @NutrientGroupDetailID