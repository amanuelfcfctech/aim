CREATE PROCEDURE [CFCUser].[usp_UserAccess_SAV_CFCUserID_PermissionID]
			  @UserAccessID				INT			
			, @CFCUserID				INT	
			, @PermissionID				INT			

			--8/22/18 - commented
			--, @UpdateStamp				DATETIME		= NULL

			, @UpdateUserID				INT				= NULL		



-- exec cfcUser.usp_UserAccess_SAV_CFCUserID_PermissionID 0, 2, 1, 1
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @UserAccessID = 0
	BEGIN
		IF EXISTS(SELECT 
						UserAccessID 
					FROM 
						dbo.UserAccess AU
					WHERE 
							AU.CFCUserID = @CFCUserID and 
							AU.PermissionID = @PermissionID)
				BEGIN
					SET @ErrorNumber = 1
					SET @ErrorMessage = 'The user already has this permission.'
				END
		ELSE
		  BEGIN
				INSERT
					dbo.UserAccess
						(
						  CFCUserID	
						, PermissionID								
						, UpdateStamp				
						, UpdateUserID
						)
				VALUES
						(
						  @CFCUserID
						, @PermissionID
						, GetDate()							
						, @UpdateUserID
				
						)
			SET @RecordID = @@IDENTITY 
		  END
	END
ELSE
	BEGIN
		UPDATE
			dbo.UserAccess WITH(ROWLOCK)
				SET 
					  CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, PermissionID = ISNULL(@PermissionID, PermissionID)					
					, UpdateStamp = GETDATE()							
					, UpdateUserID = ISNULL(@UpdateUserID, UpdateUserID)	
					
					
				WHERE 
					UserAccessID = @UserAccessID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END