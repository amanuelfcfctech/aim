CREATE PROCEDURE [Vitapro].[usp_Lot_SEL_ExportStatus]

	@ExportStatus		BIT

AS
BEGIN
SET NOCOUNT ON;

SELECT
	  L.LotID
	, L.SampleID
	, L.LotStatusID
	, L.LotStatusUpdate	
	, L.ProductionStatusLine1
	, L.ProductionStatusLine2
	, L.ProductionStatusLine3
	, L.ProductionStatusLine4	
	, L.ExportStatus
	, L.AmountAvailable
	, L.UpdateStamp
	, L.CFCUserID
	, L.DepletedDate
	, AM.AIMMaterialID
	, AM.AIMMaterialCode
	, AM.AIMMaterialName
	, S.SampleCode
	, S.SampleDate
	, S.LotNumber
	, S.ExpirationDate
	, S.ContractNumber
	, S.InvoiceNumber
	, S.BillOfLoad
	, L.Concept5ExportStatus
	

FROM 
	Vitapro.Lot L 

LEFT OUTER JOIN 
	dbo.Sample S on L.SampleID = S.SampleID
LEFT OUTER JOIN
	dbo.AIMMaterial AM
		on S.AIMMaterialID = AM.AIMMaterialID

WHERE	 
	 L.ExportStatus = @ExportStatus	 

 
ORDER BY 
	AM.AIMMaterialCode,
	AM.AIMMaterialName,
	S.ExpirationDate DESC 

END