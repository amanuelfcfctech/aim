CREATE PROCEDURE [dbo].[usp_AIMSourceLocationGuarantees_SEL_IsActive_AIMSourceID_AIMSourceLocationID_AIMMaterialID_AIMNutrientID]
		@IsActive					BIT
		, @AIMSupplierID			INT
		, @AIMSupplierLocationID	INT
		, @AIMMaterialID			INT
		, @AIMNutrientID			INT
AS
BEGIN
SET NOCOUNT ON;

-- 1/11/21 - changed to remove aspnet_User table (and use CFCUser instead)

-- exec usp_AIMSourceLocationGuarantees_SEL_IsActive_AIMSourceID_AIMSourceLocationID_AIMMaterialID_AIMNutrientID 1, 0, 0, 0, 0

DECLARE @SQL				VARCHAR(6000) = ''

SET @SQL = 
'
SELECT 
	    SLG.*	  
	  , AM.SourceName
	  , ASL.SourceLocationName
	  , AMM.AIMMaterialName
	  , AN.AIMNutrientName
	  , U.UserName

FROM 
	dbo.AIMSourceLocationGuarantee SLG WITH(NOLOCK)
	LEFT OUTER JOIN
		dbo.AIMSource AM 
			on SLG.AIMSourceID = AM.AIMSourceID
	LEFT OUTER JOIN
		dbo.AIMSourceLocation ASL 
			on SLG.AIMSourceLocationID = ASL.AIMSourceLocationID
	LEFT OUTER JOIN
		dbo.AIMMaterial AMM
			on SLG.AIMMaterialID = AMM.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.AIMNutrient AN
			ON SLG.AIMNutrientID = AN.AIMNutrientID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON SLG.CFCUserID = U.CFCUserID
	
WHERE 
	SLG.IsActive = ' + CAST(@IsActive AS VARCHAR) 

IF @AIMSupplierID > 0
	SET @SQL = @SQL + ' and SLG.AIMSourceID = ' + CAST(@AIMSupplierID AS VARCHAR)
IF @AIMSupplierLocationID > 0
	SET @SQL = @SQL + ' and SLG.AIMSourceLocationID = ' + CAST(@AIMSupplierLocationID AS VARCHAR)
IF @AIMMaterialID > 0
	SET @SQL = @SQL + ' and SLG.AIMMaterialID = ' + CAST(@AIMMaterialID AS VARCHAR)
IF @AIMNutrientID > 0
	SET @SQL = @SQL + ' and SLG.AIMNutrientID = ' + CAST(@AIMNutrientID AS VARCHAR)
	
SET @SQL = @SQL + '
ORDER BY
	AM.SourceName, ASL.SourceLocationName, AMM.AIMMaterialName, AN.AIMNutrientName '

--print @Sql
EXEC (@SQL)

END