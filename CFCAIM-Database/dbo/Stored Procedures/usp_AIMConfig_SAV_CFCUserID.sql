CREATE PROCEDURE [dbo].[usp_AIMConfig_SAV_CFCUserID]
			  @CFCUserID						INT
			, @ActiveDatabaseID					INT
			, @IsActive							BIT
AS
BEGIN
SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''



IF NOT EXISTS(SELECT TOP 1
			A.CFCUserID
		FROM
			dbo.AIMConfig A WITH(NOLOCK)
		WHERE
				A.CFCUserID = @CFCUserID)
	BEGIN
		INSERT
			dbo.AIMConfig 
				(
					CFCUserID
				, AIMDatabaseID
				)
		VALUES
				(
					@CFCUserID
				, @ActiveDatabaseID
				)
	END
ELSE
	BEGIN
		UPDATE
			dbo.AIMConfig WITH(ROWLOCK)
				SET 
					  AIMDatabaseID = ISNULL(@ActiveDatabaseID, AIMDatabaseID)
					, IsActive = ISNULL(@IsActive, IsActive)
					, UpdateStamp = GETDATE()
				WHERE 
					CFCUserID = @CFCUserID
	END

SELECT 
	  @@IDENTITY as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END