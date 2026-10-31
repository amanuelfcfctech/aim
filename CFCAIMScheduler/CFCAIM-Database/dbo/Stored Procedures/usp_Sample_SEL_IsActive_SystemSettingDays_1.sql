CREATE PROCEDURE [dbo].[usp_Sample_SEL_IsActive_SystemSettingDays]	
		@IsActive				BIT
		, @Days					INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''
DECLARE @Where				VARCHAR(1000) = ''


-- 1/11/21 - removed link to aspnet_User table
-- 1/22/21 - added SampleTypeID
-- 9/30/22 - added GroundUnGround


--exec usp_Sample_SEL_IsActive_SystemSettingDays 1, 1000

SET @Where = ' WHERE S.IsActive = ''' + Cast(@IsActive as varchar) + ''' '

-- If @Days = 0, then get ALL the samples

If @Days <> 0
	SET @Where = @Where + ' AND S.SampleDate >= ''' + Cast(DATEADD(d,-@Days,GETDATE()) as varchar) + ''' '


SET @SQL = 'SELECT 
	  S.SampleID
	  , CASE WHEN S.SampleCode IS NULL or len(S.SampleCode) = 0 then	
		convert(varchar, S.SampleID) else S.SampleCode End as SampleCode      
      ,S.CreatedStamp
      ,S.AIMOwnerID
      ,S.AIMSourceID
      ,S.AIMSourceLocationID
      ,S.ContractNumber
      ,S.PoNumber
      ,S.InvoiceNumber
      ,S.TicketNumber
      ,S.LoadNumber
      ,S.BillOfLoad
      ,S.VehicleNumber
      ,S.LotNumber
      ,S.IsActive
      ,S.AnalyzerMaterialID
      ,S.SampleDate
      ,S.AnalysisDate
      ,S.AIMOwnerLocationID
      ,S.CFCUserID
      ,S.UpdateStamp
      ,S.FurtherAnalysisID
      ,S.AIMMaterialID
      ,S.AlternateCode
	  ,S.DataSourceTypeID	   	 
	  ,S.CFCProductArchiveID
	  ,S.Notes
	  ,S.ExpirationDate
	  ,S.SupportingDocumentPath
	  ,S.SampleTypeID
	  ,S.IsGround
	  ,S.SampleTestTypeID
	  ,S.GroundUnGroundID
	  	  
FROM 
	dbo.Sample S 
	LEFT OUTER JOIN
			CFCUser.CFCUser U 
				ON S.CFCUserID = U.CFCUserID	' + @Where

SET @SQL += 'ORDER BY
	S.SampleCode '


EXEC (@SQL)

	
END