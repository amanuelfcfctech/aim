CREATE PROCEDURE [dbo].[usp_AIMMaterial_SEL_IsActive_MaterialTypeID_For_Sample]
		@IsActive				BIT		
		, @MaterialTypeID		INT

		--6/18/20
		, @AIMOwnerLocationID				INT = NULL


-- 1/11/21 - changed to remove aspnet_User table (and UserName from CFCUser.CFCUser table instead)

		
AS
BEGIN
SET NOCOUNT ON;

--exec usp_AIMMaterial_SEL_IsActive_MaterialTypeID_For_Sample 1, 1, NULL

DECLARE @SQL VARCHAR(5000) = ''
DECLARE @Count INTEGER = 0

--6/18/20
if @AIMOwnerLocationID is not null
	SET @Count = (
		Select 
			count(*) 
		From 
			dbo.AIMMaterialStockStatus
		Where 
			AIMOwnerLocationID = @AIMOwnerLocationID) 


SET @SQL = '
SELECT 
	  AM.AIMMaterialID 
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AM.MaterialTypeID	
	, AM.IsActive
	, AM.CFCUserID
	, U.UserName	

FROM 
	dbo.AIMMaterial AM WITH(NOLOCK)
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON AM.CFCUserID = U.CFCUserID '

--6/18/20			
IF @Count > 0 
	Begin
	SET @SQL = @SQL + '
		INNER JOIN
			dbo.AIMMaterialStockStatus AML
				ON AM.AIMMaterialID = AML.AIMMaterialID '		
	End

SET @SQL = @SQL + ' 

WHERE 
	AM.IsActive = ' + CAST(@IsActive as varchar) + ' AND	
	AM.MaterialTypeID = ' + CAST(@MaterialTypeID as varchar) 

--6/18/20
IF @Count > 0 	
	SET @SQL = @SQL + ' AND AML.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID as varchar) 
	
SET @SQL = @SQL + '

GROUP BY
	AM.AIMMaterialID 
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AM.MaterialTypeID
	, AM.IsActive
	, AM.CFCUserID
	, U.UserName

ORDER BY AM.AIMMaterialName
'

--PRINT @SQL
EXEC (@SQL)


END