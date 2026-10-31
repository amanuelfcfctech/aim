CREATE PROCEDURE [dbo].[usp_ExportIngredientGroup_SEL_IsActive]

	@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  EI.ExportIngredientGroupID
	, EI.ExportIngredientGroupCode	
    , EI.AIMMaterialID
	, EI.IsActive
	, EI.UpdateStamp
	, EI.CFCUserID	
	, U.UserName

FROM 
	dbo.ExportIngredientGroup EI

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON EI.CFCUserID = U.CFCUserID

WHERE
	EI.IsActive = @IsActive 
	
END