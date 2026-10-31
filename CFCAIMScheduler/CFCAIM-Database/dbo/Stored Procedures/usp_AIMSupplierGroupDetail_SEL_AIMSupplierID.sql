CREATE PROCEDURE [dbo].[usp_AIMSupplierGroupDetail_SEL_AIMSupplierID]

	@AIMSupplierID		INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ASGD.AIMSupplierGroupDetailID
	, ASGD.AIMSupplierGroupID	
	, ASGD.AIMSupplierID
	, ASGD.IsActive
	, ASGD.UpdateStamp
	, ASGD.CFCUserID	
	, U.UserName
	, AMS.AIMSupplierCode
	, AMS.SourceName as AIMSupplierName
	, ASG.AIMSupplierGroupCode
	

FROM 
	dbo.AIMSupplierGroupDetail ASGD

INNER JOIN
	dbo.AIMSupplierGroup ASG
		ON ASGD.AIMSupplierGroupID = ASG.AIMSupplierGroupID

LEFT OUTER JOIN
	dbo.AIMSource AMS
		on ASGD.AIMSupplierID = AMS.AIMSourceID


LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ASGD.CFCUserID = U.CFCUserID

WHERE
	ASGD.AIMSupplierID = @AIMSupplierID 
END