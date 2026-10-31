CREATE PROCEDURE [dbo].[usp_AIMSupplierLocation_DEL_AIMSupplierID_AIMSupplierLocationID]
			  @AIMSupplierLocationID					INT
			, @AIMSupplierID							INT
			
AS

BEGIN

SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @ID INT

BEGIN
	SET @ID = (SELECT TOP 1 AIMSourceLocationID FROM dbo.Sample S WITH(NOLOCK) 
				WHERE S.AIMSourceLocationID = @AIMSupplierLocationID
								and S.AIMSourceID = @AIMSupplierID)
	IF Len(@ID) > 0
		BEGIN			
			SET @ErrorNumber = 1
			SET @ErrorMessage = 'Unable to delete supplier location because it is being used in sample table.' 
		END
END

IF @ErrorNumber = 0
	BEGIN
		DELETE FROM
			dbo.AIMSourceLocation WITH(ROWLOCK)				
		WHERE
			AIMSourceID = @AIMSupplierID  
			and AIMSourceLocationID = @AIMSupplierLocationID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END