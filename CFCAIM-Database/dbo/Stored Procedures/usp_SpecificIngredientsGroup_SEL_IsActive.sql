CREATE PROCEDURE [dbo].[usp_SpecificIngredientsGroup_SEL_IsActive]

	@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SP.SpecificIngredientsGroupID
	, SP.SpecificIngredientsGroupCode	
	, SP.IsActive
	, SP.UpdateStamp
	, SP.CFCUserID	
	, U.UserName

FROM 
	dbo.SpecificIngredientsGroup SP

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON SP.CFCUserID = U.CFCUserID

WHERE
	SP.IsActive = @IsActive 
	
END