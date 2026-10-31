CREATE PROCEDURE [dbo].[usp_GuaranteeRefundHeader_DEL_BatchID]
				@BatchID		INT
AS
BEGIN
SET NOCOUNT ON;


DELETE 
FROM dbo.GuaranteeRefundDetail 
WHERE GuaranteeRefundHeaderID IN 

	(SELECT GuaranteeRefundHeaderID
	 FROM dbo.GuaranteeRefundHeader
	 WHERE BatchID = @BatchID)
	 
DELETE
FROM 
	dbo.GuaranteeRefundHeader 
WHERE
	BatchID = @BatchID

END