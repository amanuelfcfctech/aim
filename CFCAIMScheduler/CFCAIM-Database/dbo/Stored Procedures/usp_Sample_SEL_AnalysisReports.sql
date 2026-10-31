CREATE PROCEDURE [dbo].[usp_Sample_SEL_AnalysisReports]
		  @SampleOwnerID			INT			= NULL
		, @AIMOwnerLocationID		INT			= NULL
		, @AIMSourceID				INT			= NULL
		, @AIMSourceLocationID		INT			= NULL
		, @ToDate					DATE		= NULL
		, @FromDate					DATE		= NULL
		, @AIMMaterialID			INT			= NULL
		, @Ticket					NVARCHAR(20) = NULL
		, @QueryTemplateID			INT			= NULL


--exec usp_Sample_SEL_AnalysisReports null, null, null, null, null, null, null, null, 21
--exec usp_Sample_SEL_AnalysisReports 0, 0, 0, 0, '2015-04-03', '2015-01-01', 0, '', null

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL			VARCHAR(6000) = ''
DECLARE @Total			VARCHAR(1000) = ''
DECLARE @Where			VARCHAR(1000) = ''
DECLARE @WhereValue		VARCHAR(1000) = ''

--Variables added for ##Field temp table
DECLARE @ID					INT = 0
DECLARE @ParenOpen			VARCHAR(10)
DECLARE @ParenClose			VARCHAR(10)
DECLARE @CFCWhere			VARCHAR(1000) = ''
Declare @PrevID				INT = 0
DECLARE @PrevCFCID			INT = 0
DECLARE @WhereNutrient		VARCHAR(1000) = ''
DECLARE @MatNutWhere		VARCHAR(1000) = ''



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
		if @FromDate IS NOT NULL
			SET @Where += ' and SR.AnalysisDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' '
		if @ToDate IS NOT NULL
			SET @Where += ' and SR.AnalysisDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' '

	END
ELSE
	BEGIN	

		SET @Where = ' WHERE S.IsActive = 1 '

		------------------------------------------------------------------------------
		DECLARE WhereCur scroll cursor for
		SELECT QTD.QueryFieldID, '' as ParenOpen, '' as ParenClose, QF.FieldName + ' ' +  
						CASE WHEN QTD.Value > 0 THEN REPLACE(QO.Command, '@VALUE', QTD.Value) 
						ELSE CASE WHEN QTD.DateValue IS NULL THEN QO.Command
						ELSE CASE WHEN QTD.SecondDate IS NULL THEN REPLACE(QO.Command, '@VALUE', '''' + CAST(QTD.DateValue AS VARCHAR) + '''')
						ELSE REPLACE(REPLACE(QO.Command, '@DateValue', '''' + CAST(QTD.DateValue AS VARCHAR) + '''') , '@SecondDate', '''' + CAST(QTD.SecondDate AS VARCHAR) + '''')						
						END END END AS WhereValue
		FROM	
			dbo.QueryTemplateDetail QTD 
			INNER JOIN dbo.QueryField QF ON QF.QueryFieldID = QTD.QueryFieldID 
			INNER JOIN dbo.QueryOperation QO ON QO.QueryOperationID = QTD.QueryOperationID 	
		WHERE QTD.QueryTemplateID = @QueryTemplateID AND QF.QueryFieldID <> 9 -- Omit Field = 9 here (Material/Nutrient) - handle below
		ORDER BY QTD.QueryFieldID

		OPEN WhereCur
		FETCH NEXT FROM WhereCur INTO @ID, @ParenOpen, @ParenClose, @WhereValue
			WHILE @@FETCH_STATUS = 0		
				BEGIN	
					-- ALL OTHER FIELDS (EXCEPT 9) -----------------------------------					
					IF @ID not in (0,7)
						Begin
							if @PrevID <> @ID and @PrevID <> 0
								SET @Where += ')'	
									
							if @PrevID <> @ID	
								SET @Where +=  ' AND ('
							else
								SET @Where +=  ' OR '	
				
							SET @Where += @WhereValue	
							SET @PrevID = @ID			
						End

					-- CFC INGREDIENT ------------------------------------------------
					ELSE
						Begin
							if @PrevCFCID <> @ID and @PrevCFCID <> 0
								SET @CFCWhere += ')'	
									
							if @PrevCFCID <> @ID	
								SET @CFCWhere +=  ' AND ('
							else
								SET @CFCWhere +=  ' OR '	
				
							SET @CFCWhere += @WhereValue 
							SET @PrevCFCID = @ID
						End				
					FETCH NEXT FROM WhereCur INTO @ID, @ParenOpen, @ParenClose, @WhereValue				
				END
			if LEN(@Where) > 0
				SET @Where += ')'
			if LEN(@CFCWhere) > 0
				SET @CFCWhere += ')'
		CLOSE WhereCur
		DEALLOCATE WhereCur

		----------------------------------------------------------------------------------------------
		SET @ID	= 0
		SET @PrevID	= 0					
		DECLARE MatNutCursor scroll cursor for
		SELECT QTD.Value, '' as ParenOpen, '' as ParenClose, QF.FieldName + ' ' + 
		CASE WHEN QTD.Value > 0 THEN REPLACE(QO.Command, '@VALUE', QTD.Value) ELSE QO.Command END AS WhereValue, 
							' ' + QF.FieldName2 + ' = ' + CAST(QTDN.AIMNutrientID as varchar) +  ' ' AS WhereNutrient
			FROM
			dbo.QueryTemplateDetail QTD 
			INNER JOIN dbo.QueryField QF ON QF.QueryFieldID = QTD.QueryFieldID 
			INNER JOIN dbo.QueryOperation QO ON QO.QueryOperationID = QTD.QueryOperationID 
			LEFT OUTER JOIN dbo.QueryTemplateDetailNutrient QTDN on QTD.QueryTemplateDetailID = QTDN.QueryTemplateDetailID	
		WHERE QTD.QueryTemplateID = @QueryTemplateID AND QF.QueryFieldID = 9		
		ORDER BY QTD.Value
		OPEN MatNutCursor
		FETCH NEXT FROM MatNutCursor INTO @ID, @ParenOpen, @ParenClose, @WhereValue, @WhereNutrient
			WHILE @@FETCH_STATUS = 0		
				BEGIN	
							
					if @PrevID <> @ID and @PrevID <> 0
						SET @MatNutWhere += ')'	
						
					if @PrevID <> @ID	
						Begin
							Begin
								if @PrevID = 0
									set @MatNutWhere += ' AND ('
								else
									set @MatNutWhere += ') OR '
							End
							SET @MatNutWhere +=  '(' + @WhereValue + ' AND (' + @WhereNutrient 
						End
					else
						SET @MatNutWhere +=  ' OR ' + @WhereNutrient 																

					SET @PrevID = @ID										
					FETCH NEXT FROM MatNutCursor INTO @ID, @ParenOpen, @ParenClose, @WhereValue, @WhereNutrient
				
				END
							
				if LEN(@MatNutWhere) > 0									
					SET @MatNutWhere += ')) )'				

		CLOSE MatNutCursor
		DEALLOCATE MatNutCursor
					
	END	

	IF LEN(@MatNutWhere) > 0
		SET @Where += @MatNutWhere 


	/*


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
	*/
	
SET @SQL = 'SELECT '

If @ToDate is NOT NULL
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, '
If @FromDate IS NOT NULL
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '
SET @SQL += '	  		
	  AN.AIMNutrientCode
	, AN.AIMNutrientName
	, STDEV(SA.AIMUsedValue) AS StandardDeviation
	, SUM(SA.AIMUsedValue) as ResultValue	
	, AVG(SA.AIMUsedValue) AS Mean
	, AO.OwnerName 
	, AOL.LocationName
	, SS.SourceName
	, SL.SourceLocationName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, CI.CFCIngredientID
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, CP.CFCProductID
	, CP.CFCProductCode
	, CP.CFCProductName
	, UT.UnitTypeDescription as Unit 
	, AM.AIMMaterialCode AS MaterialCode
	, AM.AIMMaterialName AS MaterialName
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
	, AO.OwnerName 
	, AOL.LocationName
	, SS.SourceName
	, SL.SourceLocationName	
	, CI.CFCIngredientID
	, CI.CFCIngredientCode
	, CI.CFCIngredientName
	, CP.CFCProductID
	, CP.CFCProductCode
	, CP.CFCProductName
	, UT.UnitTypeDescription
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	
	'




EXEC (@SQL)
--print @sql


END
