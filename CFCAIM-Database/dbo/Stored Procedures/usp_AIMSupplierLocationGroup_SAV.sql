
CREATE PROCEDURE  [dbo].[usp_AIMSupplierLocationGroup_SAV]
			  @SupplierLocationGroupID		INT
			, @SupplierGroupID				INT
			, @AIMSupplierID				INT
			, @AIMSupplierLocationID		INT
			, @CFCUserID					INT
			, @IsActive						BIT


AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SupplierLocationGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					AIMSupplierLocationGroupID					
				FROM 
					dbo.AIMSupplierLocationGroup OLGD
				WHERE 
						OLGD.AIMSupplierGroupID = @SupplierGroupID and		
						OLGD.AIMSupplierID = @AIMSupplierID and
						OLGD.AIMSupplierLocationID = @AIMSupplierLocationID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Supplier Location already exists in the group. Information is not saved.'
				SET @RecordID = (SELECT 
									AIMSupplierLocationGroupID 
								FROM 
									dbo.AIMSupplierLocationGroup OLGD
								WHERE 
									OLGD.AIMSupplierGroupID = @SupplierGroupID and
									OLGD.AIMSupplierID = @AIMSupplierID and
									OLGD.AIMSupplierLocationID = @AIMSupplierLocationID
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.AIMSupplierLocationGroup
						(																											
						 AIMSupplierGroupID
						, AIMSupplierID
						, AIMSupplierLocationID												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(						
						 @SupplierGroupID
						, @AIMSupplierID
						, @AIMSupplierLocationID
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
				dbo.AIMSupplierLocationGroupDetail WITH(ROWLOCK)
					SET 
						  AIMSupplierLocationGroupID = ISNULL(@SupplierLocationGroupID, AIMSupplierLocationGroupID)						
						, AIMSupplierGroupID = ISNULL(@SupplierGroupID, AIMSupplierGroupID)			
						, AIMSupplierID = ISNULL(@AIMSupplierID, AIMSupplierID)						
						, AIMSupplierLocationID = ISNULL(@AIMSupplierLocationID, AIMSupplierLocationID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						AIMSupplierLocationGroupID = @SupplierLocationGroupID

			SET @RecordID = @SupplierLocationGroupID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END