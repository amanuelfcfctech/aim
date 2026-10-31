CREATE PROCEDURE [dbo].[usp_SpecificIngredients_SEL]

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SP.SpecificIngredientsID
	, SP.SpecificIngredientsGroupID
	, SP.AIMMaterialID	
	, AM.AIMMaterialName
	, AM.AIMMaterialCode
	, SP.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID	
	, U.UserName
	, SPG.SpecificIngredientsGroupCode

FROM 
	dbo.SpecificIngredients SP
INNER JOIN
	dbo.SpecificIngredientsGroup SPG
		ON SP.SpecificIngredientsGroupID = SPG.SpecificIngredientsGroupID
INNER JOIN
	dbo.AIMMaterial AM 
		ON AM.AIMMaterialID = SP.AIMMaterialID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON AM.CFCUserID = U.CFCUserID

-- Get all records but, on maintenance screen, show inactive with a different color	and readonly	
--WHERE
--	SP.IsActive = 1 and AM.IsActive = 1
	
END