CREATE PROCEDURE  [dbo].[usp_SpecificProductsGroup_SAV]
			  @SpecificProductsGroupID		INT
			, @SpecificProductsGroupCode		NVARCHAR(50)
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SpecificProductsGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					SpecificProductsGroupID					
				FROM 
					dbo.SpecificProductsGroup SP
				WHERE 
						SP.SpecificProductsGroupCode = @SpecificProductsGroupCode
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Group already exists. Information is not saved.'
				SET @RecordID = (SELECT 
									SpecificProductsGroupID 
								FROM 
									dbo.SpecificProductsGroup SP
								WHERE 
									SP.SpecificProductsGroupCode = @SpecificProductsGroupCode
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.SpecificProductsGroup
						(				
						  SpecificProductsGroupCode												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @SpecificProductsGroupCode						
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
				dbo.SpecificProductsGroup WITH(ROWLOCK)
					SET 
						  SpecificProductsGroupCode = ISNULL(@SpecificProductsGroupCode, SpecificProductsGroupCode)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						SpecificProductsGroupID = @SpecificProductsGroupID

			SET @RecordID = @SpecificProductsGroupID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END