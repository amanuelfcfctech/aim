CREATE PROCEDURE [dbo].[usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID_AIMOwnerLocationID]
		@DataSourceTypeID			INT			 	 
		, @MaterialTypeID			INT
		, @AIMOwnerLocationID		INT

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL VARCHAR(5000) = ''

--exec usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID_AIMOwnerLocationID 0, 2, 16 
--exec usp_AIMMaterialRefAnalyzerMaterial_SEL_DataSourceTypeID_MaterialTypeID_AIMOwnerLocationID 10000, 1, 0

--3/17/22 - added check of isActive switch to both ingredient and product sections


DECLARE @Count INTEGER = 0

--6/18/20
if @AIMOwnerLocationID is not null and @AIMOwnerLocationID <> 0
	SET @Count = (
		Select 
			count(*) 
		From 
			dbo.AIMMaterialLocation 			
		Where 
			AIMOwnerLocationID = @AIMOwnerLocationID) 


IF @MaterialTypeID = 0 or @MaterialTypeID = 1
Begin

	---------------------------------- INGREDIENTS ---------------------------------
	SET @SQL = '
	
	SELECT DISTINCT 
		AMM.AIMMaterialID	  
		, AMM.AIMMaterialCode
		, AMM.AIMMaterialName
		, AMM.AIMOwnerLocationID
		, X.DataSourceTypeID
		, AMM.MaterialTypeID 	  
		, X.DataSourceTypeName	
		, '''' as AIMLocationName			
		, 0 as AIMOwnerLocationID
		, X.AnalyzerMaterialCode 
		
	
	FROM dbo.aimMaterial AMM with (nolock) '

	--6/18/20
	IF @Count > 0 
	Begin
	SET @SQL = @SQL + '
		INNER JOIN
			dbo.AIMMaterialLocation AML
				ON AMM.AIMMaterialID = AML.AIMMaterialID '		
	End
	SET @SQL = @SQL + ' 

	LEFT OUTER JOIN
			(SELECT 
				AN.*, X.AIMMaterialID, DST.Name as DataSourceTypeName 
			FROM  
				dbo.AIMMaterialRefAnalyzerMaterial X with (nolock) 
			LEFT OUTER JOIN 
				dbo.AnalyzerMaterial AN with (nolock) 		
					on AN.AnalyzerMaterialID = x.AnalyzerMaterialID 
			LEFT OUTER JOIN
				dbo.DataSourceType DST 
					ON AN.DataSourceTypeID = DST.DataSourceTypeID
			WHERE AN.DataSourceTypeID IS NOT NULL '

			if @DataSourceTypeID <> 0
				SET @SQL = @SQL + ' AND AN.DataSourceTypeID = ' + CAST(@DataSourceTypeID AS VARCHAR) 

			SET @SQL = @SQL + '
			) X
		ON AMM.AIMMaterialID = X.AIMMaterialID

	WHERE AMM.MaterialTypeID = 1
	and AMM.IsActive = 1 ' 
	--and X.DataSourceTypeID IS NOT NULL     --6/18/20 - removed this from WHERE statement above

	-- 4/18/23
	if @DataSourceTypeID > 0
		SET @SQL = @SQL + ' and X.DataSourceTypeID IS NOT NULL '

	
	--6/18/20
	IF @Count > 0 	
		SET @SQL = @SQL + ' AND AML.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID as varchar) 	
	
End

IF @MaterialTypeID = 0 
	SET @SQL = @SQL + ' UNION '

IF @MaterialTypeID = 0 or @MaterialTypeID = 2
Begin
	---------------------------------- PRODUCTS ---------------------------------
	SET @SQL = @SQL + '
		
	SELECT DISTINCT 
		AMM.AIMMaterialID	  
		, AMM.AIMMaterialCode
		, AMM.AIMMaterialName
		, AMM.AIMOwnerLocationID
		, 0 as DataSourceTypeID
		, AMM.MaterialTypeID 
		, '''' as DataSourceTypeName	
		, AOL.LocationName as AIMLocationName	
		, AOL.AIMOwnerLocationID
		, X.CFCProductCode as AnalyzerMaterialCode
							
	
	FROM dbo.aimMaterial AMM with (nolock)
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL
			ON AMM.AIMOwnerLocationID = AOL.AIMOwnerLocationID
	LEFT OUTER JOIN
			(SELECT 
				CFCP.*, X.AIMMaterialID
			FROM  
				dbo.CFCProductRefAIMMaterial X with (nolock) 
			LEFT OUTER JOIN 
				dbo.CFCProduct CFCP with (nolock) 		
					on CFCP.CFCProductID = x.CFCProductID  	
			) X
		ON AMM.AIMMaterialID = X.AIMMaterialID
	WHERE AMM.MaterialTypeID = 2 
	and AMM.IsActive = 1 '
	
	If @AIMOwnerLocationID <> 0
		SET @SQL = @SQL + ' AND AMM.AIMOwnerLocationID = ' + CAST(@AIMOwnerLocationID AS VARCHAR) 

End
	
SET @SQL = @SQL + ' ORDER BY AIMMaterialCode '
 
--PRINT @SQL
EXEC (@SQL)

END