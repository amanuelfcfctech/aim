create PROCEDURE [dbo].[usp_EmailAddress_SAV]
			  @EmailAddressID	INT
			, @EmailAddress		NVARCHAR(50)	= NULL
			, @EmailIPAddress	NVARCHAR(50)	= NULL
			, @EmailUserName	NVARCHAR(25)	= NULL
			, @EmailPassword	NVARCHAR(25)	= NULL
			, @CFCUserID		INT				= NULL	
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EmailAddressID = 0
	BEGIN
		INSERT
			dbo.EmailAddress
				(
				  EmailAddress
				, EmailIPAddress
				, EmailUserName
				, EmailPassword
				, CFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @EmailAddress
				, @EmailIPAddress
				, @EmailUserName
				, @EmailPassword									
				, @CFCUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EmailAddress WITH(ROWLOCK)
				SET 
					  EmailAddress = ISNULL(@EmailAddress, EmailAddress)
					, EmailIPAddress = ISNULL(@EmailIPAddress, EmailIPAddress)
					, EmailUserName = ISNULL(@EmailUserName, EmailUserName)
					, EmailPassword = ISNULL(@EmailPassword, EmailPassword)									
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					
				WHERE 
					EmailAddressID = @EmailAddressID
			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END

