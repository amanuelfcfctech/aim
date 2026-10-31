CREATE PROCEDURE [dbo].[usp_Sample_UPD_IsActive_SupplierID_SupplierLocationID]
			  @IsActive		BIT
			, @AIMSupplierID	INT			
			, @AIMSupplierLocationID INT
			, @CFCUserID	INT

AS
BEGIN
SET NOCOUNT ON;
DECLARE @RecordID INT = 0
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

	
UPDATE
	dbo.Sample WITH(ROWLOCK)
		SET					
			IsActive = @IsActive
			, CFCUserID = @CFCUserID
			, UpdateStamp = GETDATE()
					
		WHERE
			AIMSourceID = @AIMSupplierID and
			AIMSourceLocationID = @AIMSupplierLocationID

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END