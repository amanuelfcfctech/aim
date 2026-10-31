CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_SpecificProductsGroupID_IsActive_OwnerLocationID]
		  @SpecificProductsGroupID	INT
		, @IsActive					BIT
		, @AIMOwnerLocationID		INT
		
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMMaterial_SEL_SpecificProductsGroupID_IsActive_OwnerLocationID 3, true, 6

SELECT DISTINCT 
	SP.SpecificProductsID
	, SP.SpecificProductsGroupID
	, AM.AIMMaterialID
	, AM.MaterialTypeID	
	, AM.AIMMaterialName
	, AM.AIMMaterialCode
	, AM.IsActive
	, AM.UpdateStamp
	, AM.CFCUserID	
	, AM.AIMOwnerLocationID
	, AOL.LocationName as AIMOwnerLocationName
	, U.UserName

FROM
	dbo.SpecificProducts SP

INNER JOIN
	dbo.AIMMaterial AM 
		ON SP.AIMMaterialID = AM.AIMMaterialID
INNER JOIN 
	dbo.AIMOwnerLocation AOL
		ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
LEFT OUTER JOIN
	CFCUser.CFCUser U 
		ON AM.CFCUserID = U.CFCUserID

    
WHERE 
	SP.SpecificProductsGroupID = @SpecificProductsGroupID AND
	AM.IsActive = @IsActive AND
	AM.AIMOwnerLocationID = @AIMOwnerLocationID 
	
ORDER BY AM.AIMMaterialCode

END