CREATE PROCEDURE [dbo].[usp_AIMSource_SEL_IsActive_AimMaterialCode]
		@IsActive		    BIT
		, @AimMaterialCode	VARCHAR(20)

--exec [usp_AIMSource_SEL_IsActive_AimMaterialCode] 1, '120000'

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)

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

If len(@AimMaterialCode) > 0 
	SET @SQL = @SQL + ' 
		INNER JOIN				
			dbo.Sample S 
				on AMS.AIMSourceID = S.AIMSourceID 
		LEFT OUTER JOIN
			dbo.AIMMaterial AMM
				on S.AIMMaterialID = AMM.AIMMaterialID '
	
SET @SQL = @SQL + '
LEFT OUTER JOIN
	CFCUser.CFCUser U WITH(NOLOCK)
		ON AMS.CFCUserID = U.CFCUserID


WHERE 
	AMS.IsActive = ' + cast(@IsActive as varchar) + ''

If len(@AimMaterialCode) > 0 
	SET @SQL = @SQL + ' AND AMM.AimMaterialCode = ''' + @AimMaterialCode + ''''

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