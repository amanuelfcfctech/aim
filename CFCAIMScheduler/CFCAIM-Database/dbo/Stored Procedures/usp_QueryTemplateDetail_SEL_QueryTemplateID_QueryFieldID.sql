CREATE PROCEDURE [dbo].[usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID]
				@QueryTemplateID		INT
				, @QueryFieldID			INT
				
AS
BEGIN
SET NOCOUNT ON;

-- exec usp_QueryTemplateDetail_SEL_QueryTemplateID_QueryFieldID 15, 13


DECLARE @SQL			VARCHAR(6000) = ''

SET @SQL = ' SELECT '

IF @QueryFieldID = 1		--SAMPLEDATE
	SET @SQL += ' D.QueryFieldID, D.QueryOperationID, D.DateValue, D.IsAnd, D.Value, D.SecondDate, '
IF @QueryFieldID = 2		--MATERIAL
	SET @SQL += ' AM.AIMMaterialID, AM.AIMMaterialCode, AM.AIMMaterialName, 0 as MaterialTypeID, '	
IF @QueryFieldID = 3		--OWNER	
	SET @SQL += ' AM.AIMOwnerID, AM.OwnerCode, AM.OwnerName, '	
IF @QueryFieldID = 4		--OWNERLOCATION
	SET @SQL += ' AM.AIMOwnerID, AM.AIMOwnerLocationID, AM.LocationCode, AM.LocationName, AMO.OwnerName, '	
IF @QueryFieldID = 5		--SUPPLIER
	SET @SQL += ' AM.AIMSourceID, AM.AIMSupplierCode, AM.SourceName, '	
IF @QueryFieldID = 6		--SUPPLIERLOCATION
	SET @SQL += ' AM.AIMSourceID, AM.AIMSourceLocationID, AM.SourceLocationName, AMO.SourceName, '	
IF @QueryFieldID = 7		--CFC INGREDIENT
	SET @SQL += ' CI.CFCIngredientID, CI.CFCIngredientCode, CI.CFCIngredientName, '' '' as AimDatabaseName, 0 as AimDatabaseID, '	
IF @QueryFieldID = 8		--NUTRIENT
	SET @SQL += ' AM.AIMNutrientID, AM.AIMNutrientCode, AM.AIMNutrientName, 0 as UnitTypeID, 0 as MathTypeID, 0 as PriorityAIMUsedValue, '	
IF @QueryFieldID = 9
	Begin
		SET @SQL += ' 0 as TemplateMaterialNutrientID, AM.AIMMaterialID, AM.AIMMaterialCode, AM.AIMMaterialName, '
		SET @SQL += ' AN.AIMNutrientID, AN.AIMNutrientCode, AN.AIMNutrientName, ' 
	End

--1/25/21
IF @QueryFieldID = 10		--SAMPLE TYPE
	SET @SQL += ' ST.SampleTypeID, ST.SampleTypeName, '	

--6/1/22
IF @QueryFieldID = 11		--DATA SOURCE
	SET @SQL += ' DST.DataSourceTypeID, DST.Name, D.OrderBy, '	
	
--6/13/22
IF @QueryFieldID = 12		--RESULTS ANALYSIS DATE
	SET @SQL += ' D.QueryFieldID, D.QueryOperationID, D.DateValue, D.IsAnd, D.Value, D.SecondDate, '	

--6/15/22
IF @QueryFieldID = 13		--DATA SOURCE COMPARISON	
	Begin
		SET @SQL += ' DST.DataSourceTypeID as DataSourceTypeID1, DST.Name as DataSourceTypeName1, D.OrderBy, '
		SET @SQL += ' AIMN.AIMNutrientID as DS1_NutID, AIMN.AIMNutrientCode as DS1_NutCode, AIMN.AIMNutrientName as DS1_NutName, '
	End

--12/21/22
IF @QueryFieldID = 14		--CFC PRODUCT TYPE
	SET @SQL += ' CPT.CFCProductTypeID, CPT.ProductTypeName, '	


--2/5/18
If @QueryFieldID <> 1 and @QueryFieldID <> 9
	SET @SQL += ' D.QueryOperationID, '
	
SET @SQL += ' D.QueryTemplateID, D.QueryTemplateDetailID, D.CFCUserID, 0 as IsActive '	
SET @SQL += ' FROM dbo.QueryTemplateDetail D '



IF @QueryFieldID = 2		--MATERIAL
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMMaterial AM ON D.Value = AM.AIMMaterialID '
IF @QueryFieldID = 3		--OWNER
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMOwner AM ON D.Value = AM.AIMOwnerID '
IF @QueryFieldID = 4		--OWNERLOCATION
	Begin
		SET @SQL += ' LEFT OUTER JOIN dbo.AIMOwnerLocation AM ON D.Value = AM.AIMOwnerLocationID '
		SET @SQL += ' LEFT OUTER JOIN dbo.AIMOwner AMO ON AM.AIMOwnerID = AMO.AIMOwnerID '
	End
IF @QueryFieldID = 5		--SUPPLIER
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMSource AM ON D.Value = AM.AIMSourceID '
IF @QueryFieldID = 6		--SUPPLIERLOCATION
	Begin
		SET @SQL += ' LEFT OUTER JOIN dbo.AIMSourceLocation AM ON D.Value = AM.AIMSourceLocationID '
		SET @SQL += ' LEFT OUTER JOIN dbo.AIMSource AMO ON AM.AIMSourceID = AMO.AIMSourceID '
	End
IF @QueryFieldID = 7		--CFC INGREDIENT
	SET @SQL += ' LEFT OUTER JOIN dbo.CFCINGREDIENT CI ON D.Value = CI.CFCIngredientID '
IF @QueryFieldID = 8		--NUTRIENT
	SET @SQL += ' LEFT OUTER JOIN dbo.AIMNutrient AM ON D.Value = AM.AIMNutrientID '
IF @QueryFieldID = 9		--MATERIAL/NUTRIENT
	Begin
		SET @SQL += ' LEFT OUTER JOIN dbo.QueryTemplateDetailNutrient DN on D.QueryTemplateDetailID = DN.QueryTemplateDetailID '
		SET @SQL += ' LEFT OUTER JOIN dbo.AIMMaterial AM ON D.Value = AM.AIMMaterialID '
		SET @SQL += ' LEFT OUTER JOIN dbo.AIMNutrient AN ON DN.AIMNutrientID = AN.AIMNutrientID '
	End

--1/25/21
IF @QueryFieldID = 10		--SAMPLETYPE
	SET @SQL += ' LEFT OUTER JOIN dbo.SampleType ST ON D.Value = ST.SampleTypeID '

--5/31/22
IF @QueryFieldID = 11 		--DATA SOURCE TYPE
	SET @SQL += ' LEFT OUTER JOIN dbo.DataSourceType DST ON D.Value = DST.DataSourceTypeID '

--5/31/22
IF @QueryFieldID = 13 		--DATA SOURCE COMPARISON
	Begin
		SET @SQL += ' LEFT OUTER JOIN dbo.DataSourceType DST ON D.Value = DST.DataSourceTypeID '
		SET @SQL += ' LEFT OUTER JOIN dbo.QueryTemplateDetailNutrient N on D.QueryTemplateDetailID = N.QueryTemplateDetailID and D.Value = N.DataSourceTypeID '
		SET @SQL += ' LEFT OUTER JOIN dbo.AIMNutrient AIMN on N.AIMNutrientID = AIMN.AIMNutrientID '
	End

IF @QueryFieldID = 14		--CFCPRODUCTTYPE
	SET @SQL += ' LEFT OUTER JOIN dbo.CFCProductType CPT ON D.Value = CPT.CFCProductTypeID '

SET @SQL += ' WHERE D.QueryTemplateID = ' + Cast(@QueryTemplateID as varchar) 
SET @SQL += ' and D.QueryFieldID = ' + Cast(@QueryFieldID as varchar) 


--print @SQL
EXEC (@SQL)


END