create PROCEDURE [dbo].[usp_GuaranteeRefundHeader_SEL_LastBatchID]
AS
BEGIN
SET NOCOUNT ON;

SELECT TOP 1 
	  HDR.*
	
FROM 
	dbo.GuaranteeRefundHeader HDR
			
ORDER BY BatchID DESC
END