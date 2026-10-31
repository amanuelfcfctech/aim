CREATE PROCEDURE [dbo].[usp_ExportIngredient_SEL_ExportIngredientGroupID]
	
		@ExportIngredientGroupID		INT
		

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  EI.ExportIngredientID
	, EI.ExportIngredientGroupID
	, EI.CFCIngredientID	
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, EI.IsActive
	, EI.UpdateStamp
	, EI.CFCUserID	
	, U.UserName
	, EIG.ExportIngredientGroupCode
	, EIG.AIMMaterialID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName

FROM 
	dbo.ExportIngredient EI
INNER JOIN
	dbo.ExportIngredientGroup EIG
		ON EI.ExportIngredientGroupID = EIG.ExportIngredientGroupID
INNER JOIN
	dbo.CFCIngredient CI
		ON CI.CFCIngredientID = EI.CFCIngredientID
LEFT OUTER JOIN
	dbo.AIMMaterial AM 
		ON AM.AIMMaterialID = EIG.AIMMaterialID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON AM.CFCUserID = U.CFCUserID

WHERE
	EI.ExportIngredientGroupID = @ExportIngredientGroupID 
	
END