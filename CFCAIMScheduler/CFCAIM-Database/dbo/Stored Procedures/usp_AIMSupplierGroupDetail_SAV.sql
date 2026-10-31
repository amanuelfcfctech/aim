create PROCEDURE  [dbo].[usp_AIMSupplierGroupDetail_SAV]
			  @SupplierGroupDetailID		INT
			, @SupplierGroupID				INT
			, @SupplierID					INT
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SupplierGroupDetailID = 0 
	BEGIN
		IF EXISTS(SELECT 
					AIMSupplierGroupDetailID					
				FROM 
					dbo.AIMSupplierGroupDetail OLGD
				WHERE 
						OLGD.AIMSupplierGroupID = @SupplierGroupID and
						OLGD.AIMSupplierID = @SupplierID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Supplier already exists in the group. Information is not saved.'
				SET @RecordID = (SELECT 
									AIMSupplierGroupDetailID 
								FROM 
									dbo.AIMSupplierGroupDetail OLGD
								WHERE 
									OLGD.AIMSupplierGroupID = @SupplierGroupID and
									OLGD.AIMSupplierID = @SupplierID
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.AIMSupplierGroupDetail
						(																					
						AIMSupplierGroupID												
						, AIMSupplierID												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						@SupplierGroupID
						, @SupplierID
						, GETDATE()
						, @CFCUserID						
						, 1
						)
				SET @RecordID = @@IDENTITY
			END
		END
ELSE
		BEGIN
			UPDATE
				dbo.AIMSupplierGroupDetail WITH(ROWLOCK)
					SET 
						  AIMSupplierGroupID = ISNULL(@SupplierGroupID, AIMSupplierGroupID)						
						, AIMSupplierID = ISNULL(@SupplierID, AIMSupplierID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						AIMSupplierGroupDetailID = @SupplierGroupDetailID

			SET @RecordID = @SupplierGroupDetailID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END