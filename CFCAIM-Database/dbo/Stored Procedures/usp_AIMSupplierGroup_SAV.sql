create PROCEDURE  [dbo].[usp_AIMSupplierGroup_SAV]
			  @SupplierGroupID					INT
			, @SupplierGroupCode				NVARCHAR(50)
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SupplierGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					AIMSupplierGroupID					
				FROM 
					dbo.AIMSupplierGroup OLG
				WHERE 
						OLG.AIMSupplierGroupCode = @SupplierGroupCode
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Group already exists. Information is not saved.'
				SET @RecordID = (SELECT 
									AIMSupplierGroupID 
								FROM 
									dbo.AIMSupplierGroup OLG
								WHERE 
									OLG.AIMSupplierGroupCode = @SupplierGroupCode
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.AIMSupplierGroup
						(				
						  AIMSupplierGroupCode												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @SupplierGroupCode						
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
				dbo.AIMSupplierGroup WITH(ROWLOCK)
					SET 
						  AIMSupplierGroupCode = ISNULL(@SupplierGroupCode, AIMSupplierGroupCode)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						AIMSupplierGroupID = @SupplierGroupID

			SET @RecordID = @SupplierGroupID
		END
		
SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END