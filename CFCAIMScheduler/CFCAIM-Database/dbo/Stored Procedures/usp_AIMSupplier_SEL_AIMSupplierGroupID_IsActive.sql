CREATE PROCEDURE [dbo].[usp_AIMSupplier_SEL_AIMSupplierGroupID_IsActive]
		@AIMSupplierGroupID		INT
		, @IsActive				BIT


--exec usp_AIMSupplier_SEL_AIMSupplierGroupID_IsActive 0, 1


AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(MAX) = ''
SET @SQL = 

'SELECT
	  AMS.AIMSourceID
	  , AMS.AIMSupplierCode
	  , AMS.SourceName
	  , AMS.IsActive
	  , AMS.UpdateStamp
	  , AMS.CFCUserID
	  , U.UserName

FROM 
	dbo.AIMSource AMS 
		LEFT OUTER JOIN
			CFCUser.CFCUser U 
				ON AMS.CFCUserID = U.CFCUserID '

IF @AIMSupplierGroupID > 0
	SET @SQL += '		
		LEFT OUTER JOIN	
			dbo.AIMSupplierGroupDetail ASGD
				ON AMS.AIMSourceID = ASGD.AIMSupplierID 
		LEFT OUTER JOIN
			dbo.AIMSupplierGroup ASG
				ON ASG.AIMSupplierGroupID = ASGD.AIMSupplierGroupID	'

SET @SQL += '

WHERE 
	AMS.IsActive = ' + CAST(@IsActive as VARCHAR)

IF @AIMSupplierGroupID > 0
	SET @SQL += ' and ASG.AIMSupplierGroupID = ' + CAST(@AIMSupplierGroupID as VARCHAR)

SET @SQL +=

' ORDER BY AMS.SourceName '

EXEC (@SQL)
--PRINT @SQL

END