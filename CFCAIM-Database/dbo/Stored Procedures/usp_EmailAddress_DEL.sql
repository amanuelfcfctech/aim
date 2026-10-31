create PROCEDURE [dbo].[usp_EmailAddress_DEL]
				@EmailAddressID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.EmailAddress WITH(ROWLOCK)
WHERE
	EmailAddressID = @EmailAddressID

END
