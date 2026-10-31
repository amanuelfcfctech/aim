CREATE PROCEDURE  [dbo].[usp_AIMOwnerLocationGroup_SAV]
			  @OwnerLocationGroupID		INT
			, @OwnerLocationGroupCode		NVARCHAR(50)
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @OwnerLocationGroupID = 0 
	BEGIN
		IF EXISTS(SELECT 
					AIMOwnerLocationGroupID					
				FROM 
					dbo.AIMOwnerLocationGroup OLG
				WHERE 
						OLG.AIMOwnerLocationGroupCode = @OwnerLocationGroupCode
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Group already exists. Information is not saved.'
				SET @RecordID = (SELECT 
									AIMOwnerLocationGroupID 
								FROM 
									dbo.AIMOwnerLocationGroup OLG
								WHERE 
									OLG.AIMOwnerLocationGroupCode = @OwnerLocationGroupCode
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.AIMOwnerLocationGroup
						(				
						  AIMOwnerLocationGroupCode												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						  @OwnerLocationGroupCode						
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
				dbo.AIMOwnerLocationGroup WITH(ROWLOCK)
					SET 
						  AIMOwnerLocationGroupCode = ISNULL(@OwnerLocationGroupCode, AIMOwnerLocationGroupCode)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						AIMOwnerLocationGroupID = @OwnerLocationGroupID

			SET @RecordID = @OwnerLocationGroupID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END