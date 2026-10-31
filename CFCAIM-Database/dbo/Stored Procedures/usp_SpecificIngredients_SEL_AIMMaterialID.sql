CREATE PROCEDURE [dbo].[usp_SpecificIngredients_SEL_AIMMaterialID]
	
		@AIMMaterialID				INT

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

WHERE
	SP.AIMMaterialID = @AIMMaterialID
	
END