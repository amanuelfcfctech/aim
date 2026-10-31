CREATE PROCEDURE [CFCUser].[usp_UserMenuAccess_SAV_CFCUserID_MenuItemsID]
			  @UserMenuAccessID			INT			
			, @CFCUserID				INT	
			, @MenuItemsID				INT			
			, @UpdateUserID				INT				= NULL		
			, @IsNotAllowed				BIT		



AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @UserMenuAccessID = 0
	BEGIN
		IF EXISTS(SELECT 
						UserMenuAccessID 
					FROM 
						dbo.UserMenuAccess AU
					WHERE 
							AU.CFCUserID = @CFCUserID and 
							AU.MenuItemsID = @MenuItemsID)
				BEGIN
					SET @ErrorNumber = 1				
				END
		ELSE
		  BEGIN
				INSERT
					dbo.UserMenuAccess
						(
						  CFCUserID	
						, MenuItemsID								
						, UpdateStamp				
						, UpdateUserID
						, IsNotAllowed
						)
				VALUES
						(
						  @CFCUserID
						, @MenuItemsID
						, GetDate()							
						, @UpdateUserID
						, 1
				
						)
			SET @RecordID = @@IDENTITY 
		  END
	END
ELSE
	BEGIN
		UPDATE
			dbo.UserMenuAccess WITH(ROWLOCK)
				SET 
					  CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, MenuItemsID = ISNULL(@MenuItemsID, MenuItemsID)	
					, IsNotAllowed = ISNULL(@IsNotAllowed, IsNotAllowed)
					, UpdateStamp = GETDATE()							
					, UpdateUserID = ISNULL(@UpdateUserID, UpdateUserID)	
					
					
				WHERE 
					UserMenuAccessID = @UserMenuAccessID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END