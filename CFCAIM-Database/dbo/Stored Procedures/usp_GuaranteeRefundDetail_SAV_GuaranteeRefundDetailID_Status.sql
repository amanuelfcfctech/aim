create PROCEDURE [dbo].[usp_GuaranteeRefundDetail_SAV_GuaranteeRefundDetailID_Status]

	  @GuaranteeRefundDetailID		INT 
	, @Status						INT 

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

UPDATE dbo.GuaranteeRefundDetail SET [Status] = @Status
WHERE GuaranteeRefundDetailID = @GuaranteeRefundDetailID

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END