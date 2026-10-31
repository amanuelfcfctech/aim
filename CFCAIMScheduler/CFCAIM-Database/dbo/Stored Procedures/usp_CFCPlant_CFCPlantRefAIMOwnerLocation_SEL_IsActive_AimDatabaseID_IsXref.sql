CREATE PROCEDURE [dbo].[usp_CFCPlant_CFCPlantRefAIMOwnerLocation_SEL_IsActive_AimDatabaseID_IsXref]
				  @IsActive				BIT
				, @AIMDatabaseID		INT
				, @IsXref				BIT

AS
BEGIN
SET NOCOUNT ON;

--exec usp_CFCPlant_CFCPlantRefAIMOwnerLocation_SEL_IsActive_AimDatabaseID_IsXref 1, 1, 1

DECLARE @SQL				VARCHAR(6000) = ''


SET @SQL = 

'SELECT
	  CP.*	
	, AD.Name as AimDatabaseName
	, U.UserName	


FROM 
		dbo.CFCPlant CP WITH(NOLOCK)
	LEFT OUTER JOIN dbo.AIMDatabase AD with (nolock) 
			ON CP.AIMDatabaseID = AD.AIMDatabaseID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON CP.CFCUserID = U.CFCUserID '

IF @IsXref = 1
	SET @SQL += 'INNER JOIN '					
ELSE
	SET @SQL += 'LEFT OUTER JOIN '

SET @SQL += 'dbo.CFCPlantRefAIMOwnerLocation x 
				ON CP.CFCPlantID = x.CFCPlantID 

WHERE 
	CP.IsActive = ' + cast(@IsActive as VARCHAR) + ' and CP.AIMDatabaseID = ' + cast(@AIMDatabaseID as VARCHAR)

If @IsXref = 0
	SET @SQL += ' and x.CFCPlantRefAIMOwnerLocationID is null '

--print @SQL
Exec (@SQL)

END