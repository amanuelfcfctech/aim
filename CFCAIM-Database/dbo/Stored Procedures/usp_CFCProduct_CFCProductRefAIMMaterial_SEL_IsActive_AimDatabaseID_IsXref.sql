CREATE PROCEDURE [dbo].[usp_CFCProduct_CFCProductRefAIMMaterial_SEL_IsActive_AimDatabaseID_IsXref]
		  @IsActive				BIT
		, @AimDatabaseID		INT
		, @IsXref				BIT		--> 1 = x-ref exists; 0 = xref mising
		, @CFCPlantID			INT		-- added so products are filtered by plant on Formulation x-ref screen. 2/13/25
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCProduct_CFCProductRefAIMMaterial_SEL_IsActive_AimDatabaseID_IsXref 1, 1, 1, 6

DECLARE @SQL				VARCHAR(6000) = ''


SET @SQL = 

'SELECT
	    CP.*	  
	  , AD.Name as AimDatabaseName
	  , U.UserName
	  , P.CFCPlantID
	  , P.CFCPlantCode
	  , P.CFCPlantName

FROM 
	dbo.CFCProduct CP
INNER JOIN
	dbo.CFCPlant P
		ON CP.CFCPlantID = P.CFCPlantID
LEFT OUTER JOIN 
	dbo.AIMDatabase AD
		ON CP.AIMDatabaseID = AD.AIMDatabaseID
LEFT OUTER JOIN
	CFCUser.CFCUser U
		ON CP.CFCUserID = U.CFCUserID '

IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCProductRefAIMMaterial x 
				ON CP.CFCProductID = x.CFCProductID 

WHERE 
	CP.IsActive = ' + cast(@IsActive as VARCHAR) + ' and 
	CP.AIMDatabaseID = ' + cast(@AimDatabaseID as VARCHAR) + ' and
	CP.CFCPlantID = ' + cast(@CFCPlantID as VARCHAR) 

If @IsXref = 0
	SET @SQL += ' and x.CFCProductRefAIMMaterialID is null '

SET @SQL += ' 

ORDER BY 
	CP.CFCProductName '

--print @SQL
Exec (@SQL)

END