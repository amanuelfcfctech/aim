CREATE PROCEDURE  [dbo].[usp_ExportPlant_SAV]
			  @ExportPlantID			INT
			, @ExportPlantGroupID		INT
			, @CFCPlantID					INT			
			, @CFCUserID						INT
			, @IsActive							BIT
	

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @ExportPlantID = 0 
	BEGIN
		IF EXISTS(SELECT 
					ExportPlantID					
				FROM 
					dbo.ExportPlant EP
				WHERE 
						EP.ExportPlantGroupID = @ExportPlantGroupID AND 
						EP.CFCPlantID = @CFCPlantID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Plant already exists in this group. Information is not saved.'
				SET @RecordID = (SELECT 
									ExportPlantID 
								FROM 
									dbo.ExportPlant EP
								WHERE 
									EP.ExportPlantGroupID = @ExportPlantGroupID and
									EP.CFCPlantID = @CFCPlantID)
			END
		ELSE
			BEGIN
				INSERT
					dbo.ExportPlant
						(				
						  ExportPlantGroupID
						, CFCPlantID						
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @ExportPlantGroupID
						, @CFCPlantID						
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
				dbo.ExportPlant WITH(ROWLOCK)
					SET 
						  ExportPlantGroupID = ISNULL(@ExportPlantGroupID, ExportPlantGroupID)
						, CFCPlantID = ISNULL(@CFCPlantID, CFCPlantID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						ExportPlantID = @ExportPlantID

			SET @RecordID = @ExportPlantID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END