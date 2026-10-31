CREATE PROCEDURE [dbo].[usp_UserDefaults_SAV]
				@UserDefaultsID			INT
			  , @CFCUserID				INT  = NULL
			  , @FormID					INT  = NULL
			  , @ControlID				INT  = NULL
			  , @ValueOnOff				BIT	 = NULL			  
			  , @UpdateUserID			INT	 = NULL

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @UserDefaultsID = 0
	BEGIN
		INSERT
			dbo.UserDefaults 
				(
				  CFCUserID
				, FormID
				, ControlID
				, ValueOnOff	
				, UpdateUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @CFCUserID
				, @FormID
				, @ControlID									
				, @ValueOnOff
				, @UpdateUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.UserDefaults WITH(ROWLOCK)
				SET 
					CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, FormID = ISNULL(@FormID,FormID)
					, ControlID = ISNULL(@ControlID,ControlID)
					, ValueOnOff = ISNULL(@ValueOnOff,ValueOnOff)
					, UpdateUserID = ISNULL(@UpdateUserID, UpdateUserID)
					, UpdateStamp = GETDATE()
					
				WHERE 
					UserDefaultsID = @UserDefaultsID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END