create PROCEDURE [dbo].[usp_WebService_DEL]
				@WebServiceID		INT
AS
BEGIN
SET NOCOUNT ON;

DELETE
FROM 
	dbo.WebService WITH(ROWLOCK)
WHERE
	WebServiceID = @WebServiceID

END
