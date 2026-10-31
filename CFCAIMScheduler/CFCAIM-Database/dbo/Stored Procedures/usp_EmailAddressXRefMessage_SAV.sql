CREATE PROCEDURE [dbo].[usp_EmailAddressXRefMessage_SAV]
			  @EmailAddressXRefMessageID	INT
			, @CFCUserID		INT = NULL
			, @EmailAddressID	INT = NULL
			, @MessageID		INT = NULL
			, @UpdateCFCUserID	INT	= NULL			
			, @EmailAddress		varchar(50) = NULL
			
AS
BEGIN

SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @EmailAddressXRefMessageID = 0
	BEGIN
		INSERT
			dbo.EmailAddressXRefMessage 
				(
				  CFCUserID
				, EmailAddressID
				, MessageID
				, UpdateCFCUserID
				, UpdateStamp				
				)
		VALUES
				(
				  @CFCUserID
				, @EmailAddressID
				, @MessageID
				, @UpdateCFCUserID
				, GETDATE()
				)
		SET @RecordID = @@IDENTITY 
	END
ELSE
	BEGIN
		UPDATE
			dbo.EmailAddressXRefMessage WITH(ROWLOCK)
				SET 
					  CFCUserID = ISNULL(@CFCUserID, CFCUserID)
					, EmailAddressID = ISNULL(@EmailAddressID, EmailAddressID)
					, MessageID = ISNULL(@MessageID, MessageID)
					, UpdateCFCUserID = ISNULL(@UpdateCFCUserID, UpdateCFCUserID)															
					, UpdateStamp = GETDATE()
					
				WHERE 
					EmailAddressXRefMessageID = @EmailAddressXRefMessageID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END