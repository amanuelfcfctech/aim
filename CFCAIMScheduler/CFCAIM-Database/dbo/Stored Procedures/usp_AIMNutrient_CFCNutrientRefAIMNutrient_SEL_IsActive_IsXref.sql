CREATE PROCEDURE [dbo].[usp_AIMNutrient_CFCNutrientRefAIMNutrient_SEL_IsActive_IsXref]
		  @IsActive				BIT
		, @IsXref				BIT

AS
BEGIN
SET NOCOUNT ON;
  
-- exec usp_AIMNutrient_CFCNutrientRefAIMNutrient_SEL_IsActive_IsXref 1, 1

DECLARE @SQL				VARCHAR(6000) = ''

SET @SQL =  

'SELECT
     NN.*

FROM 
   dbo.AIMNutrient NN WITH(NOLOCK) '

IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCNutrientRefAIMNutrient x 
				ON NN.AIMNutrientID = x.AIMNutrientID 

WHERE 
   NN.IsActive = ' + cast(@IsActive as VARCHAR)

If @IsXref = 0
	SET @SQL += 'and x.CFCNutrientRefAIMNutrientID is null '


--print @SQL
EXEC (@SQL)

END