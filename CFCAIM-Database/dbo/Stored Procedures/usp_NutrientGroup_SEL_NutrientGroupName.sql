CREATE PROCEDURE [dbo].[usp_NutrientGroup_SEL_NutrientGroupName]
	@NutrientGroupName NVARCHAR(50)

AS

SELECT  
       NG.NutrientGroupID
      ,NG.NutrientGroupName
      ,NG.IsActive
      ,NG.UpdateStamp
      ,NG.CFCUserID
  FROM dbo.NutrientGroup NG
		
WHERE
	TRIM(UPPER(NG.NutrientGroupName)) = TRIM(@NutrientGroupName)