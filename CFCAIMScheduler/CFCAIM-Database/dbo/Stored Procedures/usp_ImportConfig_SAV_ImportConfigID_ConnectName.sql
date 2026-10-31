CREATE PROCEDURE [dbo].[usp_ImportConfig_SAV_ImportConfigID_ConnectName]
			  @ImportConfigID					INT
			, @ConnectName						NVARCHAR(50) = NULL
			, @CFCUserID						INT
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

BEGIN
	UPDATE
		dbo.ImportConfig WITH(ROWLOCK)
			SET 				
				ConnectName = ISNULL(@ConnectName, ConnectName)					
				, CFCUserID = ISNULL(@CFCUserID, CFCUserID)
			WHERE 
				ImportConfigID = @ImportConfigID
END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END