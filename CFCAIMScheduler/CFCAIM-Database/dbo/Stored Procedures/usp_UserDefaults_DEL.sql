CREATE PROCEDURE [dbo].[usp_UserDefaults_DEL]
				@FormID					INT 
			  , @CFCUserID				INT 

AS
BEGIN
SET NOCOUNT ON;

DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

DELETE FROM
	dbo.UserDefaults WITH(ROWLOCK)
					
	WHERE 
			FormID = @FormID and
			CFCUserID = @CFCUserID


END