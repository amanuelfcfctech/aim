CREATE PROCEDURE [dbo].[usp_Report_SampleCustom]
      @MaterialTypeID		INT			
	, @FromDate				DATE		= NULL
	, @ToDate				DATE		= NULL
	, @AIMOwnerLocationID	INT			= NULL

	--2/16/21
	, @GroupMaterials		BIT			= NULL
	, @SampleResultType     INT			= NULL		--Created to choose option between Samples WITH result, WITHOUT results, and BOTH WITH and WITHOUT results Task1076 DB
													-- 0 = (Default) Samples WITH results
													-- 1 = Samples WTIHOUT results
													-- 2 = Samples WITH and WITHOUT results (all)
--exec usp_Report_SampleCustom 1, '2025-03-05', '2025-07-10', 0, 0
--exec usp_Report_SampleCustom 1, '', ''

--2/16/21 - added AIMMaterialGroup - header and detail tables

AS
BEGIN

SET NOCOUNT ON;

--10/17/23
--DECLARE @SQL			  VARCHAR(6000) = ''
--DECLARE @Where		  VARCHAR(1000) = ''
DECLARE @SQL					VARCHAR(MAX) = ''
DECLARE @Where					VARCHAR(MAX) = ''
DECLARE @SampleResultOption		VARCHAR(MAX) = ''
DECLARE @SampleResultWhere		VARCHAR(MAX) = ''

SET @Where = ' WHERE S.IsActive = 1 and AM.MaterialTypeID = ' + Cast(@MaterialTypeID as varchar)
if @AIMOwnerLocationID <> 0
	SET @Where = @Where + ' and	S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar)
if @FromDate IS NOT NULL
	SET @Where = @Where + ' and S.SampleDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' '
if @ToDate IS NOT NULL
	SET @Where = @Where + ' and S.SampleDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' '
	
SET @SampleResultOption = 
	CASE	
		WHEN @SampleResultType = 1 THEN 'LEFT OUTER JOIN'	
		WHEN @SampleResultType = 2 THEN 'LEFT OUTER JOIN'
		ELSE 'INNER JOIN'									-- Don't check for 0, just default if 0 or anything else gets passed to proc doesn't break
	END

IF @SampleResultType = 1
	SET @Where = @Where + ' AND SR.SampleResultID IS NULL '

SET @SQL = 'SELECT 

	S.SampleCode
	, S.SampleDate 
	, S.CreatedStamp as SampleCreatedStamp      
	, S.ContractNumber 
	, S.PoNumber 
	, S.InvoiceNumber 
	, S.TicketNumber 
	, S.LoadNumber 
	, S.BillOfLoad 
	, S.VehicleNumber 
	, S.LotNumber 
	, S.AlternateCode      
	, S.Notes 	          
'
--, AM.AIMMaterialCode
--, AM.AIMMaterialName
If @GroupMaterials = 0
	Begin
		SET @SQL = @SQL + ', AM.AIMMaterialCode, AM.AIMMaterialName '
	End
Else
	Begin
		SET @SQL = @SQL + '
		, Case when GH.AIMMaterialGroupHeaderName is not null then '''' Else AM.AIMMaterialCode end as AIMMaterialCode
		, Case when GH.AIMMaterialGroupHeaderName is not null then GH.AIMMaterialGroupHeaderName Else AM.AIMMaterialName end as AIMMaterialName '
	End
	
SET @SQL = @SQL + '

	, MT.MaterialTypeName
	, AO.OwnerCode as AIMOwnerCode
	, AO.OwnerName as AIMOwnerName
	, AOL.LocationCode as AIMOwnerLocationCode
	, AOL.LocationName as AIMOwnerLocationName
	, AMS.AIMSupplierCode
	, AMS.SourceName as AIMSupplierName
	, ASL.AIMSupplierLocationCode
	, ASL.SourceLocationName as AIMSupplierLocationName
	, AN.AnalyzerNutrientCode
	, AN.AnalyzerNutrientName	              
	, SR.Unit
	, SR.ResultValue      
	, SR.AnalysisDate as ResultAnalysisDate
	, DST.DataSourceTypeCode
	, CP.CFCProductCode
	, CP.CFCProductName
	, CPA.ProductionDate
	, CPA.ProductionVersion
	, Case when SFA.SampleFurtherAnalysisID is null then '''' Else DST.Name end as FurtherAnalysis
	, Case when GUG.GroundUnGroundID = ''1'' then ''Ground'' else case when GUG.GroundUnGroundID = ''2'' then ''Whole'' else '''' end end as GroundUnGround
	, ST.SampleTypeName
	, WL.Min AS WLMin 					
	, WL.Max AS WLMax					
	, ISNULL(CASE WHEN SR.ResultValue < WL.Min THEN 1 ELSE CASE WHEN SR.ResultValue > WL.Max THEN 2 ELSE 0 END END, 0) AS ResultValueFlag
	, WL.AIMNutrientID
FROM 
	dbo.Sample S with (nolock) 
	'


+ @SampleResultOption +
'

	dbo.SampleResult SR with (nolock) 
		ON S.SampleID = SR.SampleID
		   AND SR.IsActive = 1
LEFT OUTER JOIN 
	dbo.AIMMaterial AM with (nolock) 
		ON S.AIMMaterialID = AM.AIMMaterialID 
LEFT OUTER JOIN 
	dbo.MaterialType MT with (nolock) 
		ON AM.MaterialTypeID = MT.MaterialTypeID
LEFT OUTER JOIN 
	dbo.AIMOwner AO with (nolock) 
		ON S.AIMOwnerID = AO.AIMOwnerID
LEFT OUTER JOIN 
	dbo.AIMOwnerLocation AOL with (nolock) 
		ON S.AIMOwnerLocationID = AOL.AIMOwnerLocationID
LEFT OUTER JOIN 
	dbo.AIMSource AMS with (nolock) 
		ON S.AIMSourceID = AMS.AIMSourceID
LEFT OUTER JOIN 
	dbo.AIMSourceLocation ASL with (nolock) 
		ON S.AIMSourceLocationID = ASL.AIMSourceLocationID
LEFT OUTER JOIN	
	dbo.AnalyzerNutrient AN with (nolock) 
		ON SR.AnalyzerNutrientID = AN.AnalyzerNutrientID
LEFT OUTER JOIN 
	dbo.DataSourceType DST with (nolock) 
		ON AN.DataSourceTypeID = DST.DataSourceTypeID
LEFT OUTER JOIN
	dbo.CFCProductArchive CPA with (nolock)
		ON S.CFCProductArchiveID = CPA.CFCProductArchiveID
LEFT OUTER JOIN
	dbo.CFCProduct CP WITH (NOLOCK)
		ON CPA.CFCProductID = CP.CFCProductID

LEFT OUTER JOIN
	dbo.AIMMaterialGroupDetail GD on S.AimMaterialID = GD.AIMMaterialID
LEFT OUTER JOIN
	dbo.AIMMaterialGroupHeader GH on GD.AIMMaterialGroupHeaderID = GH.AIMMaterialGroupHeaderID

LEFT OUTER JOIN
	dbo.SampleFurtherAnalysis SFA on S.FurtherAnalysisID = SFA.SampleFurtherAnalysisID
LEFT OUTER JOIN
	dbo.GroundUnGround GUG on GUG.GroundUnGroundID = S.GroundUnGroundID

LEFT OUTER JOIN
	dbo.SampleType ST
	ON ST.SampleTypeID = S.SampleTypeID

LEFT OUTER JOIN 
	dbo.AIMNutrientRefAnalyzerNutrient AMNX
	ON SR.AnalyzerNutrientID = AMNX.AnalyzerNutrientID
LEFT OUTER JOIN			
		dbo.WarningLimits WL
		ON WL.AIMNutrientID = AMNX.AIMNutrientID
		AND WL.AIMMaterialID = S.AIMMaterialID
'

SET @SQL = @SQL + @Where

--print @sql
EXEC (@SQL)


END