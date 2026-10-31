CREATE PROCEDURE [dbo].[usp_UserDefinedDataSourceDefinition_SEL_AimRequiredFieldID]
 @ID		INT,
 @FieldName	VARCHAR(20)
 			
-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)


--exec dbo.usp_UserDefinedDataSourceDefinition_SEL_AimRequiredFieldID 1, 'AIMSUPPLIERLOCATION'		

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''
DECLARE @Where				VARCHAR(1000) = ''

SET @SQL = '

SELECT 
	  DU.*
	, U.UserName
FROM 
	dbo.UserDefinedDataSourceDefinition DU WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON DU.CFCUserID = U.CFCUserID
	
WHERE '

IF @FieldName = 'AIMOWNER'
	SET @SQL = @SQL + 'AIMOwnerID = ' + Cast(@ID as varchar) + ' ORDER BY DU.ImportFileName '
IF @FieldName = 'AIMOWNERLOCATION'
	SET @SQL = @SQL + 'AIMOwnerLocationID = ' + Cast(@ID as varchar) + ' ORDER BY DU.ImportFileName '
IF @FieldName = 'AIMSUPPLIER'
	SET @SQL = @SQL + 'AIMSourceID = ' + Cast(@ID as varchar) + ' ORDER BY DU.ImportFileName '
IF @FieldName = 'AIMSUPPLIERLOCATION'
	SET @SQL = @SQL + 'AIMSourceLocationID = ' + Cast(@ID as varchar) + ' ORDER BY DU.ImportFileName '
	

EXEC (@SQL)
--PRINT @SQL

END