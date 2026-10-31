CREATE PROCEDURE [dbo].[usp_Report_AnalysisPivot] 

	@SampleOwnerID				INT				= NULL , 
	@AIMOwnerLocationID			INT				= NULL , 
	@AIMSourceID				INT				= NULL , 
	@AIMSourceLocationID		INT				= NULL , 
	@ToDate						DATE			= NULL , 
	@FromDate					DATE			= NULL , 
	@AIMMaterialID				INT				= NULL ,    --> if AimMaterialID = 0, the user has selected more than 1 material (and they are in the AIMMaterialLists)
	@Ticket						NVARCHAR(20)	= NULL , 
	@QueryTemplateID			INT				= NULL , 
	@DryMatterBase				BIT				= NULL , 
	@AIMMaterialList			VARCHAR(6000)	= NULL ,    
	@AIMMaterialList2			VARCHAR(6000)	= NULL ,    
	@AIMMaterialList3			VARCHAR(6000)	= NULL 

--exec usp_Report_AnalysisPivot 0, 0, 0, 0, '2020-08-21', '2020-08-20', 390, '', null, false, '', '', '', 2

AS BEGIN 
SET NOCOUNT ON;  

--10/17/23
--DECLARE @SQL			VARCHAR(6000) = '' 
--DECLARE @Where			VARCHAR(6000) = '' 
--DECLARE @WhereValue		VARCHAR(6000) = '' 
DECLARE @SQL			VARCHAR(MAX) = '' 
DECLARE @Where			VARCHAR(MAX) = '' 
DECLARE @WhereValue		VARCHAR(MAX) = '' 

DECLARE @MoistureID		INT = 0    

-- Used for Dry-Matter Base calculation
SET @MoistureID = (Select TOP 1 AIMNutrientID from dbo.SystemSettings with (nolock))  

IF @SampleOwnerID = 0 
BEGIN 
	SET @SampleOwnerID = NULL 
END  

IF @AIMOwnerLocationID = 0 
	SET @AIMOwnerLocationID = NULL  
IF @AIMSourceID = 0 
	SET @AIMSourceID = NULL 
IF @AIMSourceLocationID = 0 
	SET @AIMSourceLocationID = NULL 
IF @AIMMaterialID = 0 
	SET @AIMMaterialID = NULL 
IF Len(@Ticket) = 0 
	SET @Ticket = NULL 

IF @AIMMaterialList IS NULL
	SET @AIMMaterialList = ''
IF @AIMMaterialList2 IS NULL
	SET @AIMMaterialList2 = ''
IF @AIMMaterialList3 IS NULL
	SET @AIMMaterialList3 = ''

SET @AIMMaterialList = @AIMMaterialList + @AIMMaterialList2 + @AIMMaterialList3
SELECT AIMMaterialID, AIMMaterialCode, AIMMaterialName
	  INTO ##TempMaterial
      FROM AIMMaterial	  
      WHERE AIMMaterialID IN(
            SELECT CAST(Item AS INTEGER)
            FROM dbo.SplitString(@AIMMaterialList, ','))

IF @QueryTemplateID IS NULL 
	BEGIN 	
	 
		SET @Where = 'WHERE AN.IsActive = 1  AND S.IsActive = 1  and AM.AIMMaterialName IS NOT NULL '  

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
			SET @Where += ' and S.TicketNumber = ''' + Cast(@Ticket as varchar) + ''' ' 
		if @FromDate IS NOT NULL 
			SET @Where += ' and S.SampleDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' ' 		
		if @ToDate IS NOT NULL 
			SET @Where += ' and S.SampleDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' ' 
			       
 --print @Where
	END 

ELSE
 
	BEGIN  
		SET @Where = ''

		---	This create the where statement that the query will be using  
		DECLARE WhereCur CURSOR FOR 
		SELECT CASE 
			WHEN QTD.IsAnd = 0 THEN 'OR  ' ELSE 'AND ' END + QF.FieldName + ' ' + CASE 
			WHEN QTD.Value > 0 THEN REPLACE(QO.Command, '@VALUE', QTD.Value) 
			ELSE REPLACE(QO.Command, '@VALUE', '''' + CAST(QTD.DateValue AS VARCHAR) + '''') END	 AS WhereValue 
		FROM dbo.QueryTemplateDetail QTD 
		INNER JOIN dbo.QueryField QF ON QF.QueryFieldID = QTD.QueryFieldID 
		INNER JOIN dbo.QueryOperation QO ON QO.QueryOperationID = QTD.QueryOperationID 
		WHERE QTD.QueryTemplateID = @QueryTemplateID 

		OPEN WhereCur 
		FETCH NEXT FROM WhereCur INTO @WhereValue 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			IF @Where = '' 				
				SET @Where += 'WHERE ' + SUBSTRING(@WhereValue, 5, LEN(@WhereValue)) 			
			ELSE 			
				SET @Where += ' ' + @WhereValue 			
			FETCH NEXT FROM WhereCur INTO @WhereValue 
		END 
		CLOSE WhereCur 
		DEALLOCATE WhereCur 
	END  


IF @DryMatterBase = 1 
	Begin  
		SET @SQL = ' SELECT S.SampleID , SUM(SA.AIMUsedValue) as ResultValue , 
					100 - SUM(SA.AIMUsedValue) as DryMatter , 
					100/(100 - SUM(SA.AIMUsedValue)) as AdjDryMatter 
					INTO ##DryMatter FROM dbo.Sample S WITH(NOLOCK) 
					INNER JOIN dbo.SampleAIM SA WITH(NOLOCK) ON	SA.SampleID = S.SampleID AND SA.IsActive = 1 
					INNER JOIN dbo.SampleResult SR WITH(NOLOCK) ON	SR.SampleResultID = SA.SampleResultID AND SR.IsActive = 1 '

					IF Len(@AIMMaterialList) > 0 
						SET @SQL = @SQL + 'INNER JOIN ##TempMaterial AM on S.AIMMaterialID = AM.AIMMaterialID '
					else
						SET @SQL = @SQL + 'INNER JOIN dbo.AIMMaterial AM WITH(NOLOCK) ON AM.AIMMaterialID = S.AIMMaterialID '
					SET @SQL = @SQL + '

					LEFT OUTER JOIN dbo.AIMNutrient AN WITH(NOLOCK) ON AN.AIMNutrientID = SA.AIMNutrientID  ' 
							 + @Where + '  and AN.AIMNutrientID = ' + Cast(@MoistureID as varchar) + '  
					GROUP BY S.SampleID '  
		EXEC (@SQL) 
		--print (@SQL)
	End    

-------------------------------------------------------------------------------------
SET @SQL = 'SELECT '  

If @ToDate is NOT NULL 
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, ' 
If @FromDate IS NOT NULL 
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '   

-- * * * Added MaterialNameTotal field because on the Pivot Report, the Material Name is the FieldValue, 
--		 which shows up in both the column headers and in the grand totals.  Needed more space for the nutrient columns,
--		 so shrunk the MaterialName column header so only the first 2 or 3 digits appear.  (Didn't want to see just part
--		 of the material name, so put spaces at the beginning - so spaces show up instead - but the whole material name
--		 still shows up in the grand total.)


SET @SQL += 
	' case when len(S.SampleCode) > 0 then S.SampleCode else convert(nvarchar,S.SampleID) end as SampleCode , 
	S.SampleID , S.SampleDate , AN.AIMNutrientCode , 
	case when AN.AIMNutrientShortName IS NULL OR LEN(AN.AIMNutrientShortName) = 0 then 
	LEFT(AN.AIMNutrientName,15) else AN.AIMNutrientShortName End as AIMNutrientName , 
	UT.UnitTypeDescription , 
	convert(varchar, aimnutrientname) + '' - '' + convert(varchar, unittypedescription) as AIMNutrientHeading , 
	convert(varchar, CAST(WL.Min AS DECIMAL(3,1)) ) + ''-'' + 
	convert(varchar, CAST(WL.Max AS DECIMAL(3,1)) ) as MinMaxRange , 
	AM.AIMMaterialCode AS MaterialCode , 
	AM.AIMMaterialName as MaterialName , ' + '''     ''+' + 'AM.AIMMaterialName AS MaterialNameTotal , 
	SS.SourceName , SL.SourceLocationName '  

IF @DryMatterBase = 1 
	SET @SQL += ', TT.AdjDryMatter , case when AN.AIMNutrientID = ' + Cast(@MoistureID as varchar) + ' then 0 else SUM(SA.AIMUsedValue) * TT.AdjDryMatter end as ResultValue ' 
Else 
	SET @SQL += ', SUM(SA.AIMUsedValue) as ResultValue	'   

SET @SQL += '  FROM dbo.Sample S WITH(NOLOCK) 				
				INNER JOIN dbo.SampleResult SR WITH(NOLOCK) ON	S.SampleID = SR.SampleID AND SR.IsActive = 1 
				INNER JOIN dbo.SampleAIM SA WITH(NOLOCK) ON	SA.SampleID = S.SampleID AND SR.SampleResultID = SA.SampleResultID and	SA.IsActive = 1 '				
				
				IF Len(@AIMMaterialList) > 0 
						SET @SQL = @SQL + 'INNER JOIN ##TempMaterial AM on S.AIMMaterialID = AM.AIMMaterialID '
					else
						SET @SQL = @SQL + 'INNER JOIN dbo.AIMMaterial AM WITH(NOLOCK) ON AM.AIMMaterialID = S.AIMMaterialID '	
				SET @SQL = @SQL + '

				LEFT OUTER JOIN dbo.AIMOwner AO WITH(NOLOCK) ON AO.AIMOwnerID = S.AIMOwnerID 
				LEFT OUTER JOIN dbo.AIMOwnerLocation AOL WITH(NOLOCK) ON AOL.AIMOwnerLocationID = S.AIMOwnerLocationID 
				LEFT OUTER JOIN dbo.AIMSource SS WITH(NOLOCK) ON SS.AIMSourceID = S.AIMSourceID 
				LEFT OUTER JOIN dbo.AIMSourceLocation SL WITH(NOLOCK) ON SL.AIMSourceLocationID = S.AIMSourceLocationID 
				LEFT OUTER JOIN dbo.AIMNutrient AN WITH(NOLOCK) ON AN.AIMNutrientID = SA.AIMNutrientID 
				LEFT OUTER JOIN dbo.UnitType UT with (nolock) ON AN.UnitTypeID = UT.UnitTypeID 
				LEFT OUTER JOIN dbo.WarningLimits WL with (nolock) ON AN.AIMNutrientID = WL.AIMNutrientID AND AM.AIMMaterialID = WL.AIMMaterialID ' 

IF @DryMatterBase = 1 
	SET @SQL += ' INNER JOIN ##DryMatter TT ON S.SampleID = TT.SampleID '  

SET @SQL += '   '  + @Where + ' ' 


SET @SQL +=  
	'GROUP BY case when len(S.SampleCode) > 0 then 
		S.SampleCode else convert(nvarchar,S.SampleID) end , S.SampleID , S.SampleDate , 
		AN.AIMNutrientCode , AN.AIMNutrientName , AN.AIMNutrientShortName , AM.AIMMaterialCode , 
		AM.AIMMaterialName , UT.UnitTypeDescription , WL.Min , WL.Max , SS.SourceName , SL.SourceLocationName  ' 



IF @DryMatterBase = 1 
	SET @SQL += ', TT.AdjDryMatter, AN.AIMNutrientID '   
	

EXEC (@SQL) 
--Print @SQL

IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##TempMaterial%')
	DROP TABLE ##TempMaterial
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##DryMatter%')
	DROP TABLE ##DryMatter

END
