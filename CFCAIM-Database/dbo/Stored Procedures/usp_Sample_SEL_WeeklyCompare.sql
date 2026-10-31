
CREATE PROCEDURE [dbo].[usp_Sample_SEL_WeeklyCompare]
		  @SampleOwnerID				INT = NULL
		, @AIMOwnerLocationID			INT = NULL
		, @AIMSourceID					INT = NULL
		, @AIMSourceLocationID			INT = NULL
		, @ToDate						Date = NULL
		, @FromDate						Date = NULL
		, @AIMMaterialID			INT = NULL
		, @Ticket						NVARCHAR(20) = NULL
		, @QueryTemplateID				INT	= NULL


--exec usp_Sample_SEL_WeeklyCompare null, null, null, null, null, null, null, null, 1113
--exec usp_Sample_SEL_WeeklyCompare 1, 0, 0, 0, '2015-01-03', '2015-01-03', 0, '', null

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL			VARCHAR(6000) = ''
DECLARE @Where			VARCHAR(1000) = ''
DECLARE @WhereValue		VARCHAR(1000) = ''

IF @SampleOwnerID = 0
	BEGIN
		SET @SampleOwnerID = NULL
	END

IF @AIMOwnerLocationID = 0
	BEGIN
		SET @AIMOwnerLocationID = NULL
	END

IF @AIMSourceID = 0
	BEGIN
		SET @AIMSourceID = NULL
	END

IF @AIMSourceLocationID = 0
	BEGIN
		SET @AIMSourceLocationID = NULL
	END

IF @AIMMaterialID = 0
	BEGIN
		SET @AIMMaterialID = NULL
	END

IF Len(@Ticket) = 0
	BEGIN
		SET @Ticket = NULL
	END

IF @QueryTemplateID IS NULL 
	BEGIN

		SET @Where = 'WHERE AN.IsActive = 1 '

		if @SampleOwnerID IS NOT NULL
			SET @Where += ' and S.AIMOwnerID = ' + Cast(@SampleOwnerID as varchar) + ' '			
		if @AIMOwnerLocationID IS NOT NULL
			SET @Where += ' and S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar) + ' '			
		if @AIMSourceID IS NOT NULL
			SET @Where += ' and S.AIMSourceID = ' + Cast(@AIMSourceID as varchar) + ' '			
		if @AIMSourceLocationID IS NOT NULL
			SET @Where += ' and S.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID as varchar) + ' '			
		if @AIMMaterialID IS NOT NULL
			SET @Where += ' and S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' '			
		if @Ticket IS NOT NULL
			SET @Where += ' and S.TicketNumber = ' + Cast(@Ticket as varchar) + ' '			 
	END
ELSE
	BEGIN	

		SET @Where = ''

	---	This create the where statement that the query will be using
		DECLARE WhereCur CURSOR FOR
			SELECT 
				CASE WHEN QTD.IsAnd = 0 THEN	
							'OR  '
						 ELSE
							'AND '
						 END
				+ QF.FieldName + ' ' + 
				CASE WHEN QTD.Value > 0 THEN		 
							REPLACE(QO.Command, '@VALUE', QTD.Value) 
						ELSE
							REPLACE(QO.Command, '@VALUE', '''' + CAST(QTD.DateValue AS VARCHAR) + '''') 
						END	 AS WhereValue
			FROM
				dbo.QueryTemplateDetail QTD 
				INNER JOIN 
					dbo.QueryField QF 
						ON QF.QueryFieldID = QTD.QueryFieldID 
				INNER JOIN 
					dbo.QueryOperation QO 
						ON QO.QueryOperationID = QTD.QueryOperationID 
			WHERE
				QTD.QueryTemplateID = @QueryTemplateID
		OPEN WhereCur
		FETCH NEXT FROM WhereCur INTO @WhereValue
		WHILE @@FETCH_STATUS = 0	
			BEGIN
				IF @Where = ''
					BEGIN
						SET @Where += 'WHERE ' + SUBSTRING(@WhereValue, 5, LEN(@WhereValue))
					END
				ELSE
					BEGIN
						SET @Where += ' ' + @WhereValue
					END
				FETCH NEXT FROM WhereCur INTO @WhereValue		
			END
		CLOSE WhereCur
		DEALLOCATE WhereCur
	END

SET @SQL = 'SELECT '
If @ToDate is NOT NULL
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, '
If @FromDate IS NOT NULL
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '
SET @SQL += ' 
	 DATEPART(WEEK, S.SampleDate) AS WeekID
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, STDEV(SR.ResultValue) AS StandardDeviation
	, AVG(SR.ResultValue) AS Mean
	, AO.OwnerName 
	, AOL.LocationName
	, SS.SourceName
	, SL.SourceLocationName	
	, CI.CFCIngredientID
	, AM.AIMMaterialCode AS MaterialCode
	, AM.AIMMaterialName AS MaterialName
	, CP.CFCProductID
	, UT.UnitTypeDescription as Unit 
 FROM 
	dbo.Sample S WITH(NOLOCK)
	INNER JOIN 
		dbo.SampleAIM SA WITH(NOLOCK)
			ON	SA.SampleID = S.SampleID 
			AND	SA.IsActive = 1
	INNER JOIN 
		dbo.SampleResult SR WITH(NOLOCK)
			ON	SR.SampleID = S.SampleID 
			AND	SR.IsActive = 1
	LEFT OUTER JOIN 
		dbo.AIMOwner AO WITH(NOLOCK)
			ON AO.AIMOwnerID = S.AIMOwnerID
	LEFT OUTER JOIN 
		dbo.AIMMaterial AM WITH(NOLOCK)
			ON AM.AIMMaterialID = S.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.AIMOwnerLocation AOL WITH(NOLOCK)
			ON AOL.AIMOwnerLocationID = S.AIMOwnerLocationID 
	LEFT OUTER JOIN 
		dbo.AIMSource SS WITH(NOLOCK)
			ON SS.AIMSourceID = S.AIMSourceID
	LEFT OUTER JOIN 
		dbo.AIMSourceLocation SL WITH(NOLOCK)
			ON SL.AIMSourceLocationID = S.AIMSourceLocationID

	LEFT OUTER JOIN 
		dbo.AIMNutrient AN WITH(NOLOCK)
			ON AN.AIMNutrientID = SA.AIMNutrientID
	LEFT OUTER JOIN
		dbo.UnitType UT with (nolock)
			ON AN.UnitTypeID = UT.UnitTypeID

	LEFT OUTER JOIN 
		dbo.CFCIngredientRefAIMMaterial CINIR WITH(NOLOCK)
			ON CINIR.AIMMaterialID = S.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.CFCIngredient CI WITH(NOLOCK)
			ON CI.CFCIngredientID = CINIR.CFCIngredientID
	LEFT OUTER JOIN 
		dbo.CFCProductRefAIMMaterial CPNIP WITH(NOLOCK)
			ON CPNIP.AIMMaterialID = S.AIMMaterialID
	LEFT OUTER JOIN 
		dbo.CFCProduct CP WITH(NOLOCK)
			ON CP.CFCProductID = CPNIP.CFCProductID '

 + @Where + ' ' +  

'GROUP BY
	  AN.AIMNutrientCode
	, AN.AIMNutrientName
	, DATEPART(WEEK, S.SampleDate)
	, AO.OwnerName 
	, AOL.LocationName
	, SS.SourceName
	, SL.SourceLocationName	
	, CI.CFCIngredientID
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, AM.AIMMaterialCode 
	, AM.AIMMaterialName 
	, CP.CFCProductID
	, CP.CFCProductCode
	, CP.CFCProductName
	, UT.UnitTypeDescription

ORDER BY 
	  AO.OwnerName 
	, AOL.LocationName
	, SS.SourceName
	, SL.SourceLocationName
	, CI.CFCIngredientCode
	, CP.CFCProductCode
	, AN.AIMNutrientCode	
	, DATEPART(WEEK, S.SampleDate) '

--print @Sql
EXEC (@SQL)
	
END
