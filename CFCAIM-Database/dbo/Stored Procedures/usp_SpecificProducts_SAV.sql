CREATE PROCEDURE  [dbo].[usp_SpecificProducts_SAV]
			  @SpecificProductsID			INT
			, @SpecificProductsGroupID		INT
			, @AIMMaterialID					INT			
			, @CFCUserID						INT
			, @IsActive							BIT
	
	
-- exec [usp_SpecificProducts_SAV] 0, 269, 1
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SpecificProductsID = 0 
	BEGIN
		IF EXISTS(SELECT 
					SpecificProductsID					
				FROM 
					dbo.SpecificProducts SP
				WHERE 
						SP.SpecificProductsGroupID = @SpecificProductsGroupID AND 
						SP.AIMMaterialID = @AIMMaterialID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Product already exists in this group. Information is not saved.'
				SET @RecordID = (SELECT 
									SpecificProductsID 
								FROM 
									dbo.SpecificProducts SP
								WHERE 
									SP.SpecificProductsGroupID = @SpecificProductsGroupID and
									SP.AIMMaterialID = @AIMMaterialID)
			END
		ELSE
			BEGIN
				INSERT
					dbo.SpecificProducts
						(				
						  SpecificProductsGroupID
						, AIMMaterialID						
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @SpecificProductsGroupID
						, @AIMMaterialID						
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
				dbo.SpecificProducts WITH(ROWLOCK)
					SET 
						  SpecificProductsGroupID = ISNULL(@SpecificProductsGroupID, SpecificProductsGroupID)
						, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						SpecificProductsID = @SpecificProductsID

			SET @RecordID = @SpecificProductsID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END