CREATE PROCEDURE [Vitapro].[usp_Lot_SEL_AIMMaterialID_ProductionStatusLine]

	@AIMMaterialID		INT  
  , @LineNumber			INT
  , @Status				INT
  , @SampleID			INT

  --exec vitapro.usp_Lot_SEL_AIMMaterialID_ProductionStatusLine 376, 2, 3

AS
BEGIN
SET NOCOUNT ON;

DECLARE @Sql			VARCHAR(1000)

SET @Sql = '

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

INNER JOIN 
	dbo.Sample S 
		on L.SampleID = S.SampleID
INNER JOIN
	dbo.AIMMaterial AM
		on S.AIMMaterialID = AM.AIMMaterialID

WHERE 
    
	S.SampleID <> ' + Cast(@SampleID as varchar) +' AND 	
	S.AIMMaterialID = ' + Cast(@AIMMaterialID as varchar) + ' AND '


IF @LineNumber = 1 
	SET @Sql = @Sql + ' 
				ProductionStatusLine1 = ' + cast(@Status as varchar)
IF @LineNumber = 2 
	SET @Sql = @Sql + ' 
				ProductionStatusLine2 = ' + cast(@Status as varchar)
IF @LineNumber = 3 
	SET @Sql = @Sql + ' 
				ProductionStatusLine3 = ' + cast(@Status as varchar)
IF @LineNumber = 4 
	SET @Sql = @Sql + ' 	
				ProductionStatusLine4 = ' + cast(@Status as varchar)


SET @Sql = @Sql + ' 
ORDER BY 
	AM.AIMMaterialCode,
	AM.AIMMaterialName,
	S.ExpirationDate DESC '


--Print @Sql
EXEC (@Sql)


END