CREATE PROCEDURE [dbo].[usp_AIMSupplierLocationGroup_UPD_IsActive]			  
			 @AIMSupplierLocationID					INT			
			, @IsActive								BIT			= NULL			
			, @CFCUserID							INT			= NULL			
			
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		dbo.AIMSupplierLocationGroup  WITH(ROWLOCK)
			SET 
				 IsActive = ISNULL(@IsActive, IsActive)														
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
				, UpdateStamp = GETDATE()								
					
			WHERE 
				AIMSupplierLocationID = @AIMSupplierLocationID

		SET @RecordID = @@IDENTITY 
END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END