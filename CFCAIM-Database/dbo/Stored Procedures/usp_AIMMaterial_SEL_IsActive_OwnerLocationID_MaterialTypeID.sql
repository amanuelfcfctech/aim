
CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_IsActive_OwnerLocationID_MaterialTypeID]
		@IsActive				BIT
		, @AIMOwnerLocationID	INT
		, @MaterialTypeID		INT
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMMaterial_SEL_IsActive_OwnerLocationID_MaterialTypeID true, 6, 2

SELECT 
	  AM.AIMMaterialID
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
	dbo.AIMMaterial AM 
	INNER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON AM.CFCUserID = U.CFCUserID

    
WHERE 
	AM.IsActive = @IsActive AND
	AM.AIMOwnerLocationID = @AIMOwnerLocationID AND
	AM.MaterialTypeID = @MaterialTypeID


ORDER BY AM.AIMMaterialCode

END