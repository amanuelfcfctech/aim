CREATE PROCEDURE [dbo].[usp_DataSourceTypeRefFileType_SAV]
			  @DataSourceTypeRefFileTypeID		INT
			, @DataSourceTypeID					INT = NULL
			, @FileTypeID						INT = NULL												
			, @CFCUserID						INT	= NULL
			, @IsSystem							BIT = NULL
			
AS
BEGIN

-- exec dbo.usp_DataSourceTypeRefFileType_SAV 0, 3, 6, 2, 1


SET NOCOUNT ON;
DECLARE @ErrorNumber INT = 0
DECLARE @RecordID INT = 0 
DECLARE @ErrorMessage VARCHAR(200) = ''

IF @DataSourceTypeRefFileTypeID = 0
	BEGIN
			
		IF NOT EXISTS(SELECT TOP 1	
						X.DataSourceTypeID
					FROM
						dbo.DataSourceTypeRefFileType X
					WHERE
						X.DataSourceTypeID = @DataSourceTypeID
						and X.FileTypeID = @FileTypeID)	

		Begin			

			INSERT
				dbo.DataSourceTypeRefFileType
					(
					  DataSourceTypeID
					, FileTypeID				
					, CFCUserID
					, UpdateStamp				
					, IsSystem
					)
			VALUES
					(
					  @DataSourceTypeID
					, @FileTypeID
					, @CFCUserID
					, GETDATE()
					, @IsSystem
					)
			SET @RecordID = @@IDENTITY 
		End
	END
ELSE
	BEGIN
		UPDATE
			dbo.DataSourceTypeRefFileType WITH(ROWLOCK)
				SET 
					  DataSourceTypeID = ISNULL(@DataSourceTypeID, DataSourceTypeID)
					, FileTypeID = ISNULL(@FileTypeID, FileTypeID)															
					, CFCUserID = ISNULL(@CFCUserID, CFCUserID)	
					, UpdateStamp = GETDATE()
					, IsSystem = ISNULL(@IsSystem, IsSystem)	
					
				WHERE 
					DataSourceTypeRefFileTypeID = @DataSourceTypeRefFileTypeID

			SET @RecordID = @@IDENTITY 
	END

SELECT 
	  @RecordID as RecordID
	, @ErrorNumber AS ErrorNumber
	, @ErrorMessage AS ErrorMessage

END