CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_IsActive_MaterialTypeID]
		@IsActive				BIT		
		, @MaterialTypeID		INT
		, @OrderBy				INT		-- 1=Name, 2=Code 
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMMaterial_SEL_IsActive_MaterialTypeID 1, 1, 1

DECLARE @SQL				VARCHAR(MAX) = ''
DECLARE @Where				VARCHAR(MAX) = ''

SET @SQL = 
'SELECT 
	  AM.AIMMaterialID 
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AM.MaterialTypeID	
	, AM.IsActive
	, AM.CFCUserID	
	, AM.AIMOwnerLocationID								-- 11/27/19	
	, AOL.LocationName as AIMOwnerLocationName			-- 11/27/19	
	, U.UserName
	, AM.UpdateStamp									-- 4/13/22


FROM 
	dbo.AIMMaterial AM WITH(NOLOCK) '

	--4/1/19 - commented this join -- if user would add new material, it wouldn't show up in grid until a cross-ref was added!
	--INNER JOIN dbo.AIMMaterialRefAnalyzerMaterial x
	--	ON AM.AIMMaterialID = x.AIMMaterialID 

	--11/27/19 - added join of AimOwnerLocation


SET @SQL += 
	
	'LEFT OUTER JOIN
		dbo.AIMOwnerLocation AOL 
			ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID

	LEFT OUTER JOIN
		CFCUser.CFCUser U 
			ON AM.CFCUserID = U.CFCUserID	
WHERE '

SET @SQL += 'AM.IsActive = ' +  Cast(@IsActive as varchar) + ' AND '
SET @SQL += 'AM.MaterialTypeID = ' + Cast(@MaterialTypeID as varchar) 

SET @SQL += 

'GROUP BY
	AM.AIMMaterialID 
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AM.MaterialTypeID
	, AM.IsActive
	, AM.CFCUserID	
	, AM.AIMOwnerLocationID						
	, AOL.LocationName							
	, U.UserName
	, AM.UpdateStamp '

If @OrderBy = 1
	SET @SQL += ' ORDER BY AM.AIMMaterialName '
Else
	SET @SQL += ' ORDER BY AM.AIMMaterialCode '


EXEC (@SQL)

END