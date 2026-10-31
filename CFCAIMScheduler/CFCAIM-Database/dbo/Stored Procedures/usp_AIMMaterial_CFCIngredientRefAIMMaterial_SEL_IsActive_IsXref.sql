CREATE PROCEDURE [dbo].[usp_AIMMaterial_CFCIngredientRefAIMMaterial_SEL_IsActive_IsXref]
		  @IsActive				BIT				
		, @IsXref				BIT
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_AIMMaterial_CFCIngredientRefAIMMaterial_SEL_IsActive_IsXref 1, 0

DECLARE @SQL				VARCHAR(6000) = ''

SET @SQL = 

'SELECT
  AM.AIMMaterialID 
, AM.AIMMaterialCode
, AM.AIMMaterialName
, AM.MaterialTypeID	
, AM.IsActive
, AM.CFCUserID	
, AM.AIMOwnerLocationID									

FROM 
	dbo.AIMMaterial AM WITH(NOLOCK) '

IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCIngredientRefAIMMaterial x 
				ON AM.aimmaterialid = x.AIMMaterialID 

WHERE 
	AM.IsActive = ' + Cast(@IsActive as varchar) + ' AND AM.MaterialTypeID = 1 '

If @IsXref = 0
	SET @SQL += 'and x.CFCIngredientRefAIMMaterialID is null '


--print @SQL
EXEC (@SQL)

END