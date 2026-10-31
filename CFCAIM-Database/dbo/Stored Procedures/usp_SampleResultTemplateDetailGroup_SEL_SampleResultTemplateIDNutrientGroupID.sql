
CREATE  PROCEDURE [dbo].[usp_SampleResultTemplateDetailGroup_SEL_SampleResultTemplateIDNutrientGroupID]
	  @SampleResultTemplateID	INT
	, @NutrientGroupID			INT
AS

SELECT  
	   SDG.SampleResultTemplateDetailGroupID
      ,SDG.SampleResultTemplateID
      ,SDG.NutrientGroupID
      ,NG.NutrientGroupName
      ,SDG.TestFrequency
      ,SDG.IsDateFrequency
	  ,SDG.StartDate
      ,SDG.UpdateStamp
      ,SDG.CFCUserID
      ,U.UserName
FROM 
	dbo.SampleResultTemplateDetailGroup SDG
INNER JOIN
	dbo.NutrientGroup NG
	ON SDG.NutrientGroupID = NG.NutrientGroupID
INNER JOIN
	CFCUser.CFCUser U
	ON U.CFCUserID = SDG.CFCUserID
WHERE
	SDG.SampleResultTemplateID = @SampleResultTemplateID
	AND NG.NutrientGroupID = @NutrientGroupID