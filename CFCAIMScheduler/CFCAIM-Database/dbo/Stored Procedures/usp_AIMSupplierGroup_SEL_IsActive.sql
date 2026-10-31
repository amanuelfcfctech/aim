CREATE PROCEDURE [dbo].[usp_AIMSupplierGroup_SEL_IsActive]

	@IsActive		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ASG.AIMSupplierGroupID
	, ASG.AIMSupplierGroupCode	
	, ASG.IsActive
	, ASG.UpdateStamp
	, ASG.CFCUserID	
	, U.UserName

FROM 
	dbo.AIMSupplierGroup ASG

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ASG.CFCUserID = U.CFCUserID

WHERE
	ASG.IsActive = @IsActive 

ORDER BY
	ASG.AIMSupplierGroupCode ASC
	
END