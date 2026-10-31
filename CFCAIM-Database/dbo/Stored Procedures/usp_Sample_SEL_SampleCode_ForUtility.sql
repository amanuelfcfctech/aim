CREATE PROCEDURE [dbo].[usp_Sample_SEL_SampleCode_ForUtility]	
		@SampleCode			NVARCHAR(20)
AS
BEGIN
SET NOCOUNT ON;

SELECT S.SampleID
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

	  --2/12/18
	  ,S.Notes

	  --5/29/18
	  ,S.ExpirationDate

	  --9/8/20
	  ,S.SupportingDocumentPath

	  --9/30/22
	  ,S.IsGround
	  
FROM 
	dbo.Sample S 
	LEFT OUTER JOIN
			CFCUser.CFCUser U 
				ON S.CFCUserID = U.CFCUserID	
WHERE
	S.SampleCode = @SampleCode

	
END