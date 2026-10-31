CREATE PROCEDURE [dbo].[usp_AIMMaterial_CFCProductRefAIMMaterial_SEL_IsActive_IsXref]
		  @IsActive				BIT				
		, @IsXref				BIT
		, @OwnerLocationID	    INT		-- added so AIM products are filtered by plant on Formulation x-ref screen. 2/13/25
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_AIMMaterial_CFCProductRefAIMMaterial_SEL_IsActive_IsXref 1, 1, 5

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
, AOL.LocationName as AIMOwnerLocationName

FROM 
	dbo.AIMMaterial AM '

IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCProductRefAIMMaterial x 
				ON AM.AIMMaterialid = x.AIMMaterialID 

LEFT OUTER JOIN dbo.AIMOwnerLocation AOL
	ON AM.AIMOwnerLocationID = AOL.AIMOwnerLocationID

WHERE 
	AM.IsActive = ' + Cast(@IsActive as varchar) + ' AND 
	AM.MaterialTypeID = 2 and
	AM.AIMOwnerLocationID = ' + cast(@OwnerLocationID as VARCHAR) 

If @IsXref = 0
	SET @SQL += 'and x.CFCProductRefAIMMaterialID is null '


--print @SQL
EXEC (@SQL)

END