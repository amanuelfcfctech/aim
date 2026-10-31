

CREATE PROCEDURE [dbo].[usp_Sample_SEL_Analysis]
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
			, @SpecificCFCPlantID	INT		= NULL		-- added 3/15/17

			-- 6/30/21 - added for Control Chart
			, @FromCharts			BIT		= NULL
			, @AIMSourceID2			INT		= NULL
			, @AIMSourceLocationID2	INT		= NULL
			, @AIMSourceID3			INT		= NULL
			, @AIMSourceLocationID3	INT		= NULL
			, @AIMSourceID4			INT		= NULL
			, @AIMSourceLocationID4	INT		= NULL
			, @AIMOwnerID2			INT		= NULL
			, @AIMOwnerLocationID2	INT		= NULL
			, @AIMOwnerID3			INT		= NULL
			, @AIMOwnerLocationID3	INT		= NULL
			, @AIMOwnerID4			INT		= NULL
			, @AIMOwnerLocationID4	INT		= NULL
			, @SupplierOwnerOption  INT		= NULL		--9/8/21

			-- task 1035 8/10/26 cursor change - old version kept for reference:  one combined list parameter
			--, @ChartsSupplierOwnerPairList	VARCHAR(MAX)	= NULL

			-- task 1035 8/10/26 cursor change - supplier rows and owner rows from the Charts screen come in as two
			--									 SEPARATE delimited lists (any number of rows, replaces the fixed 4 params above).
			--									 Entries are matched by position for 'Both (Supplier and Owner)'.
			--									 Supplier list:  'SourceID,SourceLocationID;...'
			--									 Owner list:     'OwnerID,OwnerLocationID;...'
			, @ChartsSupplierPairList	VARCHAR(MAX)	= NULL
			, @ChartsOwnerPairList		VARCHAR(MAX)	= NULL


-- 7/27/17 - Changed "Proposed" to "Trimmed" and the original values to "Untrimmed".
--						ex:  "ProposedMean" is now "TrimmedMean" and
--							 "Mean"			is now "UntrimmedMean"
-- run grouped by date:  (3)
-- exec usp_Sample_SEL_Analysis 3, '1/1/2017 12:00:00 AM', '9/13/2017 12:00:00 AM', 21, False, False, 1, 0, 0, '', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 0, 0
-- exec usp_Sample_SEL_Analysis 10, '3/27/2017 12:00:00 AM', '10/13/2017 12:00:00 AM', 0, False, False, 1, 0, 0, '', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 0, 0

--exec usp_Sample_SEL_Analysis 2, '11/20/2022 11:47:53 AM', '12/19/2022 11:47:53 AM', 33, False, True, 1, 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, 1, 0, 0, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
--exec usp_Sample_SEL_Analysis 3, '1/1/2000 12:00:00 AM', '1/6/2023 12:00:00 AM',     33, True, False, 1, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, 1,    1, 1, 0, 0, TRUE,  0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3

AS
BEGIN

-- 6/14/21
--DECLARE @SQL				VARCHAR(6000) = ''
DECLARE @SQL				VARCHAR(MAX) = ''

DECLARE @Total				VARCHAR(1000) = ''

--DECLARE @Where			VARCHAR(1000) = ''
DECLARE @Where				VARCHAR(MAX) = ''

DECLARE @WhereValue			VARCHAR(1000) = ''
DECLARE @Group				VARCHAR(1000) = ''
DECLARE @CFCLocation		VARCHAR(1000) = ''
DECLARE @CFCLocationGroup	VARCHAR(1000) = ''
DECLARE @Location			VARCHAR(100) = ''
DECLARE @LocationStat		VARCHAR(100) = ''
DECLARE @SStat				VARCHAR(4) = ''
DECLARE @DStat				VARCHAR(4) = ''
DECLARE @ONStat				VARCHAR(60) = ''
DECLARE @QONStat			VARCHAR(60) = ''
DECLARE @S					VARCHAR(4) = ''
DECLARE @D					VARCHAR(4) = ''
DECLARE @ST					VARCHAR(5) = ''
DECLARE @ON					VARCHAR(60) = ''
DECLARE @SRON				VARCHAR(60) = ''
DECLARE @PON				VARCHAR(60) = ''
DECLARE @SON				VARCHAR(60) = ''
DECLARE @QON				VARCHAR(60) = ''
DECLARE @TWhere				VARCHAR(500) = ''
DECLARE @Outlier			VARCHAR(70) = ''
DECLARE @QueryFieldID		INT = 0
DECLARE @StrNutrValue		VARCHAR(1000) = ''
DECLARE @StrJoinNutrValue	VARCHAR(1000) = ''

--DECLARE @CFCWhere			VARCHAR(1000) = ''
--DECLARE @WhereNutrient	VARCHAR(1000) = ''
--DECLARE @MatNutWhere		VARCHAR(1000) = ''
DECLARE @CFCWhere			VARCHAR(MAX) = ''
DECLARE @WhereNutrient		VARCHAR(MAX) = ''
DECLARE @MatNutWhere		VARCHAR(MAX) = ''


-- 6/30/21 - added for Randy at Perdue
-- task 1035 8/10/26 cursor change - changed to MAX (the list version can hold any number of supplier/owner rows)
DECLARE @ChartsSupplierOwnerWhere		VARCHAR(MAX) = ''

SET NOCOUNT ON;

--1/20/21 - added AimOwnerLocationName to select statements

IF @SpecificCFCPlantID IS NULL
	BEGIN
		SET @SpecificCFCPlantID = 0
	END

-- Set T-WHERE string -------------------------------------------------------- 
IF @IsSubSet = 1
	BEGIN		
		Set @TWhere = ' WHERE '
		Set @TWhere += ' T.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar)
		Set @TWhere += ' and T.AIMNutrientID = ' + Cast(@AIMNutrientID as varchar)
		
		IF @SampleDate IS NOT NULL 
			Set @TWhere += ' and T.SampleDate = ''' + Cast(@SampleDate as varchar) + ''' '
		If @SampleID IS NOT NULL
			Set @Twhere += ' and T.SampleID = ' + Cast(@SampleID as varchar)		

		--6/30/21 - commented because, from Analysis screen, these are always 0 - 
		--			and from Charts screen, they are included in the [AnalysisReports_ChartsSupplierOwner_WhereStatement] function below
		--If @AIMOwnerID IS NOT NULL
		--	Set @Twhere += ' and T.AIMOwnerID = ' + Cast(@AIMOwnerID as varchar)
		--If @AIMOwnerLocationID IS NOT NULL
		--	Set @Twhere += ' and T.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar)
	END

-----------------------------------------------------------------------------
-- 6/30/21 - added for Randy at Perdue
IF @FromCharts = 1
	BEGIN		
		IF @AIMSourceID IS NULL	SET @AIMSourceID = 0
		IF @AIMSourceID2 IS NULL SET @AIMSourceID2 = 0
		IF @AIMSourceID3 IS NULL SET @AIMSourceID3 = 0
		IF @AIMSourceID4 IS NULL SET @AIMSourceID4 = 0
		IF @AIMSourceLocationID IS NULL SET @AIMSourceLocationID = 0
		IF @AIMSourceLocationID2 IS NULL SET @AIMSourceLocationID2 = 0
		IF @AIMSourceLocationID3 IS NULL SET @AIMSourceLocationID3 = 0
		IF @AIMSourceLocationID4 IS NULL SET @AIMSourceLocationID4 = 0
		IF @AIMOwnerID IS NULL SET @AIMOwnerID = 0
		IF @AIMOwnerID2 IS NULL SET @AIMOwnerID2 = 0
		IF @AIMOwnerID3 IS NULL SET @AIMOwnerID3 = 0
		IF @AIMOwnerID4 IS NULL SET @AIMOwnerID4 = 0
		IF @AIMOwnerLocationID IS NULL SET @AIMOwnerLocationID = 0
		IF @AIMOwnerLocationID2 IS NULL SET @AIMOwnerLocationID2 = 0
		IF @AIMOwnerLocationID3 IS NULL SET @AIMOwnerLocationID3 = 0
		IF @AIMOwnerLocationID4 IS NULL SET @AIMOwnerLocationID4 = 0

		-- task 1035 8/10/26 cursor change - old version kept for reference:  one combined list
		--IF @ChartsSupplierOwnerPairList IS NOT NULL AND LEN(@ChartsSupplierOwnerPairList) > 0
		--	EXEC @ChartsSupplierOwnerWhere = [dbo].[AnalysisReports_ChartsSupplierOwnerList_WhereStatement] @ChartsSupplierOwnerPairList

		-- task 1035 8/10/26 cursor change - when the Charts screen sends the supplier and/or owner list, use them
		--									 (two separate lists, matched by entry position);
		--									 otherwise fall back to the original fixed 4-slot parameters
		IF (@ChartsSupplierPairList IS NOT NULL AND LEN(@ChartsSupplierPairList) > 0) OR (@ChartsOwnerPairList IS NOT NULL AND LEN(@ChartsOwnerPairList) > 0)
			EXEC @ChartsSupplierOwnerWhere = [dbo].[AnalysisReports_ChartsSupplierOwnerList_WhereStatement] @ChartsSupplierPairList, @ChartsOwnerPairList
		ELSE IF @AIMSourceID > 0 or @AIMSourceID2 > 0 or @AIMSourceID3 > 0 or @AIMSourceID4 > 0 or @AIMOwnerID > 0 or @AIMOwnerID2 > 0 or @AIMOwnerID3 > 0 or @AIMOwnerID4 > 0
			EXEC @ChartsSupplierOwnerWhere = [dbo].[AnalysisReports_ChartsSupplierOwner_WhereStatement] @AIMSourceID, @AIMSourceID2, @AIMSourceID3, @AIMSourceID4, @AIMSourceLocationID, @AIMSourceLocationID2, @AIMSourceLocationID3, @AIMSourceLocationID4, @AIMOwnerID, @AIMOwnerID2, @AIMOwnerID3, @AIMOwnerID4, @AIMOwnerLocationID, @AIMOwnerLocationID2, @AIMOwnerLocationID3, @AIMOwnerLocationID4, @SupplierOwnerOption

	END

-----------------------------------------------------------------------------
-- This is used when doing overlay location 
IF @IsDefaultLocation = 1
	BEGIN
		SET @Location = 'AIMOwnerLocationID '
		SET @LocationStat = 'AIMOwnerLocationID '
		SET @SStat = ', S.'
		SET @DStat = ', D.'
		SET @ONStat = ' AND D.AIMOwnerLocationID = S.AIMOwnerLocationID '
		SET @QONStat = ' AND Q.AIMOwnerLocationID = ST.AIMOwnerLocationID '
		SET @S = ', S.'
		SET @D = ', D.'
		SET @ST = ', ST.'
		SET @ON = ' AND D.AIMOwnerLocationID = S.AIMOwnerLocationID '
		SET @SRON = ' AND SR.AIMOwnerLocationID = T.AIMOwnerLocationID '
		SET @PON = ' AND P.AIMOwnerLocationID = T.AIMOwnerLocationID '
		SET @SON = ' AND ST.AIMOwnerLocationID = S.AIMOwnerLocationID '
		SET @QON = ' AND Q.AIMOwnerLocationID = ST.AIMOwnerLocationID '
		SET @StrNutrValue = 'ISNULL(ISNULL(CINO.NutrientValue, CIN.Value), 0) AS CurrentNutrientVal'
		SET @StrNutrValue += ', CASE WHEN CINO.NutrientValue IS NULL THEN 0 ELSE 1 END AS IsOverlay '
		SET @StrJoinNutrValue = ' LEFT OUTER JOIN 
			dbo.CFCIngredientNutrientOverlay CINO
				ON	CINO.CFCPlantID = T.CFCPlantID 
				AND CINO.CFCIngredientID = T.CFCIngredientID 
				AND	CINO.CFCNutrientID = T.CFCNutrientID '
	END
ELSE
	BEGIN
		IF @SpecificCFCPlantID = 0
			BEGIN
				SET @StrNutrValue = 'ISNULL(CIN.Value, 0) AS CurrentNutrientVal, 0 AS IsOverlay '
			END
		ELSE
			BEGIN
				SELECT 
					  CINO.CFCIngredientID
					, CINO.CFCNutrientID
					, CINO.NutrientValue 
				INTO ##SpecificCFCIngredientNutrient
				FROM
					dbo.CFCIngredientNutrientOverlay CINO
				WHERE 
					CINO.CFCPlantID = @SpecificCFCPlantID 
				SET @StrJoinNutrValue = ' LEFT OUTER JOIN 
					##SpecificCFCIngredientNutrient SCIN
						ON SCIN.CFCIngredientID = T.CFCIngredientID 
						AND	SCIN.CFCNutrientID = T.CFCNutrientID '
				SET @StrNutrValue = 'ISNULL(ISNULL(SCIN.NutrientValue, CIN.Value), 0) AS CurrentNutrientVal'
				SET @StrNutrValue += ', CASE WHEN SCIN.NutrientValue IS NULL THEN 0 ELSE 1 END AS IsOverlay '
			END
	END

-----------------------------------------------------------------------------
--- If there is no query, only the date is used on the where statement


IF @QueryTemplateID = 0
		SET @Where = 'AND S.SampleDate BETWEEN ''' + CAST(@StartDate AS VARCHAR) + ''' AND ''' + CAST(@EndDate AS VARCHAR) + ''' '

-----------------------------------------------------------------------------
---	This creates the where statement that the query will be using
ELSE
	BEGIN		
		-- 9/12/17 - These functions create the where statements the query will use
		EXEC @Where = [dbo].[AnalysisReports_WhereStatement] @QueryTemplateID 		
		EXEC @CFCWhere = [dbo].[AnalysisReports_CFCIngredient_WhereStatement] @QueryTemplateID		
		EXEC @MatNutWhere = [dbo].[AnalysisReports_MaterialNutrient_WhereStatement] @QueryTemplateID 	
		IF LEN(@MatNutWhere) > 0
			SET @Where += @MatNutWhere 	
					
	END

-----------------------------------------------------------------------------
--- This is used to get the numbers for each grouping
--  1 = NoGroup
--  2 = SampleID
--  3 = SampleDate
--  4 = SampleDate-Owner
--  5 = SampleDate-Owner-Source
--  6 = SampleDate-Owner-SourceLocation
--  7 = SampleDate-OwnerLocation
--  8 = SampleDate-OwnerLocation-Source
--  9 = SampleDate-OwnerLocation-SourceLocation
-- 10 = SampleDate-Source
-- 11 = SampleDate-SourceLocation
-- 12 = Owner
-- 13 = Owner-Source
-- 14 = Owner-SourceLocation
-- 15 = OwnerLocation
-- 16 = OwnerLocation-Source
-- 17 = OwnerLocation-SourceLocation
-- 18 = Source
-- 19 = SourceLocation

SET @Group = 
	CASE @AnalysisQuery 
		WHEN 1 THEN  ''
		WHEN 2 THEN  ', S.SampleID, S.SampleDate, AO.OwnerCode, AO.AIMOwnerID, S.AIMOwnerLocationID, AOL.LocationCode, AOL.LocationName, SS.AIMSourceID, SS.SourceName, ASL.AIMSourceLocationID, ASL.SourceLocationName, S.SampleCode, S.SampleAIMID, SS.AIMSupplierCode, ASL.AIMSupplierLocationCode '
		WHEN 3 THEN  ', S.SampleDate '
		WHEN 4 THEN	 ', S.SampleDate, AO.OwnerCode '
		WHEN 5 THEN  ', S.SampleDate, AO.OwnerCode, SS.AIMSupplierCode, SS.SourceName '
		WHEN 6 THEN  ', S.SampleDate, AO.OwnerCode, SS.AIMSupplierCode, SS.SourceName, ASL.AIMSupplierLocationCode, ASL.SourceLocationName '			
		WHEN 7 THEN  ', S.SampleDate, AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, AOL.LocationName '
		WHEN 8 THEN  ', S.SampleDate, AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, AOL.LocationName, SS.SourceName '
		WHEN 9 THEN  ', S.SampleDate, AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, AOL.LocationName, SS.AIMSupplierCode, SS.SourceName, ASL.AIMSupplierLocationCode, ASL.SourceLocationName '
		WHEN 10 THEN ', S.SampleDate, SS.AIMSupplierCode, SS.SourceName '			
		WHEN 11 THEN ', S.SampleDate, SS.AIMSupplierCode, SS.SourceName, ASL.AIMSupplierLocationCode, ASL.SourceLocationName '
		WHEN 12 THEN ', AO.OwnerCode '
		WHEN 13 THEN ', AO.OwnerCode, SS.AIMSupplierCode, SS.SourceName '
		WHEN 14 THEN ', AO.OwnerCode, SS.AIMSupplierCode, SS.SourceName, ASL.AIMSupplierLocationCode, ASL.SourceLocationName '
		WHEN 15 THEN ', AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, AOL.LocationName '
		WHEN 16 THEN ', AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, AOL.LocationName, SS.AIMSupplierCode, SS.SourceName '
		WHEN 17 THEN ', AO.OwnerCode, S.AIMOwnerLocationID, AOL.LocationCode, AOL.LocationName, SS.AIMSupplierCode, SS.SourceName, ASL.AIMSupplierLocationCode, ASL.SourceLocationName '
		WHEN 18 THEN ', SS.AIMSupplierCode, SS.SourceName '
		WHEN 19 THEN ', SS.AIMSupplierCode, SS.SourceName, ASL.AIMSupplierLocationCode, ASL.SourceLocationName '
	END

-----------------------------------------------------------------------------
--This is used to get the final grouping 
SET @Total =  
	CASE @AnalysisQuery 
		WHEN 1 THEN  ''
		WHEN 2 THEN  ', T.SampleID, T.SampleDate, T.OwnerCode, T.AIMOwnerID, T.AIMOwnerLocationID, T.LocationCode, T.LocationName, T.AIMSourceID, T.SourceName, T.AIMSourceLocationID, T.SourceLocationName, T.SampleCode, T.SampleAIMID, T.AIMSupplierCode, T.AIMSupplierLocationCode '		     
		WHEN 3 THEN  ', T.SampleDate '
		WHEN 4 THEN  ', T.SampleDate, T.OwnerCode '
		WHEN 5 THEN  ', T.SampleDate, T.OwnerCode, T.AIMSupplierCode, T.SourceName '
		WHEN 6 THEN  ', T.SampleDate, T.OwnerCode, T.AIMSupplierCode, T.SourceName, T.AIMSupplierLocationCode, T.SourceLocationName '
		WHEN 7 THEN  ', T.SampleDate, T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.LocationName '
		WHEN 8 THEN  ', T.SampleDate, T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.AIMSupplierCode, T.SourceName '
		WHEN 9 THEN  ', T.SampleDate, T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.LocationName, T.AIMSupplierCode, T.SourceName, T.AIMSupplierLocationCode, T.SourceLocationName '
		WHEN 10 THEN ', T.SampleDate, T.AIMSupplierCode, T.SourceName '
		WHEN 11 THEN ', T.SampleDate, T.AIMSupplierCode, T.SourceName, T.AIMSupplierLocationCode, T.SourceLocationName '
		WHEN 12 THEN ', T.OwnerCode '
		WHEN 13 THEN ', T.OwnerCode, T.SourceName '
		WHEN 14 THEN ', T.OwnerCode, T.AIMSupplierCode, T.SourceName, T.AIMSupplierLocationCode, T.SourceLocationName '
		WHEN 15 THEN ', T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.LocationName'
		WHEN 16 THEN ', T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.LocationName, T.AIMSupplierCode, T.SourceName '
		WHEN 17 THEN ', T.OwnerCode, T.AIMOwnerLocationID, T.LocationCode, T.LocationName, T.AIMSupplierCode, T.SourceName, T.AIMSupplierLocationCode, T.SourceLocationName '
		WHEN 18 THEN ', T.AIMSupplierCode, T.SourceName '
		WHEN 19 THEN ', T.AIMSupplierCode, T.SourceName, T.AIMSupplierLocationCode, T.SourceLocationName '
	END

-----------------------------------------------------------------------------
IF @AnalysisQuery IN (2, 7, 8, 9, 15, 16, 17) AND @SpecificCFCPlantID = 0		-- -- These AnalysisQueries include the OwnerLocation
	BEGIN
		SET @CFCLocation = ', T.CFCPlantID, T.CFCPlantCode, T.CFCPlantName '
		SET @CFCLocationGroup = ', CP.CFCPlantID, CP.CFCPlantCode, CP.CFCPlantName '
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
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, AN.AIMNutrientCode
	, AN.AIMNutrientName
	, AN.MathTypeID
	, S.SampleTypeID
'
if @AnalysisQuery = 2
	BEGIN
		SET @SQL += ' , case when DST.Name is null then '''' else DST.Name end as DataSourceTypeName '
		SET @SQL += ' , S.LotNumber '												--Task1147 DB260420
		SET @SQL += ' , ISNULL(ST.SampleTypeName, '''') AS SampleTypeName '			--Task1147 DB260421
		SET @SQL += ' , WL.Min AS WLMin '											--Task1147 DB260422
		SET @SQL += ' , WL.Max AS WLMax '											--Task1147 DB260422
	END
SET @SQL += '	
INTO ##Sample
FROM
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
			AND AM.MaterialTypeID = 1
	INNER JOIN 
		dbo.AIMNutrient AN 
			ON	AN.AIMNutrientID = SA.AIMNutrientID  
			AND AN.IsActive = 1 '


-----------------------------------------------------------------------------
-- 3/16/20 add link to AnalyzerNutrient and DataSourceType. Smithfield wants the option to see the datasource on the Analysis grid.
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
		SET @SQL += '					
			LEFT OUTER JOIN
					dbo.SampleType ST
					ON ST.SampleTypeID = S.SampleTypeID '		--Task1147 DB260421
		SET @SQL += '
			LEFT OUTER JOIN
					dbo.AIMNutrientRefAnalyzerNutrient ANX 
					ON ANut.AnalyzerNutrientID = ANX.AnalyzerNutrientID	
			LEFT OUTER JOIN			
					dbo.WarningLimits WL
					ON WL.AIMNutrientID = ANX.AIMNutrientID
					AND WL.AIMMaterialID = S.AIMMaterialID '	--Task1147 DB260422
					
	End

SET @SQL += '
WHERE
	S.IsActive = 1 ' + @Where 
EXEC(@SQL)
--print @sql


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
INTO ##Statis1
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

	FROM      
		##Sample S
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
				FROM      
					##Sample S
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
					FROM
						##Sample S
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
EXEC (@SQL)
--print @SQL
----goto continu

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
	, S.AIMMaterialCode
	, S.AIMMaterialName
	, S.AIMNutrientCode
	, S.AIMNutrientName
	, S.MathTypeID '				

	if @AnalysisQuery = 2
		BEGIN
			SET @SQL += ', S.DataSourceTypeName '
			SET @SQL += ', S.LotNumber '				--Task1147 DB260420
			SET @SQL += ' , S.SampleTypeID '			--Task1147 DB260421
			SET @SQL += ' , S.SampleTypeName ' 			--Task1147 DB260421
			SET @SQL += ' , S.WLMin '						--Task1147 DB260422
			SET @SQL += ' , S.WLMax ' 					--Task1147 DB260422
		END
	--6/24/20
    --SET @SQL += '
	--, CASE WHEN ST.LMin > S.AIMUsedValue THEN			1		ELSE			0		END AS LMin
	--, CASE WHEN ST.LMax > S.AIMUsedValue THEN			1		ELSE			0		END AS LMax
	--, CASE WHEN S.AIMUsedValue > ST.UMin THEN			1		ELSE			0		END AS UMin
	--, CASE WHEN S.AIMUsedValue > ST.UMax THEN			1		ELSE			0		END AS UMax	
	SET @SQL += '	
	, CASE When S.AimUsedValue < 0 Then Case When ST.LMin < S.AIMUsedValue Then 1 Else 0 End else Case When St.LMin > S.AimUsedValue Then 1 else 0 end END as LMin
	, CASE When S.AimUsedValue < 0 Then Case When St.LMax < S.AimUsedValue Then 1 Else 0 End else Case When St.LMax > S.AimUsedValue Then 1 else 0 end END as LMax
	, CASE When S.AimUsedValue < 0 Then Case When S.AIMUsedValue < ST.UMin Then 1 Else 0 End else Case When S.AIMUsedValue > ST.UMin Then 1 else 0 end END as UMin
	, CASE When S.AimUsedValue < 0 Then Case When S.AIMUsedValue < ST.UMax Then 1 Else 0 End else Case When S.AIMUsedValue > ST.UMax Then 1 else 0 end END as UMax	
	
INTO ##Statis2
FROM 
	##Sample S
	LEFT OUTER JOIN	
		##Statis1 ST 
			ON	ST.AIMMaterialID = S.AIMMaterialID ' + @SON +
			'AND ST.AIMNutrientID = S.AIMNutrientID'
EXEC (@SQL)
--print @sql
----goto continu

-- should be in the clear before this



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

----print @SQL
----goto continu
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
-- Sample Row Number
SET @SQL = 'SELECT 
		S.AIMUsedValue 
		, S.AIMMaterialID' + @S + @Location + '
		, S.AIMNutrientID 
		, ROW_NUMBER() 
		OVER (PARTITION BY S.AIMMaterialID' + ', S.AIMNutrientID
		ORDER BY S.AIMUsedValue) AS RowNum
	INTO ##SampleRowNumber
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
			SARCU.SampleAIMRefCFCUserID IS NULL ' + @Where 
EXEC (@SQL)
--print @Sql

-----------------------------------------------------------------------------
--This gets the numbers for the PROPOSED math   Any changes have to be made to the ##Statis1 table

--9/8/17 -- added "AND S.AIMNutrientID = SA.AIMNutrientID" to join of SampleAIM table below.
--			Perdue data somehow calulates exponential standard dev of 3.76972873230979E-08 
--			on SampleID 53511, AIMMaterialID 947, AIMNutrientID 102, SampleResultID 271862, AIMUsedValue = 3.15

--10/2/20 -- added TSS join and TSS.TotalSampleSize to select stmt (per Nickki)
--12/19/22 -- added join to SampleResult in ST below, to get only Active sample results (SR.IsActive = 1)

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
INTO ##Proposed
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
	FROM      
		##Sample S
		LEFT OUTER JOIN 
			dbo.SampleResultPredict SRP WITH(NOLOCK)
				ON SRP.SampleResultID = S.SampleResultID				
		INNER JOIN 
			dbo.SampleAIM SA WITH(NOLOCK) 
				ON S.SampleID = SA.SampleID
				AND S.AIMNutrientID = SA.AIMNutrientID
		INNER JOIN
			dbo.SampleResult SR
				ON SA.SampleResultID = SR.SampleResultID
		LEFT JOIN 
			dbo.SampleAIMRefCFCUser SARCU WITH(NOLOCK)
				ON SARCU.SampleAIMID = S.SampleAIMID
				AND SARCU.CFCUserID	= ' + CAST(@CFCUserID AS VARCHAR) + '
	WHERE
			S.IsActive = 1 and SR.IsActive = 1 
		AND SARCU.SampleAIMRefCFCUserID IS NULL ' + @Where + 
	'GROUP BY 
		   S.AIMMaterialID' + @S + @Location +
		', S.AIMNutrientID 
	) ST

	INNER JOIN
		(SELECT S.AIMMaterialID, S.AIMNutrientID, COUNT(*) AS TotalSampleSize
		FROM ##Sample S					
		INNER JOIN dbo.SampleAIM SA WITH(NOLOCK) 
			ON S.SampleID = SA.SampleID	AND S.AIMNutrientID = SA.AIMNutrientID		
		INNER JOIN dbo.SampleResult SR
			ON SA.SampleResultID = SR.SampleResultID
		WHERE S.IsActive = 1 and SR.IsActive = 1' + @Where + '
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
				##SampleRowNumber D 
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
----print @sql
----goto continu

-----------------------------------------------------------------------------
--This gets the final query - and will have different columns based on the grouping
SET @SQL = 'SELECT
	  CASE WHEN T.IncludedSample = 0 THEN 0 ELSE 1 END AS IsInclude
	, ISNULL(SR.SampleSize, 1)  AS SampleSize	
	, ISNULL(P.TrimmedSampleSize,1) AS TrimmedSampleSize
	, ISNULL(P.TotalSampleSize,1) AS TotalSampleSize
	, T.CFCIngredientName
	, T.SampleValue
	, T.CFCIngredientCode
	, T.Unit
	, T.AIMMaterialID 
	, T.AIMMaterialCode 
	, T.AIMMaterialName 
	, T.AIMNutrientID
	, T.AIMNutrientCode 
	, T.AIMNutrientName 
	, T.MathTypeID '

	if @AnalysisQuery = 2
		BEGIN
			SET @SQL += ', T.DataSourceTypeName '		
			SET @SQL += ', T.LotNumber '				--Task1147 DB260420
			SET @SQL += ', T.SampleTypeID '				--Task1147 DB260421
			SET @SQL += ', T.SampleTypeName'			--Task1147 DB260421
			SET @SQL += ', T.WLMin '						--Task1147 DB260422
			SET @SQL += ', T.WLMax ' 						--Task1147 DB260422
			SET @SQL += ', ISNULL(CASE WHEN T.SampleValue < T.WLMin THEN 1 ELSE CASE WHEN T.SampleValue > T.WLMax THEN 2 ELSE 0 END END, 0) AS ResultValueFlag '
		END
	SET @SQL += '
	, T.CFCNutrientCode
	, T.CFCNutrientName
	, T.NumberOfSample
	, T.CFCIngredientID	
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
	, ' + @StrNutrValue + @Total + @CFCLocation +
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
		, S.MathTypeID '

		if @AnalysisQuery = 2
			BEGIN
				SET @SQL += ', S.DataSourceTypeName '		
				SET @SQL += ', S.LotNumber '				--Task1147 DB260420
				SET @SQL += ', S.SampleTypeID '				--Task1147 DB260421
				SET @SQL += ', S.SampleTypeName '			--Task1147 DB260421
				SET @SQL += ', S.WLMin '						--Task1147 DB260422
				SET @SQL += ', S.WLMax ' 						--Task1147 DB260422
			END
		SET @SQL += '

		, CI.CFCIngredientName
		, CI.CFCIngredientID
		, CI.CFCIngredientCode
		, CN.Unit
		, CN.CFCNutrientID
		, CN.CFCNutrientCode
		, CN.CFCNutrientName	
		, AVG(S.AIMUsedValue) AS SampleValue
		, SUM(S.LMin) AS LMin
		, SUM(S.LMax) AS LMax
		, SUM(S.UMin) AS UMin
		, SUM(S.UMax) AS UMax
		, SUM(CASE WHEN SARCU.SampleAIMID IS NULL THEN 1 ELSE 0 END) AS IncludedSample
		, COUNT(*) AS NumberOfSample ' + @Group + @CFCLocationGroup +    
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
				AND AOL.IsActive = 1 AND AOL.IsFormulationPlant = 1 
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
	WHERE 
			S.IsActive = 1 			
			AND AOL.IsFormulationPlant = 1  ' + @CFCWhere 

			-- 6/30/21 - added for Randy at Perdue
			If @ChartsSupplierOwnerWhere > ''
				SET @SQL += @ChartsSupplierOwnerWhere 			
			
			SET @SQL += 
	' 
	GROUP BY
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
			, S.MathTypeID '

			IF @AnalysisQuery = 2
				BEGIN
					SET @SQL += ', S.DataSourceTypeName '		
					SET @SQL += ', S.LotNumber '				--Task1147 DB260420
					SET @SQL += ', S.SampleTypeID '				--Task1147 DB260421
					SET @SQL += ', S.SampleTypeName'			--Task1147 DB260421
					SET @SQL += ', S.WLMin '						--Task1147 DB260422
					SET @SQL += ', S.WLMax ' 						--Task1147 DB260422
				END

			SET @SQL += '

			, CN.Unit
			, CI.CFCIngredientCode
			, CN.CFCNutrientID
			, CN.CFCNutrientCode					
			, CN.CFCNutrientName ' + @Group + @CFCLocationGroup +
		' ) T
	LEFT OUTER JOIN 
		dbo.CFCIngredientNutrient CIN
			ON	CIN.CFCIngredientID = T.CFCIngredientID 
			AND	CIN.CFCNutrientID = T.CFCNutrientID 	
	LEFT OUTER JOIN	
		##Statis1 SR 
			ON	SR.AIMMaterialID = T.AIMMaterialID ' + @SRON +
'			AND SR.AIMNutrientID = T.AIMNutrientID   
	LEFT OUTER JOIN	
		##Proposed P
			ON	P.AIMMaterialID = T.AIMMaterialID ' + @PON + 
'			AND P.AIMNutrientID = T.AIMNutrientID '  
+ @StrJoinNutrValue + @TWhere + 
' 
ORDER BY 
	  T.AIMMaterialID
	, T.AIMNutrientID ' + @Total

-----print @CFCWhere

EXEC (@SQL)
--print @SQL
----GOTO CONTINU

IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##Sample%')
	DROP TABLE ##Sample
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##Statis1%')
	DROP TABLE ##Statis1
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##Proposed%')
	DROP TABLE ##Proposed
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##Statis2%')
	DROP TABLE ##Statis2
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##CFCPlant%')
	DROP TABLE ##CFCPlant
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##CFCIngredient%')
	DROP TABLE ##CFCIngredient
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##CFCNutrient%')
	DROP TABLE ##CFCNutrient
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##SampleRowNumber%')
	DROP TABLE ##SampleRowNumber
IF EXISTS(SELECT [name] FROM tempdb.sys.tables WHERE [name] like '##SpecificCFCIngredientNutrient%')
	DROP TABLE ##SpecificCFCIngredientNutrient

continu:
END