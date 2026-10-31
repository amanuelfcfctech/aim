CREATE PROCEDURE [dbo].[usp_Report_AIMSourceLocationGuarantees]	
	  @ToDate					DATE			= NULL
	, @FromDate					DATE			= NULL
	, @AimMaterialID			INT				= NULL
	, @Source					INT				= NULL
	, @SourceLocation			INT				= NULL	
	, @Flag						INT				= NULL
	, @AdjustedOrResult			VARCHAR(1)	    = NULL
	
AS
BEGIN
SET NOCOUNT ON;

--10/17/23
--DECLARE @SQL			VARCHAR(6000) = ''
--DECLARE @Where		VARCHAR(1000) = ''
DECLARE @SQL			VARCHAR(MAX) = ''
DECLARE @Where			VARCHAR(MAX) = ''

--6/14/23 - added case statement to join of AIMSourceLocationGuarantee table.
--4/20/21 - added AIMNutrientCode to Select statement (so report can be sorted by nutrient code or name)

--exec usp_Report_AIMSourceLocationGuarantees '5/11/2020 12:00:00 AM', '5/11/2020 12:00:00 AM', 0, 0, 0, 1, 'R'
--	exec usp_Report_AIMSourceLocationGuarantees @ToDate = '8/24/2023 12:00:00 AM', @FromDate='1/01/2020 12:00:00 AM', @Flag=1, @AdjustedOrResult='R'

---------------------------------------------
-- The adjusted moisture on the report is calculated in the Entity:

-- EX:  
-- 15.574 - Protein result
-- 12.432 - Moisture for (for the sample)
-- 13 -     Adjusted moisture for Protein (coming from maintenace) 
--
-- Calculation  = 100 - 12.432 = 87.5678
--                100 - 13     = 87
--				  87 / 87.5678 = .993518377 (dry matter)
--
-- Final result:  15.574 * .993518377 = 16.5678
---------------------------------------------


SET @Where = 'WHERE AM.MaterialTypeID = 1 and S.IsActive = 1 and ASLG.IsActive = 1 '
if @FromDate IS NOT NULL
	SET @Where += ' AND S.SampleDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' '
if @ToDate IS NOT NULL
	SET @Where += ' AND S.SampleDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' '
if @AimMaterialID IS NOT NULL and @AimMaterialID > 0
	SET @Where += ' AND S.AimMaterialID = ' + Convert(varchar,@AimMaterialID) + ' '
if @Source IS NOT NULL and @Source > 0
	SET @Where += ' AND S.AimSourceID = ' + Convert(varchar,@Source) + ' '

if @SourceLocation IS NOT NULL
	Begin	
	if @SourceLocation > 0 			
		SET @Where += ' AND S.AimSourceLocationID = ' + Convert(varchar,@SourceLocation) + ' '	
	End

--SET @Where += ' AND CASE WHEN SR.ResultValue > ASLG.[Min] OR (SR.ResultValue > ASLG.[Max] AND ASLG.[Max] != 0) THEN	1 ELSE 0 END = 1 '

-- Waiting to hear what Versova wants to do with samples that don't have a moisture nutrient
--SET @Where += ' and (Select top 1 ResultValue from dbo.SampleResult R 
--	inner join dbo.AIMNutrientRefAnalyzerNutrient x on R.AnalyzerNutrientID = x.AnalyzerNutrientID
--	inner join dbo.SystemSettings ss on ss.AIMNutrientID = x.AIMNutrientID
--	where R.sampleid = S.SampleID)  > 0 '

--6/16/23 - added check of flag and added AdjustedOrResult (Mountaire)
if @Flag = 1 
	Begin
	if @AdjustedOrResult = 'A'
		SET @Where += ' and ASLG.Moisture > 0 '
	else if @AdjustedOrResult = 'R'
		SET @Where += ' and (SR.ResultValue < ASLG.[Min] OR (SR.ResultValue > ASLG.[Max] and ASLG.[Max] <> 0)) '
	End

SET @SQL = 'SELECT DISTINCT '
If @ToDate is NOT NULL
	SET @Sql += '''' + convert(varchar,@ToDate) + ''' as ToDate, '
If @FromDate IS NOT NULL
	SET @Sql += '''' + convert(varchar,@FromDate) + ''' as FromDate, '


SET @SQL += '
	S.SampleID
	, S.SampleCode
	, S.SampleDate
	, S.AIMMaterialID
	, S.AimSourceID
	, S.AimSourceLocationID
	, S.AIMMaterialID
	, S.LoadNumber
	, S.BillOfLoad
	, SA.AIMNutrientID
	, SA.AIMUsedValue
	, SA.SampleResultID
	, SR.AnalyzerNutrientID
	, SR.ResultValue
	, AMS.SourceName
	, ASL.SourceLocationName
	, AM.AIMMaterialName
	, AN.AIMNutrientName
	, AN.AIMNutrientCode
	, AOL.LocationCode as OwnerLocationCode
	, AOL.LocationName as OwnerLocationName
	, ASLG.Min
	, ASLG.Max
	, ASLG.Moisture 

	, CASE WHEN SR.ResultValue < ASLG.[Min] OR (SR.ResultValue > ASLG.[Max] AND ASLG.[Max] != 0) THEN
			1
		ELSE
			0 
		END 
		AS Flag


	, (Select top 1 ResultValue from dbo.SampleResult R 
		inner join dbo.AIMNutrientRefAnalyzerNutrient x on R.AnalyzerNutrientID = x.AnalyzerNutrientID
		inner join dbo.SystemSettings ss on ss.AIMNutrientID = x.AIMNutrientID
		where R.sampleid = S.SampleID ) as SampleMoisture	

	, (Select top 1 ResultValue from dbo.SampleResult R 
		inner join dbo.AIMNutrientRefAnalyzerNutrient x on R.AnalyzerNutrientID = x.AnalyzerNutrientID
		inner join dbo.SystemSettings ss on ss.DryMatterAIMNutrientID = x.AIMNutrientID
		where R.sampleid = S.SampleID ) as SampleDryMatter

	, CASE WHEN (Select top 1 ResultValue from dbo.SampleResult R 
		inner join dbo.AIMNutrientRefAnalyzerNutrient x on R.AnalyzerNutrientID = x.AnalyzerNutrientID
		inner join dbo.SystemSettings ss on ss.AIMNutrientID = x.AIMNutrientID
		where R.sampleid = S.SampleID ) = 0 
		AND 
		(Select top 1 ResultValue from dbo.SampleResult R 
		inner join dbo.AIMNutrientRefAnalyzerNutrient x on R.AnalyzerNutrientID = x.AnalyzerNutrientID
		inner join dbo.SystemSettings ss on ss.DryMatterAIMNutrientID = x.AIMNutrientID
		where R.sampleid = S.SampleID ) = 0 THEN
			1
		ELSE
			0 
		END 
		AS FlagBothZero

FROM
	dbo.Sample S with (nolock)
INNER JOIN 
	dbo.SampleAIM SA with (nolock)
		ON SA.SampleID = S.SampleID
INNER JOIN 
	dbo.SampleResult SR with (nolock)
		ON SR.SampleResultID = SA.SampleResultID				
INNER JOIN
	dbo.AIMSourceLocationGuarantee ASLG with (nolock)
		ON S.AimSourceID = ASLG.AIMSourceID  AND 
		   S.AimSourceLocationID = case when ASLG.AIMSourceLocationID = 0 then S.AimSourceLocationID else ASLG.AIMSourceLocationID end '	
		   

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
	dbo.AIMNutrient AN with (nolock) on SA.AIMNutrientID = AN.AIMNutrientID
LEFT JOIN
	dbo.AIMOwnerLocation AOL with (nolock) on S.AIMOwnerLocationID = AOL.AIMOwnerLocationID

'
 + @Where 

 
EXEC (@SQL)
--print @sql


END

