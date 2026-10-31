CREATE PROCEDURE [dbo].[usp_Sample_SEL_Analysis_TEST]
			  @AnalysisQuery		INT
			, @StartDate			DATE	= NULL
			, @EndDate				DATE	= NULL
			, @QueryTemplateID		INT		= NULL
			, @IsDefaultLocation	BIT		= NULL			
			, @IsSubSet				BIT		= NULL
			, @AimDataBaseID		INT		= NULL
			, @AIMMaterialID		INT     = NULL
			, @AIMNutrientID		INT		= NULL
			, @SampleDate			DATE	= NULL
			, @SampleID				INT		= NULL
			, @AIMOwnerID			INT		= NULL
			, @AIMOwnerLocationID	INT		= NULL
			, @AIMSourceID			INT		= NULL
			, @AIMSourceLocationID	INT		= NULL
			, @CFCPlantID			INT		= NULL		-- currently, not used below			
			, @CFCUserID			INT		= NULL
			, @OutlierType			INT		= NULL
			, @CFCIngredientID		INT		= NULL		-- added 9/28/16 

	
--exec usp_Sample_SEL_Analysis_TEST 3, '2/14/2017 12:00:00 AM', '3/15/2017 12:00:00 AM', 3, False, False, 1, 0, 0, '', 0, 0, 0, 0, 0, 0, 1, 1, 0

AS
BEGIN
DECLARE @SQL			VARCHAR(6000) = ''
DECLARE @Total			VARCHAR(1000) = ''
DECLARE @Where			VARCHAR(1000) = ''
DECLARE @WhereValue		VARCHAR(1000) = ''
DECLARE @Group			VARCHAR(1000) = ''
DECLARE @Location		VARCHAR(100) = ''
DECLARE @S				VARCHAR(4) = ''
DECLARE @D				VARCHAR(4) = ''
DECLARE @ST				VARCHAR(5) = ''
DECLARE @ON				VARCHAR(60) = ''
DECLARE @SRON			VARCHAR(60) = ''
DECLARE @SON			VARCHAR(60) = ''
DECLARE @QON			VARCHAR(60) = ''
DECLARE @TWhere			VARCHAR(500) = ''
DECLARE @Outlier		VARCHAR(70) = ''
DECLARE @QueryFieldID	INT = 0
DECLARE @CFCWhere		VARCHAR(1000) = ''

--Variables added for ##Field temp table
DECLARE @Str			VARCHAR(1000) = ''
DECLARE @StrCFC	  	    VARCHAR(1000) = ''
DECLARE @ID				INT = 0
DECLARE @IsAnd			INT = 0
DECLARE @ParenOpen		VARCHAR(10)
DECLARE @ParenClose		VARCHAR(10)
declare @PrevIsAnd		INT
declare @TotalAnd		INT
declare @TotalOr		INT
DECLARE @intOpen		INT = 0	
DECLARE @intClose		INT = 0		
Declare @PrevID			INT = 0

SET NOCOUNT ON;

-- Set T-WHERE string -------------------------------------------------------- 
IF @IsSubSet = 1
	BEGIN		
		Set @TWhere = ' WHERE '
		Set @TWhere += ' T.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar)
		Set @TWhere += ' and T.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar)
		IF @SampleDate IS NOT NULL 
			Set @TWhere += ' and T.SampleDate = ''' + Convert(varchar,Cast(@SampleDate as datetime)) + ''' '
		--	Set @TWhere += ' and T.SampleDate = ''' + Cast(@SampleDate as datetime) + ''' '
		If @SampleID IS NOT NULL
			Set @Twhere += ' and T.SampleID = ' + Cast(@SampleID as varchar)		
		If @AIMOwnerID IS NOT NULL
			Set @Twhere += ' and T.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar)
		If @AIMOwnerLocationID IS NOT NULL
			Set @Twhere += ' and T.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar)
		If @AIMSourceID IS NOT NULL
			Set @Twhere += ' and T.AIMSourceID = ' + Cast(@AIMSourceID as varchar)
		If @AIMSourceLocationID IS NOT NULL
			Set @Twhere += ' and T.AIMSourceLocationID = ' + Cast(@AIMSourceLocationID as varchar)						
	END

-----------------------------------------------------------------------------
-- This is used when doing overlay location 
IF @IsDefaultLocation = 1
	BEGIN
		SET @Location = 'AIMOwnerLocationID '
		SET @S = ', S.'
		SET @D = ', D.'
		SET @ST = ', ST.'
		SET @ON = ' AND D.AIMOwnerLocationID = S.AIMOwnerLocationID '
		SET @SRON = ' AND SR.AIMOwnerLocationID = T.AIMOwnerLocationID '
		SET @SON = ' AND ST.AIMOwnerLocationID = S.AIMOwnerLocationID '
		SET @QON = ' AND Q.AIMOwnerLocationID = ST.AIMOwnerLocationID '
	END


-----------------------------------------------------------------------------
--- If there is no query then only the date is used on the where statement
IF @QueryTemplateID = 0 
	BEGIN				
		SET @Where = 'AND S.SampleDate BETWEEN ''' + CAST(@StartDate AS VARCHAR) + ''' AND ''' + CAST(@EndDate AS VARCHAR) + ''' '
	END

-----------------------------------------------------------------------------
---	This creates the where statement that the query will be using
ELSE
	BEGIN
	--	DECLARE WhereCur CURSOR FOR
	--		SELECT 
	--			QF.QueryFieldID, 
	--			CASE WHEN QTD.IsAnd = 0 THEN	
	--						'OR '
	--					 ELSE
	--						'AND '
	--					 END
	--			+ QF.FieldName + ' ' + 
	--			CASE WHEN QTD.Value > 0 THEN		 
	--						REPLACE(QO.Command, '@VALUE', QTD.Value) 
	--					ELSE
	--						CASE WHEN QTD.DateValue IS NULL THEN
	--								QO.Command
	--							ELSE
	--								REPLACE(QO.Command, '@VALUE', '''' + CAST(QTD.DateValue AS VARCHAR) + '''') 
	--						END
	--					END	 AS WhereValue
	--		FROM
	--			dbo.QueryTemplateDetail QTD 
	--			INNER JOIN 
	--				dbo.QueryField QF 
	--					ON QF.QueryFieldID = QTD.QueryFieldID 
	--			INNER JOIN 
	--				dbo.QueryOperation QO 
	--					ON QO.QueryOperationID = QTD.QueryOperationID 
	--		WHERE
	--			QTD.QueryTemplateID = @QueryTemplateID
	--	OPEN WhereCur
	--	FETCH NEXT FROM WhereCur INTO @QueryFieldID, @WhereValue
	--	WHILE @@FETCH_STATUS = 0	
	--		BEGIN			
	--			IF @QueryFieldID NOT IN (0, 7)							--> doing this for CFCIngredientCode (when the code is selected as criteria in the query)
	--				SET @Where = @Where + ' ' + @WhereValue					-- 0=nothing, 1=sample-date, 2=material, 3=owner, 4=ownerloc,
	--			ELSE														-- 5=supplier, 6=supplierloc, 7=ingredientcode
	--				SET @CFCWhere = @CFCWhere + ' ' + @WhereValue
	--			FETCH NEXT FROM WhereCur INTO @QueryFieldID, @WhereValue		
	--		END
	--	CLOSE WhereCur
	--	DEALLOCATE WhereCur
		
		------------------------------------------------------------------------------------
		--CLEAR VARIABLES USED TO DETERMINE PARENS IN TEMP ##FIELD TABLE
		SET @intOpen = 0
		SET @intClose = 0	
		SET @TotalAnd = 0
		SET @TotalOr = 0
	
		SET @totalAnd = (Select count(*) from dbo.QueryTemplateDetail QTD Where QTD.QueryTemplateID  = @QueryTemplateID and QTD.IsAnd = 1)
		SET @totalOr = (Select count(*) from dbo.QueryTemplateDetail QTD Where QTD.QueryTemplateID  = @QueryTemplateID and QTD.IsAnd = 0)
	
		------------------------------------------------------------------------------------
		--CREATE TEMP FIELD TABLE
		SELECT Identity(Int, 1,1) as ID, QTD.IsAnd, QF.QueryFieldID,  '' as ParenOpen, '' as ParenClose, + QF.FieldName + ' ' + 
		CASE WHEN QTD.Value > 0 THEN REPLACE(QO.Command, '@VALUE', QTD.Value) 
							ELSE CASE WHEN QTD.DateValue IS NULL THEN QO.Command
							ELSE REPLACE(QO.Command, '@VALUE', '''' + CAST(QTD.DateValue AS VARCHAR) + '''') END END	 
							AS WhereValue
		INTO ##Field
		FROM
			dbo.QueryTemplateDetail QTD 
			INNER JOIN dbo.QueryField QF ON QF.QueryFieldID = QTD.QueryFieldID 
			INNER JOIN dbo.QueryOperation QO ON QO.QueryOperationID = QTD.QueryOperationID 
	
		WHERE QTD.QueryTemplateID = @QueryTemplateID

		------------------------------------------------------------------------------------
		--ADD OPEN/CLOSE PARENS
		DECLARE WhereCur CURSOR FOR 
			SELECT F.ID, F.IsAnd, F.WhereValue, F.ParenOpen, F.QueryFieldID, F.ParenClose
			FROM ##Field F												
	
		OPEN WhereCur
		FETCH NEXT FROM WhereCur INTO @ID, @IsAnd, @WhereValue, @ParenOpen, @QueryFieldID, @ParenClose
		WHILE @@FETCH_STATUS = 0		
			BEGIN
				If @totalAnd <> 0 and @totalOr <> 0
					Begin
						If @PrevID <> 0										
							Begin
								--open parens
								If @IsAnd = 0 and @PrevIsAnd = 1	
									Begin					
										Update ##Field set ParenOpen = '(' Where ID = @PrevID							
										Set @intOpen = @intOpen + 1
									End
		
								--close parens							
								If @IsAnd = 1 and @intOpen > @intClose							
									Begin
										Update ##Field set ParenClose = ')' Where ID = @PrevID						
										Set @intClose = @intClose + 1
									End
							
								--last record							
								If @ID = (select max(id) from ##Field)  and @intOpen > @intClose 
									Update ##Field set ParenClose = ')' Where ID = @ID	
							End
		
							SET @PrevID = @ID
							SET @PrevIsAnd = @IsAnd
					End		
					FETCH NEXT FROM WhereCur INTO @ID, @IsAnd, @WhereValue, @ParenOpen, @QueryFieldID, @ParenClose					
			END
		CLOSE WhereCur
		
		----------------------------------------------------------------------------
		--BUILD SQL STRING
		
		SET @Str = ''
		OPEN WhereCur
		FETCH NEXT FROM WhereCur INTO @ID, @IsAnd, @WhereValue, @ParenOpen, @QueryFieldID, @ParenClose
		WHILE @@FETCH_STATUS = 0	
	
			BEGIN				
				IF @QueryFieldID NOT IN (0, 7)
					Begin
						IF @IsAnd = 1 				
							SET @Where = @Where + ' AND ' + @ParenOpen + @WhereValue + @ParenClose
						ELSE
							SET @Where = @Where + ' OR ' + @ParenOpen + @WhereValue + @ParenClose
					End
				Else
					Begin
					IF @IsAnd = 1 				
							SET @CFCWhere = @CFCWhere + ' AND ' + @ParenOpen + @WhereValue + @ParenClose
						ELSE
							SET @CFCWhere = @CFCWhere + ' OR ' + @ParenOpen + @WhereValue + @ParenClose
					End
				
				

				------------------------------------------------------------------------
				----IF @QueryFieldID NOT IN (0, 7)							--> doing this for CFCIngredientCode (when the code is selected as criteria in the query)
				----	SET @Where = @Where + ' ' + @Str					-- 0=nothing, 1=sample-date, 2=material, 3=owner, 4=ownerloc, 
				----Else													-- 5=supplier, 6=supplierloc, 7=ingredientcode, 8=nutrientcode
				----	SET @CFCWhere = @CFCWhere + ' ' + @Str

				
				FETCH NEXT FROM WhereCur INTO @ID, @IsAnd, @WhereValue, @ParenOpen, @QueryFieldID, @ParenClose
			END
	
		CLOSE WhereCur				
		DEALLOCATE WhereCur		
	END



print @Where
--print @CFCWhere

--------------------------------------------------------------------------------------
--- This is used to get the numbers for each grouping
SET @Group = 
	CASE @AnalysisQuery 
		WHEN 1 THEN				--NoGroup
			''
		WHEN 2 THEN				--SampleID 
			', S.SampleID, S.SampleDate, AO.OwnerCode, AO.AIMOwnerID, S.AIMOwnerLocationID, AOL.LocationCode, SS.AIMSourceID, SS.SourceName, ASL.AIMSourceLocationID, ASL.SourceLocationName, S.SampleCode, S.SampleAIMID '
		WHEN 3 THEN				--SampleDate
			', S.SampleDate '
		WHEN 4 THEN				--SampleDateOwner
			', S.SampleDate, AO.OwnerCode '
		WHEN 5 THEN				--SampleDateOwnerSource
			', S.SampleDate, AO.OwnerCode, SS.SourceName '
		WHEN 6 THEN				--SampleDateOwnerSourceLocation
			', S.SampleDate, AO.OwnerCode, SS.SourceName, ASL.SourceLocationName '
		WHEN 7 THEN				--SampleDateOwnerLocation
			', S.SampleDate, AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode '
		WHEN 8 THEN				--SampleDateOwnerLocationSource
			', S.SampleDate, AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, SS.SourceName '
		WHEN 9 THEN				--SampleDateOwnerLocationSourceLocation
			', S.SampleDate, AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, SS.SourceName, ASL.SourceLocationName '
		WHEN 10 THEN			--SampleDateSource
			', S.SampleDate, SS.SourceName '
		WHEN 11 THEN			--SampleDateSourceLocation
			', S.SampleDate, SS.SourceName, ASL.SourceLocationName '
		WHEN 12 THEN			--Owner
			', AO.OwnerCode '
		WHEN 13 THEN			--OwnerSource
			', AO.OwnerCode, SS.SourceName '
		WHEN 14 THEN			--OwnerSourceLocation
			', AO.OwnerCode, SS.SourceName, ASL.SourceLocationName '
		WHEN 15 THEN			--OwnerLocation
			', AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode '
		WHEN 16 THEN			--OwnerLocationSource
			', AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, SS.SourceName '
		WHEN 17 THEN			--OwnerLocationSourceLocation
			', AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, SS.SourceName, ASL.SourceLocationName '
		WHEN 18 THEN			--Source
			', SS.SourceName '
		WHEN 19 THEN			--SourceLocation
			', SS.SourceName, ASL.SourceLocationName '
	END

-----------------------------------------------------------------------------
--This is used to get the final grouping 
SET @Total =  
	CASE @AnalysisQuery 
		WHEN 1 THEN				--NoGroup
			''
		WHEN 2 THEN				--SampleID 
			', T.SampleID, T.SampleDate, T.OwnerCode, T.AIMOwnerID, T.AIMOwnerLocationID, T.LocationCode, T.AIMSourceID, T.SourceName, T.AIMSourceLocationID, T.SourceLocationName, T.SampleCode, T.SampleAIMID '
		WHEN 3 THEN				--SampleDate
			', T.SampleDate '
		WHEN 4 THEN				--SampleDateOwner
			', T.SampleDate, T.OwnerCode '
		WHEN 5 THEN				--SampleDateOwnerSource
			', T.SampleDate, T.OwnerCode, T.SourceName '
		WHEN 6 THEN				--SampleDateOwnerSourceLocation
			', T.SampleDate, T.OwnerCode, T.SourceName, T.SourceLocationName '
		WHEN 7 THEN				--SampleDateOwnerLocation
			', T.SampleDate, T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode '
		WHEN 8 THEN				--SampleDateOwnerLocationSource
			', T.SampleDate, T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.SourceName '
		WHEN 9 THEN				--SampleDateOwnerLocationSourceLocation
			', T.SampleDate, T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.SourceName, T.SourceLocationName '
		WHEN 10 THEN			--SampleDateSource
			', T.SampleDate, T.SourceName '
		WHEN 11 THEN			--SampleDateSourceLocation
			', T.SampleDate, T.SourceName, T.SourceLocationName '
		WHEN 12 THEN			--Owner
			', T.OwnerCode '
		WHEN 13 THEN			--OwnerSource
			', T.OwnerCode, T.SourceName '
		WHEN 14 THEN			--OwnerSourceLocation
			', T.OwnerCode, T.SourceName, T.SourceLocationName '
		WHEN 15 THEN			--OwnerLocation
			', T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode '
		WHEN 16 THEN			--OwnerLocationSource
			', T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.SourceName '
		WHEN 17 THEN			--OwnerLocationSourceLocation
			', T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.SourceName, T.SourceLocationName '
		WHEN 18 THEN			--Source
			', T.SourceName '
		WHEN 19 THEN			--SourceLocation
			', T.SourceName, T.SourceLocationName '
	END

-----------------------------------------------------------------------------
-- This inserts into temp Sample
SET @SQL = 
'SELECT
	  S.SampleID
	, S.SampleCode
	, S.AIMMaterialID 
	, S.AIMOwnerID
	, S.AIMOwnerLocationID
	, S.AIMSourceID
	, S.AIMSourceLocationID
	, S.SampleDate
	, SR.SampleResultID
	, SA.IsActive
	, SA.AIMNutrientID
	, SA.AIMUsedValue
	, SA.SampleAIMID
INTO ##Sample
FROM
dbo.Sample S WITH(NOLOCK)
INNER JOIN 
	dbo.SampleResult SR
		ON  SR.SampleID = S.SampleID
		AND SR.IsActive = 1
INNER JOIN 
	dbo.SampleAIM SA WITH(NOLOCK)
		ON	SA.SampleID = S.SampleID
		AND	SA.SampleResultID = SR.SampleResultID
		AND SA.IsActive = 1 
WHERE
	S.IsActive = 1 ' + @Where 
EXEC(@SQL)
--print @sql

-----------------------------------------------------------------------------
--This get the numbers for the math   Any changes has to be made to the ##Proposed table
--When doing the Quartile and the sample size is less then four, Quartile1 = min and Quartile3 = max of the sample set
--This is so the outlier would not kick those value out if there was last then 4 samples
SET @SQL = 'SELECT 
	  ST.AIMMaterialID' + @ST + @Location +
	', ST.AIMNutrientID
	, Q.SampleSize
	, ISNULL(Q.Quartile1, 0) AS Quartile1
	, ISNULL(Q.Quartile3, 0) AS Quartile3
	, ST.Mean
	, ST.StandardDeviation
	, ST.DetailMin
	, ST.DetailMax
	, ST.DetailSQRT
	, ST.WarningGlobalH
	, ST.WarningMinMaxLimit
	, ISNULL(ROUND(Q.Quartile1 - 1.5 * (Q.Quartile3 - Q.Quartile1), 4), 0) AS LMin
	, ISNULL(ROUND(Q.Quartile1 - 3 * (Q.Quartile3 - Q.Quartile1), 4), 0) AS LMax
	, ISNULL(ROUND(Q.Quartile3 + 1.5 * (Q.Quartile3 - Q.Quartile1), 4), 0) AS UMin
	, ISNULL(ROUND(Q.Quartile3 + 3 * (Q.Quartile3 - Q.Quartile1), 4), 0) AS UMax
	, CASE WHEN ISNULL(Q.Quartile1, 0) = ISNULL(Q.Quartile3, 0) THEN 0 ELSE 
	  CASE WHEN ISNULL(Q.Quartile1, 0) = 0 THEN 0 ELSE 
	  CASE WHEN ISNULL(Q.Quartile3, 0) = 0 THEN 0 ELSE 1 END END END AS IsValidOutlier
INTO ##Statis1
FROM
	(
	SELECT 
		  S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID
		, ROUND(AVG(S.AIMUsedValue), 4) AS Mean
		, ROUND(STDEV(S.AIMUsedValue), 4) AS StandardDeviation
		, MIN(S.AIMUsedValue) AS DetailMin
		, MAX(S.AIMUsedValue) AS DetailMax
		, SQRT(COUNT(*)) AS DetailSQRT
		, SUM(CASE WHEN SAM.AimUsedValue NOT BETWEEN WL.Min and WL.Max THEN 1 ELSE 0 END) as WarningMinMaxLimit
		, SUM(CASE WHEN SRP.GlobalH > SRP.Limit THEN 1 ELSE 0 END) as WarningGlobalH	

	FROM      
		##Sample S
		LEFT OUTER JOIN 
			dbo.SampleResultPredict SRP WITH(NOLOCK)
				ON SRP.SampleResultID = S.SampleResultID				
		LEFT JOIN 
			dbo.SampleAIM SAM WITH(NOLOCK) 
				ON S.SampleID = SAM.SampleID
		LEFT JOIN 
			dbo.WarningLimits WL WITH(NOLOCK)
				ON S.AIMMaterialID = WL.AIMMaterialID
				AND S.AIMNutrientID = WL.AIMNutrientID
	GROUP BY 
			S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID 
	) ST
	INNER JOIN
		(
	SELECT 
		  S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID
		, S.SampleSize
		, CASE WHEN SampleSize < 4 THEN MinValue ELSE Quartile1 END Quartile1
		, CASE WHEN SampleSize < 4 THEN MaxValue ELSE Quartile3 END Quartile3
	FROM (	    
		SELECT 
				S.AIMMaterialID' + @S + @Location +
			', S.AIMNutrientID
			, COUNT(*) AS SampleSize
			, MAX(AIMUsedValue) AS MaxValue
			, MIN(AIMUsedValue) AS MinValue
			, ROUND((Max(Q1NextVal) - MAX(Q1Val)) * Max(Q1Frac) + Max(Q1Val), 4) AS Quartile1
			, ROUND((Max(Q3NextVal) - MAX(Q3Val)) * Max(Q3Frac) + Max(Q3Val), 4) AS Quartile3
		FROM     
			(
			SELECT 
				  D.AIMMaterialID' +   @D + @Location +
				', D.AIMNutrientID
				, CASE WHEN RowNum = Q1Idx THEN AIMUsedValue ELSE NULL END AS Q1Val
				, CASE WHEN RowNum = Q1Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q1NextVal
				, CASE WHEN RowNum = Q3Idx THEN AIMUsedValue ELSE NULL END AS Q3Val
				, CASE WHEN RowNum = Q3Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q3NextVal
				, Q1Frac
				, Q3Frac
				, AIMUsedValue
			FROM      
				(
				SELECT 
					  S.AIMUsedValue 
					, S.AIMMaterialID' + @S + @Location + 
					', S.AIMNutrientID 
					, ROW_NUMBER() 
					OVER (PARTITION BY S.AIMMaterialID' + ', S.AIMNutrientID
					ORDER BY S.AIMUsedValue) AS RowNum
				FROM      
					##Sample S
				) D 
				LEFT OUTER JOIN
					(/*the values at the quartile points.*/ 
					SELECT 
							S.AIMMaterialID' + @S + @Location + 
						', S.AIMNutrientID
						, FLOOR((COUNT(*) + 3) / 4.0) AS Q1Idx
						, ((COUNT(*) + 3) / 4.0) - FLOOR((COUNT(*) + 3) / 4.0) AS Q1Frac
						, FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Idx
						, ((COUNT(*) * 3 + 1) / 4.0) - FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Frac
					FROM
						##Sample S
					GROUP BY 
							S.AIMMaterialID' + @S + @Location + 
						', S.AIMNutrientID
					HAVING  COUNT(*) > 1
					) S 
						ON	D.AIMMaterialID = S.AIMMaterialID ' + @ON +  
						'AND D.AIMNutrientID = S.AIMNutrientID
				) S
			GROUP BY 
					S.AIMMaterialID ' + @S + @Location + 
				', S.AIMNutrientID
		) S
	) Q
		ON	Q.AIMMaterialID = ST.AIMMaterialID ' + @QON +
		'AND	Q.AIMNutrientID = ST.AIMNutrientID 
ORDER BY 
		ST.AIMMaterialID
	, ST.AIMNutrientID'
EXEC (@SQL)
--print @sql
-----------------------------------------------------------------------------

SET @SQL = 
'SELECT
	  S.SampleID
	, S.SampleCode
	, S.AIMMaterialID
	, S.AIMOwnerID
	, S.AIMOwnerLocationID
	, S.AIMSourceID
	, S.AIMSourceLocationID
	, S.SampleDate
	, S.IsActive
	, S.AIMNutrientID
	, S.AIMUsedValue
	, S.SampleAIMID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, AN.MathTypeID
	, CASE WHEN ST.LMin > S.AIMUsedValue THEN			
			1
		ELSE
			0
		END AS LMin
	, CASE WHEN ST.LMax > S.AIMUsedValue THEN
			1
		ELSE
			0
		END AS LMax
	, CASE WHEN S.AIMUsedValue > ST.UMin THEN
			1
		ELSE
			0
		END AS UMin
	, CASE WHEN S.AIMUsedValue > ST.UMax THEN
			1
		ELSE
			0
		END AS UMax
INTO ##Statis2
FROM 
	##Sample S
	INNER JOIN 
		dbo.AIMMaterial AM WITH(NOLOCK)
			ON	AM.AIMMaterialID = S.AIMMaterialID 
			AND AM.IsActive = 1
	INNER JOIN 
		dbo.AIMNutrient AN WITH(NOLOCK)
			ON	AN.AIMNutrientID = S.AIMNutrientID  
			AND AN.IsActive = 1 	
	LEFT OUTER JOIN	
		##Statis1 ST 
			ON	ST.AIMMaterialID = S.AIMMaterialID ' + @SON +
			'AND ST.AIMNutrientID = S.AIMNutrientID'

EXEC (@SQL)
--print @sql

-----------------------------------------------------------------------------
DELETE 
	dbo.SampleAIMRefCFCUser
WHERE
		CFCUserID = @CFCUserID
	AND IsUserFlag = 0

-----------------------------------------------------------------------------
IF ISNULL(@OutlierType, 1) > 1
	BEGIN
		IF @OutlierType = 2						--Mild Outlier Excluded
			BEGIN
				SET @Outlier = 'S1.LMin > S2.AIMUsedValue OR S2.AIMUsedValue > S1.UMin'
			END
		ELSE									--Extreme Outlier Excluded
			BEGIN
				SET @Outlier = 'S1.LMax > S2.AIMUsedValue OR S2.AIMUsedValue > S1.UMax'
			END					
				SET @SQL = 'INSERT INTO dbo.SampleAIMRefCFCUser(
					  SampleAIMID
					, CFCUserID
					, IsUserFlag
					, UpdateStamp) 
				SELECT
					  S2.SampleAIMID
					, ' + CAST(@CFCUserID AS VARCHAR) + '
					, 0
					, GETDATE()					
				FROM 
					##Statis2 S2
					INNER JOIN 
						##Statis1 S1
							ON	S1.AIMMaterialID = S2.AIMMaterialID
							AND S1.AIMNutrientID = S2.AIMNutrientID
				WHERE ' + @Outlier 
		EXEC (@SQL)
		--print @sql
	END

-----------------------------------------------------------------------------
-- CFCPlant temp table
SET @SQL = 
'SELECT 
	  CPRAOL.CFCPlantID
	, CPRAOL.AIMOwnerLocationID
    , CP.CFCPlantCode
    , CP.CFCPlantName
INTO ##CFCPlant
FROM
	dbo.CFCPlantRefAIMOwnerLocation CPRAOL			
	INNER JOIN 
		dbo.CFCPlant CP
			ON  CP.CFCPlantID = CPRAOL.CFCPlantID 
			AND CP.AIMDatabaseID = ' + Cast(@AimDataBaseID as varchar) 
EXEC (@SQL)

-----------------------------------------------------------------------------
-- CFCIngredient temp table
SET @SQL = 
'SELECT 
	  CIRAM.CFCIngredientID
	, CIRAM.AIMMaterialID
    , CI.CFCIngredientCode
    , CI.CFCIngredientName
INTO ##CFCIngredient
FROM
	dbo.CFCIngredientRefAIMMaterial CIRAM
	INNER JOIN 
		dbo.CFCIngredient CI
			ON CI.CFCIngredientID = CIRAM.CFCIngredientID 	 
			AND CI.AIMDatabaseID = ' + Cast(@AimDataBaseID as varchar) 
EXEC (@SQL)

-----------------------------------------------------------------------------
-- CFCNutrient temp table
SET @SQL = 
'SELECT 
	  CNRAN.CFCNutrientID
	, CNRAN.AIMNutrientID
    , CN.CFCNutrientCode
    , CN.CFCNutrientName
	, CN.Unit
INTO ##CFCNutrient
FROM
	dbo.CFCNutrientRefAIMNutrient CNRAN 
	INNER JOIN 
		dbo.CFCNutrient CN
			ON CN.CFCNutrientID = CNRAN.CFCNutrientID 				
			AND CN.AIMDatabaseID = ' + Cast(@AimDataBaseID as varchar) 
EXEC (@SQL)

-----------------------------------------------------------------------------
--This get the numbers for the proposed math   Any changes has to be made to the ##Statis1 table
SET @SQL = 'SELECT 
	  ST.AIMMaterialID' + @ST + @Location +
	', ST.AIMNutrientID
	, Q.ProposedSampleSize
	, ISNULL(Q.ProposedQuartile1, 0) AS ProposedQuartile1
	, ISNULL(Q.ProposedQuartile3, 0) AS ProposedQuartile3	
	, ST.ProposedMean
	, ST.ProposedStandardDeviation
	, ST.ProposedDetailMin
	, ST.ProposedDetailMax
	, ST.ProposedDetailSQRT
	, ISNULL(ROUND(Q.ProposedQuartile1 - 1.5 * (Q.ProposedQuartile3 - Q.ProposedQuartile1), 4), 0) AS ProposedLMin
	, ISNULL(ROUND(Q.ProposedQuartile1 - 3 * (Q.ProposedQuartile3 - Q.ProposedQuartile1), 4), 0) AS ProposedLMax
	, ISNULL(ROUND(Q.ProposedQuartile3 + 1.5 * (Q.ProposedQuartile3 - Q.ProposedQuartile1), 4), 0) AS ProposedUMin
	, ISNULL(ROUND(Q.ProposedQuartile3 + 3 * (Q.ProposedQuartile3 - Q.ProposedQuartile1), 4), 0) AS ProposedUMax
	, CASE WHEN ISNULL(Q.ProposedQuartile1, 0) = ISNULL(Q.ProposedQuartile3, 0) THEN 0 ELSE 
	  CASE WHEN ISNULL(Q.ProposedQuartile1, 0) + ISNULL(Q.ProposedQuartile3, 0) = 0 THEN 0 ELSE 
	  CASE WHEN ISNULL(Q.ProposedQuartile1, 0) + ISNULL(Q.ProposedQuartile3, 0) = 0 THEN 0 ELSE 1 END END END AS IsProposedValidOutlier
INTO ##Proposed
FROM
	(
	SELECT 
		  S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID
		, ROUND(AVG(S.AIMUsedValue), 4) AS ProposedMean
		, ROUND(STDEV(S.AIMUsedValue), 4) AS ProposedStandardDeviation
		, MIN(S.AIMUsedValue) AS ProposedDetailMin
		, MAX(S.AIMUsedValue) AS ProposedDetailMax
		, SQRT(COUNT(*)) AS ProposedDetailSQRT
	FROM      
		##Sample S
		LEFT OUTER JOIN 
			dbo.SampleResultPredict SRP WITH(NOLOCK)
				ON SRP.SampleResultID = S.SampleResultID				
		INNER JOIN 
			dbo.SampleAIM SA WITH(NOLOCK) 
				ON S.SampleID = SA.SampleID
		LEFT JOIN 
			dbo.SampleAIMRefCFCUser SARCU WITH(NOLOCK)
				ON SARCU.SampleAIMID = S.SampleAIMID
				AND SARCU.CFCUserID	= ' + CAST(@CFCUserID AS VARCHAR) + '
	WHERE
			S.IsActive = 1
		AND SARCU.SampleAIMRefCFCUserID IS NULL ' + @Where + 
	'GROUP BY 
		   S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID 
	) ST
	INNER JOIN
		(
	SELECT 
			S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID
		, COUNT(*) AS ProposedSampleSize
		, ROUND((Max(Q1NextVal) - MAX(Q1Val)) * Max(Q1Frac) + Max(Q1Val), 4) AS ProposedQuartile1
		, ROUND((Max(Q3NextVal) - MAX(Q3Val)) * Max(Q3Frac) + Max(Q3Val), 4) AS ProposedQuartile3
	FROM     
		(
		SELECT 
			  D.AIMMaterialID' +   @D + @Location +
			', D.AIMNutrientID
			, CASE WHEN RowNum = Q1Idx THEN AIMUsedValue ELSE NULL END AS Q1Val
			, CASE WHEN RowNum = Q1Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q1NextVal
			, CASE WHEN RowNum = Q3Idx THEN AIMUsedValue ELSE NULL END AS Q3Val
			, CASE WHEN RowNum = Q3Idx + 1 THEN AIMUsedValue ELSE NULL END AS Q3NextVal
			, Q1Frac
			, Q3Frac
			, AIMUsedValue
		FROM      
			(
			SELECT 
				  S.AIMUsedValue 
				, S.AIMMaterialID' + @S + @Location + 
				', S.AIMNutrientID 
				, ROW_NUMBER() 
				OVER (PARTITION BY S.AIMMaterialID' + ', S.AIMNutrientID
				ORDER BY S.AIMUsedValue) AS RowNum
			FROM      
				##Sample S

				INNER JOIN
					dbo.SampleAIM SA 
						ON S.SampleID = SA.SampleID

				LEFT JOIN 
					dbo.SampleAIMRefCFCUser SARCU WITH(NOLOCK)
						ON SARCU.SampleAIMID = S.SampleAIMID
						AND SARCU.CFCUserID	= ' + CAST(@CFCUserID AS VARCHAR) + '
			WHERE
				SARCU.SampleAIMRefCFCUserID IS NULL ' + @Where + 
			') D 
			LEFT OUTER JOIN
				(/*the values at the quartile points.*/ 
				SELECT 
						S.AIMMaterialID' + @S + @Location + 
					', S.AIMNutrientID
					, FLOOR((COUNT(*) + 3) / 4.0) AS Q1Idx
					, ((COUNT(*) + 3) / 4.0) - FLOOR((COUNT(*) + 3) / 4.0) AS Q1Frac
					, FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Idx
					, ((COUNT(*) * 3 + 1) / 4.0) - FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Frac
				FROM
					##Sample S

					INNER JOIN
					dbo.SampleAIM SA 
						ON S.SampleID = SA.SampleID

					LEFT JOIN 
						dbo.SampleAIMRefCFCUser SARCU WITH(NOLOCK)
							ON SARCU.SampleAIMID = S.SampleAIMID
							AND SARCU.CFCUserID	= ' + CAST(@CFCUserID AS VARCHAR) + '
				WHERE   
					SARCU.SampleAIMRefCFCUserID IS NULL	' + @Where +  
				'GROUP BY 
						S.AIMMaterialID' + @S + @Location + 
					', S.AIMNutrientID
				HAVING  COUNT(*) > 1
				) AS S 
					ON	D.AIMMaterialID = S.AIMMaterialID ' + @ON +  
					'AND D.AIMNutrientID = S.AIMNutrientID
		) AS S
	GROUP BY 
			S.AIMMaterialID ' + @S + @Location + 
		', S.AIMNutrientID
	) Q
		ON	Q.AIMMaterialID = ST.AIMMaterialID ' + @QON +
		'AND	Q.AIMNutrientID = ST.AIMNutrientID 
ORDER BY 
		ST.AIMMaterialID
	, ST.AIMNutrientID'
EXEC (@SQL)
--print @sql

-----------------------------------------------------------------------------
--This gets the final query - and will have different columns based on the grouping
SET @SQL = 'SELECT
	  CASE WHEN T.IncludedSample = 0 THEN 0 ELSE 1 END AS IsInclude
	, ISNULL(SR.SampleSize, 1)  AS SampleSize	
	, T.CFCIngredientName
	, T.CurrentNutrientVal
	, T.SampleValue
	, T.CFCIngredientCode
	, T.Unit
	, T.AIMMaterialID 
	, T.AIMMaterialCode 
	, T.AIMMaterialName 
	, T.AIMNutrientID
	, T.AIMNutrientCode 
	, T.AIMNutrientName 
	, T.MathTypeID
	, T.CFCPlantID
	, T.CFCPlantCode
	, T.CFCPlantName
	, T.CFCNutrientCode
	, T.CFCNutrientName
	, T.NumberOfSample
	, T.CFCIngredientID	
	, T.CFCNutrientID
	, SR.IsValidOutlier
	, ISNULL(SR.Quartile1, T.SampleValue) AS Quartile1 
	, ISNULL(SR.Quartile3, T.SampleValue) AS Quartile3 
	, ISNULL(SR.Mean, T.SampleValue) AS Mean
	, ISNULL(SR.StandardDeviation, 0) AS StandardDeviation
	, ISNULL(SR.DetailMin, T.SampleValue) AS DetailMin
	, ISNULL(SR.DetailMax, T.SampleValue) AS DetailMax
	, ISNULL(SR.DetailSQRT, T.SampleValue) AS DetailSQRT 
	, ISNULL(SR.WarningGlobalH, 0) AS WarningGlobalH
	, ISNULL(SR.WarningMinMaxLimit, 0) AS WarningMinMaxLimit
	, ISNULL(T.LMin,0) AS LMin
	, ISNULL(T.LMax,0) AS LMax
	, ISNULL(T.UMin,0) AS UMin
	, ISNULL(T.UMax,0) AS UMax
	, P.IsProposedValidOutlier
	, ISNULL(P.ProposedQuartile1, T.SampleValue) AS ProposedFirstQuartile
	, ISNULL(P.ProposedQuartile3, T.SampleValue) AS ProposedThirdQuartile
	, ISNULL(P.ProposedMean, T.SampleValue) AS ProposedMean
	, ISNULL(P.ProposedStandardDeviation, 0) AS ProposedStandardDeviation
	, ISNULL(P.ProposedDetailMin, T.SampleValue) AS ProposedDetailMin
	, ISNULL(P.ProposedDetailMax, T.SampleValue) AS ProposedDetailMax
	, ISNULL(P.ProposedDetailSQRT, T.SampleValue) AS ProposedDetailSQRT 
	' + @Total +    
' FROM 
	(
	SELECT
		  S.IsActive
		, S.AIMMaterialID 
		, S.AIMMaterialCode 
		, S.AIMMaterialName 
		, S.AIMNutrientID 
		, S.AIMNutrientCode 
		, S.AIMNutrientName 
		, S.MathTypeID
		, CI.CFCIngredientName
		, CI.CFCIngredientID
		, CI.CFCIngredientCode
		, CN.Unit
		, CP.CFCPlantID
		, CP.CFCPlantCode
		, CP.CFCPlantName
		, CN.CFCNutrientID
		, CN.CFCNutrientCode
		, CN.CFCNutrientName	
		, ISNULL(AVG(CIN.Value), 0) AS CurrentNutrientVal					
		, AVG(S.AIMUsedValue) AS SampleValue
		, SUM(S.LMin) AS LMin
		, SUM(S.LMax) AS LMax
		, SUM(S.UMin) AS UMin
		, SUM(S.UMax) AS UMax
		, SUM(CASE WHEN SARCU.SampleAIMID IS NULL THEN 1 ELSE 0 END) AS IncludedSample
		, COUNT(*) AS NumberOfSample ' + @Group +     
	' FROM 
		##Statis2 S 
				
		LEFT JOIN 
			dbo.SampleAIMRefCFCUser SARCU WITH(NOLOCK)
				ON SARCU.SampleAIMID = S.SampleAIMID
				AND SARCU.CFCUserID	= ' + CAST(@CFCUserID AS VARCHAR) + 
'		LEFT OUTER JOIN 
			dbo.AIMOwner AO 
				ON AO.AIMOwnerID = S.AIMOwnerID 
				AND AO.IsActive = 1
		LEFT OUTER JOIN 
			dbo.AIMOwnerLocation AOL 
				ON AOL.AIMOwnerLocationID = S.AIMOwnerLocationID 
				AND AOL.IsActive = 1
		LEFT OUTER JOIN 
			dbo.AIMSource SS 
				ON  SS.AIMSourceID = S.AIMSourceID 
				AND SS.IsActive = 1				
		LEFT OUTER JOIN 
			dbo.AIMSourceLocation ASL 
				ON ASL.AIMSourceLocationID = S.AIMSourceLocationID 
				AND ASL.IsActive = 1
		LEFT OUTER JOIN 		
			##CFCNutrient CN 
				ON CN.AIMNutrientID = S.AIMNutrientID 				
		LEFT OUTER JOIN 
			##CFCPlant CP
				ON CP.AIMOwnerLocationID = S.AIMOwnerLocationID 
		LEFT OUTER JOIN 
			##CFCIngredient CI
				ON	CI.AIMMaterialID = S.AIMMaterialID 							
		LEFT OUTER JOIN 
			dbo.CFCIngredientNutrient CIN
				ON	CIN.CFCIngredientID = CI.CFCIngredientID 
				AND	CIN.CFCNutrientID = CN.CFCNutrientID 
	WHERE 
			S.IsActive = 1 ' + @Where + @CFCWhere +
	' GROUP BY
			  S.IsActive
			, CI.CFCIngredientName
			, CI.CFCIngredientCode
			, CI.CFCIngredientID
			, S.AIMMaterialID 
			, S.AIMMaterialCode 
			, S.AIMMaterialName 
			, S.AIMNutrientID 
			, S.AIMNutrientCode 
			, S.AIMNutrientName 
			, S.MathTypeID
			, CN.Unit
			, CP.CFCPlantID
			, CP.CFCPlantCode
			, CP.CFCPlantName
			, CI.CFCIngredientCode
			, CN.CFCNutrientID
			, CN.CFCNutrientCode					
			, CN.CFCNutrientName ' + @Group +
		' ) T
	LEFT OUTER JOIN	
		##Statis1 SR 
			ON	SR.AIMMaterialID = T.AIMMaterialID ' + @SRON +
'			AND SR.AIMNutrientID = T.AIMNutrientID   
	LEFT OUTER JOIN	
		##Proposed P
			ON	P.AIMMaterialID = T.AIMMaterialID ' + @SRON + 
'			AND P.AIMNutrientID = T.AIMNutrientID '  + @TWhere + 
' ORDER BY 
	  T.AIMMaterialID
	, T.AIMNutrientID ' + @Total


--EXEC (@SQL)
--print @TWhere 
--print @CFCWhere
print @SQL

/*
DROP TABLE ##Sample
DROP TABLE ##Statis1
DROP TABLE ##Proposed
DROP TABLE ##Statis2
DROP TABLE ##CFCPlant
DROP TABLE ##CFCIngredient
DROP TABLE ##CFCNutrient
DROP TABLE ##Field
*/

END