CREATE PROCEDURE [dbo].[usp_Report_SampleCustom_Product]
      @MaterialTypeID		INT			
	, @FromDate				DATE		= NULL
	, @ToDate				DATE		= NULL
	, @AIMOwnerLocationID	INT			= NULL

	--2/16/21
	, @GroupMaterials		BIT			= NULL
	
--exec usp_Report_SampleCustom_Product 2, '2020-01-01', '2020-03-31', 0, 1


--2/16/21 - added AIMMaterialGroup - header and detail tables

AS
BEGIN

SET NOCOUNT ON;

--10/17/23
--DECLARE @SQL			  VARCHAR(6000) = ''
--DECLARE @Where		  VARCHAR(1000) = ''
DECLARE @SQL			  VARCHAR(MAX) = ''
DECLARE @Where			  VARCHAR(MAX) = ''


SET @Where = ' WHERE S.IsActive = 1 and  SR.IsActive = 1 and AM.MaterialTypeID = ' + Cast(@MaterialTypeID as varchar)
if @AIMOwnerLocationID <> 0
	SET @Where = @Where + ' and	S.AIMOwnerLocationID = ' + Cast(@AIMOwnerLocationID as varchar)
if @FromDate IS NOT NULL
	SET @Where = @Where + ' and S.SampleDate >= ''' + Convert(varchar,Cast(@FromDate as datetime), 120) + ''' '
if @ToDate IS NOT NULL
	SET @Where = @Where + ' and S.SampleDate <= ''' + Convert(varchar,Cast(@ToDate as datetime), 120) + ''' '

--3/10/21
If @GroupMaterials = 1
	SET @Where = @Where + ' and GH.AIMMaterialGroupHeaderID IS NOT NULL '

	
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
	, CPAS.NutrientMin
	, CPAS.NutrientMax
	, CPAN.NutrientValue

FROM 
	dbo.Sample S with (nolock) 
INNER JOIN  
	dbo.SampleResult SR with (nolock) 
		ON S.SampleID = SR.SampleID
LEFT OUTER JOIN
	dbo.CFCProductArchive CPA with (nolock)
		ON S.CFCProductArchiveID = CPA.CFCProductArchiveID
LEFT OUTER JOIN 
	dbo.CFCProduct CP WITH(NOLOCK)
		ON CP.CFCProductID = CPA.CFCProductID

LEFT OUTER JOIN 
	dbo.AIMNutrientRefAnalyzerNutrient AMNX
		ON SR.AnalyzerNutrientID = AMNX.AnalyzerNutrientID
LEFT OUTER JOIN 
	dbo.CFCNutrientRefAIMNutrient CFCX
		ON CFCX.AIMNutrientID = AMNX.AIMNutrientID

LEFT OUTER JOIN
	dbo.CFCProductArchiveSpec CPAS with (nolock)
		ON CPA.CFCProductArchiveID = CPAS.CFCProductArchiveID and CFCX.CFCNutrientID = CPAS.CFCNutrientID
LEFT OUTER JOIN
	dbo.CFCProductArchiveNutrient CPAN with (nolock)
		ON CPA.CFCProductArchiveID = CPAN.CFCProductArchiveID and CFCX.CFCNutrientID = CPAN.CFCNutrientID

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
	dbo.AIMMaterialGroupDetail GD on S.AimMaterialID = GD.AIMMaterialID
LEFT OUTER JOIN
	dbo.AIMMaterialGroupHeader GH on GD.AIMMaterialGroupHeaderID = GH.AIMMaterialGroupHeaderID

'
SET @SQL = @SQL + @Where

--print @sql
EXEC (@SQL)


END