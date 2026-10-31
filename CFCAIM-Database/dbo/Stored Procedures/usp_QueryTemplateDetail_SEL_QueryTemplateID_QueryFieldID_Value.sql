CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value]
				@QueryTemplateID		INT
				, @QueryFieldID			INT
				, @Value				INT
				
AS
BEGIN
SET NOCOUNT ON;

-- EXEC dbo.usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID_Value 6, 2, 1275
-- EXEC usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID 10, 8

DECLARE @SQL			VARCHAR(6000) = ''

SET @SQL = ' SELECT TOP 1 '

IF @QueryFieldID = 2		--MATERIAL
	SET @SQL += ' AM.AIMMaterialID, AM.AIMMaterialCode, AM.AIMMaterialName, 0 as MaterialTypeID, '	
IF @QueryFieldID = 3		--OWNER	
	SET @SQL += ' AM.AIMOwnerID, AM.OwnerCode, AM.OwnerName, '	
IF @QueryFieldID = 4		--OWNERLOCATION
	SET @SQL += ' AM.AIMOwnerID, AM.AIMOwnerLocationID, AM.LocationCode, AM.LocationName, '	
IF @QueryFieldID = 5		--SUPPLIER
	SET @SQL += ' AM.AIMSourceID, AM.AIMSupplierCode, AM.SourceName, '	
IF @QueryFieldID = 6		--SUPPLIERLOCATION
	SET @SQL += ' AM.AIMSourceID, AM.AIMSourceLocationID, AM.SourceLocationName, '	
IF @QueryFieldID = 7		--CFC INGREDIENT
	SET @SQL += ' CI.CFCIngredientID, CI.CFCIngredientCode, CI.CFCIngredientName, '''' as AimDatabaseName, 0 as AimDatabaseID, '	
IF @QueryFieldID = 8		--NUTRIENT
	SET @SQL += ' AM.AIMNutrientID, AM.AIMNutrientCode, AM.AIMNutrientName, 0 as UnitTypeID, 0 as PriorityAimUsedValue, AM.UpdateStamp, 0 as MathTypeID, '	

IF @QueryFieldID = 10		--SAMPLETYPE
	SET @SQL += ' ST.SampleTypeID, ST.SampleTypeName, '	

--6/1/22
IF @QueryFieldID = 11		--DATA SOURCE
	SET @SQL += ' DST.DataSourceTypeID, DST.Name, D.OrderBy, '	

IF @QueryFieldID = 14		--CFC PRODUCT TYPE
	SET @SQL += ' CPT.CFCProductTypeID, CPT.ProductTypeName, D.OrderBy, '	

--2/5/18
SET @SQL += ' D.QueryOperationID, '

SET @SQL += ' D.QueryTemplateDetailID, D.CFCUserID, 0 as IsActive '	
SET @SQL += ' FROM dbo.QueryTemplateDetail D '

IF @QueryFieldID = 2		--MATERIAL
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMMaterial AM ON D.Value = AM.AIMMaterialID '
IF @QueryFieldID = 3		--OWNER
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMOwner AM ON D.Value = AM.AIMOwnerID '
IF @QueryFieldID = 4		--OWNERLOCATION
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMOwnerLocation AM ON D.Value = AM.AIMOwnerLocationID '
IF @QueryFieldID = 5		--SUPPLIER
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMSource AM ON D.Value = AM.AIMSourceID '
IF @QueryFieldID = 6		--SUPPLIERLOCATION
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMSourceLocation AM ON D.Value = AM.AIMSourceLocationID '
IF @QueryFieldID = 7		--CFC INGREDIENT
	SET @SQL += ' LEFT OUTER JOIN dbo.CFCINGREDIENT CI ON D.Value = CI.CFCIngredientID '
IF @QueryFieldID = 8		--NUTRIENT
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMNutrient AM ON D.Value = AM.AIMNutrientID '


IF @QueryFieldID = 10		--SAMPLETYPE
	SET @SQL += ' LEFT OUTER JOIN dbo.SampleType ST ON D.Value = ST.SampleTypeID '

--5/31/22
IF @QueryFieldID = 11		--DATASOURCETYPE
	SET @SQL += ' LEFT OUTER JOIN dbo.DataSourceType DST ON D.Value = DST.DataSourceTypeID '

--12/21/22
IF @QueryFieldID = 14		--CFCPRODUCTTYPE
	SET @SQL += ' LEFT OUTER JOIN dbo.CFCProductType CPT ON D.Value = CPT.CFCProductTypeID '


SET @SQL += ' WHERE D.QueryTemplateID = ' + Cast(@QueryTemplateID as varchar) 
SET @SQL += ' and D.QueryFieldID = ' + Cast(@QueryFieldID as varchar) 
SET @SQL += ' and D.Value = ' + Cast(@Value as varchar)


EXEC (@SQL)
--print @SQL

END