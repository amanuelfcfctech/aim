CREATE PROCEDURE  [dbo].[usp_SpecificIngredients_SAV]
			  @SpecificIngredientsID			INT
			, @SpecificIngredientsGroupID		INT
			, @AIMMaterialID					INT			
			, @CFCUserID						INT
			, @IsActive							BIT
	
	
-- exec [usp_SpecificIngredients_SAV] 0, 269, 1
			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SpecificIngredientsID = 0 
	BEGIN
		IF EXISTS(SELECT 
					SpecificIngredientsID					
				FROM 
					dbo.SpecificIngredients SP
				WHERE 
						SP.SpecificIngredientsGroupID = @SpecificIngredientsGroupID AND 
						SP.AIMMaterialID = @AIMMaterialID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Material already exists in this group. Information is not saved.'
				SET @RecordID = (SELECT 
									SpecificIngredientsID 
								FROM 
									dbo.SpecificIngredients SP
								WHERE 
									SP.SpecificIngredientsGroupID = @SpecificIngredientsGroupID and
									SP.AIMMaterialID = @AIMMaterialID)
			END
		ELSE
			BEGIN
				INSERT
					dbo.SpecificIngredients
						(				
						  SpecificIngredientsGroupID
						, AIMMaterialID						
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @SpecificIngredientsGroupID
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
				dbo.SpecificIngredients WITH(ROWLOCK)
					SET 
						  SpecificIngredientsGroupID = ISNULL(@SpecificIngredientsGroupID, SpecificIngredientsGroupID)
						, AIMMaterialID = ISNULL(@AIMMaterialID, AIMMaterialID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						SpecificIngredientsID = @SpecificIngredientsID

			SET @RecordID = @SpecificIngredientsID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END