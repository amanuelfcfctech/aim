CREATE PROCEDURE [dbo].[usp_CFCNutrient_CFCNutrientRefAIMNutrient_SEL_IsActive_AimDatabaseID_IsXref]
		  @IsActive				BIT
		, @AimDatabaseID		INT
		, @IsXref				BIT 
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCNutrient_CFCNutrientRefAIMNutrient_SEL_IsActive_AimDatabaseID_IsXref 1, 1, 1

DECLARE @SQL				VARCHAR(6000) = ''


SET @SQL = 

'SELECT
	    CN.*	  
	  , AD.Name as AimDatabaseName
	  , U.UserName

FROM 
	dbo.CFCNutrient CN WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMDatabase AD with (nolock) 
			ON CN.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CN.CFCUserID = U.CFCUserID '

IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCNutrientRefAIMNutrient x 
				ON CN.CFCNutrientID = x.CFCNutrientID 

WHERE 
	CN.IsActive = ' + cast(@IsActive as VARCHAR) + ' and CN.AIMDatabaseID = ' + cast(@AimDatabaseID as VARCHAR) 

If @IsXref = 0
	SET @SQL += ' and x.CFCNutrientRefAIMNutrientID is null '

SET @SQL += ' 

ORDER BY 
	CN.CFCNutrientName '

--print @SQL
Exec (@SQL)

END