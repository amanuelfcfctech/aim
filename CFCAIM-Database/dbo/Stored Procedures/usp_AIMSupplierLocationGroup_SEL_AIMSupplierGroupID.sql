CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationGroup_SEL_AIMSupplierGroupID]

	@GroupID		INT

--exec usp_AIMSupplierLocationGroup_SEL_GroupID 7

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ASLG.AIMSupplierLocationGroupID	
	, ASLG.AIMSupplierGroupID
	, ASLG.AIMSupplierID
	, ASLG.AIMSupplierLocationID
	, ASLG.IsActive
	, ASLG.UpdateStamp
	, ASLG.CFCUserID	
	, U.UserName
	, ASL.AIMSupplierLocationCode
	, ASL.SourceLocationName	
	, AimS.AIMSupplierCode
	, ASG.AIMSupplierGroupCode
	

FROM 	
	dbo.AIMSupplierLocationGroup ASLG	

INNER JOIN	
	dbo.AIMSupplierGroup ASG 
		ON ASLG.AIMSupplierGroupID = ASG.AIMSupplierGroupID

LEFT OUTER JOIN
	dbo.AIMSourceLocation ASL
		on ASLG.AIMSupplierLocationID = ASL.AIMSourceLocationID

LEFT OUTER JOIN
	dbo.AIMSource AimS
		on ASL.AIMSourceID = AimS.AIMSourceID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ASLG.CFCUserID = U.CFCUserID

WHERE
	ASLG.AIMSupplierGroupID = @GroupID 
END