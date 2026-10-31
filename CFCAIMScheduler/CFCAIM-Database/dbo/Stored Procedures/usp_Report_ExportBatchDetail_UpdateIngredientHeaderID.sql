CREATE PROCEDURE [dbo].[usp_Report_ExportBatchDetail_UpdateIngredientHeaderID]
	@UpdateIngredientHeaderID	INT
   ,@TargetID					INT

AS
BEGIN
SET NOCOUNT ON;

-- exec usp_Report_ExportBatchDetail_UpdateIngredientHeaderID 58, 4
Declare @plantName varchar(20) = ''
declare @plantID	int = 0


IF @TargetID = 3		-- Specific Overlay
	Begin
		Set @plantID = (Select CFCPlantID from dbo.UpdateIngredientHeader WHERE UpdateIngredientHeaderID = @UpdateIngredientHeaderID)
		Set @plantName = (Select CFCPlantName from dbo.CFCPlant p where p.CFCPlantID = @plantID) 
	End

IF @TargetID = 4		--Base For 
	Begin
		Set @plantID = (Select CFCPlantID from dbo.UpdateIngredientHeader WHERE UpdateIngredientHeaderID = @UpdateIngredientHeaderID)
		Set @plantName = (Select ShortName from dbo.UpdateIngredientHeader WHERE UpdateIngredientHeaderID = @UpdateIngredientHeaderID) 
	End

SELECT

  H.UpdateIngredientHeaderID 
  , H.UpdateName
  , H.UpdateStamp
  , H.UpdateCFCUserID  
  , AD.Name as AimDatabaseName 
  , case when H.ExportTargetID = 1 then 'Base' else 
    case when H.ExportTargetID = 2 then 'Default' else 
	case when H.ExportTargetID = 3  then 'Specific' else 
	case when H.ExportTargetID = 4  then 'Base For' 
	else '' end end end end  as TargetOverlay    
  , H.CreateDate 
  , CP.CFCPlantName
  , D.CFCIngredientID  
  , D.CFCNutrientID
  , Round(D.ProposedValue,3) as ProposedValue
  , Round(D.Mean,3) as Mean
  , Round(D.StandardDeviation,3) as StandardDeviation
  , D.Override   
  , CI.CFCIngredientName
  , CI.CFCIngredientCode
  , N.CFCNutrientName
  , case when D.Override > 0 then D.Override else Round(D.ProposedValue,3) end as UsedValue  
  , N.CFCNutrientCode
  , Sum(D.NumberOfSamples) as NumberOfSamples
  , D.Status
  , CP.CFCPlantCode
  , @plantName as PlantName
  , ISNULL(CN.Value, 0) as CurrentNutrientVal
  , U.UserName
  , D.UpdateCFCUserID
  
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
			ON D.CFCUserID = U.CFCUserID
	LEFT OUTER JOIN dbo.AIMDatabase AD
	ON AD.AIMDatabaseID = H.AIMDatabaseID

WHERE H.UpdateIngredientHeaderID = @UpdateIngredientHeaderID


GROUP BY H.UpdateIngredientHeaderID 
  , H.UpdateName
  , H.UpdateStamp
  , H.UpdateCFCUserID 
  , AD.Name
  , H.ExportTargetID
  , H.CreateDate 
  , CP.CFCPlantName
  , D.CFCIngredientID  
  , D.CFCNutrientID
  , D.ProposedValue
  , D.Mean
  , D.StandardDeviation
  , D.Override   
  , CI.CFCIngredientName
  , CI.CFCIngredientCode
  , N.CFCNutrientName    
  , N.CFCNutrientCode
  , D.Status
  , CP.CFCPlantCode
  , ISNULL(CN.Value, 0)
  , U.UserName
  , D.UpdateCFCUserID

 ORDER BY CI.CFCIngredientCode, N.CFCNutrientCode



END