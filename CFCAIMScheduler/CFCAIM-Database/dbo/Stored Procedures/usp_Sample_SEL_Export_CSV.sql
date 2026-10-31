CREATE PROCEDURE [dbo].[usp_Sample_SEL_Export_CSV]		
	@ExportTemplateID		INT	
		

-- 1/11/21 - removed link to aspnet_User table

--exec usp_Sample_SEL_Export_CSV 3

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL	VARCHAR(2000) = ''
DECLARE @Where	VARCHAR(500) = '' 
	
EXEC @Where = [dbo].[AnalysisReports_WhereStatement] @ExportTemplateID 	

SET @Where = ' WHERE S.IsActive = 1 ' + @Where 	
		
SET @SQL = '
	SELECT S.SampleID
		,  CASE WHEN S.SampleCode IS NULL or len(S.SampleCode) = 0 then	
			convert(varchar, S.SampleID) else S.SampleCode End as SampleCode  
		, S.SampleDate    
		, AM.AIMMaterialCode
		, AM.AIMMaterialName
		, AO.OwnerCode as AIMOwnerCode
		, AO.OwnerName as AIMOwnerName
		, AOL.LocationCode as AIMOwnerLocationCode
		, AOL.LocationName as AIMOwnerLocationName
		, AMS.AIMSupplierCode
		, AMS.SourceName as AIMSupplierName
		, AMSL.AIMSupplierLocationCode 
		, AMSL.SourceLocationName as AIMSupplierLocationName 
		, S.AlternateCode
		, S.BillOfLoad
		, S.ContractNumber		
		, S.InvoiceNumber		
		, S.LoadNumber
		, S.LotNumber
		, S.PoNumber		
		, S.TicketNumber
		, S.VehicleNumber	
		, AM.AIMMaterialID
		, AO.AIMOwnerID
		, AOL.AIMOwnerLocationID
		, AMS.AIMSourceID as AIMSupplierID
		, AMSL.AIMSourceLocationID as AIMSupplierLocationID
		, AM.MaterialTypeID
		   
	FROM  
		dbo.Sample S WITH(NOLOCK) 
	
	LEFT JOIN
		dbo.AIMMaterial AM with (nolock)
			on S.AIMMaterialID = AM.AIMMaterialID	
	LEFT JOIN
		dbo.AIMOwner AO with (nolock)
			on S.AIMOwnerID = AO.AIMOwnerID
	LEFT JOIN
		dbo.AIMOwnerLocation AOL with (nolock)
			on S.AIMOwnerLocationID = AOL.AIMOwnerLocationID
	LEFT JOIN
		dbo.AIMSource AMS with (nolock)
			on S.AIMSourceID = AMS.AIMSourceID
	LEFT JOIN
		dbo.AIMSourceLocation AMSL with (nolock)
			on S.AIMSourceLocationID = AMSL.AIMSourceLocationID
	LEFT OUTER JOIN
		CFCUser.CFCUser U WITH(NOLOCK)
			ON S.CFCUserID = U.CFCUserID	'
				
	SET @SQL += @Where

	IF @ExportTemplateID = 0
		SET @SQL += ' and S.SampleID IS NULL '
	
	EXEC(@SQL)
	--print @Sql
	
	
END