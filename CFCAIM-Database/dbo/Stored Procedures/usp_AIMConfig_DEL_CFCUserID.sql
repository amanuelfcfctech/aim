create PROCEDURE [dbo].[usp_AIMConfig_DEL_CFCUserID]
			  @CFCUserID						INT
		
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

DELETE FROM dbo.AIMConfig with (rowlock) 

WHERE CFCUserID = @CFCUserID

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END