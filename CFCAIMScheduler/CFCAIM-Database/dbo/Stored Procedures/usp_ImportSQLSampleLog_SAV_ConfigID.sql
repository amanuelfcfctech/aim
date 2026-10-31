CREATE PROCEDURE [dbo].[usp_ImportSQLSampleLog_SAV_ConfigID]
			  @ImportSQLSampleLogID			INT
			, @ImportConfigID				INT
			, @LastImportDate				DATETIME
			, @CreatedStamp					DATETIME
			, @CFCUserID					INT
			
AS
BEGIN
SET NOCOUNT ON;
DECLARE @IDinOtherTable INT
DECLARE @ErrorNumber INT = 0
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @ImportSQLSampleLogID > 0
BEGIN
	UPDATE
		dbo.ImportSQLSampleLog WITH(ROWLOCK)
			SET 
				  ImportConfigID = ISNULL(@ImportConfigID, ImportConfigID)
				, LastImportDate = ISNULL(@LastImportDate, LastImportDate)
				, CreatedStamp	 = ISNULL(@CreatedStamp, GetDate())
				, CFCUserID		 = ISNULL(@CFCUserID, CFCUserID)

			WHERE 
				ImportSQLSampleLogID = @ImportSQLSampleLogID
END
ELSE
BEGIN
	INSERT INTO
		dbo.ImportSQLSampleLog
			(
			  ImportConfigID
			, LastImportDate
			, CreatedStamp
			, CFCUserID
			
			)
		VALUES
			(
			  @ImportConfigID
			, @LastImportDate
			, GETDATE()
			, @CFCUserID
			)
END

SELECT 
	  @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END