CREATE PROCEDURE [dbo].[usp_CFCIngredient_CFCIngredientRefAIMMaterial_SEL_IsActive_AimDatabaseID_IsXref]
				  @IsActive				BIT
				, @AimDatabaseID		INT 
				, @IsXref				BIT 
AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCIngredient_CFCIngredientRefAIMMaterial_SEL_IsActive_AimDatabaseID_IsXref 1, 1, 0


DECLARE @SQL				VARCHAR(6000) = ''

SET @SQL = 

'SELECT 
	  CI.CFCIngredientID
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, CI.IsActive
	, CI.UpdateStamp	
	, CI.AIMDatabaseID 	
	, AD.Name as AimDatabaseName
	, CI.MathType 


FROM 
	dbo.CFCIngredient CI WITH(NOLOCK) 
	LEFT OUTER JOIN dbo.AIMDatabase AD with (nolock) 
			ON CI.AIMDatabaseID = AD.AIMDatabaseID '
	
IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCIngredientRefAIMMaterial x 
				ON CI.CFCIngredientID = x.CFCIngredientID

WHERE 
	CI.IsActive = ' + cast(@IsActive as VARCHAR) + ' and CI.AIMDatabaseID = ' + cast(@AimDatabaseID as VARCHAR)

If @IsXref = 0
	SET @SQL += ' and x.CFCIngredientRefAIMMaterialID is null '

SET @SQL += ' 
ORDER BY 
	CI.CFCIngredientCode '

--print @SQL
Exec (@SQL)

END