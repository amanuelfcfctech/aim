CREATE PROCEDURE [dbo].[usp_SpecificProducts_SEL_SpecificProductsGroupID]
	
		@SpecificProductsGroupID		INT

--exec usp_SpecificProducts_SEL_SpecificProductsGroupID 5		

AS
BEGIN
SET NOCOUNT ON;

SELECT 
	  SP.SpecificProductsID
	, SP.SpecificProductsGroupID
	, SP.AIMMaterialID	
	, AM.AIMMaterialName
	, AM.AIMMaterialCode
	, SP.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID	
	, U.UserName
	, SPG.SpecificProductsGroupCode
	, AOL.LocationName as AIMOwnerLocationName
FROM 
	dbo.SpecificProducts SP
INNER JOIN
	dbo.SpecificProductsGroup SPG
		ON SP.SpecificProductsGroupID = SPG.SpecificProductsGroupID
INNER JOIN
	dbo.AIMMaterial AM 
		ON AM.AIMMaterialID = SP.AIMMaterialID
LEFT OUTER JOIN
	dbo.AIMOwnerLocation aol 
		ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON AM.CFCUserID = U.CFCUserID

WHERE
	SP.SpecificProductsGroupID = @SpecificProductsGroupID 
	
END