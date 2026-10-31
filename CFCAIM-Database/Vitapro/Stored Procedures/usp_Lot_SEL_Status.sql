CREATE PROCEDURE [Vitapro].[usp_Lot_SEL_Status]

	(@Days			INT
	,@Status		INT)

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''
DECLARE @Where				VARCHAR(1000) = ''

----If @Days = 0, then get ALL the samples
If @Days <> 0
	SET @Where = @Where + ' AND (L.LotStatusUpdate IS NULL OR L.LotStatusUpdate >= ''' + Cast(DATEADD(d,-@Days,GETDATE()) as varchar) + ''') '
	
SET @SQL = 'SELECT
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
	, (Select count(*) from sampleresult SR where SR.sampleid = S.sampleid and SR.IsActive = 1) as ResultCount
	, L.Concept5ExportStatus

FROM 
	Vitapro.Lot L 

LEFT OUTER JOIN 
	dbo.Sample S on L.SampleID = S.SampleID
LEFT OUTER JOIN
	dbo.AIMMaterial AM
		on S.AIMMaterialID = AM.AIMMaterialID


WHERE L.LotStatusID = ' + cast(@Status as varchar) 
+ @Where


SET @SQL = @SQL + '

ORDER BY 
	AM.AIMMaterialCode,
	AM.AIMMaterialName,
	S.ExpirationDate DESC '

EXEC (@SQL)
--print (@SQL)

END