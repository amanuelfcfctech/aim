CREATE PROCEDURE [CFCUser].[usp_CFCUser_DEL_CFCUserID]
	@CFCUserID		INT

AS
BEGIN
SET NOCOUNT ON;

UPDATE
	CFCUser.CFCUser WITH(ROWLOCK)
		SET 
			IsActive = CASE WHEN IsActive = 1 THEN
				0
			ELSE
				1
END
WHERE
	CFCUserID = @CFCUserID

	--cant delete admin
	AND CFCUserID > 1

END


