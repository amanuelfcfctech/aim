create PROCEDURE [dbo].[usp_GuaranteeRefundDetail_SAV_Status]

	  @BatchID		INT 
	, @SampleCode	NVARCHAR(20)
	, @Status		INT 

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @BatchID > 0 and Len(@SampleCode) > 0	
	UPDATE dbo.GuaranteeRefundDetail SET Status = @Status
	FROM dbo.GuaranteeRefundDetail D
	INNER JOIN dbo.GuaranteeRefundHeader H 
		ON D.GuaranteeRefundHeaderID = H.GuaranteeRefundHeaderID
	WHERE BatchID = @BatchID and SampleCode = @SampleCode


ELSE IF @BatchID > 0 
	UPDATE dbo.GuaranteeRefundDetail SET Status = @Status
	FROM dbo.GuaranteeRefundDetail D
	INNER JOIN dbo.GuaranteeRefundHeader H 
		ON D.GuaranteeRefundHeaderID = H.GuaranteeRefundHeaderID
	WHERE BatchID = @BatchID

ELSE IF Len(@SampleCode) > 0
	UPDATE dbo.GuaranteeRefundDetail SET Status = @Status
	WHERE SampleCode = @SampleCode

ELSE
	UPDATE dbo.GuaranteeRefundDetail SET Status = @Status


SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END