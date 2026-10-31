CREATE PROCEDURE  [dbo].[usp_ExportIngredientGroup_SAV]
			  @ExportIngredientGroupID		INT
			, @ExportIngredientGroupCode		NVARCHAR(50)
			, @AIMMaterialID						INT
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @ExportIngredientGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					ExportIngredientGroupID					
				FROM 
					dbo.ExportIngredientGroup EIG
				WHERE 
						EIG.ExportIngredientGroupCode = @ExportIngredientGroupCode
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Group already exists. Information is not saved.'
				SET @RecordID = (SELECT 
									ExportIngredientGroupID 
								FROM 
									dbo.ExportIngredientGroup EIG
								WHERE 
									EIG.ExportIngredientGroupCode = @ExportIngredientGroupCode
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.ExportIngredientGroup
						(				
						  ExportIngredientGroupCode	
						, AIMMaterialID
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @ExportIngredientGroupCode	
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
				dbo.ExportIngredientGroup WITH(ROWLOCK)
					SET 
						  ExportIngredientGroupCode = ISNULL(@ExportIngredientGroupCode, ExportIngredientGroupCode)
						, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						ExportIngredientGroupID = @ExportIngredientGroupID

			SET @RecordID = @ExportIngredientGroupID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END