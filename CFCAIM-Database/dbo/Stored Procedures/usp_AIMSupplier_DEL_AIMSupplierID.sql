CREATE PROCEDURE [dbo].[usp_AIMSupplier_DEL_AIMSupplierID]
			  @AIMSupplierID					INT
AS
BEGIN
SET NOCOUNT ON;

DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ID INT


BEGIN		

	SET @ID = (SELECT TOP 1 AIMSourceID FROM dbo.Sample S WITH(NOLOCK) 
				WHERE S.AIMSourceID = @AIMSupplierID)
	IF Len(@ID) > 0
		BEGIN			
			SET @ErrorNumber = 1
			SET @ErrorMessage = 'Unable to delete supplier because it is being used in sample table.' 
		END
		
	IF EXISTS(SELECT TOP 1 AIMSourceID FROM dbo.AIMSourceLocation AOL WITH(NOLOCK) 
				WHERE AOL.AIMSourceID = @AIMSupplierID and
						AOL.IsActive = 1)
		BEGIN
			SET @ErrorNumber = 2
			SET @ErrorMessage = 'Unable to delete Supplier because an active Supplier Location is assigned to it. You must first inactivate the supplier-location(s).'										
		END
END
	
IF @ErrorNumber = 0
	BEGIN

		DELETE FROM
			dbo.AIMSourceLocation WITH(ROWLOCK)				
		WHERE
			AIMSourceID = @AIMSupplierID and IsActive = 0

		DELETE FROM
			dbo.AIMSource WITH(ROWLOCK)				
		WHERE
			AIMSourceID = @AIMSupplierID  			
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage
	

END