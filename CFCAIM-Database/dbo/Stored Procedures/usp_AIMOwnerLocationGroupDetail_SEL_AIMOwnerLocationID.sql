CREATE PROCEDURE [dbo].[usp_AIMOwnerLocationGroupDetail_SEL_AIMOwnerLocationID]

	@AIMOwnerLocationID		INT

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  ALGD.AIMOwnerLocationGroupDetailID
	, ALGD.AIMOwnerLocationGroupID	
	, ALGD.AIMOwnerLocationID
	, ALGD.IsActive
	, ALGD.UpdateStamp
	, ALGD.CFCUserID	
	, U.UserName
	, AOL.LocationCode
	, AOL.LocationName
	, ALG.AIMOwnerLocationGroupCode

FROM 
	dbo.AIMOwnerLocationGroupDetail ALGD

INNER JOIN
	dbo.AIMOwnerLocationGroup ALG
		ON ALGD.AIMOwnerLocationGroupID = ALG.AIMOwnerLocationGroupID

LEFT OUTER JOIN
	dbo.AIMOwnerLocation AOL
		on ALGD.AIMOwnerLocationID = AOL.AIMOwnerLocationID

LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON ALGD.CFCUserID = U.CFCUserID

WHERE
	ALGD.AIMOwnerLocationID = @AIMOwnerLocationID 
END