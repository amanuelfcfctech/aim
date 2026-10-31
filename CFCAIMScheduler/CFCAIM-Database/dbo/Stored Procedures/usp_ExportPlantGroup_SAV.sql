CREATE PROCEDURE  [dbo].[usp_ExportPlantGroup_SAV]
			  @ExportPlantGroupID		INT
			, @ExportPlantGroupCode		NVARCHAR(50)

			--1/25/24 - commented
			--, @AIMOwnerID						INT
			--, @AIMOwnerLocationID				INT

			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @ExportPlantGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					ExportPlantGroupID					
				FROM 
					dbo.ExportPlantGroup EPG
				WHERE 
						EPG.ExportPlantGroupCode = @ExportPlantGroupCode
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Group already exists. Information is not saved.'
				SET @RecordID = (SELECT 
									ExportPlantGroupID 
								FROM 
									dbo.ExportPlantGroup EIG
								WHERE 
									EIG.ExportPlantGroupCode = @ExportPlantGroupCode
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.ExportPlantGroup
						(				
						  ExportPlantGroupCode

						--1/25/24 - commented
						--, AIMOwnerID
						--, AIMOwnerLocationID

						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @ExportPlantGroupCode	

						--1/25/24 - commented
						--, @AIMOwnerID
						--, @AIMOwnerLocationID

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
				dbo.ExportPlantGroup WITH(ROWLOCK)
					SET 
						  ExportPlantGroupCode = ISNULL(@ExportPlantGroupCode, ExportPlantGroupCode)

						--1/25/24 - commented
						--, AIMOwnerID = ISNULL(@AIMOwnerID, AIMOwnerID)
						--, AIMOwnerLocationID = ISNULL(@AIMOwnerLocationID, AIMOwnerLocationID)

						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						ExportPlantGroupID = @ExportPlantGroupID

			SET @RecordID = @ExportPlantGroupID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END