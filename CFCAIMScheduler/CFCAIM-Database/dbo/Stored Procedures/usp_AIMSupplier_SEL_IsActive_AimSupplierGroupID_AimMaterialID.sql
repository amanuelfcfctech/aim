CREATE PROCEDURE [dbo].[usp_AIMSupplier_SEL_IsActive_AimSupplierGroupID_AimMaterialID]
		@IsActive						BIT
		, @AIMSupplierGroupID			INT
		, @AIMMaterialID				INT

-- 10/17/23 added AIMSupplierGroupID

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL varchar(1000)

SET @SQL = '

SELECT
	
	  AMS.AIMSourceID
	  , AMS.AIMSupplierCode
	  , AMS.SourceName
	  , AMS.IsActive
	  , AMS.UpdateStamp
	  , AMS.CFCUserID
	  , U.UserName
	  
FROM 
	dbo.AIMSource AMS WITH(NOLOCK) '

If @AIMMaterialID > 0 
	SET @SQL = @SQL + ' 
		INNER JOIN				
			dbo.Sample S 
				on AMS.AIMSourceID = S.AIMSourceID '


If @AIMSupplierGroupID > 0
	SET @SQL = @SQL + ' 
		LEFT OUTER JOIN	
			dbo.AIMSupplierGroupDetail ASGD
				ON AMS.AIMSourceID = ASGD.AIMSupplierID 
		LEFT OUTER JOIN
			dbo.AIMSupplierGroup ASG
				ON ASG.AIMSupplierGroupID = ASGD.AIMSupplierGroupID '

	
SET @SQL = @SQL + '
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON AMS.CFCUserID = U.CFCUserID

WHERE 
	AMS.IsActive = ' + cast(@IsActive as varchar) + ''

If @AIMMaterialID > 0 
	SET @SQL = @SQL + ' AND S.AimMaterialID = ' + cast(@AIMMaterialID as varchar) 

If @AIMSupplierGroupID > 0 
	SET @SQL = @SQL + ' AND ASG.AIMSupplierGroupID = ' + CAST(@AIMSupplierGroupID as VARCHAR)

SET @SQL = @SQL + '
GROUP BY
	AMS.AIMSourceID
	, AMS.AIMSupplierCode
	, AMS.SourceName
	, AMS.IsActive
	, AMS.UpdateStamp
	, AMS.CFCUserID
	, U.UserName

ORDER BY AMS.SourceName '

exec (@SQL)
--PRINT @sql

END