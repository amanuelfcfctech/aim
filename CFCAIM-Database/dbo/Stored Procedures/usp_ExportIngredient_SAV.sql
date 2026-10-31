CREATE PROCEDURE  [dbo].[usp_ExportIngredient_SAV]
			  @ExportIngredientID			INT
			, @ExportIngredientGroupID		INT
			, @CFCIngredientID					INT			
			, @CFCUserID						INT
			, @IsActive							BIT
	

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @ExportIngredientID = 0 
	BEGIN
		IF EXISTS(SELECT 
					ExportIngredientID					
				FROM 
					dbo.ExportIngredient EI
				WHERE 
						EI.ExportIngredientGroupID = @ExportIngredientGroupID AND 
						EI.CFCIngredientID = @CFCIngredientID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Ingredient already exists in this group. Information is not saved.'
				SET @RecordID = (SELECT 
									ExportIngredientID 
								FROM 
									dbo.ExportIngredient EI
								WHERE 
									EI.ExportIngredientGroupID = @ExportIngredientGroupID and
									EI.CFCIngredientID = @CFCIngredientID)
			END
		ELSE
			BEGIN
				INSERT
					dbo.ExportIngredient
						(				
						  ExportIngredientGroupID
						, CFCIngredientID						
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @ExportIngredientGroupID
						, @CFCIngredientID						
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
				dbo.ExportIngredient WITH(ROWLOCK)
					SET 
						  ExportIngredientGroupID = ISNULL(@ExportIngredientGroupID, ExportIngredientGroupID)
						, CFCIngredientID = ISNULL(@CFCIngredientID, CFCIngredientID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						ExportIngredientID = @ExportIngredientID

			SET @RecordID = @ExportIngredientID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END