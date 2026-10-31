CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationGroup_SEL_IsActive]

	@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ALG.AIMOwnerLocationGroupID
	, ALG.AIMOwnerLocationGroupCode	
	, ALG.IsActive
	, ALG.UpdateStamp
	, ALG.CFCUserID	
	, U.UserName

FROM 
	dbo.AIMOwnerLocationGroup ALG

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ALG.CFCUserID = U.CFCUserID

WHERE
	ALG.IsActive = @IsActive 
	
END