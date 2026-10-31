CREATE PROCEDURE [dbo].[usp_UpdateIngredientLog_SEL_UpdateIngredientHeaderID]

	@UpdateIngredientHeaderID		INT

-- EXEC DBO.usp_UpdateIngredientLog_SEL_UpdateIngredientHeaderID

AS
BEGIN


SET NOCOUNT ON;

SELECT 
	G.UpdateIngredientDetailID
	, G.PlantCode
	, G.IngredientCode
	, G.NutrientCode
	, G.BeforeNutrientValue
	, G.AfterNutrientValue
	, G.IsOverlay
	, G.CreateStamp
	, G.CFCUserID	
	, CP.CFCPlantName as PlantName
	, CI.CFCIngredientName as IngredientName
	, CN.CFCNutrientName as NutrientName
	, Case when UIG.Override is null or UIG.Override = 0 then Round(ProposedValue, 4) else  Round(Override, 4) end as Used				

	-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
	--, AU.UserName 
	, U.UserName 

FROM 
	dbo.UpdateIngredientDetail UIG with (nolock)

LEFT OUTER JOIN
	dbo.UpdateIngredientLog G with (nolock) 
		ON G.UpdateIngredientDetailID = UIG.UpdateIngredientDetailID

LEFT OUTER JOIN	
	dbo.CFCPlant CP with (nolock)
		ON G.PlantCode = CP.CFCPlantCode

LEFT OUTER JOIN
	dbo.CFCIngredient CI with (nolock)
		ON G.IngredientCode = CI.CFCIngredientCode

LEFT OUTER JOIN
	dbo.CFCNutrient CN with (nolock)
		ON G.NutrientCode = CN.CFCNutrientCode

LEFT OUTER JOIN 
	CFCUser.CFCUser U with (nolock) 
		ON G.CFCUserID = U.CFCUserID

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)
--LEFT OUTER JOIN	
--	dbo.aspnet_Users AU with (nolock) 
--		ON AU.UserId = U.UserID


WHERE
	UIG.UpdateIngredientHeaderID = @UpdateIngredientHeaderID and UIG.Status = 2

END