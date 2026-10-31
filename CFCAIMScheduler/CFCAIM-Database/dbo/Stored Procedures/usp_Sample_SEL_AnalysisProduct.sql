CREATE PROCEDURE [dbo].[usp_Sample_SEL_AnalysisProduct]
			  @Debug							VARCHAR(1) = NULL
			, @AnalysisQuery					INT
			, @StartDate						DATE	= NULL
			, @EndDate							DATE	= NULL
			, @QueryTemplateID					INT		= NULL	
				
			--12/1/22				
			, @ApplyToleranceGroup				INT		= NULL	
			
			--12/9/22
			, @NutrientToleranceHeaderID		INT		= NULL

			, @IsSubSet							BIT		= NULL
			, @AimDataBaseID					INT		= NULL
			, @AIMMaterialID					INT     = NULL
			, @AIMNutrientID					INT		= NULL
			, @SampleDate						DATE	= NULL
			, @SampleID							INT		= NULL
			, @AIMOwnerID						INT		= NULL
			, @AIMOwnerLocationID				INT		= NULL									
			, @CFCUserID						INT		= NULL
			, @OutlierType						INT		= NULL		
			, @CFCProductID						INT		= NULL					-- this isn't used ?
			, @ArchiveVersion					VARCHAR(10) = NULL				--1/4/23			

AS 
BEGIN
DECLARE @SQL					VARCHAR(6000) = ''
DECLARE @Total					VARCHAR(1000) = ''
DECLARE @Where					VARCHAR(1000) = ''
DECLARE @WhereValue				VARCHAR(1000) = ''
DECLARE @Group					VARCHAR(1000) = ''
DECLARE @CFCLocation			VARCHAR(1000) = ''
DECLARE @CFCLocationGroup		VARCHAR(1000) = ''
DECLARE @Location				VARCHAR(100) = ''
DECLARE @LocationStat			VARCHAR(100) = ''
DECLARE @SStat					VARCHAR(4) = ''
DECLARE @DStat					VARCHAR(4) = ''
DECLARE @ONStat					VARCHAR(60) = ''
DECLARE @QONStat				VARCHAR(60) = ''
DECLARE @S						VARCHAR(4) = ''
DECLARE @D						VARCHAR(4) = ''
DECLARE @ST						VARCHAR(5) = ''
DECLARE @ON						VARCHAR(60) = ''
DECLARE @SRON					VARCHAR(60) = ''
DECLARE @PON					VARCHAR(60) = ''
DECLARE @SON					VARCHAR(60) = ''
DECLARE @QON					VARCHAR(60) = ''
DECLARE @TWhere					VARCHAR(500) = ''
DECLARE @Outlier				VARCHAR(70) = ''
DECLARE @QueryFieldID			INT = 0
DECLARE @StrNutrValue			VARCHAR(1000) = ''
DECLARE @StrJoinNutrValue		VARCHAR(1000) = ''
DECLARE @CFCWhere				VARCHAR(1000) = ''
DECLARE @WhereNutrient			VARCHAR(1000) = ''
DECLARE @MatNutWhere			VARCHAR(1000) = ''
DECLARE @SampleName				VARCHAR(20) = ' ##Sample' 
DECLARE @ProposedName			VARCHAR(20) = ' ##Proposed' 
DECLARE @Statis1Name			VARCHAR(20) = ' ##Statis1' 
DECLARE @Statis2Name			VARCHAR(20) = ' ##Statis2' 
DECLARE @SampleRowNumberName	VARCHAR(25) = ' ##SampleRowNumber'
DECLARE @CFCPlantName			VARCHAR(20) = ' ##CFCPlant' 
DECLARE @CFCProductName			VARCHAR(20) = ' ##CFCProduct' 
DECLARE @CFCNutrientName		VARCHAR(20) = ' ##CFCNutrient' 

SET NOCOUNT ON;

SET @SampleName = @SampleName + CAST(@CFCUserID AS VARCHAR) + ' '
SET @ProposedName = @ProposedName + CAST(@CFCUserID AS VARCHAR) + ' '
SET @Statis1Name = @Statis1Name + CAST(@CFCUserID AS VARCHAR) + ' '
SET @Statis2Name = @Statis2Name + CAST(@CFCUserID AS VARCHAR) + ' '
SET @SampleRowNumberName = @SampleRowNumberName + CAST(@CFCUserID AS VARCHAR) + ' '
SET @CFCPlantName = @CFCPlantName + CAST(@CFCUserID AS VARCHAR) + ' '
SET @CFCProductName = @CFCProductName + CAST(@CFCUserID AS VARCHAR) + ' '
SET @CFCNutrientName = @CFCNutrientName + CAST(@CFCUserID AS VARCHAR) + ' '

-- Set T-WHERE string -------------------------------------------------------- 
IF @IsSubSet = 1
	BEGIN		
		Set @TWhere = ' WHERE '
		Set @TWhere += ' T.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar)
		Set @TWhere += ' and T.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar)
		
		--1/4/23
		--IF @SampleDate IS NOT NULL 
		IF @SampleDate IS NOT NULL and @SampleDate <> ''

			Set @TWhere += ' and T.SampleDate = ''' + Cast(@SampleDate as varchar) + ''' '		
		If @SampleID IS NOT NULL AND @SampleID <> 0
			Set @Twhere += ' and T.SampleID = ' + Cast(@SampleID as varchar)		
		If @AIMOwnerID IS NOT NULL AND @AIMOwnerID <> 0
			Set @Twhere += ' and T.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar)
		If @AIMOwnerLocationID IS NOT NULL AND @AIMOwnerLocationID <> 0
			Set @Twhere += ' and T.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar)		
		
		--1/4/23
		If @ArchiveVersion IS NOT NULL
			Set @TWhere += ' and T.ArchiveVersion = ''' + @ArchiveVersion + ''''
	END

-----------------------------------------------------------------------------
-- This is used when doing an overlay location 
SET @StrNutrValue = 'ISNULL(CPAN.NutrientValue, 0) AS PredictedValue, 0 AS IsOverlay '

-----------------------------------------------------------------------------
--- If there is no query, only the date is used on the where statement
IF @QueryTemplateID = 0
	SET @Where = 'AND S.SampleDate BETWEEN ''' + CAST(@StartDate AS VARCHAR) + ''' AND ''' + CAST(@EndDate AS VARCHAR) + ''' '

-----------------------------------------------------------------------------
---	This creates the where statement that the query will be using
ELSE
	BEGIN			

		-- These functions create the where statements the query will use
		EXEC @Where = [dbo].[AnalysisReports_WhereStatement] @QueryTemplateID 

		-- If "Product" tab is eventually added to the Query Form, then the following function needs to be created 
		-- (copy from AnalysisReports_CFCIngredient_WhereStatement) -- and add another record to the QueryField table 
		-- for Product Code.		

		----12/21/22
		EXEC @CFCWhere = [dbo].[AnalysisReports_CFCProductType_WhereStatement] @QueryTemplateID
		--IF LEN(@CFCWhere) > 0
		--	SET @Where += @CFCWhere 
		
		EXEC @MatNutWhere = [dbo].[AnalysisReports_MaterialNutrient_WhereStatement] @QueryTemplateID 	
		IF LEN(@MatNutWhere) > 0
			SET @Where += @MatNutWhere 	
	END

-----------------------------------------------------------------------------
--- This is used to get the numbers for each grouping
-- 1 = NoGroup
-- 2 = SampleID
-- 3 = SampleDate

IF @IsSubSet = 0
	Begin
	SET @Group = 
		CASE @AnalysisQuery 		

			WHEN 1 THEN  ''
			WHEN 2 THEN	 ', CPROD.ProductionVersion, S.SampleID, S.SampleDate, AO.OwnerCode, AO.AIMOwnerID, S.AIMOwnerLocationID, AOL.LocationCode, S.SampleCode, S.SampleAIMID '				
		--	WHEN 3 THEN	 ', CPROD.ProductionVersion, S.SampleDate '
			WHEN 3 THEN	 ', S.SampleDate '
		END
	End
Else
	Begin
		SET @Group = 
		CASE @AnalysisQuery 		

			WHEN 1 THEN  ''
			WHEN 2 THEN	 ', S.SampleID, S.SampleDate, AO.OwnerCode, AO.AIMOwnerID, S.AIMOwnerLocationID, AOL.LocationCode, S.SampleCode, S.SampleAIMID '				
			WHEN 3 THEN	 ', S.SampleDate '
			
		END	
	End
-----------------------------------------------------------------------------
--This is used to get the final grouping 
IF @IsSubSet = 0
	Begin
		SET @Total =  
			CASE @AnalysisQuery 
				WHEN 1 THEN	 ''
				WHEN 2 THEN	 ', T.ProductionVersion, T.SampleID, T.SampleDate, T.OwnerCode, T.AIMOwnerID, T.AIMOwnerLocationID, T.LocationCode, T.SampleCode, T.SampleAIMID '				
				--WHEN 3 THEN	 ', T.ProductionVersion, T.SampleDate '
				WHEN 3 THEN	 ', T.SampleDate '
			END
	End
Else
	Begin
		SET @Total =  
			CASE @AnalysisQuery 
				WHEN 1 THEN	 ''
				WHEN 2 THEN	 ', T.SampleID, T.SampleDate, T.OwnerCode, T.AIMOwnerID, T.AIMOwnerLocationID, T.LocationCode, T.SampleCode, T.SampleAIMID '				
				WHEN 3 THEN	 ', T.SampleDate '

			END
	End

-----------------------------------------------------------------------------
IF @AnalysisQuery IN (2)														-- All these AnalysisQueries include the OwnerLocation
	BEGIN
		SET @CFCLocation = ', T.CFCPlantID, T.CFCPlantCode, T.CFCPlantName '
		SET @CFCLocationGroup = ', CP.CFCPlantID, CP.CFCPlantCode, CP.CFCPlantName '
	END
	
-----------------------------------------------------------------------------
--12/1/22
IF @ApplyToleranceGroup = 3
		Begin

		SELECT x.* 
		  INTO #NutrientTolerance
		  FROM (SELECT AIMNutrientID, 
					MinAmount as SpecsMinAmount, MinPercent as SpecsMinPercent, 
					MaxAmount as SpecsMaxAmount, MaxPercent as SpecsMaxPercent,
					0 as PredictedMinAmount, 0 as PredictedMinPercent, 0 as PredictedMaxAmount, 0 as PredictedMaxPercent 
				FROM NutrientToleranceHeader NTH 
				INNER JOIN
					NutrientToleranceDetail NTD 
						ON NTH.NutrientToleranceHeaderID = NTD.NutrientToleranceHeaderID
				WHERE
					NTH.NutrientToleranceHeaderID = @NutrientToleranceHeaderID
				UNION
				SELECT AIMNutrientID, 
					0 as SpecsMinAmount, 0 as SpecsMinPercent,	0 as SpecsMaxAmount, 0 as SpecsMaxPercent,
					MinAmount as PredictedMinAmount, MinPercent as PredictedMinPercent, 
					MaxAmount as PredictedMaxAmount, MaxPercent as PredictedMaxPercent 
				FROM
					NutrientToleranceHeader NTH 
				INNER JOIN
					NutrientToleranceDetailTarget NTDT
						ON NTH.NutrientToleranceHeaderID = NTDT.NutrientToleranceHeaderID
				WHERE
					NTH.NutrientToleranceHeaderID = @NutrientToleranceHeaderID
			 ) x		
		End
		
-----------------------------------------------------------------------------
-- This inserts into temp Sample
-- 12/19/22 - added CFCProductArchiveID to select below
SET @SQL = 
'SELECT
	  S.SampleID
	, S.SampleCode
	, S.AIMMaterialID 
	, S.AIMOwnerID
	, S.AIMOwnerLocationID	
	, S.SampleDate
	, SR.SampleResultID
	, SA.IsActive
	, SA.AIMNutrientID
	, SA.AIMUsedValue
	, SA.SampleAIMID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, AN.MathTypeID
	, S.SampleTypeID
	, S.CFCProductArchiveID
'
if @AnalysisQuery = 2
	Begin
		SET @SQL += ' , case when DST.Name is null then '''' else DST.Name end as DataSourceTypeName '
	End

SET @SQL += '	

INTO ' + @SampleName +
'FROM
dbo.Sample S WITH(NOLOCK)
INNER JOIN 
	dbo.SampleResult SR
		ON  SR.SampleID = S.SampleID
		AND SR.IsActive = 1
INNER JOIN 
	dbo.SampleAIM SA 
		ON	SA.SampleID = S.SampleID
		AND	SA.SampleResultID = SR.SampleResultID
		AND SA.IsActive = 1 
	INNER JOIN 
		dbo.AIMMaterial AM 
			ON	AM.AIMMaterialID = S.AIMMaterialID 
			AND AM.IsActive = 1
			AND AM.MaterialTypeID = 2			
	INNER JOIN 
		dbo.AIMNutrient AN 
			ON	AN.AIMNutrientID = SA.AIMNutrientID  
			AND AN.IsActive = 1 '

if LEN(@CFCWhere) > 0
Begin
	SET @SQL += '
	LEFT OUTER JOIN
		dbo.CFCProductArchive CPA on 
			S.CFCProductArchiveID = CPA.CFCProductArchiveID
	LEFT OUTER JOIN
		dbo.CFCProduct CP on
			CPA.CFCProductID = CP.CFCProductID
	LEFT OUTER JOIN
		dbo.CFCProductRefCFCProductType CPTX on
			CP.CFCProductID = CPTX.CFCProductID '
End


-----------------------------------------------------------------------------
-- AnalyzerNutrient and DataSourceType joins added because Smithfield wants the option to see the datasource on the Analysis grid.
--		   BUT ... the datasource can show up only when the Analysis grid is set to GROUP BY SAMPLEID (@AnalysisQuery = 2)
if @AnalysisQuery = 2
	Begin
		SET @SQL += '
			LEFT OUTER JOIN																					
				dbo.AnalyzerNutrient ANut
					ON SR.AnalyzerNutrientID = ANut.AnalyzerNutrientID
			LEFT OUTER JOIN
					dbo.DataSourceType DST 
				ON ANut.DataSourceTypeID = DST.DataSourceTypeID '
	End
SET @SQL += '
WHERE
	S.IsActive = 1 ' + @Where 

IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END

-----------------------------------------------------------------------------
--This gets the numbers for the math. Any changes have to be made to the ##Proposed table
--When doing the Quartile and the sample size is less than four, Quartile1 = min and Quartile3 = max of the sample set
--This is so the outlier will not kick those values out if there are fewer than 4 samples.

--9/8/17 -- added "AND S.AIMNutrientID = SAM.AIMNutrientID" to join of SampleAIM table below.
--			Perdue data somehow calulates exponential standard dev of 3.76972873230979E-08 
--			on SampleID 53511, AIMMaterialID 947, AIMNutrientID 102, SampleResultID 271862, AIMUsedValue = 3.15

--untrimmed SampleSize is in the next select statement below.
SET @SQL = 'SELECT 
	  ST.AIMMaterialID' + @ST + @Location + '
	, ST.AIMNutrientID
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
	  CASE WHEN ISNULL(Q.Quartile3, 0) = 0 THEN 0 ELSE 1 END END END AS IsUntrimmedValidOutlier
INTO' + @Statis1Name + '
FROM
	(
	SELECT 
		  S.AIMMaterialID' + @SStat + @LocationStat + '
		, S.AIMNutrientID
		, ROUND(AVG(S.AIMUsedValue), 4) AS Mean
		, ROUND(STDEV(S.AIMUsedValue), 4) AS StandardDeviation
		, MIN(S.AIMUsedValue) AS DetailMin
		, MAX(S.AIMUsedValue) AS DetailMax
		, SQRT(COUNT(*)) AS DetailSQRT
		, SUM(CASE WHEN SAM.AimUsedValue NOT BETWEEN WL.Min and WL.Max THEN 1 ELSE 0 END) as WarningMinMaxLimit
		, SUM(CASE WHEN SRP.GlobalH > SRP.Limit THEN 1 ELSE 0 END) as WarningGlobalH	

	FROM' + @SampleName + 'S     
		LEFT OUTER JOIN 
			dbo.SampleResultPredict SRP WITH(NOLOCK)
				ON SRP.SampleResultID = S.SampleResultID				
		LEFT JOIN 
			dbo.SampleAIM SAM WITH(NOLOCK) 
				ON S.SampleID = SAM.SampleID
				AND S.AIMNutrientID = SAM.AIMNutrientID
		LEFT JOIN 
			dbo.WarningLimits WL WITH(NOLOCK)
				ON S.AIMMaterialID = WL.AIMMaterialID
				AND S.AIMNutrientID = WL.AIMNutrientID
	GROUP BY 
			S.AIMMaterialID' + @SStat + @LocationStat +
		', S.AIMNutrientID 
	) ST
	INNER JOIN
		(
	SELECT 
		  S.AIMMaterialID' + @SStat + @LocationStat + '
		, S.AIMNutrientID
		, S.SampleSize
		, CASE WHEN SampleSize < 4 THEN MinValue ELSE Quartile1 END Quartile1
		, CASE WHEN SampleSize < 4 THEN MaxValue ELSE Quartile3 END Quartile3
	FROM (	    
		SELECT 
			  S.AIMMaterialID' + @SStat + @LocationStat + '
			, S.AIMNutrientID
			, COUNT(*) AS SampleSize
			, MAX(AIMUsedValue) AS MaxValue
			, MIN(AIMUsedValue) AS MinValue
			, ROUND((Max(Q1NextVal) - MAX(Q1Val)) * Max(Q1Frac) + Max(Q1Val), 4) AS Quartile1
			, ROUND((Max(Q3NextVal) - MAX(Q3Val)) * Max(Q3Frac) + Max(Q3Val), 4) AS Quartile3
		FROM     
			(
			SELECT 
				  D.AIMMaterialID' + @DStat + @LocationStat + '
				, D.AIMNutrientID
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
					, S.AIMMaterialID' + @SStat + @LocationStat + '
					, S.AIMNutrientID 
					, ROW_NUMBER() 
					OVER (PARTITION BY S.AIMMaterialID' + ', S.AIMNutrientID
					ORDER BY S.AIMUsedValue) AS RowNum
				FROM' + @SampleName + 'S      
				) D 
				LEFT OUTER JOIN
					(/*the values at the quartile points.*/ 
					SELECT 
						  S.AIMMaterialID' + @SStat + @LocationStat + '
						, S.AIMNutrientID
						, FLOOR((COUNT(*) + 3) / 4.0) AS Q1Idx
						, ((COUNT(*) + 3) / 4.0) - FLOOR((COUNT(*) + 3) / 4.0) AS Q1Frac
						, FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Idx
						, ((COUNT(*) * 3 + 1) / 4.0) - FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Frac
					FROM' + @SampleName + 'S 
					GROUP BY 
							S.AIMMaterialID' + @SStat + @LocationStat + 
						', S.AIMNutrientID
					HAVING  COUNT(*) > 1
					) S 
						ON	D.AIMMaterialID = S.AIMMaterialID ' + @ONStat +  
						'AND D.AIMNutrientID = S.AIMNutrientID
				) S
			GROUP BY 
					S.AIMMaterialID ' + @SStat + @LocationStat + 
				', S.AIMNutrientID
		) S
	) Q
		ON	Q.AIMMaterialID = ST.AIMMaterialID ' + @QONStat +
		'AND	Q.AIMNutrientID = ST.AIMNutrientID 
ORDER BY 
		ST.AIMMaterialID
	, ST.AIMNutrientID'

IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END

-----------------------------------------------------------------------------
-- 12/19/22 - added CFCProductArchiveID to select below
SET @SQL = 
'SELECT
	  S.SampleID
	, S.SampleCode
	, S.AIMMaterialID
	, S.AIMOwnerID
	, S.AIMOwnerLocationID	
	, S.SampleDate
	, S.IsActive
	, S.AIMNutrientID
	, S.AIMUsedValue
	, S.SampleAIMID
	, S.AIMMaterialCode
	, S.AIMMaterialName
	, S.AIMNutrientCode
	, S.AIMNutrientName
	, S.MathTypeID 
	, S.CFCProductArchiveID '

	if @AnalysisQuery = 2
		Begin
			SET @SQL += ', S.DataSourceTypeName '
		End
	
	SET @SQL += '	
	, CASE When S.AimUsedValue < 0 Then Case When ST.LMin < S.AIMUsedValue Then 1 Else 0 End else Case When St.LMin > S.AimUsedValue Then 1 else 0 end END as LMin
	, CASE When S.AimUsedValue < 0 Then Case When St.LMax < S.AimUsedValue Then 1 Else 0 End else Case When St.LMax > S.AimUsedValue Then 1 else 0 end END as LMax
	, CASE When S.AimUsedValue < 0 Then Case When S.AIMUsedValue < ST.UMin Then 1 Else 0 End else Case When S.AIMUsedValue > ST.UMin Then 1 else 0 end END as UMin
	, CASE When S.AimUsedValue < 0 Then Case When S.AIMUsedValue < ST.UMax Then 1 Else 0 End else Case When S.AIMUsedValue > ST.UMax Then 1 else 0 end END as UMax	

INTO' + @Statis2Name + '
FROM' + @SampleName + 'S 
	LEFT OUTER JOIN	
		' + @Statis1Name + 'ST 
			ON	ST.AIMMaterialID = S.AIMMaterialID ' + @SON +
			'AND ST.AIMNutrientID = S.AIMNutrientID'

IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END
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
					' + @Statis2Name + 'S2
					INNER JOIN 
						' + @Statis1Name + 'S1
							ON	S1.AIMMaterialID = S2.AIMMaterialID
							AND S1.AIMNutrientID = S2.AIMNutrientID
				WHERE ' + @Outlier 
		EXEC (@SQL)		
	END

-----------------------------------------------------------------------------
-- CFCPlant temp table
SET @SQL = 
'SELECT 
	  CPRAOL.CFCPlantID
	, CPRAOL.AIMOwnerLocationID
    , CP.CFCPlantCode
    , CP.CFCPlantName
INTO' + @CFCPlantName + '
FROM
	dbo.CFCPlantRefAIMOwnerLocation CPRAOL			
	INNER JOIN 
		dbo.CFCPlant CP
			ON  CP.CFCPlantID = CPRAOL.CFCPlantID 
			AND CP.AIMDatabaseID = ' + Cast(@AimDataBaseID as varchar) 
IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END

-----------------------------------------------------------------------------
-- CFCProduct temp table
-- 12/19/22 - added CFCProductArchiveID to select below
SET @SQL = 
'SELECT 
	  CPRAM.CFCProductID
	, CPRAM.AIMMaterialID
    , CPROD.CFCProductCode
    , CPROD.CFCProductName
	, CPA.ArchiveVersion
	, CPA.ProductionVersion
	, CPA.CFCProductArchiveID							
	
INTO' + @CFCProductName + '
FROM
	dbo.CFCProductRefAIMMaterial CPRAM
	INNER JOIN 
		dbo.CFCProduct CPROD
			ON CPROD.CFCProductID = CPRAM.CFCProductID 	 
			AND CPROD.AIMDatabaseID = ' + Cast(@AimDataBaseID as varchar) 	+ '
	LEFT OUTER JOIN
		dbo.CFCProductArchive CPA 
			ON CPROD.CFCProductID = CPA.CFCProductID '

IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END
-----------------------------------------------------------------------------
-- CFCNutrient temp table
SET @SQL = 
'SELECT 
	  CNRAN.CFCNutrientID
	, CNRAN.AIMNutrientID
    , CN.CFCNutrientCode
    , CN.CFCNutrientName
	, CN.Unit
INTO' + @CFCNutrientName + '
FROM
	dbo.CFCNutrientRefAIMNutrient CNRAN 
	INNER JOIN 
		dbo.CFCNutrient CN
			ON CN.CFCNutrientID = CNRAN.CFCNutrientID 				
			AND CN.AIMDatabaseID = ' + Cast(@AimDataBaseID as varchar) 

IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END
-----------------------------------------------------------------------------
-- Sample Row Number
SET @SQL = 'SELECT 
		S.AIMUsedValue 
		, S.AIMMaterialID' + @S + @Location + '
		, S.AIMNutrientID 
		, ROW_NUMBER() 
		OVER (PARTITION BY S.AIMMaterialID' + ', S.AIMNutrientID
		ORDER BY S.AIMUsedValue) AS RowNum
	INTO' + @SampleRowNumberName + '
	FROM' + @SampleName + 'S      
		INNER JOIN
			dbo.SampleAIM SA 
				ON S.SampleID = SA.SampleID
		LEFT JOIN 
			dbo.SampleAIMRefCFCUser SARCU WITH(NOLOCK)
				ON SARCU.SampleAIMID = S.SampleAIMID
				AND SARCU.CFCUserID	= ' + CAST(@CFCUserID AS VARCHAR) + '
		WHERE
			SARCU.SampleAIMRefCFCUserID IS NULL ' + @Where 

IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END

-----------------------------------------------------------------------------
--This gets the numbers for the PROPOSED math   Any changes have to be made to the ##Statis1 table

--9/8/17 -- added "AND S.AIMNutrientID = SA.AIMNutrientID" to join of SampleAIM table below.
--			Perdue data somehow calulates exponential standard dev of 3.76972873230979E-08 
--			on SampleID 53511, AIMMaterialID 947, AIMNutrientID 102, SampleResultID 271862, AIMUsedValue = 3.15

--10/2/20 -- added TSS join and TSS.TotalSampleSize to select stmt (per Nickki)

SET @SQL = 'SELECT 
	  ST.AIMMaterialID' + @ST + @Location +
	', ST.AIMNutrientID
	, ST.TrimmedSampleSize
	, TSS.TotalSampleSize
	, ISNULL(Q.TrimmedQuartile1, 0) AS TrimmedQuartile1
	, ISNULL(Q.TrimmedQuartile3, 0) AS TrimmedQuartile3	
	, ST.TrimmedMean
	, ST.TrimmedStandardDeviation
	, ST.TrimmedDetailMin
	, ST.TrimmedDetailMax
	, ST.TrimmedDetailSQRT
	, ISNULL(ROUND(Q.TrimmedQuartile1 - 1.5 * (Q.TrimmedQuartile3 - Q.TrimmedQuartile1), 4), 0) AS TrimmedLMin
	, ISNULL(ROUND(Q.TrimmedQuartile1 - 3 * (Q.TrimmedQuartile3 - Q.TrimmedQuartile1), 4), 0) AS TrimmedLMax
	, ISNULL(ROUND(Q.TrimmedQuartile3 + 1.5 * (Q.TrimmedQuartile3 - Q.TrimmedQuartile1), 4), 0) AS TrimmedUMin
	, ISNULL(ROUND(Q.TrimmedQuartile3 + 3 * (Q.TrimmedQuartile3 - Q.TrimmedQuartile1), 4), 0) AS TrimmedUMax
	, CASE WHEN ISNULL(Q.TrimmedQuartile1, 0) = ISNULL(Q.TrimmedQuartile3, 0) THEN 0 ELSE 
	  CASE WHEN ISNULL(Q.TrimmedQuartile1, 0) + ISNULL(Q.TrimmedQuartile3, 0) = 0 THEN 0 ELSE 
	  CASE WHEN ISNULL(Q.TrimmedQuartile1, 0) + ISNULL(Q.TrimmedQuartile3, 0) = 0 THEN 0 ELSE 1 END END END AS IsTrimmedValidOutlier
INTO' + @ProposedName + '
FROM
	(
	SELECT 
		  S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID
		, ROUND(AVG(S.AIMUsedValue), 4) AS TrimmedMean
		, ROUND(STDEV(S.AIMUsedValue), 4) AS TrimmedStandardDeviation
		, MIN(S.AIMUsedValue) AS TrimmedDetailMin
		, MAX(S.AIMUsedValue) AS TrimmedDetailMax
		, SQRT(COUNT(*)) AS TrimmedDetailSQRT
		, COUNT(*) AS TrimmedSampleSize
	FROM' + @SampleName + 'S      
		LEFT OUTER JOIN 
			dbo.SampleResultPredict SRP WITH(NOLOCK)
				ON SRP.SampleResultID = S.SampleResultID				
		INNER JOIN 
			dbo.SampleAIM SA WITH(NOLOCK) 
				ON S.SampleID = SA.SampleID
				AND S.AIMNutrientID = SA.AIMNutrientID
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
		(SELECT S.AIMMaterialID, S.AIMNutrientID, COUNT(*) AS TotalSampleSize
		FROM' + @SampleName + 'S					
		INNER JOIN dbo.SampleAIM SA 
			ON S.SampleID = SA.SampleID	AND S.AIMNutrientID = SA.AIMNutrientID		
		WHERE S.IsActive = 1 ' + @Where + '
		GROUP BY S.AIMMaterialID, S.AIMNutrientID 
	) TSS 
	ON	TSS.AIMMaterialID = ST.AIMMaterialID AND TSS.AIMNutrientID = ST.AIMNutrientID 	
	INNER JOIN
		(
		SELECT 
				S.AIMMaterialID' + @S + @Location +
			', S.AIMNutrientID		
			, ROUND((Max(Q1NextVal) - MAX(Q1Val)) * Max(Q1Frac) + Max(Q1Val), 4) AS TrimmedQuartile1
			, ROUND((Max(Q3NextVal) - MAX(Q3Val)) * Max(Q3Frac) + Max(Q3Val), 4) AS TrimmedQuartile3
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
				' + @SampleRowNumberName + 'D 
				LEFT OUTER JOIN
					(/*the values at the quartile points.*/ 
					SELECT 
							S.AIMMaterialID' + @S + @Location + 
						', S.AIMNutrientID
						, FLOOR((COUNT(*) + 3) / 4.0) AS Q1Idx
						, ((COUNT(*) + 3) / 4.0) - FLOOR((COUNT(*) + 3) / 4.0) AS Q1Frac
						, FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Idx
						, ((COUNT(*) * 3 + 1) / 4.0) - FLOOR((COUNT(*) * 3 + 1) / 4.0) AS Q3Frac
					FROM' + @SampleName + 'S
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

IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END
	
-----------------------------------------------------------------------------
--This gets the final query - and will have different columns based on the grouping
SET @SQL = 'SELECT
	  CASE WHEN T.IncludedSample = 0 THEN 0 ELSE 1 END AS IsInclude
	, ISNULL(SR.SampleSize, 1)  AS SampleSize	
	, ISNULL(P.TrimmedSampleSize,1) AS TrimmedSampleSize
	, ISNULL(P.TotalSampleSize,1) AS TotalSampleSize	
	, T.CFCProductName
	, T.SampleValue
	, T.CFCProductCode
	, T.Unit
	, T.AIMMaterialID 
	, T.AIMMaterialCode 
	, T.AIMMaterialName 
	, T.AIMNutrientID
	, T.AIMNutrientCode 
	, T.AIMNutrientName 
	, T.MathTypeID '

	if @AnalysisQuery = 2
		Begin
			SET @SQL += ', T.DataSourceTypeName '
		End

	SET @SQL += '
	, T.CFCNutrientCode
	, T.CFCNutrientName
	, T.NumberOfSample
	, T.CFCProductID
	, T.CFCNutrientID
	, SR.IsUntrimmedValidOutlier
	, ISNULL(SR.Quartile1, T.SampleValue) AS Quartile1 
	, ISNULL(SR.Quartile3, T.SampleValue) AS Quartile3 
	, ISNULL(SR.Mean, T.SampleValue) AS UntrimmedMean
	, ISNULL(SR.StandardDeviation, 0) AS UntrimmedStandardDeviation
	, ISNULL(SR.DetailMin, T.SampleValue) AS UntrimmedDetailMin
	, ISNULL(SR.DetailMax, T.SampleValue) AS UntrimmedDetailMax
	, ISNULL(SR.DetailSQRT, T.SampleValue) AS UntrimmedDetailSQRT 
	, ISNULL(SR.WarningGlobalH, 0) AS WarningGlobalH
	, ISNULL(SR.WarningMinMaxLimit, 0) AS WarningMinMaxLimit
	, ISNULL(T.LMin,0) AS LMin
	, ISNULL(T.LMax,0) AS LMax
	, ISNULL(T.UMin,0) AS UMin
	, ISNULL(T.UMax,0) AS UMax
	, P.IsTrimmedValidOutlier
	, ISNULL(P.TrimmedQuartile1, T.SampleValue) AS TrimmedFirstQuartile
	, ISNULL(P.TrimmedQuartile3, T.SampleValue) AS TrimmedThirdQuartile
	, ISNULL(P.TrimmedMean, T.SampleValue) AS TrimmedMean
	, ISNULL(P.TrimmedStandardDeviation, 0) AS TrimmedStandardDeviation
	, ISNULL(P.TrimmedDetailMin, T.SampleValue) AS TrimmedDetailMin
	, ISNULL(P.TrimmedDetailMax, T.SampleValue) AS TrimmedDetailMax
	, ISNULL(P.TrimmedDetailSQRT, T.SampleValue) AS TrimmedDetailSQRT 
	, ISNULL(CPAS.NutrientMin,0) as SpecNutrientMin
	, ISNULL(CPAS.NutrientMax,0) as SpecNutrientMax
	, ISNULL(CPAN.NutrientValue, 0) AS PredictedValue
	, T.CFCProductArchiveID
	'
	
	Begin
		SET @SQL += ', T.ArchiveVersion '
	End

	SET @SQL += ', ' + cast(@ApplyToleranceGroup as varchar) + ' as ApplyToleranceGroup '

	if @ApplyToleranceGroup = 3
		Begin
			SET @SQL += ', SpecsMinAmount, SpecsMinPercent, SpecsMaxAmount, SpecsMaxPercent, PredictedMinAmount, PredictedMinPercent, PredictedMaxAmount, PredictedMaxPercent '
		End

	SET @SQL+= ', ' + @StrNutrValue + @Total + @CFCLocation + ' FROM (SELECT S.CFCProductArchiveID, S.IsActive, S.AIMMaterialID '
		
		if @ApplyToleranceGroup = 1 OR @ApplyToleranceGroup = 3
			Begin
				SET @SQL += ', S.AIMMaterialCode , S.AIMMaterialName '
			End
		else if @ApplyToleranceGroup = 2
			Begin
				SET @SQL += ', Case when GH.AIMMaterialGroupHeaderName is not null then GH.AIMMaterialCode Else S.AIMMaterialCode end as AIMMaterialCode
				, Case when GH.AIMMaterialGroupHeaderName is not null then GH.AIMMaterialGroupHeaderName Else S.AIMMaterialName end as AIMMaterialName '
			End
					
		SET @SQL += ' , S.AIMNutrientID , S.AIMNutrientCode , S.AIMNutrientName , S.MathTypeID '

		if @AnalysisQuery = 2
			Begin
				SET @SQL += ', S.DataSourceTypeName '		
			end
			
		SET @SQL += ' , CPROD.CFCProductID , CPROD.CFCProductCode , CPROD.CFCProductName '			
				
		Begin
			SET @SQL += ', CPROD.ArchiveVersion '
		End

		SET @SQL += ' , CN.Unit	, CN.CFCNutrientID	, CN.CFCNutrientCode , CN.CFCNutrientName, AVG(S.AIMUsedValue) AS SampleValue
		, SUM(S.LMin) AS LMin, SUM(S.LMax) AS LMax, SUM(S.UMin) AS UMin, SUM(S.UMax) AS UMax
		, SUM(CASE WHEN SARCU.SampleAIMID IS NULL THEN 1 ELSE 0 END) AS IncludedSample
		, COUNT(*) AS NumberOfSample ' + @Group + @CFCLocationGroup +    
	' FROM 
		' + @Statis2Name + 'S 
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
				AND AOL.IsActive = 1 AND AOL.IsFormulationPlant = 1
		LEFT OUTER JOIN
			' + @CFCNutrientName + 'CN
				ON CN.AIMNutrientID = S.AIMNutrientID
		LEFT OUTER JOIN
			' + @CFCPlantName + 'CP
				ON CP.AIMOwnerLocationID = S.AIMOwnerLocationID
		LEFT OUTER JOIN
			' + @CFCProductName + 'CPROD
				ON	CPROD.AIMMaterialID = S.AIMMaterialID
				AND CPROD.CFCProductArchiveID = S.CFCProductArchiveID
		LEFT OUTER JOIN
			dbo.CFCProductRefCFCProductType CPTX
				ON CPROD.CFCProductID = CPTX.CFCProductID
				'	
    if @ApplyToleranceGroup = 2
		Begin
		SET @SQL += ' LEFT OUTER JOIN
				dbo.AIMMaterialGroupDetail GD on S.AimMaterialID = GD.AIMMaterialID
			LEFT OUTER JOIN
				dbo.AIMMaterialGroupHeader GH on GD.AIMMaterialGroupHeaderID = GH.AIMMaterialGroupHeaderID '
		End		
	
	SET @SQL += ' WHERE S.IsActive = 1 AND AOL.IsFormulationPlant = 1 ' + @CFCWhere 
	----SET @SQL += ' WHERE S.IsActive = 1 AND AOL.IsFormulationPlant = 1 and S.SampleID in (84, 334, 366, 593, 1955) ' + @CFCWhere 
			
	if @ApplyToleranceGroup = 2
		Begin
			SET @SQL += ' and GH.AIMMaterialGroupHeaderID IS NOT NULL '
		End		

	SET @SQL += 
	' GROUP BY
			  S.CFCProductArchiveID, S.IsActive, CPROD.CFCProductID, CPROD.CFCProductCode, CPROD.CFCProductName'		
		
			Begin
				SET @SQL += ', CPROD.ArchiveVersion, CPROD.ProductionVersion '
			End

			SET @SQL += ' , S.AIMMaterialID '
			
			if @ApplyToleranceGroup = 1 OR @ApplyToleranceGroup = 3
				Begin
					SET @SQL += ' , S.AIMMaterialCode , S.AIMMaterialName '
				End
			else if @ApplyToleranceGroup = 2
				Begin
					SET @SQL += ' , Case when GH.AIMMaterialGroupHeaderName is not null then GH.AIMMaterialCode Else S.AIMMaterialCode end
					, Case when GH.AIMMaterialGroupHeaderName is not null then GH.AIMMaterialGroupHeaderName Else S.AIMMaterialName end '
				End			

			SET @SQL += ', S.AIMNutrientID , S.AIMNutrientCode , S.AIMNutrientName , S.MathTypeID '

			if @AnalysisQuery = 2
				Begin
					SET @SQL += ', S.DataSourceTypeName '		
				End

			SET @SQL += ', CN.Unit , CN.CFCNutrientID , CN.CFCNutrientCode , CN.CFCNutrientName ' + @Group + @CFCLocationGroup + ' ) T

	LEFT OUTER JOIN 
		dbo.CFCProductArchiveNutrient CPAN
			ON	CPAN.CFCProductArchiveID = T.CFCProductArchiveID 
			AND	CPAN.CFCNutrientID = T.CFCNutrientID 
	LEFT OUTER JOIN
		dbo.CFCProductArchiveSpec CPAS
			ON CPAS.CFCProductArchiveID = T.CFCProductArchiveID 
			AND CPAS.CFCNutrientID = T.CFCNutrientID
	LEFT OUTER JOIN	
		' + @Statis1Name + 'SR 
			ON	SR.AIMMaterialID = T.AIMMaterialID ' + @SRON +
'			AND SR.AIMNutrientID = T.AIMNutrientID 
	LEFT OUTER JOIN	
		' + @ProposedName + 'P
			ON	P.AIMMaterialID = T.AIMMaterialID ' + @PON + 
'			AND P.AIMNutrientID = T.AIMNutrientID '  

--12/2/22  
	if @ApplyToleranceGroup = 3
		Begin
			SET @SQL += ' INNER JOIN  #NutrientTolerance NutSpec ON NutSpec.AIMNutrientID = T.AIMNutrientID '
		End 
SET @SQL += @StrJoinNutrValue + @TWhere + ' ORDER BY T.AIMMaterialID, T.AIMNutrientID ' + @Total
	
IF @Debug = 'D'
	BEGIN
		PRINT @SQL
	END
ELSE
	BEGIN
		EXEC(@SQL)
	END

IF @Debug = 'T'
	BEGIN
		PRINT 'Did not drop temp tables'
	END
ELSE
	BEGIN
		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@SampleName))
			BEGIN
				SET @SQL = 'DROP TABLE' + @SampleName 
				EXEC(@SQL)
			END

		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@ProposedName))
			BEGIN
				SET @SQL = 'DROP TABLE' + @ProposedName 
				EXEC(@SQL)
			END

		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@Statis1Name))
			BEGIN
				SET @SQL = 'DROP TABLE' + @Statis1Name 
				EXEC(@SQL)
			END

		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@Statis2Name))
			BEGIN
				SET @SQL = 'DROP TABLE' + @Statis2Name 
				EXEC(@SQL)
			END

		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@SampleRowNumberName))
			BEGIN
				SET @SQL = 'DROP TABLE' + @SampleRowNumberName 
				EXEC(@SQL)
			END

		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@CFCPlantName))
			BEGIN
				SET @SQL = 'DROP TABLE' + @CFCPlantName 
				EXEC(@SQL)
			END

		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@CFCProductName))
			BEGIN
				SET @SQL = 'DROP TABLE' + @CFCProductName  
				EXEC(@SQL)
			END

		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = TRIM(@CFCNutrientName))
			BEGIN
				SET @SQL = 'DROP TABLE' + @CFCNutrientName  
				EXEC(@SQL)
			END

	
		--12/9/22
		IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] = '#NutrientTolerance')
			DROP TABLE #NutrientTolerance
	END	


END