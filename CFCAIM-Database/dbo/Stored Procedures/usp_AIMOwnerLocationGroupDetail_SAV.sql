CREATE PROCEDURE  [dbo].[usp_AIMOwnerLocationGroupDetail_SAV]
			  @OwnerLocationGroupDetailID		INT
			, @OwnerLocationGroupID				INT
			, @OwnerLocationID					INT
			, @CFCUserID						INT
			, @IsActive							BIT

			
AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''
DECLARE @RecordID INT = 0

IF @OwnerLocationGroupDetailID = 0 
	BEGIN
		IF EXISTS(SELECT 
					AIMOwnerLocationGroupDetailID					
				FROM 
					dbo.AIMOwnerLocationGroupDetail OLGD
				WHERE 
						OLGD.AIMOwnerLocationGroupID = @OwnerLocationGroupID and
						OLGD.AIMOwnerLocationID = @OwnerLocationID
					)
			BEGIN
				SET @ErrorNumber = 1
				SET @ErrorMessage = 'This Owner Location already exists in the group. Information is not saved.'
				SET @RecordID = (SELECT 
									AIMOwnerLocationGroupDetailID 
								FROM 
									dbo.AIMOwnerLocationGroupDetail OLGD
								WHERE 
									OLGD.AIMOwnerLocationGroupID = @OwnerLocationGroupID and
									OLGD.AIMOwnerLocationID = @OwnerLocationID
									)
			END
		ELSE
			BEGIN
				INSERT
					dbo.AIMOwnerLocationGroupDetail
						(																					
						AIMOwnerLocationGroupID												
						, AIMOwnerLocationID												
						, UpdateStamp
						, CFCUserID		
						, IsActive
						)
				VALUES
						(
						@OwnerLocationGroupID
						, @OwnerLocationID
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
				dbo.AIMOwnerLocationGroupDetail WITH(ROWLOCK)
					SET 
						  AIMOwnerLocationGroupID = ISNULL(@OwnerLocationGroupID, AIMOwnerLocationGroupID)						
						, AIMOwnerLocationID = ISNULL(@OwnerLocationID, AIMOwnerLocationID)						
						, UpdateStamp = GETDATE()
						, CFCUserID = @CFCUserID	
						, IsActive = @IsActive
					WHERE 
						AIMOwnerLocationGroupDetailID = @OwnerLocationGroupDetailID

			SET @RecordID = @OwnerLocationGroupDetailID
		END


SELECT 
	  @RecordID AS RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END