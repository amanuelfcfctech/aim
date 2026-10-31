CREATE PROCEDURE [dbo].[usp_Report_ExportBatchDetail]
	
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Report_ExportBatchDetail

SELECT

  H.UpdateIngredientHeaderID 
  , H.UpdateName
  , H.UpdateStamp
  , H.UpdateCFCUserID 
  , AD.Name as AimDatabaseName 
  , case when H.ExportTargetID = 1 then 'Base' else case when H.ExportTargetID = 2 then 'Default' else case when H.ExportTargetID = 3  then 'Specific' else '' end end end  as TargetOverlay    
  , H.CreateDate 
  , CP.CFCPlantName
  , D.CFCIngredientID  
  , D.CFCNutrientID
  , round(D.ProposedValue,3) as ProposedValue
  , round(D.Mean,3) as Mean
  , round(D.StandardDeviation,3) as StandardDeviation
  , D.Override   
  , CI.CFCIngredientName
  , CI.CFCIngredientCode
  , N.CFCNutrientCode 
  , N.CFCNutrientName
  , case when D.Override > 0 then D.Override else Round(D.ProposedValue,3) end as UsedValue  
  , D.Status
  , CP.CFCPlantCode
  , ISNULL(CN.Value, 0) as CurrentNutrientVal
  , U.UserName

FROM
dbo.UpdateIngredientDetail D WITH(NOLOCK)
	INNER JOIN 
		dbo.UpdateIngredientHeader H 
			ON D.UpdateIngredientHeaderID = H.UpdateIngredientHeaderID
	INNER JOIN 
		dbo.CFCIngredient CI WITH(NOLOCK)
			ON D.CFCIngredientID = CI.CFCIngredientID
	INNER JOIN 
		dbo.CFCNutrient N WITH(NOLOCK)
			ON D.CFCNutrientID = N.CFCNutrientID
	LEFT OUTER JOIN 
		dbo.CFCIngredientNutrient CN WITH(NOLOCK)
			ON	CN.CFCNutrientID = N.CFCNutrientID 
			AND CN.CFCIngredientID = CI.CFCIngredientID 
	LEFT OUTER JOIN
		dbo.CFCPlant CP with (nolock)
			ON D.CFCPlantID = CP.CFCPlantID 
	LEFT OUTER JOIN 
		CFCUser.CFCUser U WITH(NOLOCK) 
			ON D.UpdateCFCUserID = U.CFCUserID
	LEFT OUTER JOIN dbo.AIMDatabase AD
	ON AD.AIMDatabaseID = H.AIMDatabaseID


WHERE H.STATUS = 1 


END