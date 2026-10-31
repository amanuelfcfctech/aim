CREATE PROCEDURE  [dbo].[usp_SpecificIngredientsGroup_SAV]
			  @SpecificIngredientsGroupID		INT
			, @SpecificIngredientsGroupCode		NVARCHAR(50)
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @SpecificIngredientsGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					SpecificIngredientsGroupID					
				FROM 
					dbo.SpecificIngredientsGroup SP
				WHERE 
						SP.SpecificIngredientsGroupCode = @SpecificIngredientsGroupCode
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Group already exists. Information is not saved.'
				SET @RecordID = (SELECT 
									SpecificIngredientsGroupID 
								FROM 
									dbo.SpecificIngredientsGroup SP
								WHERE 
									SP.SpecificIngredientsGroupCode = @SpecificIngredientsGroupCode
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.SpecificIngredientsGroup
						(				
						  SpecificIngredientsGroupCode												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @SpecificIngredientsGroupCode						
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
				dbo.SpecificIngredientsGroup WITH(ROWLOCK)
					SET 
						  SpecificIngredientsGroupCode = ISNULL(@SpecificIngredientsGroupCode, SpecificIngredientsGroupCode)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						SpecificIngredientsGroupID = @SpecificIngredientsGroupID

			SET @RecordID = @SpecificIngredientsGroupID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END