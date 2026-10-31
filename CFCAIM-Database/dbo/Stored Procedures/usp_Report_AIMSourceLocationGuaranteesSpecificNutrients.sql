
CREATE PROCEDURE [dbo].[usp_Report_AIMSourceLocationGuaranteesSpecificNutrients]	
	  @ToDate					DATE		= NULL
	, @FromDate					DATE		= NULL
	, @AimMaterialID			INT			= NULL
	, @Source					INT			= NULL
	, @SourceLocation			INT			= NULL
	, @Flag						INT			= NULL
	, @AIMNutrientID1			INT			= NULL
	, @AIMNutrientID2			INT			= NULL

--exec usp_Report_AIMSourceLocationGuaranteesSpecificNutrients '2016-10-26 00:00:00', '2016-10-25 23:59:59', 11, null, null, 1, 3, 20

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL			VARCHAR(6000) = ''
DECLARE @Where			VARCHAR(1000) = ''


SET @Where = 'WHERE S.IsActive = 1 and ASLG.IsActive = 1 '
if @FromDate IS NOT NULL
	SET @Where += ' AND S.SampleDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' '
if @ToDate IS NOT NULL
	SET @Where += ' AND S.SampleDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' '
if @AimMaterialID IS NOT NULL
	SET @Where += ' AND S.AimMaterialID = ' + Convert(varchar,@AimMaterialID) + ' '
if @Source IS NOT NULL
	SET @Where += ' AND S.AimSourceID = ' + Convert(varchar,@Source) + ' '
if @SourceLocation IS NOT NULL
	Begin

		--8/5/20
		if @SourceLocation > 0 
			SET @Where += ' AND S.AimSourceLocationID = ' + Convert(varchar,@SourceLocation) + ' '
	End
if @Flag IS NOT NULL
	SET @Where += ' AND CASE WHEN (X.ResultValue < X.Min OR (X.ResultValue > X.Max AND X.Max != 0))  or 	
			                     (X2.ResultValue < X2.Min OR (X2.ResultValue > X2.Max AND X2.Max != 0)) THEN 1 ELSE 0 END = 1 '
SET @SQL = 'SELECT DISTINCT '
If @ToDate is NOT NULL
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, '
If @FromDate IS NOT NULL
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '

SET @SQL += '	
	S.SampleID
	, S.SampleDate		
	, S.AIMMaterialID	
	, S.AimSourceID
	, S.AimSourceLocationID
	, AM.AIMMaterialName
	, AMS.SourceName
	, ASL.SourceLocationName	
	, S.LoadNumber
	, S.BillOfLoad
	, CASE WHEN (X.ResultValue < X.Min OR (X.ResultValue > X.Max AND X.Max != 0))  AND 	
			                     (X2.ResultValue < X2.Min OR (X2.ResultValue > X2.Max AND X2.Max != 0)) THEN 1 ELSE 0 END AS Flag

	, 0 as AnalyzerNutrientID
	, 0 as AimUsedValue
	, 0 as Min
	, 0 as Max
	, '' '' as AimNutrientName
	, 0 as AimNutrientID
	, 0 as SampleResultID
	, 0 as ResultValue	
	
	, (Select AIMNutrientName from dbo.AIMNutrient where AIMNutrientID = ' + Convert(varchar,@AIMNutrientID1) + ') as NutrientName1
	, (Select AIMNutrientName from dbo.AIMNutrient where AIMNutrientID = ' + Convert(varchar,@AIMNutrientID2) + ') as NutrientName2
	, X.ResultValue as ResultValue1
	, X2.ResultValue as ResultValue2
	, X.AIMUsedValue as AIMUsedValue1		
	, X2.AIMUsedValue as AIMUsedValue2
	, CASE WHEN (X.ResultValue < X.Min OR (X.ResultValue > X.Max AND X.Max != 0)) THEN 1 ELSE 0 END AS Flag1
	, CASE WHEN (X2.ResultValue < X2.Min OR (X2.ResultValue > X2.Max AND X2.Max != 0)) THEN 1 ELSE 0 END AS Flag2	

	, AOL.LocationCode as OwnerLocationCode
	, AOL.LocationName as OwnerLocationName
	
FROM
	dbo.Sample S with (nolock)
INNER JOIN 
	dbo.SampleAIM SA with (nolock)
		ON SA.SampleID = S.SampleID			
INNER JOIN
	dbo.AIMSourceLocationGuarantee ASLG with (nolock)
		ON S.AimSourceID = ASLG.AIMSourceID 
		AND S.AIMSourceLocationID = ASLG.AIMSourceLocationID '

		-- 8/5/20
		if @SourceLocation IS NOT NULL
			Begin
			if @SourceLocation > 0 
				SET @Sql += ' AND S.AIMSourceLocationID = ASLG.AIMSourceLocationID '
			End

		SET @Sql += '

		AND S.AIMMaterialID = ASLG.AIMMaterialID
		AND SA.AIMNutrientID = ASLG.AIMNutrientID
LEFT JOIN 
	dbo.AIMSource AMS with (nolock) on S.AIMSourceID = AMS.AIMSourceID
LEFT JOIN
	dbo.AIMSourceLocation ASL with (nolock) on S.AIMSourceLocationID = ASL.AIMSourceLocationID
LEFT JOIN
	dbo.AIMMaterial AM with (nolock) on S.AIMMaterialID = AM.AIMMaterialID
LEFT JOIN
	dbo.AIMOwnerLocation AOL with (nolock) on S.AIMOwnerLocationID = AOL.AIMOwnerLocationID


LEFT JOIN
	 (Select Sam.SampleID, SR.ResultValue, AIMUsedValue, [Min], [Max]
		FROM
			dbo.Sample Sam with (nolock)
			INNER JOIN dbo.SampleAIM SA with (nolock) ON SA.SampleID = Sam.SampleID			
			INNER JOIN dbo.SampleResult SR with (nolock) ON SR.SampleResultID = SA.SampleResultID	
			INNER JOIN dbo.AIMSourceLocationGuarantee ASLG with (nolock)
				ON Sam.AimSourceID = ASLG.AIMSourceID  '

				-- 8/5/20
				if @SourceLocation IS NOT NULL
					Begin
					if @SourceLocation > 0 
						SET @Sql += ' AND S.AIMSourceLocationID = ASLG.AIMSourceLocationID '
					End

				SET @Sql += '
								
				AND Sam.AIMMaterialID = ASLG.AIMMaterialID
				AND SA.AIMNutrientID = ASLG.AIMNutrientID
			LEFT JOIN dbo.AIMNutrient AN with (nolock) on SA.AIMNutrientID = AN.AIMNutrientID
		WHERE AN.AIMNUTRIENTID = ' + Convert(varchar,@AIMNutrientID1) + '					
		) X on S.SampleID = X.SampleID

LEFT JOIN
	 (Select Sam.SampleID, SR.ResultValue, AIMUsedValue, [Min], [Max]
		FROM
			dbo.Sample Sam with (nolock)
			INNER JOIN dbo.SampleAIM SA with (nolock) ON SA.SampleID = Sam.SampleID			
			INNER JOIN dbo.SampleResult SR with (nolock) ON SR.SampleResultID = SA.SampleResultID	
			INNER JOIN dbo.AIMSourceLocationGuarantee ASLG with (nolock)
				ON Sam.AimSourceID = ASLG.AIMSourceID '

				-- 8/5/20
				if @SourceLocation IS NOT NULL
					Begin
					if @SourceLocation > 0 
						SET @Sql += ' AND S.AIMSourceLocationID = ASLG.AIMSourceLocationID '
					End

				SET @Sql += '

				AND Sam.AIMMaterialID = ASLG.AIMMaterialID
				AND SA.AIMNutrientID = ASLG.AIMNutrientID
			LEFT JOIN dbo.AIMNutrient AN with (nolock) on SA.AIMNutrientID = AN.AIMNutrientID
		WHERE AN.AIMNUTRIENTID = ' + Convert(varchar,@AIMNutrientID2) + '					
		) X2 on S.SampleID = X2.SampleID


'
 + @Where 

 
EXEC (@SQL)
--print @sql


END