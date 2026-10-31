create PROCEDURE [dbo].[usp_EmailAddressXRefMessage_DEL]
				@EmailAddressXRefMessageID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.EmailAddressXRefMessage WITH(ROWLOCK)
WHERE
	EmailAddressXRefMessageID = @EmailAddressXRefMessageID

END