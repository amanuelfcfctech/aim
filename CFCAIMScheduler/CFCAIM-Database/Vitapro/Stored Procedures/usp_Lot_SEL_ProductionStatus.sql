CREATE PROCEDURE [Vitapro].[usp_Lot_SEL_ProductionStatus]

	@ProductionStatus		INT
	, @Days					INT
	, @AimNutrientID1			INT
	, @AimNutrientID2			INT
	, @AimNutrientID3			INT
	, @AimNutrientID4			INT



--exec vitapro.usp_Lot_SEL_ProductionStatus 5, 15
--exec vitapro.usp_Lot_SEL_ProductionStatus 2, 15

AS
BEGIN
SET NOCOUNT ON;

DECLARE @SQL				VARCHAR(6000) = ''
DECLARE @Where				VARCHAR(1000) = ''

----If @Days = 0, then get ALL the samples
If @Days <> 0
	SET @Where = @Where + ' AND (L.DepletedDate IS NULL OR L.DepletedDate >= ''' + Cast(DATEADD(d,-@Days,GETDATE()) as varchar) + ''') '
	
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
	, (Select Sum(SA.AIMUsedValue) from sampleaim SA where SA.SampleID = S.SampleID and SA.AIMNutrientID = ' + convert(varchar,@AimNutrientID1) + ') as AimNutrientAmount1
	, (Select Sum(SA.AIMUsedValue) from sampleaim SA where SA.SampleID = S.SampleID and SA.AIMNutrientID = ' + convert(varchar,@AimNutrientID2) + ') as AimNutrientAmount2
	, (Select Sum(SA.AIMUsedValue) from sampleaim SA where SA.SampleID = S.SampleID and SA.AIMNutrientID = ' + convert(varchar,@AimNutrientID3) + ') as AimNutrientAmount3
	, (Select Sum(SA.AIMUsedValue) from sampleaim SA where SA.SampleID = S.SampleID and SA.AIMNutrientID = ' + convert(varchar,@AimNutrientID4) + ') as AimNutrientAmount4
	, L.Concept5ExportStatus
	
FROM 
	Vitapro.Lot L 

LEFT OUTER JOIN 
	dbo.Sample S on L.SampleID = S.SampleID
LEFT OUTER JOIN
	dbo.AIMMaterial AM
		on S.AIMMaterialID = AM.AIMMaterialID

WHERE	
	 (L.ProductionStatusLine1 = ' + cast(@ProductionStatus as varchar) + ' or
	 L.ProductionStatusLine2 = ' + cast(@ProductionStatus as varchar) + ' ) ' 
	 + @Where


SET @SQL = @SQL + '

ORDER BY 	
	AM.AIMMaterialCode,
	AM.AIMMaterialName,
	S.ExpirationDate DESC '

EXEC (@SQL)
--print (@SQL)

END