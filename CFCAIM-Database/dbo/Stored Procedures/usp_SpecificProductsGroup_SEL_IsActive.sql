CREATE PROCEDURE [dbo].[usp_SpecificProductsGroup_SEL_IsActive]

	@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SP.SpecificProductsGroupID
	, SP.SpecificProductsGroupCode	
	, SP.IsActive
	, SP.UpdateStamp
	, SP.CFCUserID	
	, U.UserName

FROM 
	dbo.SpecificProductsGroup SP

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON SP.CFCUserID = U.CFCUserID

WHERE
	SP.IsActive = @IsActive 
	
END