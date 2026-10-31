CREATE PROCEDURE [dbo].[usp_CFCProductType_SEL_AIMDatabaseID]
	
	 @AIMDatabaseID				INT		= NULL

AS


BEGIN
SET NOCOUNT ON;

SELECT 
	  CPT.CFCProductTypeID
	, CPT.ProductTypeName
	, CPT.AIMDatabaseID
	, CPT.UpdateStamp
	, CPT.CFCUserID
	, U.UserName

FROM	
	dbo.CFCProductType CPT

LEFT OUTER JOIN
	CFCUser.CFCUser U
		ON CPT.CFCUserID = U.CFCUserID

WHERE 
	CPT.AIMDatabaseID = @AIMDatabaseID

END