CREATE PROCEDURE [dbo].[usp_ExportIngredient_SEL_CFCIngredientID]

@CFCIngredientID INT

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

FROM 
	dbo.ExportIngredient EI
INNER JOIN
	dbo.ExportIngredientGroup EIG
		ON EI.ExportIngredientGroupID = EIG.ExportIngredientGroupID
INNER JOIN
	dbo.CFCIngredient CI
		ON CI.CFCIngredientID = EI.CFCIngredientID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON EI.CFCUserID = U.CFCUserID

WHERE
	EI.CFCIngredientID = @CFCIngredientID

-- Get all records but, on maintenance screen, show inactive with a different color	and readonly	
--WHERE
--	SP.IsActive = 1 and AM.IsActive = 1
	
END