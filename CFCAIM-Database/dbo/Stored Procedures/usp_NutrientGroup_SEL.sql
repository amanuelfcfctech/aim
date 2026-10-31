CREATE PROCEDURE [dbo].[usp_NutrientGroup_SEL]

AS

SELECT  
       NG.NutrientGroupID
      ,NG.NutrientGroupName
      ,NG.IsActive
      ,NG.UpdateStamp
      ,NG.CFCUserID
  FROM dbo.NutrientGroup NG