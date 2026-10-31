CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationGuarantee_DEL_AIMSupplierLocationGuaranteeID]
			  @AIMSupplierLocationGuaranteeID					INT

			
AS

BEGIN

SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	DELETE FROM
		dbo.AIMSourceLocationGuarantee WITH(ROWLOCK)				
	WHERE		
		AIMSourceLocationGuaranteeID = @AIMSupplierLocationGuaranteeID
END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END